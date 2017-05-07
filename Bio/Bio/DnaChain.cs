using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bio
{
    public class DnaChain
    {
        public int SequenceLength { get; set; }                 //obecna długość łańcucha
        public int SequenceMax { get; set; }                    //maksymalna długość łańcucha                                     
        public List<Oligonukleotyd> SampleOligs { get; set; }   //lista wszystkich oligonukleotydów
        public List<Oligonukleotyd> StringOfOlig { get; set; }  //obecny łańcuch oligonukleotydów
        public float Score { get { return(float)SequenceLength / StringOfOlig.Count(); } }




        public DnaChain()
        {
            SampleOligs = new List<Oligonukleotyd>();
            StringOfOlig = new List<Oligonukleotyd>();
        }

        public bool connect(Oligonukleotyd temp_olig_of, Oligonukleotyd temp2_olig_of, bool finished)
        {
            Oligonukleotyd temp_olig, temp2_olig;
            temp_olig = new Oligonukleotyd();
            temp2_olig = new Oligonukleotyd();
            temp_olig.CopyFrom(temp_olig_of);
            temp2_olig.CopyFrom(temp2_olig_of);
            int j = 0;                  //porusza się po prawej części połączenia
            int counter_neg = 0;
          //  int counter_pos = 0;

            ///inny pomysł, to aby użyć contain i sprawdzić, czy lewa strona zawiera np. 3 pierwsze litery prawej
            for (int i = 1; i < 10; i++)                                //zliczanie trafień przy możliwości negatywnych błędów
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

            if (SequenceLength + 10 - temp_olig.NmbOfNextMatchingNegative <= SequenceMax && SampleOligs != null)
            {
                SequenceLength = SequenceLength + 10 - temp_olig.NmbOfNextMatchingNegative;
                StringOfOlig.Last().NextOligonukleotid = temp2_olig.ID;
                StringOfOlig.Last().NmbOfNextMatchingNegative = counter_neg;
                //      StringOfOlig.Last().NmbOfNextMatchingPositive = counter_pos;
                temp2_olig.PrevOligonukleotid = StringOfOlig.Last().ID;
                StringOfOlig.Add(temp2_olig);
                SampleOligs.Remove(temp2_olig_of);                                         //usuwamy go tam gdzie jest
                SampleOligs.Add(temp2_olig_of);                                            //i wstawiamy na koniec
            }
            else
            {
                finished = true;
            }

            /*
            counter_pos = 0;
            j = 0;
            for (int i = 1; i < 10; i++)                                //zliczanie trafień, przy możliwości pozytywnych błędów
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
            */
            //tu był ostatni nie skomentowany fragment

            //   }

            return finished;
        }

        //Wyświetla stworzony łańcuch DNA
        public void PrintChain() 
        {
            for (int i=0; i < StringOfOlig.Count(); i++)  //należy uwzględnić nakładanie się elementów
            {
                Console.WriteLine("Ciag: {0}, negative: {1}, moje ID: {2},\tID next {3}",
                    StringOfOlig[i].Ciag,
                    StringOfOlig[i].NmbOfNextMatchingNegative,
                    StringOfOlig[i].ID,
                    StringOfOlig[i].NextOligonukleotid);
            }
            Console.WriteLine("Score: {0:f3}, n: {1}, Nmb of elem. {2}\n", Score, SequenceLength, StringOfOlig.Count());
        }

        public void PrintChainSummary()
        {
            Console.WriteLine("Score: {0:f3}, n: {1}, Nmb of elem. {2}", Score, SequenceLength, StringOfOlig.Count());
        }
        
        public void GenerateRandom()
        {
            Random rnd = new Random();
            int chosen = rnd.Next(SampleOligs.Count() - StringOfOlig.Count());  //losujemy który wybrać z przedziału 0 do maks - tyle ile jest w stworzonym łańcuchu
            StringOfOlig.Add(SampleOligs[chosen]);                              //pierwszy element
            SequenceLength = 10;
            Oligonukleotyd temp_olig, temp2_olig;
            temp_olig = SampleOligs[chosen];
            SampleOligs.Remove(temp_olig);                                         //usuwamy go tam gdzie jest
            SampleOligs.Add(temp_olig);                                            //i wstawiamy na koniec
            //temp_olig.print();
            bool finished = false;

            while (!finished == true)
            {
                

                chosen = rnd.Next(SampleOligs.Count() - StringOfOlig.Count());
                temp_olig = StringOfOlig.Last();
                temp2_olig = SampleOligs[chosen];
                finished=connect(temp_olig, temp2_olig, finished);
            }

        }

        public void CheckSum()
        {
            for (int i = 0; i < StringOfOlig.Count()-1; i++)
            {
                for (int j = i + 1; j < StringOfOlig.Count(); j++)
                {
                    if (StringOfOlig[i].ID == StringOfOlig[j].ID)
                        Console.WriteLine("We got a fuc***g problem over here");
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
        


        public void connectingTwoOligs(int first, int second)
        {
            int j = 0;                  //porusza się po prawej części połączenia
            int counter_neg = 0;

            for (int i = 1; i < 10; i++)                               
            {
                if (StringOfOlig[first].Ciag[i] == StringOfOlig[second].Ciag[j])
                {
                    counter_neg++;
                    j++;
                }
                else if (StringOfOlig[first].Ciag[i] == StringOfOlig[second].Ciag[0])
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
            StringOfOlig[first].NmbOfNextMatchingNegative = counter_neg;

        }

        public void SwapOlig(int oligNr1, int oligNr2)
        {
            Console.WriteLine("które oligonukleotydy zamieniamy: {0} & {1}", oligNr1, oligNr2);
            //Console.WriteLine("które oligonukleotydy zamieniamy: {0} & {1}", oligNr1, oligNr2);
            //PrintChain();
            StringOfOlig[oligNr1].print();
            StringOfOlig[oligNr2].print();
            Oligonukleotyd pom1 = new Oligonukleotyd();
            Oligonukleotyd pom2 = new Oligonukleotyd();
            pom1.Ciag = StringOfOlig[oligNr1].Ciag;
            pom2.Ciag = StringOfOlig[oligNr2].Ciag;
            pom1.ID = StringOfOlig[oligNr1].ID;
            pom2.ID = StringOfOlig[oligNr2].ID;
            pom1.PrevOligonukleotid = StringOfOlig[oligNr1].PrevOligonukleotid;
            pom2.PrevOligonukleotid = StringOfOlig[oligNr2].PrevOligonukleotid;
            pom1.NextOligonukleotid = StringOfOlig[oligNr1].NextOligonukleotid;
            pom2.NextOligonukleotid = StringOfOlig[oligNr2].NextOligonukleotid;


            /*   int prev1ID = StringOfOlig[oligNr1].PrevOligonukleotid;
               int next1ID = StringOfOlig[oligNr1].NextOligonukleotid;
               int prev2ID = StringOfOlig[oligNr2].PrevOligonukleotid;
               int next2ID = StringOfOlig[oligNr2].NextOligonukleotid;
               Console.WriteLine("które oligonukleotydy zamieniamy ID: {0} ", prev1ID);*/
            // StringOfOlig[oligNr1].PrevOligonukleotid
            // int x = StringOfOlig[oligNr1].PrevOligonukleotid;
            //  StringOfOlig[x].print();
            int prev1 = -1, prev2 = -1, next1 = -1, next2 = -1;
           
            if (oligNr1 > 0)
            {
                prev1 = oligNr1 - 1;
                StringOfOlig[prev1].NextOligonukleotid = StringOfOlig[oligNr2].ID;
            }
            if (oligNr1 < StringOfOlig.Count() - 1)
            {
                next1 = oligNr1 + 1;
                StringOfOlig[next1].PrevOligonukleotid = StringOfOlig[oligNr2].ID;
            }
            if (oligNr2 > 0)
            {
                prev2 = oligNr2 - 1;
                StringOfOlig[prev2].NextOligonukleotid = StringOfOlig[oligNr1].ID;
            }
            if (oligNr2 < StringOfOlig.Count() - 1)
            {
                next2 = oligNr2 + 1;
                StringOfOlig[next2].PrevOligonukleotid = StringOfOlig[oligNr1].ID;
            }

            // StringOfOlig[oligNr2].print();
           
            StringOfOlig[oligNr1].Ciag = pom2.Ciag;
            StringOfOlig[oligNr2].Ciag = pom1.Ciag;

            StringOfOlig[oligNr1].print();
            StringOfOlig[oligNr2].print();
            Console.WriteLine("które oligonukleotydy zamieniamy ID: {0} {1}", StringOfOlig[oligNr2].ID, StringOfOlig[oligNr1].ID);

            StringOfOlig[oligNr2].ID = pom1.ID;
            StringOfOlig[oligNr1].ID = pom2.ID;

            Console.WriteLine("które oligonukleotydy zamieniamy ID: {0} {1}", StringOfOlig[oligNr2].ID, StringOfOlig[oligNr1].ID);

            StringOfOlig[oligNr1].NmbOfNextMatchingNegative = 0;
            StringOfOlig[oligNr2].NmbOfNextMatchingNegative = 0;


            
               if(oligNr1 > 0)
               {
                   StringOfOlig[oligNr1].PrevOligonukleotid = pom1.PrevOligonukleotid;
               }
               else
               {
                   StringOfOlig[oligNr1].PrevOligonukleotid = -1;
               }
               if (oligNr2 > 0)
               {
                   StringOfOlig[oligNr2].PrevOligonukleotid = pom2.PrevOligonukleotid;
               }
               else
               {
                   StringOfOlig[oligNr2].PrevOligonukleotid = -1;
               }
               if (oligNr1 < StringOfOlig.Count() - 1)
               {
                   StringOfOlig[oligNr1].NextOligonukleotid = pom1.NextOligonukleotid;
               }
               else
               {
                   StringOfOlig[oligNr1].NextOligonukleotid = -1;
               }
               if (oligNr2 < StringOfOlig.Count() - 1)
               {
                   StringOfOlig[oligNr2].NextOligonukleotid = pom2.NextOligonukleotid;
               }
               else
               {
                   StringOfOlig[oligNr2].NextOligonukleotid = -1;
               }

            //  Console.WriteLine("idik: {0}", StringOfOlig[prev1].Ciag);

            if (prev1 > 0)
            {
                this.connectingTwoOligs(prev1, oligNr1);
            }
            if (next1 > 0)
            {
                this.connectingTwoOligs(oligNr1, next1);
            }
            if (prev2 > 0)
            {
                this.connectingTwoOligs(prev2, oligNr2);
            }
            if (next2 > 0)
            {
                this.connectingTwoOligs(oligNr2, next2);
            }

            //zliczanie długości łańcucha
            SequenceLength = 0;
            for(int i = 0; i < StringOfOlig.Count() - 1; i++)
            {
                SequenceLength = SequenceLength + 10 - StringOfOlig[i].NmbOfNextMatchingNegative;
            }


        }

    }
}
