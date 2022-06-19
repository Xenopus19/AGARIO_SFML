using System;

namespace Agario.GameObjects;
public class DeletableObject
{
    public Action<DeletableObject> OnDestroy;

    protected void InvokeDeleteEvent()
    {
        if (OnDestroy != null)
        {
            OnDestroy.Invoke(this);
        }
    }
}
