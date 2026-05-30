using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Energy
{
    public interface IService
    {
        UniTask InitializeAsync(CancellationToken ct);
        UniTask ReleaseAsync(CancellationToken ct);
    }
}
