using UnityEngine;

public class Grabbable : MonoBehaviour      // not an abstract class in case there is a point in which i want to get rid of the grabbable layer and use this instead
{
    public virtual void OnPickup() { }

    public virtual void OnThrow() { }

    public virtual void OnDrop() { }

}
