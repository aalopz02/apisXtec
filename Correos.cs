using System;
using System.Net.Mail;
using System.Net;

public void send_email(string email, string subject, string body)
{
    var fromAddress = new MailAddress("digitalxtec@gmail.com", "XTEC Digital");
    var toAddress = new MailAddress(email, "Student");
    const string fromPassword = "Hola1234";


    var smtp = new SmtpClient
    {
        Host = "smtp.gmail.com",
        Port = 587,
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    };

    using (var message = new MailMessage(fromAddress, toAddress)
    {
        Subject = subject,
        Body = body
    })
    {
        smtp.Send(message);
    }

}

public void email_nota(string email, int nota, string evaluacion, string curso)
{
    string subject = "Evaluacion Revisada";
    string body = "Usted tiene un " + nota.ToString() + " en " + evaluacion + " - " + curso + ".";
    send_email(email, subject, body);
}
public void email_noticia(string email, string titulo, string noticia, string curso)
{
    string subject = "Noticias -" + titulo;
    string body = "Usted tiene una Noticia del Curso " + curso + ": \n" + noticia + ".";
    send_email(email, subject, body);
}
public void email_entrega(string email, string evaluacion, string curso)
{
    string subject = "Entrega Realizada";
    string body = "Usted ha realizado su entrega de la tarea " + evaluacion + "-" + curso + ".";
    send_email(email, subject, body);
}
public void email_documento(string email, string curso)
{
    string subject = "Nuevo Documento";
    string body = "Usted tiene un nuevo documento en " + curso + ".";
    send_email(email, subject, body);
}
