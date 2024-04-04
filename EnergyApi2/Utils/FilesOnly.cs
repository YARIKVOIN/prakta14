using System.IO;

namespace EnergyApi2.Utils
{
    public class FilesOnly
    {
        public static bool isFilesBackup()
        {
            return File.Exists("C:\\BackUp\\db_EnergyTorpedaBackUp.sql");
        }
    }
}
