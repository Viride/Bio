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
            int NmbOfTestSamples=29;

            string[] tab = new string[NmbOfTestSamples];
            tab[1] = "144.500-12" + ".txt";
            tab[6] = "9.200-40" + ".txt";
            tab[2] = "9.200-80" + ".txt";
            tab[3] = "10.500-100" + ".txt";
            tab[4] = "10.500-200" + ".txt";
            tab[5] = "18.200-40" + ".txt";
            tab[0] = "18.200-80" + ".txt";
            tab[7] = "20.300-60" + ".txt";
            tab[8] = "20.300-120" + ".txt";
            tab[9] = "25.500-100" + ".txt";
            tab[10] = "25.500-200" + ".txt";
            tab[11] = "28.500-18" + ".txt";
            tab[12] = "34.500-32" + ".txt";
            tab[13] = "35.200-40" + ".txt";
            tab[14] = "35.200-80" + ".txt";
            tab[15] = "53.500-100" + ".txt";
            tab[16] = "53.500-200" + ".txt";
            tab[17] = "55.300-60" + ".txt";
            tab[18] = "55.300-120" + ".txt";
            tab[19] = "55.400-80" + ".txt";
            tab[20] = "55.400-160" + ".txt";
            tab[21] = "58.300-60" + ".txt";
            tab[22] = "58.300-120" + ".txt";
            tab[23] = "59.500-2" + ".txt";
            tab[24] = "62.400-80" + ".txt";
            tab[25] = "62.400-160" + ".txt";
            tab[26] = "68.400-80" + ".txt";
            tab[27] = "68.400-160" + ".txt";
            tab[28] = "113.500-8" + ".txt";


            int[] size = new int[NmbOfTestSamples];
            size[1] = 509; size[6] = 209; size[2] = 209; size[3] = 509; size[4] = 509; size[5] = 209;
            size[0] = 209; size[7] = 309; size[8] = 309; size[9] = 509; size[10] = 509; size[11] = 509;
            size[12] = 509; size[13] = 209; size[14] = 209; size[15] = 509; size[16] = 509; size[17] = 309;
            size[18] = 309; size[19] = 409; size[20] = 409; size[21] = 309; size[22] = 309; size[23] = 509;
            size[24] = 409; size[25] = 409; size[26] = 409; size[27] = 409; size[28] = 509;

            int NmbOfRepeats = 500;
            int N = 100;

            for (int k = 0; k < 1; k++) 
            //for (int k = 2 ; k < NmbOfTestSamples; k++)
            {

                Population population = new Population(size[k]);
                System.IO.StreamWriter file = System.IO.File.CreateText("Rozw" + tab[k]);
                file.WriteLine("Nr\tMaxOlig\tTime\tScore");

                population.GeneratePopulation(N, tab[k], size[k]);
                //population.PrintPopulationResult();

                int P = 100, L = 200;

                var watchWhole = System.Diagnostics.Stopwatch.StartNew();

                Console.WriteLine("{0}", tab[k]);

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
                    //population.PrintBestResult();
                    //Console.WriteLine("Czas: {0}\n", elapsedMsOneTick);

                    P++;
                    //population.SaveIterationToFile(elapsedMsOneTick.ToString(), j, file);
                }
                watchWhole.Stop();
                var elapsedMs = watchWhole.Elapsed;
                //population.PrintPopulationResult();
                population.PrintBestResult();
                Console.Write("Czas: {0}\n", elapsedMs);
                population.SaveBestToFile(elapsedMs.ToString(), NmbOfRepeats, file);
                file.Close();
                Console.WriteLine("Skończono {0}", tab[k]);
            }
            // population.PrintPopulationResult();
            Console.ReadLine();

        }
    }
}
