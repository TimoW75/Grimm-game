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
    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Chop() {
        currentHits++;

        if (currentHits >= requiredHits) {
            sr.sprite = choppedTreeSprite;
            // You can set a flag to make the tree unable to be chopped further

            // Get the position of the game object
            Vector3 objectPosition = transform.position;

            // Set the sprite's position to match the game object's position
            sr.transform.position = new Vector3(objectPosition.x, objectPosition.y, sr.transform.position.z);
            DropItem();
        }
    }

    private void DropItem() {
        float randomAngle = Random.Range(0f, 360f);
        Vector2 dropDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        Vector2 dropPosition = (Vector2)transform.position + dropDirection * dropDistance;

        // Adjust the drop position's Y value
        dropPosition.y -= yOffset;

        Instantiate(itemPrefab, dropPosition, Quaternion.identity);
    }
}
