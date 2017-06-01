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
            int P = 100, L=200;

            var watchWhole = System.Diagnostics.Stopwatch.StartNew();
            
            for (int j = 0; j < 100; j++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
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
                population.LongerChain();

                if (j < 300)
                {
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
                }
                else if (j < 600)
                {
                    for (int i = 0; i < N*2; i++)
                    {
                        population.Mutation();
                    }
                    population.LongerChain();

                    for (int i = 0; i < N*2; i++)
                    {
                        population.Mutation2();
                    }
                    population.LongerChain();
                }
                else
                {
                    for (int i = 0; i < N*3; i++)
                    {
                        population.Mutation();
                    }
                    population.LongerChain();

                    for (int i = 0; i < N*3; i++)
                    {
                        population.Mutation2();
                    }
                    population.LongerChain();
                }


                for (int i = 0; i < N; i++)
                {
                    population.Crossing();

                }
                population.LongerChain();

                population.Selection(5);
                watch.Stop();
                var elapsedMsOneTick = watch.Elapsed;
                Console.Write("{0}\n", j);
                population.PrintBestResult();
                Console.WriteLine("Czas: {0}\n", elapsedMsOneTick);

                P++;
            }
            watchWhole.Stop();
            var elapsedMs = watchWhole.Elapsed;
            population.PrintPopulationResult();
            population.PrintBestResult();
            Console.Write("Czas: {0}\n", elapsedMs);
            population.SaveBestToFile("Rozw1.txt");
            // population.PrintPopulationResult();
            Console.ReadLine();

        }
    }
}
