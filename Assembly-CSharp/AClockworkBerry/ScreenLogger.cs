using System;
using System.Collections.Generic;
using UnityEngine;

namespace AClockworkBerry
{
	// Token: 0x020004BC RID: 1212
	public class ScreenLogger : MonoBehaviour
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x000899CB File Offset: 0x00087DCB
		public static bool Instantiated
		{
			get
			{
				return ScreenLogger.instantiated;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x000899D4 File Offset: 0x00087DD4
		public static ScreenLogger Instance
		{
			get
			{
				if (ScreenLogger.instantiated)
				{
					return ScreenLogger.instance;
				}
				ScreenLogger.instance = (UnityEngine.Object.FindObjectOfType(typeof(ScreenLogger)) as ScreenLogger);
				if (ScreenLogger.instance == null)
				{
					ScreenLogger.instance = new GameObject("ScreenLogger", new Type[]
					{
						typeof(ScreenLogger)
					}).GetComponent<ScreenLogger>();
					if (ScreenLogger.instance == null)
					{
						UnityEngine.Debug.LogError("Problem during the creation of ScreenLogger");
					}
					else
					{
						ScreenLogger.instantiated = true;
					}
				}
				else
				{
					ScreenLogger.instantiated = true;
				}
				return ScreenLogger.instance;
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00089A78 File Offset: 0x00087E78
		public void Awake()
		{
			ScreenLogger[] array = UnityEngine.Object.FindObjectsOfType<ScreenLogger>();
			if (array.Length > 1)
			{
				UnityEngine.Debug.Log("Destroying ScreenLogger, already exists...");
				this.destroying = true;
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			this.InitStyles();
			if (ScreenLogger.IsPersistent)
			{
				UnityEngine.Object.DontDestroyOnLoad(this);
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00089AC8 File Offset: 0x00087EC8
		private void InitStyles()
		{
			Texture2D texture2D = new Texture2D(1, 1);
			this.BackgroundColor.a = this.BackgroundOpacity;
			texture2D.SetPixel(0, 0, this.BackgroundColor);
			texture2D.Apply();
			this.styleContainer = new GUIStyle();
			this.styleContainer.normal.background = texture2D;
			this.styleContainer.wordWrap = true;
			this.styleContainer.padding = new RectOffset(this.padding, this.padding, this.padding, this.padding);
			this.styleText = new GUIStyle();
			this.styleText.fontSize = this.FontSize;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00089B6E File Offset: 0x00087F6E
		private void OnEnable()
		{
			if (!this.ShowInEditor && Application.isEditor)
			{
				return;
			}
			ScreenLogger.queue = new Queue<ScreenLogger.LogMessage>();
			Application.logMessageReceived += this.HandleLog;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00089BA1 File Offset: 0x00087FA1
		private void OnDisable()
		{
			if (this.destroying)
			{
				return;
			}
			Application.logMessageReceived -= this.HandleLog;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00089BC0 File Offset: 0x00087FC0
		private void Update()
		{
			if (!this.ShowInEditor && Application.isEditor)
			{
				return;
			}
			float num = (float)(Screen.height - 2 * this.Margin) * this.Height - (float)(2 * this.padding);
			int num2 = (int)(num / this.styleText.lineHeight);
			while (ScreenLogger.queue.Count > num2)
			{
				ScreenLogger.queue.Dequeue();
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00089C34 File Offset: 0x00088034
		private void OnGUI()
		{
			if (!this.ShowLog)
			{
				return;
			}
			if (!this.ShowInEditor && Application.isEditor)
			{
				return;
			}
			float width = (float)(Screen.width - 2 * this.Margin) * this.Width;
			float height = (float)(Screen.height - 2 * this.Margin) * this.Height;
			float x = 1f;
			float y = 1f;
			switch (this.AnchorPosition)
			{
			case ScreenLogger.LogAnchor.TopLeft:
				x = (float)this.Margin;
				y = (float)this.Margin;
				break;
			case ScreenLogger.LogAnchor.TopRight:
				x = (float)this.Margin + (float)(Screen.width - 2 * this.Margin) * (1f - this.Width);
				y = (float)this.Margin;
				break;
			case ScreenLogger.LogAnchor.BottomLeft:
				x = (float)this.Margin;
				y = (float)this.Margin + (float)(Screen.height - 2 * this.Margin) * (1f - this.Height);
				break;
			case ScreenLogger.LogAnchor.BottomRight:
				x = (float)this.Margin + (float)(Screen.width - 2 * this.Margin) * (1f - this.Width);
				y = (float)this.Margin + (float)(Screen.height - 2 * this.Margin) * (1f - this.Height);
				break;
			}
			GUILayout.BeginArea(new Rect(x, y, width, height), this.styleContainer);
			foreach (ScreenLogger.LogMessage logMessage in ScreenLogger.queue)
			{
				switch (logMessage.Type)
				{
				case LogType.Error:
				case LogType.Assert:
				case LogType.Exception:
					this.styleText.normal.textColor = this.ErrorColor;
					break;
				case LogType.Warning:
					this.styleText.normal.textColor = this.WarningColor;
					break;
				case LogType.Log:
					this.styleText.normal.textColor = this.MessageColor;
					break;
				default:
					this.styleText.normal.textColor = this.MessageColor;
					break;
				}
				if (logMessage.Message.Contains("#"))
				{
					foreach (KeyValuePair<string, Color> keyValuePair in this.TagColors)
					{
						if (logMessage.Message.Contains(keyValuePair.Key))
						{
							this.styleText.normal.textColor = keyValuePair.Value;
							break;
						}
					}
				}
				GUILayout.Label(logMessage.Message, this.styleText, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
			float num = 550f;
			float height2 = (float)(Screen.height - 2 * this.Margin - this.Margin) * (1f - this.Height);
			this.styleText.normal.textColor = Color.white;
			GUILayout.BeginArea(new Rect((float)this.Margin, (float)this.Margin, num, height2), this.styleContainer);
			GUILayout.Label("Sync Top N", this.styleText, Array.Empty<GUILayoutOption>());
			for (int i = 0; i < this.SyncTopN.TopN.Count; i++)
			{
				GUILayout.Label(this.SyncTopN.ItemToString(i), this.styleText, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect((float)(this.Margin * 2) + num, (float)this.Margin, num, height2), this.styleContainer);
			GUILayout.Label("Async Top N", this.styleText, Array.Empty<GUILayoutOption>());
			for (int j = 0; j < this.AsyncTopN.TopN.Count; j++)
			{
				GUILayout.Label(this.AsyncTopN.ItemToString(j), this.styleText, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0008A084 File Offset: 0x00088484
		public void Clear()
		{
			this.SyncTopN.Clear();
			this.AsyncTopN.Clear();
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0008A09C File Offset: 0x0008849C
		private void HandleLog(string message, string stackTrace, LogType type)
		{
			if (type == LogType.Assert && !this.LogErrors)
			{
				return;
			}
			if (type == LogType.Error && !this.LogErrors)
			{
				return;
			}
			if (type == LogType.Exception && !this.LogErrors)
			{
				return;
			}
			if (type == LogType.Log && !this.LogMessages)
			{
				return;
			}
			if (type == LogType.Warning && !this.LogWarnings)
			{
				return;
			}
			string[] array = message.Split(new char[]
			{
				'\n'
			});
			foreach (string msg in array)
			{
				ScreenLogger.queue.Enqueue(new ScreenLogger.LogMessage(msg, type));
			}
			if (type == LogType.Assert && !this.StackTraceErrors)
			{
				return;
			}
			if (type == LogType.Error && !this.StackTraceErrors)
			{
				return;
			}
			if (type == LogType.Exception && !this.StackTraceErrors)
			{
				return;
			}
			if (type == LogType.Log && !this.StackTraceMessages)
			{
				return;
			}
			if (type == LogType.Warning && !this.StackTraceWarnings)
			{
				return;
			}
			string[] array3 = stackTrace.Split(new char[]
			{
				'\n'
			});
			foreach (string text in array3)
			{
				if (text.Length != 0)
				{
					ScreenLogger.queue.Enqueue(new ScreenLogger.LogMessage("  " + text, type));
				}
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0008A201 File Offset: 0x00088601
		public void EnqueueDirectly(string message, LogType type)
		{
			ScreenLogger.queue.Enqueue(new ScreenLogger.LogMessage(message, type));
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0008A214 File Offset: 0x00088614
		public void InspectorGUIUpdated()
		{
			this.InitStyles();
		}

		// Token: 0x0400191A RID: 6426
		public static bool IsPersistent = true;

		// Token: 0x0400191B RID: 6427
		private static ScreenLogger instance;

		// Token: 0x0400191C RID: 6428
		private static bool instantiated = false;

		// Token: 0x0400191D RID: 6429
		public bool ShowLog = true;

		// Token: 0x0400191E RID: 6430
		public bool ShowInEditor = true;

		// Token: 0x0400191F RID: 6431
		[Tooltip("Height of the log area as a percentage of the screen height")]
		[Range(0.3f, 1f)]
		public float Height = 0.7f;

		// Token: 0x04001920 RID: 6432
		[Tooltip("Width of the log area as a percentage of the screen width")]
		[Range(0.3f, 1f)]
		public float Width = 0.7f;

		// Token: 0x04001921 RID: 6433
		public int Margin = 20;

		// Token: 0x04001922 RID: 6434
		public ScreenLogger.LogAnchor AnchorPosition = ScreenLogger.LogAnchor.BottomLeft;

		// Token: 0x04001923 RID: 6435
		public int FontSize = 16;

		// Token: 0x04001924 RID: 6436
		[Range(0f, 1f)]
		public float BackgroundOpacity = 0.5f;

		// Token: 0x04001925 RID: 6437
		public Color BackgroundColor = Color.black;

		// Token: 0x04001926 RID: 6438
		public bool LogMessages = true;

		// Token: 0x04001927 RID: 6439
		public bool LogWarnings = true;

		// Token: 0x04001928 RID: 6440
		public bool LogErrors = true;

		// Token: 0x04001929 RID: 6441
		public Color MessageColor = Color.white;

		// Token: 0x0400192A RID: 6442
		public Color WarningColor = Color.yellow;

		// Token: 0x0400192B RID: 6443
		public Color ErrorColor = new Color(1f, 0.5f, 0.5f);

		// Token: 0x0400192C RID: 6444
		public Dictionary<string, Color> TagColors = new Dictionary<string, Color>
		{
			{
				"#LuaCache",
				Color.green
			},
			{
				"#LuaIO",
				Color.red
			}
		};

		// Token: 0x0400192D RID: 6445
		public bool StackTraceMessages;

		// Token: 0x0400192E RID: 6446
		public bool StackTraceWarnings;

		// Token: 0x0400192F RID: 6447
		public bool StackTraceErrors = true;

		// Token: 0x04001930 RID: 6448
		private static Queue<ScreenLogger.LogMessage> queue = new Queue<ScreenLogger.LogMessage>();

		// Token: 0x04001931 RID: 6449
		private GUIStyle styleContainer;

		// Token: 0x04001932 RID: 6450
		private GUIStyle styleText;

		// Token: 0x04001933 RID: 6451
		private int padding = 20;

		// Token: 0x04001934 RID: 6452
		private bool destroying;

		// Token: 0x04001935 RID: 6453
		public TopNContainer SyncTopN = new TopNContainer();

		// Token: 0x04001936 RID: 6454
		public TopNContainer AsyncTopN = new TopNContainer();

		// Token: 0x020004BD RID: 1213
		private class LogMessage
		{
			// Token: 0x06001676 RID: 5750 RVA: 0x0008A234 File Offset: 0x00088634
			public LogMessage(string msg, LogType type)
			{
				this.Message = msg;
				this.Type = type;
			}

			// Token: 0x04001937 RID: 6455
			public string Message;

			// Token: 0x04001938 RID: 6456
			public LogType Type;
		}

		// Token: 0x020004BE RID: 1214
		public enum LogAnchor
		{
			// Token: 0x0400193A RID: 6458
			TopLeft,
			// Token: 0x0400193B RID: 6459
			TopRight,
			// Token: 0x0400193C RID: 6460
			BottomLeft,
			// Token: 0x0400193D RID: 6461
			BottomRight
		}
	}
}
