using System;
using System.Text;
using AIChara;
using AIProject;
using AIProject.ColorDefine;
using AIProject.SaveData;
using Manager;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B0D RID: 2829
public class HSceneSpriteAccessoryCondition : MonoBehaviour
{
	// Token: 0x0600530A RID: 21258 RVA: 0x00244A64 File Offset: 0x00242E64
	public void Init()
	{
		this.hScene = Singleton<HSceneFlagCtrl>.Instance.GetComponent<HScene>();
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		this.females = this.hScene.GetFemales();
		this.Males = this.hScene.GetMales();
		for (int i = 0; i < 4; i++)
		{
			ChaControl chaControl;
			if (i < 2)
			{
				chaControl = this.females[i];
			}
			else
			{
				chaControl = this.Males[i - 2];
			}
			if (!(chaControl == null))
			{
				for (int j = 0; j < 4; j++)
				{
					if (chaControl.objExtraAccessory[j] == null)
					{
						this.before[i, j] = false;
					}
					else if (j != 3)
					{
						this.before[i, j] = chaControl.objExtraAccessory[j].activeSelf;
					}
					else if (i < 2)
					{
						if (i == 1 && this.hSceneManager.Player != null && this.hSceneManager.Player.ChaControl == chaControl)
						{
							if (this.hSceneManager.Player.EquipedItem != null && this.hSceneManager.Player.EquipedItem.AsGameObject != null)
							{
								this.before[i, j] = this.hSceneManager.Player.EquipedItem.AsGameObject.activeSelf;
							}
							else
							{
								this.before[i, j] = false;
							}
						}
						else
						{
							this.before[i, j] = this.hSceneManager.FemaleLumpActive[i];
						}
					}
					else if (this.hSceneManager.Player != null && this.hSceneManager.Player.ChaControl == chaControl)
					{
						if (this.hSceneManager.Player.EquipedItem != null && this.hSceneManager.Player.EquipedItem.AsGameObject != null)
						{
							this.before[i, j] = this.hSceneManager.Player.EquipedItem.AsGameObject.activeSelf;
						}
						else
						{
							this.before[i, j] = false;
						}
					}
					else
					{
						this.before[i, j] = chaControl.objExtraAccessory[j].activeSelf;
					}
				}
			}
		}
		this.sbAcsName = new StringBuilder();
		this.SetAccessoryCharacter(true);
		this.hSceneSpriteChaChoice.SetAction(delegate
		{
			this.SetAccessoryCharacter(false);
		});
	}

	// Token: 0x0600530B RID: 21259 RVA: 0x00244D0C File Offset: 0x0024310C
	public void SetAccessoryCharacter(bool init = false)
	{
		if (!init && !base.gameObject.activeSelf)
		{
			return;
		}
		Color color = default(Color);
		Text componentInChildren;
		for (int i = 0; i < this.AccessorySlots.GetToggleNum(); i++)
		{
			this.sbAcsName.Clear();
			int num = i;
			ListInfoBase listInfoBase;
			if (this.hSceneManager.numFemaleClothCustom < 2)
			{
				listInfoBase = this.females[this.hSceneManager.numFemaleClothCustom].infoAccessory[num];
			}
			else
			{
				listInfoBase = this.Males[this.hSceneManager.numFemaleClothCustom - 2].infoAccessory[num];
			}
			componentInChildren = this.AccessorySlots.lstToggle[num].GetComponentInChildren<Text>();
			if (listInfoBase != null)
			{
				this.sbAcsName.Append(listInfoBase.Name);
				this.AccessorySlots.lstToggle[num].GetComponent<Image>().raycastTarget = true;
				if (this.hSceneManager.numFemaleClothCustom < 2)
				{
					this.AccessorySlots.lstToggle[num].isOn = this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.showAccessory[num];
				}
				else
				{
					this.AccessorySlots.lstToggle[num].isOn = this.Males[this.hSceneManager.numFemaleClothCustom - 2].fileStatus.showAccessory[num];
				}
				if (this.AccessorySlots.lstToggle[num].isOn)
				{
					Define.Set(ref color, Colors.White, false);
					componentInChildren.color = color;
				}
				else
				{
					componentInChildren.color = new Color32(141, 136, 129, byte.MaxValue);
				}
			}
			else
			{
				this.sbAcsName.AppendFormat("スロット{0}", num);
				this.AccessorySlots.lstToggle[num].GetComponent<Image>().raycastTarget = false;
				componentInChildren.color = new Color32(141, 136, 129, byte.MaxValue);
			}
			componentInChildren.text = this.sbAcsName.ToString();
		}
		if (this.hSceneManager.numFemaleClothCustom < 2)
		{
			this.AccessorySlots.SetActive(this.females[this.hSceneManager.numFemaleClothCustom].objBodyBone != null && this.females[this.hSceneManager.numFemaleClothCustom].visibleAll, -1);
		}
		else
		{
			this.AccessorySlots.SetActive(this.Males[this.hSceneManager.numFemaleClothCustom - 2].objBodyBone != null && this.Males[this.hSceneManager.numFemaleClothCustom - 2].visibleAll, -1);
		}
		if (this.hSceneManager.numFemaleClothCustom > 1 || (this.hSceneManager.Player != null && this.hSceneManager.Player.ChaControl == this.females[this.hSceneManager.numFemaleClothCustom]))
		{
			if (!Config.HData.Accessory)
			{
				this.AllChange.isOn = false;
			}
			else
			{
				this.AllChange.isOn = true;
			}
		}
		else
		{
			this.AllChange.isOn = true;
		}
		this.allState[this.hSceneManager.numFemaleClothCustom] = this.AllChange.isOn;
		componentInChildren = this.AllChange.GetComponentInChildren<Text>();
		Color color2 = componentInChildren.color;
		if (this.allState[this.hSceneManager.numFemaleClothCustom])
		{
			Define.Set(ref color2, Colors.White, false);
		}
		else
		{
			color2 = new Color32(141, 136, 129, byte.MaxValue);
		}
		componentInChildren.color = color2;
		AgentActor agentActor = null;
		if (this.hSceneManager.numFemaleClothCustom < 2)
		{
			if (this.hSceneManager.females[this.hSceneManager.numFemaleClothCustom] != null)
			{
				agentActor = this.hSceneManager.females[this.hSceneManager.numFemaleClothCustom].GetComponent<AgentActor>();
			}
		}
		else if (this.hSceneManager.male != null)
		{
			agentActor = this.hSceneManager.male.GetComponent<AgentActor>();
		}
		AgentData agentData = (!(agentActor != null)) ? null : agentActor.AgentData;
		bool active = false;
		bool active2 = false;
		bool active3 = false;
		bool active4 = false;
		if (agentData != null)
		{
			this.player = false;
			active = (agentData.EquipedHeadItem != null && agentData.EquipedHeadItem.ID != -1);
			active2 = (agentData.EquipedBackItem != null && agentData.EquipedBackItem.ID != -1);
			active3 = (agentData.EquipedNeckItem != null && agentData.EquipedNeckItem.ID != -1);
			active4 = (agentData.EquipedLampItem != null && agentData.EquipedLampItem.ID != -1);
		}
		else
		{
			PlayerActor playerActor = null;
			if (this.hSceneManager.numFemaleClothCustom < 2)
			{
				if (this.hSceneManager.females[this.hSceneManager.numFemaleClothCustom] != null)
				{
					playerActor = this.hSceneManager.females[this.hSceneManager.numFemaleClothCustom].GetComponent<PlayerActor>();
				}
			}
			else if (this.hSceneManager.male != null)
			{
				playerActor = this.hSceneManager.male.GetComponent<PlayerActor>();
			}
			PlayerData playerData = (!(playerActor != null)) ? null : playerActor.PlayerData;
			if (playerData != null)
			{
				this.player = true;
				active = (playerData.EquipedHeadItem != null && playerData.EquipedHeadItem.ID != -1);
				active2 = (playerData.EquipedBackItem != null && playerData.EquipedBackItem.ID != -1);
				active3 = (playerData.EquipedNeckItem != null && playerData.EquipedNeckItem.ID != -1);
				active4 = (playerData.EquipedLampItem != null && playerData.EquipedLampItem.ID != -1);
			}
		}
		if (this.hSceneManager.numFemaleClothCustom < 2)
		{
			this.EquipBts[0].chara = this.females[this.hSceneManager.numFemaleClothCustom];
			this.EquipBts[1].chara = this.females[this.hSceneManager.numFemaleClothCustom];
			this.EquipBts[2].chara = this.females[this.hSceneManager.numFemaleClothCustom];
			this.EquipBts[3].chara = this.females[this.hSceneManager.numFemaleClothCustom];
		}
		else
		{
			this.EquipBts[0].chara = this.Males[this.hSceneManager.numFemaleClothCustom - 2];
			this.EquipBts[1].chara = this.Males[this.hSceneManager.numFemaleClothCustom - 2];
			this.EquipBts[2].chara = this.Males[this.hSceneManager.numFemaleClothCustom - 2];
			this.EquipBts[3].chara = this.Males[this.hSceneManager.numFemaleClothCustom - 2];
		}
		this.EquipBts[0].Base.SetActive(active);
		this.EquipBts[1].Base.SetActive(active2);
		this.EquipBts[2].Base.SetActive(active3);
		this.EquipBts[3].Base.SetActive(active4);
		this.EquipBts[0].id = 0;
		this.EquipBts[1].id = 1;
		this.EquipBts[2].id = 2;
		this.EquipBts[3].id = 3;
		if (this.EquipBts[0].chara != null && this.EquipBts[0].chara.objExtraAccessory[0] != null && this.EquipBts[0].chara.objExtraAccessory[0].activeSelf != this.EquipBts[0].active)
		{
			this.EquipBts[0].ChangeState();
		}
		if (this.EquipBts[1].chara != null && this.EquipBts[1].chara.objExtraAccessory[1] != null && this.EquipBts[1].chara.objExtraAccessory[1].activeSelf != this.EquipBts[1].active)
		{
			this.EquipBts[1].ChangeState();
		}
		if (this.EquipBts[2].chara != null && this.EquipBts[2].chara.objExtraAccessory[2] != null && this.EquipBts[2].chara.objExtraAccessory[2].activeSelf != this.EquipBts[2].active)
		{
			this.EquipBts[2].ChangeState();
		}
		if (this.EquipBts[3].chara != null)
		{
			GameObject gameObject = null;
			if (!this.player)
			{
				gameObject = this.EquipBts[3].chara.objExtraAccessory[3];
			}
			else if (this.hSceneManager.numFemaleClothCustom < 2)
			{
				if (this.hSceneManager.females[this.hSceneManager.numFemaleClothCustom].EquipedItem != null)
				{
					gameObject = this.hSceneManager.females[this.hSceneManager.numFemaleClothCustom].EquipedItem.AsGameObject;
				}
			}
			else if (this.hSceneManager.male.EquipedItem != null)
			{
				gameObject = this.hSceneManager.male.EquipedItem.AsGameObject;
			}
			if (gameObject != null && gameObject.activeSelf != this.EquipBts[3].active)
			{
				this.EquipBts[3].ChangeState();
			}
		}
		if (this.hSceneSpriteChaChoice.Content.activeSelf)
		{
			this.hSceneSpriteChaChoice.Content.SetActive(false);
		}
	}

	// Token: 0x0600530C RID: 21260 RVA: 0x00245790 File Offset: 0x00243B90
	public void OnClickAccessory(int _accessory)
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.hSceneSprite.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.AccessorySlots.GetToggleNum(); i++)
		{
			int num = i;
			if (num == _accessory)
			{
				Text componentInChildren = this.AccessorySlots.lstToggle[num].GetComponentInChildren<Text>();
				Color color = componentInChildren.color;
				if ((this.hSceneManager.numFemaleClothCustom > 1 || (this.hSceneManager.Player != null && this.hSceneManager.Player.ChaControl == this.females[this.hSceneManager.numFemaleClothCustom])) && !Config.HData.Accessory)
				{
					this.AccessorySlots.lstToggle[_accessory].isOn = false;
				}
				if (this.AccessorySlots.lstToggle[_accessory].isOn)
				{
					Define.Set(ref color, Colors.White, false);
				}
				else
				{
					color = new Color32(141, 136, 129, byte.MaxValue);
				}
				componentInChildren.color = color;
				break;
			}
		}
		if (this.hSceneManager.numFemaleClothCustom < 2)
		{
			this.females[this.hSceneManager.numFemaleClothCustom].SetAccessoryState(_accessory, this.AccessorySlots.lstToggle[_accessory].isOn);
		}
		else
		{
			this.Males[this.hSceneManager.numFemaleClothCustom - 2].SetAccessoryState(_accessory, this.AccessorySlots.lstToggle[_accessory].isOn);
		}
	}

	// Token: 0x0600530D RID: 21261 RVA: 0x00245970 File Offset: 0x00243D70
	public void OnClickAllAccessory()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.hSceneSprite.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if ((this.hSceneManager.numFemaleClothCustom > 1 || (this.hSceneManager.Player != null && this.hSceneManager.Player.ChaControl == this.females[this.hSceneManager.numFemaleClothCustom])) && !Config.HData.Accessory)
		{
			this.AllChange.isOn = false;
		}
		this.allState[this.hSceneManager.numFemaleClothCustom] = this.AllChange.isOn;
		Text componentInChildren;
		for (int i = 0; i < this.AccessorySlots.lstToggle.Count; i++)
		{
			componentInChildren = this.AccessorySlots.lstToggle[i].GetComponentInChildren<Text>();
			if (!GlobalMethod.StartsWith(componentInChildren.text, "スロット"))
			{
				this.AccessorySlots.SetCheck(this.AllChange.isOn, i);
			}
		}
		componentInChildren = this.AllChange.GetComponentInChildren<Text>();
		Color color = componentInChildren.color;
		if (this.allState[this.hSceneManager.numFemaleClothCustom])
		{
			Define.Set(ref color, Colors.White, false);
		}
		else
		{
			color = new Color32(141, 136, 129, byte.MaxValue);
		}
		componentInChildren.color = color;
		if (this.hSceneManager.numFemaleClothCustom < 2)
		{
			this.females[this.hSceneManager.numFemaleClothCustom].SetAccessoryStateAll(this.allState[this.hSceneManager.numFemaleClothCustom]);
		}
		else
		{
			this.Males[this.hSceneManager.numFemaleClothCustom - 2].SetAccessoryStateAll(this.allState[this.hSceneManager.numFemaleClothCustom]);
		}
	}

	// Token: 0x0600530E RID: 21262 RVA: 0x00245B80 File Offset: 0x00243F80
	public void EndProc()
	{
		for (int i = 0; i < 4; i++)
		{
			ChaControl chaControl;
			if (i < 2)
			{
				chaControl = this.females[i];
			}
			else
			{
				chaControl = this.Males[i - 2];
			}
			if (!(chaControl == null))
			{
				for (int j = 0; j < 4; j++)
				{
					chaControl.ShowExtraAccessory((ChaControlDefine.ExtraAccessoryParts)j, true);
				}
			}
		}
	}

	// Token: 0x0600530F RID: 21263 RVA: 0x00245BEA File Offset: 0x00243FEA
	public void ChangeState(int id)
	{
		this.EquipBts[id].ChangeState();
	}

	// Token: 0x06005310 RID: 21264 RVA: 0x00245BFC File Offset: 0x00243FFC
	public void ChangeStateAllEquip(bool val)
	{
		if (this.hSceneManager.Player == null)
		{
			return;
		}
		for (int i = 0; i < 4; i++)
		{
			if (i != 3)
			{
				this.hSceneManager.Player.ChaControl.ShowExtraAccessory((ChaControlDefine.ExtraAccessoryParts)i, val);
			}
			else
			{
				this.hSceneManager.Player.ChaControl.ShowExtraAccessory((ChaControlDefine.ExtraAccessoryParts)i, val);
				if (this.hSceneManager.Player.EquipedItem != null && this.hSceneManager.Player.EquipedItem.AsGameObject != null)
				{
					this.hSceneManager.Player.EquipedItem.AsGameObject.SetActive(val);
				}
			}
		}
	}

	// Token: 0x04004D80 RID: 19840
	public HSceneSpriteChaChoice hSceneSpriteChaChoice;

	// Token: 0x04004D81 RID: 19841
	public HSceneSpriteToggleCategory AccessorySlots;

	// Token: 0x04004D82 RID: 19842
	public Toggle AllChange;

	// Token: 0x04004D83 RID: 19843
	public HSceneSpriteAccessoryCondition.EquipBt[] EquipBts = new HSceneSpriteAccessoryCondition.EquipBt[4];

	// Token: 0x04004D84 RID: 19844
	private HScene hScene;

	// Token: 0x04004D85 RID: 19845
	private ChaControl[] females;

	// Token: 0x04004D86 RID: 19846
	private ChaControl[] Males;

	// Token: 0x04004D87 RID: 19847
	private bool[] allState = new bool[]
	{
		true,
		true,
		true,
		true
	};

	// Token: 0x04004D88 RID: 19848
	[SerializeField]
	private HSceneSprite hSceneSprite;

	// Token: 0x04004D89 RID: 19849
	private HSceneManager hSceneManager;

	// Token: 0x04004D8A RID: 19850
	private StringBuilder sbAcsName;

	// Token: 0x04004D8B RID: 19851
	private bool[,] before = new bool[4, 4];

	// Token: 0x04004D8C RID: 19852
	private bool player;

	// Token: 0x02000B0E RID: 2830
	[Serializable]
	public class EquipBt
	{
		// Token: 0x06005313 RID: 21267 RVA: 0x00245CD4 File Offset: 0x002440D4
		public void ChangeState()
		{
			if (this.chara == null)
			{
				return;
			}
			HSceneManager instance = Singleton<HSceneManager>.Instance;
			if (this.id != 3)
			{
				if (this.active)
				{
					this.chara.ShowExtraAccessory((ChaControlDefine.ExtraAccessoryParts)this.id, false);
				}
				else
				{
					this.chara.ShowExtraAccessory((ChaControlDefine.ExtraAccessoryParts)this.id, true);
				}
			}
			else if (this.active)
			{
				this.chara.ShowExtraAccessory((ChaControlDefine.ExtraAccessoryParts)this.id, false);
				if (instance.Player != null && instance.Player.ChaControl == this.chara && instance.Player.EquipedItem != null && instance.Player.EquipedItem.AsGameObject != null)
				{
					instance.Player.EquipedItem.AsGameObject.SetActive(false);
				}
			}
			else
			{
				this.chara.ShowExtraAccessory((ChaControlDefine.ExtraAccessoryParts)this.id, true);
				if (instance.Player != null && instance.Player.ChaControl == this.chara && instance.Player.EquipedItem != null && instance.Player.EquipedItem.AsGameObject != null)
				{
					instance.Player.EquipedItem.AsGameObject.SetActive(true);
				}
			}
			this.On.gameObject.SetActive(!this.active);
			this.Off.gameObject.SetActive(this.active);
			this.active ^= true;
		}

		// Token: 0x04004D8D RID: 19853
		public GameObject Base;

		// Token: 0x04004D8E RID: 19854
		public Button On;

		// Token: 0x04004D8F RID: 19855
		public Button Off;

		// Token: 0x04004D90 RID: 19856
		public ChaControl chara;

		// Token: 0x04004D91 RID: 19857
		public int id = -1;

		// Token: 0x04004D92 RID: 19858
		public bool active;
	}
}
