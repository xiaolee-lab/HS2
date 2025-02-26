using System;
using System.Collections.Generic;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009A8 RID: 2472
	[DisallowMultipleComponent]
	public class CustomCharaScrollViewInfo : MonoBehaviour
	{
		// Token: 0x0600471C RID: 18204 RVA: 0x001B6194 File Offset: 0x001B4594
		public void SetData(int _index, CustomCharaScrollController.ScrollData _data, Action<bool> _onClickAction, Action<string> _onPointerEnter, Action _onPointerExit)
		{
			CustomCharaFileInfo _info = (_data != null) ? _data.info : null;
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
			EventTriggerNoScroll eventTriggerNoScroll = this.rows[_index].tgl.gameObject.GetComponent<EventTriggerNoScroll>();
			if (null == eventTriggerNoScroll)
			{
				eventTriggerNoScroll = this.rows[_index].tgl.gameObject.AddComponent<EventTriggerNoScroll>();
			}
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
			this.rows[_index].tgl.interactable = !_info.isInSaveData;
			Color white = Color.white;
			if (this.rows[_index].imgEntry)
			{
				this.rows[_index].imgEntry.gameObject.SetActiveIfDifferent(_info.isInSaveData);
				if (_info.isInSaveData)
				{
					white = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);
				}
			}
			if (this.rows[_index].imgNurturing)
			{
				this.rows[_index].imgNurturing.gameObject.SetActiveIfDifferent(_info.gameRegistration);
				this.rows[_index].imgNurturing.color = white;
			}
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

		// Token: 0x0600471D RID: 18205 RVA: 0x001B64AD File Offset: 0x001B48AD
		public void SetToggleON(int _index, bool _isOn)
		{
			this.rows[_index].tgl.SetIsOnWithoutCallback(_isOn);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x001B64C2 File Offset: 0x001B48C2
		public CustomCharaFileInfo GetListInfo(int _index)
		{
			return this.rows[_index].info;
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x001B64D1 File Offset: 0x001B48D1
		public void Disable(bool disable)
		{
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x001B64D3 File Offset: 0x001B48D3
		public void Disvisible(bool disvisible)
		{
		}

		// Token: 0x04004252 RID: 16978
		[SerializeField]
		private CustomCharaScrollViewInfo.RowInfo[] rows;

		// Token: 0x04004253 RID: 16979
		[SerializeField]
		private Texture2D texEmpty;

		// Token: 0x020009A9 RID: 2473
		[Serializable]
		public class RowInfo
		{
			// Token: 0x04004254 RID: 16980
			public Toggle tgl;

			// Token: 0x04004255 RID: 16981
			public RawImage imgThumb;

			// Token: 0x04004256 RID: 16982
			public Image imgEntry;

			// Token: 0x04004257 RID: 16983
			public Image imgNurturing;

			// Token: 0x04004258 RID: 16984
			public CustomCharaFileInfo info;
		}
	}
}
