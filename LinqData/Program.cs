using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
namespace LinqData
{
    class Program
    {     
       public DataTable linqTable = new DataTable();   // our TableConteiner

        void AddNewRow(int _id, string _name)
        {
            Random random = new Random();
            DataRow dt = linqTable.NewRow();
            Thread.Sleep(20);
            dt["id"] = _id; dt["name"] = _id +_name; dt["salary"] = (float)random.Next(10000) / 100;
            linqTable.Rows.Add(dt);
        }

         void MyTable()
        {
            linqTable.Columns.Add(new DataColumn("id",typeof(int)) {AutoIncrement = true, AutoIncrementSeed = 1,Unique = true });
            linqTable.Columns.Add(new DataColumn("name",typeof(string)));
            linqTable.Columns.Add(new DataColumn("salary",typeof(float)));
        }
        static void Main(string[] args)
        {
            // Initialized the Table 
            Program program = new Program();
            program.MyTable();
            //Add 10 rows in Table
         #region Staff
            program.AddNewRow(1, " Max"); 
            program.AddNewRow(2, " Oleg"); 
            program.AddNewRow(3, " Nastja");
            program.AddNewRow(4, " Vanja");
            program.AddNewRow(5, " Vasja");
            program.AddNewRow(6, " Sasha");
            program.AddNewRow(7, " Sasha");
            program.AddNewRow(8, " Max");
            program.AddNewRow(9, " Olja");
            program.AddNewRow(10, " Volodja");
#endregion
            // LINQ query:
            var taskOne = program.linqTable.AsEnumerable().Where(x => (int)x["id"] > 3).ToList();
            var taskTwo = program.linqTable.AsEnumerable().Where(x => (int)x["id"] > 4).Select(x => x["salary"]).ToArray();
            var taskThree = program.linqTable.AsEnumerable().OrderBy(x => (float)x["salary"]).ToList();
            var taskFour = program.linqTable.AsEnumerable().Select(x => new {IdField = x["id"] ,NameField = x["name"], PriceField = x["salary"]  }).ToList();
            var taskFive = program.linqTable.AsEnumerable().Where(x => (int)x["id"] > 2 && (int)x["id"] < 8).OrderByDescending(x => (float)x["salary"]).ToArray();

            // Show in Console
            DataRow _tempRow;
            for (int index = 0; index < program.linqTable.Rows.Count; index++)
            {
                _tempRow = program.linqTable.Rows[index];

                foreach (var row in _tempRow.ItemArray)
                {
                    Console.Write(row +" ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
