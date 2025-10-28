using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/StoryPaperSkill")]
public class StoryPaperSkill : ScriptableObject, ISkill
{
    public StoryPaperManager storyPaperManager;
    public void UseItem()
    {
        storyPaperManager = FindFirstObjectByType<StoryPaperManager>();
        Debug.Log("Open");
        storyPaperManager.OpenStoryPaper();

    }
}
