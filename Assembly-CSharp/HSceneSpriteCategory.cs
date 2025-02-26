using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B0A RID: 2826
public class HSceneSpriteCategory : MonoBehaviour
{
	// Token: 0x060052F5 RID: 21237 RVA: 0x00244370 File Offset: 0x00242770
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

	// Token: 0x060052F6 RID: 21238 RVA: 0x00244408 File Offset: 0x00242808
	public bool GetEnable(int _array)
	{
		return this.lstButton.Count > _array && this.lstButton[_array].interactable;
	}

	// Token: 0x060052F7 RID: 21239 RVA: 0x00244430 File Offset: 0x00242830
	public int GetAllEnable()
	{
		int num = 0;
		for (int i = 0; i < this.lstButton.Count; i++)
		{
			if (this.lstButton[i].interactable)
			{
				num++;
			}
		}
		return (num != this.lstButton.Count) ? ((num != 0) ? 2 : 0) : 1;
	}

	// Token: 0x060052F8 RID: 21240 RVA: 0x0024449C File Offset: 0x0024289C
	public virtual void SetActive(bool _active, int _array = -1)
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
		else if (this.lstButton.Count > _array)
		{
			if (this.lstButton[_array].isActiveAndEnabled == _active)
			{
				return;
			}
			this.lstButton[_array].gameObject.SetActive(_active);
		}
	}

	// Token: 0x060052F9 RID: 21241 RVA: 0x00244544 File Offset: 0x00242944
	public bool[] GetActiveButton()
	{
		bool[] array = new bool[this.lstButton.Count];
		for (int i = 0; i < this.lstButton.Count; i++)
		{
			array[i] = this.lstButton[i].gameObject.activeSelf;
		}
		return array;
	}

	// Token: 0x04004D74 RID: 19828
	public List<Button> lstButton;
}
