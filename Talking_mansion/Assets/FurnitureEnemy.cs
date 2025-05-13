using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class FurnitureEnemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float stopDistance = 1.5f;
    private NavMeshAgent agent;

    [Header("Scare UI Elements")]
    public Image blackoutScreen;
    public TextMeshProUGUI stayOutText;
    public float messageDuration = 2f;
    public Transform teleportTarget;

    private bool hasTriggered = false;
    private bool hasStartedChasing = false;
    private Vector3 startPosition;
    private AudioSource chaseAudio;

    void Start()
    {
        chaseAudio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;

        if (blackoutScreen != null)
        {
            blackoutScreen.color = new Color(0, 0, 0, 0);
            blackoutScreen.gameObject.SetActive(false);
        }

        if (stayOutText != null)
        {
            stayOutText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (hasTriggered) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            agent.SetDestination(player.position);

            // Play sound once when chase starts
            if (!hasStartedChasing)
            {
                if (chaseAudio != null && !chaseAudio.isPlaying)
                    chaseAudio.Play();

                hasStartedChasing = true;
            }

            // Trigger scare when close enough
            if (distance < stopDistance)
            {
                StartCoroutine(ScareAndTeleport());
                hasTriggered = true;
            }
        }
        else
        {
            hasStartedChasing = false;
        }
    }

    IEnumerator ScareAndTeleport()
{
    agent.isStopped = true;

    // Stop chase sound
    if (chaseAudio != null && chaseAudio.isPlaying)
        chaseAudio.Stop();

    if (blackoutScreen != null)
    {
        blackoutScreen.gameObject.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            blackoutScreen.color = new Color(0, 0, 0, t);
            yield return null;
        }
    }

    if (stayOutText != null)
        stayOutText.gameObject.SetActive(true);

    yield return new WaitForSeconds(messageDuration);

    if (stayOutText != null)
        stayOutText.gameObject.SetActive(false);

    if (blackoutScreen != null)
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime;
            blackoutScreen.color = new Color(0, 0, 0, t);
            yield return null;
        }
        blackoutScreen.gameObject.SetActive(false);
    }

    if (teleportTarget != null)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.position = teleportTarget.position;

        if (cc != null) cc.enabled = true;
    }

    yield return new WaitForSeconds(2f);
    ResetChase();
}


    public void ResetChase()
    {
        agent.SetDestination(startPosition);
        hasTriggered = false;
        hasStartedChasing = false;
        agent.isStopped = false;
    }

    public void StopChase()
    {
        agent.isStopped = true;

        if (chaseAudio != null && chaseAudio.isPlaying)
            chaseAudio.Stop();

        hasTriggered = false;
    }

}
