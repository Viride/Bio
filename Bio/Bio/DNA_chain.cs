using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bio
{
    class DNA_chain
    {
        private int n_maks;                                 //maksymalna długość łańcucha
        private int n;                                      //obecna długość łańcucha
        private List<Oligonukleotyd> sample_oligs;          //lista wszystkich oligonukleotydów
        private List<Oligonukleotyd> string_of_olig;        //obecny łańcuch oligonukleotydów

        public int N_maks
        {
            get
            {
                return n_maks;
            }

            set
            {
                n_maks = value;
            }
        }

        public int N
        {
            get
            {
                return n;
            }

            set
            {
                n = value;
            }
        }

        public DNA_chain()
        {
            sample_oligs = new List<Oligonukleotyd>();
            string_of_olig = new List<Oligonukleotyd>();
        }


        //Wyświetla stworzony łańcuch DNA
        public void print_chain() 
        {
            for (int i=0; i < string_of_olig.Count(); i++)  //należy uwzględnić nakładanie się elementów
            {
                Console.WriteLine("Ciag: {0}, negative: {1}, positi: {2}", string_of_olig[i].Ciag, string_of_olig[i].Nmb_of_next_matching_negative, string_of_olig[i].Nmb_of_next_matching_positive);
            }
        }

        
        public void generate_random()
        {
            Random rnd = new Random();
            int choosen = rnd.Next(sample_oligs.Count() - string_of_olig.Count());  //losujemy który wybrać z przedziału 0 do maks - tyle ile jest w stworzonym łańcuchu
            string_of_olig.Add(sample_oligs[choosen]);                              //pierwszy element
            n = 10;
            Oligonukleotyd temp_olig, temp2_olig;
            temp_olig = sample_oligs[choosen];
            sample_oligs.Remove(temp_olig);                                         //usuwamy go tam gdzie jest
            sample_oligs.Add(temp_olig);                                            //i wstawiamy na koniec

            int i = 1;      //porusza się po lewej części połączenia
            int j = 0;      //porusza się po prawej części połączenia
            bool finished = false;
            int counter_neg=0;
            int counter_pos = 0;

            while (!finished == true)
            {
                j = 0;
                counter_neg = 0;
                counter_pos = 0;

                choosen = rnd.Next(sample_oligs.Count() - string_of_olig.Count());
                temp_olig = string_of_olig.Last();
                temp2_olig = sample_oligs[choosen];

                ///inny pomysł, to aby użyć contain i sprawdzić, czy lewa strona zawiera np. 3 pierwsze litery prawej
                for (i = 1; i < 10; i++)                                //zliczanie trafień przy możliwości negatywnych błędów
                {
                    if (temp_olig.Ciag[i] == temp2_olig.Ciag[j])
                    {
                        counter_neg++;
                        j++;
                    }
                    else if (temp_olig.Ciag[i] == temp2_olig.Ciag[0])
                    {
                        j = 1;
                        counter_neg = 1;
                    }
                    else
                    {
                        j = 0;
                        counter_neg = 0;
                    }
                }
                temp_olig.Nmb_of_next_matching_negative = counter_neg;
                counter_pos = 0;
                j = 0;
                for (i = 1; i < 10; i++)                                //zliczanie trafień, przy możliwości pozytywnych błędów
                {
                    Console.WriteLine("{0}  {1}", temp_olig.Ciag[i], temp2_olig.Ciag[j]);
                    if (temp_olig.Ciag[i] == temp2_olig.Ciag[j])
                    {
                        counter_pos++;
                    }
                    j++;
                }
                temp_olig.Nmb_of_next_matching_positive = counter_pos;

                if (temp_olig.Nmb_of_next_matching_positive == 8)
                {
                    if (n + 1 <= n_maks)
                    {
                        n = n + 1;
                        string_of_olig.Last().Next_oligonukleotid = temp2_olig.SetID;
                        string_of_olig.Last().Nmb_of_next_matching_negative = counter_neg;
                        string_of_olig.Last().Nmb_of_next_matching_positive = counter_pos;
                        temp2_olig.Prev_oligonukleotid = string_of_olig.Last().SetID;
                        string_of_olig.Add(temp2_olig);
                        sample_oligs.Remove(temp2_olig);                                         //usuwamy go tam gdzie jest
                        sample_oligs.Add(temp2_olig);                                            //i wstawiamy na koniec
                    }
                    else
                    {
                        finished = true;
                    }
                }
                else
                {
                    if (n + temp_olig.Nmb_of_next_matching_negative <= n_maks)
                    {
                        n = n + 10-temp_olig.Nmb_of_next_matching_negative;
                        string_of_olig.Last().Next_oligonukleotid = temp2_olig.SetID;
                        string_of_olig.Last().Nmb_of_next_matching_negative = counter_neg;
                        string_of_olig.Last().Nmb_of_next_matching_positive = counter_pos;
                        temp2_olig.Prev_oligonukleotid = string_of_olig.Last().SetID;
                        string_of_olig.Add(temp2_olig);
                        sample_oligs.Remove(temp2_olig);                                         //usuwamy go tam gdzie jest
                        sample_oligs.Add(temp2_olig);                                            //i wstawiamy na koniec
                    }
                    else
                    {
                        finished = true;
                    }

                }
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
                olig.SetID=counter;
                sample_oligs.Add(olig);
                //sample_oligs[counter].print();
                counter++;
            }

            file.Close();
        }


    }
}
