/**
* @classdesc PooledList
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.Collections.Generic;

namespace Pool
{
    public class PooledList<T> : List<T>, IDisposable
    {
        private void Reset()
        {
            Clear();
        }

        /// <summary>
        /// Resets and recycles the PooledList to the pool.
        /// </summary>
        public void Dispose()
        {
            Reset();
            s_ObjectPool.Free(this);
        }

        #region BasePool

        // Initial capacity
        private const int POOL_SIZE = 8;

        // Object pool to hold available PooledList.
        private static IPool<PooledList<T>> s_ObjectPool;

        static PooledList()
        {
            s_ObjectPool = new BasePool<PooledList<T>>(PoolExtension.DefaultConstructor<PooledList<T>>(),POOL_SIZE);
        }

        /// <summary>
        /// Retrieves a PooledList for use.
        /// </summary>
        public static  PooledList<T> Create()
        {
            return s_ObjectPool.Alloc();
        }

        /// <summary>
        /// Retrieves a PooledList for use, copying the contents
        /// of the given IEnumerable.
        /// </summary>
        public static  PooledList<T> Create(IEnumerable<T> inToCopy)
        {
            PooledList<T> list = s_ObjectPool.Alloc();
            list.AddRange(inToCopy);
            return list;
        }

        #endregion
    }
}