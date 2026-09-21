namespace Barbershop.Domain.Entity;

public class BarbershopOperationEntity : BaseEntity
{
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly? OpeningHours { get; private set; }
    public TimeOnly? ClosingTime { get; private set; }
    public bool BarbershopOpensOnThisDate => OpeningHours != null && ClosingTime != null;
}