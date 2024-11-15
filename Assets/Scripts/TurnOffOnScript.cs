using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnScript : MonoBehaviour
{
    public EnemyAI script;
    public AILerp AILerp;
    public float time;
    public bool on;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(ScriptSwitch(on));
        }
    }
    public IEnumerator ScriptSwitch(bool on)
    {
        yield return new WaitForSeconds(time);
        script.enabled = on;
        AILerp.enabled = on;
    }

}
