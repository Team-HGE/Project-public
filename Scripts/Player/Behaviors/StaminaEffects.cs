using UnityEngine;
using UnityEngine.UI;

public class StaminaEffects : MonoBehaviour
{
    private RunEffect runEffect;
    public Image image; // ������ ������ �̹���
    public float value; // 100���� 0 ������ float ��

    private void Start()
    {
    }

    void Update()
    {
        if (runEffect != null)
        {
            // ValueController�� value ���� �����ɴϴ�.
            float value = runEffect.CurrentStamina;

            // Alpha ���� ����մϴ�. 100�� �� 1(������), 0�� �� 0(���� ����)
            float alpha = Mathf.Clamp(value / 100.0f, 0.0f, 1.0f);

            // �̹����� ���� ������ �����ɴϴ�
            Color color = image.color;

            // Alpha ���� �����մϴ�
            color.a = alpha;

            // ����� ������ �ٽ� �̹����� �����մϴ�
            image.color = color;
        }
    }
}