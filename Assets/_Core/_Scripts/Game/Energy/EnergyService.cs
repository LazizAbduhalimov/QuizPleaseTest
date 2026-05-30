using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Game.Energy
{
    public sealed class EnergyService : IEnergyService
    {
        public ReadOnlyReactiveProperty<int> Current => _current;
        public ReadOnlyReactiveProperty<float> SecondsToNext => _secondsToNext;

        private readonly EnergySettings _settings;

        private readonly ReactiveProperty<int> _current;
        private readonly ReactiveProperty<float> _secondsToNext;

        private CancellationTokenSource _regenCts;
        private UniTask _regenTask;

        public EnergyService(EnergySettings settings)
        {
            _settings = settings;

            _current = new ReactiveProperty<int>(_settings.MaxEnergy);
            _secondsToNext = new ReactiveProperty<float>(0f);
        }

        public UniTask InitializeAsync(CancellationToken ct)
        {
            Debug.Log("EnergyService: InitializeAsync");
            _regenCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            _regenTask = RunRegenLoopAsync(_regenCts.Token);
            return UniTask.CompletedTask;
        }

        public async UniTask ReleaseAsync(CancellationToken ct)
        {
            if (_regenCts != null)
            {
                _regenCts.Cancel();
                try
                {
                    await _regenTask;
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("EnergyService: Regen loop cancelled [cause: Release]");
                }

                _regenCts.Dispose();
                _regenCts = null;
            }

            _current.Dispose();
            _secondsToNext.Dispose();
        }

        public bool TrySpend(int amount)
        {
            if (amount <= 0) return false;
            if (_current.Value < amount) return false;

            _current.Value -= amount;
            return true;
        }

        private async UniTask RunRegenLoopAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    if (_current.Value >= _settings.MaxEnergy)
                    {
                        _secondsToNext.Value = 0f;
                        await UniTask.WaitUntil(() => _current.Value < _settings.MaxEnergy, cancellationToken: ct);
                        continue;
                    }

                    var elapsed = 0f;
                    var step = 0.1f;

                    while (elapsed < _settings.RegenSeconds)
                    {
                        await UniTask.Delay(100, cancellationToken: ct);
                        elapsed += step;
                        _secondsToNext.Value = Mathf.Clamp01(elapsed / _settings.RegenSeconds);
                    }

                    _current.Value = Mathf.Min(_current.Value + 1, _settings.MaxEnergy);
                    _secondsToNext.Value = 0f;
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("EnergyService: Regen loop cancelled");
            }
        }
    }
}
