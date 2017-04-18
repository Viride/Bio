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

            first.Ciag="ABABABABA";
            first.print();


            DNA_chain test1 = new DNA_chain();
            test1.load_samples("9.200-40.txt");
            test1.N_maks = 209;
            test1.generate_random();
            test1.print_chain();

            Console.ReadLine();

        }
    }
}
