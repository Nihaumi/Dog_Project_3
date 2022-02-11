using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Behaviour : MonoBehaviour
{ //other script
    public GameObject dog;
    public GameObject player;
    public GameObject player_target;
    public GameObject pause_target;
    public GameObject dog_parent;
    public GameObject dir_manager;
    public GameObject agg_position;
    public Animator animator;
    public GameObject dog_sound_manager;
    Audio_Sources dog_audio;
    GameObject behav_manager;
    Basic_Behaviour basic_behav;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;
    Neutral_Behaviour neutral_behav;
    Friendly_Behaviour friendly_behav;
    Behaviour_Switch behav_switch;
    Turning_Behaviour turning_behav;
    PlayerInteraction player_interaction;
    Aggressive_Behaviour agg_behav;
    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        player = GameObject.FindGameObjectWithTag("Player");
        player_target = GameObject.Find("target");
        pause_target = GameObject.Find("pause_target");
        dog_parent = GameObject.Find("DOg");
        dir_manager = GameObject.Find("Direction_Manager");
        dog_sound_manager = GameObject.Find("Dog_sound_manager");
        behav_manager = GameObject.Find("Behaviour_Manager");
        basic_behav = dog.GetComponent<Basic_Behaviour>();
        animator = dog.GetComponent<Animator>();
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        turn_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();
        friendly_behav = dog.GetComponent<Friendly_Behaviour>();
        behav_switch = behav_manager.GetComponent<Behaviour_Switch>();
        turning_behav = dog.GetComponent<Turning_Behaviour>();
        player_interaction = player.GetComponent<PlayerInteraction>();
        agg_behav = dog.GetComponent<Aggressive_Behaviour>();
        dog_audio = dog_sound_manager.GetComponent<Audio_Sources>();

        dist_to_target = 0f;
    }
    public bool enter_pause;
    public bool go_to_location;

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseBehaviour()
    {
        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.pause:
                //1. turn towárds pause location 
                //2. walk towards pause location
                //3. if distmace to walk location < value lay down
                go_to_location = true;//bei goto function aufruf grbraucht
                if (lying_down)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                    basic_behav.SetShortTimer(10, 10);//TODO anpasse
                    end_pause = true;
                    go_to_location = false;
                }
                Debug.Log("PAUSE BEHAV");
                break;
            case Basic_Behaviour.Animation_state.lying:
                Debug.Log("PAUSE LYING ");
                ResetBools();
                if (!lying_down)
                {
                    Debug.Log("PAUSE LYING 2");
                    dog_audio.StopAllSounds();
                    basic_behav.ResetParameter();
                    anim_controll.ChangeAnimationState(anim.friendly_trans_lying_to_stand);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    basic_behav.SetShortTimer(3, 3);
                }
                break;
            case Basic_Behaviour.Animation_state.standing:
                Debug.Log("PAUSE Standing");
                ResetBools();
                basic_behav.ResetParameter();
                dog_audio.StopAllSounds();
                anim_controll.ChangeAnimationState(anim.aggresive_blend_tree);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.pause;
                basic_behav.SetShortTimer(3, 3);
                break;
            case Basic_Behaviour.Animation_state.walking:
                Debug.Log("PAUSE Walking");
                ResetBools();
                basic_behav.y_goal = basic_behav.standing_value;
                basic_behav.dog_state = Basic_Behaviour.Animation_state.pause;
                basic_behav.SetShortTimer(3, 3);
                break;
            default:
                break;
        }
    }
    void ResetBools()
    {
        Debug.Log("BOOL RESET");
        facing_pause_location = false;
        facing_player = false;
        started_walking = false;
        lying_down = false;
        enter_pause = false;
        end_pause = false;
        go_to_location = false;
    }

    public void CalculatePauseDist()
    {
        dist_to_target = Vector3.Distance(dog.transform.position, pause_target.transform.position);
    }

    public bool facing_pause_location;
    public bool started_walking;
    public bool facing_player;
    [SerializeField] bool lying_down;
    public bool end_pause;
    public float pause_goal_dist = 2f;
    public float dist_to_target;
    public void GoToPauseLocation()
    {//turn and walk to location
        if (!facing_pause_location)
        {
            Debug.Log("PAUSE WALKTO");
            basic_behav.TurnToTarget(pause_target);
            basic_behav.TurningAndWalkingLogicHandler();
        }
        if (facing_pause_location)
        {//turn to player
            if (!facing_player)
            {
                basic_behav.TurnToTarget(player_target);
                basic_behav.TurningAndWalkingLogicHandler();
                if (basic_behav.GetPlayerOffset(0, 4, 2f, true) == 0)
                {
                    Debug.Log("Player FRONT");
                    facing_player = true;
                }
            }
            if (facing_player)
            {
                if (!lying_down)
                {// lay down
                    Debug.Log("PAUSE LAYDOWN");
                    basic_behav.y_goal = basic_behav.standing_value;
                    basic_behav.x_goal = basic_behav.standing_value;
                    basic_behav.y_acceleration = basic_behav.turning_y_acceleration;
                }
                if (basic_behav.y_axis == basic_behav.standing_value && !lying_down)
                {
                    basic_behav.y_acceleration = basic_behav.default_y_acceleration;
                    anim_controll.ChangeAnimationState(anim.trans_stand_to_lying_02);
                    StartCoroutine(dog_audio.PlaySoundAfterPause(dog_audio.panting_calm));
                    basic_behav.SetShortTimer(3, 3);//TODO anpassen
                    lying_down = true;
                }

            }
        }
    }
}
