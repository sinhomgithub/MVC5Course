using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC5Course.Models
{
    internal class TwoSpaceRuleAttribute : DataTypeAttribute
    {
        public TwoSpaceRuleAttribute() :base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            string str = (string)value;

            var b = str.Count(p => p == ' ') == 2;

            return b;
        }
        
    }
}