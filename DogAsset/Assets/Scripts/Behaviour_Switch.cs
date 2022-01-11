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

    //get Player
    GameObject player;

    //distance to trigger behaviour
    float dist;
    float friendly_distance = 3.5f; 

    //behaviour states
    enum Behaviour_state
    {
        initial,
        friendly,
        neutral,
        aggressive,
    }
    Behaviour_state dog_behaviour;


    // Start is called before the first frame update
    void Start()
    {
        //get scripts 
        dog = GameObject.Find("GermanShepherd_Prefab");
        friendly_script = dog.GetComponent<Friendly_Behaviour>();
        neutral_script = dog.GetComponent<Neutral_Behaviour>();
        aggressive_script = dog.GetComponent<Aggressive_Behaviour>();

        friendly_script.enabled = false;
        aggressive_script.enabled = false;
        neutral_script.enabled = false;

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
        if(dist <= friendly_distance)
        {
            SetBehaviour(Behaviour_state.friendly);
        }
        if(dist > friendly_distance)
        {
            SetBehaviour(Behaviour_state.neutral);
        }
    }

    void SetBehaviour(Behaviour_state behaviour)
    {
        if (behaviour == dog_behaviour)
        {
            return;
        }

        Debug.Log("Behaviour set to: " + behaviour);
        dog_behaviour = behaviour;

        friendly_script.enabled = false;
        aggressive_script.enabled = false;
        neutral_script.enabled = false;

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
    }
}
