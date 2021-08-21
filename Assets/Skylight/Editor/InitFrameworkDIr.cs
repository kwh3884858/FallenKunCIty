using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

namespace Skylight
{
	public class InitFrameworkDir : Editor
	{

		/// <summary>
		/// 初始化框架文件夹
		/// </summary>
		[MenuItem ("Assets/Framework/InitFrameworkDirectory")]
		static void InitFrameworkDirectory ()
		{

			string [] frameworkDir = {
				"UI/Panel",
				"UI/Dialog",
				"UI/Overlay",
				"UI/Box",
				"Prefabs",
				"Scenes",
				"Models",
				"Images",
				"Sounds/BGM",
				"Sounds/Effects",
				"Resources",
				"Scripts",
				"Scripts/UI",
				"Scripts/Logic"
			};


			for (int i = 0; i < frameworkDir.Length; i++) {

				string path = Application.dataPath + "/" + frameworkDir [i];

				if (!Directory.Exists (path)) {
					Directory.CreateDirectory (path);
				}
			}
			AssetDatabase.Refresh ();

		}


		[MenuItem ("Assets/Framework/TestButton")]
		static void TestButton ()
		{

			Debug.Log (CreateAssetBundles.BuildTargetPlatform);
		}
	}
}
