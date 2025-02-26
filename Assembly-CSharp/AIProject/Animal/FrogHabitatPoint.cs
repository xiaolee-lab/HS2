using System;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BBF RID: 3007
	public class FrogHabitatPoint : AnimalPoint
	{
		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x0026B684 File Offset: 0x00269A84
		public int ItemID
		{
			[CompilerGenerated]
			get
			{
				return this._itemID;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x0026B68C File Offset: 0x00269A8C
		// (set) Token: 0x06005AFB RID: 23291 RVA: 0x0026B694 File Offset: 0x00269A94
		public WildFrog User
		{
			get
			{
				return this._user;
			}
			set
			{
				this._user = value;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06005AFC RID: 23292 RVA: 0x0026B69D File Offset: 0x00269A9D
		public float MoveRadius
		{
			[CompilerGenerated]
			get
			{
				return this._moveRadius;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06005AFD RID: 23293 RVA: 0x0026B6A5 File Offset: 0x00269AA5
		// (set) Token: 0x06005AFE RID: 23294 RVA: 0x0026B6AD File Offset: 0x00269AAD
		public Vector2 CoolTimeRange { get; set; } = new Vector2(240f, 300f);

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06005AFF RID: 23295 RVA: 0x0026B6B6 File Offset: 0x00269AB6
		public bool InUsed
		{
			[CompilerGenerated]
			get
			{
				return this.User != null;
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x0026B6C4 File Offset: 0x00269AC4
		// (set) Token: 0x06005B01 RID: 23297 RVA: 0x0026B6CC File Offset: 0x00269ACC
		public bool IsCountStop { get; set; } = true;

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06005B02 RID: 23298 RVA: 0x0026B6D5 File Offset: 0x00269AD5
		// (set) Token: 0x06005B03 RID: 23299 RVA: 0x0026B6DD File Offset: 0x00269ADD
		public bool IsCountCoolTime { get; set; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x0026B6E6 File Offset: 0x00269AE6
		// (set) Token: 0x06005B05 RID: 23301 RVA: 0x0026B6EE File Offset: 0x00269AEE
		public bool IsActive { get; set; }

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x0026B6F7 File Offset: 0x00269AF7
		// (set) Token: 0x06005B07 RID: 23303 RVA: 0x0026B6FF File Offset: 0x00269AFF
		public float CoolTimeCounter { get; private set; }

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x0026B708 File Offset: 0x00269B08
		// (set) Token: 0x06005B09 RID: 23305 RVA: 0x0026B710 File Offset: 0x00269B10
		public bool ForcedAdd { get; set; }

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x0026B719 File Offset: 0x00269B19
		// (set) Token: 0x06005B0B RID: 23307 RVA: 0x0026B721 File Offset: 0x00269B21
		public Func<FrogHabitatPoint, bool> AddCheck { get; set; }

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06005B0C RID: 23308 RVA: 0x0026B72A File Offset: 0x00269B2A
		// (set) Token: 0x06005B0D RID: 23309 RVA: 0x0026B732 File Offset: 0x00269B32
		public Func<FrogHabitatPoint, WildFrog> AddAnimalAction { get; set; }

		// Token: 0x06005B0E RID: 23310 RVA: 0x0026B73C File Offset: 0x00269B3C
		protected override void Start()
		{
			base.Start();
			LocateTypes locateType = base.LocateType;
			if (locateType != LocateTypes.Collider)
			{
				if (locateType == LocateTypes.NavMesh)
				{
					this.LocateOnNavMesh();
				}
			}
			else
			{
				this.LocateOnCollider();
			}
			Observable.EveryUpdate().TakeUntilDestroy(this).Subscribe(delegate(long _)
			{
				this.UpdateCoolTime();
			});
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x0026B79C File Offset: 0x00269B9C
		[ContextMenu("Locate Ground On Collider")]
		private void LocateOnCollider()
		{
			LayerMask layerMask = default(LayerMask);
			LayerMask layerMask2;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				layerMask2 = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.MapLayer;
			}
			else
			{
				layerMask2 = LayerMask.NameToLayer("MapArea");
			}
			AnimalPoint.RelocationOnCollider(base.transform, 3f, layerMask2);
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x0026B7F8 File Offset: 0x00269BF8
		[ContextMenu("Locate Ground On NavMesh")]
		private void LocateOnNavMesh()
		{
			AnimalPoint.RelocationOnNavMesh(base.transform, 10f);
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x0026B80A File Offset: 0x00269C0A
		public void SetCoolTime(float _coolTime)
		{
			this.IsCountCoolTime = true;
			this.CoolTimeCounter = _coolTime;
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x0026B81A File Offset: 0x00269C1A
		public void SetCoolTime()
		{
			this.IsCountCoolTime = true;
			this.CoolTimeCounter = this.CoolTimeRange.RandomRange();
		}

		// Token: 0x06005B13 RID: 23315 RVA: 0x0026B834 File Offset: 0x00269C34
		private void UpdateCoolTime()
		{
			if (this.IsCountStop)
			{
				return;
			}
			if (this._user != null)
			{
				return;
			}
			if (!this.IsActive)
			{
				return;
			}
			if (!this.IsCountCoolTime)
			{
				return;
			}
			if (Mathf.Approximately(0f, Time.timeScale))
			{
				return;
			}
			this.CoolTimeCounter -= Time.unscaledDeltaTime;
			if (this.CoolTimeCounter <= 0f)
			{
				this.CoolTimeCounter = 0f;
				if (this.AddCheck != null && this.AddAnimalAction != null && this.AddCheck(this))
				{
					this._user = this.AddAnimalAction(this);
					this.IsCountCoolTime = (this._user == null);
				}
			}
		}

		// Token: 0x06005B14 RID: 23316 RVA: 0x0026B904 File Offset: 0x00269D04
		public bool SetUse(WildFrog _frog)
		{
			if (_frog == null)
			{
				return false;
			}
			if (this._user != null && this._user != _frog)
			{
				return false;
			}
			this._user = _frog;
			return true;
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x0026B940 File Offset: 0x00269D40
		public bool StopUse(WildFrog _frog)
		{
			if (_frog == null || this._user == null)
			{
				return false;
			}
			if (this._user != _frog)
			{
				return false;
			}
			this._user = null;
			this.SetCoolTime();
			return true;
		}

		// Token: 0x04005290 RID: 21136
		[SerializeField]
		private int _itemID = -1;

		// Token: 0x04005291 RID: 21137
		private WildFrog _user;

		// Token: 0x04005292 RID: 21138
		[SerializeField]
		private float _moveRadius = 10f;
	}
}
