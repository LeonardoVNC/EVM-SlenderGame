using UnityEngine;
using UnityEngine.AI;

public class Slender : MonoBehaviour
{
    private NavMeshAgent navMeshAgentSlender;
    private PlayerMovement playerMovement;
    private PlayerLook playerLook;
    private SkinnedMeshRenderer slenderMeshRenderer;
    private Animator slenderAnimator;

    private float baseSpeed = 0.5f;
    private float catchDistance = 2f;
    private bool isActive = false;
    private bool isGameOver = false;

    void Start()
    {
        navMeshAgentSlender = GetComponent<NavMeshAgent>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerLook = FindAnyObjectByType<PlayerLook>();
        slenderMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        slenderAnimator = GetComponent<Animator>();

        SetEnemyState(false);
        navMeshAgentSlender.speed = baseSpeed;
    }

    void Update()
    {
        if (!isActive || isGameOver) return;

        navMeshAgentSlender.destination = playerMovement.transform.position;

        float currentVelocity = navMeshAgentSlender.velocity.magnitude;
        slenderAnimator.SetFloat("speed", currentVelocity);
        VerificarDistanciaConJugador();
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

    public void VerificarDistanciaConJugador() {
        float distanceToPlayer = Vector3.Distance(transform.position, playerMovement.transform.position);
        if (distanceToPlayer <= catchDistance) {
            AtraparJugador();
        }
    }

    public void AtraparJugador() {
        isGameOver = true;
        navMeshAgentSlender.isStopped = true;
        navMeshAgentSlender.velocity = Vector3.zero;
        
        Vector3 slenderLookAt = new Vector3(playerMovement.transform.position.x, transform.position.y, playerMovement.transform.position.z);
        transform.LookAt(slenderLookAt);
        slenderAnimator.SetTrigger("jumpscare");

        playerMovement.enabled = false;
        playerLook.enabled = false;

        Vector3 playerLookAtSlender = transform.position + (Vector3.up*2f);
        playerLook.transform.LookAt(playerLookAtSlender);
    }
}
