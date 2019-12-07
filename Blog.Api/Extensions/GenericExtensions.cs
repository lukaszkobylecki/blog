using System;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Api.Extensions
{
    public static class GenericExtensions
    {
        public static TClass Bind<TClass, TProperty>(this TClass model, Expression<Func<TClass, TProperty>> expression, object value)
        {
            if (!(expression.Body is MemberExpression memberExpression))
                return model;

            var propertyName = memberExpression.Member.Name.ToLowerInvariant();
            var modelType = model.GetType();
            var field = modelType.GetProperties()
                .SingleOrDefault(x => x.Name.ToLowerInvariant() == propertyName);

            field?.SetValue(model, value);

            return model;
        }
    }
}
