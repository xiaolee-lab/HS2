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
	// Token: 0x02000B44 RID: 2884
	public class AloneButterfly : SerializedMonoBehaviour
	{
		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x00253106 File Offset: 0x00251506
		// (set) Token: 0x06005458 RID: 21592 RVA: 0x0025310E File Offset: 0x0025150E
		public AloneButterflyHabitatPoint _habitatPoint { get; set; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06005459 RID: 21593 RVA: 0x00253118 File Offset: 0x00251518
		public bool IsReached
		{
			[CompilerGenerated]
			get
			{
				return this.TargetPoint == null || Vector3.Distance(this.Position, this._habitatPoint.Center.position + this.TargetPoint.Value) <= this._habitatPoint.ChangeTargetDistance;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x00253176 File Offset: 0x00251576
		// (set) Token: 0x0600545B RID: 21595 RVA: 0x00253183 File Offset: 0x00251583
		public Vector3 Position
		{
			get
			{
				return base.transform.position;
			}
			set
			{
				base.transform.position = value;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x0600545C RID: 21596 RVA: 0x00253191 File Offset: 0x00251591
		// (set) Token: 0x0600545D RID: 21597 RVA: 0x0025319E File Offset: 0x0025159E
		public Quaternion Rotation
		{
			get
			{
				return base.transform.rotation;
			}
			set
			{
				base.transform.rotation = value;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x0600545E RID: 21598 RVA: 0x002531AC File Offset: 0x002515AC
		public Vector3 Forward
		{
			[CompilerGenerated]
			get
			{
				return base.transform.forward;
			}
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x002531BC File Offset: 0x002515BC
		public void SetAnimationSpeed(float speed)
		{
			if (this._animation != null && this._animation.isActiveAndEnabled)
			{
				IEnumerator enumerator = this._animation.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						AnimationState animationState = (AnimationState)obj;
						animationState.speed = speed;
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
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x00253244 File Offset: 0x00251644
		public void Initialize(AloneButterflyHabitatPoint habitatPoint, GameObject prefabObj)
		{
			if (habitatPoint == null)
			{
				return;
			}
			this._habitatPoint = habitatPoint;
			float num = UnityEngine.Random.Range(0f, this._habitatPoint.MaxDelayTime);
			Observable.Timer(TimeSpan.FromSeconds((double)num)).TakeUntilDestroy(habitatPoint).TakeUntilDestroy(this).Subscribe(delegate(long _)
			{
				this._bodyObject = UnityEngine.Object.Instantiate<GameObject>(prefabObj, this.transform);
				Transform transform = this._bodyObject.transform;
				Vector3 zero = Vector3.zero;
				this._bodyObject.transform.localEulerAngles = zero;
				transform.localPosition = zero;
				this._bodyObject.SetActive(true);
				AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
				float animationSpeed = (!(animalDefinePack != null)) ? 1f : animalDefinePack.AnimatorInfo.ButterflyAnimationSpeed;
				this._animation = this.GetComponentInChildren<Animation>(true);
				this.SetAnimationSpeed(animationSpeed);
				this.Position = this._habitatPoint.Center.position + this.GetRandomMoveAreaPoint();
				(from __ in Observable.EveryUpdate().TakeUntilDestroy(this._habitatPoint).TakeUntilDestroy(this)
				where this.isActiveAndEnabled
				select __).Subscribe(delegate(long __)
				{
					this.AddWaypoint();
					this.OnUpdate();
				});
			});
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x002532B9 File Offset: 0x002516B9
		public float GetDiameter(float moveSpeed, float addAngle)
		{
			if (moveSpeed == 0f || addAngle == 0f)
			{
				return float.PositiveInfinity;
			}
			return 360f / (addAngle / moveSpeed) / 3.1415927f;
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x002532E8 File Offset: 0x002516E8
		protected Vector3 GetRandomPosOnCircle(float radius)
		{
			float f = UnityEngine.Random.value * 3.1415927f * 2f;
			float num = radius * Mathf.Sqrt(UnityEngine.Random.value);
			return new Vector3(num * Mathf.Cos(f), 0f, num * Mathf.Sin(f));
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x00253330 File Offset: 0x00251730
		private Vector3 GetRandomMoveAreaPoint()
		{
			if (this._habitatPoint == null || this._habitatPoint.Center == null)
			{
				return Vector3.zero;
			}
			float num = this._habitatPoint.MoveHeight;
			float moveRadius = this._habitatPoint.MoveRadius;
			num = UnityEngine.Random.Range(-num / 2f, num / 2f);
			Vector3 randomPosOnCircle = this.GetRandomPosOnCircle(moveRadius);
			randomPosOnCircle.y = num;
			return randomPosOnCircle;
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x002533A8 File Offset: 0x002517A8
		private void AddWaypoint()
		{
			if (3 <= this._pointList.Count)
			{
				return;
			}
			Vector3 b = (!this._pointList.IsNullOrEmpty<Vector3>()) ? (this._habitatPoint.Center.position + this._pointList.Back<Vector3>()) : this.Position;
			Vector3 randomMoveAreaPoint = this.GetRandomMoveAreaPoint();
			Vector3 a = this._habitatPoint.Center.position + randomMoveAreaPoint;
			float num = Vector3.Distance(a, b);
			if (this._habitatPoint.NextPointMaxDistance < num && this.GetDiameter(this._habitatPoint.MoveSpeed, this._habitatPoint.AddAngle) < num)
			{
				this._pointList.Add(randomMoveAreaPoint);
			}
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x00253468 File Offset: 0x00251868
		private void OnUpdate()
		{
			if (this.IsReached)
			{
				this.TargetPoint = null;
			}
			bool flag = this.TargetPoint != null;
			bool flag2 = this._pointList.IsNullOrEmpty<Vector3>();
			if (!flag && flag2)
			{
				return;
			}
			if (!flag && !flag2)
			{
				this.TargetPoint = new Vector3?(this._pointList.PopFront<Vector3>());
			}
			if (this.TargetPoint == null)
			{
				return;
			}
			Vector3 a = this._habitatPoint.Center.position + this.TargetPoint.Value;
			float num = Vector3.SignedAngle(this.Forward, a - this.Position, Vector3.up);
			float deltaTime = Time.deltaTime;
			float num2 = Vector3.Distance(a, this.Position);
			float num3 = this._habitatPoint.MoveSpeed * deltaTime;
			float num4 = this._habitatPoint.AddAngle * deltaTime;
			num4 = Mathf.Min(Mathf.Abs(num), num4);
			Vector3 vector = Vector3.zero;
			Quaternion rhs = Quaternion.identity;
			rhs = Quaternion.Euler(0f, num4 * Mathf.Sign(num), 0f);
			this.Rotation *= rhs;
			if (Mathf.Abs(num) <= this._habitatPoint.TurnAngle)
			{
				if (num2 < this._habitatPoint.SpeedDownDistance)
				{
					num3 *= 1f - Mathf.Abs(num) / this._habitatPoint.TurnAngle;
				}
				Vector3 from = a - this.Position;
				vector = from.normalized * num3;
				num = Vector3.SignedAngle(from, this.Forward, Vector3.up);
				vector = Quaternion.Euler(0f, num, 0f) * vector;
			}
			this.Position += vector;
		}

		// Token: 0x04004F30 RID: 20272
		private List<Vector3> _pointList = new List<Vector3>();

		// Token: 0x04004F31 RID: 20273
		private Vector3? TargetPoint;

		// Token: 0x04004F32 RID: 20274
		private Animation _animation;

		// Token: 0x04004F33 RID: 20275
		private GameObject _bodyObject;
	}
}
