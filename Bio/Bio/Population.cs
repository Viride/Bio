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
            // WHILE (count() of mutations == x)
            
            //wybieramy rozwiązanie do mutacji
            //losujemy dwa miejsca w ciągu
            //resetujemy next i prev dla sąsiadów wybranych elementów
            //zamieniamy elementy miejscami w liście
            //generujemy nowe next i prev dla sąsiadów
            //sprawdzamy, czy mieści się w SequenceMax, jeśli nie losujemy inne elementy do mutacji, maks 3 próby
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
        //Selekcja
        //dojdzie część rozwiązań z poprzedniego
        public void Selection()
        {

            List<DnaChain> temp = new List<DnaChain>();
            int choosenOne=0;
            int scoreOfChoosenOne = 0;

            for (int i = 0; i < population.Count(); i=i+4)
            {
                                
            }

        }
    }
}
