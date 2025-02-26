using System;
using System.Collections.Generic;
using System.Text;

namespace AClockworkBerry
{
	// Token: 0x020004BB RID: 1211
	public class TopNContainer
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x00089774 File Offset: 0x00087B74
		public bool TryAdd(double value, string text)
		{
			if (this.TopN.Count == 6 && value <= this.TopN[this.TopN.Count - 1].Key)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < this.TopN.Count; i++)
			{
				if (value > this.TopN[i].Key)
				{
					this.TopN.Insert(i, new KeyValuePair<double, string>(value, text));
					flag = true;
					break;
				}
			}
			if (flag)
			{
				while (this.TopN.Count > 6)
				{
					this.TopN.RemoveAt(this.TopN.Count - 1);
				}
				return true;
			}
			if (this.TopN.Count < 6)
			{
				this.TopN.Add(new KeyValuePair<double, string>(value, text));
				return true;
			}
			return false;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00089868 File Offset: 0x00087C68
		public string ItemToString(int i)
		{
			this.m_strBuilder.Length = 0;
			return this.m_strBuilder.AppendFormat("({0:0.00}) {1}", this.TopN[i].Key, this.TopN[i].Value).ToString();
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000898C3 File Offset: 0x00087CC3
		public void Clear()
		{
			this.TopN.Clear();
		}

		// Token: 0x04001917 RID: 6423
		public const int MaxCount = 6;

		// Token: 0x04001918 RID: 6424
		public List<KeyValuePair<double, string>> TopN = new List<KeyValuePair<double, string>>(6);

		// Token: 0x04001919 RID: 6425
		private StringBuilder m_strBuilder = new StringBuilder(256);
	}
}
