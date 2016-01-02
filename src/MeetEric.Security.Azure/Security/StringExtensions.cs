using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MeetEric.Security
{
    public static class StringExtensions
    {
        public static SecureString AsSecureString(this string value)
        {
            var secure = new SecureString();
            foreach (var c in value)
            {
                secure.AppendChar(c);
            }

            secure.MakeReadOnly();
            return secure;
        }
    }
}
