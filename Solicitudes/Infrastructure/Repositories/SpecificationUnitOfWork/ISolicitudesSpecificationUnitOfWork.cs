using System;
using Solicitudes.Domain.Entities;
using Solicitudes.Infrastructure.Repositories.SolicitudesSpecification;

namespace Solicitudes.Infrastructure.Repositories.SpecificationUnitOfWork;

public interface ISolicitudesSpecificationUnitOfWork : IDisposable
{

    public IRepository<SolicitudUsuario> _solicitudRepository { get; }

    Task<int> SaveChangesAsync();

}
