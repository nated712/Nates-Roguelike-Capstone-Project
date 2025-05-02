using UnityEngine;

public class DetachedSoundPlayer : MonoBehaviour
{
    public AudioClip soundClip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool use3DSound = false;

void Awake()
{
    if (soundClip == null) return;

    GameObject tempSound = new GameObject("DetachedSound");
    tempSound.transform.position = transform.position;

    AudioSource aSource = tempSound.AddComponent<AudioSource>();
    aSource.clip = soundClip;
    aSource.volume = volume;
    aSource.pitch = Random.Range(pitch - .2f, pitch + 0.2f);  // Random pitch variation
    aSource.spatialBlend = use3DSound ? 1f : 0f;
    aSource.Play();

    Destroy(tempSound, soundClip.length / aSource.pitch);  // Use actual pitch
}

}
