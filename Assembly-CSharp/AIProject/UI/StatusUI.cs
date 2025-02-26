using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using AIChara;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000EBC RID: 3772
	public class StatusUI : MenuUIBehaviour
	{
		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06007B5D RID: 31581 RVA: 0x0033EA2E File Offset: 0x0033CE2E
		// (set) Token: 0x06007B5E RID: 31582 RVA: 0x0033EA36 File Offset: 0x0033CE36
		public SystemMenuUI Observer { get; set; }

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06007B5F RID: 31583 RVA: 0x0033EA3F File Offset: 0x0033CE3F
		// (set) Token: 0x06007B60 RID: 31584 RVA: 0x0033EA47 File Offset: 0x0033CE47
		public System.Action OnClose { get; set; }

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06007B61 RID: 31585 RVA: 0x0033EA50 File Offset: 0x0033CE50
		// (set) Token: 0x06007B62 RID: 31586 RVA: 0x0033EA58 File Offset: 0x0033CE58
		public int OpenID { get; set; }

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x06007B63 RID: 31587 RVA: 0x0033EA61 File Offset: 0x0033CE61
		public Toggle LockParameterToggle
		{
			[CompilerGenerated]
			get
			{
				return this._lockParameterToggle;
			}
		}

		// Token: 0x06007B64 RID: 31588 RVA: 0x0033EA69 File Offset: 0x0033CE69
		public IObservable<int> OnSelectIDChangedAsObservable()
		{
			if (this._selectIDChange == null)
			{
				this._selectIDChange = this._selectedID.TakeUntilDestroy(base.gameObject).Publish<int>();
				this._selectIDChange.Connect();
			}
			return this._selectIDChange;
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06007B65 RID: 31589 RVA: 0x0033EAA4 File Offset: 0x0033CEA4
		private IntReactiveProperty _lifestyleID { get; } = new IntReactiveProperty(-1);

		// Token: 0x06007B66 RID: 31590 RVA: 0x0033EAAC File Offset: 0x0033CEAC
		protected override void OnBeforeStart()
		{
			StatusUI.<OnBeforeStart>c__AnonStorey5 <OnBeforeStart>c__AnonStorey = new StatusUI.<OnBeforeStart>c__AnonStorey5();
			<OnBeforeStart>c__AnonStorey.$this = this;
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool active)
			{
				<OnBeforeStart>c__AnonStorey.$this.SetActiveControl(active);
			});
			this._leftButton.onClick.AddListener(delegate()
			{
				int l = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
				if (l < 0)
				{
					return;
				}
				int num = 0;
				for (int m = 0; m < <OnBeforeStart>c__AnonStorey.$this._charaButtons.Length; m++)
				{
					if (<OnBeforeStart>c__AnonStorey.$this._charaButtons[m].gameObject.activeSelf)
					{
						num = m;
					}
				}
				while (l > 0)
				{
					if (<OnBeforeStart>c__AnonStorey.$this._charaButtons[l].gameObject.activeSelf)
					{
						break;
					}
					l--;
				}
				<OnBeforeStart>c__AnonStorey.$this._selectedID.Value = l;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Page);
				<OnBeforeStart>c__AnonStorey.$this._leftButton.interactable = (l > 0);
				<OnBeforeStart>c__AnonStorey.$this._rightButton.interactable = (l < num);
			});
			this._rightButton.onClick.AddListener(delegate()
			{
				int l = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value + 1;
				int num = 0;
				for (int m = 0; m < <OnBeforeStart>c__AnonStorey.$this._charaButtons.Length; m++)
				{
					if (<OnBeforeStart>c__AnonStorey.$this._charaButtons[m].gameObject.activeSelf)
					{
						num = m;
					}
				}
				if (l > num)
				{
					return;
				}
				while (l < num)
				{
					if (<OnBeforeStart>c__AnonStorey.$this._charaButtons[l].gameObject.activeSelf)
					{
						break;
					}
					l++;
				}
				<OnBeforeStart>c__AnonStorey.$this._selectedID.Value = l;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Page);
				<OnBeforeStart>c__AnonStorey.$this._leftButton.interactable = (l > 0);
				<OnBeforeStart>c__AnonStorey.$this._rightButton.interactable = (l < num);
			});
			this.OnSelectIDChangedAsObservable().Subscribe(delegate(int x)
			{
				<OnBeforeStart>c__AnonStorey.$this.ChangeContent(x);
			});
			for (int i = 0; i < this._charaButtons.Length; i++)
			{
				int id = i;
				Button button2 = this._charaButtons[i];
				if (button2 != null)
				{
					button2.onClick.AddListener(delegate()
					{
						if (<OnBeforeStart>c__AnonStorey.$this._selectedID.Value != id)
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Page);
						}
						<OnBeforeStart>c__AnonStorey.$this._selectedID.Value = id;
					});
					button2.targetGraphic.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
					{
						<OnBeforeStart>c__AnonStorey.$this._focusID = id;
					});
				}
			}
			this._slotExtendButton.onClick.AddListener(delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
				SystemMenuUI systemMenuUI = MapUIContainer.SystemMenuUI;
				InventoryUIController inventoryUI = systemMenuUI.InventoryEnterUI;
				inventoryUI.isConfirm = true;
				inventoryUI.CountViewerVisible(false);
				inventoryUI.EmptyTextAutoVisible(true);
				inventoryUI.SetItemFilter(new InventoryFacadeViewer.ItemFilter[]
				{
					new InventoryFacadeViewer.ItemFilter(itemIDDefine.ToolCategoryID, itemIDDefine.ExtendItemIDs)
				});
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				inventoryUI.itemList = (() => playerData.ItemList);
				inventoryUI.DoubleClickAction(null);
				inventoryUI.OnSubmit = delegate(StuffItem item)
				{
					playerData.InventorySlotMax += 5 * item.Count;
					MapUIContainer.AddSystemLog(string.Format("ポーチ容量が{0}スロットになりました", playerData.InventorySlotMax), true);
					InventoryUIController inventoryUI = inventoryUI;
					if (inventoryUI != null)
					{
						inventoryUI.OnClose();
					}
				};
				inventoryUI.OnClose = delegate()
				{
					inventoryUI.itemList_System = null;
					inventoryUI.OnSubmit = null;
					inventoryUI.IsActiveControl = false;
					Singleton<Manager.Input>.Instance.FocusLevel = 0;
					<OnBeforeStart>c__AnonStorey.IsActiveControl = true;
					inventoryUI.OnClose = null;
				};
				<OnBeforeStart>c__AnonStorey.$this.OpenID = 0;
				<OnBeforeStart>c__AnonStorey.$this.IsActiveControl = false;
				<OnBeforeStart>c__AnonStorey.$this.Observer.OpenModeMenu(SystemMenuUI.MenuMode.InventoryEnter);
			});
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			<OnBeforeStart>c__AnonStorey.itemIDDefine = commonDefine.ItemIDDefine;
			this._handEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedGloveItem;
				}
				else
				{
					destItem = playerData.EquipedGloveItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.ToolCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.GloveItemIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangeGloveEquipment(destItem, item);
				});
			});
			this._handEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 0;
			});
			this._handEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._netEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedNetItem;
				}
				else
				{
					destItem = playerData.EquipedNetItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.ToolCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.NetItemIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangeNetEquipment(destItem, item);
				});
			});
			this._netEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 1;
			});
			this._netEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._shovelEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedShovelItem;
				}
				else
				{
					destItem = playerData.EquipedShovelItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.ToolCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.ShovelItemlIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangeShovelEquipment(destItem, item);
				});
			});
			this._shovelEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 2;
			});
			this._shovelEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._pickelEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedPickelItem;
				}
				else
				{
					destItem = playerData.EquipedPickelItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.ToolCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.PickelItemIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangePickelEquipment(destItem, item);
				});
			});
			this._pickelEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 3;
			});
			this._pickelEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._rodEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedFishingItem;
				}
				else
				{
					destItem = playerData.EquipedFishingItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.ToolCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.RodItemIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangeRodEquipment(destItem, item);
				});
			});
			this._rodEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 4;
			});
			this._rodEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._hatEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedHeadItem;
				}
				else
				{
					destItem = playerData.EquipedHeadItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.EquipmentCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.HeadItemIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangeHatEquipment(destItem, item);
				});
			});
			this._hatEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 5;
			});
			this._hatEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._ruckEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedBackItem;
				}
				else
				{
					destItem = playerData.EquipedBackItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.EquipmentCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.BackItemIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangeRuckEquipment(destItem, item);
				});
			});
			this._ruckEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 6;
			});
			this._ruckEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._necklaceEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				StuffItem destItem;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					destItem = agentData.EquipedNeckItem;
				}
				else
				{
					destItem = playerData.EquipedNeckItem;
				}
				<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.EquipmentCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.NeckItemIDs, delegate(StuffItem item)
				{
					<OnBeforeStart>c__AnonStorey.ChangeNecklaceEquipment(destItem, item);
				});
			});
			this._necklaceEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 7;
			});
			this._necklaceEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			this._lampEQButton.onClick.AddListener(delegate()
			{
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				int value = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value;
				if (value != 0)
				{
					int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
					AgentData agentData = Singleton<Manager.Map>.Instance.AgentTable[key].AgentData;
					StuffItem destItem = agentData.EquipedLampItem;
					<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.EquipmentCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.FemaleLightItemIDs, delegate(StuffItem item)
					{
						<OnBeforeStart>c__AnonStorey.ChangeLampEquipment(destItem, item);
					});
				}
				else
				{
					StuffItem destItem = playerData.EquipedLampItem;
					<OnBeforeStart>c__AnonStorey.$this.EquipmentSeq(destItem, <OnBeforeStart>c__AnonStorey.itemIDDefine.EquipmentCategoryID, <OnBeforeStart>c__AnonStorey.itemIDDefine.PlayerLightItemIDs, delegate(StuffItem item)
					{
						<OnBeforeStart>c__AnonStorey.ChangeLampEquipment(destItem, item);
					});
				}
			});
			this._lampEQImage.OnPointerEnterAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = 8;
			});
			this._lampEQImage.OnPointerExitAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._eqFocusID.Value = -1;
			});
			for (int j = 0; j < this._normalSkillButtons.Length; j++)
			{
				int id = j;
				Button button = this._normalSkillButtons[j];
				if (button != null)
				{
					button.onClick.AddListener(delegate()
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						<OnBeforeStart>c__AnonStorey.$this._skillSelectedID = id;
						<OnBeforeStart>c__AnonStorey.$this.SkillEquipmentSeq(false, <OnBeforeStart>c__AnonStorey.itemIDDefine.NormalSkillCategoryID, delegate(StuffItem item)
						{
							int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
							AgentActor agentActor = Singleton<Manager.Map>.Instance.AgentTable[key];
							agentActor.ChaControl.fileGameInfo.normalSkill[id] = item.ID;
							<OnBeforeStart>c__AnonStorey.$this.RefreshNormalSkill(id);
						});
					});
					button.targetGraphic.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
					{
						if (!button.IsInteractable())
						{
							return;
						}
						<OnBeforeStart>c__AnonStorey.$this._skillFocusID.Value = id;
					});
					button.targetGraphic.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
					{
						if (!button.IsInteractable())
						{
							return;
						}
						<OnBeforeStart>c__AnonStorey.$this._skillFocusID.Value = -1;
					});
				}
			}
			for (int k = 0; k < this._hSkillButtons.Length; k++)
			{
				int id = this._normalSkillButtons.Length + k;
				Button button = this._hSkillButtons[k];
				if (button != null)
				{
					button.onClick.AddListener(delegate()
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						<OnBeforeStart>c__AnonStorey.$this._skillSelectedID = id;
						<OnBeforeStart>c__AnonStorey.$this.SkillEquipmentSeq(true, <OnBeforeStart>c__AnonStorey.itemIDDefine.HSkillCategoryID, delegate(StuffItem item)
						{
							int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
							AgentActor agentActor = Singleton<Manager.Map>.Instance.AgentTable[key];
							int num = id - <OnBeforeStart>c__AnonStorey.$this._normalSkillButtons.Length;
							agentActor.ChaControl.fileGameInfo.hSkill[num] = item.ID;
							<OnBeforeStart>c__AnonStorey.$this.RefreshHSkill(num);
						});
					});
					button.targetGraphic.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
					{
						if (!button.IsInteractable())
						{
							return;
						}
						<OnBeforeStart>c__AnonStorey.$this._skillFocusID.Value = id;
					});
					button.targetGraphic.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
					{
						if (!button.IsInteractable())
						{
							return;
						}
						<OnBeforeStart>c__AnonStorey.$this._skillFocusID.Value = -1;
					});
				}
			}
			this._lockParameterToggle.onValueChanged.AddListener(delegate(bool isOn)
			{
				if (<OnBeforeStart>c__AnonStorey.$this._selectedID.Value <= 0)
				{
					return;
				}
				int key = <OnBeforeStart>c__AnonStorey.$this._selectedID.Value - 1;
				AgentActor agentActor = Singleton<Manager.Map>.Instance.AgentTable[key];
				agentActor.AgentData.ParameterLock = isOn;
			});
			this._eqFocusID.Subscribe(delegate(int x)
			{
				<OnBeforeStart>c__AnonStorey.$this.RefreshEquipmentExplanation(x);
			});
			this._skillFocusID.Subscribe(delegate(int x)
			{
				<OnBeforeStart>c__AnonStorey.$this.RefreshSkillExplanation(x);
			});
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				<OnBeforeStart>c__AnonStorey.$this.Close();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where <OnBeforeStart>c__AnonStorey.$this.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				<OnBeforeStart>c__AnonStorey.$this.OnUpdate();
			});
			this._contentCanvasGroup.alpha = 1f;
			this._contentCanvasGroup.blocksRaycasts = true;
			if (this._activeContent == null)
			{
				this._activeContent = this._playerContent;
			}
			this._leftButton.interactable = false;
			this._rightButton.interactable = true;
			this._skillPanel.gameObject.SetActive(false);
			LifeStyleData.Param param = null;
			(from x in this._lifestyleID
			select (!Singleton<Manager.Resources>.IsInstance() || !Singleton<Manager.Resources>.Instance.GameInfo.initialized || !Singleton<Manager.Resources>.Instance.GameInfo.AgentLifeStyleInfoTable.TryGetValue(x, out param)) ? "ーーーーー" : param.Name).SubscribeToText(this._lifeStyleLabel);
			Text lifeStyleLabel = this._lifeStyleLabel;
			lifeStyleLabel.raycastTarget = true;
			(from _ in lifeStyleLabel.OnPointerEnterAsObservable()
			where param != null
			select _).Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._skillNameLabel.text = param.Name;
				<OnBeforeStart>c__AnonStorey.$this._skillFlavorText.text = param.Explanation;
			});
			lifeStyleLabel.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
			{
				<OnBeforeStart>c__AnonStorey.$this._skillNameLabel.text = string.Empty;
				<OnBeforeStart>c__AnonStorey.$this._skillFlavorText.text = string.Empty;
			});
		}

		// Token: 0x06007B67 RID: 31591 RVA: 0x0033F2D8 File Offset: 0x0033D6D8
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				if (this._selectedID.Value == this.OpenID)
				{
					this._contentCanvasGroup.alpha = 1f;
				}
				else
				{
					this._contentCanvasGroup.alpha = 0f;
				}
				this._activeContent.gameObject.SetActiveIfDifferent(false);
				if (this.OpenID == 0)
				{
					this._activeContent = this._playerContent;
				}
				else
				{
					this._activeContent = this._agentContent;
				}
				this._selectedID.Value = this.OpenID;
				this.RefreshEquipments(this.OpenID);
				if (this.OpenID == 0)
				{
					this.RefreshPlayerContent();
				}
				else
				{
					this.RefreshAgentContent(this.OpenID);
				}
				this.RefreshName();
				this._activeContent.gameObject.SetActiveIfDifferent(true);
				this.RefreshName();
				this.UsageRestriction();
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.OpenCoroutine();
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
				coroutine = this.CloseCoroutine();
			}
			if (this._fadeSubscriber != null)
			{
				this._fadeSubscriber.Dispose();
			}
			this._fadeSubscriber = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06007B68 RID: 31592 RVA: 0x0033F47C File Offset: 0x0033D87C
		public MenuUIBehaviour[] MenuUIList
		{
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuUIList) == null)
				{
					result = (this._menuUIList = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x0033F4AC File Offset: 0x0033D8AC
		private IEnumerator OpenCoroutine()
		{
			if (this.Observer != null)
			{
				this.Observer.OnClose = delegate()
				{
					this.Close();
				};
				this.Observer.ChangeBackground(this._selectedID.Value);
				this.ChangeBackground(this._selectedID.Value);
			}
			this.RefreshName();
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			ReadOnlyDictionary<int, Actor> actorTable = Singleton<Manager.Map>.Instance.ActorTable;
			this._charaButtons[1].gameObject.SetActiveIfDifferent(actorTable.ContainsKey(0));
			this._charaButtons[2].gameObject.SetActiveIfDifferent(actorTable.ContainsKey(1));
			this._charaButtons[3].gameObject.SetActiveIfDifferent(actorTable.ContainsKey(2));
			this._charaButtons[4].gameObject.SetActiveIfDifferent(actorTable.ContainsKey(3));
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			this.Observer.ActiveCloseButton = true;
			yield break;
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x0033F4C8 File Offset: 0x0033D8C8
		private IEnumerator CloseCoroutine()
		{
			this.Observer.ActiveCloseButton = false;
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x0033F4E4 File Offset: 0x0033D8E4
		private void ChangeContent(int id)
		{
			if (this._contentChangeDisposable != null)
			{
				this._contentChangeDisposable.Dispose();
			}
			this._contentChangeDisposable = Observable.FromCoroutine(() => this.ChangeContentCoroutine(id), false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x0033F574 File Offset: 0x0033D974
		private IEnumerator ChangeContentCoroutine(int id)
		{
			this.Observer.ChangeBackground(id);
			this.ChangeBackground(id);
			RectTransform activeContent = this._activeContent;
			float startAlpha;
			if (activeContent != null)
			{
				this._contentCanvasGroup.blocksRaycasts = false;
				float startAlpha = this._contentCanvasGroup.alpha;
				IObservable<TimeInterval<float>> outStream = ObservableEasing.Linear(0.1f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
				{
					this._contentCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
				});
				yield return outStream.ToYieldInstruction<TimeInterval<float>>();
				activeContent.gameObject.SetActive(false);
			}
			this.RefreshEquipments(id);
			if (id == 0)
			{
				this._activeContent = this._playerContent;
				this.RefreshPlayerContent();
			}
			else
			{
				this._activeContent = this._agentContent;
				this.RefreshAgentContent(id);
			}
			this.RefreshName();
			this._activeContent.gameObject.SetActive(true);
			startAlpha = this._contentCanvasGroup.alpha;
			IObservable<TimeInterval<float>> inStream = ObservableEasing.Linear(0.1f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._contentCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return inStream.ToYieldInstruction<TimeInterval<float>>();
			this._contentCanvasGroup.blocksRaycasts = true;
			yield break;
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x0033F596 File Offset: 0x0033D996
		private void Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.Observer.ChangeBackground(-1);
			this.OpenID = 0;
			System.Action onClose = this.OnClose;
			if (onClose != null)
			{
				onClose();
			}
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x0033F5D0 File Offset: 0x0033D9D0
		private void RefreshName()
		{
			Dictionary<int, Sprite> actorIconTable = Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable;
			if (this._selectedID.Value == 0)
			{
				this._nameLabel.text = Singleton<Manager.Map>.Instance.Player.CharaName;
				Sprite sprite;
				if (actorIconTable.TryGetValue(-99, out sprite))
				{
					this._charaIconImage.sprite = sprite;
					this._charaIconImage.color = new Color32(133, 214, 83, byte.MaxValue);
				}
			}
			else
			{
				int key = this._selectedID.Value - 1;
				AgentActor agentActor;
				if (!Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(key, out agentActor))
				{
					return;
				}
				this._nameLabel.text = string.Format("{0}", agentActor.CharaName);
				Sprite sprite2;
				if (actorIconTable.TryGetValue(this._selectedID.Value - 1, out sprite2))
				{
					this._charaIconImage.sprite = sprite2;
					this._charaIconImage.color = Color.white;
				}
			}
		}

		// Token: 0x06007B6F RID: 31599 RVA: 0x0033F6D8 File Offset: 0x0033DAD8
		private void RefreshPlayerContent()
		{
			this._skillPanel.gameObject.SetActive(false);
			if (!Singleton<Manager.Map>.IsInstance() || !Singleton<Game>.IsInstance())
			{
				return;
			}
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (player == null || player.PlayerData == null)
			{
				return;
			}
			PlayerData playerData = player.PlayerData;
			if (!playerData.CharaFileName.IsNullOrEmpty())
			{
				string path = player.ChaControl.chaFile.ConvertCharaFilePath(playerData.CharaFileName, playerData.Sex, false);
				if (File.Exists(path))
				{
					this._cardRawImage.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(path), 0, 0, TextureFormat.ARGB32, false);
				}
				else
				{
					this._cardRawImage.texture = null;
				}
			}
			else
			{
				this._cardRawImage.texture = null;
			}
			this._cardRawImage.gameObject.SetActiveIfDifferent(this._cardRawImage.texture != null);
			this._sexLabel.text = ((playerData.Sex != 0) ? "女性" : "男性");
			if (playerData.Sex == 1 && player.ChaControl.fileParam.futanari)
			{
				Text sexLabel = this._sexLabel;
				sexLabel.text += "\u3000ふたなり";
			}
			this._inventoryMaxLabel.text = string.Format("{0}", playerData.InventorySlotMax);
			this._fishingLevelLabel.text = string.Format("{0}", playerData.FishingSkill.Level);
			this._fishingExperienceImage.fillAmount = playerData.FishingSkill.Experience / (float)playerData.FishingSkill.NextExperience;
		}

		// Token: 0x06007B70 RID: 31600 RVA: 0x0033F898 File Offset: 0x0033DC98
		private void RefreshAgentContent(int id)
		{
			int key = id - 1;
			AgentActor agentActor;
			if (!Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(key, out agentActor))
			{
				return;
			}
			ChaFileGameInfo fileGameInfo = agentActor.ChaControl.fileGameInfo;
			AgentData agentData = agentActor.AgentData;
			if (!agentData.CharaFileName.IsNullOrEmpty())
			{
				string path = agentActor.ChaControl.chaFile.ConvertCharaFilePath(agentData.CharaFileName, 1, false);
				this._cardRawImage.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(path), 0, 0, TextureFormat.ARGB32, false);
			}
			else
			{
				this._cardRawImage.texture = null;
			}
			this._cardRawImage.gameObject.SetActiveIfDifferent(this._cardRawImage.texture != null);
			for (int i = 0; i < this._phaseImages.Length; i++)
			{
				Sprite sprite = this._phaseImages[i].sprite;
				if (i <= agentActor.ChaControl.fileGameInfo.phase)
				{
					if (sprite != this._phaseActiveSprite)
					{
						this._phaseImages[i].sprite = this._phaseActiveSprite;
					}
				}
				else if (sprite != this._phaseInactiveSprite)
				{
					this._phaseImages[i].sprite = this._phaseInactiveSprite;
				}
			}
			float num = agentData.StatsTable[0];
			int key2 = 0;
			if (num <= agentActor.ChaControl.fileGameInfo.tempBound.lower)
			{
				key2 = 1;
			}
			else if (num >= agentActor.ChaControl.fileGameInfo.tempBound.upper)
			{
				key2 = 2;
			}
			this._tempImage.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.StatusIconTable[0][key2];
			this._tempSlider.value = num / 100f;
			float x5 = agentActor.ChaControl.fileGameInfo.tempBound.lower / 100f;
			float x2 = agentActor.ChaControl.fileGameInfo.tempBound.upper / 100f;
			RectTransform rectTransform = this._tempBorderImages[0].rectTransform;
			rectTransform.anchorMin = new Vector2(0f, 0f);
			rectTransform.anchorMax = new Vector2(x5, 1f);
			rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
			rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y);
			rectTransform = this._tempBorderImages[1].rectTransform;
			rectTransform.anchorMin = new Vector2(x5, 0f);
			rectTransform.anchorMax = new Vector2(x2, 1f);
			rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
			rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y);
			rectTransform = this._tempBorderImages[2].rectTransform;
			rectTransform.anchorMin = new Vector2(x2, 0f);
			rectTransform.anchorMax = new Vector2(1f, 1f);
			rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
			rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y);
			float num2 = agentData.StatsTable[2];
			int key3 = 0;
			if (num2 < 30f)
			{
				key3 = 1;
			}
			this._hungerIcon.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.StatusIconTable[2][key3];
			this._hungerLabel.text = string.Format("{0}", Mathf.Clamp(Mathf.FloorToInt((float)((int)num2)), 0, 100));
			float num3 = agentData.StatsTable[3];
			float num4 = 100f - num3;
			int key4 = Mathf.Min((int)(num4 / 25f), 3);
			Sprite sprite2;
			if (Singleton<Manager.Resources>.Instance.itemIconTables.StatusIconTable[3].TryGetValue(key4, out sprite2))
			{
				this._physicalIcon.sprite = sprite2;
			}
			this._physicalLabel.text = string.Format("{0}", Mathf.Clamp(Mathf.FloorToInt((float)((int)num4)), 0, 100));
			bool flag = agentActor.ChaControl.fileGameInfo.phase >= 2;
			if (this._moodContent.activeSelf != flag)
			{
				this._moodContent.SetActive(flag);
			}
			if (flag)
			{
				float num5 = agentData.StatsTable[1];
				int key5 = 0;
				Color32 c = new Color32(100, 185, 22, byte.MaxValue);
				if (num5 >= agentActor.ChaControl.fileGameInfo.moodBound.upper)
				{
					key5 = 1;
					c = new Color32(245, 178, 24, byte.MaxValue);
				}
				else if (num5 <= agentActor.ChaControl.fileGameInfo.moodBound.lower)
				{
					key5 = 2;
					c = new Color32(35, 112, 216, byte.MaxValue);
				}
				this._moodIcon.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.StatusIconTable[1][key5];
				int num6 = (int)(num5 / 10f);
				for (int j = 0; j < this._moodGraphs.Length; j++)
				{
					Image image = this._moodGraphs[j];
					image.color = c;
					bool flag2 = j < num6;
					if (image.gameObject.activeSelf != flag2)
					{
						this._moodGraphs[j].gameObject.SetActive(flag2);
					}
				}
				float x3 = agentActor.ChaControl.fileGameInfo.moodBound.lower / 100f;
				float x4 = agentActor.ChaControl.fileGameInfo.moodBound.upper / 100f;
				RectTransform rectTransform2 = this._moodBorderGraphs[0].rectTransform;
				rectTransform2.anchorMin = new Vector2(0f, 0f);
				rectTransform2.anchorMax = new Vector2(x3, 1f);
				rectTransform2.offsetMin = new Vector2(0f, rectTransform2.offsetMin.y);
				rectTransform2.offsetMax = new Vector2(0f, rectTransform2.offsetMax.y);
				rectTransform2 = this._moodBorderGraphs[1].rectTransform;
				rectTransform2.anchorMin = new Vector2(x3, 0f);
				rectTransform2.anchorMax = new Vector2(x4, 1f);
				rectTransform2.offsetMin = new Vector2(0f, rectTransform2.offsetMin.y);
				rectTransform2.offsetMax = new Vector2(0f, rectTransform2.offsetMax.y);
				rectTransform2 = this._moodBorderGraphs[2].rectTransform;
				rectTransform2.anchorMin = new Vector2(x4, 0f);
				rectTransform2.anchorMax = new Vector2(1f, 1f);
				rectTransform2.offsetMin = new Vector2(0f, rectTransform2.offsetMin.y);
				rectTransform2.offsetMax = new Vector2(0f, rectTransform2.offsetMax.y);
			}
			bool flag3 = agentActor.ChaControl.fileGameInfo.phase >= 2;
			if (this._motivationContent.activeSelf != flag3)
			{
				this._motivationContent.SetActive(flag3);
			}
			if (flag3)
			{
				float num7 = agentData.StatsTable[5];
				int num8 = (int)(num7 / 10f);
				for (int k = 0; k < this._motivationGraphs.Length; k++)
				{
					Image image2 = this._motivationGraphs[k];
					bool flag4 = k < num8;
					if (image2.gameObject.activeSelf != flag4)
					{
						this._motivationGraphs[k].gameObject.SetActive(flag4);
					}
				}
				this._motivationLabel.text = string.Format("{0}", Mathf.Clamp(Mathf.FloorToInt(num7), 0, 100));
			}
			float num9 = agentData.StatsTable[6];
			if (num9 <= 20f)
			{
				this._hIcon.color = new Color32(134, 17, 11, byte.MaxValue);
			}
			else
			{
				this._hIcon.color = new Color32(215, 102, 184, byte.MaxValue);
			}
			float hRate = num9 / 100f;
			if (this._pulseDisposable != null)
			{
				this._pulseDisposable.Dispose();
			}
			this._pulseDisposable = ObservableEasing.Linear(1f, true).FrameTimeInterval(true).Repeat<TimeInterval<float>>().TakeUntilDestroy(this._hIcon).Subscribe(delegate(TimeInterval<float> x)
			{
				this._hIcon.transform.localScale = Vector3.one * Mathf.Lerp(this._hIconMin, this._hIconMax, hRate) * this._hCurve.Evaluate(x.Value);
			});
			this._hLabel.text = string.Format("{0}", Mathf.Clamp(Mathf.FloorToInt((float)((int)num9)), 0, 100));
			int id2 = agentData.SickState.ID;
			this._sickIcon.gameObject.SetActive(id2 > -1);
			if (id2 > -1)
			{
				string arg = AIProject.Definitions.Sickness.NameTable[id2];
				this._sickNameLabel.text = string.Format("{0}", arg);
				Sprite sprite3;
				if (Singleton<Manager.Resources>.Instance.itemIconTables.SickIconTable.TryGetValue(id2, out sprite3))
				{
					this._sickIcon.sprite = sprite3;
				}
				else
				{
					this._sickIcon.sprite = null;
					this._sickIcon.gameObject.SetActive(false);
				}
			}
			else
			{
				this._sickNameLabel.text = string.Empty;
			}
			this._lifestyleID.Value = fileGameInfo.lifestyle;
			this._pheromoneLabel.text = string.Format("{0}", fileGameInfo.flavorState[0]);
			this._reliabilityLabel.text = string.Format("{0}", fileGameInfo.flavorState[1]);
			this._reasonLabel.text = string.Format("{0}", fileGameInfo.flavorState[2]);
			this._instinctLabel.text = string.Format("{0}", fileGameInfo.flavorState[3]);
			this._dirtyLabel.text = string.Format("{0}", fileGameInfo.flavorState[4]);
			this._riskLabel.text = string.Format("{0}", fileGameInfo.flavorState[5]);
			this._sociabilityLabel.text = string.Format("{0}", fileGameInfo.flavorState[7]);
			this._darknessLabel.text = string.Format("{0}", fileGameInfo.flavorState[6]);
			this._skillPanel.gameObject.SetActive(true);
			foreach (Button button in this._normalSkillButtons)
			{
				button.interactable = (fileGameInfo.phase >= 2);
			}
			foreach (Button button2 in this._hSkillButtons)
			{
				button2.interactable = (fileGameInfo.phase >= 2);
			}
			this._lockParameterToggle.SetIsOnWithoutCallback(agentData.ParameterLock);
		}

		// Token: 0x06007B71 RID: 31601 RVA: 0x00340480 File Offset: 0x0033E880
		private void RefreshEquipments(int id)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			if (id != 0)
			{
				int key = id - 1;
				AgentActor agentActor;
				if (!Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(key, out agentActor))
				{
					return;
				}
				AgentData agentData = agentActor.AgentData;
				this.RefreshGloveEquipment(agentData.EquipedGloveItem);
				this.RefreshNetEquipment(agentData.EquipedNetItem);
				this.RefreshShovelEquipment(agentData.EquipedShovelItem);
				this.RefreshPickelEquipment(agentData.EquipedPickelItem);
				this.RefreshRodEquipment(agentData.EquipedFishingItem);
				this.RefreshHatEquipment(agentData.EquipedHeadItem);
				this.RefreshRuckEquipment(agentData.EquipedBackItem);
				this.RefreshNeckEquipment(agentData.EquipedNeckItem);
				this.RefreshLampEquipment(agentData.EquipedLampItem);
				foreach (KeyValuePair<int, int> keyValuePair in agentActor.ChaControl.fileGameInfo.normalSkill)
				{
					this.RefreshNormalSkill(keyValuePair.Key);
				}
				foreach (KeyValuePair<int, int> keyValuePair2 in agentActor.ChaControl.fileGameInfo.hSkill)
				{
					this.RefreshHSkill(keyValuePair2.Key);
				}
			}
			else
			{
				if (Singleton<Manager.Map>.Instance.Player == null || Singleton<Manager.Map>.Instance.Player.PlayerData == null)
				{
					return;
				}
				PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
				this.RefreshGloveEquipment(playerData.EquipedGloveItem);
				this.RefreshNetEquipment(playerData.EquipedNetItem);
				this.RefreshShovelEquipment(playerData.EquipedShovelItem);
				this.RefreshPickelEquipment(playerData.EquipedPickelItem);
				this.RefreshRodEquipment(playerData.EquipedFishingItem);
				this.RefreshHatEquipment(playerData.EquipedHeadItem);
				this.RefreshRuckEquipment(playerData.EquipedBackItem);
				this.RefreshNeckEquipment(playerData.EquipedNeckItem);
				this.RefreshLampEquipment(playerData.EquipedLampItem);
			}
		}

		// Token: 0x06007B72 RID: 31602 RVA: 0x003406A0 File Offset: 0x0033EAA0
		private void RefreshGloveEquipment(StuffItem info)
		{
			Dictionary<int, int> dictionary;
			if (!Singleton<Manager.Resources>.Instance.itemIconTables.EquipmentIconTable.TryGetValue(0, out dictionary))
			{
				return;
			}
			CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
			int key;
			if (info.ID == itemIDDefine.RareGloveID.itemID)
			{
				key = dictionary[1];
			}
			else if (info.ID == itemIDDefine.SRareGloveID.itemID)
			{
				key = dictionary[2];
			}
			else
			{
				key = dictionary[0];
			}
			this._handEQImage.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable[key];
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x00340758 File Offset: 0x0033EB58
		private void RefreshNetEquipment(StuffItem info)
		{
			Dictionary<int, int> dictionary;
			if (!Singleton<Manager.Resources>.Instance.itemIconTables.EquipmentIconTable.TryGetValue(5, out dictionary))
			{
				return;
			}
			CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
			int key;
			if (info.ID == itemIDDefine.NetID.itemID)
			{
				key = dictionary[0];
			}
			else if (info.ID == itemIDDefine.RareNetID.itemID)
			{
				key = dictionary[1];
			}
			else if (info.ID == itemIDDefine.SRareNetID.itemID)
			{
				key = dictionary[2];
			}
			else
			{
				key = -1;
			}
			Sprite sprite;
			if (Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key, out sprite))
			{
				this._netEQImage.sprite = sprite;
			}
			else
			{
				this._netEQImage.sprite = this._noneSelectSprite;
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B74 RID: 31604 RVA: 0x00340850 File Offset: 0x0033EC50
		private void RefreshShovelEquipment(StuffItem info)
		{
			Dictionary<int, int> dictionary;
			if (!Singleton<Manager.Resources>.Instance.itemIconTables.EquipmentIconTable.TryGetValue(3, out dictionary))
			{
				return;
			}
			CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
			int key;
			if (info.ID == itemIDDefine.ShovelID.itemID)
			{
				key = dictionary[0];
			}
			else if (info.ID == itemIDDefine.RareShovelID.itemID)
			{
				key = dictionary[1];
			}
			else if (info.ID == itemIDDefine.SRareShovelID.itemID)
			{
				key = dictionary[2];
			}
			else
			{
				key = -1;
			}
			Sprite sprite;
			if (Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key, out sprite))
			{
				this._shovelEQImage.sprite = sprite;
			}
			else
			{
				this._shovelEQImage.sprite = this._noneSelectSprite;
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B75 RID: 31605 RVA: 0x00340948 File Offset: 0x0033ED48
		private void RefreshPickelEquipment(StuffItem info)
		{
			Dictionary<int, int> dictionary;
			if (!Singleton<Manager.Resources>.Instance.itemIconTables.EquipmentIconTable.TryGetValue(4, out dictionary))
			{
				return;
			}
			CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
			int key;
			if (info.ID == itemIDDefine.PickelID.itemID)
			{
				key = dictionary[0];
			}
			else if (info.ID == itemIDDefine.RarePickelID.itemID)
			{
				key = dictionary[1];
			}
			else if (info.ID == itemIDDefine.SRarePickelID.itemID)
			{
				key = dictionary[2];
			}
			else
			{
				key = -1;
			}
			Sprite sprite;
			if (Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key, out sprite))
			{
				this._pickelEQImage.sprite = sprite;
			}
			else
			{
				this._pickelEQImage.sprite = this._noneSelectSprite;
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B76 RID: 31606 RVA: 0x00340A40 File Offset: 0x0033EE40
		private void RefreshRodEquipment(StuffItem info)
		{
			Dictionary<int, int> dictionary;
			if (!Singleton<Manager.Resources>.Instance.itemIconTables.EquipmentIconTable.TryGetValue(6, out dictionary))
			{
				return;
			}
			CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
			int key;
			if (info.ID == itemIDDefine.RodID.itemID)
			{
				key = dictionary[0];
			}
			else
			{
				key = -1;
			}
			Sprite sprite;
			if (Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key, out sprite))
			{
				this._rodEQImage.sprite = sprite;
			}
			else
			{
				this._rodEQImage.sprite = this._noneSelectSprite;
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B77 RID: 31607 RVA: 0x00340AE8 File Offset: 0x0033EEE8
		private void RefreshHatEquipment(StuffItem info)
		{
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(info.CategoryID, info.ID);
			if (this._hatEQImage != null)
			{
				if (item != null)
				{
					Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, item.IconID, this._hatEQImage, true);
				}
				else
				{
					this._hatEQImage.sprite = this._noneSelectSprite;
				}
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B78 RID: 31608 RVA: 0x00340B58 File Offset: 0x0033EF58
		private void RefreshRuckEquipment(StuffItem info)
		{
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(info.CategoryID, info.ID);
			if (this._hatEQImage != null)
			{
				if (item != null)
				{
					Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, item.IconID, this._ruckEQImage, true);
				}
				else
				{
					this._ruckEQImage.sprite = this._noneSelectSprite;
				}
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x00340BC8 File Offset: 0x0033EFC8
		private void RefreshNeckEquipment(StuffItem info)
		{
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(info.CategoryID, info.ID);
			if (this._necklaceEQImage != null)
			{
				if (item != null)
				{
					Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, item.IconID, this._necklaceEQImage, true);
				}
				else
				{
					this._necklaceEQImage.sprite = this._noneSelectSprite;
				}
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B7A RID: 31610 RVA: 0x00340C38 File Offset: 0x0033F038
		private void RefreshLampEquipment(StuffItem info)
		{
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(info.CategoryID, info.ID);
			if (this._lampEQImage != null)
			{
				if (item != null)
				{
					Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, item.IconID, this._lampEQImage, true);
				}
				else
				{
					this._lampEQImage.sprite = this._noneSelectSprite;
				}
			}
			this.RefreshEquipmentExplanation();
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x00340CA8 File Offset: 0x0033F0A8
		private void RefreshAccessory(int id, ChaControlDefine.ExtraAccessoryParts parts, StuffItem info)
		{
			int id2 = (info.CategoryID == -1) ? -1 : info.ID;
			if (id != 0)
			{
				int key = id - 1;
				AgentActor agentActor = Singleton<Manager.Map>.Instance.AgentTable[key];
				agentActor.ChaControl.ChangeExtraAccessory(parts, id2);
				agentActor.ChaControl.ShowExtraAccessory(parts, true);
			}
			else
			{
				Singleton<Manager.Map>.Instance.Player.ChaControl.ChangeExtraAccessory(parts, id2);
				Singleton<Manager.Map>.Instance.Player.ChaControl.ShowExtraAccessory(parts, true);
			}
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x00340D3E File Offset: 0x0033F13E
		private void RefreshEquipmentExplanation()
		{
			this.RefreshEquipmentExplanation(this._eqFocusID.Value);
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x00340D54 File Offset: 0x0033F154
		private void RefreshEquipmentExplanation(int id)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			StuffItem stuffItem = null;
			if (Singleton<Manager.Map>.IsInstance())
			{
				int value = this._selectedID.Value;
				if (value != 0)
				{
					int key = this._selectedID.Value - 1;
					AgentActor agentActor;
					if (!Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(key, out agentActor))
					{
						return;
					}
					AgentData agentData = agentActor.AgentData;
					switch (id)
					{
					case 0:
						stuffItem = agentData.EquipedGloveItem;
						break;
					case 1:
						stuffItem = agentData.EquipedNetItem;
						break;
					case 2:
						stuffItem = agentData.EquipedShovelItem;
						break;
					case 3:
						stuffItem = agentData.EquipedPickelItem;
						break;
					case 4:
						stuffItem = agentData.EquipedFishingItem;
						break;
					case 5:
						stuffItem = agentData.EquipedHeadItem;
						break;
					case 6:
						stuffItem = agentData.EquipedBackItem;
						break;
					case 7:
						stuffItem = agentData.EquipedNeckItem;
						break;
					case 8:
						stuffItem = agentData.EquipedLampItem;
						break;
					}
				}
				else if (Singleton<Manager.Map>.Instance.Player != null)
				{
					PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
					switch (id)
					{
					case 0:
						stuffItem = playerData.EquipedGloveItem;
						break;
					case 1:
						stuffItem = playerData.EquipedNetItem;
						break;
					case 2:
						stuffItem = playerData.EquipedShovelItem;
						break;
					case 3:
						stuffItem = playerData.EquipedPickelItem;
						break;
					case 4:
						stuffItem = playerData.EquipedFishingItem;
						break;
					case 5:
						stuffItem = playerData.EquipedHeadItem;
						break;
					case 6:
						stuffItem = playerData.EquipedBackItem;
						break;
					case 7:
						stuffItem = playerData.EquipedNeckItem;
						break;
					case 8:
						stuffItem = playerData.EquipedLampItem;
						break;
					}
				}
			}
			if (stuffItem != null)
			{
				StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(stuffItem.CategoryID, stuffItem.ID);
				if (item != null)
				{
					text = item.Name;
					text2 = item.Explanation;
				}
			}
			this._equipItemNameLabel.text = text;
			this._equipItemText.text = text2;
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x00340F8C File Offset: 0x0033F38C
		private void RefreshNormalSkill(int id)
		{
			if (id >= this._normalSkillTexts.Length)
			{
				return;
			}
			int key = this._selectedID.Value - 1;
			AgentActor agentActor;
			if (!Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(key, out agentActor))
			{
				return;
			}
			int id2 = agentActor.ChaControl.fileGameInfo.normalSkill[id];
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(16, id2);
			this._normalSkillTexts[id].text = (((item != null) ? item.Name : null) ?? "ーーーーー");
			this.RefreshSkillExplanation();
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x00341028 File Offset: 0x0033F428
		private void RefreshHSkill(int id)
		{
			if (id >= this._hSkillTexts.Length)
			{
				return;
			}
			int key = this._selectedID.Value - 1;
			AgentActor agentActor;
			if (!Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(key, out agentActor))
			{
				return;
			}
			int id2 = agentActor.ChaControl.fileGameInfo.hSkill[id];
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(17, id2);
			this._hSkillTexts[id].text = (((item != null) ? item.Name : null) ?? "ーーーーー");
			this.RefreshSkillExplanation();
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x003410C2 File Offset: 0x0033F4C2
		private void RefreshSkillExplanation()
		{
			this.RefreshSkillExplanation(this._skillFocusID.Value);
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x003410D8 File Offset: 0x0033F4D8
		private void RefreshSkillExplanation(int id)
		{
			if (id < 0)
			{
				return;
			}
			int key = this._selectedID.Value - 1;
			AgentActor agentActor;
			if (!Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(key, out agentActor))
			{
				return;
			}
			if (id < this._normalSkillButtons.Length)
			{
				StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(16, agentActor.ChaControl.fileGameInfo.normalSkill[id]);
				if (item != null)
				{
					this._skillNameLabel.text = item.Name;
					this._skillFlavorText.text = item.Explanation;
				}
				else
				{
					this._skillNameLabel.text = string.Empty;
					this._skillFlavorText.text = string.Empty;
				}
			}
			else
			{
				int key2 = id - this._normalSkillButtons.Length;
				StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(17, agentActor.ChaControl.fileGameInfo.hSkill[key2]);
				if (item2 != null)
				{
					this._skillNameLabel.text = item2.Name;
					this._skillFlavorText.text = item2.Explanation;
				}
				else
				{
					this._skillNameLabel.text = string.Empty;
					this._skillFlavorText.text = string.Empty;
				}
			}
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x00341220 File Offset: 0x0033F620
		private void OnUpdate()
		{
			Button button = this._charaButtons[this._focusID];
			this._tabFocusImage.transform.position = button.transform.position;
			Button button2 = this._charaButtons[this._selectedID.Value];
			this._tabSelectionImage.rectTransform.position = button2.transform.position;
			if (this._playerContent.gameObject.activeSelf && Singleton<Game>.IsInstance() && Singleton<Game>.Instance.Environment != null)
			{
				TimeSpan timeSpan = Singleton<Game>.Instance.Environment.TotalPlayTime.TimeSpan;
				this._totalPlayingTimeLabel.text = string.Format("{0} : {1:00} : {2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
			}
			if (this._skillFocusID.Value > -1)
			{
				if (this._skillFocusID.Value < this._normalSkillButtons.Length)
				{
					Button button3 = this._normalSkillButtons[this._skillFocusID.Value];
					Vector3 position = new Vector3(this._skillFocusImage.transform.position.x, button3.transform.position.y, 0f);
					this._skillFocusImage.transform.position = position;
				}
				else
				{
					int num = this._skillFocusID.Value - this._normalSkillButtons.Length;
					Button button4 = this._hSkillButtons[num];
					Vector3 position2 = new Vector3(this._skillFocusImage.transform.position.x, button4.transform.position.y, 0f);
					this._skillFocusImage.transform.position = position2;
				}
			}
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x00341400 File Offset: 0x0033F800
		private void ChangeBackground(int id)
		{
			if (this._backgroundDisposables == null)
			{
				int num = this._equipmentBackgrounds.Count + this._equipmentFlavorBackgrounds.Count + this._skillBackgrounds.Count + this._skillFlavorBackgrounds.Count;
				this._backgroundDisposables = new IDisposable[num];
			}
			foreach (IDisposable disposable in this._backgroundDisposables)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			IObservable<TimeInterval<float>> source = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			int num2 = 0;
			using (Dictionary<int, CanvasGroup>.Enumerator enumerator = this._equipmentBackgrounds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StatusUI.<ChangeBackground>c__AnonStorey1A <ChangeBackground>c__AnonStorey1A = new StatusUI.<ChangeBackground>c__AnonStorey1A();
					<ChangeBackground>c__AnonStorey1A.kvp = enumerator.Current;
					float startAlpha = <ChangeBackground>c__AnonStorey1A.kvp.Value.alpha;
					int destAlpha = (<ChangeBackground>c__AnonStorey1A.kvp.Key != id) ? 0 : 1;
					this._backgroundDisposables[num2++] = source.Subscribe(delegate(TimeInterval<float> x)
					{
						<ChangeBackground>c__AnonStorey1A.kvp.Value.alpha = Mathf.Lerp(startAlpha, (float)destAlpha, x.Value);
					});
				}
			}
			using (Dictionary<int, CanvasGroup>.Enumerator enumerator2 = this._equipmentFlavorBackgrounds.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					StatusUI.<ChangeBackground>c__AnonStorey1C <ChangeBackground>c__AnonStorey1C = new StatusUI.<ChangeBackground>c__AnonStorey1C();
					<ChangeBackground>c__AnonStorey1C.kvp = enumerator2.Current;
					float startAlpha = <ChangeBackground>c__AnonStorey1C.kvp.Value.alpha;
					int destAlpha = (<ChangeBackground>c__AnonStorey1C.kvp.Key != id) ? 0 : 1;
					this._backgroundDisposables[num2++] = source.Subscribe(delegate(TimeInterval<float> x)
					{
						<ChangeBackground>c__AnonStorey1C.kvp.Value.alpha = Mathf.Lerp(startAlpha, (float)destAlpha, x.Value);
					});
				}
			}
			using (Dictionary<int, CanvasGroup>.Enumerator enumerator3 = this._skillBackgrounds.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					StatusUI.<ChangeBackground>c__AnonStorey1E <ChangeBackground>c__AnonStorey1E = new StatusUI.<ChangeBackground>c__AnonStorey1E();
					<ChangeBackground>c__AnonStorey1E.kvp = enumerator3.Current;
					float startAlpha = <ChangeBackground>c__AnonStorey1E.kvp.Value.alpha;
					int destAlpha = (<ChangeBackground>c__AnonStorey1E.kvp.Key != id) ? 0 : 1;
					this._backgroundDisposables[num2++] = source.Subscribe(delegate(TimeInterval<float> x)
					{
						<ChangeBackground>c__AnonStorey1E.kvp.Value.alpha = Mathf.Lerp(startAlpha, (float)destAlpha, x.Value);
					});
				}
			}
			using (Dictionary<int, CanvasGroup>.Enumerator enumerator4 = this._skillFlavorBackgrounds.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					StatusUI.<ChangeBackground>c__AnonStorey20 <ChangeBackground>c__AnonStorey2 = new StatusUI.<ChangeBackground>c__AnonStorey20();
					<ChangeBackground>c__AnonStorey2.kvp = enumerator4.Current;
					float startAlpha = <ChangeBackground>c__AnonStorey2.kvp.Value.alpha;
					int destAlpha = (<ChangeBackground>c__AnonStorey2.kvp.Key != id) ? 0 : 1;
					this._backgroundDisposables[num2++] = source.Subscribe(delegate(TimeInterval<float> x)
					{
						<ChangeBackground>c__AnonStorey2.kvp.Value.alpha = Mathf.Lerp(startAlpha, (float)destAlpha, x.Value);
					});
				}
			}
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x0034178C File Offset: 0x0033FB8C
		private void EquipmentSeq(StuffItem destItem, int categoryID, int[] itemIDs, Action<StuffItem> onSubmit)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			SystemMenuUI systemMenuUI = MapUIContainer.SystemMenuUI;
			InventoryUIController inventoryUI = systemMenuUI.InventoryEnterUI;
			inventoryUI.isConfirm = false;
			inventoryUI.CountViewerVisible(false);
			inventoryUI.EmptyTextAutoVisible(true);
			inventoryUI.SetItemFilter(new InventoryFacadeViewer.ItemFilter[]
			{
				new InventoryFacadeViewer.ItemFilter(categoryID, itemIDs)
			});
			PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
			inventoryUI.itemList = (() => playerData.ItemList);
			if (Singleton<Manager.Resources>.Instance.GameInfo.GetItem(destItem.CategoryID, destItem.ID) != null)
			{
				inventoryUI.itemList_System = (() => new List<StuffItem>
				{
					StuffItem.CreateSystemItem(0, 0, 1)
				});
			}
			inventoryUI.DoubleClickAction(null);
			inventoryUI.OnSubmit = delegate(StuffItem item)
			{
				InventoryUIController inventoryUI = inventoryUI;
				if (inventoryUI != null)
				{
					inventoryUI.OnClose();
				}
				StuffItem stuffItem = new StuffItem(destItem);
				Action<StuffItem> onSubmit2 = onSubmit;
				if (onSubmit2 != null)
				{
					onSubmit2(item);
				}
				if (Singleton<Manager.Resources>.Instance.GameInfo.GetItem(stuffItem.CategoryID, stuffItem.ID) != null)
				{
					playerData.ItemList.AddItem(stuffItem);
				}
			};
			inventoryUI.OnClose = delegate()
			{
				inventoryUI.itemList_System = null;
				inventoryUI.OnSubmit = null;
				inventoryUI.IsActiveControl = false;
				Singleton<Manager.Input>.Instance.FocusLevel = 0;
				this.IsActiveControl = true;
				inventoryUI.OnClose = null;
			};
			this.OpenID = this._selectedID.Value;
			this.IsActiveControl = false;
			this.Observer.OpenModeMenu(SystemMenuUI.MenuMode.InventoryEnter);
		}

		// Token: 0x06007B85 RID: 31621 RVA: 0x003418F4 File Offset: 0x0033FCF4
		private void SkillEquipmentSeq(bool isHSkill, int categoryID, Action<StuffItem> onSubmit)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			SystemMenuUI systemMenuUI = MapUIContainer.SystemMenuUI;
			InventoryUIController inventoryUI = systemMenuUI.InventoryEnterUI;
			inventoryUI.isConfirm = true;
			inventoryUI.CountViewerVisible(false);
			inventoryUI.EmptyTextAutoVisible(true);
			AgentActor agentActor = Singleton<Manager.Map>.Instance.AgentTable[this._selectedID.Value - 1];
			Dictionary<int, int> skill = (!isHSkill) ? agentActor.ChaControl.fileGameInfo.normalSkill : agentActor.ChaControl.fileGameInfo.hSkill;
			int[] ids = (from v in Singleton<Manager.Resources>.Instance.GameInfo.GetItemTable(categoryID)
			select v.Key into id
			where !skill.ContainsValue(id)
			select id).ToArray<int>();
			inventoryUI.SetItemFilter(new InventoryFacadeViewer.ItemFilter[]
			{
				new InventoryFacadeViewer.ItemFilter(categoryID, ids)
			});
			PlayerData playerData = Singleton<Manager.Map>.Instance.Player.PlayerData;
			inventoryUI.itemList = (() => playerData.ItemList);
			inventoryUI.DoubleClickAction(null);
			inventoryUI.OnSubmit = delegate(StuffItem item)
			{
				InventoryUIController inventoryUI = inventoryUI;
				if (inventoryUI != null)
				{
					inventoryUI.OnClose();
				}
				Action<StuffItem> onSubmit2 = onSubmit;
				if (onSubmit2 != null)
				{
					onSubmit2(item);
				}
			};
			inventoryUI.OnClose = delegate()
			{
				inventoryUI.OnSubmit = null;
				inventoryUI.IsActiveControl = false;
				Singleton<Manager.Input>.Instance.FocusLevel = 0;
				this.IsActiveControl = true;
				inventoryUI.OnClose = null;
			};
			this.OpenID = this._selectedID.Value;
			this.IsActiveControl = false;
			this.Observer.OpenModeMenu(SystemMenuUI.MenuMode.InventoryEnter);
		}

		// Token: 0x06007B86 RID: 31622 RVA: 0x00341AA4 File Offset: 0x0033FEA4
		private void ChangeGloveEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshGloveEquipment(dest);
		}

		// Token: 0x06007B87 RID: 31623 RVA: 0x00341B08 File Offset: 0x0033FF08
		private void ChangeNetEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshNetEquipment(dest);
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x00341B6C File Offset: 0x0033FF6C
		private void ChangeShovelEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshShovelEquipment(dest);
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x00341BD0 File Offset: 0x0033FFD0
		private void ChangePickelEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshPickelEquipment(dest);
		}

		// Token: 0x06007B8A RID: 31626 RVA: 0x00341C34 File Offset: 0x00340034
		private void ChangeRodEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshRodEquipment(dest);
		}

		// Token: 0x06007B8B RID: 31627 RVA: 0x00341C98 File Offset: 0x00340098
		private void ChangeHatEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshHatEquipment(dest);
			this.RefreshAccessory(this._selectedID.Value, ChaControlDefine.ExtraAccessoryParts.Head, dest);
		}

		// Token: 0x06007B8C RID: 31628 RVA: 0x00341D10 File Offset: 0x00340110
		private void ChangeRuckEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshRuckEquipment(dest);
			this.RefreshAccessory(this._selectedID.Value, ChaControlDefine.ExtraAccessoryParts.Back, dest);
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x00341D88 File Offset: 0x00340188
		private void ChangeNecklaceEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshNeckEquipment(dest);
			this.RefreshAccessory(this._selectedID.Value, ChaControlDefine.ExtraAccessoryParts.Neck, dest);
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x00341E00 File Offset: 0x00340200
		private void ChangeLampEquipment(StuffItem dest, StuffItem source)
		{
			if (source.CategoryID == 0)
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = -1;
				dest.Count = 0;
			}
			else
			{
				dest.CategoryID = source.CategoryID;
				dest.ID = source.ID;
				dest.Count = source.Count;
			}
			this.RefreshLampEquipment(dest);
			if (this._selectedID.Value > 0)
			{
				int key = this._selectedID.Value - 1;
				AgentActor agentActor = Singleton<Manager.Map>.Instance.AgentTable[key];
				int id = (dest.CategoryID == -1) ? -1 : dest.ID;
				ChaControlDefine.ExtraAccessoryParts parts = ChaControlDefine.ExtraAccessoryParts.Waist;
				agentActor.ChaControl.ChangeExtraAccessory(parts, id);
				agentActor.ChaControl.ShowExtraAccessory(parts, Singleton<Manager.Map>.Instance.Simulator.TimeZone == TimeZone.Night);
			}
		}

		// Token: 0x06007B8F RID: 31631 RVA: 0x00341ED8 File Offset: 0x003402D8
		public void UsageRestriction()
		{
			bool tutorialMode = Manager.Map.TutorialMode;
			this.SetInteractable(this._rightButton, !tutorialMode);
			this.SetInteractable(this._leftButton, !tutorialMode);
			if (!this._charaButtons.IsNullOrEmpty<Button>())
			{
				for (int i = 0; i < this._charaButtons.Length; i++)
				{
					if (i != 0)
					{
						Button button = this._charaButtons[i];
						if (!(button == null))
						{
							bool activeSelf = button.gameObject.activeSelf;
							bool flag = !tutorialMode;
							if (activeSelf != flag)
							{
								button.gameObject.SetActive(flag);
							}
						}
					}
				}
			}
		}

		// Token: 0x06007B90 RID: 31632 RVA: 0x00341F82 File Offset: 0x00340382
		private bool SetInteractable(Selectable tar, bool active)
		{
			if (tar == null)
			{
				return false;
			}
			if (tar.interactable != active)
			{
				tar.interactable = active;
				return true;
			}
			return false;
		}

		// Token: 0x040062DE RID: 25310
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040062DF RID: 25311
		[SerializeField]
		private CanvasGroup _contentCanvasGroup;

		// Token: 0x040062E0 RID: 25312
		private RectTransform _activeContent;

		// Token: 0x040062E1 RID: 25313
		[SerializeField]
		private RectTransform _playerContent;

		// Token: 0x040062E2 RID: 25314
		[SerializeField]
		private RectTransform _agentContent;

		// Token: 0x040062E3 RID: 25315
		[SerializeField]
		private Button _leftButton;

		// Token: 0x040062E4 RID: 25316
		[SerializeField]
		private Button _rightButton;

		// Token: 0x040062E5 RID: 25317
		[SerializeField]
		private Button[] _charaButtons;

		// Token: 0x040062E6 RID: 25318
		[SerializeField]
		private Image _tabFocusImage;

		// Token: 0x040062E7 RID: 25319
		[SerializeField]
		private Image _tabSelectionImage;

		// Token: 0x040062E8 RID: 25320
		[SerializeField]
		private Dictionary<int, CanvasGroup> _equipmentBackgrounds = new Dictionary<int, CanvasGroup>();

		// Token: 0x040062E9 RID: 25321
		[SerializeField]
		private Dictionary<int, CanvasGroup> _equipmentFlavorBackgrounds = new Dictionary<int, CanvasGroup>();

		// Token: 0x040062EA RID: 25322
		[SerializeField]
		private Dictionary<int, CanvasGroup> _skillBackgrounds = new Dictionary<int, CanvasGroup>();

		// Token: 0x040062EB RID: 25323
		[SerializeField]
		private Dictionary<int, CanvasGroup> _skillFlavorBackgrounds = new Dictionary<int, CanvasGroup>();

		// Token: 0x040062EC RID: 25324
		[SerializeField]
		private Image _charaIconImage;

		// Token: 0x040062ED RID: 25325
		[SerializeField]
		private Text _nameLabel;

		// Token: 0x040062EE RID: 25326
		[SerializeField]
		private RawImage _cardRawImage;

		// Token: 0x040062EF RID: 25327
		[SerializeField]
		private Image[] _phaseImages;

		// Token: 0x040062F0 RID: 25328
		[SerializeField]
		private Sprite _phaseActiveSprite;

		// Token: 0x040062F1 RID: 25329
		[SerializeField]
		private Sprite _phaseInactiveSprite;

		// Token: 0x040062F2 RID: 25330
		[SerializeField]
		private Toggle _lockParameterToggle;

		// Token: 0x040062F3 RID: 25331
		[SerializeField]
		private Text _sexLabel;

		// Token: 0x040062F4 RID: 25332
		[SerializeField]
		private Text _inventoryMaxLabel;

		// Token: 0x040062F5 RID: 25333
		[SerializeField]
		private Text _fishingLevelLabel;

		// Token: 0x040062F6 RID: 25334
		[SerializeField]
		private Text _totalPlayingTimeLabel;

		// Token: 0x040062F7 RID: 25335
		[SerializeField]
		private Button _slotExtendButton;

		// Token: 0x040062F8 RID: 25336
		[SerializeField]
		private Image _fishingExperienceImage;

		// Token: 0x040062F9 RID: 25337
		[SerializeField]
		[Header("ステータス")]
		private GameObject _moodContent;

		// Token: 0x040062FA RID: 25338
		[SerializeField]
		private GameObject _motivationContent;

		// Token: 0x040062FB RID: 25339
		[SerializeField]
		private Image _tempImage;

		// Token: 0x040062FC RID: 25340
		[SerializeField]
		private Image[] _tempBorderImages;

		// Token: 0x040062FD RID: 25341
		[SerializeField]
		private Slider _tempSlider;

		// Token: 0x040062FE RID: 25342
		[SerializeField]
		private Image _hungerIcon;

		// Token: 0x040062FF RID: 25343
		[SerializeField]
		private Text _hungerLabel;

		// Token: 0x04006300 RID: 25344
		[SerializeField]
		private Image _physicalIcon;

		// Token: 0x04006301 RID: 25345
		[SerializeField]
		private Text _physicalLabel;

		// Token: 0x04006302 RID: 25346
		[SerializeField]
		private Image _moodIcon;

		// Token: 0x04006303 RID: 25347
		[SerializeField]
		private Image[] _moodGraphs;

		// Token: 0x04006304 RID: 25348
		[SerializeField]
		private Image[] _moodBorderGraphs;

		// Token: 0x04006305 RID: 25349
		[SerializeField]
		private Image[] _motivationGraphs;

		// Token: 0x04006306 RID: 25350
		[SerializeField]
		private Text _motivationLabel;

		// Token: 0x04006307 RID: 25351
		[SerializeField]
		private Image _hIcon;

		// Token: 0x04006308 RID: 25352
		[SerializeField]
		private Text _hLabel;

		// Token: 0x04006309 RID: 25353
		[SerializeField]
		private float _hIconMin = 0.2f;

		// Token: 0x0400630A RID: 25354
		[SerializeField]
		private float _hIconMax = 1f;

		// Token: 0x0400630B RID: 25355
		[SerializeField]
		private AnimationCurve _hCurve = new AnimationCurve
		{
			keys = new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(0.5f, 1.2f),
				new Keyframe(0.9f, 0.9f),
				new Keyframe(1f, 1f)
			}
		};

		// Token: 0x0400630C RID: 25356
		[SerializeField]
		private Text _sickNameLabel;

		// Token: 0x0400630D RID: 25357
		[SerializeField]
		private Image _sickIcon;

		// Token: 0x0400630E RID: 25358
		[SerializeField]
		[Header("フレーバースキル")]
		private Text _lifeStyleLabel;

		// Token: 0x0400630F RID: 25359
		[SerializeField]
		private Text _pheromoneLabel;

		// Token: 0x04006310 RID: 25360
		[SerializeField]
		private Text _reliabilityLabel;

		// Token: 0x04006311 RID: 25361
		[SerializeField]
		private Text _reasonLabel;

		// Token: 0x04006312 RID: 25362
		[SerializeField]
		private Text _instinctLabel;

		// Token: 0x04006313 RID: 25363
		[SerializeField]
		private Text _dirtyLabel;

		// Token: 0x04006314 RID: 25364
		[SerializeField]
		private Text _riskLabel;

		// Token: 0x04006315 RID: 25365
		[SerializeField]
		private Text _darknessLabel;

		// Token: 0x04006316 RID: 25366
		[SerializeField]
		private Text _sociabilityLabel;

		// Token: 0x04006317 RID: 25367
		[SerializeField]
		private Button _handEQButton;

		// Token: 0x04006318 RID: 25368
		[SerializeField]
		private Button _netEQButton;

		// Token: 0x04006319 RID: 25369
		[SerializeField]
		private Button _shovelEQButton;

		// Token: 0x0400631A RID: 25370
		[SerializeField]
		private Button _pickelEQButton;

		// Token: 0x0400631B RID: 25371
		[SerializeField]
		private Button _rodEQButton;

		// Token: 0x0400631C RID: 25372
		[SerializeField]
		private Button _hatEQButton;

		// Token: 0x0400631D RID: 25373
		[SerializeField]
		private Button _ruckEQButton;

		// Token: 0x0400631E RID: 25374
		[SerializeField]
		private Button _necklaceEQButton;

		// Token: 0x0400631F RID: 25375
		[SerializeField]
		private Button _lampEQButton;

		// Token: 0x04006320 RID: 25376
		[SerializeField]
		private Image _handEQImage;

		// Token: 0x04006321 RID: 25377
		[SerializeField]
		private Image _netEQImage;

		// Token: 0x04006322 RID: 25378
		[SerializeField]
		private Image _shovelEQImage;

		// Token: 0x04006323 RID: 25379
		[SerializeField]
		private Image _pickelEQImage;

		// Token: 0x04006324 RID: 25380
		[SerializeField]
		private Image _rodEQImage;

		// Token: 0x04006325 RID: 25381
		[SerializeField]
		private Image _hatEQImage;

		// Token: 0x04006326 RID: 25382
		[SerializeField]
		private Image _ruckEQImage;

		// Token: 0x04006327 RID: 25383
		[SerializeField]
		private Image _necklaceEQImage;

		// Token: 0x04006328 RID: 25384
		[SerializeField]
		private Image _lampEQImage;

		// Token: 0x04006329 RID: 25385
		[SerializeField]
		private Sprite _noneSelectSprite;

		// Token: 0x0400632A RID: 25386
		[SerializeField]
		private Text _equipItemNameLabel;

		// Token: 0x0400632B RID: 25387
		[SerializeField]
		private Text _equipItemText;

		// Token: 0x0400632C RID: 25388
		private IntReactiveProperty _eqFocusID = new IntReactiveProperty(-1);

		// Token: 0x0400632D RID: 25389
		[SerializeField]
		private RectTransform _skillPanel;

		// Token: 0x0400632E RID: 25390
		private IntReactiveProperty _skillFocusID = new IntReactiveProperty(-1);

		// Token: 0x0400632F RID: 25391
		private int _skillSelectedID;

		// Token: 0x04006330 RID: 25392
		[SerializeField]
		private Image _skillFocusImage;

		// Token: 0x04006331 RID: 25393
		[SerializeField]
		private Button[] _normalSkillButtons;

		// Token: 0x04006332 RID: 25394
		[SerializeField]
		private Button[] _hSkillButtons;

		// Token: 0x04006333 RID: 25395
		[SerializeField]
		private Text[] _normalSkillTexts;

		// Token: 0x04006334 RID: 25396
		[SerializeField]
		private Text[] _hSkillTexts;

		// Token: 0x04006335 RID: 25397
		[SerializeField]
		private Text _skillNameLabel;

		// Token: 0x04006336 RID: 25398
		[SerializeField]
		private Text _skillFlavorText;

		// Token: 0x04006337 RID: 25399
		private IntReactiveProperty _selectedID = new IntReactiveProperty(0);

		// Token: 0x04006338 RID: 25400
		private IConnectableObservable<int> _selectIDChange;

		// Token: 0x04006339 RID: 25401
		private int _focusID;

		// Token: 0x0400633B RID: 25403
		private IDisposable _fadeSubscriber;

		// Token: 0x0400633C RID: 25404
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x0400633D RID: 25405
		private IDisposable _contentChangeDisposable;

		// Token: 0x0400633E RID: 25406
		private IDisposable _pulseDisposable;

		// Token: 0x0400633F RID: 25407
		private IDisposable[] _backgroundDisposables;
	}
}
