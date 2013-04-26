namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    public interface IActionVisitor<in TBase> where TBase : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Register<T>(Action<T> action) where T : TBase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void Visit(TBase value);
    }
}