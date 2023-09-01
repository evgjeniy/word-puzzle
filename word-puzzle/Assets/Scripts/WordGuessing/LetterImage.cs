using UnityEngine;
using UnityEngine.UI;

namespace WordGuessing
{
    [RequireComponent(typeof(Image))]
    public class LetterImage : MonoBehaviour
    {
        private char _letter;
        private Image _image;

        public char Letter
        {
            get => _letter;
            set
            {
                _letter = value;
                gameObject.name = value.ToString().ToUpper();
            }
        }

        public Sprite Sprite
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }
    
        private void Awake() => _image = GetComponent<Image>();
    }
}