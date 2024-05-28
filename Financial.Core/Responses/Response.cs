using System.Text.Json.Serialization;

namespace Financial.Core.Responses
{
    public class Response<TData> where TData : class
    {
        private int _statusCode = Configuration.DefaultStatusCode;

        //Aponto para a serializacao do Json, qual construtor ele vai usar
        [JsonConstructor]
        public Response() => _statusCode = Configuration.DefaultStatusCode;

        public Response(TData? data, int statusCode = Configuration.DefaultStatusCode, string? message = null)
        {
            Data = data;
            _statusCode = statusCode;
            Message = message;
        }

        public string? Message {  get; set; }
        public TData? Data { get; set; }
        [JsonIgnore]
        public bool IsSuccess => _statusCode is >= 200 and <= 299;
    }
}
