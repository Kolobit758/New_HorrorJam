using UnityEngine;

public class FinalSignScript : MonoBehaviour
{
    public SubmaryUIManager submaryUIManager;
    void Start()
    {
        submaryUIManager = FindFirstObjectByType<SubmaryUIManager>();
    }

    public void GameSubmary()
    {
        submaryUIManager.GameSubmary();
        Debug.Log("Press BTN");
    }
    public void Backhome()
    {
        submaryUIManager.BacktoHomeEnding();
        Debug.Log("Press BTN");
    }
}
