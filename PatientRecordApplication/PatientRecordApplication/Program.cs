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
            string fileName = "PatientFile.txt";
            Patient patient = new Patient();
            PatientFile file = new PatientFile(fileName);
            file.FileOperations();
            file.DirectoryOperations();
            file.SequentialAccessWriteOperation();
            file.ReadSequentialAccessOperation();
            file.FindPatients();
        }
    }
}
