/**
* @classdesc DynamicPool
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description 不固定长度的缓冲池
*/

using System.Collections.Generic;

namespace Pool
{
    public class BasePool<T> : IPool<T> where T : class
    {
        protected readonly PoolDelegator<T> m_PoolDelegator;

        protected Stack<T> m_Elements;
        private int m_CountInactive;

        public BasePool(Constructor<T> constructor) : this(constructor,0)
        {
            
        }
        
        public BasePool(Constructor<T> inConstructor, int inInitialCapacity = 0)
        {

            m_PoolDelegator = new PoolDelegator<T>(inConstructor);
            //TODO optimize the List to a nogc list
            m_Elements = new Stack<T>(inInitialCapacity);
            m_CountInactive = 0;
        }
        
        public int Capacity => m_Elements.Count; 
       

        public int Count => m_Elements.Count;

        public int Ref => m_CountInactive;

        public void Warm(int inCount)
        {
            while (m_Elements.Count < inCount)
            {
                m_Elements.Push(m_PoolDelegator.Construct(this));
            }
        }
        

        /// <summary>
        ///  清除数据
        /// </summary>
        public void Clear()
        {
            for (int i = m_Elements.Count - 1; i >= 0; --i)
            {
                m_PoolDelegator.Destroy(this, m_Elements.Pop());
            }
        }

        /// <summary>
        /// 销毁对象池
        /// </summary>
        public void Dispose()
        {
            Clear();
            m_Elements = null;
        }
        
        public PoolDelegator<T> delegator => m_PoolDelegator;

        public virtual T Alloc()
        {
            T element = InternalAlloc();
            m_PoolDelegator.OnAlloc(this, element);
            return element;
        }
        public virtual void Free(T inElement)
        {

            --m_CountInactive;

            m_Elements.Push(inElement);

            m_PoolDelegator.OnFree(this, inElement);
        }
        protected T InternalAlloc()
        {
            ++m_CountInactive;

            T element;
            int elementIndex = m_Elements.Count - 1;
            if (elementIndex >= 0)
            {
                element = m_Elements.Pop();
                // m_Elements.RemoveAt(elementIndex);
            }
            else
            {
                element = m_PoolDelegator.Construct(this);
            }

            return element;
        }

    }
}