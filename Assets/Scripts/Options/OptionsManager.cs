using System;
using Autohand;
using UnityEngine;
using UnityEngine.Events;

namespace Options
{
    public class OptionsManager : MonoBehaviour
    {
        public static OptionsManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

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
            if (PlayerPrefs.HasKey("turn"))
                AutoHandPlayer.Instance.rotationType = (PlayerPrefs.GetInt("turn")) switch
                {
                    1 => RotationType.smooth,
                    _ => RotationType.snap
                };
        }

        public void SetTurnSpeed()
        {
            if (PlayerPrefs.HasKey("turnSpeed"))
                AutoHandPlayer.Instance.smoothTurnSpeed = PlayerPrefs.GetFloat("turnSpeed");
        }

        public void SetSnapTurnAngle()
        {
            if (PlayerPrefs.HasKey("snapTurnAngle"))
                AutoHandPlayer.Instance.snapTurnAngle = PlayerPrefs.GetFloat("snapTurnAngle");
        }
    }
}