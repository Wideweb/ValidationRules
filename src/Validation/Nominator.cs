using System;
using System.Linq;
using System.Reflection;

namespace Validation
{
    public static class Nominator
    {
        public static MethodInfo NominateByAttribute<TAttribute>(Type type, Func<TAttribute, long> matchingRateFunc) where TAttribute : Attribute
        {
            var candidates = type
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(m =>
            {
                var attr = m.GetCustomAttribute<TAttribute>();

                return attr == null
                    ? new { Rate = -1L, Method = m }
                    : new { Rate = matchingRateFunc(attr), Method = m };
            })
            .Where(it => it.Rate > 0)
            .ToList();

            if (!candidates.Any())
            {
                throw new Exception("No one method matches passed parameters");
            }

            var maxMatchingRate = candidates.Max(it => it.Rate);

            if (candidates.Count(it => it.Rate == maxMatchingRate) > 1)
            {
                throw new Exception("More than 1 methods match passed parameters");
            }

            return candidates.First(it => it.Rate == maxMatchingRate).Method;
        }
    }
}
