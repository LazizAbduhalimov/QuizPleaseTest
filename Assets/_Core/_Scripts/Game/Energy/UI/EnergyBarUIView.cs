using Game.UI;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Energy.UI
{
    public sealed class EnergyBarUIView : UIView<EnergyBarUIViewModel>
    {
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Image _progressFill;
        [SerializeField] private Button _spendButton;

        public override void Initialize()
        {
            var d1 = ViewModel.Current.Subscribe(OnCurrentChanged);
            var d2 = ViewModel.SecondsToNext.Subscribe(OnProgressChanged);
            SetSubscriptions(Disposable.Combine(d1, d2));

            _spendButton.onClick.AddListener(OnSpend10Clicked);
        }

        public override void Release()
        {
            _spendButton.onClick.RemoveListener(OnSpend10Clicked);
            base.Release();
        }

        private void OnCurrentChanged(int current)
        {
            _valueText.text = $"{current} / {ViewModel.MaxEnergy}";
        }

        private void OnProgressChanged(float value)
        {
            _progressFill.fillAmount = value;
        }

        private void OnSpend10Clicked()
        {
            ViewModel.Spend10();
        }
    }
}
