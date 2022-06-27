using UnityEngine;

namespace Pool
{
    /// <summary>
    /// BasePool for a prefab.
    /// </summary>
    public interface IPrefabPool<T> : IPool<T> where T : Component
    {
        string Name { get; }
        T Prefab { get; }
        Transform RootTransform { get; }
        T Alloc(Transform inParent);
        T Alloc(Transform inParent,Vector3 inPosition,bool inbWorldSpace = false);
        T Alloc(Transform inParent, Vector3 inPosition, Quaternion inOrientation,bool inbWorldSpace = false );
    }
}