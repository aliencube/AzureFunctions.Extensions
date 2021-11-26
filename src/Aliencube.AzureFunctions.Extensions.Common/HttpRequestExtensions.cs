using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.Common
{
    /// <summary>
    /// This represents the extension entity for <see cref="HttpRequest"/>.
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Converts the <see cref="HttpRequest"/> to the instance of the given type.
        /// </summary>
        /// <typeparam name="T">Type to convert and return.</typeparam>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="source"><see cref="SourceFrom"/> value. Default value is <see cref="SourceFrom.Body"/>.</param>
        /// <returns>Returns the converted instance.</returns>
        public static async Task<T> To<T>(this HttpRequest req, SourceFrom source = SourceFrom.Body)
        {
            var result = default(T);
            switch (source)
            {
                case SourceFrom.Header:
                    result = await ToFromHeader<T>(req).ConfigureAwait(false);
                    break;

                case SourceFrom.Query:
                    result = await ToFromQuery<T>(req).ConfigureAwait(false);
                    break;

                case SourceFrom.Body:
                    result = await ToFromBody<T>(req).ConfigureAwait(false);
                    break;

                case SourceFrom.None:
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidRequestSource);
            }

            return result;
        }

        private static async Task<T> ToFromHeader<T>(this HttpRequest req)
        {
            if (!typeof(IEnumerable<KeyValuePair<string, StringValues>>).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidType);
            }

            var result = (T)req.Headers;

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        private static async Task<T> ToFromQuery<T>(this HttpRequest req)
        {
            var type = typeof(T);
            if (!typeof(IEnumerable<KeyValuePair<string, StringValues>>).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidType);
            }

            var result = (T)req.Query;

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        private static async Task<T> ToFromBody<T>(this HttpRequest req)
        {
            var result = default(T);
            using (var reader = new StreamReader(req.Body))
            {
                var serialised = await reader.ReadToEndAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<T>(serialised);
            }

            return result;
        }
    }
}
