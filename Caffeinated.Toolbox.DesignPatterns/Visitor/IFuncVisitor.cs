namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IFuncVisitor<in TBase, TResult> where TBase : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Register<T>(Func<T, TResult> action) where T : TBase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        TResult Visit(TBase value);
    }
}