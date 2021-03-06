﻿using UnityEngine;
using System.Collections;

public class moveOnPath : MonoBehaviour {

    public Paths PathToFollow;
    public int CurrentPointID = 0;
    //the speed to move along the path
    public float speed;
    //how close we want to be to a node before aiming for the next one
    public float reachDist = 1f;
    //rotational speed when we need to switch directions
    public float rotateSpeed = 5f;
    public string pathName;

    //this is the target we want to send a message to
    public GameObject msgTarget;
    public string funcName = "This Is the Function We want to Call";

    private bool doPath;
    private int pathSet;

    Vector3 lastPos, currentPos;
	// Use this for initialization
	void Start ()
    {
        doPath = true;
        //PathToFollow = GameObject.Find(pathName).GetComponent<Paths>();
        //doPath = false;
        lastPos = transform.position;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (doPath)
            pathing();
	}


    void pathing()
    {
        float distance = Vector3.Distance(PathToFollow.path_objs[CurrentPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentPointID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(PathToFollow.path_objs[CurrentPointID].position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

        if (distance <= reachDist)
        {
            CurrentPointID++;
        }

        if (CurrentPointID >= PathToFollow.path_objs.Count)
        {
            doPath = false;
            CurrentPointID = 0;
            msgTarget.BroadcastMessage(funcName);
            Debug.Log("Sent message " + funcName);
        }
    }

    public void setPathing(bool go, string wayPointSet)
    {
        doPath = go;
        PathToFollow = GameObject.Find(wayPointSet).GetComponent<Paths>();
    }
}
