using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000E29 RID: 3625
	public class CommandArea : MonoBehaviour
	{
		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06007146 RID: 28998 RVA: 0x00305178 File Offset: 0x00303578
		// (set) Token: 0x06007147 RID: 28999 RVA: 0x00305180 File Offset: 0x00303580
		public Transform BaseTransform
		{
			get
			{
				return this._baseTransform;
			}
			set
			{
				this._baseTransform = value;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06007148 RID: 29000 RVA: 0x00305189 File Offset: 0x00303589
		public Vector3 Offset
		{
			[CompilerGenerated]
			get
			{
				return this._offset;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06007149 RID: 29001 RVA: 0x00305191 File Offset: 0x00303591
		public float Radius
		{
			[CompilerGenerated]
			get
			{
				return this._radius;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x0600714A RID: 29002 RVA: 0x00305199 File Offset: 0x00303599
		public float AgentRadius
		{
			[CompilerGenerated]
			get
			{
				return this._agentRadius;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x0600714B RID: 29003 RVA: 0x003051A1 File Offset: 0x003035A1
		public float AllAroundRadius
		{
			[CompilerGenerated]
			get
			{
				return this._allaroundRadius;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x0600714C RID: 29004 RVA: 0x003051A9 File Offset: 0x003035A9
		public float Height
		{
			[CompilerGenerated]
			get
			{
				return this._height;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x0600714D RID: 29005 RVA: 0x003051B1 File Offset: 0x003035B1
		// (set) Token: 0x0600714E RID: 29006 RVA: 0x003051B9 File Offset: 0x003035B9
		public Vector3 BobberPosition { get; private set; }

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x0600714F RID: 29007 RVA: 0x003051C2 File Offset: 0x003035C2
		public Transform BobberTransform
		{
			[CompilerGenerated]
			get
			{
				return this._bobberTransform;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06007150 RID: 29008 RVA: 0x003051CC File Offset: 0x003035CC
		public CommandLabel.CommandInfo FishingLabel
		{
			get
			{
				if (this._fishingLabel == null)
				{
					CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
					Sprite icon2;
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(22, out icon2);
					this._fishingLabel = new CommandLabel.CommandInfo
					{
						Text = "釣りを始める",
						Icon = icon2,
						IsHold = true,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this._bobberTransform,
						Event = delegate
						{
							PlayerActor player = Singleton<Manager.Map>.Instance.Player;
							StuffItem equipedFishingItem = player.PlayerData.EquipedFishingItem;
							ItemIDKeyPair rodID = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine.RodID;
							if (equipedFishingItem.CategoryID != rodID.categoryID && equipedFishingItem.ID != rodID.itemID)
							{
								MapUIContainer.PushWarningMessage(Popup.Warning.Type.NotSetFishingRod);
								return;
							}
							List<StuffItem> itemList = player.PlayerData.ItemList;
							int inventorySlotMax = player.PlayerData.InventorySlotMax;
							bool flag = inventorySlotMax <= itemList.Count;
							if (flag)
							{
								MapUIContainer.PushWarningMessage(Popup.Warning.Type.PouchIsFull);
							}
							else
							{
								this.StartFishing();
							}
						}
					};
				}
				return this._fishingLabel;
			}
		}

		// Token: 0x06007151 RID: 29009 RVA: 0x00305263 File Offset: 0x00303663
		private void Start()
		{
			this._isFishableState.Subscribe(delegate(bool x)
			{
				this.OnFishableStateChange();
			});
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x0030527D File Offset: 0x0030367D
		private void OnFishableStateChange()
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			if (Singleton<Manager.Map>.Instance.Player != null)
			{
				this.RefreshCommands();
			}
		}

		// Token: 0x06007153 RID: 29011 RVA: 0x003052A8 File Offset: 0x003036A8
		private void SetCollisionState(ICommandable element, CollisionState state, ref int changedCount)
		{
			int instanceID = element.InstanceID;
			CollisionState collisionState;
			this._collisionStateTable.TryGetValue(instanceID, out collisionState);
			if (collisionState == state)
			{
				return;
			}
			this._collisionStateTable[instanceID] = state;
			if (state == CollisionState.Enter || state == CollisionState.Exit)
			{
				changedCount++;
			}
		}

		// Token: 0x06007154 RID: 29012 RVA: 0x003052F4 File Offset: 0x003036F4
		private void Update()
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
			Vector3 convOffset = Vector3.zero;
			if (this._baseTransform != null)
			{
				if (Singleton<Manager.Resources>.IsInstance())
				{
					int id = player.PlayerData.EquipedFishingItem.ID;
					if (id == Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine.RodID.itemID)
					{
						Vector3 vector = this._baseTransform.position + this._baseTransform.rotation * new Vector3(0f, 20f, 20f);
						LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
						float fishingWaterCheckDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.FishingWaterCheckDistance;
						Vector3 forward = this._baseTransform.forward;
						bool flag = false;
						for (int i = 0; i < 3; i++)
						{
							Vector3 origin = vector + forward * 7.5f * (float)i;
							RaycastHit raycastHit;
							flag = Physics.Raycast(origin, Vector3.down, out raycastHit, fishingWaterCheckDistance, areaDetectionLayer);
							if (flag)
							{
								flag &= (raycastHit.collider.tag == "Water");
							}
							if (flag)
							{
								vector.y = raycastHit.point.y;
								this.BobberPosition = vector;
								if (this._bobberTransform != null)
								{
									this._bobberTransform.position = vector;
								}
								break;
							}
						}
						this._isFishableState.Value = flag;
					}
					else
					{
						this._isFishableState.Value = false;
					}
				}
				Vector3 eulerAngles = this._baseTransform.eulerAngles;
				eulerAngles.x = (eulerAngles.z = 0f);
				Quaternion rotation = Quaternion.Euler(eulerAngles);
				convOffset = rotation * this._offset;
			}
			this.UpdateCollision(player, convOffset);
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x00305500 File Offset: 0x00303900
		public void UpdateCollision(PlayerActor player)
		{
			Vector3 convOffset = Vector3.zero;
			if (this._baseTransform != null)
			{
				Vector3 eulerAngles = this._baseTransform.eulerAngles;
				eulerAngles.x = (eulerAngles.z = 0f);
				Quaternion rotation = Quaternion.Euler(eulerAngles);
				convOffset = rotation * this._offset;
			}
			this.UpdateCollision(player, convOffset);
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x00305564 File Offset: 0x00303964
		public void UpdateCollision(PlayerActor player, Vector3 convOffset)
		{
			if (!this._commandableObjects.IsNullOrEmpty<ICommandable>())
			{
				bool flag = player.Controller.State is Follow;
				NavMeshAgent navMeshAgent = player.NavMeshAgent;
				int num = 0;
				foreach (ICommandable commandable in this._commandableObjects)
				{
					int instanceID = commandable.InstanceID;
					CollisionState collisionState;
					if (!this._collisionStateTable.TryGetValue(instanceID, out collisionState))
					{
						CollisionState collisionState2 = CollisionState.None;
						this._collisionStateTable[instanceID] = collisionState2;
						collisionState = collisionState2;
					}
					bool flag2 = this.WithinRange(navMeshAgent, commandable, convOffset);
					if (flag)
					{
						flag2 = false;
					}
					if (flag2)
					{
						switch (collisionState)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							if (!commandable.IsNeutralCommand)
							{
								continue;
							}
							if (!this.HasLabels(commandable))
							{
								continue;
							}
							this.SetCollisionState(commandable, CollisionState.Enter, ref num);
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.SetCollisionState(commandable, CollisionState.Stay, ref num);
							break;
						}
						if (commandable.IsNeutralCommand)
						{
							if (commandable.SetImpossible(true, Singleton<Manager.Map>.Instance.Player) && !this._considerationObjects.Contains(commandable))
							{
								this._considerationObjects.Add(commandable);
								num++;
							}
						}
					}
					else
					{
						switch (collisionState)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this.SetCollisionState(commandable, CollisionState.None, ref num);
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.SetCollisionState(commandable, CollisionState.Exit, ref num);
							break;
						}
						if (commandable.SetImpossible(false, Singleton<Manager.Map>.Instance.Player) && this._considerationObjects.Contains(commandable))
						{
							this._considerationObjects.Remove(commandable);
						}
					}
				}
				if (num > 0)
				{
					this.RefreshCommands();
				}
			}
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x00305764 File Offset: 0x00303B64
		private bool HasLabels(ICommandable elem)
		{
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			AgentActor x = null;
			if (player != null)
			{
				x = player.AgentPartner;
			}
			if (x == null)
			{
				return !elem.Labels.IsNullOrEmpty<CommandLabel.CommandInfo>();
			}
			CommandLabel.CommandInfo[] source;
			if (player != null)
			{
				if (player.Mode == Desire.ActionType.Date)
				{
					source = elem.DateLabels;
				}
				else
				{
					source = elem.Labels;
				}
			}
			else
			{
				source = null;
			}
			return !source.IsNullOrEmpty<CommandLabel.CommandInfo>();
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x003057E8 File Offset: 0x00303BE8
		public void RefreshCommands()
		{
			List<CommandLabel.CommandInfo> list = ListPool<CommandLabel.CommandInfo>.Get();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			AgentActor x = null;
			if (player != null)
			{
				x = player.AgentPartner;
			}
			foreach (ICommandable commandable in this._considerationObjects)
			{
				if (x == null)
				{
					if (!commandable.Labels.IsNullOrEmpty<CommandLabel.CommandInfo>())
					{
						foreach (CommandLabel.CommandInfo commandInfo in commandable.Labels)
						{
							Func<bool> displayCondition = commandInfo.DisplayCondition;
							bool? flag = (displayCondition != null) ? new bool?(displayCondition()) : null;
							if (flag == null || flag.Value)
							{
								commandInfo.Position = commandable.CommandCenter;
								list.Add(commandInfo);
							}
						}
					}
				}
				else
				{
					CommandLabel.CommandInfo[] array;
					if (player != null)
					{
						if (player.Mode == Desire.ActionType.Date)
						{
							array = commandable.DateLabels;
						}
						else
						{
							array = commandable.Labels;
						}
					}
					else
					{
						array = null;
					}
					if (!array.IsNullOrEmpty<CommandLabel.CommandInfo>())
					{
						foreach (CommandLabel.CommandInfo commandInfo2 in array)
						{
							commandInfo2.Position = commandable.CommandCenter;
							list.Add(commandInfo2);
						}
					}
				}
			}
			if (x == null && this._isFishableState.Value)
			{
				this.FishingLabel.Position = this.BobberPosition;
				list.Add(this.FishingLabel);
			}
			if (Singleton<MapUIContainer>.IsInstance())
			{
				MapUIContainer.CommandLabel.ObjectCommands = list.ToArray();
			}
			ListPool<CommandLabel.CommandInfo>.Release(list);
		}

		// Token: 0x06007159 RID: 29017 RVA: 0x003059F0 File Offset: 0x00303DF0
		public bool WithinRange(NavMeshAgent agent, ICommandable element, Vector3 offset)
		{
			bool flag = true;
			Vector3 commandCenter = element.CommandCenter;
			commandCenter.y = 0f;
			Vector3 vector = this._baseTransform.position + offset;
			vector.y = 0f;
			float distance = Vector3.Distance(commandCenter, vector);
			float num = Mathf.Abs(element.CommandCenter.y - this._baseTransform.position.y + this._offset.y);
			flag &= (num < this._height);
			if (flag)
			{
				flag &= element.Entered(vector, distance, this._radius, this._allaroundRadius, this._commandFovAngle, this._baseTransform.forward);
			}
			if (flag)
			{
				flag &= this.HasLabels(element);
			}
			if (flag)
			{
				if (element is Actor)
				{
					flag &= element.IsReachable(agent, this._agentRadius, this._allaroundRadius);
				}
				else
				{
					flag &= element.IsReachable(agent, this._radius, this._allaroundRadius);
				}
			}
			return flag;
		}

		// Token: 0x0600715A RID: 29018 RVA: 0x00305B00 File Offset: 0x00303F00
		public void SetCommandableObjects(ICommandable[] commandables)
		{
			this._commandableObjects.Clear();
			foreach (ICommandable commandable in commandables)
			{
				if (this._layerMask.Contains(commandable.Layer))
				{
					this._commandableObjects.Add(commandable);
				}
			}
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x00305B54 File Offset: 0x00303F54
		public void AddCommandableObject(ICommandable commandable)
		{
			if (this._layerMask.Contains(commandable.Layer) && !this._commandableObjects.Contains(commandable))
			{
				this._commandableObjects.Add(commandable);
			}
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x00305B8C File Offset: 0x00303F8C
		public void RemoveCommandableObject(ICommandable commandable)
		{
			if (this._commandableObjects.Contains(commandable))
			{
				this._commandableObjects.Remove(commandable);
			}
			if (this._considerationObjects.Contains(commandable))
			{
				commandable.SetImpossible(false, Singleton<Manager.Map>.Instance.Player);
				this._considerationObjects.Remove(commandable);
			}
			if (this._collisionStateTable.ContainsKey(commandable.InstanceID))
			{
				this._collisionStateTable.Remove(commandable.InstanceID);
			}
			this.RefreshCommands();
		}

		// Token: 0x0600715D RID: 29021 RVA: 0x00305C15 File Offset: 0x00304015
		public void RemoveConsiderationObject(ICommandable commandable)
		{
			if (this._considerationObjects.Contains(commandable))
			{
				commandable.SetImpossible(false, Singleton<Manager.Map>.Instance.Player);
				this._considerationObjects.Remove(commandable);
			}
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x00305C48 File Offset: 0x00304048
		public void SequenceSetConsiderations(Action<ICommandable> action)
		{
			if (action == null)
			{
				return;
			}
			foreach (ICommandable obj in this._commandableObjects)
			{
				if (action != null)
				{
					action(obj);
				}
			}
		}

		// Token: 0x0600715F RID: 29023 RVA: 0x00305CB4 File Offset: 0x003040B4
		public void InitCommandStates()
		{
			foreach (ICommandable commandable in this._commandableObjects)
			{
				int instanceID = commandable.InstanceID;
				this._collisionStateTable[instanceID] = CollisionState.None;
			}
			foreach (ICommandable commandable2 in this._considerationObjects)
			{
				commandable2.SetImpossible(false, Singleton<Manager.Map>.Instance.Player);
			}
			this._considerationObjects.Clear();
		}

		// Token: 0x06007160 RID: 29024 RVA: 0x00305D80 File Offset: 0x00304180
		public bool ContainsCommandableObject(ICommandable source)
		{
			return this._commandableObjects.Contains(source);
		}

		// Token: 0x06007161 RID: 29025 RVA: 0x00305D8E File Offset: 0x0030418E
		public bool ContainsConsiderationObject(ICommandable source)
		{
			return this._considerationObjects.Contains(source);
		}

		// Token: 0x06007162 RID: 29026 RVA: 0x00305D9C File Offset: 0x0030419C
		public bool CommandCondition(ICommandable element)
		{
			if (this._baseTransform == null)
			{
				return false;
			}
			bool flag = true;
			Vector3 b = Quaternion.Euler(0f, this._baseTransform.eulerAngles.y, 0f) * this._offset;
			Vector3 position = element.Position;
			Vector3 b2 = this._baseTransform.position + b;
			position.y = (b2.y = 0f);
			float num = Vector3.Distance(position, b2);
			float num2 = Mathf.Abs(element.CommandCenter.y - this._baseTransform.position.y + this._offset.y);
			flag &= (num2 < this._height);
			if (element.CommandType == CommandType.Forward)
			{
				flag &= (num < this._radius);
				float num3 = this._commandFovAngle / 2f;
				float num4 = Vector3.Angle(element.CommandCenter - this._baseTransform.position + b, this._baseTransform.forward);
				flag &= (num4 < num3);
			}
			else
			{
				flag &= (num < this._allaroundRadius);
			}
			return flag;
		}

		// Token: 0x06007163 RID: 29027 RVA: 0x00305EDC File Offset: 0x003042DC
		private void StartFishing()
		{
			Singleton<Manager.Map>.Instance.Player.Controller.ChangeState("Fishing");
		}

		// Token: 0x04005CFA RID: 23802
		[SerializeField]
		private Transform _baseTransform;

		// Token: 0x04005CFB RID: 23803
		[SerializeField]
		private Vector3 _offset = Vector3.zero;

		// Token: 0x04005CFC RID: 23804
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x04005CFD RID: 23805
		[SerializeField]
		private float _agentRadius = 1f;

		// Token: 0x04005CFE RID: 23806
		[SerializeField]
		private float _allaroundRadius = 1f;

		// Token: 0x04005CFF RID: 23807
		[SerializeField]
		private ObjectLayer _layerMask = ObjectLayer.Standard;

		// Token: 0x04005D00 RID: 23808
		[SerializeField]
		private float _commandFovAngle = 180f;

		// Token: 0x04005D01 RID: 23809
		[SerializeField]
		private float _height = 1f;

		// Token: 0x04005D02 RID: 23810
		private List<ICommandable> _commandableObjects = new List<ICommandable>();

		// Token: 0x04005D03 RID: 23811
		private List<ICommandable> _considerationObjects = new List<ICommandable>();

		// Token: 0x04005D04 RID: 23812
		private Dictionary<int, CollisionState> _collisionStateTable = new Dictionary<int, CollisionState>();

		// Token: 0x04005D05 RID: 23813
		private ReactiveProperty<bool> _isFishableState = new ReactiveProperty<bool>();

		// Token: 0x04005D07 RID: 23815
		[SerializeField]
		private Transform _bobberTransform;

		// Token: 0x04005D08 RID: 23816
		private CommandLabel.CommandInfo _fishingLabel;
	}
}
