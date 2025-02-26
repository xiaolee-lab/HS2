using System;
using System.Collections;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000B1B RID: 2843
public class HSceneSpriteHitem : MonoBehaviour
{
	// Token: 0x06005364 RID: 21348 RVA: 0x0024A1B4 File Offset: 0x002485B4
	public IEnumerator Init()
	{
		this.lstHItem = new Dictionary<int, UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>>();
		this.bHItemEffect = new Dictionary<int, bool>();
		this.nodes = new List<ScrollCylinderNode>();
		this.hitemInfos = new List<HSceneSpriteHitem.HitemInfo>();
		this.canUse = true;
		this.CheckHadItem(3, ref this.hitemInfos);
		this.CheckHadItem(5, ref this.hitemInfos);
		this.CheckHadItem(6, ref this.hitemInfos);
		this.CheckHadItem(7, ref this.hitemInfos);
		this.hSceneScroll.BlankSet(this.Node, false);
		this.hSceneNodePool.CreatePool(this.Node.gameObject, this.lstHItemTop.transform, 5);
		this.pool = this.hSceneNodePool.GetList();
		yield return null;
		this.Yes.onClick.AddListener(delegate()
		{
			this.SetUse(this.hitemInfos[this.ItemId].id, true);
			this.RemoveItem(this.hitemInfos[this.ItemId].id);
			this.ConfirmPanel.SetActive(false);
			Toggle component = this.pool[this.ItemId].GetComponent<Toggle>();
			component.isOn = false;
			this.hSceneScroll.enabled = true;
			Singleton<HSceneFlagCtrl>.Instance.AddParam(10, 1);
		});
		this.No.onClick.AddListener(delegate()
		{
			this.ConfirmPanel.SetActive(false);
			Toggle component = this.pool[this.ItemId].GetComponent<Toggle>();
			component.isOn = false;
			this.hSceneScroll.enabled = true;
		});
		for (int i = 0; i < this.hitemInfos.Count; i++)
		{
			int no = i;
			this.tmpNode = this.pool[no].GetComponent<HItemNode>();
			this.tmpNode.text.text = this.hitemInfos[no].ItemName;
			int num = this.CheckHadItemNum(this.hitemInfos[no].id);
			if (num > 999)
			{
				this.tmpNode.NumTxt.text = "999+";
			}
			else
			{
				this.tmpNode.NumTxt.text = string.Format("{0}", num);
			}
			this.tmpNode.ScaleSet.SetActive(true);
			this.toggle = this.tmpNode.Toggle;
			this.onClick = new UITrigger.TriggerEvent();
			if (this.toggle != null)
			{
				this.images = this.toggle.GetComponentsInChildren<Image>();
				this.PointerClickTrigger = this.toggle.GetComponent<PointerClickTrigger>();
				if (this.PointerClickTrigger == null)
				{
					this.PointerClickTrigger = this.toggle.gameObject.AddComponent<PointerClickTrigger>();
				}
				this.PointerClickTrigger.Triggers.Clear();
				this.PointerClickTrigger.Triggers.Add(this.onClick);
				this.onClick.AddListener(delegate(BaseEventData _)
				{
					if (this.ConfirmPanel.activeSelf)
					{
						return;
					}
					if (no != this.hSceneScroll.GetTarget().Item1)
					{
						this.hSceneScroll.SetTarget(this.pool[no].GetComponent<HItemNode>());
						return;
					}
					this.ConfirmPanel.SetActive(true);
					this.hSceneScroll.enabled = false;
					this.ItemId = no;
					this.pool[no].GetComponent<Toggle>().isOn = true;
				});
				if (this.toggle.interactable)
				{
					this.toggle.interactable = false;
				}
				for (int j = 0; j < this.images.Length; j++)
				{
					if (this.images[j].transform.name == "ItemIcon")
					{
						Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, this.hitemInfos[no].iconId, this.images[j], false);
						if (this.images[j].sprite != null)
						{
							this.images[j].enabled = true;
						}
					}
					if (!this.images[j].raycastTarget)
					{
						this.images[j].raycastTarget = true;
					}
				}
			}
			this.nodes.Add(this.tmpNode);
			this.lstHItem.Add(this.hitemInfos[no].id, new UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>(no, this.hitemInfos[no]));
			this.bHItemEffect.Add(this.hitemInfos[no].id, false);
		}
		yield return null;
		if (this.lstHItem.Count == 0)
		{
			this.canUse = false;
			if (this.pool.Count == 0)
			{
				this.hSceneNodePool.Get(0);
			}
			HSceneSpriteHitem.HitemInfo i2 = default(HSceneSpriteHitem.HitemInfo);
			this.lstHItem.Add(-1, new UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>(-1, i2));
			this.bHItemEffect.Add(-1, false);
			this.toggle = this.pool[0].GetComponent<Toggle>();
			this.PointerClickTrigger = this.pool[0].GetComponent<PointerClickTrigger>();
			this.PointerClickTrigger.Triggers.Clear();
			if (this.toggle != null)
			{
				if (this.toggle.interactable)
				{
					this.toggle.interactable = false;
				}
				this.images = this.toggle.GetComponentsInChildren<Image>();
				for (int k = 0; k < this.images.Length; k++)
				{
					if (this.images[k].raycastTarget)
					{
						this.images[k].raycastTarget = false;
					}
				}
			}
			this.nodes.Add(this.pool[0].GetComponent<ScrollCylinderNode>());
		}
		yield return null;
		yield break;
	}

	// Token: 0x06005365 RID: 21349 RVA: 0x0024A1D0 File Offset: 0x002485D0
	private void Update()
	{
		if (this.lstHItem.Count == 0 || !this.canUse)
		{
			return;
		}
		int item = this.hSceneScroll.GetTarget().Item1;
		int key = this.ConvertScrollIDToItemID(item);
		if (!this.lstHItem.ContainsKey(key))
		{
			return;
		}
		this.ExplanatoryText.text = this.lstHItem[key].Item2.ExplanatoryText;
		for (int i = 0; i < this.nodes.Count; i++)
		{
			this.tmpImgs = this.nodes[i].GetComponentsInChildren<Image>();
			for (int j = 0; j < this.tmpImgs.Length; j++)
			{
				if (!(this.tmpImgs[j].name != "OnCursor"))
				{
					if (i == item && !this.nodes[i].GetComponent<Toggle>().isOn)
					{
						this.tmpImgs[j].enabled = true;
					}
					else
					{
						this.tmpImgs[j].enabled = false;
					}
				}
			}
		}
	}

	// Token: 0x06005366 RID: 21350 RVA: 0x0024A30C File Offset: 0x0024870C
	public void SetVisible(bool visible)
	{
		for (int i = 0; i < this.pool.Count; i++)
		{
			if (i < this.lstHItem.Count)
			{
				this.pool[i].SetActive(visible);
			}
		}
		if (visible)
		{
			this.hSceneScroll.ListNodeSet(this.nodes);
		}
		if (this.canUse)
		{
			int key = this.ConvertScrollIDToItemID(this.hSceneScroll.GetTarget().Item1);
			this.ExplanatoryText.text = this.lstHItem[key].Item2.ExplanatoryText;
		}
		else
		{
			this.ExplanatoryText.text = "Hアイテムを所持していません";
		}
	}

	// Token: 0x06005367 RID: 21351 RVA: 0x0024A3D2 File Offset: 0x002487D2
	public void SetUse(int no, bool use)
	{
		if (this.pool.Count == 0)
		{
			return;
		}
		this.bHItemEffect[no] = use;
	}

	// Token: 0x06005368 RID: 21352 RVA: 0x0024A3F2 File Offset: 0x002487F2
	public bool Effect(int id)
	{
		return this.bHItemEffect.ContainsKey(id) && this.bHItemEffect[id];
	}

	// Token: 0x06005369 RID: 21353 RVA: 0x0024A418 File Offset: 0x00248818
	public void ItemRemove()
	{
		this.canUse = true;
		this.lstHItem.Clear();
		this.hitemInfos.Clear();
		this.nodes.Clear();
		List<GameObject> list = this.hSceneNodePool.GetList();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.activeSelf)
			{
				list[i].GetComponent<HItemNode>().ScaleSet.SetActive(false);
				list[i].gameObject.SetActive(false);
			}
		}
		this.CheckHadItem(3, ref this.hitemInfos);
		this.CheckHadItem(5, ref this.hitemInfos);
		this.CheckHadItem(6, ref this.hitemInfos);
		this.CheckHadItem(7, ref this.hitemInfos);
		for (int j = 0; j < this.hitemInfos.Count; j++)
		{
			int no = j;
			this.hSceneNodePool.Get(0);
			this.tmpNode = this.pool[no].GetComponent<HItemNode>();
			this.tmpNode.text.text = this.hitemInfos[no].ItemName;
			int num = this.CheckHadItemNum(this.hitemInfos[no].id);
			if (num > 999)
			{
				this.tmpNode.NumTxt.text = "999+";
			}
			else
			{
				this.tmpNode.NumTxt.text = string.Format("{0}", num);
			}
			this.tmpNode.ScaleSet.SetActive(true);
			this.toggle = this.tmpNode.Toggle;
			this.onClick = new UITrigger.TriggerEvent();
			if (this.toggle != null)
			{
				this.images = this.toggle.GetComponentsInChildren<Image>();
				this.PointerClickTrigger = this.toggle.GetComponent<PointerClickTrigger>();
				if (this.PointerClickTrigger == null)
				{
					this.PointerClickTrigger = this.toggle.gameObject.AddComponent<PointerClickTrigger>();
				}
				this.PointerClickTrigger.Triggers.Clear();
				this.PointerClickTrigger.Triggers.Add(this.onClick);
				this.onClick.AddListener(delegate(BaseEventData _)
				{
					if (this.ConfirmPanel.activeSelf)
					{
						return;
					}
					if (no != this.hSceneScroll.GetTarget().Item1)
					{
						this.hSceneScroll.SetTarget(this.pool[no].GetComponent<HItemNode>());
						return;
					}
					this.ConfirmPanel.SetActive(true);
					this.ItemId = no;
					this.pool[no].GetComponent<Toggle>().isOn = true;
				});
				if (this.toggle.interactable)
				{
					this.toggle.interactable = false;
				}
				for (int k = 0; k < this.images.Length; k++)
				{
					if (this.images[k].transform.name == "ItemIcon")
					{
						Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, this.hitemInfos[no].iconId, this.images[k], false);
						if (this.images[k].sprite != null)
						{
							this.images[k].enabled = true;
						}
					}
					if (!this.images[k].raycastTarget)
					{
						this.images[k].raycastTarget = true;
					}
				}
			}
			this.nodes.Add(this.tmpNode);
			this.lstHItem.Add(this.hitemInfos[no].id, new UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>(no, this.hitemInfos[no]));
			this.pool[no].SetActive(true);
		}
		if (this.lstHItem.Count == 0)
		{
			this.canUse = false;
			this.hSceneNodePool.Get(0);
			HSceneSpriteHitem.HitemInfo i2 = default(HSceneSpriteHitem.HitemInfo);
			this.lstHItem.Add(-1, new UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>(-1, i2));
			this.toggle = this.pool[0].GetComponent<Toggle>();
			this.PointerClickTrigger = this.pool[0].GetComponent<PointerClickTrigger>();
			this.PointerClickTrigger.Triggers.Clear();
			if (this.toggle != null)
			{
				if (this.toggle.interactable)
				{
					this.toggle.interactable = false;
				}
				this.images = this.toggle.GetComponentsInChildren<Image>();
				for (int l = 0; l < this.images.Length; l++)
				{
					if (this.images[l].transform.name == "ItemIcon")
					{
						this.images[l].enabled = false;
					}
					if (this.images[l].raycastTarget)
					{
						this.images[l].raycastTarget = false;
					}
				}
			}
			this.tmpNode = this.pool[0].GetComponent<HItemNode>();
			this.tmpNode.text.text = string.Empty;
			this.nodes.Add(this.tmpNode);
		}
		Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
		{
			this.hSceneScroll.ListNodeSet(this.nodes);
			if (this.canUse)
			{
				int key = this.ConvertScrollIDToItemID(this.hSceneScroll.GetTarget().Item1);
				if (this.lstHItem.ContainsKey(key))
				{
					this.ExplanatoryText.text = this.lstHItem[key].Item2.ExplanatoryText;
				}
			}
			else
			{
				this.ExplanatoryText.text = "Hアイテムを所持していません";
			}
		});
	}

	// Token: 0x0600536A RID: 21354 RVA: 0x0024A960 File Offset: 0x00248D60
	protected void RemoveItem(int _ID)
	{
		int num = -1;
		List<StuffItem> list = Singleton<Map>.Instance.Player.PlayerData.ItemList;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].CategoryID == this.ItemCategory)
			{
				if (list[i].ID == _ID)
				{
					num = i;
					break;
				}
			}
		}
		if (num != -1)
		{
			StuffItem stuffItem = list[num];
			StuffItem item = new StuffItem(stuffItem.CategoryID, stuffItem.ID, 1);
			Singleton<Map>.Instance.Player.PlayerData.ItemList.RemoveItem(item);
		}
		else
		{
			list = Singleton<Game>.Instance.Environment.ItemListInStorage;
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].CategoryID == this.ItemCategory)
				{
					if (list[j].ID == _ID)
					{
						num = j;
						break;
					}
				}
			}
			StuffItem stuffItem2 = list[num];
			StuffItem item2 = new StuffItem(stuffItem2.CategoryID, stuffItem2.ID, 1);
			list.RemoveItem(item2);
		}
		Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
		{
			this.ItemRemove();
		});
	}

	// Token: 0x0600536B RID: 21355 RVA: 0x0024AAC4 File Offset: 0x00248EC4
	private bool CheckHadItem(int id, ref List<HSceneSpriteHitem.HitemInfo> hitemInfos)
	{
		bool flag = this.CheckHadItem(id, 0, ref hitemInfos);
		if (!flag)
		{
			flag = this.CheckHadItem(id, 1, ref hitemInfos);
		}
		return flag;
	}

	// Token: 0x0600536C RID: 21356 RVA: 0x0024AAF0 File Offset: 0x00248EF0
	private bool CheckHadItem(int id, int mode, ref List<HSceneSpriteHitem.HitemInfo> hitemInfos)
	{
		List<StuffItem> list = (mode != 0) ? Singleton<Game>.Instance.Environment.ItemListInStorage : Singleton<Map>.Instance.Player.PlayerData.ItemList;
		foreach (StuffItem stuffItem in list)
		{
			if (stuffItem.CategoryID == this.ItemCategory && stuffItem.ID == id)
			{
				StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(stuffItem.CategoryID, id);
				if (item != null)
				{
					HSceneSpriteHitem.HitemInfo item2;
					item2.id = item.ID;
					item2.iconId = item.IconID;
					item2.ItemName = item.Name;
					item2.ExplanatoryText = item.Explanation;
					hitemInfos.Add(item2);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600536D RID: 21357 RVA: 0x0024ABFC File Offset: 0x00248FFC
	private int CheckHadItemNum(int id)
	{
		int num = 0;
		List<StuffItem> list = Singleton<Map>.Instance.Player.PlayerData.ItemList;
		foreach (StuffItem stuffItem in list)
		{
			if (stuffItem.CategoryID == this.ItemCategory && stuffItem.ID == id)
			{
				num = stuffItem.Count;
				break;
			}
		}
		list = Singleton<Game>.Instance.Environment.ItemListInStorage;
		foreach (StuffItem stuffItem2 in list)
		{
			if (stuffItem2.CategoryID == this.ItemCategory && stuffItem2.ID == id)
			{
				num += stuffItem2.Count;
				break;
			}
		}
		return num;
	}

	// Token: 0x0600536E RID: 21358 RVA: 0x0024AD18 File Offset: 0x00249118
	private int ConvertScrollIDToItemID(int ScrollId)
	{
		foreach (KeyValuePair<int, UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>> keyValuePair in this.lstHItem)
		{
			if (keyValuePair.Value.Item1 == ScrollId)
			{
				return keyValuePair.Key;
			}
		}
		return -1;
	}

	// Token: 0x0600536F RID: 21359 RVA: 0x0024AD98 File Offset: 0x00249198
	public void EndProc()
	{
		this.hSceneScroll.ClearBlank();
		List<GameObject> list = this.hSceneNodePool.GetList();
		for (int i = 0; i < list.Count; i++)
		{
			int index = i;
			UnityEngine.Object.Destroy(list[index].gameObject);
		}
		list.Clear();
		this.Yes.onClick.RemoveAllListeners();
		this.No.onClick.RemoveAllListeners();
		if (this.ConfirmPanel.activeSelf)
		{
			this.ConfirmPanel.SetActive(false);
		}
	}

	// Token: 0x04004DEF RID: 19951
	public ScrollCylinder hSceneScroll;

	// Token: 0x04004DF0 RID: 19952
	public HItemNode Node;

	// Token: 0x04004DF1 RID: 19953
	public GameObject lstHItemTop;

	// Token: 0x04004DF2 RID: 19954
	public Text ExplanatoryText;

	// Token: 0x04004DF3 RID: 19955
	public GameObject ConfirmPanel;

	// Token: 0x04004DF4 RID: 19956
	public Button Yes;

	// Token: 0x04004DF5 RID: 19957
	public Button No;

	// Token: 0x04004DF6 RID: 19958
	private Dictionary<int, UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>> lstHItem = new Dictionary<int, UnityEx.ValueTuple<int, HSceneSpriteHitem.HitemInfo>>();

	// Token: 0x04004DF7 RID: 19959
	private Dictionary<int, bool> bHItemEffect = new Dictionary<int, bool>();

	// Token: 0x04004DF8 RID: 19960
	private List<HSceneSpriteHitem.HitemInfo> hitemInfos = new List<HSceneSpriteHitem.HitemInfo>();

	// Token: 0x04004DF9 RID: 19961
	private HSceneNodePool hSceneNodePool = new HSceneNodePool();

	// Token: 0x04004DFA RID: 19962
	private List<GameObject> pool = new List<GameObject>();

	// Token: 0x04004DFB RID: 19963
	private List<ScrollCylinderNode> nodes;

	// Token: 0x04004DFC RID: 19964
	private HItemNode tmpNode;

	// Token: 0x04004DFD RID: 19965
	private Toggle toggle;

	// Token: 0x04004DFE RID: 19966
	private bool canUse = true;

	// Token: 0x04004DFF RID: 19967
	private int ItemId;

	// Token: 0x04004E00 RID: 19968
	private Image[] tmpImgs;

	// Token: 0x04004E01 RID: 19969
	private PointerClickTrigger PointerClickTrigger;

	// Token: 0x04004E02 RID: 19970
	private UITrigger.TriggerEvent onClick;

	// Token: 0x04004E03 RID: 19971
	private Image[] images;

	// Token: 0x04004E04 RID: 19972
	[SerializeField]
	[Tooltip("薬のCategoryIDを指定")]
	private int ItemCategory = 8;

	// Token: 0x04004E05 RID: 19973
	private const string IconObjName = "ItemIcon";

	// Token: 0x04004E06 RID: 19974
	private const string OnCursorName = "OnCursor";

	// Token: 0x02000B1C RID: 2844
	public struct HitemInfo
	{
		// Token: 0x04004E07 RID: 19975
		public int id;

		// Token: 0x04004E08 RID: 19976
		public int iconId;

		// Token: 0x04004E09 RID: 19977
		public string ItemName;

		// Token: 0x04004E0A RID: 19978
		public string ExplanatoryText;
	}
}
