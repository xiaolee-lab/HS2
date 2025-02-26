using System;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C11 RID: 3089
	public class LimitGameClearedState : MonoBehaviour
	{
		// Token: 0x06005F9A RID: 24474 RVA: 0x00285363 File Offset: 0x00283763
		private void Start()
		{
			this.Refresh(true);
			Observable.EveryUpdate().TakeUntilDestroy(this).Subscribe(delegate(long _)
			{
				this.Refresh(false);
			});
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x0028538C File Offset: 0x0028378C
		private void Refresh(bool first = false)
		{
			bool flag = false;
			if (Singleton<Game>.IsInstance())
			{
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				if (worldData != null)
				{
					flag = worldData.Cleared;
				}
			}
			if (!flag)
			{
				if (base.gameObject.activeSelf)
				{
					base.gameObject.SetActive(false);
				}
			}
			else if (((flag && !this._prevCleared) || (flag && first)) && !base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this._prevCleared = flag;
		}

		// Token: 0x040054D8 RID: 21720
		[SerializeField]
		private bool _cleared = true;

		// Token: 0x040054D9 RID: 21721
		private bool _prevCleared;
	}
}
