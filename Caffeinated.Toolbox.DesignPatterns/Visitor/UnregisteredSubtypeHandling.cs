namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    /// <summary>
    /// </summary>
    public enum UnregisteredSubtypeHandling
    {
        /// <summary>
        /// Configured action won't be performed on unregistered subtype.
        /// </summary>
        Ignore,

        /// <summary>
        /// <see cref="UnhandledSubtypeException"/> will be thrown if an unregistered subtype gets visited.
        /// </summary>
        ThrowException
    }
}