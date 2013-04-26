// Inspired by the article "Dynamic Visitor Pattern" from GSerjo on http://www.codeproject.com/Tips/563043/Dynamic-Visitor-Pattern

namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;

    /// <summary>
    ///     Visitor without return result.
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    public interface IActionVisitor<in TBase> where TBase : class
    {
        /// <summary>
        ///     Register action on <see cref="T" />.
        /// </summary>
        /// <typeparam name="T">Concrete type.</typeparam>
        /// <param name="action">Action to perform.</param>
        void Register<T>(Action<T> action) where T : TBase;

        /// <summary>
        ///     Visit type and perform previously registered action.
        /// </summary>
        /// <param name="value">Type to visit.</param>
        /// <exception cref="UnhandledSubtypeException">
        ///     <para>
        ///         No fallback action is provided or <see cref="UnregisteredSubtypeHandling.ThrowException" /> is set.
        ///     </para>
        /// </exception>
        void Visit(TBase value);
    }
}