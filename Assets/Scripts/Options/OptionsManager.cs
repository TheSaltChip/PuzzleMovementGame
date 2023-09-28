using Autohand;
using UnityEngine;

namespace Options
{
    public class OptionsManager : MonoBehaviour
    {
        private void Awake()
        {
            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.Turn))
                AutoHandPlayer.Instance.rotationType = (RotationType)PlayerPrefs.GetInt(Constants.PlayerPrefsNames.Turn);

            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.TurnSpeed))
                AutoHandPlayer.Instance.smoothTurnSpeed = PlayerPrefs.GetFloat(Constants.PlayerPrefsNames.TurnSpeed);

            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.SnapTurnAngle))
                AutoHandPlayer.Instance.snapTurnAngle = PlayerPrefs.GetFloat(Constants.PlayerPrefsNames.SnapTurnAngle);
        }

        public static void SetTurnOption(RotationType value)
        {
            AutoHandPlayer.Instance.rotationType = value;
            PlayerPrefs.SetInt(Constants.PlayerPrefsNames.Turn, (int)value);
        }

        public static void SetTurnSpeed(float smoothTurnSpeed)
        {
            AutoHandPlayer.Instance.smoothTurnSpeed = smoothTurnSpeed;
            PlayerPrefs.SetFloat(Constants.PlayerPrefsNames.TurnSpeed, smoothTurnSpeed);
        }

        public static void SetSnapTurnAngle(float snapTurnAngle)
        {
            AutoHandPlayer.Instance.snapTurnAngle = snapTurnAngle;
            PlayerPrefs.SetFloat(Constants.PlayerPrefsNames.SnapTurnAngle, snapTurnAngle);
        }

        public static void SaveToPlayerPrefs()
        {
            PlayerPrefs.Save();
        }
    }
}