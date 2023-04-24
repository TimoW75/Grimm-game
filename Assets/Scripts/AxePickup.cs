using System.Collections;
using UnityEngine;

public class AxePickup : MonoBehaviour {
    public Vector3 axeHeldPosition = new Vector3(0.5f, -0.5f, 0f);
    public Vector3 axeHeldRotation = new Vector3(0f, 0f, -45f);
    public float swingDuration = 0.2f;
    public float swingAngle = 30f;
    public int maxHits = 3;
    private int currentHits = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerChopping playerChopping = other.GetComponent<PlayerChopping>();
        if (playerChopping != null) {
            playerChopping.canChop = true;

            // Make the axe a child of the player and set its relative position and rotation
            transform.SetParent(playerChopping.transform);
            transform.localPosition = axeHeldPosition;
            transform.localEulerAngles = axeHeldRotation;

            // Disable the axe's pickup collider so it doesn't trigger again
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void SwingAxe() {
        StartCoroutine(AxeSwingAnimation());
    }

    private IEnumerator AxeSwingAnimation() {
        float elapsedTime = 0f;

        Quaternion startRotation = transform.localRotation;
        Quaternion midRotation = Quaternion.Euler(new Vector3(0f, 0f, swingAngle));
        Quaternion endRotation = startRotation;

        while (elapsedTime < swingDuration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / swingDuration;

            if (t < 0.5f) {
                transform.localRotation = Quaternion.Lerp(startRotation, midRotation, t * 2f);
            } else {
                transform.localRotation = Quaternion.Lerp(midRotation, endRotation, (t - 0.5f) * 2f);
            }

            yield return null;
        }

        transform.localRotation = endRotation;
    }

    public void AxeHit() {
        currentHits++;
        if (currentHits >= maxHits) {
            if (gameObject != null) {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy() {
        if (transform.parent != null) {
            PlayerChopping playerChopping = transform.parent.GetComponent<PlayerChopping>();
            if (playerChopping != null) {
                playerChopping.canChop = false;
            }
        }
    }
}
