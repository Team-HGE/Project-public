public interface IInteractable
{
    bool isInteractable { get; set; }

    void Interact(); // ��ȣ�ۿ��� ������Ʈ�� ���� ����

    void ActivateInteraction(); // �޼��� ���
}