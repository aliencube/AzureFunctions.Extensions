using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// Converts the <see cref="HttpRequest"/> to the <see cref="MultipartFormDataContent"/> instance.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <returns>Returns the <see cref="MultipartFormDataContent"/> instance.</returns>
        public static async Task<MultipartFormDataContent> ToMultipartFormDataContent(this HttpRequest req)
        {
            if (!req.Form.Files.Any())
            {
                return null;
            }

            var mpfd = new MultipartFormDataContent();
            foreach (var file in req.Form.Files)
            {
                var content = default(ByteArrayContent);
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms).ConfigureAwait(false);
                    content = new ByteArrayContent(ms.ToArray());
                }

                mpfd.Add(content, file.Name, file.FileName);
            }

            return mpfd;
        }

        private static async Task<T> ToFromHeader<T>(this HttpRequest req)
        {
            var serialised = JsonConvert.SerializeObject(req.Headers.ToDictionary(p => p.Key, p => p.Value.ToString()));
            var deserialised = JsonConvert.DeserializeObject<T>(serialised);

            return await Task.FromResult(deserialised).ConfigureAwait(false);
        }

        private static async Task<T> ToFromQuery<T>(this HttpRequest req)
        {
            var serialised = JsonConvert.SerializeObject(req.Query.ToDictionary(p => p.Key, p => p.Value.ToString()));
            var deserialised = JsonConvert.DeserializeObject<T>(serialised);

            return await Task.FromResult(deserialised).ConfigureAwait(false);
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
