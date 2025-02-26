using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.SaveData;
using AIProject.UI;
using AIProject.UI.Popup;
using Cinemachine;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C01 RID: 3073
	public class EventPoint : Point, ICommandable
	{
		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x0027D429 File Offset: 0x0027B829
		public int GroupID
		{
			[CompilerGenerated]
			get
			{
				return this._groupID;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06005E3E RID: 24126 RVA: 0x0027D431 File Offset: 0x0027B831
		public int PointID
		{
			[CompilerGenerated]
			get
			{
				return this._pointID;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x0027D439 File Offset: 0x0027B839
		public Transform LabelPoint
		{
			get
			{
				return (!(this._labelPoint != null)) ? base.transform : this._labelPoint;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06005E40 RID: 24128 RVA: 0x0027D45D File Offset: 0x0027B85D
		public bool EnableRangeCheck
		{
			[CompilerGenerated]
			get
			{
				return this._enableRangeCheck;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x0027D465 File Offset: 0x0027B865
		public float CheckRadius
		{
			[CompilerGenerated]
			get
			{
				return this._checkRadius;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x0027D46D File Offset: 0x0027B86D
		// (set) Token: 0x06005E43 RID: 24131 RVA: 0x0027D475 File Offset: 0x0027B875
		public int OpenAreaID { get; protected set; } = -1;

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x0027D47E File Offset: 0x0027B87E
		// (set) Token: 0x06005E45 RID: 24133 RVA: 0x0027D486 File Offset: 0x0027B886
		public EventPoint.EventTypes EventType { get; protected set; } = EventPoint.EventTypes.Other;

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x0027D48F File Offset: 0x0027B88F
		// (set) Token: 0x06005E47 RID: 24135 RVA: 0x0027D497 File Offset: 0x0027B897
		public int EventID { get; protected set; } = -1;

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x0027D4A0 File Offset: 0x0027B8A0
		// (set) Token: 0x06005E49 RID: 24137 RVA: 0x0027D4A8 File Offset: 0x0027B8A8
		public int WarningID { get; protected set; } = -1;

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06005E4A RID: 24138 RVA: 0x0027D4B1 File Offset: 0x0027B8B1
		private OpenData _openData { get; } = new OpenData();

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x0027D4B9 File Offset: 0x0027B8B9
		// (set) Token: 0x06005E4C RID: 24140 RVA: 0x0027D4C1 File Offset: 0x0027B8C1
		private EventPoint.PackData _eventStoryPackData { get; set; }

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x0027D4CA File Offset: 0x0027B8CA
		// (set) Token: 0x06005E4E RID: 24142 RVA: 0x0027D4D2 File Offset: 0x0027B8D2
		private EventPoint.MessagePackData _messagePackData { get; set; }

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06005E4F RID: 24143 RVA: 0x0027D4DB File Offset: 0x0027B8DB
		// (set) Token: 0x06005E50 RID: 24144 RVA: 0x0027D4E3 File Offset: 0x0027B8E3
		private EventPoint.MessageCommandData _messageCommandData { get; set; }

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x0027D4EC File Offset: 0x0027B8EC
		public int InstanceID
		{
			get
			{
				if (this._instanceID != null)
				{
					return this._instanceID.Value;
				}
				this._instanceID = new int?(base.GetInstanceID());
				return this._instanceID.Value;
			}
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x0027D528 File Offset: 0x0027B928
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.IsStopped)
			{
				return false;
			}
			if (this._groupID == 2)
			{
				return false;
			}
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = Vector3.Distance(basePosition, commandCenter);
			CommandType commandType = this._commandType;
			bool flag;
			if (commandType != CommandType.Forward)
			{
				float num2 = (!this._enableRangeCheck) ? radiusB : (radiusB + this._checkRadius);
				flag = (distance <= num2);
			}
			else
			{
				float num3 = (!this._enableRangeCheck) ? radiusA : (radiusA + this._checkRadius);
				if (num3 < num)
				{
					return false;
				}
				Vector3 a = commandCenter;
				a.y = 0f;
				float num4 = angle / 2f;
				float num5 = Vector3.Angle(a - basePosition, forward);
				flag = (num5 <= num4);
			}
			if (!flag)
			{
				return false;
			}
			PlayerActor player = Manager.Map.GetPlayer();
			if (player != null)
			{
				IState state = player.PlayerController.State;
				if (state is Onbu)
				{
					return false;
				}
			}
			return this.IsNeutralCommand && flag;
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x0027D650 File Offset: 0x0027BA50
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				if (this._groupID == 1 && this._pointID == 3)
				{
					return true;
				}
				nmAgent.CalculatePath(this.Position, this._pathForCalc);
				flag &= (this._pathForCalc.status == NavMeshPathStatus.PathComplete);
				if (!flag)
				{
					return false;
				}
				float num = 0f;
				Vector3[] corners = this._pathForCalc.corners;
				float num2 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
				for (int i = 0; i < corners.Length - 1; i++)
				{
					float num3 = Vector3.Distance(corners[i], corners[i + 1]);
					num += num3;
					if (num2 < num)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06005E54 RID: 24148 RVA: 0x0027D743 File Offset: 0x0027BB43
		// (set) Token: 0x06005E55 RID: 24149 RVA: 0x0027D74B File Offset: 0x0027BB4B
		public bool IsImpossible { get; protected set; }

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06005E56 RID: 24150 RVA: 0x0027D754 File Offset: 0x0027BB54
		// (set) Token: 0x06005E57 RID: 24151 RVA: 0x0027D75C File Offset: 0x0027BB5C
		public Actor CommandPartner { get; private set; }

		// Token: 0x06005E58 RID: 24152 RVA: 0x0027D768 File Offset: 0x0027BB68
		public bool SetImpossible(bool value, Actor actor)
		{
			if (this.IsImpossible == value)
			{
				return false;
			}
			if (this.CommandPartner != null && this.CommandPartner != actor)
			{
				return false;
			}
			this.IsImpossible = value;
			this.CommandPartner = actor;
			return true;
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x0027D7B6 File Offset: 0x0027BBB6
		// (set) Token: 0x06005E5A RID: 24154 RVA: 0x0027D7BE File Offset: 0x0027BBBE
		public bool IsRunable { get; set; } = true;

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06005E5B RID: 24155 RVA: 0x0027D7C7 File Offset: 0x0027BBC7
		public bool IsNeutralCommand
		{
			get
			{
				return base.isActiveAndEnabled && this.IsRunable && this.IsNeutral;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06005E5C RID: 24156 RVA: 0x0027D7E8 File Offset: 0x0027BBE8
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06005E5D RID: 24157 RVA: 0x0027D7F5 File Offset: 0x0027BBF5
		public Vector3 CommandCenter
		{
			get
			{
				return (!(this._commandBasePoint != null)) ? base.transform.position : this._commandBasePoint.position;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06005E5E RID: 24158 RVA: 0x0027D823 File Offset: 0x0027BC23
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return ObjectLayer.Command;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06005E5F RID: 24159 RVA: 0x0027D826 File Offset: 0x0027BC26
		public CommandType CommandType
		{
			[CompilerGenerated]
			get
			{
				return this._commandType;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06005E60 RID: 24160 RVA: 0x0027D82E File Offset: 0x0027BC2E
		public int LabelIndex
		{
			[CompilerGenerated]
			get
			{
				return this._labelIndex.Value;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06005E61 RID: 24161 RVA: 0x0027D83B File Offset: 0x0027BC3B
		public int DateLabelIndex
		{
			[CompilerGenerated]
			get
			{
				return this._dateLabelIndex.Value;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06005E62 RID: 24162 RVA: 0x0027D848 File Offset: 0x0027BC48
		// (set) Token: 0x06005E63 RID: 24163 RVA: 0x0027D84F File Offset: 0x0027BC4F
		public static int TargetGroupID { get; set; } = -1;

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x0027D857 File Offset: 0x0027BC57
		// (set) Token: 0x06005E65 RID: 24165 RVA: 0x0027D85E File Offset: 0x0027BC5E
		public static int TargetPointID { get; set; } = -1;

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06005E66 RID: 24166 RVA: 0x0027D866 File Offset: 0x0027BC66
		public bool TargetPoint
		{
			[CompilerGenerated]
			get
			{
				return this._groupID == EventPoint.TargetGroupID && this._pointID == EventPoint.TargetPointID;
			}
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x0027D888 File Offset: 0x0027BC88
		public static UnityEx.ValueTuple<int, int> GetTargetID()
		{
			return new UnityEx.ValueTuple<int, int>(EventPoint.TargetGroupID, EventPoint.TargetPointID);
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x0027D899 File Offset: 0x0027BC99
		public static EventPoint GetTargetPoint()
		{
			return EventPoint.Get(EventPoint.TargetGroupID, EventPoint.TargetPointID);
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x0027D8AA File Offset: 0x0027BCAA
		public static void SetTargetID(int groupID, int pointID)
		{
			EventPoint.TargetGroupID = groupID;
			EventPoint.TargetPointID = pointID;
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0027D8B8 File Offset: 0x0027BCB8
		public static void SetTargetID(EventPoint point)
		{
			if (point != null)
			{
				EventPoint.TargetGroupID = point.GroupID;
				EventPoint.TargetPointID = point.PointID;
			}
			else
			{
				EventPoint.TargetGroupID = -1;
				EventPoint.TargetPointID = -1;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06005E6B RID: 24171 RVA: 0x0027D8ED File Offset: 0x0027BCED
		// (set) Token: 0x06005E6C RID: 24172 RVA: 0x0027D8F5 File Offset: 0x0027BCF5
		public bool IsFreeMode { get; private set; }

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06005E6D RID: 24173 RVA: 0x0027D8FE File Offset: 0x0027BCFE
		// (set) Token: 0x06005E6E RID: 24174 RVA: 0x0027D906 File Offset: 0x0027BD06
		public bool IsStopped { get; private set; }

		// Token: 0x06005E6F RID: 24175 RVA: 0x0027D910 File Offset: 0x0027BD10
		protected void SetSafeguardObjectActive(bool setFlag, bool forced = false)
		{
			if ((this._safeguardObjectFlag != setFlag || forced) && !this._safeguardObjects.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._safeguardObjects)
				{
					if (gameObject != null && gameObject.activeSelf != setFlag)
					{
						gameObject.SetActive(setFlag);
					}
				}
			}
			this._safeguardObjectFlag = setFlag;
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x0027D9B0 File Offset: 0x0027BDB0
		protected virtual void Awake()
		{
			this.IsFreeMode = Game.IsFreeMode;
			this.IsStopped = this.CheckStopState();
			if (EventPoint._commandRefreshEvent == null)
			{
				EventPoint._commandRefreshEvent = new Subject<Unit>();
				(from _ in EventPoint._commandRefreshEvent.Buffer(EventPoint._commandRefreshEvent.ThrottleFrame(1, FrameCountType.Update)).TakeUntilDestroy(Singleton<Manager.Map>.Instance)
				where !_.IsNullOrEmpty<Unit>()
				select _).Subscribe(delegate(IList<Unit> _)
				{
					Manager.Map instance = Singleton<Manager.Map>.Instance;
					CommandArea commandArea;
					if (instance == null)
					{
						commandArea = null;
					}
					else
					{
						PlayerActor player = instance.Player;
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
					CommandArea commandArea2 = commandArea;
					if (commandArea2 == null)
					{
						return;
					}
					commandArea2.RefreshCommands();
				});
			}
			Dictionary<int, EventPoint> dictionary;
			if (!EventPoint._eventPointTable.TryGetValue(this._groupID, out dictionary))
			{
				dictionary = (EventPoint._eventPointTable[this._groupID] = new Dictionary<int, EventPoint>());
			}
			if (!dictionary.ContainsKey(this._pointID))
			{
				dictionary[this._pointID] = this;
			}
			if (this._groupID == 2 && this._pointID == 1)
			{
				MerchantProfile merchantProfile = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.MerchantProfile;
				if (merchantProfile != null)
				{
					int lastADVSafeguardID = merchantProfile.LastADVSafeguardID;
					this._safeguardObjects = MapItemData.Get(lastADVSafeguardID);
					this.SetSafeguardObjectActive(false, true);
					if (this.GetDedicatedNumber() == 0)
					{
						Observable.EveryUpdate().TakeUntilDestroy(this).TakeWhile((long _) => this.GetDedicatedNumber() == 0).Subscribe(delegate(long _)
						{
							PlayerActor player = Manager.Map.GetPlayer();
							if (player == null)
							{
								return;
							}
							IState state = player.PlayerController.State;
							bool flag = false;
							flag |= (state is Follow);
							if (!flag)
							{
								AgentActor agentActor = player.CommCompanion as AgentActor;
								if (agentActor == null)
								{
									agentActor = (player.Partner as AgentActor);
								}
								flag = (agentActor != null && (agentActor.Mode == Desire.ActionType.EndTaskH || agentActor.Mode == Desire.ActionType.ReverseRape || agentActor.Mode == Desire.ActionType.TakeHPoint));
							}
							this.SetSafeguardObjectActive(flag, false);
						}, delegate(Exception ex)
						{
						}, delegate()
						{
							this.SetSafeguardObjectActive(false, true);
						});
					}
				}
			}
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x0027DB5C File Offset: 0x0027BF5C
		public bool CheckStopState()
		{
			int groupID = this._groupID;
			if (groupID != 0)
			{
				if (groupID != 1)
				{
					if (groupID == 2)
					{
						if (this.IsFreeMode && 0 <= this._pointID && this._pointID <= 1)
						{
							return true;
						}
					}
				}
				else if (this.IsFreeMode && 0 <= this._pointID && this._pointID <= 6)
				{
					return true;
				}
			}
			else if (this.IsFreeMode && 0 <= this._pointID && this._pointID <= 6)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x0027DC08 File Offset: 0x0027C008
		public void RemoveConsiderationCommand()
		{
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
			bool? flag = (commandArea2 != null) ? new bool?(commandArea2.ContainsConsiderationObject(this)) : null;
			if (flag != null && flag.Value)
			{
				commandArea2.RemoveConsiderationObject(this);
				if (EventPoint._commandRefreshEvent != null)
				{
					EventPoint._commandRefreshEvent.OnNext(Unit.Default);
				}
			}
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x0027DCA8 File Offset: 0x0027C0A8
		protected override void Start()
		{
			if (this.IsStopped)
			{
				return;
			}
			base.Start();
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = ((!(gameObject != null)) ? base.transform : gameObject.transform);
			}
			if (this._labelPoint == null)
			{
				GameObject gameObject2 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.EventPointLabelTargetName);
				this._labelPoint = ((!(gameObject2 != null)) ? base.transform : gameObject2.transform);
			}
			this.InitializeCommandLabels();
			this._labelIndex.TakeUntilDestroy(base.gameObject).DistinctUntilChanged<int>().Subscribe(delegate(int idx)
			{
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
				bool? flag = (commandArea2 != null) ? new bool?(commandArea2.ContainsConsiderationObject(this)) : null;
				if (flag != null && flag.Value)
				{
					if (EventPoint._commandRefreshEvent != null)
					{
						EventPoint._commandRefreshEvent.OnNext(Unit.Default);
					}
				}
				if (Singleton<MapUIContainer>.IsInstance())
				{
					CommandLabel commandLabel = MapUIContainer.CommandLabel;
					if (commandLabel.Acception != CommandLabel.AcceptionState.InvokeAcception)
					{
						EventPoint._changeNoneMode = true;
					}
				}
			});
			this._dateLabelIndex.TakeUntilDestroy(base.gameObject).Subscribe(delegate(int idx)
			{
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
				bool? flag = (commandArea2 != null) ? new bool?(commandArea2.ContainsConsiderationObject(this)) : null;
				if (flag != null && flag.Value)
				{
					if (EventPoint._commandRefreshEvent != null)
					{
						EventPoint._commandRefreshEvent.OnNext(Unit.Default);
					}
				}
				if (Singleton<MapUIContainer>.IsInstance())
				{
					CommandLabel commandLabel = MapUIContainer.CommandLabel;
					if (commandLabel.Acception != CommandLabel.AcceptionState.InvokeAcception)
					{
						EventPoint._changeNoneMode = true;
					}
				}
			});
			if (this._groupID == 2)
			{
				CheckEventPointArea orAddComponent = this.GetOrAddComponent<CheckEventPointArea>();
				orAddComponent.EventPoint = this;
			}
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x0027DE08 File Offset: 0x0027C208
		protected override void OnEnable()
		{
			base.OnEnable();
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x0027DE10 File Offset: 0x0027C210
		protected override void OnDisable()
		{
			base.OnDisable();
			this.RemoveConsiderationCommand();
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x0027DE20 File Offset: 0x0027C220
		private void OnDestroy()
		{
			Dictionary<int, EventPoint> dictionary;
			if (EventPoint._eventPointTable.TryGetValue(this._groupID, out dictionary) && dictionary.ContainsKey(this._pointID))
			{
				dictionary.Remove(this._pointID);
			}
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x0027DE64 File Offset: 0x0027C264
		private void OnUpdate()
		{
			bool flag = true;
			if (Singleton<Manager.Map>.IsInstance())
			{
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				if (player != null && player.Mode == Desire.ActionType.Date)
				{
					flag = false;
				}
			}
			int value = 0;
			int groupID = this._groupID;
			if (groupID != 0)
			{
				if (groupID != 1)
				{
					if (groupID == 2)
					{
						this.Labels_Group_02(ref value);
					}
				}
				else
				{
					this.Labels_Group_01(ref value);
				}
			}
			else
			{
				this.Labels_Group_00(ref value);
			}
			this._labelIndex.Value = value;
			if (!flag)
			{
				int value2 = 0;
				int groupID2 = this._groupID;
				if (groupID2 != 0)
				{
					if (groupID2 != 1)
					{
						if (groupID2 == 2)
						{
							this.Date_Labels_Group_02(ref value2);
						}
					}
					else
					{
						this.Date_Labels_Group_01(ref value2);
					}
				}
				else
				{
					this.Date_Labels_Group_00(ref value2);
				}
				this._dateLabelIndex.Value = value2;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06005E78 RID: 24184 RVA: 0x0027DF58 File Offset: 0x0027C358
		public static int LangIdx
		{
			get
			{
				return (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			}
		}

		// Token: 0x06005E79 RID: 24185 RVA: 0x0027DF74 File Offset: 0x0027C374
		public void SetActive(bool active)
		{
			if (base.gameObject.activeSelf != active)
			{
				base.gameObject.SetActive(active);
			}
		}

		// Token: 0x06005E7A RID: 24186 RVA: 0x0027DF93 File Offset: 0x0027C393
		public void ChangeActive()
		{
			base.gameObject.SetActive(!base.gameObject.activeSelf);
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x0027DFAE File Offset: 0x0027C3AE
		protected void PopupWarning(Popup.Warning.Type warningType)
		{
			MapUIContainer.PushWarningMessage(warningType);
			this.RemoveConsiderationCommand();
		}

		// Token: 0x06005E7C RID: 24188 RVA: 0x0027DFBC File Offset: 0x0027C3BC
		public void SetOpenArea(bool active)
		{
			this.SetOpenArea(this.OpenAreaID, active);
		}

		// Token: 0x06005E7D RID: 24189 RVA: 0x0027DFCB File Offset: 0x0027C3CB
		protected void SetOpenArea(int areaID, bool active)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			Singleton<Manager.Map>.Instance.SetOpenAreaState(areaID, active);
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x0027DFE4 File Offset: 0x0027C3E4
		protected bool GetOpenArea(int areaID)
		{
			return Singleton<Manager.Map>.IsInstance() && Singleton<Manager.Map>.Instance.GetOpenAreaState(areaID);
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x0027E000 File Offset: 0x0027C400
		protected bool GetOpenArea(Manager.Map map, int areaID)
		{
			bool? flag = (map != null) ? new bool?(map.GetOpenAreaState(areaID)) : null;
			return flag != null && flag.Value;
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x0027E044 File Offset: 0x0027C444
		protected bool GetTimeObjOpen(int groupID)
		{
			return Singleton<Manager.Map>.IsInstance() && Singleton<Manager.Map>.Instance.GetTimeObjOpenState(groupID);
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x0027E060 File Offset: 0x0027C460
		protected void ChangeRequestEventMode(int eventID, int warningID = -1)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (player == null)
			{
				return;
			}
			EventPoint.SetCurrentPlayerStateName();
			this.EventID = eventID;
			this.WarningID = warningID;
			player.CurrentEventPoint = this;
			player.PlayerController.ChangeState("RequestEvent");
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x0027E0BC File Offset: 0x0027C4BC
		protected bool CheckCharaPhaseProgress(int border)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return false;
			}
			if (!Singleton<GameSystem>.IsInstance())
			{
				return false;
			}
			ReadOnlyDictionary<int, AgentActor> agentTable = Singleton<Manager.Map>.Instance.AgentTable;
			if (agentTable.IsNullOrEmpty<int, AgentActor>())
			{
				return false;
			}
			string userUUID = Singleton<GameSystem>.Instance.UserUUID;
			foreach (KeyValuePair<int, AgentActor> keyValuePair in agentTable)
			{
				AgentActor value = keyValuePair.Value;
				if (((value != null) ? value.AgentData : null) != null)
				{
					if (border <= value.ChaControl.fileGameInfo.phase)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x0027E18C File Offset: 0x0027C58C
		public static bool SetDedicatedNumber(int groupID, int pointID, int num)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, Dictionary<int, int>> dictionary = (environment != null) ? environment.EventPointStateTable : null;
			if (dictionary == null)
			{
				return false;
			}
			Dictionary<int, int> dictionary2;
			if (!dictionary.TryGetValue(groupID, out dictionary2))
			{
				dictionary2 = (dictionary[groupID] = new Dictionary<int, int>());
			}
			dictionary2[pointID] = num;
			return true;
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x0027E1EC File Offset: 0x0027C5EC
		public bool SetDedicatedNumber(int num)
		{
			return EventPoint.SetDedicatedNumber(this.GroupID, this.PointID, num);
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x0027E200 File Offset: 0x0027C600
		public static bool SetDedicatedNumber(EventPoint point, int num)
		{
			return !(point == null) && EventPoint.SetDedicatedNumber(point.GroupID, point.PointID, num);
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x0027E224 File Offset: 0x0027C624
		public static int GetDedicatedNumber(int groupID, int pointID)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return 0;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, Dictionary<int, int>> dictionary = (environment != null) ? environment.EventPointStateTable : null;
			if (dictionary == null)
			{
				return 0;
			}
			Dictionary<int, int> dictionary2;
			if (!dictionary.TryGetValue(groupID, out dictionary2))
			{
				dictionary2 = (dictionary[groupID] = new Dictionary<int, int>());
			}
			int result;
			if (!dictionary2.TryGetValue(pointID, out result))
			{
				result = (dictionary2[pointID] = 0);
			}
			return result;
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0027E294 File Offset: 0x0027C694
		public int GetDedicatedNumber()
		{
			return EventPoint.GetDedicatedNumber(this.GroupID, this.PointID);
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x0027E2A7 File Offset: 0x0027C6A7
		public static int GetDedicatedNumber(EventPoint point)
		{
			if (point == null)
			{
				return 0;
			}
			return EventPoint.GetDedicatedNumber(point.GroupID, point.PointID);
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x0027E2C8 File Offset: 0x0027C6C8
		public static bool SetAgentOpenState(int agentID, bool state)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			Dictionary<int, AgentData> dictionary = (worldData != null) ? worldData.AgentTable : null;
			if (dictionary.IsNullOrEmpty<int, AgentData>())
			{
				return false;
			}
			AgentData agentData;
			if (dictionary.TryGetValue(agentID, out agentData) && agentData != null)
			{
				agentData.OpenState = state;
				return true;
			}
			return false;
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x0027E328 File Offset: 0x0027C728
		public bool SetAgentOpenState(bool state)
		{
			int num = -1;
			if (this._groupID == 0)
			{
				if (this._pointID == 4)
				{
					num = 1;
				}
				else if (this._pointID == 5)
				{
					num = 2;
				}
				else if (this._pointID == 6)
				{
					num = 3;
				}
			}
			return num >= 0 && EventPoint.SetAgentOpenState(num, state);
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x0027E388 File Offset: 0x0027C788
		public static bool GetAgentOpenState(int agentID)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			Dictionary<int, AgentData> dictionary = (worldData != null) ? worldData.AgentTable : null;
			AgentData agentData;
			return !dictionary.IsNullOrEmpty<int, AgentData>() && (dictionary.TryGetValue(agentID, out agentData) && agentData != null) && agentData.OpenState;
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x0027E3E4 File Offset: 0x0027C7E4
		private int GetNeedFlavorAdditionAmount(int requestID)
		{
			if (Singleton<Manager.Resources>.IsInstance())
			{
				ReadOnlyDictionary<int, RequestInfo> requestTable = Singleton<Manager.Resources>.Instance.PopupInfo.RequestTable;
				Dictionary<int, int> requestFlavorAdditionBorderTable = Singleton<Manager.Resources>.Instance.PopupInfo.RequestFlavorAdditionBorderTable;
				RequestInfo requestInfo;
				if (requestTable.TryGetValue(requestID, out requestInfo) && requestInfo != null && requestInfo.Type == 2)
				{
					System.Tuple<int, int, int> element = requestInfo.Items.GetElement(0);
					int? num = (element != null) ? new int?(element.Item1) : null;
					int num2 = (num == null) ? 0 : num.Value;
					if (!requestFlavorAdditionBorderTable.IsNullOrEmpty<int, int>())
					{
						foreach (KeyValuePair<int, int> keyValuePair in requestFlavorAdditionBorderTable)
						{
							if (keyValuePair.Key >= requestID)
							{
								break;
							}
							num2 += keyValuePair.Value;
						}
					}
					return num2;
				}
			}
			return 0;
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x0027E4F4 File Offset: 0x0027C8F4
		public static EventPoint Get(int groupID, int pointID)
		{
			Dictionary<int, EventPoint> dictionary;
			if (EventPoint._eventPointTable.TryGetValue(groupID, out dictionary))
			{
				EventPoint result;
				dictionary.TryGetValue(pointID, out result);
				return result;
			}
			return null;
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x0027E520 File Offset: 0x0027C920
		public static bool IsGameCleared()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			return worldData != null && worldData.Cleared;
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x0027E554 File Offset: 0x0027C954
		public static void OpenEventStart(PlayerActor player, float startFadeTime, float endFadeTime, int SEID, float delayTime, float endIntervalTime, System.Action changeAction = null, System.Action endAction = null)
		{
			MapUIContainer.SetVisibleHUD(false);
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, startFadeTime, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Observable.Timer(TimeSpan.FromSeconds((double)delayTime)).Subscribe(delegate(long _)
				{
					System.Action changeAction2 = changeAction;
					if (changeAction2 != null)
					{
						changeAction2();
					}
					AudioSource audioSource = null;
					if (Singleton<Manager.Resources>.IsInstance())
					{
						audioSource = Singleton<Manager.Resources>.Instance.SoundPack.Play(SEID, Sound.Type.GameSE3D, 0f);
					}
					if (audioSource != null)
					{
						audioSource.Stop();
						if (player != null)
						{
							audioSource.transform.SetPositionAndRotation(player.Position, player.Rotation);
						}
						audioSource.Play();
						audioSource.OnDestroyAsObservable().Subscribe(delegate(Unit __)
						{
							Observable.Timer(TimeSpan.FromSeconds((double)endIntervalTime)).Subscribe(delegate(long ___)
							{
								MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, endFadeTime, true).Subscribe(delegate(Unit ____)
								{
								}, delegate()
								{
									MapUIContainer.SetVisibleHUD(true);
									System.Action endAction2 = endAction;
									if (endAction2 != null)
									{
										endAction2();
									}
								});
							});
						});
					}
					else
					{
						Observable.Timer(TimeSpan.FromSeconds((double)endIntervalTime)).Subscribe(delegate(long __)
						{
							MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, endFadeTime, true).Subscribe(delegate(Unit ___)
							{
							}, delegate()
							{
								MapUIContainer.SetVisibleHUD(true);
								System.Action endAction2 = endAction;
								if (endAction2 != null)
								{
									endAction2();
								}
							});
						});
					}
				});
			});
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x0027E5E0 File Offset: 0x0027C9E0
		public void StartMessageDialogDisplay(int id, System.Action submitAction = null)
		{
			if (!Singleton<Manager.Map>.IsInstance() || !Singleton<Game>.IsInstance() || !Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<int, UnityEx.ValueTuple<int, List<string>>> eventDialogInfoTable = Singleton<Manager.Resources>.Instance.Map.EventDialogInfoTable;
			if (eventDialogInfoTable.IsNullOrEmpty<int, UnityEx.ValueTuple<int, List<string>>>())
			{
				return;
			}
			UnityEx.ValueTuple<int, List<string>> valuePair;
			if (!eventDialogInfoTable.TryGetValue(id, out valuePair))
			{
				return;
			}
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			PlayerActor player = instance.Player;
			EventPoint.SetCurrentPlayerStateName();
			player.PlayerController.ChangeState("Idle");
			MapUIContainer.SetVisibleHUD(false);
			if (this._messageCommandData == null)
			{
				this._messageCommandData = new EventPoint.MessageCommandData();
				this._messageCommandData.SendFlag = true;
			}
			if (this._messagePackData == null)
			{
				this._messagePackData = new EventPoint.MessagePackData();
				this._messagePackData.SetCommandData(new ADV.ICommandData[]
				{
					this._messageCommandData
				});
				this._messagePackData.SetParam(new IParams[]
				{
					player.PlayerData
				});
			}
			ADVScene advScene = Singleton<MapUIContainer>.Instance.advScene;
			advScene.Scenario.advUI.Visible(false);
			this._messagePackData.onComplete = delegate()
			{
				EventDialogUI eventDialogUI = MapUIContainer.EventDialogUI;
				eventDialogUI.SubmitEvent = submitAction;
				eventDialogUI.CancelEvent = delegate()
				{
					EventPoint.ChangePrevPlayerMode();
					MapUIContainer.SetVisibleHUD(true);
				};
				eventDialogUI.ClosedEvent = delegate()
				{
				};
				eventDialogUI.MessageText = (valuePair.Item2.GetElement(EventPoint.LangIdx) ?? string.Empty);
				eventDialogUI.IsActiveControl = true;
			};
			this._openData.FindLoadMessage("event", string.Format("{0}", id));
			advScene.OnDisableAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				advScene.Scenario.advUI.Visible(true);
			});
			Singleton<MapUIContainer>.Instance.OpenADV(this._openData, this._messagePackData);
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x0027E774 File Offset: 0x0027CB74
		public static void ChangeNormalState()
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (player == null)
			{
				return;
			}
			if (Singleton<MapUIContainer>.IsInstance() && MapUIContainer.CommandLabel.Acception != CommandLabel.AcceptionState.InvokeAcception)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			}
			if (Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				instance.FocusLevel = -1;
				if (instance.State != Manager.Input.ValidType.Action)
				{
					instance.ReserveState(Manager.Input.ValidType.Action);
					instance.SetupState();
				}
			}
			if (!player.CurrentInteractionState)
			{
				player.SetScheduledInteractionState(true);
				player.ReleaseInteraction();
			}
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x0027E80C File Offset: 0x0027CC0C
		public static void ChangeNormalMode()
		{
			EventPoint.ChangeNormalState();
			PlayerActor playerActor = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
			if (playerActor != null)
			{
				playerActor.PlayerController.ChangeState("Normal");
			}
			if (EventPoint._changeNoneMode)
			{
				MapUIContainer.CommandLabel.RefreshCommands();
				EventPoint._changeNoneMode = false;
			}
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x0027E86C File Offset: 0x0027CC6C
		public static void ChangePlayerMode(string playerStateName = "Normal")
		{
			EventPoint.ChangeNormalState();
			PlayerActor playerActor = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
			if (playerActor != null)
			{
				playerActor.PlayerController.ChangeState(playerStateName);
			}
			if (EventPoint._changeNoneMode)
			{
				MapUIContainer.CommandLabel.RefreshCommands();
				EventPoint._changeNoneMode = false;
			}
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x0027E8C7 File Offset: 0x0027CCC7
		public static void ChangePrevPlayerMode()
		{
			EventPoint.ChangePlayerMode(EventPoint.PrevPlayerStateName);
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06005E95 RID: 24213 RVA: 0x0027E8D4 File Offset: 0x0027CCD4
		private Dictionary<int, AgentActor> NormalChangeActorTable
		{
			get
			{
				Dictionary<int, AgentActor> result;
				if ((result = this._normalChangeActorTable) == null)
				{
					result = (this._normalChangeActorTable = new Dictionary<int, AgentActor>());
				}
				return result;
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06005E96 RID: 24214 RVA: 0x0027E8FC File Offset: 0x0027CCFC
		// (set) Token: 0x06005E97 RID: 24215 RVA: 0x0027E903 File Offset: 0x0027CD03
		public static string PrevPlayerStateName { get; set; } = string.Empty;

		// Token: 0x06005E98 RID: 24216 RVA: 0x0027E90C File Offset: 0x0027CD0C
		public static void SetCurrentPlayerStateName()
		{
			EventPoint.PrevPlayerStateName = string.Empty;
			PlayerActor player = Manager.Map.GetPlayer();
			PlayerController playerController = (!(player != null)) ? null : player.PlayerController;
			IState state = (!(playerController != null)) ? null : playerController.State;
			if (state == null)
			{
				return;
			}
			EventPoint.PrevPlayerStateName = state.GetType().ToString();
			int num = EventPoint.PrevPlayerStateName.LastIndexOf('.');
			if (0 <= num)
			{
				EventPoint.PrevPlayerStateName = EventPoint.PrevPlayerStateName.Substring(num + 1);
			}
			if (EventPoint.PrevPlayerStateName == "Houchi")
			{
				EventPoint.PrevPlayerStateName = "Normal";
			}
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x0027E9B8 File Offset: 0x0027CDB8
		public void StartMerchantADV(int id)
		{
			if (!Singleton<Manager.Map>.IsInstance() || !Singleton<Game>.IsInstance())
			{
				return;
			}
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			Game instance2 = Singleton<Game>.Instance;
			PlayerActor player = instance.Player;
			MerchantActor merchant = instance.Merchant;
			bool? flag;
			if (instance == null)
			{
				flag = null;
			}
			else
			{
				EnvironmentSimulator simulator = instance.Simulator;
				flag = ((simulator != null) ? new bool?(simulator.EnabledTimeProgression) : null);
			}
			bool? flag2 = flag;
			if (flag2 != null)
			{
				instance.Simulator.EnabledTimeProgression = false;
			}
			this._openData.FindLoad(string.Format("{0}", id), -90, 1);
			if (this._eventStoryPackData == null)
			{
				this._eventStoryPackData = new EventPoint.PackData();
				this._eventStoryPackData.SetParam(new IParams[]
				{
					merchant.MerchantData,
					player.PlayerData
				});
				this._eventStoryPackData.SetCommandData(new ADV.ICommandData[]
				{
					Singleton<Game>.Instance.Environment
				});
			}
			this._eventStoryPackData.onComplete = delegate()
			{
				this.EndMerchantADV(id);
			};
			this._eventStoryPackData.Init();
			Transform transform = this._commandBasePoint ?? base.transform;
			GameObject gameObject = base.transform.FindLoop(EventPoint.PlayerPointName);
			this._playerPoint = (((gameObject != null) ? gameObject.transform : null) ?? transform);
			GameObject gameObject2 = base.transform.FindLoop(EventPoint.MerchantPointName);
			this._merchantPoint = (((gameObject2 != null) ? gameObject2.transform : null) ?? transform);
			GameObject gameObject3 = base.transform.FindLoop(EventPoint.RecoverPointName);
			this._recoverPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			EventPoint.SetCurrentPlayerStateName();
			player.PlayerController.ChangeState("Idle");
			MapUIContainer.SetVisibleHUD(false);
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).TakeUntilDestroy(this).TakeUntilDestroy(player).TakeUntilDestroy(merchant).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				if (!Singleton<Manager.Map>.IsInstance() || !Singleton<Manager.Resources>.IsInstance())
				{
					return;
				}
				MapUIContainer.SetVisibleHUD(false);
				this.NormalChangeActorTable.Clear();
				Actor actor = merchant.Partner;
				if (actor != null)
				{
					if (actor is AgentActor)
					{
						AgentActor agentActor = actor as AgentActor;
						agentActor.ChangeBehavior(Desire.ActionType.Idle);
						agentActor.DisableBehavior();
						if (!this.NormalChangeActorTable.ContainsKey(agentActor.InstanceID))
						{
							this.NormalChangeActorTable[agentActor.InstanceID] = agentActor;
						}
						agentActor.Partner = null;
					}
					merchant.Partner = null;
				}
				actor = merchant.CommandPartner;
				if (actor != null)
				{
					if (actor is AgentActor)
					{
						AgentActor agentActor2 = actor as AgentActor;
						agentActor2.ChangeBehavior(Desire.ActionType.Idle);
						agentActor2.DisableBehavior();
						if (!this.NormalChangeActorTable.ContainsKey(agentActor2.InstanceID))
						{
							this.NormalChangeActorTable[agentActor2.InstanceID] = agentActor2;
						}
						agentActor2.CommandPartner = null;
					}
					merchant.CommandPartner = null;
				}
				if (!this.NormalChangeActorTable.IsNullOrEmpty<int, AgentActor>())
				{
					foreach (KeyValuePair<int, AgentActor> keyValuePair in this.NormalChangeActorTable)
					{
						AgentActor value = keyValuePair.Value;
						if (!(value == null))
						{
							value.SetStand(value.Animation.RecoveryPoint, false, 0f, 0);
							value.Animation.RecoveryPoint = null;
							value.Animation.EndIgnoreEvent();
							value.Animation.ResetDefaultAnimatorController();
							NavMeshAgent navMeshAgent = value.NavMeshAgent;
							if (navMeshAgent.isActiveAndEnabled && navMeshAgent.hasPath)
							{
								navMeshAgent.ResetPath();
							}
						}
					}
				}
				if (Singleton<Voice>.IsInstance())
				{
					Singleton<Voice>.Instance.Stop(-90);
				}
				merchant.SetStand(merchant.Animation.RecoveryPoint, false, 0f, 0);
				merchant.Animation.RecoveryPoint = null;
				merchant.Animation.EndIgnoreEvent();
				merchant.Animation.ResetDefaultAnimatorController();
				merchant.ChangeBehavior(Merchant.ActionType.TalkWithPlayer);
				this._merchantBackUp.Set(merchant);
				if (!merchant.ChaControl.visibleAll)
				{
					merchant.ChaControl.visibleAll = true;
				}
				StoryPointEffect storyPointEffect = Singleton<Manager.Map>.Instance.StoryPointEffect;
				if (storyPointEffect != null)
				{
					storyPointEffect.Hide();
				}
				merchant.StopStand();
				merchant.Animation.StopAllAnimCoroutine();
				merchant.Animation.Targets.Clear();
				merchant.Animation.Animator.InterruptMatchTarget(false);
				merchant.Animation.Animator.Play(Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.MerchantIdleState);
				if (merchant.NavMeshAgent.isActiveAndEnabled && (merchant.NavMeshAgent.isOnNavMesh || merchant.NavMeshAgent.isOnOffMeshLink))
				{
					merchant.NavMeshAgent.ResetPath();
				}
				this.HideAgents();
				this.SetPosAndRot(player, this._playerPoint);
				this.SetPosAndRot(merchant, this._merchantPoint);
				merchant.ActivateNavMeshObstacle(this._merchantPoint.position);
				player.CommCompanion = merchant;
				player.PlayerController.ChangeState("Communication");
				Observable.TimerFrame(2, FrameCountType.Update).TakeUntilDestroy(this).TakeUntilDestroy(player).TakeUntilDestroy(merchant).Subscribe(delegate(long _)
				{
					this._prevCameraStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
					player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
					ADV.ChangeADVCamera(merchant);
					merchant.SetLookPtn(1, 3);
					merchant.SetLookTarget(1, 0, player.CameraControl.CameraComponent.transform);
					Observable.EveryUpdate().TakeUntilDestroy(player).Skip(1).SkipWhile((long __) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).Subscribe(delegate(long __)
					{
						Singleton<MapUIContainer>.Instance.OpenADV(this._openData, this._eventStoryPackData);
					});
				});
			});
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x0027EC18 File Offset: 0x0027D018
		private void SetPosAndRot(Actor actor, Transform point)
		{
			if (actor.NavMeshAgent.enabled)
			{
				actor.NavMeshAgent.Warp(point.position);
			}
			else
			{
				actor.Position = point.position;
			}
			actor.Rotation = point.rotation;
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x0027EC64 File Offset: 0x0027D064
		private void HideAgents()
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			ReadOnlyDictionary<int, AgentActor> agentTable = Singleton<Manager.Map>.Instance.AgentTable;
			if (agentTable.IsNullOrEmpty<int, AgentActor>())
			{
				return;
			}
			foreach (KeyValuePair<int, AgentActor> keyValuePair in agentTable)
			{
				AgentActor value = keyValuePair.Value;
				if (!(value == null))
				{
					value.DisableEntity();
				}
			}
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x0027ECF4 File Offset: 0x0027D0F4
		private void ShowAgents()
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			ReadOnlyDictionary<int, AgentActor> agentTable = Singleton<Manager.Map>.Instance.AgentTable;
			if (agentTable.IsNullOrEmpty<int, AgentActor>())
			{
				return;
			}
			foreach (KeyValuePair<int, AgentActor> keyValuePair in agentTable)
			{
				AgentActor value = keyValuePair.Value;
				if (!(value == null))
				{
					value.EnableEntity();
				}
			}
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x0027ED84 File Offset: 0x0027D184
		private void EndMerchantADV(int id)
		{
			this._eventStoryPackData.Release();
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			Manager.Map map = Singleton<Manager.Map>.Instance;
			PlayerActor player = map.Player;
			MerchantActor merchant = map.Merchant;
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				if (!Singleton<Manager.Map>.IsInstance())
				{
					return;
				}
				this.ShowAgents();
				if (!this.NormalChangeActorTable.IsNullOrEmpty<int, AgentActor>())
				{
					foreach (KeyValuePair<int, AgentActor> keyValuePair in this.NormalChangeActorTable)
					{
						AgentActor value = keyValuePair.Value;
						if (!(value == null))
						{
							value.ChangeBehavior(Desire.ActionType.Normal);
						}
					}
					this.NormalChangeActorTable.Clear();
				}
				if (this._merchantBackUp.Visibled != merchant.ChaControl.visibleAll)
				{
					merchant.ChaControl.visibleAll = this._merchantBackUp.Visibled;
				}
				merchant.Position = this._merchantBackUp.Position;
				merchant.Rotation = this._merchantBackUp.Rotation;
				merchant.SetLookPtn(0, 3);
				merchant.SetLookTarget(0, 0, null);
				merchant.ChangeBehavior(merchant.LastNormalMode);
				StoryPointEffect storyPointEffect = Singleton<Manager.Map>.Instance.StoryPointEffect;
				if (storyPointEffect != null)
				{
					storyPointEffect.Show();
				}
				player.PlayerController.ChangeState("Idle");
				if (this._recoverPoint != null)
				{
					this.SetPosAndRot(player, this._recoverPoint);
				}
				player.CameraControl.Mode = CameraMode.Normal;
				player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = this._prevCameraStyle;
				Transform transform = this._recoverPoint ?? player.Locomotor.transform;
				if (player.CameraControl.ShotType == ShotType.PointOfView)
				{
					player.CameraControl.XAxisValue = transform.rotation.eulerAngles.y;
					player.CameraControl.YAxisValue = 0.5f;
				}
				else
				{
					player.CameraControl.XAxisValue = transform.rotation.eulerAngles.y - 5f;
					player.CameraControl.YAxisValue = 0.6f;
				}
				if (EventPoint._commandRefreshEvent != null)
				{
					EventPoint._commandRefreshEvent.OnNext(Unit.Default);
				}
				switch (id)
				{
				case 1:
					this.SetOpenArea(this.OpenAreaID, true);
					Manager.Map.ForcedSetTutorialProgress(18);
					break;
				case 2:
					if (Singleton<Manager.Resources>.IsInstance() && Singleton<Game>.IsInstance())
					{
						List<StuffItem> list;
						if (player != null)
						{
							PlayerData playerData = player.PlayerData;
							list = ((playerData != null) ? playerData.ItemList : null);
						}
						else
						{
							list = null;
						}
						List<StuffItem> list2 = list;
						CommonDefine commonDefine = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.CommonDefine;
						ItemIDKeyPair? itemIDKeyPair;
						if (commonDefine != null)
						{
							CommonDefine.ItemIDDefines itemIDDefine = commonDefine.ItemIDDefine;
							itemIDKeyPair = ((itemIDDefine != null) ? new ItemIDKeyPair?(itemIDDefine.ShansKeyID) : null);
						}
						else
						{
							itemIDKeyPair = null;
						}
						ItemIDKeyPair? itemIDKeyPair2 = itemIDKeyPair;
						if (list2 != null && itemIDKeyPair2 != null)
						{
							ItemIDKeyPair keyID = itemIDKeyPair2.Value;
							if (!list2.Exists((StuffItem x) => x.CategoryID == keyID.categoryID && x.ID == keyID.itemID && 0 < x.Count))
							{
								list2.AddItem(new StuffItem(keyID.categoryID, keyID.itemID, 1));
							}
						}
					}
					this.SetOpenArea(this.OpenAreaID, true);
					Manager.Map.ForcedSetTutorialProgress(21);
					break;
				case 3:
					this.SetDedicatedNumber(2);
					Manager.Map.ForcedSetTutorialProgress(24);
					break;
				case 4:
					this.SetDedicatedNumber(1);
					break;
				}
				Observable.Timer(TimeSpan.FromSeconds(1.0)).Subscribe(delegate(long _)
				{
					if (!Singleton<MapUIContainer>.IsInstance())
					{
						return;
					}
					MapUIContainer.SetVisibleHUD(true);
					MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 2f, true).Subscribe(delegate(Unit __)
					{
						if (map != null && map.Simulator != null)
						{
							map.Simulator.EnabledTimeProgression = true;
						}
						EventPoint.ChangePrevPlayerMode();
						this.AddLog(id);
					});
				});
			});
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x0027EE2C File Offset: 0x0027D22C
		private void AddLog(int advID)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
			List<string> source2;
			if (advID != 1)
			{
				if (advID == 2)
				{
					List<string> source;
					if (eventPointCommandLabelTextTable.TryGetValue(13, out source) && source.InRange(EventPoint.LangIdx))
					{
						MapUIContainer.AddSystemLog(source.GetElement(EventPoint.LangIdx) ?? string.Empty, true);
					}
				}
			}
			else if (eventPointCommandLabelTextTable.TryGetValue(12, out source2) && source2.InRange(EventPoint.LangIdx))
			{
				MapUIContainer.AddSystemLog(source2.GetElement(EventPoint.LangIdx) ?? string.Empty, true);
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06005E9F RID: 24223 RVA: 0x0027EEEB File Offset: 0x0027D2EB
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				return this._labelsList.GetElement(this._labelIndex.Value);
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06005EA0 RID: 24224 RVA: 0x0027EF03 File Offset: 0x0027D303
		public CommandLabel.CommandInfo[] DateLabels
		{
			get
			{
				return this._dateLabelsList.GetElement(this._dateLabelIndex.Value);
			}
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x0027EF1B File Offset: 0x0027D31B
		protected void Labels_Group_00(ref int idx)
		{
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0027EF24 File Offset: 0x0027D324
		protected void Labels_Group_01(ref int idx)
		{
			switch (this._pointID)
			{
			case 0:
			{
				int dedicatedNumber = this.GetDedicatedNumber();
				if (dedicatedNumber == 0)
				{
					idx = ((Manager.Map.GetTotalAgentFlavorAdditionAmount() >= this._flavorBorder) ? 1 : 0);
					if (idx == 1)
					{
						this.SetDedicatedNumber(1);
					}
				}
				else if (dedicatedNumber == 1)
				{
					idx = 1;
				}
				break;
			}
			case 1:
				if (Manager.Map.GetTutorialProgress() == 18)
				{
					idx = 0;
				}
				else
				{
					int dedicatedNumber2 = this.GetDedicatedNumber();
					if (dedicatedNumber2 == 0)
					{
						idx = ((Manager.Map.GetTotalAgentFlavorAdditionAmount() >= this._flavorBorder) ? 1 : 0);
						if (idx == 1)
						{
							this.SetDedicatedNumber(1);
						}
					}
					else if (dedicatedNumber2 == 1)
					{
						idx = 1;
					}
				}
				break;
			case 2:
				if (Manager.Map.GetTutorialProgress() == 21)
				{
					idx = 0;
				}
				else
				{
					switch (this.GetDedicatedNumber())
					{
					case 0:
						idx = ((Manager.Map.GetTotalAgentFlavorAdditionAmount() >= this._flavorBorder) ? 1 : 0);
						if (idx == 1)
						{
							this.SetDedicatedNumber(1);
						}
						break;
					case 1:
						idx = 1;
						break;
					case 2:
						idx = (this.GetTimeObjOpen(0) ? 3 : 2);
						if (idx == 3)
						{
							this.SetDedicatedNumber(3);
						}
						break;
					case 3:
						idx = 3;
						break;
					}
				}
				break;
			case 3:
			{
				EventPoint eventPoint = EventPoint.Get(1, 2);
				if (eventPoint != null)
				{
					idx = ((eventPoint.LabelIndex == 2) ? 1 : 0);
				}
				else
				{
					idx = 0;
				}
				break;
			}
			case 4:
				idx = (this.GetTimeObjOpen(0) ? 1 : 0);
				break;
			case 5:
				idx = 0;
				if (Singleton<Manager.Map>.IsInstance())
				{
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					Actor actor = (!(player != null)) ? null : player.Partner;
					if (player != null && (player.Mode == Desire.ActionType.Date || player.PlayerController.State is Onbu) && actor != null && player.NavMeshAgent.isActiveAndEnabled)
					{
						if (EventPoint._getNavPath == null)
						{
							EventPoint._getNavPath = new NavMeshPath();
						}
						if (player.NavMeshAgent.CalculatePath(actor.Position, EventPoint._getNavPath) && EventPoint._getNavPath.status == NavMeshPathStatus.PathComplete)
						{
							float num = 0f;
							Vector3[] corners = EventPoint._getNavPath.corners;
							for (int i = 0; i < corners.Length - 1; i++)
							{
								num += Vector3.Distance(corners[i], corners[i + 1]);
							}
							idx = ((30f >= num) ? 1 : 0);
						}
					}
				}
				break;
			}
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x0027F22C File Offset: 0x0027D62C
		protected void Labels_Group_02(ref int idx)
		{
			int pointID = this._pointID;
			if (pointID != 0)
			{
				if (pointID != 1)
				{
				}
			}
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x0027F261 File Offset: 0x0027D661
		protected void Date_Labels_Group_00(ref int idx)
		{
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x0027F268 File Offset: 0x0027D668
		protected void Date_Labels_Group_01(ref int idx)
		{
			switch (this._pointID)
			{
			case 2:
				idx = this._labelIndex.Value;
				break;
			case 4:
				idx = this._labelIndex.Value;
				break;
			case 5:
				idx = 0;
				if (Singleton<Manager.Map>.IsInstance())
				{
					PlayerActor player = Singleton<Manager.Map>.Instance.Player;
					Actor actor = (!(player != null)) ? null : player.Partner;
					if (player != null && actor != null && player.NavMeshAgent.isActiveAndEnabled)
					{
						if (EventPoint._getNavPath == null)
						{
							EventPoint._getNavPath = new NavMeshPath();
						}
						if (player.NavMeshAgent.CalculatePath(actor.Position, EventPoint._getNavPath) && EventPoint._getNavPath.status == NavMeshPathStatus.PathComplete)
						{
							float num = 0f;
							Vector3[] corners = EventPoint._getNavPath.corners;
							for (int i = 0; i < corners.Length - 1; i++)
							{
								num += Vector3.Distance(corners[i], corners[i + 1]);
							}
							idx = ((30f >= num) ? 1 : 0);
						}
					}
				}
				break;
			}
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x0027F3C5 File Offset: 0x0027D7C5
		protected void Date_Labels_Group_02(ref int idx)
		{
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06005EA7 RID: 24231 RVA: 0x0027F3CC File Offset: 0x0027D7CC
		protected bool IsNeutral
		{
			get
			{
				if (!Singleton<Manager.Map>.IsInstance())
				{
					return false;
				}
				switch (this._groupID)
				{
				case 0:
					return this.IsNeutral_Group_00();
				case 1:
					return this.IsNeutral_Group_01();
				case 2:
					return this.IsNeutral_Group_02();
				default:
					return false;
				}
			}
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x0027F41C File Offset: 0x0027D81C
		protected bool IsNeutral_Group_00()
		{
			switch (this._pointID)
			{
			case 0:
				return !this.GetOpenArea(0);
			case 1:
				return !this.GetOpenArea(0);
			case 2:
				return this.GetOpenArea(1) && !this.GetOpenArea(3);
			case 3:
				return this.GetOpenArea(2) && !this.GetOpenArea(3);
			case 4:
				return this.GetOpenArea(1) && !EventPoint.GetAgentOpenState(1);
			case 5:
				return this.GetOpenArea(2) && !EventPoint.GetAgentOpenState(2);
			case 6:
				return this.GetOpenArea(6) && !EventPoint.GetAgentOpenState(3);
			default:
				return false;
			}
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x0027F4F0 File Offset: 0x0027D8F0
		protected bool IsNeutral_Group_01()
		{
			switch (this._pointID)
			{
			case 0:
				return !this.GetOpenArea(1);
			case 1:
				return this.GetOpenArea(1) && !this.GetOpenArea(2);
			case 2:
			{
				if (!this.GetOpenArea(2) || this.GetOpenArea(4))
				{
					return false;
				}
				if (this.GetDedicatedNumber() != 3)
				{
					return true;
				}
				PlayerActor player = Manager.Map.GetPlayer();
				IState state = (!(player != null)) ? null : player.PlayerController.State;
				return !(state is Onbu);
			}
			case 3:
				return this.GetOpenArea(2) && !this.GetTimeObjOpen(0);
			case 4:
			{
				if (!this.GetOpenArea(2) || this.GetOpenArea(5))
				{
					return false;
				}
				if (!this.GetTimeObjOpen(0))
				{
					return true;
				}
				PlayerActor player2 = Manager.Map.GetPlayer();
				IState state2 = (!(player2 != null)) ? null : player2.PlayerController.State;
				return !(state2 is Onbu);
			}
			case 5:
				return this.GetOpenArea(5) && !this.GetOpenArea(6);
			case 6:
				return this.GetOpenArea(4) && !EventPoint.IsGameCleared();
			default:
				return false;
			}
		}

		// Token: 0x06005EAA RID: 24234 RVA: 0x0027F668 File Offset: 0x0027DA68
		protected bool IsNeutral_Group_02()
		{
			int pointID = this._pointID;
			if (pointID != 0)
			{
				return pointID == 1 && this.GetOpenArea(4) && this.GetDedicatedNumber() == 0;
			}
			return this.GetDedicatedNumber() == 0;
		}

		// Token: 0x06005EAB RID: 24235 RVA: 0x0027F6B4 File Offset: 0x0027DAB4
		private void InitializeCommandLabels()
		{
			int groupID = this._groupID;
			if (groupID != 0)
			{
				if (groupID != 1)
				{
					if (groupID == 2)
					{
						this.InitializeCommandLabels_Group_02();
					}
				}
				else
				{
					this.InitializeCommandLabels_Group_01();
				}
			}
			else
			{
				this.InitializeCommandLabels_Group_00();
			}
			int groupID2 = this._groupID;
			if (groupID2 != 0)
			{
				if (groupID2 != 1)
				{
					if (groupID2 == 2)
					{
						this.InitializeCommandDate_Labels_Group_02();
					}
				}
				else
				{
					this.InitializeCommandDate_Labels_Group_01();
				}
			}
			else
			{
				this.InitializeCommandDate_Labels_Group_00();
			}
		}

		// Token: 0x06005EAC RID: 24236 RVA: 0x0027F744 File Offset: 0x0027DB44
		private void InitializeCommandLabels_Group_00()
		{
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			Sprite icon2 = null;
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Manager.Map map = Singleton<Manager.Map>.Instance;
			int eventIconID = icon.EventIconID;
			instance.itemIconTables.ActionIconTable.TryGetValue(eventIconID, out icon2);
			Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
			CommonDefine.EventStoryInfoGroup playInfo = Singleton<Manager.Resources>.Instance.CommonDefine.EventStoryInfo;
			switch (this._pointID)
			{
			case 0:
			{
				this.EventType = EventPoint.EventTypes.AreaOpen;
				this.OpenAreaID = 0;
				List<string> source;
				eventPointCommandLabelTextTable.TryGetValue(0, out source);
				CommandLabel.CommandInfo[] item = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.PopupWarning(Popup.Warning.Type.NotOpenThisSide);
						}
					}
				};
				this._labelsList.Add(item);
				break;
			}
			case 1:
			{
				this.EventType = EventPoint.EventTypes.AreaOpen;
				this.OpenAreaID = 0;
				List<string> source2;
				eventPointCommandLabelTextTable.TryGetValue(0, out source2);
				List<string> list;
				eventPointCommandLabelTextTable.TryGetValue(4, out list);
				CommandLabel.CommandInfo[] item2 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source2.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = true,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							System.Action submitAction = delegate()
							{
								if (playInfo == null)
								{
									return;
								}
								PlayerActor player = map.Player;
								EventPoint.OpenEventStart(player, playInfo.StartEventFadeTime, playInfo.EndEventFadeTime, playInfo.JunkRoad.SEID, playInfo.JunkRoad.SoundPlayDelayTime, playInfo.JunkRoad.EndIntervalTime, delegate
								{
									this.SetOpenArea(true);
								}, delegate
								{
									EventPoint.ChangePrevPlayerMode();
								});
							};
							this.StartMessageDialogDisplay(0, submitAction);
						}
					}
				};
				this._labelsList.Add(item2);
				break;
			}
			case 2:
			{
				this.EventType = EventPoint.EventTypes.AreaOpen;
				this.OpenAreaID = 3;
				List<string> source3;
				eventPointCommandLabelTextTable.TryGetValue(0, out source3);
				CommandLabel.CommandInfo[] item3 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source3.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.PopupWarning(Popup.Warning.Type.NotOpenThisSide);
						}
					}
				};
				this._labelsList.Add(item3);
				break;
			}
			case 3:
			{
				this.EventType = EventPoint.EventTypes.AreaOpen;
				this.OpenAreaID = 3;
				List<string> source4;
				eventPointCommandLabelTextTable.TryGetValue(0, out source4);
				List<string> list2;
				eventPointCommandLabelTextTable.TryGetValue(5, out list2);
				CommandLabel.CommandInfo[] item4 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source4.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							System.Action submitAction = delegate()
							{
								if (playInfo == null)
								{
									return;
								}
								PlayerActor player = map.Player;
								EventPoint.OpenEventStart(player, playInfo.StartEventFadeTime, playInfo.EndEventFadeTime, playInfo.FenceDoor.SEID, playInfo.FenceDoor.SoundPlayDelayTime, playInfo.FenceDoor.EndIntervalTime, delegate
								{
									this.SetOpenArea(true);
								}, delegate
								{
									EventPoint.ChangePrevPlayerMode();
								});
							};
							this.StartMessageDialogDisplay(1, submitAction);
						}
					}
				};
				this._labelsList.Add(item4);
				break;
			}
			case 4:
			case 5:
			case 6:
			{
				this.EventType = EventPoint.EventTypes.Request;
				List<string> source5;
				eventPointCommandLabelTextTable.TryGetValue(0, out source5);
				CommandLabel.CommandInfo[] item5 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source5.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.ChangeRequestEventMode(2, 14);
						}
					}
				};
				this._labelsList.Add(item5);
				break;
			}
			}
		}

		// Token: 0x06005EAD RID: 24237 RVA: 0x0027FB50 File Offset: 0x0027DF50
		private void InitializeCommandLabels_Group_01()
		{
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			Sprite icon2 = null;
			Sprite icon3 = null;
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Manager.Map map = Singleton<Manager.Map>.Instance;
			instance.itemIconTables.ActionIconTable.TryGetValue(icon.EventIconID, out icon3);
			instance.itemIconTables.ActionIconTable.TryGetValue(icon.StoryIconID, out icon2);
			Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
			switch (this._pointID)
			{
			case 0:
			{
				this.OpenAreaID = 1;
				List<string> source;
				eventPointCommandLabelTextTable.TryGetValue(0, out source);
				int requestID = 4;
				this._flavorBorder = this.GetNeedFlavorAdditionAmount(requestID);
				CommandLabel.CommandInfo[] item = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.ChangeRequestEventMode(requestID, -1);
						}
					}
				};
				CommandLabel.CommandInfo[] item2 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.StartMerchantADV(1);
						}
					}
				};
				this._labelsList.Add(item);
				this._labelsList.Add(item2);
				break;
			}
			case 1:
			{
				List<string> source2;
				eventPointCommandLabelTextTable.TryGetValue(0, out source2);
				this.OpenAreaID = 2;
				int requestID = 5;
				this._flavorBorder = this.GetNeedFlavorAdditionAmount(requestID);
				CommandLabel.CommandInfo[] item3 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source2.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.ChangeRequestEventMode(requestID, -1);
							if (Manager.Map.GetTutorialProgress() == 18)
							{
								if (Manager.Map.GetTotalAgentFlavorAdditionAmount() < this._flavorBorder)
								{
									Manager.Map.ForcedSetTutorialProgress(19);
								}
								else
								{
									Manager.Map.ForcedSetTutorialProgress(20);
								}
							}
						}
					}
				};
				CommandLabel.CommandInfo[] item4 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source2.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.StartMerchantADV(2);
						}
					}
				};
				this._labelsList.Add(item3);
				this._labelsList.Add(item4);
				break;
			}
			case 2:
			{
				List<string> source3;
				eventPointCommandLabelTextTable.TryGetValue(0, out source3);
				this.OpenAreaID = 4;
				int requestID = 6;
				this._flavorBorder = this.GetNeedFlavorAdditionAmount(requestID);
				CommandLabel.CommandInfo[] item5 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source3.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.ChangeRequestEventMode(requestID, -1);
							if (Manager.Map.GetTutorialProgress() == 21)
							{
								if (Manager.Map.GetTotalAgentFlavorAdditionAmount() < this._flavorBorder)
								{
									Manager.Map.ForcedSetTutorialProgress(22);
								}
								else
								{
									Manager.Map.ForcedSetTutorialProgress(23);
								}
							}
						}
					}
				};
				CommandLabel.CommandInfo[] item6 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source3.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.StartMerchantADV(3);
						}
					}
				};
				CommandLabel.CommandInfo[] item7 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source3.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.PopupWarning(Popup.Warning.Type.CanOpenWithElec);
						}
					}
				};
				CommandLabel.CommandInfo[] item8 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source3.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.RemoveConsiderationCommand();
							map.Player.CurrentEventPoint = this;
							map.Player.PlayerController.ChangeState("OpenHarbordoor");
						}
					}
				};
				this._labelsList.Add(item5);
				this._labelsList.Add(item6);
				this._labelsList.Add(item7);
				this._labelsList.Add(item8);
				break;
			}
			case 3:
			{
				this.OpenAreaID = 4;
				List<string> source4;
				eventPointCommandLabelTextTable.TryGetValue(0, out source4);
				CommandLabel.CommandInfo[] item9 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source4.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.PopupWarning(Popup.Warning.Type.IsBroken);
						}
					}
				};
				CommandLabel.CommandInfo[] item10 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source4.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.ChangeRequestEventMode(0, -1);
						}
					}
				};
				this._labelsList.Add(item9);
				this._labelsList.Add(item10);
				break;
			}
			case 4:
			{
				this.OpenAreaID = 5;
				List<string> source5;
				eventPointCommandLabelTextTable.TryGetValue(0, out source5);
				CommandLabel.CommandInfo[] item11 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source5.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon3,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.PopupWarning(Popup.Warning.Type.CanOpenWithElec);
						}
					}
				};
				CommandLabel.CommandInfo[] item12 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source5.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon3,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.RemoveConsiderationCommand();
							map.Player.CurrentEventPoint = this;
							map.Player.PlayerController.ChangeState("OpenHarbordoor");
						}
					}
				};
				this._labelsList.Add(item11);
				this._labelsList.Add(item12);
				break;
			}
			case 5:
			{
				this.OpenAreaID = 6;
				List<string> source6;
				eventPointCommandLabelTextTable.TryGetValue(0, out source6);
				CommonDefine.EventStoryInfoGroup playInfo = instance.CommonDefine.EventStoryInfo;
				CommandLabel.CommandInfo[] item13 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source6.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon3,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.PopupWarning(Popup.Warning.Type.DontReactAlone);
						}
					}
				};
				CommandLabel.CommandInfo[] item14 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source6.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon3,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							System.Action submitAction = delegate()
							{
								if (playInfo == null)
								{
									return;
								}
								PlayerActor player = map.Player;
								EventPoint.OpenEventStart(player, playInfo.StartEventFadeTime, playInfo.EndEventFadeTime, playInfo.AutomaticDoor.SEID, playInfo.AutomaticDoor.SoundPlayDelayTime, playInfo.AutomaticDoor.EndIntervalTime, delegate
								{
									this.SetOpenArea(true);
								}, delegate
								{
									EventPoint.ChangePrevPlayerMode();
								});
							};
							this.StartMessageDialogDisplay(2, submitAction);
						}
					}
				};
				this._labelsList.Add(item13);
				this._labelsList.Add(item14);
				break;
			}
			case 6:
			{
				List<string> source7;
				eventPointCommandLabelTextTable.TryGetValue(0, out source7);
				CommandLabel.CommandInfo[] item15 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source7.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.ChangeRequestEventMode(1, -1);
							if (Manager.Map.GetTutorialProgress() == 26)
							{
								Manager.Map.ForcedSetTutorialProgress(27);
							}
						}
					}
				};
				this._labelsList.Add(item15);
				break;
			}
			}
		}

		// Token: 0x06005EAE RID: 24238 RVA: 0x00280561 File Offset: 0x0027E961
		private void InitializeCommandLabels_Group_02()
		{
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x00280568 File Offset: 0x0027E968
		private void InitializeCommandDate_Labels_Group_00()
		{
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x00280570 File Offset: 0x0027E970
		private void InitializeCommandDate_Labels_Group_01()
		{
			Sprite icon = null;
			Sprite icon2 = null;
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Manager.Map map = Singleton<Manager.Map>.Instance;
			CommonDefine.CommonIconGroup icon3 = instance.CommonDefine.Icon;
			instance.itemIconTables.ActionIconTable.TryGetValue(icon3.EventIconID, out icon);
			instance.itemIconTables.ActionIconTable.TryGetValue(icon3.StoryIconID, out icon2);
			Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
			CommonDefine.EventStoryInfoGroup playInfo = instance.CommonDefine.EventStoryInfo;
			int pointID = this._pointID;
			if (pointID != 2)
			{
				if (pointID != 4)
				{
					if (pointID == 5)
					{
						List<string> source;
						eventPointCommandLabelTextTable.TryGetValue(0, out source);
						CommandLabel.CommandInfo[] item = new CommandLabel.CommandInfo[]
						{
							new CommandLabel.CommandInfo
							{
								Text = (source.GetElement(EventPoint.LangIdx) ?? string.Empty),
								Icon = icon,
								IsHold = this._isHold,
								TargetSpriteInfo = icon3.ActionSpriteInfo,
								Transform = this.LabelPoint,
								Condition = null,
								Event = delegate
								{
									this.PopupWarning(Popup.Warning.Type.DontReactAlone);
								}
							}
						};
						CommandLabel.CommandInfo[] item2 = new CommandLabel.CommandInfo[]
						{
							new CommandLabel.CommandInfo
							{
								Text = (source.GetElement(EventPoint.LangIdx) ?? string.Empty),
								Icon = icon,
								IsHold = this._isHold,
								TargetSpriteInfo = icon3.ActionSpriteInfo,
								Transform = this.LabelPoint,
								Condition = null,
								Event = delegate
								{
									System.Action submitAction = delegate()
									{
										if (playInfo == null)
										{
											return;
										}
										PlayerActor player = map.Player;
										EventPoint.OpenEventStart(player, playInfo.StartEventFadeTime, playInfo.EndEventFadeTime, playInfo.AutomaticDoor.SEID, playInfo.AutomaticDoor.SoundPlayDelayTime, playInfo.AutomaticDoor.EndIntervalTime, delegate
										{
											this.SetOpenArea(true);
										}, delegate
										{
											EventPoint.ChangePrevPlayerMode();
										});
									};
									this.StartMessageDialogDisplay(2, submitAction);
								}
							}
						};
						this._dateLabelsList.Add(item);
						this._dateLabelsList.Add(item2);
					}
				}
				else
				{
					List<string> source2;
					eventPointCommandLabelTextTable.TryGetValue(0, out source2);
					CommandLabel.CommandInfo[] item3 = new CommandLabel.CommandInfo[]
					{
						new CommandLabel.CommandInfo
						{
							Text = (source2.GetElement(EventPoint.LangIdx) ?? string.Empty),
							Icon = icon,
							IsHold = this._isHold,
							TargetSpriteInfo = icon3.ActionSpriteInfo,
							Transform = this.LabelPoint,
							Condition = null,
							Event = delegate
							{
								this.PopupWarning(Popup.Warning.Type.CanOpenWithElec);
							}
						}
					};
					CommandLabel.CommandInfo[] item4 = new CommandLabel.CommandInfo[]
					{
						new CommandLabel.CommandInfo
						{
							Text = (source2.GetElement(EventPoint.LangIdx) ?? string.Empty),
							Icon = icon,
							IsHold = this._isHold,
							TargetSpriteInfo = icon3.ActionSpriteInfo,
							Transform = this.LabelPoint,
							Condition = null,
							Event = delegate
							{
								this.RemoveConsiderationCommand();
								map.Player.CurrentEventPoint = this;
								map.Player.PlayerController.ChangeState("DateOpenHarbordoor");
							}
						}
					};
					this._dateLabelsList.Add(item3);
					this._dateLabelsList.Add(item4);
				}
			}
			else
			{
				List<string> source3;
				eventPointCommandLabelTextTable.TryGetValue(0, out source3);
				CommandLabel.CommandInfo[] item5 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source3.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon3.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.PopupWarning(Popup.Warning.Type.CanOpenWithElec);
						}
					}
				};
				CommandLabel.CommandInfo[] item6 = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = (source3.GetElement(EventPoint.LangIdx) ?? string.Empty),
						Icon = icon2,
						IsHold = this._isHold,
						TargetSpriteInfo = icon3.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							this.RemoveConsiderationCommand();
							map.Player.CurrentEventPoint = this;
							map.Player.PlayerController.ChangeState("DateOpenHarbordoor");
						}
					}
				};
				this._dateLabelsList.Add(null);
				this._dateLabelsList.Add(null);
				this._dateLabelsList.Add(item5);
				this._dateLabelsList.Add(item6);
			}
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x002809AD File Offset: 0x0027EDAD
		private void InitializeCommandDate_Labels_Group_02()
		{
		}

		// Token: 0x0400542E RID: 21550
		public static readonly string PlayerPointName = "player_point";

		// Token: 0x0400542F RID: 21551
		public static readonly string MerchantPointName = "merchant_point";

		// Token: 0x04005430 RID: 21552
		public static readonly string HeroinePointName = "heroine_point";

		// Token: 0x04005431 RID: 21553
		public static readonly string RecoverPointName = "recover_point";

		// Token: 0x04005432 RID: 21554
		public static readonly string SEPointName = "se_point";

		// Token: 0x04005433 RID: 21555
		public static readonly string AnimStartPointName = "anim_start_point";

		// Token: 0x04005434 RID: 21556
		public static readonly string AnimEndPointName = "anim_end_point";

		// Token: 0x04005435 RID: 21557
		[SerializeField]
		private int _groupID;

		// Token: 0x04005436 RID: 21558
		[SerializeField]
		private int _pointID = -1;

		// Token: 0x04005437 RID: 21559
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x04005438 RID: 21560
		[SerializeField]
		private Transform _labelPoint;

		// Token: 0x04005439 RID: 21561
		[SerializeField]
		private CommandType _commandType;

		// Token: 0x0400543A RID: 21562
		[SerializeField]
		private bool _enableRangeCheck = true;

		// Token: 0x0400543B RID: 21563
		[SerializeField]
		private float _checkRadius = 1f;

		// Token: 0x04005440 RID: 21568
		private static Subject<Unit> _commandRefreshEvent = null;

		// Token: 0x04005445 RID: 21573
		private bool _isHold = true;

		// Token: 0x04005446 RID: 21574
		private int? _instanceID;

		// Token: 0x04005447 RID: 21575
		private NavMeshPath _pathForCalc;

		// Token: 0x0400544B RID: 21579
		private List<CommandLabel.CommandInfo[]> _labelsList = new List<CommandLabel.CommandInfo[]>();

		// Token: 0x0400544C RID: 21580
		private List<CommandLabel.CommandInfo[]> _dateLabelsList = new List<CommandLabel.CommandInfo[]>();

		// Token: 0x0400544D RID: 21581
		protected IntReactiveProperty _labelIndex = new IntReactiveProperty(0);

		// Token: 0x0400544E RID: 21582
		protected IntReactiveProperty _dateLabelIndex = new IntReactiveProperty(0);

		// Token: 0x0400544F RID: 21583
		private static bool _changeNoneMode = false;

		// Token: 0x04005450 RID: 21584
		private int _flavorBorder;

		// Token: 0x04005451 RID: 21585
		private static Dictionary<int, Dictionary<int, EventPoint>> _eventPointTable = new Dictionary<int, Dictionary<int, EventPoint>>();

		// Token: 0x04005456 RID: 21590
		protected List<GameObject> _safeguardObjects;

		// Token: 0x04005457 RID: 21591
		protected bool _safeguardObjectFlag;

		// Token: 0x04005458 RID: 21592
		private EventPoint.BackUpInfo _merchantBackUp = new EventPoint.BackUpInfo();

		// Token: 0x04005459 RID: 21593
		private Dictionary<int, EventPoint.BackUpInfo> _agentBackUpTable = new Dictionary<int, EventPoint.BackUpInfo>();

		// Token: 0x0400545A RID: 21594
		private Transform _playerPoint;

		// Token: 0x0400545B RID: 21595
		private Transform _merchantPoint;

		// Token: 0x0400545C RID: 21596
		private Transform _recoverPoint;

		// Token: 0x0400545D RID: 21597
		private CinemachineBlendDefinition.Style _prevCameraStyle;

		// Token: 0x0400545E RID: 21598
		private Dictionary<int, AgentActor> _normalChangeActorTable;

		// Token: 0x04005460 RID: 21600
		private static NavMeshPath _getNavPath = null;

		// Token: 0x02000C02 RID: 3074
		private class PackData : CharaPackData
		{
		}

		// Token: 0x02000C03 RID: 3075
		private class MessagePackData : CharaPackData
		{
		}

		// Token: 0x02000C04 RID: 3076
		private class MessageCommandData : ADV.ICommandData
		{
			// Token: 0x1700124B RID: 4683
			// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x00280CF7 File Offset: 0x0027F0F7
			// (set) Token: 0x06005EC4 RID: 24260 RVA: 0x00280CFF File Offset: 0x0027F0FF
			public bool SendFlag { get; set; }

			// Token: 0x1700124C RID: 4684
			// (get) Token: 0x06005EC5 RID: 24261 RVA: 0x00280D08 File Offset: 0x0027F108
			// (set) Token: 0x06005EC6 RID: 24262 RVA: 0x00280D10 File Offset: 0x0027F110
			public Action<bool> SendAction { get; set; }

			// Token: 0x06005EC7 RID: 24263 RVA: 0x00280D1C File Offset: 0x0027F11C
			public IEnumerable<CommandData> CreateCommandData(string head)
			{
				return new List<CommandData>
				{
					new CommandData(CommandData.Command.BOOL, head + "SendFlag", () => this.SendFlag, delegate(object o)
					{
						Action<bool> sendAction = this.SendAction;
						if (sendAction != null)
						{
							sendAction((bool)o);
						}
					})
				};
			}
		}

		// Token: 0x02000C05 RID: 3077
		public enum EventTypes
		{
			// Token: 0x0400546A RID: 21610
			Request,
			// Token: 0x0400546B RID: 21611
			Warning,
			// Token: 0x0400546C RID: 21612
			AreaOpen,
			// Token: 0x0400546D RID: 21613
			Other
		}

		// Token: 0x02000C06 RID: 3078
		public class BackUpInfo
		{
			// Token: 0x06005ECA RID: 24266 RVA: 0x00280D88 File Offset: 0x0027F188
			public BackUpInfo()
			{
				this.Set();
			}

			// Token: 0x06005ECB RID: 24267 RVA: 0x00280DE0 File Offset: 0x0027F1E0
			public BackUpInfo(Actor actor)
			{
				this.Set(actor);
			}

			// Token: 0x1700124D RID: 4685
			// (get) Token: 0x06005ECC RID: 24268 RVA: 0x00280E39 File Offset: 0x0027F239
			// (set) Token: 0x06005ECD RID: 24269 RVA: 0x00280E41 File Offset: 0x0027F241
			public Actor Actor { get; set; }

			// Token: 0x1700124E RID: 4686
			// (get) Token: 0x06005ECE RID: 24270 RVA: 0x00280E4A File Offset: 0x0027F24A
			// (set) Token: 0x06005ECF RID: 24271 RVA: 0x00280E52 File Offset: 0x0027F252
			public bool Visibled { get; set; } = true;

			// Token: 0x1700124F RID: 4687
			// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x00280E5B File Offset: 0x0027F25B
			// (set) Token: 0x06005ED1 RID: 24273 RVA: 0x00280E63 File Offset: 0x0027F263
			public Vector3 Position { get; set; } = Vector3.zero;

			// Token: 0x17001250 RID: 4688
			// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x00280E6C File Offset: 0x0027F26C
			// (set) Token: 0x06005ED3 RID: 24275 RVA: 0x00280E74 File Offset: 0x0027F274
			public Quaternion Rotation { get; set; } = Quaternion.identity;

			// Token: 0x17001251 RID: 4689
			// (get) Token: 0x06005ED4 RID: 24276 RVA: 0x00280E7D File Offset: 0x0027F27D
			// (set) Token: 0x06005ED5 RID: 24277 RVA: 0x00280E85 File Offset: 0x0027F285
			public bool ControllerEnabled { get; set; } = true;

			// Token: 0x17001252 RID: 4690
			// (get) Token: 0x06005ED6 RID: 24278 RVA: 0x00280E8E File Offset: 0x0027F28E
			// (set) Token: 0x06005ED7 RID: 24279 RVA: 0x00280E96 File Offset: 0x0027F296
			public bool AnimationEnabled { get; set; } = true;

			// Token: 0x17001253 RID: 4691
			// (get) Token: 0x06005ED8 RID: 24280 RVA: 0x00280E9F File Offset: 0x0027F29F
			// (set) Token: 0x06005ED9 RID: 24281 RVA: 0x00280EA7 File Offset: 0x0027F2A7
			public UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType> ModeCache { get; protected set; } = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType>(Desire.ActionType.Idle, Desire.ActionType.Idle);

			// Token: 0x17001254 RID: 4692
			// (get) Token: 0x06005EDA RID: 24282 RVA: 0x00280EB0 File Offset: 0x0027F2B0
			// (set) Token: 0x06005EDB RID: 24283 RVA: 0x00280EB8 File Offset: 0x0027F2B8
			public bool BehaviorEnabled { get; set; } = true;

			// Token: 0x06005EDC RID: 24284 RVA: 0x00280EC4 File Offset: 0x0027F2C4
			public void Set()
			{
				if (this.Actor == null)
				{
					this.Visibled = true;
					this.Position = Vector3.zero;
					this.Rotation = Quaternion.identity;
					this.ControllerEnabled = true;
					this.AnimationEnabled = true;
					this.BehaviorEnabled = true;
					this.ModeCache = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType>(Desire.ActionType.Idle, Desire.ActionType.Idle);
				}
				else
				{
					this.Visibled = this.Actor.ChaControl.visibleAll;
					this.Position = this.Actor.Position;
					this.Rotation = this.Actor.Rotation;
					this.ControllerEnabled = this.Actor.Controller.enabled;
					this.AnimationEnabled = this.Actor.Animation.enabled;
					if (this.IsAgent)
					{
						AgentActor agent = this.Agent;
						this.ModeCache = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType>(agent.Mode, agent.BehaviorResources.Mode);
						this.BehaviorEnabled = agent.BehaviorResources.enabled;
					}
					else
					{
						this.ModeCache = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType>(Desire.ActionType.Idle, Desire.ActionType.Idle);
						this.BehaviorEnabled = true;
					}
				}
			}

			// Token: 0x06005EDD RID: 24285 RVA: 0x00280FE4 File Offset: 0x0027F3E4
			public void Set(Actor actor)
			{
				this.Actor = actor;
				if (actor == null)
				{
					this.Visibled = false;
					this.Position = Vector3.zero;
					this.Rotation = Quaternion.identity;
					this.ControllerEnabled = false;
					this.AnimationEnabled = false;
					this.BehaviorEnabled = false;
					this.ModeCache = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType>(Desire.ActionType.Idle, Desire.ActionType.Idle);
				}
				else
				{
					this.Visibled = (!(this.Actor.ChaControl != null) || this.Actor.ChaControl.visibleAll);
					if (this.Actor is PlayerActor)
					{
						PlayerActor playerActor = this.Actor as PlayerActor;
						PlayerData playerData = playerActor.PlayerData;
						if (playerData != null)
						{
							this.Position = playerData.Position;
							this.Rotation = playerData.Rotation;
						}
						else
						{
							this.Position = playerActor.Position;
							this.Rotation = playerActor.Rotation;
						}
					}
					else if (this.Actor is AgentActor)
					{
						AgentActor agentActor = this.Actor as AgentActor;
						AgentData agentData = agentActor.AgentData;
						if (agentData != null)
						{
							this.Position = agentData.Position;
							this.Rotation = agentData.Rotation;
						}
						else
						{
							this.Position = agentActor.Position;
							this.Rotation = agentActor.Rotation;
						}
					}
					else if (this.Actor is MerchantActor)
					{
						MerchantActor merchantActor = this.Actor as MerchantActor;
						MerchantData merchantData = merchantActor.MerchantData;
						if (merchantData != null)
						{
							this.Position = merchantData.Position;
							this.Rotation = merchantData.Rotation;
						}
						else
						{
							this.Position = merchantActor.Position;
							this.Rotation = merchantActor.Rotation;
						}
					}
					this.ControllerEnabled = (!(this.Actor.Controller != null) || this.Actor.Controller.enabled);
					this.AnimationEnabled = (!(this.Actor.Animation != null) || this.Actor.Animation.enabled);
					if (this.IsAgent)
					{
						AgentActor agent = this.Agent;
						this.ModeCache = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType>(agent.Mode, agent.BehaviorResources.Mode);
						this.BehaviorEnabled = (!(agent.BehaviorResources != null) || agent.BehaviorResources.enabled);
					}
					else
					{
						this.ModeCache = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType>(Desire.ActionType.Idle, Desire.ActionType.Idle);
						this.BehaviorEnabled = true;
					}
				}
			}

			// Token: 0x06005EDE RID: 24286 RVA: 0x0028127E File Offset: 0x0027F67E
			public void Clear()
			{
				this.Actor = null;
				this.Visibled = true;
				this.Position = Vector3.zero;
				this.Rotation = Quaternion.identity;
				this.ControllerEnabled = true;
				this.AnimationEnabled = true;
			}

			// Token: 0x17001255 RID: 4693
			// (get) Token: 0x06005EDF RID: 24287 RVA: 0x002812B2 File Offset: 0x0027F6B2
			public PlayerActor Player
			{
				[CompilerGenerated]
				get
				{
					return this.Actor as PlayerActor;
				}
			}

			// Token: 0x17001256 RID: 4694
			// (get) Token: 0x06005EE0 RID: 24288 RVA: 0x002812BF File Offset: 0x0027F6BF
			public AgentActor Agent
			{
				[CompilerGenerated]
				get
				{
					return this.Actor as AgentActor;
				}
			}

			// Token: 0x17001257 RID: 4695
			// (get) Token: 0x06005EE1 RID: 24289 RVA: 0x002812CC File Offset: 0x0027F6CC
			public MerchantActor Merchant
			{
				[CompilerGenerated]
				get
				{
					return this.Actor as MerchantActor;
				}
			}

			// Token: 0x17001258 RID: 4696
			// (get) Token: 0x06005EE2 RID: 24290 RVA: 0x002812D9 File Offset: 0x0027F6D9
			public bool IsEmpty
			{
				[CompilerGenerated]
				get
				{
					return this.Actor == null;
				}
			}

			// Token: 0x17001259 RID: 4697
			// (get) Token: 0x06005EE3 RID: 24291 RVA: 0x002812E7 File Offset: 0x0027F6E7
			public bool IsPlayer
			{
				[CompilerGenerated]
				get
				{
					return this.Actor is PlayerActor;
				}
			}

			// Token: 0x1700125A RID: 4698
			// (get) Token: 0x06005EE4 RID: 24292 RVA: 0x002812F7 File Offset: 0x0027F6F7
			public bool IsAgent
			{
				[CompilerGenerated]
				get
				{
					return this.Actor is AgentActor;
				}
			}

			// Token: 0x1700125B RID: 4699
			// (get) Token: 0x06005EE5 RID: 24293 RVA: 0x00281307 File Offset: 0x0027F707
			public bool IsMerchant
			{
				[CompilerGenerated]
				get
				{
					return this.Actor is MerchantActor;
				}
			}
		}
	}
}
