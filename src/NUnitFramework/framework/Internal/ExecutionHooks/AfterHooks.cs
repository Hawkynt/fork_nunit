// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

#pragma warning disable IDE0005 // Using directive is unnecessary (analyzer doesn't see Backports LINQ)
using System;
using System.Collections.Generic;
using System.Linq;
#pragma warning restore IDE0005

namespace NUnit.Framework.Internal.ExecutionHooks
{
    internal sealed class AfterHooks : Hooks
    {
        private readonly Stack<Action<HookData>> _stack;

#if NET20 || NET35 || NET40
        protected override ICollection<Action<HookData>> Handlers => _stack.ToArray();
#else
        protected override IReadOnlyCollection<Action<HookData>> Handlers => _stack;
#endif

        internal override void AddHandler(Action<HookData> handler) => _stack.Push(handler);

        public AfterHooks()
        {
            _stack = new Stack<Action<HookData>>();
        }

        public AfterHooks(AfterHooks source)
        {
            _stack = new Stack<Action<HookData>>(source._stack);
        }
    }
}
