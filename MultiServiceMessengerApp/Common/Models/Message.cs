using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    // WCF can serialize this class on its own without the [DataContract] and [DataMember]
    // annotations because it only has types that are already compatible with XML.
    // But if we added a List<T> for example, we would need to add the annotations.
    public class Message
    {
        public int Id { get; set; }

        /*
        *  In Web API 2, if a [Required] field is passed as null/empty/whitespace
        *  to a controller, an HTTP response that contains the validation errors
        *  is returned, and the controller action is not invoked.
        *
        *  Note: You have to create and add a filter to your api config to apply the validation.
        */

        [Required(ErrorMessage = "The message can not be empty")]
        [StringLength(20, ErrorMessage = "Message is too long; 20 characters max.")]
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}\nText: {1}", Id, Text);
        }
    }
}
