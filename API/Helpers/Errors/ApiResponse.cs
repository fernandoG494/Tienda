namespace API.Helpers.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    private string GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "Peticion incorrecta",
            401 => "Usuario no encontrado",
            404 => "El recurso no existe",
            405 => "Este metodo no esta permitodo en el servidor",
            500 => "Error al procesar el la peticion"
        };
    }
}
