using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalInfoParser
{
    class NoPets
    {
        public NoPets()
        {
            countNoDogs = 0;
            countNoCats = 0;
            countNoPets = 0;
        }
        public int countNoDogs { get; set; } = 0;
        public int countNoCats { get; set; } = 0;
        public int countNoPets { get; set; } = 0;

        internal void AddCountInfo(DataRow dataRow)
        {
            string contentAdditional = dataRow[1].ToString();
            string contentDescription = dataRow[2].ToString();
            bool catsok = contentAdditional.Contains(Resources.catsok) ? true : false;
            bool dogsok = contentAdditional.Contains(Resources.dogsok) ? true : false;
            bool noDogs = contentDescription.Contains("no dog") || contentDescription.Contains("no any dog");
            bool noCats = contentDescription.Contains("no cat") || contentDescription.Contains("no any cat");
            bool noPets = contentDescription.Contains("no pet") || contentDescription.Contains("no any pet");
            if (catsok && noCats)
            {
                Console.WriteLine("Cats collapse ,{0},{1},{2}", dataRow[0], contentAdditional, contentDescription);
                //Console.ReadKey();
            }
            if (dogsok && noDogs)
            {
                Console.WriteLine("Dogs collapse ,{0},{1},{2}", dataRow[0], contentAdditional, contentDescription);
                //Console.ReadKey();
            }
            if (noCats)
            {
                this.countNoCats++;
            }
            if (noDogs)
            {
                this.countNoDogs++;
            }
            if (noPets)
            {
                this.countNoPets++;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            long CraigslistCount = DataProviders.DataProvider.Instance.GetCount(); //смотрим, сколько записей в таблице
            int step = 100;
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Convert.ToInt32(Resources.NumberOfThreads);
            NoPets one = new NoPets();
            for (int i = 1; i <= CraigslistCount; i += step)
            {
                if ((i - 1) % 50000 == 0)
                {
                    Console.WriteLine("Now {0} cells", i - 1);
                }
                DataTable table;
                if (i + step > CraigslistCount)
                {
                    table = DataProviders.DataProvider.Instance.GetDataset(i, CraigslistCount).Tables[0];
                }
                else
                {
                    table = DataProviders.DataProvider.Instance.GetDataset(i, i + step - 1).Tables[0];
                }


                //Parallel.For(0, table.Rows.Count,options, (cur) =>

                
                for (int cur = 0; cur < table.Rows.Count; cur++)
                {
                    //AdditionalDetails elem = new AdditionalDetails(table.Rows[cur]);
                    //elem.InsertInDb();
                    one.AddCountInfo(table.Rows[cur]);
                }//);
            }
            //DataSet dad = DataProviders.DataProvider.Instance.GetDataset(100, 1110);
            //Console.WriteLine(dad);
            //ConsoleView(dad);
            Console.WriteLine("No cats: {0}, no dogs: {1}, no pets:{2}", one.countNoCats, one.countNoDogs, one.countNoPets);
            File.WriteAllText("info.txt", String.Format("No cats: {0}, no dogs: {1}, no pets:{2}", one.countNoCats, one.countNoDogs, one.countNoPets));
            Console.ReadKey();
        }

        private static void ConsoleView(DataTable table)
        {
            Console.WriteLine("--- ConsoleTable(" + table.TableName + ") ---");
            int zeilen = table.Rows.Count;
            int spalten = table.Columns.Count;

            // Header
            for (int i = 0; i < table.Columns.Count; i++)
            {
                string s = table.Columns[i].ToString();
                Console.Write(String.Format("{0,-40} | ", s));
            }
            Console.WriteLine();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                Console.Write("-----------------------------------------|-");
            }
            Console.WriteLine();

            Console.WriteLine();
            // Data
            for (int i = 0; i < zeilen; i++)
            {
                DataRow row = table.Rows[i];
                //Console.WriteLine("{0} {1} ", row[0], row[1]);
                for (int j = 0; j < spalten; j++)
                {
                    string s = row[j].ToString();
                    s = s.Replace("\n", " ");
                    if (s.Length > 40) s = s.Substring(0, 37) + "...";
                    Console.Write(String.Format("{0,-40} | ", s));
                }
                Console.WriteLine();
            }
            for (int i = 0; i < table.Columns.Count; i++)
            {
                Console.Write("-----------------------------------------|-");
            }
            Console.WriteLine();
        }
    }
}
