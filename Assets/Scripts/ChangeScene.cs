using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int indexLevel;

    public void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
