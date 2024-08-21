using System.Collections;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from the GameObject.");
        }
    }

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance, float addVolume, float transitionTime, float pitch)
    {
        if (audioSource == null) return;

        CancelInvoke();
        audioSource.clip = clip;
        audioSource.volume = soundEffectVolume + addVolume;
        audioSource.pitch = soundEffectPitchVariance + Random.Range(-0.2f, 0.2f) + pitch;
        audioSource.Play();

        // Disable �޼��带 Ŭ�� ���� + �߰� ���� �ð� �Ŀ� ȣ��
        // disableDelay ���ڸ� �߰��Ͽ� ������Ʈ�� ��Ȱ��ȭ�ϱ� ���� ����� �ð��� �����ϰ� ����
        //Invoke("Disable", clip.length + 0.1f + transitionTime);
        StartCoroutine(Disable(clip.length + 0.1f + transitionTime));
    }

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance, float addVolume, float transitionTime, float pitch, bool loop)
    {
        if (audioSource == null) return;

        CancelInvoke();
        audioSource.clip = clip;
        audioSource.volume = soundEffectVolume + addVolume;
        audioSource.pitch = soundEffectPitchVariance + Random.Range(-0.2f, 0.2f) + pitch;
        audioSource.Play();

        if(loop) audioSource.loop = true;

        StartCoroutine(Disable(transitionTime));
    }

    IEnumerator Disable(float time)
    {
        yield return new WaitForSeconds(time);

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        gameObject.SetActive(false);
    }

    //public void Disable()
    //{
    //    if (audioSource != null)
    //    {
    //        audioSource.Stop();
    //    }

    //    gameObject.SetActive(false);
    //}
}
