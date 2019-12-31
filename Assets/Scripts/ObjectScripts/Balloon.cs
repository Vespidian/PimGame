using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
	private LineRenderer line;
	private GameObject linkedObject;
	private Vector3 linkOffset;

	private CharController thePlayer;
    // Start is called before the first frame update
    void Start()
    {
		thePlayer = GameObject.Find("Player").GetComponent<CharController>();
		line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        if(linkedObject != null){
    		line.SetPosition(1, linkedObject.transform.position + linkOffset);
    	}
    	if(linkedObject == null){
    		Destroy(gameObject);
    	}
    }

    public void SetFixPoint(Vector3 linkPos, GameObject linkObject){
    	line = GetComponent<LineRenderer>();

    	linkOffset = linkObject.transform.InverseTransformPoint(linkPos);

    	line.SetPosition(1, linkObject.transform.position + linkOffset);

    	GetComponent<ConfigurableJoint>().connectedBody = linkObject.GetComponent<Rigidbody>();

    	linkedObject = linkObject;
    }
}
