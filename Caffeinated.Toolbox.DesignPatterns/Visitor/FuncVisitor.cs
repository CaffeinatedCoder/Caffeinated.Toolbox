namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;
    using System.Collections.Generic;

    internal sealed class FuncVisitor<TBase, TResult> : IFuncVisitor<TBase, TResult> where TBase : class
    {
        private readonly Dictionary<Type, dynamic> repository = new Dictionary<Type, dynamic>();
        private readonly UnregisteredSubtypeHandling unregisteredSubtypeHandling;

        public FuncVisitor(UnregisteredSubtypeHandling unregisteredSubtypeHandling)
        {
            this.unregisteredSubtypeHandling = unregisteredSubtypeHandling;
        }

        public void Register<T>(Func<T, TResult> action) where T : TBase
        {
            this.repository[typeof (T)] = action;
        }

        public TResult Visit(TBase value)
        {
            dynamic action;
            if (this.repository.TryGetValue(value.GetType(), out action))
            {
                return action((dynamic) value);
            }

            if (this.unregisteredSubtypeHandling == UnregisteredSubtypeHandling.Ignore)
            {
                return default(TResult);
            }

            throw new UnhandledSubtypeException(string.Format("No suitable visitor method could be resolved for {0}.",
                                                              value.GetType().FullName));
        }
    }
}