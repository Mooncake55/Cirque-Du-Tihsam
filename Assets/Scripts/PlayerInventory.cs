using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int bloonInventory;
    public List<string> keysInventory = new List<string>();
    

    // Start is called before the first frame update
    void Start()
    {
        bloonInventory = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Bloon_Collect();
        Key_Count();
    }

    public void Bloon_Collect()
    {
        if (bloonInventory != 0)
        {
            Debug.Log($"You have a {bloonInventory} bloons.");
        }
    }
    public void Key_Collect(string key_name)
    {
        keysInventory.Add(key_name);
    }
    public void Key_Count()
    {
        if (keysInventory.Count != 0)
        {
            Debug.Log($"You have a {keysInventory.Count} keys.");
        }
    }
}
