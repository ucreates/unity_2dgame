namespace Service.Integration
{
    public partial class CommunicationRequest
    {
        public class CommunicationBinaryFileRequest
        {
            public string fieldName => "fileUpload";
            public byte[] data { get; set; }
            public string fileName { get; set; }
            public string mimeType { get; set; }
        }
    }
}