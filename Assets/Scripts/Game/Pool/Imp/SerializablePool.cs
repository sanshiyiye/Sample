/**
* @classdesc SerializablePool
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description 脚本对象池
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pool
{
    public class SerializablePool<T> : IDisposable where T : Component
    {
        private T m_Prefab = null;

        // private String m_Name = null;

        public bool isInitialize = false;
        
        private  Transform m_DefaultPoolRoot = null;
        
        private PrefabPool<T> _mInnerPool;

        [NonSerialized] private readonly List<T> m_ActiveObjects = new List<T>();
        [NonSerialized] private ReadOnlyCollection<T> m_ReadOnlyActive;

        public SerializablePool() { }
        
        public void Initialize(T inPrefab, Transform inPoolRoot = null, int inPrewarmCapacity = -1)
        {
            if (isInitialize || _mInnerPool != null)
                throw new InvalidOperationException("Cannot load pool twice");

            if (inPrefab == null)
                throw new InvalidOperationException("Prefab can not be null");
                
            //TODO add Name initial
            _mInnerPool = new PrefabPool<T>(inPrefab, inPoolRoot);
            m_Prefab = inPrefab;
            m_DefaultPoolRoot = inPoolRoot;
            _mInnerPool.delegator.RegisterOnAlloc(OnAlloc);
            _mInnerPool.delegator.RegisterOnFree(OnFree);
            if (inPrewarmCapacity > 0)
                _mInnerPool.Warm(inPrewarmCapacity);
            isInitialize = true;
        }
        
        public ReadOnlyCollection<T> ActiveObjects
        {
            get { return m_ReadOnlyActive ?? (m_ReadOnlyActive = new ReadOnlyCollection<T>(m_ActiveObjects)); }
        }
        public void Reset()
        {
            if (_mInnerPool != null && m_ActiveObjects.Count > 0)
            {
                using(PooledList<T> tempList = PooledList<T>.Create(m_ActiveObjects))
                {
                    _mInnerPool.Free(tempList);
                }
            }
        }
        public void Destroy()
        {
            if (_mInnerPool != null)
            {
                Reset();

                _mInnerPool.Dispose();
                _mInnerPool = null;
                isInitialize = false;
            }
        }
        /// <summary>
        /// TODO 管理 active scene 上的对象 
        /// </summary>
        public int FreeAllInScene(Scene inScene)
        {
            if (_mInnerPool != null)
            {
                using(PooledList<T> tempList = PooledList<T>.Create())
                {
                    foreach(var activeObj in m_ActiveObjects)
                    {
                        if (activeObj.gameObject.scene == inScene)
                            tempList.Add(activeObj);
                    }

                    int finalCount = tempList.Count;
                    _mInnerPool.Free(tempList);
                    return finalCount;
                }
            }
            
            return 0;
        }
        private void OnAlloc(IPool<T> inPool, T inElement)
        {
            m_ActiveObjects.Add(inElement);
        }
        private void OnFree(IPool<T> inPool, T inElement)
        {
            int idx = m_ActiveObjects.IndexOf(inElement);
            int end = m_ActiveObjects.Count - 1;
            if (idx >= 0)
            {
                if (idx != end)
                    m_ActiveObjects[idx] = m_ActiveObjects[end];
                m_ActiveObjects.RemoveAt(end);
            }
        }
        public PoolDelegator<T> delegator
        {
            get
            {
                if (_mInnerPool != null)
                    return _mInnerPool.delegator;

                return null;
            }
        }
        public int Capacity
        {
            get
            {
                if (_mInnerPool != null)
                    return _mInnerPool.Capacity;
                return 0;
            }
        }
        public int Count
        {
            get
            {
                if (_mInnerPool != null)
                    return _mInnerPool.Count;
                
                return 0;
            }
        }
        public int Ref
        {
            get
            {
                if (_mInnerPool != null)
                    return _mInnerPool.Ref;
                
                return 0;
            }
        }
        public T Alloc(Transform inParent)
        {
            return Alloc(inParent, Vector3.zero,Quaternion.identity);
        }
        public T Alloc(Transform inParent, Vector3 inPosition, bool inbWorldSpace = false)
        {
            return Alloc(inParent, inPosition,Quaternion.identity);
        }
        public T Alloc()
        {
            return Alloc(null, Vector3.zero,Quaternion.identity);
        }
        public T Alloc( Transform inParent,Vector3 inPosition, Quaternion inOrientation , bool inbWorldSpace = false)
        {
            if(!isInitialize)
                throw  new InvalidOperationException("The pool must be Initialize first");
            return _mInnerPool.Alloc(inParent,inPosition, inOrientation,  inbWorldSpace);
        }
        public void Free(T inElement)
        {
            if (_mInnerPool == null)
                throw new InvalidOperationException("Cannot free an element while pool is not initialized");

            _mInnerPool.Free(inElement);
        }
        public void Clear()
        {
            Reset();
        }
        public void Dispose()
        {
            Destroy();
        }

    }
}