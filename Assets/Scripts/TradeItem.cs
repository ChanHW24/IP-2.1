using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItem : MonoBehaviour
{
    public GameObject targetObject1; // The specific GameObject to detect
    public GameObject targetObject2; // The specific GameObject to detect
    public GameObject itemToInstantiate; // The prefab to instantiate when the trigger is activated

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding GameObject is the target object
        if (collision.gameObject == targetObject1 || collision.gameObject == targetObject2)
        {
            // Instantiate the item at the collision's position
            Instantiate(itemToInstantiate, collision.transform.position, Quaternion.identity);

            // Destroy the detected target object
            if (collision.gameObject == targetObject1)
            {
                Destroy(targetObject1);
            }
            else if (collision.gameObject == targetObject2)
            {
                Destroy(targetObject2);
            }
        }
    }
}


    


