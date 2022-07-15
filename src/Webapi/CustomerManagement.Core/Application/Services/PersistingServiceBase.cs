using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Application.Services;

public class PersistingServiceBase
{
    protected readonly IUnitOfWork _unitOfWork;

    public PersistingServiceBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}