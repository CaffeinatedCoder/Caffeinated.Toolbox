namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    public static class Visitor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unregisteredSubtypeHandling"></param>
        /// <returns></returns>
        public static IActionVisitor<T> For<T>(
            UnregisteredSubtypeHandling unregisteredSubtypeHandling = UnregisteredSubtypeHandling.Ignore)
            where T : class
        {
            return new ActionVisitor<T>(unregisteredSubtypeHandling);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="unregisteredSubtypeHandling"></param>
        /// <returns></returns>
        public static IFuncVisitor<T, TResult> For<T, TResult>(
            UnregisteredSubtypeHandling unregisteredSubtypeHandling = UnregisteredSubtypeHandling.Ignore) 
            where T : class
        {
            return new FuncVisitor<T, TResult>(unregisteredSubtypeHandling);
        }
    }
}