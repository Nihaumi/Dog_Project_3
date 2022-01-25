using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Friendly_Behaviour : MonoBehaviour
{
    //other script
    public GameObject dog;
    public GameObject player;
    public GameObject dog_parent;
    public GameObject dir_manager;
    public GameObject dog_sound_manager;
    public Animator animator;
    GameObject player_target;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;
    Basic_Behaviour basic_behav;
    Neutral_Behaviour neutral_behav;
    PlayerInteraction player_interaction;
    Audio_Sources dog_audio;




    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        player = GameObject.FindGameObjectWithTag("Player");
        player_target = GameObject.Find("target");
        dog_parent = GameObject.Find("DOg");
        dir_manager = GameObject.Find("Direction_Manager");
        dog_sound_manager = GameObject.Find("Dog_sound_manager");
        animator = dog.GetComponent<Animator>();
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        turn_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        basic_behav = dog.GetComponent<Basic_Behaviour>();
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();
        player_interaction = player.GetComponent<PlayerInteraction>();
        dog_audio = dog_sound_manager.GetComponent<Audio_Sources>();

        facing_player = false;
        escape_chance_on = false;
        friendly = false;
    }

    public float touching_player_timer;
    public bool escape_chance_on;
    public bool facing_player;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("IS THIS THING ON?!");
        if (basic_behav.y_goal == basic_behav.trot_value)
        {
            basic_behav.y_goal = basic_behav.walking_slow_value;
        }

        player_interaction.IsCloseToLeftHand();
        player_interaction.IsCloseToRightHand();
        player_interaction.AreHandsMoving();

        ApproachPlayer();

        //if hand is touching dog, make eyes look at player eyes
        //and head facing player 
        // bleib stehen solange ber�hrung
        //sitz nach 10 sekunden
        //platz nach 10 sekunden
        if (TouchingHand())
        {
            Debug.Log("touching Hand");
        }
    }

    //if not facing player turn face towards player
    //bleib stehen f�r mind 10 sekunden
    //sitz
    //platz
    //schlaf

    /*TODO
     * check if player left or right, turne in andre dir
     */

    /* Fix Rigs --> broken neck
     */
    IEnumerator WaitForRotation()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("FACING player, WALKING TO IT");
        StartCoroutine(basic_behav.WaitBeforeWalkingTowards(player_target));
        if (TouchingPlayer())
        {
            Debug.Log("TOUCHING");
            facing_player = true;
        }
    }

    public void ApproachPlayer()
    {
        Debug.Log("HElooooooooo");
        if (!facing_player && friendly)
        {
            Debug.Log("working on facing player");
            basic_behav.TurnToTarget(player_target);
            StartCoroutine(WaitForRotation());
        }
        if (TouchingPlayer() && facing_player)
        {
            //if (!escape_chance_on)

            Debug.Log("PANTING: " + dog_audio.panting_calm.isPlaying);
            basic_behav.SetShortTimer(10, 15);
            basic_behav.y_goal = basic_behav.standing_value;
            basic_behav.x_axis = basic_behav.standing_value;
            basic_behav.y_acceleration = 4;
            dog_audio.StopAllSounds();
            //audio
            if (anim_controll.current_state == anim.sit_00 && !dog_audio.panting_calm.isPlaying)
            {
                dog_audio.StopAllSounds();
                dog_audio.panting_calm.Play();
                escape_chance_on = true;
            }
            if (basic_behav.y_axis == basic_behav.standing_value && basic_behav.dog_state != Basic_Behaviour.Animation_state.sitting)
            {
                basic_behav.y_acceleration = basic_behav.default_y_acceleration;
                anim_controll.ChangeAnimationState(anim.friendly_trans_stand_to_sitting);

                basic_behav.dog_state = Basic_Behaviour.Animation_state.sitting;
            }

            Debug.Log("touching player");

            /* if (escape_chance_on && basic_behav.dog_state == Basic_Behaviour.Animation_state.standing)
             {
                 basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                 //audio
                 dog_audio.StopAllSounds();
                 basic_behav.y_goal = basic_behav.walking_value;
             }
             else if (escape_chance_on && basic_behav.dog_state != Basic_Behaviour.Animation_state.walking)
             {
                 basic_behav.SetShortTimer(3, 5);
                 anim_controll.ChangeAnimationState(anim.friendly_turn_after_sitting);
                 basic_behav.y_goal = basic_behav.walking_value;
                 basic_behav.TurnLeft();
                 //audio
                 dog_audio.StopAllSounds();
                 basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                 basic_behav.SetShortTimer(1, 2);
             }*/
        }
    }


    /*TODO
     * if not facing player 
     * turn towards player
     * 
     * if not close enough to player, walk until closer
     *  
     */
    public float dist_to_player;
    public bool touching_player_timer_started = false;
    bool TouchingPlayer()
    {
        /* if (touching_player_timer_started)
         {
             touching_player_timer -= Time.deltaTime;

             if (touching_player_timer <= 0)
             {
                 touching_player_timer_started = false;
                 escape_chance_on = true;
             }
             else
             {
                 return true;
             }
         }*/

        dist_to_player = Vector3.Distance(player.transform.position, dog.transform.position);
        if (dist_to_player < 3.3f)
        {
            touching_player_timer = 10;
            touching_player_timer_started = true;
            return true;
        }
        else
        {
            escape_chance_on = false;

        }
        return false;
    }
    bool TouchingHand()
    {
        return false;
    }
    public bool friendly;
    public void FriendlyBehaviour()
    {
        if (!facing_player)
        {
            Debug.Log("So are you gonna do itHUH");
            switch (basic_behav.dog_state)
            {
                case Basic_Behaviour.Animation_state.standing:
                    basic_behav.ResetParameter();
                    dog_audio.StopAllSounds();
                    friendly = true;
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    break;
                case Basic_Behaviour.Animation_state.sitting:
                    dog_audio.StopAllSounds();
                    basic_behav.ResetParameter();
                    anim_controll.ChangeAnimationState(anim.friendly_trans_sitting_to_stand);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    break;
                case Basic_Behaviour.Animation_state.lying:
                    dog_audio.StopAllSounds();
                    basic_behav.ResetParameter();
                    anim_controll.ChangeAnimationState(anim.friendly_trans_lying_to_stand);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    break;
                case Basic_Behaviour.Animation_state.sleeping:
                    dog_audio.StopAllSounds();
                    basic_behav.ResetParameter();
                    anim_controll.ChangeAnimationState(anim.friendly_trans_sleep_to_stand);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    break;
                case Basic_Behaviour.Animation_state.walking:
                    Debug.Log("friends and walking");
                    dog_audio.StopAllSounds();
                    anim_controll.ChangeAnimationState(anim.friendly_blend_tree);
                    if (basic_behav.y_goal == basic_behav.trot_value)
                    {
                        basic_behav.y_goal = basic_behav.standing_value;
                    }
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    break;
                default:
                    break;
            }
        }
        if (facing_player && escape_chance_on)
        {
            dog_audio.StopAllSounds();
            anim_controll.ChangeAnimationState(anim.friendly_sit_to_turn_walk);
            basic_behav.y_goal = basic_behav.walking_slow_value;
            basic_behav.TurnLeft();
            basic_behav.dog_state = Basic_Behaviour.Animation_state.friendly_walking;

            switch (basic_behav.dog_state)
            {
                case Basic_Behaviour.Animation_state.friendly_walking:
                    anim_controll.ChangeAnimationState(anim.friendly_blend_tree);
                    basic_behav.WalkForward();
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                    break;
                case Basic_Behaviour.Animation_state.standing:

                    basic_behav.ResetParameter();
                    dog_audio.StopAllSounds();
                    //Debug.Log("standinglist item at rndindex: " + basic_behav.random_index + "is:" + anim.list_standing[basic_behav.random_index]);
                    if (basic_behav.random_index == 0)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_list_standing[basic_behav.random_index]);
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                        //audio
                        dog_audio.panting_calm.Play();
                    }
                    if (basic_behav.random_index == 1)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_list_standing[basic_behav.random_index]);
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.sitting;
                        //audio
                        dog_audio.panting_calm.Play();
                    }
                    if (basic_behav.random_index > 1 && basic_behav.random_index < 4)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_blend_tree);
                        if (basic_behav.random_index == 2)
                        {
                            basic_behav.y_goal = basic_behav.walking_slow_value;
                        }
                        if (basic_behav.random_index == 3)
                        {
                            basic_behav.y_goal = basic_behav.walking_value;
                        }
                        //SetBlendTreeParameters();
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                    }
                    basic_behav.SetShortTimer(7, 10);
                    Debug.Log("standing list item at rndindex: " + basic_behav.random_index + "is:" + anim.friendly_list_standing[basic_behav.random_index]);
                    break;

                case Basic_Behaviour.Animation_state.sitting:
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                    dog_audio.StopAllSounds();
                    if (basic_behav.random_index == 0)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_trans_sitting_to_stand);
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    }
                    basic_behav.ResetParameter();
                    if (basic_behav.random_index == 1)
                    {
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 2)
                    {
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    //SetBlendTreeParameters();

                    basic_behav.SetShortTimer(7, 15);
                    Debug.Log("sitting list item at rndindex: " + basic_behav.random_index + "is:" + anim.friendly_list_sitting[basic_behav.random_index]);
                    break;

                case Basic_Behaviour.Animation_state.lying:
                    dog_audio.StopAllSounds();
                    basic_behav.ResetParameter();

                    if (basic_behav.random_index == 0)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_list_lying[basic_behav.random_index]);
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.sleeping;
                    }
                    else
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_trans_sitting_to_stand);

                        if (basic_behav.random_index == 2)
                        {
                            basic_behav.y_goal = basic_behav.walking_slow_value;
                        }
                        if (basic_behav.random_index == 3)
                        {
                            basic_behav.y_goal = basic_behav.walking_value;
                        }
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                    }
                    basic_behav.SetShortTimer(7, 10);
                    Debug.Log("lyingg list item at rndindex: " + basic_behav.random_index + "is:" + anim.friendly_list_lying[basic_behav.random_index]);
                    break;

                case Basic_Behaviour.Animation_state.sleeping:

                    basic_behav.ResetParameter();
                    dog_audio.StopAllSounds();
                    //Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sleeping[basic_behav.random_index]);
                    if (basic_behav.random_index == 0)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_list_sleeping[basic_behav.random_index]);

                        basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                        //audio
                        dog_audio.StopAllSounds();
                        dog_audio.panting_calm.Play();
                    }
                    else if (basic_behav.random_index == 1)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_list_sleeping[basic_behav.random_index]);

                        basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    }
                    else
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_trans_sleep_to_stand);
                        if (basic_behav.random_index == 2)
                        {
                            basic_behav.y_goal = basic_behav.walking_slow_value;
                        }
                        if (basic_behav.random_index == 3)
                        {
                            basic_behav.y_goal = basic_behav.walking_value;
                        }
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                    }
                    basic_behav.SetLongTimer();
                    Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.friendly_list_sleeping[basic_behav.random_index]);
                    break;

                case Basic_Behaviour.Animation_state.walking:
                    dog_audio.StopAllSounds();

                    if (anim_controll.current_state != anim.friendly_blend_tree)
                    {
                        anim_controll.ChangeAnimationState(anim.friendly_blend_tree);
                    }
                    basic_behav.SetLongTimer();
                    if (basic_behav.random_index == 0)
                    {
                        basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                        basic_behav.y_goal = basic_behav.standing_value;
                    }
                    else
                    {
                        if (basic_behav.random_index == 1)
                        {

                            basic_behav.y_goal = basic_behav.walking_slow_value;
                        }
                        if (basic_behav.random_index == 2)
                        {

                            basic_behav.y_goal = basic_behav.walking_value;
                        }

                        basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;

                    }
                    Debug.Log("walking list item at rndindex: " + basic_behav.random_index + "is:" + anim.friendly_list_walking[basic_behav.random_index]);
                    break;
                default:
                    return;
            }
        }
    }


}
