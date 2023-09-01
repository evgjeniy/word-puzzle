using System.Linq;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Letters", menuName = "LettersDB")]
    public class LettersDb : ScriptableObject
    {
        [SerializeField] private Sprite[] spriteLetters;

        public Sprite FindSpriteByLetter(char letter)
        {
            var stringLetter = letter.ToString().ToUpper();

            return spriteLetters.FirstOrDefault(sprite => sprite.name == stringLetter);
        }
    }
}
