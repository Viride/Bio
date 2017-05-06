using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio
{
    class Population
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
            sample_base = new DnaChain();
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
            }
        }



        //  Nowo powstałe rozwiązania (mutacja, krzyżowanie) dodajemy do populacji a na koniec 
        //  selekcja je oczyści i zostawi tylko porządaną liczbę


        //Mutacja - skrócenie końcówki mutacji
        public void Mutation()
        {
            int mutationCounter = 0;
            int canMut = 0;
            //dodanie rozwiązania do bieżącej populacji
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
                temp = population[chosenSolution];
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
        public void Mutation2()
        {


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
