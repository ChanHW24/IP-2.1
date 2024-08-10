using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public GameObject screwDriver;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == screwDriver)
        {
            Destroy(gameObject);
        }
    }
}
