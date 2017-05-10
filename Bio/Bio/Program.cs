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

            //DnaChain test1 = new DnaChain();
            //test1.LoadSamples("9.200-40.txt");
            //test1.SequenceMax = 209;
            //test1.GenerateRandom();
            //test1.PrintChainSummary();

            Population population = new Population();
            population.GeneratePopulation(3);
            population.PrintResult();

            population.Mutation();
       
            population.Mutation2();

            population.Mutation3();

            population.PrintResult();
            //   Console.ReadLine();

        }
    }
}
