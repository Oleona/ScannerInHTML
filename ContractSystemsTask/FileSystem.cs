
namespace ContractSystemsTask
{
    abstract class FileSystemElement
    {
       public readonly string Name;

       public readonly int Level;

       public FileSystemElement(string name, int level)
        {
            Name = name;
            Level = level;
        } 
    }
}
