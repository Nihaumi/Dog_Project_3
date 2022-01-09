using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_animations : MonoBehaviour
{
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
        lying,
        sleeping
    };

    Animation_state dog_state = Animation_state.standing;

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

        anim_controll.current_state = anim.stand_01;

      

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            switch (dog_state)
            {
                case Animation_state.standing:

                    Debug.Log(anim.list_sitting.Count);
                    break;
                case Animation_state.sitting:
               
                    Debug.Log(anim.list_sitting.Count);
                    break;
                case Animation_state.lying:
              
                    Debug.Log(anim.list_lying.Count);
                    break;
                case Animation_state.sleeping:
                    
                    break;
                case Animation_state.walking:
                    
                    break;
                default:
                    return;



            }
        }

        //Sit
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (dog_state)
            {
                case Animation_state.standing:
                    anim_controll.ChangeAnimationState(anim.trans_stand_to_sit_00);
                    Debug.Log("stand to sit");
                    break;
                case Animation_state.walking:
                    anim_controll.ChangeAnimationState(anim.trans_stand_to_sit_00);
                    Debug.Log("walk to sit");
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
                    anim_controll.ChangeAnimationState(anim.trans_stand_to_lying_00);
                    Debug.Log("lying to stand");
                    break;
                case Animation_state.walking:
                    anim_controll.ChangeAnimationState(anim.trans_stand_to_lying_00);
                    Debug.Log("lying to walk");
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
                    anim_controll.ChangeAnimationState(anim.trans_lying_to_stand_to_walk);
                    Debug.Log("stand to walk");
                    break;
                case Animation_state.sitting:
                    anim_controll.ChangeAnimationState(anim.trans_sit_to_stand_to_walk);
                    Debug.Log("sit to walk");
                    break;
                case Animation_state.lying:
                    anim_controll.ChangeAnimationState(anim.trans_lying_to_stand_to_walk);
                    Debug.Log("lying to walk");
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
