using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BBE RID: 3006
	public class ButterflyHabitatPoint : AnimalPoint
	{
		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06005AD5 RID: 23253 RVA: 0x0026B0DB File Offset: 0x002694DB
		public Dictionary<int, WildButterfly> User
		{
			[CompilerGenerated]
			get
			{
				return this._user;
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x0026B0E3 File Offset: 0x002694E3
		public List<WildButterfly> UserList
		{
			[CompilerGenerated]
			get
			{
				return this._userList;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06005AD7 RID: 23255 RVA: 0x0026B0EB File Offset: 0x002694EB
		public Transform Center
		{
			[CompilerGenerated]
			get
			{
				return this._center;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06005AD8 RID: 23256 RVA: 0x0026B0F3 File Offset: 0x002694F3
		public float MoveRadius
		{
			[CompilerGenerated]
			get
			{
				return this._moveRadius;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06005AD9 RID: 23257 RVA: 0x0026B0FB File Offset: 0x002694FB
		public float MoveHeight
		{
			[CompilerGenerated]
			get
			{
				return this._moveHeight;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06005ADA RID: 23258 RVA: 0x0026B103 File Offset: 0x00269503
		public float ViaPointRadius
		{
			[CompilerGenerated]
			get
			{
				return this._viaPointRadius;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x0026B10B File Offset: 0x0026950B
		public Transform ViaPoint
		{
			[CompilerGenerated]
			get
			{
				return this._viaPoint;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x0026B113 File Offset: 0x00269513
		public Transform DepopPoint
		{
			[CompilerGenerated]
			get
			{
				return this._depopPoint;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06005ADD RID: 23261 RVA: 0x0026B11B File Offset: 0x0026951B
		public override bool Available
		{
			[CompilerGenerated]
			get
			{
				return this._center != null && this._viaPoint != null && this._depopPoint != null;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x0026B14E File Offset: 0x0026954E
		public Vector2Int CreateNumRange
		{
			[CompilerGenerated]
			get
			{
				return this._createNumRange;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06005ADF RID: 23263 RVA: 0x0026B156 File Offset: 0x00269556
		public bool InUsed
		{
			[CompilerGenerated]
			get
			{
				return !this._user.IsNullOrEmpty<int, WildButterfly>();
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x0026B166 File Offset: 0x00269566
		public int UserCount
		{
			[CompilerGenerated]
			get
			{
				return this._user.Count;
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06005AE1 RID: 23265 RVA: 0x0026B173 File Offset: 0x00269573
		// (set) Token: 0x06005AE2 RID: 23266 RVA: 0x0026B17B File Offset: 0x0026957B
		public bool IsStop { get; set; } = true;

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06005AE3 RID: 23267 RVA: 0x0026B184 File Offset: 0x00269584
		// (set) Token: 0x06005AE4 RID: 23268 RVA: 0x0026B18C File Offset: 0x0026958C
		public bool IsActive { get; set; }

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06005AE5 RID: 23269 RVA: 0x0026B195 File Offset: 0x00269595
		// (set) Token: 0x06005AE6 RID: 23270 RVA: 0x0026B19D File Offset: 0x0026959D
		public bool IsCreate { get; set; }

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0026B1A6 File Offset: 0x002695A6
		// (set) Token: 0x06005AE8 RID: 23272 RVA: 0x0026B1AE File Offset: 0x002695AE
		public Func<Vector3, bool> AddCheck { get; set; }

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x0026B1B7 File Offset: 0x002695B7
		// (set) Token: 0x06005AEA RID: 23274 RVA: 0x0026B1BF File Offset: 0x002695BF
		public Func<ButterflyHabitatPoint, WildButterfly> AddAnimalAction { get; set; }

		// Token: 0x06005AEB RID: 23275 RVA: 0x0026B1C8 File Offset: 0x002695C8
		private void Awake()
		{
			if (!this._center)
			{
				this._center = base.transform;
			}
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x0026B1E8 File Offset: 0x002695E8
		protected override void Start()
		{
			base.Start();
			if (!this.Available)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this).TakeUntilDestroy(this._center).TakeUntilDestroy(this._viaPoint).TakeUntilDestroy(this._depopPoint)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.CreateCheck();
			});
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x0026B25C File Offset: 0x0026965C
		private void CreateCheck()
		{
			if (this.IsStop)
			{
				return;
			}
			if (!this.IsActive)
			{
				return;
			}
			if (this.IsCreate)
			{
				return;
			}
			if (Mathf.Approximately(0f, Time.timeScale))
			{
				return;
			}
			if (this.AddCheck != null && this.AddAnimalAction != null)
			{
				this.CreateButterfly();
				this.IsCreate = true;
			}
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x0026B2C8 File Offset: 0x002696C8
		private void CreateButterfly()
		{
			int num = this._createNumRange.RandomRange();
			this._userList.RemoveAll((WildButterfly x) => x == null || x.gameObject == null);
			int num2 = num - this._userList.Count;
			if (num2 == 0)
			{
				if (!this._userList.IsNullOrEmpty<WildButterfly>())
				{
					foreach (WildButterfly wildButterfly in this._userList)
					{
						wildButterfly.ForcedLocomotion();
					}
				}
			}
			else if (0 < num2)
			{
				if (!this._userList.IsNullOrEmpty<WildButterfly>())
				{
					foreach (WildButterfly wildButterfly2 in this._userList)
					{
						wildButterfly2.ForcedLocomotion();
					}
				}
				while (0 < this._createDisposableList.Count)
				{
					IDisposable disposable = this._createDisposableList.Dequeue();
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				for (int i = 0; i < num2; i++)
				{
					float num3 = UnityEngine.Random.Range(0f, 4f);
					IDisposable item = Observable.Timer(TimeSpan.FromSeconds((double)num3)).TakeUntilDestroy(this).TakeUntilDestroy(this._center).TakeUntilDestroy(this._viaPoint).TakeUntilDestroy(this._depopPoint).Subscribe(delegate(long _)
					{
						if (this.AddCheck == null || !this.AddCheck(this._center.position))
						{
							return;
						}
						Func<ButterflyHabitatPoint, WildButterfly> addAnimalAction = this.AddAnimalAction;
						if (addAnimalAction != null)
						{
							addAnimalAction(this);
						}
					}, delegate(Exception ex)
					{
					}, delegate()
					{
					});
					this._createDisposableList.Enqueue(item);
				}
			}
			else if (num2 < 0)
			{
				num2 = Mathf.Abs(num2);
				for (int j = 0; j < this._userList.Count; j++)
				{
					WildButterfly wildButterfly3 = this._userList[j];
					if (j < num2)
					{
						wildButterfly3.ForcedDepop();
					}
					else
					{
						wildButterfly3.ForcedLocomotion();
					}
				}
			}
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x0026B528 File Offset: 0x00269928
		public bool SetUse(WildButterfly _butterfly)
		{
			if (!this._userList.Contains(_butterfly))
			{
				this._userList.Add(_butterfly);
			}
			return this._user.AddNonContains(_butterfly.InstanceID, _butterfly);
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0026B55C File Offset: 0x0026995C
		public bool StopUse(WildButterfly _butterfly)
		{
			if (this._userList.Contains(_butterfly))
			{
				this._userList.Remove(_butterfly);
			}
			this._userList.RemoveAll((WildButterfly x) => x == null || x.gameObject == null);
			return this._user.Remove(_butterfly.InstanceID);
		}

		// Token: 0x0400527D RID: 21117
		private Dictionary<int, WildButterfly> _user = new Dictionary<int, WildButterfly>();

		// Token: 0x0400527E RID: 21118
		private List<WildButterfly> _userList = new List<WildButterfly>();

		// Token: 0x0400527F RID: 21119
		[SerializeField]
		[Tooltip("移動可能領域の中心")]
		private Transform _center;

		// Token: 0x04005280 RID: 21120
		[SerializeField]
		[Tooltip("移動可能範囲(半径)")]
		private float _moveRadius = 10f;

		// Token: 0x04005281 RID: 21121
		[SerializeField]
		[Tooltip("移動可能範囲(高さ)")]
		private float _moveHeight = 5f;

		// Token: 0x04005282 RID: 21122
		[SerializeField]
		[Tooltip("出現経由ポイントの通過可能半径")]
		private float _viaPointRadius = 3f;

		// Token: 0x04005283 RID: 21123
		[SerializeField]
		[Tooltip("経由ポイント")]
		private Transform _viaPoint;

		// Token: 0x04005284 RID: 21124
		[SerializeField]
		[Tooltip("消失ポイント")]
		private Transform _depopPoint;

		// Token: 0x04005285 RID: 21125
		[SerializeField]
		private Vector2Int _createNumRange = Vector2Int.one;

		// Token: 0x0400528B RID: 21131
		private Queue<IDisposable> _createDisposableList = new Queue<IDisposable>();
	}
}
