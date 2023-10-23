namespace PicEnfermagem.Application.Interfaces;

public interface IEmailSenderService
{
    void SendEmail(string nome, string destinatarios, string token);
    void SendResetPasswordEmail(string nome, string destinatarios, string token);
    void SendCertificateEmail(string nome, string destinatarios);
}