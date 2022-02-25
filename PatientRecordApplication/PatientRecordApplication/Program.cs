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
        /// <summary>
        /// This program creates a patient file and allows user to add more information
        /// </summary>
        /// <Student>Dominic Verardi</Student>
        /// <Class>CIS297</Class>
        /// <Semester>Winter 2022</Semester>
        static void Main(string[] args)
        {
            // asks for patient file name and stores the name
            Write("Enter a name for your patient file minus the extension type: ");
            string fileName = ReadLine() + ".txt";
            PatientFile file = new PatientFile(fileName);

            // checks if file by name is already created. if not, creates. if so, tells user and uses that file
            file.FileOperations();

            // switch case to ask for users selection
            while (true)
            {
                switch (DisplayMenu())
                {
                    // calls method to add new patients to the patient file
                    case 1:
                        file.SequentialAddPatientOperation();
                        break;

                    // calls and displays the patient file to the user
                    case 2:
                        file.ReadSequentialAccessOperation();
                        break;
                    
                    // finds the patient by their id within the file and displays the patient record
                    case 3:
                        file.FindPatientID();
                        break;

                    // finds the patients by a minimum balance and displays their record
                    case 4:
                        file.FindPatientMinBalance();
                        break;
                    
                   // exits the application
                    case 5:
                        return;

                    default:
                        break;
                }

                WriteLine("Press any key to continue...");
                ReadKey();
            }


            int DisplayMenu()
            {
                Clear();
                WriteLine("1 Add New Patients.");
                WriteLine("2 Display File Information.");
                WriteLine("3 Search Patient by ID.");
                WriteLine("4 Find Patients with the Minimun Balance Due.");
                WriteLine("5 Exit");
                Write("\nPlease enter a number to select an option: ");
                return int.Parse(ReadLine());
            }
        }
    }
}
