using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001281 RID: 4737
	public class SortCanvas : Singleton<SortCanvas>
	{
		// Token: 0x1700217C RID: 8572
		// (set) Token: 0x06009C96 RID: 40086 RVA: 0x00401479 File Offset: 0x003FF879
		public static Canvas select
		{
			set
			{
				if (Singleton<SortCanvas>.IsInstance())
				{
					Singleton<SortCanvas>.Instance.OnSelect(value);
				}
			}
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x00401490 File Offset: 0x003FF890
		public void OnSelect(Canvas _canvas)
		{
			if (_canvas == null)
			{
				return;
			}
			SortedList<int, Canvas> sortedList = new SortedList<int, Canvas>();
			_canvas.sortingOrder = 10;
			for (int j = 0; j < this.canvas.Length; j++)
			{
				sortedList.Add(this.canvas[j].sortingOrder, this.canvas[j]);
			}
			foreach (var <>__AnonType in sortedList.Select((KeyValuePair<int, Canvas> l, int i) => new
			{
				l.Value,
				i
			}))
			{
				<>__AnonType.Value.sortingOrder = <>__AnonType.i;
			}
		}

		// Token: 0x06009C98 RID: 40088 RVA: 0x00401560 File Offset: 0x003FF960
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x04007CAE RID: 31918
		[SerializeField]
		private Canvas[] canvas;
	}
}
