using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F3E RID: 3902
	public class Lure : MonoBehaviour
	{
		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x06008120 RID: 33056 RVA: 0x0036C26D File Offset: 0x0036A66D
		// (set) Token: 0x06008121 RID: 33057 RVA: 0x0036C278 File Offset: 0x0036A678
		public Lure.State state
		{
			get
			{
				return this.state_;
			}
			set
			{
				if (this.state_ == value)
				{
					return;
				}
				Lure.State state = this.state_;
				if (state == Lure.State.Float)
				{
					this.StopFloatParticle();
					this.floatingObject.UseWaterBuoyancy = false;
				}
				this.state_ = value;
				Lure.State state2 = this.state_;
				if (state2 == Lure.State.Float)
				{
					this.prevMouseMove = (this.movePower = Vector2.zero);
					this.PlayFloatParticle();
					this.floatingObject.UseWaterBuoyancy = true;
				}
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x06008122 RID: 33058 RVA: 0x0036C301 File Offset: 0x0036A701
		// (set) Token: 0x06008123 RID: 33059 RVA: 0x0036C309 File Offset: 0x0036A709
		public Transform RootPos { get; private set; }

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06008124 RID: 33060 RVA: 0x0036C312 File Offset: 0x0036A712
		// (set) Token: 0x06008125 RID: 33061 RVA: 0x0036C31C File Offset: 0x0036A71C
		public Fish HitFish
		{
			get
			{
				return this.hitFish_;
			}
			set
			{
				this.hitFish_ = value;
				if (value != null)
				{
					this.hitFishInfo = value.fishInfo;
				}
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06008126 RID: 33062 RVA: 0x0036C34A File Offset: 0x0036A74A
		private FishingDefinePack.LureParamGroup Param
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.LureParam;
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06008127 RID: 33063 RVA: 0x0036C35B File Offset: 0x0036A75B
		private FishingDefinePack.SystemParamGroup SystemParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam;
			}
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x0036C36C File Offset: 0x0036A76C
		private void Awake()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			(from _ in Observable.EveryLateUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdate();
			});
		}

		// Token: 0x06008129 RID: 33065 RVA: 0x0036C3E0 File Offset: 0x0036A7E0
		public void Initialize(FishingManager _fishingManager, Transform _rootPos, LineRenderer _line)
		{
			if (this.bodyObject == null)
			{
				int lureEventItemID = Singleton<Manager.Resources>.Instance.FishingDefinePack.IDInfo.LureEventItemID;
				ActionItemInfo actionItemInfo;
				GameObject original;
				if (Singleton<Manager.Resources>.Instance.Map.EventItemList.TryGetValue(lureEventItemID, out actionItemInfo) && (original = CommonLib.LoadAsset<GameObject>(actionItemInfo.assetbundleInfo.assetbundle, actionItemInfo.assetbundleInfo.asset, false, actionItemInfo.assetbundleInfo.manifest)) != null)
				{
					MapScene.AddAssetBundlePath(actionItemInfo.assetbundleInfo.assetbundle, actionItemInfo.assetbundleInfo.manifest);
					this.bodyObject = UnityEngine.Object.Instantiate<GameObject>(original, Vector3.zero, Quaternion.identity);
					this.bodyObject.transform.SetParent(base.transform, false);
					this.floatingObject = this.bodyObject.GetOrAddComponent<FloatingObject>();
					this.floatingObject.fishingSystem = _fishingManager;
					this.floatingObject.WaterEnterChecker = new Func<Collider, bool>(this.WaterInOutCheck);
					this.floatingObject.WaterStayChecker = new Func<Collider, bool>(this.WaterInOutCheck);
					this.floatingObject.WaterExitChecker = new Func<Collider, bool>(this.WaterInOutCheck);
				}
			}
			this.fishingSystem = _fishingManager;
			this.RootPos = _rootPos;
			base.transform.parent = this.RootPos;
			Transform transform = base.transform;
			Vector3 zero = Vector3.zero;
			base.transform.localEulerAngles = zero;
			transform.localPosition = zero;
			this.fishingLine = _line;
			this.state = Lure.State.Have;
			if (this.floatingObject)
			{
				this.floatingObject.UseWaterBuoyancy = false;
			}
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x0036C574 File Offset: 0x0036A974
		private bool WaterInOutCheck(Collider other)
		{
			return other.CompareTag(this.SystemParam.LureWaterBoxTagName) && 1 << other.gameObject.layer == this.SystemParam.LureWaterBoxLayerMask && this.fishingSystem.WaterBox == other;
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x0036C5D0 File Offset: 0x0036A9D0
		private void OnUpdate()
		{
			Lure.State state = this.state;
			if (state != Lure.State.Have)
			{
				if (state == Lure.State.Float)
				{
					this.Float();
				}
			}
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x0036C60C File Offset: 0x0036AA0C
		private void OnLateUpdate()
		{
			Lure.State state = this.state;
			if (state == Lure.State.Hit)
			{
				if (this.HitFish == null)
				{
					this.state = Lure.State.None;
					return;
				}
				base.transform.position = this.HitFish.transform.position;
			}
		}

		// Token: 0x0600812D RID: 33069 RVA: 0x0036C668 File Offset: 0x0036AA68
		private void LateUpdate()
		{
			if (this.fishingLine != null && this.fishingLine.enabled)
			{
				Transform transform = (!(this.bodyObject != null)) ? base.transform : this.bodyObject.transform;
				this.fishingLine.SetPosition(0, transform.position);
				this.fishingLine.SetPosition(1, this.RootPos.position);
			}
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x0036C6E8 File Offset: 0x0036AAE8
		private void Float()
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			Vector2 a = instance.LeftStickAxis;
			Vector2 mouseAxis = instance.MouseAxis;
			Vector3 vector = base.transform.localPosition;
			if (0f < a.sqrMagnitude)
			{
				a *= this.Param.FloatMoveMaxSpeed;
				Vector2 a2 = a - this.movePower;
				this.movePower += a2 * 0.5f;
			}
			else if (0f < mouseAxis.sqrMagnitude || 0f < this.prevMouseMove.sqrMagnitude)
			{
				Vector2 a3 = (mouseAxis + this.prevMouseMove) / 2f;
				a3 = Vector2.ClampMagnitude(a3 * this.Param.MouseAxisScale, this.Param.FloatMoveMaxSpeed);
				Vector2 a4 = a3 - this.movePower;
				this.movePower += a4 * 0.5f;
			}
			else
			{
				this.movePower += this.movePower * -0.5f;
			}
			Vector3 localEulerAngles = this.bodyObject.transform.localEulerAngles;
			float num = 60f;
			localEulerAngles.x = Mathf.Abs(this.movePower.y) / this.Param.FloatMoveMaxSpeed * num * Mathf.Sign(this.movePower.y);
			localEulerAngles.z = Mathf.Abs(this.movePower.x) / this.Param.FloatMoveMaxSpeed * num * Mathf.Sign(-this.movePower.x);
			this.bodyObject.transform.localEulerAngles = localEulerAngles;
			vector.x += this.movePower.x * Time.deltaTime;
			vector.z += this.movePower.y * Time.deltaTime;
			this.prevMouseMove = mouseAxis;
			bool flag = this.fishingSystem.CheckOnMoveAreaInLocal(base.transform.localPosition);
			if (flag)
			{
				vector = this.fishingSystem.ClampMoveAreaInLocal(vector);
				base.transform.localPosition = vector;
			}
			else if (!flag)
			{
				Vector3 position = base.transform.position;
				Vector3 vector2 = position - this.fishingSystem.MoveArea.transform.position;
				Vector3 b = Vector3.ClampMagnitude(vector2, this.SystemParam.MoveAreaRadius);
				Vector3 vector3 = vector2 - b;
				Vector3 vector4 = new Vector3(vector.x, 0f, vector.z);
				Vector3 normalized = vector4.normalized;
				Vector3 b2 = normalized * this.Param.FloatMoveMaxSpeed * Time.deltaTime;
				if (b2.sqrMagnitude < vector3.sqrMagnitude)
				{
					vector -= b2;
				}
				else
				{
					vector = normalized * this.SystemParam.MoveAreaRadius;
				}
				base.transform.localPosition = vector;
			}
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x0036CA0A File Offset: 0x0036AE0A
		public void StartThrow()
		{
			base.StartCoroutine(this.Throw());
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x0036CA1C File Offset: 0x0036AE1C
		private Vector3 GetThrowPosition()
		{
			Transform transform = this.fishingSystem.MoveArea.transform;
			Vector3 vector = transform.position;
			vector += transform.TransformPoint(this.Param.DropOffsetPosition) - vector;
			vector.y = transform.position.y;
			vector = this.fishingSystem.ClampMoveAreaInWorld(vector);
			Collider collider = null;
			this.floatingObject.waterCollider = this.fishingSystem.WaterBox;
			if (FishingManager.CheckOnWater(vector, ref collider))
			{
				return vector;
			}
			vector = transform.position;
			Vector3 a = transform.forward * (-this.SystemParam.MoveAreaRadius - 0.1f) + vector;
			Vector3 b = transform.forward * (this.SystemParam.MoveAreaRadius - 0.1f) + vector;
			a.y = (b.y = vector.y);
			for (int i = 1; i <= 4; i++)
			{
				for (int j = -1; j <= 1; j += 2)
				{
					float t = Mathf.Clamp((float)(i * j) / 10f + 0.5f, 0f, 1f);
					Vector3 vector2 = Vector3.Lerp(a, b, t);
					vector2 = this.fishingSystem.ClampMoveAreaInWorld(vector2);
					if (FishingManager.CheckOnWater(vector2, ref collider))
					{
						return vector2;
					}
				}
			}
			return vector;
		}

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06008131 RID: 33073 RVA: 0x0036CB8F File Offset: 0x0036AF8F
		private bool IsFloating
		{
			[CompilerGenerated]
			get
			{
				return this.fishingSystem.scene == FishingManager.FishingScene.StartMotion || this.fishingSystem.scene == FishingManager.FishingScene.WaitHit;
			}
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06008132 RID: 33074 RVA: 0x0036CBB3 File Offset: 0x0036AFB3
		private Transform ParticleRoot
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x0036CBBB File Offset: 0x0036AFBB
		private void PlayFloatParticle()
		{
			this.fishingSystem.PlayParticle(ParticleType.LureRippleS, this.ParticleRoot);
			this.fishingSystem.PlayParticle(ParticleType.LureRippleM, this.ParticleRoot);
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x0036CBE3 File Offset: 0x0036AFE3
		private void StopFloatParticle()
		{
			this.fishingSystem.StopParticle(ParticleType.LureRippleS, this.ParticleRoot, ParticleSystemStopBehavior.StopEmittingAndClear);
			this.fishingSystem.StopParticle(ParticleType.LureRippleM, this.ParticleRoot, ParticleSystemStopBehavior.StopEmittingAndClear);
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x0036CC0B File Offset: 0x0036B00B
		private void PlayDelayFloatParticle(float _delayTime)
		{
			this.fishingSystem.PlayDelayParticle(ParticleType.LureRippleS, this.ParticleRoot, () => this.IsFloating, _delayTime);
			this.fishingSystem.PlayDelayParticle(ParticleType.LureRippleM, this.ParticleRoot, () => this.IsFloating, _delayTime);
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x0036CC4C File Offset: 0x0036B04C
		private IEnumerator Throw()
		{
			if (this.state == Lure.State.Throw)
			{
				yield break;
			}
			this.state = Lure.State.Throw;
			yield return this.waitForEndOfFrame;
			Vector3 _center = this.GetThrowPosition();
			base.transform.parent = this.fishingSystem.MoveArea.transform;
			Quaternion _startAngle = base.transform.localRotation;
			this.beziPos[0] = base.transform.position;
			this.beziPos[1].Set(_center.x, base.transform.position.y, _center.z);
			this.beziPos[2] = _center;
			Vector3 _startPosition = base.transform.position;
			Vector3 _endPosition = _center;
			_endPosition.y += this.Param.DropOffsetHeight;
			Vector3 _position = _startPosition;
			this.fishingLine.enabled = true;
			Vector3 position;
			for (float time = 0f; time <= this.Param.ThrowTime; time += Time.deltaTime)
			{
				float t = Mathf.InverseLerp(0f, this.Param.ThrowTime, time);
				float ty = EasingFunctions.EaseInQuad(time, this.Param.ThrowTime);
				_position.x = Mathf.Lerp(_startPosition.x, _endPosition.x, t);
				_position.y = Mathf.Lerp(_startPosition.y, _endPosition.y, ty);
				_position.z = Mathf.Lerp(_startPosition.z, _endPosition.z, t);
				LineRenderer lineRenderer = this.fishingLine;
				int index = 0;
				position = _position;
				base.transform.position = position;
				lineRenderer.SetPosition(index, position);
				base.transform.localRotation = Quaternion.Lerp(_startAngle, Quaternion.identity, t);
				yield return this.waitForEndOfFrame;
			}
			LineRenderer lineRenderer2 = this.fishingLine;
			int index2 = 0;
			position = _center;
			base.transform.position = position;
			lineRenderer2.SetPosition(index2, position);
			base.transform.localEulerAngles = Vector3.zero;
			this.fishingSystem.PlaySE(SEType.LureDrop, base.transform, false, 0f);
			this.fishingSystem.PlayParticle(ParticleType.LureInOut, base.transform);
			float _delayTime = 2.5f;
			this.PlayDelayFloatParticle(_delayTime);
			this.floatingObject.UseWaterBuoyancy = true;
			this.state = Lure.State.None;
			if (Lure.ThrowEndEvent != null)
			{
				Lure.ThrowEndEvent();
			}
			yield break;
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x0036CC67 File Offset: 0x0036B067
		public void StartReturn(ParticleType _particleType)
		{
			if (this.state != Lure.State.Return)
			{
				base.StartCoroutine(this.Return(_particleType));
			}
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x0036CC84 File Offset: 0x0036B084
		private IEnumerator Return(ParticleType _particleType)
		{
			if (this.state == Lure.State.Return)
			{
				yield break;
			}
			this.state = Lure.State.Return;
			this.floatingObject.UseWaterBuoyancy = false;
			yield return this.waitForEndOfFrame;
			Transform transform = this.bodyObject.transform;
			Vector3 vector = Vector3.zero;
			this.bodyObject.transform.localEulerAngles = vector;
			transform.localPosition = vector;
			float _scale = 1f;
			if (_particleType == ParticleType.FishGet && !Singleton<Manager.Resources>.Instance.FishingDefinePack.GetEfcScaleTable.TryGetValue(this.hitFishInfo.SizeID, out _scale))
			{
				_scale = 1f;
			}
			ParticleSystem _efc = this.fishingSystem.PlayParticle(_particleType, base.transform);
			if (_efc != null && _particleType == ParticleType.FishGet)
			{
				_efc.transform.localScale = Vector3.one * _scale;
			}
			Vector3 _startPosition = base.transform.position;
			Vector3 _position = Vector3.zero;
			for (float time = 0f; time <= this.Param.ReturnTime; time += Time.deltaTime)
			{
				float t = Mathf.InverseLerp(0f, this.Param.ReturnTime, time);
				_position = Vector3.Lerp(_startPosition, this.RootPos.position, t);
				LineRenderer lineRenderer = this.fishingLine;
				int index = 0;
				vector = _position;
				base.transform.position = vector;
				lineRenderer.SetPosition(index, vector);
				yield return this.waitForEndOfFrame;
			}
			base.transform.position = this.Bezier(this.beziPos[0], this.beziPos[1], this.RootPos.position, 1f);
			base.transform.parent = this.RootPos;
			this.fishingLine.enabled = false;
			Transform transform2 = base.transform;
			vector = Vector3.zero;
			base.transform.localEulerAngles = vector;
			transform2.localPosition = vector;
			this.state = Lure.State.Have;
			yield break;
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x0036CCA6 File Offset: 0x0036B0A6
		private void OnDestroy()
		{
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x0036CCA8 File Offset: 0x0036B0A8
		private Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			float num = 1f - t;
			return num * num * p0 + 2f * num * t * p1 + t * t * p2;
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x0036CCEC File Offset: 0x0036B0EC
		private Vector3 Bezier(Vector3[] p, float t)
		{
			if (p.IsNullOrEmpty<Vector3>() || p.Length < 3)
			{
				return Vector3.zero;
			}
			float num = 1f - t;
			return num * num * p[0] + 2f * num * t * p[1] + t * t * p[2];
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x0036CD67 File Offset: 0x0036B167
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

		// Token: 0x040067C0 RID: 26560
		private Lure.State state_ = Lure.State.Have;

		// Token: 0x040067C2 RID: 26562
		private Fish hitFish_;

		// Token: 0x040067C3 RID: 26563
		private FishInfo hitFishInfo = default(FishInfo);

		// Token: 0x040067C4 RID: 26564
		private Vector3[] beziPos = new Vector3[]
		{
			default(Vector3),
			default(Vector3),
			default(Vector3)
		};

		// Token: 0x040067C5 RID: 26565
		private FishingManager fishingSystem;

		// Token: 0x040067C6 RID: 26566
		private LineRenderer fishingLine;

		// Token: 0x040067C7 RID: 26567
		public static Action ThrowEndEvent;

		// Token: 0x040067C8 RID: 26568
		private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

		// Token: 0x040067C9 RID: 26569
		private GameObject bodyObject;

		// Token: 0x040067CA RID: 26570
		private FloatingObject floatingObject;

		// Token: 0x040067CB RID: 26571
		private Vector2 movePower = Vector2.zero;

		// Token: 0x040067CC RID: 26572
		private Vector2 prevMouseMove = Vector2.zero;

		// Token: 0x02000F3F RID: 3903
		public enum State
		{
			// Token: 0x040067CE RID: 26574
			None,
			// Token: 0x040067CF RID: 26575
			Have,
			// Token: 0x040067D0 RID: 26576
			Throw,
			// Token: 0x040067D1 RID: 26577
			Float,
			// Token: 0x040067D2 RID: 26578
			Hit,
			// Token: 0x040067D3 RID: 26579
			Return
		}
	}
}
