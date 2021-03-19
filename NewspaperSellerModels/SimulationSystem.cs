using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace NewspaperSellerModels
{
    public class SimulationSystem
    {
        //for testing
        public static string txt ="";
   
        ///////////// INPUTS /////////////
        public int NumOfNewspapers { get; set; }
        public int NumOfRecords { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ScrapPrice { get; set; }
        public decimal UnitProfit { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }
        public List<DemandDistribution> DemandDistributions { get; set; }
        // lists of reading from file
        public List<string> file { get; set; }                       // list of reading txt file
        public List<string> input_inform { get; set; }               // list of reading NumOfNewspapers NumOfRecords PurchasePrice ScrapPrice ScrapPrice  SellingPrice from the file
        public List<string> DayTypeDistributions_table { get; set; }       // list of DayTypeDistributions from the file
        public List<string> DemandDistributions_table { get; set; }        // list of DemandDistributions from the file

        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }




        public SimulationSystem()
        {

            // this will be shown;
            SimulationTable = new List<SimulationCase>();

            //this for files 


            this.file = new List<string>();
            this.input_inform = new List<string>();
            this.DayTypeDistributions = new List<DayTypeDistribution>();
            this.DemandDistributions = new List<DemandDistribution>();
            this.DayTypeDistributions_table = new List<string>();
            this.DemandDistributions_table = new List<string>();
            read_from_file();
            set_day_distripution_table(DayTypeDistributions, DayTypeDistributions_table);
            set_demand(DemandDistributions, DemandDistributions_table);



        }

        public void read_from_file()
        {
            FileStream fs = new FileStream(@"F:\lectcures and labs fourth year\1st term\Modeling and Simulation\assignments\task2\SimulationTask2-main\NewspaperSellerSimulation\TestCases\TestCase2.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                file.Add(sr.ReadLine());
            }
            sr.Close();
            fs.Close();
            for (int i = 1; i < 15; i = i + 3)
            {
                input_inform.Add(file[i]);

            }


            NumOfNewspapers = int.Parse(input_inform[0]);   //set NumOfNewspapers

            NumOfRecords = int.Parse(input_inform[1]); //set StoppingNumber 

            PurchasePrice = decimal.Parse(input_inform[2]); //

            ScrapPrice = decimal.Parse(input_inform[3]);  //''''''''''''''''''''''''

            SellingPrice = decimal.Parse(input_inform[4]);

            //PerformanceMeasures.SetTotalCost(NumOfNewspapers, ScrapPrice);

            String arr_c = "";
            bool bol = false;
            for (int i = 16; i < file.Count; i++)
            {

                if (file[i] == "")
                {
                    break;
                }

                for (int z = 0; z < file[i].Length; z++)
                {

                    if (file[i][z] == ',' || file[i][z] == ' ')
                    {


                        bol = false;
                    }
                    else
                    {
                        arr_c += (file[i][z]);
                        bol = true;

                    }

                    if (bol == false && file[i][z] != ',') 
                    {



                        DayTypeDistributions_table.Add(arr_c);
                        arr_c = "";
                    }

                    if (z == file[i].Length - 1)
                    {
                        DayTypeDistributions_table.Add(arr_c);

                    }


                }
            }
            int j = 19;
            while (j < file.Count)
            {
            
                DemandDistributions_table.Add(file[j]);

                j++;
            }
        }
        //reading from file to list
        public void set_day_distripution_table(List<DayTypeDistribution> daydistripution, List<string> table)
        {
            int table_size=table.Count;
            for (int z = 0; z < table_size; z++)
            {
                daydistripution.Add(new DayTypeDistribution());
                if (z == 0)
                {

                    daydistripution[z].DayType = Enums.DayType.Good;
                }
                else if (z == 1)
                {
                    daydistripution[z].DayType = Enums.DayType.Fair;
                }
                else if (z == 2)
                {
                    daydistripution[z].DayType = Enums.DayType.Poor;

                }
                daydistripution[z].Probability = decimal.Parse(table[z]);

            }
            calculate_CummProbability(daydistripution);
            calculate_range(daydistripution);
        }
        public void set_demand(List<DemandDistribution> demand, List<string> table)
        {    
            int table_size=table.Count;
            for (int z = 0; z < table_size; z++)
            {
                demand.Add(new DemandDistribution());
                string[] sperators = { ", " };
                int count1 = 4;
                string x1 = table[z];
                string[] x = x1.Split(sperators, count1, StringSplitOptions.RemoveEmptyEntries);
                //txt = x[1];
               
                demand[z].Demand = int.Parse(x[0]);
                for (int w = 1; w < x.Length; w++)
                {

                    demand[z].DayTypeDistributions.Add(new DayTypeDistribution());
                    if (w==1)
                    {
                        demand[z].DayTypeDistributions[w-1].DayType = Enums.DayType.Good;
                       
                    }

                    else if (w == 2)
                    {
                        demand[z].DayTypeDistributions[w-1].DayType = Enums.DayType.Fair;

                    }

                    else  if (w ==3)
                    {
                        demand[z].DayTypeDistributions[w-1].DayType = Enums.DayType.Poor;

                    }

                    demand[z].DayTypeDistributions[w-1].Probability = decimal.Parse(x[w]);
                  
                  

                }

            }

            calculate_CummProbability_demand(demand);
            calculate_range_demand(demand);
            
        }
        public void calculate_CummProbability(List<DayTypeDistribution> daydistripution)
        {
            decimal sum = 0.0m;
            for (int i = 0; i < daydistripution.Count; i++)
            {
                
                /*for (int j = 0; j <= i; j++)
                {
                    sum += daydistripution[j].Probability;
                }*/
                sum += daydistripution[i].Probability;
                daydistripution[i].CummProbability = sum;
               
            }
        }
        public void calculate_CummProbability_demand(List<DemandDistribution> demand)
        {

            decimal sum_good = 0.0m;
            decimal sum_fair = 0.0m;
            decimal sum_poor = 0.0m;
            for (int i = 0; i < demand.Count; i++)
            {      
                    for (int z = 0; z < demand[i].DayTypeDistributions.Count; z++)
                    {
                        if(z==0){
                                sum_good+=demand[i].DayTypeDistributions[z].Probability;
                                demand[i].DayTypeDistributions[z].CummProbability = sum_good;
                        }
                        if(z==1){
                                sum_fair+=demand[i].DayTypeDistributions[z].Probability;
                                demand[i].DayTypeDistributions[z].CummProbability = sum_fair;
                        }
                        if(z==2){
                                sum_poor+=demand[i].DayTypeDistributions[z].Probability;
                                demand[i].DayTypeDistributions[z].CummProbability = sum_poor;
                        }
                }

            }
        }
        public void calculate_range(List<DayTypeDistribution> daydistripution)
        {
            for (int i = 0; i < daydistripution.Count; i++)
            {
                if (daydistripution[i].Probability == 0)
                {
                    daydistripution[i].MinRange = daydistripution[i - 1].MinRange;
                    int range = daydistripution[i - 1].MaxRange;
                    daydistripution[i].MaxRange = range;

                }
                else
                {

                    if (i == 0)
                    {
                        daydistripution[i].MinRange = 1;
                        Int16 range = Convert.ToInt16((daydistripution[i].CummProbability * 100));
                        daydistripution[i].MaxRange = range;

                    }
                    else
                    {
                        daydistripution[i].MinRange = daydistripution[i - 1].MaxRange + 1;

                        Int16 range = Convert.ToInt16((daydistripution[i].CummProbability * 100));
                        daydistripution[i].MaxRange = range;
                    }
                }

            }
        }
        public void calculate_range_demand(List<DemandDistribution> demand)
        {
            for (int i = 0; i < demand.Count; i++)
            {
                for (int j = 0; j < demand[i].DayTypeDistributions.Count; j++)
                {

                    if (demand[i].DayTypeDistributions[j].Probability == 0)
                    {
                        demand[i].DayTypeDistributions[j].MinRange = demand[i - 1].DayTypeDistributions[j].MinRange;
                        int range = demand[i - 1].DayTypeDistributions[j].MaxRange;
                        demand[i].DayTypeDistributions[j].MaxRange = range;

                    }
                    else
                    {


                        if (i == 0)
                        {
                            demand[i].DayTypeDistributions[j].MinRange = 1;
                            Int16 range = Convert.ToInt16((demand[i].DayTypeDistributions[j].CummProbability * 100));
                            demand[i].DayTypeDistributions[j].MaxRange = range;

                        }
                        else
                        {
                            demand[i].DayTypeDistributions[j].MinRange = demand[i - 1].DayTypeDistributions[j].MaxRange + 1;

                            Int16 range = Convert.ToInt16((demand[i].DayTypeDistributions[j].CummProbability * 100));
                            demand[i].DayTypeDistributions[j].MaxRange = range;
                        }
                    }

                    if (demand[i].DayTypeDistributions[j].CummProbability == 1 && demand[i - 1].DayTypeDistributions[j].CummProbability == 1)
                    {
                        demand[i].DayTypeDistributions[j].MinRange = 0;

                        demand[i].DayTypeDistributions[j].MaxRange = 0;
                    }

                }

            }

        }
        public void set_simulation_case()
        {
            Random rd = new Random();
            bool setdaytype = false;

            for (int i = 0; i < NumOfRecords; i++)
            {
                SimulationTable.Add( new SimulationCase());
                SimulationTable[i].DayNo = i + 1;
                SimulationTable[i].RandomNewsDayType = rd.Next(1, 100);
                foreach (DayTypeDistribution item in DayTypeDistributions)
                {
                    if (SimulationTable[i].RandomNewsDayType >= item.MinRange && SimulationTable[i].RandomNewsDayType <= item.MaxRange)
                    {

                        SimulationTable[i].NewsDayType = item.DayType;
                        setdaytype = true;
                        break;
                    }
                }

                    if (setdaytype == true)
                    {
                        SimulationTable[i].RandomDemand = rd.Next(1, 100);

                        foreach (DemandDistribution item2 in DemandDistributions)
                        {
                            foreach(DayTypeDistribution item3 in item2.DayTypeDistributions)
                                if (SimulationTable[i].NewsDayType == item3.DayType)
                                {
                                    if (SimulationTable[i].RandomDemand >= item3.MinRange && SimulationTable[i].RandomDemand <= item3.MaxRange)
                                    {
                                        SimulationTable[i].Demand = item2.Demand;

                                    }
                            }
                        }
                       
                    }
            }
        }
        // DailyCost 
        public void CaculateSalesProfit()
        {
            //decimal sumSales = 0;
            for (int i=0;i<SimulationTable.Count;i++)
            {
                if(SimulationTable[i].Demand > NumOfNewspapers)
                {
                    SimulationTable[i].SalesProfit = SellingPrice *NumOfNewspapers;
                  
                }
                else
                {
                    SimulationTable[i].SalesProfit = SellingPrice * SimulationTable[i].Demand;
                   
                }
                //sumSales += SimulationTable[i].SalesProfit;
               
            }
          //PerformanceMeasures.SetTotalSalesProfit(sumSales);
        }
        //SalesProfit
        
        //LostProfit 
       public void  CalculateLostProfit()
        {
           
            for (int i=0;i<SimulationTable.Count;i++)
            {
                int Lost =NumOfNewspapers - SimulationTable[i].Demand;
                if(Lost<0)
                {
                    SimulationTable[i].LostProfit = Math.Abs(Lost)*(SellingPrice - PurchasePrice);
                   // sumProfit += SimulationTable[i].ScrapProfit;
                    //numlost += 1;
                }
                else
                {
                    SimulationTable[i].LostProfit =0;
               }
               
            }
           
        }
        //ScrapProfit
       public void CalculateScrapProfit()
        {
            for (int i = 0; i < SimulationTable.Count; i++)
            {
                int Lost = NumOfNewspapers - SimulationTable[i].Demand;
                if (Lost>0)
                {
                    SimulationTable[i].ScrapProfit = Lost *ScrapPrice;
                  
                }
                else
                {
                   SimulationTable[i].ScrapProfit = 0 ;
                }
               
            }
          
        }
        //DailyCost
       public void CalculateDailyCost()
        {
            for (int i = 0; i < SimulationTable.Count; i++)
            {

                SimulationTable[i].DailyCost =NumOfNewspapers*PurchasePrice;
            }
        }
        //DailyNetProfit
       public void CalculateDailyNetProfit()
        {
            //decimal sumprofit = 0;
            for (int i = 0; i < SimulationTable.Count; i++)
            {
                decimal Profit = 0m;
                decimal SalesProfit =SimulationTable[i].SalesProfit;
                Profit = (((SalesProfit - SimulationTable[i].DailyCost) - SimulationTable[i].LostProfit) + SimulationTable[i].ScrapProfit);
                SimulationTable[i].DailyNetProfit = Profit;
            }
           
        }
        public int numlost = 0;

        public void performance()
        {
            this.PerformanceMeasures = new PerformanceMeasures();
            decimal sales = 0;
            decimal sum = 0;
           
            decimal sumProfit2 = 0;
            int numScrap = 0;
            decimal sumprofit = 0;
            for (int i = 0; i < SimulationTable.Count; i++)
            {
                sales += SimulationTable[i].SalesProfit;
                sum += SimulationTable[i].DailyNetProfit;
                int Lost = NumOfNewspapers - SimulationTable[i].Demand;
                if (Lost < 0)
                {
                    sumprofit += SimulationTable[i].LostProfit;
                    numlost += 1;
                }
                else if (Lost > 0)
                {
                    sumProfit2 += SimulationTable[i].ScrapProfit;
                    numScrap += 1;
                }

            }
            PerformanceMeasures.TotalSalesProfit = sales;
            PerformanceMeasures.TotalLostProfit = sumprofit;
            PerformanceMeasures.DaysWithMoreDemand = numlost;
            PerformanceMeasures.TotalScrapProfit = sumProfit2;
            PerformanceMeasures.DaysWithUnsoldPapers = numScrap;
            PerformanceMeasures.TotalNetProfit = sum;
            PerformanceMeasures.TotalCost = NumOfNewspapers * PurchasePrice * NumOfRecords;

        }


    }
}



