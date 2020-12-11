using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicatie_de_masurare_a_temperaturii
{
    public partial class Form1 : Form
    {
        int cnt;
        public Form1()
        {
            InitializeComponent();
            cnt = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start(); // porneste timer-ul 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //opreste timer-ul
        }

        double getTemp(string cmd)
        {
            double res=0;
            string resDev = "";
            if (serialPort1.IsOpen) // daca exista conexiune deschisa de la o executie anterioara atunci o inchide
                serialPort1.Close();
            serialPort1.Open(); // dechide conexiunea
            serialPort1.Write(cmd + "\r"); // scrie comanda catre  microcontroller pe PORT impreuna cu caracterul terminal
            while (!resDev.Contains("<")) // cat timp in resDev nu exista "<"     <-sfarsit de string din raspunsul microcontrollerului  
                resDev += serialPort1.ReadLine();
            resDev = resDev.Substring(0, resDev.IndexOf(".") + 2);// ia primele doua cifre inainte de punct si urmatoarele doua dupa punct ex : 23.2344
            serialPort1.Close(); // inchide conexiunea
            try
            {
                res = Convert.ToDouble(resDev);
            }
            catch (Exception ex)
            {

            }
            return res;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cnt++;
            double temp = getTemp("t");
            chart1.Series["Temperatura"].Points.AddXY(cnt, temp);
        }
    }
}
