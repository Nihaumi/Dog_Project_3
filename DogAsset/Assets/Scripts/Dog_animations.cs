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
    const string dog_trans_stand_to_lying = "Trans_Stand_to_Lying_0";
    const string dog_trans_lying_to_stand = "Trans_Lying_to_Stand";
    const string dog_trans_sit_to_stand = "Trans_Sitting_to_Stand";
    const string dog_trans_stand_to_sit = "Trans_Stand_to_Sitting_0";
    const string dog_trans_sit_to_walk = "Trans_Sitting_to_Stand_plus_walk";
    const string dog_trans_lying_to_walk = "Trans_Lying_to_Stand_plus_walk";

    // bool states --> enum
    bool is_sitting = false;
    bool is_standing = true;
    bool is_walking = false;
    bool is_lying = false;

    enum Animation_state //is a class
    {
        sitting,
        standing,
        walking,
        lying
    };

    Animation_state dog_state = Animation_state.standing;

    //animation lists - to pick a random animation
    List<string> dog_animation_list = new List<string>()
    {
        dog_lying0,
        dog_sitting,
        dog_stand0,
        dog_stand1,
        dog_trans_lying_to_stand,
        dog_trans_sit_to_stand,
        dog_trans_stand_to_lying,
        dog_trans_stand_to_sit,
        dog_walk_slow,
    };
    

    //other script
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
        //Sit
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            switch(dog_state){
                case Animation_state.standing:
                    anim_controll.ChangeAnimationState(dog_trans_stand_to_sit);
                    break;
                case Animation_state.walking:
                    anim_controll.ChangeAnimationState(dog_trans_stand_to_sit);
                    break;
                default:
                    return;
            }
            dog_state = Animation_state.sitting;    
        }

        //Lay Down
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (dog_state)
            {
                case Animation_state.standing:
                    anim_controll.ChangeAnimationState(dog_trans_stand_to_lying);
                    break;
                case Animation_state.walking:
                    anim_controll.ChangeAnimationState(dog_trans_stand_to_lying);
                    break;
                default:
                    return; //leaves upfate function
            }
            dog_state = Animation_state.lying;         
        }

        //Walk
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (dog_state)
            {
                case Animation_state.standing:
                    anim_controll.ChangeAnimationState(dog_walk_slow);
                    break;
                case Animation_state.sitting:
                    anim_controll.ChangeAnimationState(dog_trans_sit_to_walk);
                    break;
                case Animation_state.lying:
                    anim_controll.ChangeAnimationState(dog_trans_lying_to_walk);
                    break;
                default:
                    return;
            }
            dog_state = Animation_state.walking;


           
        }
    }

    IEnumerator DogCommandWithWaitCoroutine(string new_state)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(2);
        anim_controll.ChangeAnimationState(new_state);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
