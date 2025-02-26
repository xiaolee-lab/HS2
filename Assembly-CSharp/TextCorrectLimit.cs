using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A1B RID: 2587
public static class TextCorrectLimit
{
	// Token: 0x06004CFE RID: 19710 RVA: 0x001D9404 File Offset: 0x001D7804
	public static string CorrectString(Text text, string baseStr, string endStr)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(text.gameObject, null, false);
		Text component = gameObject.GetComponent<Text>();
		float spaceWidth = TextCorrectLimit.GetSpaceWidth(component);
		int length = endStr.Length;
		float num = 0f;
		for (int i = 0; i < length; i++)
		{
			num += ((endStr[i] != ' ') ? TextCorrectLimit.GetTextWidth(component, endStr.Substring(i, 1)) : spaceWidth);
		}
		float num2 = text.rectTransform.rect.width - num;
		length = baseStr.Length;
		int num3 = 0;
		num = 0f;
		for (int j = 0; j < length; j++)
		{
			num += ((baseStr[j] != ' ') ? TextCorrectLimit.GetTextWidth(component, baseStr.Substring(j, 1)) : spaceWidth);
			if (num >= num2)
			{
				break;
			}
			num3++;
		}
		UnityEngine.Object.Destroy(gameObject);
		return baseStr.Substring(0, num3) + ((num3 != length) ? endStr : string.Empty);
	}

	// Token: 0x06004CFF RID: 19711 RVA: 0x001D951E File Offset: 0x001D791E
	public static void Correct(Text text, string baseStr, string endStr)
	{
		text.text = TextCorrectLimit.CorrectString(text, baseStr, endStr);
	}

	// Token: 0x06004D00 RID: 19712 RVA: 0x001D9530 File Offset: 0x001D7930
	private static float GetSpaceWidth(Text textComp)
	{
		float textWidth = TextCorrectLimit.GetTextWidth(textComp, "m m");
		float textWidth2 = TextCorrectLimit.GetTextWidth(textComp, "mm");
		return textWidth - textWidth2;
	}

	// Token: 0x06004D01 RID: 19713 RVA: 0x001D9558 File Offset: 0x001D7958
	private static float GetTextWidth(Text textComp, string message)
	{
		textComp.text = message;
		return textComp.preferredWidth;
	}
}
