using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZDPackTool
{
    class Helper
    {
        public static string ByteArrayToString(byte[] array)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetString(array);
        }
    }
}