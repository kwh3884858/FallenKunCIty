using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Skylight
{
	public class CreateAssetBundles
	{
		public static BuildTarget BuildTargetPlatform {
			get {
				string dataPath = Application.dataPath;
				if (Application.platform == RuntimePlatform.IPhonePlayer) {
					return BuildTarget.iOS;
				}

				if (Application.platform == RuntimePlatform.Android) {
					return BuildTarget.Android;
				}

				if (Application.platform == RuntimePlatform.OSXEditor) {
					return BuildTarget.StandaloneOSX;
				}
				if (Application.platform == RuntimePlatform.WindowsEditor) {
					return BuildTarget.StandaloneWindows64;
				}
				return BuildTarget.StandaloneWindows;
			}
		}

		[MenuItem ("Assets/AssetBundles/BuildAssetBundles %#&B")]
		public static void BuildAllAssetBundles ()
		{
			string assetBundleDirectory = "Assets/StreamingAssets/AssetBundle/";

			//	BuildPipeline.BuildAssetBundles (assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

			if (!Directory.Exists (assetBundleDirectory)) {
				Directory.CreateDirectory (assetBundleDirectory);
			}


			//第一个参数获取的是AssetBundle存放的相对地址。
			BuildPipeline.BuildAssetBundles (
				assetBundleDirectory,
				BuildAssetBundleOptions.ChunkBasedCompression |
				BuildAssetBundleOptions.DeterministicAssetBundle,
				BuildTargetPlatform);

			AssetDatabase.Refresh ();

		}
	}
}
