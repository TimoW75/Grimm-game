using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChopping : MonoBehaviour {
    public float chopRange = 1f;
    public KeyCode chopKey = KeyCode.Space;
    public GameObject axe; // Reference to the axe GameObject
    public bool canChop = false;

    private TreeChopping treeChopping;

    void Start() {
        treeChopping = FindObjectOfType<TreeChopping>();
    }

    void Update() {
        if (canChop && Input.GetKeyDown(chopKey)) {
            ChopTree();
        }
    }

    private void ChopTree() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chopRange);
        foreach (Collider2D collider in colliders) {
            TreeChopping tree = collider.GetComponent<TreeChopping>();
            if (tree != null) {
                tree.Chop();
                if (axe != null && axe.GetComponent<AxePickup>() != null) {
                    axe.GetComponent<AxePickup>().SwingAxe(); // Trigger the axe swing animation
                    axe.GetComponent<AxePickup>().AxeHit(); // Handle axe hits
                }
                break;
            }
        }
    }
}
