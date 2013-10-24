namespace LFM.Submissions.Contract.LandRegistry
{
    public class EarlyCompletionReceived
    {
        public string ApplicationMessageId { get; set; }
        public string ExternalReference { get; set; }
        public string ABR{ get; set; }
        public string Filename{ get; set; }
        public string FileFormat{ get; set; }
        public byte[] DespatchDocument { get; set; }
        public string XmlRegisterData { get; set; }

        public string Username { get; set; }
    }
}
