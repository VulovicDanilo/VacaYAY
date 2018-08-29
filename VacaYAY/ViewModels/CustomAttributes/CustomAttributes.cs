using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VacaYAY.ViewModels.CustomAttributes
{
    public class MaxValue:ValidationAttribute
    {
        private readonly int _maxValue;

        public MaxValue(int maxValue)
        {
            _maxValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value <= _maxValue;
        }
    }
}