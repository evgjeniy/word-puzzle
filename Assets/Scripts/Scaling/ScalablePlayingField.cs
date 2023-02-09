using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaling
{
    [RequireComponent(typeof(RectTransform))]
    public class ScalablePlayingField : ScalableObject<RectTransform>
    {
        [SerializeField] private float scaleSpeed = 5.0f;
        [SerializeField] private float minScale = 0.5f;
        [SerializeField] private float maxScale = 3.0f;
        [SerializeField] private bool invertScaleControl;

        [Header("CentralizeScaling")] 
        [SerializeField] private float scaleTime = 0.5f;
        [SerializeField] private float newScaleValue = 1.5f;

        protected override void Scale(Touch firstTouch, Touch secondTouch)
        {
            var firstTouchPrevPosition = firstTouch.position - firstTouch.deltaPosition;
            var secondTouchPrevPosition = secondTouch.position - secondTouch.deltaPosition;

            var previousTouchDeltaMagnitude = (firstTouchPrevPosition - secondTouchPrevPosition).magnitude;
            var currentTouchDeltaMagnitude = (firstTouch.position - secondTouch.position).magnitude;

            var deltaMagnitude = previousTouchDeltaMagnitude - currentTouchDeltaMagnitude;
            deltaMagnitude *= scaleSpeed * (invertScaleControl ? -1 : 1) * Time.deltaTime;

            var newZoom = Object.localScale.x - deltaMagnitude * scaleSpeed * Time.deltaTime;
            newZoom = Mathf.Clamp(newZoom, minScale, maxScale);

            Object.localScale = new Vector3(newZoom, newZoom, 1.0f);
        }

        public void OnBubbleClick() => StartCoroutine(SmoothCentralize());

        private IEnumerator SmoothCentralize()
        {
            var startScaleValue = Object.localScale.x;
            for (float i = 0; i < 1.0f; i += Time.deltaTime / scaleTime)
            {
                var scaleValue = Mathf.Lerp(startScaleValue, newScaleValue, i);
                Object.localScale = new Vector3(scaleValue, scaleValue, 1.0f);
                yield return null;
            }
            Object.localScale = new Vector3(newScaleValue, newScaleValue, 1.0f);
        }
    }
}