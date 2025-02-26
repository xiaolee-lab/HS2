using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.UI;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000BCA RID: 3018
	public class WildMecha : AnimalGround
	{
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06005C20 RID: 23584 RVA: 0x00270D28 File Offset: 0x0026F128
		public override bool WaitPossible
		{
			[CompilerGenerated]
			get
			{
				return base.CurrentState == AnimalState.Idle;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06005C21 RID: 23585 RVA: 0x00270D33 File Offset: 0x0026F133
		public override bool ParamRisePossible
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06005C22 RID: 23586 RVA: 0x00270D36 File Offset: 0x0026F136
		public override bool DepopPossible
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x00270D3C File Offset: 0x0026F13C
		protected override void Awake()
		{
			base.Awake();
			if (base.Agent != null)
			{
				NavMeshAgent agent = base.Agent;
				int num = 0;
				base.Priority = num;
				agent.avoidancePriority = num;
			}
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x00270D75 File Offset: 0x0026F175
		protected override void OnDestroy()
		{
			if (this.habitatPoint != null)
			{
				this.habitatPoint.StopUse(this);
			}
			base.OnDestroy();
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x00270D9C File Offset: 0x0026F19C
		public void Initialize(MechaHabitatPoint _habitatPoint)
		{
			this.Clear();
			this.habitatPoint = _habitatPoint;
			if (_habitatPoint == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			if (!this.habitatPoint.SetUse(this))
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			MapArea ownerArea = this.habitatPoint.OwnerArea;
			base.ChunkID = ((!(ownerArea != null)) ? 0 : ownerArea.ChunkID);
			base.DeactivateNavMeshElements();
			Transform transform = this.habitatPoint.transform;
			this.Position = transform.position;
			base.Rotation = transform.rotation;
			base.Relocate(LocateTypes.NavMesh);
			this.stateController.Initialize(this);
			base.LoadBody();
			base.SetStateData();
			bool flag = false;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			this.SetState(AnimalState.Start, null);
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06005C26 RID: 23590 RVA: 0x00270E74 File Offset: 0x0026F274
		public override bool IsNeutralCommand
		{
			get
			{
				return !this.BadMood && base.CurrentState == AnimalState.Idle;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06005C27 RID: 23591 RVA: 0x00270E90 File Offset: 0x0026F290
		public override CommandLabel.CommandInfo[] Labels
		{
			get
			{
				if (!this.IsNeutralCommand)
				{
					return AnimalBase.emptyLabels;
				}
				LabelTypes labelType = base.LabelType;
				if (labelType != LabelTypes.GetAnimal)
				{
					return AnimalBase.emptyLabels;
				}
				return this.getLabels;
			}
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x00270ED0 File Offset: 0x0026F2D0
		protected override void InitializeCommandLabels()
		{
			if (this.getLabels.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				CommonDefine commonDefine = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.CommonDefine;
				CommonDefine.CommonIconGroup commonIconGroup = (!(commonDefine != null)) ? null : commonDefine.Icon;
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				int guideCancelID = commonIconGroup.GuideCancelID;
				Sprite icon;
				instance.itemIconTables.InputIconTable.TryGetValue(guideCancelID, out icon);
				Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
				List<string> source;
				eventPointCommandLabelTextTable.TryGetValue(0, out source);
				int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
				this.getLabels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = source.GetElement(index),
						Transform = base.LabelPoint,
						IsHold = false,
						Icon = icon,
						TargetSpriteInfo = ((commonIconGroup != null) ? commonIconGroup.CharaSpriteInfo : null),
						Event = delegate
						{
							PlayerActor playerActor = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
							PlayerController playerController = (!(playerActor != null)) ? null : playerActor.PlayerController;
							EventPoint.SetCurrentPlayerStateName();
							if (playerController != null)
							{
								playerController.ChangeState("Idle");
							}
							RequestUI requestUI = MapUIContainer.RequestUI;
							requestUI.SubmitEvent = delegate()
							{
								this.GetMecha();
							};
							requestUI.SubmitCondition = (() => this.CanGet());
							requestUI.ClosedEvent = delegate()
							{
								EventPoint.ChangePrevPlayerMode();
							};
							requestUI.Open(Popup.Request.Type.Cuby);
							if (requestUI.IsImpossible)
							{
								MapUIContainer.PushWarningMessage(Popup.Warning.Type.InsufficientBattery);
							}
						}
					}
				};
			}
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x00270FE8 File Offset: 0x0026F3E8
		protected override void EnterStart()
		{
			bool flag = true;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			base.LabelType = LabelTypes.GetAnimal;
			base.ActivateNavMeshObstacle();
			this.Active = true;
			this.SetState(AnimalState.Idle, null);
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x00271021 File Offset: 0x0026F421
		protected override void OnStart()
		{
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x00271023 File Offset: 0x0026F423
		protected override void ExitStart()
		{
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x00271025 File Offset: 0x0026F425
		protected override void AnimationStart()
		{
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x00271027 File Offset: 0x0026F427
		protected override void EnterIdle()
		{
			base.PlayInAnim(AnimationCategoryID.Etc, 0, null);
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x00271032 File Offset: 0x0026F432
		protected override void OnIdle()
		{
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x00271034 File Offset: 0x0026F434
		protected override void ExitIdle()
		{
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x00271036 File Offset: 0x0026F436
		protected override void AnimationIdle()
		{
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x00271038 File Offset: 0x0026F438
		protected override void EnterDepop()
		{
			base.AutoChangeAnimation = false;
			StuffItemInfo _itemInfo = base.ItemInfo;
			StuffItem _addItem = null;
			if (_itemInfo != null)
			{
				_addItem = new StuffItem(_itemInfo.CategoryID, _itemInfo.ID, 1);
			}
			PlayerActor playerActor = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
			List<StuffItem> list;
			if (playerActor != null)
			{
				PlayerData playerData = playerActor.PlayerData;
				list = ((playerData != null) ? playerData.ItemList : null);
			}
			else
			{
				list = null;
			}
			List<StuffItem> list2 = list;
			if (list2 != null && _addItem != null)
			{
				playerActor.PlayerData.ItemList.AddItem(_addItem);
			}
			if (Singleton<Manager.Resources>.IsInstance())
			{
				AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
				if (animalDefinePack != null)
				{
					AudioSource audioSource = base.Play3DSound(animalDefinePack.SoundID.MechaStartup);
					if (audioSource != null)
					{
						audioSource.Stop();
						audioSource.transform.SetPositionAndRotation(this.Position, base.Rotation);
						audioSource.Play();
					}
				}
			}
			base.PlayInAnim(AnimationCategoryID.Etc, 1, delegate()
			{
				this.AutoChangeAnimation = true;
				if (_itemInfo != null && _addItem != null)
				{
					MapUIContainer.AddSystemItemLog(_itemInfo, _addItem.Count, true);
				}
				else
				{
					MapUIContainer.AddNotify(MapUIContainer.ItemGetEmptyText);
				}
				this.SetState(AnimalState.Destroyed, null);
			});
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x00271183 File Offset: 0x0026F583
		protected override void OnDepop()
		{
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x00271185 File Offset: 0x0026F585
		protected override void ExitDepop()
		{
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x00271187 File Offset: 0x0026F587
		protected override void AnimationDepop()
		{
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x0027118C File Offset: 0x0026F58C
		public bool CanGet()
		{
			PlayerActor player = Manager.Map.GetPlayer();
			if (player == null)
			{
				return false;
			}
			PlayerData playerData = player.PlayerData;
			if (playerData == null)
			{
				return false;
			}
			List<StuffItem> itemList = playerData.ItemList;
			if (itemList == null)
			{
				return false;
			}
			StuffItem item = new StuffItem(this.ItemID.categoryID, this.ItemID.itemID, 1);
			int num;
			return itemList.CanAddItem(playerData.InventorySlotMax, item, out num) && 0 < num;
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x0027120C File Offset: 0x0026F60C
		protected void GetMecha()
		{
			if (!this.CanGet())
			{
				MapUIContainer.PushWarningMessage(Popup.Warning.Type.PouchIsFull);
				return;
			}
			base.LabelType = LabelTypes.None;
			this.SetState(AnimalState.Depop, null);
		}

		// Token: 0x040052FE RID: 21246
		private MechaHabitatPoint habitatPoint;

		// Token: 0x040052FF RID: 21247
		protected CommandLabel.CommandInfo[] getLabels;
	}
}
