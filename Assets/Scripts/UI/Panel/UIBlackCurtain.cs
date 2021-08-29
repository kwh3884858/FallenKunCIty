using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class UIBlackCurtain : Skylight.UIPanel
{
	private Image background;
	private Color BlackColor = new Color (0, 0, 0, 255);
	private Color AlphaColor = new Color (0, 0, 0, 0);

	//private float time = 1.0f;
	//TweenCallback callback = null;
	// Use this for initialization

	// 
	//public void AddCallback (UnityEngine.Events.UnityAction action)
	//{
	//	callback = new TweenCallback (action);
	//}

	//public void RemoveCallBack ()
	//{
	//	callback = null;
	//}
	public override void PanelInit ()
	{
		base.PanelInit ();
		background = transform.Find ("Image").GetComponent<Image> ();
	}
	public void CallBack ()
	{
		Debug.Log ("hihihihihihi");
	}

	public void PlayFadeIn ()
	{
		StartCoroutine (FadeImage (true));
		//background = transform.Find ("Image").gameObject/*.GetComponent<Image>() */;
		//background.GetComponent<Image> ().color = BlackColor;
		//transform.GetComponent<Canvas> ().sortingOrder = 6;
		//if (callback == null) {
		//	iTween.FadeTo (background, iTween.Hash ("time", time, "alpha", "0", "oncompletetarget", gameObject));
		//background.DOFade (0f, time);
		//} else {
		//iTween.FadeTo (background, iTween.Hash ("time", time, "alpha", "0", "oncomplete", "CallBack", "oncompletetarget", gameObject));

		//background.DOFade (0f, time).OnComplete (callback);

		//}
	}

	IEnumerator FadeImage (bool fadeAway)
	{
		// fade from opaque to transparent
		if (fadeAway) {
			// loop over 1 second backwards
			for (float i = 1; i >= 0; i -= Time.deltaTime / 2) {
				// set color with i as alpha
				background.color = new Color (0, 0, 0, i);
				yield return null;
			}
		}
		// fade from transparent to opaque
		else {
			// loop over 1 second
			for (float i = 0; i <= 1; i += Time.deltaTime / 2) {
				// set color with i as alpha
				background.color = new Color (0, 0, 0, i);
				yield return null;
			}
		}
		Skylight.UIManager.Instance ().ClosePanel<UIBlackCurtain> ();
	}

	public void PlayFadeOut ()
	{
		StartCoroutine (FadeImage (false));

		//background = transform.Find ("Image").gameObject/*.GetComponent<Image>() */;
		//background.GetComponent<Image> ().color = AlphaColor;
		//transform.GetComponent<Canvas> ().sortingOrder = 6;
		//if (callback == null) {
		//iTween.FadeTo (background, iTween.Hash ("time", time, "alpha", "1", "oncompletetarget", gameObject));
		//background.DOFade (1f, time);
		//} else {
		//iTween.FadeTo (background, iTween.Hash ("time", time, "alpha", "1", "oncomplete", "CallBack", "oncompletetarget", gameObject));

		//background.DOFade (1f, time).OnComplete (callback);

		//}
	}
}
