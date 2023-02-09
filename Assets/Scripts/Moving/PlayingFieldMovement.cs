using System.Collections;
using UnityEngine;

namespace Moving
{
    [RequireComponent(typeof(RectTransform))]
    public class PlayingFieldMovement : MovableObject<RectTransform>
    {
        [Header("Centralizing")] 
        [SerializeField] private Vector2 newAnchoredPosition = new (0.0f, 400.0f);
        [SerializeField] private float smoothTime;
        
        private Vector2 _touchStart;
        private Vector2 _touchDelta;
        private bool _isDragging;

        protected override void Move(Touch touch)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStart = touch.position;
                    _isDragging = true;
                    break;
                case TouchPhase.Moved:
                    _touchDelta = touch.position - _touchStart;
                    if (_isDragging) Object.anchoredPosition += _touchDelta;
                    _touchStart = touch.position;
                    break;
                case TouchPhase.Ended:
                    _isDragging = false;
                    break;
            }
        }

        public void OnBubbleClick() => StartCoroutine(SmoothCentralize());

        private IEnumerator SmoothCentralize()
        {
            var startPosition = Object.anchoredPosition;
            for (float i = 0; i < 1.0f; i += Time.deltaTime / smoothTime)
            {
                Object.anchoredPosition = Vector2.Lerp(startPosition, newAnchoredPosition, i);
                yield return null;
            }
            Object.anchoredPosition = newAnchoredPosition;
        }
    }
}