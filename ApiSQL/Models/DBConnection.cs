using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    /// <summary>
    /// Clase que se utiliza como plataforma para conectar123
    /// 
    /// </summary>
    public class DBConnection
    {

        private string urlExcel = "D://OneDrive//Escritorio//repo//cvsTemp//";
        private string tempTableName = "tablaTemp";

        public OdbcConnection connection;

        /// <summary>
        /// Constructor de la clase, se establece la conexión con la base de datos a través de ODBC
        /// </summary>
        public DBConnection()
        {
            connection = new OdbcConnection("Dsn=XTECDigital_ODBC;" + "Uid=XTECDigital;" + "Pwd=123;");
        }

        // ----------------------------- CARPETA -----------------------------

        /// <summary>
        /// Método para obtener las carpetas de un curso dado
        /// </summary>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre del curso</param>
        /// <param name="sem_anno">Año del semestre del curso</param>
        /// <returns>Lista de carpetas asociadas al curso</returns>
        public ArrayList GetCarpetas(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            ArrayList folders = new ArrayList();
            String queryString = "SELECT Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM CARPETA WHERE Curso_Grupo = " + curso_grupo+ " AND Curso_Código = '" + curso_codigo+ "' AND Sem_Periodo = " + sem_periodo + " AND Sem_Año = " + sem_anno+";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CARPETA folder = new CARPETA(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetChar(3),reader.GetString(4));
                folders.Add(folder);
            }
            connection.Close();
            return folders;
        }

        public String CreateCarpeta(CARPETA folder)
        {
            String queryString = "INSERT INTO CARPETA (Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('"+folder.Nombre+"',"+folder.Curso_Grupo+ ",'" + folder.Curso_Codigo+ "'," + folder.Sem_Periodo+ "," + folder.Sem_Anno+");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return "OK";
        }

        public String UpdateCarpeta(CARPETA origfolder, CARPETA modfolder)
        {
            String queryString = "SELECT Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM CARPETA WHERE Nombre = '" + origfolder.Nombre + "' AND Curso_Grupo = " + origfolder.Curso_Grupo + " AND Curso_Código = '" + origfolder.Curso_Codigo + "' AND Sem_Periodo = " + origfolder.Sem_Periodo + " AND Sem_Año = " + origfolder.Sem_Anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "UPDATE CARPETA SET Nombre = '" + modfolder.Nombre + "' AND Curso_Grupo = " + modfolder.Curso_Grupo + " AND Curso_Código = '" + modfolder.Curso_Codigo + "' AND Sem_Periodo = " + modfolder.Sem_Periodo + " AND Sem_Año = " + modfolder.Sem_Anno + " WHERE Nombre = '" + origfolder.Nombre + "' AND Curso_Grupo = " + origfolder.Curso_Grupo + " AND Curso_Código = '" + origfolder.Curso_Codigo + "' AND Sem_Periodo = " + origfolder.Sem_Periodo + " AND Sem_Año = " + origfolder.Sem_Anno + ";";
                OdbcCommand command1 = new OdbcCommand(queryString, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }

        public String DeleteCarpeta(CARPETA folder)
        {
            String queryString = "SELECT Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM CARPETA WHERE Nombre = '" + folder.Nombre + "' AND Curso_Grupo = " + folder.Curso_Grupo + " AND Curso_Código = '" + folder.Curso_Codigo + "' AND Sem_Periodo = " + folder.Sem_Periodo + " AND Sem_Año = " + folder.Sem_Anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "DELETE FROM CARPETA WHERE Nombre = '" + folder.Nombre + "' AND Curso_Grupo = " + folder.Curso_Grupo + " AND Curso_Código = '" + folder.Curso_Codigo + "' AND Sem_Periodo = " + folder.Sem_Periodo + " AND Sem_Año = " + folder.Sem_Anno + ";";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }

        // ----------------------------- DOCUMENTO -----------------------------

        public ArrayList GetDocumentos(String carpeta_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            ArrayList documents = new ArrayList();
            String queryString = "SELECT Nombre, Data, Tamaño, Fecha_Subida, Carpeta_Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM DOCUMENTO WHERE Carpeta_Nombre = '" + carpeta_nombre +  "' AND Curso_Grupo = " + curso_grupo + " AND Curso_Código = '" + curso_codigo + "' AND Sem_Periodo = " + sem_periodo + " AND Sem_Año = " + sem_anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                    DOCUMENTO document = new DOCUMENTO(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetChar(7), reader.GetString(8));
                documents.Add(document);
            }
            connection.Close();
            return documents;
        }

        public String CreateDocumento(DOCUMENTO document)
        {
            String queryString = "INSERT INTO DOCUMENTO (Nombre, Data, Tamaño, Fecha_Subida, Carpeta_Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + document.Nombre + "'," + document.Data + ",'" + document.Tamanno + "'," + document.Fecha_Subida + "," + document.Carpeta_Nombre + document.Curso_Grupo + "," + document.Curso_Codigo + document.Sem_Periodo + "," + document.Sem_Anno + ");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return "OK";
        }


        public String DeleteDocumento(DOCUMENTO document)
        {
            String queryString = "SELECT Nombre, Data, Tamaño, Fecha_Subida, Carpeta_Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM DOCUMENTO WHERE Nombre = '" + document.Nombre + "' AND Carpeta_Nombre = '" + document.Carpeta_Nombre + "' AND Curso_Grupo = " + document.Curso_Grupo + " AND Curso_Código = '" + document.Curso_Codigo + "' AND Sem_Periodo = " + document.Sem_Periodo + " AND Sem_Año = " + document.Sem_Anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "DELETE FROM DOCUMENTO WHERE Nombre = '" + document.Nombre + "' AND Carpeta_Nombre = '" + document.Carpeta_Nombre + "' AND Curso_Grupo = " + document.Curso_Grupo + " AND Curso_Código = '" + document.Curso_Codigo + "' AND Sem_Periodo = " + document.Sem_Periodo + " AND Sem_Año = " + document.Sem_Anno + ";";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }

        // ----------------------------- CURSO -----------------------------



        // ----------------------------- CURSO_IMPARTIDO -----------------------------

        public ArrayList GetProfesorCursoImpartido(String cedula)
        {
            ArrayList cursos_impartidos = new ArrayList();
            String queryString = "SELECT Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM PROFESOR_ASIGNADO WHERE Cédula = '" + cedula + "';";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CURSO_IMPARTIDO curso_impartido = new CURSO_IMPARTIDO(reader.GetString(0), reader.GetString(1), reader.GetChar(2), reader.GetString(3));
                cursos_impartidos.Add(curso_impartido);
            }
            connection.Close();
            return cursos_impartidos;
        }

        public ArrayList GetEstudianteCursoImpartido(String carnet)
        {
            ArrayList cursos_impartidos = new ArrayList();
            String queryString = "SELECT Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM ESTUDIANTE_MATRICULADO WHERE Carnet = '" + carnet + "';";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CURSO_IMPARTIDO curso_impartido = new CURSO_IMPARTIDO(reader.GetString(0), reader.GetString(1), reader.GetChar(2), reader.GetString(3));
                cursos_impartidos.Add(curso_impartido);
            }
            connection.Close();
            return cursos_impartidos;
        }

            public String CreateCursoImpartido(CURSO_IMPARTIDO curso_impartido)
            {
            String queryString = "INSERT INTO CURSO_IMPARTIDO (Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + curso_impartido.Grupo + ",'" + curso_impartido.Curso_Codigo + "'," + curso_impartido.Sem_Periodo + "," + curso_impartido.Sem_Anno + ");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return "OK";
            }

        // ----------------------------- RUBRO -----------------------------

        public ArrayList GetRubros(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            ArrayList rubros = new ArrayList();
            String queryString = "SELECT Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM RUBRO WHERE Curso_Grupo = " + curso_grupo + " AND Curso_Código = '" + curso_codigo + "' AND Sem_Periodo = " + sem_periodo + " AND Sem_Año = " + sem_anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                RUBRO rubro = new RUBRO(reader.GetString(0), reader.GetFloat(1), reader.GetString(2), reader.GetString(3), reader.GetChar(4), reader.GetString(5));
                rubros.Add(rubro);
            }
            connection.Close();
            return rubros;
        }

        public String CreateRubro(RUBRO rubro)
        {
            String queryString = "INSERT INTO RUBRO (Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + rubro.Nombre + "'," + rubro.Curso_Grupo + ",'" + rubro.Curso_Codigo + "'," + rubro.Sem_Periodo + "," + rubro.Sem_Anno + ");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return "OK";
        }

        public String UpdateRubro(RUBRO rubro)
        {
            String queryString = "SELECT Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM CARPETA WHERE Nombre = '" + rubro.Nombre + "' AND Curso_Grupo = " + rubro.Curso_Grupo + " AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "UPDATE CARPETA SET Nombre = '" + rubro.Nombre + "' AND Porcentaje = '" + rubro.Porcentaje + "' AND Curso_Grupo = " + rubro.Curso_Grupo + " AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + " WHERE Nombre = '" + rubro.Nombre + "' AND Curso_Grupo = " + rubro.Curso_Grupo + " AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + ";";
                OdbcCommand command1 = new OdbcCommand(queryString, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }

        public String DeleteRubro(RUBRO rubro)
        {
            String queryString = "SELECT Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM RUBRO WHERE Nombre = '" + rubro.Nombre + "' AND Curso_Grupo = " + rubro.Curso_Grupo + " AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "DELETE FROM RUBRO WHERE Nombre = '" + rubro.Nombre + "' AND Curso_Grupo = " + rubro.Curso_Grupo + " AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + ";";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }


        // ----------------------------- SEMESTRE -----------------------------

        // ----------------------------- NOTICIA -----------------------------

        public ArrayList GetNoticia(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            ArrayList noticias = new ArrayList();
            String queryString = "SELECT Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año, Título, Autor, Fecha, Mensaje FROM NOTICIA WHERE Curso_Grupo = " + curso_grupo + " AND Curso_Código = '" + curso_codigo + "' AND Sem_Periodo = " + sem_periodo + " AND Sem_Año = " + sem_anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                NOTICIA noticia = new NOTICIA(reader.GetString(0), reader.GetString(1), reader.GetChar(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
               noticias.Add(noticia);
            }
            connection.Close();
            return noticias;
        }

        public String CreateNoticia(NOTICIA noticia)
        {
            String queryString = "INSERT INTO NOTICIA (Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año, Título, Autor, Fecha, Mensaje) VALUES ('" + noticia.Curso_Grupo + ",'" + noticia.Curso_Codigo + "'," + noticia.Sem_Periodo + "," + noticia.Sem_Anno + noticia.Titulo + "," + noticia.Autor + noticia.Fecha + "," + noticia.Mensaje+ ");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return "OK";
        }

        //Reemplazar por sp

        private void createTempTable()
        {
            string sqlQuery = "CREATE TABLE " + tempTableName + "(";
            string sqlDBType = "";
            string dataType = "";
            int maxLength = 0;

            sqlQuery = sqlQuery.Trim().TrimEnd(',');
            sqlQuery += " )";
            OdbcConnection database = connection;
            database.Open();
            OdbcCommand command = new OdbcCommand(sqlQuery, database);
            command.ExecuteNonQuery();
        }

        private void loadCsvToTable(string delimeter)
        {
            string sqlQuery = "BULK INSERT " + tempTableName;
            sqlQuery += " FROM '" + urlExcel + tempTableName + ".csv'";
            sqlQuery += " WITH ( FIELDTERMINATOR = '" + delimeter + "', ROWTERMINATOR = '\n' )";
            OdbcConnection database = connection;
            database.Open();
            OdbcCommand command = new OdbcCommand(sqlQuery, database);
            command.ExecuteNonQuery();
        }

        public void processFile()
        {
            string queryString = "SELECT * FROM [" + tempTableName + ".csv" + "]";
            string strCSVConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + urlExcel + ";" + "Extended Properties='text;HDR=YES;'";
            DataTable dtCSV = new DataTable();
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, strCSVConnString))
            {
                adapter.FillSchema(dtCSV, SchemaType.Mapped);
                adapter.Fill(dtCSV);
            }

            if (dtCSV.Rows.Count > 0)
            {
                createTempTable();
                loadCsvToTable(",");
            }

        }
    }
}