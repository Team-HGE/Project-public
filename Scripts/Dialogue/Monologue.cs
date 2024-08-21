using System.Collections;
using UnityEngine;
public class Monologue : DialogueSetting, IScript
{
    private ScriptSO scriptSO; // ���� private�� ��������
    private string text;
    public Quest quest;
    public SystemMsg systemMsg;

    public void Init(ScriptSO _script)
    {
        quest = DialogueManager.Instance.quest;
        systemMsg = DialogueManager.Instance.systemMsg;
        scriptSO = null;
        scriptSO = _script;
        InitUI();
        ui.CloseDialogue();
    }

    public void Manual(string _text)
    {
        // ��ũ��Ʈ ��� ���̸� �� ��ȭ�� �������� ����, ��ȭ ���� �ʱ�ȭ
        if (isTalking) { Debug.Log("������ ��ȭ�� �� �����ϴ�."); InitDialogueSetting(); return; }
        isTalking = true;

        scriptSO = null;
        text = _text;
        InitUI();
        //ui.CloseDialogue();

        StopAllCoroutines();
        ui.OpenDialogue();
        StartCoroutine(PrintMonologue());
    }

    public void Print()
    {
        // ��ũ��Ʈ ��� ���̸� �� ��ȭ�� �������� ����, ��ȭ ���� �ʱ�ȭ
        if (isTalking) { Debug.Log("������ ��ȭ�� �� �����ϴ�."); InitDialogueSetting(); return; }
        isTalking = true;

        //Init(scriptSO);
        if (scriptSO == null) { Debug.Log("������ ������ ��ũ��Ʈ�� �����ϴ�. scriptSO null"); return; };

        StopAllCoroutines();
        ui.OpenDialogue();
        StartCoroutine(PrintScript());
    }

    private IEnumerator PrintScript()
    {

        ui.ClearDialogue(sbTitle, sbBody);

        for (int i = 0; i < scriptSO.bodyTexts.Length; i++)
        {
            UtilSB.SetText(ui.titleText, sbTitle, scriptSO.speakers[i]);
            ui.CheckNullIndex(scriptSO.speakers[i]);

            switch (scriptSO.bodyTexts[i])
            {

                case var text when text.StartsWith("NewQuest"):
                    yield return HandleNewQuest(text);
                    continue;

                case var text when text.StartsWith("SystemMsg"):
                    yield return SystemMsg(text);
                    continue;

                case var text when text.StartsWith("NewTip"):
                    yield return Tips(text);
                    continue;

                default:
                    curPrintLine = TextEffect.Typing(ui.bodyText, sbBody, scriptSO.bodyTexts[i]);
                    break;
            }
            yield return StartCoroutine(curPrintLine);

            //Debug.Log("��Ŭ������ �����ϼ���");
            yield return waitLeftClick;
            yield return waitTime;

            ui.ClearDialogue(sbTitle, sbBody);
        }

        ui.CloseDialogue();
        isTalking = false;

        yield return null;
    }

    private IEnumerator HandleNewQuest(string questText)
    {
        if (questText.StartsWith("NewQuest"))
        {
            string questString = questText.Substring(8);
            if (int.TryParse(questString, out int questNumber))
            {
                quest.NextQuest(questNumber);
            }

            yield break;
        }
    }

    private IEnumerator Tips(string TipText)
    {
        if (TipText.StartsWith("NewTip"))
        {
            string TipString = TipText.Substring(6);
            if (int.TryParse(TipString, out int TipsNumber))
            {
                systemMsg.UpdateTipMessage(TipsNumber);
            }
        }
        yield break;
    }

    private IEnumerator SystemMsg(string SystemMsgText)
    {
        if (SystemMsgText.StartsWith("SystemMsg"))
        {
            string SystemMsgString = SystemMsgText.Substring(9);
            if (int.TryParse(SystemMsgString, out int SystemMsgNumber))
            {
                systemMsg.UpdateMessage(SystemMsgNumber);
            }
        }

        yield break;
    }
    private IEnumerator PrintMonologue()
    {
        ui.ClearDialogue(sbTitle, sbBody);

        UtilSB.SetText(ui.titleText, sbTitle, null);
        ui.CheckNullIndex(null);

        TextEffect.Typing(ui.bodyText, sbBody, text);
        //yield return StartCoroutine(curPrintLine);

        //Debug.Log("��Ŭ������ �����ϼ���");
        yield return waitLeftClick;
        yield return waitTime;

        ui.ClearDialogue(sbTitle, sbBody);
        ui.CloseDialogue();
        isTalking = false;

        yield return null;
    }
}