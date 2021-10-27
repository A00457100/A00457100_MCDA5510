using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A00457100_Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            string file = "/Users/muralitulluri/Documents/Output.txt";
            string Skipped = "/Users/muralitulluri/Documents/Skipped_rows.txt";
            string Log = "/Users/muralitulluri/Documents/Log_file.txt";
            string header = "First Name,Last Name,Street Number,Street,City,Province,Postal Code,Country,Phone Number,email Address,Date";
            string filePath1 = @"/Users/muralitulluri/Documents/Output.txt";
            string skipped_rows = @"/Users/muralitulluri/Documents/Skipped_rows.txt";
            using (StreamWriter outputFile = new StreamWriter(skipped_rows, true))
            {
                outputFile.WriteLine(header);
            }

            using (StreamWriter outputFile = new StreamWriter(filePath1, true))
            {
                outputFile.WriteLine(header);
            }
            using (StreamWriter outputFile = new StreamWriter(Log, true))
            {
                outputFile.WriteLine("This program merges all the CSV files by rejecting any invalid records");
                DirWalker traverse = new DirWalker();
                traverse.walk(@"/Users/muralitulluri/Documents/Sample Data");
                watch.Stop();
                outputFile.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                outputFile.WriteLine("Number of total correct lines excluding header are : " + File.ReadLines(file).Count());
                outputFile.WriteLine("Number of total skipped lines are : " + File.ReadLines(Skipped).Count());

            }
        }
    }


    public class DirWalker
    {

        public void walk(String path)
        {

            string[] list = Directory.GetDirectories(path);
            SimpleCSVParser simpleCSV = new SimpleCSVParser();
            //int directory_count = 0;
            //int file_count = 0;

            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                       //Console.WriteLine("Dir:" + dirpath);
                }
            }
           // directory_count = directory_count + list.count();
            string[] fileList = Directory.GetFiles(path);
            

            // file_count = file_count + fileList.count();
            foreach (string filepath in fileList)
            {
               // n = n + 1;
               // if (n%100 == 0)
               // {
               //     Console.WriteLine("Processing the " + n + " st/th file");
               // }
                if (filepath.Contains(".csv"))
                {
                    char char1 = '\\';
                    string[] path_split = filepath.Split(char1);
                    string year1 = path_split[1];
                    string month1="";
                    string day = "";
                    string date_custom = "";
                    if (path_split[2].Length == 1)
                    {
                        month1 = "0" + path_split[2];
                        //Console.WriteLine("File:" + month+"another month"+month1);
                        // Console.WriteLine("File:" + date_custom);
                    }
                    else month1 = path_split[2];
                    if (path_split[3].Length == 1)
                    {
                        day = "0" + path_split[3];
                        //Console.WriteLine("File:" + month+"another month"+month1);
                        // Console.WriteLine("File:" + date_custom);
                    }
                    else day = path_split[3];
                    date_custom = year1 + "-" + month1 + "-" + day;
                    //Console.WriteLine("File:" + date_custom);
                    // Console.WriteLine("Call the file parser");
                    simpleCSV.parse(@filepath, date_custom);
                }

            }
            //Console.WriteLine("All " + n + " files are processed and the output files are generated below are the stats");

        }



    }

  
    public class SimpleCSVParser
    {

        public void parse(String fileName,String Date_Custom)
        {
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    TextFieldParser parser1 = new TextFieldParser(fileName);
                    string filePath1 = @"/Users/muralitulluri/Documents/Output.txt";
                    string skipped_rows= @"/Users/muralitulluri/Documents/Skipped_rows.txt";
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int i = 1;
                    int j;
                    int k;
                    List<string> rows = new List<string>();
                    List<string> rows_skipped = new List<string>();
                    while (!parser.EndOfData)
                    {

                            //Process row
                            string[] fields = parser.ReadFields();
                            string line = parser1.ReadLine()+ ","+Date_Custom;
                            
                           
                            j = 0;
                            k = 0;

                            if (fields.Count()!=10)
                            {
                            //Console.WriteLine("Wrong number of fields in row number " + i);
                            rows_skipped.Add("There are only "+ fields.Count()+" fields in row number " + i+ " of the file "+ fileName+"so skipping the row and the row data is "+line);
                            k = 1;
                            }
                        foreach (string field in fields)
                            {
                                j = j+1;
                            if (fields.Contains("First Name"))
                            {
                                //Console.WriteLine("Wrong number of fields in row number " + i);
                                k = 1;
                                break;
                            }
                            if (field == "" && k == 0)
                                {
                                
                                    rows_skipped.Add("Field " + j + " is blank in row number " + i + " of the file " + fileName + " so skipping the row and the row data is " + line);
                                    k = 1;
                                break;
                                }
                            }
                            if (k == 0)
                            {
                            /*  using (StreamWriter outputFile = new StreamWriter(filePath1, true))
                              {
                                  outputFile.WriteLine(line);
                              }*/

                            rows.Add(line);
                            }
                            i = i+1;
                        }
                    using (StreamWriter outputFile = new StreamWriter(filePath1, true))
                    {
                        foreach(string data in rows)
                            outputFile.WriteLine(data);
                    }
                    using (StreamWriter outputFile = new StreamWriter(skipped_rows, true))
                    {
                        foreach (string data1 in rows_skipped)
                            outputFile.WriteLine(data1);
                    }
                }

            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.StackTrace);
            }

        }


    }

}
