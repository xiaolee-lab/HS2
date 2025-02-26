using System;
using UnityEngine;

namespace PlayfulSystems.LoadingScreen
{
	// Token: 0x02000638 RID: 1592
	public class LoadingScreenTrigger : MonoBehaviour
	{
		// Token: 0x060025DF RID: 9695 RVA: 0x000D7E24 File Offset: 0x000D6224
		private void Start()
		{
			if (this.loadOnStart)
			{
				this.TriggerLoadScene();
			}
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000D7E37 File Offset: 0x000D6237
		public void TriggerLoadScene()
		{
			if (this.loadSceneFrom == LoadingScreenTrigger.SceneReference.Number)
			{
				LoadingScreenProBase.LoadScene(this.sceneNumber);
			}
			else
			{
				LoadingScreenProBase.LoadScene(this.sceneName);
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000D7E5F File Offset: 0x000D625F
		public void LoadScene(int number)
		{
			LoadingScreenProBase.LoadScene(number);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000D7E67 File Offset: 0x000D6267
		public void LoadScene(string name)
		{
			LoadingScreenProBase.LoadScene(name);
		}

		// Token: 0x04002599 RID: 9625
		public bool loadOnStart = true;

		// Token: 0x0400259A RID: 9626
		public LoadingScreenTrigger.SceneReference loadSceneFrom;

		// Token: 0x0400259B RID: 9627
		public int sceneNumber;

		// Token: 0x0400259C RID: 9628
		public string sceneName;

		// Token: 0x02000639 RID: 1593
		public enum SceneReference
		{
			// Token: 0x0400259E RID: 9630
			Number,
			// Token: 0x0400259F RID: 9631
			Name
		}
	}
}
