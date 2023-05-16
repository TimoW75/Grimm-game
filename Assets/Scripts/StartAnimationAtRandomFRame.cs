using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationAtRandomFRame : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var anim = GetComponent<Animator>();
        var state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, 0, Random.Range(0f, 1f));
    }

}
