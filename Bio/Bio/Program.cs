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
            int N = 100;
            Population population = new Population();
            population.GeneratePopulation(N);
            population.PrintPopulationResult();
            //population.Mutation();
            //population.Mutation2();
            //population.Mutation3();
            //population.Crossing();
            //population.LongerChain();
            int P = 100, L=200;
            
            for (int j = 0; j < 10000; j++)
            {
                for (int i = 0; i < N*0.5; i++)
                {
                    population.Mutation();
                    population.Mutation2();
                    population.Mutation3();
                    population.Crossing();
                }
                population.LongerChain();

                if(j == P)
                {
                    L += 200;
                    P += 200;
                }
                if (j > L && j < P)
                {
                    for (int i = 0; i < N * 2; i++)
                    {
                        population.Mutation3();
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    population.Mutation();
                }
                population.LongerChain();
            
                for (int i = 0; i < N; i++)
                {
                    population.Mutation2();
                }
                population.LongerChain();

                
                population.LongerChain();

                for (int i = 0; i < N; i++)
                {
                    population.Crossing();

                }
                population.LongerChain();

                population.Selection(5);
                Console.Write("{0}\n", j);
                population.PrintBestResult();

                P++;
            }
            population.PrintPopulationResult();
            population.PrintBestResult();
            // population.PrintPopulationResult();
            Console.ReadLine();

        }
    }
}
