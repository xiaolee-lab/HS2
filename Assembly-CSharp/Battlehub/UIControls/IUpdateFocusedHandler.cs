using System;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x02000072 RID: 114
	public interface IUpdateFocusedHandler : IEventSystemHandler
	{
		// Token: 0x060000B1 RID: 177
		void OnUpdateFocused(BaseEventData eventData);
	}
}
