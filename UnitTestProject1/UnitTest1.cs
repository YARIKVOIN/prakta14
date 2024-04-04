using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheackBackupLoadFile()
        {
            bool check = true;
            Backup backup = new Backup();
            bool load = backup.BackupLoadFile();

            Assert.AreEqual(check, load);
        }

        [TestMethod]
        public void CheackRefreshDataBase()
        {
            bool check = true;
            string tunel = "C:\\BackUp\\BackUp.sql";
            Backup backup = new Backup();
            bool load = backup.RefreshDataBase(tunel);

            Assert.AreEqual(check, load);
        }

        [TestMethod]
        public void CheackGetStatistikFordATE()
        {
            int check = 2000;
            DateTime date = new DateTime();
            Backup backup = new Backup();
            int load = backup.GetStatistikFordATE(date);

            Assert.AreEqual(check, load);
        }
    }
}
