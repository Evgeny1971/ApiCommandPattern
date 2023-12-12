using Microsoft.VisualBasic.ApplicationServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly ICommand _command;

        public Form1(ICommand command)
        {
            _command = command;

            InitializeComponent();
            Invoke();
        }

        public  void Invoke()
        {
            // Create user and let her compute


            // User presses calculator buttons

           string result = _command.ExecuteGetResult('+', 100);
             result = _command.ExecuteGetResult('-', 50);
             result = _command.ExecuteGetResult('*', 4);



            // Undo 4 commands


            // Redo 3 commands


            // Wait for user

            this.richTextBox1.Text = result;
            //Console.ReadKey();
        }
     

     
    }
}
