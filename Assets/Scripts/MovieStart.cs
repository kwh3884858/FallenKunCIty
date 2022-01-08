using Skylight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieStart : MonoBehaviour
{
    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //Debug.Log ("player");

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;

        // Skip the first 100 frames.
        videoPlayer.frame = 0;

        //videoPlayer.targetCamera = Skylight.CameraService.Instance ().m_mainCamera.GetComponent<Camera> ();

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        if (m_playOnStart)
        {
            Play();
		}
    }

    // Use this for initialization
    public void Play (VideoPlayer.EventHandler callback = null)
	{

        if (callback != null)
        {
            videoPlayer.loopPointReached += callback;
        }
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play ();

        if (m_backMask) m_backMask.SetActive(true);
    }

    // Update is called once per frame
    void Update ()
	{
        if (!videoPlayer || !videoPlayer.isPlaying)
        {
            return;
        }
        if (Input.GetMouseButton (0) || Input.GetButtonDown ("Jump")) {
            videoPlayer.frame = (long)videoPlayer.frameCount -1;
		}
	}

	void EndReached (UnityEngine.Video.VideoPlayer vp)
	{
        vp.loopPointReached -= EndReached;
        if(m_backMask) m_backMask.SetActive (false);
		SoundService.Instance().PlayMusic(m_bgmName, true);

		gameObject.SetActive (false);
	}

	public string m_bgmName;
	public bool m_playOnStart;
    public GameObject m_backMask;
    VideoPlayer videoPlayer;
}
