using UnityEngine;

public class ObjectSound1 : MonoBehaviour
{
    public AudioSource audioSource; // ����� �Ҹ� Ŭ��
    public float maxDistance = 30f; // �Ҹ��� ����Ǵ� �ִ� �Ÿ�
    
    private Transform playerTransform;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾��� Transform ��������
    }

    void Update()
    {
        // �÷��̾�� ������Ʈ ������ �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // �÷��̾ ������Ʈ ��ó�� ������ �Ҹ� ���
        if (distanceToPlayer <= maxDistance)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // �Ҹ� ���
            }

             audioSource.volume = 0.6f - (distanceToPlayer / maxDistance);
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // �÷��̾ �־����� �Ҹ� ����
            }
        }
    }
}