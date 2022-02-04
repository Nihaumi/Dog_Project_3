using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Switch : MonoBehaviour
{
    //get scripts
    GameObject dog;
    GameObject behaviour_manager;
    public Friendly_Behaviour friendly_script;
    public Neutral_Behaviour neutral_script;
    public Basic_Behaviour basic_script;
    public Aggressive_Behaviour aggressive_script;
    Turning_Behaviour turning_behav;

    //get Player
    GameObject player;

    //distance to trigger behaviour
    public float dist;
    public float friendly_distance = 0.5f;

    [SerializeField] bool friendly;
    [SerializeField] bool agressive;
    [SerializeField] bool neutral;
    [SerializeField] float friendly_timer;
    [SerializeField] float agressive_timer;
    [SerializeField] float neutral_timer;
    [SerializeField] float friendly_time = 60f;
    [SerializeField] float agressive_time = 60f;
    [SerializeField] float neutral_time = 30f;


    //behaviour states
    public enum Behaviour_state
    {
        initial,
        friendly,
        neutral,
        aggressive,
    }
    public Behaviour_state dog_behaviour;


    // Start is called before the first frame update
    void Start()
    {
        //get scripts 
        dog = GameObject.Find("GermanShepherd_Prefab");
        behaviour_manager = GameObject.Find("Behaviour_Manager");
        friendly_script = dog.GetComponent<Friendly_Behaviour>();
        neutral_script = dog.GetComponent<Neutral_Behaviour>();
        basic_script = dog.GetComponent<Basic_Behaviour>();
        aggressive_script = dog.GetComponent<Aggressive_Behaviour>();
        turning_behav = dog.GetComponent<Turning_Behaviour>();

        //set scripts
        DisableScripts();
        //neutral = true;

        //player
        player = GameObject.FindGameObjectWithTag("Player");

        // is updated immediately on first call to Up
        dog_behaviour = Behaviour_state.initial;

        ResetTimers();
    }

    float GetDistanceToObject(GameObject obj_1, GameObject obj_2)
    {
        dist = Vector3.Distance(obj_1.transform.position, obj_2.transform.position);
        return dist;
    }
   void  ResetTimers()
    {
        friendly_timer = friendly_time;
        neutral_timer = neutral_time;
        agressive_timer = agressive_time;
    }
  
    // Update is called once per frame
    void Update()
    {
        //if there is a plyer obj, check distance between player and dog and set scripts
        if (player)
        {
            GetDistanceToObject(player, dog);
            //CheckDistance();
        }
        if (friendly)
        {
            DisableScripts();
            turning_behav.enabled = true;
            friendly_script.enabled = true;
            friendly_timer = friendly_timer - Time.deltaTime * 1;
        }
        else if (neutral)
        {
            DisableScripts();
            turning_behav.enabled = true;
            neutral_script.enabled = true;
            neutral_timer = neutral_timer - Time.deltaTime * 1;
        }
        else if (agressive)
        {
            DisableScripts();
            turning_behav.enabled = false;
            aggressive_script.enabled = true;
            agressive_timer = agressive_timer - Time.deltaTime * 1;
        }
        ChangeBehavioursOnZero();
    }

    [SerializeField] bool friendly_has_been_visited = false;

    void ChangeBehavioursOnZero()
    {
        if (friendly_timer <= 0)
        {
            SetScriptsFalse();
            DisableScripts();
            ResetTimers();
            friendly_has_been_visited = true;
            neutral = true;
        }
        if (neutral_timer <= 0)
        {
            SetScriptsFalse();
            DisableScripts();
            ResetTimers();
            if(!friendly_has_been_visited)
            {
                friendly = true;
            }
            else
            {
                agressive = true;
            }
        }
        if (agressive_timer <= 0 || aggressive_script.aggressive_too_close)
        {
            SetScriptsFalse();
            DisableScripts();
            ResetTimers();
            neutral = true;
        }
    }

    void SetScriptsFalse()
    {
        agressive = false;
        neutral = false;
        friendly = false;
    }

    public void DisableScripts()
    {
        friendly_script.enabled = false;
        aggressive_script.enabled = false;
        neutral_script.enabled = false;
    }

}
