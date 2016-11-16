using System.Collections.Generic;
using System.Threading.Tasks;

namespace Validation
{
    public class ValidationProvider
    {
        public Task<List<string>> GetRules(State state)
        {
            var method = Nominator.NominateByAttribute<ValidationAttribute>(GetType(), attr =>
            {
                long matchingRate = 0;

                if (attr.Criteria_1 == state.Criteria_1)
                {
                    matchingRate++;
                }

                if (state.Criteria_1 == 2 && attr.Criteria_2 == state.Criteria_2)
                {
                    matchingRate++;
                }

                return matchingRate;
            });

            return TypeSystem.InvokeMethod<List<string>>(method, this, null);
        }

        [Validation(Criteria_1 = 1)]
        private List<string> GetRule_1()
        {
            return new List<string> { "Rule 1" };
        }

        [Validation(Criteria_1 = 2, Criteria_2 = 1)]
        private List<string> GetRule_2()
        {
            return new List<string> { "Rule 2", "Rule 2.2" };
        }

        [Validation(Criteria_2 = 1)]
        private List<string> GetRule_3()
        {
            return new List<string> { "Rule 3" };
        }

        [Validation(Criteria_1 = 2, Criteria_2 = 2)]
        private Task<List<string>> GetRule_4()
        {
            return Task.FromResult(new List<string> { "Rule 4" });
        }
    }
}
