using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ADV;
using AIChara;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.RootMotion;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI;
using AIProject.UI.Viewer;
using Cinemachine;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using Manager;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C4B RID: 3147
	public class AgentActor : Actor, ICommandable, INavMeshActor
	{
		// Token: 0x0600620A RID: 25098 RVA: 0x00290A58 File Offset: 0x0028EE58
		public bool IsBadMood()
		{
			if (base.ChaControl.fileGameInfo.phase < 2)
			{
				if (base.ChaControl.fileGameInfo.moodBound.lower >= base.ChaControl.fileGameInfo.moodBound.upper)
				{
					this.AgentData.StatsTable[1] = base.ChaControl.fileGameInfo.moodBound.lower + 1f;
				}
				else
				{
					this.AgentData.StatsTable[1] = (base.ChaControl.fileGameInfo.moodBound.lower + base.ChaControl.fileGameInfo.moodBound.upper) / 2f;
				}
			}
			return this.AgentData.StatsTable[1] <= base.ChaControl.fileGameInfo.moodBound.lower;
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x00290B48 File Offset: 0x0028EF48
		public bool IsGoodMood()
		{
			if (base.ChaControl.fileGameInfo.phase < 2)
			{
				this.AgentData.StatsTable[1] = (base.ChaControl.fileGameInfo.moodBound.lower + base.ChaControl.fileGameInfo.moodBound.upper) / 2f;
			}
			return this.AgentData.StatsTable[1] > base.ChaControl.fileGameInfo.moodBound.upper;
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600620C RID: 25100 RVA: 0x00290BD5 File Offset: 0x0028EFD5
		public int charaID
		{
			[CompilerGenerated]
			get
			{
				return this.AgentData.param.charaID;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x0600620D RID: 25101 RVA: 0x00290BE7 File Offset: 0x0028EFE7
		public bool IsAdvEvent
		{
			[CompilerGenerated]
			get
			{
				return !this.advEventName.IsNullOrEmpty() || this.CheckFishingEvent();
			}
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x00290C02 File Offset: 0x0028F002
		public bool CheckEventADV(int eventType)
		{
			this.advEventParam = this.GetAdvEvent(eventType);
			AgentAdvEventInfo.Param advEventParam = this.advEventParam;
			this.advEventName = ((advEventParam != null) ? advEventParam.FileName : null);
			return !this.advEventName.IsNullOrEmpty();
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x0600620F RID: 25103 RVA: 0x00290C3A File Offset: 0x0028F03A
		// (set) Token: 0x06006210 RID: 25104 RVA: 0x00290C42 File Offset: 0x0028F042
		private string advEventName { get; set; }

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06006211 RID: 25105 RVA: 0x00290C4B File Offset: 0x0028F04B
		// (set) Token: 0x06006212 RID: 25106 RVA: 0x00290C53 File Offset: 0x0028F053
		private AgentAdvEventInfo.Param advEventParam { get; set; }

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06006213 RID: 25107 RVA: 0x00290C5C File Offset: 0x0028F05C
		public OpenData openData { get; } = new OpenData();

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06006214 RID: 25108 RVA: 0x00290C64 File Offset: 0x0028F064
		// (set) Token: 0x06006215 RID: 25109 RVA: 0x00290C6C File Offset: 0x0028F06C
		public AgentActor.PackData packData { get; private set; }

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x00290C78 File Offset: 0x0028F078
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				if (this.TutorialMode)
				{
					switch (this.TutorialType)
					{
					case AIProject.Definitions.Tutorial.ActionType.PassFishingRod:
						return this._tutorialPassFishingRodLabels;
					case AIProject.Definitions.Tutorial.ActionType.FoodRequest:
						return this._tutorialFoodRequestLabels;
					case AIProject.Definitions.Tutorial.ActionType.WaitAtBase:
						return this._tutorialWaitAtBaseLabels;
					case AIProject.Definitions.Tutorial.ActionType.GrilledFishRequest:
						return this._tutorialGrilledFishRequestLabels;
					}
					return null;
				}
				if (!Singleton<Manager.Map>.IsInstance() || !(Singleton<Manager.Map>.Instance.Player.PlayerController.State is Normal))
				{
					return null;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				Desire.ActionType mode = this.Mode;
				if (mode == Desire.ActionType.Date)
				{
					return this.DateLabels;
				}
				if (mode == Desire.ActionType.Onbu)
				{
					return null;
				}
				if (this.CheckFishingEvent())
				{
					return this._pEventFishingLabels;
				}
				if (this.CheckEventADV(1))
				{
					return this._pEventLabels;
				}
				State.Type stateType = this.StateType;
				if (stateType == State.Type.Collapse)
				{
					return this._collapseLabels;
				}
				if (stateType != State.Type.Cold)
				{
					if (stateType != State.Type.Immobility)
					{
						if (this.IsSpecial)
						{
							if (player.ChaControl.sex == 1 && !player.ChaControl.fileParam.futanari)
							{
								if (this.HPositionID != 1)
								{
									return null;
								}
								if (this.Mode == Desire.ActionType.EndTaskSecondSleep || this.Mode == Desire.ActionType.ShallowSleep)
								{
									return null;
								}
								return this._sleepLabels;
							}
							else
							{
								switch (this.HPositionID)
								{
								case 1:
									if (this.Mode == Desire.ActionType.EndTaskSecondSleep || this.Mode == Desire.ActionType.ShallowSleep)
									{
										return null;
									}
									return this._sleepLabels;
								case 2:
									return this._bathLabels;
								case 3:
									return this._isuToiletLabels;
								case 4:
									return this._shagamiLabels;
								case 5:
									return this._shagamiFoundLabels;
								case 6:
									return this._searchLabels;
								case 7:
									return this._cookLabels;
								case 8:
									return this._standLabels;
								case 13:
									return this._mapBathLabels;
								case 14:
									return this._holeLabels;
								case 16:
									return this._sleepOnanismLabels;
								case 17:
									return this._deskBackLabels;
								}
							}
						}
						else
						{
							if (this.CheckEventADV(0))
							{
								return this._pEventLabels;
							}
							if (this.IsStandby)
							{
								return this._eventLabels;
							}
							if (this.CanTalk)
							{
								return this._labels;
							}
						}
						return null;
					}
					return null;
				}
				else
				{
					if (!this.AgentData.SickState.UsedMedicine && !this.AgentData.SickState.Enabled)
					{
						return this._coldLabels;
					}
					return null;
				}
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x00290F24 File Offset: 0x0028F324
		public CommandLabel.CommandInfo[] DateLabels
		{
			get
			{
				State.Type stateType = this.StateType;
				if (stateType == State.Type.Collapse)
				{
					return null;
				}
				if (stateType != State.Type.Cold)
				{
					if (stateType == State.Type.Immobility)
					{
						return null;
					}
					if (this.IsSpecial)
					{
						return null;
					}
					if (this.CanTalk)
					{
						return this._dateLabels;
					}
					return null;
				}
				else
				{
					if (!this.AgentData.SickState.UsedMedicine && !this.AgentData.SickState.Enabled)
					{
						return this._coldLabels;
					}
					return null;
				}
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06006218 RID: 25112 RVA: 0x00290FAA File Offset: 0x0028F3AA
		// (set) Token: 0x06006219 RID: 25113 RVA: 0x00290FB2 File Offset: 0x0028F3B2
		private CommCommandList.CommandInfo[] NormalCommandOptionInfos { get; set; }

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600621A RID: 25114 RVA: 0x00290FBB File Offset: 0x0028F3BB
		// (set) Token: 0x0600621B RID: 25115 RVA: 0x00290FC3 File Offset: 0x0028F3C3
		private CommCommandList.CommandInfo[] TalkCommandOptionInfos { get; set; }

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x00290FCC File Offset: 0x0028F3CC
		// (set) Token: 0x0600621D RID: 25117 RVA: 0x00290FD4 File Offset: 0x0028F3D4
		private CommCommandList.CommandInfo[] PraiseCommandOptionInfos { get; set; }

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x0600621E RID: 25118 RVA: 0x00290FDD File Offset: 0x0028F3DD
		// (set) Token: 0x0600621F RID: 25119 RVA: 0x00290FE5 File Offset: 0x0028F3E5
		private CommCommandList.CommandInfo[] InstructionCommandOptionInfos { get; set; }

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06006220 RID: 25120 RVA: 0x00290FEE File Offset: 0x0028F3EE
		// (set) Token: 0x06006221 RID: 25121 RVA: 0x00290FF6 File Offset: 0x0028F3F6
		private CommCommandList.CommandInfo[] DateCommandOptionInfos { get; set; }

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06006222 RID: 25122 RVA: 0x00290FFF File Offset: 0x0028F3FF
		// (set) Token: 0x06006223 RID: 25123 RVA: 0x00291007 File Offset: 0x0028F407
		private CommCommandList.CommandInfo[] DateCommandOptionInfosTP { get; set; }

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x00291010 File Offset: 0x0028F410
		// (set) Token: 0x06006225 RID: 25125 RVA: 0x00291018 File Offset: 0x0028F418
		private CommCommandList.CommandInfo[] SpecialCommandOptionInfos { get; set; }

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x00291021 File Offset: 0x0028F421
		// (set) Token: 0x06006227 RID: 25127 RVA: 0x00291029 File Offset: 0x0028F429
		private CommCommandList.CommandInfo[] ColdCommandOptionInfos { get; set; }

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06006228 RID: 25128 RVA: 0x00291032 File Offset: 0x0028F432
		// (set) Token: 0x06006229 RID: 25129 RVA: 0x0029103A File Offset: 0x0028F43A
		public int TouchCount { get; set; }

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x0600622A RID: 25130 RVA: 0x00291043 File Offset: 0x0028F443
		// (set) Token: 0x0600622B RID: 25131 RVA: 0x0029104B File Offset: 0x0028F44B
		public bool CanTalk { get; set; } = true;

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x0600622C RID: 25132 RVA: 0x00291054 File Offset: 0x0028F454
		// (set) Token: 0x0600622D RID: 25133 RVA: 0x0029105C File Offset: 0x0028F45C
		public int AttitudeID { get; set; }

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x0600622E RID: 25134 RVA: 0x00291065 File Offset: 0x0028F465
		// (set) Token: 0x0600622F RID: 25135 RVA: 0x0029106D File Offset: 0x0028F46D
		public bool UseNeckLook { get; set; } = true;

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06006230 RID: 25136 RVA: 0x00291076 File Offset: 0x0028F476
		// (set) Token: 0x06006231 RID: 25137 RVA: 0x0029107E File Offset: 0x0028F47E
		public bool CanHCommand { get; set; } = true;

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06006232 RID: 25138 RVA: 0x00291087 File Offset: 0x0028F487
		// (set) Token: 0x06006233 RID: 25139 RVA: 0x0029108F File Offset: 0x0028F48F
		public bool IsSpecial { get; set; }

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06006234 RID: 25140 RVA: 0x00291098 File Offset: 0x0028F498
		// (set) Token: 0x06006235 RID: 25141 RVA: 0x002910A0 File Offset: 0x0028F4A0
		public int HPositionID { get; set; }

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06006236 RID: 25142 RVA: 0x002910A9 File Offset: 0x0028F4A9
		// (set) Token: 0x06006237 RID: 25143 RVA: 0x002910B1 File Offset: 0x0028F4B1
		public int HPositionSubID { get; set; }

		// Token: 0x06006238 RID: 25144 RVA: 0x002910BC File Offset: 0x0028F4BC
		public void ResetActionFlag()
		{
			this.UseNeckLook = true;
			this.CanTalk = true;
			this.CanHCommand = true;
			this.AttitudeID = 0;
			this.IsSpecial = false;
			this.HPositionID = 0;
			this.HPositionSubID = 0;
			if (this._navMeshObstacle != null)
			{
				this._navMeshObstacle.radius = 2f;
			}
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x0029111C File Offset: 0x0028F51C
		public void LoadActionFlag(int actionID, int poseID)
		{
			Dictionary<int, ActAnimFlagData> dictionary;
			ActAnimFlagData actAnimFlagData;
			if (Singleton<Manager.Resources>.Instance.Action.AgentActionFlagTable.TryGetValue(actionID, out dictionary) && dictionary.TryGetValue(poseID, out actAnimFlagData))
			{
				this.UseNeckLook = actAnimFlagData.useNeckLook;
				this.CanTalk = actAnimFlagData.canTalk;
				this.AttitudeID = actAnimFlagData.attitudeID;
				this.CanHCommand = actAnimFlagData.canHCommand;
				this.IsSpecial = actAnimFlagData.isSpecial;
				this.HPositionID = actAnimFlagData.hPositionID1;
				this.HPositionSubID = actAnimFlagData.hPositionID2;
				if (this._navMeshObstacle != null)
				{
					this._navMeshObstacle.radius = actAnimFlagData.obstacleRadius;
				}
			}
			else
			{
				this.UseNeckLook = false;
				this.CanTalk = false;
				this.AttitudeID = -1;
				this.CanHCommand = false;
				this.IsSpecial = false;
				this.HPositionID = -1;
				this.HPositionSubID = -1;
				if (this._navMeshObstacle != null)
				{
					this._navMeshObstacle.radius = 2f;
				}
			}
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x00291228 File Offset: 0x0028F628
		public void DisableActionFlag()
		{
			this.UseNeckLook = false;
			this.CanTalk = false;
			this.AttitudeID = 0;
			this.CanHCommand = false;
			this.IsSpecial = false;
			this.HPositionID = -1;
			this.HPositionSubID = -1;
			if (this._navMeshObstacle != null)
			{
				this._navMeshObstacle.radius = 2f;
			}
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x00291288 File Offset: 0x0028F688
		public bool WaitTutorialUIDisplay()
		{
			PlayerActor playerActor = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
			CinemachineBrain cinemachineBrain;
			if (playerActor == null)
			{
				cinemachineBrain = null;
			}
			else
			{
				ActorCameraControl cameraControl = playerActor.CameraControl;
				cinemachineBrain = ((cameraControl != null) ? cameraControl.CinemachineBrain : null);
			}
			CinemachineBrain cinemachineBrain2 = cinemachineBrain;
			bool? flag = (cinemachineBrain2 != null) ? new bool?(cinemachineBrain2.IsBlending) : null;
			if (flag == null || !flag.Value)
			{
				ActorAnimation animation = this.Animation;
				bool? flag2 = (animation != null) ? new bool?(animation.PlayingTurnAnimation) : null;
				if (flag2 == null || !flag2.Value)
				{
					ChaControl chaControl = base.ChaControl;
					AudioSource audioSource = (chaControl != null) ? chaControl.asVoice : null;
					return !(audioSource == null) && !audioSource.loop;
				}
			}
			return true;
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x00291380 File Offset: 0x0028F780
		private void InitCommands()
		{
			GameObject gameObject = base.ChaControl.transform.FindLoop("cf_J_Mune00");
			Transform transform = (gameObject != null) ? gameObject.transform : null;
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			Sprite icon2;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable.TryGetValue(this.ID, out icon2);
			this._labels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					CoolTimeFillRate = (() => this.GetCoolTimeFillRate()),
					Event = delegate
					{
						if (this.CheckStealEvent())
						{
							this.StartStealEvent();
						}
						else if (this.CanProgressPhase())
						{
							this.StartPhaseEvent();
						}
						else if (this.CheckCatEvent())
						{
							this.StartCatEvent();
						}
						else
						{
							bool? flag = (this._timer != null) ? new bool?(this._timer.CheckPeriodEventADV()) : null;
							if (flag != null && flag.Value)
							{
								this.StartAppendRegularEvent();
							}
							else if (this.AgentData.LockTalk)
							{
								this.StartCommunication();
								Observable.EveryUpdate().Skip(1).SkipWhile((long _) => this.Animation.PlayingTurnAnimation).Take(1).Subscribe(delegate(long _)
								{
									this.openData.FindLoad("10", this.charaID, 0);
									this.packData.onComplete = delegate()
									{
										this.packData.restoreCommands = null;
										this.EndCommunication();
									};
									Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
								});
							}
							else
							{
								this.StartCommunication();
								if (this.AgentData.SickState.ID == 0)
								{
									this.openData.FindLoad("9", this.charaID, 4);
								}
								else if (this.AgentData.SickState.ID == 4)
								{
									this.openData.FindLoad("11", this.charaID, 4);
								}
								else
								{
									this.openData.FindLoad("0", this.charaID, 4);
								}
								this.packData.onComplete = delegate()
								{
									int tutorialID = 4;
									bool flag2 = Singleton<MapUIContainer>.IsInstance() && !MapUIContainer.GetTutorialOpenState(tutorialID);
									this.packData.restoreCommands = null;
									if (!flag2)
									{
										this.PopupCommands(this.NormalCommandOptionInfos, delegate
										{
											this.StartCommonSelection();
											MapUIContainer.CommandList.OnOpened = null;
										});
									}
									else
									{
										Observable.EveryLateUpdate().SkipWhile((long _) => this.WaitTutorialUIDisplay()).Take(1).Delay(TimeSpan.FromSeconds(0.5)).Subscribe(delegate(long _)
										{
											MapUIContainer.TutorialUI.ClosedEvent = delegate()
											{
												this.PopupCommands(this.NormalCommandOptionInfos, delegate
												{
													this.StartCommonSelection();
													MapUIContainer.CommandList.OnOpened = null;
												});
											};
											MapUIContainer.OpenTutorialUI(tutorialID, false);
										});
									}
								};
								Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
							}
						}
					}
				}
			};
			this._pEventLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					CoolTimeFillRate = (() => this.GetCoolTimeFillRate()),
					Event = delegate
					{
						this.AdvEventStart(this.advEventName);
					}
				}
			};
			this._pEventFishingLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					CoolTimeFillRate = (() => this.GetCoolTimeFillRate()),
					Event = delegate
					{
						this.FishingEvent();
					}
				}
			};
			this._eventLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartEvent();
					}
				}
			};
			this._sleepLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("1", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._bathLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("2", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._isuToiletLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("3", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._shagamiLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("4", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._shagamiFoundLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("5", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._searchLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("6", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._cookLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("7", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._standLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("8", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._mapBathLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("2", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._holeLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("6", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._sleepOnanismLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("6", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._deskBackLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}にアクション", this.CharaName)),
					Text = string.Format("{0}にアクション", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("6", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.SpecialCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._collapseLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}を担ぐ", this.CharaName)),
					Text = string.Format("{0}を担ぐ", this.CharaName),
					Icon = icon2,
					IsHold = true,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						Singleton<Manager.Map>.Instance.Player.Partner = this;
						this.PrevMode = this.Mode;
						this.Mode = Desire.ActionType.Onbu;
						Singleton<Manager.Map>.Instance.Player.Controller.ChangeState("Onbu");
					}
				}
			};
			this._coldLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}に話しかける", this.CharaName)),
					Text = string.Format("{0}に話しかける", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("10", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupCommands(this.ColdCommandOptionInfos, null);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._dateLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartDateCommunication();
						this.openData.FindLoad("12", this.charaID, 4);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.PopupDateCommands(Singleton<Manager.Map>.Instance.Player.Partner == this);
						};
						this.packData.isThisPartner = (Singleton<Manager.Map>.Instance.Player.Partner == this);
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._tutorialPassFishingRodLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartTutorialADV(1);
					}
				}
			};
			this._tutorialFoodRequestLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartTutorialADV(2);
					}
				}
			};
			this._tutorialWaitAtBaseLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartTutorialADV(3);
					}
				}
			};
			this._tutorialGrilledFishRequestLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					OnText = (() => string.Format("{0}と話す", this.CharaName)),
					Text = string.Format("{0}と話す", this.CharaName),
					Icon = icon2,
					TargetSpriteInfo = icon.CharaSpriteInfo,
					Transform = transform,
					Event = delegate
					{
						this.StartTutorialADV(4);
					}
				}
			};
			this.NormalCommandOptionInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("トーク")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.ChangeCommandList(this.TalkCommandOptionInfos, "トーク", delegate
						{
							this.BackCommandList(this.NormalCommandOptionInfos, this.CharaName, null);
						});
					}
				},
				new CommCommandList.CommandInfo("アドバイスする")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.ChangeCommandList(this.InstructionCommandOptionInfos, "アドバイス", delegate
						{
							this.BackCommandList(this.NormalCommandOptionInfos, this.CharaName, null);
						});
					}
				},
				new CommCommandList.CommandInfo("アイテムを渡す")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.PresentADV(this.NormalCommandOptionInfos);
					}
				},
				new CommCommandList.CommandInfo("頼まれもの")
				{
					Condition = (() => this.AgentData.ItemScrounge.isEvent),
					IsSpecial = true,
					Timer = (() => this.AgentData.ItemScrounge.remainingTime),
					Event = delegate(int x)
					{
						this.StartScroungeEvent(this.NormalCommandOptionInfos);
					}
				},
				new CommCommandList.CommandInfo("薬をあげる")
				{
					Condition = delegate
					{
						AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
						return sickState.ID != 0;
					},
					Event = delegate(int x)
					{
						this.StartSicknessEvent(0);
					}
				},
				new CommCommandList.CommandInfo("薬をあげる")
				{
					Condition = delegate
					{
						AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
						return sickState.ID == 0;
					},
					Event = delegate(int x)
					{
						this.StartSicknessEvent(0);
					}
				},
				new CommCommandList.CommandInfo("ついてきて")
				{
					Condition = (() => !this.IsBadMood()),
					Event = delegate(int x)
					{
						this.EndCommonSelection();
						this.Animation.StopAllAnimCoroutine();
						this.openData.FindLoad("5", this.charaID, 0);
						this.packData.onComplete = delegate()
						{
							if (this.packData.isSuccessFollow)
							{
								this.ResetActionFlag();
								if (this.CurrentPoint != null)
								{
									this.Animation.CrossFadeScreen(-1f);
									this.CurrentPoint.SetActiveMapItemObjs(true);
									this.CurrentPoint.CreateByproduct(this.ActionID, this.PoseID);
									this.CurrentPoint.ReleaseSlot(this);
									this.CurrentPoint = null;
									this.ActivateNavMeshAgent();
								}
								this.ActivatePairing(x, true);
								bool flag = this.ChaControl.fileGameInfo.phase >= 2;
								bool flag2 = this.ChaControl.fileGameInfo.flavorState[1] >= Singleton<Manager.Resources>.Instance.StatusProfile.HoldingHandBorderReliability;
								if (flag && flag2)
								{
									this.ActivateHoldingHands(x, true);
								}
								this.packData.restoreCommands = null;
								this.EndCommunication();
							}
							else
							{
								this.CheckTalkForceEnd(this.NormalCommandOptionInfos);
							}
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				},
				new CommCommandList.CommandInfo("エッチがしたい")
				{
					Condition = delegate
					{
						ChaControl chaControl = Singleton<Manager.Map>.Instance.Player.ChaControl;
						if (chaControl.sex == 1 && !chaControl.fileParam.futanari)
						{
							string[] lesTypeHMeshTag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.LesTypeHMeshTag;
							return this.CanSelectHCommand(lesTypeHMeshTag) && !this.IsBadMood();
						}
						return this.CanSelectHCommand() && !this.IsBadMood();
					},
					Event = delegate(int x)
					{
						this.InviteH(this.NormalCommandOptionInfos);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						ChaControl chaControl = Singleton<Manager.Map>.Instance.Player.ChaControl;
						return (chaControl.sex != 1 || chaControl.fileParam.futanari) && this.CanSelectHCommand() && this.IsBadMood();
					},
					Event = delegate(int x)
					{
						Singleton<HSceneManager>.Instance.isForce = true;
						this.InviteH(this.NormalCommandOptionInfos);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.Left("0");
					}
				}
			};
			CommCommandList.CommandInfo[] array = new CommCommandList.CommandInfo[9];
			array[0] = new CommCommandList.CommandInfo("印象を聞く")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartTalk("0", string.Empty);
				}
			};
			array[1] = new CommCommandList.CommandInfo("調子どう？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartTalk("1", string.Empty);
				}
			};
			array[2] = new CommCommandList.CommandInfo("誰と仲良し？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.EndCommonSelection();
					this.Animation.StopAllAnimCoroutine();
					this.openData.FindLoad("2", this.charaID, 0);
					this.packData.onComplete = delegate()
					{
						this.CheckTalkForceEnd(this.TalkCommandOptionInfos);
					};
					this.SearchFavoriteTargetForADV();
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				}
			};
			array[3] = new CommCommandList.CommandInfo("お気に入りの場所は？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartTalk("3", string.Empty);
				}
			};
			array[4] = new CommCommandList.CommandInfo("おだてる")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.ChangeCommandList(this.PraiseCommandOptionInfos, "おだてる", delegate
					{
						this.BackCommandList(this.TalkCommandOptionInfos, "トーク", delegate
						{
							PlayerActor player = Singleton<Manager.Map>.Instance.Player;
							CommCommandList.CommandInfo[] commands;
							if (player.Mode == Desire.ActionType.Date)
							{
								if (player.Partner == this)
								{
									commands = this.DateCommandOptionInfos;
								}
								else
								{
									commands = this.DateCommandOptionInfosTP;
								}
							}
							else
							{
								commands = this.NormalCommandOptionInfos;
							}
							this.BackCommandList(commands, this.CharaName, null);
						});
					});
				}
			};
			int num = 5;
			CommCommandList.CommandInfo commandInfo = new CommCommandList.CommandInfo("エッチな会話");
			CommCommandList.CommandInfo commandInfo2 = commandInfo;
			if (AgentActor.<>f__mg$cache0 == null)
			{
				AgentActor.<>f__mg$cache0 = new Func<bool>(Game.get_isAdd01);
			}
			commandInfo2.Condition = AgentActor.<>f__mg$cache0;
			commandInfo.Event = delegate(int x)
			{
				this.EndCommonSelection();
				this.Animation.StopAllAnimCoroutine();
				this.openData.FindLoad("0", this.charaID, 9);
				this.packData.onComplete = delegate()
				{
					this.CheckTalkForceEnd(this.TalkCommandOptionInfos);
				};
				this.packData.restoreCommands = this.TalkCommandOptionInfos;
				Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
			};
			array[num] = commandInfo;
			int num2 = 6;
			commandInfo = new CommCommandList.CommandInfo("戻る");
			commandInfo.Condition = (() => Singleton<Manager.Map>.Instance.Player.Mode != Desire.ActionType.Date);
			commandInfo.Event = delegate(int x)
			{
				this.BackCommandList(this.NormalCommandOptionInfos, this.CharaName, null);
			};
			array[num2] = commandInfo;
			array[7] = new CommCommandList.CommandInfo("戻る")
			{
				Condition = delegate
				{
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					bool flag = player.Mode == Desire.ActionType.Date;
					bool flag2 = player.Partner == this;
					return flag && flag2;
				},
				Event = delegate(int x)
				{
					this.BackCommandList(this.DateCommandOptionInfos, this.CharaName, null);
				}
			};
			array[8] = new CommCommandList.CommandInfo("戻る")
			{
				Condition = delegate
				{
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					bool flag = player.Mode == Desire.ActionType.Date;
					bool flag2 = player.Partner != this;
					return flag && flag2;
				},
				Event = delegate(int x)
				{
					this.BackCommandList(this.DateCommandOptionInfosTP, this.CharaName, null);
				}
			};
			this.TalkCommandOptionInfos = array;
			this.PraiseCommandOptionInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("容姿についてほめる")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.StartTalk("4", "トーク");
						MapUIContainer.CommandList.CancelEvent = delegate()
						{
							PlayerActor player = Singleton<Manager.Map>.Instance.Player;
							CommCommandList.CommandInfo[] commands;
							if (player.Mode == Desire.ActionType.Date)
							{
								if (player.Partner == this)
								{
									commands = this.DateCommandOptionInfos;
								}
								else
								{
									commands = this.DateCommandOptionInfosTP;
								}
							}
							else
							{
								commands = this.NormalCommandOptionInfos;
							}
							this.BackCommandList(commands, this.CharaName, null);
						};
					}
				},
				new CommCommandList.CommandInfo("内面についてほめる")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.StartTalk("6", "トーク");
						MapUIContainer.CommandList.CancelEvent = delegate()
						{
							PlayerActor player = Singleton<Manager.Map>.Instance.Player;
							CommCommandList.CommandInfo[] commands;
							if (player.Mode == Desire.ActionType.Date)
							{
								if (player.Partner == this)
								{
									commands = this.DateCommandOptionInfos;
								}
								else
								{
									commands = this.DateCommandOptionInfosTP;
								}
							}
							else
							{
								commands = this.NormalCommandOptionInfos;
							}
							this.BackCommandList(commands, this.CharaName, null);
						};
					}
				},
				new CommCommandList.CommandInfo("戻る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.BackCommandList(this.TalkCommandOptionInfos, "トーク", delegate
						{
							PlayerActor player = Singleton<Manager.Map>.Instance.Player;
							CommCommandList.CommandInfo[] commands;
							if (player.Mode == Desire.ActionType.Date)
							{
								if (player.Partner == this)
								{
									commands = this.DateCommandOptionInfos;
								}
								else
								{
									commands = this.DateCommandOptionInfosTP;
								}
							}
							else
							{
								commands = this.NormalCommandOptionInfos;
							}
							this.BackCommandList(commands, this.CharaName, null);
						});
					}
				}
			};
			CommCommandList.CommandInfo[] array2 = new CommCommandList.CommandInfo[12];
			array2[0] = new CommCommandList.CommandInfo("寝に行ったら？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("7");
				}
			};
			array2[1] = new CommCommandList.CommandInfo("少し休んだら？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("0");
				}
			};
			array2[2] = new CommCommandList.CommandInfo("採取手伝って")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("1");
				}
			};
			array2[3] = new CommCommandList.CommandInfo("なにか食べたら？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("2");
				}
			};
			array2[4] = new CommCommandList.CommandInfo("なにか飲んだら？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("6");
				}
			};
			array2[5] = new CommCommandList.CommandInfo("料理作って")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("5");
				}
			};
			array2[6] = new CommCommandList.CommandInfo("たまには気分転換を")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("3");
				}
			};
			array2[7] = new CommCommandList.CommandInfo("トイレ行ったら？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("8");
				}
			};
			array2[8] = new CommCommandList.CommandInfo("風呂入ったら？")
			{
				Condition = null,
				Event = delegate(int x)
				{
					this.StartInstruction("9");
				}
			};
			int num3 = 9;
			commandInfo = new CommCommandList.CommandInfo("エッチな行動を要求");
			CommCommandList.CommandInfo commandInfo3 = commandInfo;
			if (AgentActor.<>f__mg$cache1 == null)
			{
				AgentActor.<>f__mg$cache1 = new Func<bool>(Game.get_isAdd01);
			}
			commandInfo3.Condition = AgentActor.<>f__mg$cache1;
			commandInfo.Event = delegate(int x)
			{
				this.StartInstruction("4");
			};
			array2[num3] = commandInfo;
			int num4 = 10;
			commandInfo = new CommCommandList.CommandInfo("戻る");
			commandInfo.Condition = (() => Singleton<Manager.Map>.Instance.Player.Mode != Desire.ActionType.Date);
			commandInfo.Event = delegate(int x)
			{
				this.BackCommandList(this.NormalCommandOptionInfos, this.CharaName, null);
			};
			array2[num4] = commandInfo;
			array2[11] = new CommCommandList.CommandInfo("戻る")
			{
				Condition = delegate
				{
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					bool flag = player.Mode == Desire.ActionType.Date;
					bool flag2 = player.Partner != this;
					return flag && flag2;
				},
				Event = delegate(int x)
				{
					this.BackCommandList(this.DateCommandOptionInfosTP, this.CharaName, null);
				}
			};
			this.InstructionCommandOptionInfos = array2;
			this.DateCommandOptionInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("トーク")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.ChangeCommandList(this.TalkCommandOptionInfos, "トーク", delegate
						{
							this.BackCommandList(this.DateCommandOptionInfos, this.CharaName, delegate
							{
								this.CancelCommunication();
							});
						});
					}
				},
				new CommCommandList.CommandInfo("アイテムを渡す")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.PresentADV(this.DateCommandOptionInfos);
					}
				},
				new CommCommandList.CommandInfo("解散する")
				{
					Condition = null,
					Event = delegate(int x)
					{
						if (Singleton<Game>.Instance.MapShortcutUI != null)
						{
							return;
						}
						this.EndCommonSelection();
						this.openData.FindLoad("1", this.charaID, 5);
						this.packData.onComplete = delegate()
						{
							this.packData.restoreCommands = null;
							this.EndCommunication();
							this.DeactivatePairing(x);
							this.ActivateHoldingHands(x, false);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				},
				new CommCommandList.CommandInfo("エッチがしたい")
				{
					Condition = delegate
					{
						ChaControl chaControl = Singleton<Manager.Map>.Instance.Player.ChaControl;
						if (chaControl.sex == 1 && !chaControl.fileParam.futanari)
						{
							string[] lesTypeHMeshTag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.LesTypeHMeshTag;
							return this.CanSelectHCommand(lesTypeHMeshTag) && !this.IsBadMood();
						}
						return this.CanSelectHCommand() && !this.IsBadMood();
					},
					Event = delegate(int x)
					{
						this.InviteH(this.DateCommandOptionInfos);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						ChaControl chaControl = Singleton<Manager.Map>.Instance.Player.ChaControl;
						return (chaControl.sex != 1 || chaControl.fileParam.futanari) && this.CanSelectHCommand() && this.IsBadMood();
					},
					Event = delegate(int x)
					{
						Singleton<HSceneManager>.Instance.isForce = true;
						this.InviteH(this.DateCommandOptionInfos);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.Left("2");
					}
				}
			};
			this.DateCommandOptionInfosTP = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("トーク")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.ChangeCommandList(this.TalkCommandOptionInfos, "トーク", delegate
						{
							this.BackCommandList(this.DateCommandOptionInfosTP, this.CharaName, delegate
							{
								this.CancelCommunication();
							});
						});
					}
				},
				new CommCommandList.CommandInfo("アドバイスする")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.ChangeCommandList(this.InstructionCommandOptionInfos, "アドバイス", delegate
						{
							this.BackCommandList(this.DateCommandOptionInfosTP, this.CharaName, delegate
							{
								this.CancelCommunication();
							});
						});
					}
				},
				new CommCommandList.CommandInfo("アイテムを渡す")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.PresentADV(this.DateCommandOptionInfosTP);
					}
				},
				new CommCommandList.CommandInfo("頼まれもの")
				{
					Condition = (() => this.AgentData.ItemScrounge.isEvent),
					IsSpecial = true,
					Timer = (() => this.AgentData.ItemScrounge.remainingTime),
					Event = delegate(int x)
					{
						this.StartScroungeEvent(this.DateCommandOptionInfosTP);
					}
				},
				new CommCommandList.CommandInfo("薬をあげる")
				{
					Condition = delegate
					{
						AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
						return sickState.ID != 0;
					},
					Event = delegate(int x)
					{
						this.StartSicknessEvent(0);
					}
				},
				new CommCommandList.CommandInfo("薬をあげる")
				{
					Condition = delegate
					{
						AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
						return sickState.ID == 0;
					},
					Event = delegate(int x)
					{
						this.StartSicknessEvent(0);
					}
				},
				new CommCommandList.CommandInfo("3人でエッチがしたい")
				{
					Condition = delegate
					{
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						ChaControl chaControl = player.ChaControl;
						if (chaControl.sex == 1 && !chaControl.fileParam.futanari)
						{
							return false;
						}
						if (player.AgentPartner == null)
						{
							return false;
						}
						string[] floorTypeHMeshTag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.FloorTypeHMeshTag;
						return this.CanSelectHCommand(floorTypeHMeshTag) && !this.IsBadMood();
					},
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
						this.EndCommonSelection();
						this.Animation.StopAllAnimCoroutine();
						this.openData.FindLoad("2", this.charaID, 9);
						this.packData.onComplete = delegate()
						{
							if (this.packData.isSuccessH)
							{
								this.packData.restoreCommands = null;
								PlayerActor player = Singleton<Manager.Map>.Instance.Player;
								this.InitiateHScene3P(player.AgentPartner, this);
							}
							else
							{
								this.CheckTalkForceEnd(this.DateCommandOptionInfosTP);
							}
						};
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.Left("3");
					}
				}
			};
			this.SpecialCommandOptionInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("起こす")
				{
					Condition = delegate
					{
						bool flag = this.Mode == Desire.ActionType.EndTaskSleep || this.Mode == Desire.ActionType.EndTaskSleepAfterDate || this.Mode == Desire.ActionType.EndTaskSleepTogether;
						return flag && !this.SleepTrigger;
					},
					Event = delegate(int x)
					{
						if (Singleton<Game>.Instance.MapShortcutUI != null)
						{
							return;
						}
						this.EndCommonSelection();
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
						this.SleepTrigger = true;
						this.EndCommunication();
						this.ChangeBehavior(Desire.ActionType.WokenUp);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						if (player.ChaControl.sex == 1 && !player.ChaControl.fileParam.futanari)
						{
							return false;
						}
						bool flag = this.Mode == Desire.ActionType.EndTaskSleep || this.Mode == Desire.ActionType.EndTaskSleepAfterDate || this.Mode == Desire.ActionType.EndTaskSleepTogether;
						bool flag2 = this.HPositionID == 1;
						return flag && flag2 && !this.AgentData.YobaiTrigger;
					},
					Event = delegate(int x)
					{
						this.ClearDesire(Desire.Type.Sleep);
						this.AgentData.YobaiTrigger = true;
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 2;
					},
					Event = delegate(int x)
					{
						this.ClearDesire(Desire.Type.Bath);
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 3;
					},
					Event = delegate(int x)
					{
						int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
						Singleton<HSceneManager>.Instance.ReserveToilet = this.GetDesire(desireKey);
						this.ClearDesire(Desire.Type.Toilet);
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 4;
					},
					Event = delegate(int x)
					{
						int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
						Singleton<HSceneManager>.Instance.ReserveToilet = this.GetDesire(desireKey);
						this.ClearDesire(Desire.Type.Toilet);
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 5;
					},
					Event = delegate(int x)
					{
						Desire.Type key;
						if (Desire.ModeTable.TryGetValue(this.PrevMode, out key))
						{
							int desireKey = Desire.GetDesireKey(key);
							this.SetDesire(desireKey, 0f);
						}
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 6;
					},
					Event = delegate(int x)
					{
						this.ClearDesire(Desire.Type.Hunt);
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 7;
					},
					Event = delegate(int x)
					{
						this.ClearDesire(Desire.Type.Cook);
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 8;
					},
					Event = delegate(int x)
					{
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 9;
					},
					Event = delegate(int x)
					{
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 10;
					},
					Event = delegate(int x)
					{
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 13;
					},
					Event = delegate(int x)
					{
						this.ClearDesire(Desire.Type.Bath);
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 14;
					},
					Event = delegate(int x)
					{
						this.ClearDesire(Desire.Type.Game);
						float num5 = -this.Animation.GetAngleFromForward(this.Controller.transform.forward);
						Vector3 position = base.Position;
						position.y = 0f;
						Vector3 position2 = Singleton<Manager.Map>.Instance.Player.Position;
						position2.y = 0f;
						float angleFromForward = this.Animation.GetAngleFromForward(Vector3.Normalize(position2 - position));
						float f = angleFromForward - num5;
						if (Mathf.Abs(f) > 90f)
						{
							this.GotoHScene(this.HPositionID);
						}
						else
						{
							this.GotoHScene(this.HPositionSubID);
						}
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 16;
					},
					Event = delegate(int x)
					{
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("エッチなことをする")
				{
					Condition = delegate
					{
						if (!Singleton<Manager.Map>.IsInstance())
						{
							return false;
						}
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						return (player.ChaControl.sex != 1 || player.ChaControl.fileParam.futanari) && this.HPositionID == 17;
					},
					Event = delegate(int x)
					{
						this.GotoHScene(this.HPositionID);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.Left("4");
					}
				}
			};
			this.ColdCommandOptionInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("薬をあげる")
				{
					Condition = null,
					IsSpecial = true,
					Event = delegate(int x)
					{
						this.StartSicknessEvent(1);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.Left("10");
					}
				}
			};
		}

		// Token: 0x0600623D RID: 25149 RVA: 0x00292C20 File Offset: 0x00291020
		private float GetCoolTimeFillRate()
		{
			bool lockTalk = this.AgentData.LockTalk;
			float talkElapsedTime = this.AgentData.TalkElapsedTime;
			float talkLockDuration = Singleton<Manager.Resources>.Instance.AgentProfile.TalkLockDuration;
			if (lockTalk)
			{
				float num = Mathf.Clamp(talkElapsedTime / talkLockDuration, 0f, 1f);
				return 1f - num;
			}
			return 0f;
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x00292C7B File Offset: 0x0029107B
		public void ChangeStaticNavMeshAvoidanceUnencounter()
		{
			if (this.Mode != Desire.ActionType.Encounter)
			{
				this._originAvoidancePriority = this._navMeshAgent.avoidancePriority;
				base.ChangeStaticNavMeshAgentAvoidance();
			}
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x00292CA1 File Offset: 0x002910A1
		public void RecoverNavMeshAvoidanceUnencounter()
		{
			if (this.Mode != Desire.ActionType.Encounter)
			{
				base.RecoverNavMeshAgentAvoidance();
			}
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x00292CB8 File Offset: 0x002910B8
		public PlayState ChangeADVAnimator()
		{
			PoseKeyPair poseKeyPair = Singleton<Manager.Resources>.Instance.AgentProfile.ADVIdleTable[base.ChaControl.fileParam.personality];
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[poseKeyPair.postureID][poseKeyPair.poseID];
			AssetBundleInfo assetBundleInfo = playState.MainStateInfo.AssetBundleInfo;
			base.ChangeAnimator(assetBundleInfo.assetbundle, assetBundleInfo.asset);
			return playState;
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x00292D34 File Offset: 0x00291134
		public void ChangeADVCameraNoBlend(PlayerActor player)
		{
			CinemachineBlendDefinition.Style prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			ADV.ChangeADVCamera(this);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
			});
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x00292DB4 File Offset: 0x002911B4
		public void RevertCameraNoBlend(PlayerActor player)
		{
			CinemachineBlendDefinition.Style prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			player.CameraControl.Mode = CameraMode.Normal;
			player.ChaControl.visibleAll = (player.CameraControl.ShotType != ShotType.PointOfView);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
			});
		}

		// Token: 0x06006243 RID: 25155 RVA: 0x00292E64 File Offset: 0x00291264
		public void StartCommunication()
		{
			base.StopNavMeshAgent();
			this.ChangeStaticNavMeshAvoidanceUnencounter();
			this.packData.Init();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.NavMeshAgent.velocity = Vector3.zero;
			player.CommCompanion = this;
			player.Controller.ChangeState("Communication");
			this.packData.AttitudeID = this.AttitudeID;
			switch (this.AttitudeID)
			{
			case 0:
			{
				if (this.UseNeckLook)
				{
					base.ChaControl.ChangeLookEyesTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
					base.ChaControl.ChangeLookEyesPtn(1);
					base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					base.ChaControl.ChangeLookNeckPtn(3, 1f);
				}
				PlayState playState = this.ChangeADVAnimator();
				this.Animation.PlayTurnAnimation(player.Position, 1f, playState.MainStateInfo.InStateInfo, false);
				base.SetActiveOnEquipedItem(false);
				break;
			}
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				if (this.UseNeckLook)
				{
					base.ChaControl.ChangeLookEyesTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
					base.ChaControl.ChangeLookEyesPtn(1);
					base.ChaControl.ChangeLookNeckTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 0.8f);
					base.ChaControl.ChangeLookNeckPtn(1, 1f);
				}
				this._schedule.progress = false;
				break;
			}
			this.DisableBehavior();
			Observable.EveryUpdate().Take(5).Subscribe(delegate(long _)
			{
				switch (this.AttitudeID)
				{
				case 0:
					if (this.UseNeckLook)
					{
						this.ChaControl.ChangeLookEyesTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
						this.ChaControl.ChangeLookEyesPtn(1);
						this.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
						this.ChaControl.ChangeLookNeckPtn(3, 1f);
					}
					break;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					if (this.UseNeckLook)
					{
						this.ChaControl.ChangeLookEyesTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
						this.ChaControl.ChangeLookEyesPtn(1);
						this.ChaControl.ChangeLookNeckTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 0.8f);
						this.ChaControl.ChangeLookNeckPtn(1, 1f);
					}
					this._schedule.progress = false;
					break;
				}
				this.BehaviorResources.Current.DisableBehavior(true);
			});
			if (base.Partner != null && base.Partner is AgentActor)
			{
				AgentActor agentActor = base.Partner as AgentActor;
				int attitudeID = this.AttitudeID;
				if (attitudeID != 0)
				{
					agentActor.DisableBehavior();
				}
			}
			int attitudeID2 = this.AttitudeID;
			if (attitudeID2 != 0)
			{
				if (attitudeID2 != 1)
				{
					ADV.ChangeADVFixedAngleCamera(this, this.AttitudeID);
				}
				else
				{
					ADV.ChangeADVCameraDiagonal(this);
				}
			}
			else
			{
				ADV.ChangeADVCamera(this);
			}
			player.CameraControl.VanishControl.LoadHousingVanish(player);
			player.CameraControl.VanishControl.Load();
			MapUIContainer.SetVisibleHUD(false);
			Singleton<ADV>.Instance.TargetCharacter = this;
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x0029316C File Offset: 0x0029156C
		private void StartDateCommunication()
		{
			base.StopNavMeshAgent();
			this.packData.Init();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.CommCompanion = this;
			player.Controller.ChangeState("Communication");
			if (this.UseNeckLook)
			{
				base.ChaControl.ChangeLookEyesTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
				base.ChaControl.ChangeLookEyesPtn(1);
				base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
				base.ChaControl.ChangeLookNeckPtn(3, 1f);
			}
			this.packData.AttitudeID = this.AttitudeID;
			switch (this.AttitudeID)
			{
			case 0:
			{
				PlayState playState = this.ChangeADVAnimator();
				this.Animation.PlayTurnAnimation(player.Position, 1f, playState.MainStateInfo.InStateInfo, false);
				bool enabled = player.HandsHolder.enabled;
				player.OldEnabledHoldingHand = enabled;
				bool flag = enabled;
				if (flag)
				{
					player.HandsHolder.enabled = false;
					if (player.HandsHolder.EnabledHolding)
					{
						player.HandsHolder.EnabledHolding = false;
					}
				}
				base.SetActiveOnEquipedItem(false);
				break;
			}
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				this._schedule.progress = false;
				break;
			}
			this.DisableBehavior();
			ADV instance = Singleton<ADV>.Instance;
			ADV.ChangeADVCamera(this);
			MapUIContainer.SetVisibleHUD(false);
			instance.TargetCharacter = this;
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x00293310 File Offset: 0x00291710
		private void EndCommunication()
		{
			MapUIContainer.SetActiveCommandList(false);
			this.VanishCommands();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = true;
				player.OldEnabledHoldingHand = false;
			}
			Singleton<Manager.Map>.Instance.Player.PlayerController.CommandArea.RefreshCommands();
			this.packData.Release();
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x00293378 File Offset: 0x00291778
		private void CancelCommunication()
		{
			this.EndCommonSelection();
			MapUIContainer.SetActiveCommandList(false);
			this.VanishCommands();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = true;
				player.OldEnabledHoldingHand = false;
			}
			Singleton<Manager.Map>.Instance.Player.PlayerController.CommandArea.RefreshCommands();
			this.packData.Release();
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x002933E4 File Offset: 0x002917E4
		private void StartTalk(string asset, string titleOnBack = "")
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			this.EndCommonSelection();
			this.Animation.StopAllAnimCoroutine();
			this.openData.FindLoad(asset, this.charaID, 0);
			this.packData.onComplete = delegate()
			{
				if (!titleOnBack.IsNullOrEmpty())
				{
					MapUIContainer.CommandList.Label.text = titleOnBack;
				}
				this.CheckTalkForceEnd(this.TalkCommandOptionInfos);
			};
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x00293468 File Offset: 0x00291868
		private void StartInstruction(string asset)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			this.EndCommonSelection();
			this.Animation.StopAllAnimCoroutine();
			this.openData.FindLoad(asset, this.charaID, 1);
			this.packData.onComplete = delegate()
			{
				this.CheckTalkForceEnd(this.InstructionCommandOptionInfos);
			};
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x002934D8 File Offset: 0x002918D8
		private void InviteH(CommCommandList.CommandInfo[] restoreCommands)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			this.EndCommonSelection();
			this.Animation.StopAllAnimCoroutine();
			this.openData.FindLoad("1", this.charaID, 9);
			this.packData.onComplete = delegate()
			{
				if (this.packData.isSuccessH)
				{
					Desire.Type type;
					if (this.Mode == Desire.ActionType.Encounter)
					{
						if (Desire.ModeTable.TryGetValue(this.PrevMode, out type))
						{
							this.ClearDesire(type);
						}
					}
					else if (Desire.ModeTable.TryGetValue(this.Mode, out type))
					{
						this.ClearDesire(type);
					}
					this.packData.restoreCommands = null;
					this.InitiateHScene(HSceneManager.HEvent.Normal);
				}
				else
				{
					this.CheckTalkForceEnd(restoreCommands);
				}
			};
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x0029355F File Offset: 0x0029195F
		private void GotoHScene(int hPosID)
		{
			this.EndCommonSelection();
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			MapUIContainer.SetActiveCommandList(false);
			Singleton<HSceneManager>.Instance.isForce = true;
			this.InitiateHScene((HSceneManager.HEvent)hPosID);
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x00293590 File Offset: 0x00291990
		private void Left(string asset)
		{
			if (Singleton<Game>.Instance.MapShortcutUI != null)
			{
				return;
			}
			this.EndCommonSelection();
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.openData.FindLoad(asset, this.charaID, 5);
			this.packData.onComplete = delegate()
			{
				this.packData.restoreCommands = null;
				this.EndCommunication();
			};
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x00293609 File Offset: 0x00291A09
		private void ChangeCommandList(CommCommandList.CommandInfo[] commands, string title, System.Action onCancel)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			MapUIContainer.CommandList.Refresh(commands, null);
			MapUIContainer.CommandList.Label.text = title;
			MapUIContainer.CommandList.CancelEvent = onCancel;
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x00293642 File Offset: 0x00291A42
		private void BackCommandList(CommCommandList.CommandInfo[] commands, string title, System.Action onCancel)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			MapUIContainer.CommandList.Refresh(commands, null);
			MapUIContainer.CommandList.Label.text = title;
			MapUIContainer.CommandList.CancelEvent = onCancel;
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x0029367C File Offset: 0x00291A7C
		private void CheckTalkForceEnd(CommCommandList.CommandInfo[] restoreCommands)
		{
			if (this.AgentData.TalkMotivation <= 0f)
			{
				this.AgentData.LockTalk = true;
				this.AgentData.TalkElapsedTime = 0f;
				this.packData.restoreCommands = null;
				this.EndCommunication();
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				if (player.Mode == Desire.ActionType.Date && player.Partner == this)
				{
					this.DeactivatePairing(0);
					this.ActivateHoldingHands(0, false);
				}
			}
			else
			{
				this.packData.restoreCommands = restoreCommands;
				this.packData.OnEndRefreshCommand = delegate()
				{
					this.StartCommonSelection();
					this.packData.OnEndRefreshCommand = null;
				};
			}
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x0029372B File Offset: 0x00291B2B
		private void StartEvent()
		{
			this.IsEvent = true;
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x00293734 File Offset: 0x00291B34
		private bool CheckStealEvent()
		{
			PoseKeyPair snitchFoodID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SnitchFoodID;
			return base.ActionID == snitchFoodID.postureID && base.PoseID == snitchFoodID.poseID;
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x00293780 File Offset: 0x00291B80
		private void StartStealEvent()
		{
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			Desire.ActionType mode = this.BehaviorResources.Mode;
			this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			Observable.EveryLateUpdate().Take(20).Subscribe(delegate(long _)
			{
				this.StopNavMeshAgent();
			});
			base.StopNavMeshAgent();
			this.Animation.CrossFadeScreen(-1f);
			this.packData.Init();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.CommCompanion = this;
			player.PlayerController.ChangeState("Idle");
			PoseKeyPair foundID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.StealFoundID;
			Observable.FromCoroutine(() => this.StealAnimCoroutine(foundID.postureID, foundID.poseID), false).Subscribe(delegate(Unit _)
			{
				player.PlayerController.ChangeState("Communication");
				this.Animation.CrossFadeScreen(-1f);
				this.Animation.StopAllAnimCoroutine();
				if (this.UseNeckLook)
				{
					this.ChaControl.ChangeLookEyesTarget(1, player.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
					this.ChaControl.ChangeLookEyesPtn(1);
					this.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					this.ChaControl.ChangeLookNeckPtn(3, 1f);
				}
				PlayState playState = this.ChangeADVAnimator();
				this.Animation.PlayTurnAnimation(player.Position, 1f, playState.MainStateInfo.InStateInfo, false);
				ADV.ChangeADVCamera(this);
				player.CameraControl.VanishControl.LoadHousingVanish(player);
				player.CameraControl.VanishControl.Load();
				this.openData.FindLoad("2", this.charaID, 7);
				this.packData.onComplete = delegate()
				{
					this.packData.restoreCommands = null;
					this.SetLookPtn(0, 3);
					this.SetLookTarget(0, 0, null);
					this.Animation.StopAllAnimCoroutine();
					this.Animation.ResetDefaultAnimatorController();
					if (this.CurrentPoint != null)
					{
						this.CurrentPoint.SetActiveMapItemObjs(true);
						this.CurrentPoint.ReleaseSlot(this);
						this.CurrentPoint = null;
					}
					if (this.AgentData.CarryingItem != null)
					{
						this.ApplyFoodParameter(this.AgentData.CarryingItem);
						this.AgentData.CarryingItem = null;
					}
					this.ChangeBehavior(Desire.ActionType.Normal);
					player.CameraControl.Mode = CameraMode.Normal;
					player.PlayerController.ChangeState("Normal");
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.StorySupportUI.Open();
					this.packData.Release();
				};
				Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
			});
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x00293870 File Offset: 0x00291C70
		private bool CheckCatEvent()
		{
			if (this.AttitudeID != 0)
			{
				return false;
			}
			if (this.AgentData.CheckCatEvent)
			{
				return false;
			}
			if (this.PrevActionMode != Desire.ActionType.EndTaskWildAnimal)
			{
				return false;
			}
			int num = UnityEngine.Random.Range(0, 100);
			float num2 = Singleton<Manager.Resources>.Instance.AgentProfile.CatEventBaseProb;
			int flavor = base.ChaControl.fileGameInfo.flavorState[3];
			Threshold flavorCatCaptureMinMax = Singleton<Manager.Resources>.Instance.StatusProfile.FlavorCatCaptureMinMax;
			Threshold flavorCatCaptureRate = Singleton<Manager.Resources>.Instance.StatusProfile.FlavorCatCaptureRate;
			float f = AgentActor.FlavorVariation(flavorCatCaptureMinMax, flavorCatCaptureRate, flavor);
			num2 += (float)Mathf.RoundToInt(f);
			if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(19))
			{
				num2 += (float)Singleton<Manager.Resources>.Instance.StatusProfile.CatCaptureProbBuff;
			}
			this.AgentData.CheckCatEvent = true;
			return (float)num < num2;
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x00293958 File Offset: 0x00291D58
		private void StartCatEvent()
		{
			this.packData.Init();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			Desire.ActionType prevMode = this.BehaviorResources.Mode;
			this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			player.PlayerController.ChangeState("Idle");
			this.openData.FindLoad("1", this.charaID, 7);
			this.packData.onComplete = delegate()
			{
				this.packData.restoreCommands = null;
				this.SetLookPtn(0, 3);
				this.SetLookTarget(0, 0, null);
				if (this.AttitudeID == 0)
				{
					this.Animation.StopAllAnimCoroutine();
					this.Animation.ResetDefaultAnimatorController();
				}
				this.RevertCameraNoBlend(player);
				Singleton<Manager.Map>.Instance.EnableEntity(this);
				MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					this.BehaviorResources.ChangeMode(prevMode);
					player.PlayerController.ChangeState("Normal");
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.StorySupportUI.Open();
					this.packData.Release();
				});
			};
			this.packData.Init();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			if (MapUIContainer.FadeCanvas.IsFading)
			{
				MapUIContainer.FadeCanvas.SkipFade();
			}
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Singleton<Manager.Map>.Instance.DisableEntity(this);
				Vector3 position = this.Position;
				position.y = 0f;
				Vector3 position2 = player.Position;
				position2.y = 0f;
				this.Rotation = Quaternion.LookRotation(position2 - position);
				player.CommCompanion = this;
				player.PlayerController.ChangeState("Communication");
				this.ChangeADVAnimator();
				player.Rotation = Quaternion.LookRotation(position - position2);
				this.ChangeADVCameraNoBlend(player);
				Transform transform = player.CameraControl.CameraComponent.transform;
				this.SetLookPtn(1, 3);
				this.SetLookTarget(1, 0, transform);
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Observable.TimerFrame(30, FrameCountType.Update).Subscribe(delegate(long __)
					{
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					});
				});
			});
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x00293A62 File Offset: 0x00291E62
		private bool CheckFishingEvent()
		{
			return !this.IsBadMood() && base.ActionID == 6 && new int[]
			{
				6,
				7,
				8
			}.Contains(base.PoseID);
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x00293A9C File Offset: 0x00291E9C
		private void FishingEvent()
		{
			this.packData.Init();
			int desireKey = Desire.GetDesireKey(this.RequestedDesire);
			this.SetDesire(desireKey, 0f);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.PlayerController.ChangeState("Idle");
			this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			this.DisableBehavior();
			base.DeactivateNavMeshAgent();
			this.openData.FindLoad("0", this.charaID, 7);
			this.packData.onComplete = delegate()
			{
				this.packData.restoreCommands = null;
				this.SetLookPtn(0, 3);
				this.SetLookTarget(0, 0, null);
				this.ResetActionFlag();
				this.Animation.StopAllAnimCoroutine();
				this.Animation.ResetDefaultAnimatorController();
				this.RevertCameraNoBlend(player);
				if (this.AgentData.CarryingItem != null)
				{
					this.AgentData.CarryingItem = null;
				}
				this.EnableBehavior();
				this.ActivateNavMeshAgent();
				Singleton<Manager.Map>.Instance.EnableEntity(this);
				MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					this.ChangeBehavior(Desire.ActionType.Normal);
					player.PlayerController.ChangeState("Normal");
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.StorySupportUI.Open();
					this.packData.Release();
				});
			};
			this.packData.Init();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			if (MapUIContainer.FadeCanvas.IsFading)
			{
				MapUIContainer.FadeCanvas.SkipFade();
			}
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Singleton<Manager.Map>.Instance.DisableEntity(this);
				Vector3 position = this.Position;
				position.y = 0f;
				Vector3 position2 = player.Position;
				position2.y = 0f;
				this.Rotation = Quaternion.LookRotation(position2 - position);
				player.CommCompanion = this;
				player.PlayerController.ChangeState("Communication");
				this.ChangeADVAnimator();
				player.Rotation = Quaternion.LookRotation(position - position2);
				this.ChangeADVCameraNoBlend(player);
				Transform transform = player.CameraControl.CameraComponent.transform;
				this.SetLookPtn(1, 3);
				this.SetLookTarget(1, 0, transform);
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Observable.TimerFrame(30, FrameCountType.Update).Subscribe(delegate(long __)
					{
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					});
				});
			});
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x00293BBC File Offset: 0x00291FBC
		private void OpenDataFindLoadEvent(string fileName)
		{
			int num = 6;
			this.AgentData.advEventLimitation.Add(num);
			this.AgentData.GetAdvEventCheck(num).Add(fileName);
			this.openData.FindLoad(fileName, this.charaID, num);
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x00293C03 File Offset: 0x00292003
		public void AdvEventStart()
		{
			this.AdvEventStart(this.advEventName);
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x00293C14 File Offset: 0x00292014
		private void AdvEventStart(string fileName)
		{
			this.packData.Init();
			int desireKey = Desire.GetDesireKey(this.RequestedDesire);
			this.SetDesire(desireKey, 0f);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.PlayerController.ChangeState("Idle");
			this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			this.DisableBehavior();
			base.DeactivateNavMeshAgent();
			this.OpenDataFindLoadEvent(fileName);
			this.packData.onComplete = delegate()
			{
				this.packData.restoreCommands = null;
				this.SetLookPtn(0, 3);
				this.SetLookTarget(0, 0, null);
				this.ResetActionFlag();
				this.Animation.StopAllAnimCoroutine();
				this.Animation.ResetDefaultAnimatorController();
				if (this.CurrentPoint != null)
				{
					this.CurrentPoint.SetActiveMapItemObjs(true);
					this.CurrentPoint.ReleaseSlot(this);
					this.CurrentPoint = null;
				}
				this.RevertCameraNoBlend(player);
				if (this.AgentData.CarryingItem != null)
				{
					this.AgentData.CarryingItem = null;
				}
				this.EnableBehavior();
				this.ActivateNavMeshAgent();
				Singleton<Manager.Map>.Instance.EnableEntity(this);
				MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					this.ChangeBehavior(Desire.ActionType.Normal);
					player.PlayerController.ChangeState("Normal");
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.StorySupportUI.Open();
					this.packData.Release();
				});
			};
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			if (MapUIContainer.FadeCanvas.IsFading)
			{
				MapUIContainer.FadeCanvas.SkipFade();
			}
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Singleton<Manager.Map>.Instance.DisableEntity(this);
				AgentAdvEventInfo.Param advEventParam = this.advEventParam;
				int? num = (advEventParam != null) ? new int?(advEventParam.ExpansionID) : null;
				if (num != null)
				{
					int value = num.Value;
					if (value == 2)
					{
						if (this.CurrentPoint != null)
						{
							EventPivot component = this.CurrentPoint.GetComponent<EventPivot>();
							if (component != null && component.PivotTransform != null)
							{
								Transform pivotTransform = component.PivotTransform;
								this.Position = pivotTransform.position;
								this.Rotation = pivotTransform.rotation;
							}
						}
					}
				}
				AgentAdvEventInfo.Param advEventParam2 = this.advEventParam;
				AgentAdvEventInfo.EventPosition eventPosition = (advEventParam2 != null) ? advEventParam2.EventPos : null;
				Dictionary<int, Transform> dictionary;
				Transform transform;
				if (eventPosition != null && eventPosition.isOrder && Singleton<Manager.Map>.Instance.EventStartPointDic.TryGetValue(eventPosition.mapID, out dictionary) && dictionary.TryGetValue(eventPosition.ID, out transform))
				{
					this.Position = transform.position;
					this.Rotation = transform.rotation;
				}
				AgentAdvEventInfo.Param advEventParam3 = this.advEventParam;
				bool? flag = (advEventParam3 != null) ? new bool?(advEventParam3.LookPlayer) : null;
				if (flag == null || flag.Value)
				{
					Vector3 position = this.Position;
					position.y = 0f;
					Vector3 position2 = player.Position;
					position2.y = 0f;
					this.Rotation = Quaternion.LookRotation(position2 - position);
					player.Rotation = Quaternion.LookRotation(position - position2);
				}
				player.CommCompanion = this;
				player.PlayerController.ChangeState("Communication");
				this.SetActiveOnEquipedItem(false);
				this.ChangeADVAnimator();
				this.ChangeADVCameraNoBlend(player);
				Transform transform2 = player.CameraControl.CameraComponent.transform;
				this.SetLookPtn(1, 3);
				this.SetLookTarget(1, 0, transform2);
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Observable.TimerFrame(30, FrameCountType.Update).Subscribe(delegate(long __)
					{
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					});
				});
			});
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x00293D18 File Offset: 0x00292118
		public void AdvEventStart_SleepingPlayer(PlayerActor player)
		{
			this.packData.Init();
			int desireKey = Desire.GetDesireKey(this.RequestedDesire);
			this.SetDesire(desireKey, 0f);
			this.Animation.StopAllAnimCoroutine();
			base.SetActiveOnEquipedItem(false);
			base.ChaControl.setAllLayerWeight(0f);
			player.PlayerController.ChangeState("Idle");
			this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			this.DisableBehavior();
			base.DeactivateNavMeshAgent();
			MapUIContainer.SetVisibleHUD(false);
			<>__AnonType17<Vector3, Quaternion> bkData = new
			{
				base.Position,
				base.Rotation
			};
			base.Position = player.Position;
			base.Rotation = player.Rotation;
			this.ChangeADVAnimator();
			Transform transform = player.CameraControl.CameraComponent.transform;
			base.SetLookPtn(1, 3);
			base.SetLookTarget(1, 0, transform);
			this.OpenDataFindLoadEvent(this.advEventName);
			System.Action cameraBlendStyleChange = delegate()
			{
				CinemachineBlendDefinition.Style prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
				player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
				Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
				{
					player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
				});
			};
			this.packData.onComplete = delegate()
			{
				this.packData.restoreCommands = null;
				this.SetLookPtn(0, 3);
				this.SetLookTarget(0, 0, null);
				this.ResetActionFlag();
				this.Animation.StopAllAnimCoroutine();
				this.Animation.ResetDefaultAnimatorController();
				player.CameraControl.Mode = CameraMode.Normal;
				player.ChaControl.visibleAll = (player.CameraControl.ShotType != ShotType.PointOfView);
				cameraBlendStyleChange();
				this.Position = bkData.Position;
				this.Rotation = bkData.Rotation;
				if (this.AgentData.CarryingItem != null)
				{
					this.AgentData.CarryingItem = null;
				}
				this.EnableBehavior();
				this.ActivateNavMeshAgent();
				this.packData.Release();
				MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					Singleton<Manager.Map>.Instance.EnableEntity(this);
					this.ChangeBehavior(Desire.ActionType.Normal);
					player.PlayerController.ChangeState("Normal");
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.StorySupportUI.Open();
				});
			};
			ADV.ChangeADVCamera(this);
			cameraBlendStyleChange();
			Singleton<Manager.Map>.Instance.DisableEntity(this);
			Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
			{
				Observable.TimerFrame(30, FrameCountType.Update).Subscribe(delegate(long __)
				{
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				});
			});
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x00293EA8 File Offset: 0x002922A8
		private void StartPhaseEvent()
		{
			AgentActor.<StartPhaseEvent>c__AnonStorey36 <StartPhaseEvent>c__AnonStorey = new AgentActor.<StartPhaseEvent>c__AnonStorey36();
			<StartPhaseEvent>c__AnonStorey.$this = this;
			base.StopNavMeshAgent();
			this.ChangeStaticNavMeshAvoidanceUnencounter();
			this.packData.Init();
			<StartPhaseEvent>c__AnonStorey.player = Singleton<Manager.Map>.Instance.Player;
			<StartPhaseEvent>c__AnonStorey.prevMode = this.BehaviorResources.Mode;
			this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			<StartPhaseEvent>c__AnonStorey.player.CommCompanion = this;
			<StartPhaseEvent>c__AnonStorey.player.PlayerController.ChangeState("Idle");
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			int phase = base.ChaControl.fileGameInfo.phase;
			this.openData.FindLoad(string.Format("{0}", phase + 1), this.AgentData.param.charaID, 6);
			this.packData.onComplete = delegate()
			{
				<StartPhaseEvent>c__AnonStorey.$this.packData.restoreCommands = null;
				<StartPhaseEvent>c__AnonStorey.$this.packData.Release();
				<StartPhaseEvent>c__AnonStorey.$this.SetLookPtn(0, 3);
				<StartPhaseEvent>c__AnonStorey.$this.SetLookTarget(0, 0, null);
				if (<StartPhaseEvent>c__AnonStorey.$this.AttitudeID == 0)
				{
					<StartPhaseEvent>c__AnonStorey.$this.Animation.StopAllAnimCoroutine();
					<StartPhaseEvent>c__AnonStorey.$this.Animation.ResetDefaultAnimatorController();
				}
				<StartPhaseEvent>c__AnonStorey.$this.RevertCameraNoBlend(<StartPhaseEvent>c__AnonStorey.player);
				<StartPhaseEvent>c__AnonStorey.$this.SetPhase(<StartPhaseEvent>c__AnonStorey.$this.ChaControl.fileGameInfo.phase + 1);
				Singleton<Manager.Map>.Instance.EnableEntity(<StartPhaseEvent>c__AnonStorey.$this);
				MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					<StartPhaseEvent>c__AnonStorey.$this.RecoverNavMeshAvoidanceUnencounter();
					<StartPhaseEvent>c__AnonStorey.$this.BehaviorResources.ChangeMode(<StartPhaseEvent>c__AnonStorey.prevMode);
					<StartPhaseEvent>c__AnonStorey.player.PlayerController.ChangeState("Normal");
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.StorySupportUI.Open();
					<StartPhaseEvent>c__AnonStorey.$this.packData.Release();
				});
			};
			if (MapUIContainer.FadeCanvas.IsFading)
			{
				MapUIContainer.FadeCanvas.SkipFade();
			}
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Singleton<Manager.Map>.Instance.DisableEntity(<StartPhaseEvent>c__AnonStorey.$this);
				<StartPhaseEvent>c__AnonStorey.player.CommCompanion = <StartPhaseEvent>c__AnonStorey.$this;
				<StartPhaseEvent>c__AnonStorey.player.PlayerController.ChangeState("Communication");
				<StartPhaseEvent>c__AnonStorey.$this.SetActiveOnEquipedItem(false);
				Vector3 position = <StartPhaseEvent>c__AnonStorey.$this.Position;
				position.y = 0f;
				Vector3 position2 = <StartPhaseEvent>c__AnonStorey.player.Position;
				position2.y = 0f;
				<StartPhaseEvent>c__AnonStorey.$this.Rotation = Quaternion.LookRotation(position2 - position);
				<StartPhaseEvent>c__AnonStorey.$this.ChangeADVAnimator();
				<StartPhaseEvent>c__AnonStorey.player.Rotation = Quaternion.LookRotation(position - position2);
				CinemachineBlendDefinition.Style prevStyle = <StartPhaseEvent>c__AnonStorey.player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
				<StartPhaseEvent>c__AnonStorey.player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
				ADV.ChangeADVCamera(<StartPhaseEvent>c__AnonStorey.$this);
				Observable.EveryUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
				{
					<StartPhaseEvent>c__AnonStorey.player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
				});
				Transform transform = <StartPhaseEvent>c__AnonStorey.player.CameraControl.CameraComponent.transform;
				<StartPhaseEvent>c__AnonStorey.$this.SetLookPtn(1, 3);
				<StartPhaseEvent>c__AnonStorey.$this.SetLookTarget(1, 0, transform);
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => <StartPhaseEvent>c__AnonStorey.player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Observable.TimerFrame(30, FrameCountType.Update).Subscribe(delegate(long __)
					{
						Singleton<MapUIContainer>.Instance.OpenADV(<StartPhaseEvent>c__AnonStorey.openData, <StartPhaseEvent>c__AnonStorey.packData);
					});
				});
			});
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x00293FE8 File Offset: 0x002923E8
		private void StartAppendRegularEvent()
		{
			base.StopNavMeshAgent();
			this.ChangeStaticNavMeshAvoidanceUnencounter();
			this.packData.Init();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			Desire.ActionType prevMode = this.BehaviorResources.Mode;
			this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			player.CommCompanion = this;
			player.PlayerController.ChangeState("Idle");
			int item = 6;
			this.AgentData.advEventLimitation.Add(item);
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			List<string> agentRandEvents = Singleton<Manager.Resources>.Instance.GameInfo.GetAgentRandEvents(this);
			string element = agentRandEvents.GetElement(UnityEngine.Random.Range(0, agentRandEvents.Count));
			this.openData.FindLoad(element, this.AgentData.param.charaID, 14);
			this.packData.onComplete = delegate()
			{
				this.packData.restoreCommands = null;
				this.packData.Release();
				this.SetLookPtn(0, 3);
				this.SetLookTarget(0, 0, null);
				if (this.AttitudeID == 0)
				{
					this.Animation.StopAllAnimCoroutine();
					this.Animation.ResetDefaultAnimatorController();
				}
				this.RevertCameraNoBlend(player);
				int randomValue = Singleton<Manager.Resources>.Instance.AgentProfile.DayRandElapseCheck.RandomValue;
				this.AgentData.SetADVEventTimeCond(randomValue);
				this.AgentData.ResetADVEventTimeCount();
				Singleton<Manager.Map>.Instance.EnableEntity(this);
				MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					this.RecoverNavMeshAvoidanceUnencounter();
					this.BehaviorResources.ChangeMode(prevMode);
					player.PlayerController.ChangeState("Normal");
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.StorySupportUI.Open();
					this.packData.Release();
				});
			};
			if (MapUIContainer.FadeCanvas.IsFading)
			{
				MapUIContainer.FadeCanvas.SkipFade();
			}
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Singleton<Manager.Map>.Instance.DisableEntity(this);
				player.CommCompanion = this;
				player.PlayerController.ChangeState("Communication");
				this.SetActiveOnEquipedItem(false);
				Vector3 position = this.Position;
				position.y = 0f;
				Vector3 position2 = player.Position;
				position2.y = 0f;
				this.Rotation = Quaternion.LookRotation(position2 - position);
				this.ChangeADVAnimator();
				player.Rotation = Quaternion.LookRotation(position - position2);
				this.ChangeADVCameraNoBlend(player);
				Transform transform = player.CameraControl.CameraComponent.transform;
				this.SetLookPtn(1, 3);
				this.SetLookTarget(1, 0, transform);
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Observable.TimerFrame(30, FrameCountType.Update).Subscribe(delegate(long __)
					{
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					});
				});
			});
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x00294140 File Offset: 0x00292540
		private void StartSicknessEvent(int id)
		{
			AgentActor.<StartSicknessEvent>c__AnonStorey39 <StartSicknessEvent>c__AnonStorey = new AgentActor.<StartSicknessEvent>c__AnonStorey39();
			<StartSicknessEvent>c__AnonStorey.id = id;
			<StartSicknessEvent>c__AnonStorey.$this = this;
			this.EndCommonSelection();
			MapUIContainer.CommandList.Visibled = false;
			MapUIContainer.ReserveSystemMenuMode(SystemMenuUI.MenuMode.InventoryEnter);
			<StartSicknessEvent>c__AnonStorey.systemUI = MapUIContainer.SystemMenuUI;
			<StartSicknessEvent>c__AnonStorey.inventoryUI = <StartSicknessEvent>c__AnonStorey.systemUI.InventoryEnterUI;
			<StartSicknessEvent>c__AnonStorey.agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			Dictionary<int, List<int>> dictionary = DictionaryPool<int, List<int>>.Get();
			int id2 = this.AgentData.SickState.ID;
			ItemIDKeyPair[] array;
			if (id2 == 0 && !this.AgentData.SickState.UsedMedicine && !this.AgentData.SickState.Enabled)
			{
				array = <StartSicknessEvent>c__AnonStorey.agentProfile.MedicineColdItemList;
			}
			else if (id2 == 4 && !this.AgentData.SickState.Enabled)
			{
				array = <StartSicknessEvent>c__AnonStorey.agentProfile.MedicineHurtItemList;
			}
			else if (id2 == 1)
			{
				array = <StartSicknessEvent>c__AnonStorey.agentProfile.MedicineStomachacheItemList;
			}
			else if (id2 == 3)
			{
				array = <StartSicknessEvent>c__AnonStorey.agentProfile.MedicineHeatStrokeItemList;
			}
			else
			{
				array = <StartSicknessEvent>c__AnonStorey.agentProfile.MedicineNormalItemList;
			}
			foreach (ItemIDKeyPair itemIDKeyPair in array)
			{
				List<int> list;
				if (!dictionary.TryGetValue(itemIDKeyPair.categoryID, out list))
				{
					List<int> list2 = ListPool<int>.Get();
					dictionary[itemIDKeyPair.categoryID] = list2;
					list = list2;
				}
				list.Add(itemIDKeyPair.itemID);
			}
			<StartSicknessEvent>c__AnonStorey.inventoryUI.isConfirm = true;
			<StartSicknessEvent>c__AnonStorey.inventoryUI.CountViewerVisible(false);
			<StartSicknessEvent>c__AnonStorey.inventoryUI.EmptyTextAutoVisible(true);
			InventoryFacadeViewer.ItemFilter[] itemFilter = (from x in dictionary
			select new InventoryFacadeViewer.ItemFilter(x.Key, x.Value.ToArray())).ToArray<InventoryFacadeViewer.ItemFilter>();
			<StartSicknessEvent>c__AnonStorey.inventoryUI.SetItemFilter(itemFilter);
			int[] array3 = dictionary.Keys.ToArray<int>();
			foreach (int key in array3)
			{
				ListPool<int>.Release(dictionary[key]);
			}
			DictionaryPool<int, List<int>>.Release(dictionary);
			<StartSicknessEvent>c__AnonStorey.inventoryUI.itemList = (() => Singleton<Manager.Map>.Instance.Player.PlayerData.ItemList);
			<StartSicknessEvent>c__AnonStorey.inventoryUI.itemList_System = null;
			<StartSicknessEvent>c__AnonStorey.inventoryUI.DoubleClickAction(null);
			<StartSicknessEvent>c__AnonStorey.isVisibleCommand = true;
			<StartSicknessEvent>c__AnonStorey.inventoryUI.OnSubmit = delegate(StuffItem item)
			{
				if (Singleton<Game>.Instance.Dialog != null)
				{
					Singleton<Game>.Instance.Dialog.TimeScale = 1f;
				}
				<StartSicknessEvent>c__AnonStorey.isVisibleCommand = false;
				InventoryUIController inventoryUI = <StartSicknessEvent>c__AnonStorey.inventoryUI;
				if (inventoryUI != null)
				{
					System.Action onClose = inventoryUI.OnClose;
					if (onClose != null)
					{
						onClose();
					}
				}
				MapUIContainer.SetActiveCommandList(false);
				Desire.ActionType prevMode = <StartSicknessEvent>c__AnonStorey.$this.BehaviorResources.Mode;
				<StartSicknessEvent>c__AnonStorey.$this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				Observable.EveryLateUpdate().Take(20).Subscribe(delegate(long _)
				{
					<StartSicknessEvent>c__AnonStorey.StopNavMeshAgent();
				});
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				player.PlayerController.ChangeState("Idle");
				PoseKeyPair medicID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.MedicID;
				bool flag = item.MatchItem(<StartSicknessEvent>c__AnonStorey.agentProfile.ColdMedicineID) || item.MatchItem(<StartSicknessEvent>c__AnonStorey.agentProfile.FeverReducerID);
				if (flag)
				{
					if (<StartSicknessEvent>c__AnonStorey.id == 0)
					{
						<StartSicknessEvent>c__AnonStorey.$this.openData.FindLoad("0", <StartSicknessEvent>c__AnonStorey.$this.charaID, 11);
						<StartSicknessEvent>c__AnonStorey.$this.packData.onComplete = delegate()
						{
							<StartSicknessEvent>c__AnonStorey.packData.restoreCommands = null;
							<StartSicknessEvent>c__AnonStorey.SetLookPtn(0, 3);
							<StartSicknessEvent>c__AnonStorey.SetLookTarget(0, 0, null);
							if (<StartSicknessEvent>c__AnonStorey.AttitudeID == 0)
							{
								<StartSicknessEvent>c__AnonStorey.Animation.StopAllAnimCoroutine();
								<StartSicknessEvent>c__AnonStorey.Animation.ResetDefaultAnimatorController();
							}
							<StartSicknessEvent>c__AnonStorey.ClearItems();
							<StartSicknessEvent>c__AnonStorey.ClearParticles();
							if (item.MatchItem(<StartSicknessEvent>c__AnonStorey.agentProfile.ColdMedicineID))
							{
								<StartSicknessEvent>c__AnonStorey.AgentData.SickState.ID = -1;
								<StartSicknessEvent>c__AnonStorey.SetStatus(0, 50f);
								<StartSicknessEvent>c__AnonStorey.AgentData.ColdLockInfo.Lock = true;
								<StartSicknessEvent>c__AnonStorey.ApplySituationResultParameter(0);
							}
							else if (item.MatchItem(<StartSicknessEvent>c__AnonStorey.agentProfile.FeverReducerID))
							{
								<StartSicknessEvent>c__AnonStorey.AgentData.SickState.UsedMedicine = true;
								int num = UnityEngine.Random.Range(0, 100);
								if (num < 30)
								{
									<StartSicknessEvent>c__AnonStorey.AgentData.SickState.ID = -1;
									<StartSicknessEvent>c__AnonStorey.SetStatus(0, 50f);
									<StartSicknessEvent>c__AnonStorey.AgentData.ColdLockInfo.Lock = true;
									<StartSicknessEvent>c__AnonStorey.ApplySituationResultParameter(0);
								}
							}
							<StartSicknessEvent>c__AnonStorey.EnableBehavior();
							<StartSicknessEvent>c__AnonStorey.BehaviorResources.ChangeMode(prevMode);
							if (<StartSicknessEvent>c__AnonStorey.AgentData.TalkMotivation <= 0f)
							{
								<StartSicknessEvent>c__AnonStorey.AgentData.LockTalk = true;
								<StartSicknessEvent>c__AnonStorey.AgentData.TalkElapsedTime = 0f;
								if (player.Mode == Desire.ActionType.Date && player.Partner == <StartSicknessEvent>c__AnonStorey.$this)
								{
									<StartSicknessEvent>c__AnonStorey.DeactivatePairing(0);
									<StartSicknessEvent>c__AnonStorey.ActivateHoldingHands(0, false);
								}
							}
							player.CameraControl.Mode = CameraMode.Normal;
							player.CameraControl.VanishControl.VisibleForceVanish(true);
							player.CameraControl.VanishControl.ResetVanish();
							if (Singleton<Manager.Housing>.IsInstance())
							{
								Singleton<Manager.Housing>.Instance.EndShield();
							}
							player.PlayerController.ChangeState("Normal");
							MapUIContainer.SetVisibleHUD(true);
							<StartSicknessEvent>c__AnonStorey.packData.Release();
						};
						Singleton<MapUIContainer>.Instance.OpenADV(<StartSicknessEvent>c__AnonStorey.$this.openData, <StartSicknessEvent>c__AnonStorey.$this.packData);
					}
					else if (<StartSicknessEvent>c__AnonStorey.id == 1)
					{
						CinemachineBlendDefinition.Style prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
						player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
						ADV.ChangeADVFixedAngleCamera(<StartSicknessEvent>c__AnonStorey.$this, 2);
						Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
						{
							player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
						});
						Observable.FromCoroutine(() => <StartSicknessEvent>c__AnonStorey.AnimCoroutine(medicID.postureID, medicID.poseID), false).Subscribe(delegate(Unit _)
						{
							if (item.MatchItem(<StartSicknessEvent>c__AnonStorey.agentProfile.ColdMedicineID))
							{
								<StartSicknessEvent>c__AnonStorey.AgentData.SickState.Enabled = true;
								<StartSicknessEvent>c__AnonStorey.AgentData.SickState.UsedMedicine = true;
								if (<StartSicknessEvent>c__AnonStorey.Mode == Desire.ActionType.Cold2B)
								{
									<StartSicknessEvent>c__AnonStorey.openData.FindLoad("1", <StartSicknessEvent>c__AnonStorey.charaID, 11);
								}
								else if (<StartSicknessEvent>c__AnonStorey.Mode == Desire.ActionType.Cold3B)
								{
									<StartSicknessEvent>c__AnonStorey.openData.FindLoad("2", <StartSicknessEvent>c__AnonStorey.charaID, 11);
								}
							}
							else if (item.MatchItem(<StartSicknessEvent>c__AnonStorey.agentProfile.FeverReducerID))
							{
								int num = UnityEngine.Random.Range(0, 100);
								if (num < 30)
								{
									<StartSicknessEvent>c__AnonStorey.AgentData.SickState.Enabled = true;
								}
								<StartSicknessEvent>c__AnonStorey.AgentData.SickState.UsedMedicine = true;
								if (<StartSicknessEvent>c__AnonStorey.Mode == Desire.ActionType.Cold2B)
								{
									<StartSicknessEvent>c__AnonStorey.openData.FindLoad("1", <StartSicknessEvent>c__AnonStorey.charaID, 11);
								}
								else if (<StartSicknessEvent>c__AnonStorey.Mode == Desire.ActionType.Cold3B)
								{
									<StartSicknessEvent>c__AnonStorey.openData.FindLoad("2", <StartSicknessEvent>c__AnonStorey.charaID, 11);
								}
							}
							<StartSicknessEvent>c__AnonStorey.packData.onComplete = delegate()
							{
								<StartSicknessEvent>c__AnonStorey.packData.restoreCommands = null;
								<StartSicknessEvent>c__AnonStorey.SetLookPtn(0, 3);
								<StartSicknessEvent>c__AnonStorey.SetLookTarget(0, 0, null);
								<StartSicknessEvent>c__AnonStorey.ClearItems();
								<StartSicknessEvent>c__AnonStorey.ClearParticles();
								<StartSicknessEvent>c__AnonStorey.EnableBehavior();
								if (<StartSicknessEvent>c__AnonStorey.Mode == Desire.ActionType.Cold2B)
								{
									<StartSicknessEvent>c__AnonStorey.BehaviorResources.ChangeMode(<StartSicknessEvent>c__AnonStorey.Mode);
									if (<StartSicknessEvent>c__AnonStorey.AgentData.SickState.Enabled)
									{
										AIProject.SaveData.Sickness sickState = <StartSicknessEvent>c__AnonStorey.AgentData.SickState;
										sickState.Duration = sickState.ElapsedTime + TimeSpan.FromHours(12.0);
									}
								}
								else if (<StartSicknessEvent>c__AnonStorey.Mode == Desire.ActionType.Cold3B)
								{
									<StartSicknessEvent>c__AnonStorey.BehaviorResources.ChangeMode(<StartSicknessEvent>c__AnonStorey.Mode);
									if (<StartSicknessEvent>c__AnonStorey.AgentData.SickState.Enabled)
									{
										AIProject.SaveData.Sickness sickState2 = <StartSicknessEvent>c__AnonStorey.AgentData.SickState;
										if (sickState2.ElapsedTime.Days >= 6)
										{
											sickState2.Duration = sickState2.ElapsedTime + TimeSpan.FromHours(1.0);
										}
									}
								}
								player.CameraControl.Mode = CameraMode.Normal;
								player.CameraControl.VanishControl.VisibleForceVanish(true);
								player.CameraControl.VanishControl.ResetVanish();
								if (Singleton<Manager.Housing>.IsInstance())
								{
									Singleton<Manager.Housing>.Instance.EndShield();
								}
								player.PlayerController.ChangeState("Normal");
								MapUIContainer.SetVisibleHUD(true);
								<StartSicknessEvent>c__AnonStorey.packData.Release();
							};
							Singleton<MapUIContainer>.Instance.OpenADV(<StartSicknessEvent>c__AnonStorey.openData, <StartSicknessEvent>c__AnonStorey.packData);
						});
					}
				}
				else if (<StartSicknessEvent>c__AnonStorey.id == 0)
				{
					<StartSicknessEvent>c__AnonStorey.$this.openData.FindLoad("3", <StartSicknessEvent>c__AnonStorey.$this.charaID, 11);
					<StartSicknessEvent>c__AnonStorey.$this.packData.onComplete = delegate()
					{
						StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
						if (item.ID == 1)
						{
							<StartSicknessEvent>c__AnonStorey.AgentData.ItemList.Add(item);
							if (<StartSicknessEvent>c__AnonStorey.AgentData.SickState.ID == 4)
							{
								<StartSicknessEvent>c__AnonStorey.AgentData.SickState.Enabled = true;
							}
						}
						else if (item.ID == 2)
						{
							<StartSicknessEvent>c__AnonStorey.AddStatus(6, statusProfile.PotionImmoralAdd);
						}
						else if (item.ID == 3)
						{
							int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
							<StartSicknessEvent>c__AnonStorey.AddDesire(desireKey, statusProfile.DiureticToiletAdd);
						}
						else if (item.ID == 4)
						{
							int desireKey2 = Desire.GetDesireKey(Desire.Type.Sleep);
							<StartSicknessEvent>c__AnonStorey.AddDesire(desireKey2, statusProfile.PillSleepAdd);
						}
						else if (item.ID == 8)
						{
							<StartSicknessEvent>c__AnonStorey.AgentData.SickState.ID = -1;
						}
						else if (item.ID == 9)
						{
							<StartSicknessEvent>c__AnonStorey.AgentData.SickState.ID = -1;
							<StartSicknessEvent>c__AnonStorey.AgentData.HeatStrokeLockInfo.Lock = true;
							<StartSicknessEvent>c__AnonStorey.SetStatus(0, 50f);
						}
						<StartSicknessEvent>c__AnonStorey.packData.restoreCommands = null;
						<StartSicknessEvent>c__AnonStorey.SetLookPtn(0, 3);
						<StartSicknessEvent>c__AnonStorey.SetLookTarget(0, 0, null);
						if (<StartSicknessEvent>c__AnonStorey.AttitudeID == 0)
						{
							<StartSicknessEvent>c__AnonStorey.Animation.StopAllAnimCoroutine();
							<StartSicknessEvent>c__AnonStorey.Animation.ResetDefaultAnimatorController();
						}
						<StartSicknessEvent>c__AnonStorey.ClearItems();
						<StartSicknessEvent>c__AnonStorey.ClearParticles();
						<StartSicknessEvent>c__AnonStorey.EnableBehavior();
						<StartSicknessEvent>c__AnonStorey.BehaviorResources.ChangeMode(prevMode);
						if (<StartSicknessEvent>c__AnonStorey.AgentData.TalkMotivation <= 0f)
						{
							<StartSicknessEvent>c__AnonStorey.AgentData.LockTalk = true;
							<StartSicknessEvent>c__AnonStorey.AgentData.TalkElapsedTime = 0f;
							if (player.Mode == Desire.ActionType.Date && player.Partner == <StartSicknessEvent>c__AnonStorey.$this)
							{
								<StartSicknessEvent>c__AnonStorey.DeactivatePairing(0);
								<StartSicknessEvent>c__AnonStorey.ActivateHoldingHands(0, false);
							}
						}
						player.CameraControl.Mode = CameraMode.Normal;
						player.CameraControl.VanishControl.VisibleForceVanish(true);
						player.CameraControl.VanishControl.ResetVanish();
						if (Singleton<Manager.Housing>.IsInstance())
						{
							Singleton<Manager.Housing>.Instance.EndShield();
						}
						player.PlayerController.ChangeState("Normal");
						MapUIContainer.SetVisibleHUD(true);
						<StartSicknessEvent>c__AnonStorey.packData.Release();
					};
					Singleton<MapUIContainer>.Instance.OpenADV(<StartSicknessEvent>c__AnonStorey.$this.openData, <StartSicknessEvent>c__AnonStorey.$this.packData);
				}
				else if (<StartSicknessEvent>c__AnonStorey.id == 1)
				{
					<StartSicknessEvent>c__AnonStorey.$this.openData.FindLoad("4", <StartSicknessEvent>c__AnonStorey.$this.charaID, 11);
					<StartSicknessEvent>c__AnonStorey.$this.packData.onComplete = delegate()
					{
						StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
						if (item.ID == 2)
						{
							<StartSicknessEvent>c__AnonStorey.AddStatus(6, statusProfile.PotionImmoralAdd);
						}
						else if (item.ID == 3)
						{
							int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
							<StartSicknessEvent>c__AnonStorey.AddDesire(desireKey, statusProfile.DiureticToiletAdd);
						}
						else if (item.ID == 4)
						{
							int desireKey2 = Desire.GetDesireKey(Desire.Type.Sleep);
							<StartSicknessEvent>c__AnonStorey.AddDesire(desireKey2, statusProfile.PillSleepAdd);
						}
						<StartSicknessEvent>c__AnonStorey.packData.restoreCommands = null;
						<StartSicknessEvent>c__AnonStorey.SetLookPtn(0, 3);
						<StartSicknessEvent>c__AnonStorey.SetLookTarget(0, 0, null);
						if (<StartSicknessEvent>c__AnonStorey.AttitudeID == 0)
						{
							<StartSicknessEvent>c__AnonStorey.Animation.StopAllAnimCoroutine();
							<StartSicknessEvent>c__AnonStorey.Animation.ResetDefaultAnimatorController();
						}
						<StartSicknessEvent>c__AnonStorey.ClearItems();
						<StartSicknessEvent>c__AnonStorey.ClearParticles();
						<StartSicknessEvent>c__AnonStorey.EnableBehavior();
						<StartSicknessEvent>c__AnonStorey.BehaviorResources.ChangeMode(prevMode);
						if (<StartSicknessEvent>c__AnonStorey.AgentData.TalkMotivation <= 0f)
						{
							<StartSicknessEvent>c__AnonStorey.AgentData.LockTalk = true;
							<StartSicknessEvent>c__AnonStorey.AgentData.TalkElapsedTime = 0f;
							if (player.Mode == Desire.ActionType.Date && player.Partner == <StartSicknessEvent>c__AnonStorey.$this)
							{
								<StartSicknessEvent>c__AnonStorey.DeactivatePairing(0);
								<StartSicknessEvent>c__AnonStorey.ActivateHoldingHands(0, false);
							}
						}
						player.CameraControl.Mode = CameraMode.Normal;
						player.CameraControl.VanishControl.VisibleForceVanish(true);
						player.CameraControl.VanishControl.ResetVanish();
						if (Singleton<Manager.Housing>.IsInstance())
						{
							Singleton<Manager.Housing>.Instance.EndShield();
						}
						player.PlayerController.ChangeState("Normal");
						MapUIContainer.SetVisibleHUD(true);
						<StartSicknessEvent>c__AnonStorey.packData.Release();
					};
					Singleton<MapUIContainer>.Instance.OpenADV(<StartSicknessEvent>c__AnonStorey.$this.openData, <StartSicknessEvent>c__AnonStorey.$this.packData);
				}
			};
			<StartSicknessEvent>c__AnonStorey.inventoryUI.OnClose = delegate()
			{
				<StartSicknessEvent>c__AnonStorey.inventoryUI.OnSubmit = null;
				<StartSicknessEvent>c__AnonStorey.inventoryUI.IsActiveControl = false;
				<StartSicknessEvent>c__AnonStorey.systemUI.IsActiveControl = false;
				if (<StartSicknessEvent>c__AnonStorey.isVisibleCommand)
				{
					MapUIContainer.CommandList.Visibled = true;
					<StartSicknessEvent>c__AnonStorey.$this.StartCommonSelection();
				}
				Singleton<Manager.Input>.Instance.FocusLevel = 0;
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
				<StartSicknessEvent>c__AnonStorey.inventoryUI.OnClose = null;
			};
			MapUIContainer.SetActiveSystemMenuUI(true);
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x002943EC File Offset: 0x002927EC
		private void StartScroungeEvent(CommCommandList.CommandInfo[] restoreCommands)
		{
			this.EndCommonSelection();
			this.openData.FindLoad("13", this.charaID, 4);
			this.packData.onComplete = delegate()
			{
				MapUIContainer.CommandList.Visibled = false;
				MapUIContainer.ScroungeUI.OnClose = delegate()
				{
					if (!this.AgentData.ItemScrounge.isEvent)
					{
						this.packData.onComplete = delegate()
						{
							MapUIContainer.CommandList.Visibled = true;
							this.StartCommonSelection();
						};
						this.packData.restoreCommands = restoreCommands;
						this.openData.FindLoad("7", this.charaID, 0);
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
					else
					{
						MapUIContainer.CommandList.Refresh(restoreCommands, MapUIContainer.CommandList.CanvasGroup, null);
						MapUIContainer.CommandList.Visibled = true;
						this.StartCommonSelection();
					}
				};
				this.packData.restoreCommands = null;
				MapUIContainer.ScroungeUI.agent = this;
				MapUIContainer.SetActiveScroungeUI(true);
			};
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x00294458 File Offset: 0x00292858
		private IEnumerator AnimCoroutine(int eventID, int poseID)
		{
			PlayState info = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[eventID][poseID];
			this.Animation.LoadEventKeyTable(eventID, poseID);
			ActorAnimInfo animInfo = this.Animation.LoadActionState(eventID, poseID, info);
			this.DisableActionFlag();
			this.Animation.StopAllAnimCoroutine();
			this.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, info.MainStateInfo.FadeOutTime, animInfo.layer);
			while (this.Animation.PlayingInAnimation)
			{
				yield return null;
			}
			this.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			while (this.Animation.PlayingOutAnimation)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x00294484 File Offset: 0x00292884
		private IEnumerator StealAnimCoroutine(int eventID, int poseID)
		{
			PlayState info = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[eventID][poseID];
			this.Animation.LoadEventKeyTable(eventID, poseID);
			ActorAnimInfo animInfo = this.Animation.LoadActionState(eventID, poseID, info);
			this.DisableActionFlag();
			this.Animation.StopAllAnimCoroutine();
			this.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, info.MainStateInfo.FadeOutTime, animInfo.layer);
			while (this.Animation.PlayingInAnimation)
			{
				yield return null;
			}
			base.ClearItems();
			base.ClearParticles();
			yield break;
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x002944B0 File Offset: 0x002928B0
		public void PopupCommands(CommCommandList.CommandInfo[] infos, System.Action onOpen = null)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.RefreshCommands(this.ID, infos);
			MapUIContainer.CommandList.CancelEvent = null;
			MapUIContainer.CommandList.OnOpened = onOpen;
			MapUIContainer.SetActiveCommandList(true, this.CharaName);
			this.InitCommon();
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x00294508 File Offset: 0x00292908
		public void PopupDateCommands(bool isFirstPerson)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			if (isFirstPerson)
			{
				MapUIContainer.RefreshCommands(this.ID, this.DateCommandOptionInfos);
			}
			else
			{
				MapUIContainer.RefreshCommands(this.ID, this.DateCommandOptionInfosTP);
			}
			MapUIContainer.CommandList.CancelEvent = null;
			MapUIContainer.CommandList.OnOpened = delegate()
			{
				this.StartCommonSelection();
				MapUIContainer.CommandList.OnOpened = null;
			};
			MapUIContainer.SetActiveCommandList(true, this.CharaName);
			this.InitCommon();
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x0029458C File Offset: 0x0029298C
		public void VanishCommands()
		{
			this.EndCommonSelection();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.CameraControl.VanishControl.VisibleForceVanish(true);
			player.CameraControl.VanishControl.ResetVanish();
			if (Singleton<Manager.Housing>.IsInstance())
			{
				Singleton<Manager.Housing>.Instance.EndShield();
			}
			if (this.IsEvent)
			{
				this.IsEvent = false;
			}
			MapUIContainer.SetVisibleHUD(true);
			Desire.ActionType mode = player.Mode;
			if (mode == Desire.ActionType.Normal || mode == Desire.ActionType.Date)
			{
				player.CameraControl.Mode = CameraMode.Normal;
				player.Controller.ChangeState("Normal");
			}
			Singleton<Manager.Map>.Instance.Player.ChaControl.visibleAll = true;
			switch (this.AttitudeID)
			{
			case 0:
				this.Animation.StopAllAnimCoroutine();
				this.Animation.ResetDefaultAnimatorController();
				base.SetActiveOnEquipedItem(true);
				this.ActivateTransfer(false);
				this.EnableBehavior();
				break;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				this.EnableBehavior();
				this._schedule.progress = true;
				break;
			default:
				this.EnableBehavior();
				break;
			}
			this.RecoverNavMeshAvoidanceUnencounter();
			base.ChaControl.ChangeLookEyesPtn(0);
			base.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			base.ChaControl.ChangeLookNeckPtn(3, 1f);
			base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x00294726 File Offset: 0x00292B26
		public bool CanSelectHCommand()
		{
			return this.IsOnHMesh(null) && this.CanHCommand && Game.isAdd01;
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x00294747 File Offset: 0x00292B47
		public bool CanSelectHCommand(string[] tagName)
		{
			return this.IsOnHMesh(tagName) && this.CanHCommand && Game.isAdd01;
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x00294768 File Offset: 0x00292B68
		private bool IsOnHMesh(string[] tagName = null)
		{
			LayerMask hlayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.HLayer;
			Vector3 position = base.Position;
			position.y += 15f;
			int num = Physics.SphereCastNonAlloc(position, 7.5f, Vector3.down, this._hits, 25f, hlayer);
			if (num == 0)
			{
				return false;
			}
			bool flag = true;
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = this._hits[i];
				string tag = raycastHit.collider.tag;
				flag = (tag != "Untagged");
				if (!flag)
				{
					break;
				}
				if (!tagName.IsNullOrEmpty<string>())
				{
					bool flag2 = false;
					foreach (string b in tagName)
					{
						if (tag == b)
						{
							flag2 = true;
							break;
						}
					}
					flag = flag2;
				}
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x00294878 File Offset: 0x00292C78
		public void InitiateHScene(HSceneManager.HEvent hEvent = HSceneManager.HEvent.Normal)
		{
			this._recoveryActionPointFromHScene = base.CurrentPoint;
			this._recoveryModeFromHScene = this.Mode;
			this._canTalkCache = this.CanTalk;
			this._attitudeIDCache = this.AttitudeID;
			this._useNeckLookCache = this.UseNeckLook;
			this._canHCommandCache = this.CanHCommand;
			this._isSpecialCache = this.IsSpecial;
			this._hPositionIDCache = this.HPositionID;
			this._hPositionSubIDCache = this.HPositionSubID;
			this._obstacleSizeCache = this._navMeshObstacle.radius;
			this.AgentData.ScheduleEnabled = this._schedule.enabled;
			this.AgentData.ScheduleElapsedTime = this._schedule.elapsedTime;
			this.AgentData.ScheduleDuration = this._schedule.duration;
			if (base.CurrentPoint != null)
			{
				base.CurrentPoint.SetActiveMapItemObjs(true);
				base.CurrentPoint.ReleaseSlot(this);
				base.CurrentPoint = null;
			}
			Singleton<HSceneManager>.Instance.HsceneEnter(this, -1, null, hEvent);
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x00294988 File Offset: 0x00292D88
		public void RecoverAction()
		{
			Singleton<Manager.Map>.Instance.Player.PlayerController.CommandArea.RemoveConsiderationObject(this._recoveryActionPointFromHScene);
			this._recoveryActionPointFromHScene.SetForceImpossible(false);
			this.CanTalk = this._canTalkCache;
			this.AttitudeID = this._attitudeIDCache;
			this.UseNeckLook = this._useNeckLookCache;
			this.CanHCommand = this._canHCommandCache;
			this.IsSpecial = this._isSpecialCache;
			this.HPositionID = this._hPositionIDCache;
			this.HPositionSubID = this._hPositionSubIDCache;
			this._navMeshObstacle.radius = this._obstacleSizeCache;
			this.TargetInSightActionPoint = this._recoveryActionPointFromHScene;
			this.Mode = this._recoveryModeFromHScene;
			this._recoveryActionPointFromHScene.SetSlot(this);
			this.BehaviorResources.ChangeMode(this._recoveryModeFromHScene);
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x00294A5C File Offset: 0x00292E5C
		public void InitiateHScene3P(AgentActor main, AgentActor sub)
		{
			if (base.CurrentPoint != null)
			{
				base.CurrentPoint.SetActiveMapItemObjs(true);
				base.CurrentPoint.ReleaseSlot(this);
				base.CurrentPoint = null;
			}
			Singleton<HSceneManager>.Instance.HsceneEnter(main, -1, sub, HSceneManager.HEvent.Normal);
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x00294AAC File Offset: 0x00292EAC
		private void StartTutorialADV(int id)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			PlayerActor player;
			switch (id)
			{
			case 1:
				this.packData.onComplete = delegate()
				{
					this.packData.restoreCommands = null;
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					List<StuffItem> itemList = player.PlayerData.ItemList;
					FishingDefinePack.IDGroup idinfo = Singleton<Manager.Resources>.Instance.FishingDefinePack.IDInfo;
					FishingDefinePack.ItemIDPair brokenFishingRod = idinfo.BrokenFishingRod;
					itemList.AddItem(new StuffItem(brokenFishingRod.CategoryID, brokenFishingRod.ItemID, 1));
					player.AddTutorialUI(Popup.Tutorial.Type.Collection, false);
					Manager.Map.SetTutorialProgress(3);
					Singleton<Manager.Map>.Instance.CreateTutorialSearchPoint();
					this.ChangeTutorialBehavior(AIProject.Definitions.Tutorial.ActionType.FoodRequest);
					this.EndTutorialADV(true);
				};
				break;
			case 2:
				this.packData.onComplete = delegate()
				{
					this.packData.restoreCommands = null;
					this.ChangeTutorialBehavior(AIProject.Definitions.Tutorial.ActionType.HeadToBase);
					this.EndTutorialADV(true);
				};
				break;
			case 3:
				this.packData.onComplete = delegate()
				{
					this.packData.restoreCommands = null;
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					player.AddTutorialUI(Popup.Tutorial.Type.BasePoint, false);
					Manager.Map.SetTutorialProgress(8);
					this.ChangeTutorialBehavior(AIProject.Definitions.Tutorial.ActionType.GrilledFishRequest);
					this.EndTutorialADV(true);
				};
				break;
			case 4:
				this.packData.onComplete = delegate()
				{
					this.packData.restoreCommands = null;
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					List<StuffItem> itemList = player.PlayerData.ItemList;
					FishingDefinePack.ItemIDPair grilledFish = Singleton<Manager.Resources>.Instance.FishingDefinePack.IDInfo.GrilledFish;
					itemList.RemoveItem(new StuffItem(grilledFish.CategoryID, grilledFish.ItemID, 1));
					Manager.Map.SetTutorialProgress(14);
					Singleton<Manager.Map>.Instance.DestroyTutorialLockArea();
					this.ChangeTutorialBehavior(AIProject.Definitions.Tutorial.ActionType.HeadToAgit);
					float followGirlWaitTime = Singleton<Manager.Resources>.Instance.CommonDefine.Tutorial.FollowGirlWaitTime;
					Observable.Timer(TimeSpan.FromSeconds((double)followGirlWaitTime)).Subscribe(delegate(long _)
					{
						player.CameraControl.XAxisValue = player.Rotation.eulerAngles.y;
						player.CameraControl.YAxisValue = ((player.CameraControl.ShotType != ShotType.PointOfView) ? 0.6f : 0.5f);
						this.EndTutorialADV(true);
					});
				};
				break;
			default:
				return;
			}
			player = Singleton<Manager.Map>.Instance.Player;
			this.ChangeTutorialBehavior(AIProject.Definitions.Tutorial.ActionType.Idle);
			this.openData.FindLoad(string.Format("{0}", id), this.charaID, 100);
			this.packData.Init();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			if (id != 4)
			{
				player.CommCompanion = this;
				player.PlayerController.ChangeState("Communication");
				ADV.ChangeADVCamera(this);
				Vector3 position = player.Position;
				if (Singleton<Manager.Resources>.IsInstance())
				{
					float num = Vector3.Angle(position - base.Position, base.Forward);
					float turnEnableAngle = Singleton<Manager.Resources>.Instance.LocomotionProfile.TurnEnableAngle;
					if (turnEnableAngle <= num)
					{
						this.Animation.StopAllAnimCoroutine();
						PlayState.AnimStateInfo tutorialPersonalIdleState = this.GetTutorialPersonalIdleState();
						this.Animation.PlayTurnAnimation(position, 1f, tutorialPersonalIdleState, false);
					}
					else
					{
						Transform transform = this.Locomotor.transform;
						transform.LookAt(position);
						Vector3 eulerAngles = transform.eulerAngles;
						eulerAngles.x = (eulerAngles.z = 0f);
						transform.eulerAngles = eulerAngles;
					}
				}
				else
				{
					this.Animation.StopAllAnimCoroutine();
					PlayState.AnimStateInfo tutorialPersonalIdleState2 = this.GetTutorialPersonalIdleState();
					this.Animation.PlayTurnAnimation(position, 1f, tutorialPersonalIdleState2, false);
				}
				Transform transform2 = player.CameraControl.CameraComponent.transform;
				base.SetLookPtn(1, 3);
				base.SetLookTarget(1, 0, transform2);
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending || this.Animation.PlayingTurnAnimation).Take(1).DelayFrame(30, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				});
			}
			else
			{
				player.PlayerController.ChangeState("Idle");
				MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
				{
				}, delegate()
				{
					this.Locomotor.transform.localRotation = Quaternion.Euler(0f, 80f, 0f);
					Vector3 vector = this.Forward * 10f + this.Position;
					if (player.NavMeshAgent.enabled)
					{
						player.NavMeshAgent.Warp(vector);
					}
					else
					{
						player.Position = vector;
					}
					player.CommCompanion = this;
					player.PlayerController.ChangeState("Communication");
					Transform transform3 = player.Locomotor.transform;
					transform3.LookAt(this.Position);
					Vector3 eulerAngles2 = transform3.eulerAngles;
					eulerAngles2.x = (eulerAngles2.z = 0f);
					transform3.eulerAngles = eulerAngles2;
					this.ChangeADVCameraNoBlend(player);
					Transform transform4 = player.CameraControl.CameraComponent.transform;
					this.SetLookPtn(1, 3);
					this.SetLookTarget(1, 0, transform4);
					Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
					{
						MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit __)
						{
						}, delegate()
						{
							Observable.TimerFrame(30, FrameCountType.Update).Subscribe(delegate(long __)
							{
								Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
							});
						});
					});
				});
			}
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x00294D80 File Offset: 0x00293180
		private void EndTutorialADV(bool changeNormalMode)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			base.SetLookPtn(0, 3);
			base.SetLookTarget(0, 0, null);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.CameraControl.Mode = CameraMode.Normal;
			if (changeNormalMode)
			{
				player.PlayerController.ChangeState("Normal");
				MapUIContainer.SetVisibleHUD(true);
			}
			this.packData.Release();
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x0600626B RID: 25195 RVA: 0x00294DE7 File Offset: 0x002931E7
		private Dictionary<int, System.Action> presentParameterTable
		{
			get
			{
				return this.GetCache(ref this._presentParameterTable, delegate
				{
					Action<FlavorSkill.Type, int> action = delegate(FlavorSkill.Type type, int add)
					{
						this.AgentData.SetFlavorSkill((int)type, this.ChaControl.fileGameInfo.flavorState[(int)type] + add);
					};
					Dictionary<int, System.Action> dictionary = new Dictionary<int, System.Action>();
					dictionary[0] = delegate()
					{
						action(FlavorSkill.Type.Pheromone, 10);
					};
					dictionary[1] = delegate()
					{
						action(FlavorSkill.Type.Reliability, 10);
					};
					dictionary[2] = delegate()
					{
						action(FlavorSkill.Type.Reason, 10);
					};
					dictionary[3] = delegate()
					{
						action(FlavorSkill.Type.Instinct, 10);
					};
					dictionary[4] = delegate()
					{
						action(FlavorSkill.Type.Dirty, 10);
					};
					dictionary[5] = delegate()
					{
						action(FlavorSkill.Type.Wariness, 10);
					};
					dictionary[6] = delegate()
					{
						action(FlavorSkill.Type.Sociability, 10);
					};
					dictionary[7] = delegate()
					{
						action(FlavorSkill.Type.Darkness, 10);
					};
					dictionary[8] = delegate()
					{
						action(FlavorSkill.Type.Pheromone, 50);
					};
					dictionary[9] = delegate()
					{
						action(FlavorSkill.Type.Reliability, 50);
					};
					dictionary[10] = delegate()
					{
						action(FlavorSkill.Type.Reason, 50);
					};
					dictionary[11] = delegate()
					{
						action(FlavorSkill.Type.Instinct, 50);
					};
					dictionary[12] = delegate()
					{
						action(FlavorSkill.Type.Dirty, 50);
					};
					dictionary[13] = delegate()
					{
						action(FlavorSkill.Type.Wariness, 50);
					};
					dictionary[14] = delegate()
					{
						action(FlavorSkill.Type.Sociability, 50);
					};
					dictionary[15] = delegate()
					{
						action(FlavorSkill.Type.Darkness, 50);
					};
					dictionary[16] = delegate()
					{
						action(FlavorSkill.Type.Pheromone, 100);
					};
					dictionary[17] = delegate()
					{
						action(FlavorSkill.Type.Reliability, 100);
					};
					dictionary[18] = delegate()
					{
						action(FlavorSkill.Type.Reason, 100);
					};
					dictionary[19] = delegate()
					{
						action(FlavorSkill.Type.Instinct, 100);
					};
					dictionary[20] = delegate()
					{
						action(FlavorSkill.Type.Dirty, 100);
					};
					dictionary[21] = delegate()
					{
						action(FlavorSkill.Type.Wariness, 100);
					};
					dictionary[22] = delegate()
					{
						action(FlavorSkill.Type.Sociability, 100);
					};
					dictionary[23] = delegate()
					{
						action(FlavorSkill.Type.Darkness, 100);
					};
					dictionary[24] = delegate()
					{
						action(FlavorSkill.Type.Pheromone, -50);
					};
					dictionary[25] = delegate()
					{
						action(FlavorSkill.Type.Reliability, -50);
					};
					dictionary[26] = delegate()
					{
						action(FlavorSkill.Type.Reason, -50);
					};
					dictionary[27] = delegate()
					{
						action(FlavorSkill.Type.Instinct, -50);
					};
					dictionary[28] = delegate()
					{
						action(FlavorSkill.Type.Dirty, -50);
					};
					dictionary[29] = delegate()
					{
						action(FlavorSkill.Type.Wariness, -50);
					};
					dictionary[30] = delegate()
					{
						action(FlavorSkill.Type.Sociability, -50);
					};
					dictionary[31] = delegate()
					{
						action(FlavorSkill.Type.Darkness, -50);
					};
					return dictionary;
				});
			}
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x00294E04 File Offset: 0x00293204
		private void PresentADV(CommCommandList.CommandInfo[] restoreCommands)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			this.EndCommonSelection();
			MapUIContainer.CommandList.Visibled = false;
			MapUIContainer.ReserveSystemMenuMode(SystemMenuUI.MenuMode.InventoryEnter);
			SystemMenuUI systemUI = MapUIContainer.SystemMenuUI;
			InventoryUIController inventoryUI = systemUI.InventoryEnterUI;
			inventoryUI.isConfirm = true;
			inventoryUI.CountViewerVisible(false);
			inventoryUI.EmptyTextAutoVisible(true);
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			inventoryUI.SetItemFilter(agentProfile.PresentItemFilter);
			inventoryUI.itemList = (() => Singleton<Manager.Map>.Instance.Player.PlayerData.ItemList);
			inventoryUI.itemList_System = null;
			inventoryUI.DoubleClickAction(null);
			bool isVisibleCommand = true;
			inventoryUI.OnSubmit = delegate(StuffItem item)
			{
				if (Singleton<Game>.Instance.Dialog != null)
				{
					Singleton<Game>.Instance.Dialog.TimeScale = 1f;
				}
				this.Animation.StopAllAnimCoroutine();
				isVisibleCommand = false;
				InventoryUIController inventoryUI = inventoryUI;
				if (inventoryUI != null)
				{
					inventoryUI.OnClose();
				}
				if (item != null)
				{
					Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
					Dictionary<int, FoodParameterPacket> dictionary2;
					System.Action action;
					if (Singleton<Manager.Resources>.Instance.GameInfo.FoodParameterTable.TryGetValue(item.CategoryID, out dictionary) && dictionary.TryGetValue(item.ID, out dictionary2))
					{
						this.AgentData.ItemList.AddItem(item);
					}
					else if (Singleton<Manager.Resources>.Instance.GameInfo.DrinkParameterTable.TryGetValue(item.CategoryID, out dictionary) && dictionary.TryGetValue(item.ID, out dictionary2))
					{
						this.AgentData.ItemList.AddItem(item);
					}
					else if (this.presentParameterTable.TryGetValue(item.ID, out action) && action != null)
					{
						action();
					}
				}
				this.openData.FindLoad("0", this.charaID, 3);
				this.packData.onComplete = delegate()
				{
					this.CheckTalkForceEnd(restoreCommands);
					this.AgentData.SetAppendEventFlagCheck(5, true);
				};
				Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
			};
			inventoryUI.OnClose = delegate()
			{
				inventoryUI.OnSubmit = null;
				inventoryUI.IsActiveControl = false;
				systemUI.IsActiveControl = false;
				if (isVisibleCommand)
				{
					MapUIContainer.CommandList.Visibled = true;
					this.StartCommonSelection();
				}
				Singleton<Manager.Input>.Instance.FocusLevel = 0;
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
				inventoryUI.OnClose = null;
			};
			MapUIContainer.SetActiveSystemMenuUI(true);
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x00294F24 File Offset: 0x00293324
		private void SearchFavoriteTargetForADV()
		{
			int? num = null;
			using (IEnumerator<KeyValuePair<int, int>> enumerator = (from v in this.AgentData.FriendlyRelationShipTable
			where v.Key != -99
			orderby v.Value descending
			where v.Value > 0
			select v).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<int, int> keyValuePair = enumerator.Current;
					num = new int?(keyValuePair.Key);
				}
			}
			this.packData.isFavoriteTarget = (num != null);
			this.packData.FavoriteTargetName = string.Empty;
			if (num == null)
			{
				return;
			}
			int value = num.Value;
			AgentActor agentActor;
			if (value == -90)
			{
				this.packData.FavoriteTargetName = Singleton<Manager.Map>.Instance.Merchant.CharaName;
			}
			else if (Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(value, out agentActor))
			{
				this.packData.FavoriteTargetName = agentActor.CharaName;
			}
			else
			{
				this.packData.isFavoriteTarget = false;
			}
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x00295098 File Offset: 0x00293498
		private AgentAdvEventInfo.Param GetAdvEvent(int eventType)
		{
			int num = 6;
			if (this.AgentData.advEventLimitation.Contains(num))
			{
				return null;
			}
			if (this.AgentData.SickState.ID != -1)
			{
				return null;
			}
			if (this.IsBadMood())
			{
				return null;
			}
			int id3 = -1;
			int[] array = null;
			ActionPoint currentPoint = base.CurrentPoint;
			bool flag = currentPoint != null;
			if (flag)
			{
				if (!currentPoint.IDList.IsNullOrEmpty<int>())
				{
					array = currentPoint.IDList;
				}
				else
				{
					id3 = currentPoint.ID;
				}
			}
			IOrderedEnumerable<AgentAdvEventInfo.Param> orderedEnumerable = from x in Singleton<Manager.Resources>.Instance.GameInfo.GetAgentAdvEvents(this).Values.Shuffle<AgentAdvEventInfo.Param>()
			orderby x.SortID descending
			select x;
			using (IEnumerator<AgentAdvEventInfo.Param> enumerator = orderedEnumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AgentAdvEventInfo.Param param = enumerator.Current;
					AgentActor $this = this;
					if (param.EventType == eventType)
					{
						if (!flag || !param.IsStateEmpty)
						{
							if (!this.AgentData.GetAdvEventCheck(num).Contains(param.FileName))
							{
								if (!param.PlaceIDs.Any<int>() || param.PlaceIDs.Contains(base.AreaID))
								{
									if (array == null)
									{
										if (!param.IsState(id3, base.ActionID, base.PoseID))
										{
											continue;
										}
									}
									else if (!array.Any((int id) => param.IsState(id, $this.ActionID, $this.PoseID)))
									{
										continue;
									}
									if (!param.Phases.Any<int>() || param.Phases.Contains(base.ChaControl.fileGameInfo.phase + 1))
									{
										if (param.TimeRound.Check(Singleton<Manager.Map>.Instance.Simulator.Now.Hour))
										{
											if (!param.Weathers.Any<int>() || param.Weathers.Contains((int)Singleton<Manager.Map>.Instance.Simulator.Weather))
											{
												int expansionID = param.ExpansionID;
												switch (expansionID)
												{
												case 12:
													if (!Game.isAdd50)
													{
														continue;
													}
													if (!Singleton<Game>.Instance.WorldData.Environment.CheckPetEvent(Singleton<Manager.Resources>.Instance.StatusProfile.CheckPetCountCond))
													{
														continue;
													}
													break;
												case 13:
												{
													if (!Game.isAdd50)
													{
														continue;
													}
													int num2 = 0;
													foreach (KeyValuePair<int, AgentData> keyValuePair in Singleton<Game>.Instance.WorldData.AgentTable)
													{
														if (keyValuePair.Value.MapID == Singleton<Manager.Map>.Instance.MapID)
														{
															if (Singleton<Manager.Map>.Instance.AgentTable.ContainsKey(keyValuePair.Key) || Singleton<Manager.Map>.Instance.AgentChaFileTable.ContainsKey(keyValuePair.Key))
															{
																num2++;
															}
														}
													}
													if (num2 < 2)
													{
														continue;
													}
													break;
												}
												case 14:
												{
													if (!Game.isAdd50)
													{
														continue;
													}
													int num3 = 0;
													foreach (KeyValuePair<int, ChaFile> keyValuePair2 in Singleton<Manager.Map>.Instance.AgentChaFileTable)
													{
														if (keyValuePair2.Key != this.ID)
														{
															if (keyValuePair2.Value.gameinfo.phase >= 2)
															{
																num3++;
															}
														}
													}
													if (num3 < 1)
													{
														continue;
													}
													break;
												}
												case 15:
												case 16:
												case 17:
												case 18:
												case 19:
												case 20:
												{
													if (!Game.isAdd50)
													{
														continue;
													}
													int num4;
													if (!Singleton<Manager.Resources>.Instance.AgentProfile.DicDayElapseCheck.TryGetValue(param.ExpansionID, out num4))
													{
														continue;
													}
													if (this.AgentData.AssignedDuration.Days < num4)
													{
														continue;
													}
													if (!this.CanStartDayCheck(param.ExpansionID))
													{
														continue;
													}
													break;
												}
												default:
													switch (expansionID)
													{
													case 0:
														if (this.Mode != Desire.ActionType.EndTaskPetAnimal || !base.LivesWithAnimalSequence)
														{
															continue;
														}
														break;
													case 1:
													{
														int num5 = 89;
														PlayerActor player = Singleton<Manager.Map>.Instance.Player;
														if (player == null)
														{
															continue;
														}
														ActionPoint currentPoint2 = player.CurrentPoint;
														if (currentPoint2 == null)
														{
															continue;
														}
														if (!currentPoint2.IDList.IsNullOrEmpty<int>())
														{
															if (!currentPoint2.IDList.Contains(num5))
															{
																continue;
															}
														}
														else if (currentPoint2.ID != num5)
														{
															continue;
														}
														break;
													}
													case 2:
													{
														ActionPoint currentPoint3 = base.CurrentPoint;
														if (!flag)
														{
															continue;
														}
														EventPivot component = currentPoint3.GetComponent<EventPivot>();
														if (component == null || component.PivotTransform == null)
														{
															continue;
														}
														break;
													}
													default:
													{
														int id2 = param.ExpansionID + -3;
														if (!this.AgentData.GetAppendEventFlagCheck(id2))
														{
															if (!Game.isAdd50)
															{
																continue;
															}
															id2 = param.ExpansionID + -3 - 18;
															int num6;
															int num7;
															if (!this.AgentData.TryGetAppendEventFlagParam(id2, out num6) || !Singleton<Manager.Resources>.Instance.StatusProfile.TryGetFlagCount(id2, out num7))
															{
																continue;
															}
															if (this.AgentData.GetAppendEventFlagParam(id2) < Singleton<Manager.Resources>.Instance.StatusProfile.GetFlagCount(id2))
															{
																continue;
															}
														}
														break;
													}
													}
													break;
												}
												return param;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x002957A8 File Offset: 0x00293BA8
		private bool CanStartDayCheck(int id)
		{
			bool result;
			if (!this.AgentData.AppendEventLimitation.TryGetValue(id, out result))
			{
				List<int> list = ListPool<int>.Get();
				foreach (int num in AgentActor._appendDayEventExID)
				{
					if (num < id)
					{
						if (this.AgentData.AppendEventLimitation.ContainsKey(num))
						{
							list.Add(num);
						}
					}
				}
				if (list.Count == 0)
				{
					float value = UnityEngine.Random.value;
					bool flag = value > Singleton<Manager.Resources>.Instance.AgentProfile.DayElapseEventADVRate;
					this.AgentData.AppendEventLimitation[id] = flag;
					result = flag;
				}
				else
				{
					bool flag = false;
					this.AgentData.AppendEventLimitation[id] = flag;
					result = flag;
				}
				ListPool<int>.Release(list);
			}
			return result;
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x00295884 File Offset: 0x00293C84
		private void InitCommon()
		{
			this._ladInfo = new AgentActor.LeaveAloneDisposableInfo();
			this._colDisposableList = new List<AgentActor.ColDisposableInfo>();
			string[,] array = new string[2, 2];
			array[0, 0] = "cf_hit_Mune02_s_L";
			array[0, 1] = "Chara";
			array[1, 0] = "cf_hit_Mune02_s_R";
			array[1, 1] = "Chara";
			string[,] array2 = array;
			ChaControl chaControl = base.ChaControl;
			Transform transform = chaControl.objBodyBone.transform;
			int length = array2.GetLength(0);
			this._touchList = new List<AgentActor.TouchInfo>();
			for (int i = 0; i < length; i++)
			{
				int num = 0;
				GameObject gameObject = transform.FindLoop(array2[i, num++]);
				if (!(gameObject == null))
				{
					Collider component = gameObject.GetComponent<Collider>();
					if (!(component == null))
					{
						AgentActor.TouchInfo item = new AgentActor.TouchInfo(gameObject, component, LayerMask.NameToLayer(array2[i, num++]));
						this._touchList.Add(item);
						this._colDisposableList.Add(new AgentActor.ColDisposableInfo(component, new System.Action(this.OnTouch), new System.Action(this.OnEnter), new System.Action(this.OnExit)));
					}
				}
			}
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x002959C8 File Offset: 0x00293DC8
		private void StartCommonSelection()
		{
			this.StartLeaveAlone();
			foreach (AgentActor.ColDisposableInfo colDisposableInfo in this._colDisposableList)
			{
				colDisposableInfo.Start();
			}
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x00295A2C File Offset: 0x00293E2C
		private void EndCommonSelection()
		{
			if (this._ladInfo != null)
			{
				this._ladInfo.End();
			}
			if (!this._colDisposableList.IsNullOrEmpty<AgentActor.ColDisposableInfo>())
			{
				foreach (AgentActor.ColDisposableInfo colDisposableInfo in this._colDisposableList)
				{
					if (colDisposableInfo != null)
					{
						colDisposableInfo.End();
					}
				}
			}
			if (this._disposableInfo != null)
			{
				this._disposableInfo.End();
			}
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x00295AD4 File Offset: 0x00293ED4
		private void StartLeaveAlone()
		{
			this._ladInfo.End();
			this._ladInfo.Timer = new SingleAssignmentDisposable();
			this._ladInfo.Timer.Disposable = Observable.Timer(TimeSpan.FromSeconds(30.0)).Repeat<long>().Subscribe(delegate(long _)
			{
				this.OnLeaveAlone();
			}).AddTo(this);
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x00295B3C File Offset: 0x00293F3C
		private void OnLeaveAlone()
		{
			this._ladInfo.Timer.Dispose();
			this.openData.FindLoad("9", this.charaID, 0);
			this.packData.onComplete = delegate()
			{
			};
			this.packData.restoreCommands = null;
			this.packData.CommandListVisibleEnabled(false);
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
			Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
			this._ladInfo.Wait = new SingleAssignmentDisposable();
			this._ladInfo.Wait.Disposable = Observable.FromCoroutine(new Func<IEnumerator>(this.CommonEventEnd), false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x00295C10 File Offset: 0x00294010
		private void OnTouch()
		{
			this.TouchCount++;
			this.Animation.StopAllAnimCoroutine();
			this.openData.FindLoad("8", this.charaID, 0);
			this.packData.onComplete = delegate()
			{
			};
			this.packData.restoreCommands = null;
			this.packData.CommandListVisibleEnabled(false);
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
			this._ladInfo.End();
			Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
			foreach (AgentActor.ColDisposableInfo colDisposableInfo in this._colDisposableList)
			{
				colDisposableInfo.End();
			}
			this._disposableInfo.End();
			this._disposableInfo.Wait = new SingleAssignmentDisposable();
			this._disposableInfo.Wait.Disposable = Observable.FromCoroutine(new Func<IEnumerator>(this.CommonEventEnd), false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x00295D50 File Offset: 0x00294150
		private void OnEnter()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Texture2D touchCursorTexture = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.TouchCursorTexture;
			Vector2 hotspot = touchCursorTexture.texelSize * 0.5f;
			Cursor.SetCursor(Singleton<Manager.Resources>.Instance.CommonDefine.Icon.TouchCursorTexture, hotspot, CursorMode.ForceSoftware);
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x00295DA9 File Offset: 0x002941A9
		private void OnExit()
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x00295DB8 File Offset: 0x002941B8
		private IEnumerator CommonEventEnd()
		{
			while (base.ChaControl != null && base.ChaControl.asVoice == null && !this.Animation.PlayingInAnimation)
			{
				yield return null;
			}
			while ((base.ChaControl != null && base.ChaControl.asVoice != null) || this.Animation.PlayingInAnimation)
			{
				yield return null;
			}
			base.ChaControl.ChangeLookEyesPtn(0);
			base.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			this.StartCommonSelection();
			yield break;
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06006279 RID: 25209 RVA: 0x00295DD3 File Offset: 0x002941D3
		public override string CharaName
		{
			get
			{
				return base.ChaControl.fileParam.fullname;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x0600627A RID: 25210 RVA: 0x00295DE5 File Offset: 0x002941E5
		public override ActorAnimation Animation
		{
			[CompilerGenerated]
			get
			{
				return this._animation;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x0600627B RID: 25211 RVA: 0x00295DED File Offset: 0x002941ED
		public ActorAnimationAgent AnimationAgent
		{
			[CompilerGenerated]
			get
			{
				return this._animation;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x0600627C RID: 25212 RVA: 0x00295DF5 File Offset: 0x002941F5
		public override ActorLocomotion Locomotor
		{
			[CompilerGenerated]
			get
			{
				return this._character;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x0600627D RID: 25213 RVA: 0x00295DFD File Offset: 0x002941FD
		public ActorLocomotionAgent LocomotorAgent
		{
			[CompilerGenerated]
			get
			{
				return this._character;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x0600627E RID: 25214 RVA: 0x00295E05 File Offset: 0x00294205
		public override ActorController Controller
		{
			[CompilerGenerated]
			get
			{
				return this._controller;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x0600627F RID: 25215 RVA: 0x00295E0D File Offset: 0x0029420D
		public AgentController AgentController
		{
			[CompilerGenerated]
			get
			{
				return this._controller;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06006280 RID: 25216 RVA: 0x00295E15 File Offset: 0x00294215
		public bool ReleasableCommand
		{
			get
			{
				return !this.IsImpossible;
			}
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x00295E20 File Offset: 0x00294220
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.Mode == Desire.ActionType.Onbu)
			{
				return false;
			}
			if (!this.TutorialMode)
			{
				Desire.ActionType mode = this.BehaviorResources.Mode;
				if (mode == Desire.ActionType.Idle || mode == Desire.ActionType.ChaseToTalk || mode == Desire.ActionType.ChaseLesbianH)
				{
					return false;
				}
			}
			if (this.Mode == Desire.ActionType.SearchActor || this.Mode == Desire.ActionType.SearchBirthdayGift || this.Mode == Desire.ActionType.SearchGift || this.Mode == Desire.ActionType.GiftForceEncounter || this.Mode == Desire.ActionType.SearchH || this.Mode == Desire.ActionType.EndTaskH || this.Mode == Desire.ActionType.SearchRevRape || this.Mode == Desire.ActionType.ReverseRape || this.Mode == Desire.ActionType.SearchPlayerToTalk || this.Mode == Desire.ActionType.WalkWithAgent || this.Mode == Desire.ActionType.WalkWithAgentFollow)
			{
				return false;
			}
			if (this.Mode == Desire.ActionType.InviteSleep || this.Mode == Desire.ActionType.InviteSleepH || this.Mode == Desire.ActionType.InviteEat || this.Mode == Desire.ActionType.InviteBreak)
			{
				return false;
			}
			if (distance > radiusA)
			{
				return false;
			}
			Vector3 position = base.Position;
			position.y = 0f;
			float num = angle / 2f;
			float num2 = Vector3.Angle(position - basePosition, forward);
			return num2 <= num;
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00295F70 File Offset: 0x00294370
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				float radius = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
				if (this.TutorialMode)
				{
					if (!this.TutorialCanTalk)
					{
						return false;
					}
					Vector3 dest;
					if (this._navMeshAgent.isActiveAndEnabled)
					{
						dest = base.Position;
					}
					else
					{
						StoryPoint targetStoryPoint = this.TargetStoryPoint;
						Vector3? vector = (targetStoryPoint != null) ? new Vector3?(targetStoryPoint.Position) : null;
						dest = ((vector == null) ? Vector3.zero : vector.Value);
					}
					flag &= this.IsReachable(nmAgent, dest, radius);
				}
				else if (this._navMeshAgent.isActiveAndEnabled)
				{
					flag &= this.IsReachable(nmAgent, base.Position, radius);
				}
				else if (base.CurrentPoint != null)
				{
					bool flag2 = false;
					foreach (Transform transform in base.CurrentPoint.NavMeshPoints)
					{
						flag2 = this.IsReachable(nmAgent, transform.position, radius);
						if (flag2)
						{
							break;
						}
					}
					if (!flag2)
					{
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x002960F8 File Offset: 0x002944F8
		private bool IsReachable(NavMeshAgent navMeshAgent, Vector3 dest, float radius)
		{
			navMeshAgent.CalculatePath(dest, this._pathForCalc);
			if (this._pathForCalc.status != NavMeshPathStatus.PathComplete)
			{
				return false;
			}
			float num = 0f;
			Vector3[] corners = this._pathForCalc.corners;
			for (int i = 0; i < corners.Length - 1; i++)
			{
				float num2 = Vector3.Distance(corners[i], corners[i + 1]);
				num += num2;
				if (num > radius)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06006284 RID: 25220 RVA: 0x0029617B File Offset: 0x0029457B
		// (set) Token: 0x06006285 RID: 25221 RVA: 0x00296183 File Offset: 0x00294583
		public bool IsImpossible { get; private set; }

		// Token: 0x06006286 RID: 25222 RVA: 0x0029618C File Offset: 0x0029458C
		public void SetForceImpossible(bool value, Actor actor)
		{
			this.IsImpossible = value;
			this.CommandPartner = actor;
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x0029619C File Offset: 0x0029459C
		public bool SetImpossible(bool value, Actor actor)
		{
			if (this.IsImpossible == value)
			{
				return false;
			}
			if (value && this.CommandPartner != null)
			{
				return false;
			}
			this.IsImpossible = value;
			if (value)
			{
				this._elapsedTimeFromLastImpossible = 0f;
				this.CommandPartner = actor;
			}
			else
			{
				this.CommandPartner = null;
			}
			return true;
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06006288 RID: 25224 RVA: 0x002961FC File Offset: 0x002945FC
		public override bool IsNeutralCommand
		{
			get
			{
				if (this.TutorialMode)
				{
					return this.TutorialCanTalk;
				}
				bool flag = this.Mode == Desire.ActionType.SearchActor || this.Mode == Desire.ActionType.WithPlayer || this.Mode == Desire.ActionType.SearchPlayerToTalk || this.Mode == Desire.ActionType.EndTaskTalkToPlayer;
				return !flag && (this.IsAdvEvent || this.CanTalk);
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x00296269 File Offset: 0x00294669
		public bool IsEncounterable
		{
			get
			{
				return this.StateType == State.Type.Normal || this.StateType == State.Type.Greet;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x0600628A RID: 25226 RVA: 0x00296283 File Offset: 0x00294683
		// (set) Token: 0x0600628B RID: 25227 RVA: 0x0029628C File Offset: 0x0029468C
		public State.Type StateType
		{
			get
			{
				return this._stateType;
			}
			set
			{
				if (this._stateType == value)
				{
					return;
				}
				this._stateType = value;
				if (!Singleton<Manager.Map>.IsInstance())
				{
					return;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				CommandArea commandArea;
				if (player == null)
				{
					commandArea = null;
				}
				else
				{
					PlayerController playerController = player.PlayerController;
					commandArea = ((playerController != null) ? playerController.CommandArea : null);
				}
				CommandArea commandArea2 = commandArea;
				if (commandArea2 != null && commandArea2.ContainsConsiderationObject(this))
				{
					commandArea2.RefreshCommands();
				}
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x0600628C RID: 25228 RVA: 0x002962FF File Offset: 0x002946FF
		// (set) Token: 0x0600628D RID: 25229 RVA: 0x00296307 File Offset: 0x00294707
		public Transform TargetCommun { get; set; }

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x0600628E RID: 25230 RVA: 0x00296310 File Offset: 0x00294710
		// (set) Token: 0x0600628F RID: 25231 RVA: 0x00296318 File Offset: 0x00294718
		public int SelectedActionID { get; set; }

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06006290 RID: 25232 RVA: 0x00296321 File Offset: 0x00294721
		// (set) Token: 0x06006291 RID: 25233 RVA: 0x00296329 File Offset: 0x00294729
		public bool IsEvent { get; set; }

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06006292 RID: 25234 RVA: 0x00296332 File Offset: 0x00294732
		// (set) Token: 0x06006293 RID: 25235 RVA: 0x0029633A File Offset: 0x0029473A
		public bool IsStandby
		{
			get
			{
				return this._isStandby;
			}
			set
			{
				if (this._isStandby != value)
				{
					this._isStandby = value;
					Singleton<Manager.Map>.Instance.Player.PlayerController.CommandArea.RefreshCommands();
				}
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06006294 RID: 25236 RVA: 0x00296368 File Offset: 0x00294768
		public bool IsCloseToPlayer
		{
			get
			{
				AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
				return this._distanceTweenPlayer <= rangeSetting.arrivedDistance && this._heightDistTweenPlayer <= rangeSetting.acceptableHeight;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06006295 RID: 25237 RVA: 0x002963AC File Offset: 0x002947AC
		public bool IsFarPlayer
		{
			get
			{
				AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
				return this._distanceTweenPlayer > rangeSetting.leaveDistance || this._heightDistTweenPlayer > rangeSetting.acceptableHeight;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06006296 RID: 25238 RVA: 0x002963F0 File Offset: 0x002947F0
		public bool IsFarPlayerInSurprise
		{
			get
			{
				AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
				return this._distanceTweenPlayer > rangeSetting.leaveDistanceInSurprise || this._heightDistTweenPlayer > rangeSetting.acceptableHeight;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x00296434 File Offset: 0x00294834
		public bool IsCloseToPlayerByPhotoMode
		{
			get
			{
				AgentProfile.PhotoShotRangeParameter photoShotRangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.PhotoShotRangeSetting;
				return this._distanceTweenPlayer <= photoShotRangeSetting.arriveDistance && this._heightDistTweenPlayer <= photoShotRangeSetting.acceptableHeight && this._angleDiffTweenPlayer * 2f <= photoShotRangeSetting.sightAngle;
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06006298 RID: 25240 RVA: 0x00296490 File Offset: 0x00294890
		public bool IsFarPlayerByPhotoMode
		{
			get
			{
				AgentProfile.PhotoShotRangeParameter photoShotRangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.PhotoShotRangeSetting;
				return photoShotRangeSetting.leaveDistance < this._distanceTweenPlayer || photoShotRangeSetting.acceptableHeight < this._heightDistTweenPlayer || photoShotRangeSetting.invisibleAngle < this._angleDiffTweenPlayer * 2f;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06006299 RID: 25241 RVA: 0x002964E9 File Offset: 0x002948E9
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return this._layer;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x0600629A RID: 25242 RVA: 0x002964F1 File Offset: 0x002948F1
		// (set) Token: 0x0600629B RID: 25243 RVA: 0x002964F9 File Offset: 0x002948F9
		public AgentBehaviorTree NormalBehaviorTree { get; private set; }

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x0600629C RID: 25244 RVA: 0x00296502 File Offset: 0x00294902
		// (set) Token: 0x0600629D RID: 25245 RVA: 0x0029650A File Offset: 0x0029490A
		public override Desire.ActionType Mode
		{
			get
			{
				return this._modeType;
			}
			set
			{
				if (this._modeType == value)
				{
					return;
				}
				this._modeType = value;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x0600629E RID: 25246 RVA: 0x00296520 File Offset: 0x00294920
		// (set) Token: 0x0600629F RID: 25247 RVA: 0x00296528 File Offset: 0x00294928
		public Desire.ActionType PrevMode { get; set; }

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x060062A0 RID: 25248 RVA: 0x00296531 File Offset: 0x00294931
		// (set) Token: 0x060062A1 RID: 25249 RVA: 0x00296539 File Offset: 0x00294939
		public Desire.ActionType PrevActionMode { get; set; }

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x060062A2 RID: 25250 RVA: 0x00296542 File Offset: 0x00294942
		// (set) Token: 0x060062A3 RID: 25251 RVA: 0x0029654A File Offset: 0x0029494A
		public Desire.ActionType ReservedMode { get; set; }

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x060062A4 RID: 25252 RVA: 0x00296553 File Offset: 0x00294953
		// (set) Token: 0x060062A5 RID: 25253 RVA: 0x0029655B File Offset: 0x0029495B
		public AIProject.Definitions.Tutorial.ActionType TutorialType { get; set; }

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x060062A6 RID: 25254 RVA: 0x00296564 File Offset: 0x00294964
		// (set) Token: 0x060062A7 RID: 25255 RVA: 0x0029656C File Offset: 0x0029496C
		public bool TutorialMode { get; set; }

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x060062A8 RID: 25256 RVA: 0x00296575 File Offset: 0x00294975
		// (set) Token: 0x060062A9 RID: 25257 RVA: 0x0029657D File Offset: 0x0029497D
		public bool TutorialCanTalk { get; set; }

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x060062AA RID: 25258 RVA: 0x00296586 File Offset: 0x00294986
		// (set) Token: 0x060062AB RID: 25259 RVA: 0x0029658E File Offset: 0x0029498E
		public int TutorialLocomoCaseID { get; set; }

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x060062AC RID: 25260 RVA: 0x00296597 File Offset: 0x00294997
		// (set) Token: 0x060062AD RID: 25261 RVA: 0x0029659F File Offset: 0x0029499F
		public Actor CommandPartner { get; set; }

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x060062AE RID: 25262 RVA: 0x002965A8 File Offset: 0x002949A8
		// (set) Token: 0x060062AF RID: 25263 RVA: 0x002965B0 File Offset: 0x002949B0
		public BehaviorTreeResources BehaviorResources { get; private set; }

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x060062B0 RID: 25264 RVA: 0x002965B9 File Offset: 0x002949B9
		// (set) Token: 0x060062B1 RID: 25265 RVA: 0x002965C1 File Offset: 0x002949C1
		private protected TutorialBehaviorTreeResources TutorialBehaviorResources { protected get; private set; }

		// Token: 0x060062B2 RID: 25266 RVA: 0x002965CC File Offset: 0x002949CC
		public void EnableBehavior()
		{
			if (this.BehaviorResources != null && !this.BehaviorResources.enabled)
			{
				this.BehaviorResources.enabled = true;
			}
			if (this.TutorialBehaviorResources != null && !this.TutorialBehaviorResources.enabled)
			{
				this.TutorialBehaviorResources.enabled = true;
			}
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x00296634 File Offset: 0x00294A34
		public void DisableBehavior()
		{
			if (this.BehaviorResources != null && this.BehaviorResources.enabled)
			{
				this.BehaviorResources.enabled = false;
			}
			if (this.TutorialBehaviorResources != null && this.TutorialBehaviorResources.enabled)
			{
				this.TutorialBehaviorResources.enabled = false;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x060062B4 RID: 25268 RVA: 0x0029669B File Offset: 0x00294A9B
		public CommandType CommandType { get; }

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x060062B5 RID: 25269 RVA: 0x002966A3 File Offset: 0x00294AA3
		public override ICharacterInfo TiedInfo
		{
			[CompilerGenerated]
			get
			{
				return this.AgentData;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x060062B6 RID: 25270 RVA: 0x002966AB File Offset: 0x00294AAB
		// (set) Token: 0x060062B7 RID: 25271 RVA: 0x002966B3 File Offset: 0x00294AB3
		public AgentData AgentData { get; set; }

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x060062B8 RID: 25272 RVA: 0x002966BC File Offset: 0x00294ABC
		// (set) Token: 0x060062B9 RID: 25273 RVA: 0x002966C4 File Offset: 0x00294AC4
		public Desire.Type ScheduledDesire { get; private set; }

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x002966CD File Offset: 0x00294ACD
		// (set) Token: 0x060062BB RID: 25275 RVA: 0x002966D5 File Offset: 0x00294AD5
		public Desire.Type RuntimeDesire { get; set; }

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x060062BC RID: 25276 RVA: 0x002966DE File Offset: 0x00294ADE
		public Desire.Type RequestedDesire
		{
			get
			{
				if (!Desire.ModeTable.TryGetValue(this._modeType, out this._requestedDesire))
				{
					this._requestedDesire = Desire.Type.None;
				}
				return this._requestedDesire;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x060062BD RID: 25277 RVA: 0x00296708 File Offset: 0x00294B08
		// (set) Token: 0x060062BE RID: 25278 RVA: 0x00296710 File Offset: 0x00294B10
		public List<Actor> TargetActors { get; private set; } = new List<Actor>();

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x060062BF RID: 25279 RVA: 0x00296719 File Offset: 0x00294B19
		public ReadOnlyCollection<ActionPoint> ActionPoints
		{
			get
			{
				if (this._readonlyActionPoints == null)
				{
					this._readonlyActionPoints = new ReadOnlyCollection<ActionPoint>(this._actionPoints);
				}
				return this._readonlyActionPoints;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x060062C0 RID: 25280 RVA: 0x0029673D File Offset: 0x00294B3D
		// (set) Token: 0x060062C1 RID: 25281 RVA: 0x00296745 File Offset: 0x00294B45
		public ActionPoint[] SearchTargets { get; private set; } = Array.Empty<ActionPoint>();

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x060062C2 RID: 25282 RVA: 0x0029674E File Offset: 0x00294B4E
		// (set) Token: 0x060062C3 RID: 25283 RVA: 0x00296756 File Offset: 0x00294B56
		public List<AnimalBase> TargetAnimals { get; private set; } = new List<AnimalBase>();

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x060062C4 RID: 25284 RVA: 0x0029675F File Offset: 0x00294B5F
		// (set) Token: 0x060062C5 RID: 25285 RVA: 0x00296767 File Offset: 0x00294B67
		public IVisible TargetVisual { get; set; }

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x060062C6 RID: 25286 RVA: 0x00296770 File Offset: 0x00294B70
		// (set) Token: 0x060062C7 RID: 25287 RVA: 0x00296778 File Offset: 0x00294B78
		public ActionPoint TargetInSightActionPoint
		{
			get
			{
				return this._targetInSightActionPoint;
			}
			set
			{
				this._targetInSightActionPoint = value;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x060062C8 RID: 25288 RVA: 0x00296781 File Offset: 0x00294B81
		// (set) Token: 0x060062C9 RID: 25289 RVA: 0x00296789 File Offset: 0x00294B89
		public Vector3? DestPosition { get; set; }

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x060062CA RID: 25290 RVA: 0x00296792 File Offset: 0x00294B92
		// (set) Token: 0x060062CB RID: 25291 RVA: 0x0029679A File Offset: 0x00294B9A
		public ActionPoint BookingActionPoint { get; set; }

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x060062CC RID: 25292 RVA: 0x002967A3 File Offset: 0x00294BA3
		// (set) Token: 0x060062CD RID: 25293 RVA: 0x002967AB File Offset: 0x00294BAB
		public StoryPoint TargetStoryPoint { get; set; }

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060062CE RID: 25294 RVA: 0x002967B4 File Offset: 0x00294BB4
		// (set) Token: 0x060062CF RID: 25295 RVA: 0x002967BC File Offset: 0x00294BBC
		public OffMeshLink TargetOffMeshLink { get; set; }

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x002967C8 File Offset: 0x00294BC8
		public OffMeshLink NearOffMeshLink
		{
			get
			{
				OffMeshLink offMeshLink = null;
				if (this._navMeshAgent.currentOffMeshLinkData.activated)
				{
					offMeshLink = this._navMeshAgent.currentOffMeshLinkData.offMeshLink;
				}
				if (offMeshLink == null && this._navMeshAgent.nextOffMeshLinkData.activated)
				{
					offMeshLink = this._navMeshAgent.nextOffMeshLinkData.offMeshLink;
				}
				return offMeshLink;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060062D1 RID: 25297 RVA: 0x0029683D File Offset: 0x00294C3D
		// (set) Token: 0x060062D2 RID: 25298 RVA: 0x00296845 File Offset: 0x00294C45
		public ActionPoint PrevActionPoint { get; set; }

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060062D3 RID: 25299 RVA: 0x0029684E File Offset: 0x00294C4E
		// (set) Token: 0x060062D4 RID: 25300 RVA: 0x00296856 File Offset: 0x00294C56
		public bool UpdateMotivation { get; set; }

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x0029685F File Offset: 0x00294C5F
		// (set) Token: 0x060062D6 RID: 25302 RVA: 0x00296867 File Offset: 0x00294C67
		public float MotivationInEncounter { get; set; }

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060062D7 RID: 25303 RVA: 0x00296870 File Offset: 0x00294C70
		// (set) Token: 0x060062D8 RID: 25304 RVA: 0x00296878 File Offset: 0x00294C78
		public float RuntimeMotivationInPhoto { get; set; }

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060062D9 RID: 25305 RVA: 0x00296881 File Offset: 0x00294C81
		// (set) Token: 0x060062DA RID: 25306 RVA: 0x00296889 File Offset: 0x00294C89
		public Actor TargetInSightActor
		{
			get
			{
				return this._targetInSightActor;
			}
			set
			{
				this._targetInSightActor = value;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x060062DB RID: 25307 RVA: 0x00296892 File Offset: 0x00294C92
		// (set) Token: 0x060062DC RID: 25308 RVA: 0x0029689A File Offset: 0x00294C9A
		public PlayerActor Pair { get; set; }

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060062DD RID: 25309 RVA: 0x002968A3 File Offset: 0x00294CA3
		// (set) Token: 0x060062DE RID: 25310 RVA: 0x002968AB File Offset: 0x00294CAB
		public AnimalBase TargetInSightAnimal
		{
			get
			{
				return this._targetInSightAnimal;
			}
			set
			{
				this._targetInSightAnimal = value;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x002968B4 File Offset: 0x00294CB4
		// (set) Token: 0x060062E0 RID: 25312 RVA: 0x002968BC File Offset: 0x00294CBC
		public float AnimalFovAngleOffsetY { get; set; }

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060062E1 RID: 25313 RVA: 0x002968C5 File Offset: 0x00294CC5
		// (set) Token: 0x060062E2 RID: 25314 RVA: 0x002968CD File Offset: 0x00294CCD
		public Dictionary<int, CollisionState> ActionPointCollisionStateTable { get; private set; } = new Dictionary<int, CollisionState>();

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060062E3 RID: 25315 RVA: 0x002968D6 File Offset: 0x00294CD6
		// (set) Token: 0x060062E4 RID: 25316 RVA: 0x002968DE File Offset: 0x00294CDE
		public Dictionary<int, CollisionState> ActorCollisionStateTable { get; private set; } = new Dictionary<int, CollisionState>();

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060062E5 RID: 25317 RVA: 0x002968E7 File Offset: 0x00294CE7
		// (set) Token: 0x060062E6 RID: 25318 RVA: 0x002968EF File Offset: 0x00294CEF
		public Dictionary<int, CollisionState> ActorFarCollisionStateTable { get; private set; } = new Dictionary<int, CollisionState>();

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x060062E7 RID: 25319 RVA: 0x002968F8 File Offset: 0x00294CF8
		// (set) Token: 0x060062E8 RID: 25320 RVA: 0x00296900 File Offset: 0x00294D00
		public Dictionary<int, CollisionState> AnimalCollisionStateTable { get; private set; } = new Dictionary<int, CollisionState>();

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060062E9 RID: 25321 RVA: 0x00296909 File Offset: 0x00294D09
		// (set) Token: 0x060062EA RID: 25322 RVA: 0x00296911 File Offset: 0x00294D11
		public PoseKeyPair? SurprisePoseID { get; set; }

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060062EB RID: 25323 RVA: 0x0029691A File Offset: 0x00294D1A
		// (set) Token: 0x060062EC RID: 25324 RVA: 0x00296922 File Offset: 0x00294D22
		public bool IsMasturbating { get; set; }

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060062ED RID: 25325 RVA: 0x0029692B File Offset: 0x00294D2B
		// (set) Token: 0x060062EE RID: 25326 RVA: 0x00296933 File Offset: 0x00294D33
		public bool SleepTrigger { get; set; }

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060062EF RID: 25327 RVA: 0x0029693C File Offset: 0x00294D3C
		// (set) Token: 0x060062F0 RID: 25328 RVA: 0x00296944 File Offset: 0x00294D44
		public bool SuccessCook { get; set; }

		// Token: 0x060062F1 RID: 25329 RVA: 0x0029694D File Offset: 0x00294D4D
		private void Awake()
		{
			this.BehaviorResources = base.GetComponentInChildren<BehaviorTreeResources>();
			this.NormalBehaviorTree = this.BehaviorResources.GetBehaviorTree(Desire.ActionType.Normal);
			this.BehaviorResources.Initialize();
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x00296978 File Offset: 0x00294D78
		protected override void OnStart()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Do(delegate(long _)
			{
				this.OnUpdate();
			}, delegate(Exception ex)
			{
			}).OnErrorRetry(delegate(Exception ex)
			{
			}).Subscribe(delegate(long _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x00296A38 File Offset: 0x00294E38
		public override IEnumerator LoadAsync()
		{
			if (this.AgentData != null)
			{
				yield return this.LoadCharAsync(this.AgentData.CharaFileName);
			}
			else
			{
				yield return this.LoadCharAsync(string.Empty);
			}
			this.LoadEquipments();
			if (this._chaBodyParts == null)
			{
				this._chaBodyParts = new Dictionary<Actor.BodyPart, Transform>();
			}
			Dictionary<Actor.BodyPart, Transform> chaBodyParts = this._chaBodyParts;
			Actor.BodyPart key = Actor.BodyPart.Body;
			GameObject gameObject = base.ChaControl.animBody.transform.FindLoop("cf_J_Hips");
			chaBodyParts[key] = ((gameObject != null) ? gameObject.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts2 = this._chaBodyParts;
			Actor.BodyPart key2 = Actor.BodyPart.Bust;
			GameObject gameObject2 = base.ChaControl.animBody.transform.FindLoop("cf_J_Mune00");
			chaBodyParts2[key2] = ((gameObject2 != null) ? gameObject2.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts3 = this._chaBodyParts;
			Actor.BodyPart key3 = Actor.BodyPart.Head;
			GameObject gameObject3 = base.ChaControl.animBody.transform.FindLoop("N_Head");
			chaBodyParts3[key3] = ((gameObject3 != null) ? gameObject3.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts4 = this._chaBodyParts;
			Actor.BodyPart key4 = Actor.BodyPart.LeftFoot;
			GameObject gameObject4 = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_L");
			chaBodyParts4[key4] = ((gameObject4 != null) ? gameObject4.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts5 = this._chaBodyParts;
			Actor.BodyPart key5 = Actor.BodyPart.RightFoot;
			GameObject gameObject5 = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_R");
			chaBodyParts5[key5] = ((gameObject5 != null) ? gameObject5.transform : null);
			Animator animator = base.ChaControl.animBody;
			this.packData = new AgentActor.PackData();
			CharaPackData packData = this.packData;
			ADV.ICommandData[] array = new ADV.ICommandData[1];
			int num = 0;
			Game instance = Singleton<Game>.Instance;
			ADV.ICommandData commandData;
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
			array2[0] = this.AgentData;
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
			this.InitCommands();
			FullBodyBipedIK ik = animator.GetComponentInChildren<FullBodyBipedIK>(true);
			GameObject ctrlOld = this._animation.gameObject;
			ActorAnimationAgent actorAnimationAgent = this._animation.CloneComponent(animator.gameObject);
			actorAnimationAgent.IK = ik;
			actorAnimationAgent.Actor = this;
			actorAnimationAgent.Character = this._character;
			actorAnimationAgent.Animator = animator;
			this._animation = actorAnimationAgent;
			UnityEngine.Object.Destroy(ctrlOld);
			AssetBundleInfo animABInfo = default(AssetBundleInfo);
			RuntimeAnimatorController rac = Singleton<Manager.Resources>.Instance.Animation.GetCharaAnimator(0, ref animABInfo);
			this.Animation.SetDefaultAnimatorController(rac);
			this.Animation.SetAnimatorController(rac);
			this.Animation.AnimABInfo = animABInfo;
			animator.Play("Locomotion", 0, 0f);
			this._character.CharacterAnimation = this._animation;
			this.InitializeIK();
			this._controller.StartBehavior();
			yield break;
		}

		// Token: 0x060062F4 RID: 25332 RVA: 0x00296A54 File Offset: 0x00294E54
		public void Load()
		{
			AgentData agentData = this.AgentData;
			if (agentData != null)
			{
				this.LoadChar(agentData.CharaFileName);
			}
			else
			{
				this.LoadChar(string.Empty);
			}
			if (!base.ChaControl.fileGameInfo.gameRegistration)
			{
				base.ChaControl.chaFile.InitGameInfoParam();
				base.ChaControl.fileGameInfo.gameRegistration = true;
				if (!agentData.CharaFileName.IsNullOrEmpty())
				{
					base.ChaControl.chaFile.SaveCharaFile(base.ChaControl.chaFile.charaFileName, byte.MaxValue, false);
				}
			}
			this.LoadEquipments();
			if (this._chaBodyParts == null)
			{
				this._chaBodyParts = new Dictionary<Actor.BodyPart, Transform>();
			}
			Dictionary<Actor.BodyPart, Transform> chaBodyParts = this._chaBodyParts;
			Actor.BodyPart key = Actor.BodyPart.Body;
			GameObject gameObject = base.ChaControl.animBody.transform.FindLoop("cf_J_Hips");
			chaBodyParts[key] = ((gameObject != null) ? gameObject.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts2 = this._chaBodyParts;
			Actor.BodyPart key2 = Actor.BodyPart.Bust;
			GameObject gameObject2 = base.ChaControl.animBody.transform.FindLoop("cf_J_Mune00");
			chaBodyParts2[key2] = ((gameObject2 != null) ? gameObject2.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts3 = this._chaBodyParts;
			Actor.BodyPart key3 = Actor.BodyPart.Head;
			GameObject gameObject3 = base.ChaControl.animBody.transform.FindLoop("N_Head");
			chaBodyParts3[key3] = ((gameObject3 != null) ? gameObject3.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts4 = this._chaBodyParts;
			Actor.BodyPart key4 = Actor.BodyPart.LeftFoot;
			GameObject gameObject4 = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_L");
			chaBodyParts4[key4] = ((gameObject4 != null) ? gameObject4.transform : null);
			Dictionary<Actor.BodyPart, Transform> chaBodyParts5 = this._chaBodyParts;
			Actor.BodyPart key5 = Actor.BodyPart.RightFoot;
			GameObject gameObject5 = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_R");
			chaBodyParts5[key5] = ((gameObject5 != null) ? gameObject5.transform : null);
			Animator animBody = base.ChaControl.animBody;
			this.packData = new AgentActor.PackData();
			CharaPackData packData = this.packData;
			ADV.ICommandData[] array = new ADV.ICommandData[1];
			int num = 0;
			Game instance = Singleton<Game>.Instance;
			ADV.ICommandData commandData;
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
			array2[0] = agentData;
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
			this.InitCommands();
			FullBodyBipedIK componentInChildren = animBody.GetComponentInChildren<FullBodyBipedIK>(true);
			GameObject gameObject6 = this._animation.gameObject;
			ActorAnimationAgent actorAnimationAgent = this._animation.CloneComponent(animBody.gameObject);
			actorAnimationAgent.IK = componentInChildren;
			actorAnimationAgent.Actor = this;
			actorAnimationAgent.Character = this._character;
			actorAnimationAgent.Animator = animBody;
			this._animation = actorAnimationAgent;
			UnityEngine.Object.Destroy(gameObject6);
			AssetBundleInfo animABInfo = default(AssetBundleInfo);
			RuntimeAnimatorController charaAnimator = Singleton<Manager.Resources>.Instance.Animation.GetCharaAnimator(0, ref animABInfo);
			this.Animation.SetDefaultAnimatorController(charaAnimator);
			this.Animation.SetAnimatorController(charaAnimator);
			this.Animation.AnimABInfo = animABInfo;
			animBody.Play("Locomotion", 0, 0f);
			this._character.CharacterAnimation = this._animation;
			this.InitializeIK();
			this._controller.StartBehavior();
		}

		// Token: 0x060062F5 RID: 25333 RVA: 0x00296D80 File Offset: 0x00295180
		public override void LoadEquipments()
		{
			base.LoadEquipmentItem(this.AgentData.EquipedHeadItem, ChaControlDefine.ExtraAccessoryParts.Head);
			base.LoadEquipmentItem(this.AgentData.EquipedBackItem, ChaControlDefine.ExtraAccessoryParts.Back);
			base.LoadEquipmentItem(this.AgentData.EquipedNeckItem, ChaControlDefine.ExtraAccessoryParts.Neck);
			base.LoadEquipmentItem(this.AgentData.EquipedLampItem, ChaControlDefine.ExtraAccessoryParts.Waist);
		}

		// Token: 0x060062F6 RID: 25334 RVA: 0x00296DD8 File Offset: 0x002951D8
		public override void EnableEntity()
		{
			if (this._modeCache.Item3)
			{
				base.ActivateNavMeshAgent();
				base.NavMeshAgent.Warp(base.Position);
			}
			else if (base.EventKey == EventType.Move)
			{
				base.ActivateNavMeshAgent();
				base.SetDefaultStateHousingItem();
				if (base.CurrentPoint != null)
				{
					OffMeshLink component = base.CurrentPoint.GetComponent<OffMeshLink>();
					if (component != null)
					{
						Transform endTransform = component.endTransform;
						base.NavMeshAgent.Warp(endTransform.position);
						base.Rotation = endTransform.rotation;
					}
					base.CurrentPoint.RemoveBookingUser(this);
					base.CurrentPoint.SetActiveMapItemObjs(true);
					base.CurrentPoint.ReleaseSlot(this);
					base.CurrentPoint = null;
				}
				base.EventKey = (EventType)0;
				this.TargetInSightActionPoint = null;
				this.Animation.ResetDefaultAnimatorController();
				this._modeCache.Item1 = Desire.ActionType.Normal;
				this._modeCache.Item2 = Desire.ActionType.Normal;
			}
			else if (base.EventKey == EventType.DoorOpen)
			{
				base.ActivateNavMeshAgent();
				base.SetDefaultStateHousingItem();
				if (base.CurrentPoint != null)
				{
					DoorPoint doorPoint = base.CurrentPoint as DoorPoint;
					if (doorPoint != null)
					{
						if (doorPoint.OpenState == DoorPoint.OpenPattern.Close)
						{
							if (doorPoint.OpenType == DoorPoint.OpenTypeState.Right || doorPoint.OpenType == DoorPoint.OpenTypeState.Right90)
							{
								doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenRight, true);
							}
							else
							{
								doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenLeft, true);
							}
						}
						doorPoint.RemoveBookingUser(this);
					}
					base.CurrentPoint.SetActiveMapItemObjs(true);
					base.CurrentPoint.ReleaseSlot(this);
					base.CurrentPoint = null;
				}
				base.EventKey = (EventType)0;
				this.TargetInSightActionPoint = null;
				this.Animation.ResetDefaultAnimatorController();
				this._modeCache.Item1 = Desire.ActionType.Normal;
				this._modeCache.Item2 = Desire.ActionType.Normal;
			}
			else if (base.EventKey == EventType.Toilet || base.EventKey == EventType.DressIn || base.EventKey == EventType.Bath || base.EventKey == EventType.DressOut || this.AgentData.PlayedDressIn || this._modeCache.Item1 == Desire.ActionType.FoundPeeping)
			{
				base.ActivateNavMeshAgent();
				base.SetDefaultStateHousingItem();
				if (base.CurrentPoint != null)
				{
					base.CurrentPoint.SetActiveMapItemObjs(true);
					base.CurrentPoint.ReleaseSlot(this);
					base.CurrentPoint = null;
				}
				base.ChaControl.ChangeNowCoordinate(true, true);
				this.AgentData.BathCoordinateFileName = null;
				base.ChaControl.SetClothesState(0, 0, true);
				base.ChaControl.SetClothesState(1, 0, true);
				base.ChaControl.SetClothesState(2, 0, true);
				base.ChaControl.SetClothesState(3, 0, true);
				base.ChaControl.SetClothesState(5, 0, true);
				this.AgentData.PlayedDressIn = false;
				base.EventKey = (EventType)0;
				this.TargetInSightActionPoint = null;
				this.Animation.ResetDefaultAnimatorController();
				this._modeCache.Item1 = Desire.ActionType.Normal;
				this._modeCache.Item2 = Desire.ActionType.Normal;
			}
			else if (this._modeCache.Item1 == Desire.ActionType.Encounter)
			{
				base.ActivateNavMeshAgent();
				base.EventKey = (EventType)0;
				this.TargetInSightActionPoint = null;
				this.Animation.ResetDefaultAnimatorController();
				this._modeCache.Item1 = Desire.ActionType.Normal;
				this._modeCache.Item2 = Desire.ActionType.Normal;
			}
			this.Controller.enabled = true;
			base.ChaControl.visibleAll = true;
			this.AnimationAgent.enabled = true;
			base.SetActiveOnEquipedItem(false);
			this.EnableBehavior();
			if (this._modeCache.Item1 == Desire.ActionType.Normal && this._modeCache.Item2 == Desire.ActionType.Normal)
			{
				this.ResetActionFlag();
				if (this._schedule.enabled)
				{
					this._schedule.enabled = false;
				}
			}
			this.Mode = this._modeCache.Item1;
			this.BehaviorResources.ChangeMode(this._modeCache.Item2);
			this.AnimationAgent.EnableItems();
			this.AnimationAgent.EnableParticleRenderer();
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x002971EC File Offset: 0x002955EC
		public override void DisableEntity()
		{
			Desire.ActionType mode = this.BehaviorResources.Mode;
			bool enabled = base.NavMeshAgent.enabled;
			this._modeCache = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType, bool>(this.Mode, mode, enabled);
			base.SetActiveOnEquipedItem(false);
			this.Controller.enabled = false;
			if (enabled)
			{
				base.NavMeshAgent.enabled = false;
			}
			this.AnimationAgent.enabled = false;
			base.ChaControl.visibleAll = false;
			if (mode == Desire.ActionType.EndTaskMasturbation || mode == Desire.ActionType.EndTaskLesbianH || mode == Desire.ActionType.EndTaskLesbianMerchantH)
			{
				this.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			}
			this.DisableBehavior();
			this.AnimationAgent.DisableItems();
			this.AnimationAgent.DisableParticleRenderer();
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x002972AC File Offset: 0x002956AC
		private void OnUpdate()
		{
			this._navMeshObstacle.transform.position = base.Position;
			if (!this._scaleCtrlInfos.IsNullOrEmpty<Actor.ItemScaleInfo>())
			{
				foreach (Actor.ItemScaleInfo itemScaleInfo in this._scaleCtrlInfos)
				{
					int scaleMode = itemScaleInfo.ScaleMode;
					if (scaleMode == 0)
					{
						float shapeBodyValue = base.ChaControl.GetShapeBodyValue(0);
						float num = itemScaleInfo.Evaluate(shapeBodyValue);
						itemScaleInfo.TargetItem.transform.localScale = new Vector3(num, num, num);
					}
				}
			}
			AgentData agentData = this.AgentData;
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			EnvironmentSimulator simulator = Singleton<Manager.Map>.Instance.Simulator;
			if (simulator.EnabledTimeProgression)
			{
				Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
				if (base.AreaType == MapArea.AreaType.Indoor)
				{
					agentData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
				}
				else
				{
					if (weather != Weather.Rain)
					{
						if (weather != Weather.Storm)
						{
							agentData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
						}
						else
						{
							agentData.Wetness += statusProfile.WetRateInStorm * Time.deltaTime;
						}
					}
					else
					{
						agentData.Wetness += statusProfile.WetRateInRain * Time.deltaTime;
					}
					agentData.Wetness = Mathf.Clamp(agentData.Wetness, 0f, 100f);
				}
			}
			if (agentData.Wetness >= 100f)
			{
				agentData.IsWet = true;
			}
			if (agentData.Wetness <= 0f)
			{
				agentData.IsWet = false;
			}
			if (base.ChaControl != null)
			{
				float wetRate = Mathf.InverseLerp(0f, 100f, agentData.Wetness);
				base.ChaControl.wetRate = wetRate;
			}
			if (this.BehaviorResources.enabled && this._schedule.enabled && !this._schedule.useGameTime && this._schedule.progress)
			{
				this._schedule.elapsedTime = this._schedule.elapsedTime + Time.deltaTime;
				if (this._schedule.elapsedTime > this._schedule.duration)
				{
					this._schedule.enabled = false;
				}
			}
			Vector3 position = base.Position;
			Vector3 position2 = Singleton<Manager.Map>.Instance.Player.Position;
			Vector3 forward = base.Forward;
			position.y = (position2.y = (forward.y = 0f));
			this._distanceTweenPlayer = Vector3.Distance(position, position2);
			this._heightDistTweenPlayer = Mathf.Abs(Singleton<Manager.Map>.Instance.Player.Position.y - base.Position.y);
			forward.Normalize();
			AgentProfile.PhotoShotRangeParameter photoShotRangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.PhotoShotRangeSetting;
			Vector3 b = position + forward * photoShotRangeSetting.sightOffsetZ;
			this._angleDiffTweenPlayer = Vector3.Angle(forward, position2 - b);
			this.UpdateActionPointCollision();
			this.UpdateActorSightCollision();
			this.UpdateActorFarSightCollision();
			this.UpdateAnimalSightCollision();
			this._elapsedTimeFromLastImpossible += Time.deltaTime;
			if (base.NavMeshAgent.isActiveAndEnabled && base.NavMeshAgent.isOnNavMesh)
			{
				this.AgentData.Position = base.Position;
				this.AgentData.Rotation = base.Rotation;
			}
			this.AgentData.ChunkID = base.ChunkID;
			this.AgentData.ModeType = this.Mode;
			this.AgentData.PrevMode = this.PrevMode;
			this.AgentData.TutorialModeType = this.TutorialType;
			if (!this.TutorialMode)
			{
				this.UpdateEncounter();
			}
			if (Game.isAdd50 && (this.Mode == Desire.ActionType.Normal || this.Mode == Desire.ActionType.SearchActor || this.Mode == Desire.ActionType.SearchGimme || this.Mode == Desire.ActionType.EndTaskGimme || this.Mode == Desire.ActionType.SearchH || this.Mode == Desire.ActionType.EndTaskH || this.Mode == Desire.ActionType.SearchH || this.Mode == Desire.ActionType.ReverseRape || this.Mode == Desire.ActionType.EndTaskTalkToPlayer || this.Mode == Desire.ActionType.SearchGift || this.Mode == Desire.ActionType.GiftForceEncounter || this.Mode == Desire.ActionType.SearchBirthdayGift || this.Mode == Desire.ActionType.BirthdayGift || this.Mode == Desire.ActionType.InviteSleep || this.Mode == Desire.ActionType.InviteSleepH || this.Mode == Desire.ActionType.InviteEat || this.Mode == Desire.ActionType.InviteBreak) && this.CheckYandereWarpPossibility())
			{
				this.ChangeBehavior(Desire.ActionType.YandereWarp);
			}
			if (this._mapAreaID != null && base.MapArea != null)
			{
				this._mapAreaID.Value = base.MapArea.AreaID;
				this.AgentData.AreaID = base.MapArea.AreaID;
			}
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x00297820 File Offset: 0x00295C20
		private void UpdateEncounter()
		{
			IState state = Singleton<Manager.Map>.Instance.Player.PlayerController.State;
			int num = base.ChaControl.fileGameInfo.flavorState[2];
			int surpriseBorder = Singleton<Manager.Resources>.Instance.StatusProfile.SurpriseBorder;
			bool flag = num >= surpriseBorder;
			if (state is Normal)
			{
				if (this._modeType == Desire.ActionType.EndTaskMasturbation || this._modeType == Desire.ActionType.EndTaskToilet || this._modeType == Desire.ActionType.EndTaskBath)
				{
					if (flag && this.ExistsInvader() && base.CurrentPoint != null && this.SurprisePoseID != null && this.CanTalk)
					{
						Desire.ActionType modeType = this._modeType;
						if (modeType != Desire.ActionType.EndTaskMasturbation)
						{
							if (modeType != Desire.ActionType.EndTaskToilet)
							{
								if (modeType == Desire.ActionType.EndTaskBath)
								{
									this.ReservedMode = Desire.ActionType.GotoDressOut;
								}
							}
							else
							{
								this.ReservedMode = Desire.ActionType.Normal;
							}
						}
						else
						{
							this.ReservedMode = Desire.ActionType.Normal;
						}
						this.ChangeBehavior(Desire.ActionType.FoundPeeping);
					}
				}
				else
				{
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					if (this.Mode != Desire.ActionType.Encounter)
					{
						if (this.UseNeckLook)
						{
							if (!this.IsCloseToPlayer || !this._prevCloseToPlayer)
							{
								if (this.ReleasableCommand && this.IsFarPlayer && this._prevCloseToPlayer)
								{
									base.ChaControl.ChangeLookEyesPtn(0);
									base.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
									base.ChaControl.ChangeLookNeckPtn(3, 1f);
									base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
								}
							}
						}
						else
						{
							base.ChaControl.ChangeLookEyesPtn(0);
							base.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
							base.ChaControl.ChangeLookNeckPtn(3, 1f);
							base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
						}
					}
					if (this.IsCloseToPlayer && !this._prevCloseToPlayer && this.CanTalk && Singleton<Manager.Resources>.Instance.AgentProfile.EncounterWhitelist.Contains(this._modeType) && this.BehaviorResources.Mode != Desire.ActionType.Idle)
					{
						if (!this.IsTraverseOffMeshLink())
						{
							this._prevCloseToPlayer = true;
							this.ChangeBehavior(Desire.ActionType.Encounter);
						}
					}
					else if (this.ReleasableCommand && this.IsFarPlayer && this._prevCloseToPlayer)
					{
						this._prevCloseToPlayer = false;
					}
				}
			}
			else if (state is Onbu)
			{
				if ((this._modeType == Desire.ActionType.EndTaskMasturbation || this._modeType == Desire.ActionType.EndTaskToilet || this._modeType == Desire.ActionType.EndTaskBath) && flag && this.ExistsInvader() && base.CurrentPoint != null && this.SurprisePoseID != null && this.CanTalk)
				{
					Desire.ActionType modeType2 = this._modeType;
					if (modeType2 != Desire.ActionType.EndTaskMasturbation)
					{
						if (modeType2 != Desire.ActionType.EndTaskToilet)
						{
							if (modeType2 == Desire.ActionType.EndTaskBath)
							{
								this.ReservedMode = Desire.ActionType.GotoDressOut;
							}
						}
						else
						{
							this.ReservedMode = Desire.ActionType.Normal;
						}
					}
					else
					{
						this.ReservedMode = Desire.ActionType.Normal;
					}
					this.ChangeBehavior(Desire.ActionType.FoundPeeping);
				}
			}
			else if (state is Photo)
			{
				if (this._modeType == Desire.ActionType.EndTaskMasturbation || this._modeType == Desire.ActionType.EndTaskToilet || this._modeType == Desire.ActionType.EndTaskBath)
				{
					if (flag && this.ExistsInvader() && base.CurrentPoint != null && this.SurprisePoseID != null && this.CanTalk)
					{
						Desire.ActionType modeType3 = this._modeType;
						if (modeType3 != Desire.ActionType.EndTaskMasturbation)
						{
							if (modeType3 != Desire.ActionType.EndTaskToilet)
							{
								if (modeType3 == Desire.ActionType.EndTaskBath)
								{
									this.ReservedMode = Desire.ActionType.GotoDressOut;
								}
							}
							else
							{
								this.ReservedMode = Desire.ActionType.Normal;
							}
						}
						else
						{
							this.ReservedMode = Desire.ActionType.Normal;
						}
						this.ChangeBehavior(Desire.ActionType.FoundPeeping);
					}
				}
				else
				{
					PlayerActor player2 = Singleton<Manager.Map>.Instance.Player;
					if (this.Mode != Desire.ActionType.Encounter)
					{
						if (this.UseNeckLook)
						{
							if (this.IsCloseToPlayer && this._prevCloseToPlayer)
							{
								Transform trfHeadParent = player2.ChaControl.cmpBoneBody.targetEtc.trfHeadParent;
								Transform transform = trfHeadParent.transform;
								base.ChaControl.ChangeLookEyesTarget(1, transform, 0.5f, 0f, 1f, 2f);
								base.ChaControl.ChangeLookEyesPtn(1);
								base.ChaControl.ChangeLookNeckTarget(1, transform, 0.5f, 0f, 1f, 0.8f);
								base.ChaControl.ChangeLookNeckPtn(1, 1f);
							}
							else if (this.ReleasableCommand && this.IsFarPlayer && this._prevCloseToPlayer)
							{
								base.ChaControl.ChangeLookEyesPtn(0);
								base.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
								base.ChaControl.ChangeLookNeckPtn(3, 1f);
								base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
							}
						}
						else
						{
							base.ChaControl.ChangeLookEyesPtn(0);
							base.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
							base.ChaControl.ChangeLookNeckPtn(3, 1f);
							base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
						}
					}
					if (this.IsCloseToPlayer && !this._prevCloseToPlayer && this.CanTalk && Singleton<Manager.Resources>.Instance.AgentProfile.EncounterWhitelist.Contains(this._modeType) && this.BehaviorResources.Mode != Desire.ActionType.Idle)
					{
						if (!this.IsTraverseOffMeshLink())
						{
							this._prevCloseToPlayer = true;
							this.ChangeBehavior(Desire.ActionType.Encounter);
						}
					}
					else if (this.ReleasableCommand && this.IsFarPlayer && this._prevCloseToPlayer)
					{
						this._prevCloseToPlayer = false;
					}
				}
			}
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x00297ED8 File Offset: 0x002962D8
		protected override IEnumerator LoadCharAsync(string fileName)
		{
			ChaFileControl chaFile;
			if (!fileName.IsNullOrEmpty())
			{
				chaFile = new ChaFileControl();
				if (!chaFile.LoadCharaFile(fileName, 1, false, true))
				{
					chaFile = null;
				}
				else
				{
					Singleton<Manager.Map>.Instance.SetTableAgentChaFile(this.ID, chaFile);
				}
			}
			else
			{
				chaFile = null;
			}
			base.ChaControl = Singleton<Character>.Instance.CreateChara(1, base.gameObject, 0, chaFile);
			base.ChaControl.Load(false);
			yield return null;
			base.ChaControl.ChangeLookEyesPtn(3);
			base.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.Controller.InitializeFaceLight(base.ChaControl.gameObject);
			yield return null;
			yield break;
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x00297EFC File Offset: 0x002962FC
		protected void LoadChar(string fileName)
		{
			ChaFileControl chaFileControl;
			if (!fileName.IsNullOrEmpty())
			{
				chaFileControl = new ChaFileControl();
				if (!chaFileControl.LoadCharaFile(fileName, 1, false, true))
				{
					chaFileControl = null;
				}
				else
				{
					Singleton<Manager.Map>.Instance.SetTableAgentChaFile(this.ID, chaFileControl);
				}
			}
			else
			{
				chaFileControl = null;
			}
			base.ChaControl = Singleton<Character>.Instance.CreateChara(1, base.gameObject, 0, chaFileControl);
			base.ChaControl.Load(false);
			base.ChaControl.ChangeLookEyesPtn(3);
			base.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.Controller.InitializeFaceLight(base.ChaControl.gameObject);
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x00297FA1 File Offset: 0x002963A1
		public void RefreshWalkStatus(Waypoint[] waypoints)
		{
			this.AgentController.SearchArea.RefreshQueryPoints();
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x00297FB4 File Offset: 0x002963B4
		public void UpdateActionPointCollision()
		{
			Dictionary<int, CollisionState> actionPointCollisionStateTable = this.ActionPointCollisionStateTable;
			float viewDistance = Singleton<Manager.Resources>.Instance.AgentProfile.ActionPointSight.ViewDistance;
			foreach (ActionPoint actionPoint in this._actionPoints)
			{
				int instanceID = actionPoint.InstanceID;
				CollisionState collisionState;
				if (!actionPointCollisionStateTable.TryGetValue(instanceID, out collisionState))
				{
					CollisionState collisionState2 = CollisionState.None;
					actionPointCollisionStateTable[instanceID] = collisionState2;
					collisionState = collisionState2;
				}
				bool flag = this.FovCheck(actionPoint);
				if (flag)
				{
					flag &= actionPoint.IsReachable(base.NavMeshAgent, viewDistance);
				}
				if (flag)
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						if (actionPoint.IsNeutralCommand)
						{
							this.SetCollisionState(actionPointCollisionStateTable, instanceID, CollisionState.Enter);
						}
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(actionPointCollisionStateTable, instanceID, CollisionState.Stay);
						break;
					}
				}
				else
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(actionPointCollisionStateTable, instanceID, CollisionState.None);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(actionPointCollisionStateTable, instanceID, CollisionState.Exit);
						break;
					}
				}
			}
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x00298100 File Offset: 0x00296500
		private bool HasCommands(ActionPoint point)
		{
			Actor partner = base.Partner;
			Desire.Type requestedDesire = this.RequestedDesire;
			EventType eventType = (EventType)0;
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (valueTuple.Item2 == requestedDesire)
				{
					eventType = valueTuple.Item1;
					break;
				}
			}
			if (partner == null)
			{
				return point.HasAgentActionPointInfo(eventType);
			}
			return point.ContainsAgentDateActionPointInfo(eventType);
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x00298184 File Offset: 0x00296584
		private bool FovCheck(ActionPoint actionPoint)
		{
			Transform transform = this.AgentController.transform;
			AgentProfile.ActionPointSightSetting actionPointSight = Singleton<Manager.Resources>.Instance.AgentProfile.ActionPointSight;
			return actionPointSight.HasEntered(transform, actionPoint.CommandCenter, actionPoint.Radius);
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x002981C8 File Offset: 0x002965C8
		public static ActorController GetCapturedInSight(AgentActor agent, List<Actor> actors)
		{
			Actor element = actors.GetElement(UnityEngine.Random.Range(0, actors.Count));
			if (element == null)
			{
				return null;
			}
			if (!AgentActor.IsCaptureInSight(agent, element))
			{
				return null;
			}
			ActorController controller = element.Controller;
			if (controller == null)
			{
				return null;
			}
			return controller;
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x0029821C File Offset: 0x0029661C
		private static bool IsCaptureInSight(AgentActor agent, Actor target)
		{
			if (target == null)
			{
				return false;
			}
			Vector3 position = agent.FovTargetPointTable[Actor.FovBodyPart.Head].position;
			int num = LayerMask.NameToLayer("Map");
			foreach (Actor.FovBodyPart key in AgentActor._checkParts)
			{
				Transform transform = target.FovTargetPointTable[key];
				Vector3 direction = position - transform.position;
				Ray ray = new Ray(position, direction);
				if (Physics.Raycast(ray, direction.magnitude, 1 << num))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x002982BC File Offset: 0x002966BC
		public void UpdateActorSightCollision()
		{
			Dictionary<int, CollisionState> actorCollisionStateTable = this.ActorCollisionStateTable;
			foreach (Actor actor in this.TargetActors)
			{
				int instanceID = actor.InstanceID;
				CollisionState collisionState;
				if (!actorCollisionStateTable.TryGetValue(instanceID, out collisionState))
				{
					CollisionState collisionState2 = CollisionState.None;
					actorCollisionStateTable[instanceID] = collisionState2;
					collisionState = collisionState2;
				}
				bool flag = this.FovCheck(actor);
				if (flag)
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(actorCollisionStateTable, instanceID, CollisionState.Enter);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(actorCollisionStateTable, instanceID, CollisionState.Stay);
						break;
					}
				}
				else
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(actorCollisionStateTable, instanceID, CollisionState.None);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(actorCollisionStateTable, instanceID, CollisionState.Exit);
						break;
					}
				}
			}
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x002983C8 File Offset: 0x002967C8
		private void SetCollisionState(Dictionary<int, CollisionState> table, int id, CollisionState state)
		{
			table[id] = state;
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x002983D4 File Offset: 0x002967D4
		private bool FovCheck(Actor actor)
		{
			Transform transform = this.AgentController.transform;
			AgentProfile.SightSetting characterFarSight = Singleton<Manager.Resources>.Instance.AgentProfile.CharacterFarSight;
			AgentProfile.SightSetting characterNearSight = Singleton<Manager.Resources>.Instance.AgentProfile.CharacterNearSight;
			return characterFarSight.HasEntered(transform, actor.Position) || characterNearSight.HasEntered(transform, actor.Position);
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x00298438 File Offset: 0x00296838
		public void UpdateActorFarSightCollision()
		{
			Dictionary<int, CollisionState> actorFarCollisionStateTable = this.ActorFarCollisionStateTable;
			foreach (Actor actor in this.TargetActors)
			{
				int instanceID = actor.InstanceID;
				CollisionState collisionState;
				if (!actorFarCollisionStateTable.TryGetValue(base.InstanceID, out collisionState))
				{
					CollisionState collisionState2 = CollisionState.None;
					actorFarCollisionStateTable[base.InstanceID] = collisionState2;
					collisionState = collisionState2;
				}
				bool flag = this.FovFarCheck(actor);
				if (flag)
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(actorFarCollisionStateTable, instanceID, CollisionState.Enter);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(actorFarCollisionStateTable, instanceID, CollisionState.Stay);
						break;
					}
				}
				else
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(actorFarCollisionStateTable, instanceID, CollisionState.None);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(actorFarCollisionStateTable, instanceID, CollisionState.Exit);
						break;
					}
				}
			}
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x0029854C File Offset: 0x0029694C
		private bool FovFarCheck(Actor actor)
		{
			Transform transform = this.AgentController.transform;
			AgentProfile.SightSetting characterFarSight = Singleton<Manager.Resources>.Instance.AgentProfile.CharacterFarSight;
			return characterFarSight.HasEntered(transform, actor.Position);
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x0029858C File Offset: 0x0029698C
		public void UpdateAnimalSightCollision()
		{
			Dictionary<int, CollisionState> animalCollisionStateTable = this.AnimalCollisionStateTable;
			foreach (AnimalBase animalBase in this.TargetAnimals)
			{
				int instanceID = animalBase.InstanceID;
				CollisionState collisionState;
				if (!animalCollisionStateTable.TryGetValue(base.InstanceID, out collisionState))
				{
					CollisionState collisionState2 = CollisionState.None;
					animalCollisionStateTable[base.InstanceID] = collisionState2;
					collisionState = collisionState2;
				}
				if (this.FovCheck(animalBase))
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(animalCollisionStateTable, instanceID, CollisionState.Enter);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(animalCollisionStateTable, instanceID, CollisionState.Stay);
						break;
					}
				}
				else
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(animalCollisionStateTable, instanceID, CollisionState.None);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(animalCollisionStateTable, instanceID, CollisionState.Exit);
						break;
					}
				}
			}
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x0029869C File Offset: 0x00296A9C
		private bool FovCheck(AnimalBase animal)
		{
			if (animal == null || animal.transform == null)
			{
				return false;
			}
			Transform transform = this.AgentController.transform;
			AgentProfile.SightSetting animalSight = Singleton<Manager.Resources>.Instance.AgentProfile.AnimalSight;
			return animalSight.HasEntered(transform, animal.Position, this.AnimalFovAngleOffsetY);
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x002986F7 File Offset: 0x00296AF7
		public void AddActor(Actor actor)
		{
			if (!this.TargetActors.Contains(actor))
			{
				this.TargetActors.Add(actor);
			}
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x00298718 File Offset: 0x00296B18
		public bool ExistsInvader()
		{
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			PlayerActor player = instance.Player;
			float num = Vector3.Distance(player.Position, base.Position);
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			CollisionState collisionState;
			if (num < locomotionProfile.AccessInvasionRange && this.ActorCollisionStateTable.TryGetValue(player.InstanceID, out collisionState))
			{
				if (collisionState == CollisionState.Enter || collisionState == CollisionState.Stay)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x0029878B File Offset: 0x00296B8B
		public void RemoveActor(Actor actor)
		{
			if (this.TargetActors.Contains(actor))
			{
				this.TargetActors.Remove(actor);
			}
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x002987AC File Offset: 0x00296BAC
		public void AddActionPoint(ActionPoint point)
		{
			if (!this._actionPoints.Contains(point))
			{
				this._actionPoints.Add(point);
			}
			this._actionPointCheckFlagTable[point] = false;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			foreach (ActionPoint item in this._actionPoints)
			{
				list.Add(item);
			}
			this.SearchTargets = list.ToArray();
			point.Actor = this;
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x00298854 File Offset: 0x00296C54
		public void RemoveActionPoint(ActionPoint point)
		{
			if (this._actionPoints.Contains(point))
			{
				this._actionPoints.Remove(point);
			}
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			foreach (ActionPoint item in this._actionPoints)
			{
				list.Add(item);
			}
			this.SearchTargets = list.ToArray();
			point.Actor = null;
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x002988F0 File Offset: 0x00296CF0
		public void AddAnimal(AnimalBase animal)
		{
			if (animal == null)
			{
				return;
			}
			if (!this.TargetAnimals.Contains(animal))
			{
				this.TargetAnimals.Add(animal);
			}
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x0029891C File Offset: 0x00296D1C
		public void RemoveAnimal(AnimalBase animal)
		{
			if (animal == null)
			{
				return;
			}
			if (this.TargetAnimals.Contains(animal))
			{
				this.TargetAnimals.Remove(animal);
			}
			if (this.AnimalCollisionStateTable.ContainsKey(animal.InstanceID))
			{
				this.AnimalCollisionStateTable.Remove(animal.InstanceID);
			}
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x0029897C File Offset: 0x00296D7C
		private void ActivatePairing(int id, bool isDate)
		{
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.Partner = this;
			this.BehaviorResources.ChangeMode(Desire.ActionType.Date);
			this.Mode = Desire.ActionType.Date;
			this.AgentData.CarryingItem = null;
			base.Partner = player;
			player.Mode = Desire.ActionType.Date;
			Singleton<Manager.Map>.Instance.Player.PlayerController.CommandArea.RefreshCommands();
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x002989E4 File Offset: 0x00296DE4
		public void DeactivatePairing(int id)
		{
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.Partner = null;
			AgentBehaviorTree agentBehaviorTree = this.BehaviorResources.Current;
			this.BehaviorResources.ChangeMode(Desire.ActionType.Normal);
			this.Mode = Desire.ActionType.Normal;
			base.Partner = null;
			player.Mode = Desire.ActionType.Normal;
			player.PlayerData.DateEatTrigger = false;
			Singleton<Manager.Map>.Instance.Player.PlayerController.CommandArea.RefreshCommands();
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x00298A55 File Offset: 0x00296E55
		// (set) Token: 0x06006313 RID: 25363 RVA: 0x00298A5D File Offset: 0x00296E5D
		private Vector3 LocalTargetPosition { get; set; }

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x00298A66 File Offset: 0x00296E66
		// (set) Token: 0x06006315 RID: 25365 RVA: 0x00298A6E File Offset: 0x00296E6E
		public Quaternion LocalTargetRotation { get; set; }

		// Token: 0x06006316 RID: 25366 RVA: 0x00298A78 File Offset: 0x00296E78
		public void ActivateHoldingHands(int id, bool enabled)
		{
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			HandsHolder handsHolder = player.HandsHolder;
			handsHolder.RightHandAnimator = base.ChaControl.animBody;
			handsHolder.RightHandIK = base.ChaControl.fullBodyIK;
			if (handsHolder.RightHandTarget != null)
			{
				if (enabled)
				{
					this.LocalTargetPosition = handsHolder.TargetTransform.localPosition;
					this.LocalTargetRotation = handsHolder.TargetTransform.localRotation;
				}
				else
				{
					handsHolder.TargetTransform.localPosition = this.LocalTargetPosition;
					handsHolder.TargetTransform.localRotation = this.LocalTargetRotation;
				}
			}
			GameObject gameObject = base.ChaControl.animBody.transform.FindLoop("cf_J_Kosi02");
			handsHolder.RightLookTarget = gameObject.transform;
			if (!Singleton<Manager.Resources>.Instance.LocomotionProfile.HoldingElboTarget.IsNullOrEmpty())
			{
				GameObject gameObject2 = base.ChaControl.animBody.transform.FindLoop(Singleton<Manager.Resources>.Instance.LocomotionProfile.HoldingElboTarget);
				if (gameObject2 != null)
				{
					handsHolder.RightElboTarget = gameObject2.transform;
				}
			}
			handsHolder.MinDistance = Singleton<Manager.Resources>.Instance.AgentProfile.GetOffsetInParty(base.ChaControl.GetShapeBodyValue(0)).magnitude;
			handsHolder.enabled = enabled;
			handsHolder.EnabledHolding = enabled;
			if (!enabled)
			{
				player.OldEnabledHoldingHand = false;
			}
			string height1ParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.Height1ParameterName;
			float shapeBodyValue = base.ChaControl.GetShapeBodyValue(0);
			foreach (AnimatorControllerParameter animatorControllerParameter in player.Animation.Animator.parameters)
			{
				if (animatorControllerParameter.type == AnimatorControllerParameterType.Float && animatorControllerParameter.name == height1ParameterName)
				{
					player.Animation.Animator.SetFloat(height1ParameterName, shapeBodyValue);
				}
			}
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00298C68 File Offset: 0x00297068
		public void ActivateTransfer(bool move = true)
		{
			int id = this.AgentData.SickState.ID;
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			EquipEventItemInfo itemInfo = null;
			PlayState playState;
			this.LoadLocomotionAnimation(this, id, weather, out playState, ref itemInfo);
			if (playState == null)
			{
				return;
			}
			base.ResetEquipEventItem(itemInfo);
			ActorAnimation animation = this.Animation;
			animation.InitializeStates(playState);
			if (animation.Animator == null)
			{
				return;
			}
			AnimatorStateInfo currentAnimatorStateInfo = animation.Animator.GetCurrentAnimatorStateInfo(0);
			bool flag = false;
			foreach (PlayState.Info info in playState.MainStateInfo.InStateInfo.StateInfos)
			{
				if (currentAnimatorStateInfo.shortNameHash == info.ShortNameStateHash)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				if (playState.MaskStateInfo.layer > 0)
				{
					float layerWeight = this.Animation.Animator.GetLayerWeight(playState.MaskStateInfo.layer);
					if (layerWeight == 0f)
					{
						flag = false;
					}
				}
				else
				{
					for (int j = 1; j < animation.Animator.layerCount; j++)
					{
						float layerWeight2 = animation.Animator.GetLayerWeight(j);
						if (layerWeight2 > 0f)
						{
							flag = false;
							break;
						}
					}
				}
			}
			if (flag)
			{
				this.Animation.InStates.Clear();
				this.Animation.OutStates.Clear();
				this.Animation.ActionStates.Clear();
			}
			else
			{
				int layer = playState.Layer;
				if (animation.RefsActAnimInfo)
				{
					animation.StopAllAnimCoroutine();
					animation.PlayInLocoAnimation(animation.AnimInfo.endEnableBlend, animation.AnimInfo.endBlendSec, layer);
					animation.RefsActAnimInfo = false;
				}
				else
				{
					bool enableFade = playState.MainStateInfo.InStateInfo.EnableFade;
					float fadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
					animation.StopAllAnimCoroutine();
					animation.PlayInLocoAnimation(enableFade, fadeSecond, layer);
				}
			}
			if (move)
			{
				if (base.NavMeshAgent.isStopped)
				{
					base.NavMeshAgent.isStopped = false;
				}
				if (base.IsKinematic)
				{
					base.IsKinematic = false;
				}
			}
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x00298ED8 File Offset: 0x002972D8
		private void LoadLocomotionAnimation(AgentActor agent, int sickID, Weather weather, out PlayState info, ref EquipEventItemInfo itemInfo)
		{
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			StuffItem carryingItem = agent.AgentData.CarryingItem;
			if (carryingItem != null && !agentProfile.CanStandEatItems.Exists((ItemIDKeyPair pair) => pair.categoryID == carryingItem.CategoryID && pair.itemID == carryingItem.ID))
			{
				int cookMoveLocoID = agentProfile.PoseIDTable.CookMoveLocoID;
				info = Singleton<Manager.Resources>.Instance.Animation.AgentLocomotionStateTable[cookMoveLocoID];
				int obonEventItemID = locomotionProfile.ObonEventItemID;
				ActionItemInfo actionItemInfo = Singleton<Manager.Resources>.Instance.Map.EventItemList[obonEventItemID];
				string agentOtherParentName = base.LocomotionProfile.AgentOtherParentName;
				itemInfo = new EquipEventItemInfo
				{
					EventItemID = obonEventItemID,
					ActionItemInfo = actionItemInfo,
					ParentName = agentOtherParentName
				};
			}
			else if (sickID != 3)
			{
				if (sickID != 4)
				{
					StuffItem equipedUmbrellaItem = agent.AgentData.EquipedUmbrellaItem;
					CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
					if (equipedUmbrellaItem != null && equipedUmbrellaItem.CategoryID == itemIDDefine.UmbrellaID.categoryID && equipedUmbrellaItem.ID == itemIDDefine.UmbrellaID.itemID)
					{
						if (weather != Weather.Rain && weather != Weather.Storm)
						{
							this.LoadLocoAnimationOtherCond(weather, out info, ref itemInfo);
						}
						else
						{
							int umbrellaLocoID = agentProfile.PoseIDTable.UmbrellaLocoID;
							info = Singleton<Manager.Resources>.Instance.Animation.AgentLocomotionStateTable[umbrellaLocoID];
							ItemIDKeyPair umbrellaID = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine.UmbrellaID;
							itemInfo = Singleton<Manager.Resources>.Instance.GameInfo.CommonEquipEventItemTable[umbrellaID.categoryID][umbrellaID.itemID];
							itemInfo.ParentName = locomotionProfile.AgentLocoItemParentName;
						}
					}
					else
					{
						this.LoadLocoAnimationOtherCond(weather, out info, ref itemInfo);
					}
				}
				else
				{
					int hurtLocoID = agentProfile.PoseIDTable.HurtLocoID;
					info = Singleton<Manager.Resources>.Instance.Animation.AgentLocomotionStateTable[hurtLocoID];
				}
			}
			else
			{
				int locomotionID = agentProfile.PoseIDTable.LocomotionID;
				info = Singleton<Manager.Resources>.Instance.Animation.AgentLocomotionStateTable[locomotionID];
			}
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x00299130 File Offset: 0x00297530
		private void LoadLocoAnimationOtherCond(Weather weather, out PlayState info, ref EquipEventItemInfo itemInfo)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Desire.ActionType mode = this.Mode;
			if (mode != Desire.ActionType.EndTaskGift && mode != Desire.ActionType.EndTaskH)
			{
				this.LoadLocoAnimationDefault(out info);
			}
			else
			{
				int mojimojiLocoID = instance.AgentProfile.PoseIDTable.MojimojiLocoID;
				info = instance.Animation.AgentLocomotionStateTable[mojimojiLocoID];
			}
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x00299194 File Offset: 0x00297594
		private void LoadLocoAnimationDefault(out PlayState info)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			AgentProfile agentProfile = instance.AgentProfile;
			int key;
			if (agentProfile.ScrollWhitelist.Contains(this.Mode))
			{
				key = instance.AgentProfile.PoseIDTable.WalkLocoID;
			}
			else
			{
				key = instance.AgentProfile.PoseIDTable.LocomotionID;
			}
			instance.Animation.AgentLocomotionStateTable.TryGetValue(key, out info);
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x002991FE File Offset: 0x002975FE
		public void ResetLocomotionAnimation(bool move = true)
		{
			this.ActivateTransfer(move);
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x00299208 File Offset: 0x00297608
		public void UpdateLocomotionSpeed(Waypoint destination)
		{
			int tutorialProgress;
			if (this.TutorialMode && ((tutorialProgress = Manager.Map.GetTutorialProgress()) == 14 || tutorialProgress == 15) && (this.TutorialLocomoCaseID == 100 || this.TutorialLocomoCaseID == 101))
			{
				LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
				float speed = base.NavMeshAgent.speed;
				float b = (this.TutorialLocomoCaseID != 100) ? locomotionProfile.AgentSpeed.tutorialRunSpeed : locomotionProfile.AgentSpeed.tutorialWalkSpeed;
				base.NavMeshAgent.speed = Mathf.Lerp(speed, b, locomotionProfile.TutorialLerpSpeed);
			}
			else
			{
				float speed2 = base.NavMeshAgent.speed;
				int id = this.AgentData.SickState.ID;
				Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
				float b2;
				this.LoadLocomotionSpeed(destination, id, weather, out b2);
				LocomotionProfile locomotionProfile2 = Singleton<Manager.Resources>.Instance.LocomotionProfile;
				base.NavMeshAgent.speed = Mathf.Lerp(speed2, b2, locomotionProfile2.LerpSpeed);
			}
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x0029931C File Offset: 0x0029771C
		private void LoadLocomotionSpeed(Waypoint destination, int sickID, Weather weather, out float speed)
		{
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			StuffItem carryingItem = this.AgentData.CarryingItem;
			if (carryingItem != null && !agentProfile.CanStandEatItems.Exists((ItemIDKeyPair pair) => pair.categoryID == carryingItem.CategoryID && pair.itemID == carryingItem.ID))
			{
				speed = Singleton<Manager.Resources>.Instance.LocomotionProfile.AgentSpeed.walkSpeed;
			}
			else if (sickID != 3 && sickID != 4)
			{
				StuffItem equipedUmbrellaItem = this.AgentData.EquipedUmbrellaItem;
				CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
				if (equipedUmbrellaItem != null && equipedUmbrellaItem.CategoryID == itemIDDefine.UmbrellaID.categoryID && equipedUmbrellaItem.ID == itemIDDefine.UmbrellaID.itemID)
				{
					if (weather != Weather.Rain && weather != Weather.Storm)
					{
						this.LoadLocomotionSpeedOtherCondition(destination, sickID, weather, out speed);
					}
					else
					{
						speed = Singleton<Manager.Resources>.Instance.LocomotionProfile.AgentSpeed.walkSpeed;
					}
				}
				else
				{
					this.LoadLocomotionSpeedOtherCondition(destination, sickID, weather, out speed);
				}
			}
			else
			{
				speed = Singleton<Manager.Resources>.Instance.LocomotionProfile.AgentSpeed.walkSpeed;
			}
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00299478 File Offset: 0x00297878
		private void LoadLocomotionSpeedOtherCondition(Waypoint destination, int sickID, Weather weather, out float speed)
		{
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			Desire.ActionType mode = this.Mode;
			if (mode != Desire.ActionType.EndTaskGift && mode != Desire.ActionType.EndTaskH)
			{
				if (weather != Weather.Rain && weather != Weather.Storm)
				{
					if (this.Mode == Desire.ActionType.Normal)
					{
						if (base.MapArea != null && destination != null && destination.OwnerArea != null)
						{
							if (destination.OwnerArea.AreaID == base.MapArea.AreaID)
							{
								speed = locomotionProfile.AgentSpeed.walkSpeed;
							}
							else
							{
								speed = locomotionProfile.AgentSpeed.runSpeed;
							}
						}
						else
						{
							speed = locomotionProfile.AgentSpeed.walkSpeed;
						}
					}
					else
					{
						float num = this.AgentData.StatsTable[5];
						float num2 = num * Singleton<Manager.Resources>.Instance.AgentProfile.MustRunMotivationPercent;
						int desireKey = Desire.GetDesireKey(this.RequestedDesire);
						float? motivation = this.GetMotivation(desireKey);
						if (motivation != null && motivation.Value < num2)
						{
							speed = locomotionProfile.AgentSpeed.runSpeed;
						}
						else if (base.MapArea != null)
						{
							int areaID = base.MapArea.AreaID;
							if (this.TargetInSightActionPoint != null && this.TargetInSightActionPoint.OwnerArea != null)
							{
								if (this.TargetInSightActionPoint.OwnerArea.AreaID == areaID)
								{
									speed = locomotionProfile.AgentSpeed.walkSpeed;
								}
								else
								{
									speed = locomotionProfile.AgentSpeed.runSpeed;
								}
							}
							else if (destination != null && destination.OwnerArea != null)
							{
								if (destination.OwnerArea.AreaID == areaID)
								{
									speed = locomotionProfile.AgentSpeed.walkSpeed;
								}
								else
								{
									speed = locomotionProfile.AgentSpeed.runSpeed;
								}
							}
							else
							{
								speed = locomotionProfile.AgentSpeed.walkSpeed;
							}
						}
						else
						{
							speed = locomotionProfile.AgentSpeed.walkSpeed;
						}
					}
				}
				else
				{
					speed = locomotionProfile.AgentSpeed.runSpeed;
				}
			}
			else
			{
				speed = locomotionProfile.AgentSpeed.walkSpeed;
			}
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x00299710 File Offset: 0x00297B10
		public void ChangeBehavior(Desire.ActionType type)
		{
			this.PrevMode = this.Mode;
			if (this.Mode != Desire.ActionType.Normal)
			{
				this.PrevActionMode = this.Mode;
				if (this.PrevActionMode != Desire.ActionType.EndTaskWildAnimal)
				{
					this.AgentData.CheckCatEvent = false;
				}
			}
			this.Mode = type;
			if (this.AgentData.CarryingItem != null && type != Desire.ActionType.Encounter && type != Desire.ActionType.SearchEatSpot && type != Desire.ActionType.EndTaskEat && type != Desire.ActionType.EndTaskEatThere)
			{
				this.AgentData.CarryingItem = null;
			}
			if (type == Desire.ActionType.Normal)
			{
				int num = -1;
				base.PoseID = num;
				base.ActionID = num;
				this.StateType = State.Type.Normal;
				base.EventKey = (EventType)0;
				this.TargetInSightActionPoint = null;
				this.TargetInSightActor = null;
				base.Partner = null;
				this.CommandPartner = null;
				this.ResetActionFlag();
				if (this._schedule.enabled)
				{
					this._schedule.enabled = false;
				}
				if (this.IsHealthManager)
				{
					int desireKey = Desire.GetDesireKey(Desire.Type.Break);
					this.SetDesire(desireKey, 70f);
				}
				base.ActivateNavMeshAgent();
				this.ActivateTransfer(true);
			}
			this.BehaviorResources.ChangeMode(type);
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x00299840 File Offset: 0x00297C40
		public void ChangeReservedBehavior()
		{
			if (this.ReservedMode == Desire.ActionType.OverworkA)
			{
				this.AgentData.SetAppendEventFlagCheck(1, true);
			}
			this.ChangeBehavior(this.ReservedMode);
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x00299868 File Offset: 0x00297C68
		public void StartWeakness()
		{
			this.AgentData.WeaknessMotivation = this.AgentData.StatsTable[5];
			this.ChangeBehavior(Desire.ActionType.WeaknessA);
			this.CommandPartner = null;
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x00299898 File Offset: 0x00297C98
		public void ChangeTutorialBehavior(AIProject.Definitions.Tutorial.ActionType type)
		{
			AIProject.Definitions.Tutorial.ActionType tutorialType = this.TutorialType;
			this.AgentData.TutorialModeType = type;
			this.TutorialType = type;
			if (this.TutorialBehaviorResources == null)
			{
				return;
			}
			if (this.TutorialType != tutorialType)
			{
				CommandArea commandArea;
				if (Singleton<Manager.Map>.IsInstance())
				{
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					if (player == null)
					{
						commandArea = null;
					}
					else
					{
						PlayerController playerController = player.PlayerController;
						commandArea = ((playerController != null) ? playerController.CommandArea : null);
					}
				}
				else
				{
					commandArea = null;
				}
				CommandArea commandArea2 = commandArea;
				if (commandArea2 != null && commandArea2.ContainsConsiderationObject(this))
				{
					commandArea2.RefreshCommands();
				}
			}
			this.TutorialBehaviorResources.ChangeMode(type);
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x00299944 File Offset: 0x00297D44
		public void CreateTutorialBehaviorResources()
		{
			if (this.TutorialBehaviorResources == null)
			{
				Transform transform = new GameObject("TutorialBehaviorResources").transform;
				transform.SetParent(base.transform, false);
				this.TutorialBehaviorResources = transform.GetOrAddComponent<TutorialBehaviorTreeResources>();
				this.TutorialBehaviorResources.Initialize(this);
			}
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x00299998 File Offset: 0x00297D98
		public void ChangeFirstTutorialBehavior()
		{
			if (this.BehaviorResources != null && this.BehaviorResources.enabled)
			{
				this.BehaviorResources.enabled = false;
			}
			this.ChangeBehavior(Desire.ActionType.Idle);
			this.PrevMode = Desire.ActionType.Idle;
			this.TutorialMode = true;
			AgentProfile.TutorialSetting tutorial;
			if (Singleton<Manager.Resources>.IsInstance() && (tutorial = Singleton<Manager.Resources>.Instance.AgentProfile.Tutorial) != null)
			{
				AssetBundleInfo animABInfo = default(AssetBundleInfo);
				int animatorID;
				if (Game.isAdd50)
				{
					if (!Singleton<Manager.Resources>.Instance.AgentProfile.DicAnimatorID.TryGetValue(base.ChaControl.fileParam.personality, out animatorID))
					{
						animatorID = tutorial.AnimatorID;
					}
				}
				else
				{
					animatorID = tutorial.AnimatorID;
				}
				RuntimeAnimatorController charaAnimator = Singleton<Manager.Resources>.Instance.Animation.GetCharaAnimator(animatorID, ref animABInfo);
				this.Animation.AnimABInfo = animABInfo;
				base.ChangeAnimator(charaAnimator);
				this.PlayTutorialDefaultStateAnim();
			}
			this.ChangeTutorialBehavior(this.AgentData.TutorialModeType);
			CommandArea commandArea = Manager.Map.GetCommandArea();
			if (commandArea != null && commandArea.ContainsConsiderationObject(this))
			{
				commandArea.RefreshCommands();
			}
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x00299AC0 File Offset: 0x00297EC0
		public void ChangeFirstNormalBehavior()
		{
			if (this.TutorialBehaviorResources != null && this.TutorialBehaviorResources.gameObject != null)
			{
				UnityEngine.Object.Destroy(this.TutorialBehaviorResources.gameObject);
				this.TutorialBehaviorResources = null;
			}
			this.EnableBehavior();
			AssetBundleInfo animABInfo = default(AssetBundleInfo);
			RuntimeAnimatorController charaAnimator = Singleton<Manager.Resources>.Instance.Animation.GetCharaAnimator(0, ref animABInfo);
			if (charaAnimator != null)
			{
				this.Animation.AnimABInfo = animABInfo;
				base.ChangeAnimator(charaAnimator);
			}
			this.TutorialMode = false;
			AIProject.Definitions.Tutorial.ActionType actionType = AIProject.Definitions.Tutorial.ActionType.End;
			this.AgentData.TutorialModeType = actionType;
			this.TutorialType = actionType;
			base.ActivateNavMeshAgent();
			this.ChangeBehavior(Desire.ActionType.Normal);
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x00299B78 File Offset: 0x00297F78
		public void PlayTutorialDefaultStateAnim()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			PlayState.AnimStateInfo animStateInfo;
			if (definePack == null)
			{
				animStateInfo = null;
			}
			else
			{
				DefinePack.AnimatorStateNameGroup animatorState = definePack.AnimatorState;
				animStateInfo = ((animatorState != null) ? animatorState.IdleStateInfo : null);
			}
			PlayState.AnimStateInfo animStateInfo2 = animStateInfo;
			if (animStateInfo2 == null)
			{
				return;
			}
			Animator animator = this.Animation.Animator;
			if (!this.Animation.isActiveAndEnabled || !animator.isActiveAndEnabled)
			{
				return;
			}
			animator.CrossFadeInFixedTime(animStateInfo2.StateInfos.LastOrDefault<PlayState.Info>().stateName ?? "Locomotion", animStateInfo2.FadeSecond, 0, 0f);
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x00299C1C File Offset: 0x0029801C
		public PlayState.AnimStateInfo GetTutorialPersonalIdleState()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return null;
			}
			Dictionary<int, PoseKeyPair> tutorialIdlePoseTable = Singleton<Manager.Resources>.Instance.AgentProfile.TutorialIdlePoseTable;
			if (tutorialIdlePoseTable.IsNullOrEmpty<int, PoseKeyPair>())
			{
				return Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
			}
			int charaID = this.charaID;
			PoseKeyPair poseKeyPair;
			if (!tutorialIdlePoseTable.TryGetValue(charaID, out poseKeyPair) && !tutorialIdlePoseTable.TryGetValue(0, out poseKeyPair))
			{
				return Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
			}
			Dictionary<int, PlayState> dictionary;
			if (!Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable.TryGetValue(poseKeyPair.postureID, out dictionary))
			{
				return Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
			}
			PlayState playState;
			if (!dictionary.TryGetValue(poseKeyPair.poseID, out playState) || playState == null)
			{
				return Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
			}
			return playState.MainStateInfo.InStateInfo;
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x00299D10 File Offset: 0x00298110
		public void ElectNextPoint()
		{
			EventType ev;
			if (Singleton<Manager.Resources>.Instance.AgentProfile.AfterActionTable.TryGetValue(base.EventKey, out ev))
			{
				Chunk chunk;
				Singleton<Manager.Map>.Instance.ChunkTable.TryGetValue(base.ChunkID, out chunk);
				if (chunk)
				{
					ActionPoint actionPoint = null;
					float? num = null;
					this.NearestActionPoint(chunk.ActionPoints, ev, ref actionPoint, ref num);
					this.NearestActionPoint(chunk.AppendActionPoints, ev, ref actionPoint, ref num);
					if (actionPoint != null)
					{
						base.NextPoint = actionPoint;
					}
				}
			}
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x00299DA4 File Offset: 0x002981A4
		private void NearestActionPoint(List<ActionPoint> actionPoints, EventType ev, ref ActionPoint actionPoint, ref float? distance)
		{
			foreach (ActionPoint actionPoint2 in actionPoints)
			{
				if (actionPoint2.HasAgentActionPointInfo(ev))
				{
					float num = Vector3.Distance(actionPoint2.Position, base.Position);
					if (distance == null)
					{
						actionPoint = actionPoint2;
						distance = new float?(num);
					}
					else if (num < distance.Value)
					{
						actionPoint = actionPoint2;
						distance = new float?(num);
					}
				}
			}
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x00299E5C File Offset: 0x0029825C
		public override void OnMinuteUpdated(TimeSpan deltaTime)
		{
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			AgentData agentData = this.AgentData;
			AgentProfile.DiminuationRates diminuationRate = agentProfile.DiminuationRate;
			AgentProfile.DiminuationRates weaknessDiminuationRate = agentProfile.WeaknessDiminuationRate;
			AgentProfile.DiminuationRates talkMotivationDimRate = agentProfile.TalkMotivationDimRate;
			Dictionary<int, AgentProfile.DiminuationRates> diminMotivationRate = agentProfile.DiminMotivationRate;
			if (this.Mode == Desire.ActionType.Normal || this.Mode == Desire.ActionType.CommonSearchBreak || this.Mode == Desire.ActionType.CommonBreak || this.Mode == Desire.ActionType.CommonGameThere)
			{
				this.UpdateDesire(deltaTime, diminMotivationRate);
			}
			float max = agentData.StatsTable[5];
			if (this.BehaviorResources.enabled)
			{
				float value = agentData.TalkMotivation + talkMotivationDimRate.valueRecovery * (float)deltaTime.TotalMinutes;
				agentData.TalkMotivation = Mathf.Clamp(value, 0f, max);
			}
			if (this.UpdateMotivation)
			{
				Desire.Type key;
				if (Desire.ModeTable.TryGetValue(this.Mode, out key))
				{
					int desireKey = Desire.GetDesireKey(key);
					float? motivation = this.GetMotivation(desireKey);
					if (motivation != null)
					{
						float num = diminMotivationRate[desireKey].value * (float)deltaTime.TotalMinutes;
						float value2 = Mathf.Clamp(motivation.Value - num, 0f, max);
						this.SetMotivation(desireKey, value2);
					}
				}
				this.MotivationInEncounter -= diminuationRate.value;
				this.MotivationInEncounter = Mathf.Clamp(this.MotivationInEncounter, 0f, max);
				this.AgentData.WeaknessMotivation -= weaknessDiminuationRate.value;
				this.AgentData.WeaknessMotivation = Mathf.Clamp(this.AgentData.WeaknessMotivation, 0f, max);
				this.RuntimeMotivationInPhoto -= diminuationRate.value;
				this.RuntimeMotivationInPhoto = Mathf.Clamp(this.RuntimeMotivationInPhoto, 0f, max);
				Threshold diminuationInMasturbation = agentProfile.DiminuationInMasturbation;
				Threshold diminuationInLesbian = agentProfile.DiminuationInLesbian;
				this._runtimeMotivationInMasturbation -= diminuationInMasturbation.RandomValue;
				this._runtimeMotivationInLesbian -= diminuationInLesbian.RandomValue;
			}
			this._runtimeMotivationInMasturbation = Mathf.Clamp(this._runtimeMotivationInMasturbation, 0f, 100f);
			this._runtimeMotivationInLesbian = Mathf.Clamp(this._runtimeMotivationInLesbian, 0f, 100f);
			if (!this.AgentController.SleepedSchedule)
			{
				if (this.BehaviorResources.enabled && this._schedule.enabled && this._schedule.useGameTime && this._schedule.progress)
				{
					this._schedule.elapsedTime = this._schedule.elapsedTime + (float)deltaTime.TotalMinutes;
					if (this._schedule.elapsedTime > this._schedule.duration)
					{
						this._schedule.enabled = false;
					}
				}
			}
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x0029A144 File Offset: 0x00298544
		public override void OnSecondUpdated(TimeSpan timeSpan)
		{
			this.OnSickUpdate(timeSpan);
			AgentTimer agentTimer;
			if ((agentTimer = this._timer) == null)
			{
				agentTimer = (this._timer = new AgentTimer(this));
			}
			AgentTimer agentTimer2 = agentTimer;
			if (agentTimer2 != null)
			{
				agentTimer2.Update(timeSpan);
			}
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x0029A184 File Offset: 0x00298584
		public StuffItem SelectDrinkItem()
		{
			AgentData agentData = this.AgentData;
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> drinkParameterTable = Singleton<Manager.Resources>.Instance.GameInfo.DrinkParameterTable;
			List<StuffItem> list = ListPool<StuffItem>.Get();
			float num = agentData.StatsTable[0];
			foreach (StuffItem stuffItem3 in agentData.ItemList)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
				if (drinkParameterTable.TryGetValue(stuffItem3.CategoryID, out dictionary))
				{
					Dictionary<int, FoodParameterPacket> dictionary2;
					if (dictionary.TryGetValue(stuffItem3.ID, out dictionary2))
					{
						list.Add(stuffItem3);
					}
				}
			}
			StuffItem stuffItem2 = null;
			if (num < agentProfile.ColdTempBorder)
			{
				List<StuffItem> list2 = ListPool<StuffItem>.Get();
				using (List<StuffItem>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						StuffItem stuffItem = enumerator2.Current;
						if (agentProfile.LowerTempDrinkItems.Exists((ItemIDKeyPair pair) => pair.categoryID == stuffItem.CategoryID && pair.itemID == stuffItem.ID))
						{
							list2.Add(stuffItem);
						}
					}
				}
				if (!list2.IsNullOrEmpty<StuffItem>())
				{
					stuffItem2 = list2.GetElement(UnityEngine.Random.Range(0, list2.Count));
				}
				ListPool<StuffItem>.Release(list2);
			}
			else if (num > agentProfile.HotTempBorder)
			{
				List<StuffItem> list3 = ListPool<StuffItem>.Get();
				using (List<StuffItem>.Enumerator enumerator3 = list.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						StuffItem stuffItem = enumerator3.Current;
						if (agentProfile.RaiseTempDrinkItems.Exists((ItemIDKeyPair pair) => pair.categoryID == stuffItem.CategoryID && pair.itemID == stuffItem.ID))
						{
							list3.Add(stuffItem);
						}
					}
				}
				if (!list3.IsNullOrEmpty<StuffItem>())
				{
					stuffItem2 = list3.GetElement(UnityEngine.Random.Range(0, list3.Count));
				}
				ListPool<StuffItem>.Release(list3);
			}
			if (stuffItem2 == null)
			{
				stuffItem2 = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			}
			ListPool<StuffItem>.Release(list);
			return new StuffItem(stuffItem2.CategoryID, stuffItem2.ID, 1);
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x0029A3F8 File Offset: 0x002987F8
		protected override void LoadEquipedEventItem(EquipEventItemInfo eventItemInfo)
		{
			AssetBundleInfo bundleInfo = eventItemInfo.ActionItemInfo.assetbundleInfo;
			if (bundleInfo.assetbundle.IsNullOrEmpty() || bundleInfo.asset.IsNullOrEmpty() || bundleInfo.manifest.IsNullOrEmpty())
			{
				return;
			}
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundleInfo.assetbundle, bundleInfo.asset, false, bundleInfo.manifest);
			if (gameObject != null)
			{
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundleInfo.assetbundle && x.Item2 == bundleInfo.manifest))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundleInfo.assetbundle, bundleInfo.manifest));
				}
				string parentName = eventItemInfo.ParentName;
				GameObject gameObject2 = base.ChaControl.animBody.transform.FindLoop(parentName);
				if (gameObject2 != null)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject, gameObject2.transform, false);
					gameObject3.gameObject.name = gameObject.gameObject.name;
					gameObject3.transform.localPosition = Vector3.zero;
					gameObject3.transform.localRotation = Quaternion.identity;
					gameObject3.transform.localScale = Vector3.one;
					gameObject3.SetActive(true);
					base.EquipedItem = new ItemCache
					{
						EventItemID = eventItemInfo.EventItemID,
						KeyInfo = eventItemInfo.ActionItemInfo,
						AsGameObject = gameObject3
					};
					if (eventItemInfo.ActionItemInfo.existsAnimation)
					{
						Animator component = gameObject3.GetComponent<Animator>();
						RuntimeAnimatorController runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(eventItemInfo.ActionItemInfo.animeAssetBundle);
						component.runtimeAnimatorController = runtimeAnimatorController;
						this.Animation.ItemAnimatorTable[gameObject3.GetInstanceID()] = new ItemAnimInfo
						{
							Animator = component,
							Parameters = component.parameters,
							Sync = true
						};
					}
				}
			}
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x0029A608 File Offset: 0x00298A08
		public override void LoadEventItems(PlayState playState)
		{
			if (playState.ItemInfoCount > 0)
			{
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				for (int i = 0; i < playState.ItemInfoCount; i++)
				{
					PlayState.ItemInfo itemInfo = playState.GetItemInfo(i);
					ActionItemInfo eventItemInfo;
					if (itemInfo.fromEquipedItem)
					{
						ActionPointInfo actionPointInfo = this.Animation.ActionPointInfo;
						Dictionary<int, EquipEventItemInfo> dictionary;
						EquipEventItemInfo equipEventItemInfo;
						if (instance.GameInfo.SearchEquipEventItemTable.TryGetValue(actionPointInfo.searchAreaID, out dictionary) && dictionary.TryGetValue(actionPointInfo.gradeValue, out equipEventItemInfo))
						{
							base.LoadEventItem(equipEventItemInfo.EventItemID, itemInfo, equipEventItemInfo.ActionItemInfo);
						}
					}
					else if (instance.Map.EventItemList.TryGetValue(itemInfo.itemID, out eventItemInfo))
					{
						base.LoadEventItem(itemInfo.itemID, itemInfo, eventItemInfo);
					}
				}
			}
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x0029A6DC File Offset: 0x00298ADC
		public void LoadEventItems(List<PlayState.ItemInfo> itemList)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			foreach (PlayState.ItemInfo itemInfo in itemList)
			{
				ActionItemInfo eventItemInfo;
				if (itemInfo.fromEquipedItem)
				{
					ActionPointInfo actionPointInfo = this.Animation.ActionPointInfo;
					Dictionary<int, EquipEventItemInfo> dictionary;
					EquipEventItemInfo equipEventItemInfo;
					if (instance.GameInfo.SearchEquipEventItemTable.TryGetValue(actionPointInfo.searchAreaID, out dictionary) && dictionary.TryGetValue(actionPointInfo.gradeValue, out equipEventItemInfo))
					{
						base.LoadEventItem(equipEventItemInfo.EventItemID, itemInfo, equipEventItemInfo.ActionItemInfo);
					}
				}
				else if (instance.Map.EventItemList.TryGetValue(itemInfo.itemID, out eventItemInfo))
				{
					base.LoadEventItem(itemInfo.itemID, itemInfo, eventItemInfo);
				}
			}
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x0029A7C8 File Offset: 0x00298BC8
		public override void LoadEventParticles(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary;
			Dictionary<int, List<AnimeParticleEventInfo>> dictionary2;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentActParticleEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out dictionary2) && dictionary2 != null)
			{
				base.LoadEventParticle(dictionary2);
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06006331 RID: 25393 RVA: 0x0029A80C File Offset: 0x00298C0C
		public float MaxMotivationInMasturbation
		{
			[CompilerGenerated]
			get
			{
				return this._maxMotivationInMasturbation;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06006332 RID: 25394 RVA: 0x0029A814 File Offset: 0x00298C14
		public float RuntimeMotivationInMasturbation
		{
			[CompilerGenerated]
			get
			{
				return this._runtimeMotivationInMasturbation;
			}
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x0029A81C File Offset: 0x00298C1C
		public void StartMasturbationSequence(HScene.AnimationListInfo info)
		{
			IEnumerator enumerator = this._masturbationEnumerator = this.StartMasturbationSequenceCoroutine(info);
			this._masturbationDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x0029A864 File Offset: 0x00298C64
		public void StopMasturbationSequence()
		{
			if (this._masturbationDisposable != null)
			{
				base.ChaControl.SetClothesStateAll(0);
				this.Animation.CanUseMapIK = true;
				this.Animation.MapIK.MapIK = true;
				if (this.HParticle != null)
				{
					this.HParticle.ReleaseObject();
					this.HParticle = null;
				}
				if (this.hFlagCtrl != null)
				{
					if (this.HVoiceCtrl != null)
					{
						this.hFlagCtrl.RemoveMapHvoices(this.HVoiceID);
						this.HVoiceCtrl = null;
					}
					this.hFlagCtrl.EndProc();
					this.hFlagCtrl = null;
				}
				if (this.HItem != null)
				{
					this.HItem.ReleaseItem();
					this.HItem = null;
				}
				if (this.HItemPlace != null)
				{
					UnityEngine.Object.Destroy(this.HItemPlace);
					this.HItemPlace = null;
				}
				if (this.HLayerCtrl != null)
				{
					this.HLayerCtrl.MapHLayerRemove(this.HLayerID);
					this.HLayerCtrl = null;
				}
				if (this.HYureCtrl != null)
				{
					this.HYureCtrl.ResetShape(this.yureID);
					this.HYureCtrl.RemoveChaInit(this.yureID);
					this.HYureCtrl = null;
				}
				this._masturbationDisposable.Dispose();
				this._masturbationEnumerator = null;
				for (int i = 0; i < this.MasturbationDisposables.Count; i++)
				{
					this.MasturbationDisposables[i].Dispose();
				}
				this.MasturbationDisposables.Clear();
				if (Singleton<HSceneManager>.IsInstance())
				{
					foreach (string assetBundleName in this.hSceneManager.hashUseAssetBundle)
					{
						AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
					}
					this.hSceneManager.hashUseAssetBundle.Clear();
				}
			}
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x0029AA68 File Offset: 0x00298E68
		private IEnumerator StartMasturbationSequenceCoroutine(HScene.AnimationListInfo info)
		{
			this.Animation.CrossFadeScreen(-1f);
			Animator animator = this.Animation.Animator;
			float motivation = this.AgentData.StatsTable[5];
			this._maxMotivationInMasturbation = (this._runtimeMotivationInMasturbation = motivation);
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string speedName = definePack.AnimatorParameter.SpeedParameterName;
			this.hSceneManager = Singleton<HSceneManager>.Instance;
			this.hScene = ((!(this.hSceneManager.HSceneSet != null)) ? Singleton<Manager.Resources>.Instance.HSceneTable.HSceneSet.GetComponentInChildren<HScene>(true) : this.hSceneManager.HSceneSet.GetComponentInChildren<HScene>(true));
			byte state = (byte)info.nFemaleUpperCloths[0];
			byte state2 = (byte)info.nFemaleLowerCloths[0];
			GlobalMethod.SetAllClothState(base.ChaControl, true, (int)state, false);
			GlobalMethod.SetAllClothState(base.ChaControl, false, (int)state2, false);
			this.height = base.ChaControl.GetShapeBodyValue(0);
			this.breast = base.ChaControl.GetShapeBodyValue(1);
			base.ChaControl.setAnimatorParamFloat("height", this.height);
			base.ChaControl.setAnimatorParamFloat("breast", this.breast);
			this.Animation.CanUseMapIK = false;
			if (this.abName == null)
			{
				this.abName = CommonLib.GetAssetBundleNameListFromPath(this.hSceneManager.strAssetIKListFolder, false);
			}
			this.Animation.MapIK.MapIK = false;
			foreach (string assetBundleName in this.abName)
			{
				if (AssetBundleCheck.IsFile(assetBundleName, info.fileFemale))
				{
					this.Animation.MapIK.LoadData(assetBundleName, info.fileFemale, false);
					if (base.ChaControl.getAnimatorStateInfo(0).IsName("Idle"))
					{
						this.Animation.MapIK.Calc("Idle");
					}
					break;
				}
			}
			yield return null;
			if (this.HParticle == null)
			{
				this.HParticle = new HParticleCtrl();
				this.HParticle.particlePlace = this.hScene.hParticlePlace;
				this.HParticle.Init();
			}
			this.HParticle.RePlaceObject();
			this.HParticle.ParticleLoad(base.ChaControl.objBodyBone, 1);
			if (this.Hse == null)
			{
				this.Hse = new HSeCtrl();
			}
			this.Hse.Load(this.hSceneManager.strAssetSEListFolder, info.fileSe, null, new GameObject[]
			{
				base.ChaControl.objBodyBone
			});
			yield return null;
			if (info.isNeedItem)
			{
				if (this.HItem == null)
				{
					this.HItem = new HItemCtrl();
				}
				this.HItemPlace = new GameObject("HItemPlace");
				this.HItemPlace.transform.SetParent(base.transform, false);
				this.HItemPlace.transform.position = Vector3.zero;
				this.HItemPlace.transform.rotation = Quaternion.identity;
				this.HItem.HItemInit(this.HItemPlace.transform);
				this.HItem.LoadItem(info.ActionCtrl.Item1, info.id, null, base.ChaControl.objBodyBone, null, null);
			}
			yield return null;
			this.hFlagCtrl = this.hScene.ctrlFlag;
			this.hFlagCtrl.nowAnimationInfo = info;
			this.HVoiceID = this.hFlagCtrl.AddMapHvoices();
			this.hFlagCtrl.ctrlMapHVoice.TryGetValue(this.HVoiceID, out this.HVoiceCtrl);
			this.hFlagCtrl.MapHvoices[this.HVoiceID].voiceTrs[0] = base.ChaControl.cmpBoneBody.targetEtc.trfHeadParent;
			this.hFlagCtrl.MapHvoices[this.HVoiceID].voiceTrs[1] = null;
			this.HLayerCtrl = this.hScene.ctrlLayer;
			this.HYureCtrl = this.hScene.ctrlYures[0].CtrlMapH;
			if (this.HYureCtrl != null)
			{
				this.yureID = this.HYureCtrl.AddChaInit(base.ChaControl);
				this.HYureCtrl.AddLoadInfo(info.id, info.ActionCtrl.Item1, this.yureID);
			}
			if (this.HLayerCtrl != null)
			{
				this.HLayerID = this.HLayerCtrl.Init(base.ChaControl, this.hFlagCtrl);
				string animatorName = string.Format("{0}", info.fileFemale);
				this.HLayerCtrl.LoadExcel(animatorName, 1, 0, true, this.HLayerID);
			}
			if (this.HVoiceCtrl != null)
			{
				yield return this.HVoiceCtrl.Init(base.ChaControl.fileParam.personality, 1f, this, 0, 0f, null, -1, true, false);
				this.hFlagCtrl.MapHvoices[this.HVoiceID].playStart = 2;
				this.HVoiceCtrl.SetVoiceList(info.nAnimListInfoID, info.nBreathID, info.id, info.lstSystem);
				HVoiceCtrl hvoiceCtrl = this.HVoiceCtrl;
				ChaControl[] array = new ChaControl[2];
				array[0] = base.ChaControl;
				hvoiceCtrl.SetBreathVoiceList(array, 3, info.ActionCtrl.Item2, info.nInitiativeFemale != 0, this.hSceneManager.isForce, true);
			}
			this.MasturbationDisposables.Add(Observable.EveryUpdate().Subscribe(delegate(long _)
			{
				AnimatorStateInfo animatorStateInfo = base.ChaControl.getAnimatorStateInfo(0);
				if (this.Hse != null)
				{
					this.Hse.Proc(animatorStateInfo, base.ChaControl, 0);
				}
				if (this.HVoiceCtrl != null)
				{
					this.HVoiceCtrl.Proc(animatorStateInfo, new ChaControl[]
					{
						base.ChaControl
					});
				}
			}));
			this.MasturbationDisposables.Add(Observable.EveryLateUpdate().Subscribe(delegate(long _)
			{
				if (this.HYureCtrl != null)
				{
					this.HYureCtrl.Proc(base.ChaControl.getAnimatorStateInfo(0), this.yureID);
				}
				if (this.HLayerCtrl != null)
				{
					this.HLayerCtrl.MapHProc(this.HLayerID);
				}
			}));
			yield return null;
			foreach (AnimatorControllerParameter animatorControllerParameter in this.Animation.Parameters)
			{
				if (animatorControllerParameter.name == speedName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					animator.SetFloat(speedName, 1f);
				}
			}
			AnimatorStateInfo stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			yield return Observable.Timer(TimeSpan.FromSeconds(2.0)).ToYieldInstruction<long>();
			while (this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice)
			{
				yield return null;
			}
			this.SetPlay("WLoop", true);
			yield return null;
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			while (stateInfo.IsName("WLoop"))
			{
				if (this.GotoNextLoop(0.8f, "MLoop"))
				{
					yield return null;
					break;
				}
				stateInfo = base.ChaControl.getAnimatorStateInfo(0);
				this.Hse.Proc(stateInfo, base.ChaControl, 0);
				this.HVoiceCtrl.Proc(stateInfo, new ChaControl[]
				{
					base.ChaControl
				});
				yield return null;
			}
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			while (stateInfo.IsName("MLoop"))
			{
				if (this.GotoNextLoop(0.6f, "SLoop"))
				{
					yield return null;
					break;
				}
				stateInfo = base.ChaControl.getAnimatorStateInfo(0);
				this.Hse.Proc(stateInfo, base.ChaControl, 0);
				this.HVoiceCtrl.Proc(stateInfo, new ChaControl[]
				{
					base.ChaControl
				});
				yield return null;
			}
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			while (stateInfo.IsName("SLoop"))
			{
				if (this.GotoNextLoop(0.4f, "OLoop"))
				{
					yield return null;
					break;
				}
				stateInfo = base.ChaControl.getAnimatorStateInfo(0);
				this.Hse.Proc(stateInfo, base.ChaControl, 0);
				this.HVoiceCtrl.Proc(stateInfo, new ChaControl[]
				{
					base.ChaControl
				});
				yield return null;
			}
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			while (stateInfo.IsName("OLoop"))
			{
				float rate = this._runtimeMotivationInMasturbation / this._maxMotivationInMasturbation;
				if (rate <= 0f)
				{
					this.SetPlay("Orgasm", true);
					this.HParticle.Play(0);
					yield return null;
					break;
				}
				stateInfo = base.ChaControl.getAnimatorStateInfo(0);
				this.Hse.Proc(stateInfo, base.ChaControl, 0);
				this.HVoiceCtrl.Proc(stateInfo, new ChaControl[]
				{
					base.ChaControl
				});
				yield return null;
			}
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			while (stateInfo.IsName("Orgasm"))
			{
				if (stateInfo.normalizedTime >= 1f)
				{
					this.SetPlay("Orgasm_A", true);
					yield return null;
					break;
				}
				stateInfo = base.ChaControl.getAnimatorStateInfo(0);
				this.Hse.Proc(stateInfo, base.ChaControl, 0);
				this.HVoiceCtrl.Proc(stateInfo, new ChaControl[]
				{
					base.ChaControl
				});
				yield return null;
			}
			float timeToEnd = Mathf.Lerp(3f, 5f, UnityEngine.Random.value);
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			this.HVoiceCtrl.Proc(stateInfo, new ChaControl[]
			{
				base.ChaControl
			});
			while (this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice)
			{
				yield return null;
			}
			yield return Observable.Timer(TimeSpan.FromSeconds((double)timeToEnd)).ToYieldInstruction<long>();
			this._masturbationEnumerator = null;
			yield break;
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06006336 RID: 25398 RVA: 0x0029AA8A File Offset: 0x00298E8A
		public bool PlayingMasturbationSequence
		{
			get
			{
				return this._masturbationEnumerator != null;
			}
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x0029AA98 File Offset: 0x00298E98
		private void SetPlay(string stateName, bool isFade = true)
		{
			this.Animation.PlayAnimation(stateName, 0, 0f);
			if (this.Animation.MapIK.data != null)
			{
				this.Animation.MapIK.Calc(stateName);
			}
			base.ChaControl.setAnimatorParamFloat("height", this.height);
			base.ChaControl.setAnimatorParamFloat("breast", this.breast);
			if (isFade)
			{
				this.Animation.CrossFadeScreen(1f);
				if (stateName == "WLoop" || stateName == "SLoop" || stateName == "OLoop")
				{
					Observable.Timer(TimeSpan.FromSeconds(1.0)).Subscribe(delegate(long _)
					{
						this.HvoiceFlagSet(0);
					});
				}
			}
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x0029AB74 File Offset: 0x00298F74
		private void HvoiceFlagSet(int mode = 0)
		{
			if (mode == 0)
			{
				this.hFlagCtrl.MapHvoices[this.HVoiceID].playVoices[0] = true;
				this.hFlagCtrl.MapHvoices[this.HVoiceID].playShorts[0] = 0;
			}
			else if (mode == 1)
			{
				this.hFlagCtrl.MapHvoices[this.HVoiceLesID].playVoices[0] = true;
				this.hFlagCtrl.MapHvoices[this.HVoiceLesID].playVoices[1] = true;
				this.hFlagCtrl.MapHvoices[this.HVoiceLesID].playShorts[0] = 0;
				this.hFlagCtrl.MapHvoices[this.HVoiceLesID].playShorts[1] = 0;
			}
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x0029AC48 File Offset: 0x00299048
		private bool GotoNextLoop(float range, string nextState)
		{
			float num = this._runtimeMotivationInMasturbation / this._maxMotivationInMasturbation;
			if (num > range)
			{
				return false;
			}
			this.SetPlay(nextState, true);
			return true;
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x0600633A RID: 25402 RVA: 0x0029AC75 File Offset: 0x00299075
		public float MaxMotivationInLesbian
		{
			[CompilerGenerated]
			get
			{
				return this._maxMotivationInLesbian;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x0600633B RID: 25403 RVA: 0x0029AC7D File Offset: 0x0029907D
		public float RuntimeMotivationInLesbian
		{
			[CompilerGenerated]
			get
			{
				return this._runtimeMotivationInLesbian;
			}
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x0029AC88 File Offset: 0x00299088
		public void StartLesbianSequence(Actor partner, HScene.AnimationListInfo info)
		{
			if (partner is AgentActor)
			{
				IEnumerator enumerator = this._lesbianHEnumerator = this.StartLesbianSequenceCoroutine(partner, info);
				this._lesbianHDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
				AgentActor agentActor = partner as AgentActor;
				agentActor.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				agentActor.Mode = Desire.ActionType.Idle;
			}
			else
			{
				IEnumerator enumerator = this._lesbianHEnumerator = this.StartLesbianSequenceCoroutine(partner, info);
				this._lesbianHDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
				MerchantActor merchantActor = partner as MerchantActor;
				merchantActor.ChangeBehavior(Merchant.ActionType.HWithAgent);
			}
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x0029AD44 File Offset: 0x00299144
		public void StopLesbianSequence()
		{
			if (this._lesbianHDisposable != null)
			{
				for (int i = 0; i < this.lesChars.Length; i++)
				{
					this.lesChars[i].SetClothesStateAll(0);
				}
				for (int j = 0; j < this.lesCharAnimes.Length; j++)
				{
					if (!(this.lesCharAnimes[j] == null))
					{
						this.lesCharAnimes[j].CanUseMapIK = true;
						this.lesCharAnimes[j].MapIK.MapIK = true;
					}
				}
				if (this.HParticle != null)
				{
					this.HParticle.ReleaseObject();
					this.HParticle = null;
				}
				if (this.hFlagCtrl != null)
				{
					if (this.HVoiceCtrl != null)
					{
						this.hFlagCtrl.RemoveMapHvoices(this.HVoiceLesID);
						this.HVoiceCtrl = null;
					}
					this.hFlagCtrl.EndProc();
					this.hFlagCtrl = null;
				}
				if (this.HLayerCtrl != null)
				{
					for (int k = 0; k < 2; k++)
					{
						this.HLayerCtrl.MapHLayerRemove(this.HLayerLesID[k]);
					}
					this.HLayerCtrl = null;
				}
				for (int l = 0; l < 2; l++)
				{
					if (this.HYureCtrlLes[l] != null)
					{
						this.HYureCtrlLes[l].ResetShape(this.yureLesID[l]);
						this.HYureCtrlLes[l].RemoveChaInit(this.yureLesID[l]);
						this.HYureCtrlLes[l] = null;
					}
				}
				this._lesbianHDisposable.Dispose();
				this._lesbianHEnumerator = null;
				for (int m = 0; m < this.lesbianDisposable.Count; m++)
				{
					this.lesbianDisposable[m].Dispose();
				}
				this.lesbianDisposable.Clear();
				if (Singleton<HSceneManager>.IsInstance())
				{
					foreach (string assetBundleName in this.hSceneManager.hashUseAssetBundle)
					{
						AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
					}
					this.hSceneManager.hashUseAssetBundle.Clear();
				}
			}
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x0029AF90 File Offset: 0x00299390
		private IEnumerator StartLesbianSequenceCoroutine(Actor receiver, HScene.AnimationListInfo info)
		{
			this.lesChars[0] = receiver.ChaControl;
			this.lesChars[1] = base.ChaControl;
			this.lesCharAnimes[0] = receiver.Animation;
			this.lesCharAnimes[1] = this.Animation;
			Animator[] animator = new Animator[]
			{
				this.lesCharAnimes[0].Animator,
				this.lesCharAnimes[1].Animator
			};
			float motivation = base.GetComponent<AgentActor>().AgentData.StatsTable[5];
			this._maxMotivationInLesbian = (this._runtimeMotivationInLesbian = motivation);
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string speedName = definePack.AnimatorParameter.SpeedParameterName;
			string height = definePack.AnimatorParameter.HeightParameterName;
			string height2 = definePack.AnimatorParameter.Height1ParameterName;
			string motion = definePack.MapLesDefine.MotionParameterName;
			float changeLoopTime = definePack.MapLesDefine.LoopChangeTime;
			float[] breast = new float[]
			{
				-1f,
				-1f
			};
			this.hSceneManager = Singleton<HSceneManager>.Instance;
			this.hScene = ((!(this.hSceneManager.HSceneSet != null)) ? Singleton<Manager.Resources>.Instance.HSceneTable.HSceneSet.GetComponentInChildren<HScene>(true) : this.hSceneManager.HSceneSet.GetComponentInChildren<HScene>(true));
			for (int i = 0; i < this.lesChars.Length; i++)
			{
				byte state = (byte)info.nFemaleUpperCloths[i];
				byte state2 = (byte)info.nFemaleLowerCloths[i];
				GlobalMethod.SetAllClothState(this.lesChars[i], true, (int)state, false);
				GlobalMethod.SetAllClothState(this.lesChars[i], false, (int)state2, false);
			}
			this.lesCharAnimes[0].CanUseMapIK = false;
			if (this.abName == null)
			{
				this.abName = CommonLib.GetAssetBundleNameListFromPath(this.hSceneManager.strAssetIKListFolder, false);
			}
			this.lesCharAnimes[0].MapIK.MapIK = false;
			foreach (string assetBundleName in this.abName)
			{
				if (AssetBundleCheck.IsFile(assetBundleName, info.fileFemale2))
				{
					this.lesCharAnimes[0].MapIK.LoadData(assetBundleName, info.fileFemale2, false);
					this.lesCharAnimes[0].MapIK.SetPartners(new MotionIK[]
					{
						this.lesCharAnimes[0].MapIK,
						this.lesCharAnimes[1].MapIK
					});
					if (this.lesChars[0].getAnimatorStateInfo(0).IsName("Idle"))
					{
						this.lesCharAnimes[0].MapIK.Calc("Idle");
					}
					break;
				}
			}
			this.lesCharAnimes[1].CanUseMapIK = false;
			if (this.abName == null)
			{
				this.abName = CommonLib.GetAssetBundleNameListFromPath(this.hSceneManager.strAssetIKListFolder, false);
			}
			this.lesCharAnimes[1].MapIK.MapIK = false;
			foreach (string assetBundleName2 in this.abName)
			{
				if (AssetBundleCheck.IsFile(assetBundleName2, info.fileFemale))
				{
					this.lesCharAnimes[1].MapIK.LoadData(assetBundleName2, info.fileFemale, false);
					this.lesCharAnimes[1].MapIK.SetPartners(new MotionIK[]
					{
						this.lesCharAnimes[1].MapIK,
						this.lesCharAnimes[0].MapIK
					});
					if (this.lesChars[1].getAnimatorStateInfo(0).IsName("Idle"))
					{
						this.lesCharAnimes[1].MapIK.Calc("Idle");
					}
					break;
				}
			}
			yield return null;
			if (this.HParticle == null)
			{
				this.HParticle = new HParticleCtrl();
				this.HParticle.particlePlace = this.hScene.hParticlePlace;
				this.HParticle.Init();
			}
			this.HParticle.RePlaceObject();
			this.HParticle.ParticleLoad(receiver.ChaControl.objBodyBone, 1);
			this.HParticle.ParticleLoad(base.ChaControl.objBodyBone, 3);
			if (this.Hse == null)
			{
				this.Hse = new HSeCtrl();
			}
			GameObject[] objBoneFemale = new GameObject[]
			{
				this.lesChars[0].objBodyBone,
				this.lesChars[1].objBodyBone
			};
			this.Hse.Load(this.hSceneManager.strAssetSEListFolder, info.fileSe, null, objBoneFemale);
			yield return null;
			if (this.hFlagCtrl == null)
			{
				this.hFlagCtrl = this.hScene.ctrlFlag;
			}
			this.hFlagCtrl.nowAnimationInfo = info;
			this.HVoiceLesID = this.hFlagCtrl.AddMapHvoices();
			this.hFlagCtrl.ctrlMapHVoice.TryGetValue(this.HVoiceLesID, out this.HVoiceCtrl);
			this.hFlagCtrl.MapHvoices[this.HVoiceLesID].voiceTrs[0] = receiver.ChaControl.cmpBoneBody.targetEtc.trfHeadParent;
			this.hFlagCtrl.MapHvoices[this.HVoiceLesID].voiceTrs[1] = base.ChaControl.cmpBoneBody.targetEtc.trfHeadParent;
			if (this.HVoiceCtrl != null)
			{
				if (receiver.GetComponent<MerchantActor>() == null)
				{
					yield return this.HVoiceCtrl.Init(this.lesChars[0].fileParam.personality, this.lesChars[0].fileParam.voicePitch, receiver, this.lesChars[1].fileParam.personality, this.lesChars[1].fileParam.voicePitch, this, -1, false, true);
				}
				else
				{
					yield return this.HVoiceCtrl.Init(-90, this.lesChars[0].fileParam.voicePitch, receiver, this.lesChars[1].fileParam.personality, this.lesChars[1].fileParam.voicePitch, this, -1, false, true);
				}
				this.hFlagCtrl.MapHvoices[this.HVoiceLesID].playStart = 2;
				this.HVoiceCtrl.SetVoiceList(info.nAnimListInfoID, info.nBreathID, info.id, info.lstSystem);
				this.HVoiceCtrl.SetBreathVoiceList(this.lesChars, info.ActionCtrl.Item1, info.ActionCtrl.Item2, info.nInitiativeFemale != 0, this.hSceneManager.isForce, true);
			}
			this.HLayerCtrl = this.hScene.ctrlLayer;
			this.HYureCtrlLes[0] = this.hScene.ctrlYures[0].CtrlMapH;
			this.HYureCtrlLes[1] = this.hScene.ctrlYures[1].CtrlMapH;
			for (int j = 0; j < 2; j++)
			{
				if (this.HYureCtrlLes[j] != null)
				{
					this.yureLesID[j] = this.HYureCtrlLes[j].AddChaInit(this.lesChars[j]);
					this.HYureCtrlLes[j].AddLoadInfo(info.id, info.ActionCtrl.Item1, this.yureLesID[j]);
				}
			}
			if (this.HLayerCtrl != null)
			{
				for (int k = 0; k < 2; k++)
				{
					string animatorName;
					if (k == 0)
					{
						animatorName = info.fileFemale;
					}
					else
					{
						animatorName = info.fileFemale2;
					}
					this.HLayerLesID[k] = this.HLayerCtrl.Init(this.lesChars[k], this.hFlagCtrl);
					this.HLayerCtrl.LoadExcel(animatorName, 1, 0, true, this.HLayerLesID[k]);
				}
			}
			this.lesbianDisposable.Add(Observable.EveryUpdate().Subscribe(delegate(long _)
			{
				AnimatorStateInfo animatorStateInfo = this.ChaControl.getAnimatorStateInfo(0);
				if (this.Hse != null)
				{
					this.Hse.Proc(animatorStateInfo, this.lesChars);
				}
				if (this.HVoiceCtrl != null)
				{
					this.HVoiceCtrl.Proc(animatorStateInfo, this.lesChars);
				}
				for (int num3 = 0; num3 < this.lesCharAnimes.Length; num3++)
				{
					foreach (AnimatorControllerParameter animatorControllerParameter2 in this.lesCharAnimes[num3].Parameters)
					{
						if (animatorControllerParameter2.name == motion && animatorControllerParameter2.type == AnimatorControllerParameterType.Float)
						{
							animator[num3].SetFloat(motion, this.hFlagCtrl.motions[num3]);
						}
					}
				}
				for (int num5 = 0; num5 < this.lesCharAnimes.Length; num5++)
				{
					if (this.enableMotions[num5])
					{
						this.timeMotions[num5] = Mathf.Clamp(this.timeMotions[num5] + Time.deltaTime, 0f, this.lerpTimes[num5]);
						float num6 = Mathf.Clamp01(this.timeMotions[num5] / this.lerpTimes[num5]);
						num6 = this.hFlagCtrl.changeMotionCurve.Evaluate(num6);
						this.hFlagCtrl.motions[num5] = Mathf.Lerp(this.lerpMotions[num5].x, this.lerpMotions[num5].y, num6);
						if (num6 >= 1f)
						{
							this.enableMotions[num5] = false;
						}
					}
				}
			}));
			this.lesbianDisposable.Add(Observable.EveryLateUpdate().Subscribe(delegate(long _)
			{
				AnimatorStateInfo animatorStateInfo = this.lesChars[1].getAnimatorStateInfo(0);
				if (this.HYureCtrlLes[0] != null)
				{
					this.HYureCtrlLes[0].Proc(animatorStateInfo, this.yureLesID[0]);
				}
				animatorStateInfo = this.lesChars[0].getAnimatorStateInfo(0);
				if (this.HYureCtrlLes[1] != null)
				{
					this.HYureCtrlLes[1].Proc(animatorStateInfo, this.yureLesID[1]);
				}
				if (this.HLayerCtrl != null)
				{
					this.HLayerCtrl.MapHProc(this.HLayerLesID[0]);
					this.HLayerCtrl.MapHProc(this.HLayerLesID[1]);
				}
			}));
			for (int l = 0; l < this.lesCharAnimes.Length; l++)
			{
				foreach (AnimatorControllerParameter animatorControllerParameter in this.lesCharAnimes[l].Parameters)
				{
					if (animatorControllerParameter.name == speedName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
					{
						animator[l].SetFloat(speedName, 1f);
					}
					else if (animatorControllerParameter.name == height && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
					{
						animator[l].SetFloat(height, this.lesChars[l].GetShapeBodyValue(0));
					}
					else if (animatorControllerParameter.name == height2 && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
					{
						animator[l].SetFloat(height2, this.lesChars[l ^ 1].GetShapeBodyValue(0));
					}
				}
			}
			for (int n = 0; n < this.lesChars.Length; n++)
			{
				breast[n] = this.lesChars[n].GetShapeBodyValue(1);
				this.lesChars[n].setAnimatorParamFloat("breast", breast[n]);
			}
			AnimatorStateInfo stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			yield return Observable.Timer(TimeSpan.FromSeconds(2.0)).ToYieldInstruction<long>();
			while (this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice)
			{
				yield return null;
			}
			this.SetPlayLes("WLoop", true);
			float loopTime = 0f;
			yield return null;
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			bool isWLoop = stateInfo.IsName("WLoop");
			bool isSLoop = stateInfo.IsName("SLoop");
			while (isWLoop || isSLoop)
			{
				if (this.GotoNextLoopLes(0.4f, "OLoop"))
				{
					yield return null;
					break;
				}
				loopTime += Time.deltaTime;
				if (loopTime > changeLoopTime)
				{
					if (isWLoop)
					{
						this.SetPlayLes("SLoop", true);
					}
					else if (isSLoop)
					{
						this.SetPlayLes("WLoop", true);
					}
					loopTime = 0f;
				}
				stateInfo = base.ChaControl.getAnimatorStateInfo(0);
				isWLoop = stateInfo.IsName("WLoop");
				isSLoop = stateInfo.IsName("SLoop");
				for (int num = 0; num < this.lesCharAnimes.Length; num++)
				{
					this.timeChangeMotionDeltaTimes[num] += Time.deltaTime;
					if (this.timeChangeMotions[num] <= this.timeChangeMotionDeltaTimes[num] && !this.enableMotions[num])
					{
						this.timeChangeMotions[num] = UnityEngine.Random.Range(this.hFlagCtrl.changeAutoMotionTimeMin, this.hFlagCtrl.changeAutoMotionTimeMax);
						this.timeChangeMotionDeltaTimes[num] = 0f;
						this.enableMotions[num] = true;
						this.timeMotions[num] = 0f;
						float num2;
						if (this.allowMotions[num])
						{
							num2 = 1f - this.hFlagCtrl.motions[num];
							if (num2 <= this.hFlagCtrl.changeMotionMinRate)
							{
								num2 = 1f;
							}
							else
							{
								num2 = this.hFlagCtrl.motions[num] + UnityEngine.Random.Range(this.hFlagCtrl.changeMotionMinRate, num2);
							}
							if (num2 >= 1f)
							{
								this.allowMotions[num] = false;
							}
						}
						else
						{
							num2 = this.hFlagCtrl.motions[num];
							if (num2 <= this.hFlagCtrl.changeMotionMinRate)
							{
								num2 = 0f;
							}
							else
							{
								num2 = this.hFlagCtrl.motions[num] - UnityEngine.Random.Range(this.hFlagCtrl.changeMotionMinRate, num2);
							}
							if (num2 <= 0f)
							{
								this.allowMotions[num] = true;
							}
						}
						this.lerpMotions[num] = new Vector2(this.hFlagCtrl.motions[num], num2);
						this.lerpTimes[num] = UnityEngine.Random.Range(this.hFlagCtrl.changeMotionTimeMin, this.hFlagCtrl.changeMotionTimeMax);
					}
				}
				yield return null;
			}
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			while (stateInfo.IsName("OLoop"))
			{
				float rate = this._runtimeMotivationInLesbian / this._maxMotivationInLesbian;
				if (rate <= 0f)
				{
					this.SetPlayLes("Orgasm", true);
					if (this.GetFlavorSkill(FlavorSkill.Type.Dirty) >= 100 && info.id == 0)
					{
						this.HParticle.Play(4);
					}
					AgentActor partner = receiver.GetComponent<AgentActor>();
					if (partner != null && partner.GetFlavorSkill(FlavorSkill.Type.Dirty) >= 100)
					{
						this.HParticle.Play(0);
					}
					yield return null;
					break;
				}
				yield return null;
			}
			stateInfo = base.ChaControl.getAnimatorStateInfo(0);
			this.Hse.Proc(stateInfo, base.ChaControl, 0);
			while (stateInfo.IsName("Orgasm"))
			{
				if (stateInfo.normalizedTime >= 1f)
				{
					this.SetPlayLes("Orgasm_A", true);
					yield return null;
					break;
				}
				stateInfo = base.ChaControl.getAnimatorStateInfo(0);
				yield return null;
			}
			float timeToEnd = Mathf.Lerp(3f, 5f, UnityEngine.Random.value);
			yield return Observable.Timer(TimeSpan.FromSeconds((double)timeToEnd)).ToYieldInstruction<long>();
			while (this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice || this.HVoiceCtrl.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice)
			{
				yield return null;
			}
			this._lesbianHEnumerator = null;
			yield break;
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x0600633F RID: 25407 RVA: 0x0029AFB9 File Offset: 0x002993B9
		public bool LivesLesbianHSequence
		{
			get
			{
				return this._lesbianHEnumerator != null;
			}
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0029AFC8 File Offset: 0x002993C8
		private void SetPlayLes(string stateName, bool isFade = true)
		{
			for (int i = 0; i < this.lesCharAnimes.Length; i++)
			{
				this.lesCharAnimes[i].PlayAnimation(stateName, 0, 0f);
				if (this.lesCharAnimes[i].MapIK.data != null)
				{
					this.lesCharAnimes[i].MapIK.Calc(stateName);
				}
			}
			if (isFade)
			{
				this.Animation.CrossFadeScreen(1f);
				if (stateName == "WLoop" || stateName == "SLoop" || stateName == "OLoop")
				{
					Observable.Timer(TimeSpan.FromSeconds(1.0)).Subscribe(delegate(long _)
					{
						this.HvoiceFlagSet(1);
					});
				}
			}
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x0029B098 File Offset: 0x00299498
		private bool GotoNextLoopLes(float range, string nextState)
		{
			float num = this._runtimeMotivationInLesbian / this._maxMotivationInLesbian;
			if (num > range)
			{
				return false;
			}
			this.SetPlayLes(nextState, true);
			return true;
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x0029B0C8 File Offset: 0x002994C8
		public override Actor.SearchInfo RandomAddItem(Dictionary<int, ItemTableElement> itemTable, bool containsNone)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			if (itemTable == null)
			{
				return new Actor.SearchInfo
				{
					IsSuccess = false
				};
			}
			StatusProfile statusProfile = instance.StatusProfile;
			int flavor = base.ChaControl.fileGameInfo.flavorState[3];
			Manager.Resources.GameInfoTables gameInfo = instance.GameInfo;
			int key = base.Lottery(itemTable);
			ItemTableElement itemTableElement;
			if (itemTable.TryGetValue(key, out itemTableElement))
			{
				Actor.SearchInfo searchInfo = new Actor.SearchInfo
				{
					IsSuccess = true
				};
				foreach (ItemTableElement.GatherElement gatherElement in itemTableElement.Elements)
				{
					float value = UnityEngine.Random.value;
					StuffItemInfo item = gameInfo.GetItem(gatherElement.categoryID, gatherElement.itemID);
					float num = gatherElement.prob;
					if (item != null)
					{
						if (item.Grade == Grade._3)
						{
							float num2 = AgentActor.FlavorVariation(statusProfile.DropBuffMinMax, statusProfile.DropBuff, flavor);
							num += num2;
							if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(44))
							{
								num += 10f;
							}
						}
						if (value < num)
						{
							int num3 = UnityEngine.Random.Range(gatherElement.minCount, gatherElement.maxCount + 1);
							if (num3 > 0)
							{
								searchInfo.ItemList.Add(new Actor.ItemSearchInfo
								{
									name = item.Name,
									categoryID = gatherElement.categoryID,
									id = gatherElement.itemID,
									count = num3
								});
							}
						}
					}
				}
				if (searchInfo.ItemList.IsNullOrEmpty<Actor.ItemSearchInfo>())
				{
					if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(49))
					{
						List<ItemTableElement.GatherElement> list = ListPool<ItemTableElement.GatherElement>.Get();
						foreach (ItemTableElement.GatherElement item2 in itemTableElement.Elements)
						{
							if (item2.categoryID == 16 || item2.categoryID == 17)
							{
								list.Add(item2);
							}
						}
						if (!list.IsNullOrEmpty<ItemTableElement.GatherElement>())
						{
							ItemTableElement.GatherElement gatherElement2 = base.Lottery(list);
							StuffItemInfo item3 = gameInfo.GetItem(gatherElement2.categoryID, gatherElement2.itemID);
							if (item3 != null)
							{
								int count = UnityEngine.Random.Range(Mathf.Max(gatherElement2.minCount, 1), gatherElement2.maxCount + 1);
								searchInfo.ItemList.Add(new Actor.ItemSearchInfo
								{
									name = item3.Name,
									categoryID = gatherElement2.categoryID,
									id = gatherElement2.itemID,
									count = count
								});
							}
							else
							{
								searchInfo.IsSuccess = false;
							}
						}
						else
						{
							searchInfo.IsSuccess = false;
						}
						ListPool<ItemTableElement.GatherElement>.Release(list);
					}
					else
					{
						searchInfo.IsSuccess = false;
					}
				}
				return searchInfo;
			}
			return new Actor.SearchInfo
			{
				IsSuccess = false
			};
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x0029B40C File Offset: 0x0029980C
		public override void OnDayUpdated(TimeSpan timeSpan)
		{
			this.SetAdvEventLimitationResetReserve();
			this.SetGreetFlagResetReserve();
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x0029B41C File Offset: 0x0029981C
		public void SetAdvEventLimitationResetReserve()
		{
			if (this.advEventResetDisposable != null)
			{
				this.advEventResetDisposable.Dispose();
			}
			this.advEventResetDisposable = Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).Subscribe(delegate(long _)
			{
				if (Singleton<Manager.Map>.Instance.IsHour7After)
				{
					this.AgentData.advEventLimitation.Remove(6);
					this.AgentData.AppendEventLimitation.Clear();
					if (this.advEventResetDisposable != null)
					{
						this.advEventResetDisposable.Dispose();
					}
					this.advEventResetDisposable = null;
				}
			});
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x0029B468 File Offset: 0x00299868
		public void SetGreetFlagResetReserve()
		{
			if (this._greetEventResetDisposable != null)
			{
				this._greetEventResetDisposable.Dispose();
			}
			this._greetEventResetDisposable = Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).Subscribe(delegate(long _)
			{
				if (Singleton<Manager.Map>.Instance.IsHour7After)
				{
					this.AgentData.Greeted = false;
					this.AgentData.YandereWarpLimitation = false;
					if (this._greetEventResetDisposable != null)
					{
						this._greetEventResetDisposable.Dispose();
					}
					this._greetEventResetDisposable = null;
				}
			});
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x0029B4B4 File Offset: 0x002998B4
		public void YandereWarpRetryReserve()
		{
			if (this._yandereWarpRetryDisposable != null)
			{
				this._yandereWarpRetryDisposable.Dispose();
			}
			this._yandereWarpRetryDisposable = Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).Subscribe(delegate(long _)
			{
				this.AgentData.YandereWarpRetryTimer += Time.deltaTime;
				if (this.AgentData.YandereWarpRetryTimer < Singleton<Manager.Resources>.Instance.AgentProfile.YandereWarpRetryReserveTime)
				{
					return;
				}
				this.AgentData.YandereWarpLimitation = false;
				this.AgentData.YandereWarpRetry = false;
				if (this._yandereWarpRetryDisposable != null)
				{
					this._yandereWarpRetryDisposable.Dispose();
				}
				this._yandereWarpRetryDisposable = null;
			});
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x0029B500 File Offset: 0x00299900
		public Waypoint[] GetReservedWaypoints()
		{
			return this._reservedWaypoints.ToArray();
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06006348 RID: 25416 RVA: 0x0029B50D File Offset: 0x0029990D
		// (set) Token: 0x06006349 RID: 25417 RVA: 0x0029B515 File Offset: 0x00299915
		public Queue<Waypoint> WalkRoute { get; private set; } = new Queue<Waypoint>();

		// Token: 0x0600634A RID: 25418 RVA: 0x0029B51E File Offset: 0x0029991E
		public Waypoint[] GetReservedNearActionWaypoints()
		{
			return this._reservedNearActionWaypoints.ToArray();
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x0029B52B File Offset: 0x0029992B
		public void ClearReservedNearActionWaypoints()
		{
			this._reservedNearActionWaypoints.Clear();
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x0600634C RID: 25420 RVA: 0x0029B538 File Offset: 0x00299938
		// (set) Token: 0x0600634D RID: 25421 RVA: 0x0029B540 File Offset: 0x00299940
		public Queue<Waypoint> SearchActionRoute { get; private set; } = new Queue<Waypoint>();

		// Token: 0x0600634E RID: 25422 RVA: 0x0029B549 File Offset: 0x00299949
		public Waypoint[] GetReservedDateNearActionWaypoints()
		{
			return this._reservedDateNearActionWaypoints.ToArray();
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x0600634F RID: 25423 RVA: 0x0029B556 File Offset: 0x00299956
		// (set) Token: 0x06006350 RID: 25424 RVA: 0x0029B55E File Offset: 0x0029995E
		public Queue<Waypoint> SearchDateActionRoute { get; private set; } = new Queue<Waypoint>();

		// Token: 0x06006351 RID: 25425 RVA: 0x0029B567 File Offset: 0x00299967
		public Waypoint[] GetReservedLocationWaypoints()
		{
			return this._reservedLocationWaypoints.ToArray();
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06006352 RID: 25426 RVA: 0x0029B574 File Offset: 0x00299974
		// (set) Token: 0x06006353 RID: 25427 RVA: 0x0029B57C File Offset: 0x0029997C
		public Queue<Waypoint> SearchLocationRoute { get; private set; } = new Queue<Waypoint>();

		// Token: 0x06006354 RID: 25428 RVA: 0x0029B585 File Offset: 0x00299985
		public Waypoint[] GetReservedActorWaypoints()
		{
			return this._reservedActorWaypoints.ToArray();
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06006355 RID: 25429 RVA: 0x0029B592 File Offset: 0x00299992
		// (set) Token: 0x06006356 RID: 25430 RVA: 0x0029B59A File Offset: 0x0029999A
		public Queue<Waypoint> SearchActorRoute { get; private set; } = new Queue<Waypoint>();

		// Token: 0x06006357 RID: 25431 RVA: 0x0029B5A3 File Offset: 0x002999A3
		public Waypoint[] GetReservedPlayerWaypoints()
		{
			return this._reservedPlayerWaypoints.ToArray();
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06006358 RID: 25432 RVA: 0x0029B5B0 File Offset: 0x002999B0
		// (set) Token: 0x06006359 RID: 25433 RVA: 0x0029B5B8 File Offset: 0x002999B8
		public Queue<Waypoint> SearchPlayerRoute { get; private set; } = new Queue<Waypoint>();

		// Token: 0x0600635A RID: 25434 RVA: 0x0029B5C1 File Offset: 0x002999C1
		public Waypoint[] GetReservedAnimalWaypoints()
		{
			return this._reservedAnimalWaypoints.ToArray();
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x0600635B RID: 25435 RVA: 0x0029B5CE File Offset: 0x002999CE
		// (set) Token: 0x0600635C RID: 25436 RVA: 0x0029B5D6 File Offset: 0x002999D6
		public Queue<Waypoint> SearchAnimalRoute { get; private set; } = new Queue<Waypoint>();

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x0600635D RID: 25437 RVA: 0x0029B5DF File Offset: 0x002999DF
		// (set) Token: 0x0600635E RID: 25438 RVA: 0x0029B5E7 File Offset: 0x002999E7
		public Waypoint DestWaypoint { get; private set; }

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x0600635F RID: 25439 RVA: 0x0029B5F0 File Offset: 0x002999F0
		// (set) Token: 0x06006360 RID: 25440 RVA: 0x0029B5F8 File Offset: 0x002999F8
		public bool IsRunning { get; set; }

		// Token: 0x06006361 RID: 25441 RVA: 0x0029B604 File Offset: 0x00299A04
		private bool IsTraverseOffMeshLink()
		{
			if (base.NavMeshAgent.isOnOffMeshLink)
			{
				return true;
			}
			if (base.NavMeshAgent.currentOffMeshLinkData.valid)
			{
				return true;
			}
			if (base.NavMeshAgent.currentOffMeshLinkData.offMeshLink != null)
			{
				ActionPoint component = base.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<ActionPoint>();
				if (!component.IsNeutralCommand)
				{
					return true;
				}
			}
			return base.EventKey == EventType.Move;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x0029B694 File Offset: 0x00299A94
		public void SetOriginalDestination()
		{
			if (this.DestWaypoint == null)
			{
				return;
			}
			this.SetDestinationByDirectPathForce(this.DestWaypoint);
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x0029B6B8 File Offset: 0x00299AB8
		private bool AnyWaypoint(List<Waypoint> list, Waypoint pt)
		{
			foreach (Waypoint x in list)
			{
				if (x == pt)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x0029B720 File Offset: 0x00299B20
		public void LoadWaypoints()
		{
			Chunk chunk = null;
			foreach (KeyValuePair<int, Chunk> keyValuePair in Singleton<Manager.Map>.Instance.ChunkTable)
			{
				if (keyValuePair.Value.ID == this.AgentData.ChunkID)
				{
					chunk = keyValuePair.Value;
					break;
				}
			}
			foreach (int num in this.AgentData.ReservedWaypointIDList)
			{
				Waypoint waypoint = null;
				foreach (MapArea mapArea in chunk.MapAreas)
				{
					foreach (Waypoint waypoint2 in mapArea.Waypoints)
					{
						if (waypoint2.ID == num)
						{
							waypoint = waypoint2;
							break;
						}
					}
					if (waypoint != null)
					{
						break;
					}
				}
				if (waypoint != null)
				{
					this._reservedWaypoints.Add(waypoint);
				}
			}
			foreach (int num2 in this.AgentData.WalkRouteWaypointIDList)
			{
				Waypoint waypoint3 = null;
				foreach (MapArea mapArea2 in chunk.MapAreas)
				{
					foreach (Waypoint waypoint4 in mapArea2.Waypoints)
					{
						if (waypoint4.ID == num2)
						{
							waypoint3 = waypoint4;
							break;
						}
					}
					if (waypoint3 != null)
					{
						break;
					}
				}
				if (waypoint3 != null)
				{
					this._reservedWaypoints.Add(waypoint3);
				}
			}
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x0029B9AC File Offset: 0x00299DAC
		public void AbortCalc()
		{
			if (this._calcEnumerator != null)
			{
				base.StopCoroutine(this._calcEnumerator);
				this._calcEnumerator = null;
			}
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x0029B9CC File Offset: 0x00299DCC
		private bool ValidatePoints(Waypoint point, int chunkID, Weather weather, bool checkWeather = true, bool checkChunk = true)
		{
			if (point == null || point.OwnerArea == null)
			{
				return false;
			}
			if (checkWeather && !this.WeatherCheck(weather, point))
			{
				return false;
			}
			if (checkChunk)
			{
				MapArea ownerArea = point.OwnerArea;
				if (ownerArea.ChunkID != chunkID)
				{
					return false;
				}
			}
			return point.Reserver == null || point.Reserver == this;
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x0029BA44 File Offset: 0x00299E44
		private bool AvailableAreaCheck(MapArea area, Dictionary<int, bool> availableArea, Manager.Map mapManager)
		{
			if (area != null && area.ChunkID != base.ChunkID)
			{
				bool result;
				if (!availableArea.TryGetValue(area.AreaID, out result))
				{
					result = (availableArea[area.AreaID] = mapManager.CheckAvailableMapArea(area.AreaID));
				}
				return result;
			}
			return true;
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x0029BAA8 File Offset: 0x00299EA8
		public IEnumerator CalculateWaypoints()
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			this._reservedWaypoints.Clear();
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				bool checkWeather = waypoints.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, chunkID, weather, checkWeather, true))
					{
						elements.Add(waypoint);
					}
				}
			}
			bool existsHousingPointsSameChunk = false;
			if (!Singleton<Manager.Map>.Instance.PointAgent.AppendActionPoints.IsNullOrEmpty<ActionPoint>())
			{
				foreach (ActionPoint actionPoint in Singleton<Manager.Map>.Instance.PointAgent.AppendActionPoints)
				{
					if (!(actionPoint == null) && !(actionPoint.OwnerArea == null))
					{
						if (actionPoint.OwnerArea.ChunkID == chunkID)
						{
							existsHousingPointsSameChunk = true;
							break;
						}
					}
				}
			}
			if (Singleton<Manager.Map>.Instance.MapID > 0 || existsHousingPointsSameChunk)
			{
				foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
					{
						bool checkWeather2 = keyValuePair.Value.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
						foreach (Waypoint waypoint2 in keyValuePair.Value)
						{
							if (this.ValidatePoints(waypoint2, chunkID, weather, checkWeather2, true))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			yield return this.CalculatePath(elements, Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.reservedPathCount);
			ListPool<Waypoint>.Release(elements);
			this._calcEnumerator = null;
			yield break;
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x0029BAC4 File Offset: 0x00299EC4
		private IEnumerator CalculatePath(List<Waypoint> points, int count)
		{
			List<Waypoint> routes = ListPool<Waypoint>.Get();
			if (points.IsNullOrEmpty<Waypoint>())
			{
				this._reservedWaypoints.Clear();
				yield return AgentActor._waitMinute;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.CalculatePath(points, routes);
				}
				foreach (Waypoint item in routes)
				{
					this._reservedWaypoints.Add(item);
				}
			}
			ListPool<Waypoint>.Release(routes);
			yield break;
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x0029BAF0 File Offset: 0x00299EF0
		private IEnumerator CalculatePath(List<Waypoint> points, List<Waypoint> list)
		{
			if (this._calcElementPath == null)
			{
				this._calcElementPath = new NavMeshPath();
			}
			if (points.IsNullOrEmpty<Waypoint>())
			{
				yield break;
			}
			Waypoint pt = points.GetElement(UnityEngine.Random.Range(0, points.Count));
			if (pt == null)
			{
				yield break;
			}
			int prevCount = list.Count;
			if (!this.AnyWaypoint(list, pt) && pt.Reserver == null)
			{
				float waypointTweenMinDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.WaypointTweenMinDistance;
				if (list.IsNullOrEmpty<Waypoint>())
				{
					if (Vector3.Distance(pt.transform.position, base.Position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
				else
				{
					Waypoint waypoint = list[list.Count - 1];
					if (Vector3.Distance(pt.transform.position, waypoint.transform.position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
			}
			points.Remove(pt);
			if (prevCount == list.Count)
			{
				yield return this.CalculatePath(points, list);
			}
			yield break;
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x0029BB1C File Offset: 0x00299F1C
		public IEnumerator CalculateNearActionWaypoints()
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			int areaID = (!(base.MapArea != null)) ? this.AgentData.AreaID : base.MapArea.AreaID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, chunkID, weather, true, true))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, chunkID, weather, true, true))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			int pathCount = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.reservedPathCount;
			ActionPoint[] actionPoints = Singleton<Manager.Map>.Instance.PointAgent.ActionPoints;
			List<ActionPoint> apElements = ListPool<ActionPoint>.Get();
			Desire.Type requestedDesire = this.RequestedDesire;
			EventType type = (EventType)0;
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (valueTuple.Item2 == requestedDesire)
				{
					type = valueTuple.Item1;
					break;
				}
			}
			foreach (ActionPoint actionPoint in actionPoints)
			{
				if (this.WeatherCheck(weather, actionPoint))
				{
					MapArea ownerArea3 = actionPoint.OwnerArea;
					if (ownerArea3 != null && ownerArea3.AreaID != areaID)
					{
						bool flag;
						if (!availableArea.TryGetValue(ownerArea3.AreaID, out flag))
						{
							flag = (availableArea[ownerArea3.AreaID] = mapManager.CheckAvailableMapArea(ownerArea3.AreaID));
						}
						if (!flag)
						{
							goto IL_4A9;
						}
					}
					if (type == EventType.Search)
					{
						if (!(actionPoint is SearchActionPoint))
						{
							goto IL_4A9;
						}
						SearchActionPoint searchActionPoint = actionPoint as SearchActionPoint;
						int tableID = searchActionPoint.TableID;
						int searchAreaID = this.SearchAreaID;
						if (searchAreaID != 0)
						{
							if (this.SearchAreaID != tableID)
							{
								goto IL_4A9;
							}
						}
						else if (tableID != 0 && tableID != 1 && tableID != 2)
						{
							goto IL_4A9;
						}
						if (this.SearchAreaID != searchActionPoint.TableID)
						{
							goto IL_4A9;
						}
					}
					if (!apElements.Contains(actionPoint))
					{
						if (actionPoint.AgentEventType.Contains(type))
						{
							apElements.Add(actionPoint);
						}
					}
				}
				IL_4A9:;
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateNearActionPath(elements, apElements, pathCount);
			ListPool<Waypoint>.Release(elements);
			ListPool<ActionPoint>.Release(apElements);
			this._actionCalcEnumerator = null;
			yield break;
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x0029BB38 File Offset: 0x00299F38
		public IEnumerator CalculateNearActionWaypointsAdditive(int count)
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			float value = UnityEngine.Random.value;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, chunkID, weather, true, true))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, chunkID, weather, true, true))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			ActionPoint[] actionPoints = Singleton<Manager.Map>.Instance.PointAgent.ActionPoints;
			List<ActionPoint> apElements = ListPool<ActionPoint>.Get();
			Desire.Type requestedDesire = this.RequestedDesire;
			EventType type = (EventType)0;
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (valueTuple.Item2 == requestedDesire)
				{
					type = valueTuple.Item1;
					break;
				}
			}
			foreach (ActionPoint actionPoint in actionPoints)
			{
				if (this.WeatherCheck(weather, actionPoint))
				{
					MapArea ownerArea3 = actionPoint.OwnerArea;
					if (this.AvailableAreaCheck(ownerArea3, availableArea, mapManager))
					{
						if (!apElements.Contains(actionPoint))
						{
							if (actionPoint.AgentEventType.Contains(type))
							{
								apElements.Add(actionPoint);
							}
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateNearActionPath(elements, apElements, count);
			ListPool<Waypoint>.Release(elements);
			ListPool<ActionPoint>.Release(apElements);
			this._additiveActionCalcProcess = null;
			yield break;
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x0029BB5C File Offset: 0x00299F5C
		private IEnumerator CalculateNearActionPath(List<Waypoint> points, List<ActionPoint> actionPoints, int count)
		{
			List<Waypoint> routes = ListPool<Waypoint>.Get();
			if (points.IsNullOrEmpty<Waypoint>())
			{
				this._reservedNearActionWaypoints.Clear();
				yield return AgentActor._waitMinute;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.CalculateNearActionPath(points, actionPoints, routes);
				}
				foreach (Waypoint item in routes)
				{
					this._reservedNearActionWaypoints.Add(item);
				}
			}
			ListPool<Waypoint>.Release(routes);
			yield break;
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x0029BB8C File Offset: 0x00299F8C
		private IEnumerator CalculateNearActionPath(List<Waypoint> points, List<ActionPoint> actionPoints, List<Waypoint> list)
		{
			if (this._calcElementPath == null)
			{
				this._calcElementPath = new NavMeshPath();
			}
			if (actionPoints.IsNullOrEmpty<ActionPoint>())
			{
				yield break;
			}
			if (points.IsNullOrEmpty<Waypoint>())
			{
				yield break;
			}
			ActionPoint apt = actionPoints.GetElement(UnityEngine.Random.Range(0, actionPoints.Count));
			if (apt == null)
			{
				yield break;
			}
			float distance = Singleton<Manager.Resources>.Instance.AgentProfile.ActionPointNearDistance;
			List<Waypoint> aroundWaypoints = ListPool<Waypoint>.Get();
			foreach (Waypoint waypoint in points)
			{
				if (Vector3.Distance(waypoint.transform.position, apt.transform.position) <= distance)
				{
					aroundWaypoints.Add(waypoint);
				}
			}
			Waypoint pt = aroundWaypoints.GetElement(UnityEngine.Random.Range(0, aroundWaypoints.Count));
			ListPool<Waypoint>.Release(aroundWaypoints);
			if (pt == null)
			{
				yield break;
			}
			int prevCount = list.Count;
			if (!this.AnyWaypoint(list, pt) && pt.Reserver == null)
			{
				float waypointTweenMinDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.WaypointTweenMinDistance;
				if (list.IsNullOrEmpty<Waypoint>())
				{
					if (Vector3.Distance(pt.transform.position, base.Position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
				else
				{
					Waypoint waypoint2 = list[list.Count - 1];
					if (Vector3.Distance(pt.transform.position, waypoint2.transform.position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
			}
			actionPoints.Remove(apt);
			points.Remove(pt);
			if (prevCount == list.Count)
			{
				yield return this.CalculateNearActionPath(points, actionPoints, list);
			}
			yield break;
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x0029BBBC File Offset: 0x00299FBC
		public IEnumerator CalculateDateActionWaypoints()
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, chunkID, weather, true, true))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, chunkID, weather, true, true))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			int pathCount = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.reservedPathCount;
			ActionPoint[] actionPoints = Singleton<Manager.Map>.Instance.PointAgent.ActionPoints;
			List<ActionPoint> apElements = ListPool<ActionPoint>.Get();
			Desire.Type requestedDesire = this.RequestedDesire;
			EventType type = (EventType)0;
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (valueTuple.Item2 == requestedDesire)
				{
					type = valueTuple.Item1;
					break;
				}
			}
			foreach (ActionPoint actionPoint in actionPoints)
			{
				MapArea ownerArea3 = actionPoint.OwnerArea;
				if (this.AvailableAreaCheck(ownerArea3, availableArea, mapManager))
				{
					if (!apElements.Contains(actionPoint))
					{
						if (actionPoint.AgentDateEventType.Contains(type))
						{
							apElements.Add(actionPoint);
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateDateActionPath(elements, apElements, pathCount);
			ListPool<Waypoint>.Release(elements);
			ListPool<ActionPoint>.Release(apElements);
			this._dateActionCalcEnumerator = null;
			yield break;
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x0029BBD8 File Offset: 0x00299FD8
		public IEnumerator CalculateDateActionWaypointsAdditive(int count)
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, chunkID, weather, true, true))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, chunkID, weather, true, true))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			ActionPoint[] actionPoints = Singleton<Manager.Map>.Instance.PointAgent.ActionPoints;
			List<ActionPoint> apElements = ListPool<ActionPoint>.Get();
			Desire.Type requestedDesire = this.RequestedDesire;
			EventType type = (EventType)0;
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (valueTuple.Item2 == requestedDesire)
				{
					type = valueTuple.Item1;
					break;
				}
			}
			foreach (ActionPoint actionPoint in actionPoints)
			{
				MapArea ownerArea3 = actionPoint.OwnerArea;
				if (this.AvailableAreaCheck(ownerArea3, availableArea, mapManager))
				{
					if (!apElements.Contains(actionPoint))
					{
						if (actionPoint.AgentEventType.Contains(type))
						{
							apElements.Add(actionPoint);
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateNearActionPath(elements, apElements, count);
			ListPool<Waypoint>.Release(elements);
			ListPool<ActionPoint>.Release(apElements);
			this._additiveDateActionCalcProcess = null;
			yield break;
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x0029BBFC File Offset: 0x00299FFC
		private IEnumerator CalculateDateActionPath(List<Waypoint> points, List<ActionPoint> actionPoints, int count)
		{
			List<Waypoint> routes = ListPool<Waypoint>.Get();
			if (points.IsNullOrEmpty<Waypoint>())
			{
				this._reservedDateNearActionWaypoints.Clear();
				yield return AgentActor._waitMinute;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.CalculateDateActionPath(points, actionPoints, routes);
				}
				foreach (Waypoint item in routes)
				{
					this._reservedDateNearActionWaypoints.Add(item);
				}
			}
			ListPool<Waypoint>.Release(routes);
			yield break;
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x0029BC2C File Offset: 0x0029A02C
		private IEnumerator CalculateDateActionPath(List<Waypoint> points, List<ActionPoint> actionPoints, List<Waypoint> list)
		{
			if (this._calcElementPath == null)
			{
				this._calcElementPath = new NavMeshPath();
			}
			if (actionPoints.IsNullOrEmpty<ActionPoint>())
			{
				yield break;
			}
			if (points.IsNullOrEmpty<Waypoint>())
			{
				yield break;
			}
			ActionPoint apt = actionPoints.GetElement(UnityEngine.Random.Range(0, actionPoints.Count));
			if (apt == null)
			{
				yield break;
			}
			float distance = Singleton<Manager.Resources>.Instance.AgentProfile.ActionPointNearDistance;
			List<Waypoint> aroundWaypoints = ListPool<Waypoint>.Get();
			foreach (Waypoint waypoint in points)
			{
				if (Vector3.Distance(waypoint.transform.position, apt.transform.position) <= distance)
				{
					aroundWaypoints.Add(waypoint);
				}
			}
			Waypoint pt = aroundWaypoints.GetElement(UnityEngine.Random.Range(0, aroundWaypoints.Count));
			ListPool<Waypoint>.Release(aroundWaypoints);
			if (pt == null)
			{
				yield break;
			}
			int prevCount = list.Count;
			if (!this.AnyWaypoint(list, pt) && pt.Reserver == null)
			{
				float waypointTweenMinDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.WaypointTweenMinDistance;
				if (list.IsNullOrEmpty<Waypoint>())
				{
					if (Vector3.Distance(pt.transform.position, base.Position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
				else
				{
					Waypoint waypoint2 = list[list.Count - 1];
					if (Vector3.Distance(pt.transform.position, waypoint2.transform.position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
			}
			actionPoints.Remove(apt);
			points.Remove(pt);
			if (prevCount == list.Count)
			{
				yield return this.CalculateDateActionPath(points, actionPoints, list);
			}
			yield break;
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x0029BC5C File Offset: 0x0029A05C
		public IEnumerator CalculateLocationWaypoints()
		{
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (!(waypoint == null) && !(waypoint.OwnerArea == null))
					{
						if (waypoint.Reserver == null || waypoint.Reserver != this)
						{
							MapArea ownerArea = waypoint.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
							{
								elements.Add(waypoint);
							}
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (!(waypoint2 == null) && !(waypoint2.OwnerArea == null))
						{
							if (waypoint2.Reserver == null || waypoint2.Reserver == this)
							{
								MapArea ownerArea2 = waypoint2.OwnerArea;
								if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
								{
									elements.Add(waypoint2);
								}
							}
						}
					}
				}
			}
			int pathCount = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.reservedPathCount;
			ActionPoint[] actionPoints = Singleton<Manager.Map>.Instance.PointAgent.ActionPoints;
			List<ActionPoint> apElements = ListPool<ActionPoint>.Get();
			Desire.Type requestedDesire = this.RequestedDesire;
			EventType type = (EventType)0;
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (valueTuple.Item2 == requestedDesire)
				{
					type = valueTuple.Item1;
					break;
				}
			}
			ActionPoint[] array2 = actionPoints;
			int k = 0;
			while (k < array2.Length)
			{
				ActionPoint actionPoint = array2[k];
				MapArea ownerArea3 = actionPoint.OwnerArea;
				if (!(ownerArea3 != null))
				{
					goto IL_388;
				}
				bool flag;
				if (!availableArea.TryGetValue(ownerArea3.AreaID, out flag))
				{
					flag = (availableArea[ownerArea3.AreaID] = mapManager.CheckAvailableMapArea(ownerArea3.AreaID));
				}
				if (flag)
				{
					goto IL_388;
				}
				IL_3C3:
				k++;
				continue;
				IL_388:
				if (apElements.Contains(actionPoint))
				{
					goto IL_3C3;
				}
				if (actionPoint.AgentEventType.Contains(type))
				{
					apElements.Add(actionPoint);
					goto IL_3C3;
				}
				goto IL_3C3;
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateLocationPath(elements, apElements, pathCount);
			ListPool<Waypoint>.Release(elements);
			ListPool<ActionPoint>.Release(apElements);
			this._locationCalcEnumerator = null;
			yield break;
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x0029BC78 File Offset: 0x0029A078
		public IEnumerator CalculateLocationWaypointsAdditive(int count)
		{
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (!(waypoint == null) && !(waypoint.OwnerArea == null))
					{
						if (waypoint.Reserver == null || waypoint.Reserver != this)
						{
							MapArea ownerArea = waypoint.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
							{
								elements.Add(waypoint);
							}
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (!(waypoint2 == null) && !(waypoint2.OwnerArea == null))
						{
							if (waypoint2.Reserver == null || waypoint2.Reserver == this)
							{
								MapArea ownerArea2 = waypoint2.OwnerArea;
								if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
								{
									elements.Add(waypoint2);
								}
							}
						}
					}
				}
			}
			ActionPoint[] actionPoints = Singleton<Manager.Map>.Instance.PointAgent.ActionPoints;
			List<ActionPoint> apElements = ListPool<ActionPoint>.Get();
			Desire.Type requestedDesire = this.RequestedDesire;
			EventType type = (EventType)0;
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (valueTuple.Item2 == requestedDesire)
				{
					type = valueTuple.Item1;
					break;
				}
			}
			foreach (ActionPoint actionPoint in actionPoints)
			{
				MapArea ownerArea3 = actionPoint.OwnerArea;
				if (this.AvailableAreaCheck(ownerArea3, availableArea, mapManager))
				{
					if (!apElements.Contains(actionPoint))
					{
						if (actionPoint.AgentEventType.Contains(type))
						{
							apElements.Add(actionPoint);
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateLocationPath(elements, apElements, count);
			ListPool<Waypoint>.Release(elements);
			ListPool<ActionPoint>.Release(apElements);
			this._additiveActionCalcProcess = null;
			yield break;
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x0029BC9C File Offset: 0x0029A09C
		private IEnumerator CalculateLocationPath(List<Waypoint> points, List<ActionPoint> actionPoints, int count)
		{
			List<Waypoint> routes = ListPool<Waypoint>.Get();
			if (points.IsNullOrEmpty<Waypoint>())
			{
				this._reservedLocationWaypoints.Clear();
				yield return AgentActor._waitMinute;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.CalculateLocationPath(points, actionPoints, routes);
				}
				foreach (Waypoint item in routes)
				{
					this._reservedLocationWaypoints.Add(item);
				}
			}
			ListPool<Waypoint>.Release(routes);
			yield break;
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x0029BCCC File Offset: 0x0029A0CC
		private IEnumerator CalculateLocationPath(List<Waypoint> points, List<ActionPoint> actionPoints, List<Waypoint> list)
		{
			if (this._calcElementPath == null)
			{
				this._calcElementPath = new NavMeshPath();
			}
			if (actionPoints.IsNullOrEmpty<ActionPoint>())
			{
				yield break;
			}
			if (points.IsNullOrEmpty<Waypoint>())
			{
				yield break;
			}
			ActionPoint apt = actionPoints.GetElement(UnityEngine.Random.Range(0, actionPoints.Count));
			if (apt == null)
			{
				yield break;
			}
			float distance = Singleton<Manager.Resources>.Instance.AgentProfile.ActionPointNearDistance;
			List<Waypoint> aroundWaypoints = ListPool<Waypoint>.Get();
			foreach (Waypoint waypoint in points)
			{
				if (Vector3.Distance(waypoint.transform.position, apt.transform.position) <= distance)
				{
					aroundWaypoints.Add(waypoint);
				}
			}
			Waypoint pt = aroundWaypoints.GetElement(UnityEngine.Random.Range(0, aroundWaypoints.Count));
			ListPool<Waypoint>.Release(aroundWaypoints);
			if (pt == null)
			{
				yield break;
			}
			int prevCount = list.Count;
			if (!this.AnyWaypoint(list, pt) && pt.Reserver == null)
			{
				float waypointTweenMinDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.WaypointTweenMinDistance;
				if (list.IsNullOrEmpty<Waypoint>())
				{
					if (Vector2.Distance(pt.transform.position, base.Position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
				else
				{
					Waypoint waypoint2 = list[list.Count - 1];
					if (Vector3.Distance(pt.transform.position, waypoint2.transform.position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
			}
			actionPoints.Remove(apt);
			points.Remove(pt);
			if (prevCount == list.Count)
			{
				yield return this.CalculateLocationPath(points, actionPoints, list);
			}
			yield break;
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x0029BCFC File Offset: 0x0029A0FC
		public IEnumerator CalculateActorWaypoints()
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			float value = UnityEngine.Random.value;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			List<Actor> sameChunkActors = ListPool<Actor>.Get();
			List<Actor> diffAreaActors = ListPool<Actor>.Get();
			foreach (Actor actor in this.TargetActors)
			{
				int chunkID2 = actor.ChunkID;
				int areaID = actor.AreaID;
				if (chunkID2 == base.ChunkID)
				{
					sameChunkActors.Add(actor);
				}
				else
				{
					bool flag;
					if (!availableArea.TryGetValue(areaID, out flag))
					{
						flag = (availableArea[areaID] = mapManager.CheckAvailableMapArea(areaID));
					}
					if (flag)
					{
						diffAreaActors.Add(actor);
					}
				}
			}
			Actor target = null;
			if (!sameChunkActors.IsNullOrEmpty<Actor>())
			{
				target = sameChunkActors.GetElement(UnityEngine.Random.Range(0, sameChunkActors.Count));
			}
			else if (!diffAreaActors.IsNullOrEmpty<Actor>())
			{
				target = diffAreaActors.GetElement(UnityEngine.Random.Range(0, diffAreaActors.Count));
			}
			ListPool<Actor>.Release(sameChunkActors);
			ListPool<Actor>.Release(diffAreaActors);
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				bool checkWeather = waypoints.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, chunkID, weather, checkWeather, false))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					bool checkWeather2 = keyValuePair.Value.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, chunkID, weather, checkWeather2, false))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			int pathCount = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.reservedPathCount;
			yield return this.CalculateActorPath(elements, target, pathCount);
			ListPool<Waypoint>.Release(elements);
			this._actorCalcEnumerator = null;
			yield break;
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x0029BD18 File Offset: 0x0029A118
		public IEnumerator CalculateActorWaypointsAdditive(int count)
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			float value = UnityEngine.Random.value;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int chunkID = base.ChunkID;
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			List<Actor> sameAreaActors = ListPool<Actor>.Get();
			List<Actor> diffAreaActors = ListPool<Actor>.Get();
			foreach (Actor actor in this.TargetActors)
			{
				int chunkID2 = actor.ChunkID;
				int areaID = actor.AreaID;
				if (chunkID2 == chunkID)
				{
					sameAreaActors.Add(actor);
				}
				else
				{
					bool flag;
					if (!availableArea.TryGetValue(areaID, out flag))
					{
						flag = (availableArea[areaID] = mapManager.CheckAvailableMapArea(areaID));
					}
					if (flag)
					{
						diffAreaActors.Add(actor);
					}
				}
			}
			Actor target = null;
			if (!sameAreaActors.IsNullOrEmpty<Actor>())
			{
				target = sameAreaActors[UnityEngine.Random.Range(0, sameAreaActors.Count)];
			}
			else if (!diffAreaActors.IsNullOrEmpty<Actor>())
			{
				target = diffAreaActors[UnityEngine.Random.Range(0, diffAreaActors.Count)];
			}
			ListPool<Actor>.Release(sameAreaActors);
			ListPool<Actor>.Release(diffAreaActors);
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				bool checkWeather = waypoints.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, chunkID, weather, checkWeather, false))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					bool checkWeather2 = keyValuePair.Value.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, chunkID, weather, checkWeather2, false))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateActorPath(elements, target, count);
			ListPool<Waypoint>.Release(elements);
			yield break;
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x0029BD3C File Offset: 0x0029A13C
		private IEnumerator CalculateActorPath(List<Waypoint> points, Actor actor, int count)
		{
			List<Waypoint> routes = ListPool<Waypoint>.Get();
			if (points.IsNullOrEmpty<Waypoint>())
			{
				this._reservedActorWaypoints.Clear();
				yield return AgentActor._waitMinute;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.CalculateActorPath(points, actor, routes);
				}
				foreach (Waypoint item in routes)
				{
					this._reservedActorWaypoints.Add(item);
				}
			}
			ListPool<Waypoint>.Release(routes);
			yield break;
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x0029BD6C File Offset: 0x0029A16C
		private IEnumerator CalculateActorPath(List<Waypoint> points, Actor actor, List<Waypoint> list)
		{
			if (this._calcElementPath == null)
			{
				this._calcElementPath = new NavMeshPath();
			}
			if (actor == null)
			{
				yield break;
			}
			if (points.IsNullOrEmpty<Waypoint>())
			{
				yield break;
			}
			int chunkID = actor.ChunkID;
			int areaID = actor.AreaID;
			List<Waypoint> waypoints = ListPool<Waypoint>.Get();
			foreach (Waypoint waypoint in points)
			{
				MapArea ownerArea = waypoint.OwnerArea;
				if (ownerArea.ChunkID == chunkID)
				{
					waypoints.Add(waypoint);
				}
			}
			Waypoint pt = waypoints.GetElement(UnityEngine.Random.Range(0, waypoints.Count));
			ListPool<Waypoint>.Release(waypoints);
			if (pt == null)
			{
				yield break;
			}
			int prevCount = list.Count;
			if (!this.AnyWaypoint(list, pt) && pt.Reserver == null)
			{
				float waypointTweenMinDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.WaypointTweenMinDistance;
				if (list.IsNullOrEmpty<Waypoint>())
				{
					if (Vector3.Distance(pt.transform.position, base.Position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
				else
				{
					Waypoint waypoint2 = list[list.Count - 1];
					if (Vector3.Distance(pt.transform.position, waypoint2.transform.position) > waypointTweenMinDistance)
					{
						list.Add(pt);
					}
				}
			}
			points.Remove(pt);
			if (prevCount == list.Count)
			{
				yield return this.CalculateActorPath(points, actor, list);
			}
			yield break;
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x0029BD9C File Offset: 0x0029A19C
		public IEnumerator CalculatePlayerWaypoins()
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			float value = UnityEngine.Random.value;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			PlayerActor target = Singleton<Manager.Map>.Instance.Player;
			int targetAreaID = target.AreaID;
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				bool checkWeather = waypoints.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, base.ChunkID, weather, checkWeather, false))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					bool checkWeather2 = keyValuePair.Value.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, base.ChunkID, weather, checkWeather2, false))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			int pathCount = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.reservedPathCount;
			yield return this.CalculateActorPath(elements, target, pathCount);
			ListPool<Waypoint>.Release(elements);
			this._playerCalcEnumerator = null;
			yield break;
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x0029BDB8 File Offset: 0x0029A1B8
		public IEnumerator CalculatePlayerWaypointsAdditive(int count)
		{
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			float value = UnityEngine.Random.value;
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			PlayerActor target = Singleton<Manager.Map>.Instance.Player;
			Waypoint[] waypoints = Singleton<Manager.Map>.Instance.PointAgent.Waypoints;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				bool checkWeather = waypoints.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
				foreach (Waypoint waypoint in waypoints)
				{
					if (this.ValidatePoints(waypoint, base.ChunkID, weather, checkWeather, false))
					{
						MapArea ownerArea = waypoint.OwnerArea;
						if (this.AvailableAreaCheck(ownerArea, availableArea, mapManager))
						{
							elements.Add(waypoint);
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in Singleton<Manager.Map>.Instance.PointAgent.HousingWaypointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<Waypoint>())
				{
					bool checkWeather2 = keyValuePair.Value.Exists((Waypoint x) => !(x == null) && x.AreaType == MapArea.AreaType.Indoor);
					foreach (Waypoint waypoint2 in keyValuePair.Value)
					{
						if (this.ValidatePoints(waypoint2, base.ChunkID, weather, checkWeather2, false))
						{
							MapArea ownerArea2 = waypoint2.OwnerArea;
							if (this.AvailableAreaCheck(ownerArea2, availableArea, mapManager))
							{
								elements.Add(waypoint2);
							}
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculatePlayerPath(elements, target, count);
			ListPool<Waypoint>.Release(elements);
			yield break;
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x0029BDDC File Offset: 0x0029A1DC
		private IEnumerator CalculatePlayerPath(List<Waypoint> points, Actor actor, int count)
		{
			List<Waypoint> routes = ListPool<Waypoint>.Get();
			if (points.IsNullOrEmpty<Waypoint>())
			{
				this._reservedPlayerWaypoints.Clear();
				yield return AgentActor._waitMinute;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.CalculateActorPath(points, actor, routes);
				}
				foreach (Waypoint item in routes)
				{
					this._reservedPlayerWaypoints.Add(item);
				}
			}
			ListPool<Waypoint>.Release(routes);
			yield break;
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x0029BE0C File Offset: 0x0029A20C
		public IEnumerator CalculateAnimalWaypoints()
		{
			PointManager pointAgent = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.PointAgent;
			Waypoint[] waypoints = (!(pointAgent != null)) ? null : pointAgent.Waypoints;
			Dictionary<int, List<Waypoint>> housingPointTable = (!(pointAgent != null)) ? null : pointAgent.HousingWaypointTable;
			bool empty = waypoints.IsNullOrEmpty<Waypoint>();
			if (empty && !housingPointTable.IsNullOrEmpty<int, List<Waypoint>>())
			{
				foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in housingPointTable)
				{
					List<Waypoint> value2 = keyValuePair.Value;
					if (!(empty = value2.IsNullOrEmpty<Waypoint>()))
					{
						break;
					}
				}
			}
			if (empty)
			{
				this.SearchAnimalEmpty = true;
				this._animalCalcEnumerator = null;
				yield break;
			}
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			float value = UnityEngine.Random.value;
			int chunkID = base.ChunkID;
			int areaID = (!(base.MapArea != null)) ? this.AgentData.AreaID : base.MapArea.AreaID;
			AnimalBase target = null;
			AnimalTypes requestedAnimal = AnimalTypes.Cat;
			List<AnimalBase> animals = ListPool<AnimalBase>.Get();
			List<AnimalBase> sameAreaAnimals = ListPool<AnimalBase>.Get();
			List<AnimalBase> sameChunkAnimals = ListPool<AnimalBase>.Get();
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			foreach (AnimalBase animalBase in this.TargetAnimals)
			{
				if (!(animalBase == null))
				{
					if (!(animalBase.CurrentMapArea == null))
					{
						if (animalBase.AnimalType == requestedAnimal)
						{
							MapArea currentMapArea = animalBase.CurrentMapArea;
							if (currentMapArea.AreaID == areaID)
							{
								sameAreaAnimals.Add(animalBase);
							}
							else
							{
								bool flag;
								if (!availableArea.TryGetValue(currentMapArea.AreaID, out flag))
								{
									flag = (availableArea[currentMapArea.AreaID] = mapManager.CheckAvailableMapArea(currentMapArea.AreaID));
								}
								if (flag)
								{
									if (currentMapArea.ChunkID == chunkID)
									{
										sameChunkAnimals.Add(animalBase);
									}
									else
									{
										animals.Add(animalBase);
									}
								}
							}
						}
					}
				}
			}
			if (!sameAreaAnimals.IsNullOrEmpty<AnimalBase>())
			{
				target = sameAreaAnimals.GetElement(UnityEngine.Random.Range(0, sameAreaAnimals.Count));
			}
			else if (!sameChunkAnimals.IsNullOrEmpty<AnimalBase>())
			{
				target = sameChunkAnimals.GetElement(UnityEngine.Random.Range(0, sameChunkAnimals.Count));
			}
			else if (!animals.IsNullOrEmpty<AnimalBase>())
			{
				target = animals.GetElement(UnityEngine.Random.Range(0, animals.Count));
			}
			ListPool<AnimalBase>.Release(animals);
			ListPool<AnimalBase>.Release(sameAreaAnimals);
			ListPool<AnimalBase>.Release(sameChunkAnimals);
			bool flag2 = target == null;
			this.SearchAnimalEmpty = flag2;
			if (flag2)
			{
				DictionaryPool<int, bool>.Release(availableArea);
				this._animalCalcEnumerator = null;
				yield break;
			}
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int animalAreaID = target.CurrentMapArea.AreaID;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (!(waypoint == null) && !(waypoint.OwnerArea == null))
					{
						if (waypoint.isActiveAndEnabled)
						{
							if (this.WeatherCheck(weather, waypoint))
							{
								MapArea ownerArea = waypoint.OwnerArea;
								if (ownerArea != null && ownerArea.AreaID != animalAreaID)
								{
									bool flag3;
									if (!availableArea.TryGetValue(ownerArea.AreaID, out flag3))
									{
										flag3 = (availableArea[ownerArea.AreaID] = mapManager.CheckAvailableMapArea(ownerArea.AreaID));
									}
									if (!flag3)
									{
										goto IL_568;
									}
								}
								if (waypoint.Reserver == null || waypoint.Reserver == this)
								{
									elements.Add(waypoint);
								}
							}
						}
					}
					IL_568:;
				}
			}
			if (!housingPointTable.IsNullOrEmpty<int, List<Waypoint>>())
			{
				foreach (KeyValuePair<int, List<Waypoint>> keyValuePair2 in housingPointTable)
				{
					List<Waypoint> value3 = keyValuePair2.Value;
					if (!value3.IsNullOrEmpty<Waypoint>())
					{
						foreach (Waypoint waypoint2 in value3)
						{
							if (!(waypoint2 == null) && !(waypoint2.OwnerArea == null))
							{
								if (waypoint2.isActiveAndEnabled)
								{
									if (this.WeatherCheck(weather, waypoint2))
									{
										MapArea ownerArea2 = waypoint2.OwnerArea;
										if (ownerArea2 != null && ownerArea2.AreaID != animalAreaID)
										{
											int areaID2 = ownerArea2.AreaID;
											bool flag4;
											if (!availableArea.TryGetValue(areaID2, out flag4))
											{
												flag4 = (availableArea[areaID2] = mapManager.CheckAvailableMapArea(areaID2));
											}
											if (!flag4)
											{
												continue;
											}
										}
										if (waypoint2.Reserver == null || waypoint2.Reserver == this)
										{
											elements.Add(waypoint2);
										}
									}
								}
							}
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(availableArea);
			flag2 = elements.IsNullOrEmpty<Waypoint>();
			this.SearchAnimalEmpty = flag2;
			if (flag2)
			{
				ListPool<Waypoint>.Release(elements);
				this._animalCalcEnumerator = null;
				yield break;
			}
			int pathCount = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.reservedPathCount;
			yield return this.CalculateAnimalPath(elements, target, pathCount);
			ListPool<Waypoint>.Release(elements);
			this._animalCalcEnumerator = null;
			yield break;
		}

		// Token: 0x0600637F RID: 25471 RVA: 0x0029BE28 File Offset: 0x0029A228
		public IEnumerator CalculateAnimalWaypointsAdditive(int count)
		{
			PointManager pointAgent = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.PointAgent;
			Waypoint[] waypoints = (!(pointAgent != null)) ? null : pointAgent.Waypoints;
			Dictionary<int, List<Waypoint>> housingPointTable = (!(pointAgent != null)) ? null : pointAgent.HousingWaypointTable;
			bool empty = waypoints.IsNullOrEmpty<Waypoint>();
			if (empty && !housingPointTable.IsNullOrEmpty<int, List<Waypoint>>())
			{
				foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in housingPointTable)
				{
					List<Waypoint> value2 = keyValuePair.Value;
					if (!(empty = value2.IsNullOrEmpty<Waypoint>()))
					{
						break;
					}
				}
			}
			if (empty)
			{
				this.SearchAnimalEmpty = true;
				this._additiveAnimalCalcProcess = null;
				yield break;
			}
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			float value = UnityEngine.Random.value;
			int chunkID = base.ChunkID;
			int areaID = (!(base.MapArea != null)) ? this.AgentData.AreaID : base.MapArea.AreaID;
			AnimalBase target = null;
			AnimalTypes requestedAnimal = AnimalTypes.Cat;
			List<AnimalBase> animals = ListPool<AnimalBase>.Get();
			List<AnimalBase> sameAreaAnimals = ListPool<AnimalBase>.Get();
			List<AnimalBase> sameChunkAnimals = ListPool<AnimalBase>.Get();
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			Dictionary<int, bool> availableArea = DictionaryPool<int, bool>.Get();
			foreach (AnimalBase animalBase in this.TargetAnimals)
			{
				if (!(animalBase == null))
				{
					if (!(animalBase.CurrentMapArea == null))
					{
						if (animalBase.AnimalType == requestedAnimal)
						{
							MapArea currentMapArea = animalBase.CurrentMapArea;
							if (currentMapArea.AreaID == areaID)
							{
								sameAreaAnimals.Add(animalBase);
							}
							else
							{
								bool flag;
								if (!availableArea.TryGetValue(currentMapArea.AreaID, out flag))
								{
									flag = (availableArea[currentMapArea.AreaID] = mapManager.CheckAvailableMapArea(currentMapArea.AreaID));
								}
								if (flag)
								{
									if (currentMapArea.ChunkID == chunkID)
									{
										sameChunkAnimals.Add(animalBase);
									}
									else
									{
										animals.Add(animalBase);
									}
								}
							}
						}
					}
				}
			}
			if (!sameAreaAnimals.IsNullOrEmpty<AnimalBase>())
			{
				target = sameAreaAnimals.GetElement(UnityEngine.Random.Range(0, sameAreaAnimals.Count));
			}
			else if (!sameChunkAnimals.IsNullOrEmpty<AnimalBase>())
			{
				target = sameChunkAnimals.GetElement(UnityEngine.Random.Range(0, sameChunkAnimals.Count));
			}
			else if (!animals.IsNullOrEmpty<AnimalBase>())
			{
				target = animals.GetElement(UnityEngine.Random.Range(0, animals.Count));
			}
			ListPool<AnimalBase>.Release(animals);
			ListPool<AnimalBase>.Release(sameAreaAnimals);
			ListPool<AnimalBase>.Release(sameChunkAnimals);
			this.SearchAnimalEmpty = (target == null);
			bool flag2 = target == null;
			this.SearchAnimalEmpty = flag2;
			if (flag2)
			{
				DictionaryPool<int, bool>.Release(availableArea);
				this._additiveAnimalCalcProcess = null;
				yield break;
			}
			List<Waypoint> elements = ListPool<Waypoint>.Get();
			int animalAreaID = target.CurrentMapArea.AreaID;
			if (!waypoints.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in waypoints)
				{
					if (!(waypoint == null) && !(waypoint.OwnerArea == null))
					{
						if (this.WeatherCheck(weather, waypoint))
						{
							MapArea ownerArea = waypoint.OwnerArea;
							if (ownerArea != null && ownerArea.AreaID != animalAreaID)
							{
								bool flag3;
								if (!availableArea.TryGetValue(ownerArea.AreaID, out flag3))
								{
									flag3 = (availableArea[ownerArea.AreaID] = mapManager.CheckAvailableMapArea(ownerArea.AreaID));
								}
								if (!flag3)
								{
									goto IL_56E;
								}
							}
							if (waypoint.Reserver == null || waypoint.Reserver == this)
							{
								elements.Add(waypoint);
							}
						}
					}
					IL_56E:;
				}
			}
			if (!housingPointTable.IsNullOrEmpty<int, List<Waypoint>>())
			{
				foreach (KeyValuePair<int, List<Waypoint>> keyValuePair2 in housingPointTable)
				{
					List<Waypoint> value3 = keyValuePair2.Value;
					if (!value3.IsNullOrEmpty<Waypoint>())
					{
						foreach (Waypoint waypoint2 in value3)
						{
							if (!(waypoint2 == null) && !(waypoint2.OwnerArea == null))
							{
								if (waypoint2.isActiveAndEnabled)
								{
									if (this.WeatherCheck(weather, waypoint2))
									{
										MapArea ownerArea2 = waypoint2.OwnerArea;
										if (ownerArea2 != null && ownerArea2.AreaID != animalAreaID)
										{
											int areaID2 = ownerArea2.AreaID;
											bool flag4;
											if (!availableArea.TryGetValue(areaID2, out flag4))
											{
												flag4 = (availableArea[areaID2] = mapManager.CheckAvailableMapArea(areaID2));
											}
											if (!flag4)
											{
												continue;
											}
										}
										if (waypoint2.Reserver == null || waypoint2.Reserver == this)
										{
											elements.Add(waypoint2);
										}
									}
								}
							}
						}
					}
				}
			}
			flag2 = elements.IsNullOrEmpty<Waypoint>();
			this.SearchAnimalEmpty = flag2;
			if (flag2)
			{
				ListPool<Waypoint>.Release(elements);
				this._additiveAnimalCalcProcess = null;
				yield break;
			}
			DictionaryPool<int, bool>.Release(availableArea);
			yield return this.CalculateAnimalPath(elements, target, count);
			ListPool<Waypoint>.Release(elements);
			this._additiveAnimalCalcProcess = null;
			yield break;
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x0029BE4C File Offset: 0x0029A24C
		private IEnumerator CalculateAnimalPath(List<Waypoint> points, AnimalBase target, int count)
		{
			List<Waypoint> routes = ListPool<Waypoint>.Get();
			if (points.IsNullOrEmpty<Waypoint>())
			{
				this.SearchAnimalEmpty = true;
				this._reservedAnimalWaypoints.Clear();
				yield return AgentActor._waitMinute;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					yield return this.CalculateAnimalPath(points, target, routes);
				}
				this.SearchAnimalEmpty = routes.IsNullOrEmpty<Waypoint>();
				foreach (Waypoint item in routes)
				{
					this._reservedAnimalWaypoints.Add(item);
				}
			}
			ListPool<Waypoint>.Release(routes);
			yield break;
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x0029BE7C File Offset: 0x0029A27C
		private IEnumerator CalculateAnimalPath(List<Waypoint> points, AnimalBase animal, List<Waypoint> list)
		{
			if (this._calcElementPath == null)
			{
				this._calcElementPath = new NavMeshPath();
			}
			if (points.IsNullOrEmpty<Waypoint>())
			{
				yield break;
			}
			int prevCount = list.Count;
			List<Waypoint> waypoints = ListPool<Waypoint>.Get();
			if (animal != null)
			{
				int areaID = animal.CurrentMapArea.AreaID;
				foreach (Waypoint waypoint in points)
				{
					if (waypoint.OwnerArea.AreaID == areaID)
					{
						waypoints.Add(waypoint);
					}
				}
			}
			else
			{
				waypoints.AddRange(points);
			}
			Waypoint pt = waypoints.GetElement(UnityEngine.Random.Range(0, waypoints.Count));
			ListPool<Waypoint>.Release(waypoints);
			if (pt == null)
			{
				yield break;
			}
			if (!this.AnyWaypoint(list, pt) && pt.Reserver == null)
			{
				float waypointTweenMinDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.WaypointTweenMinDistance;
				Vector3 b = (!list.IsNullOrEmpty<Waypoint>()) ? list[list.Count - 1].transform.position : base.Position;
				if (waypointTweenMinDistance < Vector3.Distance(pt.transform.position, b))
				{
					list.Add(pt);
				}
			}
			points.Remove(pt);
			if (prevCount == list.Count)
			{
				yield return this.CalculateAnimalPath(points, animal, list);
			}
			yield break;
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x0029BEAC File Offset: 0x0029A2AC
		private bool WeatherCheck(Weather weather, Point point)
		{
			return (weather != Weather.Rain && weather != Weather.Storm) || base.ChaControl.fileGameInfo.normalSkill.ContainsValue(16) || point.AreaType == MapArea.AreaType.Indoor;
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x0029BEE7 File Offset: 0x0029A2E7
		public void StartPatrol()
		{
			this._patrolCoroutine = this.Patrol();
			base.StartCoroutine(this._patrolCoroutine);
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x0029BF02 File Offset: 0x0029A302
		public void ResumePatrol()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (this._patrolCoroutine == null)
			{
				this._patrolCoroutine = this.Patrol();
			}
			base.StartCoroutine(this._patrolCoroutine);
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x0029BF34 File Offset: 0x0029A334
		public void StopPatrol()
		{
			if (this._patrolCoroutine != null)
			{
				base.StopCoroutine(this._patrolCoroutine);
			}
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x0029BF4D File Offset: 0x0029A34D
		public void AbortPatrol()
		{
			if (this._patrolCoroutine != null)
			{
				base.StopCoroutine(this._patrolCoroutine);
				this._patrolCoroutine = null;
			}
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x0029BF6D File Offset: 0x0029A36D
		public void ClearWalkPath()
		{
			this._reservedWaypoints.Clear();
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x0029BF7C File Offset: 0x0029A37C
		private IEnumerator Patrol()
		{
			this._calcEnumerator = this.CalculateWaypoints();
			base.StartCoroutine(this._calcEnumerator);
			while (this.LivesCalc)
			{
				yield return null;
			}
			this.WalkRoute.Clear();
			int count = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.viaPointNumThreshold.RandomValue;
			for (int i = 0; i < count; i++)
			{
				this.WalkRoute.Enqueue(this._reservedWaypoints.PopFront<Waypoint>());
			}
			while (this.WalkRoute.Count > 0)
			{
				if (this.DestWaypoint != null)
				{
					while (!this.HasArrived())
					{
						yield return null;
						this.SetDestinationByDirectPath(this.DestWaypoint);
					}
				}
				if (!this.WalkRoute.IsNullOrEmpty<Waypoint>())
				{
					Waypoint destination = this.WalkRoute.Dequeue();
					if (!(destination == null))
					{
						this.SetDestinationByDirectPathForce(destination);
						do
						{
							yield return null;
							if (this.DestWaypoint != null && destination != this.DestWaypoint)
							{
								while (!this.HasArrived())
								{
									yield return null;
									this.SetDestinationByDirectPath(this.DestWaypoint);
								}
							}
							this.SetDestinationByDirectPath(destination);
						}
						while (!this.HasArrived());
						if (this.DestWaypoint != null)
						{
							this.DestWaypoint.Reserver = null;
							this.DestWaypoint = null;
						}
					}
				}
			}
			this._patrolCoroutine = null;
			yield break;
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06006389 RID: 25481 RVA: 0x0029BF97 File Offset: 0x0029A397
		public bool LivesPatrol
		{
			[CompilerGenerated]
			get
			{
				return this._patrolCoroutine != null;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x0600638A RID: 25482 RVA: 0x0029BFA5 File Offset: 0x0029A3A5
		public bool LivesCalc
		{
			[CompilerGenerated]
			get
			{
				return this._calcEnumerator != null;
			}
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x0029BFB3 File Offset: 0x0029A3B3
		private bool SetDestination(Waypoint destination)
		{
			this.DestWaypoint = destination;
			return this._navMeshAgent.SetDestination(destination.transform.position);
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x0029BFD4 File Offset: 0x0029A3D4
		private bool SetDestinationByDirectPathForce(Waypoint destination)
		{
			this.DestWaypoint = destination;
			bool result = false;
			if (this._navMeshAgent == null)
			{
				return false;
			}
			if (!this._navMeshAgent.isOnNavMesh)
			{
				return result;
			}
			NavMeshPath path = new NavMeshPath();
			if (!this._navMeshAgent.CalculatePath(destination.transform.position, path) || !this._navMeshAgent.SetPath(path) || this._navMeshAgent.path.corners.IsNullOrEmpty<Vector3>())
			{
			}
			return result;
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x0029C060 File Offset: 0x0029A460
		private bool SetDestinationByDirectPath(Waypoint destination)
		{
			this.DestWaypoint = destination;
			bool result = false;
			if (!this._navMeshAgent.isOnNavMesh)
			{
				return result;
			}
			if (this._navMeshAgent.path.corners.IsNullOrEmpty<Vector3>())
			{
				NavMeshPath path = new NavMeshPath();
				if (!this._navMeshAgent.CalculatePath(destination.transform.position, path) || !this._navMeshAgent.SetPath(path) || this._navMeshAgent.path.corners.IsNullOrEmpty<Vector3>())
				{
				}
			}
			return result;
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x0029C0F0 File Offset: 0x0029A4F0
		public bool HasArrived()
		{
			if (this._navMeshAgent.enabled)
			{
				float arrivedDistance = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.arrivedDistance;
				float num = (!this._navMeshAgent.pathPending) ? this._navMeshAgent.remainingDistance : float.PositiveInfinity;
				return num <= arrivedDistance;
			}
			return false;
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x0029C154 File Offset: 0x0029A554
		public bool HasArrivedOffMeshLink(OffMeshLink offMeshLink)
		{
			if (offMeshLink == null)
			{
				return false;
			}
			if (this._navMeshAgent.isActiveAndEnabled)
			{
				float arrivedDistance = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.arrivedDistance;
				float num = (!this._navMeshAgent.pathPending) ? this._navMeshAgent.remainingDistance : float.PositiveInfinity;
				return num <= arrivedDistance;
			}
			return false;
		}

		// Token: 0x06006390 RID: 25488 RVA: 0x0029C1C8 File Offset: 0x0029A5C8
		public bool IsInvalidMoveDestination(OffMeshLink checkLink)
		{
			if (checkLink == null)
			{
				return false;
			}
			ActionPoint component = checkLink.GetComponent<ActionPoint>();
			if (component == null)
			{
				return false;
			}
			if (!component.IsNeutralCommand)
			{
				return true;
			}
			List<ActionPoint> connectedActionPoints = component.ConnectedActionPoints;
			if (connectedActionPoints != null)
			{
				foreach (ActionPoint actionPoint in connectedActionPoints)
				{
					if (!(actionPoint == null))
					{
						if (!actionPoint.IsNeutralCommand)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06006391 RID: 25489 RVA: 0x0029C280 File Offset: 0x0029A680
		public void StartActionPatrol()
		{
			if (this._actionPatrolCoroutine == null)
			{
				this._actionPatrolCoroutine = this.ActionPatrol();
			}
			base.StartCoroutine(this._actionPatrolCoroutine);
		}

		// Token: 0x06006392 RID: 25490 RVA: 0x0029C2A6 File Offset: 0x0029A6A6
		public void StopActionPatrol()
		{
			if (this._actionPatrolCoroutine != null)
			{
				base.StopCoroutine(this._actionPatrolCoroutine);
			}
		}

		// Token: 0x06006393 RID: 25491 RVA: 0x0029C2BF File Offset: 0x0029A6BF
		public void AbortActionPatrol()
		{
			if (this._actionPatrolCoroutine != null)
			{
				base.StopCoroutine(this._actionPatrolCoroutine);
				this._actionPatrolCoroutine = null;
			}
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x0029C2E0 File Offset: 0x0029A6E0
		private IEnumerator ActionPatrol()
		{
			if (this._reservedNearActionWaypoints.IsNullOrEmpty<Waypoint>())
			{
				this._actionCalcEnumerator = this.CalculateNearActionWaypoints();
				base.StartCoroutine(this._actionCalcEnumerator);
			}
			while (this.LivesActionCalc)
			{
				yield return null;
			}
			int count = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.viaPointNumThreshold.RandomValue;
			for (int i = 0; i < count; i++)
			{
				Waypoint waypoint = this._reservedNearActionWaypoints.PopFront<Waypoint>();
				if (waypoint != null)
				{
					this.SearchActionRoute.Enqueue(waypoint);
				}
			}
			this._additiveActionCalcProcess = this.CalculateNearActionWaypointsAdditive(count);
			base.StartCoroutine(this._additiveActionCalcProcess);
			while (this.SearchActionRoute.Count > 0)
			{
				Waypoint destination = this.SearchActionRoute.Dequeue();
				if (!(destination == null))
				{
					this.SetDestinationByDirectPathForce(destination);
					do
					{
						yield return null;
						this.SetDestinationByDirectPath(destination);
					}
					while (!this.HasArrived());
					if (this.DestWaypoint != null)
					{
						this.DestWaypoint.Reserver = null;
						this.DestWaypoint = null;
					}
				}
			}
			this._actionPatrolCoroutine = null;
			yield break;
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06006395 RID: 25493 RVA: 0x0029C2FB File Offset: 0x0029A6FB
		public bool LivesActionPatrol
		{
			[CompilerGenerated]
			get
			{
				return this._actionPatrolCoroutine != null;
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x0029C309 File Offset: 0x0029A709
		public bool LivesActionCalc
		{
			[CompilerGenerated]
			get
			{
				return this._actionCalcEnumerator != null;
			}
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x0029C317 File Offset: 0x0029A717
		public void StartDateActionPatrol()
		{
			if (this._dateActionPatrolCoroutine == null)
			{
				this._dateActionPatrolCoroutine = this.DateActionPatrol();
			}
			base.StartCoroutine(this._dateActionPatrolCoroutine);
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x0029C33D File Offset: 0x0029A73D
		public void StopDateActionPatrol()
		{
			if (this._dateActionPatrolCoroutine != null)
			{
				base.StopCoroutine(this._dateActionPatrolCoroutine);
			}
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x0029C358 File Offset: 0x0029A758
		private IEnumerator DateActionPatrol()
		{
			if (this._reservedNearActionWaypoints.IsNullOrEmpty<Waypoint>())
			{
				this._dateActionCalcEnumerator = this.CalculateDateActionWaypoints();
				base.StartCoroutine(this._dateActionCalcEnumerator);
			}
			while (this.LivesDateActionCalc)
			{
				yield return null;
			}
			int count = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.viaPointNumThreshold.RandomValue;
			for (int i = 0; i < count; i++)
			{
				Waypoint waypoint = this._reservedNearActionWaypoints.PopFront<Waypoint>();
				if (waypoint != null)
				{
					this.SearchDateActionRoute.Enqueue(waypoint);
				}
			}
			this._additiveDateActionCalcProcess = this.CalculateDateActionWaypointsAdditive(count);
			base.StartCoroutine(this._additiveDateActionCalcProcess);
			while (this.SearchDateActionRoute.Count > 0)
			{
				Waypoint destination = this.SearchDateActionRoute.Dequeue();
				if (!(destination == null))
				{
					this.SetDestinationByDirectPathForce(destination);
					do
					{
						yield return null;
						this.SetDestinationByDirectPath(destination);
					}
					while (!this.HasArrived());
					if (this.DestWaypoint != null)
					{
						this.DestWaypoint.Reserver = null;
						this.DestWaypoint = null;
					}
				}
			}
			this._dateActionPatrolCoroutine = null;
			yield break;
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x0600639A RID: 25498 RVA: 0x0029C373 File Offset: 0x0029A773
		public bool LivesDateActionPatrol
		{
			[CompilerGenerated]
			get
			{
				return this._dateActionPatrolCoroutine != null;
			}
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600639B RID: 25499 RVA: 0x0029C381 File Offset: 0x0029A781
		public bool LivesDateActionCalc
		{
			[CompilerGenerated]
			get
			{
				return this._dateActionCalcEnumerator != null;
			}
		}

		// Token: 0x0600639C RID: 25500 RVA: 0x0029C38F File Offset: 0x0029A78F
		public void StartLocationPatrol()
		{
			if (this._locationPatrolCoroutine == null)
			{
				this._locationPatrolCoroutine = this.LocationPatrol();
			}
			base.StartCoroutine(this._locationPatrolCoroutine);
		}

		// Token: 0x0600639D RID: 25501 RVA: 0x0029C3B5 File Offset: 0x0029A7B5
		public void StopLocationPatrol()
		{
			if (this._locationPatrolCoroutine != null)
			{
				base.StopCoroutine(this._locationPatrolCoroutine);
			}
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x0029C3D0 File Offset: 0x0029A7D0
		private IEnumerator LocationPatrol()
		{
			if (this._reservedLocationWaypoints.IsNullOrEmpty<Waypoint>())
			{
				this._locationCalcEnumerator = this.CalculateLocationWaypoints();
				base.StartCoroutine(this._locationCalcEnumerator);
			}
			while (this.LivesLocationCalc)
			{
				yield return null;
			}
			int count = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.viaPointNumThreshold.RandomValue;
			for (int i = 0; i < count; i++)
			{
				Waypoint waypoint = this._reservedLocationWaypoints.PopFront<Waypoint>();
				if (waypoint != null)
				{
					this.SearchLocationRoute.Enqueue(waypoint);
				}
			}
			this._additiveLocationCalcProcess = this.CalculateLocationWaypointsAdditive(count);
			base.StartCoroutine(this._additiveLocationCalcProcess);
			while (this.SearchLocationRoute.Count > 0)
			{
				Waypoint destination = this.SearchLocationRoute.Dequeue();
				if (!(destination == null))
				{
					this.SetDestinationByDirectPathForce(destination);
					do
					{
						yield return null;
						this.SetDestinationByDirectPath(destination);
					}
					while (!this.HasArrived());
					if (this.DestWaypoint != null)
					{
						this.DestWaypoint.Reserver = null;
						this.DestWaypoint = null;
					}
				}
			}
			this._actionPatrolCoroutine = null;
			yield break;
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x0600639F RID: 25503 RVA: 0x0029C3EB File Offset: 0x0029A7EB
		public bool LivesLocationPatrol
		{
			[CompilerGenerated]
			get
			{
				return this._locationPatrolCoroutine != null;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x0029C3F9 File Offset: 0x0029A7F9
		public bool LivesLocationCalc
		{
			[CompilerGenerated]
			get
			{
				return this._locationCalcEnumerator != null;
			}
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x0029C407 File Offset: 0x0029A807
		public void StartActorPatrol()
		{
			if (this._actorPatrolCoroutine == null)
			{
				this._actorPatrolCoroutine = this.ActorPatrol();
			}
			base.StartCoroutine(this._actorPatrolCoroutine);
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x0029C42D File Offset: 0x0029A82D
		public void StopActorPatrol()
		{
			if (this._actorPatrolCoroutine != null)
			{
				base.StopCoroutine(this._actorPatrolCoroutine);
			}
		}

		// Token: 0x060063A3 RID: 25507 RVA: 0x0029C448 File Offset: 0x0029A848
		private IEnumerator ActorPatrol()
		{
			if (this._reservedActorWaypoints.IsNullOrEmpty<Waypoint>())
			{
				this._actorCalcEnumerator = this.CalculateActorWaypoints();
				base.StartCoroutine(this._actorCalcEnumerator);
			}
			while (this.LivesActorCalc)
			{
				yield return null;
			}
			int count = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.viaPointNumThreshold.RandomValue;
			for (int i = 0; i < count; i++)
			{
				Waypoint waypoint = this._reservedActorWaypoints.PopFront<Waypoint>();
				if (waypoint != null)
				{
					this.SearchActorRoute.Enqueue(waypoint);
				}
			}
			this._additiveActorCalcProcess = this.CalculateActorWaypointsAdditive(count);
			base.StartCoroutine(this._additiveActorCalcProcess);
			while (this.SearchActorRoute.Count > 0)
			{
				Waypoint destination = this.SearchActorRoute.Dequeue();
				if (!(destination == null))
				{
					this.SetDestinationByDirectPathForce(destination);
					do
					{
						yield return null;
						this.SetDestinationByDirectPath(destination);
					}
					while (!this.HasArrived());
					if (this.DestWaypoint != null)
					{
						this.DestWaypoint.Reserver = null;
						this.DestWaypoint = null;
					}
				}
			}
			this._actorPatrolCoroutine = null;
			yield break;
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x0029C463 File Offset: 0x0029A863
		public bool LivesActorPatrol
		{
			[CompilerGenerated]
			get
			{
				return this._actorPatrolCoroutine != null;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060063A5 RID: 25509 RVA: 0x0029C471 File Offset: 0x0029A871
		public bool LivesActorCalc
		{
			[CompilerGenerated]
			get
			{
				return this._actorCalcEnumerator != null;
			}
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x0029C47F File Offset: 0x0029A87F
		public void StartPlayerPatrol()
		{
			if (this._playerPatrolCoroutine == null)
			{
				this._playerPatrolCoroutine = this.PlayerPatrol();
			}
			base.StartCoroutine(this._playerPatrolCoroutine);
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x0029C4A5 File Offset: 0x0029A8A5
		public void StopPlayerPatrol()
		{
			if (this._playerPatrolCoroutine != null)
			{
				base.StopCoroutine(this._playerPatrolCoroutine);
			}
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x0029C4C0 File Offset: 0x0029A8C0
		private IEnumerator PlayerPatrol()
		{
			if (this._reservedPlayerWaypoints.IsNullOrEmpty<Waypoint>())
			{
				this._playerCalcEnumerator = this.CalculatePlayerWaypoins();
				base.StartCoroutine(this._playerCalcEnumerator);
			}
			while (this.LivesPlayerCalc)
			{
				yield return null;
			}
			int count = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.viaPointNumThreshold.RandomValue;
			for (int i = 0; i < count; i++)
			{
				Waypoint waypoint = this._reservedPlayerWaypoints.PopFront<Waypoint>();
				if (waypoint != null)
				{
					this.SearchPlayerRoute.Enqueue(waypoint);
				}
			}
			this._additivePlayerCalcProcess = this.CalculatePlayerWaypointsAdditive(count);
			base.StartCoroutine(this._additivePlayerCalcProcess);
			while (this.SearchPlayerRoute.Count > 0)
			{
				Waypoint destination = this.SearchPlayerRoute.Dequeue();
				if (!(destination == null))
				{
					this.SetDestinationByDirectPathForce(destination);
					do
					{
						yield return null;
						this.SetDestinationByDirectPath(destination);
					}
					while (!this.HasArrived());
					if (this.DestWaypoint != null)
					{
						this.DestWaypoint.Reserver = null;
						this.DestWaypoint = null;
					}
				}
			}
			this._playerPatrolCoroutine = null;
			yield break;
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060063A9 RID: 25513 RVA: 0x0029C4DB File Offset: 0x0029A8DB
		public bool LivesPlayerPatrol
		{
			[CompilerGenerated]
			get
			{
				return this._playerPatrolCoroutine != null;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060063AA RID: 25514 RVA: 0x0029C4E9 File Offset: 0x0029A8E9
		public bool LivesPlayerCalc
		{
			[CompilerGenerated]
			get
			{
				return this._playerCalcEnumerator != null;
			}
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x0029C4F7 File Offset: 0x0029A8F7
		public void StartAnimalPatrol()
		{
			if (this._animalPatrolCoroutine == null)
			{
				this._animalPatrolCoroutine = this.AnimalPatrol();
			}
			base.StartCoroutine(this._animalPatrolCoroutine);
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x0029C51D File Offset: 0x0029A91D
		public void StopAnimalPatrol()
		{
			if (this._animalPatrolCoroutine != null)
			{
				base.StopCoroutine(this._animalPatrolCoroutine);
			}
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x0029C538 File Offset: 0x0029A938
		public void StopForcedAnimalPatrol()
		{
			if (this._animalPatrolCoroutine != null)
			{
				base.StopCoroutine(this._animalPatrolCoroutine);
				this._animalPatrolCoroutine = null;
			}
			if (this._animalCalcEnumerator != null)
			{
				base.StopCoroutine(this._animalCalcEnumerator);
				this._animalCalcEnumerator = null;
			}
			if (this._additiveAnimalCalcProcess != null)
			{
				base.StopCoroutine(this._additiveAnimalCalcProcess);
				this._additiveAnimalCalcProcess = null;
			}
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x0029C59F File Offset: 0x0029A99F
		public void ClearAnimalRoutePoints()
		{
			if (!this._reservedAnimalWaypoints.IsNullOrEmpty<Waypoint>())
			{
				this._reservedAnimalWaypoints.Clear();
			}
			if (!this.SearchAnimalRoute.IsNullOrEmpty<Waypoint>())
			{
				this.SearchAnimalRoute.Clear();
			}
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x0029C5D8 File Offset: 0x0029A9D8
		private IEnumerator AnimalPatrol()
		{
			if (this._reservedAnimalWaypoints.IsNullOrEmpty<Waypoint>())
			{
				this._animalCalcEnumerator = this.CalculateAnimalWaypoints();
				base.StartCoroutine(this._animalCalcEnumerator);
			}
			while (this.LivesAnimalCalc)
			{
				yield return null;
			}
			int count = Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.viaPointNumThreshold.RandomValue;
			for (int i = 0; i < count; i++)
			{
				Waypoint waypoint = this._reservedAnimalWaypoints.PopFront<Waypoint>();
				if (waypoint != null)
				{
					this.SearchAnimalRoute.Enqueue(waypoint);
				}
			}
			this._additiveAnimalCalcProcess = this.CalculateAnimalWaypointsAdditive(count);
			base.StartCoroutine(this._additiveAnimalCalcProcess);
			this.SearchAnimalEmpty = this.SearchAnimalRoute.IsNullOrEmpty<Waypoint>();
			while (0 < this.SearchAnimalRoute.Count)
			{
				Waypoint destination = this.SearchAnimalRoute.Dequeue();
				if (!(destination == null))
				{
					this.SetDestinationByDirectPathForce(destination);
					do
					{
						yield return null;
						this.SetDestinationByDirectPath(destination);
					}
					while (!this.HasArrived());
					if (this.DestWaypoint != null)
					{
						this.DestWaypoint.Reserver = null;
						this.DestWaypoint = null;
					}
				}
			}
			this._animalPatrolCoroutine = null;
			yield break;
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060063B0 RID: 25520 RVA: 0x0029C5F3 File Offset: 0x0029A9F3
		// (set) Token: 0x060063B1 RID: 25521 RVA: 0x0029C5FB File Offset: 0x0029A9FB
		public bool SearchAnimalEmpty { get; set; }

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060063B2 RID: 25522 RVA: 0x0029C604 File Offset: 0x0029AA04
		public bool LivesAnimalPatrol
		{
			[CompilerGenerated]
			get
			{
				return this._animalPatrolCoroutine != null;
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x060063B3 RID: 25523 RVA: 0x0029C612 File Offset: 0x0029AA12
		public bool LivesAnimalCalc
		{
			[CompilerGenerated]
			get
			{
				return this._animalCalcEnumerator != null;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x060063B4 RID: 25524 RVA: 0x0029C620 File Offset: 0x0029AA20
		public bool CanGreet
		{
			get
			{
				AgentData agentData = this.AgentData;
				if (agentData.Greeted)
				{
					return false;
				}
				int num = base.ChaControl.fileGameInfo.flavorState[1];
				if ((float)num < Singleton<Manager.Resources>.Instance.StatusProfile.CanGreetBorder)
				{
					return false;
				}
				float num2 = agentData.StatsTable[1];
				return num2 >= base.ChaControl.fileGameInfo.moodBound.upper;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060063B5 RID: 25525 RVA: 0x0029C69C File Offset: 0x0029AA9C
		public bool CanScrounge
		{
			get
			{
				int phase = base.ChaControl.fileGameInfo.phase;
				return phase >= 2 && !this.AgentData.ItemScrounge.isEvent;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x060063B6 RID: 25526 RVA: 0x0029C6D8 File Offset: 0x0029AAD8
		public bool CanPresent
		{
			get
			{
				int phase = base.ChaControl.fileGameInfo.phase;
				return phase >= 2;
			}
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x060063B7 RID: 25527 RVA: 0x0029C6FD File Offset: 0x0029AAFD
		public bool CanBirthdayPresent
		{
			get
			{
				return !this.IsBadMood() && Singleton<Manager.Map>.Instance.Player.IsBirthday(this);
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x060063B8 RID: 25528 RVA: 0x0029C720 File Offset: 0x0029AB20
		public bool IsHealthManager
		{
			get
			{
				bool flag = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(28);
				float num = this.AgentData.StatsTable[3];
				bool flag2 = num < Singleton<Manager.Resources>.Instance.StatusProfile.HealthyPhysicalBorder;
				return flag && flag2;
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x060063B9 RID: 25529 RVA: 0x0029C77C File Offset: 0x0029AB7C
		public bool CanWashFace
		{
			get
			{
				int num = base.ChaControl.fileGameInfo.flavorState[2];
				int washFaceBorder = Singleton<Manager.Resources>.Instance.StatusProfile.WashFaceBorder;
				return num >= washFaceBorder;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x060063BA RID: 25530 RVA: 0x0029C7BC File Offset: 0x0029ABBC
		public bool CanClothChange
		{
			get
			{
				int num = base.ChaControl.fileGameInfo.flavorState[0];
				float canClothChangeBorder = Singleton<Manager.Resources>.Instance.StatusProfile.CanClothChangeBorder;
				return (float)num >= canClothChangeBorder;
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x060063BB RID: 25531 RVA: 0x0029C7FC File Offset: 0x0029ABFC
		public bool ShouldRestoreCloth
		{
			get
			{
				bool isOtherCoordinate = this.AgentData.IsOtherCoordinate;
				DateTime now = Singleton<Manager.Map>.Instance.Simulator.Now;
				DateTime time = Singleton<Manager.Resources>.Instance.StatusProfile.ShouldRestoreCoordTime.Time;
				float restoreRangeMinuteTime = Singleton<Manager.Resources>.Instance.StatusProfile.RestoreRangeMinuteTime;
				float num = Mathf.Abs((float)(time - now).TotalMinutes);
				bool flag = num < restoreRangeMinuteTime;
				return isOtherCoordinate && flag;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x060063BC RID: 25532 RVA: 0x0029C87C File Offset: 0x0029AC7C
		public bool CanGirlsAction
		{
			get
			{
				int num = base.ChaControl.fileGameInfo.flavorState[7];
				int girlsActionBorder = Singleton<Manager.Resources>.Instance.StatusProfile.GirlsActionBorder;
				return num >= girlsActionBorder;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x060063BD RID: 25533 RVA: 0x0029C8BC File Offset: 0x0029ACBC
		public bool CanCauseHurt
		{
			get
			{
				if (this.AgentData.SickState.ID != -1)
				{
					return false;
				}
				int flavor = base.ChaControl.fileGameInfo.flavorState[5];
				StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
				float num = AgentActor.FlavorVariation(statusProfile.HurtRateBuffMinMax, statusProfile.HurtRateBuff, flavor);
				float num2 = statusProfile.HurtDefaultIncidence + num;
				float num3 = UnityEngine.Random.Range(0f, 100f);
				bool flag = num3 <= num2;
				bool flag2 = base.AreaType == MapArea.AreaType.Normal;
				return flag && flag2;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x060063BE RID: 25534 RVA: 0x0029C954 File Offset: 0x0029AD54
		public bool CanInvitation
		{
			get
			{
				int num = base.ChaControl.fileGameInfo.flavorState[4];
				int invitationBorder = Singleton<Manager.Resources>.Instance.StatusProfile.InvitationBorder;
				bool flag = num >= invitationBorder;
				return Game.isAdd01 && flag;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x060063BF RID: 25535 RVA: 0x0029C9A0 File Offset: 0x0029ADA0
		public bool CanMasturbation
		{
			get
			{
				int num = base.ChaControl.fileGameInfo.flavorState[4];
				int masturbationBorder = Singleton<Manager.Resources>.Instance.StatusProfile.MasturbationBorder;
				return Game.isAdd01 && num >= masturbationBorder;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060063C0 RID: 25536 RVA: 0x0029C9EC File Offset: 0x0029ADEC
		public bool CanLesbian
		{
			get
			{
				if (!Game.isAdd01)
				{
					return false;
				}
				if (this.TargetInSightActor == null)
				{
					return false;
				}
				bool flag = base.ChaControl.fileGameInfo.hSkill.ContainsValue(12);
				int id = this.TargetInSightActor.ID;
				int num;
				if (!this.AgentData.FriendlyRelationShipTable.TryGetValue(id, out num))
				{
					int num2 = 50;
					this.AgentData.FriendlyRelationShipTable[id] = num2;
					num = num2;
				}
				StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
				bool flag2 = num >= statusProfile.LesbianFriendlyRelationBorder;
				if (flag && flag2)
				{
					float num3 = UnityEngine.Random.Range(0f, 100f);
					if (num3 <= statusProfile.LesbianRate)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060063C1 RID: 25537 RVA: 0x0029CAB5 File Offset: 0x0029AEB5
		public bool IsEmptyWeaknessMotivation
		{
			get
			{
				return this.AgentData.WeaknessMotivation <= 0f;
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060063C2 RID: 25538 RVA: 0x0029CACC File Offset: 0x0029AECC
		// (set) Token: 0x060063C3 RID: 25539 RVA: 0x0029CAD4 File Offset: 0x0029AED4
		public bool FromFemale { get; set; }

		// Token: 0x060063C4 RID: 25540 RVA: 0x0029CAE0 File Offset: 0x0029AEE0
		public void SetDefaultImmoral()
		{
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			ChaFileGameInfo fileGameInfo = base.ChaControl.fileGameInfo;
			int num = fileGameInfo.immoral;
			int num2 = fileGameInfo.flavorState[4];
			float num3 = Mathf.InverseLerp(statusProfile.DirtyImmoralMinMax.min, statusProfile.DirtyImmoralMinMax.max, (float)num2);
			float f = Mathf.Lerp(statusProfile.DirtyImmoralBuff.min, statusProfile.DirtyImmoralBuff.max, (float)num2);
			num += Mathf.RoundToInt(f);
			int num4 = 0;
			foreach (KeyValuePair<int, int> keyValuePair in fileGameInfo.hSkill)
			{
				if (keyValuePair.Value != -1)
				{
					num4++;
				}
			}
			num += Mathf.RoundToInt(statusProfile.ImmoralBuff * (float)num4);
			if (fileGameInfo.hSkill.ContainsValue(22))
			{
				num += statusProfile.LustImmoralBuff;
			}
			else if (fileGameInfo.hSkill.ContainsValue(11))
			{
				num += statusProfile.FiredBodyImmoralBuff;
			}
			this.SetStatus(6, (float)num);
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0029CC28 File Offset: 0x0029B028
		public void UpdateStatus(int actionID, int poseID)
		{
			this.ApplyActionResultParameter(actionID, poseID);
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			Temperature temperature = Singleton<Manager.Map>.Instance.Simulator.Temperature;
			if (temperature != Temperature.Hot)
			{
				if (temperature == Temperature.Cold)
				{
					this.AddStatus(0, statusProfile.ColdTemperatureValue);
				}
			}
			else
			{
				this.AddStatus(0, statusProfile.HotTemperatureValue);
			}
			if (this.AgentData.IsWet && !base.ChaControl.fileGameInfo.normalSkill.ContainsValue(4))
			{
				this.AddStatus(0, statusProfile.WetTemperatureRate);
			}
			if (this.AgentData.StatsTable[2] <= 0f)
			{
				this.AddFlavorSkill(5, statusProfile.StarveWarinessValue);
				this.AddFlavorSkill(6, statusProfile.StarveDarknessValue);
			}
			if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(24))
			{
				this.AddStatus(3, statusProfile.CursedPhysicalBuff);
				this.AddStatus(6, statusProfile.CursedImmoralBuff);
				int desireKey = Desire.GetDesireKey(Desire.Type.H);
				this.AddDesire(desireKey, statusProfile.CursedHBuff);
			}
			this.AddStatus(6, 1f);
			AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
			int id = sickState.ID;
			if (id != 1)
			{
				if (id == 3)
				{
					this.ApplySituationResultParameter(10);
				}
			}
			else
			{
				this.ApplySituationResultParameter(7);
			}
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x0029CD98 File Offset: 0x0029B198
		public void ApplyActionResultParameter(int actionID, int poseID)
		{
			Dictionary<int, Dictionary<int, ParameterPacket>> dictionary;
			if (!Singleton<Manager.Resources>.Instance.Action.ActionStatusResultTable.TryGetValue(actionID, out dictionary))
			{
				return;
			}
			Dictionary<int, ParameterPacket> parameters;
			if (!dictionary.TryGetValue(poseID, out parameters))
			{
				EventType key;
				UnityEx.ValueTuple<int, string> valueTuple;
				if (!global::Debug.isDebugBuild || !AIProject.Definitions.Action.EventTypeTable.TryGetValue(actionID, out key) || AIProject.Definitions.Action.NameTable.TryGetValue(key, out valueTuple))
				{
				}
				return;
			}
			this.ApplyParameters(parameters);
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0029CE08 File Offset: 0x0029B208
		public void ApplySituationResultParameter(int id)
		{
			Dictionary<int, ParameterPacket> parameters;
			if (!Singleton<Manager.Resources>.Instance.Action.SituationStatusResultTable.TryGetValue(id, out parameters))
			{
				return;
			}
			this.ApplyParameters(parameters);
			int id2 = this.AgentData.SickState.ID;
			if (id2 == 3)
			{
				if (this.AgentData.StatsTable[0] < base.ChaControl.fileGameInfo.tempBound.upper)
				{
					this.AgentData.SickState.ID = -1;
				}
			}
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0029CE98 File Offset: 0x0029B298
		private void ApplyParameters(Dictionary<int, ParameterPacket> parameters)
		{
			float num = 0f;
			foreach (KeyValuePair<int, ParameterPacket> keyValuePair in parameters)
			{
				num += keyValuePair.Value.Probability;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			ParameterPacket parameterPacket = null;
			foreach (KeyValuePair<int, ParameterPacket> keyValuePair2 in parameters)
			{
				if (num2 <= keyValuePair2.Value.Probability)
				{
					parameterPacket = keyValuePair2.Value;
					break;
				}
				num2 -= keyValuePair2.Value.Probability;
			}
			float rate = 1f;
			bool flag = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(5);
			if (flag)
			{
				rate = (float)((UnityEngine.Random.Range(0, 10) != 0) ? 1 : 2);
			}
			foreach (KeyValuePair<int, TriThreshold> param in parameterPacket.Parameters)
			{
				this.ApplyParameter(param, rate);
			}
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x0029D00C File Offset: 0x0029B40C
		private void ApplyParameter(KeyValuePair<int, TriThreshold> param, float rate)
		{
			int num = param.Key;
			if (num < 10)
			{
				float value = UnityEngine.Random.value;
				float value2 = (float)param.Value.Evaluate(value) * rate;
				this.AddStatus(num, value2);
			}
			else if (num < 100)
			{
				num -= 10;
				float value3 = UnityEngine.Random.value;
				float num2 = (float)param.Value.Evaluate(value3) * rate;
				bool flag = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(9);
				if (flag && num == 1)
				{
					num2 += (float)Singleton<Manager.Resources>.Instance.StatusProfile.ReliabilityGWife;
				}
				bool flag2 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(4);
				if (flag2 && num == 6)
				{
					num2 += (float)Singleton<Manager.Resources>.Instance.StatusProfile.DarknessReduceMaiden;
				}
				this.AddFlavorSkill(num, (int)num2);
			}
			else
			{
				num -= 100;
				float value4 = UnityEngine.Random.value;
				float num3 = (float)param.Value.Evaluate(value4) * rate;
				if (this.AgentData.DesireTable.ContainsKey(num))
				{
					float num4 = this.AgentData.DesireTable[num] + num3;
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					else if (num4 > 100f)
					{
						num4 = 100f;
					}
					this.SetDesire(num, num4);
					bool flag3 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(36);
					if (flag3 && num == Desire.GetDesireKey(Desire.Type.Bath))
					{
						this.AddStatus(1, Singleton<Manager.Resources>.Instance.StatusProfile.DebuffMoodInBathDesire);
					}
				}
			}
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x0029D1C8 File Offset: 0x0029B5C8
		public void ApplyFoodParameter(StuffItem item)
		{
			if (item == null)
			{
				return;
			}
			Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
			if (!Singleton<Manager.Resources>.Instance.GameInfo.FoodParameterTable.TryGetValue(item.CategoryID, out dictionary))
			{
				return;
			}
			Dictionary<int, FoodParameterPacket> parameters;
			if (!dictionary.TryGetValue(item.ID, out parameters))
			{
				return;
			}
			this.ApplyEatParameters(parameters);
			this.AgentData.ItemList.RemoveItem(item);
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x0029D22C File Offset: 0x0029B62C
		public void ApplyDrinkParameter(StuffItem item)
		{
			if (item == null)
			{
				return;
			}
			Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
			if (!Singleton<Manager.Resources>.Instance.GameInfo.DrinkParameterTable.TryGetValue(item.CategoryID, out dictionary))
			{
				return;
			}
			Dictionary<int, FoodParameterPacket> parameters;
			if (!dictionary.TryGetValue(item.ID, out parameters))
			{
				return;
			}
			if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(45))
			{
			}
			this.ApplyDrinkParameters(parameters);
			this.AgentData.ItemList.RemoveItem(item);
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x0029D2AC File Offset: 0x0029B6AC
		private void ApplyEatParameters(Dictionary<int, FoodParameterPacket> parameters)
		{
			float num = 0f;
			foreach (KeyValuePair<int, FoodParameterPacket> keyValuePair in parameters)
			{
				num += keyValuePair.Value.Probability;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			FoodParameterPacket foodParameterPacket = null;
			foreach (KeyValuePair<int, FoodParameterPacket> keyValuePair2 in parameters)
			{
				if (num2 <= keyValuePair2.Value.Probability)
				{
					foodParameterPacket = keyValuePair2.Value;
					break;
				}
				num2 -= keyValuePair2.Value.Probability;
			}
			float rate = 1f;
			bool flag = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(5);
			if (flag)
			{
				rate = (float)((UnityEngine.Random.Range(0, 10) != 0) ? 1 : 2);
			}
			foreach (KeyValuePair<int, TriThreshold> param in foodParameterPacket.Parameters)
			{
				this.ApplyParameter(param, rate);
			}
			if (this.AgentData.SickState.ID == -1)
			{
				int num3 = UnityEngine.Random.Range(0, 100);
				float num4 = foodParameterPacket.StomachacheRate;
				StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
				int flavor = base.ChaControl.fileGameInfo.flavorState[3];
				float num5 = AgentActor.FlavorVariation(statusProfile.StomachacheRateDebuffMinMax, statusProfile.StomachacheRateBuff, flavor);
				num4 += num5;
				if ((float)num3 < num4)
				{
					this.CauseStomachache();
				}
			}
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x0029D49C File Offset: 0x0029B89C
		private void ApplyDrinkParameters(Dictionary<int, FoodParameterPacket> parameters)
		{
			float num = 0f;
			foreach (KeyValuePair<int, FoodParameterPacket> keyValuePair in parameters)
			{
				num += keyValuePair.Value.Probability;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			FoodParameterPacket foodParameterPacket = null;
			foreach (KeyValuePair<int, FoodParameterPacket> keyValuePair2 in parameters)
			{
				if (num2 < keyValuePair2.Value.Probability)
				{
					foodParameterPacket = keyValuePair2.Value;
					break;
				}
				num2 -= keyValuePair2.Value.Probability;
			}
			float rate = 1f;
			bool flag = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(5);
			if (flag)
			{
				rate = (float)((UnityEngine.Random.Range(0, 10) != 0) ? 1 : 2);
			}
			foreach (KeyValuePair<int, TriThreshold> param in foodParameterPacket.Parameters)
			{
				this.ApplyParameter(param, rate);
			}
			if (this.AgentData.SickState.ID == -1)
			{
				int num3 = UnityEngine.Random.Range(0, 100);
				float num4 = foodParameterPacket.StomachacheRate;
				StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
				int flavor = base.ChaControl.fileGameInfo.flavorState[3];
				float num5 = AgentActor.FlavorVariation(statusProfile.StomachacheRateDebuffMinMax, statusProfile.StomachacheRateBuff, flavor);
				num4 += num5;
				if ((float)num3 < foodParameterPacket.StomachacheRate)
				{
					this.CauseStomachache();
				}
			}
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x0029D690 File Offset: 0x0029BA90
		private void CauseStomachache()
		{
			this.AgentData.SickState.ID = 1;
			int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
			this.SetDesire(desireKey, 100f);
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x0029D6C4 File Offset: 0x0029BAC4
		public void HealSickBySleep()
		{
			AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
			int id = sickState.ID;
			if (id != 1)
			{
				if (id != 3)
				{
					if (id == 4)
					{
						this.ApplySituationResultParameter(14);
						sickState.ID = -1;
					}
				}
				else
				{
					this.ApplySituationResultParameter(11);
					this.AgentData.HeatStrokeLockInfo.Lock = true;
					sickState.ID = -1;
				}
			}
			else
			{
				sickState.ID = -1;
			}
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x0029D744 File Offset: 0x0029BB44
		public float? GetMotivation(int key)
		{
			float value;
			if (!this.AgentData.MotivationTable.TryGetValue(key, out value))
			{
				return null;
			}
			return new float?(value);
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x0029D77C File Offset: 0x0029BB7C
		public bool SetMotivation(int key, float value)
		{
			Dictionary<int, float> motivationTable = this.AgentData.MotivationTable;
			float num;
			if (motivationTable == null || !motivationTable.TryGetValue(key, out num))
			{
				return false;
			}
			if (num == value)
			{
				return false;
			}
			motivationTable[key] = value;
			return true;
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x0029D7C0 File Offset: 0x0029BBC0
		public void AddMotivation(int key, float addValue)
		{
			float? motivation = this.GetMotivation(key);
			if (motivation == null)
			{
				return;
			}
			float value = motivation.Value + addValue;
			this.SetMotivation(key, value);
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x0029D7F8 File Offset: 0x0029BBF8
		private float GetAddRate(int key)
		{
			Dictionary<int, float> desireAddRateTable = Singleton<Manager.Resources>.Instance.GetDesireAddRateTable(0, Singleton<Manager.Map>.Instance.Simulator.TimeZone);
			if (desireAddRateTable == null)
			{
				return 0f;
			}
			float result;
			if (!desireAddRateTable.TryGetValue(key, out result))
			{
				return 0f;
			}
			return result;
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x0029D844 File Offset: 0x0029BC44
		public float? GetDesire(int key)
		{
			float value;
			if (!this.AgentData.DesireTable.TryGetValue(key, out value))
			{
				return null;
			}
			return new float?(value);
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x0029D87C File Offset: 0x0029BC7C
		public bool SetDesire(int key, float desireValue)
		{
			Dictionary<int, float> desireTable = this.AgentData.DesireTable;
			float num;
			if (desireTable == null || !desireTable.TryGetValue(key, out num))
			{
				return false;
			}
			if (num == desireValue)
			{
				return false;
			}
			desireTable[key] = desireValue;
			return true;
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x0029D8BD File Offset: 0x0029BCBD
		public void SetForceDesire(int key, float desireValue)
		{
			this.AgentData.DesireTable[key] = desireValue;
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x0029D8D4 File Offset: 0x0029BCD4
		public void AddDesire(int key, float addValue)
		{
			float? desire = this.GetDesire(key);
			if (desire == null)
			{
				return;
			}
			float desireValue = desire.Value + addValue;
			this.SetDesire(key, desireValue);
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x0029D90C File Offset: 0x0029BD0C
		public void MulDesire(int key, float mulValue)
		{
			float? desire = this.GetDesire(key);
			if (desire == null)
			{
				return;
			}
			float num = desire.Value * mulValue;
			num = Mathf.Max(0f, num);
			this.SetDesire(key, num);
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x0029D950 File Offset: 0x0029BD50
		public void ClearDesire(Desire.Type type)
		{
			int desireKey = Desire.GetDesireKey(type);
			this.SetDesire(desireKey, 0f);
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x0029D974 File Offset: 0x0029BD74
		private void UpdateDesire(TimeSpan deltaTime, Dictionary<int, AgentProfile.DiminuationRates> rate)
		{
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			ChaFileGameInfo fileGameInfo = base.ChaControl.fileGameInfo;
			int flavor = fileGameInfo.flavorState[0];
			int flavor2 = fileGameInfo.flavorState[1];
			int flavor3 = fileGameInfo.flavorState[2];
			int flavor4 = fileGameInfo.flavorState[3];
			int flavor5 = fileGameInfo.flavorState[4];
			int flavor6 = fileGameInfo.flavorState[5];
			int flavor7 = fileGameInfo.flavorState[7];
			int flavor8 = fileGameInfo.flavorState[6];
			bool flag = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(0);
			bool flag2 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(1);
			bool flag3 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(2);
			bool flag4 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(3);
			bool flag5 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(7);
			bool flag6 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(13);
			bool flag7 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(14);
			bool flag8 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(15);
			bool flag9 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(21);
			bool flag10 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(22);
			bool flag11 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(30);
			bool flag12 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(34);
			bool flag13 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(35);
			bool flag14 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(40);
			bool flag15 = base.ChaControl.fileGameInfo.normalSkill.ContainsValue(41);
			int desireKey = Desire.GetDesireKey(Desire.Type.Bath);
			int desireKey2 = Desire.GetDesireKey(Desire.Type.Sleep);
			int desireKey3 = Desire.GetDesireKey(Desire.Type.Break);
			int desireKey4 = Desire.GetDesireKey(Desire.Type.Gift);
			int desireKey5 = Desire.GetDesireKey(Desire.Type.Want);
			int desireKey6 = Desire.GetDesireKey(Desire.Type.Eat);
			int desireKey7 = Desire.GetDesireKey(Desire.Type.H);
			int desireKey8 = Desire.GetDesireKey(Desire.Type.Game);
			int desireKey9 = Desire.GetDesireKey(Desire.Type.Lonely);
			int desireKey10 = Desire.GetDesireKey(Desire.Type.Hunt);
			int desireKey11 = Desire.GetDesireKey(Desire.Type.Cook);
			int desireKey12 = Desire.GetDesireKey(Desire.Type.Animal);
			int desireKey13 = Desire.GetDesireKey(Desire.Type.Location);
			int desireKey14 = Desire.GetDesireKey(Desire.Type.Drink);
			float max = this.AgentData.StatsTable[5];
			int[] desireTableKeys = AgentData.DesireTableKeys;
			bool flag16 = false;
			foreach (int num in desireTableKeys)
			{
				float? desire = this.GetDesire(num);
				if (desire != null)
				{
					float addRate = this.GetAddRate(num);
					float num2;
					base.ChaControl.fileGameInfo.desireBuffVal.TryGetValue(num, out num2);
					float num3 = addRate + num2;
					if (num == desireKey && flag2)
					{
						num3 += statusProfile.BuffBath;
					}
					if (num == desireKey2)
					{
						float num4 = AgentActor.FlavorVariation(statusProfile.SleepSociabilityBuffMinMax, statusProfile.SleepSociabilityBuff, flavor7);
						num3 += num4;
						if (flag4)
						{
							num3 += statusProfile.BuffSleep;
						}
					}
					if (num == desireKey6)
					{
						float num5 = AgentActor.FlavorVariation(statusProfile.EatPheromoneDebuffMinMax, statusProfile.EatPheromoneDebuff, flavor);
						num3 += num5;
						float num6 = AgentActor.FlavorVariation(statusProfile.EatInstinctBuffMinMax, statusProfile.EatInstinctBuff, flavor4);
						num3 += num6;
						float num7 = AgentActor.FlavorVariation(statusProfile.EatDarknessDebuffMinMax, statusProfile.EatDarknessDebuff, flavor8);
						num3 += num7;
						if (flag8)
						{
							num3 += statusProfile.BuffEat;
						}
					}
					if (num == desireKey3)
					{
						float num8 = AgentActor.FlavorVariation(statusProfile.BreakReasonBuffMinMax, statusProfile.BreakReasonBuff, flavor3);
						num3 += num8;
						float num9 = AgentActor.FlavorVariation(statusProfile.BreakInstinctBuffMinMax, statusProfile.BreakInstinctBuff, flavor3);
						num3 += num9;
						if (flag13)
						{
							num3 += statusProfile.BuffBreak;
						}
					}
					if (num == desireKey4)
					{
						float num10 = AgentActor.FlavorVariation(statusProfile.GiftReliabilityBuffMinMax, statusProfile.GiftReliabilityBuff, flavor2);
						num3 += num10;
						if (flag5)
						{
							num3 += statusProfile.BuffGift;
						}
					}
					if (num == desireKey5)
					{
						float num11 = AgentActor.FlavorVariation(statusProfile.GimmeWarinessBuffMinMax, statusProfile.GimmeWarinessBuff, flavor6);
						num3 += num11;
						float num12 = AgentActor.FlavorVariation(statusProfile.GimmeDarknessBuffMinMax, statusProfile.GimmeDarknessBuff, flavor8);
						num3 += num12;
						if (flag6)
						{
							num3 += statusProfile.BuffGimme;
						}
					}
					if (num == desireKey9)
					{
						float num13 = AgentActor.FlavorVariation(statusProfile.LonelySociabilityBuffMinMax, statusProfile.LonelySociabilityBuff, flavor7);
						num3 += num13;
						if (flag11)
						{
							num3 += statusProfile.BuffLonely;
						}
						if (flag12)
						{
							num3 += statusProfile.BuffLonelySuperSense;
						}
					}
					if (num == desireKey7)
					{
						float num14 = AgentActor.FlavorVariation(statusProfile.HDirtyBuffMinMax, statusProfile.HDirtyBuff, flavor5);
						num3 += num14;
						if (flag10)
						{
							num3 += statusProfile.BuffH;
						}
					}
					if (num == desireKey10)
					{
						float num15 = AgentActor.FlavorVariation(statusProfile.SearchWarinessBuffMinMax, statusProfile.SearchWarinessBuff, flavor6);
						num3 += num15;
						float num16 = AgentActor.FlavorVariation(statusProfile.SearchDarknessDebuffMinMax, statusProfile.SearchDarknessDebuff, flavor8);
						num3 += num16;
						if (flag7)
						{
							num3 += statusProfile.BuffSearchTough;
						}
						if (flag15)
						{
							num3 += statusProfile.BuffSearch;
						}
					}
					if (num == desireKey8)
					{
						float num17 = AgentActor.FlavorVariation(statusProfile.PlayReasonDebuffMinMax, statusProfile.PlayReasonDebuff, flavor3);
						num3 += num17;
						float num18 = AgentActor.FlavorVariation(statusProfile.PlayInstinctBuffMinMax, statusProfile.PlayInstinctBuff, flavor4);
						num3 += num18;
						if (flag9)
						{
							num3 += statusProfile.BuffPlay;
						}
					}
					if (num == desireKey11)
					{
						float num19 = AgentActor.FlavorVariation(statusProfile.CookPheromoneBuffMinMax, statusProfile.CookPheromoneBuff, flavor);
						num3 += num19;
						if (flag)
						{
							num3 += statusProfile.BuffCook;
						}
					}
					if (num == desireKey12 && flag3)
					{
						num3 += statusProfile.BuffAnimal;
					}
					if (num == desireKey13 && flag14)
					{
						num3 += statusProfile.BuffLocation;
					}
					if (num == desireKey14)
					{
						float num20 = AgentActor.FlavorVariation(statusProfile.DrinkWarinessBuffMinMax, statusProfile.DrinkWarinessBuff, flavor6);
						num3 += num20;
					}
					num3 = Mathf.Max(0f, num3);
					float num21 = num3 / 60f;
					float desireValue = Mathf.Clamp(desire.Value + num21 * (float)deltaTime.TotalMinutes, 0f, 100f);
					if (this.SetDesire(num, desireValue))
					{
						flag16 = true;
					}
				}
				float? motivation = this.GetMotivation(num);
				if (motivation != null)
				{
					float valueRecovery = rate[num].valueRecovery;
					float value = Mathf.Clamp(motivation.Value + valueRecovery * (float)deltaTime.TotalMinutes, 0f, max);
					this.SetMotivation(num, value);
				}
			}
			float value2 = this.AgentData.TalkMotivation + Singleton<Manager.Resources>.Instance.AgentProfile.TalkMotivationDimRate.valueRecovery * (float)deltaTime.TotalMinutes;
			this.AgentData.TalkMotivation = Mathf.Clamp(value2, 0f, max);
			if (flag16)
			{
			}
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x0029E0E4 File Offset: 0x0029C4E4
		public static float FlavorVariation(Threshold minmax, Threshold threshold, int flavor)
		{
			float t = Mathf.InverseLerp(minmax.min, minmax.max, (float)flavor);
			return threshold.Lerp(t);
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x0029E110 File Offset: 0x0029C510
		public void SetStatus(int id, float value)
		{
			if (id == 1 && base.ChaControl.fileGameInfo.phase < 2)
			{
				this.AgentData.StatsTable[1] = (base.ChaControl.fileGameInfo.moodBound.lower + base.ChaControl.fileGameInfo.moodBound.upper) / 2f;
				return;
			}
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			if (id == 5)
			{
				float min;
				if (Singleton<Manager.Resources>.Instance.AgentProfile.MotivationMinValueTable.TryGetValue(base.ChaControl.fileParam.personality, out min))
				{
					value = Mathf.Clamp(value, min, 150f);
				}
				else
				{
					value = Mathf.Clamp(value, 0f, 150f);
				}
				this.AgentData.StatsTable[id] = value;
			}
			else if (id == 7)
			{
				base.ChaControl.fileGameInfo.morality = Mathf.Clamp((int)value, 0, 100);
			}
			else if (id == 1)
			{
				if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(25))
				{
					value += statusProfile.DebuffMood;
				}
				if (base.ChaControl.fileGameInfo.phase < 2)
				{
					this.AgentData.StatsTable[id] = Mathf.Clamp(value, base.ChaControl.fileGameInfo.moodBound.lower, base.ChaControl.fileGameInfo.moodBound.upper);
				}
				else
				{
					this.AgentData.StatsTable[id] = Mathf.Clamp(value, 0f, 100f);
				}
			}
			else if (id == 2)
			{
				this.AgentData.StatsTable[id] = Mathf.Clamp(value, 0f, 100f);
			}
			else
			{
				value = Mathf.Clamp(value, 0f, 100f);
				this.AgentData.StatsTable[id] = value;
			}
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x0029E31C File Offset: 0x0029C71C
		public void AddStatus(int id, float value)
		{
			ChaFileGameInfo fileGameInfo = base.ChaControl.fileGameInfo;
			if (id == 7)
			{
				int morality = fileGameInfo.morality;
				float value2 = (float)morality + value;
				this.SetStatus(id, value2);
			}
			else
			{
				float num = this.AgentData.StatsTable[id];
				float num2 = num + value;
				bool flag = fileGameInfo.normalSkill.ContainsValue(29);
				bool flag2 = fileGameInfo.normalSkill.ContainsValue(20);
				if (id == 3)
				{
					StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
					int flavor = fileGameInfo.flavorState[6];
					float num3 = AgentActor.FlavorVariation(statusProfile.DarknessPhysicalBuffMinMax, statusProfile.DarknessPhysicalBuff, flavor);
					num2 += num3;
				}
				if (id == 6 && flag2 && num2 > num)
				{
					num2 += Singleton<Manager.Resources>.Instance.StatusProfile.BuffImmoral;
				}
				if (flag && id == 2)
				{
					if (num < num2)
					{
						this.SetStatus(id, num2);
					}
				}
				else
				{
					this.SetStatus(id, num2);
				}
			}
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x0029E420 File Offset: 0x0029C820
		public bool CanRevRape()
		{
			bool flag = base.ChaControl.fileGameInfo.hSkill.ContainsValue(13);
			bool flag2 = base.ChaControl.fileGameInfo.flavorState[4] >= Singleton<Manager.Resources>.Instance.StatusProfile.RevRapeBorder;
			return flag && flag2;
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x0029E480 File Offset: 0x0029C880
		public void AddFlavorSkill(int id, int value)
		{
			if (this.AgentData == null)
			{
				return;
			}
			ChaFileGameInfo fileGameInfo = base.ChaControl.fileGameInfo;
			if (!fileGameInfo.flavorState.ContainsKey(id))
			{
				return;
			}
			if (this.AgentData.ParameterLock || Config.GameData.ParameterLock)
			{
				return;
			}
			int value2 = fileGameInfo.flavorState[id] + value;
			fileGameInfo.flavorState[id] = Mathf.Clamp(value2, 0, 99999);
			if (id == 4)
			{
				if (!fileGameInfo.isHAddTaii0 && fileGameInfo.flavorState[id] >= 100)
				{
					fileGameInfo.isHAddTaii0 = true;
				}
				if (!fileGameInfo.isHAddTaii1 && fileGameInfo.flavorState[id] >= 170)
				{
					fileGameInfo.isHAddTaii1 = true;
				}
			}
			int a = fileGameInfo.totalFlavor + value;
			fileGameInfo.totalFlavor = Mathf.Max(a, 0);
			this.AgentData.AddFlavorSkill(id, value);
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x0029E575 File Offset: 0x0029C975
		public void AddFlavorSkill(FlavorSkill.Type type, int value)
		{
			this.AddFlavorSkill((int)type, value);
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x0029E580 File Offset: 0x0029C980
		public void SetFlavorSkill(int id, int value)
		{
			if (this.AgentData == null)
			{
				return;
			}
			ChaFileGameInfo fileGameInfo = base.ChaControl.fileGameInfo;
			if (!fileGameInfo.flavorState.ContainsKey(id))
			{
				return;
			}
			if (this.AgentData.ParameterLock || Config.GameData.ParameterLock)
			{
				return;
			}
			int num = value - fileGameInfo.flavorState[id];
			fileGameInfo.flavorState[id] = Mathf.Clamp(value, 0, 99999);
			if (id == 4)
			{
				if (!fileGameInfo.isHAddTaii0 && fileGameInfo.flavorState[id] >= 100)
				{
					fileGameInfo.isHAddTaii0 = true;
				}
				if (!fileGameInfo.isHAddTaii1 && fileGameInfo.flavorState[id] >= 170)
				{
					fileGameInfo.isHAddTaii1 = true;
				}
			}
			int a = fileGameInfo.totalFlavor + num;
			fileGameInfo.totalFlavor = Mathf.Max(a, 0);
			this.AgentData.AddFlavorAdditionAmount(num);
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x0029E674 File Offset: 0x0029CA74
		public void SetFlavorSkill(FlavorSkill.Type type, int value)
		{
			this.SetFlavorSkill((int)type, value);
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x0029E67E File Offset: 0x0029CA7E
		public int GetFlavorSkill(int id)
		{
			if (base.ChaControl != null && base.ChaControl.fileGameInfo != null)
			{
				return base.ChaControl.fileGameInfo.flavorState[id];
			}
			return 0;
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x0029E6B9 File Offset: 0x0029CAB9
		public int GetFlavorSkill(FlavorSkill.Type type)
		{
			return this.GetFlavorSkill((int)type);
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x0029E6C4 File Offset: 0x0029CAC4
		private bool CanProgressPhase()
		{
			Dictionary<int, int> dictionary;
			int num;
			return this.AttitudeID == 0 && Singleton<Manager.Resources>.Instance.Action.PhaseExp.TryGetValue(base.ChaControl.fileParam.personality, out dictionary) && dictionary.TryGetValue(base.ChaControl.fileGameInfo.phase, out num) && num <= base.ChaControl.fileGameInfo.totalFlavor;
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x0029E740 File Offset: 0x0029CB40
		public void SetPhase(int phase)
		{
			int phase2 = base.ChaControl.fileGameInfo.phase;
			if (phase2 != phase)
			{
				if (global::Debug.isDebugBuild)
				{
				}
				base.ChaControl.fileGameInfo.phase = phase;
				if (phase != 2)
				{
					if (phase == 3)
					{
						KeyValuePair<int, int>[] array = (from pair in base.ChaControl.fileGameInfo.flavorState
						orderby pair.Value descending
						select pair).Take(2).ToArray<KeyValuePair<int, int>>();
						int key = array[0].Key;
						int key2 = array[1].Key;
						Dictionary<int, int> dictionary;
						int lifestyle;
						if (Singleton<Manager.Resources>.Instance.Action.LifestyleTable.TryGetValue(key, out dictionary) && dictionary.TryGetValue(key2, out lifestyle))
						{
							base.ChaControl.fileGameInfo.lifestyle = lifestyle;
						}
					}
				}
				else
				{
					KeyValuePair<int, int>[] array2 = (from pair in base.ChaControl.fileGameInfo.flavorState
					orderby pair.Value descending
					select pair).Take(3).ToArray<KeyValuePair<int, int>>();
					int num = 0;
					int num2 = 0;
					foreach (KeyValuePair<int, int> keyValuePair in array2)
					{
						Dictionary<int, ObtainItemInfo> dictionary2;
						if (Singleton<Manager.Resources>.Instance.Action.FlavorPickSkillTable.TryGetValue(keyValuePair.Key, out dictionary2))
						{
							int key3 = this.LotterySkill(dictionary2);
							ObtainItemInfo obtainItemInfo;
							if (dictionary2.TryGetValue(key3, out obtainItemInfo))
							{
								base.ChaControl.fileGameInfo.normalSkill[num++] = obtainItemInfo.Info.ItemID;
							}
						}
						if (Game.isAdd01 && Singleton<Manager.Resources>.Instance.Action.FlavorPickHSkillTable.TryGetValue(keyValuePair.Key, out dictionary2))
						{
							int key4 = this.LotterySkill(dictionary2);
							ObtainItemInfo obtainItemInfo2;
							if (dictionary2.TryGetValue(key4, out obtainItemInfo2))
							{
								base.ChaControl.fileGameInfo.hSkill[num2++] = obtainItemInfo2.Info.ItemID;
							}
						}
					}
					int morality = base.ChaControl.fileGameInfo.morality;
					Dictionary<int, ObtainItemInfo> dictionary4;
					if (morality >= 50)
					{
						Dictionary<int, ObtainItemInfo> dictionary3;
						if (Singleton<Manager.Resources>.Instance.Action.FlavorPickSkillTable.TryGetValue(17, out dictionary3))
						{
							int key5 = this.LotterySkill(dictionary3);
							ObtainItemInfo obtainItemInfo3;
							if (dictionary3.TryGetValue(key5, out obtainItemInfo3))
							{
								base.ChaControl.fileGameInfo.normalSkill[num++] = obtainItemInfo3.Info.ItemID;
							}
						}
					}
					else if (Singleton<Manager.Resources>.Instance.Action.FlavorPickSkillTable.TryGetValue(18, out dictionary4))
					{
						int key6 = this.LotterySkill(dictionary4);
						ObtainItemInfo obtainItemInfo4;
						if (dictionary4.TryGetValue(key6, out obtainItemInfo4))
						{
							base.ChaControl.fileGameInfo.normalSkill[num++] = obtainItemInfo4.Info.ItemID;
						}
					}
				}
			}
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x0029EA50 File Offset: 0x0029CE50
		private int LotterySkill(Dictionary<int, ObtainItemInfo> dic)
		{
			float num = 0f;
			foreach (KeyValuePair<int, ObtainItemInfo> keyValuePair in dic)
			{
				num += (float)keyValuePair.Value.Rate;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			int result = -1;
			foreach (KeyValuePair<int, ObtainItemInfo> keyValuePair2 in dic)
			{
				int rate = keyValuePair2.Value.Rate;
				if (num2 <= (float)rate)
				{
					result = keyValuePair2.Key;
					break;
				}
				num2 -= (float)rate;
			}
			return result;
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x0029EB34 File Offset: 0x0029CF34
		public void CauseSick()
		{
			AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
			if (sickState.ID == -1)
			{
				AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
				StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
				float num = this.AgentData.StatsTable[3];
				float num2 = this.AgentData.StatsTable[0];
				int flavor = base.ChaControl.fileGameInfo.flavorState[5];
				if (num <= 0f)
				{
					if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(14))
					{
						return;
					}
					sickState.ID = 2;
				}
				if (num2 <= agentProfile.ColdTempBorder)
				{
					if (this.AgentData.ColdLockInfo.Lock)
					{
						return;
					}
					float num3 = UnityEngine.Random.Range(0f, 100f);
					float num4 = statusProfile.ColdDefaultIncidence;
					float num5 = AgentActor.FlavorVariation(statusProfile.ColdRateBuffMinMax, statusProfile.ColdRateBuff, flavor);
					num4 += num5;
					if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(38))
					{
						num4 += statusProfile.ColdRateDebuffWeak;
					}
					if (num3 < num4)
					{
						sickState.ID = 0;
						this.AgentData.AddAppendEventFlagParam(2, 1);
					}
				}
				else if (num2 >= agentProfile.HotTempBorder)
				{
					if (this.AgentData.HeatStrokeLockInfo.Lock)
					{
						return;
					}
					float num6 = UnityEngine.Random.Range(0f, 100f);
					float num7 = statusProfile.HeatStrokeDefaultIncidence;
					float num8 = AgentActor.FlavorVariation(statusProfile.HeatStrokeRateBuffMinMax, statusProfile.HeatStrokeRateBuff, flavor);
					num7 += num8;
					if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(43))
					{
						num7 += statusProfile.HeatStrokeBuffGuts;
					}
					if (num6 < num7)
					{
						sickState.ID = 3;
					}
				}
			}
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x0029ED0C File Offset: 0x0029D10C
		private void OnSickUpdate(TimeSpan timeSpan)
		{
			AIProject.SaveData.Sickness sickState = this.AgentData.SickState;
			if (sickState.ID > -1 && this.Mode != Desire.ActionType.Onbu)
			{
				sickState.ElapsedTime += timeSpan;
				if (!Desire.ContainsSickFilterTable(this.Mode))
				{
					Manager.Resources instance = Singleton<Manager.Resources>.Instance;
					int id = sickState.ID;
					if (id != 0)
					{
						if (id == 2)
						{
							if (base.ChaControl.fileGameInfo.normalSkill.ContainsValue(14))
							{
								sickState.ID = -1;
								return;
							}
							if (this._navMeshAgent.isActiveAndEnabled && this.BehaviorResources.enabled && instance.AgentProfile.EncounterWhitelist.Contains(this.BehaviorResources.Mode))
							{
								float num = UnityEngine.Random.Range(0f, (float)TimeSpan.FromDays(1.0).TotalHours);
								sickState.Duration = sickState.ElapsedTime + TimeSpan.FromDays(2.0) + TimeSpan.FromHours((double)num);
								this.CommandPartner = null;
								this.AgentData.CarryingItem = null;
								this.ActivateTransfer(false);
								this.ReservedMode = Desire.ActionType.OverworkA;
								this.ChangeBehavior(Desire.ActionType.Faint);
								MapUIContainer.AddSystemLog(string.Format("{0}が倒れました", MapUIContainer.CharaNameColor(this)), true);
							}
						}
					}
					else if (sickState.ElapsedTime.Days >= 4)
					{
						if (this._navMeshAgent.isActiveAndEnabled && this.BehaviorResources.enabled && instance.AgentProfile.EncounterWhitelist.Contains(this.BehaviorResources.Mode))
						{
							this.CommandPartner = null;
							this.AgentData.CarryingItem = null;
							this.ActivateTransfer(false);
							this.ReservedMode = Desire.ActionType.Cold3A;
							this.ChangeBehavior(Desire.ActionType.Faint);
							MapUIContainer.AddSystemLog(string.Format("{0}が倒れました", MapUIContainer.CharaNameColor(this)), true);
							sickState.Duration = TimeSpan.FromDays(4.0) + TimeSpan.FromDays(2.0);
						}
					}
					else if (sickState.ElapsedTime.Days >= 2 && this._navMeshAgent.isActiveAndEnabled && this.BehaviorResources.enabled && instance.AgentProfile.EncounterWhitelist.Contains(this.BehaviorResources.Mode))
					{
						this.CommandPartner = null;
						this.AgentData.CarryingItem = null;
						this.ActivateTransfer(false);
						this.ReservedMode = Desire.ActionType.Cold2A;
						this.ChangeBehavior(Desire.ActionType.Faint);
						MapUIContainer.AddSystemLog(string.Format("{0}が倒れました", MapUIContainer.CharaNameColor(this)), true);
						sickState.Duration = TimeSpan.FromDays(2.0) + TimeSpan.FromDays(2.0);
					}
				}
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x0029EFF8 File Offset: 0x0029D3F8
		public bool IsYandere
		{
			get
			{
				if (!(base.ChaControl == null))
				{
					ChaFileControl chaFile = base.ChaControl.chaFile;
					if (((chaFile != null) ? chaFile.parameter : null) != null)
					{
						return base.ChaControl.fileParam.personality == 9;
					}
				}
				return false;
			}
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x0029F04C File Offset: 0x0029D44C
		public bool CheckYandereWarpPossibility()
		{
			if (!this.IsYandere)
			{
				return false;
			}
			IState state = Singleton<Manager.Map>.Instance.Player.Controller.State;
			if (!this.AgentData.Greeted || this.AgentData.YandereWarpLimitation || (!(state is Normal) && !(state is Houchi)))
			{
				this._imvisibleCount = 0f;
				return false;
			}
			if (base.ChaControl.IsVisibleInCamera)
			{
				this._imvisibleCount -= Time.deltaTime;
				this._imvisibleCount = Mathf.Max(0f, this._imvisibleCount);
				return false;
			}
			this._imvisibleCount += Time.deltaTime;
			return this._imvisibleCount >= Singleton<Manager.Resources>.Instance.AgentProfile.YandereWarpWaitTime;
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x0029F128 File Offset: 0x0029D528
		public void AbortTalkSeq(Desire.Type desire)
		{
			int desireKey = Desire.GetDesireKey(desire);
			this.SetDesire(desireKey, 0f);
			this.TargetInSightActor = null;
			this.CommandPartner = null;
			this.ChangeBehavior(Desire.ActionType.Normal);
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x0029F15E File Offset: 0x0029D55E
		// Note: this type is marked as 'beforefieldinit'.
		static AgentActor()
		{
			Actor.FovBodyPart[] array = new Actor.FovBodyPart[2];
			array[0] = Actor.FovBodyPart.Head;
			AgentActor._checkParts = array;
			AgentActor._waitMinute = new WaitForSeconds(1f);
		}

		// Token: 0x040056AF RID: 22191
		private CommandLabel.CommandInfo[] _labels;

		// Token: 0x040056B0 RID: 22192
		private CommandLabel.CommandInfo[] _pEventLabels;

		// Token: 0x040056B1 RID: 22193
		private CommandLabel.CommandInfo[] _pEventFishingLabels;

		// Token: 0x040056B2 RID: 22194
		private CommandLabel.CommandInfo[] _eventLabels;

		// Token: 0x040056B3 RID: 22195
		private CommandLabel.CommandInfo[] _sleepLabels;

		// Token: 0x040056B4 RID: 22196
		private CommandLabel.CommandInfo[] _bathLabels;

		// Token: 0x040056B5 RID: 22197
		private CommandLabel.CommandInfo[] _isuToiletLabels;

		// Token: 0x040056B6 RID: 22198
		private CommandLabel.CommandInfo[] _shagamiLabels;

		// Token: 0x040056B7 RID: 22199
		private CommandLabel.CommandInfo[] _shagamiFoundLabels;

		// Token: 0x040056B8 RID: 22200
		private CommandLabel.CommandInfo[] _searchLabels;

		// Token: 0x040056B9 RID: 22201
		private CommandLabel.CommandInfo[] _cookLabels;

		// Token: 0x040056BA RID: 22202
		private CommandLabel.CommandInfo[] _standLabels;

		// Token: 0x040056BB RID: 22203
		private CommandLabel.CommandInfo[] _mapBathLabels;

		// Token: 0x040056BC RID: 22204
		private CommandLabel.CommandInfo[] _holeLabels;

		// Token: 0x040056BD RID: 22205
		private CommandLabel.CommandInfo[] _sleepOnanismLabels;

		// Token: 0x040056BE RID: 22206
		private CommandLabel.CommandInfo[] _deskBackLabels;

		// Token: 0x040056BF RID: 22207
		private CommandLabel.CommandInfo[] _collapseLabels;

		// Token: 0x040056C0 RID: 22208
		private CommandLabel.CommandInfo[] _coldLabels;

		// Token: 0x040056C1 RID: 22209
		private CommandLabel.CommandInfo[] _tutorialPassFishingRodLabels;

		// Token: 0x040056C2 RID: 22210
		private CommandLabel.CommandInfo[] _tutorialFoodRequestLabels;

		// Token: 0x040056C3 RID: 22211
		private CommandLabel.CommandInfo[] _tutorialWaitAtBaseLabels;

		// Token: 0x040056C4 RID: 22212
		private CommandLabel.CommandInfo[] _tutorialGrilledFishRequestLabels;

		// Token: 0x040056C5 RID: 22213
		private CommandLabel.CommandInfo[] _dateLabels;

		// Token: 0x040056CE RID: 22222
		private List<AgentActor.TouchInfo> _touchList;

		// Token: 0x040056CF RID: 22223
		private List<AgentActor.ColDisposableInfo> _colDisposableList;

		// Token: 0x040056D0 RID: 22224
		private AgentActor.TouchDisposableInfo _disposableInfo = new AgentActor.TouchDisposableInfo();

		// Token: 0x040056D9 RID: 22233
		private RaycastHit[] _hits = new RaycastHit[10];

		// Token: 0x040056DA RID: 22234
		private ActionPoint _recoveryActionPointFromHScene;

		// Token: 0x040056DB RID: 22235
		private Desire.ActionType _recoveryModeFromHScene;

		// Token: 0x040056DC RID: 22236
		public bool _canTalkCache = true;

		// Token: 0x040056DD RID: 22237
		public int _attitudeIDCache;

		// Token: 0x040056DE RID: 22238
		public bool _useNeckLookCache = true;

		// Token: 0x040056DF RID: 22239
		public bool _canHCommandCache = true;

		// Token: 0x040056E0 RID: 22240
		public bool _isSpecialCache;

		// Token: 0x040056E1 RID: 22241
		public int _hPositionIDCache;

		// Token: 0x040056E2 RID: 22242
		public int _hPositionSubIDCache;

		// Token: 0x040056E3 RID: 22243
		public float _obstacleSizeCache;

		// Token: 0x040056E4 RID: 22244
		private Dictionary<int, System.Action> _presentParameterTable;

		// Token: 0x040056E5 RID: 22245
		private static readonly int[] _appendDayEventExID = new int[]
		{
			15,
			16,
			17,
			18,
			19,
			20
		};

		// Token: 0x040056E6 RID: 22246
		private AgentActor.LeaveAloneDisposableInfo _ladInfo;

		// Token: 0x040056E7 RID: 22247
		[SerializeField]
		private ActorAnimationAgent _animation;

		// Token: 0x040056E8 RID: 22248
		[SerializeField]
		private ActorLocomotionAgent _character;

		// Token: 0x040056E9 RID: 22249
		[SerializeField]
		private AgentController _controller;

		// Token: 0x040056EA RID: 22250
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private Vector3[] _positions;

		// Token: 0x040056EB RID: 22251
		private NavMeshPath _pathForCalc;

		// Token: 0x040056ED RID: 22253
		private State.Type _stateType = State.Type.Normal;

		// Token: 0x040056EF RID: 22255
		[SerializeField]
		private float _impossibleCooldownTime = 3f;

		// Token: 0x040056F0 RID: 22256
		private float _elapsedTimeFromLastImpossible;

		// Token: 0x040056F3 RID: 22259
		private bool _isStandby;

		// Token: 0x040056F4 RID: 22260
		private bool _prevCloseToPlayer;

		// Token: 0x040056F5 RID: 22261
		private float _distanceTweenPlayer = float.MaxValue;

		// Token: 0x040056F6 RID: 22262
		private float _heightDistTweenPlayer = float.MaxValue;

		// Token: 0x040056F7 RID: 22263
		private float _angleDiffTweenPlayer = float.MaxValue;

		// Token: 0x040056F8 RID: 22264
		[SerializeField]
		private ObjectLayer _layer = ObjectLayer.Command;

		// Token: 0x040056F9 RID: 22265
		public int SearchAreaID;

		// Token: 0x040056FB RID: 22267
		private Desire.ActionType _modeType;

		// Token: 0x0400570A RID: 22282
		private Desire.Type _requestedDesire;

		// Token: 0x0400570C RID: 22284
		private Dictionary<Actor, bool> _actorConsideredTable = new Dictionary<Actor, bool>();

		// Token: 0x0400570D RID: 22285
		private List<ActionPoint> _actionPoints = new List<ActionPoint>();

		// Token: 0x0400570E RID: 22286
		private ReadOnlyCollection<ActionPoint> _readonlyActionPoints;

		// Token: 0x04005711 RID: 22289
		private Dictionary<ActionPoint, bool> _actionPointCheckFlagTable = new Dictionary<ActionPoint, bool>();

		// Token: 0x04005712 RID: 22290
		private Dictionary<int, DateTimeThreshold[]> _desireDailyRhythm;

		// Token: 0x04005714 RID: 22292
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private ActionPoint _targetInSightActionPoint;

		// Token: 0x0400571A RID: 22298
		[SerializeField]
		private Threshold _coolTimeThreshold = new Threshold(0f, 1f);

		// Token: 0x0400571E RID: 22302
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private Actor _targetInSightActor;

		// Token: 0x04005720 RID: 22304
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private AnimalBase _targetInSightAnimal;

		// Token: 0x0400572A RID: 22314
		private UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType, bool> _modeCache = default(UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType, bool>);

		// Token: 0x0400572B RID: 22315
		private static readonly Actor.FovBodyPart[] _checkParts;

		// Token: 0x0400572E RID: 22318
		private AgentTimer _timer;

		// Token: 0x0400572F RID: 22319
		private float _maxMotivationInMasturbation;

		// Token: 0x04005730 RID: 22320
		private float _runtimeMotivationInMasturbation;

		// Token: 0x04005731 RID: 22321
		private List<string> abName;

		// Token: 0x04005732 RID: 22322
		private HParticleCtrl HParticle;

		// Token: 0x04005733 RID: 22323
		private HSeCtrl Hse;

		// Token: 0x04005734 RID: 22324
		private HItemCtrl HItem;

		// Token: 0x04005735 RID: 22325
		private GameObject HItemPlace;

		// Token: 0x04005736 RID: 22326
		private HSceneManager hSceneManager;

		// Token: 0x04005737 RID: 22327
		private HScene hScene;

		// Token: 0x04005738 RID: 22328
		private HSceneFlagCtrl hFlagCtrl;

		// Token: 0x04005739 RID: 22329
		private HVoiceCtrl HVoiceCtrl;

		// Token: 0x0400573A RID: 22330
		private YureCtrl.YureCtrlMapH HYureCtrl;

		// Token: 0x0400573B RID: 22331
		private HLayerCtrl HLayerCtrl;

		// Token: 0x0400573C RID: 22332
		private int yureID = -1;

		// Token: 0x0400573D RID: 22333
		private int HLayerID = -1;

		// Token: 0x0400573E RID: 22334
		private int HVoiceID = -1;

		// Token: 0x0400573F RID: 22335
		private List<IDisposable> MasturbationDisposables = new List<IDisposable>();

		// Token: 0x04005740 RID: 22336
		private float height = -1f;

		// Token: 0x04005741 RID: 22337
		private float breast = -1f;

		// Token: 0x04005742 RID: 22338
		private IEnumerator _masturbationEnumerator;

		// Token: 0x04005743 RID: 22339
		private IDisposable _masturbationDisposable;

		// Token: 0x04005744 RID: 22340
		private float _maxMotivationInLesbian;

		// Token: 0x04005745 RID: 22341
		private float _runtimeMotivationInLesbian;

		// Token: 0x04005746 RID: 22342
		private YureCtrl.YureCtrlMapH[] HYureCtrlLes = new YureCtrl.YureCtrlMapH[2];

		// Token: 0x04005747 RID: 22343
		private int[] yureLesID = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04005748 RID: 22344
		private int[] HLayerLesID = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04005749 RID: 22345
		private int HVoiceLesID = -1;

		// Token: 0x0400574A RID: 22346
		private ChaControl[] lesChars = new ChaControl[2];

		// Token: 0x0400574B RID: 22347
		private ActorAnimation[] lesCharAnimes = new ActorAnimation[2];

		// Token: 0x0400574C RID: 22348
		private List<IDisposable> lesbianDisposable = new List<IDisposable>();

		// Token: 0x0400574D RID: 22349
		private float[] timeMotions = new float[2];

		// Token: 0x0400574E RID: 22350
		private bool[] enableMotions = new bool[2];

		// Token: 0x0400574F RID: 22351
		private bool[] allowMotions = new bool[]
		{
			true,
			true
		};

		// Token: 0x04005750 RID: 22352
		private Vector2[] lerpMotions = new Vector2[]
		{
			Vector2.zero,
			Vector2.zero
		};

		// Token: 0x04005751 RID: 22353
		private float[] lerpTimes = new float[2];

		// Token: 0x04005752 RID: 22354
		protected float[] timeChangeMotions = new float[2];

		// Token: 0x04005753 RID: 22355
		protected float[] timeChangeMotionDeltaTimes = new float[2];

		// Token: 0x04005754 RID: 22356
		private IEnumerator _lesbianHEnumerator;

		// Token: 0x04005755 RID: 22357
		private IDisposable _lesbianHDisposable;

		// Token: 0x04005756 RID: 22358
		private IDisposable advEventResetDisposable;

		// Token: 0x04005757 RID: 22359
		private IDisposable _greetEventResetDisposable;

		// Token: 0x04005758 RID: 22360
		private IDisposable _yandereWarpRetryDisposable;

		// Token: 0x04005759 RID: 22361
		private List<Waypoint> _reservedWaypoints = new List<Waypoint>();

		// Token: 0x0400575B RID: 22363
		private List<Waypoint> _reservedNearActionWaypoints = new List<Waypoint>();

		// Token: 0x0400575D RID: 22365
		private List<Waypoint> _reservedDateNearActionWaypoints = new List<Waypoint>();

		// Token: 0x0400575F RID: 22367
		private List<Waypoint> _reservedLocationWaypoints = new List<Waypoint>();

		// Token: 0x04005761 RID: 22369
		private List<Waypoint> _reservedActorWaypoints = new List<Waypoint>();

		// Token: 0x04005763 RID: 22371
		private List<Waypoint> _reservedPlayerWaypoints = new List<Waypoint>();

		// Token: 0x04005765 RID: 22373
		private List<Waypoint> _reservedAnimalWaypoints = new List<Waypoint>();

		// Token: 0x04005769 RID: 22377
		private static readonly YieldInstruction _waitMinute;

		// Token: 0x0400576A RID: 22378
		private NavMeshPath _calcElementPath;

		// Token: 0x0400576B RID: 22379
		private IEnumerator _patrolCoroutine;

		// Token: 0x0400576C RID: 22380
		private IEnumerator _calcEnumerator;

		// Token: 0x0400576D RID: 22381
		private IEnumerator _additiveCalcProcess;

		// Token: 0x0400576E RID: 22382
		private IEnumerator _actionPatrolCoroutine;

		// Token: 0x0400576F RID: 22383
		private IEnumerator _actionCalcEnumerator;

		// Token: 0x04005770 RID: 22384
		private IEnumerator _additiveActionCalcProcess;

		// Token: 0x04005771 RID: 22385
		private IEnumerator _dateActionPatrolCoroutine;

		// Token: 0x04005772 RID: 22386
		private IEnumerator _dateActionCalcEnumerator;

		// Token: 0x04005773 RID: 22387
		private IEnumerator _additiveDateActionCalcProcess;

		// Token: 0x04005774 RID: 22388
		private IEnumerator _locationPatrolCoroutine;

		// Token: 0x04005775 RID: 22389
		private IEnumerator _locationCalcEnumerator;

		// Token: 0x04005776 RID: 22390
		private IEnumerator _additiveLocationCalcProcess;

		// Token: 0x04005777 RID: 22391
		private IEnumerator _actorPatrolCoroutine;

		// Token: 0x04005778 RID: 22392
		private IEnumerator _actorCalcEnumerator;

		// Token: 0x04005779 RID: 22393
		private IEnumerator _additiveActorCalcProcess;

		// Token: 0x0400577A RID: 22394
		private IEnumerator _playerPatrolCoroutine;

		// Token: 0x0400577B RID: 22395
		private IEnumerator _playerCalcEnumerator;

		// Token: 0x0400577C RID: 22396
		private IEnumerator _additivePlayerCalcProcess;

		// Token: 0x0400577E RID: 22398
		private IEnumerator _animalPatrolCoroutine;

		// Token: 0x0400577F RID: 22399
		private IEnumerator _animalCalcEnumerator;

		// Token: 0x04005780 RID: 22400
		private IEnumerator _additiveAnimalCalcProcess;

		// Token: 0x04005782 RID: 22402
		private float _imvisibleCount;

		// Token: 0x04005783 RID: 22403
		[CompilerGenerated]
		private static Func<bool> <>f__mg$cache0;

		// Token: 0x04005784 RID: 22404
		[CompilerGenerated]
		private static Func<bool> <>f__mg$cache1;

		// Token: 0x02000C4C RID: 3148
		public enum ADV_CATEGORY
		{
			// Token: 0x0400579D RID: 22429
			TALK,
			// Token: 0x0400579E RID: 22430
			INSTRUCTION,
			// Token: 0x0400579F RID: 22431
			SPEAK,
			// Token: 0x040057A0 RID: 22432
			PRESENT,
			// Token: 0x040057A1 RID: 22433
			IN,
			// Token: 0x040057A2 RID: 22434
			OUT,
			// Token: 0x040057A3 RID: 22435
			Event,
			// Token: 0x040057A4 RID: 22436
			Phase,
			// Token: 0x040057A5 RID: 22437
			Together,
			// Token: 0x040057A6 RID: 22438
			H,
			// Token: 0x040057A7 RID: 22439
			HScene,
			// Token: 0x040057A8 RID: 22440
			Sickness,
			// Token: 0x040057A9 RID: 22441
			Food,
			// Token: 0x040057AA RID: 22442
			AddEV,
			// Token: 0x040057AB RID: 22443
			Rand,
			// Token: 0x040057AC RID: 22444
			TUTORIAL = 100
		}

		// Token: 0x02000C4D RID: 3149
		public class PackData : CharaPackData
		{
			// Token: 0x17001416 RID: 5142
			// (set) Token: 0x060064CC RID: 25804 RVA: 0x002A17DC File Offset: 0x0029FBDC
			public CommCommandList.CommandInfo[] restoreCommands
			{
				set
				{
					this._restoreCommands = value;
				}
			}

			// Token: 0x17001417 RID: 5143
			// (get) Token: 0x060064CD RID: 25805 RVA: 0x002A17E5 File Offset: 0x0029FBE5
			// (set) Token: 0x060064CE RID: 25806 RVA: 0x002A17ED File Offset: 0x0029FBED
			private CommCommandList.CommandInfo[] _restoreCommands { get; set; }

			// Token: 0x17001418 RID: 5144
			// (get) Token: 0x060064CF RID: 25807 RVA: 0x002A17F6 File Offset: 0x0029FBF6
			// (set) Token: 0x060064D0 RID: 25808 RVA: 0x002A17FE File Offset: 0x0029FBFE
			public System.Action OnEndRefreshCommand { get; set; }

			// Token: 0x17001419 RID: 5145
			// (get) Token: 0x060064D1 RID: 25809 RVA: 0x002A1807 File Offset: 0x0029FC07
			// (set) Token: 0x060064D2 RID: 25810 RVA: 0x002A180F File Offset: 0x0029FC0F
			public int AttitudeID { get; set; }

			// Token: 0x1700141A RID: 5146
			// (get) Token: 0x060064D3 RID: 25811 RVA: 0x002A1818 File Offset: 0x0029FC18
			// (set) Token: 0x060064D4 RID: 25812 RVA: 0x002A1820 File Offset: 0x0029FC20
			public bool isFavoriteTarget { get; set; }

			// Token: 0x1700141B RID: 5147
			// (get) Token: 0x060064D5 RID: 25813 RVA: 0x002A1829 File Offset: 0x0029FC29
			// (set) Token: 0x060064D6 RID: 25814 RVA: 0x002A1831 File Offset: 0x0029FC31
			public string FavoriteTargetName { get; set; } = string.Empty;

			// Token: 0x1700141C RID: 5148
			// (get) Token: 0x060064D7 RID: 25815 RVA: 0x002A183A File Offset: 0x0029FC3A
			// (set) Token: 0x060064D8 RID: 25816 RVA: 0x002A1842 File Offset: 0x0029FC42
			public bool isThisPartner { get; set; } = true;

			// Token: 0x1700141D RID: 5149
			// (get) Token: 0x060064D9 RID: 25817 RVA: 0x002A184C File Offset: 0x0029FC4C
			public bool isSuccessFollow
			{
				get
				{
					ValData valData;
					return base.Vars != null && base.Vars.TryGetValue("isSuccessFollow", out valData) && (bool)valData.o;
				}
			}

			// Token: 0x1700141E RID: 5150
			// (get) Token: 0x060064DA RID: 25818 RVA: 0x002A1888 File Offset: 0x0029FC88
			public bool isSuccessH
			{
				get
				{
					ValData valData;
					return base.Vars != null && base.Vars.TryGetValue("isSuccessH", out valData) && (bool)valData.o;
				}
			}

			// Token: 0x1700141F RID: 5151
			// (get) Token: 0x060064DB RID: 25819 RVA: 0x002A18C4 File Offset: 0x0029FCC4
			public int hMode
			{
				get
				{
					ValData valData;
					if (base.Vars == null || !base.Vars.TryGetValue("hMode", out valData))
					{
						return -1;
					}
					return (int)valData.o;
				}
			}

			// Token: 0x060064DC RID: 25820 RVA: 0x002A1900 File Offset: 0x0029FD00
			public override List<Program.Transfer> Create()
			{
				List<Program.Transfer> list = base.Create();
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"int",
					"AttitudeID",
					this.AttitudeID.ToString()
				}));
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"bool",
					"isFavoriteTarget",
					this.isFavoriteTarget.ToString()
				}));
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"string",
					"FavoriteTargetName",
					this.FavoriteTargetName
				}));
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"bool",
					"isThisPartner",
					this.isThisPartner.ToString()
				}));
				return list;
			}

			// Token: 0x060064DD RID: 25821 RVA: 0x002A19F0 File Offset: 0x0029FDF0
			public override void Receive(TextScenario scenario)
			{
				base.Receive(scenario);
				CommCommandList commandList = MapUIContainer.CommandList;
				if (this._restoreCommands != null)
				{
					commandList.Refresh(this._restoreCommands, commandList.CanvasGroup, this.OnEndRefreshCommand);
				}
			}
		}

		// Token: 0x02000C4E RID: 3150
		private class LeaveAloneDisposableInfo
		{
			// Token: 0x060064DE RID: 25822 RVA: 0x002A1A2D File Offset: 0x0029FE2D
			public LeaveAloneDisposableInfo()
			{
				this._arrayDisposable = new SingleAssignmentDisposable[2];
			}

			// Token: 0x17001420 RID: 5152
			// (get) Token: 0x060064DF RID: 25823 RVA: 0x002A1A41 File Offset: 0x0029FE41
			// (set) Token: 0x060064E0 RID: 25824 RVA: 0x002A1A4B File Offset: 0x0029FE4B
			public SingleAssignmentDisposable Timer
			{
				get
				{
					return this._arrayDisposable[0];
				}
				set
				{
					this._arrayDisposable[0] = value;
				}
			}

			// Token: 0x17001421 RID: 5153
			// (get) Token: 0x060064E1 RID: 25825 RVA: 0x002A1A56 File Offset: 0x0029FE56
			// (set) Token: 0x060064E2 RID: 25826 RVA: 0x002A1A60 File Offset: 0x0029FE60
			public SingleAssignmentDisposable Wait
			{
				get
				{
					return this._arrayDisposable[1];
				}
				set
				{
					this._arrayDisposable[1] = value;
				}
			}

			// Token: 0x060064E3 RID: 25827 RVA: 0x002A1A6C File Offset: 0x0029FE6C
			public void End()
			{
				for (int i = 0; i < 2; i++)
				{
					SingleAssignmentDisposable singleAssignmentDisposable = this._arrayDisposable[i];
					if (singleAssignmentDisposable != null)
					{
						singleAssignmentDisposable.Dispose();
					}
					this._arrayDisposable[i] = null;
				}
			}

			// Token: 0x040057B3 RID: 22451
			private SingleAssignmentDisposable[] _arrayDisposable;
		}

		// Token: 0x02000C4F RID: 3151
		private class TouchDisposableInfo
		{
			// Token: 0x17001422 RID: 5154
			// (get) Token: 0x060064E5 RID: 25829 RVA: 0x002A1AB2 File Offset: 0x0029FEB2
			// (set) Token: 0x060064E6 RID: 25830 RVA: 0x002A1ABA File Offset: 0x0029FEBA
			public SingleAssignmentDisposable Wait { get; set; }

			// Token: 0x17001423 RID: 5155
			// (get) Token: 0x060064E7 RID: 25831 RVA: 0x002A1AC3 File Offset: 0x0029FEC3
			public bool Check
			{
				get
				{
					return this.Wait != null;
				}
			}

			// Token: 0x060064E8 RID: 25832 RVA: 0x002A1AD1 File Offset: 0x0029FED1
			public void End()
			{
				SingleAssignmentDisposable wait = this.Wait;
				if (wait != null)
				{
					wait.Dispose();
				}
				this.Wait = null;
			}
		}

		// Token: 0x02000C50 RID: 3152
		private class TouchInfo
		{
			// Token: 0x060064E9 RID: 25833 RVA: 0x002A1AF0 File Offset: 0x0029FEF0
			public TouchInfo(GameObject obj, Collider col, int layer)
			{
				this.Obj = obj;
				this.Col = col;
				this._enableCol = this.Col.enabled;
				this.Col.enabled = true;
				this.Layer = obj.layer;
				this.Obj.layer = layer;
			}

			// Token: 0x17001424 RID: 5156
			// (get) Token: 0x060064EA RID: 25834 RVA: 0x002A1B46 File Offset: 0x0029FF46
			// (set) Token: 0x060064EB RID: 25835 RVA: 0x002A1B4E File Offset: 0x0029FF4E
			public GameObject Obj { get; private set; }

			// Token: 0x17001425 RID: 5157
			// (get) Token: 0x060064EC RID: 25836 RVA: 0x002A1B57 File Offset: 0x0029FF57
			// (set) Token: 0x060064ED RID: 25837 RVA: 0x002A1B5F File Offset: 0x0029FF5F
			public Collider Col { get; private set; }

			// Token: 0x17001426 RID: 5158
			// (get) Token: 0x060064EE RID: 25838 RVA: 0x002A1B68 File Offset: 0x0029FF68
			// (set) Token: 0x060064EF RID: 25839 RVA: 0x002A1B70 File Offset: 0x0029FF70
			public int Layer { get; private set; }

			// Token: 0x060064F0 RID: 25840 RVA: 0x002A1B79 File Offset: 0x0029FF79
			private void Reset()
			{
				this.Col.enabled = this._enableCol;
				this.Obj.layer = this.Layer;
			}

			// Token: 0x040057B7 RID: 22455
			private bool _enableCol;
		}

		// Token: 0x02000C51 RID: 3153
		private class ColDisposableInfo
		{
			// Token: 0x060064F1 RID: 25841 RVA: 0x002A1B9D File Offset: 0x0029FF9D
			public ColDisposableInfo(Collider col, System.Action onTouch, System.Action onEnter, System.Action onExit)
			{
				this.Col = col;
				this._onTouch = onTouch;
				this._onEnter = onEnter;
				this._onExit = onExit;
			}

			// Token: 0x17001427 RID: 5159
			// (get) Token: 0x060064F2 RID: 25842 RVA: 0x002A1BC2 File Offset: 0x0029FFC2
			// (set) Token: 0x060064F3 RID: 25843 RVA: 0x002A1BCA File Offset: 0x0029FFCA
			public Collider Col { get; private set; }

			// Token: 0x060064F4 RID: 25844 RVA: 0x002A1BD4 File Offset: 0x0029FFD4
			public void Start()
			{
				this.End();
				this._disposableTouch = new SingleAssignmentDisposable();
				this._disposableTouch.Disposable = (from _ in this.Col.OnMouseDownAsObservable()
				where !EventSystem.current.IsPointerOverGameObject()
				select _).Subscribe(delegate(Unit _)
				{
					if (this._onTouch != null)
					{
						this._onTouch();
					}
				}).AddTo(this.Col);
				this._disposableEnter = new SingleAssignmentDisposable();
				this._disposableEnter.Disposable = (from _ in this.Col.OnMouseEnterAsObservable()
				where !EventSystem.current.IsPointerOverGameObject()
				select _).Subscribe(delegate(Unit _)
				{
					if (this._onEnter != null)
					{
						this._onEnter();
					}
				}).AddTo(this.Col);
				this._disposableExit = new SingleAssignmentDisposable();
				this._disposableExit.Disposable = (from _ in this.Col.OnMouseExitAsObservable()
				where !EventSystem.current.IsPointerOverGameObject()
				select _).Subscribe(delegate(Unit _)
				{
					if (this._onExit != null)
					{
						this._onExit();
					}
				}).AddTo(this.Col);
			}

			// Token: 0x060064F5 RID: 25845 RVA: 0x002A1D04 File Offset: 0x002A0104
			public void End()
			{
				if (this._disposableTouch != null)
				{
					this._disposableTouch.Dispose();
				}
				this._disposableTouch = null;
				if (this._disposableEnter != null)
				{
					this._disposableEnter.Dispose();
				}
				this._disposableEnter = null;
				if (this._disposableExit != null)
				{
					this._disposableExit.Dispose();
				}
				this._disposableExit = null;
			}

			// Token: 0x040057BA RID: 22458
			private System.Action _onTouch;

			// Token: 0x040057BB RID: 22459
			private System.Action _onEnter;

			// Token: 0x040057BC RID: 22460
			private System.Action _onExit;

			// Token: 0x040057BD RID: 22461
			private SingleAssignmentDisposable _disposableTouch;

			// Token: 0x040057BE RID: 22462
			private SingleAssignmentDisposable _disposableEnter;

			// Token: 0x040057BF RID: 22463
			private SingleAssignmentDisposable _disposableExit;
		}
	}
}
