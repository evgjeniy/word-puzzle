using UnityEngine;

namespace Moving
{
    public abstract class MovableObject<T> : MonoBehaviour
    {
        protected T Object;
    
        private void Start() => Object = GetComponent<T>();

        private void OnEnable() => PlayerInput.OnSingleTouchScreen += Move;

        private void OnDisable() => PlayerInput.OnSingleTouchScreen -= Move;

        protected abstract void Move(Touch touch);
    }
}