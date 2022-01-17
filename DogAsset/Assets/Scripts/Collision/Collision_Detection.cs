using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Detection : MonoBehaviour
{
    public bool collided;

    //cube that collided
    public GameObject side;

    //objects
    GameObject dir_manager;

    //scripts
    Turning_Direction_Handler turning_dir_handler;

    // Start is called before the first frame update
    void Start()
    {
        //obj & scripts
        dir_manager = GameObject.Find("Direction_Manager");
        turning_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();

        collided = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Environment")
        {
 
            collided = true;
            GetCollidedObject(this.gameObject);
            Debug.Log("COLLISION with cube: " + side.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            collided = false;

        }
    }
    public void GetCollidedObject(GameObject cube)
    {
        side = cube;
    }
}
