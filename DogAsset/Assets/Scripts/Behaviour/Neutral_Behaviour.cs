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


    public void NeutralBehaviour()
    {

        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.standing:

                basic_behav.ResetParameter();
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
                if (basic_behav.random_index > 1 && basic_behav.random_index < 6)
                {
                    anim_controll.ChangeAnimationState(anim.blend_tree);
                    if (basic_behav.random_index == 2)
                    {
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    if (basic_behav.random_index == 4)
                    {
                        anim_controll.ChangeAnimationState(anim.blend_tree_seek);
                        basic_behav.y_goal = basic_behav.seek_value;
                    }
                    if (basic_behav.random_index == 5)
                    {
                        basic_behav.y_goal = basic_behav.trot_value;
                    }
                    //SetBlendTreeParameters();
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                Debug.Log("standing list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_standing[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.sitting:
                anim_controll.ChangeAnimationState(anim.trans_sitting_to_stand_02);

                basic_behav.ResetParameter();
                if (basic_behav.random_index == 0)
                {
                    basic_behav.y_goal = basic_behav.walking_slow_value;
                }
                if (basic_behav.random_index == 1)
                {
                    basic_behav.y_goal = basic_behav.walking_value;
                }
                if (basic_behav.random_index == 2)
                {
                    basic_behav.y_goal = basic_behav.seek_value;
                    anim_controll.ChangeAnimationState(anim.blend_tree_seek);
                }
                if (basic_behav.random_index == 3)
                {
                    basic_behav.y_goal = basic_behav.trot_value;
                }
                //SetBlendTreeParameters();
                basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;

                basic_behav.SetLongTimer();
                Debug.Log("sitting list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sitting[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.lying:

                basic_behav.ResetParameter();
                //Debug.Log("lying list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_lying[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    anim_controll.ChangeAnimationState(anim.list_lying[basic_behav.random_index]);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sleeping;
                }
                else
                {
                    anim_controll.ChangeAnimationState(anim.trans_lying_to_stand_02);
                    if (basic_behav.random_index == 1)
                    {
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 2)
                    {
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        anim_controll.ChangeAnimationState(anim.blend_tree_seek);
                        basic_behav.y_goal = basic_behav.seek_value;
                    }
                    if (basic_behav.random_index == 4)
                    {
                        basic_behav.y_goal = basic_behav.trot_value;
                    }
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                Debug.Log("lyingg list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_lying[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.sleeping:

                basic_behav.ResetParameter();
                //Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sleeping[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    anim_controll.ChangeAnimationState(anim.list_sleeping[basic_behav.random_index]);

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                else if (basic_behav.random_index == 1)
                {
                    anim_controll.ChangeAnimationState(anim.list_sleeping[basic_behav.random_index]);

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                }
                else
                {
                    anim_controll.ChangeAnimationState(anim.trans_sleeping_to_lying_to_stand_02);
                    if (basic_behav.random_index == 2)
                    {
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    if (basic_behav.random_index == 4)
                    {
                        anim_controll.ChangeAnimationState(anim.blend_tree_seek);
                        basic_behav.y_goal = basic_behav.seek_value;
                    }
                    if (basic_behav.random_index == 5)
                    {
                        basic_behav.y_goal = basic_behav.trot_value;
                    }
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sleeping[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.walking:

                if (anim_controll.current_state != anim.blend_tree)
                {
                    anim_controll.ChangeAnimationState(anim.blend_tree);
                }
                basic_behav.SetLongTimer();
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    basic_behav.y_goal = basic_behav.standing_value;
                }
                else
                {
                    //anim_controll.ChangeAnimationState(anim.blend_tree);
                    if (basic_behav.random_index == 1)
                    {
                        if (anim_controll.current_state == anim.blend_tree_seek)
                        {
                            SwitchToOrFromSeekingBehaviour(anim.blend_tree);
                        }
                            basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 2)
                    {
                        if (anim_controll.current_state == anim.blend_tree_seek)
                        {
                            SwitchToOrFromSeekingBehaviour(anim.blend_tree);
                        }
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        if (anim_controll.current_state != anim.blend_tree_seek)
                        {
                            SwitchToOrFromSeekingBehaviour(anim.blend_tree_seek);
                        }
                        basic_behav.y_goal = basic_behav.seek_value;
                    }
                    if (basic_behav.random_index == 4)
                    {
                        if (anim_controll.current_state == anim.blend_tree_seek)
                        {
                            SwitchToOrFromSeekingBehaviour(anim.blend_tree);
                        }
                        basic_behav.y_goal = basic_behav.trot_value;
                        basic_behav.SetShortTimer(0.1f, 2f);
                    }

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;

                }
                Debug.Log("walking list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_walking[basic_behav.random_index]);
                break;
            default:
                return;
        }

    }
    public void SwitchToOrFromSeekingBehaviour(string tree)
    {
        basic_behav.SetShortTimer(4, 5);
        basic_behav.y_acceleration = 2;
        basic_behav.y_goal = basic_behav.standing_value;
        if (basic_behav.y_axis == basic_behav.standing_value)
        {
            anim_controll.ChangeAnimationState(tree);
        }
    }

    /*TODO:
     * extra hitbox for trotting --> TUrning handle, turning behav, neutral(?)
     * adjust timing for trotting
     */

    /*TODO:
     * when seeking: current anim state = seeking BT
     * from seeking to other walking state:
     * decrease y value in seeking BT until 0
     * change BT to bland tree 
     * y value = chosen value
     * 
    */
}
