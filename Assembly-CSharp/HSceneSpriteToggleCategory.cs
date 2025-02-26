using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B0C RID: 2828
public class HSceneSpriteToggleCategory : MonoBehaviour
{
	// Token: 0x06005302 RID: 21250 RVA: 0x002447BD File Offset: 0x00242BBD
	public int GetToggleNum()
	{
		return this.lstToggle.Count;
	}

	// Token: 0x06005303 RID: 21251 RVA: 0x002447CC File Offset: 0x00242BCC
	public void SetEnable(bool _enable, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.lstToggle.Count; i++)
			{
				if (this.lstToggle[i].interactable != _enable)
				{
					this.lstToggle[i].interactable = _enable;
				}
			}
		}
		else if (this.lstToggle.Count > _array && this.lstToggle[_array].interactable != _enable)
		{
			this.lstToggle[_array].interactable = _enable;
		}
	}

	// Token: 0x06005304 RID: 21252 RVA: 0x00244864 File Offset: 0x00242C64
	public bool GetEnable(int _array)
	{
		return this.lstToggle.Count > _array && this.lstToggle[_array].interactable;
	}

	// Token: 0x06005305 RID: 21253 RVA: 0x0024488C File Offset: 0x00242C8C
	public int GetAllEnable()
	{
		int num = 0;
		for (int i = 0; i < this.lstToggle.Count; i++)
		{
			if (this.lstToggle[i].interactable)
			{
				num++;
			}
		}
		return (num != this.lstToggle.Count) ? ((num != 0) ? 2 : 0) : 1;
	}

	// Token: 0x06005306 RID: 21254 RVA: 0x002448F8 File Offset: 0x00242CF8
	public void SetActive(bool _active, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.lstToggle.Count; i++)
			{
				if (this.lstToggle[i].isActiveAndEnabled != _active)
				{
					this.lstToggle[i].gameObject.SetActive(_active);
				}
			}
		}
		else if (this.lstToggle.Count > _array && this.lstToggle[_array].isActiveAndEnabled != _active)
		{
			this.lstToggle[_array].gameObject.SetActive(_active);
		}
	}

	// Token: 0x06005307 RID: 21255 RVA: 0x0024499A File Offset: 0x00242D9A
	public bool GetActive(int _array)
	{
		return this.lstToggle.Count > _array && this.lstToggle[_array].isActiveAndEnabled;
	}

	// Token: 0x06005308 RID: 21256 RVA: 0x002449C0 File Offset: 0x00242DC0
	public void SetCheck(bool _check, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.lstToggle.Count; i++)
			{
				this.lstToggle[i].isOn = _check;
			}
		}
		else if (this.lstToggle.Count > _array)
		{
			this.lstToggle[_array].isOn = _check;
		}
	}

	// Token: 0x04004D7F RID: 19839
	public List<Toggle> lstToggle;
}
