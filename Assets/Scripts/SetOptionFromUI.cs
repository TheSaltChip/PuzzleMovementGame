using System;
using System.Collections;
using Autohand;
using Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetOptionFromUI : MonoBehaviour
{
    [SerializeField] private int SnapTurnConstant = 15;
    [SerializeField] private int SmoothTurnConstant = 5;

    public Slider volumeSlider;

    public TMP_Dropdown turnDropdown;

    public Slider smoothTurnSpeedSlider;
    public Slider snapTurnAngleSlider;

    public Button confirmButton;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        confirmButton.onClick.AddListener(OptionsManager.SaveToPlayerPrefs);

        var rotationType = OptionsManager.RotationType;

        turnDropdown.SetValueWithoutNotify((int)rotationType);

        snapTurnAngleSlider.value = OptionsManager.SnapAngle / SnapTurnConstant;
        snapTurnAngleSlider.onValueChanged.AddListener(SetSnapTurnAnglePlayer);

        smoothTurnSpeedSlider.value = OptionsManager.TurnSpeed / SmoothTurnConstant;
        smoothTurnSpeedSlider.onValueChanged.AddListener(SetTurnSpeedPlayer);
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
        OptionsManager.SetTurnSpeed(value * SmoothTurnConstant);
    }

    private void SetSnapTurnAnglePlayer(float value)
    {
        OptionsManager.SetSnapTurnAngle(value * SnapTurnConstant);
    }
}