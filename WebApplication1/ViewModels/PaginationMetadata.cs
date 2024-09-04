namespace WebApplication1.ViewModels
{
    public class PaginationMetadata
    {
        public int TotalItems { get; private set; }
        public int TotalPages { get; }
        public int PageSize { get; private set; }
        public int CurrentPageNumber { get; private set; }

        public PaginationMetadata(
            int totalItems,
            int pageSize,
            int currentPageNumber
            )
        {
            TotalItems = totalItems;
            PageSize = pageSize;
            CurrentPageNumber = currentPageNumber;
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
        }
    }
}
