using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItem : MonoBehaviour
{
    public GameObject targetObject1; // The specific GameObject to detect
    public GameObject targetObject2; // The specific GameObject to detect
    public GameObject itemToInstantiate; // The prefab to instantiate when the trigger is activated

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering GameObject is the target object
        if (other.gameObject == targetObject1 || other.gameObject == targetObject2)
        {
            // Instantiate the item at the trigger's position
            Instantiate(itemToInstantiate, other.transform.position, Quaternion.identity);

            // Destroy the detected target object
            if (other.gameObject == targetObject1)
            {
                Destroy(targetObject1);
            }
            else if (other.gameObject == targetObject2)
            {
                Destroy(targetObject2);
            }
        }
    }
}
