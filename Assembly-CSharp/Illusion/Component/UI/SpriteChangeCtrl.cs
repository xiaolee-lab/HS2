using System;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Component.UI
{
	// Token: 0x0200087E RID: 2174
	public class SpriteChangeCtrl : MonoBehaviour
	{
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x0014945F File Offset: 0x0014785F
		public Image image
		{
			get
			{
				return this._image;
			}
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x00149467 File Offset: 0x00147867
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x00149478 File Offset: 0x00147878
		public void OnChangeValue(int _num)
		{
			if (this._image == null)
			{
				return;
			}
			if (this.sprites.Length <= _num)
			{
				return;
			}
			bool flag = _num >= 0;
			this._image.enabled = flag;
			if (flag)
			{
				this._image.sprite = this.sprites[_num];
			}
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x001494D3 File Offset: 0x001478D3
		public int GetCount()
		{
			return this.sprites.Length;
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x001494E0 File Offset: 0x001478E0
		public int GetVisibleNumber()
		{
			if (this._image == null)
			{
				return -1;
			}
			for (int i = 0; i < this.sprites.Length; i++)
			{
				if (this._image.sprite == this.sprites[i])
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x04003846 RID: 14406
		public Sprite[] sprites;

		// Token: 0x04003847 RID: 14407
		private Image _image;
	}
}
