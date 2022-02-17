using System;
using static System.Console;

namespace PatientRecordApplication
{
    /// <summary>
    /// User defined exception for patients that do not exist
    /// </summary>
    class PatientNotFoundException : Exception
    {
        public PatientNotFoundException()
        {
            WriteLine("Patient not found");
        }
    }
}
