using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIDisplay.Myscripts
{
    internal class MyUtils
    {
        public static string genUUID()
        {
            Guid myUUId = Guid.NewGuid();
            string convertedUUID = myUUId.ToString();
            Console.WriteLine("Current UUID is: " + convertedUUID);
            return convertedUUID;
        }
    }
}
