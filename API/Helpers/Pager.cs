namespace API.Helpers;

public class Pager<T> where T : class
{
    public int PageIndex { set; get; }
    public int PageSize { set; get; }
    public int Total { set; get; }
    public IEnumerable<T> Registers { set; get; }

    public Pager(IEnumerable<T> registers, int total, int pageIndex, int pageSizes)
    {
        Registers = registers;
        Total = total;
        PageIndex = pageIndex;
        PageSize = pageSizes;
    }

    public int TotalPages 
    {
        get
        {
            return (int) Math.Ceiling(Total / (double) PageSize);
        }
    }

    public bool HasPreviousPage
    {
        get
        {
            return (PageIndex > 1);
        }
    }

    public bool HasNextPage
    {
        get
        {
            return (PageIndex < TotalPages);
        }
    }
}
