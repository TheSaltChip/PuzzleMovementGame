using Autohand;
using Constants;
using Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetOptionFromUI : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown turnDropdown;
    public Button confirmButton;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);
        confirmButton.onClick.AddListener(OptionsManager.SaveToPlayerPrefs);

        if (PlayerPrefs.HasKey(PlayerPrefsNames.Turn))
            turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt(PlayerPrefsNames.Turn));
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetTurnPlayerPref(int value)
    {
        OptionsManager.SetTurnOption((RotationType)value);
    }

    public void SetTurnSpeedPlayerPref(float value)
    {
        OptionsManager.SetTurnSpeed(value);
    }

    public void SetSnapTurnAnglePlayerPref(float value)
    {
        OptionsManager.SetSnapTurnAngle(value);
    }
}