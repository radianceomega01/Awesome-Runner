using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform footPosition;

    Animator animator;
    PlayerState currentState;
    Rigidbody rigidBody;
    PlayerController playerController;
    public enum AnimationStates
    { 
        Idle,
        Running,
        Jumped,
        Falling,
        Landed
    }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        playerController.Enable();
    }
    private void OnDisable()
    {
        playerController.Disable();
    }
    async void Start()
    {
        currentState = new IdleState(this);
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.PhysicsProcess();
    }
    void Update()
    {
        if(currentState != null)
            currentState = currentState.Process();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(footPosition.position, 0.1f);
    }
    public void SetState(PlayerState state) => currentState = state;
    public void SetAnimation(AnimationStates aniamtionState)
    {
        animator.SetTrigger(aniamtionState.ToString());
    }
    public Rigidbody GetRigidBody() => rigidBody;
    public LayerMask GetGroundLayer() => groundLayer;
    public PlayerController GetPlayerController() => playerController;
    public Transform GetFootTransform() => footPosition;
}
