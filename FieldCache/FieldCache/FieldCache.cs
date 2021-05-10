using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FieldCacheNamespace
{
    /// <summary>
    /// Provides lazy initialization experience. Is better than the Lazy class as:
    /// 1) Is a struct
    /// 2) Does not affect record's Equals in a bad way Lazy does
    /// <code>
    /// public int MyProperty => myProperty; // same as myProperty.Value
    /// public Container int myProperty = new(() => some method);
    /// </code>
    /// </summary>
    /// <typeparam name="T">
    /// The type to store inside
    /// </typeparam>
    public partial struct FieldCache<T> : IEquatable<FieldCache<T>>
    {
        private T value { get; set; }
        private object holder { get; set; }

        /// <summary>
        /// So that when records get compared, this field will not affect the result
        /// </summary>
        public bool Equals(FieldCache<T> _)
            => true;

        /// <summary>
        /// So that when records get compared, this field will not affect the result
        /// </summary>
        public override bool Equals(object _)
            => true;

        /// <summary>
        /// So that when records get compared, this field will not affect the result
        /// </summary>
        public override int GetHashCode() => 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private T CreateValue<TThis>(Func<TThis, T> factory, TThis @this) where TThis : class
        {
            if (!ReferenceEquals(@this, holder))
                lock (@this)
                {
                    if (!ReferenceEquals(@this, holder))
                    {
                        value = factory(@this);
                        holder = @this;
                    }
                }
            return value;
        }

        /// <summary>
        /// It is guaranteed that <paramref name="factory"/> is called only once
        /// throughout all threads
        /// </summary>
        /// <param name="factory">This is a delegate which takes one argument
        /// (for example, "this" when used in records or classes) and passing it back to the factory
        /// returning created an instance of T</param>
        /// <param name="this">
        /// To avoid reallocation, your method must be static (that is, not reading any outside variables from the instance),
        /// and instead of addressing your fields by normal this, pass this argument and address via this one
        /// (see examples on the readme)
        /// </param>
        /// <returns>The value returned by factory</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue<TThis>(Func<TThis, T> factory, TThis @this) where TThis : class
        {
            if (ReferenceEquals(@this, holder))
                return value;

            return CreateValue(factory, @this);
        }

    }
}
