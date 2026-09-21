using Barbershop.Shareable.Enum;
using Barbershop.Shareable.Exceptions;
using Barbershop.Shareable.Extensions;

namespace Barbershop.Domain.Entity;

public class SchedulingEntity : BaseEntity
{
    // Um agendamento pode mudar a data ?
    public DateOnly Date { get; private set; }
    public TimeOnly Hour { get; private set; }
    public SchedulingStatus Status { get; private set; }
    public string BarberId { get; }
    public BarberEntity Barber { get; init; } = default!;
    public string UserId { get; }
    public UserEntity User { get; init; } = default!;
    public Guid ServiceId { get; }
    public ServiceEntity Service { get; init; } = default!;
    public string? Observation { get; private set; }
    public decimal Price { get; }

    private SchedulingEntity(string userId, string barberId, DateOnly date, TimeOnly hour, SchedulingStatus status, Guid serviceId, string? observation, decimal price)
    {
        if (date < DateOnly.FromDateTime(DateTimeExtensions.BrazilDateTime()))
            throw new AppException("The scheduled date cannot be earlier than the current date.", "SCHEDULING_DATE", 400);

        // validar horário, se for menor que o horário atual, não pode agendar

        UserId = userId;
        BarberId = barberId;
        Date = date;
        Hour = hour;
        Status = status;
        ServiceId = serviceId;
        Observation = observation;
        Price = price;
    }

    public static SchedulingEntity Create(string userId, string barberId, DateOnly date, TimeOnly hour, Guid serviceId, string? observation, decimal price)
        => new(userId, barberId, date, hour, SchedulingStatus.CONFIRMED, serviceId, observation, price);

    public SchedulingEntity UpdateStatus(SchedulingStatus status)
    {
        if (Status is SchedulingStatus.CANCELED or SchedulingStatus.RECUSED or SchedulingStatus.COMPLETED)
            throw new SchedulingException("Appointment status does not allow changes", "SCHEDULING_STATUS", 400);

        Status = status;
        return this;
    }
}