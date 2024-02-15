using UnityEngine;
using Variables;

namespace Options
{
    [CreateAssetMenu(fileName = "GameOptions", menuName = "Options/GameOptions")]
    public class GameOptions : ScriptableObject
    {
        public IntVariable rotationType;
        public FloatVariable snapTurnAngle;
        public FloatVariable smoothTurnSpeed;

        public int RotationType
        {
            get => rotationType.value;
            set => rotationType.value = value;
        }

        public float SnapTurnAngle
        {
            get => snapTurnAngle.value;
            set => snapTurnAngle.value = value;
        }

        public float SmoothTurnSpeed
        {
            get => smoothTurnSpeed.value;
            set => smoothTurnSpeed.value = value;
        }
    }
}