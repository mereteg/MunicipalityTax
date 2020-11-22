namespace BusinessLogic.Contract
{
    public interface IRestServiceRequest
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }

    }
}
