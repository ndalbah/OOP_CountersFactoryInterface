using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CountersFactory.bus
{
    [XmlInclude(typeof(StepCounter))]
    [XmlInclude(typeof(ModuloNCounter))]
    public abstract class Counter : ICountable
    {
        private int serialNumber;
        private int value;
        private EnumColor color;
        private Date madeDate;
        private EnumCounterType type;

        public int SerialNumber
        {
            get { return serialNumber; }
            set { this.serialNumber = value; }
        }

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public EnumColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public Date MadeDate
        {
            get { return this.madeDate; }
            set { this.madeDate = value; }
        }

        public EnumCounterType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public Counter()
        {
            this.serialNumber = 0;
            this.value = 0;
            this.color = EnumColor.UNDEFINED;
            this.type = EnumCounterType.UNDEFINED;
        }
        
        public Counter(int serialNumber, int value, EnumColor color, Date madeDate, EnumCounterType type)
        {
            this.serialNumber = serialNumber;
            this.value= value;
            this.color = color;
            this.madeDate = madeDate;
            this.type = type;
        }

        public override string ToString()
        {
            string state;
            state = this.serialNumber + " | " + this.value + " | " + this.color + " | " + this.madeDate + " | " + this.type;
            return state;
        }
        public virtual string GetState()
        {
            string state;
            state = this.serialNumber + " | " + this.value + " | " + this.color + " | " + this.madeDate + " | " + this.type;
            return state;
        }

        public virtual void Reset()
        {
            this.value = 0;
        }

        public abstract void Increment();

        public abstract void Decrement();
    }
}
