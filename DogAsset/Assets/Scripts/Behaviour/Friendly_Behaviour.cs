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


    }

    public float touching_player_timer;
    public bool escape_chance_on = false;
    // Update is called once per frame
    void Update()
    {
        if (basic_behav.y_goal == basic_behav.trot_value)
        {
            basic_behav.y_goal = basic_behav.walking_slow_value;
        }

        player_interaction.IsCloseToLeftHand();
        player_interaction.IsCloseToRightHand();
        player_interaction.AreHandsMoving();

        //if not facing player turn face towards player
        //bleib stehen für mind 10 sekunden
        //sitz
        //platz
        //schlaf

        /*TODO
         * check if player left or right, turne in andre dir
         */

        /* Fix Rigs --> broken neck
         */
        if (TouchingPlayer())
        {
            if (!escape_chance_on)
            {
                Debug.Log("PNATING: " + dog_audio.panting_calm.isPlaying);
                basic_behav.SetShortTimer(10, 15);
                basic_behav.y_goal = basic_behav.standing_value;
                basic_behav.y_acceleration = 4;
                //audio
                if (anim_controll.current_state == anim.sit_00 && dog_audio.panting_calm.isPlaying)
                {

                    dog_audio.StopAllSounds();
                    dog_audio.panting_calm.Play();
                }
                if (basic_behav.y_axis == basic_behav.standing_value && basic_behav.dog_state != Basic_Behaviour.Animation_state.sitting)
                {
                    basic_behav.y_acceleration = basic_behav.default_y_acceleration;
                    anim_controll.ChangeAnimationState(anim.friendly_trans_stand_to_sitting);

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sitting;
                }
                Debug.Log("touching player");
            }
            if (escape_chance_on && basic_behav.dog_state == Basic_Behaviour.Animation_state.standing)
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
            }
        }

        //if hand is touching dog, make eyes look at player eyes
        //and head facing player 
        // bleib stehen solange berührung
        //sitz nach 10 sekunden
        //platz nach 10 sekunden
        if (TouchingHand())
        {

            Debug.Log("touchiung Hand");
        }

        //wenn hand bestimmte geschwindigekit hat ... oder sich viel bewegt(bestimmte distanz oder zeit)
        //follow hand movement with head
        if (HandIsMoving())
        {
            Debug.Log("Hand is Moving");
        }
        //und wenn facing player 
        //aye contact
        if (PlayerHeadIsMoving())
        {
            Debug.Log("Player Head moving");
        }
    }

    public float dist_to_player;
    public bool touching_player_timer_started = false;
    bool TouchingPlayer()
    {
        if (touching_player_timer_started)
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
        }

        dist_to_player = Vector3.Distance(player.transform.position, dog.transform.position);
        if (dist_to_player < 2f)
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

    bool HandIsMoving()
    {
        return false;
    }
    bool PlayerHeadIsMoving()
    {
        return false;
    }

    bool IsFacingPlayer()
    {
        return false;
    }

    public void FriendlyBehaviour()
    {

        switch (basic_behav.dog_state)
        {
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
