using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "ScriptableObject/NewProjectile")]
public class ProjectileScriptableObject : ScriptableObject
{
    public Sprite projectileSprite;
    public ObjectState startingState;
    public ProjectileType projectileType;
    public float shrinkValue;
    public float shootSpeed;
}
