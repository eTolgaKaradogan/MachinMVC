using System;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Business.Validations
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue, string errorMessage = "Value is not valid!") : base(errorMessage)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            return Convert.ToInt32(value) >= _minValue;
        }
    }
}
