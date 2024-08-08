using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombine : MonoBehaviour
{
    public GameObject combinedItemPrefab; // The prefab of the combined item
    public string[] otherItemNames; // Names of the other items

    private static HashSet<string> collidedItems = new HashSet<string>();
    private static List<GameObject> itemObjects = new List<GameObject>();

    void OnCollisionEnter(Collision collision)
    {
        string collidedItemName = collision.gameObject.name;

        // Check if the collided item is one of the required items
        if (System.Array.Exists(otherItemNames, name => name == collidedItemName) ||
            collidedItemName == gameObject.name)
        {
            collidedItems.Add(collidedItemName);
            itemObjects.Add(collision.gameObject);

            // Check if all items have collided
            if (collidedItems.Count == otherItemNames.Length + 1) // +1 for the current item
            {
                CombineItems();
            }
        }
    }

    private void CombineItems()
    {
        itemObjects.RemoveAll(item => item == null);
        if (itemObjects.Count < otherItemNames.Length + 1) return;

        // Find the average position of the collided items
        float spawnHeightOffset = 1f;
        Vector3 spawnPosition = Vector3.zero;
        foreach (var item in itemObjects)
        {
            spawnPosition += item.transform.position;
        }
        spawnPosition /= itemObjects.Count;

        // Adjust the Y position to spawn slightly above the average position
        spawnPosition.y += spawnHeightOffset;

        // Instantiate the combined item at the calculated position
        Instantiate(combinedItemPrefab, spawnPosition, Quaternion.identity);

        // Destroy the original items
        foreach (var item in itemObjects)
        {
            Destroy(item);
        }

        // Clear the set and list for future use
        collidedItems.Clear();
        itemObjects.Clear();
    }
}