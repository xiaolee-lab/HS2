using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000EF1 RID: 3825
public class CraftInfomationUI : MonoBehaviour
{
	// Token: 0x17001884 RID: 6276
	// (get) Token: 0x06007CD5 RID: 31957 RVA: 0x00356A0B File Offset: 0x00354E0B
	// (set) Token: 0x06007CD6 RID: 31958 RVA: 0x00356A18 File Offset: 0x00354E18
	public int nCost
	{
		get
		{
			return this.cost.Value;
		}
		set
		{
			this.cost.Value = value;
		}
	}

	// Token: 0x17001885 RID: 6277
	// (get) Token: 0x06007CD7 RID: 31959 RVA: 0x00356A26 File Offset: 0x00354E26
	// (set) Token: 0x06007CD8 RID: 31960 RVA: 0x00356A33 File Offset: 0x00354E33
	public bool bFade
	{
		get
		{
			return this.fade.Value;
		}
		set
		{
			this.fade.Value = value;
		}
	}

	// Token: 0x06007CD9 RID: 31961 RVA: 0x00356A44 File Offset: 0x00354E44
	private void Start()
	{
		this.nCost = 0;
		this.prevCost = 0;
		(from x in this.cost
		where x != this.prevCost
		select x).Subscribe(delegate(int x)
		{
			this.ChangeCostTex(x);
		});
		this.gageRectBG = this.gageBG.GetComponent<RectTransform>();
		this.gageRect = this.gage.GetComponent<RectTransform>();
		this.fGageAmountvalue = this.gageRectBG.sizeDelta.x / 500f;
		this.warningBG = this.warningPanel.GetComponentInChildren<Image>();
		this.warningTex = this.warningPanel.GetComponentInChildren<Text>();
		this.WarningdefColor = new Color[]
		{
			this.warningBG.color,
			this.warningTex.color
		};
		(from _ in Observable.EveryUpdate()
		where this.fade.Value
		select _).Subscribe(delegate(long _)
		{
			this.fadeMassage();
		});
		this.fWarningExist = 0f;
		if (this.moveSelect != null)
		{
			this.moveSelect.onClick.AddListener(new UnityAction(this.BuildPartsSelect));
		}
		if (this.deleteSelect != null)
		{
			this.deleteSelect.onClick.AddListener(delegate()
			{
				this.opelatePanel.SetActive(false);
			});
		}
	}

	// Token: 0x06007CDA RID: 31962 RVA: 0x00356BAC File Offset: 0x00354FAC
	private void ChangeCostTex(int newCost)
	{
		this.prevCost = newCost;
		this.costText.text = string.Format("{0}/{1}", this.nCost, 500);
		Vector2 sizeDelta = new Vector2(this.fGageAmountvalue * (float)this.nCost, this.gageRectBG.sizeDelta.y);
		sizeDelta.x = Mathf.Clamp(sizeDelta.x, 0f, this.gageRectBG.sizeDelta.x);
		this.gageRect.sizeDelta = sizeDelta;
	}

	// Token: 0x06007CDB RID: 31963 RVA: 0x00356C4C File Offset: 0x0035504C
	public void SetWarningMessage(int Pattern = 0)
	{
		if (this.warningPanel.activeSelf)
		{
			return;
		}
		this.warningPanel.SetActive(true);
		this.warningTex.text = this.warningMassage[Pattern];
		this.fWarningExist = 0f;
		this.warningBG.color = this.WarningdefColor[0];
		this.warningTex.color = this.WarningdefColor[1];
	}

	// Token: 0x06007CDC RID: 31964 RVA: 0x00356CCC File Offset: 0x003550CC
	private void fadeMassage()
	{
		Color[] array = new Color[]
		{
			this.warningBG.color,
			this.warningTex.color
		};
		array[0].a = Mathf.SmoothDamp(array[0].a, 0f, ref this.alphaVel, 0.8f);
		array[1].a = Mathf.SmoothDamp(array[1].a, 0f, ref this.alphaVel, 0.8f);
		if (array[0].a <= 0f)
		{
			this.warningPanel.SetActive(false);
			this.bFade = false;
			this.fWarningExist = 0f;
			array[0].a = 0f;
			array[1].a = 0f;
		}
		this.warningBG.color = array[0];
		this.warningTex.color = array[1];
	}

	// Token: 0x06007CDD RID: 31965 RVA: 0x00356DEC File Offset: 0x003551EC
	public bool GetWarningActive()
	{
		return this.warningPanel.activeSelf;
	}

	// Token: 0x06007CDE RID: 31966 RVA: 0x00356DF9 File Offset: 0x003551F9
	public void SetOpeLatePanel()
	{
		this.opelatePanel.SetActive(true);
	}

	// Token: 0x06007CDF RID: 31967 RVA: 0x00356E07 File Offset: 0x00355207
	private void BuildPartsSelect()
	{
		this.opelatePanel.SetActive(false);
		this.craftControler.SelectBuldPart();
	}

	// Token: 0x040064CB RID: 25803
	[SerializeField]
	private Text costText;

	// Token: 0x040064CC RID: 25804
	[SerializeField]
	private GameObject gageBG;

	// Token: 0x040064CD RID: 25805
	private RectTransform gageRectBG;

	// Token: 0x040064CE RID: 25806
	[SerializeField]
	private GameObject gage;

	// Token: 0x040064CF RID: 25807
	private RectTransform gageRect;

	// Token: 0x040064D0 RID: 25808
	private float fGageAmountvalue;

	// Token: 0x040064D1 RID: 25809
	[SerializeField]
	private GameObject warningPanel;

	// Token: 0x040064D2 RID: 25810
	private Image warningBG;

	// Token: 0x040064D3 RID: 25811
	private Text warningTex;

	// Token: 0x040064D4 RID: 25812
	public float fWarningExist;

	// Token: 0x040064D5 RID: 25813
	[SerializeField]
	private GameObject opelatePanel;

	// Token: 0x040064D6 RID: 25814
	public Button moveSelect;

	// Token: 0x040064D7 RID: 25815
	public Button deleteSelect;

	// Token: 0x040064D8 RID: 25816
	[SerializeField]
	private CraftControler craftControler;

	// Token: 0x040064D9 RID: 25817
	private string[] warningMassage = new string[]
	{
		"モノが置かれているので置くことができません",
		"置く位置の高さが、すべて同じではないので置けません",
		"これ以上積めません"
	};

	// Token: 0x040064DA RID: 25818
	private IntReactiveProperty cost = new IntReactiveProperty(0);

	// Token: 0x040064DB RID: 25819
	private int prevCost;

	// Token: 0x040064DC RID: 25820
	private BoolReactiveProperty fade = new BoolReactiveProperty(false);

	// Token: 0x040064DD RID: 25821
	private float alphaVel;

	// Token: 0x040064DE RID: 25822
	private Color[] WarningdefColor;

	// Token: 0x040064DF RID: 25823
	public const int nMaxCost = 500;
}
