using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlataform : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
    Collider2D Collider;
    public Collider2D ColliderTrigger;
    private bool _regen = false;
    public float regenWaitingTime;
    public float desapearWaitingTime;

    // Start is called before the first frame update
    void Start()
    {   
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && _regen == false)
        {
            StartCoroutine(DesapearPlataform());           
        }
    }
    private IEnumerator RegenPlataform()
    {
        yield return new WaitForSeconds(regenWaitingTime);
        _regen = false;
        SpriteRenderer.enabled = true;
        Collider.enabled = true;
        ColliderTrigger.enabled = true;
    }
    private IEnumerator DesapearPlataform()
    {
        yield return new WaitForSeconds(desapearWaitingTime);            
        SpriteRenderer.enabled = false;
        Collider.enabled = false;
        ColliderTrigger.enabled = false;
        _regen = true;
            StartCoroutine(RegenPlataform());
    }

}

