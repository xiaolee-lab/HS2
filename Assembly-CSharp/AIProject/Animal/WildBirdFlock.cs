using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Animal.Resources;
using AIProject.Scene;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000BC5 RID: 3013
	public class WildBirdFlock : AnimalBase
	{
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x0026C516 File Offset: 0x0026A916
		public override bool IsNeutralCommand
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x0026C51C File Offset: 0x0026A91C
		public void Initialize(BirdFlockHabitatPoint _habitatPoint)
		{
			this.Clear();
			this.habitatPoint = _habitatPoint;
			if (_habitatPoint == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			if (!this.habitatPoint.SetUse(this))
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			this.areaInfo = null;
			List<BirdFlockHabitatPoint.BirdMoveAreaInfo> list = ListPool<BirdFlockHabitatPoint.BirdMoveAreaInfo>.Get();
			list.AddRange(this.habitatPoint.AreaInfos);
			while (!list.IsNullOrEmpty<BirdFlockHabitatPoint.BirdMoveAreaInfo>())
			{
				this.areaInfo = list.GetRand<BirdFlockHabitatPoint.BirdMoveAreaInfo>();
				if (this.areaInfo == null || !this.areaInfo.Available)
				{
				}
			}
			ListPool<BirdFlockHabitatPoint.BirdMoveAreaInfo>.Release(list);
			if (this.areaInfo == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			base.SetStateData();
			MapArea ownerArea = this.habitatPoint.OwnerArea;
			base.ChunkID = ((!(ownerArea != null)) ? 0 : ownerArea.ChunkID);
			this.birdNum = this.areaInfo.CreateNumRange.RandomRange();
			Vector3 position = this.areaInfo.StartPoint.position;
			Vector3 position2 = this.areaInfo.EndPoint.position;
			this.deathDistance = Vector3.Distance(position, position2);
			this.Position = position;
			base.Rotation = Quaternion.LookRotation(position2 - position, Vector3.up);
			base.MarkerEnabled = true;
			this.SetState(AnimalState.Locomotion, null);
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x0026C684 File Offset: 0x0026AA84
		public override void Clear()
		{
			base.Clear();
			if (!this.birds.IsNullOrEmpty<WildBirdFlock.Bird>())
			{
				foreach (WildBirdFlock.Bird bird in this.birds)
				{
					if (bird != null)
					{
						bird.Destroy();
					}
				}
				this.birds.Clear();
			}
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
				this.habitatPoint = null;
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x0026C72C File Offset: 0x0026AB2C
		// (set) Token: 0x06005B66 RID: 23398 RVA: 0x0026C7B4 File Offset: 0x0026ABB4
		public override bool BodyEnabled
		{
			get
			{
				if (this.birds.IsNullOrEmpty<WildBirdFlock.Bird>())
				{
					return false;
				}
				foreach (WildBirdFlock.Bird bird in this.birds)
				{
					if (bird != null)
					{
						if (bird.BodyEnabled)
						{
							return true;
						}
					}
				}
				return false;
			}
			set
			{
				if (this.birds.IsNullOrEmpty<WildBirdFlock.Bird>())
				{
					return;
				}
				foreach (WildBirdFlock.Bird bird in this.birds)
				{
					if (bird != null)
					{
						bird.BodyEnabled = value;
					}
				}
			}
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0026C82C File Offset: 0x0026AC2C
		protected override void OnDestroy()
		{
			this.Active = false;
			foreach (WildBirdFlock.Bird bird in this.birds)
			{
				if (bird != null)
				{
					bird.Destroy();
				}
			}
			this.birds.Clear();
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
				this.habitatPoint = null;
			}
			base.OnDestroy();
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0026C8CC File Offset: 0x0026ACCC
		private IEnumerator StartBirdFlockFlap(int _birdNum)
		{
			float _distance = 0f;
			float _halfWidth = this.areaInfo.MoveRect.x / 2f;
			float _halfHeight = this.areaInfo.MoveRect.y / 2f;
			float _animationSpeed = 1f;
			string _animationSpeedParamName = string.Empty;
			if (Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalDefinePack != null)
			{
				AnimalDefinePack.AnimatorInfoGroup animatorInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AnimatorInfo;
				if (animatorInfo != null)
				{
					_animationSpeed = animatorInfo.BirdAnimationSpeed;
					_animationSpeedParamName = animatorInfo.AnimationSpeedParamName;
				}
			}
			for (int i = 0; i < _birdNum; i++)
			{
				float _x = UnityEngine.Random.Range(-_halfWidth, _halfWidth);
				float _y = UnityEngine.Random.Range(-_halfHeight, _halfHeight);
				float _z = _distance;
				Vector3 _createPosition = new Vector3(_x, _y, _z);
				WildBirdFlock.Bird _bird = new WildBirdFlock.Bird();
				_bird.LoadBody(this.ModelInfo.AssetInfo, base.transform);
				_bird.BodyEnabled = AnimalBase.CreateDisplay;
				_bird.SetAnimationSpeed(_animationSpeedParamName, _animationSpeed);
				_bird.LocalPosition = _createPosition;
				_bird.startPosition = _bird.Position;
				_bird.SetPlayAnimState(this.CurrentAnimState);
				_bird.StartInAnimation();
				this.birds.Add(_bird);
				float _randomWait = UnityEngine.Random.Range(this.nextBirdWaitSecondTime.x, this.nextBirdWaitSecondTime.y);
				_distance -= _randomWait * this.flapSpeed;
				yield return new WaitForSeconds(_randomWait);
			}
			yield break;
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0026C8F0 File Offset: 0x0026ACF0
		protected override void EnterLocomotion()
		{
			base.SetPlayAnimState(AnimationCategoryID.Locomotion, 0);
			IEnumerator _coroutine = this.StartBirdFlockFlap(this.birdNum);
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x0026C944 File Offset: 0x0026AD44
		protected override void OnLocomotion()
		{
			this.Position += base.Forward * this.flapSpeed * Time.deltaTime;
			for (int i = 0; i < this.birds.Count; i++)
			{
				WildBirdFlock.Bird bird = this.birds[i];
				if (this.deathDistance < Vector3.Distance(bird.Position, bird.startPosition))
				{
					bird.Destroy();
					this.birds.RemoveAt(i);
					i--;
				}
			}
			if (this.birds.IsNullOrEmpty<WildBirdFlock.Bird>())
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
		}

		// Token: 0x040052BD RID: 21181
		[SerializeField]
		[ReadOnly]
		[HideInEditorMode]
		private BirdFlockHabitatPoint habitatPoint;

		// Token: 0x040052BE RID: 21182
		private BirdFlockHabitatPoint.BirdMoveAreaInfo areaInfo;

		// Token: 0x040052BF RID: 21183
		[SerializeField]
		[Min(0f)]
		private float flapSpeed = 5f;

		// Token: 0x040052C0 RID: 21184
		[SerializeField]
		[MinMaxSlider(0f, 100f, true)]
		private Vector2 nextBirdWaitSecondTime = Vector2.zero;

		// Token: 0x040052C1 RID: 21185
		private List<WildBirdFlock.Bird> birds = new List<WildBirdFlock.Bird>();

		// Token: 0x040052C2 RID: 21186
		private int birdNum;

		// Token: 0x040052C3 RID: 21187
		private float deathDistance;

		// Token: 0x02000BC6 RID: 3014
		public class Bird
		{
			// Token: 0x1700115F RID: 4447
			// (get) Token: 0x06005B6C RID: 23404 RVA: 0x0026CA20 File Offset: 0x0026AE20
			public bool AnimatorEnable
			{
				[CompilerGenerated]
				get
				{
					return this.animator != null && this.animator.isActiveAndEnabled;
				}
			}

			// Token: 0x17001160 RID: 4448
			// (get) Token: 0x06005B6D RID: 23405 RVA: 0x0026CA41 File Offset: 0x0026AE41
			public bool AnimatorControllerEnable
			{
				[CompilerGenerated]
				get
				{
					return this.AnimatorEnable && this.animator.runtimeAnimatorController != null;
				}
			}

			// Token: 0x17001161 RID: 4449
			// (get) Token: 0x06005B6E RID: 23406 RVA: 0x0026CA62 File Offset: 0x0026AE62
			public bool AnimationEnable
			{
				[CompilerGenerated]
				get
				{
					return this.animation != null && this.animation.isActiveAndEnabled;
				}
			}

			// Token: 0x17001162 RID: 4450
			// (get) Token: 0x06005B6F RID: 23407 RVA: 0x0026CA83 File Offset: 0x0026AE83
			// (set) Token: 0x06005B70 RID: 23408 RVA: 0x0026CAAC File Offset: 0x0026AEAC
			public Vector3 Position
			{
				get
				{
					if (this.obj == null)
					{
						return Vector3.zero;
					}
					return this.obj.transform.position;
				}
				set
				{
					if (this.obj == null)
					{
						return;
					}
					this.obj.transform.position = value;
				}
			}

			// Token: 0x17001163 RID: 4451
			// (get) Token: 0x06005B71 RID: 23409 RVA: 0x0026CAD1 File Offset: 0x0026AED1
			// (set) Token: 0x06005B72 RID: 23410 RVA: 0x0026CAFA File Offset: 0x0026AEFA
			public Quaternion Rotation
			{
				get
				{
					if (this.obj == null)
					{
						return Quaternion.identity;
					}
					return this.obj.transform.rotation;
				}
				set
				{
					if (this.obj == null)
					{
						return;
					}
					this.obj.transform.rotation = value;
				}
			}

			// Token: 0x17001164 RID: 4452
			// (get) Token: 0x06005B73 RID: 23411 RVA: 0x0026CB1F File Offset: 0x0026AF1F
			// (set) Token: 0x06005B74 RID: 23412 RVA: 0x0026CB48 File Offset: 0x0026AF48
			public Vector3 LocalPosition
			{
				get
				{
					if (this.obj == null)
					{
						return Vector3.zero;
					}
					return this.obj.transform.localPosition;
				}
				set
				{
					if (this.obj == null)
					{
						return;
					}
					this.obj.transform.localPosition = value;
				}
			}

			// Token: 0x17001165 RID: 4453
			// (get) Token: 0x06005B75 RID: 23413 RVA: 0x0026CB6D File Offset: 0x0026AF6D
			// (set) Token: 0x06005B76 RID: 23414 RVA: 0x0026CB96 File Offset: 0x0026AF96
			public Quaternion LocalRotation
			{
				get
				{
					if (this.obj == null)
					{
						return Quaternion.identity;
					}
					return this.obj.transform.localRotation;
				}
				set
				{
					if (this.obj == null)
					{
						return;
					}
					this.obj.transform.localRotation = value;
				}
			}

			// Token: 0x17001166 RID: 4454
			// (get) Token: 0x06005B77 RID: 23415 RVA: 0x0026CBBC File Offset: 0x0026AFBC
			// (set) Token: 0x06005B78 RID: 23416 RVA: 0x0026CC18 File Offset: 0x0026B018
			public bool BodyEnabled
			{
				get
				{
					if (this.bodyRenderers.IsNullOrEmpty<Renderer>())
					{
						return false;
					}
					bool flag = false;
					foreach (Renderer renderer in this.bodyRenderers)
					{
						if (!(renderer == null))
						{
							flag |= renderer.enabled;
						}
					}
					return flag;
				}
				set
				{
					if (this.bodyRenderers.IsNullOrEmpty<Renderer>())
					{
						return;
					}
					foreach (Renderer renderer in this.bodyRenderers)
					{
						if (!(renderer == null))
						{
							if (renderer.enabled != value)
							{
								renderer.enabled = value;
							}
						}
					}
				}
			}

			// Token: 0x17001167 RID: 4455
			// (get) Token: 0x06005B79 RID: 23417 RVA: 0x0026CC79 File Offset: 0x0026B079
			public bool PlayingInAnimation
			{
				[CompilerGenerated]
				get
				{
					return this.inPlayAnimationDisposable != null;
				}
			}

			// Token: 0x17001168 RID: 4456
			// (get) Token: 0x06005B7A RID: 23418 RVA: 0x0026CC87 File Offset: 0x0026B087
			public bool PlayingOutAnimation
			{
				[CompilerGenerated]
				get
				{
					return this.outPlayAnimationDisposable != null;
				}
			}

			// Token: 0x17001169 RID: 4457
			// (get) Token: 0x06005B7B RID: 23419 RVA: 0x0026CC95 File Offset: 0x0026B095
			// (set) Token: 0x06005B7C RID: 23420 RVA: 0x0026CC9D File Offset: 0x0026B09D
			public AnimalPlayState PlayState { get; private set; }

			// Token: 0x1700116A RID: 4458
			// (get) Token: 0x06005B7D RID: 23421 RVA: 0x0026CCA6 File Offset: 0x0026B0A6
			private Queue<AnimalPlayState.StateInfo> InAnimState { get; } = new Queue<AnimalPlayState.StateInfo>();

			// Token: 0x1700116B RID: 4459
			// (get) Token: 0x06005B7E RID: 23422 RVA: 0x0026CCAE File Offset: 0x0026B0AE
			private Queue<AnimalPlayState.StateInfo> OutAnimState { get; } = new Queue<AnimalPlayState.StateInfo>();

			// Token: 0x06005B7F RID: 23423 RVA: 0x0026CCB6 File Offset: 0x0026B0B6
			public void StopChangeAnimation()
			{
				if (this.inPlayAnimationDisposable != null)
				{
					this.inPlayAnimationDisposable.Dispose();
				}
				this.inPlayAnimationDisposable = null;
				if (this.outPlayAnimationDisposable != null)
				{
					this.outPlayAnimationDisposable.Dispose();
				}
				this.outPlayAnimationDisposable = null;
			}

			// Token: 0x06005B80 RID: 23424 RVA: 0x0026CCF8 File Offset: 0x0026B0F8
			public bool SetPlayAnimState(AnimalPlayState _playState)
			{
				this.InAnimState.Clear();
				this.OutAnimState.Clear();
				this.PlayState = _playState;
				if (!this.AnimatorEnable || _playState == null)
				{
					return false;
				}
				this.animator.runtimeAnimatorController = _playState.MainStateInfo.Controller;
				AnimalPlayState.PlayStateInfo mainStateInfo = _playState.MainStateInfo;
				if (!mainStateInfo.InStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
				{
					foreach (AnimalPlayState.StateInfo item in mainStateInfo.InStateInfos)
					{
						this.InAnimState.Enqueue(item);
					}
				}
				if (!mainStateInfo.OutStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
				{
					foreach (AnimalPlayState.StateInfo item2 in mainStateInfo.OutStateInfos)
					{
						this.OutAnimState.Enqueue(item2);
					}
				}
				return true;
			}

			// Token: 0x06005B81 RID: 23425 RVA: 0x0026CDE8 File Offset: 0x0026B1E8
			public void StartInAnimation()
			{
				if (!this.AnimatorEnable || this.PlayState == null)
				{
					return;
				}
				this.StopChangeAnimation();
				IEnumerator _coroutine = this.StartAnimationCoroutine(this.InAnimState, this.PlayState.MainStateInfo.InFadeEnable, this.PlayState.MainStateInfo.InFadeSecond);
				this.inPlayAnimationDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this.obj).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					this.inPlayAnimationDisposable = null;
				});
			}

			// Token: 0x06005B82 RID: 23426 RVA: 0x0026CEA4 File Offset: 0x0026B2A4
			public void StartOutAnimation()
			{
				if (!this.AnimatorEnable || this.PlayState == null)
				{
					return;
				}
				this.StopChangeAnimation();
				IEnumerator _coroutine = this.StartAnimationCoroutine(this.OutAnimState, this.PlayState.MainStateInfo.OutFadeEnable, this.PlayState.MainStateInfo.OutFadeSecond);
				this.outPlayAnimationDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this.obj).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					this.outPlayAnimationDisposable = null;
				});
			}

			// Token: 0x06005B83 RID: 23427 RVA: 0x0026CF60 File Offset: 0x0026B360
			private IEnumerator StartAnimationCoroutine(Queue<AnimalPlayState.StateInfo> _states, bool _fadeEnable, float _fadeSecond)
			{
				this.animator.runtimeAnimatorController = this.PlayState.MainStateInfo.Controller;
				while (0 < _states.Count)
				{
					AnimalPlayState.StateInfo _state = _states.Dequeue();
					if (_fadeEnable)
					{
						this.animator.CrossFadeInFixedTime(_state.animCode, _fadeSecond, _state.layer, 0f);
						IConnectableObservable<long> _waiter = Observable.Timer(TimeSpan.FromSeconds((double)_fadeSecond)).TakeUntilDestroy(this.obj).Publish<long>();
						_waiter.Connect();
						yield return _waiter.ToYieldInstruction<long>();
					}
					else
					{
						this.animator.Play(_state.animCode, _state.layer, 0f);
						yield return null;
					}
					AnimatorStateInfo _stateInfo = this.animator.GetCurrentAnimatorStateInfo(_state.layer);
					while (this.AnimatorControllerEnable && (this.animator.IsInTransition(_state.layer) || (_stateInfo.IsName(_state.animName) && _stateInfo.normalizedTime < 1f)))
					{
						_stateInfo = this.animator.GetCurrentAnimatorStateInfo(_state.layer);
						yield return null;
					}
					yield return null;
				}
				yield return null;
				yield break;
			}

			// Token: 0x06005B84 RID: 23428 RVA: 0x0026CF90 File Offset: 0x0026B390
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

			// Token: 0x06005B85 RID: 23429 RVA: 0x0026D008 File Offset: 0x0026B408
			public void SetAnimationSpeed(string _paramName, float _speed)
			{
				if (!_paramName.IsNullOrEmpty() && this.animator != null)
				{
					this.animator.SetFloat(_paramName, _speed);
				}
				this.SetAnimationSpeed(_speed);
			}

			// Token: 0x06005B86 RID: 23430 RVA: 0x0026D03C File Offset: 0x0026B43C
			public void LoadBody(AssetBundleInfo _assetInfo, Transform _parent)
			{
				this.Destroy();
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(_assetInfo.assetbundle, _assetInfo.asset, false, _assetInfo.manifest);
				if (gameObject == null)
				{
					return;
				}
				MapScene.AddAssetBundlePath(_assetInfo.assetbundle, _assetInfo.manifest);
				this.obj = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				this.obj.transform.SetParent(_parent, false);
				this.animator = this.obj.GetComponentInChildren<Animator>(true);
				this.animation = this.obj.GetComponentInChildren<Animation>(true);
				this.bodyRenderers = this.obj.GetComponentsInChildren<Renderer>(true);
			}

			// Token: 0x06005B87 RID: 23431 RVA: 0x0026D0DF File Offset: 0x0026B4DF
			public void Destroy()
			{
				this.StopChangeAnimation();
				if (this.obj != null)
				{
					UnityEngine.Object.Destroy(this.obj);
				}
				this.obj = null;
				this.animator = null;
				this.animation = null;
				this.bodyRenderers = null;
			}

			// Token: 0x040052C4 RID: 21188
			public GameObject obj;

			// Token: 0x040052C5 RID: 21189
			public Animator animator;

			// Token: 0x040052C6 RID: 21190
			public Animation animation;

			// Token: 0x040052C7 RID: 21191
			public Vector3 startPosition = Vector3.zero;

			// Token: 0x040052C8 RID: 21192
			public Renderer[] bodyRenderers;

			// Token: 0x040052C9 RID: 21193
			private IDisposable inPlayAnimationDisposable;

			// Token: 0x040052CA RID: 21194
			private IDisposable outPlayAnimationDisposable;
		}
	}
}
