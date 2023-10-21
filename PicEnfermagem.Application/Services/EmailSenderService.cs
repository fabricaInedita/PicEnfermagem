using Microsoft.Extensions.Options;
using PicEnfermagem.Application.Interfaces;
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
        var client = new SmtpClient("smtp.office365.com", 587)
        {
            Credentials = new System.Net.NetworkCredential("fabricadesoftware@baraodemaua.edu.br", "=Eup@5b+"),
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
        using (var mm = new MailMessage("fabricadesoftware@baraodemaua.edu.br", listaDestinatarios[0]))
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
        <div style='display: table-cell; vertical-align: middle; text-align: center;'>
            <img src='https://i.imgur.com/X6Ivy4Z.png' style='max-width: 270px; width: 100%; margin: 0 auto 20px;' />
            <p style='color: #fff;'>Olá, {nome}<br> Bem vindo a QUIZ ENFERMGEM. Por favor confirme seu email no botão abaixo
            :<br> <a href='{token}' style=""
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