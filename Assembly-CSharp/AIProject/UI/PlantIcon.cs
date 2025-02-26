using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E77 RID: 3703
	public class PlantIcon : MonoBehaviour
	{
		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x060075F6 RID: 30198 RVA: 0x00320882 File Offset: 0x0031EC82
		// (set) Token: 0x060075F7 RID: 30199 RVA: 0x0032088A File Offset: 0x0031EC8A
		public bool initialized { get; private set; }

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x060075F8 RID: 30200 RVA: 0x00320893 File Offset: 0x0031EC93
		// (set) Token: 0x060075F9 RID: 30201 RVA: 0x003208A0 File Offset: 0x0031ECA0
		public bool visible
		{
			get
			{
				return this.visibleObject.activeSelf;
			}
			set
			{
				this.visibleObject.SetActive(value);
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x060075FA RID: 30202 RVA: 0x003208AE File Offset: 0x0031ECAE
		public Toggle toggle
		{
			[CompilerGenerated]
			get
			{
				return this._toggle;
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x060075FB RID: 30203 RVA: 0x003208B6 File Offset: 0x0031ECB6
		public string itemName
		{
			[CompilerGenerated]
			get
			{
				StuffItemInfo value = this._itemInfo.Value;
				return ((value != null) ? value.Name : null) ?? string.Empty;
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x060075FC RID: 30204 RVA: 0x003208DE File Offset: 0x0031ECDE
		public Sprite itemIcon
		{
			[CompilerGenerated]
			get
			{
				return this._icon.sprite;
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x060075FD RID: 30205 RVA: 0x003208EB File Offset: 0x0031ECEB
		public StuffItemInfo itemInfo
		{
			[CompilerGenerated]
			get
			{
				return this._itemInfo.Value;
			}
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x060075FE RID: 30206 RVA: 0x003208F8 File Offset: 0x0031ECF8
		private ReactiveProperty<StuffItemInfo> _itemInfo { get; } = new ReactiveProperty<StuffItemInfo>();

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x060075FF RID: 30207 RVA: 0x00320900 File Offset: 0x0031ED00
		// (set) Token: 0x06007600 RID: 30208 RVA: 0x0032090D File Offset: 0x0031ED0D
		public AIProject.SaveData.Environment.PlantInfo info
		{
			get
			{
				return this._info.Value;
			}
			set
			{
				this._info.Value = value;
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06007601 RID: 30209 RVA: 0x0032091B File Offset: 0x0031ED1B
		private ReactiveProperty<AIProject.SaveData.Environment.PlantInfo> _info { get; } = new ReactiveProperty<AIProject.SaveData.Environment.PlantInfo>();

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06007602 RID: 30210 RVA: 0x00320923 File Offset: 0x0031ED23
		private GameObject visibleObject
		{
			get
			{
				return this.GetCacheObject(ref this._visibleObject, () => base.transform.GetChild(0).gameObject);
			}
		}

		// Token: 0x06007603 RID: 30211 RVA: 0x00320940 File Offset: 0x0031ED40
		private IEnumerator Start()
		{
			this._icon.enabled = false;
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			this._info.Subscribe(delegate(AIProject.SaveData.Environment.PlantInfo x)
			{
				this._itemInfo.Value = ((x != null) ? Singleton<Manager.Resources>.Instance.GameInfo.FindItemInfo(x.nameHash) : null);
			});
			this._itemInfo.Subscribe(delegate(StuffItemInfo x)
			{
				if (x != null)
				{
					Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, x.IconID, this._icon, true);
					this._icon.enabled = true;
				}
				else
				{
					this._icon.enabled = false;
					this._icon.sprite = null;
				}
			});
			this.initialized = true;
			yield break;
		}

		// Token: 0x04006016 RID: 24598
		[SerializeField]
		private Toggle _toggle;

		// Token: 0x04006017 RID: 24599
		[SerializeField]
		private Image _icon;

		// Token: 0x0400601A RID: 24602
		private GameObject _visibleObject;
	}
}
