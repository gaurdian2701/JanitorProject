using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "ScriptableObject/NewProjectile")]
public class ProjectileScriptableObject : ScriptableObject
{
    public Sprite projectileSprite;
    public ObjectState startingState;
    public ProjectilePooledType projectilePooledType;
    public ProjectileType projectileType;
    public Status statusApplied;
    public float shrinkValue;
    public float shootSpeed;
    [Range(10, 100)]
    public float damageAmount;
}
