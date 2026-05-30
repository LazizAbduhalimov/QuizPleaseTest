using UnityEngine;

namespace Game.Energy
{
    [CreateAssetMenu(menuName = "Game/Energy Settings", fileName = "EnergySettings")]
    public sealed class EnergySettings : ScriptableObject
    {
        public int MaxEnergy = 100;
        public float RegenSeconds = 1f;
    }
}
