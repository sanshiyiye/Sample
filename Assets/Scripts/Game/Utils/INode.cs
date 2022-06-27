public interface INode<T>
    where T : class, INode<T>
{
    T Next { get; set; }
    T Previous { get; set; }

#if DEBUG
    NodeList<T> List { get; set; }
#endif
}
