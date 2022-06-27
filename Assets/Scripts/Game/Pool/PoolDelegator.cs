/**
* @classdesc PoolConfig
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description 缓冲池构造工厂类
*/

using System;

namespace Pool
{
    public delegate T Constructor<T>(IPool<T> inPool,params Object[] inElement ) where T : class;
    
    public delegate void EventDelegate<T>(IPool<T> inPool, T inElement) where T : class;
    
    
    public sealed class PoolDelegator<T> where T : class
    {
        private readonly Constructor<T> m_Constructor;

        private EventDelegate<T> m_OnConstruct;
        private EventDelegate<T> m_OnDestruct;

        private EventDelegate<T> m_OnAlloc;
        private EventDelegate<T> m_OnFree;

        // internal readonly bool StrictTyping;

        internal PoolDelegator(Constructor<T> constructor)
        {
            m_Constructor = constructor;

            Type type = typeof(T);
            // if (typeof(IPooledObject<T>).IsAssignableFrom(type))
            // {
            //     m_OnConstruct += PooledOnConstruct;
            //     m_OnDestruct += PooledOnDestruct;
            //     m_OnAlloc += PooledOnAlloc;
            //     m_OnFree += PooledOnFree;
            // }
            if (typeof(IPoolConstructHandler).IsAssignableFrom(type))
            {
                m_OnConstruct += PooledOnConstruct;
                m_OnDestruct += PooledOnDestruct;
            }
            if (typeof(IPoolAllocHandler).IsAssignableFrom(type))
            {
                m_OnAlloc += PooledOnAlloc;
                m_OnFree += PooledOnFree;
            }
        }

        #region Callback Registration

        public void RegisterOnConstruct(EventDelegate<T> inEventDelegate)
        {
            m_OnConstruct += inEventDelegate;
        }

        public void RegisterOnDestruct(EventDelegate<T> inEventDelegate)
        {
            m_OnDestruct += inEventDelegate;
        }

        public void RegisterOnAlloc(EventDelegate<T> inEventDelegate)
        {
            m_OnAlloc += inEventDelegate;
        }

        public void RegisterOnFree(EventDelegate<T> inEventDelegate)
        {
            m_OnFree += inEventDelegate;
        }

        #endregion // Callback Registration

        #region Callback Execution

        internal T Construct(IPool<T> inPool,params Object[] elements) 
        {
            
            T obj = m_Constructor(inPool,elements);

            if (m_OnConstruct != null)
                m_OnConstruct(inPool, obj);

            return obj;
        }

        internal void Destroy(IPool<T> inPool, T inElement)
        {
            if (m_OnDestruct != null)
                m_OnDestruct(inPool, inElement);
        }

        internal void OnAlloc(IPool<T> inPool, T inElement)
        {
            if (m_OnAlloc != null)
                m_OnAlloc(inPool, inElement);
        }

        internal void OnFree(IPool<T> inPool, T inElement)
        {
            if (m_OnFree != null)
                m_OnFree(inPool, inElement);
        }

        #endregion // Callback Execution

        private static void PooledOnConstruct(IPool<T> inPool, T inElement)
        {
            ((IPoolConstructHandler) inElement).OnConstruct();
        }

        private static void PooledOnDestruct(IPool<T> inPool, T inElement)
        {
            ((IPoolConstructHandler) inElement).OnDestruct();
        }

        private static void PooledOnAlloc(IPool<T> inPool, T inElement)
        {
            ((IPoolAllocHandler) inElement).OnAlloc();
        }

        private static void PooledOnFree(IPool<T> inPool, T inElement)
        {
            ((IPoolAllocHandler) inElement).OnFree();
        }

    }
}