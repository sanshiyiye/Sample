/**
* @classdesc PrefabPool
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description prefab 实现缓冲池
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Pool
{
    public class PrefabPool<T> : BasePool<T>, IPrefabPool<T> where T : Component
    {
        private readonly bool _multiScene;
        
        private Transform m_CacheRootTransform;

        private readonly bool m_ComponentSkipSelfConstruct;
        private readonly bool m_ComponentSkipSelfAlloc;

        public PrefabPool(T inPrefab, Transform cacheRoot = null,bool multiScene = true)
            : base(PrefabConstructor(inPrefab,cacheRoot))
        {
            UnityHelper.Initialize();
            _multiScene = multiScene;
            m_CacheRootTransform = cacheRoot;
            Prefab = inPrefab;
            Name = inPrefab.name;
            m_ComponentSkipSelfConstruct = inPrefab is IPoolConstructHandler;
            m_ComponentSkipSelfAlloc = inPrefab is IPoolAllocHandler;
            if (inPrefab.GetComponentsInChildren<IPoolConstructHandler>().Length > (m_ComponentSkipSelfConstruct? 1:0))
            {
                m_PoolDelegator.RegisterOnConstruct(OnConstruct);
                m_PoolDelegator.RegisterOnDestruct(OnDestruct);
            }

            if (inPrefab.GetComponentsInChildren<IPoolAllocHandler>().Length > (m_ComponentSkipSelfAlloc?1:0))
            {
                m_PoolDelegator.RegisterOnAlloc(OnAlloc);
                m_PoolDelegator.RegisterOnFree(OnFree);
            }
            
            
        }
        
        
        public override T Alloc()
        {
            return  Alloc(null,Vector3.zero, Quaternion.identity);
        }
        public T Alloc(Transform inParent)
        {
            return Alloc(inParent,Vector3.zero, Quaternion.identity);
        }
        public T Alloc(Transform inParent, Vector3 inPosition, bool inbWorldSpace = false)
        {
            return Alloc(inParent,inPosition, Quaternion.identity,inbWorldSpace);
        }
        public T Alloc(Transform inParent,Vector3 inPosition, Quaternion inOrientation,  bool inbWorldSpace = false)
        {
            T element = InternalAlloc();

            Transform t = element.transform;
            if (inbWorldSpace)
            {
                t.position = inPosition;
                t.rotation = inOrientation;
            }
            else
            {
                t.localPosition = inPosition;
                t.localRotation = inOrientation;
            }
            if(inParent) t.SetParent(inParent, inbWorldSpace);

            if (!inParent && !element.transform.parent)
                SceneManager.MoveGameObjectToScene(element.gameObject, SceneManager.GetActiveScene());
            
            m_PoolDelegator.OnAlloc(this, element);
            return element;
        }
        public override void Free(T inElement)
        {
            base.Free(inElement);

            inElement.transform.SetParent(m_CacheRootTransform, false);
        }
        
        public string Name { get; }

        public T Prefab { get; }
        public Transform RootTransform => m_CacheRootTransform;
        public static Constructor<T> PrefabConstructor(T prfab, Transform inPoolRoot)
        {

            return (p,elements) =>
            {

                
                if (elements.Length == 1)
                {
                    T obj =  UnityEngine.Object.Instantiate(prfab, (Transform)elements[0], false) ;
                    obj.name = string.Format("{0} [BasePool {1}]", prfab.name, p.Count + p.Ref);
                    return obj;
                }
                else if (elements.Length == 2)
                {
                    T obj = UnityEngine.Object.Instantiate(prfab, ((GameObject)elements[0]).transform, (bool)elements[1]);
                    //TODO 减少字符串拼接的消耗
                    obj.name = string.Format("{0} [BasePool {1}]", prfab.name, p.Count + p.Ref);
                    return obj;
                }
                else if (elements.Length == 3)
                {
                    Vector3 position = (Vector3?) elements[1] ?? Vector3.zero;
                    Quaternion quaternion = (Quaternion?) elements[2] ?? Quaternion.identity;
                    
                    T obj = UnityEngine.Object.Instantiate(prfab, position,quaternion,((GameObject)elements[0]).transform);
                    //TODO 减少字符串拼接的消耗
                    obj.name = string.Format("{0} [BasePool {1}]", prfab.name, p.Count + p.Ref);
                    return obj;
                }
                else
                {
                    T obj =  UnityEngine.Object.Instantiate(prfab) ;
                    obj.name = string.Format("{0} [BasePool {1}]", prfab.name, p.Count + p.Ref);
                    return obj;
                }
               
            };
        }
        
        private void OnConstruct(IPool<T> inPool, T inElement)
        {
            using(PooledList<IPoolConstructHandler> children = PooledList<IPoolConstructHandler>.Create())
            {
                inElement.GetComponentsInChildren<IPoolConstructHandler>(true, children);

                for(int i = 0, length = children.Count; i < length; ++i)
                {
                    if (!m_ComponentSkipSelfConstruct && children[i] != inElement)
                        children[i].OnConstruct();
                }
            }
        }
        

        private void OnAlloc(IPool<T> inPool, T inElement)
        {
            using(PooledList<IPoolAllocHandler> children = PooledList<IPoolAllocHandler>.Create())
            {
                inElement.GetComponentsInChildren<IPoolAllocHandler>(true, children);

                for(int i = 0, length = children.Count; i < length; ++i)
                {
                    if (!m_ComponentSkipSelfAlloc || children[i] != inElement)
                        children[i].OnAlloc();
                }
            }
        }

        private void OnFree(IPool<T> inPool, T inElement)
        {
            using(PooledList<IPoolAllocHandler> children = PooledList<IPoolAllocHandler>.Create())
            {
                inElement.GetComponentsInChildren<IPoolAllocHandler>(true, children);

                for(int i = 0, length = children.Count; i < length; ++i)
                {
                    if (!m_ComponentSkipSelfAlloc || children[i] != inElement)
                        children[i].OnFree();
                }
            }
        }

        private  void OnDestruct(IPool<T> inPool, T inElement)
        {
            using(PooledList<IPoolConstructHandler> children = PooledList<IPoolConstructHandler>.Create())
            {
                inElement.GetComponentsInChildren<IPoolConstructHandler>(true, children);

                for(int i = 0, length = children.Count; i < length; ++i)
                {
                    if (!m_ComponentSkipSelfConstruct || children[i] != inElement)
                        children[i].OnDestruct();
                }
            }
            
            // Make sure the object hasn't already been destroyed
            if (inElement && inElement.gameObject)
            {
                if (UnityHelper.IsQuitting())
                    UnityEngine.Object.DestroyImmediate(inElement.gameObject);
                else
                    UnityEngine.Object.Destroy(inElement.gameObject);
            }
        }
        
        public void Warm(int inCount,params Object[] parameters)
        {
            while (m_Elements.Count < inCount)
            {
                
                m_Elements.Push(m_PoolDelegator.Construct(this));
            }
        }

    }
    
    
    internal static class UnityHelper
    {
        private static bool s_QuittingApplication;
        private static bool s_Initialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void Initialize()
        {
            if (!s_Initialized)
            {
                s_Initialized = true;
                Application.quitting += OnQuitting;
            }
        }

        private static void OnQuitting()
        {
            s_QuittingApplication = true;
        }

        static public bool IsQuitting()
        {
            return s_QuittingApplication;
        }
    }
}