using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public bool Die;
	// Use this for initialization
	void Start () {
        Die = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Die)
        {
            Destroy(gameObject);
        }
	}
}
