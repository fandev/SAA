using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Infra
{
    public static class DnsExtension
    {
        public static bool IsFQDN(this string data)
        {
            return System.Text.RegularExpressions.Regex
                .IsMatch(data, @"(?=^.{4,255}$)(^((?!-)[a-zA-Z0-9-]{1,63}(?<!-)\.)+[a-zA-Z]{2,63}$)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}
