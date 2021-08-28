
using UnityEngine;
using StarPlatinum.Base;

namespace Config.GameRoot
{
	/// <summary>
	/// Only for editor
	/// </summary>
	[CreateAssetMenu (fileName = "ConfigRoot", menuName = "Config/SpawnConfigRoot", order = 1)]
	public class ConfigRoot : ConfigSingleton<ConfigRoot>
	{
		[Header ("Start Scene")]
		public SceneLookupEnum StartScene;
	}
}