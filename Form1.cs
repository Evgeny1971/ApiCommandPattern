using Microsoft.VisualBasic.ApplicationServices;
using WinFormsApp1.InterfacesApi;
using WinFormsApp1.Entities;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly ICommand _command;
        private readonly EntityCalculator _entityCalculator = null;
        public Form1(ICommand command)
        {
            _entityCalculator = new EntityCalculator();
            _command = command;
            InitializeComponent();
            FillComboOperators();
            comboBox1.SelectedIndex = 0;
            textBox1.Text ="1";
            textBox2.Text = "2";
         
        }

        private void FillComboOperators()
        {
            IEnumerable<string> operators = _command.GetOperators();
            comboBox1.Items.Clear();
            foreach (var item in operators)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /// Compute
            _entityCalculator.Operand1 = int.Parse(textBox1.Text);
            _entityCalculator.Operand2  = int.Parse(textBox2.Text);
            _entityCalculator.Operator  = comboBox1.Text;
            _command.ExecuteGetResult(_entityCalculator);
            richTextBox1.Text = _entityCalculator.Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _entityCalculator.Operator = textBox3.Text;
            if (!string.IsNullOrEmpty(_entityCalculator.Operator))
            {
                //bool isSuccess = _recentDataAccessor.InsertNewOperator(@operator);

                bool isSuccess=_command.InsertNewOperator(_entityCalculator);
                if (isSuccess)
                {
                    FillComboOperators();
                    comboBox1.Text = _entityCalculator.Operator;
                    //comboBox1.SelectedText = _entityCalculator.Operator;
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
