using NewspaperSellerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperSellerSimulation
{
    public partial class Form2 : Form
    {
        SimulationSystem sm = new SimulationSystem();

        public Form2(SimulationSystem sm)
        {
            this.sm = sm;
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
         textBox1.Text = sm.PerformanceMeasures.TotalSalesProfit.ToString();
            textBox2.Text = sm.PerformanceMeasures.TotalCost.ToString();
            textBox3.Text = sm.PerformanceMeasures.TotalLostProfit.ToString();
            textBox4.Text = sm.PerformanceMeasures.TotalScrapProfit.ToString();
            textBox5.Text = sm.PerformanceMeasures.TotalNetProfit.ToString();
            textBox6.Text = sm.PerformanceMeasures.DaysWithMoreDemand.ToString();
            textBox7.Text = sm.PerformanceMeasures.DaysWithUnsoldPapers.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
        }
    }
}
