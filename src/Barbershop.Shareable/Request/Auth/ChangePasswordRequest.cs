using MediatR;
using OperationResult;
using System.Text.Json.Serialization;

namespace Barbershop.Shareable.Request.Auth;

public record ChangePasswordRequest(
    [property: JsonIgnore] string UserId,
    string CurrentPassword,
    string NewPassword) : IRequest<Result>;