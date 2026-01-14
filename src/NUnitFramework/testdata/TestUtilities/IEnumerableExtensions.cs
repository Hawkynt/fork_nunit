// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

// IAsyncEnumerable requires async infrastructure not fully available in net20 via Backports
#if !NET20
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NUnit.TestData.TestUtilities
{
    internal static class IEnumerableExtensions
    {
        public static async IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> input)
        {
            foreach (var value in input)
            {
                yield return await Task.FromResult(value);
            }
        }
    }
}
#endif
