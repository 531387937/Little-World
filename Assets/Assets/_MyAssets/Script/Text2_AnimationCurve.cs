using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Text2_AnimationCurve : MonoBehaviour {
	public AnimationCurve anim;
	public Role_MoveForward bee;
	public GameObject pos;
	private float popUp;
	private CanvasGroup cg;
	private float alpha = 0.0f;
	private float alphaspeed = 2.0f;
	private Vector3 roalPos;

	// Use this for initialization
	void Start () {
		cg = this.GetComponentInParent<CanvasGroup> ();
		popUp = 0;
	}

	// Update is called once per frame
	void Update () {
		//		popUp += 0.05f;
		//		this.transform.position = new Vector3 (-1.3f, anim.Evaluate (popUp) + 8.5f, 0.0f);
		roalPos = pos.transform.position;
		if (alpha != cg.alpha){
			cg.alpha = Mathf.Lerp (cg.alpha, alpha, alphaspeed * Time.deltaTime);
			if (Mathf.Abs (alpha - cg.alpha) <= 0.05f) {
				cg.alpha = alpha;
			}
		}
		if (bee.Collected == true) {
			popUp += 0.1f;
//			this.transform.position = new Vector3 (-0.3f, anim.Evaluate (popUp) + 10, 0.0f);
			this.transform.position = new Vector3 (roalPos.x, anim.Evaluate (popUp) + roalPos.y, 0.0f);
		}
		if (popUp >= 8.0f) {
			Hide ();
		}
		if (popUp >= 25.0f) {
			popUp = 0.0f;
			this.transform.position = new Vector3 (-6.0f, -4.5f, 0.0f);
//			bee.Collected = false;
		}
	}

	public void Show()
	{
		alpha = 1;
		this.transform.position = new Vector3 (roalPos.x,roalPos.y, 0.0f);
	}

	public void Hide(){
		alpha = 0;
	}
}
