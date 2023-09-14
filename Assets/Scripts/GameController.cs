using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    Rigidbody2D playerRb;
    public ParticleController particleController;

    Vector2 checkpointPos;
    Quaternion playerRotation;

    MovementController movementController;
    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        playerRb =GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        checkpointPos = transform.position;
        playerRotation = transform.rotation;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
        playerRotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        particleController.PlayDeadParticle();
        StartCoroutine(Respawn(0.5f));
    }
    IEnumerator Respawn(float duration)
    {
        playerRb.velocity = new Vector2(0, 0);
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.rotation = playerRotation;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
        movementController.UpdateRelativeTransform();
    }
}
