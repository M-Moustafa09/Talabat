namespace Store.APIs.Helper
{
	public class Pagination<T>
	{
        public int PageSize { get; set; }
		public int PageIndex { get; set; }
		public int Count { get; set; }

		public IReadOnlyList<T> Data { get; set; }


        public Pagination(int pageindex, int pagesize, IReadOnlyList<T> data,int count)
        {
            PageSize = pagesize;
            PageIndex = pageindex;
            Count = count;
            Data = data;
        }
    }
}
