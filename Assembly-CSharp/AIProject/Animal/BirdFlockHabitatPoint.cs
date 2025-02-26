using System;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BBC RID: 3004
	public class BirdFlockHabitatPoint : AnimalPoint
	{
		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x0026AD56 File Offset: 0x00269156
		// (set) Token: 0x06005AB4 RID: 23220 RVA: 0x0026AD5E File Offset: 0x0026915E
		public WildBirdFlock User
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

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0026AD67 File Offset: 0x00269167
		public Vector2 CoolTimeRange
		{
			[CompilerGenerated]
			get
			{
				return this._coolTimeRange;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x0026AD6F File Offset: 0x0026916F
		public BirdFlockHabitatPoint.BirdMoveAreaInfo[] AreaInfos
		{
			[CompilerGenerated]
			get
			{
				return this._areaInfos;
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x0026AD77 File Offset: 0x00269177
		public bool InUsed
		{
			[CompilerGenerated]
			get
			{
				return this._user != null;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06005AB8 RID: 23224 RVA: 0x0026AD85 File Offset: 0x00269185
		public override bool Available
		{
			[CompilerGenerated]
			get
			{
				bool result;
				if (this._areaInfos != null)
				{
					result = this._areaInfos.Exists((BirdFlockHabitatPoint.BirdMoveAreaInfo x) => x != null && x.Available);
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06005AB9 RID: 23225 RVA: 0x0026ADBD File Offset: 0x002691BD
		// (set) Token: 0x06005ABA RID: 23226 RVA: 0x0026ADC5 File Offset: 0x002691C5
		public bool IsCountStop { get; set; } = true;

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06005ABB RID: 23227 RVA: 0x0026ADCE File Offset: 0x002691CE
		// (set) Token: 0x06005ABC RID: 23228 RVA: 0x0026ADD6 File Offset: 0x002691D6
		public bool IsCountCoolTime { get; set; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06005ABD RID: 23229 RVA: 0x0026ADDF File Offset: 0x002691DF
		// (set) Token: 0x06005ABE RID: 23230 RVA: 0x0026ADE7 File Offset: 0x002691E7
		public bool IsActive { get; set; }

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06005ABF RID: 23231 RVA: 0x0026ADF0 File Offset: 0x002691F0
		// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x0026ADF8 File Offset: 0x002691F8
		public float CoolTimeCounter { get; private set; }

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x0026AE01 File Offset: 0x00269201
		// (set) Token: 0x06005AC2 RID: 23234 RVA: 0x0026AE09 File Offset: 0x00269209
		public Func<Vector3, bool> AddCheck { get; set; }

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x0026AE12 File Offset: 0x00269212
		// (set) Token: 0x06005AC4 RID: 23236 RVA: 0x0026AE1A File Offset: 0x0026921A
		public Func<BirdFlockHabitatPoint, WildBirdFlock> AddAnimalAction { get; set; }

		// Token: 0x06005AC5 RID: 23237 RVA: 0x0026AE23 File Offset: 0x00269223
		protected override void Start()
		{
			base.Start();
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.UpdateCoolTime();
			});
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x0026AE59 File Offset: 0x00269259
		public void SetCoolTime()
		{
			this.IsCountCoolTime = true;
			this.CoolTimeCounter = this.CoolTimeRange.RandomRange();
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x0026AE74 File Offset: 0x00269274
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
				if (this.AddCheck != null && this.AddAnimalAction != null && this.AddCheck(base.Position))
				{
					this._user = this.AddAnimalAction(this);
					this.IsCountCoolTime = (this._user == null);
				}
			}
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x0026AF49 File Offset: 0x00269349
		public bool SetUse(WildBirdFlock _birdFlock)
		{
			if (_birdFlock == null)
			{
				return false;
			}
			if (this._user != null && this._user != _birdFlock)
			{
				return false;
			}
			this._user = _birdFlock;
			return true;
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x0026AF88 File Offset: 0x00269388
		public bool StopUse(WildBirdFlock _birdFlock)
		{
			if (this._user == null || _birdFlock == null)
			{
				return false;
			}
			if (this._user != _birdFlock)
			{
				return false;
			}
			this._user = null;
			this.SetCoolTime();
			return true;
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x0026AFD5 File Offset: 0x002693D5
		public bool StopUse()
		{
			if (this._user == null)
			{
				return false;
			}
			this._user = null;
			return true;
		}

		// Token: 0x0400526F RID: 21103
		private WildBirdFlock _user;

		// Token: 0x04005270 RID: 21104
		[SerializeField]
		private Vector2 _coolTimeRange = Vector2.zero;

		// Token: 0x04005271 RID: 21105
		[SerializeField]
		private BirdFlockHabitatPoint.BirdMoveAreaInfo[] _areaInfos = new BirdFlockHabitatPoint.BirdMoveAreaInfo[0];

		// Token: 0x02000BBD RID: 3005
		[Serializable]
		public class BirdMoveAreaInfo
		{
			// Token: 0x1700111B RID: 4379
			// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0026B031 File Offset: 0x00269431
			public Transform StartPoint
			{
				[CompilerGenerated]
				get
				{
					return this._startPoint;
				}
			}

			// Token: 0x1700111C RID: 4380
			// (get) Token: 0x06005AD0 RID: 23248 RVA: 0x0026B039 File Offset: 0x00269439
			public Transform EndPoint
			{
				[CompilerGenerated]
				get
				{
					return this._endPoint;
				}
			}

			// Token: 0x1700111D RID: 4381
			// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x0026B041 File Offset: 0x00269441
			public bool Available
			{
				[CompilerGenerated]
				get
				{
					return this._startPoint != null && this._endPoint != null;
				}
			}

			// Token: 0x1700111E RID: 4382
			// (get) Token: 0x06005AD2 RID: 23250 RVA: 0x0026B063 File Offset: 0x00269463
			public Vector2 MoveRect
			{
				[CompilerGenerated]
				get
				{
					return this._moveRect;
				}
			}

			// Token: 0x1700111F RID: 4383
			// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x0026B06B File Offset: 0x0026946B
			public Vector2Int CreateNumRange
			{
				[CompilerGenerated]
				get
				{
					return this._createNumRange;
				}
			}

			// Token: 0x04005279 RID: 21113
			[SerializeField]
			private Transform _startPoint;

			// Token: 0x0400527A RID: 21114
			[SerializeField]
			private Transform _endPoint;

			// Token: 0x0400527B RID: 21115
			[SerializeField]
			private Vector2 _moveRect = Vector2.zero;

			// Token: 0x0400527C RID: 21116
			[SerializeField]
			private Vector2Int _createNumRange = Vector2Int.one;
		}
	}
}
