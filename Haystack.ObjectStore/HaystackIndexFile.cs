using System.Collections.Generic;

namespace Haystack.ObjectStore
{
    public class HaystackIndexFile : IHaystackIndexFile
    {
        public IEnumerable<NeedleIndex> AllIndex { get; set; }

        // non blocking
        public void Append(NeedleIndex needleIndex)
        {
            // use actor
            throw new System.NotImplementedException();
        }
    }
}
