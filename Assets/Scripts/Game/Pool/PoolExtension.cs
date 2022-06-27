/**
* @classdesc BasePool
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description 缓冲池拓展
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public static class PoolExtension
    {

        public static void Warm<T>(this IPool<T> inThis) where T : class
        {
            inThis.Warm(inThis.Capacity);
        }
        
        public static Constructor<T> DefaultConstructor<T>() where T : class, new()
        {
            return (p, element) =>
            {
                return new T();
            };
        }
        
        

        #region Collections

        #region Alloc


        public static int Alloc<T>(this IPool<T> inThis, T[] inDest) where T : class
        {
            return Alloc<T>(inThis, inDest, 0, inDest.Length);
        }

        public static int Alloc<T>(this IPool<T> inThis, T[] inDest, int inStartIndex) where T : class
        {
            return Alloc<T>(inThis, inDest, inStartIndex, inDest.Length - inStartIndex);
        }


        public static int Alloc<T>(this IPool<T> inThis, T[] inDest, int inStartIndex, int inCount) where T : class
        {
            int allocCount = 0;
            for (int i = 0; i < inCount; ++i)
            {
                int idx = inStartIndex + i;
                if (inDest[idx] == null)
                {
                    inDest[idx] = inThis.Alloc();
                    ++allocCount;
                }
            }
            return allocCount;
        }


        public static void Alloc<T>(this IPool<T> inThis, ICollection<T> inDest, int inCount) where T : class
        {
            for (int i = 0; i < inCount; ++i)
                inDest.Add(inThis.Alloc());
        }

        #endregion // Alloc
        
        #region Free


        public static void Free<T>(this IPool<T> inThis, T[] inSrc) where T : class
        {
            Free<T>(inThis, inSrc, 0, inSrc.Length);
        }
        
        public static void Free<T>(this IPool<T> inThis, T[] inSrc, int inStartIndex) where T : class
        {
            Free<T>(inThis, inSrc, inStartIndex, inSrc.Length - inStartIndex);
        }
        
        public static void Free<T>(this IPool<T> inThis, T[] inSrc, int inStartIndex, int inCount) where T : class
        {
            for (int i = 0; i < inCount; ++i)
            {
                int idx = inStartIndex + i;
                T element = inSrc[idx];
                if (element != null)
                {
                    inThis.Free(element);
                    inSrc[idx] = null;
                }
            }
        }
        
        public static void Free<T>(this IPool<T> inThis, ICollection<T> inSrc) where T : class
        {
            foreach (var element in inSrc)
                inThis.Free(element);

            inSrc.Clear();
        }

        #endregion // Free

        #endregion // Collections
        

        #region Configuration
        
        public static void UseIDisposable<T>(this IPool<T> inPool) where T : class, IDisposable
        {
            inPool.delegator.RegisterOnDestruct(IDisposableOnDispose<T>);
        }

        private static void IDisposableOnDispose<T>(IPool<T> inPool, T inElement) where T : class, IDisposable
        {
            inElement.Dispose();
        }

        #endregion // Configuration
    }
}