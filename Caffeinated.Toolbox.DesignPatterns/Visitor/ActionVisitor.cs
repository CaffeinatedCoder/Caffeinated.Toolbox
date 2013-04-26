namespace Caffeinated.Toolbox.DesignPatterns.Visitor
{
    using System;
    using System.Collections.Generic;

    internal sealed class ActionVisitor<TBase> : IActionVisitor<TBase> where TBase : class
    {
        private readonly Dictionary<Type, dynamic> repository = new Dictionary<Type, dynamic>();
        private readonly UnregisteredSubtypeHandling unregisteredSubtypeHandling;

        public ActionVisitor(UnregisteredSubtypeHandling unregisteredSubtypeHandling)
        {
            this.unregisteredSubtypeHandling = unregisteredSubtypeHandling;
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

            if (this.unregisteredSubtypeHandling == UnregisteredSubtypeHandling.ThrowException)
            {
                throw new UnhandledSubtypeException(
                    string.Format("No suitable visitor method could be resolved for {0}.", value.GetType().FullName));
            }
        }
    }
}