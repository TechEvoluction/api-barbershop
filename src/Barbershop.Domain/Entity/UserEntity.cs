using Barbershop.Shareable.Enum;
using Barbershop.Shareable.Request.Auth;
using Microsoft.AspNetCore.Identity;

namespace Barbershop.Domain.Entity;

public class UserEntity : IdentityUser
{
    public string CPF { get; init; } = string.Empty;
    public string Fullname { get; private set; } = string.Empty;
    public DateOnly DateOfBirth { get; private set; }
    public Gender? Gender { get; private set; }
    public bool IsActive { get; private set; } = true;
    public int Age => DateTime.UtcNow.AddHours(-3).Year - DateOfBirth.Year;

    public static UserEntity Create(RegisterUserRequest request)
        => new()
        {
            Email = request.Email,
            UserName = request.Email,
            CPF = request.Cpf,
            Fullname = request.Fullname,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber
        };

    public UserEntity Update(UpdateProfileRequest request)
    {
        var changedEmail = !string.IsNullOrWhiteSpace(request.NewEmail) && request.NewEmail != Email;
        var changedPhone = !string.IsNullOrWhiteSpace(request.PhoneNumber) && request.PhoneNumber != PhoneNumber;

        if (changedEmail)
        {
            UserName = request.NewEmail;
            Email = request.NewEmail;
            NormalizedEmail = request.NewEmail!.ToUpper();
            NormalizedUserName = request.NewEmail!.ToUpper();
            EmailConfirmed = false;
        }

        if (changedPhone)
        {
            PhoneNumber = request.PhoneNumber;
            PhoneNumberConfirmed = false;
        }

        return this;
    }
}