namespace SGunpacker
{
    public class Sgfile
    {
        public readonly string Name;
        private string Hash { get; set; }
        public readonly int Size;
        public readonly string Dataid;
        public readonly int Offset;
        public Sgfile(string name, string hash, int size, string dataid, int offset)
        {
            Name = name;
            Hash = hash;
            Size = size;
            Dataid = dataid;
            Offset = offset;
         }
    }
}
