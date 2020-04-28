using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{	
	private CanvasGroup uiElement;
	public bool faded = false;
	public bool autoFade = false;
	public float duration = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        uiElement = GetComponent<CanvasGroup>();
        if(autoFade){
        	Fade();
        }
    }
    
    public void Fade(){
    	if(faded == true){
    		StartCoroutine(DoFade(uiElement, 1, 0));
    	}else if(faded == false){
    		StartCoroutine(DoFade(uiElement, 0, 1));
    	}
    	faded = !faded;
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end){
    	float counter = 0f;

    	while(counter < duration){
    		counter += Time.deltaTime;
    		canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);

    		yield return null;
    	}
    }
}
