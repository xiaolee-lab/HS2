using System;

namespace AIProject.UI.Popup
{
	// Token: 0x02000FD6 RID: 4054
	public class RequestInfo
	{
		// Token: 0x060086E3 RID: 34531 RVA: 0x00384B6F File Offset: 0x00382F6F
		public RequestInfo(int _type, Tuple<int, int, int>[] _items)
		{
			this.Type = _type;
			this.Items = _items;
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x060086E4 RID: 34532 RVA: 0x00384B85 File Offset: 0x00382F85
		// (set) Token: 0x060086E5 RID: 34533 RVA: 0x00384B8D File Offset: 0x00382F8D
		public string[] Title { get; private set; }

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x060086E6 RID: 34534 RVA: 0x00384B96 File Offset: 0x00382F96
		// (set) Token: 0x060086E7 RID: 34535 RVA: 0x00384B9E File Offset: 0x00382F9E
		public int Type { get; private set; }

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x060086E8 RID: 34536 RVA: 0x00384BA7 File Offset: 0x00382FA7
		// (set) Token: 0x060086E9 RID: 34537 RVA: 0x00384BAF File Offset: 0x00382FAF
		public string[] Message { get; private set; }

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x060086EA RID: 34538 RVA: 0x00384BB8 File Offset: 0x00382FB8
		// (set) Token: 0x060086EB RID: 34539 RVA: 0x00384BC0 File Offset: 0x00382FC0
		public Tuple<int, int, int>[] Items { get; private set; }

		// Token: 0x060086EC RID: 34540 RVA: 0x00384BCC File Offset: 0x00382FCC
		public void SetText(string[] _title, string[] _message)
		{
			this.Title = _title;
			this.Message = _message;
			if (this.Type == 1 && this.Message == null)
			{
				this.Message = new string[]
				{
					string.Empty,
					string.Empty
				};
			}
			if (this.Type == 0 && this.Items == null)
			{
				this.Items = new Tuple<int, int, int>[0];
			}
		}
	}
}
