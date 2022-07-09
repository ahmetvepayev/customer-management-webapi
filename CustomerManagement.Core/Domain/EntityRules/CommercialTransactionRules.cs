namespace CustomerManagement.Core.Domain.EntityRules;

public static class CommercialTransactionRules
{
    public const int DescriptionMaxLength = 150;
    public const int AmountMaxLength = 50;
    public const int AmountPrecision = 2;
    public static readonly string AmountMaxLengthPrecision = $"{AmountMaxLength},{AmountPrecision}";
}