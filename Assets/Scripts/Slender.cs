using UnityEngine;
using UnityEngine.AI;

public class Slender : MonoBehaviour
{
    private NavMeshAgent navMeshAgentSlender;
    private PlayerMovement player;
    private SkinnedMeshRenderer slenderMeshRenderer;
    private Animator slenderAnimator;

    private float baseSpeed = 0.5f;
    private bool isActive = false;

    void Start()
    {
        navMeshAgentSlender = GetComponent<NavMeshAgent>();
        player = FindAnyObjectByType<PlayerMovement>();
        slenderMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        slenderAnimator = GetComponent<Animator>();

        SetEnemyState(false);
        navMeshAgentSlender.speed = baseSpeed;
    }

    void Update()
    {
        if (!isActive) return;

        navMeshAgentSlender.destination = player.transform.position;

        float currentVelocity = navMeshAgentSlender.velocity.magnitude;
        slenderAnimator.SetFloat("speed", currentVelocity);
    }

    public void cambiarDificultad(int notas) {
        if (!isActive && notas > 0) {
            Debug.Log("Se levanta otra veeez");
            SetEnemyState(true);
        }

        float newVel = baseSpeed + (notas*0.5f);
        navMeshAgentSlender.speed = newVel;
        if (newVel >= 2.5f) {
            slenderAnimator.SetBool("isRunning", true);
        }
        Debug.Log("Notas:" +notas + " Vel:" + navMeshAgentSlender.speed);
    }

    private void SetEnemyState(bool state) {
        isActive = state;
        slenderMeshRenderer.enabled = state;
        navMeshAgentSlender.enabled = state; 
    }
}
