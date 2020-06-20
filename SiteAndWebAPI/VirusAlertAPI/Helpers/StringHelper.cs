using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirusAlertAPI.Helpers
{
    public static class StringHelper
    {
        public static string Combine(string part1, string part2)
        {
            if (part1.EndsWith("/"))
            {
                return part1 + part2;
            }
            else
            {
                return part1 + "/" + part2;
            }
        }
    }
}