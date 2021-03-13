using System;
using System.Linq;
using System.Linq.Expressions;

namespace RIG.Shared.Domain
{
    public class OrderBy<TSource>
    {
        public Expression<Func<TSource, object>> By { get; }
        public OrderByTypes OrderByType { get; }

        private OrderBy(Expression<Func<TSource, object>> by, OrderByTypes orderByType)
        {
            By = by;
            OrderByType = orderByType;
        }

        public IQueryable<TSource> Apply(IQueryable<TSource> sources)
        {
            sources = OrderByType switch
                      {
                          OrderByTypes.Asc => sources.OrderBy(By),
                          OrderByTypes.Desc => sources.OrderByDescending(By),
                          _ => throw new ArgumentOutOfRangeException()
                      };

            return sources;
        }

        public static OrderBy<TSource> Asc(Expression<Func<TSource, object>> by)
        {
            OrderBy<TSource> orderBy = new OrderBy<TSource>(by, OrderByTypes.Asc);
            return orderBy;
        }

        public static OrderBy<TSource> Desc(Expression<Func<TSource, object>> by)
        {
            OrderBy<TSource> orderBy = new OrderBy<TSource>(by, OrderByTypes.Desc);
            return orderBy;
        }
    }

    public enum OrderByTypes
    {
        Asc = 1,
        Desc = 2
    }
}