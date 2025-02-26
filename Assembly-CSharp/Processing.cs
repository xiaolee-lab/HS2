using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200100F RID: 4111
public class Processing : MonoBehaviour
{
	// Token: 0x06008A2E RID: 35374 RVA: 0x003A23C4 File Offset: 0x003A07C4
	private void Start()
	{
		for (int i = 0; i < 12; i++)
		{
			this.color[i] = Color.HSVToRGB(0f, 0f, 1f - (float)i * 0.02f);
		}
		int index = 0;
		Observable.Interval(TimeSpan.FromMilliseconds(50.0)).Subscribe(delegate(long _)
		{
			if (this.update)
			{
				index = (index + 11) % 12;
				for (int j = 0; j < 12; j++)
				{
					int num = (index + j) % 12;
					this.img[j].color = this.color[num];
				}
			}
			for (int k = 0; k < 12; k++)
			{
				this.img[k].enabled = this.update;
			}
		}).AddTo(this);
	}

	// Token: 0x06008A2F RID: 35375 RVA: 0x003A2451 File Offset: 0x003A0851
	private void Update()
	{
	}

	// Token: 0x04007076 RID: 28790
	[SerializeField]
	private Image[] img;

	// Token: 0x04007077 RID: 28791
	private Color[] color = new Color[12];

	// Token: 0x04007078 RID: 28792
	public bool update = true;
}
