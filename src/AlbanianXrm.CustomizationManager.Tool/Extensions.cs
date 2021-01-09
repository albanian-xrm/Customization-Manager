using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace AlbanianXrm.CustomizationManager
{
    public static class Extensions
    {
        public const string EXPRESSION_REFERS_METHOD = "Expression '{0}' refers to a method, not a property.";
        public const string EXPRESSION_REFERS_FIELD = "Expression '{0}' refers to a method, not a property.";

        public static Binding Bind<T, TSource, TProperty>(this T target, Expression<Func<T, TProperty>> targetProperty, TSource source, Expression<Func<TSource, TProperty>> sourceProperty)
            where T : IBindableComponent
            where TSource : INotifyPropertyChanged
        {
            var targetPropertyInfo = targetProperty.GetProperty();
            var sourcePropertyInfo = sourceProperty.GetProperty();
            var binding = target.DataBindings.Add(
                 targetPropertyInfo.Name,
                 source,
                 sourcePropertyInfo.Name,
                 formattingEnabled: true);

            targetPropertyInfo.SetValue(target, sourcePropertyInfo.GetValue(source));
            return binding;
        }

        public static PropertyInfo GetProperty<TObject, TProperty>(this Expression<Func<TObject, TProperty>> propertyLambda)
        {
            if (!(propertyLambda.Body is MemberExpression member))
                throw new ArgumentException(string.Format(EXPRESSION_REFERS_METHOD, propertyLambda.ToString()));

            if (!(member.Member is PropertyInfo propInfo))
                throw new ArgumentException(string.Format(EXPRESSION_REFERS_FIELD, propertyLambda.ToString()));

            return propInfo;
        }

        public static List<Entity> RetrieveAll(this IOrganizationService organizationService, QueryExpression query)
        {
            var result = new List<Entity>();
            var response = organizationService.RetrieveMultiple(query);
            if (response.Entities.Any()) result.AddRange(response.Entities);
            if(response.MoreRecords && query.PageInfo==null)
            {
                query.PageInfo = new PagingInfo()
                {
                    Count = 200,
                    PageNumber = 1
                };
            }
            while (response.MoreRecords)
            {
                query.PageInfo.PagingCookie = response.PagingCookie;
                query.PageInfo.PageNumber += 1;
                response = organizationService.RetrieveMultiple(query);
                if (response.Entities.Any()) result.AddRange(response.Entities);
            }
            return result;
        }
    }
}
