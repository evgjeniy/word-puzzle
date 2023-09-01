using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Questions", menuName = "QuestionsDB")]
    public class QuestionsDb : ScriptableObject
    {
        [SerializeField] private Question[] questions;

        public Question GetRandomQuestion() => questions[Random.Range(0, questions.Length)];
    }
}