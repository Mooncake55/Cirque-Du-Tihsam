using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManacherScene2 : MonoBehaviour
{

    AudioSource _audioSource;
    public AudioSource _cameraAudioSource2;
    public float time;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(SoundsChange());
        }
    }
    public IEnumerator SoundsChange()
    {
        yield return new WaitForSeconds(time);
        _audioSource.Play();
        _cameraAudioSource2.Stop();
    }
}
