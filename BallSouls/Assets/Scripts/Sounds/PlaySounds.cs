using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] List<AudioSource> audios;

    AudioSource _nowPlaing;

    public void PlaySound(string soundName)
    {
        var sound = audios.Where(x => x.clip.name.ToLower() == soundName.ToLower()).FirstOrDefault();

        sound.Play();
    }

    public void PlayRandomSound(string partName)
    {
        var sound = audios.Where(x => x.clip.name.ToLower().Contains(partName.ToLower())).ToList();

        sound[Random.Range(0, sound.Count())].Play();
    }

    public void PlayRandomSound()
    {
        _nowPlaing = audios[Random.Range(0, audios.Count())];

        _nowPlaing.Play();
    }

    public bool IsPlayedInCollection()
    {
        if (_nowPlaing.isPlaying)
            return true;
        else
            return false;
    }
}
