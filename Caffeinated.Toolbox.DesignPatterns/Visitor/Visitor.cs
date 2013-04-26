namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;

    public static class Visitor
    {
        /// <summary>
        ///     Creates a visitor instance.
        /// </summary>
        /// <typeparam name="T">Base type of class hierarchy to visit.</typeparam>
        /// <param name="unregisteredSubtypeHandling">Action to apply on visiting unregistered subtypes.</param>
        /// <returns></returns>
        public static IActionVisitor<T> For<T>(
            UnregisteredSubtypeHandling unregisteredSubtypeHandling = UnregisteredSubtypeHandling.Ignore)
            where T : class
        {
            return new ActionVisitor<T>(unregisteredSubtypeHandling);
        }

        /// <summary>
        ///     Creates a visitor instance.
        /// </summary>
        /// <typeparam name="T">Base type of class hierarchy to visit.</typeparam>
        /// <param name="fallbackAction">Action to perform when an unregistered subtype gets visited.</param>
        /// <returns></returns>
        public static IActionVisitor<T> For<T>(Action<T> fallbackAction) where T : class
        {
            return new ActionVisitor<T>(fallbackAction);
        }

        /// <summary>
        ///     Creates a visitor instance that is capable of returning a result.
        /// </summary>
        /// <typeparam name="T">Base type of class hierarchy to visit.</typeparam>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="unregisteredSubtypeHandling">Action to apply on visiting unregistered subtypes.</param>
        /// <returns></returns>
        public static IFuncVisitor<T, TResult> For<T, TResult>(
            UnregisteredSubtypeHandling unregisteredSubtypeHandling = UnregisteredSubtypeHandling.Ignore)
            where T : class
        {
            return new FuncVisitor<T, TResult>(unregisteredSubtypeHandling);
        }

        /// <summary>
        ///     Creates a visitor instance that is capable of returning a result.
        /// </summary>
        /// <typeparam name="T">Base type of class hierarchy to visit.</typeparam>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="fallbackAction">Action to perform when an unregistered subtype gets visited.</param>
        /// <returns></returns>
        public static IFuncVisitor<T, TResult> For<T, TResult>(Func<T, TResult> fallbackAction) where T : class
        {
            return new FuncVisitor<T, TResult>(fallbackAction);
        }
    }
}