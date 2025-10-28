using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/RecoverySkill")]
public class RecoverySkill : ScriptableObject, ISkill
{
    public enum RecoverySkillType
    {
        moralStats,
        insaneStats,
        senseStats,
        moralAndinsane,
        moralAndSanse,
        insaneStatsAndSense,
        allStats
    }
    public RecoverySkillType recoverySkillType;
    public StatusData statusData;
    public void UseItem()
    {
        var player = FindAnyObjectByType<PlayerManager>();
        player.ChangePlayerStatus(statusData);

    }
}
