using Autohand;
using Constants;
using UnityEngine;
using UnityEngine.Events;

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

            var changed = false;

            if (PlayerPrefs.HasKey(PlayerPrefsNames.Turn))
            {
                var rotType = PlayerPrefs.GetInt(PlayerPrefsNames.Turn);
                gameOptions.RotationType = rotType;
                changed = true;
            }

            if (PlayerPrefs.HasKey(PlayerPrefsNames.TurnSpeed))
            {
                var smoothTurnSpeed = PlayerPrefs.GetFloat(PlayerPrefsNames.TurnSpeed);
                gameOptions.SmoothTurnSpeed = smoothTurnSpeed;
                changed = true;
            }

            if (PlayerPrefs.HasKey(PlayerPrefsNames.SnapTurnAngle))
            {
                var snapTurnAngle = PlayerPrefs.GetFloat(PlayerPrefsNames.SnapTurnAngle);
                gameOptions.SnapTurnAngle = snapTurnAngle;
                changed = true;
            }

            if (changed)
            {
                onOptionsChanged?.Invoke();
            }
        }

        public void SetTurnOption(int value)
        {
            PlayerPrefs.SetInt(PlayerPrefsNames.Turn, value);
            gameOptions.RotationType = value;

            SaveOptions();
        }

        public void SetTurnSpeed(float smoothTurnSpeed)
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.TurnSpeed, smoothTurnSpeed);
            gameOptions.SmoothTurnSpeed = smoothTurnSpeed;
            SaveOptions();
        }

        public void SetSnapTurnAngle(float snapTurnAngle)
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.SnapTurnAngle, snapTurnAngle);
            gameOptions.SnapTurnAngle = snapTurnAngle;
            SaveOptions();
        }

        private void SaveOptions()
        {
            PlayerPrefs.Save();
            onOptionsChanged?.Invoke();
        }
    }
}