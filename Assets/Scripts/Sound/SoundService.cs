using Roguelike.Utilities;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Roguelike.Sound
{
    public class SoundService : IService
    {
        private SoundScriptableObject _audioList;
        private AudioSource _audioSourcePlayer;
        private AudioSource _audioSourceEnemy;
        private AudioSource _audioSourceSFX;
        private AudioSource _audioSourceBGM;
        private AudioSource _audioSourceRadialReap;
        private AudioSource _audioSourceOrbitalFury;
        private AudioSource _audioSourceScatterShot;
        private AudioSource _audioSourceEnemyProjectile;

        public SoundService(SoundScriptableObject audioList, List<AudioSource> audioSourceList)
        {
            this._audioList = audioList;
            _audioSourceSFX = audioSourceList[0];
            _audioSourceBGM = audioSourceList[1];
            _audioSourcePlayer = audioSourceList[2];
            _audioSourceEnemy = audioSourceList[3];
           _audioSourceRadialReap = audioSourceList[4];
        _audioSourceOrbitalFury = audioSourceList[5];
        _audioSourceScatterShot = audioSourceList[6];
        _audioSourceEnemyProjectile = audioSourceList[7];
        }

        public void Initialize(params object[] dependencies) {}

        public void PlaySFX(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                _audioSourceSFX.loop = loopSound;
                _audioSourceSFX.clip = clip;
                _audioSourceSFX.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        public void PlayPlayerSFX(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                _audioSourcePlayer.loop = loopSound;
                _audioSourcePlayer.clip = clip;
                _audioSourcePlayer.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        public void PlayEnemySFX(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                _audioSourceEnemy.loop = loopSound;
                _audioSourceEnemy.clip = clip;
                _audioSourceEnemy.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        public void PlayBGM(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                _audioSourceBGM.loop = loopSound;
                _audioSourceBGM.clip = clip;
                _audioSourceBGM.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }


        public void PlayWeaponSFX(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                switch(soundType)
                {
                    case SoundType.RadialReapGrow:
                        _audioSourceRadialReap.loop = loopSound;
                        _audioSourceRadialReap.clip = clip;
                        _audioSourceRadialReap.PlayOneShot(clip);
                        break;
                    case SoundType.RadialReapShrink:
                        _audioSourceRadialReap.loop = loopSound;
                        _audioSourceRadialReap.clip = clip;
                        _audioSourceRadialReap.PlayOneShot(clip);
                        break;
                    case SoundType.OrbitalFury:
                        _audioSourceOrbitalFury.loop = loopSound;
                        _audioSourceOrbitalFury.clip = clip;
                        _audioSourceOrbitalFury.PlayOneShot(clip);
                        break;
                    case SoundType.ScatterShot:
                        _audioSourceScatterShot.loop = loopSound;
                        _audioSourceScatterShot.clip = clip;
                        _audioSourceScatterShot.PlayOneShot(clip);
                        break;
                    case SoundType.EnemyProjectile:
                        _audioSourceEnemyProjectile.loop = loopSound;
                        _audioSourceEnemyProjectile.clip = clip;
                        _audioSourceEnemyProjectile.PlayOneShot(clip);
                        break;
                }
    
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(_audioList.audioList, item => item.soundType == soundType);
            if (sound.audio != null)
                return sound.audio;
            return null;
        }
    }
}