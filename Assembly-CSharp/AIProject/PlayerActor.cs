using System;
using System.Collections;
using System.Collections.Generic;
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
using Cinemachine;
using IllusionUtility.GetUtility;
using Manager;
using RootMotion.FinalIK;
using UniRx;
using UnityEngine;
using UnityEx;
using UnityEx.Misc;

namespace AIProject
{
	// Token: 0x02000C6E RID: 3182
	public class PlayerActor : Actor
	{
		// Token: 0x06006766 RID: 26470 RVA: 0x002BE578 File Offset: 0x002BC978
		public bool IsBirthday(AgentActor agent)
		{
			if (agent != null)
			{
				if (agent.AgentData.IsPlayerForBirthdayEvent)
				{
					return false;
				}
				if (agent.ChaControl.fileGameInfo.phase < 3)
				{
					return false;
				}
			}
			DateTime now = DateTime.Now;
			int birthMonth = (int)base.ChaControl.fileParam.birthMonth;
			int birthDay = (int)base.ChaControl.fileParam.birthDay;
			return now.Month == birthMonth && now.Day == birthDay;
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06006767 RID: 26471 RVA: 0x002BE601 File Offset: 0x002BCA01
		// (set) Token: 0x06006768 RID: 26472 RVA: 0x002BE609 File Offset: 0x002BCA09
		public CommCommandList.CommandInfo[] SleepCommandInfos { get; private set; }

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06006769 RID: 26473 RVA: 0x002BE612 File Offset: 0x002BCA12
		// (set) Token: 0x0600676A RID: 26474 RVA: 0x002BE61A File Offset: 0x002BCA1A
		public CommCommandList.CommandInfo[] CookCommandInfos { get; private set; }

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x002BE623 File Offset: 0x002BCA23
		// (set) Token: 0x0600676C RID: 26476 RVA: 0x002BE62B File Offset: 0x002BCA2B
		public CommCommandList.CommandInfo[] ForgeCommandInfos { get; private set; }

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x0600676D RID: 26477 RVA: 0x002BE634 File Offset: 0x002BCA34
		// (set) Token: 0x0600676E RID: 26478 RVA: 0x002BE63C File Offset: 0x002BCA3C
		public CommCommandList.CommandInfo[] MixingCommandInfos { get; private set; }

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x002BE645 File Offset: 0x002BCA45
		// (set) Token: 0x06006770 RID: 26480 RVA: 0x002BE64D File Offset: 0x002BCA4D
		public CommCommandList.CommandInfo[] BaseCommandInfos { get; private set; }

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06006771 RID: 26481 RVA: 0x002BE656 File Offset: 0x002BCA56
		// (set) Token: 0x06006772 RID: 26482 RVA: 0x002BE65E File Offset: 0x002BCA5E
		public CommCommandList.CommandInfo[] DeviceCommandInfos { get; private set; }

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06006773 RID: 26483 RVA: 0x002BE667 File Offset: 0x002BCA67
		// (set) Token: 0x06006774 RID: 26484 RVA: 0x002BE66F File Offset: 0x002BCA6F
		public CommCommandList.CommandInfo[] ShipCommandInfos { get; private set; }

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06006775 RID: 26485 RVA: 0x002BE678 File Offset: 0x002BCA78
		// (set) Token: 0x06006776 RID: 26486 RVA: 0x002BE680 File Offset: 0x002BCA80
		public CommCommandList.CommandInfo[] ChickenCoopCommandInfos { get; private set; }

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06006777 RID: 26487 RVA: 0x002BE689 File Offset: 0x002BCA89
		// (set) Token: 0x06006778 RID: 26488 RVA: 0x002BE691 File Offset: 0x002BCA91
		public CommCommandList.CommandInfo[] CoSleepCommandInfos { get; private set; }

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06006779 RID: 26489 RVA: 0x002BE69A File Offset: 0x002BCA9A
		// (set) Token: 0x0600677A RID: 26490 RVA: 0x002BE6A2 File Offset: 0x002BCAA2
		public CommCommandList.CommandInfo[] DateEatCommandInfos { get; private set; }

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x0600677B RID: 26491 RVA: 0x002BE6AB File Offset: 0x002BCAAB
		// (set) Token: 0x0600677C RID: 26492 RVA: 0x002BE6B3 File Offset: 0x002BCAB3
		public CommCommandList.CommandInfo[] SpecialHCommandInfo { get; private set; }

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x002BE6BC File Offset: 0x002BCABC
		// (set) Token: 0x0600677E RID: 26494 RVA: 0x002BE6C4 File Offset: 0x002BCAC4
		public CommCommandList.CommandInfo[] ExitEatEventCommandInfo { get; private set; }

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x0600677F RID: 26495 RVA: 0x002BE6CD File Offset: 0x002BCACD
		// (set) Token: 0x06006780 RID: 26496 RVA: 0x002BE6D5 File Offset: 0x002BCAD5
		public CommCommandList.CommandInfo[] WarpCommandInfos { get; private set; }

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06006781 RID: 26497 RVA: 0x002BE6E0 File Offset: 0x002BCAE0
		public override bool IsNeutralCommand
		{
			get
			{
				if (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.MapShortcutUI != null)
				{
					return false;
				}
				if (Singleton<Manager.Map>.Instance.IsWarpProc)
				{
					return false;
				}
				bool flag = this.Mode != Desire.ActionType.Onbu;
				bool flag2 = this._controller.State is Normal;
				bool flag3 = this._controller.State is Houchi;
				bool flag4 = this._controller.State is WMap;
				return flag && (flag2 || flag3 || flag4);
			}
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x002BE781 File Offset: 0x002BCB81
		public void ResetCoolTime()
		{
			this._coolTime = 0f;
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06006783 RID: 26499 RVA: 0x002BE78E File Offset: 0x002BCB8E
		// (set) Token: 0x06006784 RID: 26500 RVA: 0x002BE796 File Offset: 0x002BCB96
		public int HPoseID { get; set; }

		// Token: 0x06006785 RID: 26501 RVA: 0x002BE7A0 File Offset: 0x002BCBA0
		private void InitializeLabels()
		{
			this.SleepCommandInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("そのまま寝転ぶ")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.PlayerController.ChangeState("Lie");
					}
				},
				new CommCommandList.CommandInfo("寝る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						ConfirmScene.Sentence = "一日を終了しますか？";
						ConfirmScene.OnClickedYes = delegate()
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
							this.ElapseTime(delegate()
							{
								int num10 = this.InvokeRelatedSleepEvent();
								return num10 != 1;
							}, delegate()
							{
								foreach (KeyValuePair<int, AgentActor> keyValuePair2 in Singleton<Manager.Map>.Instance.AgentTable)
								{
									if (keyValuePair2.Value.CheckEventADV(2))
									{
										keyValuePair2.Value.AdvEventStart_SleepingPlayer(this);
										return false;
									}
								}
								return true;
							});
							MapUIContainer.SetActiveCommandList(false);
							Singleton<Manager.Map>.Instance.Player.SetScheduledInteractionState(false);
							Singleton<Manager.Map>.Instance.Player.ReleaseInteraction();
							MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
						};
						ConfirmScene.OnClickedNo = delegate()
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						};
						Singleton<Game>.Instance.LoadDialog();
					}
				},
				new CommCommandList.CommandInfo("起きる")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						ActorAnimInfo animInfo = this.Animation.AnimInfo;
						base.ActivateNavMeshAgent();
						base.IsKinematic = false;
						base.SetStand(this.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
						this.Animation.RecoveryPoint = null;
						this.Animation.RefsActAnimInfo = true;
						this.Controller.ChangeState("Normal");
						this.ReleaseCurrentPoint();
						if (this.PlayerController.CommandArea != null)
						{
							this.PlayerController.CommandArea.enabled = true;
						}
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
						MapUIContainer.SetActiveCommandList(false);
					}
				}
			};
			this.CookCommandInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("料理をする")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.Controller.ChangeState("Cook");
						this.SetScheduledInteractionState(false);
					}
				},
				new CommCommandList.CommandInfo("貯蔵庫を見る")
				{
					Condition = (() => !this.ContainsExcludePantryID()),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.Controller.ChangeState("Pantry");
						this.SetScheduledInteractionState(false);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						MapUIContainer.SetActiveCommandList(false);
						MapUIContainer.SetVisibleHUDExceptStoryUI(true);
						MapUIContainer.StorySupportUI.Open();
						this.Controller.ChangeState("Normal");
						this.ReleaseCurrentPoint();
						if (this.PlayerController.CommandArea != null)
						{
							this.PlayerController.CommandArea.enabled = true;
						}
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
						base.ActivateNavMeshAgent();
						base.IsKinematic = false;
					}
				}
			};
			this.BaseCommandInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("ハウジング")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.Controller.ChangeState("Housing");
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						MapUIContainer.SetActiveCommandList(false);
						MapUIContainer.SetVisibleHUDExceptStoryUI(true);
						MapUIContainer.StorySupportUI.Open();
						this.Controller.ChangeState("Normal");
					}
				}
			};
			CommCommandList.CommandInfo[] array = new CommCommandList.CommandInfo[7];
			int num = 0;
			CommCommandList.CommandInfo commandInfo = new CommCommandList.CommandInfo("女の子を登場");
			commandInfo.Condition = delegate()
			{
				AgentData agentData;
				return Singleton<Game>.Instance.WorldData.AgentTable.TryGetValue(Singleton<Manager.Map>.Instance.AccessDeviceID, out agentData) && !agentData.PlayEnterScene;
			};
			commandInfo.Event = delegate(int x)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				MapUIContainer.SetActiveCommandList(false);
				this.PlayerController.ChangeState("EntryChara");
			};
			array[num] = commandInfo;
			int num2 = 1;
			commandInfo = new CommCommandList.CommandInfo("女の子を変更");
			commandInfo.Condition = delegate()
			{
				AgentData agentData;
				return !Singleton<Game>.Instance.WorldData.AgentTable.TryGetValue(Singleton<Manager.Map>.Instance.AccessDeviceID, out agentData) || agentData.PlayEnterScene;
			};
			commandInfo.Event = delegate(int x)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				MapUIContainer.SetActiveCommandList(false);
				this.PlayerController.ChangeState("EditChara");
			};
			array[num2] = commandInfo;
			int num3 = 2;
			commandInfo = new CommCommandList.CommandInfo("女の子の容姿変更");
			commandInfo.Condition = delegate()
			{
				AgentData agentData;
				return !Singleton<Game>.Instance.WorldData.AgentTable.TryGetValue(Singleton<Manager.Map>.Instance.AccessDeviceID, out agentData) || agentData.PlayEnterScene;
			};
			commandInfo.Event = delegate(int x)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				MapUIContainer.SetActiveCommandList(false);
				this.PlayerController.ChangeState("CharaLookEdit");
			};
			array[num3] = commandInfo;
			int num4 = 3;
			commandInfo = new CommCommandList.CommandInfo("女の子の住む島を変更");
			commandInfo.Condition = (() => Singleton<Game>.Instance.WorldData.Cleared);
			commandInfo.Event = delegate(int x)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				MapUIContainer.SetActiveCommandList(false);
				this.PlayerController.ChangeState("CharaMigration");
			};
			array[num4] = commandInfo;
			int num5 = 4;
			commandInfo = new CommCommandList.CommandInfo("主人公を変更");
			commandInfo.Condition = delegate()
			{
				AgentData agentData;
				return !Singleton<Game>.Instance.WorldData.AgentTable.TryGetValue(Singleton<Manager.Map>.Instance.AccessDeviceID, out agentData) || agentData.PlayEnterScene;
			};
			commandInfo.Event = delegate(int x)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				MapUIContainer.SetActiveCommandList(false);
				this.PlayerController.ChangeState("EditPlayer");
			};
			array[num5] = commandInfo;
			int num6 = 5;
			commandInfo = new CommCommandList.CommandInfo("主人公の容姿変更");
			commandInfo.Condition = delegate()
			{
				AgentData agentData;
				return !Singleton<Game>.Instance.WorldData.AgentTable.TryGetValue(Singleton<Manager.Map>.Instance.AccessDeviceID, out agentData) || agentData.PlayEnterScene;
			};
			commandInfo.Event = delegate(int x)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				MapUIContainer.SetActiveCommandList(false);
				this.PlayerController.ChangeState("PlayerLookEdit");
			};
			array[num6] = commandInfo;
			array[6] = new CommCommandList.CommandInfo("立ち去る")
			{
				Condition = null,
				Event = delegate(int x)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					Singleton<Manager.Map>.Instance.AccessDeviceID = -1;
					MapUIContainer.SetActiveCommandList(false);
					MapUIContainer.SetVisibleHUDExceptStoryUI(true);
					MapUIContainer.StorySupportUI.Open();
					this.Controller.ChangeState("Normal");
					this.CurrentDevicePoint = null;
				}
			};
			this.DeviceCommandInfos = array;
			List<CommCommandList.CommandInfo> list = ListPool<CommCommandList.CommandInfo>.Get();
			foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in Singleton<Manager.Resources>.Instance.Map.MapList)
			{
				int id = keyValuePair.Key;
				string mapName = keyValuePair.Value.name;
				CommCommandList.CommandInfo item = new CommCommandList.CommandInfo(mapName)
				{
					Condition = (() => Singleton<Manager.Map>.IsInstance() && id != Singleton<Manager.Map>.Instance.MapID),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						ConfirmScene.Sentence = string.Format("{0}に移動しますか？", mapName);
						ConfirmScene.OnClickedYes = delegate()
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
							MapUIContainer.SetActiveCommandList(false);
							this.SetScheduledInteractionState(false);
							this.ReleaseInteraction();
							MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
							Singleton<MapScene>.Instance.SaveProfile(true);
							Singleton<Manager.Map>.Instance.ChangeMap(id, null, delegate
							{
								this.PlayerController.ChangeState("Normal");
								this.CameraControl.EnabledInput = true;
								MapUIContainer.SetVisibleHUD(true);
								MapUIContainer.StorySupportUI.Open();
								if (this.PlayerController.CommandArea != null)
								{
									this.PlayerController.CommandArea.enabled = true;
								}
								MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
							});
						};
						ConfirmScene.OnClickedNo = delegate()
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						};
						Singleton<Game>.Instance.LoadDialog();
					}
				};
				list.Add(item);
			}
			list.Add(new CommCommandList.CommandInfo("立ち去る")
			{
				Condition = null,
				Event = delegate(int x)
				{
					MapUIContainer.SetActiveCommandList(false);
					MapUIContainer.StorySupportUI.Open();
					this.PlayerController.ChangeState("Normal");
				}
			});
			this.ShipCommandInfos = list.ToArray();
			ListPool<CommCommandList.CommandInfo>.Release(list);
			CommCommandList.CommandInfo[] array2 = new CommCommandList.CommandInfo[3];
			int num7 = 0;
			commandInfo = new CommCommandList.CommandInfo("タマゴ箱を確認");
			commandInfo.Condition = null;
			commandInfo.Event = delegate(int x)
			{
				MapUIContainer.CommandList.Visibled = false;
				MapUIContainer.SetActiveChickenCoopUI(true, ChickenCoopUI.Mode.EggBox);
			};
			array2[num7] = commandInfo;
			int num8 = 1;
			commandInfo = new CommCommandList.CommandInfo("ニワトリを追加");
			commandInfo.Condition = null;
			commandInfo.Event = delegate(int x)
			{
				MapUIContainer.CommandList.Visibled = false;
				MapUIContainer.SetActiveChickenCoopUI(true, ChickenCoopUI.Mode.Coop);
			};
			array2[num8] = commandInfo;
			array2[2] = new CommCommandList.CommandInfo("立ち去る")
			{
				Condition = null,
				Event = delegate(int x)
				{
					MapUIContainer.SetActiveCommandList(false);
					this.PlayerController.ChangeState("Normal");
				}
			};
			this.ChickenCoopCommandInfos = array2;
			this.WarpCommandInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("移動する")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						ConfirmScene.Sentence = "移動しますか";
						ConfirmScene.OnClickedYes = delegate()
						{
							MapUIContainer.SetActiveCommandList(false);
							this.SetScheduledInteractionState(false);
							this.ReleaseInteraction();
							MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
							string prevStateName = this.PlayerController.PrevStateName;
							WarpPoint warpPoint = base.CurrentPoint as WarpPoint;
							WarpPoint warpPoint2 = warpPoint.PairPoint();
							Singleton<Manager.Map>.Instance.WarpToPairPoint(warpPoint2, delegate
							{
								MapUIContainer.SetVisibleHUDExceptStoryUI(true);
								MapUIContainer.StorySupportUI.Open();
								if (prevStateName == "Onbu")
								{
									this.Controller.ChangeState("Onbu");
								}
								else
								{
									this.Controller.ChangeState("Normal");
								}
								this.Controller.ChangeState("Idle");
								GC.Collect();
							}, delegate
							{
								if (prevStateName == "Onbu")
								{
									this.Controller.ChangeState("Onbu");
								}
								else
								{
									this.Controller.ChangeState("Normal");
								}
								Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
								Singleton<Manager.Input>.Instance.SetupState();
								this.SetScheduledInteractionState(true);
								this.ReleaseInteraction();
								Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Warp_Out);
							});
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Warp_In);
						};
						ConfirmScene.OnClickedNo = delegate()
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						};
						Singleton<Game>.Instance.LoadDialog();
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.CancelWarp();
					}
				}
			};
			CommCommandList.CommandInfo[] array3 = new CommCommandList.CommandInfo[4];
			array3[0] = new CommCommandList.CommandInfo("エッチがしたい")
			{
				Condition = delegate
				{
					AgentActor agentPartner = this.AgentPartner;
					return agentPartner.CanSelectHCommand() && !agentPartner.IsBadMood();
				},
				Event = delegate(int x)
				{
					AgentActor agentPartner = this.AgentPartner;
					int personality = agentPartner.ChaControl.fileParam.personality;
					agentPartner.Animation.StopAllAnimCoroutine();
					this.AgentPartner.openData.FindLoad("1", personality, 9);
					this.AgentPartner.packData.AttitudeID = 1;
					this.AgentPartner.packData.onComplete = delegate()
					{
						if (this.AgentPartner.packData.isSuccessH)
						{
							this.AgentPartner.packData.restoreCommands = null;
							HSceneManager.SleepStart = true;
							this.InitiateHScene();
						}
						else
						{
							this.AgentPartner.packData.restoreCommands = this.CoSleepCommandInfos;
						}
					};
					Singleton<MapUIContainer>.Instance.OpenADV(this.AgentPartner.openData, this.AgentPartner.packData);
				}
			};
			array3[1] = new CommCommandList.CommandInfo("エッチなことをする")
			{
				Condition = delegate
				{
					if (base.ChaControl.sex == 1 && !base.ChaControl.fileParam.futanari)
					{
						return false;
					}
					AgentActor agentPartner = this.AgentPartner;
					return agentPartner.CanSelectHCommand() && agentPartner.IsBadMood();
				},
				Event = delegate(int x)
				{
					AgentActor partner = this.AgentPartner;
					int personality = partner.ChaControl.fileParam.personality;
					partner.Animation.StopAllAnimCoroutine();
					this.AgentPartner.openData.FindLoad("1", personality, 9);
					this.AgentPartner.packData.AttitudeID = 1;
					this.AgentPartner.packData.onComplete = delegate()
					{
						if (partner.packData.isSuccessH)
						{
							partner.packData.restoreCommands = null;
							HSceneManager.SleepStart = true;
							Singleton<HSceneManager>.Instance.isForce = true;
							this.InitiateHScene();
						}
						else
						{
							this.AgentPartner.packData.restoreCommands = this.CoSleepCommandInfos;
						}
					};
					Singleton<MapUIContainer>.Instance.OpenADV(this.AgentPartner.openData, this.AgentPartner.packData);
				}
			};
			int num9 = 2;
			commandInfo = new CommCommandList.CommandInfo("寝る");
			commandInfo.Condition = (() => Singleton<Manager.Map>.Instance.CanSleepInTime());
			commandInfo.Event = delegate(int x)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				ConfirmScene.Sentence = "一日を終了しますか？";
				ConfirmScene.OnClickedYes = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
					this.ElapseTime(delegate()
					{
						this.OnElapsedTimeFromDateSleep();
					}, true);
					MapUIContainer.SetActiveCommandList(false);
					this.SetScheduledInteractionState(false);
					this.ReleaseInteraction();
					MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			};
			array3[num9] = commandInfo;
			array3[3] = new CommCommandList.CommandInfo("起きる")
			{
				Condition = null,
				Event = delegate(int x)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					this.CameraControl.Mode = CameraMode.Normal;
					ActorAnimInfo animInfo = this.Animation.AnimInfo;
					base.ActivateNavMeshAgent();
					base.IsKinematic = false;
					base.SetStand(this.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
					this.Animation.RecoveryPoint = null;
					this.Animation.RefsActAnimInfo = true;
					this.Controller.ChangeState("Normal");
					this.ReleaseCurrentPoint();
					if (this.PlayerController.CommandArea != null)
					{
						this.PlayerController.CommandArea.enabled = true;
					}
					AgentActor agentPartner = this.AgentPartner;
					if (agentPartner != null)
					{
						agentPartner.ActivateNavMeshAgent();
						agentPartner.SetActiveOnEquipedItem(true);
						ActorAnimInfo animInfo2 = agentPartner.Animation.AnimInfo;
						agentPartner.SetStand(agentPartner.Animation.RecoveryPoint, animInfo2.outEnableBlend, animInfo2.outBlendSec, animInfo2.directionType);
						if (this.OldEnabledHoldingHand)
						{
							this.HandsHolder.enabled = true;
							this.OldEnabledHoldingHand = false;
						}
						agentPartner.ResetActionFlag();
						agentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Date);
					}
					MapUIContainer.SetActiveCommandList(false);
					MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
					MapUIContainer.SetVisibleHUDExceptStoryUI(true);
					MapUIContainer.StorySupportUI.Open();
				}
			};
			this.CoSleepCommandInfos = array3;
			this.DateEatCommandInfos = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("一緒にご飯を食べる")
				{
					Condition = null,
					Event = delegate(int x)
					{
						this.PlayerData.DateEatTrigger = true;
						MapUIContainer.CommandList.Visibled = false;
						AgentActor partner = this.AgentPartner;
						int personality = partner.ChaControl.fileParam.personality;
						partner.Animation.StopAllAnimCoroutine();
						partner.packData.Init();
						string asset = (partner.ChaControl.fileGameInfo.phase < 2) ? "0" : "1";
						partner.openData.FindLoad(asset, personality, 8);
						partner.packData.onComplete = delegate()
						{
							partner.packData.restoreCommands = null;
							partner.packData.Release();
							this.PlayerData.DateEatTrigger = true;
							this.CameraControl.Mode = CameraMode.Normal;
							this.Controller.ChangeState("Normal");
							partner.ActivateNavMeshAgent();
							partner.SetActiveOnEquipedItem(true);
							ActorAnimInfo animInfo = partner.Animation.AnimInfo;
							partner.SetStand(partner.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
							if (this.OldEnabledHoldingHand)
							{
								this.HandsHolder.enabled = true;
								this.OldEnabledHoldingHand = false;
							}
							partner.BehaviorResources.ChangeMode(Desire.ActionType.Date);
							MapUIContainer.SetActiveCommandList(false);
							partner.AgentData.SetAppendEventFlagCheck(3, true);
							partner.AgentData.AddAppendEventFlagParam(1, 1);
						};
						Singleton<MapUIContainer>.Instance.OpenADV(partner.openData, partner.packData);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						this.CameraControl.Mode = CameraMode.Normal;
						this.CancelCommand();
					}
				}
			};
			this.SpecialHCommandInfo = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("分娩台")
				{
					Condition = (() => this.HPoseID == 2),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("木馬")
				{
					Condition = (() => this.HPoseID == 3),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("拘束台鞍馬")
				{
					Condition = (() => this.HPoseID == 4),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("ギロチン")
				{
					Condition = (() => this.HPoseID == 5),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("拘束デンマ台")
				{
					Condition = (() => this.HPoseID == 6),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("拘束機械姦")
				{
					Condition = (() => this.HPoseID == 7),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("吊るし挿入")
				{
					Condition = (() => this.HPoseID == 8),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("拘束具")
				{
					Condition = (() => this.HPoseID == 9),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(-1);
					}
				},
				new CommCommandList.CommandInfo("触手")
				{
					Condition = (() => this.HPoseID == 10),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(0);
					}
				},
				new CommCommandList.CommandInfo("拘束椅子")
				{
					Condition = (() => this.HPoseID == 11),
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						MapUIContainer.SetActiveCommandList(false);
						this.StartSpecialH(-1);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						this.Controller.ChangeState("Normal");
						MapUIContainer.SetActiveCommandList(false);
					}
				}
			};
			this.ExitEatEventCommandInfo = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("一緒にご飯を食べる")
				{
					Condition = null,
					Event = delegate(int x)
					{
						AgentActor partner = this.AgentPartner;
						int personality = partner.ChaControl.fileParam.personality;
						partner.Animation.StopAllAnimCoroutine();
						OpenData openData = partner.openData;
						AgentActor.PackData packData = partner.packData;
						packData.Init();
						string asset = (partner.ChaControl.fileGameInfo.phase < 2) ? "0" : "1";
						openData.FindLoad(asset, personality, 8);
						packData.onComplete = delegate()
						{
							packData.restoreCommands = null;
							packData.Release();
							packData.Release();
							Singleton<ADV>.Instance.Captions.CanvasGroupOFF();
							Singleton<MapUIContainer>.Instance.advScene.gameObject.SetActive(false);
							partner.AgentData.SetAppendEventFlagCheck(3, true);
							partner.AgentData.AddAppendEventFlagParam(1, 1);
							this.ExitEatEventADV();
						};
						Singleton<MapUIContainer>.Instance.OpenADV(partner.openData, partner.packData);
						MapUIContainer.SetActiveCommandList(false);
						this.SetScheduledInteractionState(false);
						this.ReleaseInteraction();
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Condition = null,
					Event = delegate(int x)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						this.ExitEatEventADV();
						MapUIContainer.SetActiveCommandList(false);
						this.SetScheduledInteractionState(false);
						this.ReleaseInteraction();
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
					}
				}
			};
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x002BF0BC File Offset: 0x002BD4BC
		public void CancelCommand()
		{
			this.Controller.ChangeState("Normal");
			AgentActor agentPartner = this.AgentPartner;
			if (agentPartner != null)
			{
				agentPartner.ActivateNavMeshAgent();
				agentPartner.SetActiveOnEquipedItem(true);
				ActorAnimInfo animInfo = agentPartner.Animation.AnimInfo;
				agentPartner.SetStand(agentPartner.Animation.RecoveryPoint, animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.directionType);
				if (this.OldEnabledHoldingHand)
				{
					this.HandsHolder.enabled = true;
					this.OldEnabledHoldingHand = false;
				}
				agentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Date);
			}
			MapUIContainer.SetActiveCommandList(false);
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x002BF15C File Offset: 0x002BD55C
		public void CancelWarp()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			MapUIContainer.SetActiveCommandList(false);
			MapUIContainer.SetVisibleHUDExceptStoryUI(true);
			MapUIContainer.StorySupportUI.Open();
			if (this.PlayerController.PrevStateName == "Onbu")
			{
				this.PlayerController.ChangeState(this.PlayerController.PrevStateName);
			}
			else
			{
				this.PlayerController.ChangeState("Normal");
			}
			this.ReleaseCurrentPoint();
			if (this.PlayerController.CommandArea != null)
			{
				this.PlayerController.CommandArea.enabled = true;
			}
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			base.ActivateNavMeshAgent();
			base.IsKinematic = false;
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x002BF214 File Offset: 0x002BD614
		public void CallProc()
		{
			if (this._called)
			{
				return;
			}
			List<Desire.ActionType> encounterWhitelist = Singleton<Manager.Resources>.Instance.AgentProfile.EncounterWhitelist;
			float durationCTForCall = Singleton<Manager.Resources>.Instance.AgentProfile.DurationCTForCall;
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			MapArea mapArea = base.MapArea;
			int? num = (mapArea != null) ? new int?(mapArea.ChunkID) : null;
			int num2 = (num == null) ? this.PlayerData.ChunkID : num.Value;
			foreach (KeyValuePair<int, AgentActor> keyValuePair in Singleton<Manager.Map>.Instance.AgentTable)
			{
				if (keyValuePair.Value.Mode != Desire.ActionType.Called && keyValuePair.Value.Mode != Desire.ActionType.Date && keyValuePair.Value.Mode != Desire.ActionType.Onbu && keyValuePair.Value.Mode != Desire.ActionType.Cold2A && keyValuePair.Value.Mode != Desire.ActionType.Cold2B && keyValuePair.Value.Mode != Desire.ActionType.Cold3A && keyValuePair.Value.Mode != Desire.ActionType.Cold3B && keyValuePair.Value.Mode != Desire.ActionType.OverworkA && keyValuePair.Value.Mode != Desire.ActionType.OverworkB && keyValuePair.Value.Mode != Desire.ActionType.Cold2BMedicated && keyValuePair.Value.Mode != Desire.ActionType.Cold3BMedicated && keyValuePair.Value.Mode != Desire.ActionType.WeaknessA && keyValuePair.Value.Mode != Desire.ActionType.WeaknessB && keyValuePair.Value.Mode != Desire.ActionType.FoundPeeping)
				{
					if (keyValuePair.Value.EventKey != EventType.Sleep && keyValuePair.Value.EventKey != EventType.Toilet && keyValuePair.Value.EventKey != EventType.Bath && keyValuePair.Value.EventKey != EventType.Move && keyValuePair.Value.EventKey != EventType.Masturbation && keyValuePair.Value.EventKey != EventType.Lesbian)
					{
						if (!(keyValuePair.Value.Partner != null))
						{
							MapArea mapArea2 = keyValuePair.Value.MapArea;
							int? num3 = (mapArea2 != null) ? new int?(mapArea2.ChunkID) : null;
							int num4 = (num3 == null) ? keyValuePair.Value.AgentData.ChunkID : num3.Value;
							ChaFileGameInfo fileGameInfo = keyValuePair.Value.ChaControl.fileGameInfo;
							AgentData agentData = keyValuePair.Value.AgentData;
							float num5 = statusProfile.CallProbBaseRate;
							num5 += statusProfile.CallProbPhaseRate * (float)(fileGameInfo.phase + 1);
							float num6 = 0f;
							int num7 = fileGameInfo.flavorState[1];
							int num8 = 0;
							foreach (int num9 in statusProfile.CallReliabilityBorder)
							{
								if (num7 < num9)
								{
									break;
								}
								num8++;
							}
							num6 += statusProfile.CallReliabilityBuff[num8];
							float num10 = agentData.StatsTable[1];
							if (num10 < fileGameInfo.moodBound.lower)
							{
								num6 += statusProfile.CallLowerMoodProb;
							}
							else if (num10 > fileGameInfo.moodBound.upper)
							{
								num6 += statusProfile.CallUpperMoodProb;
							}
							if (keyValuePair.Value.ChaControl.fileGameInfo.normalSkill.ContainsValue(34))
							{
								num6 += statusProfile.CallProbSuperSense;
							}
							else if (agentData.CallCTCount < durationCTForCall)
							{
								agentData.CalledCount++;
								if (agentData.CalledCount == 2)
								{
									num6 += statusProfile.CallSecondTimeProb;
								}
								else if (agentData.CalledCount >= 3)
								{
									num6 += statusProfile.CallOverTimeProb;
								}
							}
							else
							{
								agentData.CallCTCount = 0f;
								agentData.CalledCount++;
							}
							float num11 = num5 + num6;
							if (!keyValuePair.Value.ChaControl.fileGameInfo.normalSkill.ContainsValue(34) && num4 != num2)
							{
								num11 *= 0.5f;
							}
							float value = UnityEngine.Random.value;
							bool flag = value < num11;
							if (flag)
							{
								Actor value2 = keyValuePair.Value;
								int num12 = -1;
								keyValuePair.Value.PoseID = num12;
								value2.ActionID = num12;
								agentData.CarryingItem = null;
								keyValuePair.Value.StateType = State.Type.Normal;
								if (keyValuePair.Value.CurrentPoint != null)
								{
									keyValuePair.Value.CurrentPoint.SetActiveMapItemObjs(true);
									keyValuePair.Value.CurrentPoint.ReleaseSlot(keyValuePair.Value);
									keyValuePair.Value.CurrentPoint = null;
								}
								if (keyValuePair.Value.CommandPartner != null)
								{
									Actor commandPartner = keyValuePair.Value.CommandPartner;
									if (commandPartner is AgentActor)
									{
										AgentActor agentActor = commandPartner as AgentActor;
										agentActor.CommandPartner = null;
										agentActor.ChangeBehavior(Desire.ActionType.Normal);
									}
									else if (commandPartner is MerchantActor)
									{
										MerchantActor merchantActor = commandPartner as MerchantActor;
										merchantActor.CommandPartner = null;
										merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
									}
									keyValuePair.Value.CommandPartner = null;
								}
								keyValuePair.Value.EventKey = (EventType)0;
								keyValuePair.Value.TargetInSightActionPoint = null;
								keyValuePair.Value.TargetInSightActor = this;
								keyValuePair.Value.CommandPartner = null;
								keyValuePair.Value.ResetActionFlag();
								if (keyValuePair.Value.Schedule.enabled)
								{
									Actor.BehaviorSchedule schedule = keyValuePair.Value.Schedule;
									schedule.enabled = false;
									keyValuePair.Value.Schedule = schedule;
								}
								keyValuePair.Value.ActivateNavMeshAgent();
								keyValuePair.Value.ActivateTransfer(true);
								keyValuePair.Value.ClearItems();
								keyValuePair.Value.ClearParticles();
								keyValuePair.Value.Animation.ResetDefaultAnimatorController();
								keyValuePair.Value.ChangeBehavior(Desire.ActionType.Called);
							}
						}
					}
				}
			}
			this._called = true;
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Call);
			MapUIContainer.AddSystemLog("全員を呼び出しました", true);
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x002BF8D8 File Offset: 0x002BDCD8
		public bool ContainsExcludePantryID()
		{
			if (base.CurrentPoint == null)
			{
				return false;
			}
			List<int> exPantryCommandActPTIDs = Singleton<Manager.Resources>.Instance.PlayerProfile.ExPantryCommandActPTIDs;
			if (!base.CurrentPoint.IDList.IsNullOrEmpty<int>())
			{
				foreach (int item in base.CurrentPoint.IDList)
				{
					if (exPantryCommandActPTIDs.Contains(item))
					{
						return true;
					}
				}
				return false;
			}
			return exPantryCommandActPTIDs.Contains(base.CurrentPoint.ID);
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x002BF96A File Offset: 0x002BDD6A
		public void InitiateHScene()
		{
			Singleton<HSceneManager>.Instance.HsceneEnter(this.AgentPartner, -1, null, HSceneManager.HEvent.Normal);
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x002BF980 File Offset: 0x002BDD80
		public void ElapseTime(System.Action action, bool fadeOut = true)
		{
			if (this._disposable != null)
			{
				this._disposable.Dispose();
			}
			this._enumerator = this.ElapseTimeCoroutine(action, fadeOut);
			this._disposable = Observable.FromCoroutine(() => this._enumerator, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x0600678C RID: 26508 RVA: 0x002BFA0A File Offset: 0x002BDE0A
		public bool ProcessingTimeSkip
		{
			get
			{
				return this._enumerator != null;
			}
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x002BFA18 File Offset: 0x002BDE18
		private IEnumerator ElapseTimeCoroutine(System.Action action, bool fadeOut = true)
		{
			IObservable<Unit> stream = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 3f, true) ?? Observable.Empty<Unit>();
			yield return stream.ToYieldInstruction<Unit>();
			if (action != null)
			{
				action();
			}
			Singleton<Manager.Map>.Instance.SetVisibleAll(false);
			DateTime now = Singleton<Manager.Map>.Instance.Simulator.Now;
			PlayerProfile playerProfile = Singleton<Manager.Resources>.Instance.PlayerProfile;
			EnvironmentSimulator.DateTimeSerialization wakeTime = playerProfile.WakeTime;
			if (now > wakeTime.Time && now < PlayerActor._midnightTime)
			{
				wakeTime.Time = wakeTime.Time.AddDays(1.0);
			}
			float elapsedTime = 0f;
			TimeSpan sleepDuration = wakeTime.Time - now;
			this._elapsedTimeInSleep = TimeSpan.Zero;
			float prevDayLength = Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute;
			Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute = prevDayLength / 8f;
			Time.timeScale = 20f;
			while (this._elapsedTimeInSleep < sleepDuration)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			Time.timeScale = 1f;
			Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute = prevDayLength;
			Singleton<Manager.Map>.Instance.SetVisibleAll(true);
			if (fadeOut)
			{
				stream = (MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 3f, true) ?? Observable.Empty<Unit>());
				yield return stream.ToYieldInstruction<Unit>();
			}
			this._enumerator = null;
			yield break;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x002BFA44 File Offset: 0x002BDE44
		public void ElapseTime(Func<bool> conditionBefore, Func<bool> conditionAfter = null)
		{
			if (this._disposable != null)
			{
				this._disposable.Dispose();
			}
			this._enumerator = this.ElapseTimeCoroutine(conditionBefore, conditionAfter);
			this._disposable = Observable.FromCoroutine(() => this._enumerator, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x002BFAD0 File Offset: 0x002BDED0
		private IEnumerator ElapseTimeCoroutine(Func<bool> conditionBefore, Func<bool> conditionAfter)
		{
			IObservable<Unit> stream = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 3f, true) ?? Observable.Empty<Unit>();
			yield return stream.ToYieldInstruction<Unit>();
			bool? flag = (conditionBefore != null) ? new bool?(conditionBefore()) : null;
			bool enableSkip = flag != null && flag.Value;
			if (!enableSkip)
			{
				yield break;
			}
			Singleton<Manager.Map>.Instance.SetVisibleAll(false);
			DateTime now = Singleton<Manager.Map>.Instance.Simulator.Now;
			PlayerProfile playerProfile = Singleton<Manager.Resources>.Instance.PlayerProfile;
			EnvironmentSimulator.DateTimeSerialization wakeTime = playerProfile.WakeTime;
			DateTime sleepableBegin = playerProfile.CanSleepTime[0].min.Time;
			if (now > sleepableBegin && now < PlayerActor._midnightTime)
			{
				wakeTime.Time = wakeTime.Time.AddDays(1.0);
			}
			float elapsedTime = 0f;
			TimeSpan sleepDuration = wakeTime.Time - now;
			this._elapsedTimeInSleep = TimeSpan.Zero;
			float prevDayLength = Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute;
			Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute = prevDayLength / 8f;
			Time.timeScale = 20f;
			while (this._elapsedTimeInSleep < sleepDuration)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			Time.timeScale = 1f;
			Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute = prevDayLength;
			Singleton<Manager.Map>.Instance.SetVisibleAll(true);
			bool? flag2 = (conditionAfter != null) ? new bool?(conditionAfter()) : null;
			enableSkip = (flag2 == null || flag2.Value);
			if (!enableSkip)
			{
				yield break;
			}
			stream = (MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 3f, true) ?? Observable.Empty<Unit>());
			yield return stream.ToYieldInstruction<Unit>();
			this._enumerator = null;
			yield break;
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x002BFAFC File Offset: 0x002BDEFC
		private int InvokeRelatedSleepEvent()
		{
			if (Game.isAdd01 && (base.ChaControl.sex == 0 || (base.ChaControl.sex == 1 && base.ChaControl.fileParam.futanari)))
			{
				List<AgentActor> list = ListPool<AgentActor>.Get();
				foreach (KeyValuePair<int, AgentActor> keyValuePair in Singleton<Manager.Map>.Instance.AgentTable)
				{
					if (keyValuePair.Value.CanRevRape())
					{
						int chunkID = keyValuePair.Value.ChunkID;
						if (chunkID == base.ChunkID)
						{
							list.Add(keyValuePair.Value);
						}
					}
				}
				if (!list.IsNullOrEmpty<AgentActor>())
				{
					AgentActor agentActor = null;
					List<AgentActor> list2 = list.FindAll((AgentActor x) => x.Mode == Desire.ActionType.SearchRevRape);
					if (!list2.IsNullOrEmpty<AgentActor>())
					{
						agentActor = list2.GetElement(UnityEngine.Random.Range(0, list2.Count));
					}
					if (agentActor == null)
					{
						agentActor = list.GetElement(UnityEngine.Random.Range(0, list.Count));
					}
					ListPool<AgentActor>.Release(list);
					if (agentActor.Mode == Desire.ActionType.Normal || agentActor.Mode == Desire.ActionType.SearchSleep || agentActor.Mode == Desire.ActionType.Encounter)
					{
						float value = agentActor.AgentData.StatsTable[6];
						float t = Mathf.InverseLerp(0f, 100f, value);
						float num = Singleton<Manager.Resources>.Instance.StatusProfile.YobaiMinMax.Lerp(t);
						float num2 = UnityEngine.Random.Range(0f, 100f);
						if (num2 < num)
						{
							this.StartSneakH(agentActor);
							return 1;
						}
						PoseKeyPair sleepTogetherRight = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SleepTogetherRight;
						PoseKeyPair sleepTogetherLeft = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SleepTogetherLeft;
						List<ActionPoint> groupActionPoints = base.CurrentPoint.GroupActionPoints;
						ActionPoint actionPoint = null;
						foreach (ActionPoint actionPoint2 in groupActionPoints)
						{
							if (actionPoint2.IsNeutralCommand)
							{
								actionPoint = actionPoint2;
								break;
							}
						}
						ActionPointInfo actionPointInfo;
						if (actionPoint != null && (actionPoint.FindAgentActionPointInfo(EventType.Sleep, sleepTogetherRight.poseID, out actionPointInfo) || actionPoint.FindAgentActionPointInfo(EventType.Sleep, sleepTogetherLeft.poseID, out actionPointInfo)))
						{
							GameObject gameObject = actionPoint.transform.FindLoop(actionPointInfo.baseNullName);
							Transform t2 = ((gameObject != null) ? gameObject.transform : null) ?? actionPoint.transform;
							GameObject gameObject2 = actionPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
							agentActor.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
							PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[actionPointInfo.eventID][actionPointInfo.poseID];
							agentActor.Animation.LoadEventKeyTable(actionPointInfo.eventID, actionPointInfo.poseID);
							agentActor.LoadEventItems(playState);
							agentActor.LoadEventParticles(actionPointInfo.eventID, actionPointInfo.poseID);
							agentActor.Animation.InitializeStates(playState);
							agentActor.Animation.LoadAnimatorIfNotEquals(playState);
							ActorAnimInfo actorAnimInfo = new ActorAnimInfo
							{
								layer = playState.Layer,
								inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
								inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
								outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
								outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
								directionType = playState.DirectionType,
								endEnableBlend = playState.EndEnableBlend,
								endBlendSec = playState.EndBlendRate,
								isLoop = playState.MainStateInfo.IsLoop,
								loopMinTime = playState.MainStateInfo.LoopMin,
								loopMaxTime = playState.MainStateInfo.LoopMax,
								hasAction = playState.ActionInfo.hasAction,
								loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName,
								randomCount = playState.ActionInfo.randomCount,
								oldNormalizedTime = 0f
							};
							agentActor.Animation.AnimInfo = actorAnimInfo;
							ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
							agentActor.SetActiveOnEquipedItem(false);
							agentActor.ChaControl.setAllLayerWeight(0f);
							agentActor.DisableActionFlag();
							agentActor.DeactivateNavMeshAgent();
							agentActor.IsKinematic = true;
							agentActor.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
							agentActor.SetStand(t2, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
							agentActor.CurrentPoint = actionPoint;
							agentActor.CurrentPoint.SetSlot(agentActor);
							agentActor.SetCurrentSchedule(actorAnimInfo2.isLoop, "添い寝", actorAnimInfo2.loopMinTime, actorAnimInfo2.loopMaxTime, actorAnimInfo2.hasAction, true);
							agentActor.ChangeBehavior(Desire.ActionType.EndTaskSleepAfterDate);
						}
					}
					else if (agentActor.Mode == Desire.ActionType.SearchRevRape)
					{
						this.StartSneakH(agentActor);
						return 1;
					}
				}
				else
				{
					ListPool<AgentActor>.Release(list);
				}
			}
			return 0;
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x002C00FC File Offset: 0x002BE4FC
		public void StartSneakH(AgentActor agent)
		{
			agent.packData.Init();
			agent.Animation.StopAllAnimCoroutine();
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ChangeBehavior(Desire.ActionType.Idle);
			agent.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			agent.Mode = Desire.ActionType.Idle;
			agent.DisableBehavior();
			agent.DeactivateNavMeshAgent();
			MapUIContainer.SetVisibleHUD(false);
			agent.Position = base.Position;
			agent.Rotation = base.Rotation;
			this.PlayerController.ChangeState("Communication");
			CinemachineBlendDefinition.Style prevStyle = this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			PoseKeyPair yobai = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.Yobai;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[yobai.postureID][yobai.poseID];
			AssetBundleInfo assetBundleInfo = playState.MainStateInfo.AssetBundleInfo;
			agent.ChangeAnimator(assetBundleInfo.assetbundle, assetBundleInfo.asset);
			Transform transform = this.CameraControl.CameraComponent.transform;
			agent.SetLookPtn(1, 3);
			agent.SetLookTarget(1, 0, transform);
			agent.openData.FindLoad("4", agent.AgentData.param.charaID, 2);
			agent.packData.onComplete = delegate()
			{
				agent.packData.restoreCommands = null;
				agent.packData.Release();
				this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
				agent.InitiateHScene(HSceneManager.HEvent.GyakuYobai);
			};
			Observable.EveryUpdate().Skip(1).SkipWhile((long _) => this.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
			{
				Singleton<MapUIContainer>.Instance.OpenADV(agent.openData, agent.packData);
			});
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x002C031C File Offset: 0x002BE71C
		private void StartSpecialH(int advID = 0)
		{
			this.AgentPartner.packData.Init();
			ActionPoint currentPoint = base.CurrentPoint;
			this.AgentPartner.CommandPartner = this;
			bool enabled = this.HandsHolder.enabled;
			this.OldEnabledHoldingHand = enabled;
			bool flag = enabled;
			if (flag)
			{
				this.HandsHolder.enabled = false;
				if (this.HandsHolder.EnabledHolding)
				{
					this.HandsHolder.EnabledHolding = false;
				}
			}
			if (advID == 0)
			{
				this.AgentPartner.StartCommunication();
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => this.Animation.PlayingTurnAnimation).Take(1).Subscribe(delegate(long _)
				{
					this.AgentPartner.openData.FindLoad("3", this.AgentPartner.charaID, 9);
					this.AgentPartner.packData.onComplete = delegate()
					{
						this.AgentPartner.packData.restoreCommands = null;
						HPoint hpoint = currentPoint.HPoint;
						this.AgentPartner.CommandPartner = null;
						this.ReleaseCurrentPoint();
						this.AgentPartner.packData.Release();
						Singleton<HSceneManager>.Instance.HousingHEnter(this.AgentPartner, hpoint);
					};
					Singleton<MapUIContainer>.Instance.OpenADV(this.AgentPartner.openData, this.AgentPartner.packData);
				});
			}
			else
			{
				this.AgentPartner.StopNavMeshAgent();
				this.AgentPartner.SetActiveOnEquipedItem(false);
				this.AgentPartner.DisableBehavior();
				Observable.EveryUpdate().Take(5).DoOnCompleted(delegate
				{
					this.AgentPartner.packData.restoreCommands = null;
					HPoint hpoint = currentPoint.HPoint;
					this.AgentPartner.CommandPartner = null;
					this.ReleaseCurrentPoint();
					this.AgentPartner.packData.Release();
					Singleton<HSceneManager>.Instance.HousingHEnter(this.AgentPartner, hpoint);
					this.ChaControl.visibleAll = true;
				}).Subscribe(delegate(long _)
				{
					this.AgentPartner.BehaviorResources.Current.DisableBehavior(true);
				}, delegate(Exception ex)
				{
				});
				MapUIContainer.SetVisibleHUD(false);
			}
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x002C0460 File Offset: 0x002BE860
		public void StartSleepTogetherEvent(AgentActor agent)
		{
			agent.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			this.PlayerController.ChangeState("Idle");
			base.SetActiveOnEquipedItem(false);
			base.ChaControl.setAllLayerWeight(0f);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			int item = AIProject.Definitions.Action.NameTable[EventType.Sleep].Item1;
			DateActionPointInfo apInfo;
			base.CurrentPoint.TryGetPlayerDateActionPointInfo(base.ChaControl.sex, EventType.Sleep, out apInfo);
			int poseIDA = apInfo.poseIDA;
			base.PoseID = poseIDA;
			int key = poseIDA;
			GameObject gameObject = base.CurrentPoint.transform.FindLoop(apInfo.baseNullNameA);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? base.CurrentPoint.transform;
			GameObject gameObject2 = base.CurrentPoint.transform.FindLoop(apInfo.baseNullNameB);
			Transform t2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.CurrentPoint.transform;
			GameObject gameObject3 = base.CurrentPoint.transform.FindLoop(apInfo.recoveryNullNameA);
			this.Animation.RecoveryPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = base.CurrentPoint.transform.FindLoop(apInfo.recoveryNullNameB);
			base.Partner.Animation.RecoveryPoint = ((gameObject4 != null) ? gameObject4.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)base.ChaControl.sex][item][key];
			this.Animation.LoadEventKeyTable(item, apInfo.poseIDA);
			this.LoadEventItems(playState);
			this.LoadEventParticles(item, apInfo.poseIDA);
			this.Animation.InitializeStates(playState);
			Actor partner = base.Partner;
			partner.Animation.LoadEventKeyTable(item, apInfo.poseIDB);
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[item][apInfo.poseIDB];
			partner.LoadEventItems(playState2);
			partner.LoadEventParticles(item, apInfo.poseIDB);
			partner.Animation.InitializeStates(playState2);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop
			};
			this.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			ActorAnimInfo actorAnimInfo3 = new ActorAnimInfo
			{
				layer = playState2.Layer,
				inEnableBlend = playState2.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState2.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState2.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState2.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState2.DirectionType,
				isLoop = playState2.MainStateInfo.IsLoop,
				loopMinTime = playState.MainStateInfo.LoopMin,
				loopMaxTime = playState.MainStateInfo.LoopMax,
				hasAction = playState.ActionInfo.hasAction
			};
			partner.Animation.AnimInfo = actorAnimInfo3;
			ActorAnimInfo actorAnimInfo4 = actorAnimInfo3;
			base.DeactivateNavMeshAgent();
			base.IsKinematic = true;
			partner.SetActiveOnEquipedItem(false);
			partner.ChaControl.setAllLayerWeight(0f);
			partner.DeactivateNavMeshAgent();
			partner.IsKinematic = true;
			this.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			base.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			partner.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState2.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			partner.SetStand(t2, actorAnimInfo4.inEnableBlend, actorAnimInfo4.inBlendSec, actorAnimInfo2.layer);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				if (apInfo.pointID == 501)
				{
					ADV.ChangeADVFixedAngleCamera(this, 5);
				}
				else if (apInfo.pointID == 500)
				{
					ADV.ChangeADVFixedAngleCamera(partner, 5);
				}
			});
			bool enabled = this.HandsHolder.enabled;
			this.OldEnabledHoldingHand = enabled;
			bool flag = enabled;
			if (flag)
			{
				this.HandsHolder.enabled = false;
				if (this.HandsHolder.EnabledHolding)
				{
					this.HandsHolder.EnabledHolding = false;
				}
			}
			this.CameraControl.SetShotTypeForce(ShotType.Near);
			this._sleepEventEnumerator = this.SleepEventCoroutine();
			this._sleepEventDisposable = Observable.FromCoroutine(() => this._sleepEventEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x002C0A38 File Offset: 0x002BEE38
		private IEnumerator SleepEventCoroutine()
		{
			yield return Observable.Timer(TimeSpan.FromMilliseconds(1000.0)).ToYieldInstruction<long>();
			this.ElapseTime(delegate()
			{
			}, false);
			while (this.ProcessingTimeSkip)
			{
				yield return null;
			}
			AgentActor partner = this.AgentPartner;
			OpenData openData = partner.openData;
			openData.FindLoad("1", partner.charaID, 13);
			AgentActor.PackData packData = partner.packData;
			packData.AttitudeID = 0;
			packData.Init();
			Singleton<Manager.Map>.Instance.DisableEntity(partner);
			base.SetStand(this.Animation.RecoveryPoint, false, 0f, 0);
			partner.SetStand(partner.Animation.RecoveryPoint, false, 0f, 0);
			PoseKeyPair idleID = Singleton<Manager.Resources>.Instance.AgentProfile.ADVIdleTable[partner.ChaControl.fileParam.personality];
			PlayState info = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[idleID.postureID][idleID.poseID];
			AssetBundleInfo abInfo = info.MainStateInfo.AssetBundleInfo;
			partner.ChangeAnimator(abInfo.assetbundle, abInfo.asset);
			Animator animator = partner.Animation.Animator;
			Queue<PlayState.Info> recoveryStates = QueuePool<PlayState.Info>.Get();
			foreach (PlayState.Info item in info.MainStateInfo.InStateInfo.StateInfos)
			{
				recoveryStates.Enqueue(item);
			}
			float blendTime = info.MainStateInfo.InStateInfo.FadeSecond;
			while (recoveryStates.Count > 0)
			{
				PlayState.Info state = recoveryStates.Dequeue();
				partner.Animation.PlayAnimation(state.stateName, state.layer, 0f);
				yield return null;
				if (recoveryStates.Count == 0)
				{
					break;
				}
				yield return null;
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
				{
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					yield return null;
				}
			}
			Vector3 to = base.Position;
			to.y = 0f;
			Vector3 from = partner.Position;
			from.y = 0f;
			Quaternion destRotation = Quaternion.LookRotation(to - from);
			partner.Rotation = destRotation;
			CinemachineBlendDefinition.Style prevStyle = this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			ADV.ChangeADVCamera(partner);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
			});
			yield return Observable.Timer(TimeSpan.FromSeconds(2.0)).ToYieldInstruction<long>();
			bool wait = false;
			packData.onComplete = delegate()
			{
				wait = true;
				int desireKey = Desire.GetDesireKey(Desire.Type.Lonely);
				partner.SetDesire(desireKey, 0f);
				int desireKey2 = Desire.GetDesireKey(Desire.Type.Sleep);
				partner.SetDesire(desireKey2, 0f);
				partner.AgentData.AddAppendEventFlagParam(6, 1);
				packData.Release();
				Singleton<ADV>.Instance.Captions.EndADV(null);
			};
			IObservable<Unit> stream = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 3f, true) ?? Observable.Empty<Unit>();
			yield return stream.ToYieldInstruction<Unit>();
			packData.restoreCommands = null;
			Singleton<MapUIContainer>.Instance.OpenADV(openData, packData);
			while (!wait)
			{
				yield return null;
			}
			partner.Animation.ResetDefaultAnimatorController();
			partner.ActivateNavMeshAgent();
			partner.SetActiveOnEquipedItem(true);
			partner.DeactivatePairing(0);
			this.PlayerController.ChangeState("Normal");
			this.CameraControl.Mode = CameraMode.Normal;
			this.CameraControl.RecoverShotType();
			partner.TargetInSightActionPoint = null;
			base.CurrentPoint = null;
			MapUIContainer.SetVisibleHUDExceptStoryUI(true);
			MapUIContainer.StorySupportUI.Open();
			Singleton<Manager.Map>.Instance.EnableEntity(partner);
			this._sleepEventEnumerator = null;
			yield break;
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x002C0A54 File Offset: 0x002BEE54
		public void StartGyakuYobaiEvent(AgentActor agent)
		{
			PlayerActor.<StartGyakuYobaiEvent>c__AnonStorey12 <StartGyakuYobaiEvent>c__AnonStorey = new PlayerActor.<StartGyakuYobaiEvent>c__AnonStorey12();
			<StartGyakuYobaiEvent>c__AnonStorey.agent = agent;
			<StartGyakuYobaiEvent>c__AnonStorey.$this = this;
			<StartGyakuYobaiEvent>c__AnonStorey.agent.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			<StartGyakuYobaiEvent>c__AnonStorey.agent.Mode = Desire.ActionType.Idle;
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Singleton<Manager.Map>.Instance.DisableEntity(<StartGyakuYobaiEvent>c__AnonStorey.agent);
				<StartGyakuYobaiEvent>c__AnonStorey.$this.EventKey = EventType.Sleep;
				<StartGyakuYobaiEvent>c__AnonStorey.$this.SetActiveOnEquipedItem(false);
				<StartGyakuYobaiEvent>c__AnonStorey.$this.ChaControl.setAllLayerWeight(0f);
				<StartGyakuYobaiEvent>c__AnonStorey.$this.SetScheduledInteractionState(false);
				<StartGyakuYobaiEvent>c__AnonStorey.$this.ReleaseInteraction();
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
				Singleton<Manager.Input>.Instance.SetupState();
				<StartGyakuYobaiEvent>c__AnonStorey.$this.PlayActionMotion(EventType.Sleep);
				OpenData openData = <StartGyakuYobaiEvent>c__AnonStorey.agent.openData;
				AgentActor.PackData packData = <StartGyakuYobaiEvent>c__AnonStorey.agent.packData;
				packData.Init();
				<StartGyakuYobaiEvent>c__AnonStorey.agent.Animation.StopAllAnimCoroutine();
				<StartGyakuYobaiEvent>c__AnonStorey.agent.SetActiveOnEquipedItem(false);
				<StartGyakuYobaiEvent>c__AnonStorey.agent.ChaControl.setAllLayerWeight(0f);
				<StartGyakuYobaiEvent>c__AnonStorey.agent.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				<StartGyakuYobaiEvent>c__AnonStorey.agent.Mode = Desire.ActionType.Idle;
				<StartGyakuYobaiEvent>c__AnonStorey.agent.DisableBehavior();
				<StartGyakuYobaiEvent>c__AnonStorey.agent.DeactivateNavMeshAgent();
				MapUIContainer.SetVisibleHUD(false);
				<StartGyakuYobaiEvent>c__AnonStorey.agent.Position = <StartGyakuYobaiEvent>c__AnonStorey.$this.Position;
				<StartGyakuYobaiEvent>c__AnonStorey.agent.Rotation = <StartGyakuYobaiEvent>c__AnonStorey.$this.Rotation;
				<StartGyakuYobaiEvent>c__AnonStorey.$this.PlayerController.ChangeState("Communication");
				CinemachineBlendDefinition.Style prevStyle = <StartGyakuYobaiEvent>c__AnonStorey.$this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
				<StartGyakuYobaiEvent>c__AnonStorey.$this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
				PoseKeyPair yobai = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.Yobai;
				PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[yobai.postureID][yobai.poseID];
				AssetBundleInfo assetBundleInfo = playState.MainStateInfo.AssetBundleInfo;
				<StartGyakuYobaiEvent>c__AnonStorey.agent.ChangeAnimator(assetBundleInfo.assetbundle, assetBundleInfo.asset);
				Transform transform = <StartGyakuYobaiEvent>c__AnonStorey.$this.CameraControl.CameraComponent.transform;
				<StartGyakuYobaiEvent>c__AnonStorey.agent.SetLookPtn(1, 3);
				<StartGyakuYobaiEvent>c__AnonStorey.agent.SetLookTarget(1, 0, transform);
				openData.FindLoad("4", <StartGyakuYobaiEvent>c__AnonStorey.agent.AgentData.param.charaID, 13);
				packData.onComplete = delegate()
				{
					packData.restoreCommands = null;
					packData.Release();
					<StartGyakuYobaiEvent>c__AnonStorey.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
					int desireKey = Desire.GetDesireKey(Desire.Type.Lonely);
					<StartGyakuYobaiEvent>c__AnonStorey.agent.SetDesire(desireKey, 0f);
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Sleep);
					<StartGyakuYobaiEvent>c__AnonStorey.agent.SetDesire(desireKey2, 0f);
					Singleton<Manager.Map>.Instance.EnableEntity(<StartGyakuYobaiEvent>c__AnonStorey.agent);
					HSceneManager.SleepStart = true;
					<StartGyakuYobaiEvent>c__AnonStorey.agent.InitiateHScene(HSceneManager.HEvent.GyakuYobai);
				};
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => <StartGyakuYobaiEvent>c__AnonStorey.CameraControl.CinemachineBrain.IsBlending).Take(1).DelayFrame(5, FrameCountType.Update).Subscribe(delegate(long _)
				{
					Singleton<MapUIContainer>.Instance.OpenADV(openData, packData);
				});
			});
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x002C0AD4 File Offset: 0x002BEED4
		public void StartEatEvent(AgentActor agent)
		{
			Singleton<Manager.Map>.Instance.DisableEntity(agent);
			agent.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			this.PlayerController.ChangeState("Idle");
			base.SetActiveOnEquipedItem(false);
			base.ChaControl.setAllLayerWeight(0f);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			int item = AIProject.Definitions.Action.NameTable[EventType.Eat].Item1;
			DateActionPointInfo dateActionPointInfo;
			base.CurrentPoint.TryGetPlayerDateActionPointInfo(base.ChaControl.sex, EventType.Eat, out dateActionPointInfo);
			int poseIDA = dateActionPointInfo.poseIDA;
			base.PoseID = poseIDA;
			int key = poseIDA;
			GameObject gameObject = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameA);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? base.CurrentPoint.transform;
			GameObject gameObject2 = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameB);
			Transform t2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.CurrentPoint.transform;
			GameObject gameObject3 = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameA);
			this.Animation.RecoveryPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameB);
			base.Partner.Animation.RecoveryPoint = ((gameObject4 != null) ? gameObject4.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)base.ChaControl.sex][item][key];
			this.Animation.LoadEventKeyTable(item, dateActionPointInfo.poseIDA);
			this.LoadEventItems(playState);
			this.LoadEventParticles(item, dateActionPointInfo.poseIDA);
			this.Animation.InitializeStates(playState);
			Actor partner = base.Partner;
			partner.Animation.LoadEventKeyTable(item, dateActionPointInfo.poseIDB);
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[item][dateActionPointInfo.poseIDB];
			partner.LoadEventItems(playState2);
			partner.LoadEventParticles(item, dateActionPointInfo.poseIDB);
			partner.Animation.InitializeStates(playState2);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop
			};
			this.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			ActorAnimInfo actorAnimInfo3 = new ActorAnimInfo
			{
				layer = playState2.Layer,
				inEnableBlend = playState2.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState2.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState2.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState2.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState2.DirectionType,
				isLoop = playState2.MainStateInfo.IsLoop,
				endEnableBlend = false,
				endBlendSec = 0f,
				loopMinTime = playState.MainStateInfo.LoopMin,
				loopMaxTime = playState.MainStateInfo.LoopMax,
				hasAction = playState.ActionInfo.hasAction
			};
			partner.Animation.AnimInfo = actorAnimInfo3;
			ActorAnimInfo actorAnimInfo4 = actorAnimInfo3;
			List<int> list = ListPool<int>.Get();
			foreach (KeyValuePair<int, Dictionary<int, int>> keyValuePair in Singleton<Manager.Resources>.Instance.Map.FoodDateEventItemList)
			{
				foreach (KeyValuePair<int, int> keyValuePair2 in keyValuePair.Value)
				{
					if (keyValuePair2.Value != -1)
					{
						list.Add(keyValuePair2.Value);
					}
				}
			}
			int num = -1;
			if (!list.IsNullOrEmpty<int>())
			{
				num = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			}
			ListPool<int>.Release(list);
			ActionItemInfo eventItemInfo;
			if (Singleton<Manager.Resources>.Instance.Map.EventItemList.TryGetValue(num, out eventItemInfo))
			{
				LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
				string rootParentName = locomotionProfile.RootParentName;
				GameObject gameObject5 = base.LoadEventItem(num, rootParentName, false, eventItemInfo);
				if (gameObject5 != null)
				{
					Renderer[] componentsInChildren = gameObject5.GetComponentsInChildren<Renderer>(true);
					foreach (Renderer renderer in componentsInChildren)
					{
						renderer.enabled = true;
					}
				}
				GameObject gameObject6 = partner.LoadEventItem(num, rootParentName, false, eventItemInfo);
				if (gameObject6 != null)
				{
					Renderer[] componentsInChildren2 = gameObject6.GetComponentsInChildren<Renderer>(true);
					foreach (Renderer renderer2 in componentsInChildren2)
					{
						renderer2.enabled = true;
					}
				}
			}
			base.DeactivateNavMeshAgent();
			base.IsKinematic = true;
			partner.SetActiveOnEquipedItem(false);
			partner.ChaControl.setAllLayerWeight(0f);
			partner.DeactivateNavMeshAgent();
			partner.IsKinematic = true;
			this.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			base.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			partner.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState2.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			partner.SetStand(t2, actorAnimInfo4.inEnableBlend, actorAnimInfo4.inBlendSec, actorAnimInfo2.layer);
			bool enabled = this.HandsHolder.enabled;
			this.OldEnabledHoldingHand = enabled;
			bool flag = enabled;
			if (flag)
			{
				this.HandsHolder.enabled = false;
				if (this.HandsHolder.EnabledHolding)
				{
					this.HandsHolder.EnabledHolding = false;
				}
			}
			this.CameraControl.SetShotTypeForce(ShotType.Near);
			ActAnimFlagData talkData;
			Dictionary<int, ActAnimFlagData> dictionary;
			if (Singleton<Manager.Resources>.Instance.Action.AgentActionFlagTable.TryGetValue(item, out dictionary) && dictionary.TryGetValue(dateActionPointInfo.poseIDB, out talkData))
			{
				Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
				{
					ADV.ChangeADVFixedAngleCamera(partner, talkData.attitudeID);
				});
			}
			else
			{
				ADV.ChangeADVCameraDiagonal(partner);
			}
			this._eatEventEnumerator = this.EatEventCoroutine();
			this._eatEventDisposable = Observable.FromCoroutine(() => this._eatEventEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x002C12B0 File Offset: 0x002BF6B0
		private IEnumerator EatEventCoroutine()
		{
			yield return Observable.Timer(TimeSpan.FromMilliseconds(1000.0)).ToYieldInstruction<long>();
			AgentActor partner = this.AgentPartner;
			OpenData openData = partner.openData;
			openData.FindLoad("5", partner.charaID, 13);
			AgentActor.PackData packData = partner.packData;
			packData.AttitudeID = 0;
			packData.Init();
			bool wait = false;
			packData.onComplete = delegate()
			{
				wait = true;
				packData.Release();
				Singleton<ADV>.Instance.Captions.CanvasGroupOFF();
				Singleton<MapUIContainer>.Instance.advScene.gameObject.SetActive(false);
			};
			packData.restoreCommands = null;
			Singleton<MapUIContainer>.Instance.OpenADV(openData, packData);
			while (!wait)
			{
				yield return null;
			}
			this.InitiateExitEatEventCommand();
			this._eatEventEnumerator = null;
			yield break;
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x002C12CB File Offset: 0x002BF6CB
		private void InitiateExitEatEventCommand()
		{
			this.PlayerController.ChangeState("ExitEatEvent");
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x002C12DD File Offset: 0x002BF6DD
		private void ExitEatEventADV()
		{
			this._exitEatEventEnumerator = this.ExitEatEventCoroutine();
			this._exitEatEventDisposable = Observable.FromCoroutine(() => this._exitEatEventEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x002C1308 File Offset: 0x002BF708
		private IEnumerator ExitEatEventCoroutine()
		{
			AgentActor partner = this.AgentPartner;
			OpenData openData = partner.openData;
			AgentActor.PackData packData = partner.packData;
			this.CameraControl.CrossFade.FadeStart(-1f);
			base.SetStand(this.Animation.RecoveryPoint, false, 0f, 0);
			partner.SetStand(partner.Animation.RecoveryPoint, false, 0f, 0);
			PoseKeyPair idleID = Singleton<Manager.Resources>.Instance.AgentProfile.ADVIdleTable[partner.ChaControl.fileParam.personality];
			PlayState info = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[idleID.postureID][idleID.poseID];
			AssetBundleInfo abInfo = info.MainStateInfo.AssetBundleInfo;
			partner.ChangeAnimator(abInfo.assetbundle, abInfo.asset);
			Animator animator = partner.Animation.Animator;
			Queue<PlayState.Info> recoveryStates = QueuePool<PlayState.Info>.Get();
			foreach (PlayState.Info item in info.MainStateInfo.InStateInfo.StateInfos)
			{
				recoveryStates.Enqueue(item);
			}
			float blendTime = info.MainStateInfo.InStateInfo.FadeSecond;
			while (recoveryStates.Count > 0)
			{
				PlayState.Info state = recoveryStates.Dequeue();
				partner.Animation.PlayAnimation(state.stateName, state.layer, 0f);
				yield return null;
				if (recoveryStates.Count == 0)
				{
					break;
				}
				yield return null;
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
				{
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					yield return null;
				}
			}
			Vector3 to = base.Position;
			to.y = 0f;
			Vector3 from = partner.Position;
			from.y = 0f;
			Quaternion destRotation = Quaternion.LookRotation(to - from);
			partner.Rotation = destRotation;
			base.ClearItems();
			base.ClearParticles();
			partner.ClearItems();
			partner.ClearParticles();
			CinemachineBlendDefinition.Style prevStyle = this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			ADV.ChangeADVCamera(partner);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
			});
			while (Singleton<ADV>.Instance.Captions.IsProcEndADV)
			{
				Singleton<Manager.Map>.Instance.Player.SetScheduledInteractionState(false);
				Singleton<Manager.Map>.Instance.Player.ReleaseInteraction();
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
				yield return null;
			}
			Singleton<Manager.Map>.Instance.Player.SetScheduledInteractionState(false);
			Singleton<Manager.Map>.Instance.Player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			openData.FindLoad("2", partner.charaID, 13);
			packData.Init();
			bool wait = false;
			packData.onComplete = delegate()
			{
				wait = true;
				int desireKey = Desire.GetDesireKey(Desire.Type.Lonely);
				partner.SetDesire(desireKey, 0f);
				int desireKey2 = Desire.GetDesireKey(Desire.Type.Eat);
				partner.SetDesire(desireKey2, 0f);
				packData.Release();
				Singleton<ADV>.Instance.Captions.EndADV(null);
			};
			packData.restoreCommands = null;
			Singleton<MapUIContainer>.Instance.OpenADV(openData, packData);
			while (!wait)
			{
				yield return null;
			}
			partner.Animation.ResetDefaultAnimatorController();
			partner.ActivateNavMeshAgent();
			partner.SetActiveOnEquipedItem(true);
			partner.DeactivatePairing(0);
			this.PlayerController.ChangeState("Normal");
			this.CameraControl.Mode = CameraMode.Normal;
			this.CameraControl.RecoverShotType();
			partner.TargetInSightActionPoint = null;
			base.CurrentPoint = null;
			MapUIContainer.SetVisibleHUDExceptStoryUI(true);
			MapUIContainer.StorySupportUI.Open();
			Singleton<Manager.Map>.Instance.EnableEntity(partner);
			this._exitEatEventEnumerator = null;
			yield break;
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x002C1324 File Offset: 0x002BF724
		public void StartHizamakuraEvent(AgentActor agent)
		{
			Singleton<Manager.Map>.Instance.DisableEntity(agent);
			agent.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			this.PlayerController.ChangeState("Idle");
			base.SetActiveOnEquipedItem(false);
			base.ChaControl.setAllLayerWeight(0f);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			int item = AIProject.Definitions.Action.NameTable[EventType.Break].Item1;
			int hizamakuraPTID = Singleton<Manager.Resources>.Instance.PlayerProfile.HizamakuraPTID;
			DateActionPointInfo dateActionPointInfo;
			base.CurrentPoint.FindPlayerDateActionPointInfo(base.ChaControl.sex, hizamakuraPTID, out dateActionPointInfo);
			int poseIDA = dateActionPointInfo.poseIDA;
			base.PoseID = poseIDA;
			int key = poseIDA;
			GameObject gameObject = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameA);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? base.CurrentPoint.transform;
			GameObject gameObject2 = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameB);
			Transform t2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.CurrentPoint.transform;
			GameObject gameObject3 = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameA);
			this.Animation.RecoveryPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = base.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameB);
			base.Partner.Animation.RecoveryPoint = ((gameObject4 != null) ? gameObject4.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)base.ChaControl.sex][item][key];
			this.Animation.LoadEventKeyTable(item, dateActionPointInfo.poseIDA);
			this.LoadEventItems(playState);
			this.LoadEventParticles(item, dateActionPointInfo.poseIDA);
			this.Animation.InitializeStates(playState);
			Actor partner = base.Partner;
			partner.Animation.LoadEventKeyTable(item, dateActionPointInfo.poseIDB);
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[item][dateActionPointInfo.poseIDB];
			partner.LoadEventItems(playState2);
			partner.LoadEventParticles(item, dateActionPointInfo.poseIDB);
			partner.Animation.InitializeStates(playState2);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop
			};
			this.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			ActorAnimInfo actorAnimInfo3 = new ActorAnimInfo
			{
				layer = playState2.Layer,
				inEnableBlend = playState2.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState2.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState2.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState2.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState2.DirectionType,
				isLoop = playState2.MainStateInfo.IsLoop,
				endEnableBlend = false,
				endBlendSec = 0f,
				loopMinTime = playState.MainStateInfo.LoopMin,
				loopMaxTime = playState.MainStateInfo.LoopMax,
				hasAction = playState.ActionInfo.hasAction
			};
			partner.Animation.AnimInfo = actorAnimInfo3;
			ActorAnimInfo actorAnimInfo4 = actorAnimInfo3;
			base.DeactivateNavMeshAgent();
			base.IsKinematic = true;
			partner.SetActiveOnEquipedItem(false);
			partner.ChaControl.setAllLayerWeight(0f);
			partner.DeactivateNavMeshAgent();
			partner.IsKinematic = true;
			this.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			base.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			partner.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState2.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			partner.SetStand(t2, actorAnimInfo4.inEnableBlend, actorAnimInfo4.inBlendSec, actorAnimInfo2.layer);
			bool enabled = this.HandsHolder.enabled;
			this.OldEnabledHoldingHand = enabled;
			bool flag = enabled;
			if (flag)
			{
				this.HandsHolder.enabled = false;
				if (this.HandsHolder.EnabledHolding)
				{
					this.HandsHolder.EnabledHolding = false;
				}
			}
			this.CameraControl.SetShotTypeForce(ShotType.Near);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				ADV.ChangeADVFixedAngleCamera(partner, 6);
			});
			this._hizamakuraEventEnumerator = this.HizamakuraEventCoroutine();
			this._hizamakuraEventDisposable = Observable.FromCoroutine(() => this._hizamakuraEventEnumerator, false).Subscribe<Unit>();
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x002C1904 File Offset: 0x002BFD04
		private IEnumerator HizamakuraEventCoroutine()
		{
			yield return Observable.Timer(TimeSpan.FromMilliseconds(1000.0)).ToYieldInstruction<long>();
			AgentActor partner = this.AgentPartner;
			OpenData openData = partner.openData;
			openData.FindLoad("0", partner.charaID, 13);
			AgentActor.PackData packData = partner.packData;
			packData.AttitudeID = 0;
			packData.Init();
			bool wait = false;
			packData.onComplete = delegate()
			{
				wait = true;
				packData.Release();
				Singleton<ADV>.Instance.Captions.CanvasGroupOFF();
				Singleton<MapUIContainer>.Instance.advScene.gameObject.SetActive(false);
			};
			packData.restoreCommands = null;
			Singleton<MapUIContainer>.Instance.OpenADV(openData, packData);
			while (!wait)
			{
				yield return null;
			}
			base.SetStand(this.Animation.RecoveryPoint, false, 0f, 0);
			partner.SetStand(partner.Animation.RecoveryPoint, false, 0f, 0);
			PoseKeyPair idleID = Singleton<Manager.Resources>.Instance.AgentProfile.ADVIdleTable[partner.ChaControl.fileParam.personality];
			PlayState info = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[idleID.postureID][idleID.poseID];
			AssetBundleInfo abInfo = info.MainStateInfo.AssetBundleInfo;
			partner.ChangeAnimator(abInfo.assetbundle, abInfo.asset);
			Animator animator = partner.Animation.Animator;
			Queue<PlayState.Info> recoveryStates = QueuePool<PlayState.Info>.Get();
			foreach (PlayState.Info item in info.MainStateInfo.InStateInfo.StateInfos)
			{
				recoveryStates.Enqueue(item);
			}
			float blendTime = info.MainStateInfo.InStateInfo.FadeSecond;
			while (recoveryStates.Count > 0)
			{
				PlayState.Info state = recoveryStates.Dequeue();
				partner.Animation.PlayAnimation(state.stateName, state.layer, 0f);
				yield return null;
				if (recoveryStates.Count == 0)
				{
					break;
				}
				yield return null;
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
				{
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					yield return null;
				}
			}
			Vector3 to = base.Position;
			to.y = 0f;
			Vector3 from = partner.Position;
			from.y = 0f;
			Quaternion destRotation = Quaternion.LookRotation(to - from);
			partner.Rotation = destRotation;
			CinemachineBlendDefinition.Style prevStyle = this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
			this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
			ADV.ChangeADVCamera(partner);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				this.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
			});
			yield return Observable.Timer(TimeSpan.FromSeconds(2.0)).ToYieldInstruction<long>();
			openData.FindLoad("3", partner.charaID, 13);
			packData.Init();
			wait = false;
			packData.onComplete = delegate()
			{
				wait = true;
				int desireKey = Desire.GetDesireKey(Desire.Type.Lonely);
				partner.SetDesire(desireKey, 0f);
				int desireKey2 = Desire.GetDesireKey(Desire.Type.Break);
				partner.SetDesire(desireKey2, 0f);
				packData.Release();
				Singleton<ADV>.Instance.Captions.EndADV(null);
			};
			yield return MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).ToYieldInstruction<Unit>();
			packData.restoreCommands = null;
			Singleton<MapUIContainer>.Instance.OpenADV(openData, packData);
			while (!wait)
			{
				yield return null;
			}
			partner.Animation.ResetDefaultAnimatorController();
			partner.ActivateNavMeshAgent();
			partner.SetActiveOnEquipedItem(true);
			partner.DeactivatePairing(0);
			this.PlayerController.ChangeState("Normal");
			this.CameraControl.Mode = CameraMode.Normal;
			this.CameraControl.RecoverShotType();
			partner.TargetInSightActionPoint = null;
			base.CurrentPoint = null;
			MapUIContainer.SetVisibleHUDExceptStoryUI(true);
			MapUIContainer.StorySupportUI.Open();
			Singleton<Manager.Map>.Instance.EnableEntity(partner);
			this._hizamakuraEventEnumerator = null;
			yield break;
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x0600679D RID: 26525 RVA: 0x002C191F File Offset: 0x002BFD1F
		public override string CharaName
		{
			[CompilerGenerated]
			get
			{
				return base.ChaControl.fileParam.fullname;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x0600679E RID: 26526 RVA: 0x002C1931 File Offset: 0x002BFD31
		public override ICharacterInfo TiedInfo
		{
			[CompilerGenerated]
			get
			{
				return this.PlayerData;
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x0600679F RID: 26527 RVA: 0x002C1939 File Offset: 0x002BFD39
		// (set) Token: 0x060067A0 RID: 26528 RVA: 0x002C1941 File Offset: 0x002BFD41
		public PlayerData PlayerData { get; set; }

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x060067A1 RID: 26529 RVA: 0x002C194A File Offset: 0x002BFD4A
		public override ActorAnimation Animation
		{
			[CompilerGenerated]
			get
			{
				return this._animation;
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x060067A2 RID: 26530 RVA: 0x002C1952 File Offset: 0x002BFD52
		public ActorCameraControl CameraControl
		{
			[CompilerGenerated]
			get
			{
				return this._cameraCtrl;
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x060067A3 RID: 26531 RVA: 0x002C195A File Offset: 0x002BFD5A
		public CameraConfig CameraConfig
		{
			[CompilerGenerated]
			get
			{
				return this._cameraCtrl.CameraConfig;
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x002C1967 File Offset: 0x002BFD67
		public Vector3 NormalOffset
		{
			[CompilerGenerated]
			get
			{
				return this._normalOffset;
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x060067A5 RID: 26533 RVA: 0x002C196F File Offset: 0x002BFD6F
		public Vector3 BehaviourOffset
		{
			[CompilerGenerated]
			get
			{
				return this._behaviourOffset;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x060067A6 RID: 26534 RVA: 0x002C1977 File Offset: 0x002BFD77
		public GameObject CameraTarget
		{
			[CompilerGenerated]
			get
			{
				return this._cameraTarget;
			}
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x060067A7 RID: 26535 RVA: 0x002C197F File Offset: 0x002BFD7F
		public override ActorLocomotion Locomotor
		{
			[CompilerGenerated]
			get
			{
				return this._character;
			}
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x060067A8 RID: 26536 RVA: 0x002C1987 File Offset: 0x002BFD87
		public ActorLocomotionThirdPerson CharacterTPS
		{
			[CompilerGenerated]
			get
			{
				return this._character;
			}
		}

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x060067A9 RID: 26537 RVA: 0x002C198F File Offset: 0x002BFD8F
		public override ActorController Controller
		{
			[CompilerGenerated]
			get
			{
				return this._controller;
			}
		}

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x060067AA RID: 26538 RVA: 0x002C1997 File Offset: 0x002BFD97
		public PlayerController PlayerController
		{
			[CompilerGenerated]
			get
			{
				return this._controller;
			}
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x060067AB RID: 26539 RVA: 0x002C199F File Offset: 0x002BFD9F
		// (set) Token: 0x060067AC RID: 26540 RVA: 0x002C19A7 File Offset: 0x002BFDA7
		public HandsHolder HandsHolder { get; protected set; }

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x060067AD RID: 26541 RVA: 0x002C19B0 File Offset: 0x002BFDB0
		// (set) Token: 0x060067AE RID: 26542 RVA: 0x002C19B8 File Offset: 0x002BFDB8
		public bool OldEnabledHoldingHand { get; set; }

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x060067AF RID: 26543 RVA: 0x002C19C1 File Offset: 0x002BFDC1
		// (set) Token: 0x060067B0 RID: 26544 RVA: 0x002C19C9 File Offset: 0x002BFDC9
		public DevicePoint CurrentDevicePoint { get; set; }

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x060067B1 RID: 26545 RVA: 0x002C19D2 File Offset: 0x002BFDD2
		// (set) Token: 0x060067B2 RID: 26546 RVA: 0x002C19DA File Offset: 0x002BFDDA
		public EventPoint CurrentEventPoint { get; set; }

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x060067B3 RID: 26547 RVA: 0x002C19E3 File Offset: 0x002BFDE3
		// (set) Token: 0x060067B4 RID: 26548 RVA: 0x002C19EB File Offset: 0x002BFDEB
		public FarmPoint CurrentFarmPoint { get; set; }

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x002C19F4 File Offset: 0x002BFDF4
		// (set) Token: 0x060067B6 RID: 26550 RVA: 0x002C19FC File Offset: 0x002BFDFC
		public PetHomePoint CurrentPetHomePoint { get; set; }

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x060067B7 RID: 26551 RVA: 0x002C1A05 File Offset: 0x002BFE05
		// (set) Token: 0x060067B8 RID: 26552 RVA: 0x002C1A0D File Offset: 0x002BFE0D
		public JukePoint CurrentjukePoint { get; set; }

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x060067B9 RID: 26553 RVA: 0x002C1A16 File Offset: 0x002BFE16
		// (set) Token: 0x060067BA RID: 26554 RVA: 0x002C1A1E File Offset: 0x002BFE1E
		public CraftPoint CurrentCraftPoint { get; set; }

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x060067BB RID: 26555 RVA: 0x002C1A27 File Offset: 0x002BFE27
		// (set) Token: 0x060067BC RID: 26556 RVA: 0x002C1A2F File Offset: 0x002BFE2F
		public List<UnityEx.ValueTuple<Popup.Tutorial.Type, bool>> TutorialIndexList { get; set; } = new List<UnityEx.ValueTuple<Popup.Tutorial.Type, bool>>();

		// Token: 0x060067BD RID: 26557 RVA: 0x002C1A38 File Offset: 0x002BFE38
		public void SetScheduledInteractionState(bool isEnabled)
		{
			this._scheduledInteractionState = new bool?(isEnabled);
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x002C1A48 File Offset: 0x002BFE48
		public void ReleaseInteraction()
		{
			if (this._scheduledInteractionState == null)
			{
				return;
			}
			this.CurrentInteractionState = this._scheduledInteractionState.Value;
			this._cameraCtrl.EnabledInput = this._scheduledInteractionState.Value;
			this._scheduledInteractionState = null;
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x060067BF RID: 26559 RVA: 0x002C1A9C File Offset: 0x002BFE9C
		// (set) Token: 0x060067C0 RID: 26560 RVA: 0x002C1AA4 File Offset: 0x002BFEA4
		public bool CurrentInteractionState { get; private set; }

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x060067C1 RID: 26561 RVA: 0x002C1AAD File Offset: 0x002BFEAD
		// (set) Token: 0x060067C2 RID: 26562 RVA: 0x002C1AB5 File Offset: 0x002BFEB5
		public Actor CommCompanion { get; set; }

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x060067C3 RID: 26563 RVA: 0x002C1ABE File Offset: 0x002BFEBE
		// (set) Token: 0x060067C4 RID: 26564 RVA: 0x002C1ACB File Offset: 0x002BFECB
		public AgentActor AgentPartner
		{
			get
			{
				return base.Partner as AgentActor;
			}
			set
			{
				base.Partner = value;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x060067C5 RID: 26565 RVA: 0x002C1AD4 File Offset: 0x002BFED4
		// (set) Token: 0x060067C6 RID: 26566 RVA: 0x002C1ADC File Offset: 0x002BFEDC
		public AnimalBase Animal { get; set; }

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x060067C7 RID: 26567 RVA: 0x002C1AE5 File Offset: 0x002BFEE5
		// (set) Token: 0x060067C8 RID: 26568 RVA: 0x002C1AED File Offset: 0x002BFEED
		public bool IsRunning { get; set; }

		// Token: 0x060067C9 RID: 26569 RVA: 0x002C1AF8 File Offset: 0x002BFEF8
		protected override void OnStart()
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

		// Token: 0x060067CA RID: 26570 RVA: 0x002C1B6C File Offset: 0x002BFF6C
		private void OnElapsedTimeFromDateSleep()
		{
			AgentActor agentPartner = this.AgentPartner;
			ActorAnimInfo animInfo = agentPartner.Animation.AnimInfo;
			if (this.OldEnabledHoldingHand)
			{
				this.HandsHolder.enabled = false;
				this.HandsHolder.EnabledHolding = false;
			}
			agentPartner.CurrentPoint = base.CurrentPoint;
			agentPartner.CurrentPoint.SetSlot(agentPartner);
			agentPartner.SetCurrentSchedule(animInfo.isLoop, "睡眠", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, true);
			agentPartner.BehaviorResources.ChangeMode(Desire.ActionType.EndTaskSleepAfterDate);
			agentPartner.Mode = Desire.ActionType.EndTaskSleepAfterDate;
			agentPartner.DisableActionFlag();
			agentPartner.Partner = null;
			base.Partner = null;
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x002C1C1C File Offset: 0x002C001C
		public void StashData()
		{
			Singleton<MapScene>.Instance.SaveProfile(true);
			Game.PrevPlayerStateFromCharaCreate = this.PlayerController.State.GetType().Name;
			Game.PrevAccessDeviceID = this.CurrentDevicePoint.ID;
			Singleton<Game>.Instance.WorldData.Copy(Singleton<Game>.Instance.Data.AutoData);
			Singleton<Game>.Instance.IsAuto = true;
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x002C1C88 File Offset: 0x002C0088
		public void PlayActionMotion(EventType eventType)
		{
			ActionPointInfo actionPointInfo;
			base.CurrentPoint.TryGetPlayerActionPointInfo(eventType, out actionPointInfo);
			int poseID = actionPointInfo.poseID;
			base.PoseID = poseID;
			int key = poseID;
			GameObject gameObject = base.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? base.CurrentPoint.transform;
			GameObject gameObject2 = base.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			this.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)base.ChaControl.sex][actionPointInfo.eventID][key];
			this.Animation.LoadEventKeyTable(actionPointInfo.eventID, actionPointInfo.poseID);
			this.LoadEventItems(playState);
			this.LoadEventParticles(actionPointInfo.eventID, actionPointInfo.poseID);
			this.Animation.InitializeStates(playState);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate
			};
			this.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			base.DeactivateNavMeshAgent();
			base.IsKinematic = true;
			this.Animation.StopAllAnimCoroutine();
			this.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			base.SetStand(t, actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.directionType);
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x002C1EC4 File Offset: 0x002C02C4
		public override IEnumerator LoadAsync()
		{
			yield return this.LoadCharAsync(this.PlayerData.CharaFileNames[(int)this.PlayerData.Sex]);
			this.LoadEquipments();
			if (this._chaBodyParts == null)
			{
				this._chaBodyParts = new Dictionary<Actor.BodyPart, Transform>();
			}
			GameObject hips = base.ChaControl.animBody.transform.FindLoop("cf_J_Hips");
			Dictionary<Actor.BodyPart, Transform> chaBodyParts = this._chaBodyParts;
			Actor.BodyPart key = Actor.BodyPart.Body;
			GameObject gameObject = hips;
			chaBodyParts[key] = ((gameObject != null) ? gameObject.transform : null);
			GameObject bust = base.ChaControl.animBody.transform.FindLoop("cf_J_Mune00");
			Dictionary<Actor.BodyPart, Transform> chaBodyParts2 = this._chaBodyParts;
			Actor.BodyPart key2 = Actor.BodyPart.Bust;
			GameObject gameObject2 = bust;
			chaBodyParts2[key2] = ((gameObject2 != null) ? gameObject2.transform : null);
			GameObject head = base.ChaControl.animBody.transform.FindLoop("N_Head");
			Dictionary<Actor.BodyPart, Transform> chaBodyParts3 = this._chaBodyParts;
			Actor.BodyPart key3 = Actor.BodyPart.Head;
			GameObject gameObject3 = head;
			chaBodyParts3[key3] = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject lf = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_L");
			Dictionary<Actor.BodyPart, Transform> chaBodyParts4 = this._chaBodyParts;
			Actor.BodyPart key4 = Actor.BodyPart.LeftFoot;
			GameObject gameObject4 = lf;
			chaBodyParts4[key4] = ((gameObject4 != null) ? gameObject4.transform : null);
			GameObject rf = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_R");
			Dictionary<Actor.BodyPart, Transform> chaBodyParts5 = this._chaBodyParts;
			Actor.BodyPart key5 = Actor.BodyPart.RightFoot;
			GameObject gameObject5 = rf;
			chaBodyParts5[key5] = ((gameObject5 != null) ? gameObject5.transform : null);
			Animator animator = base.ChaControl.animBody;
			this.InitializeLabels();
			FullBodyBipedIK ik = animator.GetComponentInChildren<FullBodyBipedIK>(true);
			GameObject ctrlOld = this._animation.gameObject;
			ActorAnimationPlayer actorAnimationPlayer = this._animation.CloneComponent(animator.gameObject);
			actorAnimationPlayer.IK = ik;
			actorAnimationPlayer.Actor = this;
			actorAnimationPlayer.Character = this._character;
			actorAnimationPlayer.Animator = animator;
			this._animation = actorAnimationPlayer;
			UnityEngine.Object.Destroy(ctrlOld);
			AssetBundleInfo animABInfo = default(AssetBundleInfo);
			RuntimeAnimatorController rac = Singleton<Manager.Resources>.Instance.Animation.GetPlayerAnimator((int)base.ChaControl.sex, ref animABInfo);
			this.Animation.SetDefaultAnimatorController(rac);
			this.Animation.SetAnimatorController(rac);
			this.Animation.AnimABInfo = animABInfo;
			animator.Play("Locomotion", 0, 0f);
			this._character.CharacterAnimation = this._animation;
			Observable.EveryLateUpdate().Take(1).Subscribe(delegate(long _)
			{
				this._animation.IK.enabled = true;
			});
			if (this._lensEffect == null)
			{
				this._lensEffect = base.GetComponentInChildren<AQUAS_LensEffects>(true);
			}
			if (this._lensEffect)
			{
				GameObject waterObject = Singleton<Manager.Map>.Instance.WaterObject;
				if (waterObject != null)
				{
					if (this._lensEffect.gameObjects.waterPlanes.Count > 0)
					{
						this._lensEffect.gameObjects.waterPlanes[0] = waterObject;
					}
					else
					{
						this._lensEffect.gameObjects.waterPlanes.Add(waterObject);
					}
					this._lensEffect.SetActive(true);
				}
			}
			this.LoadCamera();
			base.Relocate();
			this.InitializeIK();
			this._controller.StartBehavior();
			yield break;
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x002C1EE0 File Offset: 0x002C02E0
		public void ReloadChara()
		{
			ActorAnimationPlayer actorAnimationPlayer = this._animation.CloneComponent(this.Controller.gameObject);
			actorAnimationPlayer.Actor = this;
			actorAnimationPlayer.Character = this._character;
			this._animation = actorAnimationPlayer;
			Singleton<Character>.Instance.DeleteChara(base.ChaControl, false);
			this.LoadChara(this.PlayerData.CharaFileNames[(int)this.PlayerData.Sex]);
			this.LoadEquipments();
			base.ReleaseEquipedEventItem();
			this._chaFovTargets = null;
			if (this._chaBodyParts == null)
			{
				this._chaBodyParts = new Dictionary<Actor.BodyPart, Transform>();
			}
			this._chaBodyParts.Clear();
			GameObject gameObject = base.ChaControl.animBody.transform.FindLoop("cf_J_Hips");
			this._chaBodyParts[Actor.BodyPart.Body] = ((gameObject != null) ? gameObject.transform : null);
			GameObject gameObject2 = base.ChaControl.animBody.transform.FindLoop("cf_J_Mune00");
			this._chaBodyParts[Actor.BodyPart.Bust] = ((gameObject2 != null) ? gameObject2.transform : null);
			GameObject gameObject3 = base.ChaControl.animBody.transform.FindLoop("N_Head");
			this._chaBodyParts[Actor.BodyPart.Head] = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_L");
			this._chaBodyParts[Actor.BodyPart.LeftFoot] = ((gameObject4 != null) ? gameObject4.transform : null);
			GameObject gameObject5 = base.ChaControl.animBody.transform.FindLoop("cf_J_Foot01_R");
			this._chaBodyParts[Actor.BodyPart.RightFoot] = ((gameObject5 != null) ? gameObject5.transform : null);
			Animator animBody = base.ChaControl.animBody;
			FullBodyBipedIK componentInChildren = animBody.GetComponentInChildren<FullBodyBipedIK>(true);
			ActorAnimationPlayer animation = this._animation;
			ActorAnimationPlayer actorAnimationPlayer2 = this._animation.CloneComponent(animBody.gameObject);
			actorAnimationPlayer2.IK = componentInChildren;
			actorAnimationPlayer2.Actor = this;
			actorAnimationPlayer2.Character = this._character;
			actorAnimationPlayer2.Animator = animBody;
			this._animation = actorAnimationPlayer2;
			UnityEngine.Object.Destroy(animation);
			AssetBundleInfo animABInfo = default(AssetBundleInfo);
			RuntimeAnimatorController playerAnimator = Singleton<Manager.Resources>.Instance.Animation.GetPlayerAnimator((int)base.ChaControl.sex, ref animABInfo);
			this.Animation.SetDefaultAnimatorController(playerAnimator);
			this.Animation.SetAnimatorController(playerAnimator);
			this.Animation.AnimABInfo = animABInfo;
			animBody.Play("Locomotion", 0, 0f);
			this._character.CharacterAnimation = this._animation;
			Observable.EveryLateUpdate().Take(1).Subscribe(delegate(long _)
			{
				this._animation.IK.enabled = true;
			});
			this.InitializeIK();
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x002C21A0 File Offset: 0x002C05A0
		public override void LoadEquipments()
		{
			base.LoadEquipmentItem(this.PlayerData.EquipedHeadItem, ChaControlDefine.ExtraAccessoryParts.Head);
			base.LoadEquipmentItem(this.PlayerData.EquipedBackItem, ChaControlDefine.ExtraAccessoryParts.Back);
			base.LoadEquipmentItem(this.PlayerData.EquipedNeckItem, ChaControlDefine.ExtraAccessoryParts.Neck);
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x002C21D8 File Offset: 0x002C05D8
		public IEnumerator LoadTrialAsync()
		{
			yield return this.LoadCharAsync(this.PlayerData.CharaFileNames[(int)this.PlayerData.Sex]);
			Animator animator = base.ChaControl.animBody;
			this.InitializeLabels();
			FullBodyBipedIK ik = animator.GetComponentInChildren<FullBodyBipedIK>(true);
			GameObject ctrlOld = this._animation.gameObject;
			ActorAnimationPlayer actorAnimationPlayer = this._animation.CloneComponent(animator.gameObject);
			actorAnimationPlayer.IK = ik;
			actorAnimationPlayer.Actor = this;
			actorAnimationPlayer.Character = this._character;
			actorAnimationPlayer.Animator = animator;
			this._animation = actorAnimationPlayer;
			UnityEngine.Object.Destroy(ctrlOld);
			AssetBundleInfo animABInfo = default(AssetBundleInfo);
			RuntimeAnimatorController rac = Singleton<Manager.Resources>.Instance.Animation.GetPlayerAnimator((int)base.ChaControl.sex, ref animABInfo);
			this.Animation.SetAnimatorController(rac);
			this.Animation.AnimABInfo = animABInfo;
			animator.Play("Locomotion", 0, 0f);
			this._character.CharacterAnimation = this._animation;
			Observable.EveryLateUpdate().Take(1).Subscribe(delegate(long _)
			{
				this._animation.IK.enabled = true;
			});
			if (this._lensEffect == null)
			{
				this._lensEffect = base.GetComponentInChildren<AQUAS_LensEffects>(true);
			}
			if (this._lensEffect)
			{
				GameObject waterObject = Singleton<Manager.Map>.Instance.WaterObject;
				if (waterObject != null)
				{
					if (this._lensEffect.gameObjects.waterPlanes.Count > 0)
					{
						this._lensEffect.gameObjects.waterPlanes[0] = waterObject;
					}
					else
					{
						this._lensEffect.gameObjects.waterPlanes.Add(waterObject);
					}
					this._lensEffect.SetActive(true);
				}
			}
			this.LoadTrialCamera();
			base.Relocate();
			this.InitializeIK();
			this._controller.StartBehavior();
			yield break;
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x002C21F4 File Offset: 0x002C05F4
		public void LoadCamera()
		{
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string cameraAdd = definePack.ABPaths.CameraAdd05;
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			string add = definePack.ABManifests.Add05;
			this.LoadCamera(cameraAdd, commonDefine.FileNames.MainCameraName, add);
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x002C2244 File Offset: 0x002C0644
		public void LoadTrialCamera()
		{
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string camera = definePack.ABPaths.Camera;
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			string @default = definePack.ABManifests.Default;
			this.LoadCamera(camera, commonDefine.FileNames.TrialCamera, @default);
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x002C2294 File Offset: 0x002C0694
		private void LoadCamera(string assetBundle, string asset, string manifest = null)
		{
			GameObject original = CommonLib.LoadAsset<GameObject>(assetBundle, asset, false, manifest);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.SetParent(base.transform, false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			this._cameraCtrl = gameObject.GetComponentInChildren<ActorCameraControl>();
			this._cameraCtrl.LocomotionSetting = this._locomotionSetting;
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			GameObject original2 = CommonLib.LoadAsset<GameObject>(assetBundle, commonDefine.FileNames.NormalCameraName, false, manifest);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2);
			gameObject2.transform.SetParent(this._cameraCtrl.VirtualCameraRoot, false);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localRotation = Quaternion.identity;
			GameObject original3 = CommonLib.LoadAsset<GameObject>(assetBundle, commonDefine.FileNames.ActionCameraFreeLookName, false, manifest);
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(original3);
			gameObject3.transform.SetParent(this._cameraCtrl.VirtualCameraRoot, false);
			gameObject3.transform.localPosition = Vector3.zero;
			gameObject3.transform.localRotation = Quaternion.identity;
			CameraTable component = gameObject2.GetComponent<CameraTable>();
			CameraTable component2 = gameObject3.GetComponent<CameraTable>();
			this._cameraCtrl.AssignCameraTable(component, component2);
			GameObject original4 = CommonLib.LoadAsset<GameObject>(assetBundle, commonDefine.FileNames.ActionCameraNotMoveName, false, manifest);
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(original4);
			gameObject4.transform.SetParent(this._cameraCtrl.VirtualCameraRoot, false);
			this._cameraCtrl.ActionCameraNotMove = gameObject4.GetComponentInChildren<CinemachineVirtualCamera>();
			this._cameraCtrl.ActionCameraNotMove.Follow = this._locomotionSetting.ActionFollow;
			GameObject original5 = CommonLib.LoadAsset<GameObject>(assetBundle, commonDefine.FileNames.FishingCamera, false, manifest);
			GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(original5);
			gameObject5.transform.SetParent(base.transform, false);
			this._cameraCtrl.FishingLocalPosition = gameObject5.transform.localPosition;
			this._cameraCtrl.FishingLocalRotation = gameObject5.transform.localRotation;
			this._cameraCtrl.FishingCamera = gameObject5.GetComponent<CinemachineVirtualCamera>();
			this._cameraCtrl.SetLensSetting(Singleton<Manager.Resources>.Instance.LocomotionProfile.DefaultLensSetting);
			this._cameraCtrl.XAxisValue = base.Rotation.eulerAngles.y;
			this._cameraCtrl.YAxisValue = 0.6f;
			this._cameraCtrl.enabled = true;
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x002C2508 File Offset: 0x002C0908
		protected override void InitializeIK()
		{
			base.InitializeIK();
			string assetName = (base.ChaControl.fileParam.sex != 0) ? "HandsTarget_F" : "HandsTarget_M";
			GameObject original = CommonLib.LoadAsset<GameObject>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.ActorPrefab, assetName, false, "abdata");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, base.transform);
			HandsHolder component = gameObject.GetComponent<HandsHolder>();
			this.HandsHolder = component;
			HandsHolder handsHolder = component;
			handsHolder.enabled = false;
			handsHolder.EnabledHolding = false;
			handsHolder.LeftHandAnimator = base.ChaControl.animBody;
			handsHolder.LeftHandIK = base.ChaControl.fullBodyIK;
			GameObject gameObject2 = base.ChaControl.animBody.transform.FindLoop("cf_J_Kosi02");
			handsHolder.LeftLookTarget = gameObject2.transform;
			if (!Singleton<Manager.Resources>.Instance.LocomotionProfile.HoldingHandTarget.IsNullOrEmpty())
			{
				GameObject gameObject3 = base.ChaControl.animBody.transform.FindLoop(Singleton<Manager.Resources>.Instance.LocomotionProfile.HoldingHandTarget);
				if (gameObject3 != null)
				{
					handsHolder.RightHandTarget = gameObject3.transform;
				}
			}
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x002C2634 File Offset: 0x002C0A34
		protected override IEnumerator LoadCharAsync(string fileName)
		{
			ChaFileControl chaFile;
			if (!fileName.IsNullOrEmpty())
			{
				chaFile = new ChaFileControl();
				if (!chaFile.LoadCharaFile(fileName, this.PlayerData.Sex, false, true))
				{
					chaFile = null;
				}
			}
			else
			{
				chaFile = null;
				string text;
				if (this.PlayerData.Sex == 0)
				{
					text = "ill_Default_Male";
				}
				else
				{
					text = "ill_Default_Female";
				}
				Dictionary<int, ListInfoBase> categoryInfo;
				if (this.PlayerData.Sex == 0)
				{
					categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.cha_sample_m);
				}
				else
				{
					categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.cha_sample_f);
				}
				foreach (KeyValuePair<int, ListInfoBase> keyValuePair in categoryInfo)
				{
					if (keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainData) == text)
					{
						chaFile = new ChaFileControl();
						chaFile.LoadFromAssetBundle(keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainAB), text, false, true);
						break;
					}
				}
			}
			if (this.PlayerData.Sex == 0)
			{
				base.ChaControl = Singleton<Character>.Instance.CreateChara(0, base.gameObject, 0, chaFile);
			}
			else
			{
				base.ChaControl = Singleton<Character>.Instance.CreateChara(1, base.gameObject, 0, chaFile);
			}
			base.ChaControl.isPlayer = true;
			base.ChaControl.Load(false);
			yield return null;
			base.ChaControl.ChangeLookEyesPtn(3);
			base.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.Controller.InitializeFaceLight(base.ChaControl.gameObject);
			yield return null;
			yield break;
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x002C2658 File Offset: 0x002C0A58
		private void LoadChara(string fileName)
		{
			ChaFileControl chaFileControl;
			if (!fileName.IsNullOrEmpty())
			{
				chaFileControl = new ChaFileControl();
				if (!chaFileControl.LoadCharaFile(fileName, this.PlayerData.Sex, false, true))
				{
					chaFileControl = null;
				}
			}
			else
			{
				chaFileControl = null;
				string text;
				if (this.PlayerData.Sex == 0)
				{
					text = "ill_Default_Male";
				}
				else
				{
					text = "ill_Default_Female";
				}
				Dictionary<int, ListInfoBase> categoryInfo;
				if (this.PlayerData.Sex == 0)
				{
					categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.cha_sample_m);
				}
				else
				{
					categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.cha_sample_f);
				}
				foreach (KeyValuePair<int, ListInfoBase> keyValuePair in categoryInfo)
				{
					if (keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainData) == text)
					{
						chaFileControl = new ChaFileControl();
						chaFileControl.LoadFromAssetBundle(keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainAB), text, false, true);
						break;
					}
				}
			}
			if (this.PlayerData.Sex == 0)
			{
				base.ChaControl = Singleton<Character>.Instance.CreateChara(0, base.gameObject, 0, chaFileControl);
			}
			else
			{
				base.ChaControl = Singleton<Character>.Instance.CreateChara(1, base.gameObject, 0, chaFileControl);
			}
			base.ChaControl.isPlayer = true;
			base.ChaControl.Load(false);
			base.ChaControl.ChangeLookEyesPtn(3);
			base.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.Controller.InitializeFaceLight(base.ChaControl.gameObject);
		}

		// Token: 0x060067D7 RID: 26583 RVA: 0x002C2804 File Offset: 0x002C0C04
		private void OnEnable()
		{
			this.Animation.enabled = true;
			this.Locomotor.enabled = true;
			this.Controller.enabled = true;
		}

		// Token: 0x060067D8 RID: 26584 RVA: 0x002C282A File Offset: 0x002C0C2A
		private void OnDisable()
		{
			this.Animation.enabled = false;
			this.Locomotor.enabled = false;
			this.Controller.enabled = false;
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x002C2850 File Offset: 0x002C0C50
		private void OnDestroy()
		{
		}

		// Token: 0x060067DA RID: 26586 RVA: 0x002C2852 File Offset: 0x002C0C52
		public override void EnableEntity()
		{
			this.CameraControl.enabled = true;
			this.Animation.enabled = true;
			this.Controller.enabled = true;
			this.Locomotor.enabled = true;
			base.ChaControl.visibleAll = true;
		}

		// Token: 0x060067DB RID: 26587 RVA: 0x002C2890 File Offset: 0x002C0C90
		public override void DisableEntity()
		{
			this.CameraControl.enabled = false;
			this.Animation.enabled = false;
			this.Controller.enabled = false;
			this.Locomotor.enabled = false;
			base.ChaControl.visibleAll = false;
		}

		// Token: 0x060067DC RID: 26588 RVA: 0x002C28D0 File Offset: 0x002C0CD0
		private void OnUpdate()
		{
			this._navMeshObstacle.transform.position = base.Position;
			this._coolTime += Time.deltaTime;
			PlayerData playerData = this.PlayerData;
			if (this._called)
			{
				this._callElapsedTime += Time.unscaledDeltaTime;
				if (this._callElapsedTime > 2f)
				{
					this._called = false;
					this._callElapsedTime = 0f;
				}
			}
			if (this._schedule.enabled && !this._schedule.useGameTime && this._schedule.progress)
			{
				this._schedule.elapsedTime = this._schedule.elapsedTime + Time.deltaTime;
				if (this._schedule.elapsedTime > this._schedule.duration)
				{
					this._schedule.enabled = false;
				}
			}
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
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			EnvironmentSimulator simulator = Singleton<Manager.Map>.Instance.Simulator;
			if (simulator.EnabledTimeProgression)
			{
				Weather weather = simulator.Weather;
				if (base.AreaType == MapArea.AreaType.Indoor)
				{
					playerData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
				}
				else
				{
					if (weather != Weather.Rain)
					{
						if (weather != Weather.Storm)
						{
							playerData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
						}
						else
						{
							playerData.Wetness += statusProfile.WetRateInStorm * Time.deltaTime;
						}
					}
					else
					{
						playerData.Wetness += statusProfile.WetRateInRain * Time.deltaTime;
					}
					playerData.Wetness = Mathf.Clamp(playerData.Wetness, 0f, 100f);
				}
			}
			if (base.ChaControl != null)
			{
				float wetRate = Mathf.InverseLerp(0f, 100f, playerData.Wetness);
				base.ChaControl.wetRate = wetRate;
			}
			playerData.Position = base.Position;
			playerData.Rotation = base.Rotation;
			playerData.ChunkID = base.ChunkID;
			if (this._mapAreaID != null && base.MapArea != null)
			{
				this._mapAreaID.Value = base.MapArea.AreaID;
				this.PlayerData.AreaID = base.MapArea.AreaID;
			}
			if (this.HandsHolder != null)
			{
				this.HandsHolder.BaseTransform.position = base.Position;
				this.HandsHolder.BaseTransform.rotation = base.Rotation;
			}
		}

		// Token: 0x060067DD RID: 26589 RVA: 0x002C2C20 File Offset: 0x002C1020
		private void OnLateUpdate()
		{
			if (this._cameraCtrl != null && this._cameraCtrl.enabled)
			{
				if (!(this.Controller.State is Communication) && !(this.Controller.State is Sex) && !(this.Controller.State is Fishing))
				{
					if (this._cameraCtrl.Mode != CameraMode.ADV && this._cameraCtrl.Mode != CameraMode.Fishing && this._cameraCtrl.Mode != CameraMode.Event)
					{
						bool flag = true;
						flag &= (this._cameraCtrl.ShotType != ShotType.PointOfView);
						if (flag)
						{
							if (!(this.Controller.State is Move))
							{
								flag &= base.IsVisibleDistanceAll(this._cameraCtrl.transform);
							}
							else
							{
								flag &= base.IsVisibleDistance(this._cameraCtrl.transform, Actor.BodyPart.Head, Singleton<Manager.Resources>.Instance.LocomotionProfile.CharaVisibleDistance);
							}
						}
						base.ChaControl.visibleAll = (base.IsVisible && flag);
						if (this.Mode == Desire.ActionType.Onbu && base.Partner != null)
						{
							base.Partner.IsVisible = (base.IsVisible && flag);
						}
					}
				}
				else if (this.Controller.State is Fishing)
				{
					bool flag2 = base.IsVisibleDistanceAll(this._cameraCtrl.transform);
					base.ChaControl.visibleAll = (base.IsVisible && flag2);
				}
			}
		}

		// Token: 0x060067DE RID: 26590 RVA: 0x002C2DC8 File Offset: 0x002C11C8
		public void ActivateTransfer()
		{
			EquipEventItemInfo itemInfo = null;
			PlayState playState;
			this.LoadLocomotionAnimation(out playState, ref itemInfo);
			base.ResetEquipEventItem(itemInfo);
			ActorAnimation animation = this.Animation;
			AnimatorStateInfo currentAnimatorStateInfo = this.Animation.Animator.GetCurrentAnimatorStateInfo(0);
			if (playState != null)
			{
				animation.InitializeStates(playState);
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
						float layerWeight = animation.Animator.GetLayerWeight(playState.MaskStateInfo.layer);
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
			}
			else
			{
				for (int k = 1; k < animation.Animator.layerCount; k++)
				{
					animation.Animator.SetLayerWeight(k, 0f);
				}
				string normalLocoStateName = Singleton<Manager.Resources>.Instance.PlayerProfile.PoseIDData.NormalLocoStateName;
				int num = Animator.StringToHash(normalLocoStateName);
				if (currentAnimatorStateInfo.shortNameHash != num)
				{
					animation.StopAllAnimCoroutine();
					if (animation.AnimInfo.endEnableBlend)
					{
						this.Animation.CrossFadeAnimation(normalLocoStateName, animation.AnimInfo.outBlendSec, 0, 0f);
					}
					else
					{
						this.CameraControl.CrossFade.FadeStart(-1f);
						this.Animation.PlayAnimation(normalLocoStateName, 0, 0f);
					}
				}
			}
		}

		// Token: 0x060067DF RID: 26591 RVA: 0x002C3080 File Offset: 0x002C1480
		private void LoadLocomotionAnimation(out PlayState info, ref EquipEventItemInfo itemInfo)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			LocomotionProfile locomotionProfile = instance.LocomotionProfile;
			PlayerProfile playerProfile = instance.PlayerProfile;
			StuffItem equipedLampItem = this.PlayerData.EquipedLampItem;
			CommonDefine.ItemIDDefines itemIDDefine = instance.CommonDefine.ItemIDDefine;
			if (equipedLampItem != null)
			{
				ItemIDKeyPair torchID = itemIDDefine.TorchID;
				ItemIDKeyPair flashlightID = itemIDDefine.FlashlightID;
				ItemIDKeyPair maleLampID = itemIDDefine.MaleLampID;
				if (equipedLampItem.CategoryID == torchID.categoryID && equipedLampItem.ID == torchID.itemID)
				{
					info = instance.Animation.PlayerLocomotionStateTable[(int)base.ChaControl.sex][playerProfile.PoseIDData.TorchLocoID];
					itemInfo = instance.GameInfo.CommonEquipEventItemTable[torchID.categoryID][torchID.itemID];
					itemInfo.ParentName = instance.LocomotionProfile.PlayerLocoItemParentName;
					return;
				}
				if (equipedLampItem.CategoryID == flashlightID.categoryID && equipedLampItem.ID == flashlightID.itemID)
				{
					info = instance.Animation.PlayerLocomotionStateTable[(int)base.ChaControl.sex][playerProfile.PoseIDData.TorchLocoID];
					itemInfo = instance.GameInfo.CommonEquipEventItemTable[flashlightID.categoryID][flashlightID.itemID];
					itemInfo.ParentName = instance.LocomotionProfile.PlayerLocoItemParentName;
					return;
				}
				if (equipedLampItem.CategoryID == maleLampID.categoryID && equipedLampItem.ID == maleLampID.itemID)
				{
					info = instance.Animation.PlayerLocomotionStateTable[(int)base.ChaControl.sex][playerProfile.PoseIDData.LampLocoID];
					itemInfo = instance.GameInfo.CommonEquipEventItemTable[maleLampID.categoryID][maleLampID.itemID];
					itemInfo.ParentName = instance.LocomotionProfile.PlayerLocoItemParentName;
					return;
				}
			}
			info = null;
		}

		// Token: 0x060067E0 RID: 26592 RVA: 0x002C327C File Offset: 0x002C167C
		public override void OnMinuteUpdated(TimeSpan deltaTime)
		{
			this._elapsedTimeInSleep += deltaTime;
			if (this._schedule.enabled && this._schedule.useGameTime && this._schedule.progress)
			{
				this._schedule.elapsedTime = this._schedule.elapsedTime + (float)deltaTime.TotalMinutes;
				if (this._schedule.elapsedTime > this._schedule.duration)
				{
					this._schedule.enabled = false;
				}
			}
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x002C330C File Offset: 0x002C170C
		public void ReleaseCurrentPoint()
		{
			if (base.CurrentPoint != null)
			{
				base.SetDefaultStateHousingItem();
				ActionPoint currentPoint = base.CurrentPoint;
				base.CurrentPoint = null;
				CommandArea commandArea = this.PlayerController.CommandArea;
				commandArea.RemoveConsiderationObject(currentPoint);
				commandArea.RefreshCommands();
			}
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x002C3358 File Offset: 0x002C1758
		public override bool CanAddItem(StuffItem sourceItem)
		{
			if (this.PlayerData.ItemList.Count >= this.PlayerData.InventorySlotMax)
			{
				foreach (StuffItem stuffItem in this.PlayerData.ItemList)
				{
					if (stuffItem.CategoryID == sourceItem.CategoryID && stuffItem.ID == sourceItem.ID && stuffItem.Count + sourceItem.Count < Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemStackUpperLimit)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x002C3428 File Offset: 0x002C1828
		public override bool CanAddItem(StuffItemInfo item)
		{
			if (this.PlayerData.ItemList.Count >= this.PlayerData.InventorySlotMax)
			{
				foreach (StuffItem stuffItem in this.PlayerData.ItemList)
				{
					if (stuffItem.CategoryID == item.CategoryID && stuffItem.ID == item.ID && stuffItem.Count < Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemStackUpperLimit)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x002C34F0 File Offset: 0x002C18F0
		protected override void LoadEquipedEventItem(EquipEventItemInfo eventItemInfo)
		{
			AssetBundleInfo assetbundleInfo = eventItemInfo.ActionItemInfo.assetbundleInfo;
			if (assetbundleInfo.assetbundle.IsNullOrEmpty() || assetbundleInfo.asset.IsNullOrEmpty() || assetbundleInfo.manifest.IsNullOrEmpty())
			{
				return;
			}
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetbundleInfo.assetbundle, assetbundleInfo.asset, false, assetbundleInfo.manifest);
			if (gameObject != null)
			{
				string playerLocoItemParentName = Singleton<Manager.Resources>.Instance.LocomotionProfile.PlayerLocoItemParentName;
				GameObject gameObject2 = base.ChaControl.animBody.transform.FindLoop(playerLocoItemParentName);
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

		// Token: 0x060067E5 RID: 26597 RVA: 0x002C36A4 File Offset: 0x002C1AA4
		public override void LoadEventItems(PlayState playState)
		{
			if (playState.ItemInfoCount > 0)
			{
				for (int i = 0; i < playState.ItemInfoCount; i++)
				{
					PlayState.ItemInfo itemInfo = playState.GetItemInfo(i);
					Manager.Resources instance = Singleton<Manager.Resources>.Instance;
					ActionItemInfo eventItemInfo;
					if (itemInfo.fromEquipedItem)
					{
						ActionPointInfo actionPointInfo;
						Dictionary<int, EquipEventItemInfo> dictionary;
						EquipEventItemInfo equipEventItemInfo;
						if (base.CurrentPoint.TryGetPlayerActionPointInfo(base.EventKey, out actionPointInfo) && instance.GameInfo.SearchEquipEventItemTable.TryGetValue(actionPointInfo.searchAreaID, out dictionary) && dictionary.TryGetValue(actionPointInfo.gradeValue, out equipEventItemInfo))
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

		// Token: 0x060067E6 RID: 26598 RVA: 0x002C3784 File Offset: 0x002C1B84
		public override void LoadEventParticles(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> dictionary;
			Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary2;
			Dictionary<int, List<AnimeParticleEventInfo>> dictionary3;
			if (Singleton<Manager.Resources>.Instance.Animation.PlayerActParticleEventKeyTable.TryGetValue((int)base.ChaControl.sex, out dictionary) && dictionary.TryGetValue(eventID, out dictionary2) && dictionary2.TryGetValue(poseID, out dictionary3) && dictionary3 != null)
			{
				base.LoadEventParticle(dictionary3);
			}
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x002C37E0 File Offset: 0x002C1BE0
		public void AddTutorialUI(Popup.Tutorial.Type type, bool groupDisplay)
		{
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			Dictionary<int, bool> dictionary = (worldData == null) ? null : worldData.TutorialOpenStateTable;
			if (dictionary != null)
			{
				bool flag = false;
				if (!dictionary.TryGetValue((int)type, out flag))
				{
					flag = (dictionary[(int)type] = false);
				}
				if (flag)
				{
					return;
				}
				if (!flag)
				{
					dictionary[(int)type] = true;
				}
			}
			this.TutorialIndexList.Add(new UnityEx.ValueTuple<Popup.Tutorial.Type, bool>(type, groupDisplay));
		}

		// Token: 0x040058E6 RID: 22758
		private float _coolTime;

		// Token: 0x040058E8 RID: 22760
		private bool _called;

		// Token: 0x040058E9 RID: 22761
		private float _callElapsedTime;

		// Token: 0x040058EA RID: 22762
		private const float _callCoolTime = 2f;

		// Token: 0x040058EB RID: 22763
		private IEnumerator _enumerator;

		// Token: 0x040058EC RID: 22764
		private IDisposable _disposable;

		// Token: 0x040058ED RID: 22765
		private static readonly DateTime _midnightTime = new DateTime(1, 1, 1, 23, 59, 59);

		// Token: 0x040058EE RID: 22766
		private TimeSpan _elapsedTimeInSleep = default(TimeSpan);

		// Token: 0x040058EF RID: 22767
		private IEnumerator _sleepEventEnumerator;

		// Token: 0x040058F0 RID: 22768
		private IDisposable _sleepEventDisposable;

		// Token: 0x040058F1 RID: 22769
		private IEnumerator _eatEventEnumerator;

		// Token: 0x040058F2 RID: 22770
		private IDisposable _eatEventDisposable;

		// Token: 0x040058F3 RID: 22771
		private IEnumerator _exitEatEventEnumerator;

		// Token: 0x040058F4 RID: 22772
		private IDisposable _exitEatEventDisposable;

		// Token: 0x040058F5 RID: 22773
		private IEnumerator _hizamakuraEventEnumerator;

		// Token: 0x040058F6 RID: 22774
		private IDisposable _hizamakuraEventDisposable;

		// Token: 0x040058F8 RID: 22776
		[SerializeField]
		protected ActorAnimationPlayer _animation;

		// Token: 0x040058F9 RID: 22777
		[SerializeField]
		private ActorCameraControl _cameraCtrl;

		// Token: 0x040058FA RID: 22778
		[SerializeField]
		private ActorCameraControl.LocomotionSettingData _locomotionSetting;

		// Token: 0x040058FB RID: 22779
		private Vector3 _defaultOfs = Vector3.zero;

		// Token: 0x040058FC RID: 22780
		[SerializeField]
		private Vector3 _normalOffset = Vector3.zero;

		// Token: 0x040058FD RID: 22781
		[SerializeField]
		private Vector3 _behaviourOffset = Vector3.zero;

		// Token: 0x040058FE RID: 22782
		[SerializeField]
		private AQUAS_LensEffects _lensEffect;

		// Token: 0x040058FF RID: 22783
		[SerializeField]
		private GameObject _cameraTarget;

		// Token: 0x04005900 RID: 22784
		[SerializeField]
		private ActorLocomotionThirdPerson _character;

		// Token: 0x04005901 RID: 22785
		[SerializeField]
		private PlayerController _controller;

		// Token: 0x0400590B RID: 22795
		private bool? _scheduledInteractionState = new bool?(false);
	}
}
