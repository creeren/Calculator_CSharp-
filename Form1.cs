using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double first_number;
        double second_number;
        string operation;
        bool op_pressed = false;
        bool percentage_pressed = false;
        bool prefix = false;
        int equals_pressed = 0;
        int prefix_counter = 0;
        double result;
       
        public Form1()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// Numbers.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            
            if (textBox.Text == "0" || op_pressed == true)
            {
                textBox.Text = " ";
            }
            else if (equals_pressed == 1)
            {
                btn_clear.PerformClick();
                btn_backwards.PerformClick();
            }
            Button button = (Button)sender;
            op_pressed = false;
            btn_equal.Focus();

            if (button.Text == ".")
            {
                if (!textBox.Text.Contains("."))
                {
                    textBox.Text += button.Text;
                }

            }
            else
                textBox.Text += button.Text;   
        }

        /// <summary>
        /// Operators.
        /// </summary>
        private void Operator_Click(object sender, EventArgs e)
        {           
            Button button = (Button)sender;

            //if operator is already pressed 
            if (first_number != 0 && prefix == false)
            {
                    textBox_up.Text = textBox_up.Text + " " +textBox.Text + " " ;
                
                switch (operation)
                {
                        case "+":
                            Addiction();                       
                        break;
                        case "-":
                            Substraction();
                            break;
                        case "*":
                            Multiply();
                            break;
                        case "/":
                            second_number = double.Parse(textBox.Text);

                            if (second_number != 0) 
                            {
                                Dividion();
                            }
                            else
                            {
                                textBox.Text = "Cannot divide with zero";
                            }
                            break;                         
                }
                operation = button.Text;
                textBox_up.Text = textBox_up.Text + " " + operation + " " ;
                op_pressed = true;
            }
            else
            {
                //first_number number before operator 
                first_number = double.Parse(textBox.Text); 

                //operator saved
                operation = button.Text;
              
                textBox_up.Text = textBox.Text + " " + operation + " ";
                op_pressed = true;

                //prefix to default to be available to use for next number
                prefix = false;
             }
             btn_equal.Focus();
        }
            
        /// <summary>
        /// Equal
        /// </summary>
        private void Equal_Click(object sender, EventArgs e)
        {
            //by repeatedly pressing the equals 
            if (equals_pressed == 1) 
            {
                textBox.Text = second_number.ToString();
                switch (operation)
                {
                    case "+":
                        textBox_up.Text = first_number + " " + operation + " " + second_number + " =";
                        Addiction();                        
                        break;
                    case "-":
                        textBox_up.Text = first_number + " " + operation + " " + second_number + " =";
                        Substraction();
                        break;
                    case "*":
                        textBox_up.Text = first_number + " " + operation + " " + second_number + " =";
                        Multiply();
                        break;
                    case "/":
                        second_number = double.Parse(textBox.Text);

                        //Checks if divisior is not 0
                        if (second_number != 0) 
                        {
                            textBox_up.Text = first_number + operation + second_number + " =";
                            Dividion();                           
                        }
                        else
                        {
                            textBox.Text = "Cannot divide by zero";
                        }
                        break;
                }   
            } 
            else if (equals_pressed == 0) 
            {
                switch (operation)
                {
                    case "+":
                        if (percentage_pressed == true)
                        {
                            textBox_up.Text += " =";
                            Addiction();  
                        }
                        else
                        {
                            textBox_up.Text = textBox_up.Text + textBox.Text +  " =";
                            Addiction();                  
                        }
                        equals_pressed = +1;
                        break;
                    case "-":
                        if (percentage_pressed == true)
                        {
                            textBox_up.Text += " =";
                            Substraction();               
                        }
                        else
                        {
                            textBox_up.Text = textBox_up.Text + textBox.Text + " =";
                            Substraction();                 
                        }
                        equals_pressed = +1;
                        break;
                    case "*":
                        if (percentage_pressed == true)
                        {
                            textBox_up.Text += " =";
                            Multiply();           
                        }
                        else
                        {
                            textBox_up.Text = textBox_up.Text + textBox.Text +  " =";
                            Multiply();             
                        }
                        equals_pressed = +1;
                        break;
                    case "/":                        
                        second_number = double.Parse(textBox.Text);
                        
                        if (second_number != 0) 
                        {
                            // by pressing percentage other output, othervwise ordinary calucation
                            if (percentage_pressed == true) 
                            {
                                textBox_up.Text += " =";
                                Dividion();
                            }
                            else 
                            {
                                textBox_up.Text = textBox_up.Text + textBox.Text + " =";
                                Dividion();
                            }
                        }
                        else
                        {
                            textBox.Text = "Cannot divide by zero";
                        }
                        equals_pressed = +1;
                        break;
                }
            }      
        }

        /// <summary>
        /// Clears the last entry. CE.
        /// </summary>
        private void Btn_Clear_Entry_Click(object sender, EventArgs e)
        {
            textBox.Text = "0";
            btn_equal.Focus();
        }
        
        /// <summary>
        /// Clears all entries. C.
        /// </summary>
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            textBox_up.Clear();
            textBox.Text = "0";
            first_number = 0;
            equals_pressed = 0;
            btn_equal.Focus();
        }

        /// <summary>
        /// Clears the last entered digit. 
        /// </summary>
        private void Btn_Backwards_Click(object sender, EventArgs e)
        {
            int lenght = textBox.TextLength - 1;
            string text = textBox.Text;
            textBox.Clear();
            for(int i = 0; i < lenght; i++)
            {
                textBox.Text += text[i];
            }
            btn_equal.Focus();
        }

        /// <summary>
        /// Performs square calculation
        /// </summary>
        private void Btn_Square_Click(object sender, EventArgs e)
        {         
            first_number = double.Parse(textBox.Text);
            textBox_up.Text = "√( " + first_number + " )";
            first_number = Math.Sqrt(first_number);
            textBox.Text = first_number.ToString();
            btn_equal.Focus();
        }

        /// <summary>
        /// Performs percentage calculation
        /// </summary>
        private void Btn_Percentage_Click(object sender, EventArgs e)
        {
            percentage_pressed = true;
            double result;
            second_number = double.Parse(textBox.Text);
            result = (second_number / 100) * first_number;
            textBox.Text = result.ToString();
            textBox_up.Text += result.ToString();
            btn_equal.Focus();
        }

        /// <summary>
        /// Performs fraction calculation
        /// </summary>
        private void Btn_Fraction_Click(object sender, EventArgs e)
        {
            double result;
            first_number = double.Parse(textBox.Text);
            result = 1.0 / first_number;
            textBox.Text = result.ToString();
            textBox_up.Text += "1/( "+ first_number +" )";
            btn_equal.Focus();
        }

        /// <summary>
        /// Prefix sign
        /// </summary>
        private void Btn_Prefix_Click(object sender, EventArgs e)
        {           
            // prefix activisation
            prefix = true; 
            // pressing prefix next time
            if (prefix_counter == 1) 
            {   
                second_number = double.Parse(textBox.Text);
                textBox.Text = "-" + second_number.ToString();
                prefix = false;                
            }
            else
            {
                //pressing prefix first time
                first_number = double.Parse(textBox.Text); 
                textBox.Text = "-" + first_number.ToString();
                prefix_counter = +1;              
            }
            btn_equal.Focus();
        }

        /// <summary>
        /// Performs addiction calculation
        /// </summary>
        public void Addiction()
        {
            second_number = double.Parse(textBox.Text);
            result = first_number + second_number;
            first_number = result;
            textBox.Text = first_number.ToString();
        }

        /// <summary>
        /// Performs substraction calculation
        /// </summary>
        public void Substraction()
        {
            second_number = double.Parse(textBox.Text);
            result = first_number - second_number;
            first_number = result;
            textBox.Text = first_number.ToString();
        }

        /// <summary>
        /// Performs multiply calculation
        /// </summary>
        public void Multiply()
        {
            second_number = double.Parse(textBox.Text);
            result = first_number * second_number;
            first_number = result;
            textBox.Text = first_number.ToString();
        }

        /// <summary>
        /// Performs Division calculation
        /// </summary>
        public void Dividion()
        {
            result = first_number / second_number;
            first_number = result;
            textBox.Text = first_number.ToString();
        }

        /// <summary>
        /// Using keyboard
        /// </summary>
        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad0:
                case Keys.D0:
                    btn0.PerformClick();
                    break;
                case Keys.NumPad1:
                case Keys.D1:
                    btn1.PerformClick();
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    btn2.PerformClick();
                    break;
                case Keys.NumPad3:
                case Keys.D3:
                    btn3.PerformClick();
                    break;
                case Keys.NumPad4:
                case Keys.D4:
                    btn4.PerformClick();
                    break;
                case Keys.NumPad5:
                case Keys.D5:
                    btn5.PerformClick();
                    break;
                case Keys.NumPad6:
                case Keys.D6:
                    btn6.PerformClick();
                    break;
                case Keys.NumPad7:
                case Keys.D7:
                    btn7.PerformClick();
                    break;
                case Keys.NumPad8:
                case Keys.D8:
                    btn8.PerformClick();
                    break;
                case Keys.NumPad9:
                case Keys.D9:
                    btn9.PerformClick();
                    break;
                case Keys.Decimal:
                    btn_dec.PerformClick();
                    break;
                case Keys.Add:
                    btn_add.PerformClick();
                    break;
                case Keys.Subtract:
                    btn_substract.PerformClick();
                    break;
                case Keys.Multiply:
                    btn_multiply.PerformClick();
                    break;
                case Keys.Divide:
                    btn_divide.PerformClick();
                    break;
                case Keys.Back:
                    btn_backwards.PerformClick();
                    break;
                case Keys.Enter:
                    btn_equal.PerformClick();
                    break;
            }     
        } 
    }   
}

