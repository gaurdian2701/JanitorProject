using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckableObjectStateManager : MonoBehaviour
{
    [SerializeField] ProjectileScriptableObject projectileSO;
    [SerializeField] private GameObject hitbox;

    private SuckableBase currentState;
    private ObjectState startingState;
    private Vector3 shrinkRate;

    private ProjectilePooledType projectilePooledType;
    private ProjectileType projectileType;
    private SpriteRenderer spriteRenderer;

    private Vector3 originalSize = new Vector3(1f, 1f, 1f);
    private float shootSpeed;
    private int usabilityIndex;
    private GameObject launcher;
    private Transform shootPosition;
    private Transform suckPosition;

    public ProjectileIdleState idle = new ProjectileIdleState();
    public ProjectileSuckedState sucked = new ProjectileSuckedState();
    public ProjectileShotState shot = new ProjectileShotState();

    public bool isSucked;
    private void Awake()
    {
        isSucked = false;
        shrinkRate = new Vector3(projectileSO.shrinkValue, projectileSO.shrinkValue, 0f);

        shootSpeed = projectileSO.shootSpeed;
        projectilePooledType = projectileSO.projectilePooledType;
        projectileType = projectileSO.projectileType;
        startingState = projectileSO.startingState;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = projectileSO.projectileSprite;

        ChooseStartingState();

        if (currentState != null)
            currentState.EnterState(this);

        hitbox.SetActive(false);
    }

    private void OnDisable()
    {
        currentState = null;
        isSucked = false;
    }

    private void OnEnable()
    {
        ChooseStartingState();
    }

    private void ChooseStartingState()
    {
        switch (startingState)
        {
            case ObjectState.idle:
                currentState = idle;
                break;

            case ObjectState.sucked:
                currentState = sucked;
                break;

            case ObjectState.shot:
                currentState = null;    //It is not ideal to enable the shooting state from inside the object itself, as that would tie in to 
                break;                  //handling script execution order leading to null reference exception issues, so I've decided to set the
                                        //current state as null and enable shooting by the launcher object.
            default:
                currentState = idle;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.UpdateState(this);
        else
            Debug.Log("NULL STATE SCRIPT EXECUTING");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    //I have made sure through layer overrides that the hitbox only gets triggered when it enters an enemy's hurtbox.
    //But still I am adding a layer check just in case.
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        currentState.OnTriggerEnter(this, collision);
    }

    public Vector3 GetShrinkRate() { return shrinkRate; }
    
    public Vector3 GetOriginalSize() { return originalSize; }

    public float GetShootSpeed() { return shootSpeed;  }

    public ProjectilePooledType GetProjectilePooledType() { return projectilePooledType; }

    public ProjectileType GetProjectileType() {  return projectileType; }

    public int GetUsabilityIndex() {  return usabilityIndex; }

    public void SetUsabilityIndex(int _usabilityIndex) { usabilityIndex = _usabilityIndex; }

    public GameObject GetLauncher() { return launcher; }
    public void SetLauncher(GameObject _launcher) {  launcher = _launcher; }

    public Transform GetShootPosition() { return shootPosition; }

    public Transform GetSuckPosition() { return suckPosition; }

    public void SwitchState(SuckableBase state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void SwitchToShoot(Transform _shootPosition)
    {
        shootPosition = _shootPosition;
        SwitchState(shot);
        isSucked = false;
    }
    public void SwitchToSuck(Transform _suckPosition)
    {
        suckPosition = _suckPosition;
        SwitchState(sucked);
        isSucked = true;
    }

    public void ToggleHitbox(bool status)
    {
        hitbox.SetActive(status);
    }
}
