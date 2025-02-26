using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Player;
using AIProject.SaveData;
using Housing;
using IllusionUtility.GetUtility;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C10 RID: 3088
	public class LightSwitchPoint : Point, ICommandable
	{
		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x00284A6B File Offset: 0x00282E6B
		// (set) Token: 0x06005F79 RID: 24441 RVA: 0x00284A73 File Offset: 0x00282E73
		public override int RegisterID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06005F7A RID: 24442 RVA: 0x00284A7C File Offset: 0x00282E7C
		public bool IsLinkedSE
		{
			[CompilerGenerated]
			get
			{
				return this._useEnv3D && this._env3DSEPoint != null;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06005F7B RID: 24443 RVA: 0x00284A98 File Offset: 0x00282E98
		public Transform CommandBasePoint
		{
			get
			{
				if (this._commandBasePoint != null)
				{
					return this._commandBasePoint;
				}
				if (base.transform != null)
				{
					return base.transform;
				}
				return null;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x00284ACC File Offset: 0x00282ECC
		public Vector3 CommandBasePosition
		{
			get
			{
				Transform commandBasePoint = this.CommandBasePoint;
				return (!(commandBasePoint != null)) ? Vector3.zero : commandBasePoint.position;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06005F7D RID: 24445 RVA: 0x00284AFC File Offset: 0x00282EFC
		public Transform LabelPoint
		{
			get
			{
				return (!(this._labelPoint != null)) ? base.transform : this._labelPoint;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06005F7E RID: 24446 RVA: 0x00284B20 File Offset: 0x00282F20
		// (set) Token: 0x06005F7F RID: 24447 RVA: 0x00284B28 File Offset: 0x00282F28
		public bool IsHousingItem { get; private set; }

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06005F80 RID: 24448 RVA: 0x00284B31 File Offset: 0x00282F31
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

		// Token: 0x06005F81 RID: 24449 RVA: 0x00284B6C File Offset: 0x00282F6C
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			CommandType commandType = this.CommandType;
			bool flag;
			if (commandType != CommandType.Forward)
			{
				flag = (distance <= this._rangeRadius + ((!this.IsHousingItem) ? 0f : radiusB));
			}
			else
			{
				float num = this._rangeRadius + ((!this.IsHousingItem) ? 0f : radiusA);
				if (num < distance)
				{
					return false;
				}
				Vector3 commandBasePosition = this.CommandBasePosition;
				commandBasePosition.y = 0f;
				float num2 = angle / 2f;
				float num3 = Vector3.Angle(commandBasePosition - basePosition, forward);
				flag = (num3 <= num2);
			}
			if (!flag)
			{
				return false;
			}
			PlayerActor player = Map.GetPlayer();
			if (player != null)
			{
				IState state = player.PlayerController.State;
				if (state is Onbu)
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x00284C60 File Offset: 0x00283060
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool result;
			if (nmAgent.isActiveAndEnabled)
			{
				float num = Mathf.Abs(nmAgent.transform.position.y - this.CommandBasePosition.y);
				result = (num <= this._height / 2f);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06005F83 RID: 24451 RVA: 0x00284CD3 File Offset: 0x002830D3
		public bool IsImpossible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x00284CD6 File Offset: 0x002830D6
		public bool SetImpossible(bool value, Actor actor)
		{
			return true;
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06005F85 RID: 24453 RVA: 0x00284CD9 File Offset: 0x002830D9
		public bool IsNeutralCommand
		{
			[CompilerGenerated]
			get
			{
				return true;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x00284CDC File Offset: 0x002830DC
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06005F87 RID: 24455 RVA: 0x00284CEC File Offset: 0x002830EC
		public Vector3 CommandCenter
		{
			get
			{
				Vector3 commandBasePosition = this.CommandBasePosition;
				CommandArea commandArea = Map.GetCommandArea();
				if (commandArea != null && commandArea.BaseTransform != null)
				{
					commandBasePosition.y = commandArea.BaseTransform.position.y;
				}
				return commandBasePosition;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x00284D3E File Offset: 0x0028313E
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				return (!this._lightEnable) ? this._onLabels : this._offLabels;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06005F89 RID: 24457 RVA: 0x00284D5C File Offset: 0x0028315C
		public CommandLabel.CommandInfo[] DateLabels
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06005F8A RID: 24458 RVA: 0x00284D5F File Offset: 0x0028315F
		public ObjectLayer Layer { get; } = 2;

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06005F8B RID: 24459 RVA: 0x00284D67 File Offset: 0x00283167
		public CommandType CommandType
		{
			[CompilerGenerated]
			get
			{
				return this._commandType;
			}
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x00284D70 File Offset: 0x00283170
		private void Awake()
		{
			if (LightSwitchPoint._commandRefreshEvent == null)
			{
				LightSwitchPoint._commandRefreshEvent = new Subject<Unit>();
				(from _ in LightSwitchPoint._commandRefreshEvent.Buffer(LightSwitchPoint._commandRefreshEvent.ThrottleFrame(1, FrameCountType.Update)).TakeUntilDestroy(Singleton<Map>.Instance)
				where !_.IsNullOrEmpty<Unit>()
				select _).Subscribe(delegate(IList<Unit> _)
				{
					CommandArea commandArea = Map.GetCommandArea();
					if (commandArea == null)
					{
						return;
					}
					commandArea.RefreshCommands();
				});
			}
			if (this._linkMapObject)
			{
				LightSwitchData lightSwitchData = LightSwitchData.Get(this._linkID);
				if (lightSwitchData != null)
				{
					this._onModeObjects = lightSwitchData.OnModeObjects;
					this._offModeObjects = lightSwitchData.OffModeObjects;
				}
			}
			this.ActiveChange(this._firstLightEnable);
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x00284E40 File Offset: 0x00283240
		protected override void Start()
		{
			base.Start();
			ItemComponent x = base.GetComponent<ItemComponent>();
			if (x == null)
			{
				x = base.GetComponentInParent<ItemComponent>();
			}
			this.IsHousingItem = (x != null);
			if (Singleton<Manager.Resources>.IsInstance())
			{
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				if (this._commandBasePoint == null)
				{
					GameObject gameObject = base.transform.FindLoop(instance.DefinePack.MapDefines.CommandTargetName);
					Transform transform = (!(gameObject != null)) ? null : gameObject.transform;
					this._commandBasePoint = ((!(transform != null)) ? base.transform : transform);
				}
				if (this._labelPoint == null)
				{
					GameObject gameObject2 = base.transform.FindLoop(instance.DefinePack.MapDefines.LightPointLabelTargetName);
					Transform transform2 = (!(gameObject2 != null)) ? null : gameObject2.transform;
					this._labelPoint = ((!(transform2 != null)) ? base.transform : transform2);
				}
			}
			if (this._commandBasePoint == null)
			{
				this._commandBasePoint = base.transform;
			}
			if (this._labelPoint == null)
			{
				this._labelPoint = base.transform;
			}
			this.InitializeCommandLabels();
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x00284F98 File Offset: 0x00283398
		private void InitializeCommandLabels()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			CommonDefine.CommonIconGroup icon = instance.CommonDefine.Icon;
			int guideCancelID = instance.CommonDefine.Icon.GuideCancelID;
			Sprite icon2;
			instance.itemIconTables.InputIconTable.TryGetValue(guideCancelID, out icon2);
			Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
			int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			List<string> source;
			eventPointCommandLabelTextTable.TryGetValue(15, out source);
			this._onLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					Text = source.GetElement(index),
					Icon = icon2,
					IsHold = true,
					TargetSpriteInfo = icon.ActionSpriteInfo,
					Transform = this._labelPoint,
					Condition = null,
					Event = delegate
					{
						this.Switch(true);
					}
				}
			};
			eventPointCommandLabelTextTable.TryGetValue(16, out source);
			this._offLabels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					Text = source.GetElement(index),
					Icon = icon2,
					IsHold = true,
					TargetSpriteInfo = icon.ActionSpriteInfo,
					Transform = this._labelPoint,
					Condition = null,
					Event = delegate
					{
						this.Switch(false);
					}
				}
			};
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x00285104 File Offset: 0x00283504
		public void Switch(bool active)
		{
			if (this._lightEnable != active)
			{
				this.RefreshCommand();
			}
			this.ActiveChange(active);
			Dictionary<int, bool> dictionary;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				dictionary = ((environment != null) ? environment.LightObjectSwitchStateTable : null);
			}
			else
			{
				dictionary = null;
			}
			Dictionary<int, bool> dictionary2 = dictionary;
			if (dictionary2 != null)
			{
				dictionary2[this.RegisterID] = active;
			}
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x00285168 File Offset: 0x00283568
		private void ActiveChange(bool active)
		{
			this._lightEnable = active;
			if (!this._onModeObjects.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._onModeObjects)
				{
					if (gameObject != null && gameObject.activeSelf != active)
					{
						gameObject.SetActive(active);
					}
				}
			}
			if (!this._offModeObjects.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject2 in this._offModeObjects)
				{
					if (gameObject2 != null && gameObject2.activeSelf == active)
					{
						gameObject2.SetActive(!active);
					}
				}
			}
			if (this.IsLinkedSE)
			{
				bool flag = active == this._lightEnablePlaySE;
				if (flag)
				{
					this._env3DSEPoint.SoundForcedPlay(false);
				}
				else
				{
					this._env3DSEPoint.SoundForcedStop();
				}
			}
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x0028525C File Offset: 0x0028365C
		public bool IsSwitch()
		{
			Dictionary<int, bool> dictionary;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				dictionary = ((environment != null) ? environment.LightObjectSwitchStateTable : null);
			}
			else
			{
				dictionary = null;
			}
			Dictionary<int, bool> dictionary2 = dictionary;
			if (dictionary2.IsNullOrEmpty<int, bool>())
			{
				return this._firstLightEnable;
			}
			bool result;
			if (!dictionary2.TryGetValue(this.RegisterID, out result))
			{
				return this._firstLightEnable;
			}
			return result;
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x002852C0 File Offset: 0x002836C0
		private void RefreshCommand()
		{
			CommandArea commandArea = Map.GetCommandArea();
			if (commandArea == null)
			{
				return;
			}
			if (commandArea.ContainsConsiderationObject(this))
			{
				if (LightSwitchPoint._commandRefreshEvent != null)
				{
					LightSwitchPoint._commandRefreshEvent.OnNext(Unit.Default);
				}
			}
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x00285307 File Offset: 0x00283707
		public bool TutorialHideMode()
		{
			return Map.TutorialMode;
		}

		// Token: 0x040054C0 RID: 21696
		private static Subject<Unit> _commandRefreshEvent;

		// Token: 0x040054C1 RID: 21697
		[SerializeField]
		[DisableInPlayMode]
		private int _id = -1;

		// Token: 0x040054C2 RID: 21698
		[SerializeField]
		private bool _linkMapObject;

		// Token: 0x040054C3 RID: 21699
		[SerializeField]
		private int _linkID;

		// Token: 0x040054C4 RID: 21700
		[SerializeField]
		private GameObject[] _onModeObjects;

		// Token: 0x040054C5 RID: 21701
		[SerializeField]
		private GameObject[] _offModeObjects;

		// Token: 0x040054C6 RID: 21702
		[SerializeField]
		private bool _firstLightEnable;

		// Token: 0x040054C7 RID: 21703
		private bool _lightEnable;

		// Token: 0x040054C8 RID: 21704
		[SerializeField]
		private bool _useEnv3D;

		// Token: 0x040054C9 RID: 21705
		[SerializeField]
		private Env3DSEPoint _env3DSEPoint;

		// Token: 0x040054CA RID: 21706
		[SerializeField]
		private bool _lightEnablePlaySE = true;

		// Token: 0x040054CB RID: 21707
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x040054CC RID: 21708
		[SerializeField]
		private Transform _labelPoint;

		// Token: 0x040054CD RID: 21709
		[SerializeField]
		private float _rangeRadius;

		// Token: 0x040054CE RID: 21710
		[SerializeField]
		private float _height;

		// Token: 0x040054CF RID: 21711
		[SerializeField]
		private CommandType _commandType;

		// Token: 0x040054D1 RID: 21713
		private int? _instanceID;

		// Token: 0x040054D2 RID: 21714
		private NavMeshPath _pathForCalc;

		// Token: 0x040054D3 RID: 21715
		private CommandLabel.CommandInfo[] _onLabels;

		// Token: 0x040054D4 RID: 21716
		private CommandLabel.CommandInfo[] _offLabels;
	}
}
