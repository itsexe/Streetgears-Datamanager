namespace SGunpacker
{
    public class Sgfile
    {
        public string Name { get; }
        private string Hash { get; set; }
        public int Size { get; }
        public string Dataid { get; }
        public int Offset { get; }
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
