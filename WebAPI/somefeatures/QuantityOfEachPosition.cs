using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.somefeatures
{
    public class QuantityOfEachPosition
    {
        private int _qa;
        private int _dev;
        private int _tl;
        private int _selfself;
        public int QA
        {
            get => _qa;
            set => _qa = value;
        }
        public int Dev
        {
            get => _dev;
            set => _dev = value;
        }
        public int TL
        {
            get => _tl;
            set => _tl = value;
        }
        public int Selfself
        {
            get => _selfself;
            set => _selfself = value;
        }
        public QuantityOfEachPosition() : this(0, 0, 0, 0) { }
        public QuantityOfEachPosition(int numbersOfQA, int numbersOfDev, int numbersOfTL, int selfelf)
        {
            QA = numbersOfQA;
            Dev = numbersOfDev;
            TL = numbersOfTL;
            Selfself = selfelf;
        }
    }
}
