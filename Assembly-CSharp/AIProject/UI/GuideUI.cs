using System;
using System.Collections.Generic;
using AIProject.Scene;
using Manager;
using UniRx.Toolkit;
using UnityEngine;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000F98 RID: 3992
	public class GuideUI : MonoBehaviour
	{
		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x06008533 RID: 34099 RVA: 0x00374AC3 File Offset: 0x00372EC3
		// (set) Token: 0x06008534 RID: 34100 RVA: 0x00374ACC File Offset: 0x00372ECC
		public UnityEx.ValueTuple<string, ActionID>[] Elements
		{
			get
			{
				return this._elements;
			}
			set
			{
				this._elements = value;
				foreach (GuideOption instance in this._options)
				{
					this._pool.Return(instance);
				}
				this._options.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					UnityEx.ValueTuple<string, ActionID> valueTuple = value[i];
					GuideOption guideOption = this._pool.Rent();
					guideOption.transform.SetParent(this._parent, false);
					guideOption.Icon = null;
					guideOption.CaptionText = "決定";
				}
			}
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x00374B9C File Offset: 0x00372F9C
		private void Start()
		{
			string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			string manifest = "abdata";
			this._source = CommonLib.LoadAsset<GameObject>(bundle, "GuideOption", false, manifest);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle && x.Item2 == manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, manifest));
			}
			this._pool.Source = this._source;
		}

		// Token: 0x06008536 RID: 34102 RVA: 0x00374C38 File Offset: 0x00373038
		private void OnDestroy()
		{
			this._pool.Clear(false);
		}

		// Token: 0x04006BC0 RID: 27584
		[SerializeField]
		private RectTransform _parent;

		// Token: 0x04006BC1 RID: 27585
		[SerializeField]
		private GameObject _source;

		// Token: 0x04006BC2 RID: 27586
		private List<GuideOption> _options = new List<GuideOption>();

		// Token: 0x04006BC3 RID: 27587
		private GuideUI.GuideOptionPool _pool = new GuideUI.GuideOptionPool();

		// Token: 0x04006BC4 RID: 27588
		private UnityEx.ValueTuple<string, ActionID>[] _elements;

		// Token: 0x02000F99 RID: 3993
		public class GuideOptionPool : ObjectPool<GuideOption>
		{
			// Token: 0x17001D28 RID: 7464
			// (get) Token: 0x06008538 RID: 34104 RVA: 0x00374C4E File Offset: 0x0037304E
			// (set) Token: 0x06008539 RID: 34105 RVA: 0x00374C56 File Offset: 0x00373056
			public GameObject Source { get; set; }

			// Token: 0x0600853A RID: 34106 RVA: 0x00374C5F File Offset: 0x0037305F
			protected override GuideOption CreateInstance()
			{
				return UnityEngine.Object.Instantiate<GameObject>(this.Source).GetComponent<GuideOption>();
			}
		}
	}
}
