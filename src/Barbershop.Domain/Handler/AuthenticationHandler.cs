using Barbershop.Domain.Entity;
using Barbershop.Shareable.Config;
using Barbershop.Shareable.Exceptions;
using Barbershop.Shareable.Request.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OperationResult;

namespace Barbershop.Domain.Handler;

public class AuthenticationHandler
    : IRequestHandler<RegisterUserRequest, Result>,
        IRequestHandler<ForgetPasswordRequest, Result>,
        IRequestHandler<ResetPasswordRequest, Result>,
        IRequestHandler<ChangePasswordRequest, Result>,
        IRequestHandler<ConfirmEmailRequest, Result>,
        IRequestHandler<UpdateProfileRequest, Result>
{
    private readonly UserManager<UserEntity> _userManager;

    public AuthenticationHandler(UserManager<UserEntity> userManager) => _userManager = userManager;

    public async Task<Result> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var emailExists = await _userManager.FindByEmailAsync(request.Email);

        if (emailExists is not null)
            return new ApplicationException("Email already exists");

        var user = UserEntity.Create(request);

        var result = await _userManager.CreateAsync(user, request.Password);

        await _userManager.AddToRoleAsync(user, Roles.Customer);

        // TODO: enviar e-mail de confirmação
        //var tokenEmailConfirm = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return !result.Succeeded
            ? new ApplicationException("User creation failed")
            : Result.Success();
    }

    public async Task<Result> Handle(ForgetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Success();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // TODO: send email

        return Result.Success();
    }

    public async Task<Result> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new AppException("Invalid credentials", "INVALID_CREDENTIALS");

        var resetPassword = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!resetPassword.Succeeded)
            return new AppException("Invalid credentials", "INVALID_CREDENTIALS");

        return Result.Success();
    }

    public async Task<Result> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return new NotFoundException("User");

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
            return new AppException("Unable to change the password", "CHANGE_PASSWORD");

        return Result.Success();
    }

    public async Task<Result> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new NotFoundException("User");

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
            return new AppException("An error occurred while confirming the email.", "CONFIRM_EMAIL");

        return Result.Success();
    }

    public async Task<Result> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new NotFoundException("User");

        user.Update(request);

        var result = await _userManager.UpdateAsync(user);

        // TODO: enviar email confirmação e-mail caso altere o e-mail

        if (!result.Succeeded)
            return new AppException("An error occurred while update profile.", "UPDATE_PROFILE");

        return Result.Success();
    }
}