using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountersFactory.bus
{
    internal interface ICountable
    {
        public abstract void Reset();
        public abstract void Increment();
        public abstract void Decrement();
    }
}
