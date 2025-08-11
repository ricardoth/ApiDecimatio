namespace Decimatio.Domain.QueryFilters
{
    public class UsuarioQueryFilter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? Query { get; set; }
    }
}
