using System;
using System.Collections.Generic;
using Illusion.Extensions;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009BE RID: 2494
	[DisallowMultipleComponent]
	public class CustomSelectScrollViewInfo : MonoBehaviour
	{
		// Token: 0x060047AA RID: 18346 RVA: 0x001B89D0 File Offset: 0x001B6DD0
		public void SetData(int _index, CustomSelectScrollController.ScrollData _data, Action<bool> _onClickAction, Action<string> _onPointerEnter, Action _onPointerExit)
		{
			CustomSelectInfo _info = (_data != null) ? _data.info : null;
			bool flag = _info != null;
			this.rows[_index].tgl.gameObject.SetActiveIfDifferent(flag);
			this.rows[_index].tgl.onValueChanged.RemoveAllListeners();
			if (!flag)
			{
				return;
			}
			_data.toggle = this.rows[_index].tgl;
			this.rows[_index].tgl.onValueChanged.RemoveAllListeners();
			this.rows[_index].tgl.onValueChanged.AddListener(delegate(bool _isOn)
			{
				_onClickAction(_isOn);
			});
			EventTriggerNoScroll eventTriggerNoScroll = this.rows[_index].tgl.gameObject.AddComponent<EventTriggerNoScroll>();
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
			this.rows[_index].tgl.SetIsOnWithoutCallback(false);
			Texture2D texture2D = CommonLib.LoadAsset<Texture2D>(_info.assetBundle, _info.assetName, false, string.Empty);
			if (texture2D)
			{
				this.rows[_index].imgThumb.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
			}
			else
			{
				this.rows[_index].imgThumb.sprite = null;
			}
			if (this.rows[_index].imgNew)
			{
				this.rows[_index].imgNew.gameObject.SetActiveIfDifferent(_info.newItem);
			}
			this.rows[_index].info = _info;
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x001B8C14 File Offset: 0x001B7014
		public void SetToggleON(int _index, bool _isOn)
		{
			this.rows[_index].tgl.SetIsOnWithoutCallback(_isOn);
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x001B8C2C File Offset: 0x001B702C
		public void SetNewFlagOff(int _index)
		{
			this.rows[_index].imgNew.gameObject.SetActiveIfDifferent(false);
			this.rows[_index].info.newItem = false;
			Singleton<Character>.Instance.chaListCtrl.AddItemID(this.rows[_index].info.category, this.rows[_index].info.id, 2);
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x001B8C99 File Offset: 0x001B7099
		public CustomSelectInfo GetListInfo(int _index)
		{
			return this.rows[_index].info;
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x001B8CA8 File Offset: 0x001B70A8
		public void Disable(bool disable)
		{
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x001B8CAA File Offset: 0x001B70AA
		public void Disvisible(bool disvisible)
		{
		}

		// Token: 0x040042E6 RID: 17126
		[SerializeField]
		private CustomSelectScrollViewInfo.RowInfo[] rows;

		// Token: 0x020009BF RID: 2495
		[Serializable]
		public class RowInfo
		{
			// Token: 0x040042E7 RID: 17127
			public Toggle tgl;

			// Token: 0x040042E8 RID: 17128
			public Image imgThumb;

			// Token: 0x040042E9 RID: 17129
			public Image imgNew;

			// Token: 0x040042EA RID: 17130
			public CustomSelectInfo info;
		}
	}
}
