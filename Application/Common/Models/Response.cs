
namespace Application.Common.Models;

public class CustomResponse {
    public bool Status {get; set;}
    public int StatusCode {get; set;}
    public string Message {get; set;}

    public static CustomResponse Succeeded(string message){
        
        return new CustomResponse() {
            Status = true,
            Message = message,
            StatusCode = 200
        };

    }

    public static CustomResponse Failed(ICollection<string> messages, int code = 500){
        return new CustomResponse(){
            Status = false,
            Message = string.Join(", ", messages),
            StatusCode = code
        };
    }

    public static CustomResponse Failed(string message, int code = 500){
        return new CustomResponse(){
            Status = false,
            Message = message,
            StatusCode = code
        };
    }
}