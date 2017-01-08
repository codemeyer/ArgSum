using System.IO;

namespace ArgSum
{
    public class FileSystem
    {
        public virtual bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public virtual long GetFileLength(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Length;
        }
    }
}
