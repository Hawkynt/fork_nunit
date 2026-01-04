// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt
#if NET20 || NET35

using System.Collections.Generic;

namespace NUnit.Framework.Internal
{
    internal static partial class AsyncEnumerableAdapter
    {
        // Async enumerable is not supported on net20/net35
        private static partial bool TryGetAsyncBlockingEnumerable(object enumerable, out IEnumerable<object?>? result)
        {
            result = null;
            return false;
        }
    }
}
#endif
