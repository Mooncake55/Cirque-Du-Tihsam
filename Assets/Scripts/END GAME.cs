using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENDGAME : MonoBehaviour
{
    public float timeChange;
    public int index;
    private ChangeScene _changeScene;

    private void Start()
    {
        _changeScene = GetComponent<ChangeScene>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(ChangeToScene());
        }
    }

    private IEnumerator ChangeToScene()
    {
        yield return new WaitForSeconds(timeChange);

        _changeScene.ChangeLevel(index);
    }
}
