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

        public void generate_population(int rozmiar)
        {
            for (int i = 0; i < rozmiar; i++)
            {
                DnaChain sample_base = new DnaChain();
                DnaChain temp = new DnaChain();

                sample_base = new DnaChain();
                sample_base.load_samples("9.200 - 40.txt");

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
