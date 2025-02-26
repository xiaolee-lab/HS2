using System;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BC3 RID: 3011
	public class MechaHabitatPoint : AnimalPoint
	{
		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x0026C1D7 File Offset: 0x0026A5D7
		// (set) Token: 0x06005B45 RID: 23365 RVA: 0x0026C1DF File Offset: 0x0026A5DF
		public WildMecha User
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

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0026C1E8 File Offset: 0x0026A5E8
		public Vector2 CoolTimeRange
		{
			[CompilerGenerated]
			get
			{
				return this._coolTimeRange;
			}
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x0026C1F0 File Offset: 0x0026A5F0
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
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.UpdateCoolTime();
			});
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x0026C264 File Offset: 0x0026A664
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

		// Token: 0x06005B49 RID: 23369 RVA: 0x0026C2C0 File Offset: 0x0026A6C0
		[ContextMenu("Locate Ground On NavMesh")]
		private void LocateOnNavMesh()
		{
			AnimalPoint.RelocationOnNavMesh(base.transform, 10f);
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x0026C2D2 File Offset: 0x0026A6D2
		public bool InUsed
		{
			[CompilerGenerated]
			get
			{
				return this._user != null;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x0026C2E0 File Offset: 0x0026A6E0
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0026C2E8 File Offset: 0x0026A6E8
		public bool IsCountStop { get; set; } = true;

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x0026C2F1 File Offset: 0x0026A6F1
		// (set) Token: 0x06005B4E RID: 23374 RVA: 0x0026C2F9 File Offset: 0x0026A6F9
		public bool IsCountCoolTime { get; set; }

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x0026C302 File Offset: 0x0026A702
		// (set) Token: 0x06005B50 RID: 23376 RVA: 0x0026C30A File Offset: 0x0026A70A
		public bool IsActive { get; set; }

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x0026C313 File Offset: 0x0026A713
		// (set) Token: 0x06005B52 RID: 23378 RVA: 0x0026C31B File Offset: 0x0026A71B
		public float CoolTimeCounter { get; private set; }

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x0026C324 File Offset: 0x0026A724
		// (set) Token: 0x06005B54 RID: 23380 RVA: 0x0026C32C File Offset: 0x0026A72C
		public bool ForcedAdd { get; set; }

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06005B55 RID: 23381 RVA: 0x0026C335 File Offset: 0x0026A735
		// (set) Token: 0x06005B56 RID: 23382 RVA: 0x0026C33D File Offset: 0x0026A73D
		public Func<MechaHabitatPoint, bool> AddCheck { get; set; }

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0026C346 File Offset: 0x0026A746
		// (set) Token: 0x06005B58 RID: 23384 RVA: 0x0026C34E File Offset: 0x0026A74E
		public Func<MechaHabitatPoint, WildMecha> AddAnimalAction { get; set; }

		// Token: 0x06005B59 RID: 23385 RVA: 0x0026C357 File Offset: 0x0026A757
		public void SetCoolTime(float coolTime)
		{
			this.IsCountCoolTime = true;
			this.CoolTimeCounter = coolTime;
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x0026C367 File Offset: 0x0026A767
		public void SetCoolTime()
		{
			this.IsCountCoolTime = true;
			this.CoolTimeCounter = this.CoolTimeRange.RandomRange();
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x0026C384 File Offset: 0x0026A784
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

		// Token: 0x06005B5C RID: 23388 RVA: 0x0026C454 File Offset: 0x0026A854
		public bool SetUse(WildMecha _mecha)
		{
			if (_mecha == null)
			{
				return false;
			}
			if (this._user != null && this._user != _mecha)
			{
				return false;
			}
			this._user = _mecha;
			return true;
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x0026C490 File Offset: 0x0026A890
		public bool StopUse(WildMecha _mecha)
		{
			if (_mecha == null || this._user == null)
			{
				return false;
			}
			if (this._user != _mecha)
			{
				return false;
			}
			this._user = null;
			this.SetCoolTime();
			return true;
		}

		// Token: 0x040052B4 RID: 21172
		private WildMecha _user;

		// Token: 0x040052B5 RID: 21173
		[SerializeField]
		private Vector2 _coolTimeRange = Vector2.zero;
	}
}
