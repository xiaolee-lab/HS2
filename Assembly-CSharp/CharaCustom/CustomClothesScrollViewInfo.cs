using System;
using System.Collections.Generic;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009B2 RID: 2482
	[DisallowMultipleComponent]
	public class CustomClothesScrollViewInfo : MonoBehaviour
	{
		// Token: 0x0600475E RID: 18270 RVA: 0x001B7538 File Offset: 0x001B5938
		public void SetData(int _index, CustomClothesScrollController.ScrollData _data, Action<bool> _onClickAction, Action<string> _onPointerEnter, Action _onPointerExit)
		{
			CustomClothesFileInfo _info = (_data != null) ? _data.info : null;
			bool flag = _info != null;
			this.rows[_index].tgl.gameObject.SetActiveIfDifferent(flag);
			this.rows[_index].tgl.onValueChanged.RemoveAllListeners();
			if (!flag)
			{
				return;
			}
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
			if (_info.pngData != null || !_info.FullPath.IsNullOrEmpty())
			{
				if (this.rows[_index].imgThumb.texture)
				{
					UnityEngine.Object.Destroy(this.rows[_index].imgThumb.texture);
				}
				this.rows[_index].imgThumb.texture = PngAssist.ChangeTextureFromByte(_info.pngData ?? PngFile.LoadPngBytes(_info.FullPath), 0, 0, TextureFormat.ARGB32, false);
			}
			else
			{
				this.rows[_index].imgThumb.texture = this.texEmpty;
			}
			this.rows[_index].info = _info;
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x001B7753 File Offset: 0x001B5B53
		public void SetToggleON(int _index, bool _isOn)
		{
			this.rows[_index].tgl.SetIsOnWithoutCallback(_isOn);
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x001B7768 File Offset: 0x001B5B68
		public CustomClothesFileInfo GetListInfo(int _index)
		{
			return this.rows[_index].info;
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x001B7777 File Offset: 0x001B5B77
		public void Disable(bool disable)
		{
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x001B7779 File Offset: 0x001B5B79
		public void Disvisible(bool disvisible)
		{
		}

		// Token: 0x0400429A RID: 17050
		[SerializeField]
		private CustomClothesScrollViewInfo.RowInfo[] rows;

		// Token: 0x0400429B RID: 17051
		[SerializeField]
		private Texture2D texEmpty;

		// Token: 0x020009B3 RID: 2483
		[Serializable]
		public class RowInfo
		{
			// Token: 0x0400429C RID: 17052
			public Toggle tgl;

			// Token: 0x0400429D RID: 17053
			public RawImage imgThumb;

			// Token: 0x0400429E RID: 17054
			public CustomClothesFileInfo info;
		}
	}
}
