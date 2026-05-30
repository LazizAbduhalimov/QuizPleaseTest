using System;

namespace Game.UI
{
    public abstract class UIView<TVm> : UIView
        where TVm : class, IUIViewModel
    {
        protected TVm ViewModel;
        private IDisposable _subscriptions;

        public void Construct(TVm viewModel)
        {
            ViewModel = viewModel;
        }

        protected void SetSubscriptions(IDisposable subscriptions)
        {
            _subscriptions?.Dispose();
            _subscriptions = subscriptions;
        }

        public override void Release()
        {
            _subscriptions?.Dispose();
            _subscriptions = null;
        }
    }
}
