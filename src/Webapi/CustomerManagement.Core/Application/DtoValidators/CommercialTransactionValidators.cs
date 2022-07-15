using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.EntityRules;

namespace CustomerManagement.Core.Application.DtoValidators;

public static class CommercialTransactionValidators
{
    public static bool IsValid(this CommercialTransactionAddRequest request, Customer customer, out List<string> errors)
    {
        errors = new();
        
        if (customer == null)
        {
            errors.Add("The customer corresponding to the given CustomerId doesn't exist");
        }

        if (String.IsNullOrEmpty(request.Description))
        {
            errors.Add("Description is required");
        }
        else if (request.Description.Length > CommercialTransactionRules.DescriptionMaxLength)
        {
            errors.Add($"Description too large. Description max length: {CommercialTransactionRules.DescriptionMaxLength}");
        }

        if (!request.Amount.HasValue)
        {
            errors.Add("Amount is required");
        }

        return !errors.Any();
    }

    public static bool IsValid(this CommercialTransactionUpdateRequest request, Customer customer, out List<string> errors)
    {
        errors = new();
        
        if (customer == null)
        {
            errors.Add("The customer corresponding to the given CustomerId doesn't exist");
        }

        if (String.IsNullOrEmpty(request.Description))
        {
            errors.Add("Description is required");
        }
        else if (request.Description.Length > CommercialTransactionRules.DescriptionMaxLength)
        {
            errors.Add($"Description too large. Description max length: {CommercialTransactionRules.DescriptionMaxLength}");
        }

        if (!request.Amount.HasValue)
        {
            errors.Add("Amount is required");
        }

        return !errors.Any();
    }
}