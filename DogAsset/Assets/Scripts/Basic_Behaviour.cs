using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Behaviour : MonoBehaviour
{
    //other script
    GameObject dog;
    Animation_Controll anim_controll;
    Animations anim;
    Behaviour_Switch behav_switch;
    Neutral_Behaviour neutral_behav;

    //turning
    int random_turn_index;

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        behav_switch = dog.GetComponent<Behaviour_Switch>();
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Environment")
        {
            Debug.Log("trigger");
            // neutral_behav.GetRandomIndexFromList(anim.list_turn);//gets random animation from turning list
            anim_controll.ChangeAnimationState(anim.stand_02);
            StartCoroutine(DogCommandWithWaitCoroutine(anim.turn_left_seek));

        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit");
    }
    public void EnableScripts()
    {
        behav_switch.CheckDistance();
        //behav_switch.enabled = true;
    }

    IEnumerator DogCommandWithWaitCoroutine(string new_state)
  {
      Debug.Log("Started Coroutine at timestamp : " + Time.time);
      behav_switch.DisableScripts();
      //behav_switch.enabled = false;
      yield return new WaitForSeconds(2);
      anim_controll.ChangeAnimationState(new_state);
      EnableScripts();
      Debug.Log("Finished Coroutine at timestamp : " + Time.time);
  }
}
