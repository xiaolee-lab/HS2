using System;
using System.Runtime.CompilerServices;
using Battlehub.UIControls;
using UnityEngine;

namespace Housing.List
{
	// Token: 0x020008A9 RID: 2217
	public class ListInputProvider : InputProvider
	{
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x00152FB4 File Offset: 0x001513B4
		public override float HorizontalAxis
		{
			get
			{
				if (!this.canAxis)
				{
					return 0f;
				}
				return (float)((!Input.GetKey(KeyCode.LeftArrow)) ? ((!Input.GetKey(KeyCode.RightArrow)) ? 0 : 1) : -1);
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x00152FF3 File Offset: 0x001513F3
		public override float VerticalAxis
		{
			get
			{
				if (!this.canAxis)
				{
					return 0f;
				}
				return (float)((!Input.GetKey(KeyCode.UpArrow)) ? ((!Input.GetKey(KeyCode.DownArrow)) ? 0 : -1) : 1);
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x00153032 File Offset: 0x00151432
		public override float HorizontalAxis2
		{
			get
			{
				if (!this.canAxis)
				{
					return 0f;
				}
				return (float)((!Input.GetKey(KeyCode.Keypad4)) ? ((!Input.GetKey(KeyCode.Keypad6)) ? 0 : 1) : -1);
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x00153071 File Offset: 0x00151471
		public override float VerticalAxis2
		{
			get
			{
				if (!this.canAxis)
				{
					return 0f;
				}
				return (float)((!Input.GetKey(KeyCode.Keypad8)) ? ((!Input.GetKey(KeyCode.Keypad2)) ? 0 : -1) : 1);
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x001530B0 File Offset: 0x001514B0
		public override bool IsFunctionalButtonPressed
		{
			[CompilerGenerated]
			get
			{
				return Input.GetKey(KeyCode.LeftControl) | Input.GetKey(KeyCode.RightControl);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x001530C7 File Offset: 0x001514C7
		public override bool IsFunctional2ButtonPressed
		{
			[CompilerGenerated]
			get
			{
				return Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift);
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x001530DE File Offset: 0x001514DE
		public override bool IsDeleteButtonDown
		{
			[CompilerGenerated]
			get
			{
				return Input.GetKeyDown(KeyCode.Delete);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x001530E7 File Offset: 0x001514E7
		public override bool IsSelectAllButtonDown
		{
			[CompilerGenerated]
			get
			{
				return Input.GetKeyDown(KeyCode.A);
			}
		}

		// Token: 0x0400393B RID: 14651
		[SerializeField]
		[Tooltip("キーボード操作で対象の切り替えが出来るか")]
		private bool canAxis = true;
	}
}
