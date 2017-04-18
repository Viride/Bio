using System;


namespace Bio
{
    class Oligonukleotyd
    {
        private int ID;                                         //Identyfikator oligonukleotydu
        private string ciag;                                    //Oligonukleotyd
        private int nmb_of_next_matching_negative;              //Ilość występujących po sobie pozycji na których następny oligonukleotyd się nakłada
        private int nmb_of_prev_matching_negative;              //Ilość występujących po sobie pozycji na których poprzedni oligonukleotyd się nakłada
        private int nmb_of_next_matching_positive;              //Ilość pozycji na których następny oligonukleotyd się nakłada
        private int nmb_of_prev_matching_positive;              //Ilość pozycji na których następny oligonukleotyd się nakłada
        private int next_oligonukleotid;                        //ID następnego oligonukleotydu
        private int prev_oligonukleotid;                        //ID poprzedniego oligonukleotydu

        public int Prev_oligonukleotid
        {
            get
            {
                return prev_oligonukleotid;
            }

            set
            {
                prev_oligonukleotid = value;
            }
        }

        public int Next_oligonukleotid
        {
            get
            {
                return next_oligonukleotid;
            }

            set
            {
                next_oligonukleotid = value;
            }
        }

        public int SetID
        {
            get
            {
                return ID;
            }

            set
            {
                ID = value;
            }
        }

        public string Ciag
        {
            get
            {
                return ciag;
            }

            set
            {
                ciag = value;
            }
        }

        public int Nmb_of_next_matching_negative
        {
            get
            {
                return nmb_of_next_matching_negative;
            }

            set
            {
                nmb_of_next_matching_negative = value;
            }
        }

        public int Nmb_of_prev_matching_negative
        {
            get
            {
                return nmb_of_prev_matching_negative;
            }

            set
            {
                nmb_of_prev_matching_negative = value;
            }
        }

        public int Nmb_of_next_matching_positive
        {
            get
            {
                return nmb_of_next_matching_positive;
            }

            set
            {
                nmb_of_next_matching_positive = value;
            }
        }

        public Oligonukleotyd()
        {
            ciag = null;
        }



        public Oligonukleotyd(string ciag)
        {
            this.ciag = ciag;
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
