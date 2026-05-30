using Game.Energy.UI;
using UnityEngine;
using VContainer;

public sealed class EnergyBarUIBinder : MonoBehaviour
{
    [SerializeField] private EnergyBarUIView _view;
    
    private EnergyBarUIViewModel _vm;

    [Inject]
    public void Construct(EnergyBarUIViewModel vm)
    {
        _vm = vm;
    }

    private void Start()
    {
        _view.Construct(_vm);
        _view.Initialize();
    }

    private void OnDestroy()
    {
        _view.Release();
    }
}