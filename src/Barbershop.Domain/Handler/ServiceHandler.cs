using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Entity;
using Barbershop.Shareable.Exceptions;
using Barbershop.Shareable.Extensions;
using Barbershop.Shareable.Request;
using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Domain.Handler;

internal class ServiceHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateServiceRequest, Result>,
        IRequestHandler<UpdateServiceRequest, Result<ServiceResponse>>,
        IRequestHandler<GetServiceRequest, Result<ServiceResponse>>,
        IRequestHandler<GetServicesRequest, Result<ServiceResponse[]>>,
        IRequestHandler<ActivateServiceRequest, Result>,
        IRequestHandler<DeactivateServiceRequest, Result>,
        IRequestHandler<ServiceOnSaleRequest, Result>
{
    public async Task<Result> Handle(CreateServiceRequest request, CancellationToken cancellationToken)
    {
        var service = ServiceEntity.Create(request);

        serviceRepository.Add(service);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<ServiceResponse>> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetByIdAsync(request.Id, cancellationToken);

        if (service is null)
            return new NotFoundException("Serviço");

        if (service.IsPromotional && request.Price != service.Price)
            return new AppException(message: "O serviço está em promoção e o preço não pode ser modificado", code: "SERVICE_PROMOTIONAL");

        if (!service.IsPromotional)
            service.RemoveServiceFromSale();

        service.Update(request);

        serviceRepository.Update(service);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToResponse(service);
    }

    public async Task<Result<ServiceResponse>> Handle(GetServiceRequest request, CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetForReadingAsync(request.Id, cancellationToken);

        if (service is null)
            return new NotFoundException("Serviço");

        return MapToResponse(service);
    }

    public async Task<Result<ServiceResponse[]>> Handle(GetServicesRequest request, CancellationToken cancellationToken)
    {
        var services = await serviceRepository.GetAllAsync(cancellationToken);

        return services.Select(MapToResponse).ToArray();
    }

    public async Task<Result> Handle(ActivateServiceRequest request, CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetByIdAsync(request.Id, cancellationToken);

        if (service is null)
            return new NotFoundException("Serviço");

        service.ActivateService();

        serviceRepository.Update(service);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(DeactivateServiceRequest request, CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetByIdAsync(request.Id, cancellationToken);

        if (service is null)
            return new NotFoundException("Serviço");

        service.DeactivateService();

        serviceRepository.Update(service);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(ServiceOnSaleRequest request, CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetByIdAsync(request.Id, cancellationToken);

        if (service is null)
            return new NotFoundException("Serviço");

        if (service.IsActive is false)
            return new AppException("O serviço não pode ser colocado em promoção antes da ativação", "INACTIVE_SERVICE");

        service.PutServiceOnSale(request);

        serviceRepository.Update(service);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static ServiceResponse MapToResponse(ServiceEntity service)
        => new ServiceResponse(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            (int)service.Duration,
            service.Duration.GetDescription(),
            service.IsPromotional ? service.PromotionalPrice : null,
            service.IsPromotional ? service.PromotionalPriceEndDate : null,
            service.IsActive,
            service.Image
        );
}