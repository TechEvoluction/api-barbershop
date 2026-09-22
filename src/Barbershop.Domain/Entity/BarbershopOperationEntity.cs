namespace Barbershop.Domain.Entity;

public class BarbershopOperationEntity : BaseEntity
{
    public DayOfWeek DayOfWeek { get; init; }
    public TimeOnly? OpeningHours { get; private set; }
    public TimeOnly? ClosingTime { get; private set; }
    public DateOnly? Date { get; private set; }
    public bool BarbershopOpensOnThisDate => OpeningHours != null && ClosingTime != null;

    internal BarbershopOperationEntity(DateOnly? date, DayOfWeek dayOfWeek, TimeOnly? openingHours, TimeOnly? closingTime)
    {
        Date = date;
        DayOfWeek = dayOfWeek;
        OpeningHours = openingHours;
        ClosingTime = closingTime;
    }

    public static BarbershopOperationEntity Create(DateOnly? date, DayOfWeek? dayOfWeek, TimeOnly? openingHours, TimeOnly? closingTime)
    {
        if (openingHours is not null && closingTime is null
            || openingHours is null && closingTime is not null)
            throw new ArgumentNullException("Invalid operation hours");

        if (date is null && dayOfWeek is null)
            throw new ArgumentNullException("It is necessary to define the day of the week when a date is not specified");

        if (date.HasValue)
            dayOfWeek = date.Value.DayOfWeek;

        return new(date, dayOfWeek!.Value, openingHours, closingTime);
    }

    public void UpdateOperationHours(TimeOnly? openingHours, TimeOnly? closingTime)
    {
        if (openingHours is not null && closingTime is null
            || openingHours is null && closingTime is not null)
            throw new ArgumentNullException("Invalid operation hours");

        OpeningHours = openingHours;
        ClosingTime = closingTime;
    }
}