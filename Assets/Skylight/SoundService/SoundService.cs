using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Skylight
{
	public class SoundService : GameModule<SoundService>
	{
		GameObject music;
		GameObject effect;
		public AudioSource backsoundSource = null;
		private Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip> ();
		private Dictionary<string, AudioSource> effectsounCache = new Dictionary<string, AudioSource> ();

		public override void SingletonInit ()
		{
			music = new GameObject ("Music");
			music.transform.SetParent (transform);
			backsoundSource = music.AddComponent<AudioSource> ();

			effect = new GameObject ("Effect");
			effect.transform.SetParent (transform);
		}

		public void AdjustMusicVolume (float volume)
		{
			if (backsoundSource.clip != null && name.IndexOf (this.backsoundSource.clip.name) > -1) {
				return;
			}

			backsoundSource.volume = volume;
		}

		/// <summary>
		/// Play the music.
		/// </summary>
		/// <param name="name">music name.</param>
		/// <param name="isloop">If set to <c>true</c> is loop.</param>
		public void PlayMusic (string name, bool isloop = false)
		{
			if (backsoundSource.clip != null && name.IndexOf (this.backsoundSource.clip.name) > -1) {
				return;
			}
			backsoundSource.loop = isloop;
			backsoundSource.clip = LoadAudioClip (name);
			if (backsoundSource.clip != null) {
				backsoundSource.Play ();
			}
		}

		public void StopMusic ()
		{
			backsoundSource.Stop ();
			backsoundSource.clip = null;
		}

		public AudioClip LoadAudioClip (string name)
		{
			AudioClip ac = null;
			clipCache.TryGetValue (name, out ac);
			if (ac == null) {
				ac = AssetsManager.Load<AudioClip> ("Sound/" + name);

				//if (Application.platform == RuntimePlatform.OSXPlayer) {
				//	ac = AssetsManager.Load<AudioClip> ("Sound/" + name);
				//}

				if (ac != null) {
					clipCache.Add (name, ac);
				} else {
					Debug.Log ("Can`t load sound " + name);
				}
			}

			return ac;
		}

		/// <summary>
		/// 播放音效
		/// </summary>
		public void PlayEffect (string name, bool isLoop = false, float volume = 0.5f, bool isCover = false)
		{
			string _name = name;
			if (isCover) {
				//允许重复播放
				while (true) {
					name += UnityEngine.Random.Range (0, 9999).ToString ();
					if (effectsounCache.ContainsKey (name)) {
						continue;
					} else {
						break;
					}
				}
			} else {
				if (effectsounCache.ContainsKey (name)) {
					//杜绝重复播放
					return;
				}
			}


			GameObject gameObject = new GameObject (name);
			gameObject.transform.SetParent (effect.transform);

			AudioSource audioSource = (AudioSource)gameObject.AddComponent<AudioSource> ();
			var clip = LoadAudioClip (_name);
			audioSource.clip = clip;
			//audioSource.spatialBlend = 1f;
			//audioSource.volume = 1;
			audioSource.Play ();
			audioSource.volume = volume;
			audioSource.loop = isLoop;
			effectsounCache.Add (name, audioSource);
			var clearTime = clip.length * ((Time.timeScale >= 0.01f) ? Time.timeScale : 0.01f);

			StartCoroutine (DelayToInvokeDo (() => {
				GameObject.Destroy (gameObject);
				effectsounCache.Remove (name);
			}, clearTime));
		}

		//public void PlayEffectCover (string name)
		//{


		//	GameObject gameObject = new GameObject (gameName);
		//	gameObject.transform.SetParent (effect.transform);

		//	AudioSource audioSource = (AudioSource)gameObject.AddComponent<AudioSource> ();
		//	var clip = LoadAudioClip (name);
		//	audioSource.clip = clip;
		//	//audioSource.spatialBlend = 1f;
		//	//audioSource.volume = 1;
		//	audioSource.Play ();
		//	audioSource.volume = 0.5f;
		//	effectsounCache.Add (gameName, audioSource);
		//	var clearTime = clip.length * ((Time.timeScale >= 0.01f) ? Time.timeScale : 0.01f);

		//	StartCoroutine (DelayToInvokeDo (() => {
		//		GameObject.Destroy (gameObject);
		//		effectsounCache.Remove (gameName);
		//	}, clearTime));
		//}
		/// <summary>
		/// 播放音效
		/// </summary>
		/// <param name="name">片段名.</param>
		/// <param name="isLoop">是否重复.</param>
		//public void PlayEffect (string name, bool isLoop)
		//{
		//	if (effectsounCache.ContainsKey (name)) {
		//		//杜绝重复播放
		//		return;
		//	}

		//	GameObject gameObject = new GameObject (name);
		//	gameObject.transform.SetParent (effect.transform);

		//	AudioSource audioSource = (AudioSource)gameObject.AddComponent<AudioSource> ();
		//	var clip = LoadAudioClip (name);
		//	audioSource.clip = clip;
		//	audioSource.loop = isLoop;
		//	//audioSource.spatialBlend = 1f;
		//	//audioSource.volume = 1;
		//	audioSource.Play ();
		//	effectsounCache.Add (name, audioSource);

		//	//var clearTime = clip.length * ((Time.timeScale >= 0.01f) ? Time.timeScale : 0.01f);

		//}

		public void StopEffect (string name)
		{
			if (effectsounCache.ContainsKey (name)) {
				//杜绝重复播放
				GameObject.Destroy (effectsounCache [name].gameObject);
				effectsounCache.Remove (name);

			} else {
				return;
			}


		}

		IEnumerator DelayToInvokeDo (Action action, float delaySeconds)
		{
			yield return new WaitForSeconds (delaySeconds);
			action ();
		}

		public void Play (string name)
		{
			AudioSource.PlayClipAtPoint (LoadAudioClip (name), new Vector3 (), 1);
		}
	}
}