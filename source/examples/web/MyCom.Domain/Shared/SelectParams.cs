using System.Linq.Expressions;

namespace MyCom.Domain.Shared;

public record struct SelectParams<T>(Expression<Func<T, bool>>? Predicate, Expression<Func<T, object>>? Sort, bool IsDesc, long Offset, int Limit);

