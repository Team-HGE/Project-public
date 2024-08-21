using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public RunEffect runEffect; // RunEffect ��ũ��Ʈ ����
    public Image fillImage; // Fill �̹��� ����

    private float timeAtMaxStamina;
    private bool isAtMaxStamina;
    private RectTransform rectTransform;

    void Start()
    {
        runEffect = GetComponent<RunEffect>();
        if (runEffect != null)
        {
            staminaSlider.maxValue = runEffect.MaxStamina;
            staminaSlider.value = runEffect.CurrentStamina;
        }
        timeAtMaxStamina = -1f; // �ʱ�ȭ
        isAtMaxStamina = false;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (runEffect != null)
        {
            float currentStamina = runEffect.CurrentStamina;
            staminaSlider.value = currentStamina;
            float fillPercentage = currentStamina / runEffect.MaxStamina;

           
            if (fillPercentage > 0.5f)
            {
                // 60% �̻��� �� �ʷϻ����� ��������� ���������� ����
                fillImage.color = Color.Lerp(Color.yellow, Color.green, (fillPercentage - 0.5f) / 0.3f);
            }
            else if (fillPercentage > 0.2f)
            {
                // 30%���� 60% ������ �� ��������� ���������� ���������� ����
                fillImage.color = Color.Lerp(Color.red, Color.yellow, (fillPercentage - 0.2f) / 0.2f);
            }

            // ���׹̳��� �ִ�ġ�� �������� �� Ÿ�̸� ����
            if (currentStamina == runEffect.MaxStamina)
            {
                if (!isAtMaxStamina)
                {
                    isAtMaxStamina = true;
                    timeAtMaxStamina = Time.time;
                }

                // 2�� ���� �ִ�ġ�� �����Ǹ� ���׹̳� �� �����
                if (isAtMaxStamina && Time.time - timeAtMaxStamina >= 2f)
                {
                    rectTransform.localScale = Vector3.zero; // ���׹̳� �� �����
                }
            }
            else
            {
                // ���׹̳��� �ִ�ġ�� �ƴϸ� Ÿ�̸� �ʱ�ȭ
                isAtMaxStamina = false;
                if (rectTransform.localScale == Vector3.zero)
                {
                    rectTransform.localScale = Vector3.one; // ���׹̳� �� �ٽ� ���̰� ����
                }
            }
        }
    }
}