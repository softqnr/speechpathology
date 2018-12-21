using System.Collections;
using System.Collections.Generic;

namespace SpeechPathology.Infrastructure.Navigation
{
    public class NavigationParameters : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly List<KeyValuePair<string, object>> _entries = new List<KeyValuePair<string, object>>();
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this._entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(string key, object value)
        {
            this._entries.Add(new KeyValuePair<string, object>(key, value));
        }
    }
}
