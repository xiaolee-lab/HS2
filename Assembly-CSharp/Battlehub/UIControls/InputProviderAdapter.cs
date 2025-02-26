using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x02000074 RID: 116
	public class InputProviderAdapter : InputProvider
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00009710 File Offset: 0x00007B10
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00009718 File Offset: 0x00007B18
		public InputProvider InputProvider
		{
			get
			{
				return this.m_inputProvider;
			}
			set
			{
				this.m_inputProvider = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00009721 File Offset: 0x00007B21
		public override float HorizontalAxis
		{
			get
			{
				return this.m_inputProvider.HorizontalAxis;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000972E File Offset: 0x00007B2E
		public override float VerticalAxis
		{
			get
			{
				return this.m_inputProvider.VerticalAxis;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000973B File Offset: 0x00007B3B
		public override float HorizontalAxis2
		{
			get
			{
				return this.m_inputProvider.HorizontalAxis2;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00009748 File Offset: 0x00007B48
		public override float VerticalAxis2
		{
			get
			{
				return this.m_inputProvider.VerticalAxis2;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00009755 File Offset: 0x00007B55
		public override bool IsHorizontalButtonDown
		{
			get
			{
				return this.m_inputProvider.IsHorizontalButtonDown;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00009762 File Offset: 0x00007B62
		public override bool IsVerticalButtonDown
		{
			get
			{
				return this.m_inputProvider.IsVerticalButtonDown;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000976F File Offset: 0x00007B6F
		public override bool IsHorizontal2ButtonDown
		{
			get
			{
				return this.m_inputProvider.IsHorizontal2ButtonDown;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000977C File Offset: 0x00007B7C
		public override bool IsVertical2ButtonDown
		{
			get
			{
				return this.m_inputProvider.IsVertical2ButtonDown;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00009789 File Offset: 0x00007B89
		public override bool IsFunctionalButtonPressed
		{
			get
			{
				return this.m_inputProvider.IsFunctionalButtonPressed;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00009796 File Offset: 0x00007B96
		public override bool IsFunctional2ButtonPressed
		{
			get
			{
				return this.m_inputProvider.IsFunctional2ButtonPressed;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000097A3 File Offset: 0x00007BA3
		public override bool IsSubmitButtonDown
		{
			get
			{
				return this.m_inputProvider.IsSubmitButtonDown;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000097B0 File Offset: 0x00007BB0
		public override bool IsSubmitButtonUp
		{
			get
			{
				return this.m_inputProvider.IsSubmitButtonUp;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000097BD File Offset: 0x00007BBD
		public override bool IsCancelButtonDown
		{
			get
			{
				return this.m_inputProvider.IsCancelButtonDown;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000097CA File Offset: 0x00007BCA
		public override bool IsDeleteButtonDown
		{
			get
			{
				return this.m_inputProvider.IsDeleteButtonDown;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000097D7 File Offset: 0x00007BD7
		public override bool IsSelectAllButtonDown
		{
			get
			{
				return this.m_inputProvider.IsSelectAllButtonDown;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000097E4 File Offset: 0x00007BE4
		public override bool IsAnyKeyDown
		{
			get
			{
				return this.m_inputProvider.IsAnyKeyDown;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000097F1 File Offset: 0x00007BF1
		public override Vector3 MousePosition
		{
			get
			{
				return this.m_inputProvider.MousePosition;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000097FE File Offset: 0x00007BFE
		public override bool IsMouseButtonDown(int button)
		{
			return this.m_inputProvider.IsMouseButtonDown(button);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000980C File Offset: 0x00007C0C
		public override bool IsMousePresent
		{
			get
			{
				return this.m_inputProvider.IsMousePresent;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00009819 File Offset: 0x00007C19
		public override bool IsKeyboardPresent
		{
			get
			{
				return this.m_inputProvider.IsKeyboardPresent;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00009826 File Offset: 0x00007C26
		public override int TouchCount
		{
			get
			{
				return this.m_inputProvider.TouchCount;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00009833 File Offset: 0x00007C33
		public override Touch GetTouch(int i)
		{
			return this.m_inputProvider.GetTouch(i);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00009841 File Offset: 0x00007C41
		public override bool IsTouchSupported
		{
			get
			{
				return this.m_inputProvider.IsTouchSupported;
			}
		}

		// Token: 0x040001F1 RID: 497
		[SerializeField]
		private InputProvider m_inputProvider;
	}
}
