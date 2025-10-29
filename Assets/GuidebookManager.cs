using System.Collections.Generic;
using UnityEngine;

public class GuidebookManager : MonoBehaviour
{
    public GameObject guidebook;
    public List<GameObject> page = new List<GameObject>();
    public GameObject thispage;
    [SerializeField] int currentPage = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thispage = page[currentPage];
    }

    public void OpenGuideBook()
    {
        guidebook.SetActive(true);
        thispage.SetActive(true);
    }
    public void nextPage()
    {
        thispage.SetActive(false);
        currentPage++;
        if(currentPage > page.Count -1)
        {
            currentPage = 0;
        }
        thispage = page[currentPage];
        thispage.SetActive(true);
    }
    public void CloseGuideBook()
    {
        guidebook.SetActive(false);
        thispage.SetActive(false);
    }
}
