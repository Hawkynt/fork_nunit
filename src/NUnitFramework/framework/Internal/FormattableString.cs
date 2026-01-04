// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

// FormattableString polyfill for frameworks where it's not available
// This type was introduced in .NET Framework 4.6
#if NET20 || NET35 || NET40 || NET45

namespace System
{
    using System.Globalization;

    /// <summary>
    /// Represents a composite format string, along with the arguments to be formatted.
    /// </summary>
    public abstract class FormattableString : IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattableString"/> class.
        /// </summary>
        protected FormattableString()
        {
        }

        /// <summary>
        /// Gets the format string.
        /// </summary>
        public abstract string Format { get; }

        /// <summary>
        /// Gets the number of arguments to be formatted.
        /// </summary>
        public abstract int ArgumentCount { get; }

        /// <summary>
        /// Returns the specified argument.
        /// </summary>
        /// <param name="index">The index of the argument.</param>
        /// <returns>The argument.</returns>
        public abstract object? GetArgument(int index);

        /// <summary>
        /// Returns an object array that contains all arguments.
        /// </summary>
        /// <returns>An object array that contains all arguments.</returns>
        public abstract object?[] GetArguments();

        /// <summary>
        /// Returns the string that results from formatting the format string along with its arguments
        /// by using the formatting conventions of a specified culture.
        /// </summary>
        /// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
        /// <returns>A string formatted using the specified formatting conventions.</returns>
        public abstract string ToString(IFormatProvider? formatProvider);

        /// <summary>
        /// Returns the string that results from formatting the composite format string along with its arguments
        /// by using the formatting conventions of the current culture.
        /// </summary>
        /// <returns>A result string formatted by using the conventions of the current culture.</returns>
        public override string ToString() => ToString(CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns the string that results from formatting the format string along with its arguments
        /// by using the formatting conventions of a specified culture.
        /// </summary>
        /// <param name="format">Not used.</param>
        /// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
        /// <returns>A string formatted using the specified formatting conventions.</returns>
        string IFormattable.ToString(string? format, IFormatProvider? formatProvider) => ToString(formatProvider);

        /// <summary>
        /// Returns the string that results from formatting the format string along with its arguments
        /// by using the formatting conventions of the current culture.
        /// </summary>
        /// <param name="formattable">The object to convert to a result string.</param>
        /// <returns>The string that results from formatting the format string along with its arguments
        /// by using the formatting conventions of the current culture.</returns>
        public static string CurrentCulture(FormattableString formattable)
        {
            if (formattable is null)
                throw new ArgumentNullException(nameof(formattable));

            return formattable.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a result string in which arguments are formatted by using the conventions
        /// of the invariant culture.
        /// </summary>
        /// <param name="formattable">The object to convert to a result string.</param>
        /// <returns>The string that results from formatting the current instance by using the
        /// conventions of the invariant culture.</returns>
        public static string Invariant(FormattableString formattable)
        {
            if (formattable is null)
                throw new ArgumentNullException(nameof(formattable));

            return formattable.ToString(CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// A concrete FormattableString implementation used by the compiler.
    /// </summary>
    internal sealed class ConcreteFormattableString : FormattableString
    {
        private readonly string _format;
        private readonly object?[] _arguments;

        internal ConcreteFormattableString(string format, object?[] arguments)
        {
            _format = format;
            _arguments = arguments;
        }

        /// <inheritdoc />
        public override string Format => _format;

        /// <inheritdoc />
        public override int ArgumentCount => _arguments.Length;

        /// <inheritdoc />
        public override object? GetArgument(int index) => _arguments[index];

        /// <inheritdoc />
        public override object?[] GetArguments() => _arguments;

        /// <inheritdoc />
        public override string ToString(IFormatProvider? formatProvider) =>
            string.Format(formatProvider, _format, _arguments);
    }
}

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// A factory type used by compilers to create instances of <see cref="FormattableString"/>.
    /// </summary>
    public static class FormattableStringFactory
    {
        /// <summary>
        /// Creates a <see cref="FormattableString"/> instance from a composite format string and its arguments.
        /// </summary>
        /// <param name="format">The composite format string.</param>
        /// <param name="arguments">The arguments to be formatted.</param>
        /// <returns>A <see cref="FormattableString"/> instance.</returns>
        public static FormattableString Create(string format, params object?[] arguments)
        {
            if (format is null)
                throw new ArgumentNullException(nameof(format));
            if (arguments is null)
                throw new ArgumentNullException(nameof(arguments));

            return new ConcreteFormattableString(format, arguments);
        }
    }
}

#endif
