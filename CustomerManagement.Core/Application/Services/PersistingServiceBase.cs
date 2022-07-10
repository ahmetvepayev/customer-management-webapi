using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Application.Services;

public class PersistingServiceBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PersistingServiceBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}