namespace Lvc.BackendPatterns.Core.QueriesDetails
{
    public class PageDetails
    {
        public int Number { get; set; }

        public int Size { get; set; }

        public int Skip =>
            Number * Size;
    }
}
