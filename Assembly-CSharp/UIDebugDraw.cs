using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x020004C1 RID: 1217
public class UIDebugDraw : MonoBehaviour
{
	// Token: 0x06001679 RID: 5753 RVA: 0x0008A272 File Offset: 0x00088672
	public void StartStats()
	{
		if (base.enabled)
		{
			return;
		}
		base.enabled = true;
		this.m_widgetHighlightCount.Clear();
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x0008A294 File Offset: 0x00088694
	public void StopStats()
	{
		if (!base.enabled)
		{
			return;
		}
		base.enabled = false;
		List<KeyValuePair<string, int>> list = this.m_widgetHighlightCount.ToList<KeyValuePair<string, int>>();
		list.Sort((KeyValuePair<string, int> pair1, KeyValuePair<string, int> pair2) => pair2.Value.CompareTo(pair1.Value));
		List<string> list2 = new List<string>();
		foreach (KeyValuePair<string, int> keyValuePair in list)
		{
			list2.Add(string.Format("{0} {1}", keyValuePair.Key, keyValuePair.Value));
		}
		string path = Path.Combine(Application.persistentDataPath, string.Format("TestTools/ui_stats_{0}_{1}.log", SysUtil.FormatDateAsFileNameString(DateTime.Now), SysUtil.FormatTimeAsFileNameString(DateTime.Now)));
		File.WriteAllLines(path, list2.ToArray());
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x0008A388 File Offset: 0x00088788
	private void Start()
	{
		Texture2D texture2D = new Texture2D(1, 1);
		Color color = Color.magenta;
		color.a = 0.2f;
		texture2D.SetPixel(0, 0, color);
		texture2D.Apply();
		this.m_texUpdateGeometry = texture2D;
		texture2D = new Texture2D(1, 1);
		color = Color.green;
		color.a = 0.2f;
		texture2D.SetPixel(0, 0, color);
		texture2D.Apply();
		this.m_texUpdateAlpha = texture2D;
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x0008A3F5 File Offset: 0x000887F5
	private void Update()
	{
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x0008A3F8 File Offset: 0x000887F8
	private void OnGUI()
	{
		if (!base.enabled)
		{
			return;
		}
		Color color = GUI.color;
		Color color2 = GUI.color;
		for (int i = 0; i < this.m_highlightedWidgets.Count; i++)
		{
			color2.a = 1f - (Time.time - this.m_highlightedWidgets[i].timeChanged) * 2f;
			GUI.color = color2;
			Vector3[] screenPos = this.m_highlightedWidgets[i].screenPos;
			float num = screenPos[2].y - screenPos[0].y;
			Rect position = new Rect(screenPos[0].x, (float)Screen.height - screenPos[0].y - num, screenPos[2].x - screenPos[0].x, num);
			UIHighlightType type = this.m_highlightedWidgets[i].type;
			if (type != UIHighlightType.HType_UpdateGeometry)
			{
				if (type == UIHighlightType.HType_Invalidate)
				{
					GUI.DrawTexture(position, this.m_texUpdateAlpha, ScaleMode.StretchToFill);
				}
			}
			else
			{
				GUI.DrawTexture(position, this.m_texUpdateGeometry, ScaleMode.StretchToFill);
			}
			position.width = Math.Max(position.width, 200f);
			position.height = Math.Max(position.height, 50f);
			GUI.Label(position, this.m_highlightedWidgets[i].name);
		}
		GUI.color = color;
		this.m_highlightedWidgets.RemoveAll((UIHighlightWidget w) => Time.time - w.timeChanged > 0.5f);
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x0008A59E File Offset: 0x0008899E
	private void OnDrawGizmos()
	{
	}

	// Token: 0x04001945 RID: 6469
	public static UIDebugDraw Instance;

	// Token: 0x04001946 RID: 6470
	private Texture m_texUpdateGeometry;

	// Token: 0x04001947 RID: 6471
	private Texture m_texUpdateAlpha;

	// Token: 0x04001948 RID: 6472
	private List<UIHighlightWidget> m_highlightedWidgets = new List<UIHighlightWidget>();

	// Token: 0x04001949 RID: 6473
	private Dictionary<string, int> m_widgetHighlightCount = new Dictionary<string, int>(100);
}
