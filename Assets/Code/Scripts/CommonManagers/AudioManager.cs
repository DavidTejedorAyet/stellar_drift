using MelenitasDev.SoundsGood;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private Music backgroundMusic;
    private Sound soundEffect;

    public static AudioManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        backgroundMusic = new Music(Track.background);
        soundEffect = new Sound(SFX.point);
    }

    void Start() {
        Play();
    }
    public void Play() {
        backgroundMusic
            .SetLoop(true)
            .SetVolume(1)
        .Play();
        soundEffect
            .SetVolume(1)
            .SetSpatialSound(false);
    }
    public void ChangeMusicVolume(float volume) {
        backgroundMusic.ChangeVolume(volume);
    }

    public void ChangeEffectVolume(float volume) {
        soundEffect.ChangeVolume(volume);
    }

    public void PlaySoundEffect(SFX effect) {
        soundEffect.SetClip(effect);
        soundEffect.Play();
    }
}
