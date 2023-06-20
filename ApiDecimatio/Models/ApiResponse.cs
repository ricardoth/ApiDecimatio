using Decimatio.Domain.CustomEntities;
using Grpc.Core;

namespace Decimatio.WebApi.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public MetaData Meta { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }

    }
}
