using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Entity;
using Barbershop.Shareable.Config;
using Barbershop.Shareable.Exceptions;
using Barbershop.Shareable.Request.Barber;
using Barbershop.Shareable.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OperationResult;

namespace Barbershop.Domain.Handler;

public class BarberHandler
    : IRequestHandler<BarberInvitationRequest, Result<CreateBarberResponse>>,
        IRequestHandler<AcceptBarberInvitationRequest, Result>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IBarberRepository _barberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private const string INVITATION_TOKEN_PURPOSE = "BarberInvations";

    public BarberHandler(
        UserManager<UserEntity> userManager,
        IBarberRepository barberRepository,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _barberRepository = barberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateBarberResponse>> Handle(BarberInvitationRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);

        if (userExists is not null)
            return new AppException("User already exists", "USER_ALREADY_EXISTS");

        var userToCreated = UserEntity.Create(request);

        var result = await _userManager.CreateAsync(userToCreated);

        if (!result.Succeeded)
            return new AppException("Failed to create user", "FAILED_TO_CREATE_USER");

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new AppException("Failed to create user", "FAILED_TO_CREATE_USER");

        await _userManager.AddToRoleAsync(user, Roles.Barber);

        var token = await _userManager.GenerateUserTokenAsync(
            user,
            TokenOptions.DefaultProvider,
            INVITATION_TOKEN_PURPOSE);

        // send invitation email to the barber with a link to set their password (and create barber profile)

        return new CreateBarberResponse("A link has been sent to the registered email address to complete the creation of the barber profile");
    }

    public async Task<Result> Handle(AcceptBarberInvitationRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.BarberId.ToString());

        if (user is null)
            return new NotFoundException("User");

        var isValidToken = await _userManager.VerifyUserTokenAsync(
            user,
            TokenOptions.DefaultProvider,
            INVITATION_TOKEN_PURPOSE,
            request.Token);

        if (!isValidToken)
            return new AppException("Invalid token. Contact the administrator", "INVALID_TOKEN", 400);

        var addPasswordResult = await _userManager.AddPasswordAsync(user, request.Password);

        if (!addPasswordResult.Succeeded)
            return new AppException("Failed to change password", "FAILED_TO_CHANGE_PASSWORD");

        var barber = BarberEntity.Create(user.Id, request.Biography);

        _barberRepository.Add(barber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}