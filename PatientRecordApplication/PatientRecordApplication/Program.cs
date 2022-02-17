using System;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PatientRecordApplication
{
    /// <summary>
    /// Main Patient Record Program that calls all methods and displays menu
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a name for your patient file: ");
            string fileName = ReadLine() + ".txt";
            Patient patient = new Patient();
            PatientFile file = new PatientFile(fileName);


            file.FileOperations();

            while (true)
            {
                switch (DisplayMenu())
                {
                    case 1:
                        file.SequentialAddPatientOperation();
                        break;

                    case 2:
                        file.ReadSequentialAccessOperation();
                        break;

                    case 3:
                        file.FindPatientID();
                        break;

                    case 4:
                        file.FindPatientMinBalance();
                        break;

                    case 5:
                        return;

                    default:
                        break;
                }

                WriteLine("Press any key to continue.");
                ReadLine();
            }


            int DisplayMenu()
            {
                int input = 0;
                Clear();
                WriteLine("1 Add New Patients.");
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
                    WriteLine("Invalid input entered by user.");
                }

                return input;
            }
        }
    }
}
