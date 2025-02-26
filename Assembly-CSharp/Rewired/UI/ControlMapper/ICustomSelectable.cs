using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000549 RID: 1353
	public interface ICustomSelectable : ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001C2E RID: 7214
		// (set) Token: 0x06001C2F RID: 7215
		Sprite disabledHighlightedSprite { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001C30 RID: 7216
		// (set) Token: 0x06001C31 RID: 7217
		Color disabledHighlightedColor { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001C32 RID: 7218
		// (set) Token: 0x06001C33 RID: 7219
		string disabledHighlightedTrigger { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001C34 RID: 7220
		// (set) Token: 0x06001C35 RID: 7221
		bool autoNavUp { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001C36 RID: 7222
		// (set) Token: 0x06001C37 RID: 7223
		bool autoNavDown { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001C38 RID: 7224
		// (set) Token: 0x06001C39 RID: 7225
		bool autoNavLeft { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001C3A RID: 7226
		// (set) Token: 0x06001C3B RID: 7227
		bool autoNavRight { get; set; }

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06001C3C RID: 7228
		// (remove) Token: 0x06001C3D RID: 7229
		event UnityAction CancelEvent;
	}
}
