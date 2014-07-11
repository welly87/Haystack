using System.Collections.Generic;

namespace Haystack.ObjectStore
{
    public interface IHaystackIndexFile
    {
        IEnumerable<NeedleIndex> AllIndex { get; set; }
        void Append(NeedleIndex needleIndex);
    }
}
