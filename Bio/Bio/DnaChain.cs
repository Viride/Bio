using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bio
{
    class DnaChain
    {
        public int SequenceLength { get; set; }                 //maksymalna długość łańcucha
        public int SequenceMax { get; set; }                    //obecna długość łańcucha                                     
        public List<Oligonukleotyd> SampleOligs { get; set; }   //lista wszystkich oligonukleotydów
        public List<Oligonukleotyd> StringOfOlig { get; set; }  //obecny łańcuch oligonukleotydów
        public float Score { get { return(float)SequenceLength / StringOfOlig.Count(); } }




        public DnaChain()
        {
            SampleOligs = new List<Oligonukleotyd>();
            StringOfOlig = new List<Oligonukleotyd>();
        }


        //Wyświetla stworzony łańcuch DNA
        public void PrintChain() 
        {
            for (int i=0; i < StringOfOlig.Count(); i++)  //należy uwzględnić nakładanie się elementów
            {
                Console.WriteLine("Ciag: {0}, positi: {2}, negative: {1}", 
                    StringOfOlig[i].Ciag, 
                    StringOfOlig[i].NmbOfNextMatchingNegative, 
                    StringOfOlig[i].NmbOfNextMatchingPositive);
            }
            Console.WriteLine("Score: {0:f3}, n: {1}, Nmb of elem. {2}", Score, SequenceLength, StringOfOlig.Count());
        }

        public void PrintChainSummary()
        {
            Console.WriteLine("Score: {0:f3}, n: {1}, Nmb of elem. {2}", Score, SequenceLength, StringOfOlig.Count());
        }
        
        public void GenerateRandom()
        {
            Random rnd = new Random();
            int choosen = rnd.Next(SampleOligs.Count() - StringOfOlig.Count());  //losujemy który wybrać z przedziału 0 do maks - tyle ile jest w stworzonym łańcuchu
            StringOfOlig.Add(SampleOligs[choosen]);                              //pierwszy element
            SequenceLength = 10;
            Oligonukleotyd temp_olig, temp2_olig;
            temp_olig = SampleOligs[choosen];
            SampleOligs.Remove(temp_olig);                                         //usuwamy go tam gdzie jest
            SampleOligs.Add(temp_olig);                                            //i wstawiamy na koniec

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

                choosen = rnd.Next(SampleOligs.Count() - StringOfOlig.Count());
                temp_olig = StringOfOlig.Last();
                temp2_olig = SampleOligs[choosen];

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
                temp_olig.NmbOfNextMatchingNegative = counter_neg;
                counter_pos = 0;
                j = 0;
                for (i = 1; i < 10; i++)                                //zliczanie trafień, przy możliwości pozytywnych błędów
                {
//Do usunięcia      //Console.WriteLine("{0}  {1}", temp_olig.Ciag[i], temp2_olig.Ciag[j]);
                    if (temp_olig.Ciag[i] == temp2_olig.Ciag[j])
                    {
                        counter_pos++;
                    }
                    j++;
                }
                temp_olig.NmbOfNextMatchingPositive = counter_pos;

                if (temp_olig.NmbOfNextMatchingPositive == 8)
                {
                    if (SequenceLength + 1 <= SequenceMax)
                    {
                        SequenceLength = SequenceLength + 1;
                        StringOfOlig.Last().NextOligonukleotid = temp2_olig.ID;
                        StringOfOlig.Last().NmbOfNextMatchingNegative = counter_neg;
                        StringOfOlig.Last().NmbOfNextMatchingPositive = counter_pos;
                        temp2_olig.PrevOligonukleotid = StringOfOlig.Last().ID;
                        StringOfOlig.Add(temp2_olig);
                        SampleOligs.Remove(temp2_olig);                                         //usuwamy go tam gdzie jest
                        SampleOligs.Add(temp2_olig);                                            //i wstawiamy na koniec
                    }
                    else
                    {
                        finished = true;
                    }
                }
                else
                {
                    if (SequenceLength + 10-temp_olig.NmbOfNextMatchingNegative <= SequenceMax)
                    {
                        SequenceLength = SequenceLength + 10-temp_olig.NmbOfNextMatchingNegative;
                        StringOfOlig.Last().NextOligonukleotid = temp2_olig.ID;
                        StringOfOlig.Last().NmbOfNextMatchingNegative = counter_neg;
                        StringOfOlig.Last().NmbOfNextMatchingPositive = counter_pos;
                        temp2_olig.PrevOligonukleotid = StringOfOlig.Last().ID;
                        StringOfOlig.Add(temp2_olig);
                        SampleOligs.Remove(temp2_olig);                                         //usuwamy go tam gdzie jest
                        SampleOligs.Add(temp2_olig);                                            //i wstawiamy na koniec
                    }
                    else
                    {
                        finished = true;
                    }

                }
            }

        }

        public void LoadSamples(string file_name)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(file_name);
            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                Oligonukleotyd olig = new Oligonukleotyd(line);
                olig.ID=counter;
                SampleOligs.Add(olig);
                //sample_oligs[counter].print();
                counter++;
            }

            file.Close();
        }
        public void LoadSamples(string file_name, int SequenceMax)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(file_name);
            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                Oligonukleotyd olig = new Oligonukleotyd(line);
                olig.ID = counter;
                SampleOligs.Add(olig);
                //sample_oligs[counter].print();
                counter++;
            }
            this.SequenceMax = SequenceMax;

            file.Close();
        }

    }
}
