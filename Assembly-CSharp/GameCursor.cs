using System;
using System.Runtime.InteropServices;
using UniRx;
using UnityEngine;

// Token: 0x0200086F RID: 2159
public class GameCursor : Singleton<GameCursor>
{
	// Token: 0x170009CE RID: 2510
	// (get) Token: 0x06003706 RID: 14086 RVA: 0x001460A8 File Offset: 0x001444A8
	// (set) Token: 0x06003707 RID: 14087 RVA: 0x001460AF File Offset: 0x001444AF
	public static bool isDraw
	{
		get
		{
			return Cursor.visible;
		}
		set
		{
			Cursor.visible = value;
		}
	}

	// Token: 0x170009CF RID: 2511
	// (get) Token: 0x06003708 RID: 14088 RVA: 0x001460B7 File Offset: 0x001444B7
	// (set) Token: 0x06003709 RID: 14089 RVA: 0x001460BE File Offset: 0x001444BE
	public static bool MouseLocked
	{
		get
		{
			return GameCursor.mouseLocked;
		}
		set
		{
			GameCursor.mouseLocked = value;
			Cursor.visible = !value;
			Cursor.lockState = ((!Cursor.visible) ? CursorLockMode.Locked : CursorLockMode.None);
		}
	}

	// Token: 0x170009D0 RID: 2512
	// (get) Token: 0x0600370A RID: 14090 RVA: 0x001460E5 File Offset: 0x001444E5
	// (set) Token: 0x0600370B RID: 14091 RVA: 0x001460ED File Offset: 0x001444ED
	public GameCursor.CursorKind kindCursor { get; private set; }

	// Token: 0x0600370C RID: 14092 RVA: 0x001460F8 File Offset: 0x001444F8
	private void Start()
	{
		GameCursor.pos = Input.mousePosition;
		GameCursor.GetCursorPos(out this.lockPos);
		this.windowPtr = GameCursor.GetForegroundWindow();
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			if (GameCursor.isLock)
			{
				GameCursor.SetCursorPos(this.lockPos.X, this.lockPos.Y);
				return;
			}
			GameCursor.pos = Input.mousePosition;
			GameCursor.GetCursorPos(out this.lockPos);
		});
	}

	// Token: 0x0600370D RID: 14093 RVA: 0x0014615C File Offset: 0x0014455C
	public bool setCursor(GameCursor.CursorKind _eKind, Vector2 _vHotSpot)
	{
		Texture2D texture = null;
		if (_eKind == GameCursor.CursorKind.Spanking)
		{
			texture = this.atexChange[(int)_eKind];
		}
		Cursor.SetCursor(texture, _vHotSpot, CursorMode.ForceSoftware);
		this.kindCursor = _eKind;
		return true;
	}

	// Token: 0x0600370E RID: 14094
	[DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void SetCursorPos(int X, int Y);

	// Token: 0x0600370F RID: 14095
	[DllImport("USER32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out GameCursor.POINT lpPoint);

	// Token: 0x06003710 RID: 14096
	[DllImport("USER32.dll")]
	private static extern bool ScreenToClient(IntPtr hWnd, ref GameCursor.POINT lpPoint);

	// Token: 0x06003711 RID: 14097
	[DllImport("USER32.dll")]
	private static extern bool ClientToScreen(IntPtr hWnd, ref GameCursor.POINT lpPoint);

	// Token: 0x06003712 RID: 14098
	[DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

	// Token: 0x06003713 RID: 14099
	[DllImport("user32.dll")]
	private static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

	// Token: 0x06003714 RID: 14100
	[DllImport("user32.dll")]
	private static extern int GetWindowRect(IntPtr hwnd, ref GameCursor.RECT lpRect);

	// Token: 0x06003715 RID: 14101
	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	// Token: 0x06003716 RID: 14102
	[DllImport("user32.dll")]
	private static extern IntPtr FindWindow(string className, string windowName);

	// Token: 0x06003717 RID: 14103
	[DllImport("user32.dll")]
	public static extern bool SetWindowText(IntPtr hwnd, string lpString);

	// Token: 0x06003718 RID: 14104 RVA: 0x0014619C File Offset: 0x0014459C
	public void SetCoursorPosition(Vector3 mousePos)
	{
		GameCursor.POINT point = new GameCursor.POINT(0, 0);
		GameCursor.ClientToScreen(this.windowPtr, ref point);
		GameCursor.GetWindowRect(this.windowPtr, ref this.winRect);
		GameCursor.POINT point2 = new GameCursor.POINT(point.X - this.winRect.left, point.Y - this.winRect.top);
		point.X = (int)mousePos.x;
		point.Y = Screen.height - (int)mousePos.y;
		GameCursor.ClientToScreen(this.windowPtr, ref point);
		GameCursor.SetCursorPos(point.X + point2.X, point.Y + point2.Y);
	}

	// Token: 0x06003719 RID: 14105 RVA: 0x00146254 File Offset: 0x00144654
	public void SetCursorLock(bool setLockFlag)
	{
		if (setLockFlag)
		{
			if (!GameCursor.isLock)
			{
				GameCursor.GetCursorPos(out this.lockPos);
				GameCursor.isLock = true;
				Cursor.visible = false;
			}
		}
		else if (GameCursor.isLock)
		{
			GameCursor.SetCursorPos(this.lockPos.X, this.lockPos.Y);
			GameCursor.isLock = false;
			Cursor.visible = true;
		}
	}

	// Token: 0x0600371A RID: 14106 RVA: 0x001462BF File Offset: 0x001446BF
	public void UnLockCursor()
	{
		if (GameCursor.isLock)
		{
			GameCursor.isLock = false;
			Cursor.visible = true;
		}
	}

	// Token: 0x0600371B RID: 14107 RVA: 0x001462D7 File Offset: 0x001446D7
	public void UpdateCursorLock()
	{
		if (GameCursor.isLock)
		{
			GameCursor.SetCursorPos(this.lockPos.X, this.lockPos.Y);
		}
	}

	// Token: 0x0400378C RID: 14220
	public static Vector3 pos = Vector3.zero;

	// Token: 0x0400378D RID: 14221
	public static float speed = 2000f;

	// Token: 0x0400378E RID: 14222
	public static bool isLock = false;

	// Token: 0x0400378F RID: 14223
	private GameCursor.POINT lockPos = default(GameCursor.POINT);

	// Token: 0x04003790 RID: 14224
	private bool GUICheckFlag;

	// Token: 0x04003791 RID: 14225
	private static bool mouseLocked = false;

	// Token: 0x04003792 RID: 14226
	private const int numTex = 1;

	// Token: 0x04003793 RID: 14227
	private Texture2D[] atexChange = new Texture2D[1];

	// Token: 0x04003795 RID: 14229
	private readonly string[] anameTex = new string[]
	{
		"spanking"
	};

	// Token: 0x04003796 RID: 14230
	private const int MOUSEEVENTF_LEFTDOWN = 2;

	// Token: 0x04003797 RID: 14231
	private const int MOUSEEVENTF_LEFTUP = 4;

	// Token: 0x04003798 RID: 14232
	private IntPtr windowPtr = GameCursor.GetForegroundWindow();

	// Token: 0x04003799 RID: 14233
	private GameCursor.RECT winRect = default(GameCursor.RECT);

	// Token: 0x02000870 RID: 2160
	public enum CursorKind
	{
		// Token: 0x0400379B RID: 14235
		None = -1,
		// Token: 0x0400379C RID: 14236
		Spanking
	}

	// Token: 0x02000871 RID: 2161
	private struct RECT
	{
		// Token: 0x0400379D RID: 14237
		public int left;

		// Token: 0x0400379E RID: 14238
		public int top;

		// Token: 0x0400379F RID: 14239
		public int right;

		// Token: 0x040037A0 RID: 14240
		public int bottom;
	}

	// Token: 0x02000872 RID: 2162
	public struct POINT
	{
		// Token: 0x0600371F RID: 14111 RVA: 0x00146366 File Offset: 0x00144766
		public POINT(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x040037A1 RID: 14241
		public int X;

		// Token: 0x040037A2 RID: 14242
		public int Y;
	}
}
