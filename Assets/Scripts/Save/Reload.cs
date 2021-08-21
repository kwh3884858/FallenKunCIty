using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour {
    PlayerDataLS playerLS;

	Collider2D [] _colliders;

	public LayerMask playerMask;

	int sceneId;

	// Use this for initialization
	void Start () {
        playerLS = GameObject.Find("Player").GetComponent<PlayerDataLS>();
		_colliders = GetComponents<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		sceneId = playerLS.data.scenceID;

		foreach(Collider2D _collider in _colliders){
			int VerticalRayCount = 40;

			RaycastHit2D [] hits = new RaycastHit2D [VerticalRayCount];
			float rayLength = 1;
			float verticalRaySpacing = _collider.bounds.size.x / (VerticalRayCount - 1);

			Vector2 rayOrigin ;
			for (int i = 0; i < VerticalRayCount; i ++){
				rayOrigin =(Vector2)_collider.bounds.max + Vector2.left * verticalRaySpacing * i;
				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up, rayLength, playerMask);
				hits [i] = hit;
				Debug.DrawRay (rayOrigin,Vector2.up, Color.red);
			}

			for (int i = 0; i < VerticalRayCount; i++){
				if(!hits[i]){
					continue;

				}
				if(hits[i].transform.name == "Player" ){
					Debug.Log ("Dead");
					Debug.Log (sceneId);
					Debug.Log (hits [i].transform.GetComponent<PlayerStateManager> ().GetCurrentStateID () );
					Globe.isLoad = true;
					Globe.nextSence = sceneId + 1;
					Globe.preSence = sceneId - 1;
					SceneManager.LoadScene (sceneId);
                    break;
				}
			}
		}
	}



}
