using System.Collections;
using System.Collections.Generic;
using Autohand;
using Options;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SetOptionFromUI : MonoBehaviour
{
    public Slider volumeSlider;
    public TMPro.TMP_Dropdown turnDropdown;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        if (PlayerPrefs.HasKey("turn"))
            turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("turn"));
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetTurnPlayerPref(int value)
    {
        PlayerPrefs.SetInt("turn", value);

        OptionsManager.Instance.SetTurnOption();
    }

    public void SetTurnSpeedPlayerPref(float value)
    {
        PlayerPrefs.SetFloat("turnSpeed", value);

        OptionsManager.Instance.SetTurnSpeed();
    }

    public void SetSnapTurnAnglePlayerPref(float value)
    {
        PlayerPrefs.SetFloat("snapTurnAngle", value);

        OptionsManager.Instance.SetSnapTurnAngle();
    }
}
