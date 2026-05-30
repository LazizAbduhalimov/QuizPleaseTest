using UnityEngine;

namespace Game.UI
{
    public abstract class UIView : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Release();
    }
}
