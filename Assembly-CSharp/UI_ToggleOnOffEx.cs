using System;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A31 RID: 2609
public class UI_ToggleOnOffEx : Toggle
{
	// Token: 0x06004D78 RID: 19832 RVA: 0x001DBC44 File Offset: 0x001DA044
	private void Initialize()
	{
		if (null == this.imgOn)
		{
			Transform transform = base.transform.Find("imgOn");
			if (null != transform)
			{
				this.imgOn = transform.GetComponent<Image>();
			}
		}
		if (null == this.imgOff)
		{
			Transform transform = base.transform.Find("imgOff");
			if (null != transform)
			{
				this.imgOff = transform.GetComponent<Image>();
			}
		}
		if (null == this.imgOnOver)
		{
			Transform transform = base.transform.Find("imgOnOver");
			if (null != transform)
			{
				this.imgOnOver = transform.GetComponent<Image>();
			}
			if (null != this.imgOnOver)
			{
				this.imgOnOver.enabled = false;
			}
		}
		if (null == this.imgOffOver)
		{
			Transform transform = base.transform.Find("imgOffOver");
			if (null != transform)
			{
				this.imgOffOver = transform.GetComponent<Image>();
			}
			if (null != this.imgOffOver)
			{
				this.imgOffOver.enabled = false;
			}
		}
	}

	// Token: 0x06004D79 RID: 19833 RVA: 0x001DBD75 File Offset: 0x001DA175
	protected override void Start()
	{
		base.Start();
		this.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
		{
			if (null != this.imgOn)
			{
				this.imgOn.gameObject.SetActiveIfDifferent(isOn);
			}
			if (null != this.imgOnOver)
			{
				this.imgOnOver.gameObject.SetActiveIfDifferent(isOn);
			}
			if (null != this.imgOff)
			{
				this.imgOff.gameObject.SetActiveIfDifferent(!isOn);
			}
			if (null != this.imgOffOver)
			{
				this.imgOffOver.gameObject.SetActiveIfDifferent(!isOn);
			}
		});
	}

	// Token: 0x06004D7A RID: 19834 RVA: 0x001DBD98 File Offset: 0x001DA198
	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		if (null != this.imgOnOver)
		{
			this.imgOnOver.enabled = base.interactable;
		}
		if (null != this.imgOffOver)
		{
			this.imgOffOver.enabled = base.interactable;
		}
	}

	// Token: 0x06004D7B RID: 19835 RVA: 0x001DBDF0 File Offset: 0x001DA1F0
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if (null != this.imgOnOver)
		{
			this.imgOnOver.enabled = false;
		}
		if (null != this.imgOffOver)
		{
			this.imgOffOver.enabled = false;
		}
	}

	// Token: 0x040046D6 RID: 18134
	[SerializeField]
	private Image imgOn;

	// Token: 0x040046D7 RID: 18135
	[SerializeField]
	private Image imgOff;

	// Token: 0x040046D8 RID: 18136
	[SerializeField]
	private Image imgOnOver;

	// Token: 0x040046D9 RID: 18137
	[SerializeField]
	private Image imgOffOver;
}
