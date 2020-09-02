﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoruchyHlaseni.Model
{
    class LogClass
    {
        public void writeLog(string ex)
        {
            using (StreamWriter sw = new StreamWriter(@"Log_Hlaseni.txt", true))
            {
                string dateTime = DateTime.Now.ToString();
                sw.WriteLine(dateTime + " - " + ex);
                sw.Flush();
            }
        }
    }
}
