using System;
using System.Linq.Expressions;
using Ardalis.Specification;

namespace Solicitudes.Application.Specifications;

public class GenericSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    public GenericSpecification(Expression<Func<TEntity, bool>> condicionWhere)
    {
        Query.Where(condicionWhere);
    }
}
