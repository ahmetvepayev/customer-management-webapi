using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Domain.EntityRules;

namespace CustomerManagement.Core.Application.DtoValidators;

public static class CustomerValidators
{
    public static bool IsValid(this CustomerAddRequest request, out List<string> errors)
    {
        errors = new();

        if (String.IsNullOrEmpty(request.Firstname))
        {
            errors.Add("FirstName is required");
        }
        else if (request.Firstname.Length > CustomerRules.FirstNameMaxLength)
        {
            errors.Add($"FirstName too large. FirstName max length: {CustomerRules.FirstNameMaxLength}");
        }

        if (String.IsNullOrEmpty(request.LastName))
        {
            errors.Add("LastName is required");
        }
        else if (request.LastName.Length > CustomerRules.LastNameMaxLength)
        {
            errors.Add($"LastName too large. LastName max length: {CustomerRules.LastNameMaxLength}");
        }

        if (String.IsNullOrEmpty(request.Email))
        {
            errors.Add("Email is required");
        }
        else if (request.Email.Length > CustomerRules.EmailMaxLength)
        {
            errors.Add($"Email too large. Email max length: {CustomerRules.EmailMaxLength}");
        }

        if (String.IsNullOrEmpty(request.Phone))
        {
            errors.Add("Phone is required");
        }
        else if (request.Phone.Length > CustomerRules.PhoneMaxLength)
        {
            errors.Add($"Phone too large. Phone max length: {CustomerRules.PhoneMaxLength}");
        }

        if (String.IsNullOrEmpty(request.City))
        {
            errors.Add("City is required");
        }
        else if (request.City.Length > CustomerRules.CityMaxLength)
        {
            errors.Add($"City too large. City max length: {CustomerRules.CityMaxLength}");
        }

        if (String.IsNullOrEmpty(request.Photo))
        {
            errors.Add("Photo is required");
        }

        return !errors.Any();
    }

    public static bool IsValid(this CustomerUpdateRequest request, out List<string> errors)
    {
        errors = new();

        if (String.IsNullOrEmpty(request.Firstname))
        {
            errors.Add("FirstName is required");
        }
        else if (request.Firstname.Length > CustomerRules.FirstNameMaxLength)
        {
            errors.Add($"FirstName too large. FirstName max length: {CustomerRules.FirstNameMaxLength}");
        }

        if (String.IsNullOrEmpty(request.LastName))
        {
            errors.Add("LastName is required");
        }
        else if (request.LastName.Length > CustomerRules.LastNameMaxLength)
        {
            errors.Add($"LastName too large. LastName max length: {CustomerRules.LastNameMaxLength}");
        }

        if (String.IsNullOrEmpty(request.Email))
        {
            errors.Add("Email is required");
        }
        else if (request.Email.Length > CustomerRules.EmailMaxLength)
        {
            errors.Add($"Email too large. Email max length: {CustomerRules.EmailMaxLength}");
        }

        if (String.IsNullOrEmpty(request.Phone))
        {
            errors.Add("Phone is required");
        }
        else if (request.Phone.Length > CustomerRules.PhoneMaxLength)
        {
            errors.Add($"Phone too large. Phone max length: {CustomerRules.PhoneMaxLength}");
        }

        if (String.IsNullOrEmpty(request.City))
        {
            errors.Add("City is required");
        }
        else if (request.City.Length > CustomerRules.CityMaxLength)
        {
            errors.Add($"City too large. City max length: {CustomerRules.CityMaxLength}");
        }

        if (String.IsNullOrEmpty(request.Photo))
        {
            errors.Add("Photo is required");
        }

        return !errors.Any();
    }
}