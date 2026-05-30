using Game.UI;
using R3;

namespace Game.Energy.UI
{
    public sealed class EnergyBarUIViewModel : IUIViewModel
    {
        private readonly IEnergyService _energyService;
        private readonly EnergySettings _settings;

        public EnergyBarUIViewModel(IEnergyService energyService, EnergySettings settings)
        {
            _energyService = energyService;
            _settings = settings;
        }

        public int MaxEnergy => _settings.MaxEnergy;
        public ReadOnlyReactiveProperty<int> Current => _energyService.Current;
        public ReadOnlyReactiveProperty<float> SecondsToNext => _energyService.SecondsToNext;

        public bool Spend10()
        {
            return _energyService.TrySpend(10);
        }
    }
}
