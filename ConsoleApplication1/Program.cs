using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] test = new byte[10];
            for(int i=0; i < 10; i++)
            {
                Console.WriteLine(test[i]);
            }
            Console.ReadKey();
        }
    }
}
