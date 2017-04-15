using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio
{
    class Program
    {

      



        static void Main(string[] args)
        {
            Oligonukleotyd first = new Oligonukleotyd();

            first.SetCiag("ABABABABA");
            first.print();


            DNA_chain test1 = new DNA_chain();
            test1.load_samples("9.200-40.txt");

            Console.ReadLine();

        }
    }
}
