using System.Collections.Generic;
using System.Linq;
using Haystack.ObjectStore;

namespace PhotoStore.Server
{
    public class InMemoryIndexStorage
    {
        // create map to store 

        readonly IDictionary<long, InMemoryIndex> _inMemoryIndices = new Dictionary<long, InMemoryIndex>();

        public void SaveAll(IEnumerable<NeedleIndex> allIndex)
        {

            // should separate based on object key, alternate key, cookie etc... 
            var idxGroup = allIndex.GroupBy(x => x.Key, x => x.AlternateKey);
        }

        private InMemoryIndex ConvertToInMemoryIndex(NeedleIndex[] needleIndex)
        {
            var idx = new InMemoryIndex
            {
                PhotoKey = needleIndex[0].Key
            };

            idx.AddAll(needleIndex);

            return idx;
        }


        public void Save(NeedleIndex[] index)
        {
            var inMemoryIdx = ConvertToInMemoryIndex(index);

            _inMemoryIndices.Add(inMemoryIdx.PhotoKey, inMemoryIdx);
        }

        public InMemoryIndex Get(long photoKey)
        {
            return _inMemoryIndices[photoKey];
        }
    }
}
