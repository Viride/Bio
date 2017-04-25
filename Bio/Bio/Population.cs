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
                population[i].PrintChainSummary();
            }
        }



        //  Nowo powstałe rozwiązania (mutacja, krzyżowanie) dodajemy do populacji a na koniec 
        //  selekcja je oczyści i zostawi tylko porządaną liczbę


        //Mutacja
        public void Mutation()
        {

        }

        //Mutacja
        public void Mutation2()
        {

        }

        //Krzyżowanie
        public void Crossing()
        {

        }
        //Selekcja
        public void Selection()
        {

        }
    }
}
