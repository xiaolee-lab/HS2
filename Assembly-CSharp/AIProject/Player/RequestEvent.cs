using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.Scene;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E0D RID: 3597
	public class RequestEvent : PlayerStateBase
	{
		// Token: 0x06006F51 RID: 28497 RVA: 0x002FC08C File Offset: 0x002FA48C
		protected override void OnAwake(PlayerActor player)
		{
			RequestEvent.<OnAwake>c__AnonStorey0 <OnAwake>c__AnonStorey = new RequestEvent.<OnAwake>c__AnonStorey0();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.$this = this;
			this._eventPoint = <OnAwake>c__AnonStorey.player.CurrentEventPoint;
			if (this._eventPoint != null)
			{
				RequestEvent.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey2 = new RequestEvent.<OnAwake>c__AnonStorey1();
				<OnAwake>c__AnonStorey2.<>f__ref$0 = <OnAwake>c__AnonStorey;
				RequestEvent.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey3 = <OnAwake>c__AnonStorey2;
				CommonDefine.EventStoryInfoGroup playInfo;
				if (Singleton<Manager.Resources>.IsInstance())
				{
					CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
					playInfo = ((commonDefine != null) ? commonDefine.EventStoryInfo : null);
				}
				else
				{
					playInfo = null;
				}
				<OnAwake>c__AnonStorey3.playInfo = playInfo;
				RequestEvent.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey4 = <OnAwake>c__AnonStorey2;
				Dictionary<int, List<string>> textTable;
				if (Singleton<Manager.Resources>.IsInstance())
				{
					Manager.Resources.MapTables map = Singleton<Manager.Resources>.Instance.Map;
					textTable = ((map != null) ? map.EventPointCommandLabelTextTable : null);
				}
				else
				{
					textTable = null;
				}
				<OnAwake>c__AnonStorey4.textTable = textTable;
				MapUIContainer.RequestUI.CancelEvent = delegate()
				{
					EventPoint.ChangePrevPlayerMode();
				};
				MapUIContainer.RequestUI.ClosedEvent = delegate()
				{
					<OnAwake>c__AnonStorey2.<>f__ref$0.player.CurrentEventPoint = null;
				};
				MapUIContainer.RequestUI.SubmitEvent = delegate()
				{
					if (<OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint == null)
					{
						EventPoint.ChangePrevPlayerMode();
						return;
					}
					<OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint.RemoveConsiderationCommand();
					int eventID = <OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint.EventID;
					int groupID = <OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint.GroupID;
					int pointID = <OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint.PointID;
					switch (eventID)
					{
					case 0:
						if (<OnAwake>c__AnonStorey2.<>f__ref$0.player == null || <OnAwake>c__AnonStorey2.playInfo == null)
						{
							EventPoint.ChangePrevPlayerMode();
							return;
						}
						<OnAwake>c__AnonStorey2.<>f__ref$0.player.PlayerController.ChangeState("Idle");
						EventPoint.OpenEventStart(<OnAwake>c__AnonStorey2.<>f__ref$0.player, <OnAwake>c__AnonStorey2.playInfo.StartEventFadeTime, <OnAwake>c__AnonStorey2.playInfo.EndEventFadeTime, <OnAwake>c__AnonStorey2.playInfo.Generator.SEID, <OnAwake>c__AnonStorey2.playInfo.Generator.SoundPlayDelayTime, <OnAwake>c__AnonStorey2.playInfo.Generator.EndIntervalTime, delegate
						{
							if (Singleton<Manager.Map>.IsInstance())
							{
								Singleton<Manager.Map>.Instance.SetTimeRelationAreaOpenState(0, true);
							}
							Manager.Map.ForcedSetTutorialProgress(25);
						}, delegate
						{
							if (!<OnAwake>c__AnonStorey2.textTable.IsNullOrEmpty<int, List<string>>())
							{
								List<string> source;
								<OnAwake>c__AnonStorey2.textTable.TryGetValue(6, out source);
								string mes = source.GetElement(EventPoint.LangIdx) ?? string.Empty;
								MapUIContainer.PushMessageUI(mes, 0, 0, null);
							}
							EventPoint.ChangePrevPlayerMode();
						});
						break;
					case 1:
						if (<OnAwake>c__AnonStorey2.<>f__ref$0.player == null || <OnAwake>c__AnonStorey2.playInfo == null)
						{
							EventPoint.ChangePrevPlayerMode();
							return;
						}
						<OnAwake>c__AnonStorey2.<>f__ref$0.player.PlayerController.ChangeState("Idle");
						EventPoint.OpenEventStart(<OnAwake>c__AnonStorey2.<>f__ref$0.player, <OnAwake>c__AnonStorey2.playInfo.StartEventFadeTime, <OnAwake>c__AnonStorey2.playInfo.EndEventFadeTime, <OnAwake>c__AnonStorey2.playInfo.ShipRepair.SEID, <OnAwake>c__AnonStorey2.playInfo.ShipRepair.SoundPlayDelayTime, <OnAwake>c__AnonStorey2.playInfo.ShipRepair.EndIntervalTime, delegate
						{
							if (<OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint != null)
							{
								<OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint.SetDedicatedNumber(1);
								<OnAwake>c__AnonStorey2.<>f__ref$0.$this.ChangeGameCleared();
							}
						}, delegate
						{
							StoryPointEffect storyPointEffect = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.StoryPointEffect;
							if (storyPointEffect != null)
							{
								storyPointEffect.FadeOutAndDestroy();
							}
							if (!<OnAwake>c__AnonStorey2.textTable.IsNullOrEmpty<int, List<string>>())
							{
								List<string> source;
								<OnAwake>c__AnonStorey2.textTable.TryGetValue(7, out source);
								string mes = source.GetElement(EventPoint.LangIdx) ?? string.Empty;
								MapUIContainer.PushMessageUI(mes, 0, 0, null);
							}
							int number = 28;
							Manager.Map.ForcedSetTutorialProgress(number);
							float num = (!Singleton<Manager.Resources>.IsInstance()) ? 5f : Singleton<Manager.Resources>.Instance.CommonDefine.EventStoryInfo.StoryCompleteNextSupportChangeTime;
							MapScene instance = Singleton<MapScene>.Instance;
							Observable.Timer(TimeSpan.FromSeconds((double)num)).Subscribe(delegate(long _)
							{
								int number2 = 29;
								Manager.Map.ForcedSetTutorialProgressAndUIUpdate(number2);
							}).AddTo(instance);
							EventPoint.ChangePrevPlayerMode();
						});
						break;
					case 2:
						if (<OnAwake>c__AnonStorey2.<>f__ref$0.player == null || <OnAwake>c__AnonStorey2.playInfo == null || (pointID != 4 && pointID != 5 && pointID != 6))
						{
							EventPoint.ChangePrevPlayerMode();
							return;
						}
						<OnAwake>c__AnonStorey2.<>f__ref$0.player.PlayerController.ChangeState("Idle");
						EventPoint.OpenEventStart(<OnAwake>c__AnonStorey2.<>f__ref$0.player, <OnAwake>c__AnonStorey2.playInfo.StartEventFadeTime, <OnAwake>c__AnonStorey2.playInfo.EndEventFadeTime, <OnAwake>c__AnonStorey2.playInfo.PodDevice.SEID, <OnAwake>c__AnonStorey2.playInfo.PodDevice.SoundPlayDelayTime, <OnAwake>c__AnonStorey2.playInfo.PodDevice.EndIntervalTime, delegate
						{
							<OnAwake>c__AnonStorey2.<>f__ref$0.$this._eventPoint.SetAgentOpenState(true);
						}, delegate
						{
							if (!<OnAwake>c__AnonStorey2.textTable.IsNullOrEmpty<int, List<string>>())
							{
								List<string> source;
								<OnAwake>c__AnonStorey2.textTable.TryGetValue(9, out source);
								string mes = source.GetElement(EventPoint.LangIdx) ?? string.Empty;
								MapUIContainer.PushMessageUI(mes, 0, 0, null);
							}
							EventPoint.ChangePrevPlayerMode();
						});
						break;
					case 3:
						EventPoint.ChangePrevPlayerMode();
						break;
					case 4:
					case 5:
					case 6:
						EventPoint.ChangePrevPlayerMode();
						break;
					}
				};
				MapUIContainer.OpenRequestUI((Popup.Request.Type)this._eventPoint.EventID);
				if (0 <= this._eventPoint.WarningID && MapUIContainer.RequestUI.IsImpossible)
				{
					MapUIContainer.PushWarningMessage((Popup.Warning.Type)this._eventPoint.WarningID);
				}
				return;
			}
			<OnAwake>c__AnonStorey.player.PlayerController.ChangeState("Normal");
		}

		// Token: 0x06006F52 RID: 28498 RVA: 0x002FC1E8 File Offset: 0x002FA5E8
		private void ChangeGameCleared()
		{
			if (Singleton<Game>.IsInstance())
			{
				Game instance = Singleton<Game>.Instance;
				GlobalSaveData globalData = instance.GlobalData;
				WorldData worldData = instance.WorldData;
				if (globalData != null)
				{
					globalData.Cleared = true;
				}
				if (worldData != null)
				{
					worldData.Cleared = true;
				}
				instance.SaveGlobalData();
			}
		}

		// Token: 0x06006F53 RID: 28499 RVA: 0x002FC234 File Offset: 0x002FA634
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006F54 RID: 28500 RVA: 0x002FC25A File Offset: 0x002FA65A
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F55 RID: 28501 RVA: 0x002FC269 File Offset: 0x002FA669
		protected override void OnRelease(PlayerActor player)
		{
			if (this._eventPoint == null)
			{
				return;
			}
			player.CurrentEventPoint = null;
		}

		// Token: 0x06006F56 RID: 28502 RVA: 0x002FC284 File Offset: 0x002FA684
		~RequestEvent()
		{
		}

		// Token: 0x04005C0B RID: 23563
		private EventPoint _eventPoint;
	}
}
