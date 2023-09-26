using Options;
using UnityEngine;
using UnityEngine.UI;

public class SetOptionFromUI : MonoBehaviour
{
    public Slider volumeSlider;
    public TMPro.TMP_Dropdown turnDropdown;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        if (PlayerPrefs.HasKey(Constants.PlayerPrefsNames.Turn))
            turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt(Constants.PlayerPrefsNames.Turn));
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetTurnPlayerPref(int value)
    {
        OptionsManager.Instance.SetTurnOption();
    }

    public void SetTurnSpeedPlayerPref(float value)
    {
        OptionsManager.Instance.SetTurnSpeed();
    }

    public void SetSnapTurnAnglePlayerPref(float value)
    {
        OptionsManager.Instance.SetSnapTurnAngle();
    }
}