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

    //get Player
    GameObject player;

    //distance to trigger behaviour
    float dist;
    float friendly_distance = 3.5f;

    //script disabling
    bool all_scripts_off;

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

        //set scripts
        friendly_script.enabled = false;
        aggressive_script.enabled = false;
        neutral_script.enabled = true;
        all_scripts_off = true;

        //player
        player = GameObject.FindGameObjectWithTag("Player");

        // is updated immediately on first call to Up
        dog_behaviour = Behaviour_state.initial;
    }

    float GetDistanceToObject(GameObject obj_1, GameObject obj_2)
    {
        dist = Vector3.Distance(obj_1.transform.position, obj_2.transform.position);
        return dist;
    }

    // Update is called once per frame
    void Update()
    {
        //if there is a plyer obj, check distance between player and dog and set scripts
        if (player)
        {
            GetDistanceToObject(player, dog);
            CheckDistance();
        }
    }

    //switch between behaviours states according to distance
    public void CheckDistance()
    {
        if (dist <= friendly_distance)
        {
            SetBehaviour(Behaviour_state.friendly);
        }
        if (dist > friendly_distance)
        {
            SetBehaviour(Behaviour_state.neutral);
        }
    }

    //
    void SetBehaviour(Behaviour_state behaviour)
    {
        if (basic_script.dog_state == Basic_Behaviour.Animation_state.turning_left && all_scripts_off)
        {
            EnableBehaviourScripts(behaviour);
        }

        if (behaviour == dog_behaviour)
        {
            return;
        }
        EnableBehaviourScripts(behaviour);
    }
    void EnableBehaviourScripts(Behaviour_state behaviour)
    {
        Debug.Log("Behaviour set to: " + behaviour);
        dog_behaviour = behaviour;

        DisableScripts();

        if (dog_behaviour == Behaviour_state.friendly)
        {
            friendly_script.enabled = true;
        }
        if (dog_behaviour == Behaviour_state.neutral)
        {
            neutral_script.enabled = true;
        }
        if (dog_behaviour == Behaviour_state.aggressive)
        {
            aggressive_script.enabled = true;
        }
        all_scripts_off = false;
    }

    public void DisableScripts()
    {
        friendly_script.enabled = false;
        aggressive_script.enabled = false;
        //neutral_script.enabled = false;
        all_scripts_off = true;
    }

}
