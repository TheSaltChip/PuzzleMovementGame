using System;
using System.Collections;
using Autohand;
using Options;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SetOptionFromUI : MonoBehaviour
{
    [SerializeField] private int snapTurnFactor = 15;
    [SerializeField] private int smoothTurnFactor = 5;

    [SerializeField] private Slider volumeSlider;

    [SerializeField] private TMP_Dropdown turnDropdown;

    [SerializeField] private Slider smoothTurnSpeedSlider;
    [SerializeField] private Slider snapTurnAngleSlider;

    [SerializeField] private Button confirmButton;

    private void Start()
    {
        print("Hello");
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        confirmButton.onClick.AddListener(OptionsManager.SaveToPlayerPrefs);

        var rotationType = OptionsManager.RotationType;

        turnDropdown.SetValueWithoutNotify((int)rotationType);

        snapTurnAngleSlider.value = OptionsManager.SnapAngle / snapTurnFactor;
        snapTurnAngleSlider.onValueChanged.AddListener(SetSnapTurnAnglePlayer);

        smoothTurnSpeedSlider.value = OptionsManager.TurnSpeed / smoothTurnFactor;
        smoothTurnSpeedSlider.onValueChanged.AddListener(SetTurnSpeedPlayer);

        switch (rotationType)
        {
            case RotationType.snap:
                smoothTurnSpeedSlider.gameObject.SetActive(false);
                break;
            case RotationType.smooth:
                snapTurnAngleSlider.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetTurnPlayerPref(int value)
    {
        var rotationType = (RotationType)value;

        OptionsManager.SetTurnOption(rotationType);

        switch (rotationType)
        {
            case RotationType.snap:
                snapTurnAngleSlider.gameObject.SetActive(true);
                smoothTurnSpeedSlider.gameObject.SetActive(false);
                break;
            case RotationType.smooth:
                smoothTurnSpeedSlider.gameObject.SetActive(true);
                snapTurnAngleSlider.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetTurnSpeedPlayer(float value)
    {
        OptionsManager.SetTurnSpeed(value * smoothTurnFactor);
    }

    private void SetSnapTurnAnglePlayer(float value)
    {
        OptionsManager.SetSnapTurnAngle(value * snapTurnFactor);
    }
}