using Game.ParticleEffects;
using Zenject;

namespace Game.Core
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IVisualEffectsContainer>()
                .To<VisualEffectsContainer>()
                .AsSingle();
        }
    }
}
