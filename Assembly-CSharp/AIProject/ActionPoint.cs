using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.Scene;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000BCB RID: 3019
	public class ActionPoint : Point, ICommandable
	{
		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06005C3C RID: 23612 RVA: 0x002713F3 File Offset: 0x0026F7F3
		public static Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> LabelTable { get; }

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06005C3D RID: 23613 RVA: 0x002713FA File Offset: 0x0026F7FA
		public static Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> DateLabelTable { get; }

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06005C3E RID: 23614 RVA: 0x00271401 File Offset: 0x0026F801
		public static Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> SickLabelTable { get; }

		// Token: 0x06005C3F RID: 23615 RVA: 0x00271408 File Offset: 0x0026F808
		private static void PutOnTheBed(PlayerActor player, ActionPoint point)
		{
			player.CameraControl.CrossFade.FadeStart(-1f);
			player.Animation.StopAllAnimCoroutine();
			player.PlayerController.ChangeState("Normal");
			player.CameraControl.Mode = CameraMode.Normal;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			AgentActor agentPartner = player.AgentPartner;
			agentPartner.Animation.StopAllAnimCoroutine();
			agentPartner.IsSlave = false;
			agentPartner.DeactivateNavMeshAgent();
			agentPartner.Partner = null;
			player.PlayerController.CommandArea.RemoveConsiderationObject(point);
			player.PlayerController.CommandArea.RefreshCommands();
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				player.PlayerController.CommandArea.RemoveConsiderationObject(point);
				player.PlayerController.CommandArea.RefreshCommands();
			});
			agentPartner.CurrentPoint = point;
			Desire.ActionType type = Desire.SickPairTable[agentPartner.PrevMode];
			agentPartner.ChangeBehavior(type);
			player.AgentPartner = null;
			agentPartner.IsVisible = true;
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06005C40 RID: 23616 RVA: 0x00271534 File Offset: 0x0026F934
		// (set) Token: 0x06005C41 RID: 23617 RVA: 0x0027153C File Offset: 0x0026F93C
		public override int RegisterID
		{
			get
			{
				return this._registerID;
			}
			set
			{
				this._registerID = value;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06005C42 RID: 23618 RVA: 0x00271545 File Offset: 0x0026F945
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this._id;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x0027154D File Offset: 0x0026F94D
		public int[] IDList
		{
			[CompilerGenerated]
			get
			{
				return this._idList;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06005C44 RID: 23620 RVA: 0x00271555 File Offset: 0x0026F955
		public EventType PlayerEventType
		{
			[CompilerGenerated]
			get
			{
				return this._playerEventType;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x0027155D File Offset: 0x0026F95D
		public EventType AgentEventType
		{
			[CompilerGenerated]
			get
			{
				return this._agentEventType;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06005C46 RID: 23622 RVA: 0x00271565 File Offset: 0x0026F965
		public EventType[] PlayerDateEventType
		{
			[CompilerGenerated]
			get
			{
				return this._playerDateEventType;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06005C47 RID: 23623 RVA: 0x0027156D File Offset: 0x0026F96D
		public EventType AgentDateEventType
		{
			[CompilerGenerated]
			get
			{
				return this._agentDateEventType;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06005C48 RID: 23624 RVA: 0x00271575 File Offset: 0x0026F975
		// (set) Token: 0x06005C49 RID: 23625 RVA: 0x0027157D File Offset: 0x0026F97D
		public List<Transform> NavMeshPoints { get; set; } = new List<Transform>();

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06005C4A RID: 23626 RVA: 0x00271586 File Offset: 0x0026F986
		// (set) Token: 0x06005C4B RID: 23627 RVA: 0x0027158E File Offset: 0x0026F98E
		public HPoint HPoint { get; set; }

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x00271597 File Offset: 0x0026F997
		public Vector3 CommandCenter
		{
			get
			{
				if (this._commandBasePoint != null)
				{
					return this._commandBasePoint.position;
				}
				return base.transform.position;
			}
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x002715C1 File Offset: 0x0026F9C1
		protected void ChangeStateIn(PlayerActor actor, string stateName, bool isDate, System.Action onCompleted = null)
		{
			actor.PlayerController.ChangeState(stateName, this, onCompleted);
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x002715D2 File Offset: 0x0026F9D2
		protected void ChangeStateOut(PlayerActor actor, string stateName)
		{
			actor.PlayerController.ChangeState(stateName);
			actor.CameraControl.Mode = CameraMode.Normal;
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x002715EC File Offset: 0x0026F9EC
		protected void ChangeStateDateIn(PlayerActor actor, string stateName, System.Action onCompleted = null)
		{
			this.ChangeStateIn(actor, stateName, true, onCompleted);
			actor.AgentPartner.AgentController.ChangeState(stateName);
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x00271609 File Offset: 0x0026FA09
		protected void ChangeStateDateOut(PlayerActor actor, string stateName)
		{
			this.ChangeStateOut(actor, stateName);
			actor.AgentPartner.AgentController.ChangeState(stateName);
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x00271624 File Offset: 0x0026FA24
		public bool HasPlayerDateActionPointInfo(byte sex, EventType eventType)
		{
			List<DateActionPointInfo> list;
			if (!this._playerDateInfos.TryGetValue((int)sex, out list))
			{
				return false;
			}
			foreach (DateActionPointInfo dateActionPointInfo in list)
			{
				if (dateActionPointInfo.eventTypeMask == eventType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x002716A0 File Offset: 0x0026FAA0
		public bool HasAgentActionPointInfo(EventType eventType)
		{
			foreach (ActionPointInfo actionPointInfo in this._agentInfos)
			{
				if (actionPointInfo.eventTypeMask == eventType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x0027170C File Offset: 0x0026FB0C
		public bool TryGetAgentActionPointInfo(EventType eventType, out ActionPointInfo outInfo)
		{
			foreach (ActionPointInfo actionPointInfo in this._agentInfos)
			{
				if (actionPointInfo.eventTypeMask == eventType)
				{
					outInfo = actionPointInfo;
					return true;
				}
			}
			outInfo = default(ActionPointInfo);
			return false;
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x00271788 File Offset: 0x0026FB88
		public void GetAgentActionPointInfos(EventType eventType, List<ActionPointInfo> outInfoList)
		{
			foreach (ActionPointInfo item in this._agentInfos)
			{
				if (item.eventTypeMask == eventType)
				{
					outInfoList.Add(item);
				}
			}
		}

		// Token: 0x06005C55 RID: 23637 RVA: 0x002717F4 File Offset: 0x0026FBF4
		public void GetAgentDateActionPointInfos(EventType eventType, List<DateActionPointInfo> outInfoList)
		{
			foreach (DateActionPointInfo item in this._agentDateInfos)
			{
				if (item.eventTypeMask == eventType)
				{
					outInfoList.Add(item);
				}
			}
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x00271860 File Offset: 0x0026FC60
		public ActionPointInfo GetActionPointInfo(AgentActor agent)
		{
			List<ActionPointInfo> list = ListPool<ActionPointInfo>.Get();
			this.GetAgentActionPointInfos(agent.EventKey, list);
			int item = AIProject.Definitions.Action.NameTable[agent.EventKey].Item1;
			List<int> list2 = Singleton<Manager.Resources>.Instance.Animation.PersonalActionListTable[agent.ChaControl.fileParam.personality][item];
			List<ActionPointInfo> list3 = ListPool<ActionPointInfo>.Get();
			foreach (ActionPointInfo item2 in list)
			{
				if (list2.Contains(item2.poseID))
				{
					list3.Add(item2);
				}
			}
			ListPool<ActionPointInfo>.Release(list);
			ActionPointInfo element;
			if (agent.AgentData.StatsTable[1] <= 40f)
			{
				Dictionary<int, Dictionary<int, ActAnimFlagData>> flagTable = Singleton<Manager.Resources>.Instance.Action.AgentActionFlagTable;
				List<ActionPointInfo> list4 = list3.FindAll(delegate(ActionPointInfo x)
				{
					Dictionary<int, ActAnimFlagData> dictionary;
					ActAnimFlagData actAnimFlagData;
					return flagTable.TryGetValue(x.eventID, out dictionary) && dictionary.TryGetValue(x.poseID, out actAnimFlagData) && actAnimFlagData.isBadMood;
				});
				if (!list4.IsNullOrEmpty<ActionPointInfo>())
				{
					element = list4.GetElement(UnityEngine.Random.Range(0, list4.Count));
				}
				else
				{
					element = list3.GetElement(UnityEngine.Random.Range(0, list3.Count));
				}
			}
			else
			{
				element = list3.GetElement(UnityEngine.Random.Range(0, list3.Count));
			}
			ListPool<ActionPointInfo>.Release(list3);
			return element;
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x002719E0 File Offset: 0x0026FDE0
		public DateActionPointInfo GetDateActionPointInfo(AgentActor agent)
		{
			List<DateActionPointInfo> list = ListPool<DateActionPointInfo>.Get();
			this.GetAgentDateActionPointInfos(agent.EventKey, list);
			int item = AIProject.Definitions.Action.NameTable[agent.EventKey].Item1;
			List<int> list2 = Singleton<Manager.Resources>.Instance.Animation.PersonalActionListTable[agent.ChaControl.fileParam.personality][item];
			List<DateActionPointInfo> list3 = ListPool<DateActionPointInfo>.Get();
			foreach (DateActionPointInfo item2 in list)
			{
				if (list2.Contains(item2.poseIDA))
				{
					list3.Add(item2);
				}
			}
			ListPool<DateActionPointInfo>.Release(list);
			DateActionPointInfo element = list3.GetElement(UnityEngine.Random.Range(0, list3.Count));
			ListPool<DateActionPointInfo>.Release(list3);
			return element;
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00271AD0 File Offset: 0x0026FED0
		public bool FindAgentActionPointInfo(EventType eventType, int poseID, out ActionPointInfo outInfo)
		{
			foreach (ActionPointInfo actionPointInfo in this._agentInfos)
			{
				if (actionPointInfo.eventTypeMask == eventType)
				{
					if (actionPointInfo.poseID == poseID)
					{
						outInfo = actionPointInfo;
						return true;
					}
				}
			}
			outInfo = default(ActionPointInfo);
			return false;
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x00271B64 File Offset: 0x0026FF64
		public bool FindPlayerDateActionPointInfo(byte sex, int pointID, out DateActionPointInfo outInfo)
		{
			List<DateActionPointInfo> list;
			if (!this._playerDateInfos.TryGetValue((int)sex, out list))
			{
				outInfo = default(DateActionPointInfo);
				return false;
			}
			foreach (DateActionPointInfo dateActionPointInfo in this._playerDateInfos[(int)sex])
			{
				if (dateActionPointInfo.pointID == pointID)
				{
					outInfo = dateActionPointInfo;
					return true;
				}
			}
			outInfo = default(DateActionPointInfo);
			return false;
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x00271C08 File Offset: 0x00270008
		public bool TryGetPlayerActionPointInfo(EventType eventType, out ActionPointInfo outInfo)
		{
			foreach (ActionPointInfo actionPointInfo in this._playerInfos)
			{
				if (actionPointInfo.eventTypeMask == eventType)
				{
					outInfo = actionPointInfo;
					return true;
				}
			}
			outInfo = default(ActionPointInfo);
			return false;
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x00271C84 File Offset: 0x00270084
		public bool TryGetPlayerDateActionPointInfo(byte sex, EventType eventType, out DateActionPointInfo outInfo)
		{
			List<DateActionPointInfo> list;
			if (!this._playerDateInfos.TryGetValue((int)sex, out list))
			{
				outInfo = default(DateActionPointInfo);
				return false;
			}
			foreach (DateActionPointInfo dateActionPointInfo in list)
			{
				if (dateActionPointInfo.eventTypeMask == eventType)
				{
					outInfo = dateActionPointInfo;
					return true;
				}
			}
			outInfo = default(DateActionPointInfo);
			return false;
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x00271D18 File Offset: 0x00270118
		public bool TryGetAgentDateActionPointInfo(EventType eventType, out DateActionPointInfo outInfo)
		{
			foreach (DateActionPointInfo dateActionPointInfo in this._agentDateInfos)
			{
				if (dateActionPointInfo.eventTypeMask == eventType)
				{
					outInfo = dateActionPointInfo;
					return true;
				}
			}
			outInfo = default(DateActionPointInfo);
			return false;
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x00271D94 File Offset: 0x00270194
		public bool ContainsAgentDateActionPointInfo(EventType eventType)
		{
			foreach (DateActionPointInfo dateActionPointInfo in this._agentDateInfos)
			{
				if (dateActionPointInfo.eventTypeMask == eventType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06005C5E RID: 23646 RVA: 0x00271E00 File Offset: 0x00270200
		// (set) Token: 0x06005C5F RID: 23647 RVA: 0x00271E08 File Offset: 0x00270208
		public Actor Actor { get; set; }

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06005C60 RID: 23648 RVA: 0x00271E11 File Offset: 0x00270211
		public bool EnabledRangeCheck
		{
			[CompilerGenerated]
			get
			{
				return this._enabledRangeCheck;
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06005C61 RID: 23649 RVA: 0x00271E19 File Offset: 0x00270219
		public float Radius
		{
			[CompilerGenerated]
			get
			{
				return this._radius;
			}
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06005C62 RID: 23650 RVA: 0x00271E21 File Offset: 0x00270221
		// (set) Token: 0x06005C63 RID: 23651 RVA: 0x00271E29 File Offset: 0x00270229
		public GameObject[] MapItemObjs
		{
			get
			{
				return this._mapItemObjs;
			}
			set
			{
				this._mapItemObjs = value;
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06005C64 RID: 23652 RVA: 0x00271E32 File Offset: 0x00270232
		// (set) Token: 0x06005C65 RID: 23653 RVA: 0x00271E3A File Offset: 0x0027023A
		public MapItemKeyValuePair[] MapItemData
		{
			get
			{
				return this._mapItemData;
			}
			set
			{
				this._mapItemData = value;
			}
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06005C66 RID: 23654 RVA: 0x00271E43 File Offset: 0x00270243
		public Vector3 LocatedPosition
		{
			get
			{
				if (this._destination != null)
				{
					return this._destination.position;
				}
				return base.transform.position;
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06005C67 RID: 23655 RVA: 0x00271E6D File Offset: 0x0027026D
		public ActionPoint.ActionSlot Slot
		{
			[CompilerGenerated]
			get
			{
				return this._actionSlot;
			}
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x00271E75 File Offset: 0x00270275
		// (set) Token: 0x06005C69 RID: 23657 RVA: 0x00271E7D File Offset: 0x0027027D
		public List<ActionPoint> ConnectedActionPoints { get; set; } = new List<ActionPoint>();

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06005C6A RID: 23658 RVA: 0x00271E86 File Offset: 0x00270286
		// (set) Token: 0x06005C6B RID: 23659 RVA: 0x00271E8E File Offset: 0x0027028E
		public List<ActionPoint> GroupActionPoints { get; private set; } = new List<ActionPoint>();

		// Token: 0x06005C6C RID: 23660 RVA: 0x00271E98 File Offset: 0x00270298
		public virtual bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = Vector3.Distance(basePosition, commandCenter);
			List<ActionPoint> connectedActionPoints = this.ConnectedActionPoints;
			if (connectedActionPoints != null)
			{
				foreach (ActionPoint actionPoint in connectedActionPoints)
				{
					if (!(actionPoint == null))
					{
						if (!actionPoint.IsNeutralCommand)
						{
							return false;
						}
					}
				}
			}
			if (this.CommandType == CommandType.Forward)
			{
				float num2 = (!this._enabledRangeCheck) ? radiusA : (radiusA + this._radius);
				if (num > num2)
				{
					return false;
				}
				Vector3 a = commandCenter;
				a.y = 0f;
				float num3 = angle / 2f;
				float num4 = Vector3.Angle(a - basePosition, forward);
				if (num4 > num3)
				{
					return false;
				}
			}
			else
			{
				float num5 = (!this._enabledRangeCheck) ? radiusB : (radiusB + this._radius);
				if (distance > num5)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x00271FD8 File Offset: 0x002703D8
		public bool IsReachable(NavMeshAgent navMeshAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool result = true;
			if (navMeshAgent.isActiveAndEnabled)
			{
				bool flag = false;
				foreach (Transform transform in this.NavMeshPoints)
				{
					navMeshAgent.CalculatePath(transform.position, this._pathForCalc);
					if (this._pathForCalc.status == NavMeshPathStatus.PathComplete)
					{
						float num = 0f;
						Vector3[] corners = this._pathForCalc.corners;
						float num2 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
						num2 += this._radius;
						for (int i = 0; i < corners.Length - 1; i++)
						{
							float num3 = Vector3.Distance(corners[i], corners[i + 1]);
							num += num3;
						}
						if (num < num2)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00272114 File Offset: 0x00270514
		public bool IsReachable(NavMeshAgent navMeshAgent, float radius)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool result = true;
			if (navMeshAgent.isActiveAndEnabled)
			{
				bool flag = false;
				foreach (Transform transform in this.NavMeshPoints)
				{
					navMeshAgent.CalculatePath(transform.position, this._pathForCalc);
					if (this._pathForCalc.status == NavMeshPathStatus.PathComplete)
					{
						float num = 0f;
						Vector3[] corners = this._pathForCalc.corners;
						float num2 = radius + this._radius;
						for (int i = 0; i < corners.Length - 1; i++)
						{
							float num3 = Vector3.Distance(corners[i], corners[i + 1]);
							num += num3;
						}
						if (num < num2)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06005C6F RID: 23663 RVA: 0x0027223C File Offset: 0x0027063C
		// (set) Token: 0x06005C70 RID: 23664 RVA: 0x00272244 File Offset: 0x00270644
		public bool IsImpossible { get; protected set; }

		// Token: 0x06005C71 RID: 23665 RVA: 0x0027224D File Offset: 0x0027064D
		public void SetForceImpossible(bool value)
		{
			this.IsImpossible = value;
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x00272258 File Offset: 0x00270658
		public virtual bool SetImpossible(bool value, Actor actor)
		{
			if (this.IsImpossible != value)
			{
				this.IsImpossible = value;
				if (value)
				{
					this.SetSlot(actor);
				}
				else if (this._actionSlot.Actor != null && this._actionSlot.Actor == actor)
				{
					this.ReleaseSlot(actor);
				}
				return true;
			}
			if (value)
			{
				if (this._actionSlot.Actor == null)
				{
					this.IsImpossible = value;
					this.SetSlot(actor);
					return true;
				}
				return false;
			}
			else
			{
				if (this._actionSlot.Actor != null && this._actionSlot.Actor == actor)
				{
					this.IsImpossible = value;
					this.ReleaseSlot(actor);
					return true;
				}
				this.IsImpossible = false;
				return true;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06005C73 RID: 23667 RVA: 0x00272338 File Offset: 0x00270738
		public virtual bool IsNeutralCommand
		{
			get
			{
				return !this.TutorialHideMode() && !(this._actionSlot.Actor != null) && this._bookingList.IsNullOrEmpty<UnityEx.ValueTuple<int, int>>();
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06005C74 RID: 23668 RVA: 0x00272372 File Offset: 0x00270772
		// (set) Token: 0x06005C75 RID: 23669 RVA: 0x0027237A File Offset: 0x0027077A
		public Transform TargetCommun { get; set; }

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06005C76 RID: 23670 RVA: 0x00272383 File Offset: 0x00270783
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return this.LocatedPosition;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06005C77 RID: 23671 RVA: 0x0027238B File Offset: 0x0027078B
		public virtual CommandLabel.CommandInfo[] Labels
		{
			get
			{
				if (Singleton<Manager.Map>.Instance.Player.PlayerController.State is Onbu)
				{
					return this._sickLabels;
				}
				return this._labels;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x002723B8 File Offset: 0x002707B8
		public virtual CommandLabel.CommandInfo[] DateLabels
		{
			get
			{
				if (!Singleton<Manager.Map>.IsInstance() || Singleton<Manager.Map>.Instance.Player == null)
				{
					return null;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				if (this.HasPlayerDateActionPointInfo(player.ChaControl.sex, EventType.Lesbian))
				{
					if (player.ChaControl.sex == 1 && !Singleton<Manager.Map>.Instance.Player.ChaControl.fileParam.futanari)
					{
						return null;
					}
				}
				else if (this.HasPlayerDateActionPointInfo(player.ChaControl.sex, EventType.Eat) && player.PlayerData.DateEatTrigger)
				{
					return null;
				}
				return this._dateLabels[(int)player.ChaControl.sex];
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06005C79 RID: 23673 RVA: 0x0027247D File Offset: 0x0027087D
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return this._layer;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06005C7A RID: 23674 RVA: 0x00272488 File Offset: 0x00270888
		public CommandType CommandType
		{
			get
			{
				if (this._playerEventType.Contains(EventType.Move) || this._playerEventType.Contains(EventType.DoorOpen) || this._playerEventType.Contains(EventType.DoorClose))
				{
					return CommandType.AllAround;
				}
				return CommandType.Forward;
			}
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x002724D8 File Offset: 0x002708D8
		public virtual bool TutorialHideMode()
		{
			if (!Manager.Map.TutorialMode)
			{
				return false;
			}
			if (this._playerEventType.Contains(EventType.Move))
			{
				return false;
			}
			CommonDefine commonDefine = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.CommonDefine;
			if (commonDefine != null)
			{
				int[] kitchenPointIDList = commonDefine.Tutorial.KitchenPointIDList;
				if (!kitchenPointIDList.IsNullOrEmpty<int>() && kitchenPointIDList.Contains(this._id))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x0027255C File Offset: 0x0027095C
		private void Awake()
		{
			this._obstacles = base.GetComponentsInChildren<NavMeshObstacle>(true);
			foreach (NavMeshObstacle navMeshObstacle in this._obstacles)
			{
			}
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00272598 File Offset: 0x00270998
		public void Init()
		{
			DefinePack.MapGroup mapDefines = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines;
			this.NavMeshPoints.Add(base.transform);
			List<GameObject> list = ListPool<GameObject>.Get();
			base.transform.FindLoopPrefix(list, mapDefines.NavMeshTargetName);
			if (!list.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in list)
				{
					this.NavMeshPoints.Add(gameObject.transform);
				}
			}
			ListPool<GameObject>.Release(list);
			if (this._destination == null)
			{
				this._destination = base.transform;
			}
			if (this._commandBasePoint == null)
			{
				GameObject gameObject2 = base.transform.FindLoop(mapDefines.CommandTargetName);
				this._commandBasePoint = (((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform);
			}
			this._agentInfos = new List<ActionPointInfo>();
			this._playerInfos = new List<ActionPointInfo>();
			Dictionary<int, List<DateActionPointInfo>> dictionary = new Dictionary<int, List<DateActionPointInfo>>();
			dictionary[0] = new List<DateActionPointInfo>();
			dictionary[1] = new List<DateActionPointInfo>();
			this._playerDateInfos = dictionary;
			this._agentDateInfos = new List<DateActionPointInfo>();
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			List<ActionPointInfo> list2;
			if (Singleton<Manager.Resources>.Instance.Map.AgentActionPointInfoTable[0].TryGetValue(this._id, out list2))
			{
				foreach (ActionPointInfo item in list2)
				{
					this._agentInfos.Add(item);
				}
			}
			List<ActionPointInfo> list3;
			if (Singleton<Manager.Resources>.Instance.Map.PlayerActionPointInfoTable[0].TryGetValue(this._id, out list3))
			{
				foreach (ActionPointInfo item2 in list3)
				{
					this._playerInfos.Add(item2);
				}
			}
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			Dictionary<int, Dictionary<int, List<DateActionPointInfo>>> dictionary2;
			if (Singleton<Manager.Resources>.Instance.Map.PlayerDateActionPointInfoTable.TryGetValue(0, out dictionary2))
			{
				foreach (KeyValuePair<int, Dictionary<int, List<DateActionPointInfo>>> keyValuePair in dictionary2)
				{
					int key = keyValuePair.Key;
					List<DateActionPointInfo> list4;
					if (keyValuePair.Value.TryGetValue(this._id, out list4))
					{
						List<DateActionPointInfo> list5;
						if (!this._playerDateInfos.TryGetValue(key, out list5))
						{
							List<DateActionPointInfo> list6 = new List<DateActionPointInfo>();
							this._playerDateInfos[key] = list6;
							list5 = list6;
						}
						foreach (DateActionPointInfo item3 in list4)
						{
							list5.Add(item3);
						}
					}
				}
			}
			List<DateActionPointInfo> list7;
			if (Singleton<Manager.Resources>.Instance.Map.AgentDateActionPointInfoTable[0].TryGetValue(this._id, out list7))
			{
				foreach (DateActionPointInfo item4 in list7)
				{
					this._agentDateInfos.Add(item4);
				}
			}
			if (!this._idList.IsNullOrEmpty<int>())
			{
				this._agentInfos.Clear();
				this._playerInfos.Clear();
				foreach (KeyValuePair<int, List<DateActionPointInfo>> keyValuePair2 in this._playerDateInfos)
				{
					keyValuePair2.Value.Clear();
				}
				this._agentDateInfos.Clear();
				foreach (int key2 in this._idList)
				{
					if (Singleton<Manager.Resources>.Instance.Map.PlayerActionPointInfoTable[0].TryGetValue(key2, out list2))
					{
						foreach (ActionPointInfo item5 in list2)
						{
							this._playerInfos.Add(item5);
						}
					}
					if (Singleton<Manager.Resources>.Instance.Map.AgentActionPointInfoTable[0].TryGetValue(key2, out list3))
					{
						foreach (ActionPointInfo item6 in list3)
						{
							this._agentInfos.Add(item6);
						}
					}
					if (Singleton<Manager.Resources>.Instance.Map.PlayerDateActionPointInfoTable.TryGetValue(0, out dictionary2))
					{
						foreach (KeyValuePair<int, Dictionary<int, List<DateActionPointInfo>>> keyValuePair3 in dictionary2)
						{
							int key3 = keyValuePair3.Key;
							List<DateActionPointInfo> list8;
							if (keyValuePair3.Value.TryGetValue(key2, out list8))
							{
								List<DateActionPointInfo> list9;
								if (!this._playerDateInfos.TryGetValue(key3, out list9))
								{
									List<DateActionPointInfo> list6 = new List<DateActionPointInfo>();
									this._playerDateInfos[key3] = list6;
									list9 = list6;
								}
								foreach (DateActionPointInfo item7 in list8)
								{
									list9.Add(item7);
								}
							}
						}
					}
					if (Singleton<Manager.Resources>.Instance.Map.AgentDateActionPointInfoTable[0].TryGetValue(key2, out list7))
					{
						foreach (DateActionPointInfo item8 in list7)
						{
							this._agentDateInfos.Add(item8);
						}
					}
				}
			}
			base.Start();
			EventType eventType = (EventType)0;
			foreach (ActionPointInfo actionPointInfo in this._playerInfos)
			{
				eventType |= actionPointInfo.eventTypeMask;
			}
			this._playerEventType = eventType;
			eventType = (EventType)0;
			foreach (ActionPointInfo actionPointInfo2 in this._agentInfos)
			{
				eventType |= actionPointInfo2.eventTypeMask;
			}
			this._agentEventType = eventType;
			this._playerDateEventType = new EventType[2];
			for (int j = 0; j < 2; j++)
			{
				eventType = (EventType)0;
				foreach (DateActionPointInfo dateActionPointInfo in this._playerDateInfos[j])
				{
					eventType |= dateActionPointInfo.eventTypeMask;
				}
				this._playerDateEventType[j] = eventType;
			}
			eventType = (EventType)0;
			foreach (DateActionPointInfo dateActionPointInfo2 in this._agentDateInfos)
			{
				eventType |= dateActionPointInfo2.eventTypeMask;
			}
			this._agentDateEventType = eventType;
			this.InitSub();
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x00272E04 File Offset: 0x00271204
		protected virtual void InitSub()
		{
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			List<CommandLabel.CommandInfo> list = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator = ActionPoint.LabelTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator.Current;
					ActionPoint $this = this;
					if (this._playerEventType.Contains(pair.Key))
					{
						UnityEx.ValueTuple<int, string> valueTuple;
						if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple))
						{
							ActionPointInfo actionPointInfo = this._playerInfos.Find((ActionPointInfo x) => x.eventTypeMask == pair.Key);
							string actionName = actionPointInfo.actionName;
							Sprite icon2;
							Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo.iconID, out icon2);
							GameObject gameObject = base.transform.FindLoop(actionPointInfo.labelNullName);
							Transform transform = ((gameObject != null) ? gameObject.transform : null) ?? base.transform;
							CommandLabel.CommandInfo commandInfo = new CommandLabel.CommandInfo
							{
								Text = actionName,
								Icon = icon2,
								IsHold = (pair.Key == EventType.Sleep || pair.Key == EventType.Break || pair.Key == EventType.Search || pair.Key == EventType.StorageIn || pair.Key == EventType.Cook || pair.Key == EventType.DressIn || pair.Key == EventType.ClothChange),
								TargetSpriteInfo = icon.ActionSpriteInfo,
								Transform = transform,
								Condition = null,
								Event = delegate
								{
									pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
								}
							};
							if (pair.Key == EventType.Sleep)
							{
								commandInfo.Condition = ((PlayerActor player) => Singleton<Manager.Map>.Instance.CanSleepInTime());
								commandInfo.ErrorText = delegate(PlayerActor player)
								{
									if (Singleton<Manager.Map>.Instance.CanSleepInTime())
									{
										return string.Empty;
									}
									return "夜になるまで寝ることはできません";
								};
							}
							list.Add(commandInfo);
						}
					}
				}
			}
			this._labels = list.ToArray();
			ListPool<CommandLabel.CommandInfo>.Release(list);
			for (int i = 0; i < 2; i++)
			{
				List<CommandLabel.CommandInfo> list2 = ListPool<CommandLabel.CommandInfo>.Get();
				using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator2 = ActionPoint.DateLabelTable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator2.Current;
						ActionPoint $this = this;
						if (this._playerDateEventType[i].Contains(pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple2;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple2))
							{
								List<DateActionPointInfo> list3;
								if (this._playerDateInfos.TryGetValue(i, out list3))
								{
									DateActionPointInfo dateActionPointInfo = list3.Find((DateActionPointInfo x) => x.eventTypeMask == pair.Key);
									string actionName2 = dateActionPointInfo.actionName;
									Sprite icon3;
									Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(dateActionPointInfo.iconID, out icon3);
									GameObject gameObject2 = base.transform.FindLoop(dateActionPointInfo.labelNullName);
									Transform transform2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform;
									list2.Add(new CommandLabel.CommandInfo
									{
										Text = actionName2,
										Icon = icon3,
										IsHold = (pair.Key == EventType.Sleep || pair.Key == EventType.Break || pair.Key == EventType.Eat || pair.Key == EventType.Search || pair.Key == EventType.StorageIn || pair.Key == EventType.Cook || pair.Key == EventType.DressIn || pair.Key == EventType.Lesbian || pair.Key == EventType.ClothChange),
										TargetSpriteInfo = icon.ActionSpriteInfo,
										Transform = transform2,
										Condition = null,
										Event = delegate
										{
											pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
										}
									});
								}
							}
						}
					}
				}
				this._dateLabels[i] = list2.ToArray();
				ListPool<CommandLabel.CommandInfo>.Release(list2);
			}
			List<CommandLabel.CommandInfo> list4 = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator3 = ActionPoint.SickLabelTable.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator3.Current;
					ActionPoint $this = this;
					if (this._playerEventType.Contains(pair.Key))
					{
						UnityEx.ValueTuple<int, string> valueTuple3;
						if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple3))
						{
							ActionPointInfo actionPointInfo2 = this._playerInfos.Find((ActionPointInfo x) => x.eventTypeMask == pair.Key);
							Sprite icon4;
							Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo2.iconID, out icon4);
							GameObject gameObject3 = base.transform.FindLoop(actionPointInfo2.labelNullName);
							Transform transform3 = ((gameObject3 != null) ? gameObject3.transform : null) ?? base.transform;
							list4.Add(new CommandLabel.CommandInfo
							{
								Text = pair.Value.Item2,
								Icon = icon4,
								IsHold = true,
								TargetSpriteInfo = icon.ActionSpriteInfo,
								Transform = transform3,
								Condition = null,
								Event = delegate
								{
									pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
								}
							});
						}
					}
				}
			}
			this._sickLabels = list4.ToArray();
			ListPool<CommandLabel.CommandInfo>.Release(list4);
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x0027349C File Offset: 0x0027189C
		public void SetActiveMapItemObjs(bool active)
		{
			if (!this._mapItemObjs.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._mapItemObjs)
				{
					if (gameObject.activeSelf != active)
					{
						gameObject.SetActive(active);
					}
				}
			}
			if (!this._mapItemData.IsNullOrEmpty<MapItemKeyValuePair>())
			{
				foreach (MapItemKeyValuePair mapItemKeyValuePair in this._mapItemData)
				{
					if (mapItemKeyValuePair != null && !(mapItemKeyValuePair.ItemObj == null))
					{
						mapItemKeyValuePair.ItemObj.SetActiveIfDifferent(active);
					}
				}
			}
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x00273550 File Offset: 0x00271950
		public GameObject CreateEventItems(int itemID, string parentName, bool isSync)
		{
			if (this._mapItemData.IsNullOrEmpty<MapItemKeyValuePair>())
			{
				return null;
			}
			MapItemKeyValuePair mapItemKeyValuePair = this._mapItemData.FirstOrDefault((MapItemKeyValuePair x) => x.ID == itemID);
			if (mapItemKeyValuePair == null)
			{
				return null;
			}
			return mapItemKeyValuePair.ItemObj;
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x002735A2 File Offset: 0x002719A2
		public override void LocateGround()
		{
			base.LocateGround();
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06005C82 RID: 23682 RVA: 0x002735AA File Offset: 0x002719AA
		// (set) Token: 0x06005C83 RID: 23683 RVA: 0x002735B2 File Offset: 0x002719B2
		public Actor Reserver { get; set; }

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x002735BB File Offset: 0x002719BB
		// (set) Token: 0x06005C85 RID: 23685 RVA: 0x002735C3 File Offset: 0x002719C3
		public Actor BookingUser { get; private set; }

		// Token: 0x06005C86 RID: 23686 RVA: 0x002735CC File Offset: 0x002719CC
		protected override void OnDisable()
		{
			PlayerActor playerActor = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
			if (playerActor != null)
			{
				CommandArea commandArea = playerActor.PlayerController.CommandArea;
				if (this.SetImpossible(false, playerActor) && commandArea.ContainsConsiderationObject(this))
				{
					commandArea.RemoveConsiderationObject(this);
					commandArea.RefreshCommands();
				}
			}
			base.OnDisable();
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x00273638 File Offset: 0x00271A38
		public int GetPriority(int charaID)
		{
			return (0 > charaID) ? (charaID * -1 * 1000) : charaID;
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x00273650 File Offset: 0x00271A50
		public bool AddBooking(Actor actor)
		{
			int charaID = actor.ID;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			list.Add(this);
			list.AddRange(this.ConnectedActionPoints);
			bool result = false;
			foreach (ActionPoint actionPoint in list)
			{
				if (!actionPoint._bookingList.Exists((UnityEx.ValueTuple<int, int> x) => x.Item2 == charaID))
				{
					actionPoint._bookingList.Add(new UnityEx.ValueTuple<int, int>(this.GetPriority(charaID), charaID));
					actionPoint._bookingList.Sort((UnityEx.ValueTuple<int, int> x, UnityEx.ValueTuple<int, int> y) => x.Item1 - y.Item1);
					result = true;
				}
			}
			ListPool<ActionPoint>.Release(list);
			return result;
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x00273744 File Offset: 0x00271B44
		public bool RemoveBooking(Actor actor)
		{
			int charaID = actor.ID;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			list.Add(this);
			list.AddRange(this.ConnectedActionPoints);
			bool flag = false;
			foreach (ActionPoint actionPoint in list)
			{
				int num = actionPoint._bookingList.RemoveAll((UnityEx.ValueTuple<int, int> x) => x.Item2 == charaID);
				flag |= (1 <= num);
				if (0 < num)
				{
					actionPoint._bookingList.Sort((UnityEx.ValueTuple<int, int> x, UnityEx.ValueTuple<int, int> y) => x.Item1 - y.Item1);
				}
			}
			if (this.BookingUser == actor)
			{
				this.RemoveBookingUser(actor);
			}
			ListPool<ActionPoint>.Release(list);
			return flag;
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x00273838 File Offset: 0x00271C38
		public bool OffMeshAvailablePoint(Actor actor)
		{
			if (this.BookingUser != null)
			{
				return false;
			}
			if (this._bookingList.IsNullOrEmpty<UnityEx.ValueTuple<int, int>>())
			{
				this.AddBooking(actor);
				return true;
			}
			return this._bookingList[0].Item2 == actor.ID;
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x00273890 File Offset: 0x00271C90
		public bool ForceUse(Actor user)
		{
			if (!(this.BookingUser == null) && !(this.BookingUser == user))
			{
				return false;
			}
			this.SetBookingUser(user);
			if (Singleton<Manager.Map>.IsInstance())
			{
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				CommandArea commandArea = player.PlayerController.CommandArea;
				List<ActionPoint> list = ListPool<ActionPoint>.Get();
				list.Add(this);
				list.AddRange(this.ConnectedActionPoints);
				bool flag = false;
				foreach (ActionPoint actionPoint in list)
				{
					if (!(actionPoint == null))
					{
						if (actionPoint.SetImpossible(false, player) && commandArea.ContainsConsiderationObject(actionPoint))
						{
							commandArea.RemoveConsiderationObject(actionPoint);
							flag = true;
						}
					}
				}
				if (flag)
				{
					commandArea.RefreshCommands();
				}
				ListPool<ActionPoint>.Release(list);
			}
			return true;
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x0027399C File Offset: 0x00271D9C
		public bool SetBookingUser(Actor user)
		{
			if (user == null)
			{
				return false;
			}
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			list.Add(this);
			list.AddRange(this.ConnectedActionPoints);
			bool flag = true;
			foreach (ActionPoint actionPoint in list)
			{
				if (!(actionPoint == null))
				{
					flag &= (actionPoint.BookingUser == null || actionPoint.BookingUser == user);
					actionPoint.BookingUser = user;
				}
			}
			ListPool<ActionPoint>.Release(list);
			return flag;
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x00273A58 File Offset: 0x00271E58
		public bool RemoveBookingUser(Actor user)
		{
			if (user == null)
			{
				return false;
			}
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			list.Add(this);
			list.AddRange(this.ConnectedActionPoints);
			bool flag = true;
			foreach (ActionPoint actionPoint in list)
			{
				if (!(actionPoint == null))
				{
					flag &= (actionPoint.BookingUser == null || actionPoint.BookingUser == user);
					actionPoint.BookingUser = null;
				}
			}
			ListPool<ActionPoint>.Release(list);
			return true;
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x00273B14 File Offset: 0x00271F14
		private void TransitionAction()
		{
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x00273B16 File Offset: 0x00271F16
		public Tuple<Transform, Transform> GetSlot(Actor actor)
		{
			return new Tuple<Transform, Transform>(this._actionSlot.Point, this._actionSlot.RecoveryPoint);
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x00273B33 File Offset: 0x00271F33
		public Tuple<Transform, Transform> SetSlot(Actor actor)
		{
			if (this._actionSlot.Actor == actor)
			{
				actor.CurrentPoint = this;
				return this.GetSlot(actor);
			}
			this._actionSlot.Actor = actor;
			return this.GetSlot(actor);
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x00273B6D File Offset: 0x00271F6D
		public void ReleaseSlot(Actor actor)
		{
			if (actor == this._actionSlot.Actor)
			{
				this._actionSlot.Actor = null;
				this.Reserver = null;
				actor.CurrentPoint = null;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06005C92 RID: 23698 RVA: 0x00273B9F File Offset: 0x00271F9F
		public int InstanceID
		{
			get
			{
				if (this._hashCode != null)
				{
					return this._hashCode.Value;
				}
				this._hashCode = new int?(base.GetInstanceID());
				return this._hashCode.Value;
			}
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x00273BDC File Offset: 0x00271FDC
		public void CreateByproduct(int actionID, int poseID)
		{
			MapItemNull component = base.GetComponent<MapItemNull>();
			if (component == null)
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<int, ByproductInfo> dictionary;
			if (!Singleton<Manager.Resources>.Instance.Action.ByproductList.TryGetValue(actionID, out dictionary))
			{
				return;
			}
			ByproductInfo byproductInfo;
			if (!dictionary.TryGetValue(poseID, out byproductInfo))
			{
				return;
			}
			List<int> element = byproductInfo.ItemList.GetElement(UnityEngine.Random.Range(0, byproductInfo.ItemList.Count));
			component.SetActiveObjs(true, element);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x00273C5C File Offset: 0x0027205C
		public void DestroyByproduct(int actionID, int poseID)
		{
			MapItemNull component = base.GetComponent<MapItemNull>();
			if (component == null)
			{
				return;
			}
			component.SetActiveObjs(false, null);
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x00273C88 File Offset: 0x00272088
		public bool IsReserved(AgentActor agent)
		{
			if (this.Reserver != null && this.Reserver != agent)
			{
				if (!(this.Reserver is AgentActor))
				{
					return true;
				}
				AgentActor agentActor = this.Reserver as AgentActor;
				if (agentActor.TargetInSightActionPoint == this)
				{
					return true;
				}
				this.Reserver = null;
			}
			return false;
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00273CF8 File Offset: 0x002720F8
		// Note: this type is marked as 'beforefieldinit'.
		static ActionPoint()
		{
			Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> dictionary = new Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>();
			dictionary[EventType.Sleep] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(0, "寝る", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Sleep", false, null);
			});
			dictionary[EventType.Break] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(1, "休憩する", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Break", false, null);
			});
			dictionary[EventType.Search] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(6, "調べる", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Search", false, null);
			});
			dictionary[EventType.StorageIn] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(7, "ストレージ", delegate(PlayerActor x, ActionPoint y)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.BoxOpen);
				y.ChangeStateIn(x, "ItemBox", false, null);
			});
			dictionary[EventType.Cook] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(9, "料理する", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Kitchen", false, null);
			});
			dictionary[EventType.DressIn] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(10, "脱衣所", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DressRoom", false, null);
			});
			dictionary[EventType.Move] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(14, "移動する", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Move", false, null);
			});
			dictionary[EventType.DoorOpen] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(19, "ドアを開ける", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DoorOpen", false, null);
			});
			dictionary[EventType.DoorClose] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(20, "ドアを閉める", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DoorClose", false, null);
			});
			dictionary[EventType.ClothChange] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(22, "クローゼット", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "ClothChange", false, null);
			});
			dictionary[EventType.Warp] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(23, "ワープ装置", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Warp", false, null);
			});
			ActionPoint.LabelTable = dictionary;
			dictionary = new Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>();
			dictionary[EventType.Sleep] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(0, "寝る", delegate(PlayerActor x, ActionPoint y)
			{
				ConfirmScene.Sentence = "一緒に寝た場合2人で行動状態が解除されます。";
				ConfirmScene.OnClickedYes = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
					x.AgentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
					y.ChangeStateIn(x, "DateSleep", false, null);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
				};
				Singleton<Game>.Instance.LoadDialog();
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			});
			dictionary[EventType.Break] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(1, "一緒に休憩する", delegate(PlayerActor x, ActionPoint y)
			{
				x.AgentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				y.ChangeStateIn(x, "DateBreak", false, null);
			});
			dictionary[EventType.Eat] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(3, "一緒に御飯を食べる", delegate(PlayerActor x, ActionPoint y)
			{
				x.AgentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				y.ChangeStateIn(x, "DateEat", false, null);
			});
			dictionary[EventType.Search] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(6, "調べる", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DateSearch", false, null);
			});
			dictionary[EventType.StorageIn] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(7, "ストレージ", delegate(PlayerActor x, ActionPoint y)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.BoxOpen);
				y.ChangeStateIn(x, "ItemBox", false, null);
			});
			dictionary[EventType.Lesbian] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(13, "Hポイント", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "SpecialH", false, null);
			});
			dictionary[EventType.Move] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(14, "移動する", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Move", false, null);
			});
			dictionary[EventType.DoorOpen] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(19, "ドアを開ける", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DoorOpen", false, null);
			});
			dictionary[EventType.DoorClose] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(20, "ドアを閉める", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DoorClose", false, null);
			});
			dictionary[EventType.Warp] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(23, "ワープ装置", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Warp", false, null);
			});
			ActionPoint.DateLabelTable = dictionary;
			dictionary = new Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>();
			dictionary[EventType.DoorOpen] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(19, "ドアを開ける", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DoorOpen", false, null);
			});
			dictionary[EventType.DoorClose] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(20, "ドアを閉める", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "DoorClose", false, null);
			});
			dictionary[EventType.Warp] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(23, "ワープ装置", delegate(PlayerActor x, ActionPoint y)
			{
				y.ChangeStateIn(x, "Warp", false, null);
			});
			dictionary[EventType.Sleep] = new Tuple<int, string, Action<PlayerActor, ActionPoint>>(0, "ベッドに寝かせる", delegate(PlayerActor x, ActionPoint y)
			{
				ActionPoint.PutOnTheBed(x, y);
			});
			ActionPoint.SickLabelTable = dictionary;
		}

		// Token: 0x04005304 RID: 21252
		[SerializeField]
		private int _registerID;

		// Token: 0x04005305 RID: 21253
		[SerializeField]
		protected int _id;

		// Token: 0x04005306 RID: 21254
		[SerializeField]
		protected int[] _idList;

		// Token: 0x04005307 RID: 21255
		[SerializeField]
		[HideInEditorMode]
		protected EventType _playerEventType;

		// Token: 0x04005308 RID: 21256
		[SerializeField]
		[HideInEditorMode]
		protected EventType _agentEventType;

		// Token: 0x04005309 RID: 21257
		[SerializeField]
		protected EventType[] _playerDateEventType = new EventType[2];

		// Token: 0x0400530A RID: 21258
		[SerializeField]
		protected EventType _agentDateEventType;

		// Token: 0x0400530D RID: 21261
		protected List<ActionPointInfo> _agentInfos;

		// Token: 0x0400530E RID: 21262
		protected List<ActionPointInfo> _playerInfos;

		// Token: 0x0400530F RID: 21263
		protected Dictionary<int, List<DateActionPointInfo>> _playerDateInfos;

		// Token: 0x04005310 RID: 21264
		protected List<DateActionPointInfo> _agentDateInfos;

		// Token: 0x04005312 RID: 21266
		[SerializeField]
		private bool _enabledRangeCheck = true;

		// Token: 0x04005313 RID: 21267
		[SerializeField]
		[ShowIf("_enabledRangeCheck", true)]
		private float _radius = 1f;

		// Token: 0x04005314 RID: 21268
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x04005315 RID: 21269
		[SerializeField]
		protected Transform _destination;

		// Token: 0x04005316 RID: 21270
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private GameObject[] _mapItemObjs;

		// Token: 0x04005317 RID: 21271
		[SerializeField]
		private MapItemKeyValuePair[] _mapItemData;

		// Token: 0x04005318 RID: 21272
		private NavMeshObstacle[] _obstacles;

		// Token: 0x04005319 RID: 21273
		[SerializeField]
		protected ActionPoint.ActionSlot _actionSlot = new ActionPoint.ActionSlot();

		// Token: 0x0400531C RID: 21276
		private NavMeshPath _pathForCalc;

		// Token: 0x0400531F RID: 21279
		protected CommandLabel.CommandInfo[] _labels;

		// Token: 0x04005320 RID: 21280
		protected CommandLabel.CommandInfo[] _sickLabels;

		// Token: 0x04005321 RID: 21281
		protected CommandLabel.CommandInfo[][] _dateLabels = new CommandLabel.CommandInfo[2][];

		// Token: 0x04005322 RID: 21282
		[SerializeField]
		private ObjectLayer _layer = ObjectLayer.Command;

		// Token: 0x04005324 RID: 21284
		protected List<UnityEx.ValueTuple<int, int>> _bookingList = new List<UnityEx.ValueTuple<int, int>>();

		// Token: 0x04005326 RID: 21286
		private int? _hashCode;

		// Token: 0x02000BCC RID: 3020
		[Serializable]
		public struct PoseTypePair
		{
			// Token: 0x06005CB5 RID: 23733 RVA: 0x00274302 File Offset: 0x00272702
			public PoseTypePair(EventType eventType_, PoseType poseType_)
			{
				this.eventType = eventType_;
				this.poseType = poseType_;
			}

			// Token: 0x0400532C RID: 21292
			public EventType eventType;

			// Token: 0x0400532D RID: 21293
			public PoseType poseType;
		}

		// Token: 0x02000BCD RID: 3021
		[Serializable]
		public class PointPair
		{
			// Token: 0x170011A5 RID: 4517
			// (get) Token: 0x06005CB7 RID: 23735 RVA: 0x0027431A File Offset: 0x0027271A
			// (set) Token: 0x06005CB8 RID: 23736 RVA: 0x00274322 File Offset: 0x00272722
			public Transform Point
			{
				get
				{
					return this._point;
				}
				set
				{
					this._point = value;
				}
			}

			// Token: 0x170011A6 RID: 4518
			// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x0027432C File Offset: 0x0027272C
			// (set) Token: 0x06005CBA RID: 23738 RVA: 0x00274355 File Offset: 0x00272755
			public Transform RecoveryPoint
			{
				get
				{
					Transform result;
					if ((result = this._recoveryPoint) == null)
					{
						result = (this._recoveryPoint = this._point);
					}
					return result;
				}
				set
				{
					this._recoveryPoint = value;
				}
			}

			// Token: 0x0400532E RID: 21294
			[SerializeField]
			private Transform _point;

			// Token: 0x0400532F RID: 21295
			[SerializeField]
			private Transform _recoveryPoint;
		}

		// Token: 0x02000BCE RID: 3022
		public enum DirectionKind
		{
			// Token: 0x04005331 RID: 21297
			Free,
			// Token: 0x04005332 RID: 21298
			Lock,
			// Token: 0x04005333 RID: 21299
			Look
		}

		// Token: 0x02000BE6 RID: 3046
		[Serializable]
		public class ActionSlotTable : IEnumerable<ActionPoint.ActionSlot>, IEnumerable
		{
			// Token: 0x170011BC RID: 4540
			// (get) Token: 0x06005D1C RID: 23836 RVA: 0x00274371 File Offset: 0x00272771
			public List<ActionPoint.ActionSlot> Table
			{
				[CompilerGenerated]
				get
				{
					return this._table;
				}
			}

			// Token: 0x170011BD RID: 4541
			// (get) Token: 0x06005D1D RID: 23837 RVA: 0x00274379 File Offset: 0x00272779
			public int Count
			{
				[CompilerGenerated]
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x170011BE RID: 4542
			public ActionPoint.ActionSlot this[int idx]
			{
				get
				{
					return this._table[idx];
				}
				set
				{
					this._table[idx] = value;
				}
			}

			// Token: 0x06005D20 RID: 23840 RVA: 0x002743A3 File Offset: 0x002727A3
			public void Initialize()
			{
				this.Distinct();
			}

			// Token: 0x06005D21 RID: 23841 RVA: 0x002743AC File Offset: 0x002727AC
			private void Distinct()
			{
				List<ActionPoint.ActionSlot> list = ListPool<ActionPoint.ActionSlot>.Get();
				foreach (ActionPoint.ActionSlot actionSlot in this._table)
				{
					bool flag = true;
					foreach (ActionPoint.ActionSlot actionSlot2 in list)
					{
						if (actionSlot2.Point == actionSlot.Point)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						list.Add(actionSlot);
					}
				}
				List<ActionPoint.ActionSlot> list2 = ListPool<ActionPoint.ActionSlot>.Get();
				foreach (ActionPoint.ActionSlot item in this._table)
				{
					if (!list.Contains(item))
					{
						list2.Add(item);
					}
				}
				foreach (ActionPoint.ActionSlot item2 in list2)
				{
					this._table.Remove(item2);
				}
				ListPool<ActionPoint.ActionSlot>.Release(list2);
				ListPool<ActionPoint.ActionSlot>.Release(list);
			}

			// Token: 0x06005D22 RID: 23842 RVA: 0x0027453C File Offset: 0x0027293C
			public ActionPoint.ActionSlotTable.Enumerator GetEnumerator()
			{
				return new ActionPoint.ActionSlotTable.Enumerator(this);
			}

			// Token: 0x06005D23 RID: 23843 RVA: 0x00274544 File Offset: 0x00272944
			IEnumerator<ActionPoint.ActionSlot> IEnumerable<ActionPoint.ActionSlot>.GetEnumerator()
			{
				return new ActionPoint.ActionSlotTable.Enumerator(this);
			}

			// Token: 0x06005D24 RID: 23844 RVA: 0x00274551 File Offset: 0x00272951
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ActionPoint.ActionSlotTable.Enumerator(this);
			}

			// Token: 0x04005389 RID: 21385
			[SerializeField]
			private List<ActionPoint.ActionSlot> _table = new List<ActionPoint.ActionSlot>();

			// Token: 0x02000BE7 RID: 3047
			public struct Enumerator : IEnumerator<ActionPoint.ActionSlot>, IDisposable, IEnumerator
			{
				// Token: 0x06005D25 RID: 23845 RVA: 0x0027455E File Offset: 0x0027295E
				public Enumerator(List<ActionPoint.ActionSlot> list)
				{
					this._list = list;
					this._index = 0;
					this._current = null;
				}

				// Token: 0x06005D26 RID: 23846 RVA: 0x00274575 File Offset: 0x00272975
				public Enumerator(ActionPoint.ActionSlotTable table)
				{
					this._list = table._table;
					this._index = 0;
					this._current = null;
				}

				// Token: 0x170011C0 RID: 4544
				// (get) Token: 0x06005D27 RID: 23847 RVA: 0x00274591 File Offset: 0x00272991
				public ActionPoint.ActionSlot Current
				{
					[CompilerGenerated]
					get
					{
						return this._current;
					}
				}

				// Token: 0x170011BF RID: 4543
				// (get) Token: 0x06005D28 RID: 23848 RVA: 0x00274599 File Offset: 0x00272999
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._list.Count + 1)
						{
							throw new InvalidOperationException();
						}
						return this._current;
					}
				}

				// Token: 0x06005D29 RID: 23849 RVA: 0x002745CA File Offset: 0x002729CA
				public void Dispose()
				{
				}

				// Token: 0x06005D2A RID: 23850 RVA: 0x002745CC File Offset: 0x002729CC
				public bool MoveNext()
				{
					if (this._index < this._list.Count)
					{
						this._current = this._list[this._index];
						this._index++;
						return true;
					}
					return this.MoveNextRare();
				}

				// Token: 0x06005D2B RID: 23851 RVA: 0x0027461C File Offset: 0x00272A1C
				private bool MoveNextRare()
				{
					this._index = this._list.Count + 1;
					this._current = null;
					return false;
				}

				// Token: 0x06005D2C RID: 23852 RVA: 0x00274639 File Offset: 0x00272A39
				void IEnumerator.Reset()
				{
					this._index = 0;
					this._current = null;
				}

				// Token: 0x0400538A RID: 21386
				private List<ActionPoint.ActionSlot> _list;

				// Token: 0x0400538B RID: 21387
				private int _index;

				// Token: 0x0400538C RID: 21388
				private ActionPoint.ActionSlot _current;
			}
		}

		// Token: 0x02000BE8 RID: 3048
		[Serializable]
		public class ActionSlot
		{
			// Token: 0x170011C1 RID: 4545
			// (get) Token: 0x06005D2E RID: 23854 RVA: 0x00274651 File Offset: 0x00272A51
			public EventType AcceptionKey
			{
				[CompilerGenerated]
				get
				{
					return this._acceptionKey;
				}
			}

			// Token: 0x170011C2 RID: 4546
			// (get) Token: 0x06005D2F RID: 23855 RVA: 0x00274659 File Offset: 0x00272A59
			// (set) Token: 0x06005D30 RID: 23856 RVA: 0x00274661 File Offset: 0x00272A61
			public Transform Point
			{
				get
				{
					return this._point;
				}
				set
				{
					this._point = value;
				}
			}

			// Token: 0x170011C3 RID: 4547
			// (get) Token: 0x06005D31 RID: 23857 RVA: 0x0027466A File Offset: 0x00272A6A
			// (set) Token: 0x06005D32 RID: 23858 RVA: 0x00274672 File Offset: 0x00272A72
			public Transform RecoveryPoint
			{
				get
				{
					return this._recoveryPoint;
				}
				set
				{
					this._recoveryPoint = value;
				}
			}

			// Token: 0x170011C4 RID: 4548
			// (get) Token: 0x06005D33 RID: 23859 RVA: 0x0027467B File Offset: 0x00272A7B
			// (set) Token: 0x06005D34 RID: 23860 RVA: 0x00274683 File Offset: 0x00272A83
			public Actor Actor
			{
				get
				{
					return this._actor;
				}
				set
				{
					this._actor = value;
				}
			}

			// Token: 0x0400538D RID: 21389
			[SerializeField]
			private EventType _acceptionKey;

			// Token: 0x0400538E RID: 21390
			[SerializeField]
			private Transform _point;

			// Token: 0x0400538F RID: 21391
			[SerializeField]
			private Transform _recoveryPoint;

			// Token: 0x04005390 RID: 21392
			[SerializeField]
			private Actor _actor;
		}
	}
}
