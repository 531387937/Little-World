using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Role_MoveForward : MonoBehaviour {

    public AudioSource WalkA;
    public AudioSource JumpA;
    public AudioSource HomeA;

    public AudioSource CollectA;
    public AudioSource ProduceA;

    public Animator anim;
    public Animator animC;
    public Animator animP;
    public GameObject Tar;

    enum Direction {up,right,down,left};
	Direction Dir = Direction.down;
	private bool blocked;
	private bool outOfRange;
	public bool goal;
	public bool Cancollect;
	public bool Canproduce;
	private bool collect;
	private bool produce;
	public bool Collected;
	public bool Produced;
	public bool Uncertain;
	public bool StageClear;
	public bool OnTheBox;
	private float dice;
	private int count;
	public GameObject text1;
	public GameObject text2;
	public GameObject uncertain;
	public GameObject parent;
	public GameObject pro;
	public GameObject col;
    public GameObject END;
	private Vector3 Current;
	private Vector3 Target;
    private Vector3 TargetM;

    private Vector3 CurrentR;
    private Vector3 TargetR;
    private Vector3 TargetL;

    static public bool clicked;

	static public bool CantClick;

	// Use this for initialization
	void Start () {
		this.Dir = Direction.down;
		this.blocked = false; 
		this.outOfRange = false;
		this.Cancollect = false;
		this.Canproduce = false;
		this.collect = false;
		this.produce = false;
		this.Collected = false;
		this.Produced = false;
		this.count = 0;
		this.StageClear = false;
		this.OnTheBox = false;
		clicked = false;
		CantClick = false;
		//		this.transform.position = new Vector3 (0.91f, 0.58f, 0.0f);
		Current  = new Vector3(0, 0, 0);
		TargetM = new Vector3(0.0f, -1.34f, 0.0f);
        //Target = new Vector3(0.355f, -1.328f, 0.0f);
        Target = new Vector3(0.0f, -1.37f, 0.0f);

        CurrentR = new Vector3(0, 0, 0);
        TargetR = new Vector3(0, 0, -90);
        TargetL = new Vector3(0, 0, 90);

    }

	void Update(){
        //this.transform.Translate (Current);
        this.transform.position = Vector3.MoveTowards(transform.position, Tar.transform.position, 2.5f * Time.deltaTime);
        //this.transform.Rotate(CurrentR);
        
    }




    public void MoveForward()
	{
        
		if (CantClick == true)
			return;
		if (clicked == true) 
			return;
		//clicked = true;
		anim.SetBool("IsForward",true);
		if (outOfRange == true) {
			Debug.Log ("Forward1");
			clicked = false;
			anim.SetBool("IsForward",false);
			return;}
		if (blocked == true) {
			if (OnTheBox == false) {
				Debug.Log ("Forward2");
				clicked = false;
				anim.SetBool("IsForward",false);
				return;
			} 
		} else
			OnTheBox = false;
		if (collect == true) {
			Debug.Log("Can Collect!");
			Cancollect = true;
		}
		else
			Cancollect = false;
		if (produce == true) {
			Debug.Log("Can Produce!");
			Canproduce = true;
		}
		else
			Canproduce = false;
		if (goal == true) {

            //			Debug.Log ("clear1");
            StartCoroutine("Delay");
			int SceneIndex = SceneManager.GetActiveScene().buildIndex;
			if (SceneIndex >= 5 + ScenceManage.StageSave)
				ScenceManage.StageSave = SceneIndex - 5;
			PlayerPrefs.SetInt ("Stage", ScenceManage.StageSave);
			Debug.Log (ScenceManage.StageSave);
			CantClick = true;
            if (!HomeA.isPlaying)
            {
                StartCoroutine(WaitH());
            }
        }
		if (Uncertain == true) {
			Debug.Log("Uncertain Grid!");
			//获得0-1之间的随机数
			dice  = Random.Range(0.0f,1.0f);
			if (count >= 6) {
				dice = 0;
			} 
			else if (count == 2) {
				dice = 1;
			}
			//如果小于0.5是花，大于是酿蜜机
			if (dice <= 0.5) {
				col.SetActive (true);
				Cancollect = true;
                animC = col.GetComponent<Animator>();
				uncertain.SetActive (false);
				Uncertain = false;
				count++;
//				Debug.Log (count);
			} 
			else {
				pro.SetActive (true);
				Canproduce = true;
                animP = pro.GetComponent<Animator>();
                if (SceneManager.GetActiveScene ().name == "Stage5-3") {
					Collected = true;
				}
				uncertain.SetActive (false);
				Uncertain = false;
				count += 3;
//				Debug.Log (count);
			}
			//在场景中找tag为为collect或者produce的tilemap（也可以通过上面定义的Pro和Col获取）
			//激活，SetActive
			//把当前Uncertain地图关掉
		}
		

//		this.transform.Translate (0.04f, -1.34f, 0.0f);

		Debug.Log("Move!");
        if (!WalkA.isPlaying)
        {
            WalkA.Play();
        }
        Tar.transform.Translate(Target);
        //Vector3 tempPos = Tar.transform.position;
        //Current = Vector3.MoveTowards(this.transform.position, tempPos, 2.5f * Time.deltaTime);
        //Current = Vector3.MoveTowards (Current, Target, 0.02f);
        Debug.Log("Wait!");
        StartCoroutine(Wait());
    }

	public void TurnRight(){
		int temp;

        if (CantClick == true)
			return;

        if (clicked == true) 
			return;

        //clicked = true;
        Debug.Log("3");
        if (Dir >= Direction.left) {
			Dir = Direction.up;
		}
		else
			this.Dir += 1;
		temp = (100 + (int)Dir) % 4;
		switch (temp) {
		case 0:
			Dir = Direction.up;
			break;
		case 1:
			Dir = Direction.right;
			break;
		case 2:
			Dir = Direction.down;
			break;
		case 3:
			Dir = Direction.left;
			break;
		default:
			Debug.Log ("transform failure!");
			break;
		}

        Debug.Log("RoteteR!");
        Tar.transform.Rotate(TargetR);
        StartCoroutine("Turn", -transform.forward);
        //Vector3 tempRot = this.transform.localEulerAngles;
        //CurrentR = Vector3.MoveTowards(CurrentR, TargetR, 1);
        //Quaternion tempRot = Tar.transform.rotation ;
        //CurrentR = Vector3.MoveTowards(CurrentR, TargetR, 150f * Time.deltaTime);
        if (!WalkA.isPlaying)
        {
            WalkA.Play();
        }
        Debug.Log("Wait!");
        StartCoroutine(WaitR());
        //this.transform.Rotate(new Vector3(0,0,-90));
        //		Debug.Log (Dir);
    }

	public void TurnLeft(){
		int temp;

		if (CantClick == true)
			return;

		if (clicked == true) 
			return;
        //clicked = true;
        if (Dir == Direction.right) {
			Dir = Direction.up;
		}
		else
			this.Dir -= 1;
		temp = (100 + (int)Dir) % 4;
		switch (temp) {
		case 0:
			Dir = Direction.up;
			break;
		case 1:
			Dir = Direction.right;
			break;
		case 2:
			Dir = Direction.down;
			break;
		case 3:
			Dir = Direction.left;
			break;
		default:
			Debug.Log ("transform failure!");
			break;
		}

        Debug.Log("RoteteL!");
        Tar.transform.Rotate(TargetL);
        //Vector3 tempRot = this.transform.localEulerAngles;
        //Quaternion tempRot = Tar.transform.rotation;
        //Quaternion tempRot = this.transform.rotation;
        //CurrentR = Vector3.MoveTowards(CurrentR, TargetL, 1);
        //CurrentR = Vector3.MoveTowards(CurrentR, TargetL, 150f * Time.deltaTime);
        if (!WalkA.isPlaying)
        {
            WalkA.Play();
        }
        Debug.Log("Wait!");
        StartCoroutine(WaitL());
        StartCoroutine("Turn", transform.forward);
        //this.transform.Rotate(new Vector3(0,0,90));
        //		Debug.Log (Dir);
    }

	public void Collect()
	{

		if (CantClick == true)
			return;

		Debug.Log("Collecting");
		if (Cancollect == false) {
			Debug.Log("CannotCollecting");
			return;
		}
		else {
			Debug.Log("Collected!");
            if (!CollectA.isPlaying)
            {
                CollectA.Play();
            }
            animC.SetBool("IsCollected", true);
            StartCoroutine(Wait2());
            text2.gameObject.SendMessage ("Show");
			Collected = true;

			if (SceneManager.GetActiveScene ().name == "Stage5-3" || SceneManager.GetActiveScene ().name == "Stage5-1"){
                StartCoroutine("Delay");
                int SceneIndex = SceneManager.GetActiveScene().buildIndex;
				if (SceneIndex >= 5 + ScenceManage.StageSave)
					ScenceManage.StageSave = SceneIndex - 5;
				PlayerPrefs.SetInt ("Stage", ScenceManage.StageSave);
				Debug.Log (ScenceManage.StageSave);
				CantClick = true;
            }
        }
	}

	public void Produce ()
	{

		if (CantClick == true)
			return;

		if (Canproduce == false || Collected == false)
			return;
		else {

            animP.SetBool("IsProduced", true);
            StartCoroutine(Wait2());
            Produced = true;
            if (!ProduceA.isPlaying)
            {
                ProduceA.Play();
            }
            text1.gameObject.SendMessage ("Show");
			Debug.Log("Produced!");
            StartCoroutine("Delay");
            int SceneIndex = SceneManager.GetActiveScene().buildIndex;
			if (SceneIndex >= 5 + ScenceManage.StageSave)
				ScenceManage.StageSave = SceneIndex - 5;
			PlayerPrefs.SetInt ("Stage", ScenceManage.StageSave);
			Debug.Log (ScenceManage.StageSave);
			CantClick = true;

        }
			
		
	}

	public void Jump()
	{

		if (CantClick == true)
			return;

		if (blocked == false || OnTheBox == true)
			return;
		else {
//			this.transform.Translate (0.04f, -1.34f, 0.0f);
			anim.SetBool("IsJump",true);
            if (!JumpA.isPlaying)
            {
                JumpA.Play();
            }
            Debug.Log("Jump!");
            Tar.transform.Translate(Target);
            //Vector3 tempPos = this.transform.position;
            this.OnTheBox = true;
            //Current = Vector3.MoveTowards(Current, Target, 0.02f);
            //Current = Vector3.MoveTowards (Current, TargetM, 2.5f * Time.deltaTime);
			Debug.Log("Wait!");
			StartCoroutine(Wait());
		}
	}

	void OnTriggerStay2D(Collider2D other){
		this.outOfRange = false;
//		Debug.Log (outOfRange);
		if (other.CompareTag ("highland")){
			Debug.Log ("Stay");
			blocked = true;
		}
		if (other.CompareTag ("goal")) {
            Debug.Log("GOAL");
            goal = true;
		} else
			goal = false;
		if (other.CompareTag ("Uncertain")){
			Debug.Log("Uncertain Trigger");
			Uncertain = true;
			uncertain = other.gameObject;
			parent = other.gameObject.transform.parent.gameObject;
			col = parent.transform.GetChild(0).gameObject;
			pro = parent.transform.GetChild(1).gameObject;
//			col = other.gameObject.GetComponentInParent<Transform> ().gameObject.transform.Find("Sprite-Collect").gameObject;
//			pro = other.gameObject.GetComponentInParent<Transform> ().gameObject.transform.Find("Sprite-Produce").gameObject;
		}
		if (other.CompareTag ("Collect")){
			collect = true;
            animC = other.GetComponent<Animator>();
        }
		else
			collect = false;
		if (other.CompareTag ("Produce")){
			produce = true;
            animP = other.GetComponent<Animator>();
        }
		else
			produce = false;
	}

	void OnTriggerExit2D(Collider2D other){
		this.outOfRange = true;
//		Debug.Log (outOfRange);
		if (other.CompareTag ("highland")) {
			blocked = false;
		}
		if (other.CompareTag ("Uncertain")){
			Uncertain = false;
		}
//		if (other.CompareTag ("Collect")) {
//			Cancollect = false;
//		}
//		if (other.CompareTag ("Produce")) {
//			Canproduce = false;
//		}
	}

    IEnumerator Wait()
    {

        Debug.Log("Before Waiting 0.6 seconds");
        yield return new WaitForSeconds(0.582f);
        Debug.Log("After Waiting 0.6 Seconds");
        Current = new Vector3(0, 0, 0);
        CurrentR = new Vector3(0, 0, 0);
        anim.SetBool("IsForward", false);
        anim.SetBool("IsJump", false);
        if (animC != null)
        {
            animC.SetBool("IsCollected", false);
            animP.SetBool("IsProduced", false);
        }
        yield return new WaitForSeconds(0.35f);
        clicked = false;
        Debug.Log("click = false");
        //this.transform.position = tempPos + Target;
        //this.transform.localPosition = tempPos;
    }


    IEnumerator WaitL()
    {

        Debug.Log("Before Waiting 0.763 seconds");
        yield return new WaitForSeconds(0.75f);
        //yield return new WaitForSeconds(0.7644f);
        Debug.Log("After Waiting 0.763 Seconds"); ;
        //CurrentR = new Vector3(0, 0, 0);
        anim.SetBool("IsForward", false);
        anim.SetBool("IsJump", false);
        //yield return new WaitForSeconds(0.35f);
        clicked = false;
        Debug.Log("click = false");
        //this.transform.Rotate(tempRotation + TargetL);
        //this.transform.rotation = tempRotation;
    }

    IEnumerator WaitR()
    {

        Debug.Log("Before Waiting 0.763 seconds");
        yield return new WaitForSeconds(0.75f);
        //yield return new WaitForSeconds(0.7644f);
        Debug.Log("After Waiting 0.763 Seconds"); ;
        //CurrentR = new Vector3(0, 0, 0);
        anim.SetBool("IsForward", false);
        anim.SetBool("IsJump", false);
        //yield return new WaitForSeconds(0.35f);
        clicked = false;
        Debug.Log("click = false");
        //this.transform.Rotate(tempRotation + TargetR);
        //this.transform.rotation = new Quaternion(tempRotation.x + TargetR.x, tempRotation.y + TargetR.y, tempRotation.z + TargetR.z, tempRotation.w);
        //this.transform.rotation = tempRotation;
    }
    IEnumerator Wait2()
    {

        Debug.Log("Before Waiting 1 seconds");
        yield return new WaitForSeconds(1f);
        Debug.Log("After Waiting 1 Seconds");
        animC.SetBool("IsCollected", false);
        animP.SetBool("IsProduced", false);
        //yield return new WaitForSeconds(0.35f);
        clicked = false;
        Debug.Log("click = false");
    }

    IEnumerator WaitH()
    {

        //Debug.Log("Before Waiting 1 seconds");
        yield return new WaitForSeconds(1f);
        //Debug.Log("After Waiting 1 Seconds");
        HomeA.Play();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        END.SetActive(true);
    }
    IEnumerator Turn(Vector3 TargetAxis)
    {
        
        bool turned = false;
        float turnedAngle = 0;
        while (!turned)
        {
            
            
            if (turnedAngle < 90f)
            {
                transform.Rotate(TargetAxis, 3.0f, Space.Self);
                turnedAngle += 3.0f;
            }
            else
            {
                turned = true;
                
            }
            
            yield return 0;
        }
        
        StopCoroutine("Turn");
        
        yield return 0;
    }
}


