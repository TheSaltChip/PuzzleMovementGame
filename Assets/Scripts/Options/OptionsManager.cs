using System;
using Autohand;
using UnityEngine;
using UnityEngine.Events;

namespace Options
{
    public class OptionsManager : MonoBehaviour
    {
        public static OptionsManager Instance;

        private XROptions _options;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _options = new XROptions(AutoHandPlayer.Instance);

            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.Turn))
                _options.turn = PlayerPrefs.GetInt(Constants.PlayerPrefsNames.Turn);

            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.TurnSpeed))
                _options.turnSpeed = PlayerPrefs.GetFloat(Constants.PlayerPrefsNames.TurnSpeed);

            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.SnapTurnAngle))
                _options.snapTurnAngle = PlayerPrefs.GetFloat(Constants.PlayerPrefsNames.SnapTurnAngle);

            DontDestroyOnLoad(gameObject);
        }

        public void SetOptions()
        {
            SetTurnOption();
            SetTurnSpeed();
            SetSnapTurnAngle();
        }

        public void SetTurnOption()
        {
            AutoHandPlayer.Instance.rotationType = _options.turn switch
            {
                1 => RotationType.smooth,
                _ => RotationType.snap
            };
        }

        public void SetTurnSpeed()
        {
            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.TurnSpeed))
                AutoHandPlayer.Instance.smoothTurnSpeed = PlayerPrefs.GetFloat(Constants.PlayerPrefsNames.TurnSpeed);
        }

        public void SetSnapTurnAngle()
        {
            if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.SnapTurnAngle))
                AutoHandPlayer.Instance.snapTurnAngle = PlayerPrefs.GetFloat(Constants.PlayerPrefsNames.SnapTurnAngle);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetFloat(Constants.PlayerPrefsNames.Turn, _options.turn);
            PlayerPrefs.SetFloat(Constants.PlayerPrefsNames.TurnSpeed, _options.turnSpeed);
            PlayerPrefs.SetFloat(Constants.PlayerPrefsNames.SnapTurnAngle, _options.snapTurnAngle);
            PlayerPrefs.Save();
        }
    }

    internal struct XROptions
    {
        public int turn;
        public float turnSpeed;
        public float snapTurnAngle;

        public XROptions(AutoHandPlayer instance)
        {
            turn = (int) instance.rotationType;
            turnSpeed = instance.smoothTurnSpeed;
            snapTurnAngle = instance.snapTurnAngle;
        }
    }
}