using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Behaviour : MonoBehaviour
{
    //other script
    GameObject dog;
    Animation_Controll anim_controll;
    Animations anim;
    Behaviour_Switch behav_switch;

    //when colliding with other object
    bool collided;

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        behav_switch = dog.GetComponent<Behaviour_Switch>();

        //collision
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Environment")
        {
            Debug.Log("trigger");
            collided = true;
            anim_controll.ChangeAnimationState(anim.stand_01);
            anim_controll.ChangeAnimationState(anim.turn_left_seek);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit");
    }
}
