using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SL
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectController : MonoBehaviour
    {
        AudioSource audio_Source;

        // Use this for initialization
        void Start()
        {
            audio_Source = GetComponent<AudioSource>();
            audio_Source.mute = !SoundManager.EffectOn;
            SoundManager.OnSoundSettingChange += SoundManager_OnSoundSettingChange;
        }

        void SoundManager_OnSoundSettingChange()
        {
            audio_Source.mute = !SoundManager.EffectOn;
        }

        public void OnDestroy()
        {
            SoundManager.OnSoundSettingChange -= SoundManager_OnSoundSettingChange;
        }
    }
}
