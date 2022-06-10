using System;
using System.Collections;

namespace Agario;
public class AgarioList<T> : IEnumerable<T>
{
    private List<T> _list;

    public AgarioList()
    {
        _list = new List<T>();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)_list).GetEnumerator();
    }

    public void TryAddToCollection(object gameObject)
    {
        if (gameObject is T Object)
            _list.Add(Object);
    }


    public void TryRemoveFromCollection(object ToRemove)
    {
        if (!(ToRemove is T Object)) return;

        if (_list.Contains(Object))
            _list.Remove(Object);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_list).GetEnumerator();
    }
}
