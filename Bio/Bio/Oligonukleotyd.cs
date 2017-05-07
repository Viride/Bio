using System;


namespace Bio
{
    public class Oligonukleotyd
    {
        public int ID { get; set; }                                         //Identyfikator oligonukleotydu
        public string Ciag { get; set; }                                    //Oligonukleotyd
        public int NmbOfNextMatchingNegative { get; set; }             //Ilość występujących po sobie pozycji na których następny oligonukleotyd się nakłada
        public int NmbOfPrevMatchingNegative { get; set; }              //Ilość występujących po sobie pozycji na których poprzedni oligonukleotyd się nakłada
        public int NmbOfNextMatchingPositive { get; set; }              //Ilość pozycji na których następny oligonukleotyd się nakłada
        public int NmbOfPrevMatchingPositive { get; set; }             //Ilość pozycji na których następny oligonukleotyd się nakłada
        public int NextOligonukleotid { get; set; }                        //ID następnego oligonukleotydu
        public int PrevOligonukleotid { get; set; }                        //ID poprzedniego oligonukleotydu



        public Oligonukleotyd()
        {
            Ciag = null;
            NextOligonukleotid = -1;
            PrevOligonukleotid = -1;
        }

        public Oligonukleotyd(string ciag)
        {
            this.Ciag = ciag;
            NextOligonukleotid = -1;
            PrevOligonukleotid = -1;
        }

        public void CopyFrom( Oligonukleotyd sample)
        {
            Ciag = sample.Ciag;
            ID = sample.ID;
            PrevOligonukleotid = sample.PrevOligonukleotid;
        }

        public void print()
        {
            if (Ciag != null)
            {
                Console.WriteLine("{0}", Ciag);
            }
            else
            {
                Console.WriteLine("Zla dlugosc Oligonukleotydu");
            }
        }

    }
}
