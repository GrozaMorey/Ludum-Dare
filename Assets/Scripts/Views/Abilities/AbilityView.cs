using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core.Abilities;
using TMPro;

namespace Core.UI
{
    public class AbilityView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _cooldownImage;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _manacostText;

        private Ability _ability;

        public event Action<Transform> PointerEnter;
        public event Action PointerExit;
        public event Action CooldownReady;

        private void OnAbilityCasted()
        {
            if (_ability.Effect == null) return;
            _ability.Effect.Execute();
        }

        public void SetData(ref Ability ability)
        {
            _ability = ability;
            _manacostText.text = ability.Manacost.ToString();
            _icon.sprite = ability.Icon;
            _button.onClick.AddListener(OnAbilityCasted);
        }
        public void StartCooldown()
        {
            StartCoroutine(CooldownCoroutine(_ability.Cooldown));
        }
        private IEnumerator CooldownCoroutine(float time)
        {
            _cooldownImage.gameObject.SetActive(true);
            _button.interactable = false;
            float factor = 0;
            while (factor < 1)
            {
                _cooldownImage.fillAmount = Mathf.Lerp(0, 1, factor += Time.deltaTime / time);
                yield return null;
            }
            _cooldownImage.gameObject.SetActive(false);
            _button.interactable = true;
            CooldownReady?.Invoke();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
     
        }
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
         
        }
    }
}