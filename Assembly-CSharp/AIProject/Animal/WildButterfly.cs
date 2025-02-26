using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BC7 RID: 3015
	public class WildButterfly : ButterflyBase
	{
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06005B8B RID: 23435 RVA: 0x0026D748 File Offset: 0x0026BB48
		// (set) Token: 0x06005B8C RID: 23436 RVA: 0x0026D750 File Offset: 0x0026BB50
		private Vector3? TargetPoint { get; set; }

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06005B8D RID: 23437 RVA: 0x0026D75C File Offset: 0x0026BB5C
		public bool IsReached
		{
			[CompilerGenerated]
			get
			{
				return this.TargetPoint == null || Vector3.Distance(this.Position, this.TargetPoint.Value) <= this.changeTargetDistance;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06005B8E RID: 23438 RVA: 0x0026D7A6 File Offset: 0x0026BBA6
		public override bool ParamRisePossible
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x0026D7AC File Offset: 0x0026BBAC
		public void Initialize(ButterflyHabitatPoint _habitatPoint)
		{
			this.Clear();
			this.habitatPoint = _habitatPoint;
			if (_habitatPoint == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			this.habitatPoint.SetUse(this);
			if (!this.habitatPoint.Available)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			MapArea ownerArea = this.habitatPoint.OwnerArea;
			base.ChunkID = ((!(ownerArea != null)) ? 0 : ownerArea.ChunkID);
			base.LoadBody();
			base.SetStateData();
			this.animation = base.GetComponentInChildren<Animation>(true);
			string paramName = string.Empty;
			float speed = 1f;
			if (Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalDefinePack != null)
			{
				AnimalDefinePack.AnimatorInfoGroup animatorInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AnimatorInfo;
				if (animatorInfo != null)
				{
					paramName = animatorInfo.AnimationSpeedParamName;
					speed = animatorInfo.ButterflyAnimationSpeed;
				}
			}
			this.SetAnimationSpeed(paramName, speed);
			Vector3 position = this.habitatPoint.DepopPoint.position;
			Vector3 position2 = this.habitatPoint.ViaPoint.position;
			float viaPointRadius = this.habitatPoint.ViaPointRadius;
			Vector3 randomMoveAreaPoint = this.GetRandomMoveAreaPoint();
			Vector3 vector = randomMoveAreaPoint;
			vector.y = position2.y;
			vector = Vector3.ClampMagnitude(vector - position2, viaPointRadius);
			vector = position2 + vector;
			this.pointList.Clear();
			this.pointList.Add(vector);
			this.pointList.Add(randomMoveAreaPoint);
			randomMoveAreaPoint.y = position.y;
			base.Rotation = Quaternion.LookRotation(randomMoveAreaPoint - position, Vector3.up);
			this.Position = position;
			bool flag = false;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			this.SetState(AnimalState.Start, null);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0026D97C File Offset: 0x0026BD7C
		public override void Clear()
		{
			this.TargetPoint = null;
			base.Clear();
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0026D99E File Offset: 0x0026BD9E
		protected override void OnDestroy()
		{
			this.Active = false;
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
				this.habitatPoint = null;
			}
			base.OnDestroy();
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x0026D9D4 File Offset: 0x0026BDD4
		private void SetAnimationSpeed(float _speed)
		{
			if (this.animation == null)
			{
				return;
			}
			IEnumerator enumerator = this.animation.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					animationState.speed = _speed;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x0026DA4C File Offset: 0x0026BE4C
		public void SetAnimationSpeed(string _paramName, float _speed)
		{
			if (!_paramName.IsNullOrEmpty() && base.AnimatorEnabled)
			{
				this.animator.SetFloat(_paramName, _speed);
			}
			this.SetAnimationSpeed(_speed);
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x0026DA78 File Offset: 0x0026BE78
		public void StartAddWaypoint()
		{
			this.StopAddWaypoint();
			this.addWaypointDisposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).TakeWhile((long _) => base.CurrentState == AnimalState.Locomotion)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.AddWaypoint();
			});
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x0026DAD4 File Offset: 0x0026BED4
		private void AddWaypoint()
		{
			if (3 <= this.pointList.Count)
			{
				return;
			}
			Vector3 b = (!this.pointList.IsNullOrEmpty<Vector3>()) ? this.pointList.Back<Vector3>() : this.Position;
			Vector3 randomMoveAreaPoint = this.GetRandomMoveAreaPoint();
			float num = Vector3.Distance(randomMoveAreaPoint, b);
			if (this.nextPointMaxDistance < num && this.GetDiameter(this.moveSpeed, this.addAngle) < num)
			{
				this.pointList.Add(randomMoveAreaPoint);
			}
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x0026DB59 File Offset: 0x0026BF59
		public void StopAddWaypoint()
		{
			if (this.addWaypointDisposable != null)
			{
				this.addWaypointDisposable.Dispose();
			}
			this.addWaypointDisposable = null;
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x0026DB7C File Offset: 0x0026BF7C
		private void FlyUpdate()
		{
			if (this.IsReached)
			{
				this.TargetPoint = null;
			}
			bool flag = this.TargetPoint != null;
			bool flag2 = this.pointList.IsNullOrEmpty<Vector3>();
			if (!flag && flag2)
			{
				return;
			}
			if (!flag && !flag2)
			{
				this.TargetPoint = new Vector3?(this.pointList.PopFront<Vector3>());
			}
			if (this.TargetPoint == null)
			{
				return;
			}
			Vector3 value = this.TargetPoint.Value;
			float num = Vector3.SignedAngle(base.Forward, value - this.Position, Vector3.up);
			float deltaTime = Time.deltaTime;
			float num2 = Vector3.Distance(value, this.Position);
			float num3 = this.moveSpeed * deltaTime;
			float num4 = this.addAngle * deltaTime;
			num4 = Mathf.Min(Mathf.Abs(num), num4);
			Vector3 vector = Vector3.zero;
			Quaternion rhs = Quaternion.identity;
			rhs = Quaternion.Euler(0f, num4 * Mathf.Sign(num), 0f);
			base.Rotation *= rhs;
			if (Mathf.Abs(num) <= this.turnAngle)
			{
				if (num2 < this.speedDownDistance)
				{
					num3 *= 1f - Mathf.Abs(num) / this.turnAngle;
				}
				Vector3 from = value - this.Position;
				vector = from.normalized * num3;
				num = Vector3.SignedAngle(from, base.Forward, Vector3.up);
				vector = Quaternion.Euler(0f, num, 0f) * vector;
			}
			this.Position += vector;
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x0026DD44 File Offset: 0x0026C144
		public override void OnTimeZoneChanged(EnvironmentSimulator _simulator)
		{
			bool flag = _simulator.Weather == Weather.Rain || _simulator.Weather == Weather.Storm;
			TimeZone timeZone = _simulator.TimeZone;
			if (timeZone != TimeZone.Night)
			{
				if (base.CurrentState == AnimalState.Depop && !flag && !this.forcedDepop)
				{
					this.SetState(AnimalState.Locomotion, null);
				}
			}
			else if (base.CurrentState != AnimalState.Depop)
			{
				this.SetState(AnimalState.Depop, null);
			}
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x0026DDC0 File Offset: 0x0026C1C0
		public override void OnWeatherChanged(EnvironmentSimulator _simulator)
		{
			bool flag = _simulator.TimeZone == TimeZone.Night;
			Weather weather = _simulator.Weather;
			if (weather != Weather.Rain && weather != Weather.Storm)
			{
				if (base.CurrentState == AnimalState.Depop && !flag && !this.forcedDepop)
				{
					this.SetState(AnimalState.Locomotion, null);
				}
			}
			else if (base.CurrentState != AnimalState.Depop)
			{
				this.SetState(AnimalState.Depop, null);
			}
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x0026DE34 File Offset: 0x0026C234
		public void ForcedLocomotion()
		{
			this.forcedDepop = false;
			if (base.CurrentState == AnimalState.Depop)
			{
				this.SetState(AnimalState.Locomotion, null);
			}
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x0026DE51 File Offset: 0x0026C251
		public void ForcedDepop()
		{
			this.forcedDepop = true;
			if (base.CurrentState != AnimalState.Depop && base.CurrentState != AnimalState.Destroyed)
			{
				this.SetState(AnimalState.Depop, null);
			}
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x0026DE7C File Offset: 0x0026C27C
		protected override void OnStart()
		{
			base.PlayParticle();
			bool flag = true;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			this.Active = true;
			this.SetState(AnimalState.Locomotion, null);
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x0026DEAE File Offset: 0x0026C2AE
		protected override void EnterLocomotion()
		{
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			this.StartAddWaypoint();
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x0026DEBF File Offset: 0x0026C2BF
		protected override void OnLocomotion()
		{
			this.FlyUpdate();
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x0026DEC7 File Offset: 0x0026C2C7
		protected override void ExitLocomotion()
		{
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x0026DECC File Offset: 0x0026C2CC
		protected override void EnterDepop()
		{
			if (this.habitatPoint == null || !this.habitatPoint.Available)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			this.pointList.Clear();
			Vector3 position = this.habitatPoint.ViaPoint.position;
			float viaPointRadius = this.habitatPoint.ViaPointRadius;
			Vector3 vector = this.Position;
			vector.y = position.y;
			vector = Vector3.ClampMagnitude(vector - position, viaPointRadius);
			vector = position + vector;
			this.TargetPoint = new Vector3?(vector);
			this.pointList.Add(this.habitatPoint.DepopPoint.position);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x0026DF80 File Offset: 0x0026C380
		protected override void OnDepop()
		{
			if (this.pointList.IsNullOrEmpty<Vector3>() && this.TargetPoint == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			this.FlyUpdate();
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x0026DFC0 File Offset: 0x0026C3C0
		protected override void ExitDepop()
		{
			this.TargetPoint = null;
			this.pointList.Clear();
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x0026DFE7 File Offset: 0x0026C3E7
		public float GetDiameter(float _moveSpeed, float _addAngle)
		{
			if (_moveSpeed == 0f || _addAngle == 0f)
			{
				return float.PositiveInfinity;
			}
			return 360f / (_addAngle / _moveSpeed) / 3.1415927f;
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x0026E014 File Offset: 0x0026C414
		public Vector3 GetRandomMoveAreaPoint()
		{
			if (this.habitatPoint == null || this.habitatPoint.Center == null)
			{
				return Vector3.zero;
			}
			float num = this.habitatPoint.MoveHeight;
			float moveRadius = this.habitatPoint.MoveRadius;
			num = UnityEngine.Random.Range(-num / 2f, num / 2f);
			Vector3 randomPosOnCircle = base.GetRandomPosOnCircle(moveRadius);
			randomPosOnCircle.y = num;
			return this.habitatPoint.Center.position + randomPosOnCircle;
		}

		// Token: 0x040052D0 RID: 21200
		[SerializeField]
		[ReadOnly]
		[HideInEditorMode]
		private ButterflyHabitatPoint habitatPoint;

		// Token: 0x040052D1 RID: 21201
		[SerializeField]
		[Tooltip("移動速度")]
		private float moveSpeed = 1f;

		// Token: 0x040052D2 RID: 21202
		[SerializeField]
		[Tooltip("回転速度")]
		private float addAngle = 90f;

		// Token: 0x040052D3 RID: 21203
		[SerializeField]
		[Tooltip("回転する角度の範囲")]
		private float turnAngle = 170f;

		// Token: 0x040052D4 RID: 21204
		[SerializeField]
		[Tooltip("角度が付きすぎている時,距離がこの距離以内の時減速する")]
		private float speedDownDistance = 2f;

		// Token: 0x040052D5 RID: 21205
		[SerializeField]
		[Tooltip("次のターゲット座標に切り替わる距離")]
		private float changeTargetDistance = 1f;

		// Token: 0x040052D6 RID: 21206
		[SerializeField]
		private float nextPointMaxDistance = 5f;

		// Token: 0x040052D8 RID: 21208
		private List<Vector3> pointList = new List<Vector3>();

		// Token: 0x040052D9 RID: 21209
		private Animation animation;

		// Token: 0x040052DA RID: 21210
		private IDisposable addWaypointDisposable;

		// Token: 0x040052DB RID: 21211
		private bool forcedDepop;
	}
}
