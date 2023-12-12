using Microsoft.VisualBasic.ApplicationServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly ICommand _command;
        private readonly IRecentDataAccessor _recentDataAccessor;

        public Form1(ICommand command, IRecentDataAccessor recentDataAccessor)
        {
            _command = command;
            _recentDataAccessor = recentDataAccessor;
            InitializeComponent();
            FillComboOperators();
            /*
             IEnumerable<string> operators = _recentDataAccessor.GetOperators();
             foreach (var item in operators)
             {
                 comboBox1.Items.Add(item);

             }
            */
        }

        private void FillComboOperators()
        {
            IEnumerable<string> operators = _recentDataAccessor.GetOperators();
            comboBox1.Items.Clear();
            foreach (var item in operators)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        public void Invoke()
        {
            // Create user and let her compute


            // User presses calculator buttons




            // Undo 4 commands


            // Redo 3 commands


            // Wait for user

            //this.richTextBox1.Text = result;
            //Console.ReadKey();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /// Compute
            int operand1 = int.Parse(textBox1.Text);
            int operand2 = int.Parse(textBox2.Text);
            string @operator = comboBox1.Text;
            string result = _command.ExecuteGetResult(operand1, @operator, operand2);
            richTextBox1.Text = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string @operator = textBox3.Text;
            if (!string.IsNullOrEmpty(@operator))
            {
                bool isSuccess = _recentDataAccessor.InsertNewOperator(@operator);
                if (isSuccess)
                {
                    FillComboOperators();
                    comboBox1.Text = @operator;
                    comboBox1.SelectedText = @operator;
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
