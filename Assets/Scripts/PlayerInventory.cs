using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int bloonInventory;
    // Start is called before the first frame update
    void Start()
    {
        bloonInventory = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Bloon_Collect();
    }

    public void Bloon_Collect()
    {
        if (bloonInventory != 0)
        {
            Debug.Log($"You have a {bloonInventory} bloons.");
        }
    }
}
