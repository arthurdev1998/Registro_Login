namespace CrudCadastro.Common.Messages;

public class ServiceResult<T>
{
    public T? Data { get; private set; }
    public bool Sucess { get; set; }
    public string? ErrorMessage { get; set; }

    public ServiceResult(T data)
    {
        Data = data;
        Sucess = true;
    }
}