using System;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001361 RID: 4961
	public class UI_OnOffColor : MonoBehaviour
	{
		// Token: 0x0600A654 RID: 42580 RVA: 0x0043A244 File Offset: 0x00438644
		private void Start()
		{
			Toggle component = base.GetComponent<Toggle>();
			if (component)
			{
				this.OnChange(component.isOn);
			}
		}

		// Token: 0x0600A655 RID: 42581 RVA: 0x0043A270 File Offset: 0x00438670
		public void OnChange(bool check)
		{
			if (this.images != null)
			{
				Color color = (!check) ? this.offColor : this.onColor;
				foreach (Image image in this.images)
				{
					image.color = color;
				}
			}
		}

		// Token: 0x040082A9 RID: 33449
		public Image[] images;

		// Token: 0x040082AA RID: 33450
		public Color onColor = Color.white;

		// Token: 0x040082AB RID: 33451
		public Color offColor = Color.white;
	}
}
