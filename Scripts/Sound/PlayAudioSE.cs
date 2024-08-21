using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
public class PlayAudioSE : MonoBehaviour
{
    
    public float volume = 0.6f;
    private List<AudioSource> activeAudioSources = new List<AudioSource>();
    public void PlayAudioCIip(AudioClip clip)
    {  
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        activeAudioSources.Add(audioSource);
        StartCoroutine(DestroyAfterPlayback(audioSource, clip.length));

    }

    public void PlayDialSE(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        activeAudioSources.Add(audioSource);

    }

    public void StopAudioCIip(AudioClip clip)
    {
        for (int i = activeAudioSources.Count - 1; i >= 0; i--)
        {
            AudioSource audioSource = activeAudioSources[i];
            if (audioSource.clip == clip)
            {
                audioSource.Stop();
                Destroy(audioSource);
                activeAudioSources.RemoveAt(i);


            }

        }
    }
        public void StopDialSE(AudioClip clip)
        {
            // ����Ʈ���� �ش� Ŭ���� ��� ���� ����� �ҽ��� ã�Ƽ� ���� �� ����
            for (int i = activeAudioSources.Count - 1; i >= 0; i--)
            {
                AudioSource audioSource = activeAudioSources[i];
                if (audioSource.clip == clip)
                {
                    audioSource.Stop();
                    Destroy(audioSource);
                    activeAudioSources.RemoveAt(i);
                }
            }
        }
         private IEnumerator DestroyAfterPlayback(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (source != null && !source.isPlaying)
        {
            Destroy(source);
            activeAudioSources.Remove(source);
        }
    }
}