using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    private AudioClip currentClip;
    private bool isUsingSource1 = true;

    public void PlayWaveAudio(AudioClip newClip, float delay)
    {
        StartCoroutine(PlayAudioWithDelay(newClip, delay));
    }

    private IEnumerator PlayAudioWithDelay(AudioClip newClip, float delay)
    {
        AudioSource currentSource = isUsingSource1 ? audioSource1 : audioSource2;
        AudioSource nextSource = isUsingSource1 ? audioSource2 : audioSource1;

        if (currentSource.isPlaying)
        {
            yield return new WaitForSeconds(currentSource.clip.length - delay);
        }

        nextSource.clip = newClip;
        nextSource.loop = false; // Set loop ke false
        nextSource.Play();

        if (currentSource.isPlaying)
        {
            currentSource.Stop();
        }

        isUsingSource1 = !isUsingSource1;
    }

    public bool IsAudioPlaying()
    {
        return audioSource1.isPlaying || audioSource2.isPlaying;
    }
}
