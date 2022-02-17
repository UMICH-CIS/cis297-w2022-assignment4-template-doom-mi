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
            }
        }

        //Directory Operations
        public void DirectoryOperations()
        {
            //Directory operations
            string directoryName;
            string[] listOfFiles;
            Write("Enter a folder >> ");
            directoryName = ReadLine();
            if (Directory.Exists(directoryName))
            {
                WriteLine("Directory exists, and it contains the following:");
                listOfFiles = Directory.GetFiles(directoryName);
                for (int x = 0; x < listOfFiles.Length; ++x)
                    WriteLine("   {0}", listOfFiles[x]);
            }
            else
            {
                WriteLine("Directory does not exist");
            }
        }

        //Using FileStream to create and write some text into it
        public void FileStreamOperations()
        {
            FileStream outFile = new
            FileStream("SomeText.txt", FileMode.Create,
            FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            Write("Enter some text >> ");
            string text = ReadLine();
            writer.WriteLine(text);
            // Error occurs if the next two statements are reversed
            writer.Close();
            outFile.Close();
        }

        //Writing data to a Sequential Access text file
        public void SequentialAccessWriteOperation()
        {
            const int END = 999;
            const string DELIM = ",";
            string FILENAME = FileName;
            Patient patient = new Patient();
            FileStream outFile = new FileStream(FILENAME,
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
             string FILENAME = FileName;
            Patient patient = new Patient();
            FileStream inFile = new FileStream(FILENAME,
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

        //Serializable Demonstration
        /// <summary>
        /// writes Person class objects to a file and later reads them 
        /// from the file into the program
        /// </summary>
        public void SerializableDemonstration()
        {
            const int END = 999;
            const string FILENAME = "Data.ser";
            Patient patient = new Patient();
            FileStream outFile = new FileStream(FILENAME,
               FileMode.Create, FileAccess.Write);
            BinaryFormatter bFormatter = new BinaryFormatter();
            Write("Enter patient ID number or " + END +
               " to quit >> ");
            patient.PatientID = Convert.ToInt32(ReadLine());
            while (patient.PatientID != END)
            {
                Write("Enter last name >> ");
                patient.Name = ReadLine();
                Write("Enter balance owed >> ");
                patient.Balance = Convert.ToDouble(ReadLine());
                bFormatter.Serialize(outFile, patient);
                Write("Enter patient ID number or " + END +
                   " to quit >> ");
                patient.PatientID = Convert.ToInt32(ReadLine());
            }
            outFile.Close();
            FileStream inFile = new FileStream(FILENAME,
               FileMode.Open, FileAccess.Read);
            WriteLine("\n{0,-5}{1,-12}{2,8}\n",
               "ID", "Name", "Balance");
            while (inFile.Position < inFile.Length)
            {
                patient = (Patient)bFormatter.Deserialize(inFile);
                WriteLine("{0,-5}{1,-12}{2,8}",
                   patient.PatientID, patient.Name, patient.Balance.ToString("C"));
            }
            inFile.Close();
        }

        //repeatedly searches a file to produce 
        //lists of employees who meet a minimum salary requirement
        public void FindPatients()
        {
            const char DELIM = ',';
            const int END = 999;
            string FILENAME = FileName;
            Patient patient = new Patient();
            FileStream inFile = new FileStream(FILENAME,
               FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string recordIn;
            string[] fields;
            double minSalary;
            Write("Enter minimum balance owed to find or " +
               END + " to quit >> ");
            minSalary = Convert.ToDouble(Console.ReadLine());
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
                minSalary = Convert.ToDouble(Console.ReadLine());
            }
            reader.Close();  // Error occurs if
            inFile.Close(); //these two statements are reversed
        }
    }
}
