using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    private Text text;
    
    // Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = "";
        StartCoroutine(ShowMessage(2, "Ouuuuh... Il fait bien noir ici..."));
        StartCoroutine(ShowMessage(2, "Mais d'où vient cette lumière?"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ShowMessage(int delay, string sentence)
    {
        foreach (char i in sentence)
        {
            text.text += i;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(delay);
        text.text = "";
    }
    
}
