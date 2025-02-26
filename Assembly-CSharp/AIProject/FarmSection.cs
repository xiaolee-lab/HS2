using System;
using System.Collections.Generic;
using AIProject.SaveData;
using AIProject.Scene;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C0A RID: 3082
	public class FarmSection : MonoBehaviour
	{
		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06005F1A RID: 24346 RVA: 0x00283D6D File Offset: 0x0028216D
		// (set) Token: 0x06005F1B RID: 24347 RVA: 0x00283D75 File Offset: 0x00282175
		public int HarvestID { get; set; }

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x00283D7E File Offset: 0x0028217E
		// (set) Token: 0x06005F1D RID: 24349 RVA: 0x00283D86 File Offset: 0x00282186
		public int SectionID { get; set; }

		// Token: 0x06005F1E RID: 24350 RVA: 0x00283D90 File Offset: 0x00282190
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Do(delegate(long _)
			{
				this.OnUpdate();
			}, delegate(Exception ex)
			{
			}).OnErrorRetry<long>().Subscribe(delegate(long _)
			{
			});
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x00283E10 File Offset: 0x00282210
		private void OnUpdate()
		{
			List<AIProject.SaveData.Environment.PlantInfo> source;
			if (!Singleton<Game>.Instance.Environment.FarmlandTable.TryGetValue(this.HarvestID, out source))
			{
				return;
			}
			AIProject.SaveData.Environment.PlantInfo element = source.GetElement(this.SectionID);
			if (element == null)
			{
				if (this._plantInfo != null)
				{
					this._plantInfo = null;
					if (this._plantItem != null)
					{
						UnityEngine.Object.Destroy(this._plantItem.gameObject);
						this._plantItem = null;
					}
				}
				return;
			}
			if (this._plantInfo != element)
			{
				this._plantInfo = element;
				StuffItemInfo seedInfo = Singleton<Manager.Resources>.Instance.GameInfo.FindItemInfo(element.nameHash);
				ItemInfo itemInfo = Singleton<Manager.Resources>.Instance.Map.PlantIvyFilterList.Find((ItemInfo x) => x.CategoryID == seedInfo.CategoryID && x.ItemID == seedInfo.ID);
				AssetBundleInfo assetBundleInfo;
				if (Singleton<Manager.Resources>.Instance.Map.PlantItemList.TryGetValue(itemInfo.ObjID, out assetBundleInfo))
				{
					GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetBundleInfo.assetbundle, assetBundleInfo.asset, false, assetBundleInfo.manifest);
					MapScene.AddAssetBundlePath(assetBundleInfo.assetbundle, assetBundleInfo.manifest);
					if (gameObject != null)
					{
						if (this._plantItem != null)
						{
							UnityEngine.Object.Destroy(this._plantItem.gameObject);
							this._plantItem = null;
						}
						this._plantItem = UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform, false).GetComponent<PlantItem>();
						this._plantItem.ChangeState(0);
					}
				}
			}
			if (this._plantItem != null)
			{
				float num = element.timer / (float)element.timeLimit;
				if (num < 0.5f)
				{
					this._plantItem.ChangeState(0);
				}
				else if (num < 1f)
				{
					this._plantItem.ChangeState(1);
				}
				else
				{
					this._plantItem.ChangeState(2);
				}
			}
		}

		// Token: 0x04005496 RID: 21654
		private AIProject.SaveData.Environment.PlantInfo _plantInfo;

		// Token: 0x04005497 RID: 21655
		private PlantItem _plantItem;
	}
}
