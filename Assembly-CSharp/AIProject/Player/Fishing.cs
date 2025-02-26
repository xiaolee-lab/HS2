using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.MiniGames.Fishing;
using AIProject.Scene;
using AIProject.UI;
using IllusionUtility.GetUtility;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF8 RID: 3576
	public class Fishing : PlayerStateBase
	{
		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06006E7F RID: 28287 RVA: 0x002F469D File Offset: 0x002F2A9D
		private FishingDefinePack.SystemParamGroup SystemParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06006E80 RID: 28288 RVA: 0x002F46AE File Offset: 0x002F2AAE
		private FishingDefinePack.PlayerParamGroup PlayerParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.PlayerParam;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06006E81 RID: 28289 RVA: 0x002F46BF File Offset: 0x002F2ABF
		// (set) Token: 0x06006E82 RID: 28290 RVA: 0x002F46D5 File Offset: 0x002F2AD5
		private FishingManager fishingSystem
		{
			get
			{
				Map instance = Singleton<Map>.Instance;
				return (instance != null) ? instance.FishingSystem : null;
			}
			set
			{
				if (Singleton<Map>.IsInstance())
				{
					Singleton<Map>.Instance.FishingSystem = value;
				}
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06006E83 RID: 28291 RVA: 0x002F46EC File Offset: 0x002F2AEC
		// (set) Token: 0x06006E84 RID: 28292 RVA: 0x002F46F4 File Offset: 0x002F2AF4
		public float MoveAreaPosY { get; private set; }

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06006E85 RID: 28293 RVA: 0x002F46FD File Offset: 0x002F2AFD
		// (set) Token: 0x06006E86 RID: 28294 RVA: 0x002F4705 File Offset: 0x002F2B05
		public GameObject hand { get; private set; }

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06006E87 RID: 28295 RVA: 0x002F470E File Offset: 0x002F2B0E
		// (set) Token: 0x06006E88 RID: 28296 RVA: 0x002F4716 File Offset: 0x002F2B16
		public bool EndAnimation { get; private set; }

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06006E89 RID: 28297 RVA: 0x002F471F File Offset: 0x002F2B1F
		// (set) Token: 0x06006E8A RID: 28298 RVA: 0x002F4727 File Offset: 0x002F2B27
		public bool LastAnimation { get; private set; }

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06006E8B RID: 28299 RVA: 0x002F4730 File Offset: 0x002F2B30
		// (set) Token: 0x06006E8C RID: 28300 RVA: 0x002F4738 File Offset: 0x002F2B38
		public float HorizontalValue
		{
			get
			{
				return this.horizontalValue;
			}
			set
			{
				this.horizontalValue = Mathf.Clamp(value, -1f, 1f);
				this.SetHorizontal(this.horizontalValue);
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06006E8D RID: 28301 RVA: 0x002F475C File Offset: 0x002F2B5C
		// (set) Token: 0x06006E8E RID: 28302 RVA: 0x002F4764 File Offset: 0x002F2B64
		public Fishing.PoseID currentPoseID { get; private set; }

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06006E8F RID: 28303 RVA: 0x002F476D File Offset: 0x002F2B6D
		// (set) Token: 0x06006E90 RID: 28304 RVA: 0x002F4775 File Offset: 0x002F2B75
		public PlayerActor player { get; set; }

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06006E91 RID: 28305 RVA: 0x002F477E File Offset: 0x002F2B7E
		private Manager.Resources.FishingTable FishingInfos
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.Fishing;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06006E92 RID: 28306 RVA: 0x002F478A File Offset: 0x002F2B8A
		private Dictionary<int, PlayState> AnimTable
		{
			[CompilerGenerated]
			get
			{
				return this.FishingInfos.PlayerAnimStateTable;
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06006E93 RID: 28307 RVA: 0x002F4797 File Offset: 0x002F2B97
		private FishingUI fishingUI
		{
			[CompilerGenerated]
			get
			{
				return MapUIContainer.FishingUI;
			}
		}

		// Token: 0x06006E94 RID: 28308 RVA: 0x002F47A0 File Offset: 0x002F2BA0
		protected override void OnAwake(PlayerActor actor)
		{
			this.player = actor;
			this.player.SetActiveOnEquipedItem(false);
			this.player.ChaControl.setAllLayerWeight(0f);
			this.currentPoseID = Fishing.PoseID.Idle;
			this.currentPlayState = null;
			this.fishingBeforeAnimController = null;
			if (this.player == null)
			{
				actor.Controller.ChangeState("Normal");
				return;
			}
			this.InitFishingSystem();
			this.LoadFishingRod();
			if (!this.fishingSystem.playerInfo.ActiveFishingRodInfo)
			{
				this.SetActive(Fishing.fishingSystemRoot, false);
				this.player.PlayerController.ChangeState("Normal");
				return;
			}
			this.MoveAreaPosY = this.player.PlayerController.CommandArea.BobberPosition.y;
			MapUIContainer.SetVisibleHUD(false);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			Transform transform = this.player.CameraControl.FishingCamera.transform;
			transform.position = this.player.Controller.transform.TransformPoint(this.player.CameraControl.FishingLocalPosition);
			transform.rotation = this.player.Rotation * this.player.CameraControl.FishingLocalRotation;
			this.player.CameraControl.Mode = CameraMode.Fishing;
			this.LoadFishingAnimator();
			this.PlayStandbyMotion(false);
			this.HaveFishingRod();
			MapUIContainer.SetActiveFishingUI(true);
			this.fishingSystem.Initialize(this);
			this.OnFinished = delegate(PlayerActor x)
			{
				this.Finished(x);
			};
			this._prevShotType = this.player.CameraControl.ShotType;
			this.player.CameraControl.SetShotTypeForce(ShotType.Near);
			this.initFlag = true;
		}

		// Token: 0x06006E95 RID: 28309 RVA: 0x002F495C File Offset: 0x002F2D5C
		private void InitFishingSystem()
		{
			if (this.fishingSystem == null)
			{
				GameObject original = CommonLib.LoadAsset<GameObject>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, this.PlayerParam.FishingGamePrefabName, false, Singleton<Manager.Resources>.Instance.DefinePack.ABManifests.Default);
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, this.player.transform);
				gameObject.name = this.PlayerParam.FishingGamePrefabName;
				Fishing.fishingSystemRoot = gameObject;
				this.fishingSystem = gameObject.GetComponentInChildren<FishingManager>();
				this.fishingSystem.FishGetEvent = delegate()
				{
					this.AcquirementFish();
				};
				MapScene.AddAssetBundlePath(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, Singleton<Manager.Resources>.Instance.DefinePack.ABManifests.Default);
			}
			Fishing.fishingSystemRoot.transform.position = this.player.Position;
			Fishing.fishingSystemRoot.transform.rotation = this.player.Rotation;
			this.FitMoveAreaOnWater();
			this.SetActive(Fishing.fishingSystemRoot, true);
		}

		// Token: 0x06006E96 RID: 28310 RVA: 0x002F4A74 File Offset: 0x002F2E74
		private void FitMoveAreaOnWater()
		{
			Vector3 position;
			FishingManager.GetWaterPosition(this.fishingSystem.MoveArea.transform.position, out position);
			this.fishingSystem.MoveArea.transform.position = position;
			this.fishingSystem.MoveArea.transform.localEulerAngles = Vector3.zero;
			Vector3 moveAreaOriginLocalPosition = this.fishingSystem.moveAreaOriginLocalPosition;
			moveAreaOriginLocalPosition.y = this.fishingSystem.MoveArea.transform.localPosition.y;
			this.fishingSystem.moveAreaOriginLocalPosition = moveAreaOriginLocalPosition;
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x002F4B0C File Offset: 0x002F2F0C
		public void SetHorizontal(float _value)
		{
			PlayerActor player = this.player;
			if (player != null)
			{
				player.Animation.Animator.SetFloat(this.PlayerParam.PlayerAnimParamName, _value);
			}
			this.fishingSystem.playerInfo.fishingRodAnimator.SetFloat(this.PlayerParam.RodAnimParamName, _value);
		}

		// Token: 0x06006E98 RID: 28312 RVA: 0x002F4B64 File Offset: 0x002F2F64
		private void LoadFishingRod()
		{
			this.hand = this.player.ChaControl.transform.FindLoop("k_f_handR_00");
			FishingManager fishingSystem = this.fishingSystem;
			PlayerInfo playerInfo = (fishingSystem != null) ? fishingSystem.playerInfo : null;
			if (playerInfo == null)
			{
				return;
			}
			Dictionary<int, FishingRodInfo> rodInfos = this.FishingInfos.RodInfos;
			if (!playerInfo.ActiveFishingRodInfo && !rodInfos.IsNullOrEmpty<int, FishingRodInfo>())
			{
				FishingRodInfo fishingRodInfo = rodInfos[0];
				playerInfo.fishingRod = UnityEngine.Object.Instantiate<GameObject>(fishingRodInfo.Rod);
				playerInfo.fishingRodAnimController = fishingRodInfo.AnimController;
				playerInfo.lurePos = playerInfo.fishingRod.transform.FindLoop(fishingRodInfo.TipName);
				playerInfo.fishingRodAnimator = playerInfo.fishingRod.GetComponent<Animator>();
				playerInfo.fishingRodAnimator.runtimeAnimatorController = playerInfo.fishingRodAnimController;
			}
			if (playerInfo.ActiveFishingRodInfo)
			{
				this.SetActive(playerInfo.fishingRod, false);
				Transform transform = playerInfo.fishingRod.transform;
				transform.parent = this.fishingSystem.RootObject.transform;
				Transform transform2 = transform;
				Vector3 zero = Vector3.zero;
				transform.localEulerAngles = zero;
				transform2.localPosition = zero;
			}
		}

		// Token: 0x06006E99 RID: 28313 RVA: 0x002F4C84 File Offset: 0x002F3084
		private void LoadFishingAnimator()
		{
			Animator animator = this.player.Animation.Animator;
			this.fishingBeforeAnimController = animator.runtimeAnimatorController;
			if (this.fishingAnimController == null)
			{
				int sex = (int)this.player.ChaControl.sex;
				this.FishingInfos.PlayerAnimatorTable.TryGetValue(sex, out this.fishingAnimController);
			}
		}

		// Token: 0x06006E9A RID: 28314 RVA: 0x002F4CE8 File Offset: 0x002F30E8
		public void HaveFishingRod()
		{
			FishingManager fishingSystem = this.fishingSystem;
			Transform transform;
			if (fishingSystem == null)
			{
				transform = null;
			}
			else
			{
				PlayerInfo playerInfo = fishingSystem.playerInfo;
				if (playerInfo == null)
				{
					transform = null;
				}
				else
				{
					GameObject fishingRod = playerInfo.fishingRod;
					transform = ((fishingRod != null) ? fishingRod.transform : null);
				}
			}
			Transform transform2 = transform;
			if (transform2 == null || this.hand == null)
			{
				return;
			}
			transform2.parent = this.hand.transform;
			Transform transform3 = transform2;
			Vector3 zero = Vector3.zero;
			transform2.localEulerAngles = zero;
			transform3.localPosition = zero;
			this.SetActive(transform2, true);
			this.SetActive(this.fishingSystem.lure, true);
			this.HorizontalValue = 0f;
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x002F4D94 File Offset: 0x002F3194
		public void ReleaseFishingRod()
		{
			FishingManager fishingSystem = this.fishingSystem;
			Transform transform;
			if (fishingSystem == null)
			{
				transform = null;
			}
			else
			{
				PlayerInfo playerInfo = fishingSystem.playerInfo;
				if (playerInfo == null)
				{
					transform = null;
				}
				else
				{
					GameObject fishingRod = playerInfo.fishingRod;
					transform = ((fishingRod != null) ? fishingRod.transform : null);
				}
			}
			Transform transform2 = transform;
			if (transform2 == null)
			{
				return;
			}
			transform2.parent = this.fishingSystem.RootObject.transform;
			this.SetActive(this.fishingSystem.lure, false);
			this.SetActive(transform2, false);
		}

		// Token: 0x06006E9C RID: 28316 RVA: 0x002F4E12 File Offset: 0x002F3212
		private void Finished(PlayerActor player)
		{
			this.fishingSystem.Release();
			this.SetActive(Fishing.fishingSystemRoot, false);
			player.PlayerController.ChangeState("Normal");
		}

		// Token: 0x06006E9D RID: 28317 RVA: 0x002F4E3B File Offset: 0x002F323B
		private void AcquirementFish()
		{
		}

		// Token: 0x06006E9E RID: 28318 RVA: 0x002F4E40 File Offset: 0x002F3240
		protected override void OnRelease(PlayerActor player)
		{
			this.ReleaseFishingRod();
			this.ChangeNormalAnimation();
			player.CameraControl.Mode = CameraMode.Normal;
			player.CameraControl.SetShotTypeForce(this._prevShotType);
			MapUIContainer.SetActiveFishingUI(false);
			if (MapUIContainer.CommandLabel.Acception != CommandLabel.AcceptionState.InvokeAcception)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			}
			MapUIContainer.SetVisibleHUD(true);
		}

		// Token: 0x06006E9F RID: 28319 RVA: 0x002F4E98 File Offset: 0x002F3298
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			if (!this.initFlag)
			{
				return;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			this.inputFlag = false;
			this.HorizontalUpdate(instance);
			this.UpdateRodAngle();
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x002F4EE4 File Offset: 0x002F32E4
		private void UpdateRodAngle()
		{
			Transform transform = this.fishingSystem.lure.transform;
			Transform transform2 = this.fishingSystem.playerInfo.fishingRod.transform;
			Transform transform3 = this.player.Controller.transform;
			if (transform2 == null || transform == null || transform3 == null)
			{
				return;
			}
			FishingManager.FishingScene scene = this.fishingSystem.scene;
			if (scene != FishingManager.FishingScene.WaitHit)
			{
				if (scene != FishingManager.FishingScene.Fishing)
				{
					transform2.localEulerAngles = Vector3.zero;
				}
				else
				{
					Vector2 vector = new Vector2(transform3.forward.x, transform3.forward.z);
					Vector2 normalized = vector.normalized;
					Vector2 vector2 = new Vector2(transform.position.x - transform3.position.x, transform.position.z - transform3.position.z);
					Vector2 normalized2 = vector2.normalized;
					float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
					float num = Mathf.Acos(f) * 57.29578f;
					Vector3 vector3 = Vector3.Cross(new Vector3(normalized.x, 0f, normalized.y), new Vector3(normalized2.x, 0f, normalized2.y));
					float num2 = EasingFunctions.EaseOutExpo(num, 90f);
					float y = this.AngleAbs(num * num2 * Mathf.Sign(vector3.y) * this.PlayerParam.RodAngleScale);
					transform2.localEulerAngles = new Vector3(0f, y, 0f);
				}
			}
			else
			{
				Vector3 localEulerAngles = transform2.localEulerAngles;
				localEulerAngles.x = (localEulerAngles.z = 0f);
				if (this.inputFlag)
				{
					Vector2 vector4 = new Vector2(transform3.forward.x, transform3.forward.z);
					Vector2 normalized3 = vector4.normalized;
					Vector2 vector5 = new Vector2(transform.position.x - transform3.position.x, transform.position.z - transform3.position.z);
					Vector2 normalized4 = vector5.normalized;
					float f2 = Mathf.Clamp(Vector2.Dot(normalized3, normalized4), -1f, 1f);
					float num3 = Mathf.Acos(f2) * 57.29578f;
					float num4 = Mathf.Sign(Vector3.Cross(new Vector3(normalized3.x, 0f, normalized3.y), new Vector3(normalized4.x, 0f, normalized4.y)).y);
					float num5 = EasingFunctions.EaseOutExpo(num3, 90f);
					float num6 = num3 * num5 * num4 * this.PlayerParam.RodAngleScale;
					localEulerAngles.y = this.Angle360To180(localEulerAngles.y);
					float num7 = this.PlayerParam.RodHitWaitAngleSpeed * Time.deltaTime * num4;
					localEulerAngles.y = (((localEulerAngles.y + num7) * num4 >= num6 * num4) ? num6 : (localEulerAngles.y + num7));
					localEulerAngles.y = this.AngleAbs(localEulerAngles.y);
					transform2.localEulerAngles = localEulerAngles;
				}
				else if (localEulerAngles.y != 0f)
				{
					localEulerAngles.y = this.Angle360To180(localEulerAngles.y);
					float num8 = Mathf.Sign(localEulerAngles.y);
					localEulerAngles.y -= this.PlayerParam.RodHitWaitAngleSpeed * Time.deltaTime * num8;
					if (localEulerAngles.y * num8 < 0f)
					{
						localEulerAngles.y = 0f;
					}
					localEulerAngles.y = this.AngleAbs(localEulerAngles.y);
					transform2.localEulerAngles = localEulerAngles;
				}
			}
		}

		// Token: 0x06006EA1 RID: 28321 RVA: 0x002F52FC File Offset: 0x002F36FC
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

		// Token: 0x06006EA2 RID: 28322 RVA: 0x002F5332 File Offset: 0x002F3732
		private Vector3 AngleAbs(Vector3 _angle)
		{
			_angle.x = this.AngleAbs(_angle.x);
			_angle.y = this.AngleAbs(_angle.y);
			_angle.z = this.AngleAbs(_angle.z);
			return _angle;
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x002F5371 File Offset: 0x002F3771
		private float Angle360To180(float _angle)
		{
			_angle = this.AngleAbs(_angle);
			if (180f < _angle)
			{
				_angle -= 360f;
			}
			return _angle;
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x002F5394 File Offset: 0x002F3794
		private void HorizontalUpdate(Manager.Input _input)
		{
			if (_input == null)
			{
				return;
			}
			if (_input.State == Manager.Input.ValidType.Action && this.LastAnimation)
			{
				FishingManager.FishingScene scene = this.fishingSystem.scene;
				if (scene != FishingManager.FishingScene.WaitHit)
				{
					if (scene != FishingManager.FishingScene.Fishing)
					{
						this.prevAxisX = 0f;
					}
					else
					{
						float num = this.fishingSystem.ArrowPowerVector.x / this.SystemParam.ArrowMaxPower;
						if (this.horizontalValue != num)
						{
							bool flag = this.horizontalValue <= num;
							this.horizontalValue += (float)((!flag) ? -1 : 1) * Time.deltaTime * this.PlayerParam.FishingHorizontalScale;
							if ((flag && num <= this.horizontalValue) || (!flag && this.horizontalValue < num))
							{
								this.horizontalValue = num;
							}
						}
						this.horizontalValue = Mathf.Clamp(this.horizontalValue, -1f, 1f);
						this.SetHorizontal(this.horizontalValue);
					}
				}
				else
				{
					float x = _input.LeftStickAxis.x;
					float x2 = this.fishingSystem.MouseAxis.normalized.x;
					float num2 = (Mathf.Abs(x2) > Mathf.Abs(x)) ? x2 : x;
					bool flag2 = !Mathf.Approximately(num2, 0f) || !Mathf.Approximately(this.prevAxisX, 0f);
					if (flag2)
					{
						float f = (num2 + this.prevAxisX) / 2f;
						this.horizontalValue += this.PlayerParam.WaitHitMoveHorizontalScale * Mathf.Sign(f) * Time.deltaTime;
						this.inputFlag = true;
					}
					else if (!Mathf.Approximately(this.horizontalValue, 0f))
					{
						bool flag3 = 0f <= this.horizontalValue;
						this.horizontalValue += this.PlayerParam.WaitHitReturnHorizontalScale * ((!flag3) ? 1f : -1f) * Time.deltaTime;
						if ((flag3 && this.horizontalValue <= 0f) || (!flag3 && 0f <= this.horizontalValue))
						{
							this.horizontalValue = 0f;
						}
					}
					else
					{
						this.HorizontalValue = 0f;
					}
					this.prevAxisX = num2;
					this.horizontalValue = Mathf.Clamp(this.horizontalValue, -1f, 1f);
					this.SetHorizontal(this.horizontalValue);
				}
			}
		}

		// Token: 0x06006EA5 RID: 28325 RVA: 0x002F564F File Offset: 0x002F3A4F
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006EA6 RID: 28326 RVA: 0x002F565E File Offset: 0x002F3A5E
		protected override void OnAnimatorStateEnterInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006EA7 RID: 28327 RVA: 0x002F5660 File Offset: 0x002F3A60
		protected override void OnAnimatorStateExitInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x002F5664 File Offset: 0x002F3A64
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06006EA9 RID: 28329 RVA: 0x002F5678 File Offset: 0x002F3A78
		public float CurrentAnimatorNormalizedTime
		{
			get
			{
				PlayerActor player = this.player;
				float? num = (player != null) ? new float?(player.Animation.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime) : null;
				return (num == null) ? 0f : num.Value;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06006EAA RID: 28330 RVA: 0x002F56D8 File Offset: 0x002F3AD8
		public bool IsInTransition
		{
			get
			{
				PlayerActor player = this.player;
				bool? flag;
				if (player == null)
				{
					flag = null;
				}
				else
				{
					Animator animator = player.Animation.Animator;
					int? num = (this.currentPlayState != null) ? new int?(this.currentPlayState.Layer) : null;
					flag = new bool?(animator.IsInTransition((num == null) ? 0 : num.Value));
				}
				bool? flag2 = flag;
				return flag2 != null && flag2.Value;
			}
		}

		// Token: 0x06006EAB RID: 28331 RVA: 0x002F576C File Offset: 0x002F3B6C
		private bool SetStateInfo(int _poseID, ref PlayState _getPlayState)
		{
			this.StateInInfos.Clear();
			PlayState playState = null;
			if (!this.AnimTable.TryGetValue(_poseID, out playState))
			{
				return false;
			}
			if (!playState.MainStateInfo.InStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item in playState.MainStateInfo.InStateInfo.StateInfos)
				{
					this.StateInInfos.Add(item);
				}
				_getPlayState = playState;
				return true;
			}
			return false;
		}

		// Token: 0x06006EAC RID: 28332 RVA: 0x002F57FC File Offset: 0x002F3BFC
		public void PlayAnimation(Fishing.PoseID _poseType)
		{
			this.currentPoseID = _poseType;
			PlayState playState = null;
			if (!this.SetStateInfo((int)_poseType, ref playState))
			{
				return;
			}
			this.currentPlayState = playState;
			if (this.animationDisposable != null)
			{
				this.animationDisposable.Dispose();
			}
			IEnumerator _coroutine = this.StartInAnimation(playState);
			this.animationDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06006EAD RID: 28333 RVA: 0x002F5872 File Offset: 0x002F3C72
		private bool AnimLoopActive(List<PlayState.Info> _animStates, PlayState _playState)
		{
			return 0 < _animStates.Count && this.currentPlayState == _playState;
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x002F588C File Offset: 0x002F3C8C
		private void CrossFadeInFixedTime(int _stateHashName, float _fixedTransitionDuration, int _layer)
		{
			if (this.player.Animation.Animator.isActiveAndEnabled)
			{
				this.player.Animation.Animator.CrossFadeInFixedTime(_stateHashName, _fixedTransitionDuration, _layer);
			}
			if (this.fishingSystem.playerInfo.fishingRodAnimator.isActiveAndEnabled)
			{
				this.fishingSystem.playerInfo.fishingRodAnimator.CrossFadeInFixedTime(_stateHashName, _fixedTransitionDuration, _layer);
			}
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x002F5900 File Offset: 0x002F3D00
		private void CrossFadeInFixedTime(string _animName, float _fixedTransitionDuration, int _layer, float _fixedTimeOffset)
		{
			if (this.player.Animation.Animator.isActiveAndEnabled)
			{
				this.player.Animation.Animator.CrossFadeInFixedTime(_animName, _fixedTransitionDuration, _layer, _fixedTimeOffset);
			}
			if (this.fishingSystem.playerInfo.fishingRodAnimator.isActiveAndEnabled)
			{
				this.fishingSystem.playerInfo.fishingRodAnimator.CrossFadeInFixedTime(_animName, _fixedTransitionDuration, _layer, _fixedTimeOffset);
			}
		}

		// Token: 0x06006EB0 RID: 28336 RVA: 0x002F5978 File Offset: 0x002F3D78
		private void Play(int _stateNameHash, int _layer, float _normalizedTime)
		{
			if (this.player.Animation.Animator.isActiveAndEnabled)
			{
				this.player.Animation.Animator.Play(_stateNameHash, _layer, _normalizedTime);
			}
			if (this.fishingSystem.playerInfo.fishingRodAnimator.isActiveAndEnabled)
			{
				this.fishingSystem.playerInfo.fishingRodAnimator.Play(_stateNameHash, _layer, _normalizedTime);
			}
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x002F59EC File Offset: 0x002F3DEC
		private void Play(string _stateName, int _layer, float _normalizedTime)
		{
			if (this.player.Animation.Animator.isActiveAndEnabled)
			{
				this.player.Animation.Animator.Play(_stateName, _layer, _normalizedTime);
			}
			if (this.fishingSystem.playerInfo.fishingRodAnimator.isActiveAndEnabled)
			{
				this.fishingSystem.playerInfo.fishingRodAnimator.Play(_stateName, _layer, _normalizedTime);
			}
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x002F5A5D File Offset: 0x002F3E5D
		private bool AnimWaitAcitve(AnimatorStateInfo _animStateInfo, Animator _animator, PlayState.Info _stateInfo)
		{
			return _animator.IsInTransition(_stateInfo.layer) || (_animStateInfo.IsName(_stateInfo.stateName) && _animStateInfo.normalizedTime < 1f);
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x002F5A98 File Offset: 0x002F3E98
		private IEnumerator StartInAnimation(PlayState _playState)
		{
			bool flag = false;
			this.EndAnimation = flag;
			this.LastAnimation = flag;
			if (this.fishingAnimController.name != this.fishingBeforeAnimController.name)
			{
				this.player.Animation.Animator.runtimeAnimatorController = this.fishingAnimController;
			}
			List<PlayState.Info> _animStates = ListPool<PlayState.Info>.Get();
			_animStates.AddRange(this.StateInInfos);
			Animator _playerAnimator = this.player.Animation.Animator;
			bool _fadeEnable = true;
			float _fadeTime = 0.2f;
			while (this.AnimLoopActive(_animStates, _playState))
			{
				PlayState.Info _state = _animStates.Pop<PlayState.Info>();
				List<Fishing.Schedule> _schedules = ListPool<Fishing.Schedule>.Get();
				Dictionary<string, List<Fishing.Schedule>> _scheduleTable = Singleton<Manager.Resources>.Instance.Fishing.AnimEventScheduler;
				if (_scheduleTable.ContainsKey(_state.stateName))
				{
					_schedules.AddRange(_scheduleTable[_state.stateName]);
					_schedules.Sort(Fishing.ScheduleTimeCompare.Get);
				}
				if (_fadeEnable)
				{
					this.CrossFadeInFixedTime(_state.ShortNameStateHash, _fadeTime, _state.layer);
					IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)_fadeTime)).Publish<long>();
					waiter.Connect();
					yield return waiter.ToYieldInstruction<long>();
				}
				else
				{
					this.Play(_state.ShortNameStateHash, _state.layer, 0f);
					yield return null;
				}
				if (_animStates.Count == 0)
				{
					this.LastAnimation = true;
				}
				AnimatorStateInfo _playerStateInfo = _playerAnimator.GetCurrentAnimatorStateInfo(_state.layer);
				while (this.AnimWaitAcitve(_playerStateInfo, _playerAnimator, _state))
				{
					_playerStateInfo = _playerAnimator.GetCurrentAnimatorStateInfo(_state.layer);
					while (0 < _schedules.Count && _playerStateInfo.IsName(_state.stateName) && _schedules[0].startTime <= _playerStateInfo.normalizedTime)
					{
						Fishing.Schedule schedule = _schedules.Pop<Fishing.Schedule>();
						this.EventExecute(ref schedule);
					}
					yield return null;
				}
				while (0 < _schedules.Count)
				{
					Fishing.Schedule schedule2 = _schedules.Pop<Fishing.Schedule>();
					this.EventExecute(ref schedule2);
				}
				ListPool<Fishing.Schedule>.Release(_schedules);
				yield return null;
			}
			ListPool<PlayState.Info>.Release(_animStates);
			yield return null;
			flag = true;
			this.EndAnimation = flag;
			this.LastAnimation = flag;
			yield break;
		}

		// Token: 0x06006EB4 RID: 28340 RVA: 0x002F5ABA File Offset: 0x002F3EBA
		public void EndFishing()
		{
			if (this.OnFinished != null)
			{
				this.OnFinished(this.player);
				this.OnFinished = null;
			}
		}

		// Token: 0x06006EB5 RID: 28341 RVA: 0x002F5AE0 File Offset: 0x002F3EE0
		public void PlayMissAnimation()
		{
			Fishing.PoseID poseID = Fishing.PoseID.Miss;
			this.currentPoseID = poseID;
			int poseID2 = (int)poseID;
			PlayState playState = null;
			if (!this.SetStateInfo(poseID2, ref playState))
			{
				return;
			}
			this.currentPlayState = playState;
			if (this.animationDisposable != null)
			{
				this.animationDisposable.Dispose();
			}
			IEnumerator _coroutine = this.StartMissAnimation(playState);
			this.animationDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06006EB6 RID: 28342 RVA: 0x002F5B58 File Offset: 0x002F3F58
		private IEnumerator StartMissAnimation(PlayState _playState)
		{
			yield return this.StartInAnimation(_playState);
			if (this.MissAnimationEndEvent != null)
			{
				this.MissAnimationEndEvent();
			}
			this.PlayStandbyMotion(true);
			if (this.MissAnimationEndEvent != null)
			{
				this.MissAnimationEndEvent();
			}
			yield break;
		}

		// Token: 0x06006EB7 RID: 28343 RVA: 0x002F5B7C File Offset: 0x002F3F7C
		public void PlayMissEndAnimation()
		{
			Fishing.PoseID poseID = Fishing.PoseID.Miss;
			this.currentPoseID = poseID;
			int poseID2 = (int)poseID;
			PlayState playState = null;
			if (!this.SetStateInfo(poseID2, ref playState))
			{
				return;
			}
			this.currentPlayState = playState;
			if (this.animationDisposable != null)
			{
				this.animationDisposable.Dispose();
			}
			IEnumerator _coroutine = this.StartMissEndAnimation(playState);
			this.animationDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x002F5BF4 File Offset: 0x002F3FF4
		private IEnumerator StartMissEndAnimation(PlayState _playState)
		{
			yield return this.StartInAnimation(_playState);
			if (this.currentPlayState != _playState)
			{
				this.currentPlayState = _playState;
			}
			if (this.MissAnimationEndEvent != null)
			{
				this.MissAnimationEndEvent();
			}
			this.EndFishing();
			yield break;
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x002F5C18 File Offset: 0x002F4018
		public void PlayStopAnimation()
		{
			Fishing.PoseID poseID = Fishing.PoseID.Stop;
			this.currentPoseID = poseID;
			int poseID2 = (int)poseID;
			PlayState playState = null;
			if (!this.SetStateInfo(poseID2, ref playState))
			{
				return;
			}
			this.currentPlayState = playState;
			if (this.animationDisposable != null)
			{
				this.animationDisposable.Dispose();
			}
			IEnumerator _coroutine = this.StartStopAnimation(playState);
			this.animationDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06006EBA RID: 28346 RVA: 0x002F5C90 File Offset: 0x002F4090
		private IEnumerator StartStopAnimation(PlayState _playState)
		{
			yield return this.StartInAnimation(_playState);
			if (this.currentPlayState != _playState)
			{
				this.currentPlayState = _playState;
			}
			this.PlayStandbyMotion(true);
			if (this.StopAnimationEndEvent != null)
			{
				this.StopAnimationEndEvent();
			}
			yield break;
		}

		// Token: 0x06006EBB RID: 28347 RVA: 0x002F5CB4 File Offset: 0x002F40B4
		public void PlayStandbyMotion(bool _crossFadePlay = true)
		{
			if (this.player == null)
			{
				return;
			}
			if (this.animationDisposable != null)
			{
				this.animationDisposable.Dispose();
			}
			bool flag = true;
			this.EndAnimation = flag;
			this.LastAnimation = flag;
			this.currentPlayState = null;
			string text = null;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				text = Singleton<Manager.Resources>.Instance.FishingDefinePack.PlayerParam.AnimStandbyName;
			}
			if (text.IsNullOrEmpty())
			{
				text = "Locomotion";
			}
			Animator animator = this.player.Animation.Animator;
			if (animator == null)
			{
				return;
			}
			RuntimeAnimatorController runtimeAnimatorController = animator.runtimeAnimatorController;
			if (runtimeAnimatorController == null)
			{
				animator.runtimeAnimatorController = this.fishingAnimController;
				animator.CrossFadeInFixedTime(text, 0.2f, 0, 0f);
				return;
			}
			if (runtimeAnimatorController.name == this.fishingAnimController.name && animator.GetCurrentAnimatorStateInfo(0).IsName(text))
			{
				return;
			}
			if (runtimeAnimatorController.name != this.fishingAnimController.name)
			{
				this.player.Animation.Animator.runtimeAnimatorController = this.fishingAnimController;
			}
			if (_crossFadePlay)
			{
				this.CrossFadeInFixedTime(text, 0.2f, 0, 0f);
			}
			else
			{
				this.Play(text, 0, 0f);
			}
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x002F5E18 File Offset: 0x002F4218
		public void ChangeNormalAnimation()
		{
			if (this.animationDisposable != null)
			{
				this.animationDisposable.Dispose();
			}
			bool flag = true;
			this.EndAnimation = flag;
			this.LastAnimation = flag;
			this.currentPlayState = null;
			RuntimeAnimatorController runtimeAnimatorController = this.player.Animation.Animator.runtimeAnimatorController;
			if (this.fishingBeforeAnimController.name == runtimeAnimatorController.name && this.player.Animation.Animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
			{
				return;
			}
			if (this.fishingAnimController.name != this.fishingBeforeAnimController.name)
			{
				this.player.Animation.Animator.runtimeAnimatorController = this.fishingBeforeAnimController;
			}
			this.player.Animation.Animator.CrossFadeInFixedTime("Locomotion", 0.2f, 0, 0f);
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x002F5F10 File Offset: 0x002F4310
		private void EventExecute(ref Fishing.Schedule _s)
		{
			if (_s.enable)
			{
				switch (_s.eventID)
				{
				case 0:
					this.fishingSystem.lure.StartThrow();
					break;
				case 1:
					this.fishingSystem.lure.StartReturn(ParticleType.LureInOut);
					break;
				case 2:
					this.fishingSystem.PlaySE(SEType.FishGet, this.fishingSystem.lure.transform, false, 0f);
					this.fishingSystem.lure.StartReturn(ParticleType.FishGet);
					break;
				case 3:
					this.fishingSystem.PlaySE(SEType.FishEscape, this.fishingSystem.lure.transform, false, 0f);
					this.fishingSystem.lure.StartReturn(ParticleType.LureInOut);
					break;
				case 4:
					this.fishingSystem.PlaySE(SEType.LureThrow, this.fishingSystem.lure.transform, false, 0f);
					break;
				}
				_s.enable = false;
			}
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x002F601C File Offset: 0x002F441C
		private void SetActive(Component c, bool a)
		{
			if (c == null)
			{
				return;
			}
			if (c.gameObject.activeSelf != a)
			{
				c.gameObject.SetActive(a);
			}
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x002F6048 File Offset: 0x002F4448
		private void SetActive(GameObject o, bool a)
		{
			if (o == null)
			{
				return;
			}
			if (o.activeSelf != a)
			{
				o.SetActive(a);
			}
		}

		// Token: 0x04005BA4 RID: 23460
		private bool initFlag;

		// Token: 0x04005BA5 RID: 23461
		private const string exitStateName = "Normal";

		// Token: 0x04005BA6 RID: 23462
		private Action<PlayerActor> OnFinished;

		// Token: 0x04005BA7 RID: 23463
		private static GameObject fishingSystemRoot;

		// Token: 0x04005BAC RID: 23468
		private float horizontalValue;

		// Token: 0x04005BAE RID: 23470
		private PlayState currentPlayState;

		// Token: 0x04005BAF RID: 23471
		private RuntimeAnimatorController fishingBeforeAnimController;

		// Token: 0x04005BB0 RID: 23472
		private RuntimeAnimatorController fishingAnimController;

		// Token: 0x04005BB2 RID: 23474
		private ShotType _prevShotType;

		// Token: 0x04005BB3 RID: 23475
		private bool inputFlag;

		// Token: 0x04005BB4 RID: 23476
		private float prevAxisX;

		// Token: 0x04005BB5 RID: 23477
		private List<PlayState.Info> StateInInfos = new List<PlayState.Info>();

		// Token: 0x04005BB6 RID: 23478
		private IDisposable animationDisposable;

		// Token: 0x04005BB7 RID: 23479
		public Action MissAnimationEndEvent;

		// Token: 0x04005BB8 RID: 23480
		public Action MissEndAnimationEndEvent;

		// Token: 0x04005BB9 RID: 23481
		public Action StopAnimationEndEvent;

		// Token: 0x02000DF9 RID: 3577
		public enum PoseID
		{
			// Token: 0x04005BBB RID: 23483
			Idle,
			// Token: 0x04005BBC RID: 23484
			Hit,
			// Token: 0x04005BBD RID: 23485
			Success,
			// Token: 0x04005BBE RID: 23486
			MissToIdle,
			// Token: 0x04005BBF RID: 23487
			Miss,
			// Token: 0x04005BC0 RID: 23488
			Stop
		}

		// Token: 0x02000DFA RID: 3578
		public struct Schedule
		{
			// Token: 0x06006EC3 RID: 28355 RVA: 0x002F607D File Offset: 0x002F447D
			public Schedule(string _animName, int _eventID, float _startTime)
			{
				this.animName = _animName;
				this.eventID = _eventID;
				this.startTime = _startTime;
				this.memo = null;
				this.enable = true;
			}

			// Token: 0x06006EC4 RID: 28356 RVA: 0x002F60A2 File Offset: 0x002F44A2
			public Schedule(string _animName, int _eventID, float _startTime, string _memo)
			{
				this.animName = _animName;
				this.eventID = _eventID;
				this.startTime = _startTime;
				this.memo = _memo;
				this.enable = true;
			}

			// Token: 0x06006EC5 RID: 28357 RVA: 0x002F60C8 File Offset: 0x002F44C8
			public override string ToString()
			{
				return string.Format("Fishing.Schedule animName[{0}] eventID[{1}] startTime[{2}] memo[{3}] enable[{4}]", new object[]
				{
					this.animName,
					this.eventID,
					this.startTime,
					this.memo,
					this.enable
				});
			}

			// Token: 0x06006EC6 RID: 28358 RVA: 0x002F6121 File Offset: 0x002F4521
			public void DebugLog()
			{
			}

			// Token: 0x04005BC1 RID: 23489
			public string animName;

			// Token: 0x04005BC2 RID: 23490
			public int eventID;

			// Token: 0x04005BC3 RID: 23491
			public float startTime;

			// Token: 0x04005BC4 RID: 23492
			public string memo;

			// Token: 0x04005BC5 RID: 23493
			public bool enable;
		}

		// Token: 0x02000DFB RID: 3579
		public class ScheduleTimeCompare : IComparer<Fishing.Schedule>
		{
			// Token: 0x17001557 RID: 5463
			// (get) Token: 0x06006EC8 RID: 28360 RVA: 0x002F612B File Offset: 0x002F452B
			public static Fishing.ScheduleTimeCompare Get
			{
				[CompilerGenerated]
				get
				{
					Fishing.ScheduleTimeCompare result;
					if ((result = Fishing.ScheduleTimeCompare.instance_) == null)
					{
						result = (Fishing.ScheduleTimeCompare.instance_ = new Fishing.ScheduleTimeCompare());
					}
					return result;
				}
			}

			// Token: 0x06006EC9 RID: 28361 RVA: 0x002F6144 File Offset: 0x002F4544
			public static void Clear()
			{
				Fishing.ScheduleTimeCompare.instance_ = null;
			}

			// Token: 0x06006ECA RID: 28362 RVA: 0x002F614C File Offset: 0x002F454C
			public int Compare(Fishing.Schedule _s1, Fishing.Schedule _s2)
			{
				if (_s1.startTime <= _s2.startTime)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x04005BC6 RID: 23494
			private static Fishing.ScheduleTimeCompare instance_;
		}
	}
}
