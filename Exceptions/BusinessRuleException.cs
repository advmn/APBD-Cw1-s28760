namespace APBD_Cw1_s28760.Exceptions;

public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message) { }
}