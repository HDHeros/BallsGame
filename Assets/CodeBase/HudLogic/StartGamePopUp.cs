using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HudLogic
{
    public class StartGamePopUp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreAmountText;
        [SerializeField] private Button _startButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        private Action _startButtonClickCallback;
        
        public void Show(IScore score, Action onStartBtnClickCallback)
        {
            SetupCanvasGroup(true);
            _scoreAmountText.SetText(Mathf.Round(score.GetScore()).ToString(CultureInfo.InvariantCulture));
            _startButtonClickCallback = onStartBtnClickCallback;
        }

        public void Hide()
        {
            SetupCanvasGroup(false);
            _startButtonClickCallback = null;
        }

        private void SetupCanvasGroup(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.interactable = isActive;
        }

        private void OnEnable() => 
            _startButton.onClick.AddListener(OnStartButtonClick);

        private void OnDisable() => 
            _startButton.onClick.RemoveListener(OnStartButtonClick);

        private void OnStartButtonClick() => 
            _startButtonClickCallback?.Invoke();
    }
}