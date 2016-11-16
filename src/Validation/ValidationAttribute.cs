using System;

namespace Validation
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidationAttribute : Attribute
    {
        public long Criteria_1 { get; set; }
        public long Criteria_2 { get; set; }
    }
}
