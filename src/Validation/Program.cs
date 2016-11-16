using Newtonsoft.Json;
using System;

namespace Validation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var validationProvider = new ValidationProvider();

            Console.WriteLine(string.Join("; ", validationProvider.GetRules(new State
            {
                Criteria_1 = 2,
                Criteria_2 = 2
            }).Result.ToArray()));

            var rules = ValidationAggregator.Aggregate<State, string, ValidationAttribute>(validationProvider, attr =>
            {
                return new State
                {
                    Criteria_1 = attr.Criteria_1,
                    Criteria_2 = attr.Criteria_2
                };
            }).Result;

            var rulesJson = JsonConvert.SerializeObject(rules, Formatting.Indented);

            Console.WriteLine(rulesJson);

            Console.ReadKey();
        }
    }
}
