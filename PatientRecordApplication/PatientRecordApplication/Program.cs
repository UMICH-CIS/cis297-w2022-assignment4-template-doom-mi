using System;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PatientRecordApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a name for your patient file: ");
            string fileName = ReadLine() + ".txt";
            Patient patient = new Patient();
            PatientFile file = new PatientFile(fileName);


            file.FileOperations();
            file.DirectoryOperations();
            file.SequentialAccessWriteOperation();
            file.ReadSequentialAccessOperation();
            file.FindPatients();

            while (true)
            {
                switch (DisplayMenu())
                {
                    case 1:
                        
                        break;

                    case 2:
                        
                        break;

                    case 3:
                        
                        break;

                    case 4:
                        
                        break;

                    case 5:
                        return;

                    default:
                        break;
                }

                WriteLine("Press Enter to Continue.");
                ReadLine();
            }


            int DisplayMenu()
            {
                int input = 0;
                Clear();
                WriteLine("1 Add New Patient.");
                WriteLine("2 Display File Information.");
                WriteLine("3 Search Patient by ID.");
                WriteLine("4 Find Patients with the Minimun Balance Due.");
                WriteLine("5 Exit");
                Write("\nPlease enter a number to select an option: ");

                try
                {
                    input = int.Parse(ReadLine());
                }
                catch (FormatException)
                {
                    WriteLine("Invalid value entered for user input.");
                }

                return input;
            }
        }
    }
}
