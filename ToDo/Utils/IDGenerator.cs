using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIDisplay.Utils
{
    internal class IDGenerator
    {
        public static string genUUID()
        {
            Guid myUUId = Guid.NewGuid();
            string convertedUUID = myUUId.ToString();
            Console.WriteLine("Current TID is: " + convertedUUID);
            return convertedUUID;
        }
    }
}
