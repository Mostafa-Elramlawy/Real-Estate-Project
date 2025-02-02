using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime;

namespace ClassLibrary2
{
    public class PythonService
    {
        public void ExecutePythonCode()
        {
            using (Py.GIL()) // Acquire the Python Global Interpreter Lock (GIL)
            {
                dynamic python = Py.Import("sys");
                Console.WriteLine(python.version);
            }
        }

        public string GetPythonVariable()
        {
            using (Py.GIL()) // Acquire the Python Global Interpreter Lock (GIL)
            {
                dynamic python = Py.Import("predection"); // Import your Python module or script

                // Access the desired Python variable
                string variableValue = python.variable_name;

                return variableValue;
            }
        }
    }
}
