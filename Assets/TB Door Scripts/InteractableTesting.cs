using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTesting : MonoBehaviour
{
    public virtual void Interact(PlayerTesting thePlayer)
    {
        Debug.Log(gameObject.name + " was interacted with.");
    }
}
