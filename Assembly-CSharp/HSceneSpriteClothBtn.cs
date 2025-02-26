using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B10 RID: 2832
public class HSceneSpriteClothBtn : MonoBehaviour
{
	// Token: 0x17000EF0 RID: 3824
	// (get) Token: 0x0600531D RID: 21277 RVA: 0x0024643B File Offset: 0x0024483B
	private int State
	{
		[CompilerGenerated]
		get
		{
			return this.state;
		}
	}

	// Token: 0x0600531E RID: 21278 RVA: 0x00246444 File Offset: 0x00244844
	public void SetButton(int State)
	{
		this.state = State;
		if (State >= this.buttons.Length)
		{
			this.state = this.buttons.Length - 1;
		}
		for (int i = 0; i < this.buttons.Length; i++)
		{
			if (i != this.state)
			{
				this.buttons[i].gameObject.SetActive(false);
			}
			else
			{
				this.buttons[i].gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x04004D9C RID: 19868
	private int state;

	// Token: 0x04004D9D RID: 19869
	public Button[] buttons;
}
