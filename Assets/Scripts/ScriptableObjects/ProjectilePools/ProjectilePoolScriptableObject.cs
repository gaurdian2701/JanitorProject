using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectilePoolScriptableObject", menuName = "ScriptableObject/NewProjectilePool")]
public class ProjectilePoolScriptableObject : ScriptableObject
{
    public GameObject spawnablePrefab;
    public int listSize;
}
