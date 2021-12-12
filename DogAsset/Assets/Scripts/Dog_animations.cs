using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_animations : MonoBehaviour
{
    //animation_states -- accesses the topmost layer of the animator
    const string dog_stand0 = "Stand_00_Copy";
    const string dog_stand1 = "Stand_01";
    const string dog_lying0 = "Lying_00";
    const string dog_walk_slow = "Loco_WalkSlow_Copy";
    const string dog_sitting = "Sitting_00";
    const string dog_trans_stand_to_lying = "Trans_Stand_to_Lying";
    const string dog_trans_lying_to_stand = "Trans_Lying_to_Stand";
    const string dog_trans_sit_to_stand = "Trans_Sitting_to_Stand";
    const string dog_trans_stand_to_sit = "Trans_Stand_to_Sitting";

    // bool states
    bool is_sitting = false;
    bool is_standing = true;
    bool is_walking = false;
    bool is_lying = false;

    //othe script
    GameObject dog;
    Animation_Controll anim_controll;

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();

        anim_controll.current_state = dog_stand0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (is_sitting)
            {
                Debug.Log("already sitting");
                return;
            }
            if(is_standing || is_walking)
            {
                Debug.Log("standing to sitting");
                anim_controll.ChangeAnimationState(dog_trans_stand_to_sit);
                //StartCoroutine(DogCommandWithWaitCoroutine(dog_sitting));
                //anim_controll.ChangeAnimationState(dog_sitting);

                SetBoolsToFalse();
                is_sitting = true;
            }


        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (is_lying)
            {
                Debug.Log("already laying");
                return;
            }
            if (is_walking || is_standing)
            {
                anim_controll.ChangeAnimationState(dog_trans_stand_to_lying);
                Debug.Log("lay down");

                SetBoolsToFalse();
                is_lying = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (is_walking)
            {
                Debug.Log("already walking");
                return;
            }
            if (is_lying)
            {
                anim_controll.ChangeAnimationState(dog_trans_lying_to_stand);
                StartCoroutine(DogCommandWithWaitCoroutine(dog_walk_slow));
                Debug.Log("walk from ly");

                SetBoolsToFalse();
                is_walking = true;
            }

            if (is_sitting)
            {
                anim_controll.ChangeAnimationState(dog_trans_sit_to_stand);
                StartCoroutine(DogCommandWithWaitCoroutine(dog_walk_slow));
                Debug.Log("walk from sit");

                SetBoolsToFalse();
                is_walking = true;
            }

            if (is_standing)
            {
                anim_controll.ChangeAnimationState(dog_walk_slow);

                SetBoolsToFalse();
                is_walking = true;
            }
        }
    }

    void SetBoolsToFalse()
    {
        is_standing = false;
        is_walking = false;
        is_lying = false;
        is_sitting = false;
    }
    IEnumerator DogCommandWithWaitCoroutine(string new_state)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(2);
        anim_controll.ChangeAnimationState(new_state);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
