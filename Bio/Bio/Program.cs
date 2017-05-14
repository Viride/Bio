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
            population.GeneratePopulation(40);
            population.PrintPopulationResult();

            //population.Mutation();
            //population.Mutation2();
            //population.Mutation3();
            //population.Crossing();

            for (int j = 0; j < 2000; j++)
            {
                for (int i = 0; i < 40; i++) 
                {
                    //population.Mutation();
                    population.Mutation2();
                    //Console.Write("Mutatacj2  ");
                    population.Mutation3();
                    //Console.Write("Mutatacj3  ");
                    population.Crossing();
                    //Console.Write("Krzyżowanie  ");
                    population.LongerChain();
                    //Console.Write("LongerChain\n");
                }
                population.Shuffle();
                population.Selection();
                Console.Write("{0}\n", j);
            }

            population.PrintPopulationResult();
            Console.ReadLine();

        }
    }
}
