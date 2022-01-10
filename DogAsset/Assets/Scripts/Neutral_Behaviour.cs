using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral_Behaviour : MonoBehaviour
{
    enum Animation_state //is a class
    {
        sitting,
        standing,
        walking,
        running,
        lying,
        sleeping,
        aggressiv
    };
    Animation_state dog_state;

    //timer
    float change_anim_timer;
    float starting_timer = 5f;
    int new_timer;
    int min_timer;
    int max_timer;

    //other script
    GameObject dog;
    Animation_Controll anim_controll;
    Animations anim;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started now");

        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();

        //state
        anim_controll.current_state = anim.stand_01;
        dog_state = Animation_state.standing;

        //timer
        change_anim_timer = starting_timer;
        min_timer = 5;
        max_timer = 10;
    }

    //Timer
    void ResetTimerFunction()
    {
        if (change_anim_timer <= 0)
        {
            change_anim_timer = ChooseRandomTimer();
            SetLongTimer();
        }
    }
    int ChooseRandomTimer()
    {
        new_timer = Random.Range(min_timer, max_timer);
        return new_timer;
    }

    void SetShortTimer()
    {
        min_timer = 2;
        max_timer = 3;
    }

    void SetLongTimer()
    {
        min_timer = 5;
        max_timer = 10;
    }

    //choosing random index of animation lists
    int random_index = 0;
    int list_length = 0;
    int ChooseRandomIndex()
    {
        switch (dog_state)
        {
            case Animation_state.standing:
                random_index = Random.Range(0, anim.list_standing.Count - 1);
                DisplayList(anim.list_standing);
                //Debug.Log("von standing  list");
                break;
            case Animation_state.sitting:
                random_index = Random.Range(0, anim.list_sitting.Count - 1);
                DisplayList(anim.list_sitting);
                //Debug.Log("von SITTING  list");
                break;
            case Animation_state.sleeping:
                random_index = Random.Range(0, anim.list_sleeping.Count - 1);
                DisplayList(anim.list_sleeping);
                //Debug.Log("von sLEEPING  list");
                break;
            case Animation_state.walking:
                random_index = Random.Range(0, anim.list_walking.Count - 1);
                DisplayList(anim.list_walking);
                //Debug.Log("von WALKING  list");
                break;
            case Animation_state.running:
                random_index = Random.Range(0, anim.list_running.Count - 1);
                DisplayList(anim.list_running);
                //Debug.Log("von running  list");
                break;
            case Animation_state.lying:
                random_index = Random.Range(0, anim.list_lying.Count - 1);
                DisplayList(anim.list_lying);
                //Debug.Log("von lying  list");
                break;
            default:
                break;
        }
        //Debug.Log("index " + random_index);
        return random_index;
    }

    void DisplayList(List<string> list)
    {
        list_length = list.Count;
        int i = 0;
        while(i < list_length)
        {
           // //Debug.Log("list item number: "+ i + "is" + list[i]);
            i++;
        }
    }

    private void OnEnable()
    {
        Debug.Log("is enabled now");
    }

    // Update is called once per frame
    void Update()
    {
        change_anim_timer = change_anim_timer - Time.deltaTime;

        if (change_anim_timer <= 0)
        {
            ChooseRandomIndex();
            switch (dog_state)
            {
                case Animation_state.standing:
                    anim_controll.ChangeAnimationState(anim.list_standing[random_index]);
                    Debug.Log("standinglist item at rndindex: " + random_index + "is:" + anim.list_standing[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.lying;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.sitting;
                    }
                    if (random_index > 1)
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.sitting:
                    anim_controll.ChangeAnimationState(anim.list_sitting[random_index]);
                    //Debug.Log("sitting list item at rndindex: " + random_index + "is:" + anim.list_sitting[random_index]);
                    dog_state = Animation_state.walking;
                    break;
                case Animation_state.lying:
                    anim_controll.ChangeAnimationState(anim.list_lying[random_index]);
                    //Debug.Log("lying list item at rndindex: " + random_index + "is:" + anim.list_lying[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.sleeping;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.sleeping:
                    anim_controll.ChangeAnimationState(anim.list_sleeping[random_index]);
                    //Debug.Log("sleeping list item at rndindex: " + random_index + "is:" + anim.list_sleeping[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.lying;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.standing;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.walking:
                    anim_controll.ChangeAnimationState(anim.list_walking[random_index]);
                    //Debug.Log("walking list item at rndindex: "+ random_index + "is:" + anim.list_walking[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.standing;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.running;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.running:
                    anim_controll.ChangeAnimationState(anim.list_running[random_index]);
                    //Debug.Log("running list item at rndindex: " + random_index + "is:" + anim.list_running[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.standing;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                default:
                    return;
            }
            ResetTimerFunction();
            //Debug.Log("new state " + dog_state);
        }
    }

    /*IEnumerator DogCommandWithWaitCoroutine(string new_state)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(2);
        anim_controll.ChangeAnimationState(new_state);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }*/
}
