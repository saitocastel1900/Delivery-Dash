using Zenject;

namespace Widget.InGame.StageNumber
{
    /// <summary>
    /// ステージ番号を注入する
    /// </summary>
    public class StageNumberInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(StageNumberPresenter), typeof(IInitializable)).To<StageNumberPresenter>().AsCached()
                .NonLazy();
        }
    }
}