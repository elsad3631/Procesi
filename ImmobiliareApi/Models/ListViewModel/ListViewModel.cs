namespace ImmobiliareApi.Models.ListViewModel
{
    public class ListViewModel<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}
