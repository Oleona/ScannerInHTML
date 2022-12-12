
using System.Collections.Generic;

namespace ContractSystemsTask
{
    class Directory : FileSystemElement
    {
        public readonly List<File> Files;

        public readonly long FolderSize;

        public Directory(string name, int level, List<File> files, long size)
            : base(name, level)
        {
            Files = files;
            FolderSize = size;
        }
    }
}
