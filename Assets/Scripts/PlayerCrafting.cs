using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask interactLayerMask;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
        {
            float interactDistance = 3f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit,interactDistance, interactLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out CraftingAnvil craftingAnvil))
                {
                    // Interacting with Crafting Anvil
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        craftingAnvil.NextRecipe();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        craftingAnvil.Craft();
                    }
                }
            }
        }
    }
    
}
