﻿using System;
using System.Runtime.CompilerServices;

namespace FieldCacheNamespace
{
    partial struct FieldCache<T> : IEquatable<FieldCache<T>>
    {
        
        /// <summary>
        /// Use this in your property's getter
        /// </summary>
        /// <param name="ctor">Expression to initialize the given property</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue

(in Func<
    
T> ctor

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

);
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
    
T> ctor

        , T0 arg0

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

        , T2 arg2

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1

        , arg2


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

        , T2 arg2

        , T3 arg3

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1

        , arg2

        , arg3


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

        , T2 arg2

        , T3 arg3

        , T4 arg4

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1

        , arg2

        , arg3

        , arg4


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

        , T2 arg2

        , T3 arg3

        , T4 arg4

        , T5 arg5

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1

        , arg2

        , arg3

        , arg4

        , arg5


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

        , T2 arg2

        , T3 arg3

        , T4 arg4

        , T5 arg5

        , T6 arg6

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1

        , arg2

        , arg3

        , arg4

        , arg5

        , arg6


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

        , T2 arg2

        , T3 arg3

        , T4 arg4

        , T5 arg5

        , T6 arg6

        , T7 arg7

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1

        , arg2

        , arg3

        , arg4

        , arg5

        , arg6

        , arg7


);
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
    
T> ctor

        , T0 arg0

        , T1 arg1

        , T2 arg2

        , T3 arg3

        , T4 arg4

        , T5 arg5

        , T6 arg6

        , T7 arg7

        , T8 arg8

)
        {
            if (!initted)
            {
                lock (ctor)
                {
                    if (!initted)
                    {
                        value = ctor(

arg0

        , arg1

        , arg2

        , arg3

        , arg4

        , arg5

        , arg6

        , arg7

        , arg8


);
                        initted = true;
                    }
                }
            }
            return value;
        }
        
    }
}