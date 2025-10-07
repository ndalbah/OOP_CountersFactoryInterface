using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountersFactory.bus
{
    public class StepCounter : Counter
    {
        private int step;

        public StepCounter() : base()
        {
            this.step = 0;
        }

        public StepCounter(int serialNumber, int value, EnumColor color, Date madeDate, EnumCounterType type, int step) : base(serialNumber, value, color, madeDate, type)
        {
            this.step = step;
        }

        public int Step
        {
            get { return this.step; }
            set { this.step = value; }
        }

        public override string GetState()
        {
            string state;
            state = base.GetState() + " | " + this.step;
            return state;
        }

        public override void Increment()
        {
            this.Value = this.Value + 1;
        }

        public override void Decrement()
        {
            this.Value = this.Value - 1;
        }
    }
}
