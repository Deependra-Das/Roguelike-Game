using UnityEngine;
using System.Collections;
using TMPro;
using Roguelike.Main;

namespace Roguelike.DamageNumber
{
    public class DamageNumberView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageNumberText;
        [SerializeField] private float _textFloatSpeed ;
        [SerializeField] private float _lifeTime;

        private Vector3 _initialPosition;
        private Vector3 _targetPosition;

        public DamageNumberController _controller { get; private set; }

        public void SetController(DamageNumberController controllerToSet) => _controller = controllerToSet;

        public void PlayTextAnimation(int value)
        {
            _damageNumberText.text = value.ToString();
            _initialPosition = transform.position;
            _targetPosition = _initialPosition + Vector3.up * _textFloatSpeed;
            StartCoroutine(AnimateTextMovement());
            StartCoroutine(DisableObjectAfterAnimation());
        }

        private IEnumerator AnimateTextMovement()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _lifeTime)
            {
                transform.position = Vector3.Lerp(_initialPosition, _targetPosition, elapsedTime / _lifeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = _targetPosition;
        }

        private IEnumerator DisableObjectAfterAnimation()
        {
            yield return new WaitForSeconds(_lifeTime);
            _controller.ReturnToPool();
        }
    }

}

