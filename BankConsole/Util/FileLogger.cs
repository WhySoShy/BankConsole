using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsole.Util
{
    public class FileLogger
    {
        static readonly string _fileName = @"C:\Users\emilk\source\repos\BankConsole\Log\Log.txt"; 


        public static void WriteToLog(string logMessage)
        {
            using (StreamWriter logWriter = File.AppendText(_fileName))
            {
                logWriter.WriteLine($"{DateTime.UtcNow}     {logMessage}");
            }
        }

        public static string ReadFromLog()
        {
            using (StreamReader reader = new StreamReader(_fileName))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
