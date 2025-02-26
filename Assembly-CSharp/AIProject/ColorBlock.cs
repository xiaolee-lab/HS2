using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F96 RID: 3990
	[Serializable]
	public struct ColorBlock : IEquatable<ColorBlock>
	{
		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x0600851A RID: 34074 RVA: 0x003748D6 File Offset: 0x00372CD6
		// (set) Token: 0x0600851B RID: 34075 RVA: 0x003748DE File Offset: 0x00372CDE
		public Color NormalColor
		{
			get
			{
				return this._normalColor;
			}
			set
			{
				this._normalColor = value;
			}
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x0600851C RID: 34076 RVA: 0x003748E7 File Offset: 0x00372CE7
		// (set) Token: 0x0600851D RID: 34077 RVA: 0x003748EF File Offset: 0x00372CEF
		public Color HighlightColor
		{
			get
			{
				return this._highlightColor;
			}
			set
			{
				this._highlightColor = value;
			}
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x0600851E RID: 34078 RVA: 0x003748F8 File Offset: 0x00372CF8
		// (set) Token: 0x0600851F RID: 34079 RVA: 0x00374900 File Offset: 0x00372D00
		public Color PressedColor
		{
			get
			{
				return this._pressedColor;
			}
			set
			{
				this._pressedColor = value;
			}
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06008520 RID: 34080 RVA: 0x0037490C File Offset: 0x00372D0C
		public static ColorBlock Default
		{
			get
			{
				return new ColorBlock
				{
					_normalColor = default(Color),
					_highlightColor = default(Color),
					_pressedColor = default(Color)
				};
			}
		}

		// Token: 0x06008521 RID: 34081 RVA: 0x00374952 File Offset: 0x00372D52
		public override bool Equals(object obj)
		{
			return obj is ColorBlock && this.Equals((ColorBlock)obj);
		}

		// Token: 0x06008522 RID: 34082 RVA: 0x00374970 File Offset: 0x00372D70
		public bool Equals(ColorBlock other)
		{
			return this.NormalColor == other.NormalColor && this.HighlightColor == other.HighlightColor && this.PressedColor == other.PressedColor;
		}

		// Token: 0x06008523 RID: 34083 RVA: 0x003749C0 File Offset: 0x00372DC0
		public static bool operator ==(ColorBlock a, ColorBlock b)
		{
			return a.Equals(b);
		}

		// Token: 0x06008524 RID: 34084 RVA: 0x003749CA File Offset: 0x00372DCA
		public static bool operator !=(ColorBlock a, ColorBlock b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06008525 RID: 34085 RVA: 0x003749D7 File Offset: 0x00372DD7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04006BBA RID: 27578
		[SerializeField]
		private Color _normalColor;

		// Token: 0x04006BBB RID: 27579
		[SerializeField]
		private Color _highlightColor;

		// Token: 0x04006BBC RID: 27580
		[SerializeField]
		private Color _pressedColor;
	}
}
