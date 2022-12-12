
using MimeTypes;


namespace ContractSystemsTask
{
    class File : FileSystemElement
    {

        public readonly string MimeType;
        public readonly long FileSize;

        public File(string name, int level, long fileSize)
            : base(name, level)
        {
            MimeType = MimeTypeMap.GetMimeType(name);
            FileSize = fileSize;
        }
    }

}
