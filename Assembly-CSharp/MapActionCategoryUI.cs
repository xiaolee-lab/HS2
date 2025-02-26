using System;
using AIProject;
using AIProject.UI;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000FB7 RID: 4023
public class MapActionCategoryUI : MenuUIBehaviour
{
	// Token: 0x17001D43 RID: 7491
	// (get) Token: 0x060085C8 RID: 34248 RVA: 0x0037B3D4 File Offset: 0x003797D4
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

	// Token: 0x060085C9 RID: 34249 RVA: 0x0037B404 File Offset: 0x00379804
	public void Init()
	{
		this.Input = Singleton<Manager.Input>.Instance;
		if (this.CategoryFilterID != null)
		{
			this.CategoryFilterID.Dispose();
		}
		this.CategoryFilterID.Subscribe(delegate(int x)
		{
			this.Cursor.transform.position = this.actionFilter.ActionToggles[x].transform.position;
		});
		for (int i = 0; i < this.actionFilter.ActionToggles.Length; i++)
		{
			int id = i;
			PointerEnterTrigger pointerEnterTrigger = this.actionFilter.ActionToggles[id].gameObject.GetComponent<PointerEnterTrigger>();
			if (pointerEnterTrigger == null)
			{
				pointerEnterTrigger = this.actionFilter.ActionToggles[id].gameObject.AddComponent<PointerEnterTrigger>();
			}
			UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
			if (pointerEnterTrigger.Triggers.Count > 0)
			{
				pointerEnterTrigger.Triggers.Clear();
			}
			pointerEnterTrigger.Triggers.Add(triggerEvent);
			triggerEvent.AddListener(delegate(BaseEventData x)
			{
				if (this.Input.FocusLevel == this.warpListUI.FocusLevel)
				{
					this.Input.FocusLevel = this.FocusLevel;
					this.Input.MenuElements = this.MenuUIList;
					this.warpListUI.DelCursor();
				}
				this.CategoryFilterID.Value = id;
			});
		}
		if (this.Dispose != null)
		{
			this.Dispose.Dispose();
		}
		this.Dispose = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where this.Input.FocusLevel == base.FocusLevel
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
		ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
		{
			ActionID = ActionID.Submit
		};
		actionIDDownCommand.TriggerEvent.AddListener(delegate()
		{
			this.OnInputSubmit();
		});
		this._actionCommands.Clear();
		this._actionCommands.Add(actionIDDownCommand);
		base.Start();
	}

	// Token: 0x060085CA RID: 34250 RVA: 0x0037B5A0 File Offset: 0x003799A0
	private void OnUpdate()
	{
		if (!this.Cursor.enabled)
		{
			this.SetCursor();
		}
		this.ChangeShowPointIcon();
		this.Cursor.transform.position = this.actionFilter.ActionToggles[this.CategoryFilterID.Value].transform.position;
	}

	// Token: 0x060085CB RID: 34251 RVA: 0x0037B5FC File Offset: 0x003799FC
	private void ChangeShowPointIcon()
	{
		if (this.Input.IsPressedKey(ActionID.RightShoulder1) || this.Input.IsPressedKey(KeyCode.E))
		{
			int num = this.CategoryFilterID.Value;
			num++;
			if (!this.areaMapUI.GameClear)
			{
				num = ((num <= 28) ? num : 28);
			}
			else
			{
				num = ((num <= 29) ? num : 29);
			}
			this.CategoryFilterID.Value = num;
		}
		else if (this.Input.IsPressedKey(ActionID.LeftShoulder1) || this.Input.IsPressedKey(KeyCode.Q))
		{
			int num2 = this.CategoryFilterID.Value;
			num2--;
			num2 = ((num2 >= 0) ? num2 : 0);
			this.CategoryFilterID.Value = num2;
		}
	}

	// Token: 0x060085CC RID: 34252 RVA: 0x0037B6D4 File Offset: 0x00379AD4
	public void SetCursor()
	{
		this.Cursor.transform.position = this.actionFilter.ActionToggles[this.CategoryFilterID.Value].transform.position;
		this.Cursor.enabled = true;
	}

	// Token: 0x060085CD RID: 34253 RVA: 0x0037B713 File Offset: 0x00379B13
	public void DelCursor()
	{
		this.Cursor.enabled = false;
	}

	// Token: 0x060085CE RID: 34254 RVA: 0x0037B724 File Offset: 0x00379B24
	public override void OnInputMoveDirection(MoveDirection moveDir)
	{
		if (moveDir == MoveDirection.Down)
		{
			if (this.areaMapUI._WarpNodes != null && this.areaMapUI._WarpNodes.Count > 0)
			{
				this.Cursor.enabled = false;
				this.Input.FocusLevel = this.warpListUI.FocusLevel;
				this.Input.MenuElements = this.warpListUI.MenuUIList;
				this.warpListUI._WarpID = ((!this.areaMapUI.GameClear) ? 1 : 0);
				this.warpListUI.SetCursor();
			}
		}
	}

	// Token: 0x060085CF RID: 34255 RVA: 0x0037B7CD File Offset: 0x00379BCD
	private void OnInputSubmit()
	{
		this.actionFilter.ActionToggles[this.CategoryFilterID.Value].isOn ^= true;
	}

	// Token: 0x04006CA1 RID: 27809
	[SerializeField]
	private Image Cursor;

	// Token: 0x04006CA2 RID: 27810
	[SerializeField]
	private AllAreaMapUI areaMapUI;

	// Token: 0x04006CA3 RID: 27811
	[SerializeField]
	private AllAreaMapActionFilter actionFilter;

	// Token: 0x04006CA4 RID: 27812
	[SerializeField]
	private WarpListUI warpListUI;

	// Token: 0x04006CA5 RID: 27813
	private IntReactiveProperty CategoryFilterID = new IntReactiveProperty(0);

	// Token: 0x04006CA6 RID: 27814
	private Manager.Input Input;

	// Token: 0x04006CA7 RID: 27815
	private MenuUIBehaviour[] _menuUIList;

	// Token: 0x04006CA8 RID: 27816
	private IDisposable Dispose;
}
