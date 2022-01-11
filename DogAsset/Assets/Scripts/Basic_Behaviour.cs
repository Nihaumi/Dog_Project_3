using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Behaviour : MonoBehaviour
{
    //other script
    GameObject dog;
    Animation_Controll anim_controll;
    Animations anim;

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Environment")
        {
            Debug.Log("collision detected");
            anim_controll.ChangeAnimationState(anim.turn_left_seek);
        }

        if (collision.gameObject.name == "Ground")
        {
            Debug.Log("collision with player");
        }
    }
}
