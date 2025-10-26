using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Notifier : MonoBehaviour
{
    public GameObject panel;        // ตัว Panel หลัก (มีพื้นหลัง + ข้อความ)
    public TMP_Text messageText;    // ข้อความ
    public float fadeInTime = 0.3f;
    public float showTime = 1.5f;
    public float fadeOutTime = 0.5f;

    private Image panelImage;
    private Coroutine routine;

    void Awake()
    {
        if (panel == null) panel = gameObject;
        panelImage = panel.GetComponent<Image>();
        panel.SetActive(false);
    }

    public void Show(string message)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {
        panel.SetActive(true);
        messageText.text = message;

        // เริ่มจากจางและเล็ก
        SetAlpha(0f);
        panel.transform.localScale = Vector3.one * 0.8f;

        // Fade In + ขยาย
        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(0f, 1f, t / fadeInTime);
            SetAlpha(a);
            panel.transform.localScale = Vector3.Lerp(Vector3.one * 0.8f, Vector3.one, t / fadeInTime);
            yield return null;
        }

        // รอค้างข้อความไว้
        yield return new WaitForSeconds(showTime);

        // Fade Out + หด
        t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(1f, 0f, t / fadeOutTime);
            SetAlpha(a);
            panel.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.8f, t / fadeOutTime);
            yield return null;
        }

        panel.SetActive(false);
    }

    private void SetAlpha(float a)
    {
        // เปลี่ยนความโปร่งใสของ panel + ข้อความ
        if (panelImage != null)
        {
            Color c = panelImage.color;
            c.a = a;
            panelImage.color = c;
        }

        if (messageText != null)
        {
            Color c = messageText.color;
            c.a = a;
            messageText.color = c;
        }
    }
}
