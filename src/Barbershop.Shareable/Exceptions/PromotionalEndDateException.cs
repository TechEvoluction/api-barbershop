namespace Barbershop.Shareable.Exceptions;

public class PromotionalEndDateException : BaseException
{
    public PromotionalEndDateException(DateTime endDate) 
        : base($"A data de encerramento da promoção deve ser inferior a {endDate:dd/MM/yyyy HH:mm:ss}",
            "PROMOTIONAL_END_DATE",
            422) { }
}