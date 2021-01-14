using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ApiSQL.Models
{
    public class ProcessFiles
    {
        private static string urlExcel = "D://OneDrive//Escritorio//repo//cvsTemp//";

        /// <summary>
        /// Funcion que salva un cvs en una carpeta temporal para leerlo
        /// </summary>
        /// <param name="inFile"></param>
        /// <param name="nombreCarrera"></param>
        /// <returns>Direccion del archivo de la forma tablaTemp.csv/returns>
        public static string saveCsvTemp(FileModel inFile)
        {
            string content = inFile.file;
            byte[] data = Encoding.ASCII.GetBytes(content);
            string name = "tablaTemp.csv";
            try
            {
                using (var fs = new FileStream(urlExcel + name, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    return name;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return null;
            }
        }
    }
}