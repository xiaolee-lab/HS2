using System;
using System.Runtime.CompilerServices;
using Manager;
using ReMotion;
using UnityEngine;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F1D RID: 3869
	public class FishHitBehaviour : MonoBehaviour
	{
		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06007F3E RID: 32574 RVA: 0x00362664 File Offset: 0x00360A64
		private FishingDefinePack.FishHitParamGroup HitParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.FishParam.HitParam;
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06007F3F RID: 32575 RVA: 0x0036267A File Offset: 0x00360A7A
		private FishingDefinePack.SystemParamGroup SystemParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam;
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06007F40 RID: 32576 RVA: 0x0036268B File Offset: 0x00360A8B
		private float DeltaTime
		{
			[CompilerGenerated]
			get
			{
				return Time.deltaTime;
			}
		}

		// Token: 0x06007F41 RID: 32577 RVA: 0x00362692 File Offset: 0x00360A92
		private void Awake()
		{
			if (!this.fish)
			{
				this.fish = base.GetComponent<Fish>();
			}
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x003626B0 File Offset: 0x00360AB0
		private void ResetAllCounter()
		{
			this.firstCounter = (this.angleCounter = (this.radiusCounter = 0f));
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x003626DC File Offset: 0x00360ADC
		private void ResetAllTimeLimit()
		{
			this.firstTimeLimit = (this.angleTimeLimit = (this.radiusTimeLimit = 0f));
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x00362708 File Offset: 0x00360B08
		private void ResetAllWaitCounter()
		{
			this.angleWaitCounter = (this.positionWaitCounter = (this.radiusWaitCounter = 0f));
		}

		// Token: 0x06007F45 RID: 32581 RVA: 0x00362734 File Offset: 0x00360B34
		private void ResetAllWaitTimeLimit()
		{
			this.angleWaitTimeLimit = (this.positionWaitTimeLimit = (this.radiusWaitTimeLimit = 0f));
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x00362760 File Offset: 0x00360B60
		private void ResetAllWaitFlag()
		{
			this.angleWait = (this.positionWait = (this.radiusWait = true));
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x00362788 File Offset: 0x00360B88
		private void ResetAllActiveFlag()
		{
			this.activeNextAngle = (this.activeNextPosition = (this.activeNextRadius = false));
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x003627B0 File Offset: 0x00360BB0
		private void ResetAllSCNMember()
		{
			this.nextAngle = 0f;
			this.startPosition = (this.nextPosition = Vector3.zero);
			this.startHitMoveAreaRadius = (this.currentHitMoveAreaRadius = (this.nextHitMoveAreaRadius = 0f));
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x003627F9 File Offset: 0x00360BF9
		private void ResetAllOther()
		{
			this.firstHitUseTime = 0f;
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x00362806 File Offset: 0x00360C06
		private void ResetAllMember()
		{
			this.ResetAllCounter();
			this.ResetAllTimeLimit();
			this.ResetAllWaitCounter();
			this.ResetAllWaitTimeLimit();
			this.ResetAllWaitFlag();
			this.ResetAllActiveFlag();
			this.ResetAllSCNMember();
			this.ResetAllOther();
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x00362838 File Offset: 0x00360C38
		public void StartHit()
		{
			this.fishingSystem = this.fish.fishingSystem;
			this.InitializeFirst();
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x00362854 File Offset: 0x00360C54
		private void InitializeFirst()
		{
			this.fishingSystem = this.fish.fishingSystem;
			this.firstHit = true;
			this.ResetAllMember();
			this.currentHitMoveAreaRadius = (this.startHitMoveAreaRadius = (this.nextHitMoveAreaRadius = this.HitParam.MoveAreaMaxRadius));
			float num = this.HitParam.MoveAreaAngle / 2f;
			float d = this.HitParam.MoveAreaMaxRadius + 1f;
			Vector3 forward = this.fishingSystem.HitMoveArea.forward;
			this.nextPosition = this.fishingSystem.HitMoveArea.TransformDirection(Quaternion.Euler(0f, UnityEngine.Random.Range(-num, num), 0f) * Vector3.forward * d) + this.fishingSystem.HitMoveArea.position;
			this.startPosition = base.transform.position;
			float magnitude = (this.nextPosition - this.startPosition).magnitude;
			this.firstHitUseTime = magnitude / this.HitParam.MoveSpeed;
			this.firstTimeLimit = this.firstHitUseTime + this.HitParam.AngleMinTimeLimit;
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x00362984 File Offset: 0x00360D84
		private void ResetAngle()
		{
			float num = UnityEngine.Random.Range(-90f, 90f);
			float num2 = this.Angle360To180(base.transform.localEulerAngles.y);
			float num3 = Mathf.Abs(num - num2);
			if (this.HitParam.NextMinAngle <= num3 && num3 <= this.HitParam.NextMaxAngle)
			{
				this.nextAngle = num;
				this.angleTimeLimit = UnityEngine.Random.Range(this.HitParam.AngleMinTimeLimit, this.HitParam.AngleMaxTimeLimit);
				this.activeNextAngle = true;
			}
		}

		// Token: 0x06007F4E RID: 32590 RVA: 0x00362A15 File Offset: 0x00360E15
		private void ResetPosition()
		{
			this.activeNextPosition = true;
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x00362A20 File Offset: 0x00360E20
		private void ResetRadius()
		{
			this.currentHitMoveAreaRadius = this.nextHitMoveAreaRadius;
			this.startHitMoveAreaRadius = (base.transform.position - this.fishingSystem.HitMoveArea.transform.position).magnitude;
			float num = UnityEngine.Random.Range(this.HitParam.MoveAreaMinRadius, this.HitParam.MoveAreaMaxRadius);
			float num2 = Mathf.Abs(num - this.currentHitMoveAreaRadius);
			if (this.HitParam.NextMinRadius <= num2 && num2 < this.HitParam.NextMaxRadius)
			{
				this.currentHitMoveAreaRadius = this.startHitMoveAreaRadius;
				this.nextHitMoveAreaRadius = num;
				this.radiusTimeLimit = num2 / UnityEngine.Random.Range(this.HitParam.RadiusMinSpeed, this.HitParam.RadiusMaxSpeed);
				this.activeNextRadius = true;
			}
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x00362AF8 File Offset: 0x00360EF8
		public void OnHit()
		{
			this.fishingSystem.HitMoveArea.localPosition = this.HitParam.MoveAreaOffsetPosition;
			if (this.firstHit)
			{
				this.UpdateFirst();
			}
			else
			{
				this.UpdateAngle();
				this.UpdatePosition();
				this.UpdateRadius();
			}
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x00362B48 File Offset: 0x00360F48
		private void UpdateFirst()
		{
			float t = Mathf.InverseLerp(0f, this.firstHitUseTime, Mathf.Clamp(this.firstCounter, 0f, this.firstHitUseTime));
			Vector3 onHitMoveAreaRadius = this.GetOnHitMoveAreaRadius(Vector3.Lerp(this.startPosition, this.nextPosition, t));
			base.transform.position = onHitMoveAreaRadius;
			float deltaTime = Time.deltaTime;
			Vector2 vector = new Vector2(base.transform.forward.x, base.transform.forward.z);
			Vector2 normalized = vector.normalized;
			Vector2 vector2 = new Vector2(this.nextPosition.x - base.transform.position.x, this.nextPosition.z - base.transform.position.z);
			Vector2 normalized2 = vector2.normalized;
			float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
			float num = Mathf.Acos(f) * 57.29578f;
			Vector3 vector3 = Vector3.Cross(new Vector3(normalized.x, 0f, normalized.y), new Vector3(normalized2.x, 0f, normalized2.y));
			if (0f < num)
			{
				float num2 = this.HitParam.FirstAddAngle * Mathf.Sign(vector3.y) * deltaTime;
				if (num <= Mathf.Abs(num2))
				{
					num2 = num * Mathf.Sign(vector3.y);
				}
				Vector3 eulerAngles = base.transform.eulerAngles;
				eulerAngles.x = (eulerAngles.z = 0f);
				eulerAngles.y = this.AngleAbs(eulerAngles.y + num2);
				base.transform.eulerAngles = eulerAngles;
			}
			if (this.firstTimeLimit <= this.firstCounter)
			{
				this.firstHit = false;
				this.radiusWaitTimeLimit = UnityEngine.Random.Range(this.HitParam.RadiusMinWaitTimeLimit, this.HitParam.RadiusMaxWaitTimeLimit);
			}
			else
			{
				this.firstCounter += deltaTime;
			}
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x00362D6C File Offset: 0x0036116C
		private void UpdateAngle()
		{
			float deltaTime = Time.deltaTime;
			if (this.angleWait)
			{
				if (!this.activeNextAngle)
				{
					this.ResetAngle();
				}
				if (this.activeNextAngle && this.angleWaitTimeLimit <= this.angleWaitCounter)
				{
					this.angleWaitCounter = 0f;
					this.angleWait = false;
				}
				else
				{
					this.angleWaitCounter += deltaTime;
				}
			}
			if (!this.angleWait)
			{
				Vector3 vector = Quaternion.Euler(0f, base.transform.localEulerAngles.y, 0f) * Vector3.forward;
				Vector3 vector2 = Quaternion.Euler(0f, this.nextAngle, 0f) * Vector3.forward;
				Vector2 vector3 = new Vector2(vector.x, vector.z);
				Vector2 normalized = vector3.normalized;
				Vector2 vector4 = new Vector2(vector2.x, vector2.z);
				Vector2 normalized2 = vector4.normalized;
				float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
				float num = Mathf.Acos(f) * 57.29578f;
				Vector3 vector5 = Vector3.Cross(new Vector3(normalized.x, 0f, normalized.y), new Vector3(normalized2.x, 0f, normalized2.y));
				float num2 = this.HitParam.AddAngle * Mathf.Sign(vector5.y) * deltaTime;
				if (num <= Mathf.Abs(num2))
				{
					num2 = num * Mathf.Sign(vector5.y);
				}
				Vector3 localEulerAngles = base.transform.localEulerAngles;
				localEulerAngles.x = (localEulerAngles.z = 0f);
				localEulerAngles.y = this.AngleAbs(localEulerAngles.y + num2);
				base.transform.localEulerAngles = localEulerAngles;
				if (this.angleTimeLimit <= this.angleCounter)
				{
					this.angleWait = true;
					this.activeNextAngle = false;
					this.angleCounter = 0f;
				}
				else
				{
					this.angleCounter += deltaTime;
				}
			}
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x00362F94 File Offset: 0x00361394
		private void UpdatePosition()
		{
			float deltaTime = Time.deltaTime;
			if (this.positionWait)
			{
				if (!this.activeNextPosition)
				{
					this.ResetPosition();
				}
				if (this.activeNextPosition && this.positionWaitTimeLimit <= this.positionWaitCounter)
				{
					this.positionWaitCounter = 0f;
					this.positionWait = false;
				}
				else
				{
					this.positionWaitCounter += deltaTime;
				}
			}
			if (!this.positionWait)
			{
				float num = this.HitParam.MoveSpeed * deltaTime;
				Vector3 b = base.transform.forward * num;
				Vector3 vector = base.transform.position + b;
				vector = this.GetOnHitMoveAreaAngle(vector);
				Vector3 vector2 = base.transform.position - this.fishingSystem.HitMoveArea.position;
				Vector3 vector3 = vector - this.fishingSystem.HitMoveArea.position;
				float magnitude = vector2.magnitude;
				float magnitude2 = vector3.magnitude;
				float num2 = this.currentHitMoveAreaRadius;
				if (0f <= num2 - magnitude)
				{
					vector = this.GetOnHitMoveAreaRadius(vector);
				}
				else if (0f <= magnitude2 - magnitude)
				{
					vector = vector3.normalized * (magnitude - num) + this.fishingSystem.HitMoveArea.position;
					magnitude2 = (vector - this.fishingSystem.HitMoveArea.position).magnitude;
					if (0f <= num2 - magnitude2)
					{
						vector = (vector - this.fishingSystem.HitMoveArea.position).normalized * num2 + this.fishingSystem.HitMoveArea.position;
					}
				}
				base.transform.position = vector;
			}
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x00363168 File Offset: 0x00361568
		private void UpdateRadius()
		{
			float deltaTime = Time.deltaTime;
			if (this.radiusWait)
			{
				if (!this.activeNextRadius)
				{
					this.ResetRadius();
				}
				if (this.activeNextRadius && this.radiusWaitTimeLimit <= this.radiusWaitCounter)
				{
					this.radiusWaitCounter = 0f;
					this.radiusWait = false;
				}
				else
				{
					this.radiusWaitCounter += deltaTime;
				}
			}
			if (!this.radiusWait)
			{
				float num = Mathf.InverseLerp(0f, this.radiusTimeLimit, Mathf.Clamp(this.radiusCounter, 0f, this.radiusTimeLimit));
				if (this.HitParam.ChangeRadiusEasing)
				{
					num = EasingFunctions.EaseOutQuart(num, 1f);
				}
				this.currentHitMoveAreaRadius = Mathf.Lerp(this.startHitMoveAreaRadius, this.nextHitMoveAreaRadius, num);
				if (this.radiusTimeLimit <= this.radiusCounter)
				{
					this.radiusWait = true;
					this.radiusCounter = 0f;
					this.currentHitMoveAreaRadius = this.nextHitMoveAreaRadius;
					this.radiusWaitTimeLimit = UnityEngine.Random.Range(this.HitParam.RadiusMinWaitTimeLimit, this.HitParam.RadiusMaxWaitTimeLimit);
					this.activeNextRadius = false;
				}
				else
				{
					this.radiusCounter += deltaTime;
				}
			}
		}

		// Token: 0x06007F55 RID: 32597 RVA: 0x003632B0 File Offset: 0x003616B0
		public bool CheckOnHitMoveAreaInWorld(Vector3 _checkPos)
		{
			return (_checkPos - this.fishingSystem.HitMoveArea.position).sqrMagnitude <= this.currentHitMoveAreaRadius * this.currentHitMoveAreaRadius;
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x003632F0 File Offset: 0x003616F0
		private Vector3 GetOnHitMoveAreaRadius(Vector3 _checkPos)
		{
			if (!this.CheckOnHitMoveAreaInWorld(_checkPos))
			{
				Vector3 vector = Vector3.ClampMagnitude(_checkPos - this.fishingSystem.HitMoveArea.position, this.currentHitMoveAreaRadius) + this.fishingSystem.HitMoveArea.position;
				Vector3 vector2 = _checkPos - vector;
				float num = this.HitParam.MoveSpeed * Time.deltaTime;
				_checkPos = ((num >= vector2.magnitude) ? vector : (_checkPos - vector2.normalized * num));
			}
			return _checkPos;
		}

		// Token: 0x06007F57 RID: 32599 RVA: 0x00363384 File Offset: 0x00361784
		private Vector3 GetOnHitMoveAreaAngle(Vector3 _checkPos)
		{
			Transform hitMoveArea = this.fishingSystem.HitMoveArea;
			Vector2 vector = new Vector2(hitMoveArea.forward.x, hitMoveArea.forward.z);
			Vector2 normalized = vector.normalized;
			Vector2 vector2 = new Vector2(_checkPos.x - hitMoveArea.position.x, _checkPos.z - hitMoveArea.position.z);
			Vector2 normalized2 = vector2.normalized;
			float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
			float num = Mathf.Acos(f) * 57.29578f;
			Vector3 vector3 = Vector3.Cross(new Vector3(normalized.x, 0f, normalized.y), new Vector3(normalized2.x, 0f, normalized2.y));
			if (this.HitParam.MoveAreaAngle * 0.5f < num)
			{
				_checkPos = hitMoveArea.TransformDirection(Quaternion.Euler(0f, this.HitParam.MoveAreaAngle * Mathf.Sign(vector3.y) * 0.5f, 0f) * Vector3.forward * vector2.magnitude) + hitMoveArea.position;
			}
			return _checkPos;
		}

		// Token: 0x06007F58 RID: 32600 RVA: 0x003634D5 File Offset: 0x003618D5
		private float AngleAbs(float _angle)
		{
			if (_angle < 0f)
			{
				_angle = _angle % 360f + 360f;
			}
			else if (_angle > 360f)
			{
				_angle %= 360f;
			}
			return _angle;
		}

		// Token: 0x06007F59 RID: 32601 RVA: 0x0036350B File Offset: 0x0036190B
		private float Angle360To180(float _angle)
		{
			_angle = this.AngleAbs(_angle);
			if (180f < _angle)
			{
				_angle -= 360f;
			}
			return _angle;
		}

		// Token: 0x04006672 RID: 26226
		[SerializeField]
		private Fish fish;

		// Token: 0x04006673 RID: 26227
		private FishingManager fishingSystem;

		// Token: 0x04006674 RID: 26228
		private bool firstHit = true;

		// Token: 0x04006675 RID: 26229
		private float firstCounter;

		// Token: 0x04006676 RID: 26230
		private float firstTimeLimit;

		// Token: 0x04006677 RID: 26231
		private float angleCounter;

		// Token: 0x04006678 RID: 26232
		private float angleTimeLimit = 0.5f;

		// Token: 0x04006679 RID: 26233
		private float radiusCounter;

		// Token: 0x0400667A RID: 26234
		private float radiusTimeLimit;

		// Token: 0x0400667B RID: 26235
		private bool angleWait = true;

		// Token: 0x0400667C RID: 26236
		private bool positionWait = true;

		// Token: 0x0400667D RID: 26237
		private bool radiusWait = true;

		// Token: 0x0400667E RID: 26238
		private float angleWaitCounter;

		// Token: 0x0400667F RID: 26239
		private float angleWaitTimeLimit;

		// Token: 0x04006680 RID: 26240
		private float positionWaitCounter;

		// Token: 0x04006681 RID: 26241
		private float positionWaitTimeLimit;

		// Token: 0x04006682 RID: 26242
		private float radiusWaitCounter;

		// Token: 0x04006683 RID: 26243
		private float radiusWaitTimeLimit;

		// Token: 0x04006684 RID: 26244
		private bool activeNextAngle;

		// Token: 0x04006685 RID: 26245
		private bool activeNextPosition;

		// Token: 0x04006686 RID: 26246
		private bool activeNextRadius;

		// Token: 0x04006687 RID: 26247
		private float nextAngle;

		// Token: 0x04006688 RID: 26248
		private Vector3 startPosition = Vector3.zero;

		// Token: 0x04006689 RID: 26249
		private Vector3 nextPosition = Vector3.zero;

		// Token: 0x0400668A RID: 26250
		private float startHitMoveAreaRadius;

		// Token: 0x0400668B RID: 26251
		private float currentHitMoveAreaRadius;

		// Token: 0x0400668C RID: 26252
		private float nextHitMoveAreaRadius;

		// Token: 0x0400668D RID: 26253
		private float firstHitUseTime;
	}
}
