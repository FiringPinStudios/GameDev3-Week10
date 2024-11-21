using GameDevWithMarco.DataSO;
using UnityEngine;

namespace GameDevWithMarco.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        /// <summary>
        /// This script will drive anything related to Audio
        /// </summary>

        [SerializeField] AudioClip backgroundMusic;
        [SerializeField] SoundSO goodPickupSoundData;
        [SerializeField] SoundSO badPickupSoundData;
        [SerializeField] SoundSO dashSoundData  ;
        [SerializeField] SoundSO lifeSoundData;
        [SerializeField] AudioSource audioSource_Music;
        [SerializeField] AudioSource audioSource_Sounds;

        // Start is called before the first frame update
        void Start()
        {
            if (backgroundMusic != null)
            {
                PlayBackgroundMusic();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (backgroundMusic != null)
            {
                audioSource_Music.volume += 0.1f * Time.deltaTime;
                if (audioSource_Music.volume >= 0.2f)
                {
                    audioSource_Music.volume = 0.2f;
                }
            }
        }

        private void PlaySound (float lowPitchRange, float highPitchRange, AudioClip clipToPlay, float volume)
        {
            audioSource_Sounds.pitch = Random.Range(lowPitchRange, highPitchRange);
            audioSource_Sounds.PlayOneShot(clipToPlay);
            audioSource_Sounds.volume = volume;
        }


        public void GoodPickupSound()
        {
            PlaySound(goodPickupSoundData.minPitchValue, goodPickupSoundData.maxPitchValue, 
                goodPickupSoundData.clipToUse, goodPickupSoundData.soundVolume);
        }
        public void BadPickupSound()
        {
            PlaySound(badPickupSoundData.minPitchValue, badPickupSoundData.maxPitchValue,
                badPickupSoundData.clipToUse, badPickupSoundData.soundVolume);
        }
        public void PlayBackgroundMusic()
        {
            audioSource_Music.clip = backgroundMusic;
            audioSource_Music.volume = 0f;
            audioSource_Music.loop = true;
        }
        public void Dash()
        {
            PlaySound(dashSoundData.minPitchValue, dashSoundData.maxPitchValue,
                dashSoundData.clipToUse, dashSoundData.soundVolume);
        }
        public void LifePickupSound()
        {
            PlaySound(lifeSoundData.minPitchValue, lifeSoundData.maxPitchValue,
                lifeSoundData.clipToUse, lifeSoundData.soundVolume);
        }
    }
}