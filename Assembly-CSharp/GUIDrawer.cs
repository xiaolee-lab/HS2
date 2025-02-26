using System;
using System.Linq;
using UnityEngine;

// Token: 0x02001189 RID: 4489
public static class GUIDrawer
{
	// Token: 0x060093FF RID: 37887 RVA: 0x003D2A05 File Offset: 0x003D0E05
	public static void Draw(float x, float y, string str, float baseW = 7f, float baseH = 15f, bool isLeftUp = true)
	{
		GUIDrawer.Draw(ref x, ref y, str, baseW, baseH, true);
	}

	// Token: 0x06009400 RID: 37888 RVA: 0x003D2A15 File Offset: 0x003D0E15
	public static void Draw(ref float x, float y, string str, float baseW = 7f, float baseH = 15f, bool isLeftUp = true)
	{
		GUIDrawer.Draw(ref x, ref y, str, baseW, baseH, true);
	}

	// Token: 0x06009401 RID: 37889 RVA: 0x003D2A24 File Offset: 0x003D0E24
	public static void Draw(float x, ref float y, string str, float baseW = 7f, float baseH = 15f, bool isLeftUp = true)
	{
		GUIDrawer.Draw(ref x, ref y, str, baseW, baseH, true);
	}

	// Token: 0x06009402 RID: 37890 RVA: 0x003D2A34 File Offset: 0x003D0E34
	public static void Draw(ref float x, ref float y, string str, float baseW = 7f, float baseH = 15f, bool isLeftUp = true)
	{
		if (str == string.Empty)
		{
			return;
		}
		string[] array = str.Replace(Environment.NewLine, "\n").Split(new char[]
		{
			'\n'
		});
		float num = (float)array.Max((string max) => max.Length) * baseW;
		float num2 = (float)array.Length * baseH;
		GUI.Box(new Rect(x, y, num + 10f, num2 + 5f), string.Empty);
		GUI.Label(new Rect(x + 5f, y, (float)Screen.width, (float)Screen.height), str);
		x += num;
		if (isLeftUp)
		{
			y += num2 + baseH;
		}
		else
		{
			y -= num2 + baseH;
		}
	}

	// Token: 0x06009403 RID: 37891 RVA: 0x003D2B08 File Offset: 0x003D0F08
	public static float GetWidth(string str, float baseW = 7f)
	{
		float result;
		if (str == string.Empty)
		{
			result = 0f;
		}
		else
		{
			result = (float)str.Replace(Environment.NewLine, "\n").Split(new char[]
			{
				'\n'
			}).Max((string max) => max.Length) * baseW;
		}
		return result;
	}

	// Token: 0x06009404 RID: 37892 RVA: 0x003D2B74 File Offset: 0x003D0F74
	public static float GetHeight(string str, float baseH = 15f)
	{
		return (!(str == string.Empty)) ? ((float)str.Replace(Environment.NewLine, "\n").Split(new char[]
		{
			'\n'
		}).Length * baseH + baseH) : 0f;
	}

	// Token: 0x0200118A RID: 4490
	public class Window
	{
		// Token: 0x06009407 RID: 37895 RVA: 0x003D2BD4 File Offset: 0x003D0FD4
		public Window()
		{
			this.type = GUIDrawer.Window.Type.None;
			this.defaultTitle = (this.title = string.Empty);
		}

		// Token: 0x06009408 RID: 37896 RVA: 0x003D2C0C File Offset: 0x003D100C
		public Window(string title)
		{
			this.title = (title ?? string.Empty);
			this.defaultTitle = this.title;
			this.rect = new Rect(0f, 0f, 300f, 0f);
		}

		// Token: 0x06009409 RID: 37897 RVA: 0x003D2C64 File Offset: 0x003D1064
		public Window(string title, Rect rect)
		{
			this.title = (title ?? string.Empty);
			this.defaultTitle = this.title;
			this.rect = rect;
		}

		// Token: 0x0600940A RID: 37898 RVA: 0x003D2C99 File Offset: 0x003D1099
		public void Open()
		{
			this.isClose = false;
		}

		// Token: 0x0600940B RID: 37899 RVA: 0x003D2CA2 File Offset: 0x003D10A2
		public void Close()
		{
			this.isClose = true;
		}

		// Token: 0x0600940C RID: 37900 RVA: 0x003D2CAB File Offset: 0x003D10AB
		public void Hide()
		{
			this.isHide = true;
		}

		// Token: 0x0600940D RID: 37901 RVA: 0x003D2CB4 File Offset: 0x003D10B4
		public void View()
		{
			this.isHide = false;
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x0600940E RID: 37902 RVA: 0x003D2CBD File Offset: 0x003D10BD
		// (set) Token: 0x0600940F RID: 37903 RVA: 0x003D2CC5 File Offset: 0x003D10C5
		public bool isHide
		{
			get
			{
				return this._isHide;
			}
			set
			{
				this._isHide = value;
				if (this._isHide)
				{
					this.HideEvent.Call();
				}
			}
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06009410 RID: 37904 RVA: 0x003D2CE4 File Offset: 0x003D10E4
		// (set) Token: 0x06009411 RID: 37905 RVA: 0x003D2CEC File Offset: 0x003D10EC
		public bool isClose
		{
			get
			{
				return this._isClose;
			}
			set
			{
				this._isClose = value;
				if (this._isClose)
				{
					this.CloseEvent.Call();
					this.title = this.defaultTitle;
				}
			}
		}

		// Token: 0x06009412 RID: 37906 RVA: 0x003D2D18 File Offset: 0x003D1118
		public void Draw(int windowID)
		{
			if (this.backupRect == null)
			{
				this.backupRect = new Rect?(this.rect);
			}
			if (this.isClose)
			{
				return;
			}
			this.CloseButton();
			GUIDrawer.Window.Type type = this.type;
			switch (type)
			{
			case GUIDrawer.Window.Type.None:
				this.DoWindow(-1);
				break;
			case GUIDrawer.Window.Type.Normal:
			case GUIDrawer.Window.Type.Layout:
			{
				Action<int> doWindow = delegate(int unusedWindowID)
				{
					using (new GUILayout.VerticalScope(Array.Empty<GUILayoutOption>()))
					{
						if (this.isHide)
						{
							GUILayout.Label(string.Empty, new GUILayoutOption[]
							{
								GUILayout.Height(this.rect.height)
							});
						}
						this.DoWindow(unusedWindowID);
					}
					GUI.DragWindow();
				};
				if (this.type == GUIDrawer.Window.Type.Normal || this.isHide)
				{
					if (this.isHide)
					{
						this.rect.height = 50f;
					}
					this.rect = GUI.Window(windowID, this.rect, delegate(int unusedWindowID)
					{
						doWindow(unusedWindowID);
					}, this.title);
				}
				else
				{
					this.rect = GUILayout.Window(windowID, this.rect, delegate(int unusedWindowID)
					{
						doWindow(unusedWindowID);
					}, this.title, Array.Empty<GUILayoutOption>());
				}
				this.HideButton();
				break;
			}
			case GUIDrawer.Window.Type.Custom:
			{
				Rect screenRect = this.rect;
				using (new GUILayout.AreaScope(screenRect))
				{
					GUIContent content = new GUIContent(this.title);
					using (new GUILayout.VerticalScope("box", Array.Empty<GUILayoutOption>()))
					{
						if (GUILayout.RepeatButton(content, new GUILayoutOption[]
						{
							GUILayout.ExpandWidth(true),
							GUILayout.ExpandHeight(false)
						}))
						{
							float y = new GUIStyle().CalcSize(content).y;
							this.rect.x = Input.mousePosition.x - screenRect.width * 0.5f;
							this.rect.y = (float)Screen.height - Input.mousePosition.y - y * 0.5f;
						}
						this.DoWindow(-1);
					}
				}
				break;
			}
			}
		}

		// Token: 0x06009413 RID: 37907 RVA: 0x003D2F48 File Offset: 0x003D1348
		private void HideButton()
		{
			Rect position = this.rect;
			float x = GUIDrawer.Window.buttonSize.x;
			float y = GUIDrawer.Window.buttonSize.y;
			position.x = position.x + position.width - x * 2f;
			position.y -= y;
			position.width = x;
			position.height = y;
			if (GUI.Button(position, (!this.isHide) ? "-" : "□"))
			{
				this.isHide = !this.isHide;
				if (!this.isHide)
				{
					this.rect.height = this.backupRect.Value.height;
				}
			}
		}

		// Token: 0x06009414 RID: 37908 RVA: 0x003D300C File Offset: 0x003D140C
		private void CloseButton()
		{
			Rect position = this.rect;
			float x = GUIDrawer.Window.buttonSize.x;
			float y = GUIDrawer.Window.buttonSize.y;
			position.x = position.x + position.width - x;
			position.y -= y;
			position.width = x;
			position.height = y;
			if (GUI.Button(position, "X"))
			{
				this.isClose = true;
			}
		}

		// Token: 0x04007750 RID: 30544
		public string title;

		// Token: 0x04007751 RID: 30545
		public Rect rect;

		// Token: 0x04007752 RID: 30546
		public Action<int> DoWindow;

		// Token: 0x04007753 RID: 30547
		public GUIDrawer.Window.Type type = GUIDrawer.Window.Type.Layout;

		// Token: 0x04007754 RID: 30548
		public Action HideEvent;

		// Token: 0x04007755 RID: 30549
		private bool _isHide;

		// Token: 0x04007756 RID: 30550
		public Action CloseEvent;

		// Token: 0x04007757 RID: 30551
		private bool _isClose;

		// Token: 0x04007758 RID: 30552
		private static Vector2 buttonSize = new Vector2(20f, 20f);

		// Token: 0x04007759 RID: 30553
		private string defaultTitle;

		// Token: 0x0400775A RID: 30554
		private Rect? backupRect;

		// Token: 0x0200118B RID: 4491
		public enum Type
		{
			// Token: 0x0400775C RID: 30556
			None,
			// Token: 0x0400775D RID: 30557
			Normal,
			// Token: 0x0400775E RID: 30558
			Layout,
			// Token: 0x0400775F RID: 30559
			Custom
		}
	}
}
