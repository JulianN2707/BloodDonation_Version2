using System;
using Usuarios.Infrastructure.Context;

namespace Usuarios.Infrastructure.Repositories.SpecificationUnitOfWork;

public class UsuariosSpecificationUnitOfWork : IUsuariosSpecificationUnitOfWork
{
    private readonly UsuariosContext _usuariosContext;

    public UsuariosSpecificationUnitOfWork(UsuariosContext usuariosContext)
    {
        _usuariosContext = usuariosContext;
    }

    public async Task BeginTransaction()
        {
            await _usuariosContext.Database.BeginTransactionAsync();
        }

        public async Task Rollback()
        {
            await _usuariosContext.Database.RollbackTransactionAsync();
        }

        public async Task<int> Save() => await _usuariosContext.SaveChangesAsync();

        public void Dispose()
        {
            _usuariosContext.Dispose();
        }

}
