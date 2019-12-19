using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRestrictions : MonoBehaviour
{

    [Header("Interactivity")]
	public bool allowDragging = true;
	public bool allowDelete = false;
	public bool allowWeld = true;
	public bool enableBuoyancy = true;
    public bool reverseGravity = false;

	public bool allowThruster = true;
	public bool allowWheel = true;

    [Header("Identity")]
    public bool prop = true;
    public bool thruster = false;
    public bool wheel = false;
    public bool balloon = false;
    public bool hinge = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
