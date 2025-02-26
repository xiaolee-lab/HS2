using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B1D RID: 2845
public class HSceneSpriteLightCategory : MonoBehaviour
{
	// Token: 0x06005373 RID: 21363 RVA: 0x0024B914 File Offset: 0x00249D14
	public void SetValue(float _value, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.lstSlider.Count; i++)
			{
				this.lstSlider[i].value = _value;
			}
		}
		else if (this.lstSlider.Count > _array)
		{
			this.lstSlider[_array].value = _value;
		}
	}

	// Token: 0x06005374 RID: 21364 RVA: 0x0024B97E File Offset: 0x00249D7E
	public float GetValue(int _array)
	{
		if (this.lstSlider.Count <= _array)
		{
			return 0f;
		}
		return this.lstSlider[_array].value;
	}

	// Token: 0x06005375 RID: 21365 RVA: 0x0024B9A8 File Offset: 0x00249DA8
	public void SetEnable(bool _enable, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.lstButton.Count; i++)
			{
				if (this.lstButton[i].interactable != _enable)
				{
					this.lstButton[i].interactable = _enable;
				}
			}
		}
		else if (this.lstButton.Count > _array && this.lstButton[_array].interactable != _enable)
		{
			this.lstButton[_array].interactable = _enable;
		}
	}

	// Token: 0x06005376 RID: 21366 RVA: 0x0024BA40 File Offset: 0x00249E40
	public void SetActive(bool _active, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.lstButton.Count; i++)
			{
				if (this.lstButton[i].isActiveAndEnabled != _active)
				{
					this.lstButton[i].gameObject.SetActive(_active);
				}
			}
		}
		else if (this.lstButton.Count > _array && this.lstButton[_array].isActiveAndEnabled != _active)
		{
			this.lstButton[_array].gameObject.SetActive(_active);
		}
	}

	// Token: 0x04004E0B RID: 19979
	public List<Slider> lstSlider;

	// Token: 0x04004E0C RID: 19980
	public List<Button> lstButton;
}
