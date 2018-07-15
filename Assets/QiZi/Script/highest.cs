using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class highest : MonoBehaviour {
    public AudioSource WalkA;
    public AudioSource JumpA;
    public AudioSource HomeA;

    public GameObject map;
    public GameObject highh;
    public GameObject[] girds;
    public GameObject[] high_ground;
    public GameObject[] dou_higher;
    public GameObject shit_ground;
    public GameObject double_high;
    public GameObject three_high;
    public GameObject END;
    private Animator anim;
	static public bool CantClick;
    public float turnSpeed = 3.0f;
    public int[] high;
    public int shit;
    public int cha;
    public int[] higher;
    public int the_highest;
    public int head=0;

    private int p;
    private int look = 0;

    public bool isHigh;
    public bool isShit;
    public bool onHigh;
    public bool onHigher;
    public bool onHighest;
    public bool isHigher;
    public bool isHighest;
    
    private bool click=true;
    // Use this for initialization
    void Start()
    {
        anim =GetComponent<Animator>();
		CantClick = false;
        look = head;
        this.transform.rotation = Quaternion.Euler(0, 0, -90 * look);
        high_ground = new GameObject[high.Length];
        dou_higher = new GameObject[higher.Length];
        girds = new GameObject[map.transform.childCount];
        for (int i = 0; i < girds.Length; i++)
        {
            girds[i] = map.transform.GetChild(i).gameObject;
        }
        p = cha;
        this.gameObject.transform.position = girds[p].gameObject.transform.position;
        if (isHigh)
        {
            for (int i = 0; i < high.Length; i++)
                high_ground[i] = Instantiate(highh, girds[high[i]].gameObject.transform.position, girds[high[i]].gameObject.transform.rotation);
        }
        if (isShit)
        {
            shit_ground.gameObject.transform.position = girds[shit].gameObject.transform.position;
        }
        if (isHigher)
        {
            for (int i = 0; i < higher.Length; i++)
                dou_higher[i] = Instantiate(double_high, girds[higher[i]].gameObject.transform.position, girds[higher[i]].gameObject.transform.rotation);
        }
        if(isHighest)
        {
            three_high.gameObject.transform.position = girds[the_highest].gameObject.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.position = girds[p].gameObject.transform.position;
        this.gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, girds[p].gameObject.transform.position, 2f*Time.deltaTime);
        for (int i = 0; i < high.Length; i++)
        {
            if (p == high[i])
            {
                onHigh = true;
                break;
            }
            else
            {
                onHigh = false;

            }
        }
        for (int i = 0; i < higher.Length; i++)
        {
            if (p == higher[i])
            {
                onHigher = true;
                break;
            }
            else
            {
                onHigher = false;

            }
        }
        if (p == the_highest)
            onHighest = true;
        else
            onHighest = false;
       
        if (look == 4)
            look = 0;
        if (look == -1)
            look = 3;

        //this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -90 * look), 5f * Time.deltaTime);
    }
    public void turnleft()
    {
        if (CantClick == true)
        return;
        if(click)
        {
            StartCoroutine("Turn", transform.forward);
            look -= 1;
        }
        
        if (!WalkA.isPlaying)
        {
            WalkA.Play();
        }
    }
    public void turnright()
    {
        if (CantClick == true)
        return;
        if(click)
        {
            StartCoroutine("Turn", -transform.forward);
            look += 1;
        }
        
        if (!WalkA.isPlaying)
        {
            WalkA.Play();
        }
    }
    public void walk()
    {
        if(!Score.run)
        {
            return;
        }
		if (CantClick == true)
			return;
        if (Score.run)
        {
            anim.SetBool("Walk", true);
            if (look == 0 && p < 20)
            {
                p += 5;
                for (int i = 0; i < higher.Length; i++)
                {
                    if (higher[i] == p && !onHigher && !onHighest)
                        p -= 5;
                }
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh && !onHigher && !onHighest)
                        p -= 5;
                }
                if (the_highest == p)
                {
                    p -= 5;
                }
            }
            if (look == 1 && p != 0 && p != 5 && p != 10 && p != 15 && p != 20)
            {
                p -= 1;
                for (int i = 0; i < higher.Length; i++)
                {
                    if (higher[i] == p && !onHigher && !onHighest)
                        p += 1;
                }
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh && !onHigher && !onHighest)
                        p += 1;
                }
                if (the_highest == p)
                {
                    p += 1;
                }
            }
            if (look == 2 && p > 4)
            {
                p -= 5;
                for (int i = 0; i < higher.Length; i++)
                {
                    if (higher[i] == p && !onHigher && !onHighest)

                        p += 5;
                }
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh && !onHigher && !onHighest)
                        p += 5;
                }
                if (the_highest == p)
                {
                    p += 5;
                }
            }
            if (look == 3 && p != 4 && p != 9 && p != 14 && p != 19 && p != 24)
            {
                p += 1;
                for (int i = 0; i < higher.Length; i++)
                {
                    if (higher[i] == p && !onHigher && !onHighest)

                        p -= 1;
                }
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh && !onHigher && !onHighest)
                        p -= 1;
                }
                if (the_highest == p)
                {
                    p -= 1;
                }
            }
        }
    }
    public void jump()
    {
        if(!Score.run)
        {
            return;
        }
		if (CantClick == true)
			return;
        if (isHigh)
        {
            anim.SetBool("Jump", true);
            for (int i = 0; i < high.Length; i++)
            {

                if (p == high[i] - 1 && look == 3 && high[i] % 5 != 0)
                {
                    p = high[i];
                    if (onHigh||onHigher||onHighest)
                        p = high[i] - 1;
                    break;
                }
                if (p == high[i] - 5 && look == 0)
                {
                    p = high[i];
                    if (onHigh || onHigher || onHighest)
                        p = high[i] - 5;
                    break;
                }
                if (p == high[i] + 5 && look == 2)
                {

                    p = high[i];
                    if (onHigh || onHigher || onHighest)
                        p = high[i] + 5;
                    break;
                }
                if (p == high[i] + 1 && look == 1 && high[i] % 5 != 4)
                {
                    p = high[i];
                    if (onHigh || onHigher || onHighest)
                        p = high[i] + 1;
                    break;
                }
            }
            if (onHigh)
            {
                for (int i = 0; i < higher.Length; i++)
                {
                    if (look == 3 && higher[i] % 5 != 0 && p == higher[i] - 1)
                    {
                        p = higher[i];
                        break;

                    }
                    if (p == higher[i] - 5 && look == 0)
                    {
                        p = higher[i];
                        break;

                    }
                    if (p == higher[i] + 5 && look == 2)
                    {

                        p = higher[i];
                        break;

                    }
                    if (p == higher[i] + 1 && look == 1 && higher[i] % 5 != 4)
                    {
                        p = higher[i];
                        break;

                    }
                }
            }
            if(onHigher)
            {
                if (look == 3 && the_highest % 5 != 0 && p == the_highest - 1)
                {
                    p = the_highest;
                    

                }
                if (p == the_highest - 5 && look == 0)
                {
                    p = the_highest;
                    

                }
                if (p == the_highest + 5 && look == 2)
                {

                    p = the_highest;
                    

                }
                if (p == the_highest + 1 && look == 1 && the_highest % 5 != 4)
                {
                    p = the_highest;
                   

                }
            }
            if (onHighest)
                ;
        }
        if (!JumpA.isPlaying)
        {
            JumpA.Play();
        }
    }
    public void push()
    {
		if (CantClick == true)
			return;
        if (p == shit)
            anim.SetTrigger("Push");
        StartCoroutine("Delay");
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (SceneIndex >= 5 + ScenceManage.StageSave)
			ScenceManage.StageSave = SceneIndex - 5;
		PlayerPrefs.SetInt ("Stage", ScenceManage.StageSave);
		Debug.Log (ScenceManage.StageSave);
		CantClick = true;
        if (!HomeA.isPlaying)
        {
            HomeA.Play();
        }
    }
    public void reStart()
    {
        p = cha;
        look = head; ;
        this.gameObject.transform.position = girds[p].gameObject.transform.position;
        this.transform.localRotation = Quaternion.EulerRotation(0, 0, 0);
        Start();
    }
    IEnumerator Turn(Vector3 TargetAxis)
    {
        click=false;
        bool turned = false;
        float turnedAngle = 0;
        while (!turned)
        {


            if (turnedAngle < 90f)
            {
                transform.Rotate(TargetAxis, turnSpeed, Space.Self);
                turnedAngle += turnSpeed;
            }
            else
            {
                turned = true;
                click=true;
            }

            yield return 0;
        }

        StopCoroutine("Turn");

        yield return 0;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        END.SetActive(true);
    }
}

