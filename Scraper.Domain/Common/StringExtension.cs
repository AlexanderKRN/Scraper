using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Domain.Common
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string? stringExtended)
        {
            return string.IsNullOrWhiteSpace(stringExtended);
        }
    }
}
