using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
    private int reality = 0;
    public int best_step;
    public GameObject END;
    static public bool run=true;
	// Use this for initialization
	public void Start () {
        reality = 0;
        run=true;
    }
    
    // Update is called once per frame
    void Update () {

		if (reality <= best_step)
			;

        if (reality > best_step && reality <= best_step + 2)
        {
            END.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (reality >= best_step + 3)
        {
           
            END.transform.GetChild(1).gameObject.SetActive(false);
            END.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    public void Step()
    {
        if (Role_MoveForward.clicked)
        return;
        if (Cha_Ctr.CantClick)
        return;
        if (Cha_Boxes.CantClick)
        return;
        if (Role_MoveForward.CantClick)
        return;
        if (run)
        {
            reality++;
            run = false;
            StartCoroutine(Wait());
        }
        Role_MoveForward.clicked = true;
    }
    public void step1()
    {
		if (Role_MoveForward.clicked)
			return;
		if (Cha_Ctr.CantClick)
			return;
        if (Cha_Boxes.CantClick)
            return;
		if (Role_MoveForward.CantClick)
			return;
        if (run)
        {
            reality++;
            run = false;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.75f);
        run = true;
    }
}
