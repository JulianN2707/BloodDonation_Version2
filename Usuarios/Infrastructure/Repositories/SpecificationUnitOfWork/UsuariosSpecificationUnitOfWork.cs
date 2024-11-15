using System;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Context;
using Usuarios.Infrastructure.Repositories.UsuariosSpecification;

namespace Usuarios.Infrastructure.Repositories.SpecificationUnitOfWork;

public class UsuariosSpecificationUnitOfWork : IUsuariosSpecificationUnitOfWork
{
    private readonly UsuariosContext _usuariosContext;
    public IRepository<Usuario> _usuarioRepository { get; private set; }

    public UsuariosSpecificationUnitOfWork(UsuariosContext usuariosContext,IRepository<Usuario> usuarioRepository)
    {
        _usuariosContext = usuariosContext;
        _usuarioRepository = usuarioRepository;
    }

    public void Dispose()
    {
        _usuariosContext.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _usuariosContext.SaveChangesAsync();
    }
    

}
