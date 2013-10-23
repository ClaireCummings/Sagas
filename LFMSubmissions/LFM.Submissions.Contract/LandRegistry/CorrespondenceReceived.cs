namespace LFM.Submissions.Contract.LandRegistry
{
    public class CorrespondenceReceived
    {
        public string ApplicationMessageId { get; set; }
        public string ExternalReference { get; set; }
        public string ABR { get; set; }
        public string Filename { get; set; }
        public string FileFormat { get; set; }
        public byte[] FileData { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string CorrespondenceId { get; set; }
    }
}
