using System;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000D20 RID: 3360
	[TaskCategory("")]
	public class StartEventADV : AgentAction
	{
		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06006B6D RID: 27501 RVA: 0x002E08EA File Offset: 0x002DECEA
		private OpenData openData { get; } = new OpenData();

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06006B6E RID: 27502 RVA: 0x002E08F2 File Offset: 0x002DECF2
		private PackData packData { get; } = new PackData();

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06006B6F RID: 27503 RVA: 0x002E08FA File Offset: 0x002DECFA
		private int charaID
		{
			[CompilerGenerated]
			get
			{
				return base.Agent.AgentData.param.charaID;
			}
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x002E0914 File Offset: 0x002DED14
		public override void OnStart()
		{
			base.OnStart();
			Singleton<MapUIContainer>.Instance.MinimapUI.ChangeCamera(false, false);
			MapUIContainer.SetVisibleHUD(false);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			AgentActor agent = base.Agent;
			this._prevPartner = agent.CommandPartner;
			agent.CommandPartner = agent.TargetInSightActor;
			agent.TargetInSightActor = null;
			agent.ChaControl.ChangeLookEyesTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
			agent.ChaControl.ChangeLookEyesPtn(1);
			agent.ChaControl.ChangeLookNeckPtn(3, 1f);
			agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			ActorCameraControl cameraControl = Singleton<Manager.Map>.Instance.Player.CameraControl;
			cameraControl.OnCameraBlended = delegate()
			{
				Observable.EveryUpdate().SkipWhile((long _) => agent.Animation.PlayingTurnAnimation).SkipWhile((long _) => !Singleton<ADV>.IsInstance() || Singleton<ADV>.Instance.Captions.IsProcEndADV).Take(1).Subscribe(delegate(long _)
				{
					this.OpenADV(player, agent);
				});
			};
			player.CommCompanion = agent;
			player.Controller.ChangeState("Communication");
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair poseKeyPair = Singleton<Manager.Resources>.Instance.AgentProfile.ADVIdleTable[agent.ChaControl.fileParam.personality];
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[poseKeyPair.postureID][poseKeyPair.poseID];
			AssetBundleInfo assetBundleInfo = playState.MainStateInfo.AssetBundleInfo;
			agent.ChangeAnimator(assetBundleInfo.assetbundle, assetBundleInfo.asset);
			agent.Animation.PlayTurnAnimation(player.Position, 1f, playState.MainStateInfo.InStateInfo, false);
			agent.DisableBehavior();
			ADV.ChangeADVCamera(agent);
			MapUIContainer.SetVisibleHUD(false);
			Singleton<ADV>.Instance.TargetCharacter = agent;
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x002E0B50 File Offset: 0x002DEF50
		private void OpenADV(PlayerActor player, AgentActor agent)
		{
			this.packData.Init();
			int eventID = this._eventID;
			this.openData.FindLoad(string.Format("{0}", eventID), this.charaID, 2);
			CharaPackData packData = this.packData;
			ICommandData[] array = new ICommandData[1];
			int num = 0;
			Game instance = Singleton<Game>.Instance;
			ICommandData commandData;
			if (instance == null)
			{
				commandData = null;
			}
			else
			{
				WorldData worldData = instance.WorldData;
				commandData = ((worldData != null) ? worldData.Environment : null);
			}
			array[num] = commandData;
			packData.SetCommandData(array);
			CharaPackData packData2 = this.packData;
			IParams[] array2 = new IParams[2];
			array2[0] = agent.AgentData;
			int num2 = 1;
			Game instance2 = Singleton<Game>.Instance;
			IParams @params;
			if (instance2 == null)
			{
				@params = null;
			}
			else
			{
				WorldData worldData2 = instance2.WorldData;
				@params = ((worldData2 != null) ? worldData2.PlayerData : null);
			}
			array2[num2] = @params;
			packData2.SetParam(array2);
			int eventID2 = this._eventID;
			if (eventID2 == 1)
			{
				bool flag = player.IsBirthday(agent);
				this.packData.isBirthday = flag;
				if (flag)
				{
					agent.AgentData.IsPlayerForBirthdayEvent = true;
				}
			}
			this.packData.onComplete = delegate()
			{
				if (agent.IsEvent)
				{
					agent.IsEvent = false;
				}
				agent.EnableBehavior();
				switch (this._eventID)
				{
				case 0:
				{
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					this.Complete();
					int desireKey = Desire.GetDesireKey(Desire.Type.Lonely);
					agent.SetDesire(desireKey, 0f);
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Game);
					agent.SetDesire(desireKey2, 0f);
					agent.ChangeBehavior(Desire.ActionType.Normal);
					break;
				}
				case 1:
				{
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					this.Complete();
					int desireKey3 = Desire.GetDesireKey(Desire.Type.Gift);
					agent.SetDesire(desireKey3, 0f);
					agent.ChangeBehavior(Desire.ActionType.Normal);
					break;
				}
				case 2:
				{
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					this.Complete();
					int desireKey4 = Desire.GetDesireKey(Desire.Type.Want);
					agent.SetDesire(desireKey4, 0f);
					agent.ChangeBehavior(Desire.ActionType.Normal);
					break;
				}
				case 3:
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					if (this.packData.isSuccessH)
					{
						if (player.ChaControl.sex == 1 && !player.ChaControl.fileParam.futanari)
						{
							string[] lesTypeHMeshTag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.LesTypeHMeshTag;
							if (agent.CanSelectHCommand(lesTypeHMeshTag))
							{
								Singleton<HSceneManager>.Instance.nInvitePtn = 0;
								agent.InitiateHScene(HSceneManager.HEvent.Normal);
							}
							else
							{
								this.ChangeTakeMode(agent, player, false);
							}
						}
						else if (agent.CanSelectHCommand())
						{
							Singleton<HSceneManager>.Instance.nInvitePtn = 0;
							agent.InitiateHScene(HSceneManager.HEvent.Normal);
						}
						else
						{
							this.ChangeTakeMode(agent, player, false);
						}
					}
					else if (agent.CanMasturbation)
					{
						MapUIContainer.SetVisibleHUD(true);
						Desire.ActionType mode = player.Mode;
						if (mode == Desire.ActionType.Normal || mode == Desire.ActionType.Date)
						{
							player.CameraControl.Mode = CameraMode.Normal;
							player.Controller.ChangeState("Normal");
							player.CameraControl.OnCameraBlended = delegate()
							{
								Singleton<Manager.Map>.Instance.Player.ChaControl.visibleAll = (player.CameraControl.ShotType != ShotType.PointOfView);
							};
						}
						agent.Animation.ResetDefaultAnimatorController();
						agent.CommandPartner = this._prevPartner;
						int desireKey5 = Desire.GetDesireKey(Desire.Type.H);
						agent.AddMotivation(desireKey5, 50f);
						agent.ChangeBehavior(Desire.ActionType.SearchMasturbation);
					}
					else
					{
						this.Complete();
						int desireKey6 = Desire.GetDesireKey(Desire.Type.H);
						agent.SetDesire(desireKey6, 0f);
						int desireKey7 = Desire.GetDesireKey(Desire.Type.Lonely);
						agent.SetDesire(desireKey7, 0f);
						agent.ChangeBehavior(Desire.ActionType.Normal);
						MapUIContainer.SetVisibleHUD(true);
					}
					break;
				case 4:
					break;
				case 5:
				{
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					string[] floorTypeHMeshTag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.FloorTypeHMeshTag;
					bool flag2 = agent.CanSelectHCommand(floorTypeHMeshTag);
					if (flag2)
					{
						agent.InitiateHScene(HSceneManager.HEvent.FromFemale);
					}
					else
					{
						this.ChangeTakeMode(agent, player, true);
					}
					break;
				}
				case 6:
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					if (this.packData.isSuccessH)
					{
						this.ChangeAbductMode(agent, player, Desire.ActionType.TakeSleepPoint);
					}
					else
					{
						this.Complete();
						int desireKey8 = Desire.GetDesireKey(Desire.Type.Lonely);
						agent.SetDesire(desireKey8, 0f);
						agent.ChangeBehavior(Desire.ActionType.Normal);
						MapUIContainer.SetVisibleHUD(true);
					}
					break;
				case 7:
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					agent.FromFemale = true;
					this.ChangeAbductMode(agent, player, Desire.ActionType.TakeSleepHPoint);
					break;
				case 8:
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					if (this.packData.isSuccessH)
					{
						this.ChangeAbductMode(agent, player, Desire.ActionType.TakeEatPoint);
					}
					else
					{
						this.Complete();
						int desireKey9 = Desire.GetDesireKey(Desire.Type.Lonely);
						agent.SetDesire(desireKey9, 0f);
						agent.ChangeBehavior(Desire.ActionType.Normal);
						MapUIContainer.SetVisibleHUD(true);
					}
					break;
				case 9:
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					if (this.packData.isSuccessH)
					{
						this.ChangeAbductMode(agent, player, Desire.ActionType.TakeBreakPoint);
					}
					else
					{
						this.Complete();
						int desireKey10 = Desire.GetDesireKey(Desire.Type.Lonely);
						agent.SetDesire(desireKey10, 0f);
						agent.ChangeBehavior(Desire.ActionType.Normal);
						MapUIContainer.SetVisibleHUD(true);
					}
					break;
				default:
					agent.EnableBehavior();
					agent.ChaControl.ChangeLookEyesPtn(0);
					agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookNeckPtn(3, 1f);
					agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					break;
				}
				this.packData.Release();
				Singleton<ADV>.Instance.Captions.EndADV(null);
			};
			this.packData.restoreCommands = null;
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}

		// Token: 0x06006B72 RID: 27506 RVA: 0x002E0CB3 File Offset: 0x002DF0B3
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}

		// Token: 0x06006B73 RID: 27507 RVA: 0x002E0CB8 File Offset: 0x002DF0B8
		private void Complete()
		{
			MapUIContainer.SetVisibleHUD(true);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			Desire.ActionType mode = player.Mode;
			if (mode == Desire.ActionType.Normal || mode == Desire.ActionType.Date)
			{
				player.CameraControl.Mode = CameraMode.Normal;
				player.Controller.ChangeState("Normal");
				player.CameraControl.OnCameraBlended = delegate()
				{
					Singleton<Manager.Map>.Instance.Player.ChaControl.visibleAll = (player.CameraControl.ShotType != ShotType.PointOfView);
				};
			}
			base.Agent.Animation.ResetDefaultAnimatorController();
			if (this._prevPartner == player)
			{
				base.Agent.CommandPartner = null;
			}
			else
			{
				base.Agent.CommandPartner = this._prevPartner;
			}
			MapUIContainer.SetVisibleHUD(true);
		}

		// Token: 0x06006B74 RID: 27508 RVA: 0x002E0D94 File Offset: 0x002DF194
		private void ChangeTakeMode(AgentActor agent, PlayerActor player, bool fromFemale)
		{
			agent.FromFemale = fromFemale;
			this.ChangeAbductMode(agent, player, Desire.ActionType.TakeHPoint);
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x002E0DA8 File Offset: 0x002DF1A8
		private void ChangeAbductMode(AgentActor agent, PlayerActor player, Desire.ActionType mode)
		{
			player.CameraControl.Mode = CameraMode.Normal;
			player.CameraControl.OnCameraBlended = delegate()
			{
				Singleton<Manager.Map>.Instance.Player.ChaControl.visibleAll = (player.CameraControl.ShotType != ShotType.PointOfView);
			};
			agent.Animation.ResetDefaultAnimatorController();
			agent.Partner = player;
			if (player.Partner != null)
			{
				AgentActor agentActor = player.Partner as AgentActor;
				if (agentActor != null)
				{
					agentActor.DeactivatePairing(0);
				}
			}
			player.Partner = agent;
			player.PlayerController.ChangeState("Follow");
			Observable.EveryUpdate().SkipWhile((long _) => MapUIContainer.ChoiceUI.IsActiveControl).Take(1).Subscribe(delegate(long _)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			});
			agent.DestPosition = null;
			agent.ChangeBehavior(mode);
		}

		// Token: 0x04005A85 RID: 23173
		[SerializeField]
		private int _eventID;

		// Token: 0x04005A86 RID: 23174
		private Actor _prevPartner;
	}
}
