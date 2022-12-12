using System;
using System.Linq;


namespace ContractSystemsTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input path to scan directory");
            string rootDirectory = Console.ReadLine();

            try
            {
                var directoryAnalyzer = new DirectoryAnalyzer(rootDirectory);

                var dirStructure = directoryAnalyzer.AnalyzeDirectoryStructure(rootDirectory, 0, new(), new());
                var mimeInfo = directoryAnalyzer.AnalyzeMimeTypes(dirStructure);

                HTMLFile.Save(@"DirectoryStructure.html", dirStructure, mimeInfo);
            } 
            catch (ArgumentException)
            {
                Console.WriteLine($"Directory {rootDirectory} does not exist");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error during directory parsing", e);
            }


        }

    }
}



