﻿using UnityEngine;
using System.Collections;

public class tourScript : MonoBehaviour {

    public string[,] script = new string[10,2];
    private int currLine;

    public GameObject diverSpeechBubble, fishSpeechBubble;
    private SpeechBubble diverSpeechScript, fishSpeechScript;

    public float textspeed = 5;

    private Animator diverAnim, fishAnim, CameraAnim;

    public GameObject[] diverTargets, fishTargets;
    public GameObject diverCurrentTarget, fishCurrentTarget;

    public GameObject fish, diver, cameraLocation, miniMap, viewcamera, supersucker; //viewcamera is basically the camera location, but y is at the feet of the diver


    public bool exitCutscene = false;
    private bool diverMove = true, fishMove = true;
    public bool moveOnFromBreakpoint = false;
    public bool moveOnToThree = false;
    public bool moveOnToFour = false;

    private bool supersuckerMove = false, superSuckerOn = true;
    


    // Use this for initialization
    void Start () {

        diverSpeechScript = diverSpeechBubble.GetComponent<SpeechBubble>();
        fishSpeechScript = fishSpeechBubble.GetComponent<SpeechBubble>();
        diverAnim = gameObject.GetComponent<Animator>();
        fishAnim = gameObject.GetComponent<Animator>();
        CameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();

        //set the script for the different characters
        script[0,0] = "diver"; script[0,1] = "Hello! I'm so glad you're here!";
        script[1,0] = "aumakua"; script[1,1] = "It's about time you showed up, my reef is getting ruined!";
        script[2,0] = "diver"; script[2,1] = "Well, we are going to try and help fix it. But first, let's check in on this little urchin.";
        script[3,0] = "diver"; script[3,1] = "This is Tripneustes gratilla, a type of sea urchin.";
        script[4,0] = "aumakua"; script[4,1] = "It's also called a Collector Urchin!";
        script[5,0] = "diver"; script[5,1] = "This little friend eats, algae, which is a good thing because there's a lot of it here.";
        script[6,0] = "aumakua"; script[6,1] = "Yeah, and it's taking over my reef!";
        script[7,0] = "diver"; script[7,1] = "But even though there are a lot of these Collector Urchins here, they can't keep up with how fast this algae grows.";
        script[8,0] = "aumakua"; script[8,1] = "It's invasive! That means it wasn't growing here originally and was introduced to the area by humans.";
        script[9,0] = "diver"; script[9,1] = "Yep, so now we have to help out and try to remove what we can so the reef can bounce back and get healthy.";
        script[10, 0] = "aumakua"; script[10, 1] = "This is Kaneohe Bay, on Oahu. It used to be a nice reef, but lately these algae have shown up and they are taking over!";
        script[11, 0] = "diver"; script[11, 1] = "There are three species causing problems.";
        script[12, 0] = "diver"; script[12, 1] = "We have the acanthrophora spicifera";
        script[13, 0] = "aumakua"; script[13, 1] = "Sometimes people call it Spiny Seaweed";
        script[14, 0] = "diver"; script[14, 1] = "There's also kappaphycus alvarezii";
        script[15, 0] = "diver"; script[15, 1] = "And lastly we've got gracilera salicornia";
        script[16, 0] = "aumakua"; script[16, 1] = "It's also called gorilla ogo!";
        script[17, 0] = "diver"; script[17, 1] = "So these three algae are taking over the reef, and all three were introduced by us humans.";
        script[18, 0] = "aumakua"; script[18, 1] = "That's why you should fix it!";
        script[19, 0] = "diver"; script[19, 1] = "Yep, we can't depend on our friend here to do all the work, the algae grows too fast.";
        script[20, 0] = "diver"; script[20, 1] = "So we've got to pull off what we can and use the Super Sucker to remove as much of it as we can.";
        script[21, 0] = "aumakua"; script[21, 1] = "What's a Super Sucker?";
        script[22, 0] = "diver"; script[22, 1] = "It's a special underwater vacuum that we can use to help clean algae off the reef! Call it down when you're ready!";




        StartCoroutine("play");

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (moveOnFromBreakpoint == true)
        {
            Debug.Log("Moving on to part 2!");
        }
        if (moveOnToThree == true)
        {
            Debug.Log("Moving on to part 3!");
        }

        if (moveOnToFour == true)
        {
            Debug.Log("Moving on to part 4!");

        }

        if (supersuckerMove)
        {
            supersucker.transform.position = Vector3.MoveTowards(supersucker.transform.position, gameObject.transform.position, 0.05f);
        }
        if (exitCutscene == false)
        {
            if(fishCurrentTarget) moveFish(fishCurrentTarget);
            if (diverCurrentTarget) moveDiver(diverCurrentTarget);
            
            
            
        }


    }

    IEnumerator play()
    {
        Debug.Log("starting script");
        yield return new WaitForSeconds(6);
        setDiverWave(false);

        StartCoroutine("sayNextLine");
        
        yield return new WaitForSeconds(2.5f);
        fishCurrentTarget = fishTargets[0];
        moveFish(fishCurrentTarget);

        yield return new WaitForSeconds(2.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(2.5f);
        diverCurrentTarget = diverTargets[0];
        moveDiver(diverCurrentTarget);
        yield return new WaitForSeconds(2.5f);
        fishCurrentTarget = fishTargets[1];
        moveFish(fishCurrentTarget);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(3.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("sayNextLine");
        yield return new WaitForSeconds(2.5f);



        //need a way to wait for a signal from update
        while (moveOnFromBreakpoint == false)
        {
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Got to end of play coroutine!");

       
    }

    IEnumerator sayNextLine()
    {

        if (script[currLine,0] == "diver")
        {
            updateDiverBubble(script[currLine,1]);
            showDiverBubble(true);
            currLine++;
            yield return new WaitForSeconds(5);
            showDiverBubble(false);
        }
        else if(script[currLine,0] == "aumakua")
        {
            updateFishBubble(script[currLine,1]);
            showFishBubble(true);
            currLine++;
            yield return new WaitForSeconds(5);
            showFishBubble(false);
        }
    
    }

    void turnCamera(GameObject target)
    {

        cameraLocation.GetComponent<Animator>().enabled = false;
        Vector3 newDir = new Vector3(target.transform.position.x, cameraLocation.transform.position.y, target.transform.position.z);
        cameraLocation.transform.rotation = Quaternion.Slerp(cameraLocation.transform.rotation, Quaternion.LookRotation(newDir - cameraLocation.transform.position), 1 * Time.deltaTime);
        
    }

    void moveDiver(GameObject moveTarget)
    {

        diverAnim.SetBool("move", true);


        if (moveTarget.transform.position != diver.transform.position)
        {
            Vector3 direction = moveTarget.transform.position - diver.transform.position;
            diverLookAtTarget(moveTarget);
            diver.transform.position = Vector3.MoveTowards(diver.transform.position, moveTarget.transform.position, 0.05f);
            Debug.Log("Moving!");
            turnCamera(moveTarget);
        }
        else
        {
            Debug.Log("Not Moving!");
            stopMoveDiver();
            diverLookAtTarget(viewcamera);

        }
    }

    void stopMoveDiver()
    {
        diverAnim.SetBool("move", false);
    }

    void moveFish(GameObject moveTarget)
    {
        if (moveTarget.transform.position != fish.transform.position)
        {
            Vector3 direction = moveTarget.transform.position - fish.transform.position;
            fishLookAtTarget(moveTarget);
            fish.transform.position = Vector3.MoveTowards(fish.transform.position, moveTarget.transform.position, 0.05f);
            turnCamera(moveTarget);
        }
        else
        {
            stopMoveFish();
            fishLookAtTarget(viewcamera); 

        }
    }

    void stopMoveFish()
    {
        fishAnim.SetBool("move", false);
    }
   

    void setDiverWave(bool value)
    {
        diverAnim.SetBool("wave", value);
    }

    void showDiverBubble(bool value)
    {
        diverSpeechBubble.SetActive(value);
    }

    void updateDiverBubble(string text)
    {
        diverSpeechScript.text = text;
    }

    void destroyDiverBubble()
    {
        //run the animation to destroy the object
        showDiverBubble(false);
    }


    void showFishBubble(bool value)
    {
        fishSpeechBubble.SetActive(value);
    }

    void updateFishBubble(string text)
    {
        fishSpeechScript.text = text;
    }

    void destroyFishBubble()
    {
        //run the animation to destroy the object
        showFishBubble(false);
    }

    
    void fishLookAtTarget(GameObject target)
    {
        Vector3 newDir = new Vector3(target.transform.position.x, fish.transform.position.y, target.transform.position.z);
        fish.transform.rotation = Quaternion.LookRotation(newDir - fish.transform.position);
    }

    void diverLookAtTarget(GameObject target)
    {
        Debug.Log("turning diver!");
        Vector3 newDir = new Vector3(target.transform.position.x, diver.transform.position.y, target.transform.position.z);
        diver.transform.rotation = Quaternion.LookRotation(newDir - diver.transform.position);
    }

    public void GetSuperSucker()
    {
        if (superSuckerOn == true)
        {
            supersucker.SetActive(true);
            supersuckerMove = true;
        }

    }




}
