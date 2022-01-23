using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    playerScript playerscript;
    Animator swordAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerscript = GetComponent<playerScript>();
        swordAnim = GameObject.FindGameObjectWithTag("Sword").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        swordAnim.SetBool("Swinging", playerscript.swordSwinging);
    }
}
