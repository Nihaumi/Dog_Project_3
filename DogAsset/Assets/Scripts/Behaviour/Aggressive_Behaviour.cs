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



    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("AGGROOO");

    }

    bool is_aggressive = false;
    public bool facing_player = false;


    public float dist_to_target;

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
    public void MoveToPositionAndFacePlayer()
    {


        Debug.Log("entering MOVING POS");
        if (!facing_player)
        {
            Debug.Log("not facing player");
            //check if dog is on position
            if (dist_to_target < 1)
            {
                Debug.Log("POS REACHED, TURNING");
                //rotate towards player
                basic_behav.TurnToTarget(player);

                StartCoroutine(WaitBeforeAggro());

            }
            //if dog is not on position, switch animation to walking, then turn towards and walk to position
            else
            {
                if (anim_controll.current_state == anim.blend_tree && basic_behav.y_goal == basic_behav.walking_value)
                {
                    Debug.Log("Walk To POS");
                    basic_behav.TurnToTarget(basic_behav.agg_position);
                    StartCoroutine(basic_behav.WaitBeforeWalkingTowards(basic_behav.agg_position));
                }
                else
                {
                    Debug.Log("Switching TREE");
                    anim_controll.ChangeAnimationState(anim.blend_tree);
                    basic_behav.y_goal = basic_behav.walking_value;
                }
            }

        }
    }

    IEnumerator WaitBeforeAggro()
    {
        basic_behav.turning_in_place = false;
        anim_controll.ChangeAnimationState(anim.stand_agg);
        yield return new WaitForSeconds(4);
        facing_player = true;
    }
    public float dist_to_player;
    public void StopAgression()
    {
        if (facing_player)
        {

            if (dist_to_player < 4f)
            {
                anim_controll.ChangeAnimationState(anim.trans_agg_to_stand);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                facing_player = false;
                //tell behav switch to switch to neutral/friendly
            }
        }
    }


    public void AggressiveBehaviour()
    {
        Debug.Log("AGRssive FUNCTION");
        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.aggressiv:
                if (facing_player)
                {

                    dog_audio.StopAllSounds();
                    if (basic_behav.random_index == 1 && is_aggressive)
                    {
                        anim_controll.ChangeAnimationState(anim.aggressive);
                        dog_audio.StopAllSounds();
                        dog_audio.aggressive_bark.Play();
                    }
                    if (basic_behav.random_index == 1 && is_aggressive)
                    {
                        anim_controll.ChangeAnimationState(anim.bite_L);
                    }
                    if (basic_behav.random_index == 1 && is_aggressive)
                    {
                        anim_controll.ChangeAnimationState(anim.bite_R);
                    }
                    if (!is_aggressive)
                    {
                        anim_controll.ChangeAnimationState(anim.trans_stand_to_agg);
                        is_aggressive = true;
                        dog_audio.aggressive_bark.Play();
                    }
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                }
                break;
            case Basic_Behaviour.Animation_state.standing:
                dog_audio.StopAllSounds();
                anim_controll.ChangeAnimationState(anim.stand_agg);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                basic_behav.SetShortTimer(2, 2);
                break;
            case Basic_Behaviour.Animation_state.lying:
                dog_audio.StopAllSounds();
                anim_controll.ChangeAnimationState(anim.agg_trans_lying_to_stand);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.aggressiv;
                basic_behav.SetShortTimer(2, 2);
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
