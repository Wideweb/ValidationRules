using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Validation
{
    public static class ValidationAggregator
    {
        public async static Task<List<KeyValuePair<TState, List<TRule>>>> Aggregate<TState, TRule, TAttribute>(
            ValidationProvider provider,
            Func<TAttribute, TState> map,
            object[] providerMethodParameters = null) where TAttribute : Attribute
        {
            var validationRules = new List<KeyValuePair<TState, List<TRule>>>();

            var rules = provider.GetType()
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(m => new {
                    Attr = m.GetCustomAttribute<TAttribute>(),
                    Method = m
                })
                .Where(it => it.Attr != null);

            foreach (var rule in rules)
            {
                var key = map(rule.Attr);
                var value = await TypeSystem.InvokeMethod<List<TRule>>(rule.Method, provider, providerMethodParameters);
                var validationRule = new KeyValuePair<TState, List<TRule>>(map(rule.Attr), value);
                validationRules.Add(validationRule);
            }

            return validationRules;
        }
    }
}
