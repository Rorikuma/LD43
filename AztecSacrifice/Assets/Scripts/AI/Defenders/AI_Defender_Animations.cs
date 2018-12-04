using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Defender_Animations : MonoBehaviour {

    public Animator KidAnim;
    public Animator AdultAnim;
    public Animator OldAnim;

    public SpriteRenderer KidRend;
    public SpriteRenderer AdultRend;
    public SpriteRenderer OldRend;

    AI_Defender brain;
    AI_Stats stats;

    private void Awake()
    {
        brain = GetComponent<AI_Defender>();
        stats = GetComponent<AI_Stats>();
    }

    private void Update()
    {
        // No animations
        if(stats.Age == Phase.Kid)
        {
            AdultRend.enabled = false;
            OldRend.enabled = false;
            KidRend.enabled = true;
        }
        else if(stats.Age == Phase.Adult)
        {
            KidRend.enabled = false;
            OldRend.enabled = false;
            AdultRend.enabled = true;
        }
        else
        {
            KidRend.enabled = false;
            AdultRend.enabled = false;
            OldRend.enabled = true;
        }
    }

}
