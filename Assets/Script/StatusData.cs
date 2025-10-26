using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatusOBJ", menuName = "status/ new_object")]
public class StatusData : ScriptableObject
{
    public enum objectType
    {
        Item,
        Sign,
        structure,
        SignEvent
    }
    public objectType object_Type;
    public GameObject ObjectPrefab;
    public string objectName;
    [Header("Player change value")]
    public int moralStatsChange;
    public int insaneStatsChange;
    public int senseStatsChange;
    [Header("Requir Value")]
    public int reuquirMoralStats;
    public int reuquirInsaneStats;
    public int reuquirSenseStats;

    [Header("Sign")]
    public string signText;
    [Header("About Event")]
    public string eventName;

    [Header("Skill")]
    public ScriptableObject SO_Skill;
    
}
