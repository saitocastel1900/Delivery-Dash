using Zenject;

namespace Commons.Save
{
    /// <summary>
    /// セーブマネージャーを注入する
    /// </summary>
    public class SaveManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveManager>().AsSingle().NonLazy();
        }
    }
}