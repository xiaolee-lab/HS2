using System;
using System.Linq;
using AIChara;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x0200099B RID: 2459
	public class CustomColorSet : MonoBehaviour
	{
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x001B3400 File Offset: 0x001B1800
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x001B3407 File Offset: 0x001B1807
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x001B3414 File Offset: 0x001B1814
		public void SetColor(Color color)
		{
			this.image.color = color;
			this.customBase.customColorCtrl.SetColor(this, color);
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x001B3434 File Offset: 0x001B1834
		public void EnableColorAlpha(bool enable)
		{
			this.useAlpha = enable;
			this.customBase.customColorCtrl.EnableAlpha(this.useAlpha);
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x001B3454 File Offset: 0x001B1854
		public void Reset()
		{
			this.title = base.transform.GetComponentInChildren<Text>();
			Image[] componentsInChildren = base.transform.GetComponentsInChildren<Image>();
			if (componentsInChildren != null)
			{
				this.image = (from x in componentsInChildren
				where x.name == "imgColor"
				select x).FirstOrDefault<Image>();
			}
			this.button = base.transform.GetComponentInChildren<Button>();
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x001B34C3 File Offset: 0x001B18C3
		public void Start()
		{
			if (this.button)
			{
				this.button.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.customBase.customColorCtrl.Setup(this, this.image.color, delegate(Color color)
					{
						this.image.color = color;
						if (this.actUpdateColor != null)
						{
							this.actUpdateColor(color);
						}
					}, this.useAlpha);
				});
			}
		}

		// Token: 0x040041E1 RID: 16865
		public Text title;

		// Token: 0x040041E2 RID: 16866
		public Button button;

		// Token: 0x040041E3 RID: 16867
		public Image image;

		// Token: 0x040041E4 RID: 16868
		public bool useAlpha;

		// Token: 0x040041E5 RID: 16869
		public Action<Color> actUpdateColor;
	}
}
