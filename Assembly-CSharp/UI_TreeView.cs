using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02001165 RID: 4453
public class UI_TreeView : MonoBehaviour
{
	// Token: 0x0600930C RID: 37644 RVA: 0x003CF8F3 File Offset: 0x003CDCF3
	private void Start()
	{
		if (this.rootFlag)
		{
			this.CreateTree(this);
			if (this.ExpandFirst)
			{
				this.ExpandAll();
			}
			else
			{
				this.CollapseAll();
			}
		}
	}

	// Token: 0x0600930D RID: 37645 RVA: 0x003CF924 File Offset: 0x003CDD24
	private void CreateTree(UI_TreeView tvroot)
	{
		if (null == this.minmax)
		{
			this.minmax = base.gameObject.GetComponent<Toggle>();
		}
		if (this.minmax)
		{
			this.minmax.onValueChanged.AddListener(new UnityAction<bool>(this.MinMaxChange));
		}
		this.tvRoot = tvroot;
		IEnumerator enumerator = base.gameObject.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				UI_TreeView component = transform.gameObject.GetComponent<UI_TreeView>();
				if (!(null == component))
				{
					this.lstChild.Add(component);
					component.CreateTree(tvroot);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600930E RID: 37646 RVA: 0x003CFA0C File Offset: 0x003CDE0C
	public void UpdateView(ref float totalPosY, float parentPosY)
	{
		float parentPosY2 = totalPosY;
		if (this.rootFlag)
		{
			totalPosY = -this.topMargin;
		}
		else
		{
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			if (component)
			{
				component.anchoredPosition = new Vector2(component.anchoredPosition.x, totalPosY - parentPosY);
				if (base.gameObject.activeSelf && !this.unused)
				{
					totalPosY -= component.sizeDelta.y;
				}
			}
		}
		IEnumerator enumerator = base.gameObject.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				UI_TreeView component2 = transform.gameObject.GetComponent<UI_TreeView>();
				if (!(null == component2))
				{
					if (!base.gameObject.activeSelf || component2.unused)
					{
						transform.gameObject.SetActive(false);
					}
					else if (this.minmax)
					{
						transform.gameObject.SetActive(this.minmax.isOn);
					}
					component2.UpdateView(ref totalPosY, parentPosY2);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		if (this.rootFlag && this.rtScroll)
		{
			this.rtScroll.sizeDelta = new Vector2(this.rtScroll.sizeDelta.x, -totalPosY + this.bottomMargin);
			if (this.scrollRect)
			{
				this.scrollRect.enabled = false;
				this.scrollRect.enabled = true;
			}
		}
	}

	// Token: 0x0600930F RID: 37647 RVA: 0x003CFBE8 File Offset: 0x003CDFE8
	public void ExpandAll()
	{
		if (!this.rootFlag)
		{
			return;
		}
		this.ChangeExpandOrCollapseLoop(true);
		float num = 0f;
		this.UpdateView(ref num, 0f);
	}

	// Token: 0x06009310 RID: 37648 RVA: 0x003CFC1C File Offset: 0x003CE01C
	public void CollapseAll()
	{
		if (!this.rootFlag)
		{
			return;
		}
		this.ChangeExpandOrCollapseLoop(false);
		float num = 0f;
		this.UpdateView(ref num, 0f);
	}

	// Token: 0x06009311 RID: 37649 RVA: 0x003CFC50 File Offset: 0x003CE050
	private void ChangeExpandOrCollapseLoop(bool expand)
	{
		if (this.minmax)
		{
			this.minmax.isOn = expand;
		}
		IEnumerator enumerator = base.gameObject.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				UI_TreeView component = transform.gameObject.GetComponent<UI_TreeView>();
				if (!(null == component))
				{
					component.ChangeExpandOrCollapseLoop(expand);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06009312 RID: 37650 RVA: 0x003CFCF4 File Offset: 0x003CE0F4
	public void SetUnused(bool flag)
	{
		this.unused = flag;
		base.gameObject.SetActive(!this.unused);
	}

	// Token: 0x06009313 RID: 37651 RVA: 0x003CFD11 File Offset: 0x003CE111
	private void Update()
	{
	}

	// Token: 0x06009314 RID: 37652 RVA: 0x003CFD14 File Offset: 0x003CE114
	private void MinMaxChange(bool flag)
	{
		float num = 0f;
		if (this.tvRoot)
		{
			this.tvRoot.UpdateView(ref num, 0f);
		}
	}

	// Token: 0x06009315 RID: 37653 RVA: 0x003CFD4C File Offset: 0x003CE14C
	public void UpdateView()
	{
		float num = 0f;
		if (this.tvRoot && this.tvRoot.gameObject.activeSelf)
		{
			this.tvRoot.UpdateView(ref num, 0f);
		}
	}

	// Token: 0x06009316 RID: 37654 RVA: 0x003CFD96 File Offset: 0x003CE196
	public UI_TreeView GetRoot()
	{
		return this.tvRoot;
	}

	// Token: 0x040076F3 RID: 30451
	public bool rootFlag;

	// Token: 0x040076F4 RID: 30452
	public bool ExpandFirst = true;

	// Token: 0x040076F5 RID: 30453
	public float topMargin = 2f;

	// Token: 0x040076F6 RID: 30454
	public float bottomMargin = 2f;

	// Token: 0x040076F7 RID: 30455
	public ScrollRect scrollRect;

	// Token: 0x040076F8 RID: 30456
	public RectTransform rtScroll;

	// Token: 0x040076F9 RID: 30457
	public bool unused;

	// Token: 0x040076FA RID: 30458
	private Toggle minmax;

	// Token: 0x040076FB RID: 30459
	private UI_TreeView tvRoot;

	// Token: 0x040076FC RID: 30460
	private List<UI_TreeView> lstChild = new List<UI_TreeView>();
}
