using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BC0 RID: 3008
	public class GroundAnimalHabitatPoint : AnimalPoint
	{
		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x0026B9FB File Offset: 0x00269DFB
		// (set) Token: 0x06005B19 RID: 23321 RVA: 0x0026BA03 File Offset: 0x00269E03
		public WildGround User
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

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x0026BA0C File Offset: 0x00269E0C
		public GroundAnimalHabitatPoint.PointTypes PointType
		{
			[CompilerGenerated]
			get
			{
				return this._pointType;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06005B1B RID: 23323 RVA: 0x0026BA14 File Offset: 0x00269E14
		public GroundAnimalHabitatPoint.AvailableUserTypes UserType
		{
			[CompilerGenerated]
			get
			{
				return this._userType;
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x0026BA1C File Offset: 0x00269E1C
		public Transform CenterPoint
		{
			[CompilerGenerated]
			get
			{
				return this._centerPoint;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x0026BA24 File Offset: 0x00269E24
		public Transform InsidePoint
		{
			[CompilerGenerated]
			get
			{
				return this._insidePoint;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06005B1E RID: 23326 RVA: 0x0026BA2C File Offset: 0x00269E2C
		public Transform OutsidePoint
		{
			[CompilerGenerated]
			get
			{
				return this._outsidePoint;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x0026BA34 File Offset: 0x00269E34
		public override bool Available
		{
			[CompilerGenerated]
			get
			{
				return this._centerPoint != null && this._insidePoint != null && this._outsidePoint != null;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06005B20 RID: 23328 RVA: 0x0026BA67 File Offset: 0x00269E67
		public bool IsPopPoint
		{
			[CompilerGenerated]
			get
			{
				return this._pointType == GroundAnimalHabitatPoint.PointTypes.Both || this._pointType == GroundAnimalHabitatPoint.PointTypes.Pop;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06005B21 RID: 23329 RVA: 0x0026BA80 File Offset: 0x00269E80
		public bool IsDepopPoint
		{
			[CompilerGenerated]
			get
			{
				return this._pointType == GroundAnimalHabitatPoint.PointTypes.Both || this._pointType == GroundAnimalHabitatPoint.PointTypes.Depop;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06005B22 RID: 23330 RVA: 0x0026BA99 File Offset: 0x00269E99
		public bool IsCatOnly
		{
			[CompilerGenerated]
			get
			{
				return this._userType == GroundAnimalHabitatPoint.AvailableUserTypes.Cat;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06005B23 RID: 23331 RVA: 0x0026BAA4 File Offset: 0x00269EA4
		public bool IsChickenOnly
		{
			[CompilerGenerated]
			get
			{
				return this._userType == GroundAnimalHabitatPoint.AvailableUserTypes.Chicken;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06005B24 RID: 23332 RVA: 0x0026BAAF File Offset: 0x00269EAF
		public bool IsBoth
		{
			[CompilerGenerated]
			get
			{
				return this._userType == (GroundAnimalHabitatPoint.AvailableUserTypes)0;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06005B25 RID: 23333 RVA: 0x0026BABA File Offset: 0x00269EBA
		public float MoveRadius
		{
			[CompilerGenerated]
			get
			{
				return this._moveRadius;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06005B26 RID: 23334 RVA: 0x0026BAC2 File Offset: 0x00269EC2
		public float MoveHeight
		{
			[CompilerGenerated]
			get
			{
				return this._moveHeight;
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06005B27 RID: 23335 RVA: 0x0026BACA File Offset: 0x00269ECA
		// (set) Token: 0x06005B28 RID: 23336 RVA: 0x0026BAD2 File Offset: 0x00269ED2
		public Vector2 CoolTime { get; set; } = Vector2.zero;

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x0026BADB File Offset: 0x00269EDB
		// (set) Token: 0x06005B2A RID: 23338 RVA: 0x0026BAE3 File Offset: 0x00269EE3
		public bool IsCountStop { get; set; } = true;

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0026BAEC File Offset: 0x00269EEC
		// (set) Token: 0x06005B2C RID: 23340 RVA: 0x0026BAF4 File Offset: 0x00269EF4
		public bool IsCountCoolTime { get; set; }

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x0026BAFD File Offset: 0x00269EFD
		// (set) Token: 0x06005B2E RID: 23342 RVA: 0x0026BB05 File Offset: 0x00269F05
		public bool IsActive { get; set; }

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x0026BB0E File Offset: 0x00269F0E
		// (set) Token: 0x06005B30 RID: 23344 RVA: 0x0026BB16 File Offset: 0x00269F16
		public float CoolTimeCounter { get; private set; }

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x0026BB1F File Offset: 0x00269F1F
		// (set) Token: 0x06005B32 RID: 23346 RVA: 0x0026BB27 File Offset: 0x00269F27
		public Func<Vector3, bool> AddCheck { get; set; }

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06005B33 RID: 23347 RVA: 0x0026BB30 File Offset: 0x00269F30
		// (set) Token: 0x06005B34 RID: 23348 RVA: 0x0026BB38 File Offset: 0x00269F38
		public Func<GroundAnimalHabitatPoint, WildGround> AddAnimalAction { get; set; }

		// Token: 0x06005B35 RID: 23349 RVA: 0x0026BB44 File Offset: 0x00269F44
		protected override void Start()
		{
			base.Start();
			this.locateInfo.LocateAll();
			if (!this.Available)
			{
				return;
			}
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this).TakeUntilDestroy(this._centerPoint).TakeUntilDestroy(this._insidePoint).TakeUntilDestroy(this._outsidePoint)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.UpdateCoolTime();
			});
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06005B36 RID: 23350 RVA: 0x0026BBBD File Offset: 0x00269FBD
		public bool InUsed
		{
			[CompilerGenerated]
			get
			{
				return this._user != null;
			}
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x0026BBCB File Offset: 0x00269FCB
		public void SetCoolTime()
		{
			this.IsCountCoolTime = true;
			this.CoolTimeCounter = this.CoolTime.RandomRange();
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x0026BBE5 File Offset: 0x00269FE5
		public void SetCoolTime(float _coolTime)
		{
			this.IsCountCoolTime = true;
			this.CoolTimeCounter = _coolTime;
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x0026BBF8 File Offset: 0x00269FF8
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
				if (this.AddCheck != null && this.AddAnimalAction != null && this.AddCheck(this._insidePoint.position))
				{
					this._user = this.AddAnimalAction(this);
					this.IsCountCoolTime = (this._user == null);
				}
			}
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x0026BCD2 File Offset: 0x0026A0D2
		public bool SetUse(WildGround _animal)
		{
			if (_animal == null)
			{
				return false;
			}
			if (this._user != null && this._user != _animal)
			{
				return false;
			}
			this._user = _animal;
			return true;
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x0026BD10 File Offset: 0x0026A110
		public bool StopUse(WildGround _animal)
		{
			if (_animal == null || this._user == null)
			{
				return false;
			}
			if (this._user != _animal)
			{
				return false;
			}
			this._user = null;
			this.SetCoolTime();
			return true;
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x0026BD60 File Offset: 0x0026A160
		private void LookAt(Transform _origin, Vector3 _target)
		{
			_target.y = _origin.position.y;
			_origin.LookAt(_target, Vector3.up);
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x0026BD90 File Offset: 0x0026A190
		private void Awake()
		{
			if (this._centerPoint == null)
			{
				this._centerPoint = base.transform;
			}
			if (this._outsidePoint != null && this._insidePoint != null)
			{
				this.LookAt(this._outsidePoint, this._insidePoint.position);
				this.LookAt(this._insidePoint, this._outsidePoint.position);
			}
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x0026BE0C File Offset: 0x0026A20C
		private bool AvailablePoint(Waypoint _point)
		{
			if (!_point.gameObject.activeSelf)
			{
				return false;
			}
			Vector3 position = _point.transform.position;
			Vector3 position2 = this._centerPoint.position;
			return base.DistanceY(position, position2) <= this._moveHeight && base.DistanceXZ(position, position2) <= this._moveRadius;
		}

		// Token: 0x06005B3F RID: 23359 RVA: 0x0026BE6C File Offset: 0x0026A26C
		private bool AvailablePoint(GroundAnimalHabitatPoint _point)
		{
			if (_point.InsidePoint == null)
			{
				return false;
			}
			if (!_point.gameObject.activeSelf)
			{
				return false;
			}
			Vector3 position = _point.InsidePoint.position;
			Vector3 position2 = this._centerPoint.position;
			return base.DistanceY(position, position2) <= this._moveHeight && base.DistanceXZ(position, position2) <= this._moveRadius;
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x0026BEE0 File Offset: 0x0026A2E0
		public IEnumerator SetPointsAsync(Waypoint[] _waypoints, GroundAnimalHabitatPoint[] _depopPoints, int _breakCount)
		{
			this.Waypoints.Clear();
			this.DepopPoints.Clear();
			if (this._centerPoint == null)
			{
				yield break;
			}
			int _count = 0;
			if (!_waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint _point in _waypoints)
				{
					if (this.AvailablePoint(_point))
					{
						this.Waypoints.Add(_point);
					}
					if (++_count % _breakCount == 0)
					{
						yield return null;
					}
				}
			}
			if (!_depopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				foreach (GroundAnimalHabitatPoint _point2 in _depopPoints)
				{
					if (this.AvailablePoint(_point2))
					{
						this.DepopPoints.Add(_point2);
					}
					if (++_count % _breakCount == 0)
					{
						yield return null;
					}
				}
			}
			if (this.IsDepopPoint && !this.DepopPoints.Contains(this) && this.Available)
			{
				this.DepopPoints.Add(this);
			}
			yield return null;
			yield break;
		}

		// Token: 0x0400529B RID: 21147
		private WildGround _user;

		// Token: 0x0400529C RID: 21148
		[SerializeField]
		private GroundAnimalHabitatPoint.PointTypes _pointType;

		// Token: 0x0400529D RID: 21149
		[SerializeField]
		private GroundAnimalHabitatPoint.AvailableUserTypes _userType = GroundAnimalHabitatPoint.AvailableUserTypes.Cat;

		// Token: 0x0400529E RID: 21150
		[SerializeField]
		private Transform _centerPoint;

		// Token: 0x0400529F RID: 21151
		[SerializeField]
		private Transform _insidePoint;

		// Token: 0x040052A0 RID: 21152
		[SerializeField]
		private Transform _outsidePoint;

		// Token: 0x040052A1 RID: 21153
		[SerializeField]
		private float _moveRadius = 100f;

		// Token: 0x040052A2 RID: 21154
		[SerializeField]
		private float _moveHeight = 20f;

		// Token: 0x040052A3 RID: 21155
		[SerializeField]
		private AnimalPoint.LocateInfo locateInfo = new AnimalPoint.LocateInfo();

		// Token: 0x040052AB RID: 21163
		public List<Waypoint> Waypoints = new List<Waypoint>();

		// Token: 0x040052AC RID: 21164
		public List<GroundAnimalHabitatPoint> DepopPoints = new List<GroundAnimalHabitatPoint>();

		// Token: 0x02000BC1 RID: 3009
		public enum PointTypes
		{
			// Token: 0x040052AE RID: 21166
			Both,
			// Token: 0x040052AF RID: 21167
			Pop,
			// Token: 0x040052B0 RID: 21168
			Depop
		}

		// Token: 0x02000BC2 RID: 3010
		public enum AvailableUserTypes
		{
			// Token: 0x040052B2 RID: 21170
			Cat = 1,
			// Token: 0x040052B3 RID: 21171
			Chicken
		}
	}
}
