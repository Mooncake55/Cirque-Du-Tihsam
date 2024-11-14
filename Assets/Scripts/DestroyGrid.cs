using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGrid : MonoBehaviour
{
    public GameObject grid;
    public float timeDestroyGrid;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(DestroyGridFunction());
        }
    }
    public IEnumerator DestroyGridFunction()
    {
        yield return new WaitForSeconds(timeDestroyGrid);
        Destroy(grid);
    }        

}
