using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteToParticlesAsset
{
	// Token: 0x02000599 RID: 1433
	public class LoadingTimeSpritesPool : MonoBehaviour
	{
		// Token: 0x06002133 RID: 8499 RVA: 0x000B55B3 File Offset: 0x000B39B3
		private void Awake()
		{
			if (this.loadAllOnAwake)
			{
				this.LoadAll();
			}
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000B55C8 File Offset: 0x000B39C8
		public void LoadAll()
		{
			foreach (Sprite sprite in this.spritesToLoad)
			{
				Rect rect = sprite.rect;
				SpritesDataPool.GetSpriteColors(sprite, (int)rect.position.x, (int)rect.position.y, (int)rect.size.x, (int)rect.size.y);
			}
		}

		// Token: 0x040020A2 RID: 8354
		[Tooltip("Drag here all the sprites to be loaded in the pool.")]
		public List<Sprite> spritesToLoad;

		// Token: 0x040020A3 RID: 8355
		[Tooltip("If enabled the load will be called on this GameObject’s Awake method. Otherwise it can be called by the method LoadAll() ")]
		public bool loadAllOnAwake;
	}
}
