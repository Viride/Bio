using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio
{
    public class Population
    {
        //  Wydaje mi się, że najłatwiej będzie pracować na listach, bo można swobodnie do nich dodawać i odejmować, a do elementów
        //  można się odwoływać jak w tablicy
        private List<DnaChain> population; 

        public Population()
        {
            population = new List<DnaChain>();
        }

        public void GeneratePopulation(int rozmiar)
        {
            DnaChain sample_base = new DnaChain();
           // sample_base = new DnaChain();
            sample_base.LoadSamples("9.200-40.txt", 209);

            for (int i = 0; i < rozmiar; i++)
            {
                DnaChain temp = new DnaChain();

                temp.SampleOligs = sample_base.SampleOligs;
                temp.SequenceMax = sample_base.SequenceMax;
                temp.GenerateRandom();
                population.Add(temp);

            }
        }

        public void PrintResult()
        {
            for (int i = 0; i < population.Count(); i++)
            {
               // population[i].PrintChainSummary();
                population[i].PrintChain();
                population[i].CheckSum();

            }
            Console.WriteLine("Finito\n");
        }



        //  Nowo powstałe rozwiązania (mutacja, krzyżowanie) dodajemy do populacji a na koniec 
        //  selekcja je oczyści i zostawi tylko porządaną liczbę


        //Mutacja - skrócenie końcówki mutacji
        public void Mutation()
        {
            int mutationCounter = 0;
            int canMut = 0;
           
            while (mutationCounter < 3 && canMut == 0)
            {
                Random rand = new Random();
                int size = population.Count();
                // Console.WriteLine("ile w populacji: {0}", size);

                int chosenSolution = rand.Next(size);   //losuje rozwiązanie z populacji do mutacji
                population[chosenSolution].PrintChain();  //wypisanie wylosowanego rozwiązania do mutacji
                int solutionSize = population[chosenSolution].StringOfOlig.Count();
                Console.WriteLine("ile w rozwiązaniu: {0}", solutionSize);



                int chosenOlig1 = rand.Next(solutionSize);
                int chosenOlig2 = rand.Next(solutionSize);
                while (chosenOlig1 == chosenOlig2)
                {
                    chosenOlig2 = rand.Next(size);
                }
                Console.WriteLine("Wylosowane oligo do zamiany: {0} {1}", chosenOlig1, chosenOlig2);

                /////////////////////////////////////////
                DnaChain temp = new DnaChain();
              
                //Console.WriteLine("ile w rozwiązaniu: {0}", temp.StringOfOlig.Count()); 
                temp.SequenceLength = population[chosenSolution].SequenceLength;
                temp.SequenceMax = population[chosenSolution].SequenceMax;
             
               for(int i = 0; i < population[chosenSolution].SampleOligs.Count(); i++)
                {
                    Oligonukleotyd pom1 = new Oligonukleotyd();
                    pom1.Ciag = population[chosenSolution].SampleOligs[i].Ciag;
                    pom1.ID = population[chosenSolution].SampleOligs[i].ID;
                    pom1.PrevOligonukleotid = population[chosenSolution].SampleOligs[i].PrevOligonukleotid;
                    pom1.NextOligonukleotid = population[chosenSolution].SampleOligs[i].NextOligonukleotid;

                    temp.SampleOligs.Add(pom1);
                }
                for (int i = 0; i < population[chosenSolution].StringOfOlig.Count(); i++)
                {
                    Oligonukleotyd pom2 = new Oligonukleotyd();
                    pom2.Ciag = population[chosenSolution].StringOfOlig[i].Ciag;
                    pom2.ID = population[chosenSolution].StringOfOlig[i].ID;
                    pom2.PrevOligonukleotid = population[chosenSolution].StringOfOlig[i].PrevOligonukleotid;
                    pom2.NextOligonukleotid = population[chosenSolution].StringOfOlig[i].NextOligonukleotid;

                    temp.StringOfOlig.Add(pom2);

                }


                temp.SwapOlig(chosenOlig1, chosenOlig2);

                if (temp.SequenceLength > temp.SequenceMax)
                {
                    canMut = 0;
                }
                else
                {
                    canMut = 1;
                    population.Add(temp);
                }
                mutationCounter++;

                if (canMut == 1)
                {
                    Console.WriteLine("Mutation done!");
                    int x = population.Count();
                    population[x - 1].PrintChain();
                }
            }


        }

        //Mutacja
        public void Mutation2()
        {
            int mutationCounter = 0;
            int canMut = 0;
            
            while (mutationCounter < 3 && canMut == 0)
            {
                Random rand = new Random();
                int size = population.Count();
                // Console.WriteLine("ile w populacji: {0}", size);

                int chosenSolution = rand.Next(size);   //losuje rozwiązanie z populacji do mutacji
                population[chosenSolution].PrintChain();  //wypisanie wylosowanego rozwiązania do mutacji
                                                          //   int solutionSize = population[chosenSolution].StringOfOlig.Count();
                                                          //   Console.WriteLine("ile w rozwiązaniu: {0}", solutionSize);


                /////////////////////////////////////////
                DnaChain temp = new DnaChain();

                //Console.WriteLine("ile w rozwiązaniu: {0}", temp.StringOfOlig.Count()); 
                temp.SequenceLength = population[chosenSolution].SequenceLength;
                temp.SequenceMax = population[chosenSolution].SequenceMax;

                for (int i = 0; i < population[chosenSolution].SampleOligs.Count(); i++)
                {
                    Oligonukleotyd pom1 = new Oligonukleotyd();
                    pom1.Ciag = population[chosenSolution].SampleOligs[i].Ciag;
                    pom1.ID = population[chosenSolution].SampleOligs[i].ID;
                    pom1.PrevOligonukleotid = population[chosenSolution].SampleOligs[i].PrevOligonukleotid;
                    pom1.NextOligonukleotid = population[chosenSolution].SampleOligs[i].NextOligonukleotid;

                    temp.SampleOligs.Add(pom1);
                }
                for (int i = 0; i < population[chosenSolution].StringOfOlig.Count(); i++)
                {
                    Oligonukleotyd pom2 = new Oligonukleotyd();
                    pom2.Ciag = population[chosenSolution].StringOfOlig[i].Ciag;
                    pom2.ID = population[chosenSolution].StringOfOlig[i].ID;
                    pom2.PrevOligonukleotid = population[chosenSolution].StringOfOlig[i].PrevOligonukleotid;
                    pom2.NextOligonukleotid = population[chosenSolution].StringOfOlig[i].NextOligonukleotid;
                    pom2.NmbOfNextMatchingNegative = population[chosenSolution].StringOfOlig[i].NmbOfNextMatchingNegative;

                    temp.StringOfOlig.Add(pom2);

                }
                ////////////////////////////////////////
                int min = -1;
                int leftOlig = -1; //, rightOlig = -1;
                for(int i = 0; i < temp.StringOfOlig.Count() - 1; i++) //szukam miejsca zamiany
                {
                    if(temp.StringOfOlig[i].NmbOfNextMatchingNegative < min)
                    {
                        min = temp.StringOfOlig[i].NmbOfNextMatchingNegative;
                        leftOlig = i;
                    //    rightOlig = i+1;
                    }
                }

                temp.SwapChainParts(leftOlig);


                DnaChain temp2 = new DnaChain();
                temp2.SequenceLength = temp.SequenceLength;
                temp2.SequenceMax = temp.SequenceMax;

                int counter = 0;
                int currentID = temp.StringOfOlig[0].ID;
                while(counter < temp.StringOfOlig.Count())
                {
                    temp.StringOfOlig[currentID];

                    for (int k = 0; k < population[chosenSolution].SampleOligs.Count(); k++)
                    {
                        Oligonukleotyd pom1 = new Oligonukleotyd();
                        pom1.Ciag = population[chosenSolution].SampleOligs[k].Ciag;
                        pom1.ID = population[chosenSolution].SampleOligs[k].ID;
                        pom1.PrevOligonukleotid = population[chosenSolution].SampleOligs[k].PrevOligonukleotid;
                        pom1.NextOligonukleotid = population[chosenSolution].SampleOligs[k].NextOligonukleotid;

                        temp.SampleOligs.Add(pom1);
                    }
                    for (int k = 0; k < population[chosenSolution].StringOfOlig.Count(); k++)
                    {
                        Oligonukleotyd pom2 = new Oligonukleotyd();
                        pom2.Ciag = population[chosenSolution].StringOfOlig[k].Ciag;
                        pom2.ID = population[chosenSolution].StringOfOlig[k].ID;
                        pom2.PrevOligonukleotid = population[chosenSolution].StringOfOlig[k].PrevOligonukleotid;
                        pom2.NextOligonukleotid = population[chosenSolution].StringOfOlig[k].NextOligonukleotid;
                        pom2.NmbOfNextMatchingNegative = population[chosenSolution].StringOfOlig[k].NmbOfNextMatchingNegative;

                        temp.StringOfOlig.Add(pom2);

                    }







                }










                //Console.WriteLine("ile w rozwiązaniu: {0}", temp.StringOfOlig.Count()); 
                temp.SwapOlig(chosenOlig1, chosenOlig2);

                if (temp.SequenceLength > temp.SequenceMax)
                {
                    canMut = 0;
                }
                else
                {
                    canMut = 1;
                    population.Add(temp);
                }
                mutationCounter++;

                if (canMut == 1)
                {
                    Console.WriteLine("Mutation done!");
                    int x = population.Count();
                    population[x - 1].PrintChain();
                }
            }


            
        }

        //Mutacja
        public void Mutation3()
        {
            //wymiana używanego z nieużywanym

        }

        //Krzyżowanie
        public void Crossing()
        {

        }

        //Dodawanie oligo jak rozwiązanie jest zakrótkie
        public void LongerChain()
        {

        }


        //Selekcja
        //dojdzie część rozwiązań z poprzedniego
        public void Selection()
        {

            List<DnaChain> temp = new List<DnaChain>();
            int chosenOne=0;
            int scoreOfChosenOne = 0;

            for (int i = 0; i < population.Count(); i=i+4)
            {
                                
            }

        }
    }
}
