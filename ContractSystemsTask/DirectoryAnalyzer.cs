using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ContractSystemsTask.Tests")]
namespace ContractSystemsTask
{
    internal class DirectoryAnalyzer
    {
        private string rootDirectoryPath;

        internal DirectoryAnalyzer(string rootDirectoryPath)
        {
            this.rootDirectoryPath = rootDirectoryPath;
        }

        internal List<Directory> AnalyzeDirectoryStructure(
            string currentDirectoryPath,
            int level,
            List<File> files,
            List<Directory> directories
            )
        {
            if (!System.IO.Directory.Exists(currentDirectoryPath))
                throw new ArgumentException($"Directory {currentDirectoryPath} does not exist.");

           
                Directory currentDirectory = new(
                    currentDirectoryPath.Replace(rootDirectoryPath, ""),
                    level,
                    new List<File>(),
                    CalculateSize(currentDirectoryPath)
                    );
                directories.Add(currentDirectory);

                level++;

                files.AddRange(System.IO.Directory.GetFiles(currentDirectoryPath)
                    .Select(file => CreateFile(file, level)));
                currentDirectory.Files.AddRange(files);


                foreach (var subDir in System.IO.Directory.GetDirectories(currentDirectoryPath))
                {
                    AnalyzeDirectoryStructure(subDir, level, new(), directories);
                }

            return directories;
        }

        internal List<MimeTypeInfo> AnalyzeMimeTypes(List<Directory> directoryStructure)
        {
            List<MimeTypeInfo> mimeTypeInfos = new();
            var filesByMimeType = directoryStructure
                .SelectMany(d => d.Files)
                .GroupBy(f => f.MimeType)
                .ToDictionary(gr => gr.Key, gr => gr.ToList());

            var allFilesCount = directoryStructure.SelectMany(f => f.Files).Count();

            foreach (KeyValuePair<string, List<File>> mimeTypeFiles in filesByMimeType)
            {
                var thisTypeFilesCount = mimeTypeFiles.Value.Count;
                var thisTypeFilesPercentage = Math.Round(100.0 * thisTypeFilesCount / allFilesCount, 2);
                var thisTypeAverageSize = Math.Round(mimeTypeFiles.Value.Average(f => f.FileSize), 2);

                mimeTypeInfos.Add(
                    new(mimeTypeFiles.Key, thisTypeFilesCount, thisTypeFilesPercentage, thisTypeAverageSize)
                    );
            }

            return mimeTypeInfos;
        }

        private File CreateFile(string fullName, int level)
        {
            var shortFileName = Path.GetFileName(fullName);
            var fileSize = new FileInfo(fullName).Length;

            return new(shortFileName, level, fileSize);
        }

        private long CalculateSize(string directoryPath)
        {
            var dirInfo = new DirectoryInfo(directoryPath);
            return dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }
    }
}
