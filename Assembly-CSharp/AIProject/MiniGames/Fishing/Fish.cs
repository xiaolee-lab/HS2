using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F1A RID: 3866
	public class Fish : MonoBehaviour
	{
		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x06007EE5 RID: 32485 RVA: 0x00360D9B File Offset: 0x0035F19B
		public bool OnSearch
		{
			[CompilerGenerated]
			get
			{
				return this.state == Fish.State.Wait || this.state == Fish.State.SearchNextPos || this.state == Fish.State.Swim || this.state == Fish.State.FollowLure;
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06007EE6 RID: 32486 RVA: 0x00360DCD File Offset: 0x0035F1CD
		private FishingDefinePack.FishParamGroup Param
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.FishParam;
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06007EE7 RID: 32487 RVA: 0x00360DDE File Offset: 0x0035F1DE
		private FishingDefinePack.SystemParamGroup SystemParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06007EE8 RID: 32488 RVA: 0x00360DEF File Offset: 0x0035F1EF
		public Vector3 Forward
		{
			[CompilerGenerated]
			get
			{
				return base.transform.forward;
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06007EE9 RID: 32489 RVA: 0x00360DFC File Offset: 0x0035F1FC
		// (set) Token: 0x06007EEA RID: 32490 RVA: 0x00360E04 File Offset: 0x0035F204
		public Fish.State state
		{
			get
			{
				return this.state_;
			}
			set
			{
				if (this.state_ != value)
				{
					Fish.State prev = this.state_;
					this.state_ = value;
					this.ChangedState(prev);
				}
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06007EEB RID: 32491 RVA: 0x00360E34 File Offset: 0x0035F234
		public float HeartPoint
		{
			get
			{
				return (float)this.fishInfo.HeartPoint;
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x06007EEC RID: 32492 RVA: 0x00360E50 File Offset: 0x0035F250
		// (set) Token: 0x06007EED RID: 32493 RVA: 0x00360E58 File Offset: 0x0035F258
		public FishingManager fishingSystem { get; private set; }

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x06007EEE RID: 32494 RVA: 0x00360E61 File Offset: 0x0035F261
		// (set) Token: 0x06007EEF RID: 32495 RVA: 0x00360E69 File Offset: 0x0035F269
		public FishInfo fishInfo { get; private set; } = default(FishInfo);

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06007EF0 RID: 32496 RVA: 0x00360E72 File Offset: 0x0035F272
		// (set) Token: 0x06007EF1 RID: 32497 RVA: 0x00360E7A File Offset: 0x0035F27A
		public bool WarningMode { get; private set; }

		// Token: 0x06007EF2 RID: 32498 RVA: 0x00360E84 File Offset: 0x0035F284
		private void Awake()
		{
			this.originScale = (this.startScale = base.transform.localScale);
			this.fishHitBehaviour = base.GetComponent<FishHitBehaviour>();
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x00360EB7 File Offset: 0x0035F2B7
		private void OnEnable()
		{
			this.activeNextPosition = false;
			(from _ in Observable.EveryUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x00360EF3 File Offset: 0x0035F2F3
		private void OnDisable()
		{
			this.state = Fish.State.None;
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x00360EFC File Offset: 0x0035F2FC
		private void OnDestroy()
		{
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x00360F00 File Offset: 0x0035F300
		public void Initialize(FishingManager _fishingSystem, FishInfo _fishInfo)
		{
			this.state = Fish.State.None;
			this.fishInfo = _fishInfo;
			this.fishingSystem = _fishingSystem;
			this.activeNextPosition = false;
			if (this.bodyObj != null)
			{
				UnityEngine.Object.Destroy(this.bodyObj);
			}
			base.transform.localScale = this.originScale;
			Tuple<AssetBundleInfo, RuntimeAnimatorController, string> tuple;
			GameObject original;
			if (Singleton<Manager.Resources>.Instance.Fishing.FishBodyTable.TryGetValue(this.fishInfo.SizeID, out tuple) && (original = CommonLib.LoadAsset<GameObject>(tuple.Item1.assetbundle, tuple.Item1.asset, false, tuple.Item1.manifest)) != null)
			{
				AssetBundleInfo item = tuple.Item1;
				MapScene.AddAssetBundlePath(item.assetbundle, item.manifest);
				this.bodyObj = UnityEngine.Object.Instantiate<GameObject>(original, Vector3.zero, Quaternion.identity);
				this.fishAnim = this.bodyObj.GetComponent<Animator>();
				if (this.fishAnim != null)
				{
					this.fishAnim.runtimeAnimatorController = tuple.Item2;
				}
				GameObject gameObject = this.bodyObj.transform.FindLoop(tuple.Item3);
				this.eyeT = (((gameObject != null) ? gameObject.transform : null) ?? base.transform);
				Renderer componentInChildren = this.bodyObj.GetComponentInChildren<Renderer>(true);
				float createOffsetHeight = Singleton<Manager.Resources>.Instance.FishingDefinePack.FishParam.CreateOffsetHeight;
				if (componentInChildren != null)
				{
					Vector3 center = componentInChildren.bounds.center;
					Vector3 extents = componentInChildren.bounds.extents;
					Vector3 position = this.bodyObj.transform.position;
					position.z -= extents.z + center.z;
					position.y -= extents.y + center.y + 0.25f;
					position.y += createOffsetHeight - extents.y;
					this.bodyObj.transform.position = position;
				}
				else
				{
					Vector3 position2 = this.bodyObj.transform.position;
					position2.y += createOffsetHeight;
					this.bodyObj.transform.position = position2;
				}
				this.bodyObj.transform.SetParent(base.transform, true);
				if (this.fishAnim != null)
				{
					this.fishAnim.CrossFadeInFixedTime(this.Param.AnimLoopName, 0.2f, 0, 0f);
				}
			}
			else
			{
				this.fishAnim = null;
				this.eyeT = base.transform;
				this.bodyObj = null;
			}
			this.lureSearcher.ResetFollowPercentage();
			this.state = Fish.State.FadeIn;
			this.ResetDestroyCount();
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x003611E6 File Offset: 0x0035F5E6
		public void SetWaitOrSwim()
		{
			this.state = ((UnityEngine.Random.value >= 0.5f) ? Fish.State.Swim : Fish.State.Wait);
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x00361204 File Offset: 0x0035F604
		public void ChangeState(Fish.State _state)
		{
			this.state = _state;
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x00361210 File Offset: 0x0035F610
		private void ChangedState(Fish.State _prev)
		{
			this.stateCounter = 0f;
			if (_prev == Fish.State.Hit)
			{
				this.fishingSystem.StopParticle(ParticleType.FishHitNormal, base.transform, ParticleSystemStopBehavior.StopEmittingAndClear);
				this.fishingSystem.StopParticle(ParticleType.FishHitAngry, base.transform, ParticleSystemStopBehavior.StopEmittingAndClear);
				this.fishingSystem.StopSE(SEType.FishResist, base.transform);
			}
			switch (this.state)
			{
			case Fish.State.FadeIn:
				this.StartFadeIn();
				break;
			case Fish.State.FadeOut:
				this.StartFadeOut();
				break;
			case Fish.State.Wait:
				this.StartWait();
				break;
			case Fish.State.SearchNextPos:
				this.StartSearchNextPos();
				break;
			case Fish.State.Swim:
				this.StartSwim();
				break;
			case Fish.State.FollowLure:
				this.StartFollowLure();
				break;
			case Fish.State.Hit:
				this.StartHit();
				break;
			case Fish.State.Escape:
				this.StartEscape();
				break;
			case Fish.State.HitToEscape:
				this.StartHitToEscape();
				break;
			case Fish.State.Get:
				this.StartGet();
				break;
			}
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x0036131C File Offset: 0x0035F71C
		private void OnUpdate()
		{
			this.DestroyCountDown();
			switch (this.state)
			{
			case Fish.State.FadeIn:
				this.OnFadeIn();
				break;
			case Fish.State.FadeOut:
				this.OnFadeOut();
				break;
			case Fish.State.Wait:
				this.OnWait();
				break;
			case Fish.State.SearchNextPos:
				this.OnSearchNextPos();
				break;
			case Fish.State.Swim:
				this.OnSwim();
				break;
			case Fish.State.FollowLure:
				this.OnFollowLure();
				break;
			case Fish.State.Hit:
				this.OnHit();
				break;
			case Fish.State.Escape:
				this.OnEscape();
				break;
			case Fish.State.HitToEscape:
				this.OnHitToEscape();
				break;
			case Fish.State.Get:
				this.OnGet();
				break;
			}
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x003613D9 File Offset: 0x0035F7D9
		private void ResetDestroyCount()
		{
			this.destroyCounter = 0f;
			this.destroyTimeLimit = UnityEngine.Random.Range(this.Param.DestroyMinTime, this.Param.DestroyMaxTime);
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x00361407 File Offset: 0x0035F807
		private float VecToRand(Vector2 _vec)
		{
			return UnityEngine.Random.Range(_vec.x, _vec.y);
		}

		// Token: 0x06007EFD RID: 32509 RVA: 0x0036141C File Offset: 0x0035F81C
		private bool ValueOnVec(float _value, Vector2 _vec)
		{
			return _vec.x <= _value && _value < _vec.y;
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x00361438 File Offset: 0x0035F838
		private void DestroyCountDown()
		{
			if (this.state < Fish.State.Wait || Fish.State.Hit <= this.state)
			{
				return;
			}
			if (this.state == Fish.State.FollowLure)
			{
				this.ResetDestroyCount();
				return;
			}
			this.destroyCounter += Time.deltaTime;
			if (this.destroyTimeLimit <= this.destroyCounter)
			{
				this.state = Fish.State.FadeOut;
			}
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x0036149C File Offset: 0x0035F89C
		private void ToDestroy()
		{
			this.state = Fish.State.None;
			this.fishingSystem.RemoveSE(SEType.FishResist, base.transform);
			this.fishingSystem.RemoveParticle(ParticleType.FishHitNormal, base.transform);
			this.fishingSystem.RemoveParticle(ParticleType.FishHitAngry, base.transform);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x003614F1 File Offset: 0x0035F8F1
		private void StartFadeIn()
		{
			this.stateTimeLimit = 1f;
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x00361500 File Offset: 0x0035F900
		private void OnFadeIn()
		{
			this.stateCounter += Time.deltaTime;
			if (this.stateTimeLimit < this.stateCounter)
			{
				base.transform.localScale = this.originScale;
				base.transform.localPosition = this.endFadePosition;
				this.SetWaitOrSwim();
				return;
			}
			float t = Mathf.InverseLerp(0f, this.stateTimeLimit, this.stateCounter);
			base.transform.localScale = Vector3.Lerp(Vector3.zero, this.originScale, t);
			base.transform.localPosition = Vector3.Lerp(this.startFadePosition, this.endFadePosition, t);
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x003615AC File Offset: 0x0035F9AC
		private void StartFadeOut()
		{
			this.startFadeOutScale = base.transform.localScale;
			this.startFadePosition = (this.endFadePosition = base.transform.localPosition);
			this.endFadePosition.y = this.endFadePosition.y - 5f;
			this.stateTimeLimit = 1f;
		}

		// Token: 0x06007F03 RID: 32515 RVA: 0x00361608 File Offset: 0x0035FA08
		private void OnFadeOut()
		{
			this.stateCounter += Time.deltaTime;
			if (this.stateTimeLimit < this.stateCounter)
			{
				base.transform.localScale = Vector3.zero;
				this.ToDestroy();
				return;
			}
			float t = Mathf.InverseLerp(0f, this.stateTimeLimit, this.stateCounter);
			base.transform.localScale = Vector3.Lerp(this.startFadeOutScale, Vector3.zero, t);
			base.transform.localPosition = Vector3.Lerp(this.startFadePosition, this.endFadePosition, t);
		}

		// Token: 0x06007F04 RID: 32516 RVA: 0x0036169F File Offset: 0x0035FA9F
		private void StartWait()
		{
			this.stateTimeLimit = UnityEngine.Random.Range(2f, 6f);
			this.activeNextPosition = false;
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x003616C0 File Offset: 0x0035FAC0
		private void OnWait()
		{
			if (!this.activeNextPosition)
			{
				this.SetTargetPos();
			}
			this.stateCounter += Time.deltaTime;
			if (this.stateTimeLimit <= this.stateCounter && this.activeNextPosition)
			{
				this.state = Fish.State.Swim;
				return;
			}
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x00361714 File Offset: 0x0035FB14
		private void StartSearchNextPos()
		{
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x00361716 File Offset: 0x0035FB16
		private void OnSearchNextPos()
		{
			if (!this.activeNextPosition)
			{
				this.SetTargetPos();
			}
			if (this.activeNextPosition)
			{
				this.state = Fish.State.Swim;
				return;
			}
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x0036173C File Offset: 0x0035FB3C
		private void StartSwim()
		{
			if (!this.activeNextPosition)
			{
				this.state = Fish.State.SearchNextPos;
				return;
			}
		}

		// Token: 0x06007F09 RID: 32521 RVA: 0x00361754 File Offset: 0x0035FB54
		private void OnSwim()
		{
			Vector2 vector = new Vector2(base.transform.forward.x, base.transform.forward.z);
			Vector2 normalized = vector.normalized;
			Vector2 vector2 = new Vector2(this.targetPosition.x - base.transform.position.x, this.targetPosition.z - base.transform.position.z);
			Vector2 normalized2 = vector2.normalized;
			float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
			float num = Mathf.Acos(f) * 57.29578f;
			if (0f < num)
			{
				Vector3 vector3 = Vector3.Cross(new Vector3(normalized.x, 0f, normalized.y), new Vector3(normalized2.x, 0f, normalized2.y));
				Vector3 localEulerAngles = base.transform.localEulerAngles;
				float num2 = this.Param.SwimAddAngle * Time.deltaTime * Mathf.Sign(vector3.y);
				if (num <= Mathf.Abs(num2))
				{
					localEulerAngles.y += num * Mathf.Sign(vector3.y);
				}
				else
				{
					localEulerAngles.y += num2;
				}
				localEulerAngles.y = this.AngleAbs(localEulerAngles.y);
				base.transform.localEulerAngles = localEulerAngles;
			}
			Vector3 position = base.transform.position + base.transform.forward * this.Param.SwimSpeed * Time.deltaTime;
			position = this.fishingSystem.ClampMoveAreaInWorld(position);
			base.transform.position = position;
			float worldDistanceFishToTarget = this.GetWorldDistanceFishToTarget();
			if (worldDistanceFishToTarget <= this.Param.SwimStopDistance)
			{
				this.state = Fish.State.Wait;
				return;
			}
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x00361954 File Offset: 0x0035FD54
		private bool FishOnArea()
		{
			float worldDistanceFishToLure = this.GetWorldDistanceFishToLure();
			if (0f >= worldDistanceFishToLure)
			{
				return true;
			}
			Vector2 vector = new Vector2(base.transform.forward.x, base.transform.forward.z);
			Vector2 normalized = vector.normalized;
			Vector2 vector2 = new Vector2(this.lure.transform.position.x - base.transform.position.x, this.lure.transform.position.z - base.transform.position.z);
			Vector2 normalized2 = vector2.normalized;
			float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
			float num = Mathf.Acos(f) * 57.29578f;
			Vector3 vector3 = Vector3.Cross(new Vector3(normalized.x, 0f, normalized.y), new Vector3(normalized2.x, 0f, normalized2.y));
			float num2 = this.Param.FollowAddAngle * Mathf.Sign(vector3.y) * Time.deltaTime;
			Vector3 eulerAngles = base.transform.eulerAngles;
			if (num <= Mathf.Abs(num2))
			{
				num2 = num * Mathf.Sign(vector3.y);
			}
			eulerAngles.y = this.AngleAbs(eulerAngles.y + num2);
			base.transform.eulerAngles = eulerAngles;
			Vector3 b = base.transform.forward * (this.Param.FollowSpeed * Time.deltaTime);
			if (worldDistanceFishToLure * worldDistanceFishToLure <= b.sqrMagnitude || worldDistanceFishToLure <= this.Param.HitDistance)
			{
				base.transform.position = this.lure.transform.position;
				return true;
			}
			base.transform.position = base.transform.position + b;
			return false;
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x00361B69 File Offset: 0x0035FF69
		private void StartFollowLure()
		{
			this.activeNextPosition = false;
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x00361B74 File Offset: 0x0035FF74
		private void OnFollowLure()
		{
			if (this.FishOnArea())
			{
				base.transform.position = this.lure.transform.position;
				this.state = Fish.State.Hit;
				if (Fish.FishHitEvent != null)
				{
					Fish.FishHitEvent(this);
				}
				return;
			}
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x00361BC8 File Offset: 0x0035FFC8
		private void StartHit()
		{
			if (this.fishAnim != null)
			{
				this.fishAnim.CrossFadeInFixedTime(this.Param.AnimHitName, 0.2f, 0, 0f);
			}
			this.fishingSystem.PlaySE(SEType.FishResist, base.transform, true, 0f);
			base.StartCoroutine(this.WarningCoroutine());
			this.fishHitBehaviour.StartHit();
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x00361C33 File Offset: 0x00360033
		private void OnHit()
		{
			this.fishHitBehaviour.OnHit();
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x00361C40 File Offset: 0x00360040
		private IEnumerator WarningCoroutine()
		{
			this.WarningMode = false;
			this.fishingSystem.PlayParticle(ParticleType.FishHitNormal, base.transform);
			while (this.state == Fish.State.Hit)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(this.Param.HitParam.AngryNextMinTime, this.Param.HitParam.AngryNextMaxTime));
				if (this.state != Fish.State.Hit)
				{
					yield break;
				}
				this.WarningMode = true;
				this.fishingSystem.StopParticle(ParticleType.FishHitNormal, base.transform, ParticleSystemStopBehavior.StopEmitting);
				this.fishingSystem.PlayParticle(ParticleType.FishHitAngry, base.transform);
				if (this.fishAnim != null)
				{
					this.fishAnim.CrossFadeInFixedTime(this.Param.AnimAngryName, 0.2f, 0, 0f);
				}
				yield return new WaitForSeconds(UnityEngine.Random.Range(this.Param.HitParam.AngryMinTimeLimit, this.Param.HitParam.AngryMaxTimeLimit));
				if (this.state != Fish.State.Hit)
				{
					yield break;
				}
				this.WarningMode = false;
				this.fishingSystem.StopParticle(ParticleType.FishHitAngry, base.transform, ParticleSystemStopBehavior.StopEmitting);
				this.fishingSystem.PlayParticle(ParticleType.FishHitNormal, base.transform);
				if (this.fishAnim != null)
				{
					this.fishAnim.CrossFadeInFixedTime(this.Param.AnimHitName, 0.2f, 0, 0f);
				}
			}
			yield break;
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x00361C5B File Offset: 0x0036005B
		private void SetEscapeAngle()
		{
			this.escapeSpeed = this.Param.EscapeSpeed;
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x00361C70 File Offset: 0x00360070
		private void Escape()
		{
			if (this.stateCounter < this.Param.EscapeFadeTime)
			{
				base.transform.position += base.transform.forward * (this.escapeSpeed * Time.deltaTime);
				float t = Mathf.InverseLerp(0f, this.Param.EscapeFadeTime, this.stateCounter);
				base.transform.localScale = Vector3.Lerp(this.startScale, Vector3.zero, t);
				this.stateCounter += Time.deltaTime;
				return;
			}
			this.ToDestroy();
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x00361D1C File Offset: 0x0036011C
		private void StartEscape()
		{
			this.startScale = base.transform.localScale;
			base.transform.LookAt(this.lure.transform);
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			localEulerAngles.y = this.AngleAbs(localEulerAngles.y + 180f);
			localEulerAngles.z = 0f;
			base.transform.localEulerAngles = localEulerAngles;
			this.SetEscapeAngle();
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x00361D94 File Offset: 0x00360194
		private bool CheckOnWater(float _distance)
		{
			Vector3 checkPos = base.transform.TransformDirection(Vector3.forward * _distance) + base.transform.position;
			return FishingManager.CheckOnWater(checkPos);
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x00361DD0 File Offset: 0x003601D0
		private void OnEscape()
		{
			this.Escape();
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x00361DD8 File Offset: 0x003601D8
		private void StartHitToEscape()
		{
			this.startScale = base.transform.localScale;
			this.SetEscapeAngle();
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x00361DF1 File Offset: 0x003601F1
		private void OnHitToEscape()
		{
			this.Escape();
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x00361DF9 File Offset: 0x003601F9
		private void StartGet()
		{
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x00361DFB File Offset: 0x003601FB
		private void OnGet()
		{
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x00361E00 File Offset: 0x00360200
		private int GetRandom(int _max, int _notNum)
		{
			int num;
			for (num = _notNum; num == _notNum; num = UnityEngine.Random.Range(0, _max))
			{
			}
			return num;
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x00361E24 File Offset: 0x00360224
		public FishInfo Get()
		{
			this.state = Fish.State.Get;
			this.ToDestroy();
			return this.fishInfo;
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x00361E3C File Offset: 0x0036023C
		public bool CheckLureInAngleFindArea()
		{
			Vector2 vector = new Vector2(base.transform.forward.x, base.transform.forward.z);
			Vector2 normalized = vector.normalized;
			Vector2 vector2 = new Vector2(this.lure.transform.position.x - this.eyeT.position.x, this.lure.transform.position.z - this.eyeT.position.z);
			Vector2 normalized2 = vector2.normalized;
			float num = Vector2.Dot(normalized, normalized2);
			num = Mathf.Clamp(num, -1f, 1f);
			float num2 = Mathf.Acos(num) * 57.29578f;
			return num2 * 2f <= this.Param.FindAngle;
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x00361F30 File Offset: 0x00360330
		private void SetTargetPos()
		{
			Vector3 vector = this.fishingSystem.GetRandomPosOnMoveArea();
			vector = this.fishingSystem.MoveArea.transform.TransformPoint(vector);
			if ((vector - base.transform.position).sqrMagnitude < this.Param.SwimDistance * this.Param.SwimDistance)
			{
				return;
			}
			this.targetPosition = vector;
			this.activeNextPosition = true;
		}

		// Token: 0x06007F1D RID: 32541 RVA: 0x00361FA4 File Offset: 0x003603A4
		private float GetWorldDistanceFishToTarget()
		{
			return Vector3.Distance(this.targetPosition, base.transform.position);
		}

		// Token: 0x06007F1E RID: 32542 RVA: 0x00361FBC File Offset: 0x003603BC
		private float GetWorldDistanceFishToLure()
		{
			return Vector3.Distance(this.lure.transform.position, base.transform.position);
		}

		// Token: 0x06007F1F RID: 32543 RVA: 0x00361FE0 File Offset: 0x003603E0
		public float GetWorldDistanceFishEyeToLure()
		{
			Vector3 position = this.eyeT.position;
			position.y = base.transform.position.y;
			return Vector3.Distance(this.lure.transform.position, position);
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x00362029 File Offset: 0x00360429
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

		// Token: 0x06007F21 RID: 32545 RVA: 0x0036205F File Offset: 0x0036045F
		private float Angle360To180(float _angle)
		{
			_angle = this.AngleAbs(_angle);
			if (180f < _angle)
			{
				_angle -= 360f;
			}
			return _angle;
		}

		// Token: 0x04006646 RID: 26182
		[NonSerialized]
		public Lure lure;

		// Token: 0x04006647 RID: 26183
		[SerializeField]
		private FishHitBehaviour fishHitBehaviour;

		// Token: 0x04006648 RID: 26184
		[SerializeField]
		private FishLureSearcher lureSearcher;

		// Token: 0x04006649 RID: 26185
		private float escapeSpeed = 1f;

		// Token: 0x0400664A RID: 26186
		private Fish.State state_;

		// Token: 0x0400664B RID: 26187
		private Vector3 targetPosition = Vector3.zero;

		// Token: 0x0400664C RID: 26188
		public static Action<Fish> FishHitEvent;

		// Token: 0x0400664D RID: 26189
		private Vector3 originScale = Vector3.one;

		// Token: 0x0400664E RID: 26190
		private Vector3 startScale = Vector3.one;

		// Token: 0x04006650 RID: 26192
		private GameObject bodyObj;

		// Token: 0x04006651 RID: 26193
		private Animator fishAnim;

		// Token: 0x04006652 RID: 26194
		private Transform eyeT;

		// Token: 0x04006655 RID: 26197
		private float stateCounter;

		// Token: 0x04006656 RID: 26198
		private float stateTimeLimit;

		// Token: 0x04006657 RID: 26199
		private bool activeNextPosition;

		// Token: 0x04006658 RID: 26200
		private float destroyCounter;

		// Token: 0x04006659 RID: 26201
		private float destroyTimeLimit;

		// Token: 0x0400665A RID: 26202
		[NonSerialized]
		public Vector3 startFadePosition = Vector3.zero;

		// Token: 0x0400665B RID: 26203
		[NonSerialized]
		public Vector3 endFadePosition = Vector3.zero;

		// Token: 0x0400665C RID: 26204
		[NonSerialized]
		public Vector3 startFadeOutScale = Vector3.one;

		// Token: 0x02000F1B RID: 3867
		public enum State
		{
			// Token: 0x0400665E RID: 26206
			None,
			// Token: 0x0400665F RID: 26207
			FadeIn,
			// Token: 0x04006660 RID: 26208
			FadeOut,
			// Token: 0x04006661 RID: 26209
			Wait,
			// Token: 0x04006662 RID: 26210
			SearchNextPos,
			// Token: 0x04006663 RID: 26211
			Swim,
			// Token: 0x04006664 RID: 26212
			FollowLure,
			// Token: 0x04006665 RID: 26213
			Hit,
			// Token: 0x04006666 RID: 26214
			Escape,
			// Token: 0x04006667 RID: 26215
			HitToEscape,
			// Token: 0x04006668 RID: 26216
			Get
		}
	}
}
