using UnityEngine;

[CreateAssetMenu(fileName = "AudioWave", menuName = "ScriptableObjects/AudioWave", order = 1)]
public class AudioWave : ScriptableObject
{
    public AudioClip audioClip; // Audio clip untuk wave ini
}
