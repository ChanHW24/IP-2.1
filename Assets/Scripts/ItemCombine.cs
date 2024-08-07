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
        // Find the average position of the collided items
        Vector3 spawnPosition = Vector3.zero;
        foreach (var item in itemObjects)
        {
            spawnPosition += item.transform.position;
        }
        spawnPosition /= itemObjects.Count;

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

    /*
    public GameObject combinedItemPrefab; // The prefab of the combined item
    public string[] otherItemNames; // Names of the other items

    private HashSet<string> collidedItems = new HashSet<string>();
    private List<GameObject> itemObjects = new List<GameObject>();

    void OnCollisionEnter(Collision collision)
    {
        string collidedItemName = collision.gameObject.name;

        // Check if the collided item is one of the required items
        if (System.Array.Exists(otherItemNames, name => name == collidedItemName) ||
            collidedItemName == gameObject.name)
        {
            // Only add the item if it's not null
            if (collision.gameObject != null)
            {
                collidedItems.Add(collidedItemName);
                itemObjects.Add(collision.gameObject);
            }

            // Check if all items have collided
            if (collidedItems.Count == otherItemNames.Length + 1) // +1 for the current item
            {
                CombineItems();
            }
        }
    }

    private void CombineItems()
    {
        // Find the average position of the collided items
        Vector3 spawnPosition = Vector3.zero;
        int validItemCount = 0; // Count valid (non-null) items

        foreach (var item in itemObjects)
        {
            if (item != null)
            {
                spawnPosition += item.transform.position;
                validItemCount++;
            }
        }

        if (validItemCount > 0)
        {
            spawnPosition /= validItemCount;

            // Instantiate the combined item at the calculated position
            Instantiate(combinedItemPrefab, spawnPosition, Quaternion.identity);

            // Destroy the original items
            foreach (var item in itemObjects)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
        }

        // Clear the set and list for future use
        collidedItems.Clear();
        itemObjects.Clear();
    }
    */
}