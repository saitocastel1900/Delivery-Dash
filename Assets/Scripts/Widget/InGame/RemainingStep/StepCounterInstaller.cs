using Zenject;

/// <summary>
/// StepCounterを注入する
/// </summary>
public class StepCounterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(StepCounterPresenter), typeof(IInitializable))
            .To<StepCounterPresenter>().AsCached()
            .NonLazy();
    }
}