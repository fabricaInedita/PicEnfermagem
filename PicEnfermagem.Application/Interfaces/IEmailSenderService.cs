namespace PicEnfermagem.Application.Interfaces;

public interface IEmailSenderService
{
    void SendEmail(string nome, string destinatarios, string token);
}