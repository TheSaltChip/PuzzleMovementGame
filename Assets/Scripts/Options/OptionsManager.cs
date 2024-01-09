using Autohand;
using Constants;
using UnityEngine;

namespace Options
{
    public class OptionsManager : MonoBehaviour
    {
        private void Start()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsNames.Turn))
                AutoHandPlayer.Instance.rotationType = (RotationType)PlayerPrefs.GetInt(PlayerPrefsNames.Turn);

            if (PlayerPrefs.HasKey(PlayerPrefsNames.TurnSpeed))
                AutoHandPlayer.Instance.smoothTurnSpeed = PlayerPrefs.GetFloat(PlayerPrefsNames.TurnSpeed);

            if (PlayerPrefs.HasKey(PlayerPrefsNames.SnapTurnAngle))
                AutoHandPlayer.Instance.snapTurnAngle = PlayerPrefs.GetFloat(PlayerPrefsNames.SnapTurnAngle);
        }

        public static void SetTurnOption(RotationType value)
        {
            AutoHandPlayer.Instance.rotationType = value;
            PlayerPrefs.SetInt(PlayerPrefsNames.Turn, (int)value);
        }

        public static void SetTurnSpeed(float smoothTurnSpeed)
        {
            AutoHandPlayer.Instance.smoothTurnSpeed = smoothTurnSpeed;
            PlayerPrefs.SetFloat(PlayerPrefsNames.TurnSpeed, smoothTurnSpeed);
        }

        public static void SetSnapTurnAngle(float snapTurnAngle)
        {
            AutoHandPlayer.Instance.snapTurnAngle = snapTurnAngle;
            PlayerPrefs.SetFloat(PlayerPrefsNames.SnapTurnAngle, snapTurnAngle);
        }

        public static void SaveToPlayerPrefs()
        {
            PlayerPrefs.Save();
        }

        public static RotationType RotationType => AutoHandPlayer.Instance.rotationType;
        public static float TurnSpeed => AutoHandPlayer.Instance.smoothTurnSpeed;
        public static float SnapAngle => AutoHandPlayer.Instance.snapTurnAngle;
    }
}