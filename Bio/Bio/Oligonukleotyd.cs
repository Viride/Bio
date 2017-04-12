using System;


namespace Bio
{
    class Oligonukleotyd
    {
        private int ID;                                     //Identyfikator oligonukleotydu
        private string ciag;                                //Oligonukleotyd
        private int nmb_of_next_matching_negative;          //Ilość występujących po sobie pozycji na których następny oligonukleotyd się nakłada
        private int nmb_of_prev_matching_negative;          //Ilość występujących po sobie pozycji na których poprzedni oligonukleotyd się nakłada
        private int nmb_of_next_matching_positive;          //Ilość pozycji na których następny oligonukleotyd się nakłada
        private int nmb_of_prev_matching_positive;          //Ilość pozycji na których następny oligonukleotyd się nakłada
        private int next_oligonukleotid;                    //ID następnego oligonukleotydu
        private int prev_oligonukleotid;                    //ID poprzedniego oligonukleotydu


        public Oligonukleotyd()
        {
            ciag = null;
        }

        public Oligonukleotyd(string ciag)
        {
            this.ciag = ciag;
        }

        public void SetCiag(string ciag)
        {
            if(ciag.Length == 10)
            {
                this.ciag = ciag;
            }
            else
            {
                this.ciag = null;
            }
        }

        public void print()
        {
            if (ciag != null)
            {
                Console.WriteLine("{0}", ciag);
            }
            else
            {
                Console.WriteLine("Zla dlugosc Oligonukleotydu");
            }
        }

    }
}
