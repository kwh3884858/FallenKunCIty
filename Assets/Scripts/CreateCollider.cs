using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    box,
    polygon,
    circle
}
public class CreateCollider : MonoBehaviour {


    public ColliderType colliderType = ColliderType.box;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
