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
            int N = 50;
            Population population = new Population();
            population.GeneratePopulation(N);
            population.PrintPopulationResult();
            //population.Mutation();
            //population.Mutation2();
            //population.Mutation3();
            //population.Crossing();
            //population.LongerChain();
            for (int j = 0; j < 5000; j++)
            {
                for (int i = 0; i < N*2; i++)
                {
                    population.Mutation();
                    population.Mutation2();
                    population.Mutation3();
                    population.Crossing();
                }
                population.LongerChain();

                for (int i = 0; i < N*2; i++)
                {
                    population.Mutation();
                }
                population.LongerChain();
            
                for (int i = 0; i < N* 1.5; i++)
                {
                    population.Mutation2();
                }
                population.LongerChain();

                for (int i = 0; i < N*3; i++)
                {
                    population.Mutation3();
                }
                population.LongerChain();

                for (int i = 0; i < N*1.5; i++)
                {
                    population.Crossing();

                }
                population.LongerChain();

                population.Selection(5);
                Console.Write("{0}\n", j);
                population.PrintBestResult();
            }
            population.PrintPopulationResult();
            population.PrintBestResult();
            // population.PrintPopulationResult();
            Console.ReadLine();

        }
    }
}
