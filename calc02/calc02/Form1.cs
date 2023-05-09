using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc02
{
    public partial class Form1 : Form
    {
        Double result = 0;
        string operation = string.Empty;
        string fstNum, secNum;
        bool enterValue = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnMathOperation_Click(object sender, EventArgs e)
        {
            //Nastavení reakce základních matematických operací a vymezení nuly
            if (result != 0) BtnEquals.PerformClick();
            else result = Double.Parse(TxtDisplay1.Text);

            Button button = (Button)sender;
            operation= button.Text;
            enterValue= true;
            if(TxtDisplay1.Text != "0")
            {
                TxtDisplay2.Text = fstNum = $"{result}{operation}";
                TxtDisplay1.Text = string.Empty;
            }
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            //Nastevení tlačítka Rovná se a zajištění základních matematických funkcí a jejich správné prezentování 
            secNum = TxtDisplay1.Text;
            TxtDisplay2.Text = $"{TxtDisplay2.Text}{TxtDisplay1.Text} =";
            if (TxtDisplay1.Text != string.Empty)
            {
                if (TxtDisplay1.Text =="0")TxtDisplay2.Text = string.Empty;
                switch (operation)
                {
                    case "+":
                        TxtDisplay1.Text = (result +Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "-":
                        TxtDisplay1.Text = (result - Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "×":
                        TxtDisplay1.Text = (result * Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "÷":
                        TxtDisplay1.Text = (result / Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {TxtDisplay1.Text} \n");
                        break;
                    default: TxtDisplay2.Text = $"{TxtDisplay1.Text} =";
                        break;
                }

                result = Double.Parse(TxtDisplay1.Text);
                operation = string.Empty;
            }
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka Historie
            PnlHistory.Height = (PnlHistory.Height == 5) ? PnlHistory.Height = 420 : 5;
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            //Nastavení tlalačítka na mazání historie
            RtBoxDisplayHistory.Clear();
            if (RtBoxDisplayHistory.Text == string.Empty)
            {
                RtBoxDisplayHistory.Text = "Historie příkladů:\n";
            }               
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka BackSpace
            if (TxtDisplay1.Text.Length > 0)
                TxtDisplay1.Text = TxtDisplay1.Text.Remove(TxtDisplay1.Text.Length - 1, 1);
            if (TxtDisplay1.Text == string.Empty) TxtDisplay1.Text = "0";
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka Clear
            TxtDisplay1.Text = "0";
            TxtDisplay2.Text = string.Empty;
            result = 0;
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka CE (Clear Entry)
            TxtDisplay1.Text = "0";
        }

        private void BtnOperations_Click(object sender, EventArgs e)
        {
            //Fungování a prezentování speciálních matematických operací
            Button button = (Button)sender;
            operation = button.Text;
            switch (operation)
            {
                case "√x":
                    TxtDisplay2.Text = $"√({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(Math.Sqrt(Double.Parse(TxtDisplay1.Text)));
                    break;
                case "x²":
                    TxtDisplay2.Text = $"({TxtDisplay1.Text})²";
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text) * Convert.ToDouble(TxtDisplay1.Text));
                    break;
                case "⅟x":
                    TxtDisplay2.Text = $"1/({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(1.0 / Convert.ToDouble(TxtDisplay1.Text));
                    break;
                case "%":
                    TxtDisplay2.Text = $"({TxtDisplay1.Text})%";
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text) / Convert.ToDouble(100));
                    break;
                case "±":
                    TxtDisplay2.Text = $"(Negace {TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(-1 * Convert.ToDouble(TxtDisplay1.Text));
                    break;
            }
            RtBoxDisplayHistory.AppendText($"{TxtDisplay2.Text}={TxtDisplay1.Text} \n");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            //Nastavuje tlačítko na zavření aplikace
            Application.Exit();
        }

        private void TxtDisplay1_KeyP(object sender, KeyPressEventArgs e)
        {
            //Určuje funkčnost vstupů přes klávesnici do TxtDisaplaye1
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            //Povolí pouze jednu desetinnou čárku
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void TxtDisplay_TextChanged(object sender, EventArgs e)
        {
            //Zajišťuje aby se přes copypaste dali vkládat pouze čísla(a +-E a ∞) 
            if (System.Text.RegularExpressions.Regex.IsMatch(TxtDisplay1.Text, "[^0-9,E+∞-]"))
            {
                TxtDisplay1.Text = "Neplatné zadání";
                MessageBox.Show("Prosím vložte pouze čísla.");
                TxtDisplay1.Text = string.Empty;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Nastavení responsitivy klávesnice (čísla a znaky)
            if (e.KeyCode == Keys.NumPad0) {Btn0.PerformClick();}
            if (e.KeyCode == Keys.D0) { Btn0.PerformClick();}
            if (e.KeyCode == Keys.NumPad1) {Btn1.PerformClick();}
            if (e.KeyCode == Keys.D1) { Btn1.PerformClick(); }
            if (e.KeyCode == Keys.NumPad2) {Btn2.PerformClick();}
            if (e.KeyCode == Keys.D2) { Btn2.PerformClick(); }
            if (e.KeyCode == Keys.NumPad3) {Btn3.PerformClick();}
            if (e.KeyCode == Keys.D3) { Btn3.PerformClick(); }
            if (e.KeyCode == Keys.NumPad4) {Btn4.PerformClick();}
            if (e.KeyCode == Keys.D4) { Btn4.PerformClick(); }
            if (e.KeyCode == Keys.NumPad5) {Btn5.PerformClick();}
            if (e.KeyCode == Keys.D5) { Btn5.PerformClick(); }
            if (e.KeyCode == Keys.NumPad6) {Btn6.PerformClick();}
            if (e.KeyCode == Keys.D6) { Btn6.PerformClick(); }
            if (e.KeyCode == Keys.NumPad7) {Btn7.PerformClick();}
            if (e.KeyCode == Keys.D7) { Btn7.PerformClick(); }
            if (e.KeyCode == Keys.NumPad8) {Btn8.PerformClick();}
            if (e.KeyCode == Keys.D8) { Btn8.PerformClick(); }
            if (e.KeyCode == Keys.NumPad9) {Btn9.PerformClick();}
            if (e.KeyCode == Keys.D9) { Btn9.PerformClick(); }
            if (e.KeyCode == Keys.Decimal) { BtnDesimal.PerformClick(); }
            if (e.KeyCode == Keys.Add) { BtnAdd.PerformClick(); }
            if (e.KeyCode == Keys.Subtract) { BtnSubtraction.PerformClick(); }
            if (e.KeyCode == Keys.Multiply) { BtnMultiply.PerformClick(); }
            if (e.KeyCode == Keys.Divide) { BtnDivision.PerformClick(); }
            if (e.KeyCode == Keys.Back) { BtnBackSpace.PerformClick(); }  
            if (e.KeyCode == Keys.H) { BtnHistory.PerformClick(); }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Nastavení responsivity Entru
            if (keyData == Keys.Enter)
            {
                BtnEquals.PerformClick(); 
                return true; 
            }
            return base.ProcessCmdKey(ref msg, keyData); 
        }


        private void BtnNum_Click(object sender, EventArgs e)
        {
            //Když TxtDisplay1 prezentuje 0 při zadání další hodnoty zmizí 
            if (TxtDisplay1.Text == "0" || enterValue) TxtDisplay1.Text = string.Empty;

            enterValue = false;
            Button button = (Button)sender;

            //Definuje tlačítka 0-9 a desetinnou čárku 
            if (button.Text == ",")
            {
                if (!TxtDisplay1.Text.Contains(","))
                    TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
            }
            else TxtDisplay1.Text = TxtDisplay1.Text + button.Text;


        }
    }
}
