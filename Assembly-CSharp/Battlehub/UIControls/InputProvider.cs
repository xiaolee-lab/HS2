using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x02000073 RID: 115
	public class InputProvider : MonoBehaviour
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00009551 File Offset: 0x00007951
		public virtual float HorizontalAxis
		{
			get
			{
				if (Input.GetKey(KeyCode.LeftArrow))
				{
					return -1f;
				}
				if (Input.GetKey(KeyCode.RightArrow))
				{
					return 1f;
				}
				return 0f;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00009582 File Offset: 0x00007982
		public virtual float VerticalAxis
		{
			get
			{
				if (Input.GetKey(KeyCode.UpArrow))
				{
					return 1f;
				}
				if (Input.GetKey(KeyCode.DownArrow))
				{
					return -1f;
				}
				return 0f;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000095B3 File Offset: 0x000079B3
		public virtual float HorizontalAxis2
		{
			get
			{
				if (Input.GetKey(KeyCode.Keypad4))
				{
					return -1f;
				}
				if (Input.GetKey(KeyCode.Keypad6))
				{
					return 1f;
				}
				return 0f;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000095E4 File Offset: 0x000079E4
		public virtual float VerticalAxis2
		{
			get
			{
				if (Input.GetKey(KeyCode.Keypad8))
				{
					return 1f;
				}
				if (Input.GetKey(KeyCode.Keypad2))
				{
					return -1f;
				}
				return 0f;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00009615 File Offset: 0x00007A15
		public virtual bool IsHorizontalButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00009633 File Offset: 0x00007A33
		public virtual bool IsVerticalButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00009651 File Offset: 0x00007A51
		public virtual bool IsHorizontal2ButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad6);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000966F File Offset: 0x00007A6F
		public virtual bool IsVertical2ButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Keypad2);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000968D File Offset: 0x00007A8D
		public virtual bool IsFunctionalButtonPressed
		{
			get
			{
				return Input.GetKey(KeyCode.LeftControl);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00009699 File Offset: 0x00007A99
		public virtual bool IsFunctional2ButtonPressed
		{
			get
			{
				return Input.GetKey(KeyCode.LeftShift);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000096A5 File Offset: 0x00007AA5
		public virtual bool IsSubmitButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.Return);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000096AE File Offset: 0x00007AAE
		public virtual bool IsSubmitButtonUp
		{
			get
			{
				return Input.GetKeyUp(KeyCode.Return);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000096B7 File Offset: 0x00007AB7
		public virtual bool IsCancelButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.Escape);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000096C0 File Offset: 0x00007AC0
		public virtual bool IsDeleteButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.Delete);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000096C9 File Offset: 0x00007AC9
		public virtual bool IsSelectAllButtonDown
		{
			get
			{
				return Input.GetKeyDown(KeyCode.A);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000096D2 File Offset: 0x00007AD2
		public virtual bool IsAnyKeyDown
		{
			get
			{
				return Input.anyKeyDown;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000096D9 File Offset: 0x00007AD9
		public virtual Vector3 MousePosition
		{
			get
			{
				return Input.mousePosition;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000096E0 File Offset: 0x00007AE0
		public virtual bool IsMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000096E8 File Offset: 0x00007AE8
		public virtual bool IsMousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000096EF File Offset: 0x00007AEF
		public virtual bool IsKeyboardPresent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000096F2 File Offset: 0x00007AF2
		public virtual int TouchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000096F9 File Offset: 0x00007AF9
		public virtual Touch GetTouch(int i)
		{
			return Input.GetTouch(i);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00009701 File Offset: 0x00007B01
		public virtual bool IsTouchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}
	}
}
