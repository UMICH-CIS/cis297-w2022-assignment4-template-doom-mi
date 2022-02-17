using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PatientRecordApplication
{
    class PatientNotFoundException : Exception
    {
        public PatientNotFoundException()
        {
            WriteLine("Patient not found");
        }
    }
}
