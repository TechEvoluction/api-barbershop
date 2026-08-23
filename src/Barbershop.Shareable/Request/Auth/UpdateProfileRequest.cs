using MediatR;
using OperationResult;
using System.Text.Json.Serialization;

namespace Barbershop.Shareable.Request.Auth;

public record UpdateProfileRequest(
    [property: JsonIgnore] string Email,
    string? NewEmail,
    string? PhoneNumber) : IRequest<Result>;