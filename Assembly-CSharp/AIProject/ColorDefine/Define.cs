using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject.ColorDefine
{
	// Token: 0x02000EBE RID: 3774
	public static class Define
	{
		// Token: 0x06007B97 RID: 31639 RVA: 0x00343C13 File Offset: 0x00342013
		public static Color Get(Colors colors)
		{
			return Define._colorDefine[colors];
		}

		// Token: 0x06007B98 RID: 31640 RVA: 0x00343C20 File Offset: 0x00342020
		public static void Set(ref Color color, Colors colors, bool setAlpha = false)
		{
			float a = color.a;
			color = Define._colorDefine[colors];
			if (setAlpha)
			{
				color.a = a;
			}
		}

		// Token: 0x04006352 RID: 25426
		private static readonly Dictionary<Colors, Color> _colorDefine = new Dictionary<Colors, Color>
		{
			{
				Colors.White,
				new Color32(235, 226, 215, byte.MaxValue)
			},
			{
				Colors.LightGreen,
				new Color32(133, 214, 83, byte.MaxValue)
			},
			{
				Colors.Green,
				new Color32(100, 185, 22, byte.MaxValue)
			},
			{
				Colors.Yellow,
				new Color32(204, 197, 59, byte.MaxValue)
			},
			{
				Colors.Blue,
				new Color32(0, 183, 238, byte.MaxValue)
			},
			{
				Colors.Cian,
				new Color32(98, 215, 245, byte.MaxValue)
			},
			{
				Colors.DarkRed,
				new Color32(198, 69, 73, byte.MaxValue)
			},
			{
				Colors.Red,
				new Color32(222, 69, 41, byte.MaxValue)
			},
			{
				Colors.Black,
				new Color32(23, 30, 36, byte.MaxValue)
			},
			{
				Colors.DarkBlack,
				new Color32(3, 4, 5, byte.MaxValue)
			},
			{
				Colors.Orange,
				new Color32(237, 122, 35, byte.MaxValue)
			}
		};
	}
}
