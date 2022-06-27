namespace Pool
{
    public interface IPooledObject<T> where T : class
    {

        void OnConstruct(IPool<T> inPool);
        
        void OnDestruct();
        
        void OnAlloc();
        
        void OnFree();
    }
    
    public interface IPoolConstructHandler
    {
        void OnConstruct();
        void OnDestruct();
    }
    
    public interface IPoolAllocHandler
    {
        
        void OnAlloc();
        void OnFree();
    }
}