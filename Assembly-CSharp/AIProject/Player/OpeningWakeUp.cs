using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ADV;
using AIChara;
using AIProject.Definitions;
using AIProject.Scene;
using Cinemachine;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E08 RID: 3592
	public class OpeningWakeUp : PlayerStateBase
	{
		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06006F25 RID: 28453 RVA: 0x002FA87C File Offset: 0x002F8C7C
		private OpenData openData { get; } = new OpenData();

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06006F26 RID: 28454 RVA: 0x002FA884 File Offset: 0x002F8C84
		// (set) Token: 0x06006F27 RID: 28455 RVA: 0x002FA88C File Offset: 0x002F8C8C
		private OpeningWakeUp.PackData packData { get; set; }

		// Token: 0x06006F28 RID: 28456 RVA: 0x002FA898 File Offset: 0x002F8C98
		protected override void OnAwake(PlayerActor player)
		{
			this._player = player;
			this._agent = Singleton<Manager.Map>.Instance.TutorialAgent;
			this._isFinish = false;
			if (this._player == null || this._agent == null)
			{
				return;
			}
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			Manager.Resources.AnimationTables animation = Singleton<Manager.Resources>.Instance.Animation;
			CommonDefine.TutorialSetting setting = commonDefine.Tutorial;
			player.ChaControl.visibleAll = false;
			this._agent.ChangeTutorialBehavior(Tutorial.ActionType.Idle);
			player.EventKey = (EventType)0;
			this._fadeEnd = false;
			int personality = this._agent.ChaControl.fileParam.personality;
			ChaControl chaControl = this._agent.ChaControl;
			chaControl.ChangeLookEyesPtn(0);
			chaControl.ChangeLookNeckPtn(3, 1f);
			PoseKeyPair poseKeyPair;
			PlayState playState;
			this.TryGetWakeUpAnimState(personality, out poseKeyPair, out playState);
			MapUIContainer.SetVisibleHUD(false);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			this._prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			this._eventCamera = player.CameraControl.EventCamera;
			this._eventCamera.transform.SetParent(player.CameraControl.EventCameraParent, false);
			this._eventCamera.transform.localPosition = Vector3.zero;
			this._eventCamera.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			Animator eventCameraLocator = player.CameraControl.EventCameraLocator;
			RuntimeAnimatorController itemAnimator = animation.GetItemAnimator(this.GetWakeUpCameraAnimatorID(personality));
			eventCameraLocator.runtimeAnimatorController = itemAnimator;
			eventCameraLocator.transform.position = this._agent.Position;
			eventCameraLocator.transform.rotation = this._agent.Rotation;
			eventCameraLocator.speed = 0f;
			float shapeBodyValue = this._agent.ChaControl.GetShapeBodyValue(0);
			string heightParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
			eventCameraLocator.SetFloat(heightParameterName, shapeBodyValue);
			player.CameraControl.Mode = CameraMode.Event;
			this._agent.Animation.LoadEventKeyTable(poseKeyPair.postureID, poseKeyPair.poseID);
			this._agent.Animation.InitializeStates(playState);
			this._agent.LoadEventItems(playState);
			this._agent.Animation.StopAllAnimCoroutine();
			this._agent.Animation.PlayInAnimation(playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.MainStateInfo.FadeOutTime, playState.Layer);
			this.PlayCameraAnimation(eventCameraLocator, personality);
			Transform transform = player.CameraControl.CameraComponent.transform;
			ChaControl chaControl2 = this._agent.ChaControl;
			chaControl2.ChangeLookEyesTarget(1, transform, 0.5f, 0f, 1f, 2f);
			chaControl2.ChangeLookEyesPtn(1);
			this._fadeTimerDisposable = Observable.Timer(TimeSpan.FromSeconds((double)setting.OpeningWakeUpStartFadeTime)).TakeUntilDestroy(player).Subscribe(delegate(long _)
			{
				if (Singleton<Sound>.IsInstance())
				{
					Sound instance = Singleton<Sound>.Instance;
					instance.StopBGM(setting.OpeningWakeUpFadeTime);
				}
				IObservable<Unit> source = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, setting.OpeningWakeUpFadeTime, false);
				this._fadeTimerDisposable = source.Subscribe(delegate(Unit __)
				{
				}, delegate()
				{
					this._fadeEnd = true;
				});
			});
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				this._isFinish = true;
				this.Elapsed(player);
			});
		}

		// Token: 0x06006F29 RID: 28457 RVA: 0x002FAC54 File Offset: 0x002F9054
		private int GetWakeUpCameraAnimatorID(int personalID)
		{
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			int result;
			if (!commonDefine.OpeningWakeUpCameraAnimatorIDDic.TryGetValue(personalID, out result) && !commonDefine.OpeningWakeUpCameraAnimatorIDDic.TryGetValue(0, out result))
			{
				return commonDefine.ItemAnims.OpeningWakeUpCameraAnimatorID;
			}
			return result;
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x002FACA0 File Offset: 0x002F90A0
		private bool TryGetWakeUpAnimState(int personalID, out PoseKeyPair pose, out PlayState playState)
		{
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			if (!agentProfile.TutorialWakeUpPoseTable.TryGetValue(personalID, out pose) && !agentProfile.TutorialWakeUpPoseTable.TryGetValue(0, out pose))
			{
				playState = null;
				return false;
			}
			Manager.Resources.AnimationTables animation = Singleton<Manager.Resources>.Instance.Animation;
			Dictionary<int, PlayState> dictionary;
			if (animation.AgentActionAnimTable.TryGetValue(pose.postureID, out dictionary) && dictionary.TryGetValue(pose.poseID, out playState))
			{
				return true;
			}
			playState = null;
			return false;
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x002FAD1C File Offset: 0x002F911C
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			if (this._isFinish)
			{
				return;
			}
			if (!this._fadeEnd || MapUIContainer.FadeCanvas.IsFadeIn)
			{
				return;
			}
			if (this.PlayingCameraAnimation || this._agent.Animation.PlayingInAnimation)
			{
				return;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x002FAD90 File Offset: 0x002F9190
		private void StartADV()
		{
			if (this._player == null)
			{
				return;
			}
			AgentActor tutorialAgent = Singleton<Manager.Map>.Instance.TutorialAgent;
			this._player.CommCompanion = tutorialAgent;
			this._player.PlayerController.ChangeState("Communication");
			if (tutorialAgent != null)
			{
				Transform transform = this._player.CameraControl.CameraComponent.transform;
				tutorialAgent.SetLookPtn(1, 3);
				tutorialAgent.SetLookTarget(1, 0, transform);
				this.openData.FindLoad("0", tutorialAgent.AgentData.param.charaID, 100);
				this.packData = new OpeningWakeUp.PackData();
				this.packData.Init();
				this.packData.SetParam(new IParams[]
				{
					tutorialAgent.AgentData,
					this._player.PlayerData
				});
				this.packData.onComplete = delegate()
				{
					this.EndADV();
					this.packData.Release();
					this.packData = null;
				};
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => this._player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				});
			}
		}

		// Token: 0x06006F2D RID: 28461 RVA: 0x002FAEC0 File Offset: 0x002F92C0
		private void EndADV()
		{
			AgentActor tutorialAgent = Singleton<Manager.Map>.Instance.TutorialAgent;
			if (tutorialAgent != null)
			{
				tutorialAgent.ClearItems();
				Transform transform = this._player.CameraControl.CameraComponent.transform;
				tutorialAgent.SetLookPtn(0, 3);
				tutorialAgent.SetLookTarget(0, 0, null);
			}
			MapUIContainer.SetVisibleHUD(true);
			if (this._player.CameraControl.ShotType == ShotType.PointOfView)
			{
				this._player.CameraControl.XAxisValue = this._player.Rotation.y;
				this._player.CameraControl.YAxisValue = 0.5f;
			}
			else
			{
				this._player.CameraControl.XAxisValue = this._player.Rotation.eulerAngles.y - 30f;
				this._player.CameraControl.YAxisValue = 0.6f;
			}
			SoundPlayer instance = Singleton<SoundPlayer>.Instance;
			instance.StartAllSubscribe();
			instance.ActivateWideEnvSE(true);
			this._player.CameraControl.Mode = CameraMode.Normal;
			Manager.Map.SetTutorialProgress(1);
			this._player.PlayerController.ChangeState("Idle");
			this._agent.ChangeTutorialBehavior(Tutorial.ActionType.HeadToSandBeach);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			Observable.EveryUpdate().Skip(1).SkipWhile((long _) => this._player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
			{
				this._player.PlayerController.ChangeState("Normal");
				if (Singleton<MapScene>.IsInstance())
				{
					MapScene instance2 = Singleton<MapScene>.Instance;
					instance2.SaveProfile(true);
					instance2.SaveProfile(false);
				}
				if (Singleton<Game>.IsInstance())
				{
					Singleton<Game>.Instance.SaveGlobalData();
				}
			});
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x002FB041 File Offset: 0x002F9441
		protected override void OnRelease(PlayerActor player)
		{
			if (this._fadeTimerDisposable != null)
			{
				this._fadeTimerDisposable.Dispose();
			}
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x002FB05C File Offset: 0x002F945C
		private void Elapsed(PlayerActor player)
		{
			this._player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = this._prevStyle;
			Vector3 position = this._eventCamera.transform.position;
			player.CameraControl.EventCameraLocator.runtimeAnimatorController = null;
			this._eventCamera.transform.position = position;
			this.StartADV();
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x002FB0C4 File Offset: 0x002F94C4
		private void PlayCameraAnimation(Animator animator, int personalID)
		{
			this._inQueue.Clear();
			Dictionary<int, string[]> openingWakeUpCameraInStates = Singleton<Manager.Resources>.Instance.CommonDefine.OpeningWakeUpCameraInStates;
			string[] array;
			if (!openingWakeUpCameraInStates.TryGetValue(personalID, out array) && !openingWakeUpCameraInStates.TryGetValue(0, out array))
			{
				return;
			}
			if (array.IsNullOrEmpty<string>())
			{
				return;
			}
			foreach (string name_ in array)
			{
				PlayState.Info item = new PlayState.Info(name_, 0);
				this._inQueue.Enqueue(item);
			}
			animator.speed = 1f;
			this._cameraAnimEnumerator = this.StartCameraAnimation(animator);
			this._cameraAnimDisposable = Observable.FromCoroutine((CancellationToken _) => this._cameraAnimEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x002FB17F File Offset: 0x002F957F
		private void StopCameraAnimation()
		{
			if (this._cameraAnimDisposable != null)
			{
				this._cameraAnimDisposable.Dispose();
				this._cameraAnimEnumerator = null;
			}
		}

		// Token: 0x06006F32 RID: 28466 RVA: 0x002FB1A0 File Offset: 0x002F95A0
		private IEnumerator StartCameraAnimation(Animator animator)
		{
			Queue<PlayState.Info> queue = this._inQueue;
			while (0 < queue.Count)
			{
				PlayState.Info state = queue.Dequeue();
				animator.Play(state.stateName, state.layer, 0f);
				yield return null;
				yield return null;
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
				{
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this._cameraAnimEnumerator = null;
			yield break;
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06006F33 RID: 28467 RVA: 0x002FB1C2 File Offset: 0x002F95C2
		public bool PlayingCameraAnimation
		{
			get
			{
				return this._cameraAnimEnumerator != null;
			}
		}

		// Token: 0x04005BED RID: 23533
		private PlayerActor _player;

		// Token: 0x04005BEE RID: 23534
		private AgentActor _agent;

		// Token: 0x04005BEF RID: 23535
		private CinemachineVirtualCameraBase _eventCamera;

		// Token: 0x04005BF0 RID: 23536
		private bool _fadeEnd;

		// Token: 0x04005BF1 RID: 23537
		private CinemachineBlendDefinition.Style _prevStyle;

		// Token: 0x04005BF2 RID: 23538
		private IDisposable _fadeTimerDisposable;

		// Token: 0x04005BF3 RID: 23539
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005BF4 RID: 23540
		private bool _isFinish;

		// Token: 0x04005BF7 RID: 23543
		private Queue<PlayState.Info> _inQueue = new Queue<PlayState.Info>();

		// Token: 0x04005BF8 RID: 23544
		private IEnumerator _cameraAnimEnumerator;

		// Token: 0x04005BF9 RID: 23545
		private IDisposable _cameraAnimDisposable;

		// Token: 0x02000E09 RID: 3593
		private class PackData : CharaPackData
		{
		}
	}
}
