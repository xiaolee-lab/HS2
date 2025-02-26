using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000E3E RID: 3646
	public class Hyphenation : UIBehaviour
	{
		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x0600722E RID: 29230 RVA: 0x003084DC File Offset: 0x003068DC
		public Text Label
		{
			[CompilerGenerated]
			get
			{
				Text result;
				if ((result = this._label) == null)
				{
					result = (this._label = base.GetComponent<Text>());
				}
				return result;
			}
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x0600722F RID: 29231 RVA: 0x00308508 File Offset: 0x00306908
		public RectTransform RectTransform
		{
			[CompilerGenerated]
			get
			{
				RectTransform result;
				if ((result = this._rectTransform) == null)
				{
					result = (this._rectTransform = (base.transform as RectTransform));
				}
				return result;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06007230 RID: 29232 RVA: 0x00308538 File Offset: 0x00306938
		// (set) Token: 0x06007231 RID: 29233 RVA: 0x00308558 File Offset: 0x00306958
		public float TextWidth
		{
			get
			{
				return this.RectTransform.rect.width;
			}
			set
			{
				this.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06007232 RID: 29234 RVA: 0x00308567 File Offset: 0x00306967
		// (set) Token: 0x06007233 RID: 29235 RVA: 0x00308574 File Offset: 0x00306974
		public int FontSize
		{
			get
			{
				return this._label.fontSize;
			}
			set
			{
				this._label.fontSize = value;
			}
		}

		// Token: 0x06007234 RID: 29236 RVA: 0x00308582 File Offset: 0x00306982
		public void Set(string t)
		{
			this._text = t;
		}

		// Token: 0x06007235 RID: 29237 RVA: 0x0030858B File Offset: 0x0030698B
		private void UpdateText(string str)
		{
			this.Label.text = this.GetFormattedText(this.Label, str);
		}

		// Token: 0x06007236 RID: 29238 RVA: 0x003085A8 File Offset: 0x003069A8
		private bool IsHeightOver(Text label)
		{
			return label.preferredHeight > this.RectTransform.rect.height;
		}

		// Token: 0x06007237 RID: 29239 RVA: 0x003085D0 File Offset: 0x003069D0
		private bool IsLineCountOver(Text label, int lineCount)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < lineCount; i++)
			{
				stringBuilder.Append("\n");
			}
			label.text = stringBuilder.ToString();
			return label.preferredHeight > this.RectTransform.rect.height;
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x00308628 File Offset: 0x00306A28
		private float GetSpaceWidth(Text label)
		{
			float textWidth = this.GetTextWidth(label, "m m");
			float textWidth2 = this.GetTextWidth(label, "mm");
			return textWidth - textWidth2;
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x00308652 File Offset: 0x00306A52
		private float GetTextWidth(Text label, string message)
		{
			if (this._label.supportRichText)
			{
				message = Regex.Replace(message, Hyphenation.RichTextReplace, string.Empty);
			}
			label.text = message;
			return label.preferredWidth;
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x00308684 File Offset: 0x00306A84
		private string GetFormattedText(Text label, string msg)
		{
			if (msg.IsNullOrEmpty())
			{
				return string.Empty;
			}
			float width = this.RectTransform.rect.width;
			float spaceWidth = this.GetSpaceWidth(label);
			label.horizontalOverflow = HorizontalWrapMode.Overflow;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			float num2 = 0f;
			string text = "\n";
			bool flag = Hyphenation.ExistsBehindHyphenation(msg[0]);
			bool flag2 = false;
			foreach (string text2 in this.GetWordList(msg))
			{
				float num3 = this.GetTextWidth(label, text2);
				num2 += width;
				if (text2 == text)
				{
					num2 = 0f;
					num2 += spaceWidth * 2f;
					num++;
				}
				else
				{
					if (text2 == string.Empty)
					{
						num2 += spaceWidth;
					}
					if (flag)
					{
						if (!flag2)
						{
							flag2 = this.IsLineCountOver(label, num - 1);
						}
						if (flag2)
						{
							num3 = 0f;
						}
						if (num2 > width - num3)
						{
							stringBuilder.Append(text);
							stringBuilder.Append("\u3000");
							num2 = this.GetTextWidth(label, text2);
							num2 += spaceWidth * 2f;
							num++;
						}
					}
					else if (num2 > width)
					{
						stringBuilder.Append(text);
						num2 = this.GetTextWidth(label, text2);
						num++;
					}
				}
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600723B RID: 29243 RVA: 0x00308834 File Offset: 0x00306C34
		private List<string> GetWordList(string source)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			char c = '\0';
			for (int i = 0; i < source.Length; i++)
			{
				char c2 = source[i];
				char c3 = (i >= source.Length - 1) ? c : source[i + 1];
				char c4 = (i <= 0) ? c : source[i - 1];
				stringBuilder.Append(c2);
				bool flag = Hyphenation.ExistsLatin(c2) && Hyphenation.ExistsLatin(c4) && Hyphenation.ExistsLatin(c3) && !Hyphenation.ExistsLatin(c4);
				bool flag2 = !Hyphenation.ExistsLatin(c2) && Hyphenation.ExistsBehindHyphenation(c4);
				bool flag3 = !Hyphenation.ExistsLatin(c3) && !Hyphenation.ExistsAheadHyphenation(c3) && !Hyphenation.ExistsBehindHyphenation(c2);
				bool flag4 = i == source.Length - 1;
				if (flag || flag2 || flag3 || flag4)
				{
					list.Add(stringBuilder.ToString());
					stringBuilder = new StringBuilder();
				}
			}
			return list;
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x00308968 File Offset: 0x00306D68
		private static bool ExistsAheadHyphenation(char c)
		{
			foreach (char c2 in Hyphenation.AheadHyphenationSet)
			{
				if (c2 == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x003089A0 File Offset: 0x00306DA0
		private static bool ExistsBehindHyphenation(char c)
		{
			foreach (char c2 in Hyphenation.BehindHyphenationSet)
			{
				if (c2 == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x003089D8 File Offset: 0x00306DD8
		private static bool ExistsLatin(char c)
		{
			foreach (char c2 in Hyphenation.LatinHyphenationSet)
			{
				if (c2 == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x00308A0D File Offset: 0x00306E0D
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this.UpdateText(this._text);
		}

		// Token: 0x04005D57 RID: 23895
		private static readonly string RichTextReplace = "(\\<color=.*\\>|</color>)\\<size=.n\\>|</size>|<b>|</b><i>|</i>";

		// Token: 0x04005D58 RID: 23896
		private static readonly char[] AheadHyphenationSet = ",)]｝、。）〕〉》」』】〙〗〟’”｠»ァィゥェォッャュョヮヵヶっぁぃぅぇぉっゃゅょゎ‐゠–〜ー?!！？‼⁇⁈⁉・:;。.".ToCharArray();

		// Token: 0x04005D59 RID: 23897
		private static readonly char[] BehindHyphenationSet = "(（[｛〔〈《「『【〘〖〝‘“｟«".ToCharArray();

		// Token: 0x04005D5A RID: 23898
		private static readonly char[] LatinHyphenationSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789<>=/().,".ToCharArray();

		// Token: 0x04005D5B RID: 23899
		[SerializeField]
		private string _text = string.Empty;

		// Token: 0x04005D5C RID: 23900
		private Text _label;

		// Token: 0x04005D5D RID: 23901
		private RectTransform _rectTransform;
	}
}
