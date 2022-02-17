using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PatientRecordApplication
{
    class PatientFile
    {
        private string FileName;

        public PatientFile(string fileName)
        {
            this.FileName = fileName;
        }

        //File operations
        public void FileOperations()
        {
            if (File.Exists(FileName))
            {
                WriteLine("File exists");
                WriteLine("File was created " +
                File.GetCreationTime(FileName));
                WriteLine("File was last written to " +
                File.GetLastWriteTime(FileName));
            }
            else
            {
                WriteLine("File does not exist");
                FileStream outFile = new
                FileStream(FileName, FileMode.Create,
                FileAccess.Write);
                StreamWriter writer = new StreamWriter(outFile);
                writer.WriteLine("PATIENT RECORDS:");
                writer.Close();
                outFile.Close();
            }
        }

        //Writing data to a Sequential Access text file
        public void SequentialAddPatientOperation()
        {
            const int END = 999;
            const string DELIM = ",";
            Patient patient = new Patient();
            FileStream outFile = new FileStream(FileName,
               FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            Write("Enter patient ID number or " + END +
               " to quit >> ");
            patient.PatientID = Convert.ToInt32(ReadLine());
            while (patient.PatientID != END)
            {
                Write("Enter last name >> ");
                patient.Name = ReadLine();
                Write("Enter balance owed >> ");
                patient.Balance = Convert.ToDouble(ReadLine());
                writer.WriteLine(patient.PatientID + DELIM + patient.Name +
                   DELIM + patient.Balance);
                Write("Enter next patient ID number or " +
                   END + " to quit >> ");
                patient.PatientID = Convert.ToInt32(ReadLine());
            }
            writer.Close();
            outFile.Close();
        }

        //Read data from a Sequential Access File
        public void ReadSequentialAccessOperation()
        {
            const char DELIM = ',';
            Patient patient = new Patient();
            FileStream inFile = new FileStream(FileName,
               FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string recordIn;
            string[] fields;
            WriteLine("\n{0,-5}{1,-12}{2,8}\n",
               "ID", "Name", "Balance");
            recordIn = reader.ReadLine();
            while (recordIn != null)
            {
                fields = recordIn.Split(DELIM);
                patient.PatientID = Convert.ToInt32(fields[0]);
                patient.Name = fields[1];
                patient.Balance = Convert.ToDouble(fields[2]);
                WriteLine("{0,-5}{1,-12}{2,8}",
                   patient.PatientID, patient.Name, patient.Balance.ToString("C"));
                recordIn = reader.ReadLine();
            }
            reader.Close();
            inFile.Close();
        }

        //repeatedly searches a file to produce 
        //lists of employees who meet a minimum salary requirement
        public void FindPatientMinBalance()
        {
            const char DELIM = ',';
            const int END = 999;
            Patient patient = new Patient();
            FileStream inFile = new FileStream(FileName,
               FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string recordIn;
            string[] fields;
            double minSalary;
            Write("Enter minimum balance owed to find or " +
               END + " to quit >> ");
            minSalary = Convert.ToDouble(ReadLine());
            while (minSalary != END)
            {
                WriteLine("\n{0,-5}{1,-12}{2,8}\n",
                   "Num", "Name", "Balance");
                inFile.Seek(0, SeekOrigin.Begin);
                recordIn = reader.ReadLine();
                while (recordIn != null)
                {
                    fields = recordIn.Split(DELIM);
                    patient.PatientID = Convert.ToInt32(fields[0]);
                    patient.Name = fields[1];
                    patient.Balance = Convert.ToDouble(fields[2]);
                    if (patient.Balance >= minSalary)
                        WriteLine("{0,-5}{1,-12}{2,8}", patient.PatientID,
                           patient.Name, patient.Balance.ToString("C"));
                    recordIn = reader.ReadLine();
                }
                Write("\nEnter minimum balance owed to find or " +
                   END + " to quit >> ");
                minSalary = Convert.ToDouble(ReadLine());
            }
            reader.Close();  // Error occurs if
            inFile.Close(); //these two statements are reversed
        }

        public void FindPatientID()
        {
            Clear();
            Write("Enter Patient ID: ");
            string id = ReadLine();
            FileStream inFile = new FileStream(FileName,
               FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string recordIn;
            string[] fields;

            recordIn = reader.ReadLine();

            while (recordIn != null)
            {
                fields = recordIn.Split(' ');

                if (fields[0] == id)
                {
                    WriteLine(recordIn);
                }

                recordIn = reader.ReadLine();
            }

            reader.Close();
            inFile.Close();
        }
    }
}
