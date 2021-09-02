using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConnectWiseAssessment
{
    class Program
    {
        static string workingDirectory = Environment.CurrentDirectory;
        static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        static string filename = $"{projectDirectory}\\SampleInputFile.txt";
        static string invalidDataErrorMessage = "Invalid data, unable to process";
        static void Main(string[] args)
        {
            string content = "";
            try
            {
                content = File.ReadAllText(filename);
            }
            catch (NotSupportedException)
            {
                Console.WriteLine(invalidDataErrorMessage);
            }

            var outputRecords = RecordHelperMethods.GetRecords(content);
            RecordHelperMethods.PrintRecords(outputRecords);
        }

        
    }
}
