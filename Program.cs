using System;
using System.IO;
using System.Collections;
using System.Text;

namespace ParseCSV
{
    class Program
    {

        // Email validation method, taken and simplified from https://stackoverflow.com/questions/1365407/
        public static bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

        public static void Main (string[] args) {
            Console.WriteLine("Please type the file name: ");
            var fileName = Console.ReadLine() + ".csv";
            string directory = Directory.GetCurrentDirectory();
            string filePath = directory + "\\" + fileName;
            Console.WriteLine($"Now searching the directory {directory} for the file {fileName}");

            try
            {
                if (!File.Exists(fileName)) 
                    throw new FileNotFoundException();
                Console.WriteLine("This file exists");
            } 
            catch  (FileNotFoundException error)
            {
                Console.WriteLine($"This file does not exist - {error}");
            }
            
            // Read the entire csv file an array of strings
            string[] csvLines = System.IO.File.ReadAllLines(filePath);

            // Splitting the contents of each line

            List<string> validEmails = new List<string>();
            List<string> invalidEmails = new List<string>();

            for(int i = 0; i < csvLines.Length; i++) {
                string[] rowData = csvLines[i].Split(',');
                string emailField = rowData[2];
                if(IsValidEmail(emailField)) {
                    validEmails.Add(emailField);
                } 
                else {
                    invalidEmails.Add(emailField);
                }
            }

            Console.WriteLine("Valid email addresses: ");
            for(int i = 0; i < validEmails.Count; i++) {
                Console.WriteLine(validEmails[i]);
            }

            Console.WriteLine("Invalid email addresses: ");
            for(int i = 0; i < invalidEmails.Count; i++) {
                Console.WriteLine(invalidEmails[i]);
            }
        }

    }
}