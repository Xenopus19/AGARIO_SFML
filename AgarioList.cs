using System;

namespace Agario;
public class AgarioList<T> 
{
    private List<T> _list;

    public AgarioList()
    {
        _list = new List<T>();
    }

    public List<T> list
    {
        get { return _list; }
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

}
