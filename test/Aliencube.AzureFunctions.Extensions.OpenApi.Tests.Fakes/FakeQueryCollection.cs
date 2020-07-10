using System.Collections;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes
{
    public class FakeQueryCollection : IQueryCollection
    {
        private readonly Dictionary<string, string> _values;

        public FakeQueryCollection()
        {
            this._values = new Dictionary<string, string>();
        }

        public StringValues this[string key]
        {
            get { return this._values[key]; }
            set { this._values[key] = value; }
        }

        public int Count => throw new System.NotImplementedException();

        public ICollection<string> Keys => throw new System.NotImplementedException();

        public bool ContainsKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(string key, out StringValues value)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}