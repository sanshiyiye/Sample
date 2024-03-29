using System;
using System.Collections;
using System.Collections.Generic;

/**
* @classdesc NodeList
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

/// <summary>
  /// A storage-free doubly linked list for node objects.
  /// Note that nodes can only safely be in one of these lists at a time.
  /// 
  /// This structure has no GC allocations for "foreach" iteration, but should
  /// not be iterated on more than once at a time (i.e. no nested n*n loops).
  /// </summary>
  public class NodeList<T> : IEnumerable<T>
    where T : class, INode<T>
  {
    public class NodeListEnumerator : IEnumerator<T>
    {
      private bool reset;
      private T current;
      private NodeList<T> list;

      public T Current { get { return this.current; } }
      object IEnumerator.Current { get { return this.current; } }

      public bool MoveNext()
      {
        // Need to skip the first MoveNext() immediately after resetting
        if ((current != null) && (reset == false))
          current = current.Next;
        reset = false;
        return current != null;
      }

      public void Dispose() { }

      public void Reset()
      {
        this.current = this.list.first;
        this.reset = true;
      }

      internal NodeListEnumerator(NodeList<T> list)
      {
        this.current = null;
        this.list = list;
        this.reset = true;
      }
    }

#if DEBUG
    // Special identifier for debugging and checking for invalid nodes
    internal bool isPoolList;
#endif

    internal T first;
    internal T last;

    // Preallocate a single enumerator to reuse over time
    private NodeListEnumerator enumerator;

    public int Count { get; private set; }

    public NodeList()
    {
#if DEBUG
      this.isPoolList = false;
#endif

      this.first = null;
      this.last = null;
      this.enumerator = new NodeListEnumerator(this);

      this.Count = 0;
    }

    /// <summary>
    /// Adds a node to the end of the list. O(1)
    /// </summary>
    public void Add(T value)
    {
#if DEBUG
      if (value.List != null)
        throw new InvalidOperationException("Node is already in a list");
#endif

      if (this.first == null)
        this.first = value;
      value.Previous = this.last;

      if (this.last != null)
        this.last.Next = value;
      value.Next = null;

      this.last = value;

#if DEBUG
      value.List = this;
#endif

      this.Count++;
    }

    /// <summary>
    /// Appends another list onto this one, clearing out the original list.
    /// O(n), but marginally cheaper than the naive way.
    /// </summary>
    public void Append(NodeList<T> list)
    {
      if (this.first == null)
        this.first = list.first;
      if (this.last != null)
        this.last.Next = list.first;

      if (list.first != null)
        list.first.Previous = this.last;
      if (list.last != null)
        this.last = list.last;

      // Reassign the list pointer
      T node = list.first;
      while (node != null)
      {
#if DEBUG
        node.List = this;
#endif
        node = node.Next;
      }

      this.Count += list.Count;

      list.first = null;
      list.last = null;
      list.Count = 0;
    }

    /// <summary>
    /// Removes a node from the list. O(1)
    /// </summary>
    public void Remove(T value)
    {
#if DEBUG
      if (value.List != this)
        throw new AccessViolationException("Node is not in this list");
#endif

      if (this.first == value)
        this.first = value.Next;
      if (this.last == value)
        this.last = value.Previous;

      if (value.Previous != null)
        value.Previous.Next = value.Next;
      if (value.Next != null)
        value.Next.Previous = value.Previous;

      value.Next = null;
      value.Previous = null;

#if DEBUG
      value.List = null;
#endif

      this.Count--;
    }

    /// <summary>
    /// Removes and returns the first element. O(1)
    /// </summary>
    public T RemoveFirst()
    {
      if (this.first == null)
        throw new AccessViolationException();

      T result = this.first;
      if (result.Next != null)
        result.Next.Previous = null;
      this.first = result.Next;
      if (this.last == result)
        this.last = null;

      result.Next = null;
      result.Previous = null;

#if DEBUG
      result.List = null;
#endif

      this.Count--;

      return result;
    }

    /// <summary>
    /// Removes and returns the last element. O(1)
    /// </summary>
    public T RemoveLast()
    {
      if (this.last == null)
        throw new AccessViolationException();

      T result = this.last;
      if (result.Previous != null)
        result.Previous.Next = null;
      this.last = result.Previous;
      if (this.first == result)
        this.first = null;

      result.Next = null;
      result.Previous = null;

#if DEBUG
      result.List = null;
#endif

      this.Count--;

      return result;
    }

    /// <summary>
    /// Returns the first element, keeping it in the list. O(1)
    /// </summary>
    public T PeekFirst()
    {
      return this.first;
    }

    public IEnumerator<T> GetEnumerator()
    {
      this.enumerator.Reset();
      return this.enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
