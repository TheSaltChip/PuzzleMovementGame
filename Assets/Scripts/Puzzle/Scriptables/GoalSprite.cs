using UnityEngine;

namespace Puzzle.Scriptables
{
    [CreateAssetMenu(fileName = "GoalSprite", menuName = "Puzzle/GoalSprite", order = 0)]
    public class GoalSprite : ScriptableObject
    {
        public Sprite image;
    }
}