// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnit.Framework.Internal.ExecutionHooks
{
    internal abstract class Hooks
    {
#if NET20 || NET35 || NET40
        protected abstract ICollection<Action<HookData>> Handlers { get; }
#else
        protected abstract IReadOnlyCollection<Action<HookData>> Handlers { get; }
#endif

        internal int Count => Handlers.Count;

        internal abstract void AddHandler(Action<HookData> handler);

#if NET20 || NET35 || NET40
        internal ICollection<Action<HookData>> GetHandlers()
        {
            lock (Handlers)
            {
                return Handlers.ToArray();
            }
        }
#else
        internal IReadOnlyCollection<Action<HookData>> GetHandlers()
        {
            lock (Handlers)
            {
                return Handlers.ToArray();
            }
        }
#endif

        internal void InvokeHandlers(HookData hookInfo)
        {
            foreach (var handler in GetHandlers())
            {
                handler(hookInfo);
            }
        }
    }
}
