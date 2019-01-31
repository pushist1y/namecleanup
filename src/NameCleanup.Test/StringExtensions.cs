using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NameCleanup.Test
{
    public static class StringExtensions
    {
        public static string NameCleanup(this string input)
        {
            var result = input;

            result = result.Split(" + ").First();

            result = Regex.Replace(result, @"((\d+\s*)?[xà]\s*)?\d+\s+(stuks?|paar|crackers|Zakjes|patches|Bruistabletten|Luierbroekjes|Navulmesjes|Maaltijdzakjes|Zuigtabletten|Smelttabletjes|Wasbeurten|Capsules|Rollen|Koekjes|Plakken|Luiers|Wegwerpbare\s+Zwemluiers),?", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"(\d+(,\s*\d+)?\s*cm\s*[xØ]\s*)?\d+(,\s*\d+)?\s*cm$", "");
            result = Regex.Replace(result, @"\s+((\d+\s*)?x\s*)?(\d+[,-]\s*)?\d+\s*(kg|g|cl|ml|cm|mm|m\b|cm|l\b|liter)(?![\/])", "", RegexOptions.IgnoreCase);

            result = Regex.Replace(result, @"ca\.", "");
            result = Regex.Replace(result, @"\d+\s*x\s*$", "");
            result = Regex.Replace(result, @"\s+x\d+$", "");
            result = Regex.Replace(result, @"stuks?", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"Krat Bier", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"Krat", "", RegexOptions.IgnoreCase);
            
            result = result.Trim();
            result = result.TrimEnd(',');
            result = result.Trim();

            return result;
        }
    }
}
