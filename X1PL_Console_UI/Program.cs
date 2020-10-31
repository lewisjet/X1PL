using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using X1PL_Backend;

namespace X1PL_Console_UI
{
   public class StringArray
    {
        public string[] Connections { get; set; }
    }
    class Program
    {
        

        static void Main(string[] args)
        {
            new X1PL_Easy(args[0]);
        }

    }
}
