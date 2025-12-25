using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource source;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicks(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
