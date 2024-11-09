using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToScene2 : MonoBehaviour
{
    public int timeChange;
    public int index;

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

        ChangeLevel(index);
    }
    public void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
