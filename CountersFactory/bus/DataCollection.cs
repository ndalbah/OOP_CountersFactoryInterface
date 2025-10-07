using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountersFactory.bus
{
    public class DataCollection
    {
        private static List<Counter> listOfCounters = new List<Counter>();

        public static List<Counter> ListOfCounters
        {
            get { return listOfCounters; }
            set { listOfCounters = value; }
        }

        public static void Add(Counter newCounter) 
        {
            ListOfCounters.Add(newCounter);
        }

        public static void Remove(Counter newCounter)
        {
            ListOfCounters.Remove(newCounter);
        }

        public static void RemoveAt(int currentIndex)
        {
            ListOfCounters.RemoveAt(currentIndex);
        }

        public static void InsertAt(int currentIndex, Counter currentCounter)
        {
            listOfCounters.Insert(currentIndex, currentCounter);
        }

        public static void Clear() { }

        public static Counter Search(int key, EnumCounterType type)
        {
            Counter foundCounter = null;

            if(type == EnumCounterType.STEP)
            {
                foundCounter = new StepCounter();
            }

            else if(type == EnumCounterType.MODULO)
            {
                foundCounter = new ModuloNCounter();
            }

            foreach(Counter currentCounter in ListOfCounters)
            {
                if (currentCounter.SerialNumber == key)
                {
                    foundCounter = currentCounter;
                }
            }

            return foundCounter;
        }

        public static List<Counter> GetCounterList()
        {
            return ListOfCounters;
        }

        public static List<StepCounter> GetStepCounterList()
        {
            List<StepCounter> listOfStepCounter = new List<StepCounter>();
            foreach(Counter currentCounter in ListOfCounters)
            {
                if(currentCounter is StepCounter)
                {
                    StepCounter currentStepCounter = new StepCounter();
                    currentStepCounter = (StepCounter)currentCounter;
                    listOfStepCounter.Add(currentStepCounter);
                }
            }

            return listOfStepCounter;
        }

        public static List<ModuloNCounter> GetModuloNCounterList()
        {
            List<ModuloNCounter> listOfModuloNCounter = new List<ModuloNCounter>();

            foreach(Counter currentCounter in ListOfCounters)
            {
                if(currentCounter is ModuloNCounter)
                {
                    ModuloNCounter currentModuloNCounter = new ModuloNCounter();
                    currentModuloNCounter = (ModuloNCounter)currentCounter;
                    listOfModuloNCounter.Add(currentModuloNCounter);
                }
            }

            return listOfModuloNCounter;
        }
    }
}
