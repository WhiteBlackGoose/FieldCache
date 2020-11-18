using System;
using System.Runtime.CompilerServices;
using System.Threading;

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
    public struct FieldCache<T> : IEquatable<FieldCache<T>>
    {
        private T value;
        private bool initted;
        Func<T> ctor;
        public FieldCache(Func<T> ctor)
        {
            value = default;
            initted = false;
            this.ctor = ctor;
        }

        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        public T GetValue
        {
            get
            {
                if (!initted)
                {
                    lock (ctor)
                    {
                        if (!initted)
                        {
                            value = ctor();
                            initted = true;
                        }
                    }
                }
                return value;
            }
        }

        /// <summary>
        /// So that when records get compared, this field will not affect the result
        /// </summary>
        public bool Equals(FieldCache<T> _)
            => true;

        /// <summary>
        /// So that when records get compared, this field will not affect the result
        /// </summary>
        public override bool Equals(object obj)
            => true;

        /// <summary>
        /// So that when records get compared, this field will not affect the result
        /// </summary>
        public override int GetHashCode() => 0;
    }
}
