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
        if (gameObject is T)
            _list.Add((T)gameObject);
    }


    public void TryRemoveFromCollection(object ToRemove)
    {
        if (!(ToRemove is T)) return;

        if (_list.Contains((T)ToRemove))
            _list.Remove((T)ToRemove);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_list).GetEnumerator();
    }
}
