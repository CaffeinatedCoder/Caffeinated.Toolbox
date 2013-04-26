// Inspired by the article "Dynamic Visitor Pattern" from GSerjo on http://www.codeproject.com/Tips/563043/Dynamic-Visitor-Pattern
// Added some logic to customize handling of unregistered subtypes and provided an optional way to execute a fallback action.

namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;
    using System.Collections.Generic;

    internal sealed class FuncVisitor<TBase, TResult> : IFuncVisitor<TBase, TResult> where TBase : class
    {
        private readonly Func<TBase, TResult> fallbackAction = obj => default(TResult);
        private readonly bool invokeFallbackOnUnregisteredType;
        private readonly Dictionary<Type, dynamic> repository = new Dictionary<Type, dynamic>();
        private readonly UnregisteredSubtypeHandling unregisteredSubtypeHandling;

        private FuncVisitor(bool fallback)
        {
            this.invokeFallbackOnUnregisteredType = fallback;
        }

        public FuncVisitor(UnregisteredSubtypeHandling unregisteredSubtypeHandling) : this(false)
        {
            this.unregisteredSubtypeHandling = unregisteredSubtypeHandling;
        }

        public FuncVisitor(Func<TBase, TResult> fallbackAction) : this(true)
        {
            this.fallbackAction = fallbackAction;
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

            if (this.invokeFallbackOnUnregisteredType)
            {
                return this.fallbackAction(value);
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