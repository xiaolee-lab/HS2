using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200129E RID: 4766
	public class StartScene : MonoBehaviour
	{
		// Token: 0x06009D98 RID: 40344 RVA: 0x0040580C File Offset: 0x00403C0C
		private IEnumerator LoadCoroutine()
		{
			yield return Singleton<Info>.Instance.LoadExcelDataCoroutine();
			yield return null;
			Singleton<Scene>.Instance.SetFadeColor(Color.black);
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "Studio",
				isFade = true
			}, false);
			yield break;
		}

		// Token: 0x06009D99 RID: 40345 RVA: 0x00405820 File Offset: 0x00403C20
		private void Start()
		{
			base.StartCoroutine(this.LoadCoroutine());
		}
	}
}
