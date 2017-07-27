using Pattern.DataContext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Services.Extension
{
    public static class CopyToExtension
    {
        public static void CopyTo<TEntity>(this TEntity source, TEntity destination) where TEntity : class
        {

            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");

            Type destT = destination.GetType();
            Type srcT = source.GetType();

            var properties = new List<PropertyInfo>();

            destT.GetProperties().ToList().ForEach(p =>
            {
                if (p.GetCustomAttributes(false).Any(attr => attr.GetType() == typeof(CloneableAttribute))) properties.Add(p);
            });

            var results = from p in properties
                          let tp = srcT.GetProperty(p.Name)
                          where p.CanRead && tp != null &&
                          (tp.GetSetMethod(true) != null && !tp.GetSetMethod(true).IsPrivate) &&
                          (tp.GetSetMethod().Attributes & MethodAttributes.Static) == 0 &&
                          tp.PropertyType.IsAssignableFrom(p.PropertyType)
                          select new { sourceProperty = p, targetProperty = tp };

            foreach (var p in results)
            {
                p.targetProperty.SetValue(destination, p.sourceProperty.GetValue(source, null), null);
            }
        }
    }
}
