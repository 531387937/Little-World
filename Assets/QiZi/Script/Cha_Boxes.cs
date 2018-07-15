using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cha_Boxes : MonoBehaviour {
    public AudioSource WalkA;
    public AudioSource JumpA;
    public AudioSource HomeA;

    public GameObject map;
    public GameObject highh;
    public GameObject[] girds;
    public GameObject[] high_ground;
    public GameObject shit_ground;
    public GameObject double_high;
    public GameObject END;
    private Animator anim;

	static public bool CantClick;
    public float turnSpeed = 3.0f;
    public int[] high;
    public int shit;
    public int cha;
    public int higher;
    public int head = 0;

    private int p;
    private int look ;

    public bool isHigh;
    public bool isShit;
    public bool onHigh;
    public bool isHigher;
    private bool click=true;
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
		CantClick = false;
        look = head;
        this.transform.rotation = Quaternion.Euler(0, 0, -90 * look);
        high_ground =new GameObject[high.Length];
        girds = new GameObject[map.transform.childCount];
        for (int i = 0; i < girds.Length; i++)
        {
            girds[i] = map.transform.GetChild(i).gameObject;
        }
        p = cha;
        this.gameObject.transform.position = girds[p].gameObject.transform.position;
        if (isHigh)
        {
            for(int i=0;i<high.Length;i++)
                high_ground[i]=Instantiate(highh,girds[high[i]].gameObject.transform.position, girds[high[i]].gameObject.transform.rotation);
        }
        if (isShit)
        {
            shit_ground.gameObject.transform.position = girds[shit].gameObject.transform.position;
        }
        if(isHigher)
        {
            Instantiate(double_high, girds[higher].gameObject.transform.position, girds[higher].gameObject.transform.rotation);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, girds[p].gameObject.transform.position, 2.5f * Time.deltaTime);
        
        for (int i=0;i<high.Length;i++)
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
        //this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -90 * look), 5f * Time.deltaTime);
        if (look == 4)
            look = 0;
        if (look == -1)
            look = 3;


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
                if (higher == p && isHigher)
                    p -= 5;
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh)
                        p -= 5;
                }
            }
            if (look == 1 && p != 0 && p != 5 && p != 10 && p != 15 && p != 20)
            {
                p -= 1;
                if (higher == p && isHigher)
                    p += 1;
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh)
                        p += 1;
                }
            }
            if (look == 2 && p > 4)
            {
                p -= 5;
                if (higher == p && isHigher)
                    p += 5;
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh)
                        p += 5;
                }
            }
            if (look == 3 && p != 4 && p != 9 && p != 14 && p != 19 && p != 24)
            {
                p += 1;
                if (higher == p && isHigher)
                    p -= 1;
                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh)
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
                    if (onHigh)
                        p = high[i] - 1;
                    break;
                }
                if (p == high[i] - 5 && look == 0)
                {
                    p = high[i];
                    if (onHigh)
                        p = high[i] - 5;
                    break;
                }
                if (p == high[i] + 5 && look == 2)
                {

                    p = high[i];
                    if (onHigh)
                        p = high[i] + 5;
                    break;
                }
                if (p == high[i] + 1 && look == 1 && high[i] % 5 != 4)
                {
                    p = high[i];
                    if (onHigh)
                        p = high[i] + 1;
                    break;
                }
            }
            if(onHigh)
            {
                if (look == 3 && higher % 5 != 0&&p==higher-1)
                {
                    p = higher;
                    
                }
                if (p == higher - 5 && look == 0)
                {
                    p = higher;
                    
                }
                if (p == higher + 5 && look == 2)
                {

                    p = higher;
                    
                }
                if (p == higher + 1 && look == 1 && higher % 5 != 4)
                {
                    p = higher;
                   
                }
            }
        }
        if (!JumpA.isPlaying)
        {
            JumpA.Play();
        }
    }
    public void push()
    {
        if(!Score.run)
        {
            return;
        }
        if (CantClick == true)
				return;
        if (p == shit)
        {
            if (!HomeA.isPlaying)
            {
                HomeA.Play();
            }
            anim.SetTrigger("Push");
            print("OK");
            StartCoroutine("Delay");
            int SceneIndex = SceneManager.GetActiveScene().buildIndex;
			if (SceneIndex >= 5 + ScenceManage.StageSave)
				ScenceManage.StageSave = SceneIndex - 5;
			PlayerPrefs.SetInt ("Stage", ScenceManage.StageSave);
			Debug.Log (ScenceManage.StageSave);
			CantClick = true;
        }
    }
    public void reStart()
    {
        p = cha;
        look = head; ;
       
        this.transform.localRotation = Quaternion.EulerRotation(0, 0, 0);
        Start();
    }
    IEnumerator Turn(Vector3 TargetAxis)
    {
        click = false;
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
