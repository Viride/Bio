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



        }
    }
}
