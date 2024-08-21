using UnityEngine;
public class GameSettings : MonoBehaviour
{
    public float volume = 1f;
    public float mouseSensitivity = 100f;
    public bool isFullScreen = true;

    public void SaveSettings() 
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("MouseSensitivityX", mouseSensitivity);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        

        PlayerPrefs.Save(); // ����
    }


    public void LoadSettings()
    {
        // �ʿ信 ���� ���� �ҷ����� ����� �߰��� �� ����
    }
}
