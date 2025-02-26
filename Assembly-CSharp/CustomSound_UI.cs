using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000846 RID: 2118
public class CustomSound_UI : MonoBehaviour
{
	// Token: 0x06003616 RID: 13846 RVA: 0x0013EC8C File Offset: 0x0013D08C
	private void Start()
	{
		this.bCanUseList = false;
		this.nSetBGMArea = -1;
		this.SetList = new string[this.bn_DecidedSong.Length];
		this.customSound.FileToBGMList(this.SetList);
		for (int l = 0; l < this.bn_DecidedSong.Length; l++)
		{
			int ID = l;
			if (this.bn_DecidedSong[l] != null)
			{
				this.bn_DecidedSong[l].GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(this.SetList[l]);
				this.bn_DecidedSong[l].onClick.AddListener(delegate()
				{
					this.BGMListOpen(ID);
				});
			}
		}
		string[] files = Directory.GetFiles(contents.AudioFileDirectory, "*.wav");
		string[] files2 = Directory.GetFiles(contents.AudioFileDirectory, "*.mp3");
		this.nSongNum = files.Length + files2.Length;
		this.nPageNum = this.nSongNum / 2;
		if (this.nSongNum % 2 != 0)
		{
			this.nPageNum++;
		}
		this.SongPath = new string[this.nSongNum];
		RectTransform component = GameObject.Find("Canvas/SongList/Viewport/Content").GetComponent<RectTransform>();
		float spacing = component.GetComponent<HorizontalOrVerticalLayoutGroup>().spacing;
		float preferredWidth = this.PageListColumn.GetComponent<LayoutElement>().preferredWidth;
		component.sizeDelta = new Vector2((preferredWidth + spacing) * (float)this.nPageNum, 0f);
		for (int j = 0; j < this.nPageNum; j++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PageListColumn);
			this.PageList.Add(gameObject);
			gameObject.transform.SetParent(component, false);
		}
		RectTransform content = this.PageList[0].GetComponentInChildren<ScrollRect>().content;
		float spacing2 = content.GetComponent<VerticalLayoutGroup>().spacing;
		float preferredHeight = this.SongListLine.GetComponent<LayoutElement>().preferredHeight;
		content.sizeDelta = new Vector2(0f, (preferredHeight + spacing2) * (float)this.nSongNum);
		for (int k = 0; k < this.nSongNum; k++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.SongListLine);
			this.SongList.Add(gameObject2);
			gameObject2.transform.SetParent(this.PageList[k / 2].GetComponentInChildren<ScrollRect>().content, false);
			if (k < files.Length)
			{
				this.SongPath[k] = files[k].Remove(0, Application.dataPath.ToString().Length - "Assets".Length);
				gameObject2.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(this.SongPath[k]);
			}
			else
			{
				this.SongPath[k] = files2[k - files.Length].Remove(0, Application.dataPath.ToString().Length - "Assets".Length);
				gameObject2.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(this.SongPath[k]);
			}
			int i = k;
			gameObject2.GetComponentInChildren<Button>().onClick.AddListener(delegate()
			{
				this.SongPlay(this.SongPath[i]);
			});
			gameObject2.GetComponentInChildren<Toggle>().group = this.SongList[0].GetComponentInChildren<Toggle>().group;
			if (gameObject2.GetComponentInChildren<Text>().text == Path.GetFileNameWithoutExtension(this.SetList[0]))
			{
				gameObject2.GetComponentInChildren<Toggle>().isOn = true;
			}
			else
			{
				gameObject2.GetComponentInChildren<Toggle>().isOn = false;
			}
		}
		this.nCanLookPageIdx = 0;
		this.PageNoScroll.SetActive(false);
		if (this.bn_SongSet != null)
		{
			this.bn_SongSet.onClick.AddListener(new UnityAction(this.BGMSet));
		}
		if (this.bn_Back != null)
		{
			this.bn_Back.onClick.AddListener(new UnityAction(this.Back));
		}
		if (this.bn_Stop != null)
		{
			this.bn_Stop.onClick.AddListener(new UnityAction(this.customSound.SongEnd));
		}
		if (this.bn_Prev != null)
		{
			if (this.nSongNum < 2)
			{
				this.bn_Prev.interactable = false;
			}
			this.bn_Prev.onClick.AddListener(delegate()
			{
				this.Scroll(false);
			});
		}
		if (this.bn_Next != null)
		{
			if (this.nSongNum < 2)
			{
				this.bn_Next.interactable = false;
			}
			this.bn_Next.onClick.AddListener(delegate()
			{
				this.Scroll(true);
			});
		}
		if (this.cantImageFilter != null)
		{
			this.cantImageFilter.enabled = true;
		}
		if (this.SelectCursol != null)
		{
			this.SelectCursol.enabled = false;
		}
		if (this.bt_PageNum != null)
		{
			this.bt_PageNum.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", this.nCanLookPageIdx + 1, this.nPageNum);
			this.bt_PageNum.onClick.AddListener(new UnityAction(this.SetPageNoList));
		}
		this.parent = GameObject.Find("Canvas/SongList").GetComponent<ScrollRect>();
		this.bPageNoScroll = false;
	}

	// Token: 0x06003617 RID: 13847 RVA: 0x0013F210 File Offset: 0x0013D610
	private void Update()
	{
		if (this.bCanUseList)
		{
			this.cantImageFilter.enabled = false;
			this.SetSelectCursol();
		}
		else
		{
			this.cantImageFilter.enabled = true;
		}
	}

	// Token: 0x06003618 RID: 13848 RVA: 0x0013F240 File Offset: 0x0013D640
	public void BGMListOpen(int ID)
	{
		if (ID != this.nSetBGMArea)
		{
			this.bCanUseList = true;
			this.nSetBGMArea = ID;
			this.ChangeListMark();
			this.SetSelectCursol();
		}
	}

	// Token: 0x06003619 RID: 13849 RVA: 0x0013F268 File Offset: 0x0013D668
	public bool IsUse()
	{
		return this.bCanUseList;
	}

	// Token: 0x0600361A RID: 13850 RVA: 0x0013F270 File Offset: 0x0013D670
	public void SongPlay(string szTarget)
	{
		this.customSound.LoadFile(szTarget);
		this.customSound.SongPlay();
	}

	// Token: 0x0600361B RID: 13851 RVA: 0x0013F28C File Offset: 0x0013D68C
	public void BGMSet()
	{
		if (this.bCanUseList)
		{
			this.bCanUseList = false;
			for (int i = 0; i < this.nSongNum; i++)
			{
				if (this.SongList[i].GetComponentInChildren<Toggle>().isOn)
				{
					this.SetList[this.nSetBGMArea] = this.SongPath[i];
					this.bn_DecidedSong[this.nSetBGMArea].GetComponentInChildren<Text>().text = this.SongList[i].GetComponentInChildren<Text>().text;
					break;
				}
			}
			this.nSetBGMArea = -1;
			this.SelectCursol.enabled = false;
			return;
		}
	}

	// Token: 0x0600361C RID: 13852 RVA: 0x0013F340 File Offset: 0x0013D740
	public void Back()
	{
		if (this.bCanUseList)
		{
			this.bCanUseList = false;
			this.SelectCursol.enabled = false;
			this.nSetBGMArea = -1;
		}
		else
		{
			this.customSound.BGMListToFile(this.SetList);
			this.customSound.SongEnd();
		}
	}

	// Token: 0x0600361D RID: 13853 RVA: 0x0013F394 File Offset: 0x0013D794
	private void ChangeListMark()
	{
		for (int i = 0; i < this.nSongNum; i++)
		{
			if (this.SongList[i].GetComponentInChildren<Text>().text == Path.GetFileNameWithoutExtension(this.SetList[this.nSetBGMArea]))
			{
				this.SongList[i].GetComponentInChildren<Toggle>().isOn = true;
			}
		}
	}

	// Token: 0x0600361E RID: 13854 RVA: 0x0013F404 File Offset: 0x0013D804
	private void SetSelectCursol()
	{
		Vector3 position = this.SelectCursol.transform.position;
		position.y = this.bn_DecidedSong[this.nSetBGMArea].transform.position.y;
		this.SelectCursol.transform.position = position;
		this.SelectCursol.enabled = true;
	}

	// Token: 0x0600361F RID: 13855 RVA: 0x0013F468 File Offset: 0x0013D868
	private void Scroll(bool next)
	{
		if (!this.bCanUseList)
		{
			return;
		}
		if (this.nPageNum == 1)
		{
			return;
		}
		float num = 1f / (float)(this.nPageNum - 1);
		if (next && this.nCanLookPageIdx < this.nPageNum - 1)
		{
			this.nCanLookPageIdx++;
		}
		else if (!next && this.nCanLookPageIdx > 0)
		{
			this.nCanLookPageIdx--;
		}
		this.parent.horizontalNormalizedPosition = num * (float)this.nCanLookPageIdx;
		this.bt_PageNum.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", this.nCanLookPageIdx + 1, this.nPageNum);
	}

	// Token: 0x06003620 RID: 13856 RVA: 0x0013F530 File Offset: 0x0013D930
	private void SetPageNoList()
	{
		this.PageNoScroll.SetActive(true);
		if (this.nPageNum < 5)
		{
			this.PageNoScroll.GetComponent<RectTransform>().sizeDelta = new Vector2(this.PageNoScroll.GetComponent<RectTransform>().sizeDelta.x, this.PageNoListLine.GetComponent<RectTransform>().sizeDelta.y * (float)this.nPageNum);
		}
		else
		{
			this.PageNoScroll.GetComponent<RectTransform>().sizeDelta = new Vector2(this.PageNoScroll.GetComponent<RectTransform>().sizeDelta.x, this.PageNoListLine.GetComponent<RectTransform>().sizeDelta.y * 5f);
		}
		if (this.bPageNoScroll)
		{
			return;
		}
		RectTransform component = GameObject.Find("Canvas/PageNumList/Viewport/Content").GetComponent<RectTransform>();
		float spacing = component.GetComponent<HorizontalOrVerticalLayoutGroup>().spacing;
		float preferredHeight = this.PageNoListLine.GetComponent<LayoutElement>().preferredHeight;
		component.sizeDelta = new Vector2((spacing + preferredHeight) * (float)this.nPageNum, 0f);
		for (int i = 0; i < this.nPageNum; i++)
		{
			int No = i;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PageNoListLine);
			this.PageNoList.Add(gameObject);
			gameObject.transform.SetParent(component, false);
			gameObject.GetComponentInChildren<Text>().text = (No + 1).ToString();
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.PageJump(No);
			});
		}
		this.bPageNoScroll = true;
	}

	// Token: 0x06003621 RID: 13857 RVA: 0x0013F6F4 File Offset: 0x0013DAF4
	private void PageJump(int JumpPageNo)
	{
		float num = 1f / (float)(this.nPageNum - 1);
		this.nCanLookPageIdx = JumpPageNo;
		this.parent.horizontalNormalizedPosition = num * (float)this.nCanLookPageIdx;
		this.bt_PageNum.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", this.nCanLookPageIdx + 1, this.nPageNum);
		this.PageNoScroll.SetActive(false);
	}

	// Token: 0x04003658 RID: 13912
	public CustomSound customSound;

	// Token: 0x04003659 RID: 13913
	public Button bn_SongSet;

	// Token: 0x0400365A RID: 13914
	public Button bn_Back;

	// Token: 0x0400365B RID: 13915
	public Button bn_Stop;

	// Token: 0x0400365C RID: 13916
	public Button bn_Prev;

	// Token: 0x0400365D RID: 13917
	public Button bn_Next;

	// Token: 0x0400365E RID: 13918
	public Image cantImageFilter;

	// Token: 0x0400365F RID: 13919
	public Image SelectCursol;

	// Token: 0x04003660 RID: 13920
	public Button[] bn_DecidedSong;

	// Token: 0x04003661 RID: 13921
	public Button bt_PageNum;

	// Token: 0x04003662 RID: 13922
	public GameObject PageNoScroll;

	// Token: 0x04003663 RID: 13923
	private string[] SongPath;

	// Token: 0x04003664 RID: 13924
	private string[] SetList;

	// Token: 0x04003665 RID: 13925
	private bool bCanUseList;

	// Token: 0x04003666 RID: 13926
	private int nSetBGMArea;

	// Token: 0x04003667 RID: 13927
	private bool bPageNoScroll;

	// Token: 0x04003668 RID: 13928
	[SerializeField]
	private GameObject SongListLine;

	// Token: 0x04003669 RID: 13929
	[SerializeField]
	private GameObject PageListColumn;

	// Token: 0x0400366A RID: 13930
	[SerializeField]
	private GameObject PageNoListLine;

	// Token: 0x0400366B RID: 13931
	private int nCanLookPageIdx;

	// Token: 0x0400366C RID: 13932
	private int nSongNum;

	// Token: 0x0400366D RID: 13933
	private int nPageNum;

	// Token: 0x0400366E RID: 13934
	private const int nCanLookPageNoNum = 5;

	// Token: 0x0400366F RID: 13935
	private const int nSongNumPerPage = 2;

	// Token: 0x04003670 RID: 13936
	private List<GameObject> SongList = new List<GameObject>();

	// Token: 0x04003671 RID: 13937
	private List<GameObject> PageList = new List<GameObject>();

	// Token: 0x04003672 RID: 13938
	private List<GameObject> PageNoList = new List<GameObject>();

	// Token: 0x04003673 RID: 13939
	private ScrollRect parent;
}
