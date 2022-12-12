
namespace ContractSystemsTask
{
    internal struct MimeTypeInfo
    {
        public readonly string MimeType;

        public readonly int Quantity;

        public readonly double Percentage;

        public readonly double AverageFileSize;

        public MimeTypeInfo(string mimeType, int quantity, double percentage, double averageFileSize)
        {
            MimeType = mimeType;
            Quantity = quantity;
            Percentage = percentage;
            AverageFileSize = averageFileSize;
        }
    }
}
