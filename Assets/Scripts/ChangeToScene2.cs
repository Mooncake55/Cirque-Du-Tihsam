using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToScene2 : MonoBehaviour
{
    public float timeChange;
    public int index;
    private ChangeScene _changeScene;
    public GameObject grid;
    public float timeDestroyGrid;
    public GameObject lights;

    private void Start()
    {
        _changeScene = GetComponent<ChangeScene>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(DestroyGrid());
            StartCoroutine(ChangeToScene());
        }
    }

    private IEnumerator ChangeToScene()
    {
        yield return new WaitForSeconds(timeChange);

        _changeScene.ChangeLevel(index);
    }

    private IEnumerator DestroyGrid()
    {
        yield return new WaitForSeconds(timeDestroyGrid);
        Destroy(grid);
        lights.SetActive(true);

    }
}
