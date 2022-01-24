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

    GameObject target_position;

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

        target_position = GameObject.Find("agg_position");

    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("AGGROOO");

    }

    bool is_aggressive = false;
    public bool facing_player = false;

    public Vector3 current_position;
    public Vector3 target_pos;
    public float speed = 1;
    Vector3 direction;
    Quaternion rotation;
    public float dist_to_target;

    /*turn in direction of green cube ( aggression position) and move there while walking animation plays
     * if distance between dig and cube is smaller than 1, dog reached cube
     * rotate towards the player and play standing animation 
     * activate agressive animiation state and play aggressive animation
     */
    /*TODO
     * animations are snappy
     * turning towrads cube works very well,  not for towrads player
     * make head rigging look in camera
     * work on if position not yet reached part (||true)
     */
    public void MoveToPositionAndFacePlayer()
    {
        target_pos = target_position.transform.position;
        current_position = dog.transform.position;
        dist_to_target = Vector3.Distance(dog.transform.position, target_position.transform.position);

        Debug.Log("entering MOVING POS");
        if (!facing_player)
        {
            Debug.Log("not facing player");
            //turn towards player
            if (dist_to_target < 1)
            {
                Debug.Log("POS REACHED, TURNING");
                //rotate towards player
                TurnToTarget(player);
                StartCoroutine(WaitBeforeAggro());

            }
            else
            {
                Debug.Log("POS NOT reached");
                if (anim_controll.current_state == anim.stand_agg || true)
                {
                    Debug.Log("Switching TREE");
                    anim_controll.ChangeAnimationState(anim.blend_tree);
                    basic_behav.y_goal = basic_behav.walking_value;
                }
                if (anim_controll.current_state == anim.blend_tree && basic_behav.y_goal == basic_behav.walking_value)
                {
                    Debug.Log("Walk To POS");
                    TurnToTarget(target_position);
                    StartCoroutine(WaitBeforeWalkingTowards());
                }
            }

        }
    }
    IEnumerator WaitBeforeAggro()
    {
        yield return new WaitForSeconds(5);
        anim_controll.ChangeAnimationState(anim.stand_agg);
        yield return new WaitForSeconds(2);
        facing_player = true;
    }
    IEnumerator WaitBeforeWalkingTowards()
    {
        yield return new WaitForSeconds(3);
        dog.transform.position = Vector3.MoveTowards(current_position, target_position.transform.position, Time.deltaTime * speed);
    }

    void TurnToTarget(GameObject target)
    {
        direction = target.transform.position - dog.transform.position;
        rotation = Quaternion.LookRotation(direction);
        dog.transform.rotation = Quaternion.Lerp(dog.transform.rotation, rotation, speed * Time.deltaTime);
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
