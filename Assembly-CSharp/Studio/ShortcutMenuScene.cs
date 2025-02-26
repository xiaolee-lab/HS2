using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200129D RID: 4765
	public class ShortcutMenuScene : MonoBehaviour
	{
		// Token: 0x06009D94 RID: 40340 RVA: 0x004057B6 File Offset: 0x00403BB6
		private void Awake()
		{
			this.timeScale = Time.timeScale;
			Time.timeScale = 0f;
		}

		// Token: 0x06009D95 RID: 40341 RVA: 0x004057CD File Offset: 0x00403BCD
		private void Update()
		{
			if (UnityEngine.Input.GetMouseButtonDown(1) || UnityEngine.Input.GetKeyDown(KeyCode.F2))
			{
				Singleton<Scene>.Instance.UnLoad();
			}
		}

		// Token: 0x06009D96 RID: 40342 RVA: 0x004057F4 File Offset: 0x00403BF4
		private void OnDestroy()
		{
			Time.timeScale = this.timeScale;
		}

		// Token: 0x04007D5D RID: 32093
		private float timeScale = 1f;
	}
}
