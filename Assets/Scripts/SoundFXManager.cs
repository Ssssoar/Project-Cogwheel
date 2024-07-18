using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    AudioSource audioSource;
    public static SoundFXManager instance;
    AudioSource toStop;
    float stopTimer = -1f;

    private void Awake()
    {
        if (SoundFXManager.instance != null) Destroy(gameObject);
        else instance = this;

        audioSource = GetComponent<AudioSource>();
    }
    public void ReproducirSFX(LibreriaDeSonidos lib)
    {
        audioSource.PlayOneShot(lib.clip);
    }

    public void ReproducirSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void ReproducirDetenible(AudioClip clip, float timer){
        if (clip == null) return;
        toStop = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        toStop.clip = clip;
        stopTimer = timer;
        toStop.volume = 0.5f;
        toStop.pitch = 1 + Random.Range(-0.2f,0.2f);
        toStop.Play();
    }

    void Update(){
        if (stopTimer == -1f) return;
        stopTimer -= Time.deltaTime;
        if (stopTimer <= 0){
            stopTimer = -1;
            Destroy(toStop);
        }
    }
}
