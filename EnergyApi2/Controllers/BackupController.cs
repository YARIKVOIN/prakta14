
using EnergyApi2.Models;
using EnergyApi2.Utils;
using EnergyApi2.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EnergyApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : Controller
    {
        private readonly EnergyTorpedaContext _context;
        public BackupController(EnergyTorpedaContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> DownloadDatabaseBackup()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "C:\\BackUp\\BackupFile.bat";
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();
                proc.WaitForExit();
                var net = new System.Net.WebClient();
                var data = net.DownloadData("C:\\BackUp\\db_EnergyTorpedaBackUp.sql");
                var content = new System.IO.MemoryStream(data);
                var contentType = "APPLICATION/octet-stream";
                var fileName = "db_EnergyTorpedaBackUp2.sql";
                return File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("restore")]
        public async Task<IActionResult> restoreDatabase()
        {
            if (!FilesOnly.isFilesBackup()) return BadRequest("Выбранного файла не существует");
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "C:\\BackUp\\RestoreFile.bat";
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();

                return Ok("Импорт базы данных успешно произведён");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        public class FileDetails
        {
            public string Name { get; set; } = null!;
            public string Extension { get; set; } = null!;
            public DateTime CreationTime { get; set; }
            public DateTime LastWriteTime { get; set; }
        }
    }
}