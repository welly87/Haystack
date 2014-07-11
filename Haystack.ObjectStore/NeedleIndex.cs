namespace Haystack.ObjectStore
{
    public class NeedleIndex
    {
        /// <summary>
        /// 64 bit object key
        /// </summary>
        public long Key { get; set; }

        public int AlternateKey { get; set; }

        public int Flags { get; set; }

        public int Offset { get; set; }

        public int DataSize { get; set; }
    }
}
