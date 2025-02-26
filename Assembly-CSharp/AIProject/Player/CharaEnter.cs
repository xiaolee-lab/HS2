using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ADV;
using AIProject.Definitions;
using AIProject.SaveData;
using Cinemachine;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Player
{
	// Token: 0x02000DE8 RID: 3560
	public class CharaEnter : PlayerStateBase
	{
		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06006E13 RID: 28179 RVA: 0x002F1258 File Offset: 0x002EF658
		private OpenData openData { get; } = new OpenData();

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06006E14 RID: 28180 RVA: 0x002F1260 File Offset: 0x002EF660
		private PackData packData { get; } = new PackData();

		// Token: 0x06006E15 RID: 28181 RVA: 0x002F1268 File Offset: 0x002EF668
		protected override void OnAwake(PlayerActor player)
		{
			player.EventKey = (EventType)0;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			this._onEndFadeIn.Take(1).Subscribe(delegate(Unit _)
			{
				this.Refresh(player);
				Observable.Timer(TimeSpan.FromMilliseconds(100.0)).Subscribe(delegate(long __)
				{
					this._completeWait = true;
					MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 1f, false).Subscribe(delegate(Unit ___)
					{
					}, delegate()
					{
						this._onEndFadeOut.OnNext(Unit.Default);
					});
				});
			});
			this._onEndFadeOut.Take(1).Subscribe(delegate(Unit _)
			{
				this.StartEventSeq(player);
			});
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			this._prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			IObservable<Unit> source = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true);
			source.Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				this._onEndFadeIn.OnNext(Unit.Default);
			});
		}

		// Token: 0x06006E16 RID: 28182 RVA: 0x002F1368 File Offset: 0x002EF768
		private int GetAppearCameraAnimatorID(int personalID)
		{
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			int result;
			if (!commonDefine.AppearCameraAnimatorIDDic.TryGetValue(personalID, out result) && !commonDefine.AppearCameraAnimatorIDDic.TryGetValue(0, out result))
			{
				return commonDefine.ItemAnims.AppearCameraAnimatorID;
			}
			return result;
		}

		// Token: 0x06006E17 RID: 28183 RVA: 0x002F13B4 File Offset: 0x002EF7B4
		protected override void OnRelease(PlayerActor player)
		{
			player.CameraControl.EventCameraLocator.runtimeAnimatorController = null;
			player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = this._prevStyle;
			player.CameraControl.Mode = CameraMode.Normal;
			player.ChaControl.visibleAll = true;
			if (this._agent != null)
			{
				AssetBundleInfo assetBundleInfo = default(AssetBundleInfo);
				RuntimeAnimatorController charaAnimator = Singleton<Manager.Resources>.Instance.Animation.GetCharaAnimator(0, ref assetBundleInfo);
				this._agent.ChangeAnimator(charaAnimator);
				if (!this._registeredBefore)
				{
					this._agent.RefreshWalkStatus(Singleton<Manager.Map>.Instance.PointAgent.Waypoints);
					Singleton<Manager.Map>.Instance.InitSearchActorTargets(this._agent);
					player.PlayerController.CommandArea.AddCommandableObject(this._agent);
					foreach (KeyValuePair<int, AgentActor> keyValuePair in Singleton<Manager.Map>.Instance.AgentTable)
					{
						if (!(keyValuePair.Value == this._agent))
						{
							keyValuePair.Value.AddActor(this._agent);
						}
					}
				}
				this._agent.ClearItems();
				this._agent.ClearParticles();
				this._agent.ActivateNavMeshAgent();
				this._agent.EnableBehavior();
				this._agent.ChangeBehavior(Desire.ActionType.Normal);
			}
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
		}

		// Token: 0x06006E18 RID: 28184 RVA: 0x002F155C File Offset: 0x002EF95C
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.FadeCanvas.IsFadeIn)
			{
				return;
			}
			if (!this._completeWait)
			{
				return;
			}
			if (MapUIContainer.FadeCanvas.IsFadeOut)
			{
				return;
			}
			DevicePoint currentDevicePoint = player.CurrentDevicePoint;
			if (this._agent != null && (this._agent.Animation.PlayingInLocoAnimation || currentDevicePoint.PlayingInAnimation || this.PlayingCameraAnimation))
			{
				return;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006E19 RID: 28185 RVA: 0x002F160F File Offset: 0x002EFA0F
		private void Elapsed(PlayerActor player)
		{
			player.CurrentDevicePoint = null;
			MapUIContainer.SetVisibleHUD(true);
			MapUIContainer.StorySupportUI.Open();
			player.PlayerController.CommandArea.UpdateCollision(player);
			player.Controller.ChangeState("Normal");
		}

		// Token: 0x06006E1A RID: 28186 RVA: 0x002F164C File Offset: 0x002EFA4C
		private void Refresh(PlayerActor player)
		{
			int id = player.CurrentDevicePoint.ID;
			AgentData agentData = Singleton<Game>.Instance.WorldData.AgentTable[id];
			MapUIContainer.SetVisibleHUD(false);
			AgentActor agentActor = Singleton<Manager.Map>.Instance.AddAgent(id, agentData);
			agentActor.DisableBehavior();
			Actor.BehaviorSchedule schedule = agentActor.Schedule;
			schedule.enabled = false;
			agentActor.Schedule = schedule;
			agentActor.TargetInSightActor = null;
			agentActor.DeactivateNavMeshAgent();
			if (agentActor.CurrentPoint != null)
			{
				agentActor.CurrentPoint.SetActiveMapItemObjs(true);
				agentActor.CurrentPoint.ReleaseSlot(agentActor);
				agentActor.CurrentPoint = null;
			}
			agentActor.TargetInSightActionPoint = null;
			player.ChaControl.visibleAll = false;
			Transform pivotPoint = player.CurrentDevicePoint.PivotPoint;
			agentActor.Position = pivotPoint.position;
			agentActor.Rotation = pivotPoint.rotation;
			agentData.Position = player.CurrentDevicePoint.RecoverPoints[0].position;
			agentData.Rotation = player.CurrentDevicePoint.RecoverPoints[0].rotation;
			Animator animator = this._locator = player.CameraControl.EventCameraLocator;
			animator.transform.position = pivotPoint.position;
			animator.transform.rotation = pivotPoint.rotation;
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			RuntimeAnimatorController itemAnimator = Singleton<Manager.Resources>.Instance.Animation.GetItemAnimator(this.GetAppearCameraAnimatorID(agentActor.ChaControl.fileParam.personality));
			animator.runtimeAnimatorController = itemAnimator;
			animator.transform.position = pivotPoint.position;
			animator.transform.rotation = pivotPoint.rotation;
			animator.speed = 0f;
			animator.Play(commonDefine.AppearCameraInStates[agentActor.ChaControl.fileParam.personality][0]);
			this._eventCamera = player.CameraControl.EventCamera.transform;
			this._eventCamera.transform.SetParent(player.CameraControl.EventCameraParent, false);
			this._eventCamera.transform.localPosition = Vector3.zero;
			this._eventCamera.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			player.SetActiveOnEquipedItem(false);
			player.CameraControl.Mode = CameraMode.Event;
			Transform playerRecoverPoint = player.CurrentDevicePoint.PlayerRecoverPoint;
			if (playerRecoverPoint != null)
			{
				player.NavMeshAgent.Warp(playerRecoverPoint.position);
				player.Rotation = playerRecoverPoint.rotation;
			}
			this._agent = agentActor;
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x06006E1B RID: 28187 RVA: 0x002F18F4 File Offset: 0x002EFCF4
		private void StartEventSeq(PlayerActor player)
		{
			if (this._agent != null)
			{
				DevicePoint devicePoint = player.CurrentDevicePoint;
				int id = devicePoint.ID;
				AgentData agentData = Singleton<Game>.Instance.WorldData.AgentTable[id];
				AgentActor agent = this._agent;
				if (!agentData.PlayEnterScene)
				{
					agentData.PlayEnterScene = true;
					int personality = agent.ChaControl.fileParam.personality;
					PoseKeyPair poseKeyPair = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.AppearIDList[personality];
					PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[poseKeyPair.postureID][poseKeyPair.poseID];
					ActorAnimInfo actorAnimInfo = new ActorAnimInfo
					{
						layer = playState.Layer,
						inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
						inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
						outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
						outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
						directionType = playState.DirectionType,
						endEnableBlend = playState.EndEnableBlend,
						endBlendSec = playState.EndBlendRate
					};
					agent.Animation.AnimInfo = actorAnimInfo;
					ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
					AssetBundleInfo assetBundleInfo = playState.MainStateInfo.AssetBundleInfo;
					agent.ChangeAnimator(assetBundleInfo.assetbundle, assetBundleInfo.asset);
					float shapeBodyValue = agent.ChaControl.GetShapeBodyValue(0);
					string heightParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
					agent.Animation.Animator.SetFloat(heightParameterName, shapeBodyValue);
					this._locator.SetFloat(heightParameterName, shapeBodyValue);
					agent.Animation.LoadEventKeyTable(poseKeyPair.postureID, poseKeyPair.poseID);
					agent.LoadEventItems(playState);
					agent.LoadEventParticles(poseKeyPair.postureID, poseKeyPair.poseID);
					agent.Animation.InitializeStates(playState.MainStateInfo.InStateInfo.StateInfos, playState.MainStateInfo.OutStateInfo.StateInfos, playState.MainStateInfo.AssetBundleInfo);
					Transform basePoint = devicePoint.PivotPoint;
					agent.Position = basePoint.position;
					agent.Rotation = basePoint.rotation;
					agent.Animation.PlayInLocoAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.layer);
					devicePoint.PlayInAnimation();
					this._locator.speed = 1f;
					this.PlayCameraAnimation(this._locator, personality);
					this._onEndAction.Take(1).Subscribe(delegate(Unit __)
					{
						this.packData.Init();
						this.openData.FindLoad("0", agent.AgentData.param.charaID, 6);
						this.packData.SetParam(new IParams[]
						{
							agent.AgentData,
							player.PlayerData
						});
						this.packData.onComplete = delegate()
						{
							if (agent.IsEvent)
							{
								agent.IsEvent = false;
							}
							agent.AgentData.ResetAssignedDuration();
							int randomValue = Singleton<Manager.Resources>.Instance.AgentProfile.DayRandElapseCheck.RandomValue;
							agent.AgentData.SetADVEventTimeCond(randomValue);
							agent.AgentData.ResetADVEventTimeCount();
							if (!Config.GraphicData.CharasEntry[agent.ID])
							{
								this.packData.Release();
								Singleton<ADV>.Instance.Captions.EndADV(null);
								devicePoint.PlayOutAnimation();
								this.PlayCloseSE(player, basePoint);
								MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true).Subscribe(delegate(Unit _)
								{
								}, delegate()
								{
									Singleton<Manager.Map>.Instance.RemoveAgent(agent);
									agent = null;
									MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 1f, true).Subscribe(delegate(Unit _)
									{
									}, delegate()
									{
										this.Elapsed(player);
									});
								});
							}
							else
							{
								this.packData.Release();
								Singleton<ADV>.Instance.Captions.EndADV(null);
								this.Elapsed(player);
								devicePoint.PlayOutAnimation();
								this.PlayCloseSE(player, basePoint);
							}
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					});
				}
			}
			else
			{
				this._onEndAction.Take(1).Subscribe(delegate(Unit __)
				{
					player.CurrentDevicePoint = null;
					player.PlayerController.CommandArea.UpdateCollision(player);
					player.Controller.ChangeState("Normal");
				});
			}
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x002F1C7C File Offset: 0x002F007C
		private void PlayCameraAnimation(Animator animator, int personality)
		{
			this._inQueue.Clear();
			string[] array = Singleton<Manager.Resources>.Instance.CommonDefine.AppearCameraInStates[personality];
			foreach (string name_ in array)
			{
				PlayState.Info item = new PlayState.Info(name_, 0);
				this._inQueue.Enqueue(item);
			}
			this._cameraAnimEnumerator = this.StartCameraAnimation(animator);
			this._cameraAnimDisposable = Observable.FromCoroutine((CancellationToken _) => this._cameraAnimEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x002F1D04 File Offset: 0x002F0104
		private void StopCameraAnimation()
		{
			if (this._cameraAnimDisposable != null)
			{
				this._cameraAnimDisposable.Dispose();
				this._cameraAnimEnumerator = null;
			}
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x002F1D24 File Offset: 0x002F0124
		private IEnumerator StartCameraAnimation(Animator animator)
		{
			Queue<PlayState.Info> queue = this._inQueue;
			while (queue.Count > 0)
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

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06006E1F RID: 28191 RVA: 0x002F1D46 File Offset: 0x002F0146
		public bool PlayingCameraAnimation
		{
			get
			{
				return this._cameraAnimEnumerator != null;
			}
		}

		// Token: 0x06006E20 RID: 28192 RVA: 0x002F1D54 File Offset: 0x002F0154
		private void PlayCloseSE(PlayerActor player, Transform playRoot)
		{
			if (player == null)
			{
				return;
			}
			if (Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			if (playRoot == null)
			{
				playRoot = player.Locomotor.transform;
			}
			Dictionary<DoorMatType, SoundPack.DoorSEIDInfo> doorIDTable = Singleton<Manager.Resources>.Instance.SoundPack.DoorIDTable;
			DoorMatType key = DoorMatType.Capsule;
			SoundPack.DoorSEIDInfo doorSEIDInfo;
			if (doorIDTable.TryGetValue(key, out doorSEIDInfo))
			{
				AudioSource audioSource = Singleton<Manager.Resources>.Instance.SoundPack.Play(doorSEIDInfo.CloseID, Sound.Type.GameSE3D, 0f);
				audioSource.Stop();
				audioSource.transform.SetPositionAndRotation(playRoot.position, playRoot.rotation);
				audioSource.Play();
			}
		}

		// Token: 0x04005B7A RID: 23418
		protected int _currentState = -1;

		// Token: 0x04005B7B RID: 23419
		private Subject<Unit> _onEndFadeIn = new Subject<Unit>();

		// Token: 0x04005B7C RID: 23420
		private Subject<Unit> _onEndFadeOut = new Subject<Unit>();

		// Token: 0x04005B7D RID: 23421
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005B7E RID: 23422
		private AgentActor _agent;

		// Token: 0x04005B7F RID: 23423
		private Transform _eventCamera;

		// Token: 0x04005B80 RID: 23424
		private Animator _locator;

		// Token: 0x04005B81 RID: 23425
		private bool _registeredBefore;

		// Token: 0x04005B82 RID: 23426
		private bool _completeWait;

		// Token: 0x04005B85 RID: 23429
		private CinemachineBlendDefinition.Style _prevStyle;

		// Token: 0x04005B86 RID: 23430
		private Queue<PlayState.Info> _inQueue = new Queue<PlayState.Info>();

		// Token: 0x04005B87 RID: 23431
		private IEnumerator _cameraAnimEnumerator;

		// Token: 0x04005B88 RID: 23432
		private IDisposable _cameraAnimDisposable;
	}
}
