using System;
using System.Text;
using Rewired.UI;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x0200056E RID: 1390
	public class PlayerPointerEventData : PointerEventData
	{
		// Token: 0x06001D6B RID: 7531 RVA: 0x000AC1C3 File Offset: 0x000AA5C3
		public PlayerPointerEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.playerId = -1;
			this.inputSourceIndex = -1;
			this.buttonIndex = -1;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x000AC1E1 File Offset: 0x000AA5E1
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x000AC1E9 File Offset: 0x000AA5E9
		public int playerId { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x000AC1F2 File Offset: 0x000AA5F2
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x000AC1FA File Offset: 0x000AA5FA
		public int inputSourceIndex { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000AC203 File Offset: 0x000AA603
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x000AC20B File Offset: 0x000AA60B
		public IMouseInputSource mouseSource { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000AC214 File Offset: 0x000AA614
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x000AC21C File Offset: 0x000AA61C
		public ITouchInputSource touchSource { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x000AC225 File Offset: 0x000AA625
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x000AC22D File Offset: 0x000AA62D
		public PointerEventType sourceType { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x000AC236 File Offset: 0x000AA636
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x000AC23E File Offset: 0x000AA63E
		public int buttonIndex { get; set; }

		// Token: 0x06001D78 RID: 7544 RVA: 0x000AC248 File Offset: 0x000AA648
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Player Id</b>: " + this.playerId);
			stringBuilder.AppendLine("<b>Mouse Source</b>: " + this.mouseSource);
			stringBuilder.AppendLine("<b>Input Source Index</b>: " + this.inputSourceIndex);
			stringBuilder.AppendLine("<b>Touch Source/b>: " + this.touchSource);
			stringBuilder.AppendLine("<b>Source Type</b>: " + this.sourceType);
			stringBuilder.AppendLine("<b>Button Index</b>: " + this.buttonIndex);
			stringBuilder.Append(base.ToString());
			return stringBuilder.ToString();
		}
	}
}
