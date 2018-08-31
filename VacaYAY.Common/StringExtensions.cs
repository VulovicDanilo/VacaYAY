using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacaYAY.Common
{
    public static class StringExtensions
    {
        public static bool CaseInsensitiveContains(this string one,string two,StringComparison comp=StringComparison.OrdinalIgnoreCase)
        {
            return one.IndexOf(two, comp) >= 0;
        }
    }
}
