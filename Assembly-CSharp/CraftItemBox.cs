using System;
using System.Collections.Generic;
using AIProject.UI;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000EF4 RID: 3828
public class CraftItemBox : MenuUIBehaviour
{
	// Token: 0x17001886 RID: 6278
	// (get) Token: 0x06007CE6 RID: 31974 RVA: 0x00356E9C File Offset: 0x0035529C
	protected MenuUIBehaviour[] MenuUIList
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

	// Token: 0x17001887 RID: 6279
	// (get) Token: 0x06007CE7 RID: 31975 RVA: 0x00356EC9 File Offset: 0x003552C9
	// (set) Token: 0x06007CE8 RID: 31976 RVA: 0x00356ED1 File Offset: 0x003552D1
	public Action<MoveDirection> OnMove { get; set; }

	// Token: 0x17001888 RID: 6280
	// (get) Token: 0x06007CE9 RID: 31977 RVA: 0x00356EDA File Offset: 0x003552DA
	// (set) Token: 0x06007CEA RID: 31978 RVA: 0x00356EE7 File Offset: 0x003552E7
	public bool isActive
	{
		get
		{
			return this._isActive.Value;
		}
		set
		{
			this._isActive.Value = value;
		}
	}

	// Token: 0x17001889 RID: 6281
	// (get) Token: 0x06007CEB RID: 31979 RVA: 0x00356EF5 File Offset: 0x003552F5
	// (set) Token: 0x06007CEC RID: 31980 RVA: 0x00356F02 File Offset: 0x00355302
	public bool EndLoad
	{
		get
		{
			return this._EndLoad.Value;
		}
		set
		{
			this._EndLoad.Value = value;
		}
	}

	// Token: 0x06007CED RID: 31981 RVA: 0x00356F10 File Offset: 0x00355310
	protected override void Start()
	{
		this.inputManager = Singleton<Manager.Input>.Instance;
		this.selectedID = 0;
		this._isActive.Subscribe(delegate(bool x)
		{
			this.SetActiveControl(x);
		});
		(from x in this._EndLoad
		where x
		select x).Subscribe(delegate(bool x)
		{
			this.ChangeList(1);
		});
		this.OnMove = delegate(MoveDirection x)
		{
			this.SelectMove(x);
		};
		if (this.carsol != null)
		{
			this.color.r = this.carsol.GetComponent<Image>().color.r;
			this.color.g = this.carsol.GetComponent<Image>().color.g;
			this.color.b = this.carsol.GetComponent<Image>().color.b;
			this.color.a = this.carsol.GetComponent<Image>().color.a;
		}
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
		this.playerstrage.Add(0, 1);
		for (int i = 0; i < this.buildPartsRecipes.Length; i++)
		{
			this.buildPartsRecipes[i].MatName = this.resipeMatName[i];
			this.buildPartsRecipes[i].Image = this.resipeMatImage[i];
			this.buildPartsRecipes[i].HavingNum = this.resipeHavingNum[i];
			this.buildPartsRecipes[i].NeedNum = this.resipeNeedNum[i];
			this.buildPartsRecipes[i].root = this.resipe[i];
		}
	}

	// Token: 0x06007CEE RID: 31982 RVA: 0x00357114 File Offset: 0x00355514
	private void SetActiveControl(bool isActive)
	{
		if (isActive)
		{
			this.carsol.SetActive(true);
			this.selectedID = 0;
			this.ItemKind[this.selectedID].GetComponent<Button>().Select();
		}
		else
		{
			this.carsol.SetActive(false);
			this.inputManager.ClearMenuElements();
			this.inputManager.FocusLevel = -1;
			this.inputManager.ReserveState(Manager.Input.ValidType.Action);
			this.inputManager.SetupState();
		}
	}

	// Token: 0x06007CEF RID: 31983 RVA: 0x00357190 File Offset: 0x00355590
	private void OnUpdate()
	{
		if (this.ItemKind == null || this.ItemKind.Length == 0 || this.ItemKind[0] == null)
		{
			return;
		}
		if (!this.isActive)
		{
			return;
		}
		if (this.bAlphaAdd)
		{
			this.color.a = Mathf.SmoothDamp(this.color.a, 0.39215687f, ref this._alphaVelocity, 0.00095f, float.PositiveInfinity, Time.unscaledDeltaTime);
			if (this.color.a == 0.39215687f)
			{
				this.bAlphaAdd ^= true;
			}
		}
		else
		{
			this.color.a = Mathf.SmoothDamp(this.color.a, 0f, ref this._alphaVelocity, 0.00095f, float.PositiveInfinity, Time.unscaledDeltaTime);
			if (this.color.a == 0f)
			{
				this.bAlphaAdd ^= true;
			}
		}
		this.carsol.GetComponent<Image>().color = this.color;
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		if (currentSelectedGameObject.transform.parent != this.scrollView.transform)
		{
			return;
		}
		this.SelectedRectTransform = currentSelectedGameObject.GetComponent<RectTransform>();
		RectTransform component = this.scrollView.GetComponent<RectTransform>();
		float num = this.SelectedRectTransform.rect.width / component.rect.width;
		if (this.carsol.transform.localPosition.x >= 1000f)
		{
			float num2 = this.scroll.horizontalNormalizedPosition + num;
			if (num2 >= 1f)
			{
				num2 = 1f;
			}
			this.scroll.horizontalNormalizedPosition = Mathf.SmoothDamp(this.scroll.horizontalNormalizedPosition, num2, ref this._vel, this._followAccelerationTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		}
		else if (this.carsol.transform.localPosition.x < 0f)
		{
			float num3 = this.scroll.horizontalNormalizedPosition - num;
			if (num3 <= 0f)
			{
				num3 = 0f;
			}
			this.scroll.horizontalNormalizedPosition = Mathf.SmoothDamp(this.scroll.horizontalNormalizedPosition, num3, ref this._vel, this._followAccelerationTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		}
		this.carsol.transform.position = Vector3.SmoothDamp(this.carsol.transform.position, this.ItemKind[this.selectedID].transform.position, ref this._velocity, this._followAccelerationTime, float.PositiveInfinity, Time.unscaledDeltaTime);
	}

	// Token: 0x06007CF0 RID: 31984 RVA: 0x00357467 File Offset: 0x00355867
	private void ChangeItemKind(int formKind, int nID)
	{
		this.craftControler._nPartsForm = formKind;
		this.craftControler._nID = nID;
		this.craftControler.ChangeParts();
	}

	// Token: 0x06007CF1 RID: 31985 RVA: 0x0035748C File Offset: 0x0035588C
	public void ChangeList(int id)
	{
		if (!this._prevEndLoad)
		{
			this._prevEndLoad = true;
		}
		for (int i = 0; i < this.ItemKind.Length; i++)
		{
			if (!(this.ItemKind[i] == null))
			{
				UnityEngine.Object.Destroy(this.ItemKind[i]);
				this.ItemKind[i] = null;
				UnityEngine.Object.Destroy(this.LockImages[i]);
				this.LockImages[i] = null;
			}
		}
		int num = 0;
		Dictionary<int, Tuple<int, int>> dictionary = new Dictionary<int, Tuple<int, int>>();
		List<BuildPartsPool>[] baseParts = Singleton<CraftCommandListBaseObject>.Instance.BaseParts;
		for (int j = 0; j < baseParts.Length; j++)
		{
			for (int k = 0; k < baseParts[j].Count; k++)
			{
				if (baseParts[j][k].GetCategoryKind() == id)
				{
					dictionary.Add(num, new Tuple<int, int>(j, k));
					num++;
				}
			}
		}
		this.ItemKind = new GameObject[num];
		this.LockImages = new GameObject[num];
		for (int l = 0; l < this.ItemKind.Length; l++)
		{
			this.ItemKind[l] = UnityEngine.Object.Instantiate<GameObject>(this.itemButton);
			this.ItemKind[l].transform.SetParent(this.scrollView, false);
			int nForm = dictionary[l].Item1;
			int nPool = dictionary[l].Item2;
			bool flag = Singleton<CraftCommandListBaseObject>.Instance.BaseParts[nForm][nPool].CheckLock();
			this.ItemKind[l].GetComponentInChildren<Text>().text = this.buildPartsMgr.BuildPartPoolDic[nForm][nPool].Item2.Name;
			Button button = this.ItemKind[l].GetComponent<Button>();
			button.onClick.AddListener(delegate()
			{
				this.Clicked(nForm, nPool, button);
			});
			if (l == 0)
			{
				button.Select();
			}
			this.ItemKind[l].SetActive(true);
			this.LockImages[l] = this.ItemKind[l].transform.GetChild(1).gameObject;
			if (flag)
			{
				this.LockImages[l].SetActive(true);
			}
		}
		this.inputManager.FocusLevel = 0;
		this.inputManager.MenuElements = this.MenuUIList;
		this.inputManager.ReserveState(Manager.Input.ValidType.UI);
		this.inputManager.SetupState();
	}

	// Token: 0x06007CF2 RID: 31986 RVA: 0x00357740 File Offset: 0x00355B40
	private void SelectMove(MoveDirection moveDir)
	{
		if (moveDir != MoveDirection.Right)
		{
			if (moveDir == MoveDirection.Left)
			{
				int num = this.selectedID - 1;
				if (num < 0)
				{
					num = this.ItemKind.Length - 1;
				}
				this.selectedID = num;
			}
		}
		else
		{
			int num2 = this.selectedID + 1;
			if (num2 >= this.ItemKind.Length)
			{
				num2 = 0;
			}
			this.selectedID = num2;
		}
		this.ItemKind[this.selectedID].GetComponent<Button>().Select();
	}

	// Token: 0x06007CF3 RID: 31987 RVA: 0x003577C4 File Offset: 0x00355BC4
	private void Clicked(int nForm, int nPool, Button clickedButton)
	{
		if (!Singleton<CraftCommandListBaseObject>.Instance.BaseParts[nForm][nPool].CheckLock())
		{
			this.ChangeItemKind(nForm, nPool);
			this.isActive = false;
		}
		else
		{
			this.SetUnLockPanel(nForm, nPool, clickedButton);
		}
	}

	// Token: 0x06007CF4 RID: 31988 RVA: 0x0035780C File Offset: 0x00355C0C
	private void SetUnLockPanel(int nForm, int nPool, Button clickedButton)
	{
		this.unLockPanel.SetActive(true);
		bool flag = true;
		for (int i = 0; i < this.buildPartsRecipes.Length; i++)
		{
			this.buildPartsRecipes[i].root.SetActive(false);
		}
		int num = 0;
		for (int j = 0; j < this.buildPartsMgr.BuildPartPoolDic[nForm][nPool].Item2.recipe.Length; j++)
		{
			if (this.buildPartsMgr.BuildPartPoolDic[nForm][nPool].Item2.recipe[j].Item2 <= 0)
			{
				break;
			}
			num++;
		}
		for (int k = 0; k < num; k++)
		{
			int num2 = 0;
			int item = this.buildPartsMgr.BuildPartPoolDic[nForm][nPool].Item2.recipe[k].Item2;
			this.buildPartsRecipes[k].HavingNum.text = string.Format("{0}", num2);
			this.buildPartsRecipes[k].NeedNum.text = string.Format("／ {0}", item);
			this.buildPartsRecipes[k].HavingNum.color = Color.white;
			this.buildPartsRecipes[k].root.SetActive(true);
			if (num2 < item)
			{
				this.buildPartsRecipes[k].HavingNum.color = Color.red;
				flag = false;
			}
		}
		if (!flag)
		{
			this.unLockButton.interactable = false;
		}
		if (this.unLockButton != null)
		{
			this.unLockButton.onClick.AddListener(delegate()
			{
				this.UnLock(nForm, nPool, clickedButton);
			});
		}
		if (this.cancelButton != null)
		{
			this.cancelButton.onClick.AddListener(delegate()
			{
				this.unLockPanel.SetActive(false);
			});
		}
	}

	// Token: 0x06007CF5 RID: 31989 RVA: 0x00357A5C File Offset: 0x00355E5C
	private void UnLock(int nForm, int nPool, Button clickedButton)
	{
		Singleton<CraftCommandListBaseObject>.Instance.BaseParts[nForm][nPool].UnLock();
		this.unLockPanel.SetActive(false);
		clickedButton.transform.GetChild(1).gameObject.SetActive(false);
	}

	// Token: 0x06007CF6 RID: 31990 RVA: 0x00357A98 File Offset: 0x00355E98
	private void close()
	{
		this.isActive = false;
	}

	// Token: 0x06007CF7 RID: 31991 RVA: 0x00357AA1 File Offset: 0x00355EA1
	public override void OnInputMoveDirection(MoveDirection moveDir)
	{
		this.OnMove(moveDir);
	}

	// Token: 0x040064EA RID: 25834
	public GameObject[] ItemKind;

	// Token: 0x040064EB RID: 25835
	private GameObject[] LockImages;

	// Token: 0x040064EC RID: 25836
	[SerializeField]
	private GameObject unLockPanel;

	// Token: 0x040064ED RID: 25837
	[SerializeField]
	private Text[] resipeMatName;

	// Token: 0x040064EE RID: 25838
	[SerializeField]
	private Image[] resipeMatImage;

	// Token: 0x040064EF RID: 25839
	[SerializeField]
	private Text[] resipeHavingNum;

	// Token: 0x040064F0 RID: 25840
	[SerializeField]
	private Text[] resipeNeedNum;

	// Token: 0x040064F1 RID: 25841
	[SerializeField]
	private GameObject[] resipe;

	// Token: 0x040064F2 RID: 25842
	[SerializeField]
	private Button unLockButton;

	// Token: 0x040064F3 RID: 25843
	[SerializeField]
	private Button cancelButton;

	// Token: 0x040064F4 RID: 25844
	public BuildPartsMgr buildPartsMgr;

	// Token: 0x040064F5 RID: 25845
	public Dictionary<int, int> playerstrage = new Dictionary<int, int>();

	// Token: 0x040064F6 RID: 25846
	private BuildPartsRecipe[] buildPartsRecipes = new BuildPartsRecipe[3];

	// Token: 0x040064F7 RID: 25847
	public ScrollRect scroll;

	// Token: 0x040064F8 RID: 25848
	private RectTransform SelectedRectTransform;

	// Token: 0x040064F9 RID: 25849
	public GameObject itemButton;

	// Token: 0x040064FA RID: 25850
	public Transform scrollView;

	// Token: 0x040064FB RID: 25851
	public GameObject carsol;

	// Token: 0x040064FC RID: 25852
	[SerializeField]
	private CraftControler craftControler;

	// Token: 0x040064FD RID: 25853
	private int selectedID;

	// Token: 0x040064FE RID: 25854
	private Color color;

	// Token: 0x040064FF RID: 25855
	private MenuUIBehaviour[] _menuUIList;

	// Token: 0x04006501 RID: 25857
	[SerializeField]
	private BoolReactiveProperty _EndLoad = new BoolReactiveProperty(false);

	// Token: 0x04006502 RID: 25858
	private bool _prevEndLoad;

	// Token: 0x04006503 RID: 25859
	private Manager.Input inputManager;

	// Token: 0x04006504 RID: 25860
	private Vector3 _velocity = Vector3.zero;

	// Token: 0x04006505 RID: 25861
	private float _vel;

	// Token: 0x04006506 RID: 25862
	private float _alphaVelocity;

	// Token: 0x04006507 RID: 25863
	private bool bAlphaAdd = true;
}
