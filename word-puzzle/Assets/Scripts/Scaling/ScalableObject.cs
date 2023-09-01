using UnityEngine;

namespace Scaling
{
    public abstract class ScalableObject<T> : MonoBehaviour
    {
        protected T Object;
        
        private void Awake() => Object = GetComponent<T>();
        
        private void OnEnable() => PlayerInput.OnDoubleTouchScreen += Scale;
        
        private void OnDisable() => PlayerInput.OnDoubleTouchScreen -= Scale;
        
        protected abstract void Scale(Touch firstTouch, Touch secondTouch);
    }
}