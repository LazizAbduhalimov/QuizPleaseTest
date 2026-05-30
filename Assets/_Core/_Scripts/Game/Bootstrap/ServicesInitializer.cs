using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Game.Energy
{
    public sealed class ServicesInitializer : IStartable, IDisposable
    {
        private readonly IEnergyService _energyService;
        private CancellationTokenSource _cts;

        public ServicesInitializer(IEnergyService energyService)
        {
            _energyService = energyService;
        }

        public void Start()
        {
            _cts = new CancellationTokenSource();
            StartAsync(_cts.Token).Forget();
        }

        public void Dispose()
        {
            if (_cts == null) return;

            _cts.Cancel();
            StopAsync().Forget();
        }

        private async UniTask StartAsync(CancellationToken ct)
        {
            // В отличии от остановки я предпочитаю явный порядок инициализации сервисов
            // Контроль для меня более важнее чем автоматическаий инит сервисов
            await _energyService.InitializeAsync(ct);
        }

        private async UniTask StopAsync()
        {
            // Тут в целом можно автоматизировать но для консистентности оставил явный порядок релиза сервисов
            await _energyService.ReleaseAsync(CancellationToken.None);

            _cts.Dispose();
            _cts = null;
        }
    }
}
