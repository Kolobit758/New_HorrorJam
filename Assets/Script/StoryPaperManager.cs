using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StoryPaperManager : MonoBehaviour
{
    public List<string> storyText = new List<string>();
    public List<string> copyStoryText;
    [SerializeField]List<string> textHasFound = new List<string>();
    private string textReturn;


    public GameObject paper;
    public TextMeshProUGUI papertext;

    void Start()
    {
        paper.SetActive(false);
        copyStoryText = new List<string>(storyText);
    }

    public string RandomText()
    {
        int random = Random.Range(0, copyStoryText.Count);
        textReturn = copyStoryText[random];
        copyStoryText.RemoveAt(random);

        return textReturn;

    }
    public void OpenStoryPaper()
    {
        papertext.text = RandomText();
        textHasFound.Add(papertext.text);
        paper.SetActive(true);
        Debug.Log("Open");
      
        
    }
    public void CloseStoryPaper()
    {
        paper.SetActive(false);
    }
}
