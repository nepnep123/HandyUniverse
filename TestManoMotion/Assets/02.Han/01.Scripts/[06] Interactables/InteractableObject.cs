using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public virtual void ProcessInit<T>(T obj) { }
    public virtual void ProcessCollisionEnter() { }
    public virtual void ProcessCollisionExit() { }
    public virtual void ProcessPick() { }
    public virtual void ProcessDrop() { }
    public virtual void ProcessGrab() { }
    public virtual void ProcessRelease() { }
}
