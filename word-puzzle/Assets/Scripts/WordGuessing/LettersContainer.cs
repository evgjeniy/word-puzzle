using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace WordGuessing
{
    // sorry for that multiple-responsibility class  ;(  
    // i will fix it, honestly...  :) 
    public class LettersContainer : MonoBehaviour
    {
        [SerializeField] private Text inputField;
        [SerializeField] private Text questionText;
        [SerializeField] private LetterImage letterImagePrefab;
        [SerializeField] private Keyboard keyboard;
        [SerializeField] private UnityEvent OnGameWin = new();

        private readonly List<LetterImage> _letterImages = new ();
        private int _currentLetterIndex;
        private string _wordToGuess;
    
        private void Start()
        {
            var lettersDb = Resources.Load<LettersDb>("DB/Letters");
            var questionsDb = Resources.Load<QuestionsDb>("DB/Questions");

            SetQuestion(questionsDb.GetRandomQuestion());
            InstantiateLetterImages(lettersDb);
            ShuffleLetterImages();
            keyboard.RecalculateKeyboardButtons(_wordToGuess);
        }

        private void SetQuestion(Question question)
        {
            questionText.text = question.question;
            _wordToGuess = question.answer.ToUpper();
            ClearInputField();
        }

        private void ClearInputField()
        {
            inputField.text = new string('●', _wordToGuess.Length);
            _currentLetterIndex = 0;
        }

        private void InstantiateLetterImages(LettersDb lettersDb)
        {
            foreach (var letter in _wordToGuess)
            {
                var newLetterImage = Instantiate(letterImagePrefab, transform);
            
                newLetterImage.Letter = letter;
                newLetterImage.Sprite = lettersDb.FindSpriteByLetter(letter);
            
                _letterImages.Add(newLetterImage);
            }
        }

        private void ShuffleLetterImages()
        {
            foreach (var letterImages in _letterImages)
                letterImages.transform.SetSiblingIndex(Random.Range(0, transform.childCount));
        }
    
        public void SetLetter(char letter, System.Action<char> oneLetterCallback = null)
        {
            if (_currentLetterIndex >= _wordToGuess.Length) return;
        
            ChangeImageActiveState(letter, false);
            var newInputField = new StringBuilder(inputField.text)
            {
                [_currentLetterIndex++] = letter
            };
        
            if (HasActiveLetterImages(letter)) oneLetterCallback?.Invoke(letter);
        
            inputField.text = newInputField.ToString();

            CheckIfWin();
        }

        public void OnBackSpaceEnter(System.Action<char> backspaceCallback = null)
        {
            if (_currentLetterIndex <= 0) return;
        
            var previousLetter = inputField.text[_currentLetterIndex - 1];
            backspaceCallback?.Invoke(previousLetter);
        
            ChangeImageActiveState(previousLetter, true);
            var newInputField = new StringBuilder(inputField.text)
            {
                [--_currentLetterIndex] = '●'
            };

            inputField.text = newInputField.ToString();
        }

        private void ChangeImageActiveState(char letter, bool activeState)
        {
            _letterImages
                .Where(imageLetter => imageLetter.gameObject.activeSelf != activeState)
                .FirstOrDefault(imageLetter => imageLetter.Letter == letter)?
                .gameObject.SetActive(activeState);
        }

        private bool HasActiveLetterImages(char letter)
        {
            return null == _letterImages
                .Where(imageLetter => imageLetter.gameObject.activeSelf)
                .FirstOrDefault(imageLetter => imageLetter.name[0] == letter);
        }

        private void CheckIfWin()
        {
            if (inputField.text.Contains('●')) return;

            if (inputField.text == _wordToGuess)
            {
                inputField.StartCoroutine(WinCoroutine());
            }
            else
            {
                keyboard.RecalculateKeyboardButtons(_wordToGuess);
                _letterImages.ForEach(letterImage => letterImage.gameObject.SetActive(true));
                ClearInputField();
            }
        }

        private IEnumerator WinCoroutine()
        {
            OnGameWin?.Invoke();

            yield return new WaitForSeconds(3.0f);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}