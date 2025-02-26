using System;
using Cinemachine;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace AIProject
{
	// Token: 0x02000C49 RID: 3145
	[Serializable]
	public struct CustomAxisState
	{
		// Token: 0x060061FE RID: 25086 RVA: 0x002900C4 File Offset: 0x0028E4C4
		public CustomAxisState(float minValue_, float maxValue_, bool wrap_, bool rangeLocked, float maxSpeed_, float accelTime_, float decelTime_, ActionID id, string axisName, bool invert)
		{
			this.minValue = minValue_;
			this.maxValue = maxValue_;
			this.wrap = wrap_;
			this.ValueRangeLocked = rangeLocked;
			this.maxSpeed = maxSpeed_;
			this.accelTime = accelTime_;
			this.decelTime = decelTime_;
			this.value = (this.minValue + this.maxValue) / 2f;
			this.inputAxisID = id;
			this.inputAxisName = axisName;
			this.inputAxisValue = 0f;
			this.invertInput = invert;
			this._currentSpeed = 0f;
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x060061FF RID: 25087 RVA: 0x0029014D File Offset: 0x0028E54D
		// (set) Token: 0x06006200 RID: 25088 RVA: 0x00290155 File Offset: 0x0028E555
		internal bool ValueRangeLocked { get; set; }

		// Token: 0x06006201 RID: 25089 RVA: 0x00290160 File Offset: 0x0028E560
		public void Validate()
		{
			this.maxSpeed = Mathf.Max(0f, this.maxSpeed);
			this.accelTime = Mathf.Max(0f, this.accelTime);
			this.decelTime = Mathf.Max(0f, this.decelTime);
			this.maxValue = Mathf.Clamp(this.maxValue, this.minValue, this.maxValue);
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x002901CC File Offset: 0x0028E5CC
		public bool Update(float deltaTime)
		{
			if (Application.isPlaying)
			{
				try
				{
					Manager.Input instance = Singleton<Manager.Input>.Instance;
					if (instance.State == Manager.Input.ValidType.Action)
					{
						float? num = (instance != null) ? new float?(instance.GetAxis(this.inputAxisID)) : null;
						float num2 = (num == null) ? 0f : num.Value;
						float axis = UnityEngine.Input.GetAxis(this.inputAxisName);
						this.inputAxisValue = Mathf.Clamp(num2 + axis, -1f, 1f);
					}
					else
					{
						this.inputAxisValue = 0f;
					}
				}
				catch (ArgumentException ex)
				{
					return false;
				}
			}
			float num3 = this.inputAxisValue;
			if (this.invertInput)
			{
				num3 *= -1f;
			}
			if (this.maxSpeed > 0.0001f)
			{
				float num4 = num3 * this.maxSpeed;
				if (Mathf.Abs(num4) < 0.0001f || (Mathf.Sign(this._currentSpeed) == Mathf.Sign(num4) && Mathf.Abs(num4) < Mathf.Abs(this._currentSpeed)))
				{
					float num5 = Mathf.Abs(num4 - this._currentSpeed) / Mathf.Max(0.0001f, this.decelTime);
					float num6 = Mathf.Min(num5 * deltaTime, Mathf.Abs(this._currentSpeed));
					this._currentSpeed -= Mathf.Sign(this._currentSpeed) * num6;
				}
				else
				{
					float num7 = Mathf.Abs(num4 - this._currentSpeed) / Mathf.Max(0.0001f, this.accelTime);
					this._currentSpeed += Mathf.Sign(num4) * num7 * deltaTime;
					if (Mathf.Sign(this._currentSpeed) == Mathf.Sign(num4) && Mathf.Abs(this._currentSpeed) > Mathf.Abs(num4))
					{
						this._currentSpeed = num4;
					}
				}
			}
			float num8 = this.GetMaxSpeed();
			this._currentSpeed = Mathf.Clamp(this._currentSpeed, -num8, num8);
			this.value += this._currentSpeed * deltaTime;
			bool flag = this.value > this.maxValue || this.value < this.minValue;
			if (flag)
			{
				if (this.wrap)
				{
					if (this.value > this.maxValue)
					{
						this.value = this.minValue + (this.value - this.maxValue);
					}
					else
					{
						this.value = this.maxValue + (this.value - this.minValue);
					}
				}
				else
				{
					this.value = Mathf.Clamp(this.value, this.minValue, this.maxValue);
					this._currentSpeed = 0f;
				}
			}
			return Mathf.Abs(num3) > 0.0001f;
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x002904B8 File Offset: 0x0028E8B8
		private float GetMaxSpeed()
		{
			float num = this.maxValue - this.minValue;
			if (!this.wrap && num > 0f)
			{
				float num2 = num / 10f;
				if (this._currentSpeed > 0f && this.maxValue - this.value < num2)
				{
					float t = this.maxValue - this.value / num2;
					return Mathf.Lerp(0f, this.maxSpeed, t);
				}
				if (this._currentSpeed < 0f && this.value - this.minValue < num2)
				{
					float t2 = (this.value - this.minValue) / num2;
					return Mathf.Lerp(0f, this.maxSpeed, t2);
				}
			}
			return this.maxSpeed;
		}

		// Token: 0x04005696 RID: 22166
		[NoSaveDuringPlay]
		[Tooltip("現在の軸")]
		public float value;

		// Token: 0x04005697 RID: 22167
		[Tooltip("最大速度")]
		public float maxSpeed;

		// Token: 0x04005698 RID: 22168
		[Tooltip("加速時間")]
		public float accelTime;

		// Token: 0x04005699 RID: 22169
		[Tooltip("減速時間")]
		public float decelTime;

		// Token: 0x0400569A RID: 22170
		[FormerlySerializedAs("axisID")]
		[Tooltip("入力軸 (ActionID)")]
		public ActionID inputAxisID;

		// Token: 0x0400569B RID: 22171
		[FormerlySerializedAs("axisName")]
		[Tooltip("入力軸 (Input)")]
		public string inputAxisName;

		// Token: 0x0400569C RID: 22172
		[NoSaveDuringPlay]
		[Tooltip("フレーム内の入力値")]
		public float inputAxisValue;

		// Token: 0x0400569D RID: 22173
		[NoSaveDuringPlay]
		[FormerlySerializedAs("invertAxis")]
		[Tooltip("入力された軸の値を反転するか")]
		public bool invertInput;

		// Token: 0x0400569E RID: 22174
		[Tooltip("最小値")]
		public float minValue;

		// Token: 0x0400569F RID: 22175
		[Tooltip("最大値")]
		public float maxValue;

		// Token: 0x040056A0 RID: 22176
		[Tooltip("最小値・最大値の範囲を超えたらループするか")]
		public bool wrap;

		// Token: 0x040056A1 RID: 22177
		private float _currentSpeed;

		// Token: 0x040056A2 RID: 22178
		private const float Epsilon = 0.0001f;

		// Token: 0x02000C4A RID: 3146
		[Serializable]
		public struct Recentering
		{
			// Token: 0x06006204 RID: 25092 RVA: 0x0029058C File Offset: 0x0028E98C
			public Recentering(bool enabled_, float recenterWaitTime, float recenteringSpeed)
			{
				this.enabled = enabled_;
				this.waitTime = recenterWaitTime;
				this.recenteringTime = recenteringSpeed;
				this._lastAxisInputTime = (this._recenteringVelocity = 0f);
				this._legacyHeadingDefinition = (this._legacyVelocityFilterStrength = 0);
			}

			// Token: 0x06006205 RID: 25093 RVA: 0x002905D2 File Offset: 0x0028E9D2
			public void Validate()
			{
				this.waitTime = Mathf.Max(0f, this.waitTime);
				this.recenteringTime = Mathf.Max(0f, this.recenteringTime);
			}

			// Token: 0x06006206 RID: 25094 RVA: 0x00290600 File Offset: 0x0028EA00
			public void CancelRecentering()
			{
				this._lastAxisInputTime = Time.time;
				this._recenteringVelocity = 0f;
			}

			// Token: 0x06006207 RID: 25095 RVA: 0x00290618 File Offset: 0x0028EA18
			public void DoRecentering(ref CustomAxisState axis, float deltaTime, float recenterTarget)
			{
				if (this.enabled)
				{
					if (deltaTime < 0f)
					{
						this.CancelRecentering();
						axis.value = recenterTarget;
					}
					else if (Time.time > this._lastAxisInputTime + this.waitTime)
					{
						float num = this.recenteringTime / 3f;
						if (num <= deltaTime)
						{
							axis.value = recenterTarget;
						}
						else
						{
							float f = Mathf.DeltaAngle(axis.value, recenterTarget);
							float num2 = Mathf.Abs(f);
							if (num2 < 0.0001f)
							{
								axis.value = recenterTarget;
								this._recenteringVelocity = 0f;
							}
							else
							{
								float num3 = deltaTime / num;
								float num4 = Mathf.Sign(f) * Mathf.Min(num2, num2 * num3);
								float num5 = num4 * this._recenteringVelocity;
								if ((num4 < 0f && num5 < 0f) || (num4 > 0f && num5 > 0f))
								{
									num4 = this._recenteringVelocity + num4 * num3;
								}
								axis.value += num4;
								this._recenteringVelocity = num4;
							}
						}
					}
				}
			}

			// Token: 0x06006208 RID: 25096 RVA: 0x00290730 File Offset: 0x0028EB30
			internal bool LegacyUpdate(ref int heading, ref int velocityFilter)
			{
				bool result;
				if (this._legacyHeadingDefinition != -1 && this._legacyVelocityFilterStrength != -1)
				{
					heading = this._legacyHeadingDefinition;
					velocityFilter = this._legacyVelocityFilterStrength;
					this._legacyHeadingDefinition = (this._legacyVelocityFilterStrength = -1);
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			}

			// Token: 0x040056A4 RID: 22180
			[Tooltip("If checked, will enable automatic recentering of the axis. If unchecked, recenting is disabled.")]
			public bool enabled;

			// Token: 0x040056A5 RID: 22181
			[FormerlySerializedAs("waitTime")]
			[Tooltip("If no user input has been detected on the axis, the axis will wait this long in seconds before recentering.")]
			public float waitTime;

			// Token: 0x040056A6 RID: 22182
			[Tooltip("Maximum angular speed of recentering. Will accelerate into and decelerate out of this.")]
			public float recenteringTime;

			// Token: 0x040056A7 RID: 22183
			private float _lastAxisInputTime;

			// Token: 0x040056A8 RID: 22184
			private float _recenteringVelocity;

			// Token: 0x040056A9 RID: 22185
			[SerializeField]
			[HideInInspector]
			[FormerlySerializedAs("_headingDefinition")]
			private int _legacyHeadingDefinition;

			// Token: 0x040056AA RID: 22186
			[SerializeField]
			[HideInInspector]
			[FormerlySerializedAs("_velocityFilterStrength")]
			private int _legacyVelocityFilterStrength;
		}
	}
}
