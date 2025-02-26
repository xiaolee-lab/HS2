using System;
using System.Linq;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Illusion.Component.UI
{
	// Token: 0x0200105A RID: 4186
	public class MouseButtonCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x06008CD3 RID: 36051 RVA: 0x003AE1C0 File Offset: 0x003AC5C0
		// (set) Token: 0x06008CD4 RID: 36052 RVA: 0x003AE1EC File Offset: 0x003AC5EC
		public bool isLeft
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.ButtonType>.Normalize(this.buttonType).HasFlag(MouseButtonCheck.ButtonType.Left);
			}
			set
			{
				this.SetButtonType(value, MouseButtonCheck.ButtonType.Left);
			}
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x06008CD5 RID: 36053 RVA: 0x003AE1F8 File Offset: 0x003AC5F8
		// (set) Token: 0x06008CD6 RID: 36054 RVA: 0x003AE224 File Offset: 0x003AC624
		public bool isRight
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.ButtonType>.Normalize(this.buttonType).HasFlag(MouseButtonCheck.ButtonType.Right);
			}
			set
			{
				this.SetButtonType(value, MouseButtonCheck.ButtonType.Right);
			}
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x06008CD7 RID: 36055 RVA: 0x003AE230 File Offset: 0x003AC630
		// (set) Token: 0x06008CD8 RID: 36056 RVA: 0x003AE25C File Offset: 0x003AC65C
		public bool isCenter
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.ButtonType>.Normalize(this.buttonType).HasFlag(MouseButtonCheck.ButtonType.Center);
			}
			set
			{
				this.SetButtonType(value, MouseButtonCheck.ButtonType.Center);
			}
		}

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x06008CD9 RID: 36057 RVA: 0x003AE268 File Offset: 0x003AC668
		// (set) Token: 0x06008CDA RID: 36058 RVA: 0x003AE294 File Offset: 0x003AC694
		public bool isOnPointerDown
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.EventType>.Normalize(this.eventType).HasFlag(MouseButtonCheck.EventType.PointerDown);
			}
			set
			{
				this.SetEventType(value, MouseButtonCheck.EventType.PointerDown);
			}
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x06008CDB RID: 36059 RVA: 0x003AE2A0 File Offset: 0x003AC6A0
		// (set) Token: 0x06008CDC RID: 36060 RVA: 0x003AE2CC File Offset: 0x003AC6CC
		public bool isOnPointerUp
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.EventType>.Normalize(this.eventType).HasFlag(MouseButtonCheck.EventType.PointerUp);
			}
			set
			{
				this.SetEventType(value, MouseButtonCheck.EventType.PointerUp);
			}
		}

		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x06008CDD RID: 36061 RVA: 0x003AE2D8 File Offset: 0x003AC6D8
		// (set) Token: 0x06008CDE RID: 36062 RVA: 0x003AE304 File Offset: 0x003AC704
		public bool isOnBeginDrag
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.EventType>.Normalize(this.eventType).HasFlag(MouseButtonCheck.EventType.BeginDrag);
			}
			set
			{
				this.SetEventType(value, MouseButtonCheck.EventType.BeginDrag);
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x06008CDF RID: 36063 RVA: 0x003AE310 File Offset: 0x003AC710
		// (set) Token: 0x06008CE0 RID: 36064 RVA: 0x003AE33C File Offset: 0x003AC73C
		public bool isOnDrag
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.EventType>.Normalize(this.eventType).HasFlag(MouseButtonCheck.EventType.Drag);
			}
			set
			{
				this.SetEventType(value, MouseButtonCheck.EventType.Drag);
			}
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x06008CE1 RID: 36065 RVA: 0x003AE348 File Offset: 0x003AC748
		// (set) Token: 0x06008CE2 RID: 36066 RVA: 0x003AE375 File Offset: 0x003AC775
		public bool isOnEndDrag
		{
			get
			{
				return Utils.Enum<MouseButtonCheck.EventType>.Normalize(this.eventType).HasFlag(MouseButtonCheck.EventType.EndDrag);
			}
			set
			{
				this.SetEventType(value, MouseButtonCheck.EventType.EndDrag);
			}
		}

		// Token: 0x06008CE3 RID: 36067 RVA: 0x003AE380 File Offset: 0x003AC780
		public virtual void OnPointerDown(PointerEventData data)
		{
			if (this.isOnPointerDown)
			{
				if (this.Indexeser.Any((int i) => MouseButtonCheck.Check(i)[0]))
				{
					this.onPointerDown.Invoke(data);
				}
			}
		}

		// Token: 0x06008CE4 RID: 36068 RVA: 0x003AE3D4 File Offset: 0x003AC7D4
		public virtual void OnPointerUp(PointerEventData data)
		{
			if (this.isOnPointerUp)
			{
				if (this.Indexeser.Any((int i) => MouseButtonCheck.Check(i)[2]))
				{
					this.onPointerUp.Invoke(data);
				}
			}
		}

		// Token: 0x06008CE5 RID: 36069 RVA: 0x003AE428 File Offset: 0x003AC828
		public virtual void OnBeginDrag(PointerEventData data)
		{
			if (this.isOnBeginDrag)
			{
				if (this.Indexeser.Any((int i) => MouseButtonCheck.Check(i)[1]))
				{
					this.onBeginDrag.Invoke(data);
				}
			}
		}

		// Token: 0x06008CE6 RID: 36070 RVA: 0x003AE47C File Offset: 0x003AC87C
		public virtual void OnDrag(PointerEventData data)
		{
			if (this.isOnDrag)
			{
				if (this.Indexeser.Any((int i) => MouseButtonCheck.Check(i)[1]))
				{
					this.onDrag.Invoke(data);
				}
			}
		}

		// Token: 0x06008CE7 RID: 36071 RVA: 0x003AE4D0 File Offset: 0x003AC8D0
		public virtual void OnEndDrag(PointerEventData data)
		{
			if (this.isOnEndDrag)
			{
				if (this.Indexeser.Any((int i) => MouseButtonCheck.Check(i)[2]))
				{
					this.onEndDrag.Invoke(data);
				}
			}
		}

		// Token: 0x06008CE8 RID: 36072 RVA: 0x003AE524 File Offset: 0x003AC924
		private void SetButtonType(bool isOn, MouseButtonCheck.ButtonType type)
		{
			this.buttonType = Utils.Enum<MouseButtonCheck.ButtonType>.Normalize(this.buttonType);
			this.buttonType = (MouseButtonCheck.ButtonType)((!isOn) ? this.buttonType.Sub(type) : this.buttonType.Add(type));
		}

		// Token: 0x06008CE9 RID: 36073 RVA: 0x003AE580 File Offset: 0x003AC980
		private void SetEventType(bool isOn, MouseButtonCheck.EventType type)
		{
			this.eventType = Utils.Enum<MouseButtonCheck.EventType>.Normalize(this.eventType);
			this.eventType = (MouseButtonCheck.EventType)((!isOn) ? this.eventType.Sub(type) : this.eventType.Add(type));
		}

		// Token: 0x06008CEA RID: 36074 RVA: 0x003AE5DC File Offset: 0x003AC9DC
		private static bool[] Check(int i)
		{
			return new bool[]
			{
				Input.GetMouseButtonDown(i),
				Input.GetMouseButton(i),
				Input.GetMouseButtonUp(i)
			};
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x06008CEB RID: 36075 RVA: 0x003AE600 File Offset: 0x003ACA00
		private int[] Indexeser
		{
			get
			{
				return (from i in new int[]
				{
					(!this.isLeft) ? -1 : 0,
					(!this.isRight) ? -1 : 1,
					(!this.isCenter) ? -1 : 2
				}
				where i != -1
				select i).ToArray<int>();
			}
		}

		// Token: 0x04007288 RID: 29320
		[EnumMask]
		public MouseButtonCheck.ButtonType buttonType = Utils.Enum<MouseButtonCheck.ButtonType>.Everything;

		// Token: 0x04007289 RID: 29321
		[EnumMask]
		public MouseButtonCheck.EventType eventType = Utils.Enum<MouseButtonCheck.EventType>.Everything;

		// Token: 0x0400728A RID: 29322
		public MouseButtonCheck.Callback onPointerDown = new MouseButtonCheck.Callback();

		// Token: 0x0400728B RID: 29323
		public MouseButtonCheck.Callback onPointerUp = new MouseButtonCheck.Callback();

		// Token: 0x0400728C RID: 29324
		public MouseButtonCheck.Callback onBeginDrag = new MouseButtonCheck.Callback();

		// Token: 0x0400728D RID: 29325
		public MouseButtonCheck.Callback onDrag = new MouseButtonCheck.Callback();

		// Token: 0x0400728E RID: 29326
		public MouseButtonCheck.Callback onEndDrag = new MouseButtonCheck.Callback();

		// Token: 0x0200105B RID: 4187
		[Flags]
		public enum ButtonType
		{
			// Token: 0x04007296 RID: 29334
			Left = 1,
			// Token: 0x04007297 RID: 29335
			Right = 2,
			// Token: 0x04007298 RID: 29336
			Center = 4
		}

		// Token: 0x0200105C RID: 4188
		[Flags]
		public enum EventType
		{
			// Token: 0x0400729A RID: 29338
			PointerDown = 1,
			// Token: 0x0400729B RID: 29339
			PointerUp = 2,
			// Token: 0x0400729C RID: 29340
			BeginDrag = 4,
			// Token: 0x0400729D RID: 29341
			Drag = 8,
			// Token: 0x0400729E RID: 29342
			EndDrag = 16
		}

		// Token: 0x0200105D RID: 4189
		[Serializable]
		public class Callback : UnityEvent<PointerEventData>
		{
		}

		// Token: 0x0200105E RID: 4190
		private enum Key
		{
			// Token: 0x040072A0 RID: 29344
			Down,
			// Token: 0x040072A1 RID: 29345
			Hold,
			// Token: 0x040072A2 RID: 29346
			Up
		}
	}
}
