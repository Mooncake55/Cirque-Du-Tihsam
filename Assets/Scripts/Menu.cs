using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private ChangeScene _changeScene;
    public int index;
    private void Start()
    {
        _changeScene = GetComponent<ChangeScene>();
    }
    public void Play()
    {
        _changeScene.ChangeLevel(index);
    }
    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
