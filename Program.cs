using FastReport;
using System.Data;

namespace FastReportPdf // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Current directory: " + Util.AppDirectory);
            CreateReport();
            Console.WriteLine("Done. Press any key to continue.");
            Console.ReadKey();
        }
                                        
        static void CreateReport()
        {
            var pDataSet = CreateDataSet();

            // optional. not needed for report rendering but needed for report designing
            var pFilename = System.IO.Path.Combine(Util.AppDirectory, "dataset.xml");
            pDataSet.WriteXml(pFilename, XmlWriteMode.WriteSchema); 

            pFilename = System.IO.Path.Combine(Util.AppDirectory, "report.frx");
            var pReport = new Report();
            pReport.Load(pFilename);            
            pReport.RegisterData(pDataSet);
            pReport.Prepare();
            
            pFilename = System.IO.Path.Combine(Util.AppDirectory, "rendered_report.pdf");
            FastReport.Export.Pdf.PDFExport pExport = new();
            pExport.Producer = "My Application"; // optional
            pExport.Title = "Products Report"; // optional
            pReport.Export(pExport, pFilename);
        }

        static DataSet CreateDataSet()
        {
            DataTable pTable;
            DataRow pRow;
            var pDataSet = new DataSet();

            pTable = new DataTable();
            pTable.TableName = "Products";
            pTable.Columns.Add("Name");
            pTable.Columns.Add("Price", typeof(decimal));
            pTable.Columns.Add("LastUpdated", typeof(DateTime));
            pDataSet.Tables.Add(pTable);

            pRow = pTable.NewRow();
            pRow["Name"] = "Ice Cream";
            pRow["Price"] = 10.95;
            pRow["LastUpdated"] = new DateTime(2000, 1, 2);
            pTable.Rows.Add(pRow);

            pRow = pTable.NewRow();
            pRow["Name"] = "Candy";
            pRow["Price"] = 3.5;
            pRow["LastUpdated"] = new DateTime(2000, 3, 4);
            pTable.Rows.Add(pRow);

            pRow = pTable.NewRow();
            pRow["Name"] = "Sandwich";
            pRow["Price"] = 29.99;
            pRow["LastUpdated"] = DateTime.Now;
            pTable.Rows.Add(pRow);

            return pDataSet;
        }
    }
}