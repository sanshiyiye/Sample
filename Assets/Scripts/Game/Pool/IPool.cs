using System;

namespace Pool
{
    
    /// <summary>
    /// 非范型接口
    /// </summary>
    public interface IPool : IDisposable
    {
        /// <summary
        /// 容器容量
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// 当前容器大小
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 目前引用计数
        /// </summary>
        int Ref { get; }
        
        void Warm(int inCount);
        
        void Clear();
    }

    /// <summary>
    /// 范型接口
    /// </summary>
    public interface IPool<T> : IPool where T : class
    {

        PoolDelegator<T> delegator { get; }
        
        T Alloc();

        void Free(T inElement);
    }
}