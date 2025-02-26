using System;
using AIProject;
using AIProject.UI;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000FC8 RID: 4040
public class WarpListUI : MenuUIBehaviour
{
	// Token: 0x17001D46 RID: 7494
	// (get) Token: 0x0600863A RID: 34362 RVA: 0x00382004 File Offset: 0x00380404
	// (set) Token: 0x0600863B RID: 34363 RVA: 0x00382011 File Offset: 0x00380411
	public int _WarpID
	{
		get
		{
			return this.WarpID.Value;
		}
		set
		{
			this.WarpID.Value = value;
		}
	}

	// Token: 0x17001D47 RID: 7495
	// (get) Token: 0x0600863C RID: 34364 RVA: 0x00382020 File Offset: 0x00380420
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

	// Token: 0x0600863D RID: 34365 RVA: 0x00382050 File Offset: 0x00380450
	public void Init()
	{
		this.Input = Singleton<Manager.Input>.Instance;
		this.WarpID.Subscribe(delegate(int x)
		{
			if (x == 0)
			{
				this.Cursor[0].enabled = true;
				this.Cursor[1].enabled = false;
			}
			else if (x > 0)
			{
				this.Cursor[0].enabled = false;
				this.Cursor[1].enabled = true;
			}
		});
		this.disposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where this.Input.FocusLevel == base.FocusLevel
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
		PointerEnterTrigger pointerEnterTrigger = this.areaMapUI._WorldMap.gameObject.GetComponent<PointerEnterTrigger>();
		if (pointerEnterTrigger == null)
		{
			pointerEnterTrigger = this.areaMapUI._WorldMap.gameObject.AddComponent<PointerEnterTrigger>();
		}
		UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
		pointerEnterTrigger.Triggers.Add(triggerEvent);
		triggerEvent.AddListener(delegate(BaseEventData x)
		{
			if (this.Input.FocusLevel == this.mapActionCategoryUI.FocusLevel)
			{
				this.Input.FocusLevel = base.FocusLevel;
				this.Input.MenuElements = this.MenuUIList;
				this.mapActionCategoryUI.DelCursor();
			}
			this._WarpID = 0;
		});
		ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
		{
			ActionID = ActionID.Submit
		};
		actionIDDownCommand.TriggerEvent.AddListener(delegate()
		{
			this.OnInputSubmit();
		});
		this._actionCommands.Add(actionIDDownCommand);
		base.Start();
	}

	// Token: 0x0600863E RID: 34366 RVA: 0x0038214F File Offset: 0x0038054F
	public void SetCursor()
	{
		this.Cursor[this.WarpID.Value].enabled = true;
	}

	// Token: 0x0600863F RID: 34367 RVA: 0x00382169 File Offset: 0x00380569
	public void DelCursor()
	{
		this.Cursor[0].enabled = false;
		this.Cursor[1].enabled = false;
		this._WarpID = -1;
	}

	// Token: 0x06008640 RID: 34368 RVA: 0x00382190 File Offset: 0x00380590
	private void OnUpdate()
	{
		if (!this.Cursor[(!this.areaMapUI.GameClear) ? 1 : 0].enabled && this.WarpID.Value == ((!this.areaMapUI.GameClear) ? 1 : 0))
		{
			this.SetCursor();
		}
		if (this._WarpID > 0 && this.areaMapUI._WarpNodes != null && this.areaMapUI._WarpNodes.Count > 0)
		{
			this.Cursor[1].transform.position = this.areaMapUI._WarpNodes[this._WarpID - 1].transform.position;
		}
	}

	// Token: 0x06008641 RID: 34369 RVA: 0x00382258 File Offset: 0x00380658
	public override void OnInputMoveDirection(MoveDirection moveDir)
	{
		if (moveDir != MoveDirection.Up)
		{
			if (moveDir == MoveDirection.Down)
			{
				int num = this.WarpID.Value;
				num++;
				if (num > this.areaMapUI._WarpNodes.Count)
				{
					num = this.areaMapUI._WarpNodes.Count;
				}
				this.WarpID.Value = num;
				if (num > this.EndShowID + 1)
				{
					this.StartShowID++;
					this.EndShowID++;
				}
				float num2 = 1f - (float)this.StartShowID / (float)(this.areaMapUI._WarpNodes.Count - 5);
				num2 = Mathf.Clamp(num2, 0f, 1f);
				this.scrollRect.verticalNormalizedPosition = num2;
			}
		}
		else
		{
			int num3 = this.WarpID.Value;
			num3--;
			if (num3 < ((!this.areaMapUI.GameClear) ? 1 : 0))
			{
				this.Input.FocusLevel = this.mapActionCategoryUI.FocusLevel;
				this.Input.MenuElements = this.mapActionCategoryUI.MenuUIList;
				num3 = ((!this.areaMapUI.GameClear) ? 1 : 0);
				this.Cursor[num3].enabled = false;
			}
			this.WarpID.Value = num3;
			if (num3 < this.StartShowID + 1)
			{
				this.StartShowID--;
				this.EndShowID--;
			}
			float num4 = 1f - (float)this.StartShowID / (float)(this.areaMapUI._WarpNodes.Count - 5);
			num4 = Mathf.Clamp(num4, 0f, 1f);
			this.scrollRect.verticalNormalizedPosition = num4;
		}
	}

	// Token: 0x06008642 RID: 34370 RVA: 0x00382424 File Offset: 0x00380824
	private void OnInputSubmit()
	{
		if (this.Input.FocusLevel != base.FocusLevel)
		{
			return;
		}
		if (this.WarpID.Value == 0)
		{
			Button.ButtonClickedEvent onClick = this.areaMapUI._WorldMap.onClick;
			if (onClick != null)
			{
				onClick.Invoke();
			}
		}
		else
		{
			Button.ButtonClickedEvent onClick2 = this.areaMapUI._WarpNodes[this.WarpID.Value - 1].onClick;
			if (onClick2 != null)
			{
				onClick2.Invoke();
			}
		}
	}

	// Token: 0x06008643 RID: 34371 RVA: 0x003824AB File Offset: 0x003808AB
	public void DisposeWarpListUI()
	{
		if (this.disposable != null)
		{
			this.disposable.Dispose();
			this.disposable = null;
		}
	}

	// Token: 0x04006D4E RID: 27982
	[SerializeField]
	private Image[] Cursor;

	// Token: 0x04006D4F RID: 27983
	[SerializeField]
	private AllAreaMapUI areaMapUI;

	// Token: 0x04006D50 RID: 27984
	[SerializeField]
	private MapActionCategoryUI mapActionCategoryUI;

	// Token: 0x04006D51 RID: 27985
	public ScrollRect scrollRect;

	// Token: 0x04006D52 RID: 27986
	private IntReactiveProperty WarpID = new IntReactiveProperty(-1);

	// Token: 0x04006D53 RID: 27987
	private Manager.Input Input;

	// Token: 0x04006D54 RID: 27988
	public int StartShowID;

	// Token: 0x04006D55 RID: 27989
	public int EndShowID = 4;

	// Token: 0x04006D56 RID: 27990
	private const int ShowNodeNum = 5;

	// Token: 0x04006D57 RID: 27991
	private MenuUIBehaviour[] _menuUIList;

	// Token: 0x04006D58 RID: 27992
	private IDisposable disposable;
}
