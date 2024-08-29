using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
namespace gasStation
{
    public partial class Form1 : Form
    {
        private double totalRevenue = 0;
        private bool updatingTextBox = false;

        private Timer timer = new Timer();
        private bool showDateTime = true;        
        public Form1()
        {

            InitializeComponent();
            LoadSettingsFromRegistry();
            //таймер переключения даты и часов
            timer1.Start();
            timer1.Interval = 3000;            
        }
        #region блок с расчетом бензина
        private void comboBox1_SelectIndexChander(object senger, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                switch (selectedValue)
                {
                case "A-92":
                    {
                        textBox2.Text = "50,55";
                        break;
                    }
                case "A-95":
                    {
                        textBox2.Text = "51,05";
                        break;
                    }
                case "A-95 Drive":
                    {
                        textBox2.Text = "57,20";
                        break;
                    }
                default:
                    {
                        textBox2.Text = " ";
                        break;
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = radioButton1.Checked;
            textBox3.Enabled = !radioButton1.Checked;
            textBox1.Enabled = true;
            textBox3.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = radioButton2.Checked;
            textBox1.Enabled = !radioButton2.Checked;
            textBox1.Enabled = false;
            textBox3.Enabled = true;
        }
        
        private void textBox3_TextChanged(object sender, EventArgs e)
        {            
            if(!updatingTextBox && double.TryParse(textBox3.Text, out double enteredAbmount))
            {
                if (double.TryParse(textBox2.Text, out double pricePerLiter))
                {
                    updatingTextBox = true;
                    double liters = enteredAbmount / pricePerLiter;
                    textBox1.Text = liters.ToString("F2");
                    updatingTextBox = false;
                    label6.Text = enteredAbmount.ToString("F2");
                }
            }            
        }  

        private void textBox1_TextChanged(object sender, EventArgs e)
        {            
            if (!updatingTextBox && double.TryParse(textBox1.Text, out double enteredAbmount))
            {
                if(double.TryParse(textBox2.Text, out double pricePerLiter))
                {
                    updatingTextBox = true;
                    double liters = pricePerLiter * enteredAbmount;                    
                    updatingTextBox = false;
                    textBox3.Text = liters.ToString("F2");

                }
            }
        }
        #endregion


        #region блок с расчетом кафе

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Text = "40,00";
            textBox6.Text = "50,40";
            textBox8.Text = "70,20";
            textBox10.Text = "40,70";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {            
            textBox5.Enabled = checkBox1.Checked;
            //при отмене гадочки отчистка числа
            if (!checkBox1.Checked)
            {
                textBox5.Text = " ";
            }                     
        }       

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Enabled = checkBox2.Checked;
            if (!checkBox2.Checked)
            {
                textBox7.Text = " ";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = checkBox3.Checked;
            if (!checkBox3.Checked)
            {
                textBox9.Text = " ";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox11.Enabled = checkBox4.Checked;
            if (!checkBox4.Checked)
            {
                textBox11.Text = " ";
            }
        }

        private void UpdateResult()
        {
            double result = 0;
            
            if (double.TryParse(textBox5.Text, out double enteredQuantity1) && double.TryParse(textBox4.Text, out double priceForOne1))
            {
                double liters1 = enteredQuantity1 * priceForOne1;
                result += liters1;                
            }

            if (double.TryParse(textBox7.Text, out double enteredQuantity2) && double.TryParse(textBox6.Text, out double priceForOne2))
            {
                double liters2 = enteredQuantity2 * priceForOne2;
                result += liters2;
            }

            if (double.TryParse(textBox9.Text, out double enteredQuantity3) && double.TryParse(textBox8.Text, out double priceForOne3))
            {
                double liters3 = enteredQuantity3 * priceForOne3;
                result += liters3;
            }

            if (double.TryParse(textBox11.Text, out double enteredQuantity4) && double.TryParse(textBox10.Text, out double priceForOne4))
            {
                double liters4 = enteredQuantity4 * priceForOne4;
                result += liters4;
            }
            label7.Text = $"{result:F2}";
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {            
            UpdateResult();
        }
        #endregion
        

        #region всего
        private void button1_Click(object sender, EventArgs e)
        {
            double result = 0;
            {               
                if (double.TryParse(textBox3.Text, out double summaOil))
                {
                    result += summaOil;
                }

                if (double.TryParse(label7.Text, out double summaCafe))
                {
                    result += summaCafe;
                }
                totalRevenue += result;
                label11.Text = $"{totalRevenue:F2}";
                label10.Text = $"{result:F2}";
                ShapeChangeTimer.Start();
            }
            SaveSettingsToRegistry();
        }
        #endregion


        #region очистка формы
        private void clearForm()
        {
            textBox1.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox11.Text = string.Empty;
            label6.Text = string.Empty;
            label7.Text = string.Empty;
            label10.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            textBox2.Text = string.Empty;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }

        private void ShapeChangeTimer_Tick(object sender, EventArgs e)
        {
            ShapeChangeTimer.Stop();
            DialogResult result = MessageBox.Show("Хотите очистить форму?", "Очистка формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                clearForm();
            }
            else
            {
                ShapeChangeTimer.Start();
            }
        }

        private void Form1_FontChanged(object sender, EventArgs e)
        {
            MessageBox.Show($"Общая сумма выручки за день: {totalRevenue:F2}");
        }
        #endregion

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Переключение между отображением даты и времени
            if (showDateTime)
            {
                toolStripStatusLabel2.Text = DateTime.Now.ToLongDateString();
            }
            else
            {
                toolStripStatusLabel2.Text = DateTime.Now.ToLongTimeString();
            }

            // Инвертирование флага
            showDateTime = !showDateTime;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BringToFront();
            if (panel2.Visible == false) 
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }
        
        private void UpdateColor()
        {
            Color c = Color.FromArgb(this.trackBar1.Value, this.trackBar2.Value, this.trackBar3.Value);
            this.BackColor = c;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            UpdateColor();
        }

        //сохранение настроек
        private void SaveSettingsToRegistry()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\gasStation"))
                {
                    if(key != null)
                    {
                        key.SetValue("TotalRevenue", totalRevenue.ToString());
                        key.SetValue("ShowDateTime", showDateTime.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Загрузка настроек
        private void LoadSettingsFromRegistry()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\gasStation"))
                {
                    if (key != null)                                      
                    {
                        totalRevenue = Convert.ToDouble(key.GetValue("TotalRevenue").ToString());
                        label11.Text = $"{totalRevenue:F2}";
                    }
                    if (key.GetValue("ShowDateTime") != null)
                    {
                        showDateTime = Convert.ToBoolean(key.GetValue("ShowDateTime").ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
        