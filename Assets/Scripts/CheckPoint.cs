using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private PlayerController pController;

    private void Start()
    {
        pController = GetComponent<PlayerController>();
    }

}
