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
        private List<DNA_chain> population; 

        public Population()
        {
            population = new List<DNA_chain>();
        }


        //  Nowo powstałe rozwiązania (mutacja, krzyżowanie) dodajemy do populacji a na koniec 
        //  selekcja je oczyści i zostawi tylko porządaną liczbę


        //Mutacja
        public void Mutation()
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
