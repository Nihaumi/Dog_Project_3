using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Switch : MonoBehaviour
{
    //get scripts
    GameObject dog;
    Friendly_Behaviour friendly_script;
    Neutral_Behaviour neutral_script;
    Aggressive_Behaviour aggressive_script;
    Collision_Behaviour basic_behav_script;

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
        friendly_script = dog.GetComponent<Friendly_Behaviour>();
        neutral_script = dog.GetComponent<Neutral_Behaviour>();
        aggressive_script = dog.GetComponent<Aggressive_Behaviour>();

        //scripts
        friendly_script.enabled = false;
        aggressive_script.enabled = false;
        neutral_script.enabled = false;
        all_scripts_off = true;

        //player
        player = GameObject.Find("Player");

        // is updated immediately on first call to Up
        dog_behaviour = Behaviour_state.initial;
    }

    float GetDistanceToObject(GameObject obj)
    {
        dist = Vector3.Distance(obj.transform.position, transform.position);
        return dist;
    }

    // Update is called once per frame
    void Update()
    {
        //ruft wohl jeden frame switchscripts auf 
        //neutral scrioot kann nicht dinge tun, weil an aus an aus
        GetDistanceToObject(player);
        CheckDistance();
    }
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

    void SetBehaviour(Behaviour_state behaviour)
    {
        if (neutral_script.dog_state == Neutral_Behaviour.Animation_state.turning_left && all_scripts_off)
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
        neutral_script.enabled = false;
        all_scripts_off = true;
    }

}
