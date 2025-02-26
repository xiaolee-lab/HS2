using System;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x0200104E RID: 4174
	internal class ImagePack
	{
		// Token: 0x06008C57 RID: 35927 RVA: 0x003AE05C File Offset: 0x003AC45C
		public ImagePack(Image image)
		{
			this.rectTransform = image.rectTransform;
			this.tex2D = new Texture2D((int)this.rectTransform.rect.width, (int)this.rectTransform.rect.height);
			this.tex2D.Apply();
			image.sprite = Sprite.Create(this.tex2D, new Rect(0f, 0f, (float)this.tex2D.width, (float)this.tex2D.height), Vector2.zero);
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x06008C58 RID: 35928 RVA: 0x003AE0F6 File Offset: 0x003AC4F6
		// (set) Token: 0x06008C59 RID: 35929 RVA: 0x003AE0FE File Offset: 0x003AC4FE
		public RectTransform rectTransform { get; private set; }

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x06008C5A RID: 35930 RVA: 0x003AE107 File Offset: 0x003AC507
		// (set) Token: 0x06008C5B RID: 35931 RVA: 0x003AE10F File Offset: 0x003AC50F
		public Texture2D tex2D { get; private set; }

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x06008C5C RID: 35932 RVA: 0x003AE118 File Offset: 0x003AC518
		public Vector2 size
		{
			get
			{
				return new Vector2((float)this.tex2D.width, (float)this.tex2D.height);
			}
		}

		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x06008C5D RID: 35933 RVA: 0x003AE137 File Offset: 0x003AC537
		public bool isTex
		{
			get
			{
				return this.tex2D != null;
			}
		}

		// Token: 0x06008C5E RID: 35934 RVA: 0x003AE145 File Offset: 0x003AC545
		public void SetPixels(Color[] colors)
		{
			this.tex2D.SetPixels(colors);
			this.tex2D.Apply();
		}
	}
}
