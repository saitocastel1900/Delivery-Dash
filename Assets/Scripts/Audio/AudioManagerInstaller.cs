using UnityEngine;
using Zenject;

/// <summary>
/// AudioManagerを注入する
/// </summary>
public class AudioManagerInstaller : MonoInstaller
{
    /// <summary>
    /// AudioManager
    /// </summary>
    [SerializeField] private AudioManager audioManager;
        
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInNewPrefab(audioManager).AsSingle();
    }
}
