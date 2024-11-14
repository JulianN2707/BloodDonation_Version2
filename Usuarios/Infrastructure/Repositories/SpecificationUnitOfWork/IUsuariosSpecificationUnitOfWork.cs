using System;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Repositories.UsuariosSpecification;

namespace Usuarios.Infrastructure.Repositories.SpecificationUnitOfWork;

public interface IUsuariosSpecificationUnitOfWork : IDisposable
{
    public IRepository<Usuario> _usuarioRepository { get; }
    Task<int> SaveChangesAsync();

}
