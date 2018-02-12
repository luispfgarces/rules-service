using System;

namespace RulesService.Domain.Models.Operators
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class OperatorDescriptionAttribute : Attribute
    {
        public OperatorDescriptionAttribute(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.Description = description;
        }

        public string Description { get; private set; }
    }
}