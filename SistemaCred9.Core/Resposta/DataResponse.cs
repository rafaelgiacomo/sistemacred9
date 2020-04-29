namespace SistemaCred9.Core.Resposta
{
    public class DataResponse<T> : BaseResponse where T : new()
    {
        public DataResponse()
        {
            Data = new T();
        }

        public DataResponse(bool success)
        {
            Success = success;
            Data = new T();
        }

        public T Data { get; set; }
    }
}
