using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{

    Light testLight;
    public float minWaitTime;
    public float maxWaitTime;
    public Material material;
    public Color emissionColorOn = Color.white;
    public Color emissionColorOff = Color.black;
    public float emissionIntensity = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        testLight = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            testLight.enabled = !testLight.enabled;

            // Toggle the emission on the material
            if (testLight.enabled)
            {
                material.SetColor("_EmissionColor", emissionColorOn * emissionIntensity);
                material.EnableKeyword("_EMISSION");
            }
            else
            {
                material.SetColor("_EmissionColor", emissionColorOff);
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
