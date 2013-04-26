// Inspired by the article "Dynamic Visitor Pattern" from GSerjo on http://www.codeproject.com/Tips/563043/Dynamic-Visitor-Pattern
// Added some logic to customize handling of unregistered subtypes and provided an optional way to execute a fallback action.

namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;
    using System.Collections.Generic;

    internal sealed class ActionVisitor<TBase> : IActionVisitor<TBase> where TBase : class
    {
        private readonly Action<TBase> fallbackAction = obj => { };
        private readonly bool invokeFallbackOnUnregisteredType;
        private readonly Dictionary<Type, dynamic> repository = new Dictionary<Type, dynamic>();
        private readonly UnregisteredSubtypeHandling unregisteredSubtypeHandling;

        private ActionVisitor(bool fallback)
        {
            this.invokeFallbackOnUnregisteredType = fallback;
        }

        public ActionVisitor(UnregisteredSubtypeHandling unregisteredSubtypeHandling) : this(false)
        {
            this.unregisteredSubtypeHandling = unregisteredSubtypeHandling;
        }

        public ActionVisitor(Action<TBase> fallbackAction) : this(true)
        {
            this.fallbackAction = fallbackAction;
        }

        public void Register<T>(Action<T> action) where T : TBase
        {
            this.repository[typeof (T)] = action;
        }

        public void Visit(TBase value)
        {
            dynamic action;
            if (this.repository.TryGetValue(value.GetType(), out action))
            {
                action((dynamic) value);
                return;
            }

            if (this.invokeFallbackOnUnregisteredType)
            {
                this.fallbackAction(value);
                return;
            }

            if (this.unregisteredSubtypeHandling == UnregisteredSubtypeHandling.ThrowException)
            {
                throw new UnhandledSubtypeException(
                    string.Format("No suitable visitor method could be resolved for {0}.", value.GetType().FullName));
            }
        }
    }
}