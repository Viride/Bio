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
        static Random rand = new Random();
        int maxLength = 200;        //DO ZAMIANY PRZY INNYM PLIKU!!!!!!!!!!!!

        public Population(int maxLength)
        {
            population = new List<DnaChain>();
            this.maxLength = maxLength - 9;
        }

        public void GeneratePopulation(int rozmiar, string fileName, int size)
        {
            DnaChain sample_base = new DnaChain();
            // sample_base = new DnaChain();
            //53.500-200.txt        //max length do zmiany przy innym pliku!!!!!!!!!!!!
            sample_base.LoadSamples(fileName, size);

            for (int i = 0; i < rozmiar; i++)
            {
                DnaChain temp = new DnaChain();

                temp.CopyDnaChain(sample_base);
                //for (int j = 0; j < sample_base.SampleOligs.Count(); j++)
                //{
                //    temp.SampleOligs[j].CopyFrom2(sample_base.SampleOligs[j]);
                //}
                //temp.SequenceMax = sample_base.SequenceMax;
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

        public void PrintPopulationResult()
        {
            Console.WriteLine("\n\nObecna populacja:\n");
            Console.WriteLine("Score:\t\tLength:\tNmb of elem.:\n");
            for (int i = 0; i < population.Count(); i++)
            {
                population[i].CheckSumSample();
                population[i].CheckSum();
                Console.WriteLine("{0:f6}\t{1}\t{2}", population[i].Score, population[i].SequenceLength,population[i].StringOfOlig.Count() );
            }
            Console.WriteLine("Koniec wypisywania. Rozmiar populacji: {0}\n\n", population.Count());
        }

        public void PrintBestResult()
        {
            int max = 0;
            for (int i = 0; i < population.Count(); i++)
            {
                if(population[i].StringOfOlig.Count() > max)
                {
                    max = population[i].StringOfOlig.Count();
                }
             }
            Console.WriteLine("Najwięcej olig: {0}, population size: {1}", max, population.Count());
        }

        public void SaveBestToFile(string file_name, string time, int NmbOfRepeats)
        {
            int max = 0;
            int maxCounter=0;
            float maxScore=0;
            int maxScoreCounter = 0;
            for (int i = 0; i < population.Count(); i++)
            {
                if (population[i].StringOfOlig.Count() > max)
                {
                    max = population[i].StringOfOlig.Count();
                    maxCounter = i;
                }
                if (population[i].Score > max)
                {
                    maxScore = population[i].Score;
                    maxScoreCounter = i;
                }
            }
            population[maxScoreCounter].PrintWholeChain(file_name, time, NmbOfRepeats);

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
                ///Random rand = new Random();
                int size = population.Count();
                // Console.WriteLine("ile w populacji: {0}", size);

                int chosenSolution = rand.Next(size);   //losuje rozwiązanie z populacji do mutacji
                //population[chosenSolution].PrintChain();  //wypisanie wylosowanego rozwiązania do mutacji
                int solutionSize = population[chosenSolution].StringOfOlig.Count();
                //Console.WriteLine("ile w rozwiązaniu: {0}", solutionSize);



                int chosenOlig1 = rand.Next(solutionSize);
                int chosenOlig2 = rand.Next(solutionSize);
                while (chosenOlig1 == chosenOlig2)
                {
                    chosenOlig2 = rand.Next(solutionSize);
                }
                //Console.WriteLine("Wylosowane oligo do zamiany: {0} {1}", chosenOlig1, chosenOlig2);

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

                for (int i = 0; i < temp.StringOfOlig.Count(); i++)
                {
                    Oligonukleotyd temp2;
                    temp2 = temp.SampleOligs.Find(x => x.ID == temp.StringOfOlig[i].ID);
                    temp.SampleOligs.Remove(temp2);
                    temp.SampleOligs.Add(temp2);
                }

                if (temp.SequenceLength > temp.SequenceMax)
                {
                    canMut = 0;
                }
                else
                {
                    canMut = 1;
                    int index = rand.Next(population.Count());
                    population.Insert(index, temp);
                }
                mutationCounter++;

                if (canMut == 1)
                {
         //           Console.WriteLine("Mutation done!");
                    int x = population.Count();
                //    population[x - 1].PrintChain();
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
                ///Random rand = new Random();
                int size = population.Count();
                // Console.WriteLine("ile w populacji: {0}", size);

                int chosenSolution = rand.Next(size);   //losuje rozwiązanie z populacji do mutacji
                //population[chosenSolution].PrintChain();  //wypisanie wylosowanego rozwiązania do mutacji
 
                int solutionSize = population[chosenSolution].StringOfOlig.Count();
                //Console.WriteLine("ile w rozwiązaniu: {0}", solutionSize);
                /////////////////////////////////////////
                DnaChain temp = new DnaChain();
                DnaChain temp2 = new DnaChain();
                //Console.WriteLine("ile w rozwiązaniu: {0}", temp.StringOfOlig.Count()); 
                temp.SequenceLength = 0;
                temp.SequenceMax = population[chosenSolution].SequenceMax;

                for (int i = 0; i < population[chosenSolution].SampleOligs.Count(); i++)
                {
                    Oligonukleotyd pom1 = new Oligonukleotyd();
                    pom1.Ciag = population[chosenSolution].SampleOligs[i].Ciag;
                    pom1.ID = population[chosenSolution].SampleOligs[i].ID;
                    pom1.PrevOligonukleotid = population[chosenSolution].SampleOligs[i].PrevOligonukleotid;
                    pom1.NextOligonukleotid = population[chosenSolution].SampleOligs[i].NextOligonukleotid;

                    temp.SampleOligs.Add(pom1);
                    temp2.SampleOligs.Add(pom1);
                }
                
                int min = 1000000000;
                int leftOlig = -1; //, rightOlig = -1;
                for(int i = 0; i < population[chosenSolution].StringOfOlig.Count() - 1; i++) //szukam miejsca zamiany
                {
                    if(population[chosenSolution].StringOfOlig[i].NmbOfNextMatchNeg < min)
                    {
                        min = population[chosenSolution].StringOfOlig[i].NmbOfNextMatchNeg;
                        leftOlig = i;
                    //    rightOlig = i+1;
                    }
                }

                int counter = leftOlig+1; int nrOlig1 = 0;
                while (counter < population[chosenSolution].StringOfOlig.Count())
                {

                    Oligonukleotyd pom = new Oligonukleotyd();
                    pom.Ciag = population[chosenSolution].StringOfOlig[counter].Ciag;
                    pom.ID = population[chosenSolution].StringOfOlig[counter].ID;
                    pom.PrevOligonukleotid = population[chosenSolution].StringOfOlig[counter].PrevOligonukleotid;
                    pom.NextOligonukleotid = population[chosenSolution].StringOfOlig[counter].NextOligonukleotid;
                    pom.NmbOfNextMatchNeg = population[chosenSolution].StringOfOlig[counter].NmbOfNextMatchNeg;

                    temp.StringOfOlig.Add(pom);
                    //Console.WriteLine("nrOlig1 z drugiego {0}", nrOlig1);
                    nrOlig1++;
                    counter++;
                } //koniec ZAMIANY
                //Console.WriteLine("nrOlig1 z drugiego {0}", nrOlig1);
                //nowy początek
                temp.StringOfOlig[0].PrevOligonukleotid = -1;
                //koniec nowego początku
                int check = 0;
                if(nrOlig1 != 0)
                {
                    check = nrOlig1 - 1;
                }
                //Console.WriteLine("tuuu {0} {1}", check, counter - 1);
                temp.StringOfOlig[check].NextOligonukleotid = population[chosenSolution].StringOfOlig[0].ID;

                counter = 0; 
                while (counter <= leftOlig)
                {

                    Oligonukleotyd pom = new Oligonukleotyd();
                    pom.Ciag = population[chosenSolution].StringOfOlig[counter].Ciag;
                    pom.ID = population[chosenSolution].StringOfOlig[counter].ID;
                    pom.PrevOligonukleotid = population[chosenSolution].StringOfOlig[counter].PrevOligonukleotid;
                    pom.NextOligonukleotid = population[chosenSolution].StringOfOlig[counter].NextOligonukleotid;
                    pom.NmbOfNextMatchNeg = population[chosenSolution].StringOfOlig[counter].NmbOfNextMatchNeg;

                    temp.StringOfOlig.Add(pom);

                    
                    counter++;
                } //koniec ZAMIANY2
                //początek nowej drugiej części
                temp.StringOfOlig[nrOlig1].PrevOligonukleotid = temp.StringOfOlig[check].ID;
                //koniec nowej drugiej części
                temp.StringOfOlig[temp.StringOfOlig.Count() - 1].NextOligonukleotid = -1;
                temp.StringOfOlig[temp.StringOfOlig.Count() - 1].NmbOfNextMatchNeg = 0;


                //zliczanie pokrycia             
                temp.connectingTwoOligs(nrOlig1-1, nrOlig1);


                //zliczanie długości łańcucha
                temp.SequenceLength = 0;
                for (int i = 0; i < temp.StringOfOlig.Count() - 1; i++)
                {
                    temp.SequenceLength = temp.SequenceLength + 10 - temp.StringOfOlig[i].NmbOfNextMatchNeg;
                }



                if (temp.SequenceLength > temp.SequenceMax)
                {
                    canMut = 0;
                }
                else
                {
                    canMut = 1;
                    int index = rand.Next(population.Count());
                    population.Insert(index, temp);
                }
                mutationCounter++;

                if (canMut == 1)
                {
                //    Console.WriteLine("Mutation2 done!");
                    int x = population.Count();
                //   population[x - 1].PrintChain();
                }
            }


            
        }

        //Mutacja
        public void Mutation3()
        {
            ///Random rand = new Random();
            int mutationCounter = 0;
            int canMut = 0;

            while (mutationCounter < 3 && canMut == 0)
            {
                
                int size = population.Count();
                
                int chosenSolution = rand.Next(size);   //losuje rozwiązanie z populacji do mutacji
                //Console.WriteLine("Rozwiązanie do mutacji 3: ");
               // population[chosenSolution].PrintChain();  //wypisanie wylosowanego rozwiązania do mutacji

                int solutionSize = population[chosenSolution].StringOfOlig.Count();
               // Console.WriteLine("ile w rozwiązaniu: {0}", solutionSize);
                int sampleSize = population[chosenSolution].SampleOligs.Count() - solutionSize;  //TUTUTUTUTUTUTUTU
               // Console.WriteLine("ile w rozwiązaniu: {0}", sampleSize);

                int chosenOlig1 = rand.Next(solutionSize);
                int chosenOlig2 = rand.Next(sampleSize);

                Oligonukleotyd Olig1 = new Oligonukleotyd();
                //Olig1 = population[chosenSolution].StringOfOlig[chosenOlig1];
                //Olig1.CopyFrom2(population[chosenSolution].StringOfOlig[chosenOlig1]);
                Oligonukleotyd Olig2 = new Oligonukleotyd();
                //Olig2 = population[chosenSolution].SampleOligs[chosenOlig2];
                //Olig2.CopyFrom2(population[chosenSolution].SampleOligs[chosenOlig2]);
                //Console.WriteLine("do zamiany id: {0} {1}", population[chosenSolution].StringOfOlig[chosenOlig1].ID, population[chosenSolution].SampleOligs[chosenOlig2].ID);


                //////////////////////////////////////////
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

                    temp.StringOfOlig.Add(pom2);

                }

                Olig1 = temp.SampleOligs.Find(x => x.ID == temp.StringOfOlig[chosenOlig1].ID);
                Olig2 = temp.SampleOligs[chosenOlig2];

               temp.StringOfOlig[chosenOlig1].CopyFrom3(Olig2);  //dodanie do łańcucha

                    //Oligonukleotyd temp3;
                    //temp3 = temp.SampleOligs.Find(x => x.ID == temp.SampleOligs[chosenOlig2].ID);
                    //temp.SampleOligs.Remove(temp3);
                    //temp.SampleOligs.Add(temp2);
                    
                temp.SampleOligs.Remove(Olig2);   //usuwam z sampli
                temp.SampleOligs.Insert(0, Olig2);  //dodaje na poczatek

                    //temp3 = temp.SampleOligs.Find(x => x.ID == temp.StringOfOlig[chosenOlig1].ID);
                    //temp.SampleOligs.Remove(temp3);
                temp.SampleOligs.Remove(Olig1);   //usuwam z sampli
                temp.SampleOligs.Add(Olig1);  //dodaje na koniec sampli

                //ustawienie id u sąsiadów JAK MNIEJ NIŻ 0!!!!!!!!!!!!!!!!!!!!
                if (chosenOlig1 > 0)
                {
                    temp.StringOfOlig[chosenOlig1 - 1].NextOligonukleotid = temp.StringOfOlig[chosenOlig1].ID;
                    temp.connectingTwoOligs(chosenOlig1 - 1, chosenOlig1);
                }
                if (chosenOlig1 < temp.StringOfOlig.Count() - 1)
                {
                    temp.StringOfOlig[chosenOlig1 + 1].PrevOligonukleotid = temp.StringOfOlig[chosenOlig1].ID;
                    temp.connectingTwoOligs(chosenOlig1, chosenOlig1 + 1);
                }


                //zliczanie długości łańcucha
                temp.SequenceLength = 0;
                for (int i = 0; i < temp.StringOfOlig.Count() - 1; i++)
                {
                    temp.SequenceLength = temp.SequenceLength + 10 - temp.StringOfOlig[i].NmbOfNextMatchNeg;
                }

                for (int i = 0; i < temp.StringOfOlig.Count(); i++)
                {
                    Oligonukleotyd temp2;
                    temp2 = temp.SampleOligs.Find(x => x.ID == temp.StringOfOlig[i].ID);
                    temp.SampleOligs.Remove(temp2);
                    temp.SampleOligs.Add(temp2);
                }


                if (temp.SequenceLength > temp.SequenceMax)
                {
                    canMut = 0;
                }
                else
                {
                    canMut = 1;
                    int index = rand.Next(population.Count());
                    population.Insert(index, temp);
                }
                mutationCounter++;

                if (canMut == 1)
                {
                //    Console.WriteLine("Mutation3 done!");
                    int x = population.Count();
                //    population[x - 1].PrintChain();
                }






            }

        }

        //Krzyżowanie
        public void Crossing()
        {
            
            //wylosować dwa łańcuchy, skopiować ich dane do nowych
            ///Random rand = new Random();
            int crossPlace = -1;
            DnaChain temp1;
            DnaChain temp2;
            int choosenOne, choosenTwo;
            int howAcurate = 2;
            int whileCounter = 0;
            do
            {
                if (whileCounter % 5 == 4)
                {
                    howAcurate++;
                }
                choosenOne = rand.Next(population.Count());                
                do
                {
                    choosenTwo = rand.Next(population.Count());
                } while (choosenTwo == choosenOne);
                if (whileCounter <= 20)
                {
                    int NmbOfTry = population[choosenOne].StringOfOlig.Count() - 1;
                    if (population[choosenTwo].StringOfOlig.Count() - 1 < NmbOfTry)
                    {
                        NmbOfTry = population[choosenTwo].StringOfOlig.Count() - 1;
                    }
                    for (int i = 0; i < NmbOfTry; i++)
                    {
                        if (population[choosenOne].StringOfOlig[i].NmbOfNextMatchNeg == 0 &&
                            population[choosenTwo].StringOfOlig[i].NmbOfNextMatchNeg < howAcurate)
                        {
                            crossPlace = i;
                            break;
                        }
                        else
                            if (population[choosenOne].StringOfOlig[i].NmbOfNextMatchNeg < howAcurate &&
                                   population[choosenTwo].StringOfOlig[i].NmbOfNextMatchNeg == 0)
                        {
                            crossPlace = i;
                            break;
                        }
                    }
                }
                if (whileCounter > 20)
                {
                    int NmbOfTry = population[choosenOne].StringOfOlig.Count() - 1;
                    if (population[choosenTwo].StringOfOlig.Count() - 1 < NmbOfTry)
                    {
                        NmbOfTry = population[choosenTwo].StringOfOlig.Count() - 1;
                    }
                    for (int i = 0; i < NmbOfTry; i++)
                    {
                        if (population[choosenOne].StringOfOlig[i].NmbOfNextMatchNeg < howAcurate &&
                            population[choosenTwo].StringOfOlig[i].NmbOfNextMatchNeg < howAcurate)
                        {
                            crossPlace = i;
                            break;
                        }                    
                    }
                }
                whileCounter++;
            } while (crossPlace == -1);

            temp1 = new DnaChain();
            temp2 = new DnaChain();
            temp1.CopyDnaChain(population[choosenOne]);
            temp2.CopyDnaChain(population[choosenTwo]);

            //Console.Write("{0} {1}", choosenOne, choosenTwo);

            //Console.Write("Kopiowanie skończone, punkt zamiany wybrany\n" +
            //    "Zaczynam składanie nowych łańcuchów.............. ");

            //zamienić i naprawić sprawdzając, czy nie powtarzają się
            DnaChain new1, new2;
            new1 = new DnaChain();
            new2 = new DnaChain();
            //Kopiowanie niezmiennych danych
            new1.SampleOligs = temp1.SampleOligs;
            new1.SequenceMax = temp1.SequenceMax;
            new2.SampleOligs = temp2.SampleOligs;
            new2.SequenceMax = temp2.SequenceMax;

            //Kopiowanie początków
            for(int i = 0; i < crossPlace+1; i++)
            {
                new1.StringOfOlig.Add(temp1.StringOfOlig[i]);
                new2.StringOfOlig.Add(temp2.StringOfOlig[i]);
            }

            //Kopiowanie końców z uwzględnieniem opcji powtórzenia (może wystąpić tylko do crossPlace
            for (int i = crossPlace+1; i < temp1.StringOfOlig.Count(); i++)
            {
                if (new2.StringOfOlig.Exists(x => x.ID == temp1.StringOfOlig[i].ID) == false)
                {
                    new2.StringOfOlig.Add(temp1.StringOfOlig[i]);
                }
            }

            for (int i = crossPlace + 1; i < temp2.StringOfOlig.Count(); i++)
            {
                if (new1.StringOfOlig.Exists(x => x.ID == temp2.StringOfOlig[i].ID) == false)
                {
                    new1.StringOfOlig.Add(temp2.StringOfOlig[i]);
                }
            }
            //Console.Write("Skończono tworzenie nowych łańcuchów\n" +
            //    "Zaczynam naprawę łańcucha..........");
            //Console.Write("copy1   ");
            int stoppedRepair = -1;
            new1.SequenceLength = 10;
            new2.SequenceLength = 10;
            for (int i = 0; i < new1.StringOfOlig.Count()-1; i++)
            {
                if (new1.StringOfOlig[i].NextOligonukleotid == new1.StringOfOlig[i + 1].ID)
                {
                    if (new1.SequenceLength + 10 - new1.StringOfOlig[i].NmbOfNextMatchNeg < new1.SequenceMax)
                    {
                        new1.SequenceLength += 10 - new1.StringOfOlig[i].NmbOfNextMatchNeg;
                    }
                    else
                    {
                        stoppedRepair = i;
                        new1.StringOfOlig[i].NmbOfNextMatchNeg = -1;
                        //Console.WriteLine("koniec miejsca");
                    }
                }
                else
                {
                    new1.StringOfOlig[i].NmbOfNextMatchNeg =
                        new1.crossConnect(new1.StringOfOlig[i], new1.StringOfOlig[i + 1]);
                    if (new1.SequenceLength + 10 - new1.StringOfOlig[i].NmbOfNextMatchNeg < new1.SequenceMax)
                    {
                        new1.SequenceLength += 10 - new1.StringOfOlig[i].NmbOfNextMatchNeg;
                        new1.StringOfOlig[i + 1].NmbOfPrevMatchNeg = new1.StringOfOlig[i].NmbOfNextMatchNeg;
                        new1.StringOfOlig[i].NextOligonukleotid = new1.StringOfOlig[i + 1].ID;
                        new1.StringOfOlig[i + 1].PrevOligonukleotid = new1.StringOfOlig[i].ID;
                    }
                    else
                    {
                        stoppedRepair = i;
                        new1.StringOfOlig[i].NmbOfNextMatchNeg = -1;
                        //Console.WriteLine("koniec miejsca");

                    }
                }
                
            }
            if (stoppedRepair != -1)
            {
                for (int i = new1.StringOfOlig.Count() - 1; i > stoppedRepair; i--)
                {
                    new1.StringOfOlig.Remove(new1.StringOfOlig[i]);
                    //Console.WriteLine("usuwałem");
                }
            }
            //Console.Write("repair1  ");

            




            stoppedRepair = -1;

            for (int i = 0; i < new2.StringOfOlig.Count() - 1; i++)
            {
                if (new2.StringOfOlig[i].NextOligonukleotid == new2.StringOfOlig[i + 1].ID)
                {
                    if (new2.SequenceLength + 10 - new2.StringOfOlig[i].NmbOfNextMatchNeg < new2.SequenceMax)
                    {
                        new2.SequenceLength += 10 - new2.StringOfOlig[i].NmbOfNextMatchNeg;
                    }
                    else
                    {
                        stoppedRepair = i;
                        new2.StringOfOlig[i].NmbOfNextMatchNeg = -1;
                        //Console.WriteLine("koniec miejsca");
                    }
                }
                else
                {
                    new2.StringOfOlig[i].NmbOfNextMatchNeg =
                        new2.crossConnect(new2.StringOfOlig[i], new2.StringOfOlig[i + 1]);
                    if (new2.SequenceLength + 10 - new2.StringOfOlig[i].NmbOfNextMatchNeg < new2.SequenceMax)
                    {
                        new2.SequenceLength += 10 - new2.StringOfOlig[i].NmbOfNextMatchNeg;
                        new2.StringOfOlig[i + 1].NmbOfPrevMatchNeg = new2.StringOfOlig[i].NmbOfNextMatchNeg;
                        new2.StringOfOlig[i].NextOligonukleotid = new2.StringOfOlig[i + 1].ID;
                        new2.StringOfOlig[i + 1].PrevOligonukleotid = new2.StringOfOlig[i].ID;
                    }
                    else
                    {
                        stoppedRepair = i;
                        new2.StringOfOlig[i].NmbOfNextMatchNeg = -1;
                        //Console.WriteLine("koniec miejsca");

                    }
                }
            }
            //jeżeli długość jest większa niż maks
            if (stoppedRepair != -1)
            {
                for (int i = new2.StringOfOlig.Count() - 1; i > stoppedRepair; i--)
                {
                    new2.StringOfOlig.Remove(new2.StringOfOlig[i]);
                    //Console.WriteLine("usuwałem");
                }
            }


            

            //Console.Write("repaire2   ");
            //Console.Write("Skończono naprawę łańcucha\n");

            //Console.Write("Rozpoczęto naprawę SampleOligs.........  ");

            //przesuwa użyte na koniec
            for (int i = 0; i < new1.StringOfOlig.Count(); i ++)
            {
                Oligonukleotyd temp;
                temp = new1.SampleOligs.Find(x => x.ID == new1.StringOfOlig[i].ID);
                new1.SampleOligs.Remove(temp);
                new1.SampleOligs.Add(temp);
            }
            for (int i = 0; i < new2.StringOfOlig.Count(); i++)
            {
                Oligonukleotyd temp;
                temp = new2.SampleOligs.Find(x => x.ID == new2.StringOfOlig[i].ID);
                new2.SampleOligs.Remove(temp);
                new2.SampleOligs.Add(temp);
            }

            bool finished = false;
            if (new1.SequenceLength < maxLength)
            {
                Oligonukleotyd temp_olig, temp2_olig;
                while (finished == false)
                {
                    int choosen1 = rand.Next(new1.SampleOligs.Count() - new1.StringOfOlig.Count());
                    temp_olig = new1.StringOfOlig.Last();
                    temp2_olig = new1.SampleOligs[choosen1];
                    finished = new1.connect(temp_olig, temp2_olig, finished);
                }
            }

            finished = false;

            if (new2.SequenceLength < maxLength)
            {
                Oligonukleotyd temp_olig, temp2_olig;
                while (finished == false)
                {
                    int choosen1 = rand.Next(new2.SampleOligs.Count() - new2.StringOfOlig.Count());
                    temp_olig = new2.StringOfOlig.Last();
                    temp2_olig = new2.SampleOligs[choosen1];
                    finished = new2.connect(temp_olig, temp2_olig, finished);
                }
            }


            //Console.Write("Skończono naprawę SampleOligs\n");
            int index1 = rand.Next(population.Count());
            population.Insert(index1, new1);
            int index2 = rand.Next(population.Count());
            population.Insert(index2, new2);
            //Console.Write("add\n   ");
        }

        //Dodawanie oligo jak rozwiązanie jest zakrótkie
        public void LongerChain()
        {
            ///Random rand = new Random();
            Oligonukleotyd temp_olig, temp2_olig;
            bool finished=false;
            for (int i = 0; i < population.Count(); i++)
            {
                
                if (population[i].SequenceLength < maxLength)
                {
                    while (finished == false)
                    {
                        int choosenOne = rand.Next(population[i].SampleOligs.Count() - population[i].StringOfOlig.Count());
                        temp_olig = population[i].StringOfOlig.Last();
                        temp2_olig = population[i].SampleOligs[choosenOne];
                        finished = population[i].connect(temp_olig, temp2_olig, finished);
                    }
                }
            }

        }


        //Selekcja
        //dojdzie część rozwiązań z poprzedniego
        public void Selection(int N)
        {
            
            //int N = 4;
            List<DnaChain> temp = new List<DnaChain>();
            int chosenOne=0;
            int m = population.Count() / N;
            for (int i = 0; i < m * N; i = i + N)
            {
                chosenOne = i;
                for (int j = 1; j < N; j++)
                {
                    if (population[chosenOne].Score > population[i + j].Score)
                    {
                        chosenOne = i + j;
                    }
                }
                temp.Add(population[chosenOne]);
            }
            if (population.Count() % N != 0)
            {
                chosenOne = m * N;
                for (int i = m * N; i < population.Count(); i++)
                {
                    if (population[chosenOne].Score > population[i].Score)
                    {
                        chosenOne = i;
                    }
                }
                temp.Add(population[chosenOne]);
            }
            population = temp;
        }

        

    }
}
