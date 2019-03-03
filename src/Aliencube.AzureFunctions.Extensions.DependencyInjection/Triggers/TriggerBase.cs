namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions
{
    /// <summary>
    /// This represents the base function entity for other functions to inherit.
    /// </summary>
    /// <typeparam name="TLogger">Type of logger.</typeparam>
    public abstract class TriggerBase<TLogger> : ITrigger<TLogger>
    {
        /// <inheritdoc />
        public virtual object ResultInvoked { get; protected set; }
    }
}
