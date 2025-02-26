using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C24 RID: 3108
	public class OnceSearchActionPoint : SearchActionPoint
	{
		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x002875BF File Offset: 0x002859BF
		public int MapItemID
		{
			[CompilerGenerated]
			get
			{
				return this._mapItemID;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06005FF6 RID: 24566 RVA: 0x002875C7 File Offset: 0x002859C7
		public bool HaveMapItems
		{
			[CompilerGenerated]
			get
			{
				return !this._mapItems.IsNullOrEmpty<GameObject>();
			}
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x002875D7 File Offset: 0x002859D7
		private void Awake()
		{
			this._mapItems = AIProject.MapItemData.Get(this._mapItemID);
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x002875EA File Offset: 0x002859EA
		protected override void Start()
		{
			base.Start();
			if (!this.IsAvailable())
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x0028760C File Offset: 0x00285A0C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (!this._mapItems.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._mapItems)
				{
					if (gameObject != null && !gameObject.activeSelf)
					{
						gameObject.SetActive(true);
					}
				}
			}
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x00287698 File Offset: 0x00285A98
		protected override void OnDisable()
		{
			if (!this._mapItems.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._mapItems)
				{
					if (gameObject != null && gameObject.activeSelf)
					{
						gameObject.SetActive(false);
					}
				}
			}
			base.OnDisable();
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x00287724 File Offset: 0x00285B24
		public override bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			return this.IsAvailable() && base.Entered(basePosition, distance, radiusA, radiusB, angle, forward);
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x00287744 File Offset: 0x00285B44
		public void SetAvailable(bool active)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, bool> dictionary = (environment != null) ? environment.OnceActionPointStateTable : null;
			if (dictionary == null)
			{
				return;
			}
			if (base.gameObject != null && base.gameObject.activeSelf != active)
			{
				base.gameObject.SetActive(active);
			}
			dictionary[this.RegisterID] = !active;
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x002877BC File Offset: 0x00285BBC
		public bool IsAvailable()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, bool> dictionary = (environment != null) ? environment.OnceActionPointStateTable : null;
			if (dictionary == null)
			{
				return false;
			}
			bool flag;
			if (!dictionary.TryGetValue(this.RegisterID, out flag))
			{
				flag = (dictionary[this.RegisterID] = false);
			}
			return !flag;
		}

		// Token: 0x04005565 RID: 21861
		[SerializeField]
		private int _mapItemID = -1;

		// Token: 0x04005566 RID: 21862
		private List<GameObject> _mapItems;
	}
}
