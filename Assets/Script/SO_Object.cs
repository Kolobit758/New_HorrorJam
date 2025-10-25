using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllGameObject",menuName = "GameOBJ/ new_gameobject_folder")]
public class SO_Object : ScriptableObject
{
    public List<StatusData> gameObjects = new List<StatusData>();
}
