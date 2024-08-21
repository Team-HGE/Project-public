using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public Button fullscreenToggleButton;
    public TextMeshProUGUI fullscreentext;


    void Start()
    {
        // ���� �������� �����̴��� ��ۿ� ����
        volumeSlider.value = GameDataSaveLoadManager.Instance.gameSettings.volume;
        sensitivitySlider.value = GameDataSaveLoadManager.Instance.gameSettings.mouseSensitivity;
        fullscreenToggleButton.onClick.AddListener(ToggleFullScreen);
        

        // �̺�Ʈ ������ ����
        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        
    }

    public void SetVolume(float volume)
    {
        GameDataSaveLoadManager.Instance.gameSettings.volume = volume;
        AudioListener.volume = volume; // ���� ���� ������ �ݿ�
        GameDataSaveLoadManager.Instance.gameSettings.SaveSettings(); // ���� ������ ����
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        GameDataSaveLoadManager.Instance.gameSettings.mouseSensitivity = sensitivity;

        // ���콺 ������ �÷��̾� ��Ʈ�ѷ��� �ݿ� (����)
        //playerController.mouseSensitivity = sensitivity;
        GameDataSaveLoadManager.Instance.gameSettings.SaveSettings();
    }


    void ToggleFullScreen()
    {
        GameDataSaveLoadManager.Instance.gameSettings.isFullScreen = !GameDataSaveLoadManager.Instance.gameSettings.isFullScreen;

        if (GameDataSaveLoadManager.Instance.gameSettings.isFullScreen)
        {
            // ��üȭ�� ���� ��ȯ
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            GameDataSaveLoadManager.Instance.gameSettings.SaveSettings();
        }
        else
        {
            // â ���� ��ȯ
            Screen.SetResolution(1920, 1080, false);
            GameDataSaveLoadManager.Instance.gameSettings.SaveSettings();
        }
    }

    void UpdateFullScreenButtonText()
    {
        // ���� ��üȭ�� ���¿� ���� ��ư �ؽ�Ʈ ����
        if (Screen.fullScreen == true)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "on";
        }
        else if (Screen.fullScreen == false)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "off";
        }
    }

}