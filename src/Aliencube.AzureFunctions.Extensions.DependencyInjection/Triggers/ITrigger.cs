using System;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions
{
    /// <summary>
    /// This provides interfaces to triggers.
    /// </summary>
    /// <typeparam name="TLogger">Type of logger.</typeparam>
    public interface ITrigger<TLogger>
    {
        /// <summary>
        /// Gets a result from the trigger invocation. This is particularly useful, if a trigger is <c>void</c> or returns <see cref="Type"/>.
        /// </summary>
        object ResultInvoked { get; }
    }
}
