﻿using UniRx;
using UnityEngine;

/// <summary>
/// AudioSourceを管理するクラス
/// </summary>
public class AudioManagerComponent : MonoBehaviour
    {
        /// <summary>
        /// BGMのマスタ音量
        /// </summary>
        public IReactiveProperty<float> BgmMasterVolume => bgmMasterVolume;
        [SerializeField, Tooltip("BGMのマスタ音量")]
        private FloatReactiveProperty bgmMasterVolume = new FloatReactiveProperty(1.0f);
        
        /// <summary>
        /// SEのマスタ音量
        /// </summary>
        public IReactiveProperty<float> SeMasterVolume => seMasterVolume;
        [SerializeField, Tooltip("SEのマスタ音量")]
        private FloatReactiveProperty seMasterVolume = new FloatReactiveProperty(1.0f);
        
        /// <summary>
        /// BGMを再生するか
        /// </summary>
        public IReactiveProperty<bool> IsPlayBgm => _isPlayBgm;
        private BoolReactiveProperty _isPlayBgm = new BoolReactiveProperty(true);
        
        /// <summary>
        /// SEを再生するか
        /// </summary>
        public IReactiveProperty<bool> IsPlaySe => _isPlaySe;
        private BoolReactiveProperty _isPlaySe = new BoolReactiveProperty(true);
        
        /// <summary>
        /// BGMのAudioSource
        /// </summary>
        [SerializeField] private AudioSource _bgmAudioSourceSource;

        /// <summary>
        /// SEのAudioSource
        /// </summary>
        [SerializeField] private AudioSource _seAudioSourceSource;

        /// <summary>
        /// BGMのデータベース
        /// </summary>
        [SerializeField] private BgmDataBase _bgmDataBase;

        /// <summary>
        /// SEのデータベース
        /// </summary>
        [SerializeField] private SoundEffectDataBase _soundEffectDataBase;
        
        /// <summary>
        /// マスタ-音量
        /// </summary>
        [SerializeField, Tooltip("マスタ-音量")]
        private float masterVolume = 1.0f;
        
        private void Start()
        {
            //初期化
            _bgmAudioSourceSource = InitializeAudioSource(_bgmAudioSourceSource, true);
            _seAudioSourceSource = InitializeAudioSource(_seAudioSourceSource, false);

            //スライダーをいじったら、音量を変える
            bgmMasterVolume
                .Subscribe(volume=>_bgmAudioSourceSource.volume=volume)
                .AddTo(this.gameObject);
            
            seMasterVolume
                .Subscribe(volume=>_seAudioSourceSource.volume=volume)
                .AddTo(this.gameObject);

            //音量のオン・オフを切り替える
            _isPlayBgm
                .Select(x=>!x)
                .Subscribe(x=>_bgmAudioSourceSource.mute=x)
                .AddTo(this.gameObject);
            
            _isPlaySe
                .Select(x=>!x)
                .Subscribe(x=>_seAudioSourceSource.mute=x)
                .AddTo(this.gameObject);
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private AudioSource InitializeAudioSource(AudioSource audioSource, bool isLoop = false)
        {
            audioSource.loop = isLoop;
            audioSource.playOnAwake = false;

            return audioSource;
        }

        /// <summary>
        /// BGMを流す
        /// </summary>
        public void PlayBGM(BgmData.BGM bgm)
        {
            var bgmData = _bgmDataBase.GetBgmData(bgm);
            
            if (bgmData == null)
            {
                DebugUtility.Log(bgm + "は見つかりません");
                return;
            }
            
            //音の調整
            _bgmAudioSourceSource.volume = Mathf.Clamp(bgmData.volume * bgmMasterVolume.Value * masterVolume, 0.0f, 1.0f);
            
            _bgmAudioSourceSource.Play(bgmData.audioClip);
        }

        /// <summary>
        /// SEを流す
        /// </summary>
        /// <param name="soundEffect">流したいSE</param>
        public void PlaySoundEffect(SoundEffectData.SoundEffect soundEffect)
        {
            var soundEffectData = _soundEffectDataBase.GetSoundEffectData(soundEffect);
            
            if (soundEffectData == null)
            {
                DebugUtility.Log(soundEffect + "は見つかりません");
                return;
            }
            
            //音の調整
            _seAudioSourceSource.volume = Mathf.Clamp(soundEffectData.volume * seMasterVolume.Value * masterVolume, 0.0f, 1.0f);
            
            _seAudioSourceSource.PlayOneShot(soundEffectData.audioClip);
        }

        /// <summary>
        /// BGMの音量を設定する
        /// </summary>
        /// <param name="volume">設定する音量</param>
        public void SetBgmVolume(float volume)
        {
            bgmMasterVolume.Value = Mathf.Clamp(volume,0.0f,1.0f);       
        }
       
        /// <summary>
        /// SEの音量を設定する
        /// </summary>
        /// <param name="volume">設定する音量</param>
        public void SetSoundEffectVolume(float volume)
        {
            seMasterVolume.Value = Mathf.Clamp(volume,0.0f,1.0f);            
        }
        
        /// <summary>
        /// BGMのオン・オフを切り替える
        /// </summary>
        public void SwitchPlayBgmState()
        {
            _isPlayBgm.Value = !_isPlayBgm.Value;
        }
        
        /// <summary>
        /// SEのオン・オフを切り替える
        /// </summary>
        public void SwitchPlaySeState()
        {
            _isPlaySe.Value = !_isPlaySe.Value;
        }
    }
