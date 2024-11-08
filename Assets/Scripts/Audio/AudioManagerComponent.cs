using UniRx;
using UnityEngine;

/// <summary>
/// AudioSourceを管理するクラス
/// </summary>
public class AudioManagerComponent : MonoBehaviour
    {
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

        /// <summary>
        /// BGMのマスタ音量
        /// </summary>
        [SerializeField, Tooltip("BGMのマスタ音量")]
        private FloatReactiveProperty bgmMasterVolume = new FloatReactiveProperty(1.0f);
        public IReactiveProperty<float> BgmMasterVolume => bgmMasterVolume;
        
        /// <summary>
        /// SEのマスタ音量
        /// </summary>
        [SerializeField, Range(0.0f, 1.0f), Tooltip("SEのマスタ音量")]
        private FloatReactiveProperty seMasterVolume = new FloatReactiveProperty(1.0f);
        public IReactiveProperty<float> SeMasterVolume => seMasterVolume;
        
        public void Start()
        {
            //初期化
            _bgmAudioSourceSource = InitializeAudioSource(_bgmAudioSourceSource, true);
            _seAudioSourceSource = InitializeAudioSource(_seAudioSourceSource, false);

            bgmMasterVolume
                .Subscribe(volume=>_bgmAudioSourceSource.volume=volume)
                .AddTo(this.gameObject);
            
            seMasterVolume
                .Subscribe(volume=>_seAudioSourceSource.volume=volume)
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
            bgmMasterVolume.Value = Mathf.Clamp(volume,0.0f,0.1f);       
        }
        
        /// <summary>
        /// SEの音量を設定する
        /// </summary>
        /// <param name="volume">設定する音量</param>
        public void SetSoundEffectVolume(float volume)
        {
            seMasterVolume.Value = Mathf.Clamp(volume,0.0f,0.1f);            
        }
    }
