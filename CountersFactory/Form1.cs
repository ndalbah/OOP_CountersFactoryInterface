using CountersFactory.bus;
using System.Xml.Serialization;
using System.Xml;

namespace CountersFactory
{
    public partial class Form1 : Form
    {
        List<Counter>? listOfCounters = new List<Counter>();
        List<StepCounter>? listOfStepCounter = new List<StepCounter>();
        List<ModuloNCounter> listOfModuloNCounter = new List<ModuloNCounter>();

        Counter currentCounter;
        StepCounter currentStepCounter;
        ModuloNCounter currentModuloNCounter;
        Date currentMadeDate;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int serialNumber, value, currentMonth, currentDay, currentYear;

            try
            {
                currentMadeDate = new Date();

                EnumCounterType currentCounterType;
                Enum.TryParse(this.comboBoxType.Text, out currentCounterType);

                EnumColor currentColor;
                Enum.TryParse(this.comboBoxColor.Text, out currentColor);

                serialNumber = Convert.ToInt32(this.textBoxSerialNumber.Text);
                value = Convert.ToInt32(this.textBoxValue.Text);

                currentMonth = Convert.ToInt32(this.textBoxMonth.Text);
                currentDay = Convert.ToInt32(this.textBoxDay.Text);
                currentYear = Convert.ToInt32(this.textBoxYear.Text);

                currentMadeDate.Month = currentMonth;
                currentMadeDate.Day = currentDay;
                currentMadeDate.Year = currentYear;

                if (currentCounterType == EnumCounterType.STEP)
                {
                    currentStepCounter = new StepCounter();

                    currentStepCounter.Type = currentCounterType;
                    currentStepCounter.SerialNumber = serialNumber;
                    currentStepCounter.Value = value;
                    currentStepCounter.Color = currentColor;
                    currentStepCounter.MadeDate = currentMadeDate;

                    try
                    {
                        currentStepCounter.Step = Convert.ToInt32(this.textBoxStep.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (DataCollection.GetCounterList() != null)
                    {
                        DataCollection.Add(currentStepCounter);
                    }
                }
                else if (currentCounterType == EnumCounterType.MODULO)
                {
                    currentModuloNCounter = new ModuloNCounter();

                    currentModuloNCounter.Type = currentCounterType;
                    currentModuloNCounter.SerialNumber = serialNumber;
                    currentModuloNCounter.Value = value;
                    currentModuloNCounter.Color = currentColor;
                    currentModuloNCounter.MadeDate = currentMadeDate;

                    try
                    {
                        currentModuloNCounter.MaxLimit = Convert.ToInt32(this.textBoxMaxLimit.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (DataCollection.ListOfCounters != null)
                    {
                        DataCollection.Add(currentModuloNCounter);
                    }
                }
                else
                {
                    MessageBox.Show("You cannot enter an undefined counter. Please select a counter type.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n \t You must input valid data");
                this.textBoxSerialNumber.Focus();
            }

            this.buttonAdd.Enabled = false;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            this.listBoxCounterFactory.Items.Clear();

            if (DataCollection.GetCounterList() != null && DataCollection.GetCounterList().Count > 0 && this.listBoxCounterFactory.Items.Count == 0)
            {
                foreach (Counter currentCounter in DataCollection.GetCounterList())
                {
                    if (currentCounter is StepCounter)
                    {
                        currentStepCounter = (StepCounter)currentCounter;
                        this.listBoxCounterFactory.Items.Add(currentCounter.GetState());
                    }
                    else if (currentCounter is ModuloNCounter)
                    {
                        currentModuloNCounter = (ModuloNCounter)currentCounter;
                        this.listBoxCounterFactory.Items.Add(currentModuloNCounter.GetState());
                    }
                }
            }
            else
            {
                MessageBox.Show("The list of counters is already printed in the list box or the list of counters in memory is empty...");
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.comboBoxType.Text = EnumCounterType.UNDEFINED.ToString();
            this.comboBoxColor.Text = EnumColor.UNDEFINED.ToString();
            this.textBoxSerialNumber.Text = string.Empty;
            this.textBoxValue.Text = string.Empty;
            this.textBoxMonth.Text = string.Empty;
            this.textBoxDay.Text = string.Empty;
            this.textBoxYear.Text = string.Empty;
            this.textBoxStep.Text = string.Empty;
            this.textBoxMaxLimit.Text = string.Empty;
            this.textBoxStep.Enabled = true;
            this.textBoxMaxLimit.Enabled = true;
            this.textBoxSerialNumber.Focus();
            this.listBoxCounterFactory.Items.Clear();
            this.buttonAdd.Enabled = true;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            bool found = false;
            Counter counterToSearch = null;

            StepCounter currentStepCounter;
            ModuloNCounter currentModuloNCounter;

            if (DataCollection.GetCounterList != null)
            {
                foreach (Counter currentCounter in DataCollection.GetCounterList())
                {
                    if (currentCounter.SerialNumber == Convert.ToInt32(this.textBoxSerialNumber.Text))
                    {
                        found = true;
                        counterToSearch = currentCounter;
                        break;
                    }
                }
            }

            if (found)
            {
                if (counterToSearch.Type == EnumCounterType.STEP)
                {
                    currentStepCounter = (StepCounter)counterToSearch;

                    MessageBox.Show("Step counter found. " + currentStepCounter.GetState(), "Event Programming with C#", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.listBoxCounterFactory.Items.Add(currentStepCounter.GetState());
                }
                else if (counterToSearch.Type == EnumCounterType.MODULO)
                {
                    currentModuloNCounter = (ModuloNCounter)counterToSearch;

                    MessageBox.Show("Modulo counter found. " + currentModuloNCounter.GetState(), "Event Programming with C#", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.listBoxCounterFactory.Items.Add(currentModuloNCounter.GetState());
                }
            }
            else
            {
                MessageBox.Show("Counter not found..", "Event Programming with C#", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (EnumColor item in Enum.GetValues(typeof(EnumColor)))
            {
                this.comboBoxColor.Items.Add(item);
            }

            this.comboBoxColor.Text = Convert.ToString(EnumColor.UNDEFINED);

            foreach (EnumCounterType item in Enum.GetValues(typeof(EnumCounterType)))
            {
                this.comboBoxType.Items.Add(item);
            }

            this.comboBoxType.Text = Convert.ToString(EnumCounterType.UNDEFINED);
        }

        int currentIndex = -1;
        private void listBoxCounterFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentIndex = this.listBoxCounterFactory.SelectedIndex;

            EnumCounterType currentCounterType;

            this.listOfCounters = DataCollection.GetCounterList();

            if (this.listOfCounters != null)
            {
                currentCounterType = this.listOfCounters[currentIndex].Type;

                this.comboBoxType.Text = Convert.ToString(currentCounterType);

                if (currentIndex >= 0 && currentCounterType == EnumCounterType.STEP)
                {
                    StepCounter currentStepCounter = new StepCounter();

                    currentStepCounter = (StepCounter)this.listOfCounters[currentIndex];

                    this.textBoxSerialNumber.Text = currentStepCounter.SerialNumber.ToString();
                    this.textBoxValue.Text = currentStepCounter.Value.ToString();

                    this.comboBoxColor.Text = currentStepCounter.Color.ToString();

                    this.textBoxMonth.Text = currentStepCounter.MadeDate.Month.ToString();
                    this.textBoxDay.Text = currentStepCounter.MadeDate.Day.ToString();
                    this.textBoxYear.Text = currentStepCounter.MadeDate.Year.ToString();

                    this.textBoxStep.Text = currentStepCounter.Step.ToString();
                }
                else if (currentIndex >= 0 && currentCounterType == EnumCounterType.MODULO)
                {
                    ModuloNCounter currentModuloNCounter = new ModuloNCounter();

                    currentModuloNCounter = (ModuloNCounter)this.listOfCounters[currentIndex];

                    this.textBoxSerialNumber.Text = currentModuloNCounter.SerialNumber.ToString();
                    this.textBoxValue.Text = currentModuloNCounter.Value.ToString();

                    this.comboBoxColor.Text = currentModuloNCounter.Color.ToString();

                    this.textBoxMonth.Text = currentModuloNCounter.MadeDate.Month.ToString();
                    this.textBoxDay.Text = currentModuloNCounter.MadeDate.Day.ToString();
                    this.textBoxYear.Text = currentModuloNCounter.MadeDate.Year.ToString();

                    this.textBoxMaxLimit.Text = currentModuloNCounter.MaxLimit.ToString();
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            EnumCounterType currentCounterType;
            Enum.TryParse(this.comboBoxType.Text, out currentCounterType);

            if (currentIndex < 0 || currentIndex >= DataCollection.ListOfCounters.Count)
            {
                MessageBox.Show("Choose from the list box the counter to update.");
                return;
            }

            if (DataCollection.ListOfCounters != null)
            {
                currentCounterType = DataCollection.ListOfCounters[currentIndex].Type;
            }

            if (currentIndex >= 0 && listOfCounters != null)
            {
                if (currentCounterType == EnumCounterType.STEP)
                {
                    StepCounter currentStepCounter = new StepCounter();

                    currentStepCounter.Type = currentCounterType;
                    currentStepCounter.SerialNumber = Convert.ToInt32(this.textBoxSerialNumber.Text);
                    currentStepCounter.Value = Convert.ToInt32(this.textBoxValue.Text);

                    EnumColor currentColor;
                    Enum.TryParse(this.comboBoxColor.Text, out currentColor);
                    currentStepCounter.Color = currentColor;

                    Date currentDate = new Date();
                    currentDate.Month = Convert.ToInt32(this.textBoxMonth.Text);
                    currentDate.Day = Convert.ToInt32(this.textBoxDay.Text);
                    currentDate.Year = Convert.ToInt32(this.textBoxYear.Text);

                    currentStepCounter.MadeDate = currentDate;

                    currentStepCounter.Step = Convert.ToInt32(this.textBoxStep.Text);

                    DataCollection.RemoveAt(currentIndex);

                    DataCollection.InsertAt(currentIndex, currentStepCounter);
                }
                else if (currentCounterType == EnumCounterType.MODULO)
                {
                    ModuloNCounter currentModuloNCounter = new ModuloNCounter();

                    currentModuloNCounter.Type = currentCounterType;
                    currentModuloNCounter.SerialNumber = Convert.ToInt32(this.textBoxSerialNumber.Text);
                    currentModuloNCounter.Value = Convert.ToInt32(this.textBoxValue.Text);

                    EnumColor currentColor;
                    Enum.TryParse(this.comboBoxColor.Text, out currentColor);
                    currentModuloNCounter.Color = currentColor;

                    Date currentDate = new Date();
                    currentDate.Month = Convert.ToInt32(this.textBoxMonth.Text);
                    currentDate.Day = Convert.ToInt32(this.textBoxDay.Text);
                    currentDate.Year = Convert.ToInt32(this.textBoxYear.Text);

                    currentModuloNCounter.MadeDate = currentDate;

                    currentModuloNCounter.MaxLimit = Convert.ToInt32(this.textBoxMaxLimit.Text);

                    DataCollection.RemoveAt(currentIndex);
                    DataCollection.InsertAt(currentIndex, currentModuloNCounter);
                }
            }
            else
            {
                MessageBox.Show("Choose from the listBox the counter to UPDATE.");
            }

            this.listBoxCounterFactory.Items.Clear();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (currentIndex >= 0 && this.listOfCounters != null)
            {
                DataCollection.ListOfCounters.RemoveAt(currentIndex);
            }
            else
            {
                MessageBox.Show("Choose from the list box the counter to REMOVE");
            }

            this.listBoxCounterFactory.Items.Clear();
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            if (DataCollection.ListOfCounters != null)
            {
                FileManager.WriteToXmlFile(DataCollection.ListOfCounters);
            }
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            this.listOfCounters.Clear();

            DataCollection.ListOfCounters = FileManager.ReadFromXmlFile();
            if ((DataCollection.ListOfCounters = FileManager.ReadFromXmlFile()) != null)
            {
                foreach (Counter item in DataCollection.ListOfCounters)
                {
                    if (item is StepCounter)
                    {
                        currentStepCounter = (StepCounter)item;
                        this.listBoxCounterFactory.Items.Add(currentStepCounter.GetState());
                    }
                    else if (item is ModuloNCounter)
                    {
                        currentModuloNCounter = (ModuloNCounter)item;
                        this.listBoxCounterFactory.Items.Add(currentModuloNCounter.GetState());
                    }
                }
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnumCounterType currentCounterType;

            Enum.TryParse(this.comboBoxType.Text, out currentCounterType);

            if (currentCounterType == EnumCounterType.STEP)
            {
                this.textBoxStep.Enabled = true;
                this.textBoxMaxLimit.Enabled = false;
            }


            else if (currentCounterType == EnumCounterType.MODULO)
            {
                this.textBoxStep.Enabled = false;
                this.textBoxMaxLimit.Enabled = true;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Application written by Noah Dalbah | 2333960", "Event Programming with C#", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}