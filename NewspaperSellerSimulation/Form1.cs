using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {

        public SimulationSystem sm = new SimulationSystem();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            sm.set_simulation_case();
           sm.CalculateDailyCost();
           sm.CaculateSalesProfit();
            sm.CalculateScrapProfit();
            sm.CalculateLostProfit();
            sm.CalculateDailyNetProfit();
            sm.performance(); 
            for (int i = 0; i < sm.SimulationTable.Count; i++)
            {
                string DayNo = sm.SimulationTable[i].DayNo.ToString();

                string RandomNewsDayType = sm.SimulationTable[i].RandomNewsDayType.ToString();

                string NewsDayType = sm.SimulationTable[i].NewsDayType.ToString();

                string randomdem = sm.SimulationTable[i].RandomDemand.ToString();

                string demand = sm.SimulationTable[i].Demand.ToString();
                string RevenueProfit = sm.SimulationTable[i].SalesProfit.ToString();
                string LostProfit = sm.SimulationTable[i].LostProfit.ToString();
                if (sm.SimulationTable[i].LostProfit == 0)
                    LostProfit = "_";
                string ScrapProfit = sm.SimulationTable[i].ScrapProfit.ToString();
                if (sm.SimulationTable[i].ScrapProfit == 0)
                    ScrapProfit = "_";
                string DailyNetProfit = sm.SimulationTable[i].DailyNetProfit.ToString();

                if (dataGridView1.ColumnCount == 0)
                {
                    dataGridView1.Columns.Add("Day", "Day");
                    dataGridView1.Columns.Add("Random Digits for Type of NewsDayType", "Random Digits for Type of NewsDayType");
                    dataGridView1.Columns.Add("Type of Newsday", "Type of Newsday");
                    dataGridView1.Columns.Add("Random Digits for Demand", "Random Digits for Demand");
                    dataGridView1.Columns.Add("Demand", "Demand");
                    dataGridView1.Columns.Add("Revenue From Sales", "Revenue From Sales");
                    dataGridView1.Columns.Add("Lost profit  From Excess Profit", "Lost profit  From Excess Profit");
                    dataGridView1.Columns.Add("Salvage From Sales Of Scrap", "Salvage From Sales Of Scrap");
                    dataGridView1.Columns.Add("Daily Profit", "Daily Profit");
                }
               dataGridView1.Rows.Add(new string[] {DayNo,RandomNewsDayType,NewsDayType,randomdem,demand,RevenueProfit,LostProfit,ScrapProfit,DailyNetProfit });
              
            }
            
            string testingResult = TestingManager.Test(sm, Constants.FileNames.TestCase2);
               
                MessageBox.Show(testingResult);
        }
        private void MaskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(sm);
            f2.Show();
        }
    }
}
