using System;
using IllusionUtility.SetUtility;
using UnityEngine;

// Token: 0x02000847 RID: 2119
public class SpCalc : MonoBehaviour
{
	// Token: 0x06003625 RID: 13861 RVA: 0x0013F81C File Offset: 0x0013DC1C
	private Vector2 GetPivotInSprite(Sprite sprite)
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

	// Token: 0x06003626 RID: 13862 RVA: 0x0013F8FB File Offset: 0x0013DCFB
	private void Update()
	{
		this.Calc();
	}

	// Token: 0x06003627 RID: 13863 RVA: 0x0013F904 File Offset: 0x0013DD04
	public void Calc()
	{
		Transform parent = base.transform.parent;
		if (null == parent)
		{
			return;
		}
		SpRoot component = parent.GetComponent<SpRoot>();
		if (null == component)
		{
			return;
		}
		SpriteRenderer component2 = base.gameObject.transform.GetComponent<SpriteRenderer>();
		if (null == component2)
		{
			return;
		}
		if (null == component2.sprite)
		{
			return;
		}
		float baseScreenWidth = component.baseScreenWidth;
		float baseScreenHeight = component.baseScreenHeight;
		float spriteRate = component.GetSpriteRate();
		float spriteCorrectY = component.GetSpriteCorrectY();
		Vector2 pivotInSprite = this.GetPivotInSprite(component2.sprite);
		float x = pivotInSprite.x;
		float num = 1f - pivotInSprite.y;
		float x2 = (this.Pos.x - (baseScreenWidth * 0.5f - component2.sprite.rect.width * x)) * spriteRate * 0.01f;
		float num2 = (baseScreenHeight * 0.5f - component2.sprite.rect.height * num - this.Pos.y) * spriteRate * 0.01f;
		if (this.CorrectY == 0)
		{
			num2 += spriteCorrectY;
		}
		else if (this.CorrectY == 2)
		{
			num2 -= spriteCorrectY;
		}
		component2.transform.SetLocalPosition(x2, num2, 0f);
		float x3 = spriteRate * this.Scale.x;
		float y = spriteRate * this.Scale.y;
		component2.transform.SetLocalScale(x3, y, 1f);
	}

	// Token: 0x04003674 RID: 13940
	public Vector2 Pos = new Vector2(0f, 0f);

	// Token: 0x04003675 RID: 13941
	public Vector2 Scale = new Vector2(1f, 1f);

	// Token: 0x04003676 RID: 13942
	public byte CorrectX = 1;

	// Token: 0x04003677 RID: 13943
	public byte CorrectY = 1;
}
