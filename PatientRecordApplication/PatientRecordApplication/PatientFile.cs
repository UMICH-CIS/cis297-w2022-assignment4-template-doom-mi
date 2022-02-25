using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// This program implements exception handling and data entry from a file in the form of a patient system for doctors
/// </summary>
/// <Student>Dominic Verardi</Student>
/// <Class>CIS297</Class>
/// <Semester>Winter 2022</Semester>

namespace PatientRecordApplication
{
    /// <summary>
    /// File class that creats and edits file
    /// </summary>
    class PatientFile
    {
        private string FileName;

        public PatientFile(string fileName)
        {
            this.FileName = fileName;
        }

        //File operations that checks if it needs to create file
        public void FileOperations()
        {
            if (File.Exists(FileName))
            {
                WriteLine("File exists");
                WriteLine("File was created " +
                File.GetCreationTime(FileName));
                WriteLine("File was last written to " +
                File.GetLastWriteTime(FileName));
                WriteLine("\nPress any key to continue");
                ReadKey();
            }
            else
            {
                WriteLine("File does not exist");
                FileStream outFile = new
                FileStream(FileName, FileMode.Create,
                FileAccess.Write);
                StreamWriter writer = new StreamWriter(outFile);
                writer.Close();
                outFile.Close();
                WriteLine("File created.");
                WriteLine("\nPress any key to continue");
                ReadKey();
            }
        }

        //Writing data to a Sequential Access text file for patient information
        public void SequentialAddPatientOperation()
        {
            Clear();
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

                try
                {
                    patient.Balance = Convert.ToDouble(ReadLine());
                }

                catch (FormatException)
                {
                    WriteLine("Invalid input for balance.");
                }

                writer.WriteLine(patient.PatientID + DELIM + patient.Name +
                   DELIM + patient.Balance);
                Write("Enter next patient ID number or " +
                   END + " to quit >> ");
                patient.PatientID = Convert.ToInt32(ReadLine());
            }
            writer.Close();
            outFile.Close();
        }

        //Read patient data from a Sequential Access File
        public void ReadSequentialAccessOperation()
        {
            Clear();
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
        //lists of patients who meet a minimum balance owed
        public void FindPatientMinBalance()
        {
            Clear();
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
                try
                {
                    inFile.Seek(0, SeekOrigin.Begin);
                }
                catch (FileNotFoundException)
                {
                    WriteLine("Error: File Not Found");
                }
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

        //finds patient in text file by ID
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

                foreach (var record in fields)
                {
                    if (record.Contains(id))
                    {
                        WriteLine(record);
                    }
                }

                recordIn = reader.ReadLine();
            }

            reader.Close();
            inFile.Close();
        }
    }
}
