using System;
using System.Runtime.CompilerServices;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000FAF RID: 4015
public class AllAreaMapActionFilter : MonoBehaviour
{
	// Token: 0x17001D3F RID: 7487
	// (get) Token: 0x060085B1 RID: 34225 RVA: 0x00379CBB File Offset: 0x003780BB
	public Toggle[] ActionToggles
	{
		[CompilerGenerated]
		get
		{
			return this._ActionToggles;
		}
	}

	// Token: 0x060085B2 RID: 34226 RVA: 0x00379CC4 File Offset: 0x003780C4
	public void Init(MiniMapControler miniMapCtrl, AllAreaMapUI _allAreaMapUI)
	{
		this.CanH = Game.isAdd01;
		this.HToggle.gameObject.SetActive(this.CanH);
		this.miniMapcontroler = miniMapCtrl;
		this.AllAreaMapUI = _allAreaMapUI;
		this.ActionCategoryShow.onClick.RemoveAllListeners();
		this.ActionCategoryShow.onClick.AddListener(delegate()
		{
			this.ActionOpen ^= true;
			this.ActionMoving = true;
			this.ActionMovingTime = 0f;
			if (this.mapAction != null)
			{
				this.mapAction.DelCursor();
			}
		});
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where this.enabled && this.ActionMoving
		select _).Subscribe(delegate(long _)
		{
			this.ChangeActionWindowState();
		});
		for (int i = 0; i < this._ActionToggles.Length; i++)
		{
			int id = i;
			Image component = this._ActionToggles[id].GetComponent<Image>();
			if (id == 0)
			{
				Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Category, 0, component, true);
				this._ActionToggles[id].onValueChanged.RemoveAllListeners();
				this._ActionToggles[id].onValueChanged.AddListener(delegate(bool x)
				{
					miniMapCtrl.AllAreaMap.GetComponent<AllAreaCameraControler>().ChangeActionFilterAllTgl(x);
					for (int j = 1; j < this._ActionToggles.Length; j++)
					{
						this._ActionToggles[j].isOn = x;
					}
					if (x)
					{
						Color color = Color.white;
						this._ActionToggles[id].GetComponent<Image>().color = color;
					}
					else
					{
						Color color = Color.gray;
						this._ActionToggles[id].GetComponent<Image>().color = color;
					}
				});
			}
			else
			{
				int num = -1;
				if (this.IDContain(id, ref num))
				{
					if (this.IconIDs[num].Category == 0)
					{
						Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Category, this.IconIDs[num].IconID, component, true);
					}
					else if (this.IconIDs[num].Category == 1)
					{
						Sprite sprite = component.sprite;
						Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(this.IconIDs[num].IconID, out sprite);
					}
				}
				this._ActionToggles[id].onValueChanged.RemoveAllListeners();
				this._ActionToggles[id].onValueChanged.AddListener(delegate(bool x)
				{
					miniMapCtrl.AllAreaMap.GetComponent<AllAreaCameraControler>().ChangeactionFilter((MapUIActionCategory)id, x);
					if (x)
					{
						Color color = Color.white;
						this._ActionToggles[id].GetComponent<Image>().color = color;
					}
					else
					{
						Color color = Color.gray;
						this._ActionToggles[id].GetComponent<Image>().color = color;
					}
				});
			}
		}
	}

	// Token: 0x060085B3 RID: 34227 RVA: 0x00379EDC File Offset: 0x003782DC
	public void ChangeActionWindowState()
	{
		Vector2 anchoredPosition = this.actionWindowRect.anchoredPosition;
		int num = (!this.CanH) ? 1 : 0;
		float a;
		float b;
		if (this.ActionOpen)
		{
			a = this.ClosePos;
			b = this.OpenPositions[num].pos[(!(this.AllAreaMapUI != null)) ? 1 : ((!this.AllAreaMapUI.GameClear) ? 1 : 0)];
		}
		else
		{
			a = this.OpenPositions[num].pos[(!(this.AllAreaMapUI != null)) ? 1 : ((!this.AllAreaMapUI.GameClear) ? 1 : 0)];
			b = this.ClosePos;
		}
		this.ActionMovingTime += Time.unscaledDeltaTime;
		float num2 = this.ActionMovingTime / this.ActionMovingEndTime;
		if (num2 >= 1f)
		{
			num2 = 1f;
			this.ActionMoving = false;
		}
		anchoredPosition.x = Mathf.Lerp(a, b, num2);
		this.actionWindowRect.anchoredPosition = anchoredPosition;
	}

	// Token: 0x060085B4 RID: 34228 RVA: 0x0037A00C File Offset: 0x0037840C
	public void Refresh()
	{
		Vector2 anchoredPosition = this.actionWindowRect.anchoredPosition;
		int num = (!this.CanH) ? 1 : 0;
		anchoredPosition.x = ((!this.ActionOpen) ? this.ClosePos : this.OpenPositions[num].pos[(!(this.AllAreaMapUI != null)) ? 1 : ((!this.AllAreaMapUI.GameClear) ? 1 : 0)]);
		this.actionWindowRect.anchoredPosition = anchoredPosition;
		AllAreaCameraControler component = this.miniMapcontroler.AllAreaMap.GetComponent<AllAreaCameraControler>();
		component.ChangeActionFilterAllTgl(this._ActionToggles[0].isOn);
		for (int i = 1; i < this._ActionToggles.Length; i++)
		{
			component.ChangeactionFilter((MapUIActionCategory)i, this._ActionToggles[i].isOn);
		}
	}

	// Token: 0x060085B5 RID: 34229 RVA: 0x0037A0F4 File Offset: 0x003784F4
	private bool IDContain(int check, ref int id)
	{
		for (int i = 0; i < this.IconIDs.Length; i++)
		{
			if (this.IconIDs[i].ToggleID == check)
			{
				id = i;
				return true;
			}
		}
		return false;
	}

	// Token: 0x04006C64 RID: 27748
	[SerializeField]
	[Tooltip("エロありか")]
	private bool CanH;

	// Token: 0x04006C65 RID: 27749
	[SerializeField]
	private Button ActionCategoryShow;

	// Token: 0x04006C66 RID: 27750
	[SerializeField]
	private Toggle[] _ActionToggles;

	// Token: 0x04006C67 RID: 27751
	[SerializeField]
	private Toggle HToggle;

	// Token: 0x04006C68 RID: 27752
	[SerializeField]
	private AllAreaMapActionFilter.IconIDInfo[] IconIDs;

	// Token: 0x04006C69 RID: 27753
	[SerializeField]
	private AllAreaMapActionFilter.OpenPos[] OpenPositions;

	// Token: 0x04006C6A RID: 27754
	[SerializeField]
	private float ClosePos;

	// Token: 0x04006C6B RID: 27755
	[SerializeField]
	private MapActionCategoryUI mapAction;

	// Token: 0x04006C6C RID: 27756
	private bool ActionOpen = true;

	// Token: 0x04006C6D RID: 27757
	private bool ActionMoving;

	// Token: 0x04006C6E RID: 27758
	private float ActionMovingTime;

	// Token: 0x04006C6F RID: 27759
	[SerializeField]
	private float ActionMovingEndTime = 1f;

	// Token: 0x04006C70 RID: 27760
	[SerializeField]
	private RectTransform actionWindowRect;

	// Token: 0x04006C71 RID: 27761
	private AllAreaMapUI AllAreaMapUI;

	// Token: 0x04006C72 RID: 27762
	private MiniMapControler miniMapcontroler;

	// Token: 0x02000FB0 RID: 4016
	[Serializable]
	public struct OpenPos
	{
		// Token: 0x04006C73 RID: 27763
		public float[] pos;
	}

	// Token: 0x02000FB1 RID: 4017
	[Serializable]
	public struct IconIDInfo
	{
		// Token: 0x04006C74 RID: 27764
		public int ToggleID;

		// Token: 0x04006C75 RID: 27765
		public int Category;

		// Token: 0x04006C76 RID: 27766
		public int IconID;
	}
}
