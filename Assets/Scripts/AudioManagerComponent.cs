using UnityEngine;

public class AudioManagerComponent : MonoBehaviour {

    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip loseSound;
    [SerializeField]
    private AudioSource audioSource;

    public static AudioManagerComponent Instance = null;

    public void Awake() {
        Instance = this;
    }

    public void PlayWinSound() {
        this.PlaySound(this.winSound);
    }

    public void PlayLoseSound() {
        this.PlaySound(this.loseSound);
    }

    private void PlaySound(AudioClip sound) {
        if (this.audioSource.isPlaying) {
            this.audioSource.Stop();
        }

        this.audioSource.clip = sound;
        this.audioSource.Play();
    }
}
