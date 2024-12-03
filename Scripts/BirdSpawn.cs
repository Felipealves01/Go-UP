using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour {
    [SerializeField] private float minY, maxY;
    [SerializeField] private GameObject birdPrefab;

    private void Start() {
        InvokeRepeating(nameof(Spawn), 0f, Random.Range(1f, 3f));
    }

    private void Spawn() {
        Vector2 spawnPosition = new Vector2(
            transform.position.x + (Random.Range(0, 100) > 50 ? -30 : 30),
            Random.Range(minY, maxY)
        );

        GameObject bird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
        bird.AddComponent<Bird>();
    }
}

public class Bird : MonoBehaviour {
    private float birdSpeed;
    private Vector3 originalScale;
    private float randomScaleFactor;
    private bool isMovingRight;

    private AudioSource audioSource;

    private void Start() {

        audioSource = GetComponent<AudioSource>();

        birdSpeed = Random.Range(5f, 10f);

        originalScale = transform.localScale;

        randomScaleFactor = Random.Range(1.0f, 3.0f);
        transform.localScale = new Vector3(originalScale.x * randomScaleFactor, originalScale.y * randomScaleFactor, originalScale.z);

        if ( transform.position.x < 0 ) {
            isMovingRight = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        } else {
            isMovingRight = false;
        }

        Destroy(gameObject, 30f);

        InvokeRepeating(nameof(Sound), 0, Random.Range(3, 6));
    }

    void Update() {
        if ( isMovingRight ) {
            transform.position += Vector3.right * birdSpeed * Time.deltaTime;
        } else {
            transform.position += Vector3.left * birdSpeed * Time.deltaTime;
        }
    }

    void Sound() {
        audioSource.Play();
    }

}
