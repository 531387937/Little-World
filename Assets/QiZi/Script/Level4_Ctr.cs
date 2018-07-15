using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4_Ctr : MonoBehaviour {
    public AudioSource WalkA;
    public AudioSource JumpA;
    public AudioSource HomeA;

    public GameObject map;
    public GameObject highh;
    public GameObject[] girds;
    public GameObject[] high_ground;
    public GameObject[] beauty;
    public GameObject END;
    public float turnSpeed = 3.0f;
    public int[] high;
    public int cha;
    public int[] cloth;
    public int[] beauties;
    private Animator anim;
    private int p;
    public int look = 0;

	static public bool CantClick;

    public bool isHigh;
    public bool onHigh;
    private int numbers;
    private bool click=true;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
		CantClick = false;
        beauties =new int [cloth.Length];
        for (int i = 0; i < beauties.Length; i++)
        {

            beauties[i] = cloth[i];
        }
        this.transform.rotation = Quaternion.Euler(0, 0, -90 * look);
        numbers = beauties.Length;
        high_ground=new GameObject[high.Length];
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
  
       
        for (int i = 0; i < beauties.Length; i++)
        {
            
            beauty[i]=Instantiate(beauty[i], girds[beauties[i]].gameObject.transform.position, girds[beauties[i]].gameObject.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, girds[p].gameObject.transform.position, 2f * Time.deltaTime);
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
		if (CantClick == true)
			return;
        if (Score.run)
        {
            anim.SetBool("Walk", true);
            if (look == 0 && p < 20)
            {
                p += 5;

                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh)
                        p -= 5;
                }
            }
            if (look == 1 && p != 0 && p != 5 && p != 10 && p != 15 && p != 20)
            {
                p -= 1;

                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh)
                        p += 1;
                }
            }
            if (look == 2 && p > 4)
            {
                p -= 5;

                for (int i = 0; i < high.Length; i++)
                {
                    if (p == high[i] && isHigh && !onHigh)
                        p += 5;
                }
            }
            if (look == 3 && p != 4 && p != 9 && p != 14 && p != 19 && p != 24)
            {
                p += 1;

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
		if (CantClick == true)
			return;
        if (isHigh)
        {
            for (int i = 0; i < high.Length; i++)
            {
                anim.SetBool("Jump", true);
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
        anim.SetBool("Collect", true);
        if (!HomeA.isPlaying)
        {
            HomeA.Play();
        }
        for (int i=0;i<beauties.Length;i++)
        {
            if(p==beauties[i])
            {
                numbers--;
                beauty[i].SetActive(false);
                beauties[i] = 100;
                break;
            }
        }
        if (numbers==0)
        {
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
        look = 0;
        for (int i = 0; i < beauties.Length; i++)
        {
            beauty[i].SetActive(true);
           
        }
        this.transform.localRotation = Quaternion.EulerRotation(0, 0, 0);
        beauties = new int[cloth.Length];
        for (int i = 0; i < beauties.Length; i++)
        {

            beauties[i] = cloth[i];
        }
        this.transform.Rotate(0, 0, -90 * look);
        numbers = beauties.Length;
        high_ground = new GameObject[high.Length];
        girds = new GameObject[map.transform.childCount];

        for (int i = 0; i < girds.Length; i++)
        {
            girds[i] = map.transform.GetChild(i).gameObject;
        }
        p = cha;
        this.gameObject.transform.position = girds[p].gameObject.transform.position;
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
