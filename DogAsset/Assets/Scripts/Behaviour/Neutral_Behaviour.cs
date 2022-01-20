using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral_Behaviour : MonoBehaviour
{
    //other script
    public GameObject dog;
    public GameObject dog_parent;
    public GameObject dir_manager;
    public Animator animator;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;
    Basic_Behaviour basic_behav;

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        dog_parent = GameObject.Find("DOg");
        dir_manager = GameObject.Find("Direction_Manager");
        animator = dog.GetComponent<Animator>();
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        turn_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        basic_behav = dog.GetComponent<Basic_Behaviour>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //coodinates in Blend Tree
    public float x_axis = 0;
    public float y_axis = 0;

    public float accelaration = 2;
    float walking_slow_value = 0.25f;
    float walking_value = 0.5f;
    float seek_value = 1f;
    float trot_value = 1.5f;

    /*todo fo rblend tree:
     * while loop with thresholds for each wakling state
     * erstmal nur für: walkSlow, walk, seek, trot, run
     * variablen für thresholds -> min y in while loop, x ist immer 0
     * evtl in basic behaviour integrieren weil für alle gleich
     * change animation state nicht gebraucht
     * stop animation player????
     * 
     * reset  X und Y function
     * Function mit thershold paramater
    */

    //Sets parameters in animator
    void SetBlendTreeParameters()
    {
        animator.SetFloat("X", x_axis);
        animator.SetFloat("Y", y_axis);
    }

    void ResetParameter()
    {
        x_axis = 0;
        y_axis = 0;
    }

    //increases Y axis until specific walking animation is reached
    void IncreaseYAxisToValue(float value)
    {
        while (y_axis < value)
        {
            y_axis += Time.deltaTime * accelaration;
            SetBlendTreeParameters();
        }
    }
    public void NeutralBehaviour()
    {

        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.standing:
                //Debug.Log("standinglist item at rndindex: " + basic_behav.random_index + "is:" + anim.list_standing[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    anim_controll.ChangeAnimationState(anim.list_standing[basic_behav.random_index]);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                if (basic_behav.random_index == 1)
                {
                    anim_controll.ChangeAnimationState(anim.list_standing[basic_behav.random_index]);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sitting;
                }
                if (basic_behav.random_index > 1)
                {
                    if (basic_behav.random_index == 2)
                    {
                        IncreaseYAxisToValue(walking_slow_value);
                    }
                    if (basic_behav.random_index == 3)
                    {
                        IncreaseYAxisToValue(walking_value);
                    }
                    if (basic_behav.random_index == 4)
                    {
                        IncreaseYAxisToValue(seek_value);
                    }
                    if (basic_behav.random_index == 5)
                    {
                        IncreaseYAxisToValue(trot_value);
                    }
                    //SetBlendTreeParameters();
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.sitting:
                anim_controll.ChangeAnimationState(anim.list_sitting[basic_behav.random_index]);
                //Debug.Log("sitting list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sitting[basic_behav.random_index]);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.lying:
                anim_controll.ChangeAnimationState(anim.list_lying[basic_behav.random_index]);
                //Debug.Log("lying list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_lying[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sleeping;
                }
                if (basic_behav.random_index > 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.sleeping:
                anim_controll.ChangeAnimationState(anim.list_sleeping[basic_behav.random_index]);
                //Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sleeping[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                if (basic_behav.random_index == 1)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                }
                if (basic_behav.random_index > 1)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.walking:

                Debug.Log("walking list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_walking[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    anim_controll.ChangeAnimationState(anim.list_walking[basic_behav.random_index]);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    basic_behav.SetLongTimer();
                }
                if (basic_behav.random_index == 4)
                {
                    IncreaseYAxisToValue(trot_value);
                    basic_behav.SetShortTimer(2f, 3f);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                else
                {
                    if (basic_behav.random_index == 1)
                    {
                        IncreaseYAxisToValue(walking_slow_value);
                    }
                    if (basic_behav.random_index == 2)
                    {
                        IncreaseYAxisToValue(walking_value);
                    }
                    if (basic_behav.random_index == 3)
                    {
                        IncreaseYAxisToValue(seek_value);
                    }

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                    basic_behav.SetLongTimer();
                }
                break;
            default:
                return;
        }

    }
}
