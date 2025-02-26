using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AIProject.Player;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E18 RID: 3608
	[RequireComponent(typeof(ActorLocomotion))]
	public class PlayerController : ActorController
	{
		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06006FA9 RID: 28585 RVA: 0x002FDD8D File Offset: 0x002FC18D
		// (set) Token: 0x06006FAA RID: 28586 RVA: 0x002FDD95 File Offset: 0x002FC195
		public Transform CameraTransform
		{
			get
			{
				return this._cameraTransform;
			}
			set
			{
				this._cameraTransform = value;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06006FAB RID: 28587 RVA: 0x002FDD9E File Offset: 0x002FC19E
		// (set) Token: 0x06006FAC RID: 28588 RVA: 0x002FDDA6 File Offset: 0x002FC1A6
		public CommandArea CommandArea
		{
			get
			{
				return this._commandArea;
			}
			set
			{
				this._commandArea = value;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06006FAD RID: 28589 RVA: 0x002FDDAF File Offset: 0x002FC1AF
		// (set) Token: 0x06006FAE RID: 28590 RVA: 0x002FDDB7 File Offset: 0x002FC1B7
		public string PrevStateName { get; set; }

		// Token: 0x06006FAF RID: 28591 RVA: 0x002FDDC0 File Offset: 0x002FC1C0
		private void OnEnable()
		{
			if (this._character)
			{
				this._character.enabled = true;
			}
		}

		// Token: 0x06006FB0 RID: 28592 RVA: 0x002FDDE0 File Offset: 0x002FC1E0
		private void OnDisable()
		{
			if (this._character)
			{
				this._character.enabled = false;
			}
			this._actor.StateInfo.Init();
			if (this._cameraTransform)
			{
				Actor.InputInfo stateInfo = this._actor.StateInfo;
				stateInfo.lookPos = base.transform.position + this._cameraTransform.forward * 100f;
				this._actor.StateInfo = stateInfo;
			}
		}

		// Token: 0x06006FB1 RID: 28593 RVA: 0x002FDE70 File Offset: 0x002FC270
		protected override void Start()
		{
			base.Start();
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06006FB2 RID: 28594 RVA: 0x002FDEAB File Offset: 0x002FC2AB
		public override void StartBehavior()
		{
			if (this._cameraTransform == null)
			{
				this._cameraTransform = Camera.main.transform;
			}
			if (this._character == null)
			{
				this._character = base.GetComponent<ActorLocomotionThirdPerson>();
			}
		}

		// Token: 0x06006FB3 RID: 28595 RVA: 0x002FDEEC File Offset: 0x002FC2EC
		private void OnUpdate()
		{
			if (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
			bool? flag = (this._actor != null) ? new bool?(this._actor.IsInit) : null;
			if (flag == null || !flag.Value)
			{
				return;
			}
			Actor.InputInfo stateInfo = this._actor.StateInfo;
			if (this._state != null)
			{
				this._state.Update(this._actor, ref stateInfo);
			}
			this._actor.StateInfo = stateInfo;
			if (this._state != null)
			{
				this._state.AfterUpdate(this._actor, this._actor.StateInfo);
			}
			this._character.Move(Vector3.zero);
			PlayerActor playerActor = this._actor as PlayerActor;
			if (playerActor != null && playerActor.Partner != null && playerActor.Partner.IsSlave)
			{
				playerActor.Partner.Position = playerActor.Position;
				playerActor.Partner.Rotation = playerActor.Rotation;
			}
		}

		// Token: 0x06006FB4 RID: 28596 RVA: 0x002FE01C File Offset: 0x002FC41C
		protected override void SubFixedUpdate()
		{
			if (this._actor == null || this._state == null)
			{
				return;
			}
			Actor.InputInfo stateInfo = this._actor.StateInfo;
			if (this._state != null)
			{
				this._state.FixedUpdate(this._actor, stateInfo);
			}
			this._actor.StateInfo = stateInfo;
		}

		// Token: 0x06006FB5 RID: 28597 RVA: 0x002FE080 File Offset: 0x002FC480
		public void ChangeState(string target, ActionPoint point, Action onCompleted = null)
		{
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			if (this.CommandArea != null)
			{
				this.CommandArea.SequenceSetConsiderations(delegate(ICommandable x)
				{
					this.CleanPoint(x, point);
				});
			}
			this._actor.CurrentPoint = point;
			this.ChangeState(target);
			(this._state as PlayerStateBase).OnCompleted = onCompleted;
		}

		// Token: 0x06006FB6 RID: 28598 RVA: 0x002FE0F8 File Offset: 0x002FC4F8
		private void CleanPoint(ICommandable commandable, ActionPoint point)
		{
			if (!(commandable is ActionPoint))
			{
				return;
			}
			if (commandable as ActionPoint == point)
			{
				return;
			}
			this.CommandArea.RemoveConsiderationObject(commandable);
		}

		// Token: 0x06006FB7 RID: 28599 RVA: 0x002FE124 File Offset: 0x002FC524
		public override void ChangeState(string target)
		{
			if (this.CommandArea != null && target != "Normal" && target != "Onbu" && target != "Houchi")
			{
				this.CommandArea.enabled = false;
			}
			Func<IState> func;
			if (PlayerController._getTypeEventTable.TryGetValue(target, out func))
			{
				IState state = this._state;
				IState state2 = func();
				this._state = state2;
				if (state != null)
				{
					string name = state.GetType().Name;
					this.PrevStateName = name;
					EventType type = (EventType)0;
					PlayerController._statePairedEventTable.TryGetValue(name, out type);
					state.Release(this._actor, type);
					Action onCompleted = (state as PlayerStateBase).OnCompleted;
					if (onCompleted != null)
					{
						onCompleted();
					}
				}
				this._state.Awake(this._actor);
			}
		}

		// Token: 0x06006FB8 RID: 28600 RVA: 0x002FE208 File Offset: 0x002FC608
		// Note: this type is marked as 'beforefieldinit'.
		static PlayerController()
		{
			Dictionary<string, Func<IState>> dictionary = new Dictionary<string, Func<IState>>();
			dictionary["Normal"] = (() => new Normal());
			dictionary["Sleep"] = (() => new Sleep());
			dictionary["Lie"] = (() => new Lie());
			dictionary["Break"] = (() => new Break());
			dictionary["DateSleep"] = (() => new DateSleep());
			dictionary["DateBreak"] = (() => new DateBreak());
			dictionary["DateEat"] = (() => new DateEat());
			dictionary["DateSearch"] = (() => new DateSearch());
			dictionary["DateOpenHarbordoor"] = (() => new DateOpenHarbordoor());
			dictionary["SpecialH"] = (() => new SpecialH());
			dictionary["Search"] = (() => new Search());
			dictionary["Kitchen"] = (() => new Kitchen());
			dictionary["Cook"] = (() => new Cook());
			dictionary["ItemBox"] = (() => new ItemBox());
			dictionary["Pantry"] = (() => new Pantry());
			dictionary["Fishing"] = (() => new Fishing());
			dictionary["Sex"] = (() => new Sex());
			dictionary["Move"] = (() => new Move());
			dictionary["CatchAnimal"] = (() => new CatchAnimal());
			dictionary["Photo"] = (() => new Photo());
			dictionary["Communication"] = (() => new Communication());
			dictionary["Menu"] = (() => new Menu());
			dictionary["WMap"] = (() => new WMap());
			dictionary["Houchi"] = (() => new Houchi());
			dictionary["RequestEvent"] = (() => new RequestEvent());
			dictionary["Tutorial"] = (() => new Tutorial());
			dictionary["OpeningWakeUp"] = (() => new OpeningWakeUp());
			dictionary["Onbu"] = (() => new Onbu());
			dictionary["DoorOpen"] = (() => new DoorOpen());
			dictionary["DoorClose"] = (() => new DoorClose());
			dictionary["OpenHarbordoor"] = (() => new OpenHarbordoor());
			dictionary["BaseMenu"] = (() => new BaseMenu());
			dictionary["Housing"] = (() => new AIProject.Player.Housing());
			dictionary["ChickenCoopMenu"] = (() => new ChickenCoopMenu());
			dictionary["DeviceMenu"] = (() => new DeviceMenu());
			dictionary["CharaEnter"] = (() => new CharaEnter());
			dictionary["CharaChange"] = (() => new CharaChange());
			dictionary["Harvest"] = (() => new Harvest());
			dictionary["Craft"] = (() => new Craft());
			dictionary["ShipMenu"] = (() => new ShipMenu());
			dictionary["DressRoom"] = (() => new DressRoom());
			dictionary["ClothChange"] = (() => new ClothChange());
			dictionary["EntryChara"] = (() => new EntryChara());
			dictionary["EditChara"] = (() => new EditChara());
			dictionary["EditPlayer"] = (() => new EditPlayer());
			dictionary["CharaLookEdit"] = (() => new CharaLookEdit());
			dictionary["PlayerLookEdit"] = (() => new PlayerLookEdit());
			dictionary["CharaMigration"] = (() => new CharaMigration());
			dictionary["Idle"] = (() => new Idle());
			dictionary["Follow"] = (() => new Follow());
			dictionary["ExitEatEvent"] = (() => new ExitEatEvent());
			dictionary["Warp"] = (() => new Warp());
			PlayerController._getTypeEventTable = dictionary;
			Dictionary<string, EventType> dictionary2 = new Dictionary<string, EventType>();
			dictionary2["Sleep"] = EventType.Sleep;
			dictionary2["Lie"] = (EventType)0;
			dictionary2["Break"] = EventType.Break;
			dictionary2["DateSleep"] = EventType.Sleep;
			dictionary2["DateBreak"] = EventType.Break;
			dictionary2["DateEat"] = EventType.Eat;
			dictionary2["DateSearch"] = EventType.Search;
			dictionary2["DateOpenHarbordoor"] = (EventType)0;
			dictionary2["SpecialH"] = (EventType)0;
			dictionary2["Search"] = EventType.Search;
			dictionary2["Kitchen"] = (EventType)0;
			dictionary2["Cook"] = (EventType)0;
			dictionary2["Pantry"] = (EventType)0;
			dictionary2["ItemBox"] = EventType.StorageIn;
			dictionary2["Fishing"] = EventType.Search;
			dictionary2["Move"] = EventType.Move;
			dictionary2["CatchAnimal"] = (EventType)0;
			dictionary2["Photo"] = (EventType)0;
			dictionary2["Communication"] = (EventType)0;
			dictionary2["Menu"] = (EventType)0;
			dictionary2["WMap"] = (EventType)0;
			dictionary2["Houchi"] = (EventType)0;
			dictionary2["RequestEvent"] = (EventType)0;
			dictionary2["Tutorial"] = (EventType)0;
			dictionary2["OpeningWakeUp"] = (EventType)0;
			dictionary2["Onbu"] = (EventType)0;
			dictionary2["DoorOpen"] = (EventType)0;
			dictionary2["DoorClose"] = (EventType)0;
			dictionary2["OpenHarbordoor"] = (EventType)0;
			dictionary2["BaseMenu"] = (EventType)0;
			dictionary2["Housing"] = (EventType)0;
			dictionary2["ChickenCoopMenu"] = (EventType)0;
			dictionary2["DeviceMenu"] = (EventType)0;
			dictionary2["CharaEnter"] = (EventType)0;
			dictionary2["CharaChange"] = (EventType)0;
			dictionary2["Harvest"] = (EventType)0;
			dictionary2["Craft"] = (EventType)0;
			dictionary2["ShipMenu"] = (EventType)0;
			dictionary2["DressRoom"] = (EventType)0;
			dictionary2["ClothChange"] = (EventType)0;
			dictionary2["EntryChara"] = (EventType)0;
			dictionary2["EditChara"] = (EventType)0;
			dictionary2["EditPlayer"] = (EventType)0;
			dictionary2["CharaLookEdit"] = (EventType)0;
			dictionary2["PlayerLookEdit"] = (EventType)0;
			dictionary2["CharaMigration"] = (EventType)0;
			dictionary2["Idle"] = (EventType)0;
			dictionary2["Follow"] = (EventType)0;
			dictionary2["ExitEatEvent"] = (EventType)0;
			dictionary2["Warp"] = (EventType)0;
			PlayerController._statePairedEventTable = new ReadOnlyDictionary<string, EventType>(dictionary2);
		}

		// Token: 0x04005C21 RID: 23585
		public bool WalkByDefault;

		// Token: 0x04005C22 RID: 23586
		public bool CanCrouch = true;

		// Token: 0x04005C23 RID: 23587
		public bool CanJump = true;

		// Token: 0x04005C24 RID: 23588
		[SerializeField]
		protected Transform _cameraTransform;

		// Token: 0x04005C25 RID: 23589
		[SerializeField]
		protected ActorLocomotionThirdPerson _character;

		// Token: 0x04005C26 RID: 23590
		[SerializeField]
		private CommandArea _commandArea;

		// Token: 0x04005C28 RID: 23592
		private static Dictionary<string, Func<IState>> _getTypeEventTable;

		// Token: 0x04005C29 RID: 23593
		private static ReadOnlyDictionary<string, EventType> _statePairedEventTable;
	}
}
