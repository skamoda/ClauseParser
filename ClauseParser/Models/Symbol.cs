namespace ClauseParser.Models
{
    public class Symbol
    {
        public Symbol Parent { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public int ChildrenCount { get; set; }

        public Symbol()
        {
            Parent = null;
            Name = null;
            Priority = 0;
            ChildrenCount = 0;
        }
    }
}
