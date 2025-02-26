using System;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x02001016 RID: 4118
	[Serializable]
	public class NetFileComponent : MonoBehaviour
	{
		// Token: 0x06008A42 RID: 35394 RVA: 0x003A2F1C File Offset: 0x003A131C
		public void SetState(bool interactable, bool enable)
		{
			if (null != this.tglItem)
			{
				this.tglItem.interactable = interactable;
			}
			if (null != this.rawImage)
			{
				this.rawImage.enabled = enable;
			}
			if (null != this.objSortInfo)
			{
				this.objSortInfo.SetActiveIfDifferent(interactable);
			}
			if (null != this.btnLike)
			{
				this.btnLike.gameObject.SetActiveIfDifferent(enable);
			}
		}

		// Token: 0x06008A43 RID: 35395 RVA: 0x003A2FA4 File Offset: 0x003A13A4
		public void SetImage(Texture tex)
		{
			if (null != this.rawImage)
			{
				this.rawImage.enabled = (null != tex);
				if (null != this.rawImage.texture)
				{
					UnityEngine.Object.Destroy(this.rawImage.texture);
				}
				this.rawImage.texture = tex;
			}
		}

		// Token: 0x06008A44 RID: 35396 RVA: 0x003A3008 File Offset: 0x003A1408
		public void UpdateSortType(int type)
		{
			bool[,] array = new bool[,]
			{
				{
					false,
					false,
					false,
					false,
					true,
					true,
					false
				},
				{
					false,
					false,
					false,
					false,
					false,
					false,
					true
				},
				{
					true,
					true,
					true,
					true,
					false,
					false,
					false
				}
			};
			if (null != this.objRank)
			{
				this.objRank.SetActiveIfDifferent(array[0, type]);
			}
			if (null != this.objApplause)
			{
				this.objApplause.SetActiveIfDifferent(array[1, type]);
			}
			if (null != this.objDate)
			{
				this.objDate.SetActiveIfDifferent(array[2, type]);
			}
		}

		// Token: 0x06008A45 RID: 35397 RVA: 0x003A3098 File Offset: 0x003A1498
		public void SetRanking(int no)
		{
			for (int i = 0; i < this.imgRank.Length; i++)
			{
				this.imgRank[i].enabled = false;
			}
			if (1 <= no && no <= 3)
			{
				this.imgRank[no - 1].enabled = true;
				if (null != this.textRank)
				{
					this.textRank.text = string.Empty;
				}
			}
			else if (null != this.textRank)
			{
				this.textRank.text = string.Format("{0}位", no);
			}
		}

		// Token: 0x06008A46 RID: 35398 RVA: 0x003A313C File Offset: 0x003A153C
		public void SetUpdateTime(DateTime time, int kind)
		{
			if (null != this.textDateTitle)
			{
				this.textDateTitle.text = ((kind != 0) ? "更新日時：" : "投稿日時：");
			}
			if (null != this.textDate)
			{
				this.textDate.text = time.ToString("yyyy/MM/dd");
			}
		}

		// Token: 0x06008A47 RID: 35399 RVA: 0x003A31A2 File Offset: 0x003A15A2
		public void SetApplauseNum(int num)
		{
			if (null != this.textApplauseNum)
			{
				this.textApplauseNum.text = num.ToString();
			}
		}

		// Token: 0x06008A48 RID: 35400 RVA: 0x003A31CD File Offset: 0x003A15CD
		public void EnableApplause(bool enable)
		{
			if (null != this.btnLike)
			{
				this.btnLike.interactable = enable;
			}
		}

		// Token: 0x06008A49 RID: 35401 RVA: 0x003A31EC File Offset: 0x003A15EC
		private void Awake()
		{
		}

		// Token: 0x06008A4A RID: 35402 RVA: 0x003A31F0 File Offset: 0x003A15F0
		private void Reset()
		{
			Transform transform = base.transform.Find("imgBack/sortinfo");
			if (null != transform)
			{
				this.objSortInfo = transform.gameObject;
			}
			transform = base.transform.Find("Image/RawImage");
			if (null != transform)
			{
				this.rawImage = transform.GetComponent<RawImage>();
			}
			this.tglItem = base.GetComponent<Toggle>();
			transform = base.transform.Find("imgBack/sortinfo/rank");
			if (null != transform)
			{
				this.objRank = transform.gameObject;
			}
			transform = base.transform.Find("imgBack/sortinfo/rank/textRank");
			if (null != transform)
			{
				this.textRank = transform.GetComponent<Text>();
			}
			this.imgRank = new Image[3];
			transform = base.transform.Find("imgBack/sortinfo/rank/imgRank00");
			if (null != transform)
			{
				this.imgRank[0] = transform.GetComponent<Image>();
			}
			transform = base.transform.Find("imgBack/sortinfo/rank/imgRank01");
			if (null != transform)
			{
				this.imgRank[1] = transform.GetComponent<Image>();
			}
			transform = base.transform.Find("imgBack/sortinfo/rank/imgRank02");
			if (null != transform)
			{
				this.imgRank[2] = transform.GetComponent<Image>();
			}
			transform = base.transform.Find("imgBack/sortinfo/applausenum");
			if (null != transform)
			{
				this.objApplause = transform.gameObject;
			}
			transform = base.transform.Find("imgBack/sortinfo/applausenum/textApplauseNum");
			if (null != transform)
			{
				this.textApplauseNum = transform.GetComponent<Text>();
			}
			transform = base.transform.Find("imgBack/sortinfo/applausenum/btnLike");
			if (null != transform)
			{
				this.btnLike = transform.GetComponent<UI_ButtonEx>();
			}
			transform = base.transform.Find("imgBack/sortinfo/date");
			if (null != transform)
			{
				this.objDate = transform.gameObject;
			}
			transform = base.transform.Find("imgBack/sortinfo/date/textUpTimeTitle");
			if (null != transform)
			{
				this.textDateTitle = transform.GetComponent<Text>();
			}
			transform = base.transform.Find("imgBack/sortinfo/date/textUpTime");
			if (null != transform)
			{
				this.textDate = transform.GetComponent<Text>();
			}
		}

		// Token: 0x06008A4B RID: 35403 RVA: 0x003A3432 File Offset: 0x003A1832
		private void Start()
		{
			if (null != this.btnLike)
			{
				this.btnLike.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (this.actApplause != null)
					{
						this.actApplause();
					}
				});
			}
		}

		// Token: 0x06008A4C RID: 35404 RVA: 0x003A3462 File Offset: 0x003A1862
		private void Update()
		{
		}

		// Token: 0x0400709F RID: 28831
		[Header("---< 基本 >--------------------------")]
		[SerializeField]
		private GameObject objSortInfo;

		// Token: 0x040070A0 RID: 28832
		[SerializeField]
		private RawImage rawImage;

		// Token: 0x040070A1 RID: 28833
		public Toggle tglItem;

		// Token: 0x040070A2 RID: 28834
		[Header("---< ランク >------------------------")]
		[SerializeField]
		private GameObject objRank;

		// Token: 0x040070A3 RID: 28835
		[SerializeField]
		private Text textRank;

		// Token: 0x040070A4 RID: 28836
		[SerializeField]
		private Image[] imgRank;

		// Token: 0x040070A5 RID: 28837
		[Header("---< 拍手 >--------------------")]
		[SerializeField]
		private GameObject objApplause;

		// Token: 0x040070A6 RID: 28838
		[SerializeField]
		private UI_ButtonEx btnLike;

		// Token: 0x040070A7 RID: 28839
		[SerializeField]
		private Text textApplauseNum;

		// Token: 0x040070A8 RID: 28840
		public Action actApplause;

		// Token: 0x040070A9 RID: 28841
		[Header("---< 更新日 >------------------------")]
		[SerializeField]
		private GameObject objDate;

		// Token: 0x040070AA RID: 28842
		[SerializeField]
		private Text textDateTitle;

		// Token: 0x040070AB RID: 28843
		[SerializeField]
		private Text textDate;
	}
}
