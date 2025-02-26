using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteToParticlesAsset
{
	// Token: 0x0200059A RID: 1434
	public class SpritesDataPool
	{
		// Token: 0x06002136 RID: 8502 RVA: 0x000B5678 File Offset: 0x000B3A78
		public static Color[] GetSpriteColors(Sprite sprite, int x, int y, int blockWidth, int blockHeight)
		{
			if (SpritesDataPool.spritesShared == null)
			{
				SpritesDataPool.spritesShared = new Dictionary<Sprite, Color[]>();
			}
			Color[] array;
			if (!SpritesDataPool.spritesShared.ContainsKey(sprite))
			{
				array = sprite.texture.GetPixels(x, y, blockWidth, blockHeight);
				SpritesDataPool.spritesShared.Add(sprite, array);
			}
			else
			{
				array = SpritesDataPool.spritesShared[sprite];
			}
			return array;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000B56D8 File Offset: 0x000B3AD8
		public static void ReleaseMemory()
		{
			SpritesDataPool.spritesShared = null;
		}

		// Token: 0x040020A4 RID: 8356
		private static Dictionary<Sprite, Color[]> spritesShared = new Dictionary<Sprite, Color[]>();
	}
}
