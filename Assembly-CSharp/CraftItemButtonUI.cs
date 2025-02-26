using System;
using AIProject.UI;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000EF5 RID: 3829
public class CraftItemButtonUI : MenuUIBehaviour
{
	// Token: 0x1700188A RID: 6282
	// (get) Token: 0x06007CFF RID: 31999 RVA: 0x00357B5C File Offset: 0x00355F5C
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

	// Token: 0x1700188B RID: 6283
	// (get) Token: 0x06007D00 RID: 32000 RVA: 0x00357B89 File Offset: 0x00355F89
	// (set) Token: 0x06007D01 RID: 32001 RVA: 0x00357B91 File Offset: 0x00355F91
	public Action<MoveDirection> OnMove { get; set; }

	// Token: 0x1700188C RID: 6284
	// (get) Token: 0x06007D02 RID: 32002 RVA: 0x00357B9A File Offset: 0x00355F9A
	// (set) Token: 0x06007D03 RID: 32003 RVA: 0x00357BA7 File Offset: 0x00355FA7
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

	// Token: 0x06007D04 RID: 32004 RVA: 0x00357BB8 File Offset: 0x00355FB8
	protected override void Start()
	{
		this._isActive.Subscribe(delegate(bool x)
		{
			this.SetActiveControl(x);
		});
		this.OnMove = delegate(MoveDirection x)
		{
			this.SelectMove(x);
		};
		if (this.carsolPanel != null)
		{
			this.color = this.carsolPanel.GetComponent<Image>().color;
		}
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled && this.carsolPanel.activeSelf
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
	}

	// Token: 0x06007D05 RID: 32005 RVA: 0x00357C4C File Offset: 0x0035604C
	private void OnUpdate()
	{
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
		this.carsolPanel.GetComponent<Image>().color = this.color;
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		if (currentSelectedGameObject.transform.parent != this.ContentsRectTransform.transform)
		{
			return;
		}
		this.SelectedRectTransform = currentSelectedGameObject.GetComponent<RectTransform>();
		Vector3 vector = this.rectTransform.localPosition - this.SelectedRectTransform.localPosition;
		float num = this.ContentsRectTransform.rect.height - this.rectTransform.rect.height;
		float num2 = this.ContentsRectTransform.rect.height - vector.y;
		float num3 = this.scroll.normalizedPosition.y * num;
		float num4 = num3 - this.SelectedRectTransform.rect.height / 2f + this.rectTransform.rect.height;
		float num5 = num3 + this.SelectedRectTransform.rect.height / 2f;
		this.carsolPanel.transform.position = Vector3.SmoothDamp(this.carsolPanel.transform.position, this.ItemKind[this.selectedID].transform.position, ref this._velocity, this._followAccelerationTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		if (num2 > num4)
		{
			float num6 = num2 - num4;
			float num7 = num3 + num6;
			float target = num7 / num;
			this.scroll.verticalNormalizedPosition = Mathf.SmoothDamp(this.scroll.verticalNormalizedPosition, target, ref this._vel, this._followAccelerationTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		}
		else if (num2 < num5)
		{
			float num8 = num2 - num5;
			float num9 = num3 + num8;
			float target2 = num9 / num;
			this.scroll.verticalNormalizedPosition = Mathf.SmoothDamp(this.scroll.verticalNormalizedPosition, target2, ref this._vel, this._followAccelerationTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		}
	}

	// Token: 0x06007D06 RID: 32006 RVA: 0x00357F3C File Offset: 0x0035633C
	private void SetActiveControl(bool isActive)
	{
		Manager.Input instance = Singleton<Manager.Input>.Instance;
		if (isActive)
		{
			instance.FocusLevel = 0;
			instance.MenuElements = this.MenuUIList;
			instance.ReserveState(Manager.Input.ValidType.UI);
			instance.SetupState();
			this.selectedID = 0;
			this.ItemKind[this.selectedID].Select();
			this.carsolPanel.SetActive(true);
		}
		else
		{
			instance.ClearMenuElements();
			instance.FocusLevel = -1;
			instance.ReserveState(Manager.Input.ValidType.Action);
			instance.SetupState();
			this.carsolPanel.SetActive(false);
		}
	}

	// Token: 0x06007D07 RID: 32007 RVA: 0x00357FC8 File Offset: 0x003563C8
	private void SelectMove(MoveDirection moveDir)
	{
		if (moveDir != MoveDirection.Down)
		{
			if (moveDir == MoveDirection.Up)
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
		this.ItemKind[this.selectedID].Select();
	}

	// Token: 0x06007D08 RID: 32008 RVA: 0x00358048 File Offset: 0x00356448
	public void CreateCategoryButton()
	{
		this.ItemKind = new Button[18];
		for (int i = 0; i < this.ItemKind.Length; i++)
		{
			this.ItemKind[i] = UnityEngine.Object.Instantiate<Button>(this.ItemKindButton);
			this.ItemKind[i].transform.SetParent(this.ContentsRectTransform.transform, false);
			this.ItemKind[i].gameObject.SetActive(true);
			if (Singleton<CraftCommandListBaseObject>.Instance.CategoryNames.ContainsKey(i + 1))
			{
				this.ItemKind[i].GetComponentInChildren<Text>().text = Singleton<CraftCommandListBaseObject>.Instance.CategoryNames[i + 1];
			}
		}
	}

	// Token: 0x06007D09 RID: 32009 RVA: 0x003580FA File Offset: 0x003564FA
	public override void OnInputMoveDirection(MoveDirection moveDir)
	{
		this.OnMove(moveDir);
		this.ItemKind[this.selectedID].Select();
	}

	// Token: 0x04006509 RID: 25865
	public Button[] ItemKind;

	// Token: 0x0400650A RID: 25866
	public Button ItemKindButton;

	// Token: 0x0400650B RID: 25867
	public ScrollRect scroll;

	// Token: 0x0400650C RID: 25868
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x0400650D RID: 25869
	[SerializeField]
	private RectTransform ContentsRectTransform;

	// Token: 0x0400650E RID: 25870
	private RectTransform SelectedRectTransform;

	// Token: 0x0400650F RID: 25871
	private int selectedID;

	// Token: 0x04006510 RID: 25872
	private MenuUIBehaviour[] _menuUIList;

	// Token: 0x04006512 RID: 25874
	[SerializeField]
	private GameObject carsolPanel;

	// Token: 0x04006513 RID: 25875
	private Color color;

	// Token: 0x04006514 RID: 25876
	private Vector3 _velocity = Vector3.zero;

	// Token: 0x04006515 RID: 25877
	private float _vel;

	// Token: 0x04006516 RID: 25878
	private float _alphaVelocity;

	// Token: 0x04006517 RID: 25879
	private bool bAlphaAdd = true;
}
