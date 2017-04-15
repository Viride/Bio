using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bio
{
    class DNA_chain
    {
        private List<Oligonukleotyd> sample_oligs;
        private List<Oligonukleotyd> string_of_olig;


        public DNA_chain()
        {
            sample_oligs = new List<Oligonukleotyd>();
            string_of_olig = new List<Oligonukleotyd>();
        }


        //Wyświetla stworzony łańcuch DNA
        public void print() 
        {
            for (int i=0; i < string_of_olig.Count(); i++)  //należy uwzględnić nakładanie się elementów
            {

            }
        }
        public void generate_random(Oligonukleotyd oligonukleotyd)
        {
            int i = 1;      //porusza się po lewej części połączenia
            int j = 0;      //porusza się po prawej części połączenia
            string a;
            string b;


            while (i < 10)
            {

            }
        
        }

        public void load_samples(string file_name)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(file_name);
            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                Oligonukleotyd olig = new Oligonukleotyd(line);
                olig.SetID(counter);
                sample_oligs.Add(olig);
                //sample_oligs[counter].print();
                counter++;
            }

            file.Close();
        }


    }
}
