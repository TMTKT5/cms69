#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace TMTK05.Models
{
    public class ContactModel
    {
        public ContactModel()
        {
            Done = false;
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        public bool Done { get; set; }

        public void SendMail()
        {
            Classes.Message.SendMail(Name, Email, Subject, Message);
            Done = true;
        }
    }
}