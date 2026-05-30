using VContainer;
using VContainer.Unity;
using Game.Energy;
using Game.Energy.UI;
using UnityEngine;

namespace Game.Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private EnergySettings _energySettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_energySettings);

            builder.Register<EnergyService>(Lifetime.Singleton)
                .As<IEnergyService>()
                .As<IService>();

            builder.Register<EnergyBarUIViewModel>(Lifetime.Transient);

            builder.RegisterEntryPoint<ServicesInitializer>();
        }
    }
}
