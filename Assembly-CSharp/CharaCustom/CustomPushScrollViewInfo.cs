using System;
using System.Collections.Generic;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009B9 RID: 2489
	[DisallowMultipleComponent]
	public class CustomPushScrollViewInfo : MonoBehaviour
	{
		// Token: 0x06004792 RID: 18322 RVA: 0x001B8220 File Offset: 0x001B6620
		public void SetData(int _index, CustomPushInfo _info, Action _onClickAction, Action<string> _onPointerEnter, Action _onPointerExit)
		{
			bool flag = _info != null;
			this.rows[_index].btn.gameObject.SetActiveIfDifferent(flag);
			this.rows[_index].btn.onClick.RemoveAllListeners();
			if (!flag)
			{
				return;
			}
			this.rows[_index].btn.onClick.RemoveAllListeners();
			this.rows[_index].btn.onClick.AddListener(delegate()
			{
				_onClickAction();
			});
			EventTriggerNoScroll eventTriggerNoScroll = this.rows[_index].btn.gameObject.AddComponent<EventTriggerNoScroll>();
			eventTriggerNoScroll.triggers = new List<EventTriggerNoScroll.Entry>();
			EventTriggerNoScroll.Entry entry = new EventTriggerNoScroll.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback.AddListener(delegate(BaseEventData value)
			{
				_onPointerEnter(_info.name);
			});
			eventTriggerNoScroll.triggers.Add(entry);
			entry = new EventTriggerNoScroll.Entry();
			entry.eventID = EventTriggerType.PointerExit;
			entry.callback.AddListener(delegate(BaseEventData value)
			{
				_onPointerExit();
			});
			eventTriggerNoScroll.triggers.Add(entry);
			Texture2D texture2D = CommonLib.LoadAsset<Texture2D>(_info.assetBundle, _info.assetName, false, string.Empty);
			if (texture2D)
			{
				this.rows[_index].imgThumb.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
			}
			else
			{
				this.rows[_index].imgThumb.sprite = null;
			}
			this.rows[_index].info = _info;
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x001B83F6 File Offset: 0x001B67F6
		public CustomPushInfo GetListInfo(int _index)
		{
			return this.rows[_index].info;
		}

		// Token: 0x040042CB RID: 17099
		[SerializeField]
		private CustomPushScrollViewInfo.RowInfo[] rows;

		// Token: 0x020009BA RID: 2490
		[Serializable]
		public class RowInfo
		{
			// Token: 0x040042CC RID: 17100
			public Button btn;

			// Token: 0x040042CD RID: 17101
			public Image imgThumb;

			// Token: 0x040042CE RID: 17102
			public CustomPushInfo info;
		}
	}
}
