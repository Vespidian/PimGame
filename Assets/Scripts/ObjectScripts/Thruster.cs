using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{

	Rigidbody parentRb;
    public ParticleSystem smokeParticles;
    // Start is called before the first frame update
    void Start()
    {
        parentRb = this.transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
        	parentRb.AddForceAtPosition(transform.TransformDirection (-Vector3.forward)*5, transform.position, ForceMode.Impulse);
        }else if(Input.GetKey(KeyCode.DownArrow)){
        	parentRb.AddForceAtPosition(transform.TransformDirection (Vector3.forward)*5, transform.position, ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
            ShowParticles(true);
        }else if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)){
            ShowParticles(false);
        }

    }

    void ShowParticles(bool mode){
        if(mode == true){
            smokeParticles.Play();
        }else if(mode == false){
            smokeParticles.Stop();
        }
    }
}
