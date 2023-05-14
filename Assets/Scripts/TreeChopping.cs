using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopping : MonoBehaviour {
    public int requiredHits = 3;
    public int currentHits = 0;
    public GameObject itemPrefab;
    public Sprite choppedTreeSprite;
    public float dropDistance = 1.5f;
    public float yOffset = 0.5f;
    public AudioClip chopSound; // Make this public so you can drag and drop the sound file in the editor
    private AudioSource audioSource; // Audio Source for the sound effect
    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        // Create an audio source at runtime
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Chop() {
        currentHits++;

        if (currentHits >= requiredHits) {
            sr.sprite = choppedTreeSprite;

            Vector3 objectPosition = transform.position;

            sr.transform.position = new Vector3(objectPosition.x, objectPosition.y, sr.transform.position.z);
            DropItem();
            // Play the sound
            if (audioSource != null && chopSound != null) {
                audioSource.PlayOneShot(chopSound);
            }
        }
    }

    private void DropItem() {
        float randomAngle = Random.Range(0f, 360f);
        Vector2 dropDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        Vector2 dropPosition = (Vector2)transform.position + dropDirection * dropDistance;

        dropPosition.y -= yOffset;

        Instantiate(itemPrefab, dropPosition, Quaternion.identity);
    }
}
