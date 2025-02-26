using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02001368 RID: 4968
public class CheckLog : MonoBehaviour
{
	// Token: 0x0600A670 RID: 42608 RVA: 0x0043AB8C File Offset: 0x00438F8C
	private void Start()
	{
		this.scrollR = this.rtfScroll.GetComponent<ScrollRect>();
		if (this.scrollR)
		{
			(from _ in this.scrollR.UpdateAsObservable()
			where this.updateNormalizePosition
			select _).Subscribe(delegate(Unit _)
			{
				this.updateNormalizePosition = false;
				this.scrollR.verticalNormalizedPosition = 0f;
			});
		}
	}

	// Token: 0x0600A671 RID: 42609 RVA: 0x0043ABE8 File Offset: 0x00438FE8
	private Text CloneText(string str)
	{
		Text text = UnityEngine.Object.Instantiate<Text>(this.tmpText);
		text.transform.SetParent(this.rtfContent.transform, false);
		text.text = str;
		text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.preferredHeight);
		text.gameObject.SetActive(true);
		this.updateNormalizePosition = true;
		return text;
	}

	// Token: 0x0600A672 RID: 42610 RVA: 0x0043AC5C File Offset: 0x0043905C
	public int AddLog(string format, params object[] args)
	{
		string text = format;
		for (int i = 0; i < args.Length; i++)
		{
			text = text.Replace("{" + i + "}", args[i].ToString());
		}
		Text item = this.CloneText(text);
		this.lstText.Add(item);
		return this.lstText.Count - 1;
	}

	// Token: 0x0600A673 RID: 42611 RVA: 0x0043ACC4 File Offset: 0x004390C4
	public void UpdateLog(int index, string format, params object[] args)
	{
		if (index >= this.lstText.Count)
		{
			return;
		}
		string text = format;
		for (int i = 0; i < args.Length; i++)
		{
			text = text.Replace("{" + i + "}", args[i].ToString());
		}
		this.lstText[index].text = text;
	}

	// Token: 0x0600A674 RID: 42612 RVA: 0x0043AD30 File Offset: 0x00439130
	public int AddLog(Color color, string format, params object[] args)
	{
		string text = format;
		for (int i = 0; i < args.Length; i++)
		{
			text = text.Replace("{" + i + "}", args[i].ToString());
		}
		text = string.Format("<color=#{0}>{1}</color>\n", ColorUtility.ToHtmlStringRGBA(color), text);
		Text item = this.CloneText(text);
		this.lstText.Add(item);
		return this.lstText.Count - 1;
	}

	// Token: 0x0600A675 RID: 42613 RVA: 0x0043ADAC File Offset: 0x004391AC
	public void UpdateLog(int index, Color color, string format, params object[] args)
	{
		if (index >= this.lstText.Count)
		{
			return;
		}
		string text = format;
		for (int i = 0; i < args.Length; i++)
		{
			text = text.Replace("{" + i + "}", args[i].ToString());
		}
		text = string.Format("<color=#{0}>{1}</color>\n", ColorUtility.ToHtmlStringRGBA(color), text);
		this.lstText[index].text = text;
	}

	// Token: 0x0600A676 RID: 42614 RVA: 0x0043AE2C File Offset: 0x0043922C
	public void StartLog()
	{
		for (int i = this.rtfContent.childCount - 1; i >= 0; i--)
		{
			Transform child = this.rtfContent.GetChild(i);
			UnityEngine.Object.Destroy(child.gameObject);
		}
		Resources.UnloadUnusedAssets();
		this.lstText.Clear();
	}

	// Token: 0x0600A677 RID: 42615 RVA: 0x0043AE80 File Offset: 0x00439280
	public void EndLog()
	{
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x0600A678 RID: 42616 RVA: 0x0043AE88 File Offset: 0x00439288
	private void Update()
	{
	}

	// Token: 0x040082C3 RID: 33475
	[SerializeField]
	private RectTransform rtfScroll;

	// Token: 0x040082C4 RID: 33476
	[SerializeField]
	private RectTransform rtfContent;

	// Token: 0x040082C5 RID: 33477
	[SerializeField]
	private Text tmpText;

	// Token: 0x040082C6 RID: 33478
	private ScrollRect scrollR;

	// Token: 0x040082C7 RID: 33479
	private bool updateNormalizePosition;

	// Token: 0x040082C8 RID: 33480
	private List<Text> lstText = new List<Text>();
}
