using System;
using Manager;
using Studio.SceneAssist;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001291 RID: 4753
	public class ExitScene : MonoBehaviour
	{
		// Token: 0x06009D3F RID: 40255 RVA: 0x0040404E File Offset: 0x0040244E
		private void Awake()
		{
			this.timeScale = Time.timeScale;
			Time.timeScale = 0f;
		}

		// Token: 0x06009D40 RID: 40256 RVA: 0x00404068 File Offset: 0x00402468
		private void Start()
		{
			this.yes.addOnClick = delegate()
			{
				Singleton<Scene>.Instance.GameEnd(false);
				Singleton<Scene>.Instance.isSkipGameExit = false;
			};
			this.yes.addOnClick = delegate()
			{
				Assist.PlayDecisionSE();
			};
			this.no.addOnClick = delegate()
			{
				Singleton<Scene>.Instance.UnLoad();
				Singleton<Scene>.Instance.isGameEndCheck = true;
				Singleton<Scene>.Instance.isSkipGameExit = false;
			};
			this.no.addOnClick = delegate()
			{
				Singleton<Sound>.Instance.Play(Sound.Type.SystemSE, Assist.AssetBundleSystemSE, "sse_00_03", 0f, 0f, true, true, -1, true);
			};
		}

		// Token: 0x06009D41 RID: 40257 RVA: 0x00404115 File Offset: 0x00402515
		private void OnDestroy()
		{
			Time.timeScale = this.timeScale;
		}

		// Token: 0x04007D1E RID: 32030
		[SerializeField]
		private VoiceNode yes;

		// Token: 0x04007D1F RID: 32031
		[SerializeField]
		private VoiceNode no;

		// Token: 0x04007D20 RID: 32032
		private float timeScale = 1f;
	}
}
