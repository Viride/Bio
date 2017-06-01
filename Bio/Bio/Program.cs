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
            int NmbOfTestSamples=18;

            string[] tab = new string[NmbOfTestSamples];
            tab[0] = "9.200+20" + ".txt";
            tab[1] = "9.200+80" + ".txt";
            tab[2] = "9.200-40" + ".txt";
            tab[3] = "10.500+50" + ".txt";
            tab[4] = "10.500+200" + ".txt";
            tab[5] = "10.500-100" + ".txt";
            tab[6] = "10.500-200" + ".txt";
            tab[7] = "20.300+30" + ".txt";
            tab[8] = "20.300+120" + ".txt";
            tab[9] = "53.500-200" + ".txt";
            tab[10] = "55.300-60" + ".txt";
            tab[11] = "55.300-120" + ".txt";
            tab[12] = "55.400+40" + ".txt";
            tab[13] = "55.400+160" + ".txt";
            tab[14] = "55.400-80" + ".txt";
            tab[15] = "55.400-160" + ".txt";
            tab[16] = "55.300-120" + ".txt";
            tab[17] = "144.500-12" + ".txt";

            int[] size = new int[NmbOfTestSamples];
            size[0] = 209; size[1] = 209; size[2] = 209; size[3] = 509; size[4] = 509; size[5] = 509;
            size[6] = 509; size[7] = 309; size[8] = 309; size[9] = 509; size[10] = 309; size[11] = 309;
            size[12] = 409; size[13] = 409; size[14] = 409; size[15] = 409; size[16] = 309; size[17] = 509;

            int NmbOfRepeats = 10;


            for (int k = 0; k < NmbOfTestSamples; k++) {
                int N = 50;
                Population population = new Population(size[k]);

                population.GeneratePopulation(N, tab[k], size[k]);
                population.PrintPopulationResult();

                int P = 100, L = 200;

                var watchWhole = System.Diagnostics.Stopwatch.StartNew();

                for (int j = 0; j < NmbOfRepeats; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    for (int i = 0; i < N * 0.5; i++)
                    {
                        population.Mutation();
                        population.Mutation2();
                        population.Mutation3();
                        population.LongerChain();
                        population.Crossing();
                    }
                    population.LongerChain();

                    if (j == P)
                    {
                        L += 200;
                        P += 200;
                    }
                    if (j > L && j < P)
                    {
                        for (int i = 0; i < N * 2; i++)
                        {
                            population.Mutation3();
                            population.LongerChain();
                        }
                    }
                    //population.LongerChain();
                    //population.LongerChain();

                    if (j < 300)
                    {
                        for (int i = 0; i < N; i++)
                        {
                            population.Mutation();
                            population.LongerChain();
                        }
                        //population.LongerChain();

                        for (int i = 0; i < N; i++)
                        {
                            population.Mutation2();
                            population.LongerChain();
                        }
                        //population.LongerChain();
                    }
                    else if (j < 600)
                    {
                        for (int i = 0; i < N * 2; i++)
                        {
                            population.Mutation();
                            population.LongerChain();

                        }
                        //population.LongerChain();

                        for (int i = 0; i < N * 2; i++)
                        {
                            population.Mutation2();
                            population.LongerChain();
                        }
                        //population.LongerChain();
                    }
                    else
                    {
                        for (int i = 0; i < N * 3; i++)
                        {
                            population.Mutation();
                            population.LongerChain();
                        }
                        //population.LongerChain();

                        for (int i = 0; i < N * 3; i++)
                        {
                            population.Mutation2();
                            population.LongerChain();
                        }
                        //population.LongerChain();
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
                population.SaveBestToFile("Rozw"+tab[k], elapsedMs.ToString(), NmbOfRepeats);
            }
            // population.PrintPopulationResult();
            Console.ReadLine();

        }
    }
}
