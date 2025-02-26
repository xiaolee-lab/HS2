using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02001161 RID: 4449
public class SliderValueText : MonoBehaviour
{
	// Token: 0x17001F76 RID: 8054
	// (get) Token: 0x060092EC RID: 37612 RVA: 0x003CE904 File Offset: 0x003CCD04
	// (set) Token: 0x060092ED RID: 37613 RVA: 0x003CE90C File Offset: 0x003CCD0C
	public bool isParcent
	{
		get
		{
			return this._isParcent;
		}
		set
		{
			this._isParcent = value;
			this.UpdateText(this.value);
		}
	}

	// Token: 0x17001F77 RID: 8055
	// (get) Token: 0x060092EE RID: 37614 RVA: 0x003CE921 File Offset: 0x003CCD21
	// (set) Token: 0x060092EF RID: 37615 RVA: 0x003CE949 File Offset: 0x003CCD49
	public string text
	{
		get
		{
			return (!(this.label == null)) ? this.label.text : string.Empty;
		}
		set
		{
			if (this.label)
			{
				this.label.text = value;
			}
		}
	}

	// Token: 0x17001F78 RID: 8056
	// (get) Token: 0x060092F0 RID: 37616 RVA: 0x003CE967 File Offset: 0x003CCD67
	// (set) Token: 0x060092F1 RID: 37617 RVA: 0x003CE98F File Offset: 0x003CCD8F
	public float value
	{
		get
		{
			return (!(this.slider == null)) ? this.slider.value : 0f;
		}
		set
		{
			if (this.slider)
			{
				this.slider.value = value;
			}
		}
	}

	// Token: 0x060092F2 RID: 37618 RVA: 0x003CE9B0 File Offset: 0x003CCDB0
	private void UpdateText(float f)
	{
		this.text = (this.isParcent ? f.ToString("P0") : (f * 100f).ToString("0"));
	}

	// Token: 0x060092F3 RID: 37619 RVA: 0x003CE9F4 File Offset: 0x003CCDF4
	private void Awake()
	{
		if (this.slider == null)
		{
			this.slider = base.gameObject.GetComponent<Slider>();
		}
		if (this.slider != null)
		{
			this.slider.onValueChanged.AddListener(delegate(float f)
			{
				this.UpdateText(f);
			});
			this.UpdateText(this.slider.value);
		}
		if (this.label == null)
		{
			this.label = base.gameObject.transform.GetComponentInChildren<Text>();
		}
	}

	// Token: 0x060092F4 RID: 37620 RVA: 0x003CEA88 File Offset: 0x003CCE88
	private void OnDestroy()
	{
		if (this.slider != null)
		{
			this.slider.onValueChanged.RemoveListener(delegate(float f)
			{
				this.UpdateText(f);
			});
		}
	}

	// Token: 0x040076E5 RID: 30437
	[SerializeField]
	private Slider slider;

	// Token: 0x040076E6 RID: 30438
	[SerializeField]
	private Text label;

	// Token: 0x040076E7 RID: 30439
	private bool _isParcent;
}
