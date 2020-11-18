



using System;
using System.Runtime.CompilerServices;

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
        //public FieldCache(object holder)
        //{
        //    this.holder = holder;
        //    value = default;
        //    initted = false;
        //}
        //
        //private object holder;
        private T value;
        private bool initted;

        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

(in Func<
        
T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

>

(in Func<
        
        T0, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

>

(in Func<
        
        T0, 

        T1, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

        , T2

>

(in Func<
        
        T0, 

        T1, 

        T2, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

        , T2

        , T3

>

(in Func<
        
        T0, 

        T1, 

        T2, 

        T3, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

        , T2

        , T3

        , T4

>

(in Func<
        
        T0, 

        T1, 

        T2, 

        T3, 

        T4, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

        , T2

        , T3

        , T4

        , T5

>

(in Func<
        
        T0, 

        T1, 

        T2, 

        T3, 

        T4, 

        T5, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

        , T2

        , T3

        , T4

        , T5

        , T6

>

(in Func<
        
        T0, 

        T1, 

        T2, 

        T3, 

        T4, 

        T5, 

        T6, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

        , T2

        , T3

        , T4

        , T5

        , T6

        , T7

>

(in Func<
        
        T0, 

        T1, 

        T2, 

        T3, 

        T4, 

        T5, 

        T6, 

        T7, 

T> ctor)
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
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

<
T0

        , T1

        , T2

        , T3

        , T4

        , T5

        , T6

        , T7

        , T8

>

(in Func<
        
        T0, 

        T1, 

        T2, 

        T3, 

        T4, 

        T5, 

        T6, 

        T7, 

        T8, 

T> ctor)
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
