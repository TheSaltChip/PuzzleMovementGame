using Autohand;
using Constants;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Variables;

namespace Options
{
    public class OptionsManager : MonoBehaviour
    {
        [SerializeField] private GameOptions gameOptions;

        public UnityEvent onOptionsChanged;

        private void Start()
        {
            gameOptions.SnapTurnAngle = AutoHandPlayer.Instance.snapTurnAngle;
            gameOptions.SmoothTurnSpeed = AutoHandPlayer.Instance.smoothTurnSpeed;
            gameOptions.RotationType = (int)AutoHandPlayer.Instance.rotationType;

            if (PlayerPrefs.HasKey(PlayerPrefsNames.Turn))
            {
                var rotType = PlayerPrefs.GetInt(PlayerPrefsNames.Turn);
                AutoHandPlayer.Instance.rotationType = (RotationType)rotType;
                gameOptions.RotationType = rotType;
            }

            if (PlayerPrefs.HasKey(PlayerPrefsNames.TurnSpeed))
            {
                var smoothTurnSpeed = PlayerPrefs.GetFloat(PlayerPrefsNames.TurnSpeed);
                AutoHandPlayer.Instance.smoothTurnSpeed = smoothTurnSpeed;
                gameOptions.SmoothTurnSpeed = smoothTurnSpeed;
            }

            if (!PlayerPrefs.HasKey(PlayerPrefsNames.SnapTurnAngle))
                return;

            var snapTurnAngle = PlayerPrefs.GetFloat(PlayerPrefsNames.SnapTurnAngle);
            AutoHandPlayer.Instance.snapTurnAngle = snapTurnAngle;
            gameOptions.SnapTurnAngle = snapTurnAngle;

            onOptionsChanged?.Invoke();
        }

        public void SetTurnOption(int value)
        {
            AutoHandPlayer.Instance.rotationType = (RotationType)value;
            PlayerPrefs.SetInt(PlayerPrefsNames.Turn, value);
            gameOptions.RotationType = value;
            SaveOptions();
        }

        public void SetTurnSpeed(float smoothTurnSpeed)
        {
            AutoHandPlayer.Instance.smoothTurnSpeed = smoothTurnSpeed;
            PlayerPrefs.SetFloat(PlayerPrefsNames.TurnSpeed, smoothTurnSpeed);
            gameOptions.SmoothTurnSpeed = smoothTurnSpeed;
            SaveOptions();
        }

        public void SetSnapTurnAngle(float snapTurnAngle)
        {
            AutoHandPlayer.Instance.snapTurnAngle = snapTurnAngle;
            PlayerPrefs.SetFloat(PlayerPrefsNames.SnapTurnAngle, snapTurnAngle);
            gameOptions.SnapTurnAngle = snapTurnAngle;
            SaveOptions();
        }

        private void SaveOptions(){
            PlayerPrefs.Save();
            onOptionsChanged?.Invoke();
        }
    }
}