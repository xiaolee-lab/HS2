using System;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A30 RID: 2608
public class UI_ToggleOnOff : MonoBehaviour
{
	// Token: 0x06004D75 RID: 19829 RVA: 0x001DBA54 File Offset: 0x001D9E54
	private void Start()
	{
		Toggle component = base.GetComponent<Toggle>();
		if (component)
		{
			this.OnChange(component.isOn);
		}
	}

	// Token: 0x06004D76 RID: 19830 RVA: 0x001DBA80 File Offset: 0x001D9E80
	public void OnChange(bool check)
	{
		if (this.imgOn != null)
		{
			foreach (Image image in this.imgOn)
			{
				if (null != image)
				{
					image.enabled = check;
				}
			}
		}
		if (this.imgOff != null)
		{
			foreach (Image image2 in this.imgOff)
			{
				if (null != image2)
				{
					image2.enabled = !check;
				}
			}
		}
		if (this.objOn != null)
		{
			foreach (GameObject gameObject in this.objOn)
			{
				if (null != gameObject)
				{
					gameObject.SetActiveIfDifferent(check);
				}
			}
		}
		if (this.objOff != null)
		{
			foreach (GameObject gameObject2 in this.objOff)
			{
				if (null != gameObject2)
				{
					gameObject2.SetActiveIfDifferent(!check);
				}
			}
		}
		if (this.cgOn != null)
		{
			foreach (CanvasGroup canvasGroup in this.cgOn)
			{
				if (null != canvasGroup)
				{
					canvasGroup.Enable(check, false);
				}
			}
		}
		if (this.cgOff != null)
		{
			foreach (CanvasGroup canvasGroup2 in this.cgOff)
			{
				if (null != canvasGroup2)
				{
					canvasGroup2.Enable(!check, false);
				}
			}
		}
	}

	// Token: 0x040046D0 RID: 18128
	public Image[] imgOn;

	// Token: 0x040046D1 RID: 18129
	public Image[] imgOff;

	// Token: 0x040046D2 RID: 18130
	public GameObject[] objOn;

	// Token: 0x040046D3 RID: 18131
	public GameObject[] objOff;

	// Token: 0x040046D4 RID: 18132
	public CanvasGroup[] cgOn;

	// Token: 0x040046D5 RID: 18133
	public CanvasGroup[] cgOff;
}
