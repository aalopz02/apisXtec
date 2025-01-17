﻿using System;
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
        private Correos emailService = new Correos();
        private clienteHttp comsMongo = new clienteHttp();
        private string urlExcel = "C://inetpub//wwwroot//repo//cvsTemp//";

        private string tempTableName = "tablaTemp";

        /// <summary>
        /// Instanciación de la clase que contiene los métodos para el envío de correos electrónicos
        /// </summary>
        Correos correos = new Correos();

        public OdbcConnection connection;

        /// <summary>
        /// Constructor de la clase, se establece la conexión con la base de datos a través de ODBC
        /// </summary>
        public DBConnection()
        {
            connection = new OdbcConnection("Dsn=XTECDigital_ODBC;" + "Uid=XTECDigital;" + "Pwd=123;");
        }

        /// <summary>
        /// Método usado para pruebas
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSemestre()
        {
            try
            {
                String queryString = "SELECT * FROM ESTUDIANTE_MATRICULADO;";
                connection.Open();
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();
                ArrayList folders = new ArrayList();
                while (reader.Read())
                {
                    ESTUDIANTE_MATRICULADO folder = new ESTUDIANTE_MATRICULADO(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3).ToCharArray()[0], reader.GetString(4));
                    folders.Add(folder);
                }
                connection.Close();

                queryString = "SELECT * FROM tablaTemp;";
                connection.Open();
                command = new OdbcCommand(queryString, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ESTUDIANTE_MATRICULADO folder = new ESTUDIANTE_MATRICULADO(reader.GetString(0), reader.GetString(4), reader.GetString(5), reader.GetString(3).ToCharArray()[0], reader.GetString(4));
                    folders.Add(folder);
                }
                connection.Close();

                return folders;
            }
            catch (OdbcException e)
            {
                ArrayList error = new ArrayList();
                error.Add(e.Message);
                return error;
            }


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
            String queryString = "SELECT Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM CARPETA WHERE Curso_Grupo = " + curso_grupo + " AND Curso_Código = '" + curso_codigo + "' AND Sem_Periodo = " + sem_periodo + " AND Sem_Año = " + sem_anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CARPETA folder = new CARPETA(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetChar(3), reader.GetString(4));
                folders.Add(folder);
            }
            connection.Close();
            return folders;
        }

        /// <summary>
        /// Método para crear una carpeta
        /// </summary>
        /// <param name="folder">Carpeta por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        public String CreateCarpeta(CARPETA folder)
        {
            String queryString = "INSERT INTO CARPETA (Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + folder.Nombre + "','" + folder.Curso_Grupo + "','" + folder.Curso_Codigo + "'," + folder.Sem_Periodo + "," + folder.Sem_Anno + ");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return "OK";
        }

        /// <summary>
        /// Método para eliminar una carpeta
        /// </summary>
        /// <param name="folder">Carpetar por eliminar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        public String DeleteCarpeta(CARPETA folder)
        {
            String queryString = "SELECT Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM CARPETA WHERE Nombre = '" + folder.Nombre + "' AND Curso_Grupo = '" + folder.Curso_Grupo + "' AND Curso_Código = '" + folder.Curso_Codigo + "' AND Sem_Periodo = " + folder.Sem_Periodo + " AND Sem_Año = " + folder.Sem_Anno + ";";
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

        /// <summary>
        /// Método para manejar documentos
        /// </summary>
        /// <param name="carpeta_nombre"></param>
        /// <param name="curso_grupo"></param>
        /// <param name="curso_codigo"></param>
        /// <param name="sem_periodo"></param>
        /// <param name="sem_anno"></param>
        /// <returns></returns>
        public ArrayList GetDocumentos(String carpeta_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            ArrayList documents = new ArrayList();
            String queryString = "SELECT Nombre, Data, Tamaño, Fecha_Subida, Carpeta_Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM DOCUMENTO WHERE Carpeta_Nombre = '" + carpeta_nombre + "' AND Curso_Grupo = " + curso_grupo + " AND Curso_Código = '" + curso_codigo + "' AND Sem_Periodo = " + sem_periodo + " AND Sem_Año = " + sem_anno + ";";
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

        /// <summary>
        /// Método para crear documentos
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public String CreateDocumento(DOCUMENTO document)
        {
            if (document == null)
            {
                return "mae sigue null";
            }
            String queryString = "INSERT INTO DOCUMENTO (Nombre, Data, Tamaño, Fecha_Subida, Carpeta_Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ( '" + document.Nombre + "' , '" + document.Data + "' , '" + document.Tamanno + "' , '" + document.Fecha_Subida + "' , '" + document.Carpeta_Nombre + "' , " + document.Curso_Grupo + ", '" + document.Curso_Codigo + "' , " + document.Sem_Periodo + "," + document.Sem_Anno + ");";
            connection.Open();
            try
            {

                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.ExecuteNonQuery();
                connection.Close();
                IEnumerable correos = comsMongo.GetCorreos();
                queryString = "SELECT Carnet FROM ESTUDIANTE_MATRICULADO WHERE " +
                        " Curso_Grupo = '" + document.Curso_Grupo + "' AND " +
                        " Curso_Código = '" + document.Curso_Codigo + "' AND " +
                        " Sem_Periodo = '" + document.Sem_Periodo + "' AND " +
                        "Sem_Año = '" + document.Sem_Anno + "';";
                command = new OdbcCommand(queryString, connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                ArrayList carnetEstudiantesCurso = new ArrayList();

                while (reader.Read())
                {
                    carnetEstudiantesCurso.Add(reader.GetString(0));
                }
                foreach (ModelEstudianteCarnet estudiante in correos)
                {
                    if (carnetEstudiantesCurso.Contains(estudiante.carnet))
                    {
                        emailService.email_documento(estudiante.correo, document.Curso_Codigo);
                    }
                    connection.Close();
                }
                return "OK";

            }
            catch (OdbcException e)
            {
                connection.Close();
                return "caca" + e.Message;
            }

        }

        /// <summary>
        /// Método para eliminar documentos
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
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
            String queryString = "INSERT INTO CURSO_IMPARTIDO (Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + curso_impartido.Grupo + ",'" + curso_impartido.Curso_Codigo + "', " + curso_impartido.Sem_Periodo + " , " + curso_impartido.Sem_Anno + ");";
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
            String queryString = "INSERT INTO RUBRO (Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + rubro.Nombre + "'," + rubro.Porcentaje + ",'" + rubro.Curso_Grupo + "','" + rubro.Curso_Codigo + "'," + rubro.Sem_Periodo + "," + rubro.Sem_Anno + ");";
            connection.Open();
            try
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
            catch (OdbcException e)
            {
                connection.Close();
                return "error: " + e.Message + "; query: " + queryString;
            }
            connection.Close();
            return "OK";
        }

        public String UpdateRubro(RUBRO rubro)
        {
            String queryString = "SELECT Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM RUBRO WHERE Nombre = '" + rubro.Nombre + "' AND Curso_Grupo = '" + rubro.Curso_Grupo + "' AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "UPDATE RUBRO SET Porcentaje = " + rubro.Porcentaje + " WHERE Nombre = '" + rubro.Nombre + "' AND Curso_Grupo = '" + rubro.Curso_Grupo + "' AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + ";";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404" + queryString;
        }

        public String DeleteRubro(RUBRO rubro)
        {
            String queryString = "SELECT Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año FROM RUBRO WHERE Nombre = '" + rubro.Nombre + "' AND Curso_Grupo = '" + rubro.Curso_Grupo + "' AND Curso_Código = '" + rubro.Curso_Codigo + "' AND Sem_Periodo = " + rubro.Sem_Periodo + " AND Sem_Año = " + rubro.Sem_Anno + ";";
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

        public String PostSemestreCurso(String anno, String perido, cursoModel value)
        {
            CrearSemestre(anno, perido);
            CreateCursoImpartidoAdmin(new CURSO_IMPARTIDO(value.numeroGrupo.ToString(), value.codigoCurso, perido.ToCharArray()[0], anno));
            CrearProfesorAsignado(new PROFESOR_ASIGNADO(value.profesor1.ToString(), value.numeroGrupo.ToString(), value.codigoCurso, perido.ToCharArray()[0], anno));
            if (!String.IsNullOrEmpty(value.profesor2))
            {
                CrearProfesorAsignado(new PROFESOR_ASIGNADO(value.profesor2.ToString(), value.numeroGrupo.ToString(), value.codigoCurso, perido.ToCharArray()[0], anno));
            }
            for (int i = 0; i < value.grupo.Count; i++)
            {
                String carnet = value.grupo[i].ToString().Trim().Split(':')[1].Replace("}", "").Trim();
                carnet = carnet.Substring(1, carnet.Length - 2);
                carnet = carnet.Substring(0, carnet.Length);
                CrearEstudianteMatriculado(new ESTUDIANTE_MATRICULADO(carnet, value.numeroGrupo.ToString(), value.codigoCurso, perido.ToCharArray()[0], anno));
            }
            return "ok";
        }

        public void CrearSemestre(String anno, String perido)
        {
            String queryString = "INSERT INTO SEMESTRE (Periodo, Año) VALUES ( '" + perido + "' , '" + anno + "');";
            connection.Open();
            try
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();

            }
            catch (OdbcException)
            {

            }
            connection.Close();

        }

        public void CreateCursoImpartidoAdmin(CURSO_IMPARTIDO curso_impartido)
        {
            String queryString = "INSERT INTO CURSO_IMPARTIDO (Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + curso_impartido.Grupo + "','" + curso_impartido.Curso_Codigo + "', '" + curso_impartido.Sem_Periodo + "' , '" + curso_impartido.Sem_Anno + "');";
            connection.Open();
            try
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
            catch (OdbcException)
            {

            }
            connection.Close();
        }

        public String DeleteSemestre(SEMESTRE semestre)
        {
            String queryString = "SELECT Periodo, Año FROM SEMESTRE WHERE Periodo = '" + semestre.Periodo + "AND Año = " + semestre.Anno + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "DELETE FROM CURSO WHERE Periodo = '" + semestre.Periodo + "AND Año = " + semestre.Anno + ";";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }


        public ArrayList GetSemestres()
        {
            ArrayList semestres = new ArrayList();
            String queryString = "SELECT Periodo, Año FROM SEMESTRE;";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SEMESTRE semestre = new SEMESTRE(reader.GetChar(0), reader.GetString(1));
                semestres.Add(semestre);
            }
            connection.Close();
            return semestres;
        }


        //------------------------------Profesor asignado-----------------------


        public void CrearProfesorAsignado(PROFESOR_ASIGNADO profesor)
        {
            String queryString = "INSERT INTO PROFESOR_ASIGNADO (Cédula, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año) VALUES ('" + profesor.Cedula + "' , '" +
                                  profesor.Curso_Grupo + "' , '" + profesor.Curso_Codigo + "' , '" + profesor.Sem_Periodo + "' , '" + profesor.Sem_Anno + "');";
            connection.Open();
            try
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();
            }
            catch (OdbcException)
            {

            }
            connection.Close();
        }

        //------------------------------Estudiante matriculado

        public void CrearEstudianteMatriculado(ESTUDIANTE_MATRICULADO estudiante)
        {
            String queryString = "INSERT INTO ESTUDIANTE_MATRICULADO  (Carnet,Curso_Grupo,Curso_Código,Sem_Periodo,Sem_Año) VALUES (" +
               "'" + estudiante.Carnet + "' , '" + estudiante.Curso_Grupo + "' , '" + estudiante.Curso_Codigo + "' , '" + estudiante.Sem_Periodo + "' , '" + estudiante.Sem_Anno + "');";
            connection.Open();
            try
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();
            }
            catch (OdbcException)
            {

            }
            connection.Close();
        }

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
            String queryString = "INSERT INTO NOTICIA (Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año, Título, Autor, Fecha, Mensaje) VALUES ('" + noticia.Curso_Grupo + ",'" + noticia.Curso_Codigo + "'," + noticia.Sem_Periodo + "," + noticia.Sem_Anno + noticia.Titulo + "," + noticia.Autor + noticia.Fecha + "," + noticia.Mensaje + ");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();

            IEnumerable correos = comsMongo.GetCorreos();
            queryString = "SELECT Carnet FROM ESTUDIANTE_MATRICULADO WHERE " +
                    " Curso_Grupo = '" + noticia.Curso_Grupo + "' AND " +
                    " Curso_Código = '" + noticia.Curso_Codigo + "' AND " +
                    " Sem_Periodo = '" + noticia.Sem_Periodo + "' AND " +
                    "Sem_Año = '" + noticia.Sem_Anno + "';";
            command = new OdbcCommand(queryString, connection);
            connection.Open();
            OdbcDataReader reader = command.ExecuteReader();
            ArrayList carnetEstudiantesCurso = new ArrayList();

            while (reader.Read())
            {
                carnetEstudiantesCurso.Add(reader.GetString(0));
            }
            foreach (ModelEstudianteCarnet estudiante in correos)
            {
                if (carnetEstudiantesCurso.Contains(estudiante.carnet))
                {
                    emailService.email_noticia(estudiante.correo, noticia.Titulo, noticia.Mensaje, noticia.Curso_Codigo);
                }
                connection.Close();
            }

            return "OK";
        }

        // ----------------------------- EVALUACIÓN -----------------------------

        /// <summary>
        /// Método para obtener las evaluaciones de un tipo de todos los estudiantes
        /// </summary>
        /// <param name="rubro_nombre">Nombre del rubro al que corresponde la evaluación</param>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre</param>
        /// <param name="sem_anno">Año del semestre</param>
        /// <param name="est_curso_grupo">Grupo del curso</param>
        /// <param name="est_curso_codigo">Código del curso</param>
        /// <param name="est_sem_periodo">Periodo del semestre</param>
        /// <param name="est_sem_anno">Año del semestre</param>
        /// <param name="nombre">Nombre (tipo) de la evaluación</param>
        /// <returns></returns>
        public ArrayList GetEvaluacionesProfesor(String rubro_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String est_curso_grupo, String est_curso_codigo, char est_sem_periodo, String est_sem_anno, String nombre)
        {
            rubro_nombre = Uri.UnescapeDataString(rubro_nombre);
            nombre = Uri.UnescapeDataString(nombre);
            ArrayList evaluaciones = new ArrayList();
            String queryString = "SELECT Rubro_Nombre,Curso_Grupo,Curso_Código,Sem_Periodo,Sem_Año,Ent_ID,Est_Carnet,Est_Curso_Grupo,Est_Curso_Código,Est_Sem_Periodo,Est_Sem_Año,Nombre,Peso,Fecha_Entrega,Observaciones,Forma_Evaluación,Nota,Retroalimentación,Estado " +
                "FROM EVALUACIÓN WHERE Rubro_Nombre = '" + rubro_nombre + "' AND" +
                " Curso_Grupo = '" + curso_grupo + "' AND" +
                " Curso_Código = '" + curso_codigo + "' AND" +
                "Sem_Periodo = " + sem_periodo + " AND" +
                " Sem_Año = " + sem_anno + " AND" +
                " Est_Curso_Grupo = '" + est_curso_grupo + "' AND" +
                " Est_Curso_Código = '" + est_curso_codigo + "' AND" +
                " Est_Sem_Periodo = " + est_sem_periodo + " AND" +
                " Est_Sem_Año = " + est_sem_anno + " AND" +
                " Nombre = '" + nombre + "';";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                EVALUACION evaluacion = new EVALUACION(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetChar(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetChar(9), reader.GetString(10), reader.GetString(11), reader.GetFloat(12), reader.GetString(13), reader.GetString(14), reader.GetInt32(15), reader.GetFloat(16), reader.GetString(17), reader.GetString(18));
                evaluaciones.Add(evaluacion);
            }
            connection.Close();
            return evaluaciones;
        }

        /// <summary>
        /// Método para obtener los tipos de evaluaciones existentes en un rubro dado
        /// </summary>
        /// <param name="rubro_nombre">Nombre del rubro</param>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre</param>
        /// <param name="sem_anno">Año del semestre</param>
        /// <returns></returns>
        public ArrayList GetTiposEvaluacion(String rubro_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            rubro_nombre = Uri.UnescapeDataString(rubro_nombre);
            ArrayList evaluaciones = new ArrayList();
            String queryString = "SELECT 0,'0','0','0000',0,3000,Nombre,0,'0000','000',0,0,'N/A','N/A'" +
                "FROM EVALUACIÓN WHERE Rubro_Nombre = '" + rubro_nombre + "' AND " +
                "Curso_Grupo = '" + curso_grupo + "' AND" +
                " Curso_Código = '" + curso_codigo + "' AND" +
                " Sem_Periodo = " + sem_periodo + " AND" +
                " Sem_Año = " + sem_anno + " GROUP BY Nombre;";

            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                EVALUACION evaluacion = new EVALUACION(rubro_nombre, curso_grupo, curso_codigo, sem_periodo, sem_anno, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetChar(4), reader.GetString(5), reader.GetString(6), reader.GetFloat(7), reader.GetString(8), reader.GetString(9), reader.GetInt32(10), reader.GetFloat(11), reader.GetString(12), reader.GetString(13));
                evaluaciones.Add(evaluacion);
            }
            connection.Close();
            return evaluaciones;
        }

        /// <summary>
        /// Método para obtener todas las evaluaciones de un estudiante
        /// </summary>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre</param>
        /// <param name="sem_anno">Año del semestre</param>
        /// <param name="est_carnet">Carnet del estudiante</param>
        /// <param name="est_curso_grupo">Grupo del curso</param>
        /// <param name="est_curso_codigo">Código del curso</param>
        /// <param name="est_sem_periodo">Periodo del semestre</param>
        /// <param name="est_sem_anno">Año del semestre</param>
        /// <returns>Lista de evaluaciones de un estudiante</returns>
        public ArrayList GetEvaluacionesEstudiante(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String est_carnet, String est_curso_grupo, String est_curso_codigo, char est_sem_periodo, String est_sem_anno)
        {
            ArrayList evaluaciones = new ArrayList();
            String queryString = "SELECT Rubro_Nombre, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año, Est_Carnet, Est_Curso_Grupo, Est_Curso_Código, Est_Sem_Periodo, Est_Sem_Año, " +
                "                        Nombre, Peso, Fecha_Entrega, Observaciones , Forma_Evaluación, Nota, Estado" +
                                 " FROM EVALUACIÓN WHERE " +
                                 " Curso_Grupo = '" + curso_grupo + "' AND " +
                                 " Curso_Código = '" + curso_codigo + "' AND " +
                                 " Sem_Periodo = " + sem_periodo + " AND " +
                                 " Sem_Año = " + sem_anno + " AND " +
                                 " Est_Carnet = '" + est_carnet + "' AND " +
                                 " Est_Curso_Grupo = '" + est_curso_grupo + "' AND " +
                                 " Est_Curso_Código = '" + est_curso_codigo + "' AND " +
                                 " Est_Sem_Periodo = " + est_sem_periodo + " AND " +
                                 " Est_Sem_Año = " + est_sem_anno + " ;";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            String estado;
            while (reader.Read())
            {
                estado = reader.GetString(18);
                if (estado == "Visible")
                {
                    reader.GetString(13);
                    EVALUACION evaluacion = new EVALUACION(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetChar(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetChar(9), reader.GetString(10), reader.GetString(11), reader.GetFloat(12), reader.GetString(13), reader.GetString(14), reader.GetInt32(15), Convert.ToSingle(reader.GetDouble(16)), reader.GetString(17), estado);
                    evaluaciones.Add(evaluacion);

                }
                else
                {
                    EVALUACION evaluacion = new EVALUACION(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetChar(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetChar(9), reader.GetString(10), reader.GetString(11), reader.GetFloat(12), reader.GetString(13), reader.GetString(14), reader.GetInt32(15), 0, reader.GetString(17), estado);
                    evaluaciones.Add(evaluacion);
                }
            }
            connection.Close();
            return evaluaciones;
        }

        /// <summary>
        /// Método para crear una evaluación
        /// </summary>
        /// <param name="evaluacion">Evalaución por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        public String CreateEvaluacion(EVALUACION evaluacion)
        {
            if (evaluacion.Forma_Evaluacion == 1)
            {
                String queryString = "EXEC SP_EVALUACIÓN_IND_INS @Rubro_Nombre = '" + evaluacion.Rubro_Nombre + "', @Curso_Grupo = '" + evaluacion.Curso_Grupo + "', @Curso_Código = '" + evaluacion.Curso_Codigo + "', @Sem_Periodo = " + evaluacion.Sem_Periodo + ", @Sem_Año = " + evaluacion.Sem_Anno + ", @Est_Carnet = '" + evaluacion.Est_Carnet + "', @Est_Curso_Grupo = " + evaluacion.Est_Curso_Grupo + ", @Est_Curso_Código = '" + evaluacion.Est_Curso_Codigo + "', @Est_Sem_Periodo = " + evaluacion.Est_Sem_Periodo + ", @Est_Sem_Año = " + evaluacion.Est_Sem_Anno + ", @Nombre = '" + evaluacion.Nombre + "', @Peso = " + evaluacion.Peso + ", @Fecha_Entrega = '" + evaluacion.Fecha_Entrega + "', @Observaciones = '" + evaluacion.Observaciones + "', @Forma_Evaluación = " + evaluacion.Forma_Evaluacion + ", @Nota = " + evaluacion.Nota + ", @Retroalimentación = " + evaluacion.Retroalimentacion + ", @Estado = 'Sin calificar';";
                connection.Open();
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return "OK";
            }
            else
            {
                String queryString = "EXEC SP_EVALUACIÓN_GR_INS @Rubro_Nombre = '" + evaluacion.Rubro_Nombre + "', @Curso_Grupo = '" + evaluacion.Curso_Grupo + "', @Curso_Código = '" + evaluacion.Curso_Codigo + "', @Sem_Periodo = " + evaluacion.Sem_Periodo + ", @Sem_Año = " + evaluacion.Sem_Anno + ", @Est_Carnet = '" + evaluacion.Est_Carnet + "', @Est_Curso_Grupo = " + evaluacion.Est_Curso_Grupo + ", @Est_Curso_Código = '" + evaluacion.Est_Curso_Codigo + "', @Est_Sem_Periodo = " + evaluacion.Est_Sem_Periodo + ", @Est_Sem_Año = " + evaluacion.Est_Sem_Anno + ", @Nombre = '" + evaluacion.Nombre + "', @Peso = " + evaluacion.Peso + ", @Fecha_Entrega = '" + evaluacion.Fecha_Entrega + "', @Observaciones = '" + evaluacion.Observaciones + "', @Forma_Evaluación = " + evaluacion.Forma_Evaluacion + ", @Nota = " + evaluacion.Nota + ", @Retroalimentación = " + evaluacion.Retroalimentacion + ", @Estado = 'Sin calificar'; ";
                connection.Open();
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return "OK";
            }
        }

        /// <summary>
        /// Método para actualizar una evaluación
        /// </summary>
        /// <param name="evaluacion">Evaluación por actualizar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        public String UpdateEvaluacion(EVALUACION evaluacion)
        {
            String queryString = "SELECT Rubro_Nombre,Curso_Grupo,Curso_Código,Sem_Periodo,Sem_Año,Ent_ID,Est_Carnet,Est_Curso_Grupo,Est_Curso_Código,Est_Sem_Periodo,Est_Sem_Año,Nombre,Peso,Fecha_Entrega,Observaciones,Forma_Evaluación,Nota,Retroalimentación,Estado " +
                "FROM EVALUACIÓN WHERE Rubro_Nombre = '" + evaluacion.Rubro_Nombre + "' AND" +
                " Curso_Grupo = '" + evaluacion.Curso_Grupo + "' AND" +
                " Curso_Código = '" + evaluacion.Curso_Codigo + "' AND" +
                " Sem_Periodo = " + evaluacion.Sem_Periodo + " AND" +
                " Sem_Año = " + evaluacion.Sem_Anno + " AND" +
                " Est_Carnet = '" + evaluacion.Est_Carnet + "' AND" +
                " Est_Curso_Grupo = '" + evaluacion.Est_Curso_Grupo + "' AND" +
                " Est_Curso_Código = '" + evaluacion.Est_Curso_Codigo + "' AND" +
                " Est_Sem_Periodo = " + evaluacion.Est_Sem_Periodo + " AND" +
                " Est_Sem_Año = " + evaluacion.Est_Sem_Anno + " AND" +
                " Nombre = '" + evaluacion.Nombre + "';";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "UPDATE EVALUACIÓN SET Peso = " + evaluacion.Peso + ", Fecha_Entrega = '" + evaluacion.Fecha_Entrega + "', Observaciones = '" + evaluacion.Observaciones + "', Forma_Evaluación = " + evaluacion.Forma_Evaluacion + ", Nota = " + evaluacion.Nota + ", Retroalimentación = '" + evaluacion.Retroalimentacion + "', Estado = '" + evaluacion.Estado + " WHERE Rubro_Nombre = '" + evaluacion.Rubro_Nombre + "' AND" +
                " Curso_Grupo = '" + evaluacion.Curso_Grupo + "' AND" +
                " Curso_Código = '" + evaluacion.Curso_Codigo + "' AND" +
                " Sem_Periodo = " + evaluacion.Sem_Periodo + " AND" +
                " Sem_Año = " + evaluacion.Sem_Anno + " AND" +
                " Est_Carnet = '" + evaluacion.Est_Carnet + "' AND" +
                " Est_Curso_Grupo = '" + evaluacion.Est_Curso_Grupo + "' AND" +
                " Est_Curso_Código = '" + evaluacion.Est_Curso_Codigo + "' AND" +
                " Est_Sem_Periodo = " + evaluacion.Est_Sem_Periodo + " AND" +
                " Est_Sem_Año = " + evaluacion.Est_Sem_Anno + " AND" +
                " Nombre = '" + evaluacion.Nombre + "';";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                if (evaluacion.Estado.Equals("Visible"))
                {
                    connection.Open();
                    IEnumerable correos = comsMongo.GetCorreos();
                    queryString = "SELECT Carnet FROM ESTUDIANTE_MATRICULADO WHERE " +
                        " Curso_Grupo = '" + evaluacion.Curso_Grupo + "' AND " +
                        " Curso_Código = '" + evaluacion.Curso_Codigo + "' AND " +
                        " Sem_Periodo = '" + evaluacion.Sem_Periodo + "' AND " +
                        "Sem_Año = '" + evaluacion.Sem_Anno + "';";

                    command = new OdbcCommand(queryString, connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    ArrayList carnetEstudiantesCurso = new ArrayList();

                    while (reader.Read())
                    {
                        carnetEstudiantesCurso.Add(reader.GetString(0));
                    }
                    foreach (ModelEstudianteCarnet estudiante in correos)
                    {
                        if (carnetEstudiantesCurso.Contains(estudiante.carnet))
                        {
                            emailService.email_entrega(estudiante.correo, evaluacion.Nombre, evaluacion.Curso_Codigo);
                        }
                    }

                    connection.Close();
                }
                return "200";
            }
            connection.Close();
            return "404";
        }

        /// <summary>
        /// Método para eliminar una evaluación
        /// </summary>
        /// <param name="evaluacion">Evaluación por eliminar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        public String DeleteEvaluacion(EVALUACION evaluacion)
        {
            String queryString = "SELECT Nombre, Porcentaje, Curso_Grupo, Curso_Código, Sem_Periodo, Sem_Año " +
                "FROM RUBRO WHERE Rubro_Nombre = '" + evaluacion.Rubro_Nombre + "' AND" +
                " Curso_Grupo = '" + evaluacion.Curso_Grupo + "' AND " +
                " Curso_Código = '" + evaluacion.Curso_Codigo + "' AND " +
                " Sem_Periodo = " + evaluacion.Sem_Periodo + " AND " +
                " Sem_Año = " + evaluacion.Sem_Anno + " AND" +
                " Est_Carnet = '" + evaluacion.Est_Carnet + "' AND " +
                " Est_Curso_Grupo = '" + evaluacion.Est_Curso_Grupo + "' AND" +
                " Est_Curso_Código = '" + evaluacion.Est_Curso_Codigo + "' AND " +
                " Est_Sem_Periodo = " + evaluacion.Est_Sem_Periodo + " AND " +
                " Est_Sem_Año = " + evaluacion.Est_Sem_Anno + " AND " +
                " Nombre = '" + evaluacion.Nombre + "';";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "DELETE FROM EVALUACIÓN WHERE Rubro_Nombre = '" + evaluacion.Rubro_Nombre + "' AND" +
                " Curso_Grupo = '" + evaluacion.Curso_Grupo + "' AND " +
                " Curso_Código = '" + evaluacion.Curso_Codigo + "' AND " +
                " Sem_Periodo = " + evaluacion.Sem_Periodo + " AND " +
                " Sem_Año = " + evaluacion.Sem_Anno + " AND" +
                " Est_Carnet = '" + evaluacion.Est_Carnet + "' AND " +
                " Est_Curso_Grupo = '" + evaluacion.Est_Curso_Grupo + "' AND" +
                " Est_Curso_Código = '" + evaluacion.Est_Curso_Codigo + "' AND " +
                " Est_Sem_Periodo = " + evaluacion.Est_Sem_Periodo + " AND " +
                " Est_Sem_Año = " + evaluacion.Est_Sem_Anno + " AND " +
                " Nombre = '" + evaluacion.Nombre + "';";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }

        // ----------------------------- EXPEDIENTE -----------------------------
        public ArrayList GetExpediente(String carnet)
        {
            ArrayList cursos = new ArrayList();
            String queryString = "EXEC SP_EVALUACIÓN_EXP_EST @Carnet = '" + carnet + "';";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                EXPEDIENTE expediente = new EXPEDIENTE(reader.GetFloat(0), reader.GetString(1), reader.GetString(2), reader.GetChar(3), reader.GetString(4), reader.GetString(5));
                cursos.Add(expediente);
            }
            connection.Close();
            return cursos;
        }

        // ----------------------------- Cursos -----------------------------
        public ArrayList GetCursos()
        {
            ArrayList cursos = new ArrayList();
            String queryString = "SELECT Código FROM CURSO;";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cursos.Add(reader.GetString(0));
            }
            connection.Close();
            return cursos;
        }

        public void CreateCurso(CURSO value)
        {
            String queryString = "INSERT INTO CURSO (Código, Nombre, Créditos, Carrera_ID) VALUES ('" + value.Codigo + "','" + value.Nombre + "', ' " + value.Creditos + "' ," + value.Carrera_ID + ");";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }

        public String DeleteCurso(CURSO curso)
        {
            String queryString = "SELECT Código, Nombre, Créditos, Carrera_ID FROM CURSO WHERE Código = '" + curso.Codigo + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "DELETE FROM CURSO WHERE Código = '" + curso.Codigo + ";";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }

        //Carreras

        public ArrayList GetCarreras()
        {
            ArrayList carreras = new ArrayList();
            String queryString = "SELECT ID ,Nombre FROM CARRERA;";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CARRERA expediente = new CARRERA(int.Parse(reader.GetString(0)), reader.GetString(1));
                carreras.Add(expediente);
            }
            connection.Close();

            return carreras;
        }

        public String CreateCarrera(String nombreCarrera)
        {
            String queryString = "INSERT INTO CARRERA (Nombre) VALUES ( '" + nombreCarrera + "' );";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return "OK";
        }

        public String DeleteCarrera(CARRERA carrera)
        {
            String queryString = "SELECT ID, Nombre FROM CARRERA WHERE ID = '" + carrera.ID + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "DELETE FROM CARRERA WHERE ID = '" + carrera.ID + ";";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            return "404";
        }

        // ----------------------------- ENTREGABLE -----------------------------

        public ENTREGABLE GetEntregable(int id)
        {
            String queryString = "SELECT ID, Data FROM ENTREGABLE WHERE ID = " + id + ";";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            ENTREGABLE entregable = new ENTREGABLE(reader.GetInt32(0), reader.GetString(1));
            connection.Close();
            return entregable;
        }
        public String UpdateEntregable(ENTREGABLE entregable)
        {
            String queryString = "SELECT ID, Data FROM ENTREGABLE WHERE ID = '" + entregable.ID + "';";
            connection.Open();
            OdbcCommand command = new OdbcCommand(queryString, connection);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String queryString1 = "UPDATE ENTREGABLE SET Data = '" + entregable.Data + "';";
                OdbcCommand command1 = new OdbcCommand(queryString1, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return "200";
            }
            connection.Close();
            //email_entrega();
            return "404";
        }

        //Excel
        /// <summary>
        /// Método para cargar los datos desde un excel a una tabla temporal usando bulk insert
        /// </summary>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        private string loadCsvToTable(string delimeter)
        {
            string sqlQuery = "BULK INSERT " + tempTableName;
            sqlQuery += " FROM '" + urlExcel + tempTableName + ".csv'";
            sqlQuery += " WITH ( FIELDTERMINATOR = '" + delimeter + "', ROWTERMINATOR = '\n' )";

            try
            {
                OdbcConnection database = connection;
                database.Open();
                OdbcCommand command = new OdbcCommand(sqlQuery, database);
                command.ExecuteNonQuery();
                return "filldone";
            }
            catch (OdbcException e)
            {
                return e.Message;
            }

        }

        /// <summary>
        /// Método para ejecutar el método que llena la tabla temporar y llamar al sp para cargar los datos en las tablas de sql que corresponden
        /// </summary>
        /// <returns></returns>
        public string processFile()
        {
            String load = loadCsvToTable(",");
            if (!load.Equals("filldone"))
            {
                return "fillnotdone" + load;
            }
            try
            {
                String queryString = "EXEC dbo.store_excel;";
                OdbcConnection database = connection;
                OdbcCommand command = new OdbcCommand(queryString, database);
                command.ExecuteNonQuery();
                return "Proccess Done";
            }
            catch (OdbcException e)
            {
                return "error exc, " + e.Message;
            }

        }


    }
}