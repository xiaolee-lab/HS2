using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.UI.Popup;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000FDB RID: 4059
	[RequireComponent(typeof(RectTransform))]
	public class ResultMessageUI : MonoBehaviour
	{
		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06008738 RID: 34616 RVA: 0x00386965 File Offset: 0x00384D65
		public OneColor White
		{
			[CompilerGenerated]
			get
			{
				return this.white;
			}
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06008739 RID: 34617 RVA: 0x0038696D File Offset: 0x00384D6D
		public OneColor LightGreen
		{
			[CompilerGenerated]
			get
			{
				return this.lightGreen;
			}
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x0600873A RID: 34618 RVA: 0x00386975 File Offset: 0x00384D75
		public OneColor Green
		{
			[CompilerGenerated]
			get
			{
				return this.green;
			}
		}

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x0600873B RID: 34619 RVA: 0x0038697D File Offset: 0x00384D7D
		public OneColor Yellow
		{
			[CompilerGenerated]
			get
			{
				return this.yellow;
			}
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x0600873C RID: 34620 RVA: 0x00386985 File Offset: 0x00384D85
		public OneColor Blue
		{
			[CompilerGenerated]
			get
			{
				return this.blue;
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x0600873D RID: 34621 RVA: 0x0038698D File Offset: 0x00384D8D
		public OneColor Cyan
		{
			[CompilerGenerated]
			get
			{
				return this.cyan;
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x0600873E RID: 34622 RVA: 0x00386995 File Offset: 0x00384D95
		public OneColor Red
		{
			[CompilerGenerated]
			get
			{
				return this.red;
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x0600873F RID: 34623 RVA: 0x0038699D File Offset: 0x00384D9D
		public OneColor DarkRed
		{
			[CompilerGenerated]
			get
			{
				return this.darkRed;
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06008740 RID: 34624 RVA: 0x003869A5 File Offset: 0x00384DA5
		public OneColor Black
		{
			[CompilerGenerated]
			get
			{
				return this.black;
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06008741 RID: 34625 RVA: 0x003869AD File Offset: 0x00384DAD
		public OneColor DarkBlack
		{
			[CompilerGenerated]
			get
			{
				return this.darkBlack;
			}
		}

		// Token: 0x06008742 RID: 34626 RVA: 0x003869B5 File Offset: 0x00384DB5
		private void Awake()
		{
			this.openMessageList = ListPool<ResultMessageElement>.Get();
			this.closeMessageList = ListPool<ResultMessageElement>.Get();
		}

		// Token: 0x06008743 RID: 34627 RVA: 0x003869CD File Offset: 0x00384DCD
		private void OnDestroy()
		{
			ListPool<ResultMessageElement>.Release(this.openMessageList);
			ListPool<ResultMessageElement>.Release(this.closeMessageList);
			this.openMessageList = null;
			this.closeMessageList = null;
		}

		// Token: 0x06008744 RID: 34628 RVA: 0x003869F4 File Offset: 0x00384DF4
		private void CloseAction(ResultMessageElement _child)
		{
			if (this.openMessageList == null || this.closeMessageList == null)
			{
				return;
			}
			if (this.openMessageList.Contains(_child))
			{
				this.openMessageList.Remove(_child);
			}
			if (!this.closeMessageList.Contains(_child))
			{
				this.closeMessageList.Add(_child);
			}
			if (_child.gameObject.activeSelf)
			{
				_child.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008745 RID: 34629 RVA: 0x00386A70 File Offset: 0x00384E70
		public void ShowCancel()
		{
			if (!this.openMessageList.IsNullOrEmpty<ResultMessageElement>())
			{
				foreach (ResultMessageElement resultMessageElement in this.openMessageList)
				{
					if (resultMessageElement != null)
					{
						resultMessageElement.Dispose();
						resultMessageElement.CanvasAlpha = 0f;
						if (resultMessageElement.gameObject.activeSelf)
						{
							resultMessageElement.gameObject.SetActive(false);
						}
						if (!this.closeMessageList.Contains(resultMessageElement))
						{
							this.closeMessageList.Add(resultMessageElement);
						}
					}
				}
				this.openMessageList.Clear();
			}
		}

		// Token: 0x06008746 RID: 34630 RVA: 0x00386B38 File Offset: 0x00384F38
		public void ShowMessage(string _mes)
		{
			if (_mes.IsNullOrEmpty())
			{
				return;
			}
			if (!this.openMessageList.IsNullOrEmpty<ResultMessageElement>())
			{
				ResultMessageElement resultMessageElement = this.openMessageList[this.openMessageList.Count - 1];
				if (resultMessageElement.Message == _mes)
				{
					if (resultMessageElement.PlayingFadeIn)
					{
						return;
					}
					if (resultMessageElement.PlayingDisplay)
					{
						resultMessageElement.StartDisplay();
						return;
					}
				}
			}
			ResultMessageElement resultMessageElement2 = this.closeMessageList.FirstOrDefault<ResultMessageElement>();
			if (resultMessageElement2 == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.messagePrefab, base.transform, true);
				resultMessageElement2 = gameObject.GetComponent<ResultMessageElement>();
				if (resultMessageElement2 == null)
				{
					return;
				}
				resultMessageElement2.Root = this;
				resultMessageElement2.EndAction = new Action<ResultMessageElement>(this.CloseAction);
			}
			else
			{
				this.closeMessageList.RemoveAt(0);
			}
			foreach (ResultMessageElement resultMessageElement3 in this.openMessageList)
			{
				resultMessageElement3.CloseMessage();
			}
			this.openMessageList.Add(resultMessageElement2);
			resultMessageElement2.transform.SetAsLastSibling();
			if (!resultMessageElement2.gameObject.activeSelf)
			{
				resultMessageElement2.gameObject.SetActive(true);
			}
			resultMessageElement2.ShowMessage(_mes);
		}

		// Token: 0x04006DE3 RID: 28131
		[SerializeField]
		private GameObject messagePrefab;

		// Token: 0x04006DE4 RID: 28132
		private List<ResultMessageElement> openMessageList;

		// Token: 0x04006DE5 RID: 28133
		private List<ResultMessageElement> closeMessageList;

		// Token: 0x04006DE6 RID: 28134
		[SerializeField]
		[LabelText("白")]
		private OneColor white = new OneColor(235f, 226f, 215f, 255f);

		// Token: 0x04006DE7 RID: 28135
		[SerializeField]
		[LabelText("明緑")]
		private OneColor lightGreen = new OneColor(133f, 214f, 83f, 255f);

		// Token: 0x04006DE8 RID: 28136
		[SerializeField]
		[LabelText("緑")]
		private OneColor green = new OneColor(100f, 185f, 22f, 255f);

		// Token: 0x04006DE9 RID: 28137
		[SerializeField]
		[LabelText("黄")]
		private OneColor yellow = new OneColor(204f, 197f, 59f, 255f);

		// Token: 0x04006DEA RID: 28138
		[SerializeField]
		[LabelText("青")]
		private OneColor blue = new OneColor(0f, 183f, 238f, 255f);

		// Token: 0x04006DEB RID: 28139
		[SerializeField]
		[LabelText("シアン")]
		private OneColor cyan = new OneColor(98f, 215f, 245f, 255f);

		// Token: 0x04006DEC RID: 28140
		[SerializeField]
		[LabelText("赤")]
		private OneColor red = new OneColor(222f, 69f, 41f, 255f);

		// Token: 0x04006DED RID: 28141
		[SerializeField]
		[LabelText("暗赤")]
		private OneColor darkRed = new OneColor(198f, 69f, 73f, 255f);

		// Token: 0x04006DEE RID: 28142
		[SerializeField]
		[LabelText("黒")]
		private OneColor black = new OneColor(23f, 30f, 36f, 255f);

		// Token: 0x04006DEF RID: 28143
		[SerializeField]
		[LabelText("暗黒")]
		private OneColor darkBlack = new OneColor(3f, 4f, 5f, 255f);

		// Token: 0x04006DF0 RID: 28144
		private CompositeDisposable showMessageDisposable;
	}
}
