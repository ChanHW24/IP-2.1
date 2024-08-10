//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class KeyCardTesting : InteractableTesting
//{
//    /// <summary>
//    /// The door that this key card unlocks
//    /// </summary>
//    public Door linkedDoor;

//    private void Start()
//    {
//        // Check if there is a linked door
//        if (linkedDoor != null)
//        {
//            // Lock the door that is linked
//            linkedDoor.SetLock(true);
//        }
//    }

//    protected virtual void Collected()
//    {
//        // Destroy the attached GameObject
//        Destroy(gameObject);
//    }

//    public override void Interact(PlayerTesting thePlayer)
//    {
//        base.Interact(thePlayer);
//        //thePlayer.IncreaseScore(myScore);
//        //GameManager.instance.IncreaseScore(myScore);
//        Collected();
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardTesting : InteractableTesting
{
    /// <summary>
    /// The door that this key card unlocks
    /// </summary>
    public DoorTesting linkedDoor;

    private void Start()
    {
        // Check if there is a linked door
        if (linkedDoor != null)
        {
            // Lock the door that is linked
            linkedDoor.SetLock(true);
        }
    }

    protected virtual void Collected()
    {
        if (linkedDoor != null)
        {
            linkedDoor.SetLock(false);
        }

        // Destroy the attached GameObject
        Destroy(gameObject);
    }

    public override void Interact(PlayerTesting thePlayer)
    {
        base.Interact(thePlayer);
        Collected();
    }
}
