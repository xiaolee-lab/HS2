using System;
using UnityEngine;

namespace PlayfulSystems.LoadingScreen
{
	// Token: 0x02000633 RID: 1587
	[Serializable]
	public class SceneInfo
	{
		// Token: 0x04002574 RID: 9588
		public string sceneName;

		// Token: 0x04002575 RID: 9589
		[Tooltip("Images are loaded from Resources/ScenePreviews/. Leave empty to keep default background in Loading Scene.")]
		public string imageName;

		// Token: 0x04002576 RID: 9590
		public string header;

		// Token: 0x04002577 RID: 9591
		[Multiline(4)]
		public string description;
	}
}
