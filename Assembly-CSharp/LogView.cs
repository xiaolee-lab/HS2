using System;
using System.Collections.Generic;
using System.Text;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200100E RID: 4110
public class LogView : MonoBehaviour
{
	// Token: 0x17001E2B RID: 7723
	// (get) Token: 0x06008A23 RID: 35363 RVA: 0x003A209C File Offset: 0x003A049C
	public bool IsActive
	{
		get
		{
			return this.processing.update;
		}
	}

	// Token: 0x06008A24 RID: 35364 RVA: 0x003A20AC File Offset: 0x003A04AC
	private void Start()
	{
		if (this.btnClose)
		{
			this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.SetActiveCanvas(false);
				if (this.onClose != null)
				{
					this.onClose();
				}
			});
		}
		this.UpdateAsObservable().Subscribe(delegate(Unit _)
		{
			if (null == this.rtfScroll || null == this.rtfContent)
			{
				return;
			}
			if (null == this.textLog || this.sbAdd.Length == 0)
			{
				return;
			}
			Text text = UnityEngine.Object.Instantiate<Text>(this.textLog);
			text.transform.SetParent(this.rtfContent.transform, false);
			text.text = this.sbAdd.ToString().TrimEnd(new char[]
			{
				'\r',
				'\n'
			});
			text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.preferredHeight);
			text.gameObject.SetActive(true);
			this.sbAdd.Length = 0;
		});
	}

	// Token: 0x06008A25 RID: 35365 RVA: 0x003A20FE File Offset: 0x003A04FE
	public void SetActiveCanvas(bool active)
	{
		if (this.canvasLog)
		{
			this.canvasLog.gameObject.SetActive(active);
		}
	}

	// Token: 0x06008A26 RID: 35366 RVA: 0x003A2124 File Offset: 0x003A0524
	public void AddLog(string format, params object[] args)
	{
		string text = format;
		for (int i = 0; i < args.Length; i++)
		{
			text = text.Replace("{" + i + "}", args[i].ToString());
		}
		this.sbAdd.Append(string.Format("{0}\n", text));
	}

	// Token: 0x06008A27 RID: 35367 RVA: 0x003A2184 File Offset: 0x003A0584
	public void AddLog(Color color, string format, params object[] args)
	{
		string text = format;
		for (int i = 0; i < args.Length; i++)
		{
			text = text.Replace("{" + i + "}", args[i].ToString());
		}
		this.sbAdd.Append(string.Format("<color=#{0}>{1}</color>\n", ColorUtility.ToHtmlStringRGBA(color), text));
	}

	// Token: 0x06008A28 RID: 35368 RVA: 0x003A21E8 File Offset: 0x003A05E8
	public void StartLog()
	{
		for (int i = this.rtfContent.childCount - 1; i >= 0; i--)
		{
			Transform child = this.rtfContent.GetChild(i);
			UnityEngine.Object.Destroy(child.gameObject);
		}
		Resources.UnloadUnusedAssets();
		this.dictTextLog.Clear();
		this.sbAdd.Length = 0;
		this.processing.update = true;
		if (this.btnClose)
		{
			this.btnClose.interactable = false;
		}
		this.SetActiveCanvas(true);
	}

	// Token: 0x06008A29 RID: 35369 RVA: 0x003A2277 File Offset: 0x003A0677
	public void EndLog()
	{
		this.processing.update = false;
		if (this.btnClose)
		{
			this.btnClose.interactable = true;
		}
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x06008A2A RID: 35370 RVA: 0x003A22A7 File Offset: 0x003A06A7
	private void Update()
	{
	}

	// Token: 0x0400706D RID: 28781
	[SerializeField]
	private Processing processing;

	// Token: 0x0400706E RID: 28782
	[SerializeField]
	private Canvas canvasLog;

	// Token: 0x0400706F RID: 28783
	[SerializeField]
	private RectTransform rtfScroll;

	// Token: 0x04007070 RID: 28784
	[SerializeField]
	private RectTransform rtfContent;

	// Token: 0x04007071 RID: 28785
	[SerializeField]
	private Text textLog;

	// Token: 0x04007072 RID: 28786
	[SerializeField]
	private Button btnClose;

	// Token: 0x04007073 RID: 28787
	private Dictionary<int, Text> dictTextLog = new Dictionary<int, Text>();

	// Token: 0x04007074 RID: 28788
	private StringBuilder sbAdd = new StringBuilder(4096);

	// Token: 0x04007075 RID: 28789
	public Action onClose;
}
