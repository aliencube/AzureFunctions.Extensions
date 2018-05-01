using System.Diagnostics;

using Microsoft.Azure.WebJobs.Host;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the fake trace writer entity.
    /// </summary>
    public class FakeTraceWriter : TraceWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeTraceWriter"/> class.
        /// </summary>
        public FakeTraceWriter() : base(TraceLevel.Verbose)
        {
        }

        /// <inheritdoc />
        public override void Trace(TraceEvent traceEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}