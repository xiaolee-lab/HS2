using System;
using System.Collections.Generic;
using ADV;
using AIChara;
using AIProject.Definitions;
using AIProject.SaveData;
using Cinemachine;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DE7 RID: 3559
	public class CharaChange : PlayerStateBase
	{
		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06006E0A RID: 28170 RVA: 0x002F0A15 File Offset: 0x002EEE15
		private OpenData openData { get; } = new OpenData();

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06006E0B RID: 28171 RVA: 0x002F0A1D File Offset: 0x002EEE1D
		private PackData packData { get; } = new PackData();

		// Token: 0x06006E0C RID: 28172 RVA: 0x002F0A28 File Offset: 0x002EEE28
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
				this._onEndAction.Take(1).Subscribe(delegate(Unit __)
				{
					this.Elapsed(player);
				});
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

		// Token: 0x06006E0D RID: 28173 RVA: 0x002F0B28 File Offset: 0x002EEF28
		protected override void OnRelease(PlayerActor player)
		{
			player.CameraControl.EventCameraLocator.runtimeAnimatorController = null;
			player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = this._prevStyle;
			player.CameraControl.Mode = CameraMode.Normal;
			player.ChaControl.visibleAll = true;
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x002F0B9C File Offset: 0x002EEF9C
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
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x002F0C0B File Offset: 0x002EF00B
		private void Elapsed(PlayerActor player)
		{
			player.CurrentDevicePoint = null;
			MapUIContainer.SetVisibleHUD(true);
			MapUIContainer.StorySupportUI.Open();
			player.PlayerController.CommandArea.UpdateCollision(player);
			player.Controller.ChangeState("Normal");
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x002F0C48 File Offset: 0x002EF048
		private void Refresh(PlayerActor player)
		{
			ChaFileControl chaFileControl = new ChaFileControl();
			int id = player.CurrentDevicePoint.ID;
			int num = 0;
			foreach (KeyValuePair<int, string> keyValuePair in Singleton<Manager.Map>.Instance.ChangedCharaFiles)
			{
				string value = keyValuePair.Value;
				AgentData agentData = Singleton<Game>.Instance.WorldData.AgentTable[keyValuePair.Key];
				if (agentData.ParameterLock)
				{
					agentData.ParameterLock = false;
				}
				bool flag = !value.IsNullOrEmpty() && Config.GraphicData.CharasEntry[keyValuePair.Key] && chaFileControl.LoadCharaFile(value, 1, false, true) && agentData.MapID == Singleton<Manager.Map>.Instance.MapID;
				AgentActor agentActor;
				bool flag2 = Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(keyValuePair.Key, out agentActor);
				if (flag2)
				{
					agentActor.DisableBehavior();
					agentActor.ClearItems();
					agentActor.ClearParticles();
					Actor.BehaviorSchedule schedule = agentActor.Schedule;
					schedule.enabled = false;
					agentActor.Schedule = schedule;
					agentActor.TargetInSightActor = null;
					if (agentActor.CurrentPoint != null)
					{
						agentActor.CurrentPoint.SetActiveMapItemObjs(true);
						agentActor.CurrentPoint.ReleaseSlot(agentActor);
						agentActor.CurrentPoint = null;
					}
					agentActor.TargetInSightActionPoint = null;
					if (agentActor.Partner != null)
					{
						if (agentActor.Partner is PlayerActor)
						{
							PlayerActor playerActor = agentActor.Partner as PlayerActor;
							playerActor.PlayerController.ChangeState("Normal");
						}
						else if (agentActor.Partner is AgentActor)
						{
							AgentActor agentActor2 = agentActor.Partner as AgentActor;
							agentActor.StopLesbianSequence();
							agentActor2.StopLesbianSequence();
							agentActor2.Animation.EndIgnoreEvent();
							Game.Expression expression = Singleton<Game>.Instance.GetExpression(agentActor2.ChaControl.fileParam.personality, "標準");
							if (expression != null)
							{
								expression.Change(agentActor2.ChaControl);
							}
							agentActor2.Animation.ResetDefaultAnimatorController();
							agentActor2.ChangeBehavior(Desire.ActionType.Normal);
						}
						else if (agentActor.Partner is MerchantActor)
						{
							MerchantActor merchantActor = agentActor.Partner as MerchantActor;
							agentActor.StopLesbianSequence();
							merchantActor.ResetState();
							merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
						}
						agentActor.Partner = null;
					}
					if (agentActor.CommandPartner != null)
					{
						if (agentActor.CommandPartner is AgentActor)
						{
							AgentActor agentActor3 = agentActor.CommandPartner as AgentActor;
							agentActor3.ChangeBehavior(Desire.ActionType.Normal);
						}
						else if (agentActor.CommandPartner is MerchantActor)
						{
							MerchantActor merchantActor2 = agentActor.CommandPartner as MerchantActor;
							merchantActor2.ChangeBehavior(merchantActor2.LastNormalMode);
						}
						agentActor.CommandPartner = null;
					}
					Singleton<Manager.Map>.Instance.RemoveAgent(agentActor);
					if (flag)
					{
						agentActor = Singleton<Manager.Map>.Instance.AddAgent(keyValuePair.Key, agentData);
						agentData.PrevMapID = null;
					}
					else
					{
						agentActor = null;
					}
				}
				else if (flag)
				{
					agentActor = Singleton<Manager.Map>.Instance.AddAgent(keyValuePair.Key, agentData);
					agentData.PrevMapID = null;
				}
				else
				{
					agentActor = null;
				}
				if (agentActor != null)
				{
					agentActor.RefreshWalkStatus(Singleton<Manager.Map>.Instance.PointAgent.Waypoints);
					Singleton<Manager.Map>.Instance.InitSearchActorTargets(agentActor);
					player.PlayerController.CommandArea.AddCommandableObject(agentActor);
					foreach (KeyValuePair<int, AgentActor> keyValuePair2 in Singleton<Manager.Map>.Instance.AgentTable)
					{
						if (!(keyValuePair2.Value == agentActor))
						{
							keyValuePair2.Value.AddActor(agentActor);
						}
					}
					agentActor.ActivateNavMeshAgent();
					Transform transform = player.CurrentDevicePoint.RecoverPoints[num++];
					agentActor.NavMeshAgent.Warp(transform.position);
					agentActor.Rotation = transform.rotation;
					agentActor.EnableBehavior();
					agentActor.ChangeBehavior(Desire.ActionType.Normal);
				}
			}
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x04005B71 RID: 23409
		protected int _currentState = -1;

		// Token: 0x04005B72 RID: 23410
		private Subject<Unit> _onEndFadeIn = new Subject<Unit>();

		// Token: 0x04005B73 RID: 23411
		private Subject<Unit> _onEndFadeOut = new Subject<Unit>();

		// Token: 0x04005B74 RID: 23412
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005B75 RID: 23413
		private bool _completeWait;

		// Token: 0x04005B78 RID: 23416
		private CinemachineBlendDefinition.Style _prevStyle;
	}
}
