namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    internal sealed class UnhandledSubtypeException : Exception
    {
        public UnhandledSubtypeException(string message) : base(message)
        {
        }
    }
}