using R3;

namespace Game.Energy
{
    public interface IEnergyService : IService
    {
        ReadOnlyReactiveProperty<int> Current { get; }
        ReadOnlyReactiveProperty<float> SecondsToNext { get; }

        bool TrySpend(int amount);
    }
}
