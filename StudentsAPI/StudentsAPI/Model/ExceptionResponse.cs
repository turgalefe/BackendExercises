using System.Text.Json.Serialization;

namespace StudentsAPI.Model
{
    public class ExceptionResponse
    {
        public int StatusCode { get;}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get;}
        public ExceptionResponse(string message, int statusCode = 500)
        {
            StatusCode = statusCode;
            Message = message; 
        }
    }
}
