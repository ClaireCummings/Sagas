using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFM.Submissions.InternalMessages.LandRegistry.Messages
{
    public interface IEdrsAttachmentResponseReceived
    {
        string AttachmentId { get; set; }
        string ApplicationId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
