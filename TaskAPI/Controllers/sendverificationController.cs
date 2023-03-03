
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using TaskAPI.DTOs;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sendverificationController : ControllerBase
    {
       
        [HttpPost]
        public  IActionResult sendemail(emaildataDTO dto)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("aya483263@gmail.com"));
                email.To.Add(MailboxAddress.Parse(dto.mailto));
                email.Subject = dto.subject;
                email.Body = new TextPart(TextFormat.Text) { Text = dto.code };
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("aya483263@gmail.com", "rdytcgubsaiwwulg");
                smtp.Send(email);
                smtp.Disconnect(true);

            }
            catch
            {
                return BadRequest();
            }


            return Ok();
        }
        
  
    }
}
