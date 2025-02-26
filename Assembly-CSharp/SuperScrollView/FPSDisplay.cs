using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005B2 RID: 1458
	public class FPSDisplay : MonoBehaviour
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x000BA15C File Offset: 0x000B855C
		private void Awake()
		{
			this.mStyle = new GUIStyle();
			this.mStyle.alignment = TextAnchor.UpperLeft;
			this.mStyle.normal.background = null;
			this.mStyle.fontSize = 25;
			this.mStyle.normal.textColor = new Color(0f, 1f, 0f, 1f);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000BA1C7 File Offset: 0x000B85C7
		private void Update()
		{
			this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000BA1E8 File Offset: 0x000B85E8
		private void OnGUI()
		{
			int width = Screen.width;
			int height = Screen.height;
			Rect position = new Rect(0f, 0f, (float)width, (float)(height * 2 / 100));
			float num = 1f / this.deltaTime;
			string text = string.Format("   {0:0.} FPS", num);
			GUI.Label(position, text, this.mStyle);
		}

		// Token: 0x04002151 RID: 8529
		private float deltaTime;

		// Token: 0x04002152 RID: 8530
		private GUIStyle mStyle;
	}
}
