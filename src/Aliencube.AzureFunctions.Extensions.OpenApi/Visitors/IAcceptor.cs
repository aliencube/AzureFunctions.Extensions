using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This provides interfaces to the acceptor classes.
    /// </summary>
    public interface IAcceptor
    {
        /// <summary>
        /// Accepts the visitors.
        /// </summary>
        /// <param name="collection"><see cref="VisitorCollection"/> instance.</param>
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance.</param>
        void Accept(VisitorCollection collection, NamingStrategy namingStrategy);
    }
}
