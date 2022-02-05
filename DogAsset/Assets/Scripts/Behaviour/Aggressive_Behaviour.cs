using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggressive_Behaviour : MonoBehaviour
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
        player = GameObject.Find("target");
        //player = GameObject.FindGameObjectWithTag("Player");
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

        basic_behav.turning_in_place = false;
        facing_player = false;
        facing_target = false;
        started_walking = false;
        escape_chance = false;
        aggressive = false;
        timer_started = false;


        basic_behav.speed = 0.1f;


    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("AGGROOO");

    }
    //on start false
    bool is_aggressive = false;
    public bool facing_player;
    public bool facing_target; // in logic handler true
    public bool started_walking; // in logic Handler true
    public bool escape_chance;
    bool aggressive;    //in animation state aggressive true
    bool timer_started; //false, true wehn dog at target and standing

    public float dist_to_target;
    public float reached_target = 1f;

    /*turn in direction of green cube ( aggression position) and move there while walking animation plays
     * if distance between dig and cube is smaller than 1, dog reached cube
     * rotate towards the player and play standing animation 
     * activate agressive animiation state and play aggressive animation
     */
    /*TODO
     * animations are snappy
     * FIX WEIRD TURNING BEHAVIOUR
     * make head rigging look in camera
     * work on if position not yet reached part 
     */
    public bool turn_to_player = false;

    public void MoveToPositionAndFacePlayer()
    {

        Debug.Log("entering MOVING POS");
        if (!facing_target && aggressive)
        {
            Debug.Log("Walk To POS");
            basic_behav.TurnToTarget(basic_behav.agg_position);
            basic_behav.TurningAndWalkingLogicHandler();
            turn_to_player = false;
            //StartCoroutine(basic_behav.WaitBeforeWalkingTowards(basic_behav.agg_position));
        }else if(turn_to_player){
            Debug.Log("MOMENT");
            basic_behav.TurnToTarget(player);
            if (basic_behav.GetPlayerOffset(0, 32, 0.125f, true) == 0)
            {
                Debug.Log("facing player");
                facing_player = true;
                turn_to_player = false;
                
                basic_behav.y_goal = basic_behav.standing_value;               
                basic_behav.x_goal = basic_behav.standing_value;
                basic_behav.change_anim_timer = 2;
            }
        }
        //check if dog is on position
        if (dist_to_target < reached_target && facing_target)
        {
            Debug.Log("position reached");
            if (!escape_chance && !timer_started)
            {//walk to stand
                Debug.Log("standing");
                basic_behav.y_goal = basic_behav.standing_value;
                basic_behav.x_goal = basic_behav.standing_value;
                basic_behav.y_acceleration = basic_behav.turning_y_acceleration;
            }
            if (basic_behav.y_axis == basic_behav.standing_value && !escape_chance)
            {
                turn_to_player = true;
                Debug.Log("ready to turn to PLAyer");
                //turn to player
                if (!facing_player)
                {
                    if (!timer_started)
                    {
                        basic_behav.speed = 0.1f;
                        Debug.Log("WALK Y_VAL: " + basic_behav.y_goal);
                        basic_behav.change_anim_timer = 1;
                        timer_started = true;
                    }

                }
            }

        }
    }
    public float dist_to_player;
    public float close_enough_to_player = 2f;
    public bool aggressive_too_close = false;
    public void StopAgression()
    {
        if (facing_player)
        {
            if (dist_to_player < close_enough_to_player)
            {
                anim_controll.ChangeAnimationState(anim.trans_agg_to_stand);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                facing_player = false;
                //tell behav switch to switch to neutral/friendly
                aggressive_too_close = true;
            }
        }
    }


    public void AggressiveBehaviour()
    {
        Debug.Log("AGRssive FUNCTION");
        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.aggressiv:
                aggressive = true;
                if (facing_player)
                {

                    dog_audio.StopAllSounds();
                    if (basic_behav.random_index == 0)
                    {
                        anim_controll.ChangeAnimationState(anim.aggressive);
                        dog_audio.StopAllSounds();
                        dog_audio.aggressive_bark.Play();
                    }
                    if (basic_behav.random_index == 1)
                    {
                        anim_controll.ChangeAnimationState(anim.bite_L);
                    }
                    if (basic_behav.random_index == 2)
                    {
                        anim_controll.ChangeAnimationState(anim.bite_R);
                    }
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                    basic_behav.SetShortTimer(10, 10);
                }
                break;
            case Basic_Behaviour.Animation_state.standing:
                if (aggressive)
                {
                    anim_controll.ChangeAnimationState(anim.lying_01);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                else
                {
                    dog_audio.StopAllSounds();
                    anim_controll.ChangeAnimationState(anim.stand_agg);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                    basic_behav.SetShortTimer(2, 2);
                }
                break;
            case Basic_Behaviour.Animation_state.lying:
                if (aggressive)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                else
                {
                    dog_audio.StopAllSounds();
                    anim_controll.ChangeAnimationState(anim.agg_trans_lying_to_stand);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                    basic_behav.SetShortTimer(2, 2);
                }
                break;
            case Basic_Behaviour.Animation_state.sleeping:
                dog_audio.StopAllSounds();
                anim_controll.ChangeAnimationState(anim.agg_trans_sleep_to_stand);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                basic_behav.SetShortTimer(2, 2);
                break;
            case Basic_Behaviour.Animation_state.sitting:
                dog_audio.StopAllSounds();
                anim_controll.ChangeAnimationState(anim.agg_trans_sit_to_stand);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                basic_behav.SetShortTimer(2, 2);
                break;
            case Basic_Behaviour.Animation_state.walking:
                dog_audio.StopAllSounds();
                anim_controll.ChangeAnimationState(anim.blend_tree);
                basic_behav.y_goal = basic_behav.standing_value;
                basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                basic_behav.SetShortTimer(2, 2);
                break;
            default:
                break;
        }

    }
}
