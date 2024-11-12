using System;
using Solicitudes.Domain.Entities;
using Solicitudes.Infrastructure.Context;
using Solicitudes.Infrastructure.Repositories.SolicitudesSpecification;

namespace Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

public class SolicitudesSpecificationUnitOfWork : ISolicitudesSpecificationUnitOfWork
{
    private readonly SolicitudesContext _solicitudesContext;
    public IRepository<SolicitudUsuario> _solicitudRepository { get; private set; }

    public SolicitudesSpecificationUnitOfWork(SolicitudesContext solicitudesContext,
    IRepository<SolicitudUsuario> solicitudRepository)
    {
        _solicitudesContext = solicitudesContext;
        _solicitudRepository = solicitudRepository;
    }

    public void Dispose()
    {
        _solicitudesContext.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _solicitudesContext.SaveChangesAsync();
    }





}
