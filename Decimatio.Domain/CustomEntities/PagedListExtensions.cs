namespace Decimatio.Domain.CustomEntities
{
    public static class PagedListExtensions
    {
        public static MetaData ToMetaData<T>(this PagedList<T> pagedList)
        {
            return new MetaData
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
            };
        }
    }
}
