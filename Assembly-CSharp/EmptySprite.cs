using System;
using UnityEngine;

// Token: 0x02000628 RID: 1576
public static class EmptySprite
{
	// Token: 0x06002589 RID: 9609 RVA: 0x000D6C59 File Offset: 0x000D5059
	public static Sprite Get()
	{
		if (EmptySprite.instance == null)
		{
			EmptySprite.instance = Resources.Load<Sprite>("procedural_ui_image_default_sprite");
		}
		return EmptySprite.instance;
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x000D6C7F File Offset: 0x000D507F
	public static bool IsEmptySprite(Sprite s)
	{
		return EmptySprite.Get() == s;
	}

	// Token: 0x04002558 RID: 9560
	private static Sprite instance;
}
