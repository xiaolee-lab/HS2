using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000779 RID: 1913
[ExecuteInEditMode]
public class HyphenationJpn : UIBehaviour
{
	// Token: 0x06002CB8 RID: 11448 RVA: 0x0010079D File Offset: 0x000FEB9D
	[Conditional("UNITY_EDITOR")]
	private void CheckHeight()
	{
		if (!this.text.IsActive() || this.IsHeightOver(this.text))
		{
		}
	}

	// Token: 0x06002CB9 RID: 11449 RVA: 0x001007C0 File Offset: 0x000FEBC0
	public void SetText(Text text)
	{
		this._text = text;
	}

	// Token: 0x06002CBA RID: 11450 RVA: 0x001007C9 File Offset: 0x000FEBC9
	public void SetText(string str)
	{
		this.str = str;
		this.UpdateText(this.str);
	}

	// Token: 0x1700076B RID: 1899
	// (get) Token: 0x06002CBB RID: 11451 RVA: 0x001007E0 File Offset: 0x000FEBE0
	// (set) Token: 0x06002CBC RID: 11452 RVA: 0x00100800 File Offset: 0x000FEC00
	public float textWidth
	{
		get
		{
			return this.rectTransform.rect.width;
		}
		set
		{
			this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
		}
	}

	// Token: 0x1700076C RID: 1900
	// (get) Token: 0x06002CBD RID: 11453 RVA: 0x0010080F File Offset: 0x000FEC0F
	// (set) Token: 0x06002CBE RID: 11454 RVA: 0x0010081C File Offset: 0x000FEC1C
	public int fontSize
	{
		get
		{
			return this.text.fontSize;
		}
		set
		{
			this.text.fontSize = value;
		}
	}

	// Token: 0x1700076D RID: 1901
	// (get) Token: 0x06002CBF RID: 11455 RVA: 0x0010082A File Offset: 0x000FEC2A
	private RectTransform rectTransform
	{
		get
		{
			return this.GetComponentCache(ref this._rectTransform);
		}
	}

	// Token: 0x1700076E RID: 1902
	// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x00100838 File Offset: 0x000FEC38
	private Text text
	{
		get
		{
			return this.GetComponentCache(ref this._text);
		}
	}

	// Token: 0x06002CC1 RID: 11457 RVA: 0x00100846 File Offset: 0x000FEC46
	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange();
		this.UpdateText(this.str);
	}

	// Token: 0x06002CC2 RID: 11458 RVA: 0x0010085A File Offset: 0x000FEC5A
	private void UpdateText(string str)
	{
		this.text.text = this.GetFormatedText(this.text, str);
	}

	// Token: 0x06002CC3 RID: 11459 RVA: 0x00100874 File Offset: 0x000FEC74
	private bool IsHeightOver(Text textComp)
	{
		return textComp.preferredHeight > this.rectTransform.rect.height;
	}

	// Token: 0x06002CC4 RID: 11460 RVA: 0x0010089C File Offset: 0x000FEC9C
	private bool IsLineCountOver(Text textComp, int lineCount)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < lineCount; i++)
		{
			stringBuilder.Append("\n");
		}
		textComp.text = stringBuilder.ToString();
		return textComp.preferredHeight > this.rectTransform.rect.height;
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x001008F4 File Offset: 0x000FECF4
	private float GetSpaceWidth(Text textComp)
	{
		float textWidth = this.GetTextWidth(textComp, "m m");
		float textWidth2 = this.GetTextWidth(textComp, "mm");
		return textWidth - textWidth2;
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x0010091E File Offset: 0x000FED1E
	private float GetTextWidth(Text textComp, string message)
	{
		if (this._text.supportRichText)
		{
			message = Regex.Replace(message, HyphenationJpn.RITCH_TEXT_REPLACE, string.Empty);
		}
		textComp.text = message;
		return textComp.preferredWidth;
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x00100950 File Offset: 0x000FED50
	private string GetFormatedText(Text textComp, string msg)
	{
		if (string.IsNullOrEmpty(msg))
		{
			return string.Empty;
		}
		float width = this.rectTransform.rect.width;
		float spaceWidth = this.GetSpaceWidth(textComp);
		textComp.horizontalOverflow = HorizontalWrapMode.Overflow;
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		float num2 = 0f;
		string text = "\n";
		bool flag = HyphenationJpn.CHECK_HYP_BACK(msg[0]);
		bool flag2 = false;
		foreach (string text2 in this.GetWordList(msg))
		{
			float num3 = this.GetTextWidth(textComp, text2);
			num2 += num3;
			if (text2 == text)
			{
				num2 = 0f;
				num2 += spaceWidth * 2f;
				num++;
			}
			else
			{
				if (text2 == " ")
				{
					num2 += spaceWidth;
				}
				if (flag)
				{
					if (!flag2)
					{
						flag2 = this.IsLineCountOver(textComp, num + 1);
					}
					if (flag2)
					{
						num3 = 0f;
					}
					if (num2 > width - num3)
					{
						stringBuilder.Append(text);
						stringBuilder.Append("\u3000");
						num2 = this.GetTextWidth(textComp, text2);
						num2 += spaceWidth * 2f;
						num++;
					}
				}
				else if (num2 > width)
				{
					stringBuilder.Append(text);
					num2 = this.GetTextWidth(textComp, text2);
					num++;
				}
			}
			stringBuilder.Append(text2);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x00100B04 File Offset: 0x000FEF04
	private List<string> GetWordList(string tmpText)
	{
		List<string> list = new List<string>();
		StringBuilder stringBuilder = new StringBuilder();
		char c = '\0';
		for (int i = 0; i < tmpText.Length; i++)
		{
			char c2 = tmpText[i];
			char s = (i >= tmpText.Length - 1) ? c : tmpText[i + 1];
			char s2 = (i <= 0) ? c : tmpText[i - 1];
			stringBuilder.Append(c2);
			if ((HyphenationJpn.IsLatin(c2) && HyphenationJpn.IsLatin(s2) && HyphenationJpn.IsLatin(c2) && !HyphenationJpn.IsLatin(s2)) || (!HyphenationJpn.IsLatin(c2) && HyphenationJpn.CHECK_HYP_BACK(s2)) || (!HyphenationJpn.IsLatin(s) && !HyphenationJpn.CHECK_HYP_FRONT(s) && !HyphenationJpn.CHECK_HYP_BACK(c2)) || i == tmpText.Length - 1)
			{
				list.Add(stringBuilder.ToString());
				stringBuilder = new StringBuilder();
			}
		}
		return list;
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x00100C14 File Offset: 0x000FF014
	private static bool CHECK_HYP_FRONT(char str)
	{
		return Array.Exists<char>(HyphenationJpn.HYP_FRONT, (char item) => item == str);
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x00100C44 File Offset: 0x000FF044
	private static bool CHECK_HYP_BACK(char str)
	{
		return Array.Exists<char>(HyphenationJpn.HYP_BACK, (char item) => item == str);
	}

	// Token: 0x06002CCB RID: 11467 RVA: 0x00100C74 File Offset: 0x000FF074
	private static bool IsLatin(char s)
	{
		return Array.Exists<char>(HyphenationJpn.HYP_LATIN, (char item) => item == s);
	}

	// Token: 0x04002B78 RID: 11128
	[TextArea(3, 10)]
	[SerializeField]
	private string str;

	// Token: 0x04002B79 RID: 11129
	private RectTransform _rectTransform;

	// Token: 0x04002B7A RID: 11130
	private Text _text;

	// Token: 0x04002B7B RID: 11131
	private static readonly string RITCH_TEXT_REPLACE = "(\\<color=.*\\>|</color>|\\<size=.n\\>|</size>|<b>|</b>|<i>|</i>)";

	// Token: 0x04002B7C RID: 11132
	private static readonly char[] HYP_FRONT = ",)]｝、。）〕〉》」』】〙〗〟’”｠»ァィゥェォッャュョヮヵヶっぁぃぅぇぉっゃゅょゎ‐゠–〜ー?!！？‼⁇⁈⁉・:;。.".ToCharArray();

	// Token: 0x04002B7D RID: 11133
	private static readonly char[] HYP_BACK = "(（[｛〔〈《「『【〘〖〝‘“｟«".ToCharArray();

	// Token: 0x04002B7E RID: 11134
	private static readonly char[] HYP_LATIN = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789<>=/().,".ToCharArray();
}
