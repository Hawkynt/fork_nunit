// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

// Polyfill for frameworks where Backports provides NotNullIfNotNullAttribute as internal
// but NUnit needs public access. This applies to:
// - netstandard2.0: Backports provides internal, need public
// - net20-net45: Backports provides internal, need public
// Native support: netstandard2.1+, net5.0+
// Nullable package: net462, net48
#if USES_BACKPORTS && !NETSTANDARD2_1_OR_GREATER

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Specifies that the output will be non-null if the named parameter is non-null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
    public sealed class NotNullIfNotNullAttribute : Attribute
    {
        /// <summary>
        /// Initializes the attribute with the associated parameter name.
        /// </summary>
        /// <param name="parameterName">
        /// The associated parameter name. The output will be non-null if the argument to the parameter specified is non-null.
        /// </param>
        public NotNullIfNotNullAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        /// <summary>
        /// Gets the associated parameter name.
        /// </summary>
        public string ParameterName { get; }
    }
}

#endif
