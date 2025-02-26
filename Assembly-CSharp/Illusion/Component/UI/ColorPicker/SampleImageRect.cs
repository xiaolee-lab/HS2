using System;
using UnityEngine;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x02001058 RID: 4184
	public class SampleImageRect : SampleImage
	{
		// Token: 0x06008CCD RID: 36045 RVA: 0x003B0589 File Offset: 0x003AE989
		private void Start()
		{
			this.rect.SetColor(this.image.color);
			this.rect.updateColorAction += delegate(Color color)
			{
				this.image.color = color;
			};
		}

		// Token: 0x04007286 RID: 29318
		[SerializeField]
		private PickerRect rect;
	}
}
