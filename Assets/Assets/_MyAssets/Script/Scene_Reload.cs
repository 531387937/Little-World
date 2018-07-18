using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Reload : MonoBehaviour {
    [SerializeField]
    private GameObject LayerMask;
    [SerializeField]
    private float i;

    void Start()
    {
        i = 2;
        LayerMask = Camera.main.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (i < 1)
        {
            i += 0.01f;
            LayerMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
        }
    }

    public void Reload(){
        //		Application.LoadLevel(Application.loadedLevelName);
        StartCoroutine("FadeInAndOut");
	}

    IEnumerator FadeInAndOut()
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }
}
