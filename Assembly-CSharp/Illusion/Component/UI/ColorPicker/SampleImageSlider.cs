using System;
using UnityEngine;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x02001059 RID: 4185
	public class SampleImageSlider : SampleImage
	{
		// Token: 0x06008CD0 RID: 36048 RVA: 0x003B05CE File Offset: 0x003AE9CE
		private void Start()
		{
			this.slider.color = this.image.color;
			this.slider.updateColorAction += delegate(Color color)
			{
				this.image.color = color;
			};
		}

		// Token: 0x04007287 RID: 29319
		[SerializeField]
		private PickerSlider slider;
	}
}
