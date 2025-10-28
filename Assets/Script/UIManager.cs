using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject skillPaperPanel; // UI กระดาษ
    public TMP_Text skillText; // ข้อความบนกระดาษ

    private void Start()
    {
        skillPaperPanel.SetActive(false);
    }

    public void ShowSkillPaper(string text, float duration = 2f)
    {
        skillText.text = text;
        skillPaperPanel.SetActive(true);
        CancelInvoke(nameof(HideSkillPaper));
        Invoke(nameof(HideSkillPaper), duration);
    }

    private void HideSkillPaper()
    {
        skillPaperPanel.SetActive(false);
    }
}
