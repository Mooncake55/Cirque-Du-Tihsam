using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffLights : MonoBehaviour
{
    public GameObject lights;
    public float time;
    public bool on;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(LightSwitch(on));
        }
    }
    public IEnumerator LightSwitch(bool on)
    {
        yield return new WaitForSeconds(time);
        lights.SetActive(on);
    }
}
   
    
