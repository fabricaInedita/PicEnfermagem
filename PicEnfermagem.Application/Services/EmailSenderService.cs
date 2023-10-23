using Microsoft.Extensions.Options;
using PicEnfermagem.Application.Interfaces;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace PicEnfermagem.Application.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly EmailSenderOptions _options;

    public EmailSenderService(IOptions<EmailSenderOptions> options)
    {
        _options = options.Value;
    }
    private SmtpClient ObterClient()
    {
        var client = new SmtpClient("smtp.titan.email", 587)
        {
            Credentials = new System.Net.NetworkCredential("suporte@studiosconnect.com.br", "Quita123*"),
            EnableSsl = true,
        };
        return client;

    }

    public void SendEmail(string nome, string destinatarios, string token)
    {
        if (string.IsNullOrEmpty(destinatarios) ||
        string.IsNullOrEmpty(nome) ||
        string.IsNullOrEmpty(token))
        {
            throw new ArgumentException("Os parâmetros remetente, destinatário, assunto e corpoo do email são obrigatórios.");
        }
        var listaDestinatarios = destinatarios.Split(",");
        using (var mm = new MailMessage("suporte@studiosconnect.com.br", listaDestinatarios[0]))
        {
            foreach (var emailAtual in listaDestinatarios)
            {
                mm.To.Add(new MailAddress(emailAtual.TrimStart().TrimEnd()));
            }
            mm.Subject = "CONFIRME SEU EMAIL QUIZ ENFERMGEM";
            mm.IsBodyHtml = true;
            mm.Body = $@"
<div style='background: rgb(168, 85, 247);
            background: linear-gradient(90deg, rgba(168, 85, 247, 1) 0%, rgba(147, 51, 234, 1) 35%);
            display: table;
            width: 550px;
            height: 500px;
            margin: auto;'>
    <div style='display: table-row;'>
        <div style='display: table-cell; vertical-align: middle; text-align: center; color: #fff;'>
            <img src='https://i.imgur.com/X6Ivy4Z.png' style='max-width: 270px; width: 100%; margin: 0 auto 20px;' />
            <p style='color: #fff;'>Olá, {nome}<br> Bem vindo a QUIZ ENFERMGEM. Por favor confirme seu email no botão abaixo
            <br><br><br> <a href='{token}' style=""
    background-color: #1F2937;
    border: 1px solid #1F2937;
    border-radius: 3px;
    color: #fff;
    text-decoration: none;
    padding: 5px 10px;"">Confirmar email</a></p>
    
        </div>
    </div>
</div>
";

            mm.SubjectEncoding = Encoding.GetEncoding("UTF-8");
            mm.BodyEncoding = Encoding.GetEncoding("UTF-8");
            using (var client = ObterClient())
            {
                client.Send(mm);
            }
        }
    }
    public void SendResetPasswordEmail(string nome, string destinatarios, string token)
    {
        if (string.IsNullOrEmpty(destinatarios) ||
        string.IsNullOrEmpty(nome) ||
        string.IsNullOrEmpty(token))
        {
            throw new ArgumentException("Os parâmetros remetente, destinatário, assunto e corpoo do email são obrigatórios.");
        }
        var listaDestinatarios = destinatarios.Split(",");
        using (var mm = new MailMessage("suporte@studiosconnect.com.br", listaDestinatarios[0]))
        {
            foreach (var emailAtual in listaDestinatarios)
            {
                mm.To.Add(new MailAddress(emailAtual.TrimStart().TrimEnd()));
            }
            mm.Subject = "ALTERAR SENHA";
            mm.IsBodyHtml = true;
            mm.Body = $@"
<div style='background: rgb(168, 85, 247);
            background: linear-gradient(90deg, rgba(168, 85, 247, 1) 0%, rgba(147, 51, 234, 1) 35%);
            display: table;
            width: 550px;
            height: 500px;
            margin: auto;'>
    <div style='display: table-row;'>
        <div style='display: table-cell; vertical-align: middle; text-align: center; color: #fff;'>
            <img src='https://i.imgur.com/X6Ivy4Z.png' style='max-width: 270px; width: 100%; margin: 0 auto 20px;' />
            <p style='color: #fff;'>Olá, {nome}<br> Foi criado uma solicitação para alteração de senha, click no botão abaixo para seguir com a alteração
            <br><br><br> <a href='{token}' style=""
    background-color: #1F2937;
    border: 1px solid #1F2937;
    border-radius: 3px;
    color: #fff;
    text-decoration: none;
    padding: 5px 10px;"">Alterar senha</a></p>
    
        </div>
    </div>
</div>
";

            mm.SubjectEncoding = Encoding.GetEncoding("UTF-8");
            mm.BodyEncoding = Encoding.GetEncoding("UTF-8");
            using (var client = ObterClient())
            {
                client.Send(mm);
            }
        }
    }
    public void SendCertificateEmail(string nome, string destinatarios)
    {
        if (string.IsNullOrEmpty(destinatarios) ||
        string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Os parâmetros remetente, destinatário, assunto e corpoo do email são obrigatórios.");
        }
        var listaDestinatarios = destinatarios.Split(",");
        DateTime dataAtual = DateTime.Now;

        var diaAtual = dataAtual.Day;
        var anoAtual = dataAtual.Year;

        CultureInfo culturaPtBR = new CultureInfo("pt-BR");
        string nomeDoMes = dataAtual.ToString("MMMM", culturaPtBR);

        using (var mm = new MailMessage("suporte@studiosconnect.com.br", listaDestinatarios[0]))
        {
            foreach (var emailAtual in listaDestinatarios)
            {
                mm.To.Add(new MailAddress(emailAtual.TrimStart().TrimEnd()));
            }
            mm.Subject = "CERTIFICADO";
            mm.IsBodyHtml = true;
            mm.Body = $@"
<div style=""width:600px;
            height:400px;
            background-color: #EEEEEE;
            color: #12011dce;
            font-family: Arial, Helvetica, sans-serif;
            margin: 0 auto;
            box-sizing: border-box;
            padding: 30px;
            position: relative;
            overflow: hidden;"">

    <div style=""width: 65%;
                    margin: 0 auto;
                    text-align: center;"">
        <h1 style=""font-size: 48;
        font-weight: bold;
        text-transform: uppercase;
        margin: 0 0 10px;"">Certificado</h1>

        <img src=""https://iili.io/JKTKb94.png"" alt=""Figura de medalha"" border=""0"" width=""54"" height=""53"">
        <p style=""font-size: 12;
                font-style: italic;"">{diaAtual} de {nomeDoMes}, {anoAtual}</p>
        <hr style=""width: 80%;"">
        <p style=""font-size: 12;
                font-style: italic;
                width: 80%;
                margin: 0 auto;"">Este Certificado comprova que
            <strong style="" display: block;
                        font-size: 16;
                        font-weight: bold;
                        font-style: normal;
                        margin: 10px 0;"">{nome}</strong>
            Concluiu com êxito o Quiz de Enfermagem 
            sobre o conhecimento específico relativo 
            à escala MEWS (Modified Early Warning Score),
        </p>
        <br>
        <img src=""https://i.imgur.com/iblzYr6.png"" alt=""Logo Barão de Mauá"" style='max-width: 189px; width: 100%; margin: 0 auto -48px;' />>
    </div>
        
</div>
";

            mm.SubjectEncoding = Encoding.GetEncoding("UTF-8");
            mm.BodyEncoding = Encoding.GetEncoding("UTF-8");
            using (var client = ObterClient())
            {
                client.Send(mm);
            }
        }
    }
}
public class EmailSenderOptions
{
    public string FromName { get; set; }
    public string FromEmail { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}