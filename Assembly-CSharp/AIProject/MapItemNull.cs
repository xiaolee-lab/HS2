using System;
using System.Collections.Generic;
using AIProject.Scene;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000BE2 RID: 3042
	public class MapItemNull : ActionPointComponentBase
	{
		// Token: 0x06005D06 RID: 23814 RVA: 0x00275E7B File Offset: 0x0027427B
		protected override void OnStart()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x00275EB0 File Offset: 0x002742B0
		public void SetActiveObjs(bool active, List<int> items = null)
		{
			if (active)
			{
				if (!this._itemObjs.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject obj in this._itemObjs)
					{
						UnityEngine.Object.Destroy(obj);
					}
					this._itemObjs.Clear();
				}
				foreach (int key in items)
				{
					ActionItemInfo info;
					if (Singleton<Manager.Resources>.Instance.Map.EventItemList.TryGetValue(key, out info))
					{
						GameObject gameObject = CommonLib.LoadAsset<GameObject>(info.assetbundleInfo.assetbundle, info.assetbundleInfo.asset, false, info.assetbundleInfo.manifest);
						if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == info.assetbundleInfo.assetbundle && x.Item2 == info.assetbundleInfo.manifest))
						{
							MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(info.assetbundleInfo.assetbundle, info.assetbundleInfo.asset));
						}
						if (gameObject != null)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, (this._pivot != null) ? this._pivot.transform : null, false);
							gameObject2.transform.localPosition = Vector3.zero;
							gameObject2.transform.localRotation = Quaternion.identity;
							gameObject2.transform.localScale = Vector3.one;
							this._itemObjs.Add(gameObject2);
						}
						else
						{
							AssetBundleInfo assetbundleInfo = info.assetbundleInfo;
						}
					}
				}
			}
			else
			{
				if (this._itemObjs.IsNullOrEmpty<GameObject>())
				{
					return;
				}
				foreach (GameObject obj2 in this._itemObjs)
				{
					UnityEngine.Object.Destroy(obj2);
				}
				this._itemObjs.Clear();
			}
			this._active = (!this._itemObjs.IsNullOrEmpty<GameObject>() && active);
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x0027614C File Offset: 0x0027454C
		private void OnUpdate()
		{
			if (this._active)
			{
				this._elapsedTime += Time.unscaledDeltaTime;
				if (this._elapsedTime > Singleton<Map>.Instance.EnvironmentProfile.RuntimeMapItemLifeTime)
				{
					this.SetActiveObjs(false, null);
				}
			}
			else
			{
				this._elapsedTime = 0f;
			}
		}

		// Token: 0x0400537A RID: 21370
		[SerializeField]
		private Transform _pivot;

		// Token: 0x0400537B RID: 21371
		private float _elapsedTime;

		// Token: 0x0400537C RID: 21372
		private bool _active;

		// Token: 0x0400537D RID: 21373
		private List<GameObject> _itemObjs = new List<GameObject>();
	}
}
