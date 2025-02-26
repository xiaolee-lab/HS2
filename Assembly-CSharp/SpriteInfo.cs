using System;
using UnityEngine;

// Token: 0x020011A7 RID: 4519
public class SpriteInfo
{
	// Token: 0x0600949C RID: 38044 RVA: 0x003D4CF4 File Offset: 0x003D30F4
	public static Vector2 GetPivotInSprite(Sprite sprite)
	{
		Vector2 result = default(Vector2);
		if (null != sprite)
		{
			if (sprite.bounds.size.x != 0f)
			{
				result.x = 0.5f - sprite.bounds.center.x / sprite.bounds.size.x;
			}
			if (sprite.bounds.size.y != 0f)
			{
				result.y = 0.5f - sprite.bounds.center.y / sprite.bounds.size.y;
			}
		}
		return result;
	}
}
