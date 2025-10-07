using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountersFactory.bus
{
    public class ModuloNCounter : Counter
    {
        private int maxLimit;

        public ModuloNCounter() : base() 
        {
            this.maxLimit = 0;
        }

        public ModuloNCounter(int serialNumber, int value, EnumColor color, Date madeDate, EnumCounterType type, int maxLimit) : base(serialNumber, value, color, madeDate, type)
        {
            this.maxLimit = maxLimit;
        }

        public int MaxLimit
        {
            get { return this.maxLimit; }
            set { this.maxLimit = value; }
        }

        public override string GetState()
        {
            string state;
            state = base.GetState() + " | " + this.maxLimit;
            return state;
        }

        public override void Reset()
        {
            this.Value = 1;
        }

        public override void Increment()
        {
            this.Value = this.Value + 2;
        }

        public override void Decrement()
        {
            this.Value = this.Value - 2;
        }
    }
}
