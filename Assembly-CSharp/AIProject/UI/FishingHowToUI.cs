using System;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000F29 RID: 3881
	public class FishingHowToUI : MonoBehaviour
	{
		// Token: 0x06007FEA RID: 32746 RVA: 0x00363F3E File Offset: 0x0036233E
		public void Set(Sprite _sprite, string _text)
		{
			if (this.image)
			{
				this.image.sprite = _sprite;
			}
			if (this.text)
			{
				this.text.text = _text;
			}
		}

		// Token: 0x04006709 RID: 26377
		public Image image;

		// Token: 0x0400670A RID: 26378
		public Text text;
	}
}
