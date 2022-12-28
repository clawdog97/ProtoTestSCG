using Newtonsoft.Json;
using SCG.Test;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace ProtoTestSCG
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            // Check for valid command line argument
            if(args.Length != 1)
            {
                Console.WriteLine("Error: Invalid arguments");
                return -1;
            }

            string filename = args[0];

            // Check for valid filename
            if(filename.Length == 0)
            {
                Console.WriteLine("Error: Invalid filename");
                return -1;
            }

            if (File.Exists(filename))
            {
                try
                {
                    // Open file for reading
                    using (Stream file = File.OpenRead(filename))
                    {
                        // Parse message from file using google protocol buffer
                        TestMessage testMessage = TestMessage.Parser.ParseFrom(file);

                        // Serialize Test Message object to a JSON string
                        string content = JsonConvert.SerializeObject(testMessage, Formatting.Indented);
                        
                        // Write JSON string to console
                        Console.WriteLine(content);
                        
                        // Get the parent directory of the file that is being read 
                        DirectoryInfo directoryInfo = Directory.GetParent(filename);

                        // Create a new text file that the contents will be written to
                        string outputFile = Path.Combine(directoryInfo.FullName, "jsonMessage.txt");

                        // Write JSON string to output file
                        File.WriteAllTextAsync(outputFile, content);

                    }
                }
                catch (Exception ex)
                {
                    // Write exception message to console
                    Console.WriteLine(ex.Message);
                }
            }
            return 0;
        }
    }
}
