using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpeechPathology.Types
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public string ShortKey { get; private set; }

        public Grouping(K key, string shortKey, IEnumerable<T> items)
        {
            Key = key;
            ShortKey = shortKey;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
