using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConnectWiseAssessment
{
    public static class RecordHelperMethods
    {
        public static List<OutputRecord> GetRecords(string content)
        {
            string[] inputEntries = Regex.Split(content, @"\r\n\r\n");
            List<InputRecord> inputRecords = new List<InputRecord>();
            for (int i = 0; i < inputEntries.Length; i++)
            {
                string[] currentRecord = Regex.Split(inputEntries[i], @"\r\n");
                Dictionary<string, string> currentRecordValues = new Dictionary<string, string>();

                foreach (var keyvaluepair in currentRecord)
                {
                    currentRecordValues.Add(keyvaluepair.Split(')')[0].Replace("(", String.Empty), keyvaluepair.Split(')')[1]);
                }

                inputRecords.Add(new InputRecord()
                {
                    Name = currentRecordValues[nameof(InputRecord.Name)],
                    Age = currentRecordValues.ContainsKey(nameof(InputRecord.Age)) ? currentRecordValues[nameof(InputRecord.Age)] : "",
                    City = currentRecordValues[nameof(InputRecord.City)],
                    Flags = currentRecordValues[nameof(InputRecord.Flags)]
                });
            }

            List<OutputRecord> outputRecords = new List<OutputRecord>();
            foreach (var record in inputRecords)
            {
                outputRecords.Add(new OutputRecord()
                {
                    Name = record.Name,
                    Age = record.Age,
                    City = record.City.Contains(",") ? record.City.Split(',')[0].Trim() : record.City,
                    State = record.City.Contains(",") ? record.City.Split(',')[1].Trim() : "",
                    Gender = record.Flags[0] == 'Y' ? "Female" : "Male",
                    Student = record.Flags[1] == 'Y' ? "Yes" : "No",
                    Employee = record.Flags[2] == 'Y' ? "Yes" : "No"
                });
            }

            return outputRecords;
        }

        public static void PrintRecords(List<OutputRecord> outputRecords)
        {
            foreach (var record in outputRecords)
            {
                Console.WriteLine($"{record.Name} [{(!String.IsNullOrEmpty(record.Age) ? (record.Age + ", ") : "")}{record.Gender}]");
                Console.WriteLine($"    {nameof(OutputRecord.City)} : {record.City}");
                Console.WriteLine($"    {nameof(OutputRecord.State)} : {(!String.IsNullOrEmpty(record.State) ? record.State : "N/A")}");
                Console.WriteLine($"    {nameof(OutputRecord.Student)} : {record.Student}");
                Console.WriteLine($"    {nameof(OutputRecord.Employee)} : {record.Employee}");
            }
        }
    }
}
