using System;
using System.Collections.Generic;
using AIChara;
using AIProject;
using AIProject.ColorDefine;
using CharaCustom;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000B12 RID: 2834
public class HSceneSpriteCoordinatesCard : MonoBehaviour
{
	// Token: 0x17000EF1 RID: 3825
	// (get) Token: 0x06005328 RID: 21288 RVA: 0x00246B94 File Offset: 0x00244F94
	// (set) Token: 0x06005327 RID: 21287 RVA: 0x00246B86 File Offset: 0x00244F86
	private int _SelectedID
	{
		get
		{
			return this.SelectedID.Value;
		}
		set
		{
			this.SelectedID.Value = value;
		}
	}

	// Token: 0x06005329 RID: 21289 RVA: 0x00246BA1 File Offset: 0x00244FA1
	private void Start()
	{
		(from x in this.SelectedID
		where x >= 0 && x < this.lstCoordinates.Count
		select x).Subscribe(delegate(int x)
		{
			for (int i = 0; i < this.lstCoordinates.Count; i++)
			{
				if (this.lstCoordinates[i].id == x)
				{
					this.SelectedLabel.text = this.lstCoordinates[i].coodeName.text;
					this.filename = this.lstCoordinates[i].fileName;
					this.CardImage.texture = PngAssist.ChangeTextureFromByte((this.lstCoordinatesBase[x].pngData == null) ? PngFile.LoadPngBytes(this.lstCoordinatesBase[x].FullPath) : this.lstCoordinatesBase[x].pngData, 0, 0, TextureFormat.ARGB32, false);
				}
			}
			if (!this.CardImage.gameObject.activeSelf)
			{
				this.CardImage.gameObject.SetActive(true);
			}
			for (int j = 0; j < this.lstCoordinates.Count; j++)
			{
				if (this.lstCoordinates[j].id != x)
				{
					this.lstCoordinates[j].image.color = Color.white;
				}
				else
				{
					this.lstCoordinates[j].image.color = Define.Get(Colors.Yellow);
				}
			}
		});
	}

	// Token: 0x0600532A RID: 21290 RVA: 0x00246BCC File Offset: 0x00244FCC
	public void Init()
	{
		this.hScene = Singleton<HSceneFlagCtrl>.Instance.GetComponent<HScene>();
		this.femailes = this.hScene.GetFemales();
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		this.Sort.onClick.AddListener(delegate()
		{
			this.SortPanel.SetActive(this.SortPanel.activeSelf ^ true);
		});
		this.SortUpDown[0].onClick.AddListener(delegate()
		{
			this.SortUpDown[0].gameObject.SetActive(false);
			this.SortUpDown[1].gameObject.SetActive(true);
			this.ListSortUpDown(0);
		});
		this.SortUpDown[1].onClick.AddListener(delegate()
		{
			this.SortUpDown[0].gameObject.SetActive(true);
			this.SortUpDown[1].gameObject.SetActive(false);
			this.ListSortUpDown(1);
		});
		this.cross.onClick.AddListener(delegate()
		{
			this.SortPanel.SetActive(false);
		});
		this.lstCoordinatesBase = CustomClothesFileInfoAssist.CreateClothesFileInfoList(false, true, true, true);
		this.lstCoordinates.Clear();
		for (int i = 0; i < this.lstCoordinatesBase.Count; i++)
		{
			int no = i;
			HSceneSpriteCoordinatesNode hsceneSpriteCoordinatesNode = UnityEngine.Object.Instantiate<HSceneSpriteCoordinatesNode>(this.CoordinatesNode, this.Content);
			hsceneSpriteCoordinatesNode.gameObject.SetActive(true);
			this.lstCoordinates.Add(hsceneSpriteCoordinatesNode);
			this.lstCoordinates[no].id = no;
			this.lstCoordinates[no].coodeName.text = this.lstCoordinatesBase[no].name;
			this.lstCoordinates[no].CreateCoodeTime = this.lstCoordinatesBase[no].time;
			this.lstCoordinates[no].GetComponent<Button>().onClick.AddListener(delegate()
			{
				this._SelectedID = no;
			});
			this.lstCoordinates[no].image = this.lstCoordinates[no].GetComponent<Image>();
			this.lstCoordinates[no].fileName = this.lstCoordinatesBase[no].FileName;
		}
		UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
		UITrigger.TriggerEvent triggerEvent2 = new UITrigger.TriggerEvent();
		for (int j = 0; j < this.lstSortCategory.Count; j++)
		{
			int no = j;
			this.lstSortCategory[no].onValueChanged.RemoveAllListeners();
			this.lstSortCategory[no].onValueChanged.AddListener(delegate(bool On)
			{
				if (On)
				{
					this.ListSort(no);
				}
			});
			triggerEvent = new UITrigger.TriggerEvent();
			PointerEnterTrigger pointerEnterTrigger = this.lstSortCategory[no].gameObject.GetComponent<PointerEnterTrigger>();
			if (pointerEnterTrigger == null)
			{
				pointerEnterTrigger = this.lstSortCategory[no].gameObject.AddComponent<PointerEnterTrigger>();
			}
			if (pointerEnterTrigger.Triggers.Count > 0)
			{
				pointerEnterTrigger.Triggers.Clear();
			}
			pointerEnterTrigger.Triggers.Add(triggerEvent);
			triggerEvent.AddListener(delegate(BaseEventData x)
			{
				if (Cursor.visible)
				{
					this.lstSortCategory[no].targetGraphic.enabled = true;
				}
			});
			triggerEvent2 = new UITrigger.TriggerEvent();
			PointerExitTrigger pointerExitTrigger = this.lstSortCategory[no].gameObject.GetComponent<PointerExitTrigger>();
			if (pointerExitTrigger == null)
			{
				pointerExitTrigger = this.lstSortCategory[no].gameObject.AddComponent<PointerExitTrigger>();
			}
			if (pointerExitTrigger.Triggers.Count > 0)
			{
				pointerExitTrigger.Triggers.Clear();
			}
			pointerExitTrigger.Triggers.Add(triggerEvent2);
			triggerEvent2.AddListener(delegate(BaseEventData x)
			{
				this.lstSortCategory[no].targetGraphic.enabled = false;
			});
		}
		this.ListSort(0);
		this.BeforeCoode.onClick.AddListener(delegate()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			this.femailes[this.hSceneManager.numFemaleClothCustom].ChangeNowCoordinate(true, true);
			this.hSceneSpriteCloth.SetClothCharacter(false);
		});
		this.DecideCoode.onClick.AddListener(delegate()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			if (this.SelectedID.Value == -1 || this.filename.IsNullOrEmpty())
			{
				this.femailes[this.hSceneManager.numFemaleClothCustom].ChangeNowCoordinate(true, true);
			}
			else
			{
				this.femailes[this.hSceneManager.numFemaleClothCustom].ChangeNowCoordinate(this.filename, true, true);
			}
			this.hSceneSpriteCloth.SetClothCharacter(false);
		});
		this.hSceneSpriteChaChoice.SetAction(delegate
		{
			this.SetCoordinatesCharacter();
		});
	}

	// Token: 0x0600532B RID: 21291 RVA: 0x00246FEF File Offset: 0x002453EF
	private void SetCoordinatesCharacter()
	{
		this._SelectedID = -1;
	}

	// Token: 0x0600532C RID: 21292 RVA: 0x00246FF8 File Offset: 0x002453F8
	public void ListSort(int sortkind)
	{
		HSceneSpriteCoordinatesCard.ListComparer listComparer = new HSceneSpriteCoordinatesCard.ListComparer();
		this.sortKind = sortkind;
		listComparer.nCompare = sortkind;
		listComparer.bAscending = this.Ascending;
		if (this.lstCoordinates == null || this.lstCoordinates.Count == 0)
		{
			return;
		}
		this.lstCoordinates.Sort(listComparer);
		for (int i = 0; i < this.lstCoordinates.Count; i++)
		{
			int siblingIndex = i;
			this.lstCoordinates[i].transform.SetSiblingIndex(siblingIndex);
		}
	}

	// Token: 0x0600532D RID: 21293 RVA: 0x00247084 File Offset: 0x00245484
	public void ListSortUpDown(int Ascending)
	{
		HSceneSpriteCoordinatesCard.ListComparer listComparer = new HSceneSpriteCoordinatesCard.ListComparer();
		this.Ascending = (Ascending == 0);
		listComparer.nCompare = this.sortKind;
		listComparer.bAscending = this.Ascending;
		if (this.lstCoordinates == null || this.lstCoordinates.Count == 0)
		{
			return;
		}
		this.lstCoordinates.Sort(listComparer);
		for (int i = 0; i < this.lstCoordinates.Count; i++)
		{
			int siblingIndex = i;
			this.lstCoordinates[i].transform.SetSiblingIndex(siblingIndex);
		}
	}

	// Token: 0x0600532E RID: 21294 RVA: 0x00247118 File Offset: 0x00245518
	public void EndProc()
	{
		for (int i = 0; i < this.lstCoordinates.Count; i++)
		{
			UnityEngine.Object.Destroy(this.lstCoordinates[i].gameObject);
		}
		this.lstCoordinates.Clear();
		this.Sort.onClick.RemoveAllListeners();
		this.SortUpDown[0].onClick.RemoveAllListeners();
		this.SortUpDown[1].onClick.RemoveAllListeners();
		this.cross.onClick.RemoveAllListeners();
		this.BeforeCoode.onClick.RemoveAllListeners();
		this.DecideCoode.onClick.RemoveAllListeners();
		this.CloseSort();
	}

	// Token: 0x0600532F RID: 21295 RVA: 0x002471CC File Offset: 0x002455CC
	private void OnDisable()
	{
		this.CloseSort();
	}

	// Token: 0x06005330 RID: 21296 RVA: 0x002471D4 File Offset: 0x002455D4
	public void CloseSort()
	{
		this.SortPanel.SetActive(false);
	}

	// Token: 0x04004DA6 RID: 19878
	[SerializeField]
	private HSceneSpriteChaChoice hSceneSpriteChaChoice;

	// Token: 0x04004DA7 RID: 19879
	[SerializeField]
	private HSceneSpriteClothCondition hSceneSpriteCloth;

	// Token: 0x04004DA8 RID: 19880
	[SerializeField]
	private RawImage CardImage;

	// Token: 0x04004DA9 RID: 19881
	[SerializeField]
	private Text SelectedLabel;

	// Token: 0x04004DAA RID: 19882
	private string filename;

	// Token: 0x04004DAB RID: 19883
	[SerializeField]
	private Button Sort;

	// Token: 0x04004DAC RID: 19884
	[SerializeField]
	private Button[] SortUpDown;

	// Token: 0x04004DAD RID: 19885
	[SerializeField]
	private GameObject SortPanel;

	// Token: 0x04004DAE RID: 19886
	[SerializeField]
	private HSceneSpriteCoordinatesNode CoordinatesNode;

	// Token: 0x04004DAF RID: 19887
	[SerializeField]
	private Transform Content;

	// Token: 0x04004DB0 RID: 19888
	[SerializeField]
	private Button cross;

	// Token: 0x04004DB1 RID: 19889
	[SerializeField]
	private List<Toggle> lstSortCategory = new List<Toggle>();

	// Token: 0x04004DB2 RID: 19890
	private List<HSceneSpriteCoordinatesNode> lstCoordinates = new List<HSceneSpriteCoordinatesNode>();

	// Token: 0x04004DB3 RID: 19891
	private List<CustomClothesFileInfo> lstCoordinatesBase = new List<CustomClothesFileInfo>();

	// Token: 0x04004DB4 RID: 19892
	[SerializeField]
	private Button BeforeCoode;

	// Token: 0x04004DB5 RID: 19893
	[SerializeField]
	private Button DecideCoode;

	// Token: 0x04004DB6 RID: 19894
	private int sortKind;

	// Token: 0x04004DB7 RID: 19895
	private bool Ascending;

	// Token: 0x04004DB8 RID: 19896
	private HScene hScene;

	// Token: 0x04004DB9 RID: 19897
	private HSceneManager hSceneManager;

	// Token: 0x04004DBA RID: 19898
	private ChaControl[] femailes;

	// Token: 0x04004DBB RID: 19899
	private IntReactiveProperty SelectedID = new IntReactiveProperty(-1);

	// Token: 0x02000B13 RID: 2835
	private class ListComparer : IComparer<HSceneSpriteCoordinatesNode>
	{
		// Token: 0x0600533B RID: 21307 RVA: 0x002474C0 File Offset: 0x002458C0
		public int Compare(HSceneSpriteCoordinatesNode a, HSceneSpriteCoordinatesNode b)
		{
			int num = this.nCompare;
			if (num != 0)
			{
				if (num != 1)
				{
					return 0;
				}
				if (this.bAscending)
				{
					return this.SortCompare<DateTime>(a.CreateCoodeTime, b.CreateCoodeTime);
				}
				return this.SortCompare<DateTime>(b.CreateCoodeTime, a.CreateCoodeTime);
			}
			else
			{
				if (this.bAscending)
				{
					return this.SortCompare<string>(a.coodeName.text, b.coodeName.text);
				}
				return this.SortCompare<string>(b.coodeName.text, a.coodeName.text);
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0024755D File Offset: 0x0024595D
		private int SortCompare<T>(T a, T b) where T : IComparable
		{
			return a.CompareTo(b);
		}

		// Token: 0x04004DBC RID: 19900
		public int nCompare;

		// Token: 0x04004DBD RID: 19901
		public bool bAscending;
	}
}
