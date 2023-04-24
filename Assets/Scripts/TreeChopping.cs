using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopping : MonoBehaviour {
    public int requiredHits = 3;
    public int currentHits = 0;
    public GameObject itemPrefab;
    public Sprite choppedTreeSprite;
    public float dropDistance = 1.5f;
    public float offsetX = 0f;
    public float offsetY = 0f;
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

            // Adjust the sprite location of the chopped tree
            sr.transform.localPosition = new Vector3(offsetX, offsetY, sr.transform.localPosition.z);
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
