using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    public AudioClip footSlowClip;
    public AudioClip footFastClip;

    private AudioSource audioSource;
    private CharacterController characterController;
    private bool isRunning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // �÷��̾ �����̴��� Ȯ��
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            // Shift Ű�� ���� �޸��� �������� Ȯ��
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isRunning || audioSource.clip != footFastClip)
                {
                    // �޸��� ����
                    isRunning = true;
                    audioSource.clip = footFastClip;
                    audioSource.pitch = 0.6f; // ��� �ӵ� 40% ������ ����
                    audioSource.loop = true; // ���� ����
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
            }
            else
            {
                if (isRunning || audioSource.clip != footSlowClip)
                {
                    // �ȱ� ����
                    isRunning = false;
                    audioSource.clip = footSlowClip;
                    audioSource.pitch = 0.6f; // ��� �ӵ� 40% ������ ����
                    audioSource.loop = true; // ���� ����
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
                else if (!audioSource.isPlaying)
                {
                    // �Ȱ� ������ �Ҹ��� ������� �ʴ� ��� (��: ó�� �ȱ� ������ ��)
                    audioSource.clip = footSlowClip;
                    audioSource.pitch = 0.6f; // ��� �ӵ� 40% ������ ����
                    audioSource.loop = true; // ���� ����
                    audioSource.Play();
                }
            }
        }
        else
        {
            // �÷��̾ ���߸� �Ҹ� ����
            audioSource.Stop();
        }
    }
}