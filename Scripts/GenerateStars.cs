using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStars : MonoBehaviour {
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starsAmount = 200;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private int maxAttempts = 100;

    private List<Vector3> starPositions;

    private void Start() {
        starPositions = new List<Vector3>();

        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float height = GetComponent<SpriteRenderer>().bounds.size.y;

        for ( int i = 0; i < starsAmount; i++ ) {
            Vector3 newPosition = Vector3.zero;
            bool positionFound = false;

            for ( int attempts = 0; attempts < maxAttempts; attempts++ ) {
                newPosition = new Vector3(
                    Random.Range(-width / 2, width / 2),
                    Random.Range(-height / 2, height / 2),
                    0
                );

                if ( !IsPositionOccupied(newPosition) ) {
                    positionFound = true;
                    break;
                }
            }

            if ( positionFound ) {
                GameObject newStar = Instantiate(starPrefab, newPosition + transform.position, Quaternion.identity, transform);
                newStar.transform.Rotate(Vector3.forward, Random.Range(0f, 360f));

                float randomScale = Random.Range(0.01f, 0.15f);
                newStar.transform.localScale = new Vector3(randomScale, randomScale, 1);

                starPositions.Add(newPosition);

                StartCoroutine(AnimateStar(newStar));
            }
        }
    }

    private bool IsPositionOccupied(Vector3 position) {
        foreach ( Vector3 existingPosition in starPositions ) {
            if ( Vector3.Distance(existingPosition, position) < minDistance ) {
                return true;
            }
        }
        return false;
    }

    private IEnumerator AnimateStar(GameObject star) {
        float randomTime = Random.Range(0.5f, 1.5f);
        float timer = 0f;
        SpriteRenderer starRenderer = star.GetComponent<SpriteRenderer>();

        while ( true ) {
            timer += Time.deltaTime;
            float alpha = Mathf.PingPong(timer / randomTime, 1f);
            Color newColor = starRenderer.color;
            newColor.a = alpha;
            starRenderer.color = newColor;

            yield return null;
        }
    }
}
