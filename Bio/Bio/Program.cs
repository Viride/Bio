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
            population.GeneratePopulation(50);
            population.PrintPopulationResult();
            //population.Mutation();
            //population.Mutation2();
            //population.Mutation3();
            //population.Crossing();
            //population.LongerChain();
            for (int j = 0; j < 1000; j++)
            {
                for (int i = 0; i < 50; i++)
                {
                    population.Mutation();
                    population.Mutation2();
                    //population.Mutation3();
                    population.Crossing();
                    population.LongerChain();
                }
                population.Shuffle();
                population.Selection(5);
                Console.Write("{0}\n", j);
            }
            population.PrintPopulationResult();
            population.PrintPopulationResult();
            Console.ReadLine();

        }
    }
}
