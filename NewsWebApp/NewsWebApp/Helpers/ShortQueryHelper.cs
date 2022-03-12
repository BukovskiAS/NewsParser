using System.Linq;

namespace NewsWebApp.Helpers
{
    public static class ShortQueryHelper
    {
        public static IOrderedQueryable<TSource> Sort<TSource>(this IQueryable<TSource> source, string sortingProperty)
        {
            return source.OrderBy(item => item.GetReflectedPropertyValue(sortingProperty));
        }

        private static object GetReflectedPropertyValue(this object subject, string field)
        {
            return subject.GetType().GetProperty(field).GetValue(subject, null);
        }
    }
}
