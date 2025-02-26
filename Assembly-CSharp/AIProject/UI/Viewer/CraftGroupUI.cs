using System;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000E5F RID: 3679
	public class CraftGroupUI : MonoBehaviour
	{
		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06007476 RID: 29814 RVA: 0x00317FB9 File Offset: 0x003163B9
		public Image cursor
		{
			[CompilerGenerated]
			get
			{
				return this._cursor;
			}
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06007477 RID: 29815 RVA: 0x00317FC1 File Offset: 0x003163C1
		public Toggle toggle
		{
			[CompilerGenerated]
			get
			{
				return this._toggle;
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x00317FCC File Offset: 0x003163CC
		public CraftViewer viewer
		{
			get
			{
				if (this._viewer == null)
				{
					string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(CommonLib.LoadAsset<GameObject>(bundle, "CraftViewer", false, string.Empty), this._parent, false);
					this._viewer = gameObject.GetComponent<CraftViewer>();
					if (this._image != null)
					{
						this._viewer.SetIcon(this._image.sprite);
					}
					this._viewer.SetIcon(this._iconText);
					this._viewer.SetID(this._id);
					this._viewer.itemListUI.SetCraftUI(this._craftUI);
					if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
					{
						MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
					}
				}
				return this._viewer;
			}
		}

		// Token: 0x06007479 RID: 29817 RVA: 0x003180D3 File Offset: 0x003164D3
		public void SetActive(bool isOn)
		{
			this.viewer.Visible = isOn;
		}

		// Token: 0x04005F21 RID: 24353
		[SerializeField]
		private CraftUI _craftUI;

		// Token: 0x04005F22 RID: 24354
		[SerializeField]
		private Image _cursor;

		// Token: 0x04005F23 RID: 24355
		[SerializeField]
		private Toggle _toggle;

		// Token: 0x04005F24 RID: 24356
		[SerializeField]
		private int _id = -1;

		// Token: 0x04005F25 RID: 24357
		[SerializeField]
		private Image _image;

		// Token: 0x04005F26 RID: 24358
		[SerializeField]
		private string _iconText;

		// Token: 0x04005F27 RID: 24359
		[SerializeField]
		private RectTransform _parent;

		// Token: 0x04005F28 RID: 24360
		private CraftViewer _viewer;
	}
}
