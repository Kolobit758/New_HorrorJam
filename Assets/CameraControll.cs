using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CameraControll : MonoBehaviour
{
    [Header("CAMERA POSITION")]
    public GameObject cameraObj;
    public List<Transform> campos = new List<Transform>();

    [Header("SETTING")]
    public int currentPos; // 1 2 3 middle is 2
    public Button leftBtn;
    public Button rightBtn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPos = 1;
    }

    public void GoLeft()
    {
        if (currentPos <= 0) return;
        currentPos--;

        Vector3 targetPos = campos[currentPos].position;
        targetPos.z = -10;
        cameraObj.transform.position = targetPos;

        leftBtn.interactable = currentPos > 0;
        rightBtn.interactable = currentPos < campos.Count - 1;
    }

    public void GoRight()
    {
        if (currentPos >= campos.Count - 1) return;
        currentPos++;

        Vector3 targetPos = campos[currentPos].position;
        targetPos.z = -10;
        cameraObj.transform.position = targetPos;

        leftBtn.interactable = currentPos > 0;
        rightBtn.interactable = currentPos < campos.Count - 1;
    }


}
