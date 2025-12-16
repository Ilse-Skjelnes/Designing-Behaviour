using NUnit.Framework;
using UnityEngine;
using static Unity.VisualScripting.Member;
using System.Collections.Generic;

public class PlayerDeathHandler : MonoBehaviour
{

    public List<AudioClip> clips = new List<AudioClip>();
    public AudioSource source;
    private void HandleDeath()
    {
        PlaySound();
        GameManager.Instance.NotifyPlayerDeath();
    }

    private void PlaySound()
    {
        int i = Random.Range(0, clips.Count + 1);
        source.clip = clips[i];
        source.Play();
    }
}
