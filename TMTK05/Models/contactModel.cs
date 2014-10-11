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
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public bool Done { get; private set; }

        public void SendMail()
        {
            Classes.Message.SendMail(Name, Email, Subject, Message);
            Done = true;
        }
    }
}