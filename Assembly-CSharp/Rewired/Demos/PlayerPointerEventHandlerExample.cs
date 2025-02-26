using System;
using System.Collections.Generic;
using System.Text;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x0200051C RID: 1308
	[AddComponentMenu("")]
	public sealed class PlayerPointerEventHandlerExample : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IScrollHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x06001918 RID: 6424 RVA: 0x0009B2E9 File Offset: 0x000996E9
		private void Log(string o)
		{
			this.log.Add(o);
			if (this.log.Count > 10)
			{
				this.log.RemoveAt(0);
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0009B318 File Offset: 0x00099718
		private void Update()
		{
			if (this.text != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.log)
				{
					stringBuilder.AppendLine(value);
				}
				this.text.text = stringBuilder.ToString();
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0009B3A0 File Offset: 0x000997A0
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerEnter:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0009B410 File Offset: 0x00099810
		public void OnPointerExit(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerExit:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0009B480 File Offset: 0x00099880
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerUp:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0009B504 File Offset: 0x00099904
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerDown:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0009B588 File Offset: 0x00099988
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerClick:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0009B60C File Offset: 0x00099A0C
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnScroll:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0009B67C File Offset: 0x00099A7C
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnBeginDrag:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0009B700 File Offset: 0x00099B00
		public void OnDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnDrag:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0009B784 File Offset: 0x00099B84
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnEndDrag:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0009B808 File Offset: 0x00099C08
		private static string GetSourceName(PlayerPointerEventData playerEventData)
		{
			if (playerEventData.sourceType == PointerEventType.Mouse)
			{
				if (playerEventData.mouseSource is Behaviour)
				{
					return (playerEventData.mouseSource as Behaviour).name;
				}
			}
			else if (playerEventData.sourceType == PointerEventType.Touch && playerEventData.touchSource is Behaviour)
			{
				return (playerEventData.touchSource as Behaviour).name;
			}
			return null;
		}

		// Token: 0x04001C18 RID: 7192
		public Text text;

		// Token: 0x04001C19 RID: 7193
		private const int logLength = 10;

		// Token: 0x04001C1A RID: 7194
		private List<string> log = new List<string>();
	}
}
