using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020000A6 RID: 166
	public class VirtualizingScrollRect : ScrollRect
	{
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600037F RID: 895 RVA: 0x000151E0 File Offset: 0x000135E0
		// (remove) Token: 0x06000380 RID: 896 RVA: 0x00015218 File Offset: 0x00013618
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DataBindAction ItemDataBinding;

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001524E File Offset: 0x0001364E
		public bool UseGrid
		{
			get
			{
				return this.m_useGrid;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00015256 File Offset: 0x00013656
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00015270 File Offset: 0x00013670
		public int ContainersPerGroup
		{
			get
			{
				if (this.m_useGrid)
				{
					return this.m_gridLayoutGroup.constraintCount;
				}
				return 1;
			}
			set
			{
				if (this.m_useGrid)
				{
					this.m_gridLayoutGroup.constraintCount = value;
					base.scrollSensitivity = this.ContainerSize;
					this.UpdateContentSize();
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001529B File Offset: 0x0001369B
		// (set) Token: 0x06000385 RID: 901 RVA: 0x000152A3 File Offset: 0x000136A3
		public IList Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				if (this.m_items != value)
				{
					this.m_items = value;
					this.DataBind(this.RoundedIndex, true);
					this.UpdateContentSize();
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000152CB File Offset: 0x000136CB
		public int ItemsCount
		{
			get
			{
				if (this.Items == null)
				{
					return 0;
				}
				return this.Items.Count;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000152E5 File Offset: 0x000136E5
		private int RoundedItemsCount
		{
			get
			{
				return Mathf.CeilToInt((float)this.ItemsCount / (float)this.ContainersPerGroup) * this.ContainersPerGroup;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00015302 File Offset: 0x00013702
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0001530A File Offset: 0x0001370A
		private float NormalizedIndex
		{
			get
			{
				return this.m_normalizedIndex;
			}
			set
			{
				if (value == this.m_normalizedIndex)
				{
					return;
				}
				this.OnNormalizedIndexChanged(value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00015320 File Offset: 0x00013720
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0001532F File Offset: 0x0001372F
		public int Index
		{
			get
			{
				return this.RoundedIndex + this.LocalGroupIndex;
			}
			set
			{
				this.RoundedIndex = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00015338 File Offset: 0x00013738
		private int LocalGroupIndex
		{
			get
			{
				if (this.RoundedItemsCount == 0)
				{
					return 0;
				}
				int num = Mathf.RoundToInt(this.NormalizedIndex * (float)Mathf.Max(this.RoundedItemsCount - this.VisibleItemsCount, 0));
				return num % this.ContainersPerGroup;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00015380 File Offset: 0x00013780
		// (set) Token: 0x0600038E RID: 910 RVA: 0x000153DC File Offset: 0x000137DC
		public int RoundedIndex
		{
			get
			{
				if (this.RoundedItemsCount == 0)
				{
					return 0;
				}
				float num = 0.5f / (float)this.RoundedItemsCount;
				int num2 = Mathf.RoundToInt((this.NormalizedIndex + num) * (float)Mathf.Max(this.RoundedItemsCount - this.VisibleItemsCount, 0));
				return num2 / this.ContainersPerGroup * this.ContainersPerGroup;
			}
			set
			{
				if (value < 0 || value >= this.RoundedItemsCount)
				{
					return;
				}
				this.NormalizedIndex = this.EvalNormalizedIndex(value);
				if (this.m_mode == VirtualizingMode.Vertical)
				{
					base.verticalNormalizedPosition = 1f - this.NormalizedIndex;
				}
				else
				{
					base.horizontalNormalizedPosition = this.NormalizedIndex;
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001543C File Offset: 0x0001383C
		private float EvalNormalizedIndex(int index)
		{
			int num = this.RoundedItemsCount - this.VisibleItemsCount;
			if (num <= 0)
			{
				return 0f;
			}
			return (float)index / (float)num;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001546B File Offset: 0x0001386B
		public int VisibleItemsCount
		{
			get
			{
				return Mathf.Min(this.ItemsCount, this.PossibleItemsCount);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001547E File Offset: 0x0001387E
		private int PossibleItemsCount
		{
			get
			{
				if (this.ContainerSize < 1E-05f)
				{
					return 0;
				}
				return Mathf.RoundToInt(this.Size / this.ContainerSize) * this.ContainersPerGroup;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000154AC File Offset: 0x000138AC
		private float ContainerSize
		{
			get
			{
				if (this.m_mode == VirtualizingMode.Horizontal)
				{
					return Mathf.Max(0f, this.ContainerPrefab.rect.width + ((!this.m_useGrid) ? this.m_layoutGroup.spacing : this.m_gridSpacing.x));
				}
				if (this.m_mode == VirtualizingMode.Vertical)
				{
					return Mathf.Max(0f, this.ContainerPrefab.rect.height + ((!this.m_useGrid) ? this.m_layoutGroup.spacing : this.m_gridSpacing.y));
				}
				throw new InvalidOperationException("Unable to eval container size in non-virtualizing mode");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00015564 File Offset: 0x00013964
		private float Size
		{
			get
			{
				if (this.m_mode == VirtualizingMode.Horizontal)
				{
					return Mathf.Max(0f, this.m_virtualContent.rect.width);
				}
				return Mathf.Max(0f, this.m_virtualContent.rect.height);
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000155B8 File Offset: 0x000139B8
		protected override void Awake()
		{
			base.Awake();
			if (this.m_virtualContent == null)
			{
				return;
			}
			this.m_virtualContentTransformChangeListener = this.m_virtualContent.GetComponent<RectTransformChangeListener>();
			this.m_virtualContentTransformChangeListener.RectTransformChanged += this.OnVirtualContentTransformChaged;
			this.UpdateVirtualContentPosition();
			if (this.m_useGrid)
			{
				LayoutGroup component = this.m_virtualContent.GetComponent<LayoutGroup>();
				if (component != null && !(component is GridLayoutGroup))
				{
					UnityEngine.Object.DestroyImmediate(component);
				}
				GridLayoutGroup gridLayoutGroup = this.m_virtualContent.GetComponent<GridLayoutGroup>();
				if (gridLayoutGroup == null)
				{
					gridLayoutGroup = this.m_virtualContent.gameObject.AddComponent<GridLayoutGroup>();
				}
				gridLayoutGroup.cellSize = this.ContainerPrefab.rect.size;
				gridLayoutGroup.childAlignment = TextAnchor.UpperLeft;
				gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
				gridLayoutGroup.spacing = this.m_gridSpacing;
				if (this.m_mode == VirtualizingMode.Vertical)
				{
					gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
					gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
				}
				else
				{
					gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical;
					gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
				}
				this.m_gridLayoutGroup = gridLayoutGroup;
			}
			else if (this.m_mode == VirtualizingMode.Horizontal)
			{
				LayoutGroup component2 = this.m_virtualContent.GetComponent<LayoutGroup>();
				if (component2 != null && !(component2 is HorizontalLayoutGroup))
				{
					UnityEngine.Object.DestroyImmediate(component2);
				}
				HorizontalLayoutGroup horizontalLayoutGroup = this.m_virtualContent.GetComponent<HorizontalLayoutGroup>();
				if (horizontalLayoutGroup == null)
				{
					horizontalLayoutGroup = this.m_virtualContent.gameObject.AddComponent<HorizontalLayoutGroup>();
				}
				horizontalLayoutGroup.childControlHeight = true;
				horizontalLayoutGroup.childControlWidth = false;
				horizontalLayoutGroup.childForceExpandWidth = false;
				this.m_layoutGroup = horizontalLayoutGroup;
			}
			else
			{
				LayoutGroup component3 = this.m_virtualContent.GetComponent<LayoutGroup>();
				if (component3 != null && !(component3 is VerticalLayoutGroup))
				{
					UnityEngine.Object.DestroyImmediate(component3);
				}
				VerticalLayoutGroup verticalLayoutGroup = this.m_virtualContent.GetComponent<VerticalLayoutGroup>();
				if (verticalLayoutGroup == null)
				{
					verticalLayoutGroup = this.m_virtualContent.gameObject.AddComponent<VerticalLayoutGroup>();
				}
				verticalLayoutGroup.childControlWidth = true;
				verticalLayoutGroup.childControlHeight = false;
				verticalLayoutGroup.childForceExpandHeight = false;
				this.m_layoutGroup = verticalLayoutGroup;
			}
			base.scrollSensitivity = this.ContainerSize;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000157DE File Offset: 0x00013BDE
		protected override void Start()
		{
			base.Start();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000157E6 File Offset: 0x00013BE6
		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (this.m_virtualContentTransformChangeListener != null)
			{
				this.m_virtualContentTransformChangeListener.RectTransformChanged -= this.OnVirtualContentTransformChaged;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00015818 File Offset: 0x00013C18
		private void OnVirtualContentTransformChaged()
		{
			if (this.m_containers.Count == 0)
			{
				this.DataBind(this.RoundedIndex, false);
				this.UpdateContentSize();
			}
			if (this.m_mode == VirtualizingMode.Horizontal)
			{
				base.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.m_virtualContent.rect.height);
				if (this.m_useGrid)
				{
					if (this.m_gridLayoutGroup.cellSize.y + this.m_gridSpacing.y < 1E-05f)
					{
					}
					RectTransform rectTransform = (RectTransform)this.m_virtualContent.parent;
					if (base.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.Permanent)
					{
						this.ContainersPerGroup = Mathf.FloorToInt(rectTransform.rect.height / Mathf.Max(1E-05f, this.m_gridLayoutGroup.cellSize.y + this.m_gridSpacing.y));
					}
					else
					{
						this.ContainersPerGroup = Mathf.RoundToInt(rectTransform.rect.height / Mathf.Max(1E-05f, this.m_gridLayoutGroup.cellSize.y + this.m_gridSpacing.y));
					}
				}
			}
			else if (this.m_mode == VirtualizingMode.Vertical)
			{
				base.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.m_virtualContent.rect.width);
				if (this.m_useGrid)
				{
					if (this.m_gridLayoutGroup.cellSize.x + this.m_gridSpacing.x < 1E-05f)
					{
					}
					RectTransform rectTransform2 = (RectTransform)this.m_virtualContent.parent;
					if (base.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.Permanent)
					{
						this.ContainersPerGroup = Mathf.RoundToInt(rectTransform2.rect.width / Mathf.Max(1E-05f, this.m_gridLayoutGroup.cellSize.x + this.m_gridSpacing.x));
					}
					else
					{
						this.ContainersPerGroup = Mathf.RoundToInt(rectTransform2.rect.width / Mathf.Max(1E-05f, this.m_gridLayoutGroup.cellSize.x + this.m_gridSpacing.x));
					}
				}
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00015A64 File Offset: 0x00013E64
		protected override void SetNormalizedPosition(float value, int axis)
		{
			base.SetNormalizedPosition(value, axis);
			this.UpdateVirtualContentPosition();
			if (this.m_mode == VirtualizingMode.Vertical && axis == 1)
			{
				this.NormalizedIndex = 1f - value;
			}
			else if (this.m_mode == VirtualizingMode.Horizontal && axis == 0)
			{
				this.NormalizedIndex = value;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00015ABC File Offset: 0x00013EBC
		protected override void SetContentAnchoredPosition(Vector2 position)
		{
			base.SetContentAnchoredPosition(position);
			this.UpdateVirtualContentPosition();
			if (this.m_mode == VirtualizingMode.Vertical)
			{
				this.NormalizedIndex = 1f - base.verticalNormalizedPosition;
			}
			else if (this.m_mode == VirtualizingMode.Horizontal)
			{
				this.NormalizedIndex = base.horizontalNormalizedPosition;
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00015B10 File Offset: 0x00013F10
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.CoRectTransformDimensionsChange());
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00015B30 File Offset: 0x00013F30
		private IEnumerator CoRectTransformDimensionsChange()
		{
			yield return new WaitForEndOfFrame();
			if (this.VisibleItemsCount != this.m_containers.Count)
			{
				this.DataBind(this.RoundedIndex, false);
			}
			this.OnVirtualContentTransformChaged();
			yield break;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00015B4C File Offset: 0x00013F4C
		private void UpdateVirtualContentPosition()
		{
			if (this.m_virtualContent != null)
			{
				if (this.m_mode == VirtualizingMode.Horizontal)
				{
					this.m_virtualContent.anchoredPosition = new Vector2(0f, base.content.anchoredPosition.y);
				}
				else if (this.m_mode == VirtualizingMode.Vertical)
				{
					this.m_virtualContent.anchoredPosition = new Vector2(base.content.anchoredPosition.x, 0f);
				}
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00015BD8 File Offset: 0x00013FD8
		private void UpdateContentSize()
		{
			if (this.m_mode == VirtualizingMode.Horizontal)
			{
				base.content.sizeDelta = new Vector2((float)Mathf.CeilToInt((float)this.RoundedItemsCount / (float)this.ContainersPerGroup) * this.ContainerSize, base.content.sizeDelta.y);
			}
			else if (this.m_mode == VirtualizingMode.Vertical)
			{
				base.content.sizeDelta = new Vector2(base.content.sizeDelta.x, (float)Mathf.CeilToInt((float)this.RoundedItemsCount / (float)this.ContainersPerGroup) * this.ContainerSize);
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00015C80 File Offset: 0x00014080
		private void OnNormalizedIndexChanged(float newValue)
		{
			newValue = Mathf.Clamp01(newValue);
			int num = this.RoundedIndex;
			float normalizedIndex = this.m_normalizedIndex;
			this.m_normalizedIndex = newValue;
			int roundedIndex = this.RoundedIndex;
			if (roundedIndex < 0 || roundedIndex >= this.RoundedItemsCount)
			{
				this.m_normalizedIndex = normalizedIndex;
				return;
			}
			if (num != roundedIndex)
			{
				int num2 = roundedIndex - num;
				bool flag = num2 > 0;
				num2 = Mathf.Abs(num2);
				if (num2 > this.VisibleItemsCount)
				{
					this.DataBind(roundedIndex, false);
				}
				else if (flag)
				{
					for (int i = 0; i < num2; i++)
					{
						LinkedListNode<RectTransform> first = this.m_containers.First;
						RectTransform value;
						if (this.m_containers.Count > 1)
						{
							this.m_containers.RemoveFirst();
							int siblingIndex = this.m_containers.Last.Value.transform.GetSiblingIndex();
							this.m_containers.AddLast(first);
							value = first.Value;
							value.SetSiblingIndex(siblingIndex + 1);
						}
						else
						{
							value = first.Value;
						}
						if (this.ItemDataBinding != null && this.Items != null)
						{
							int num3 = num + this.VisibleItemsCount;
							if (num3 < this.ItemsCount)
							{
								object item = this.Items[num + this.VisibleItemsCount];
								this.ItemDataBinding(value, item);
							}
							else
							{
								this.ItemDataBinding(value, null);
							}
						}
						num++;
					}
				}
				else
				{
					for (int j = 0; j < num2; j++)
					{
						LinkedListNode<RectTransform> last = this.m_containers.Last;
						RectTransform value2;
						if (this.m_containers.Count > 1)
						{
							this.m_containers.RemoveLast();
							int siblingIndex2 = this.m_containers.First.Value.transform.GetSiblingIndex();
							this.m_containers.AddFirst(last);
							value2 = last.Value;
							value2.SetSiblingIndex(siblingIndex2);
						}
						else
						{
							value2 = last.Value;
						}
						num--;
						if (this.ItemDataBinding != null && this.Items != null)
						{
							if (num < this.ItemsCount)
							{
								object item2 = this.Items[num];
								this.ItemDataBinding(value2, item2);
							}
							else
							{
								this.ItemDataBinding(value2, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00015EDC File Offset: 0x000142DC
		private void DataBind(int firstItemIndex, bool sibling = false)
		{
			int num = this.VisibleItemsCount - this.m_containers.Count;
			if (num < 0)
			{
				for (int i = 0; i < -num; i++)
				{
					UnityEngine.Object.Destroy(this.m_containers.Last.Value.gameObject);
					this.m_containers.RemoveLast();
				}
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					RectTransform value = UnityEngine.Object.Instantiate<RectTransform>(this.ContainerPrefab, this.m_virtualContent);
					this.m_containers.AddLast(value);
				}
			}
			if (this.ItemDataBinding != null && this.Items != null)
			{
				int num2 = 0;
				foreach (RectTransform rectTransform in this.m_containers)
				{
					int num3 = firstItemIndex + num2;
					if (num3 < this.Items.Count)
					{
						this.ItemDataBinding(rectTransform, this.Items[firstItemIndex + num2]);
					}
					else
					{
						this.ItemDataBinding(rectTransform, null);
					}
					if (sibling && rectTransform.GetSiblingIndex() != num2)
					{
						rectTransform.SetSiblingIndex(num2);
					}
					num2++;
				}
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00016040 File Offset: 0x00014440
		public bool IsParentOf(Transform child)
		{
			return !(this.m_virtualContent == null) && child.IsChildOf(this.m_virtualContent);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00016064 File Offset: 0x00014464
		public void InsertItem(int index, object item, bool raiseItemDataBindingEvent = true)
		{
			int roundedIndex = this.RoundedIndex;
			int num = roundedIndex + this.VisibleItemsCount - 1;
			this.m_items.Insert(index, item);
			this.UpdateContentSize();
			this.UpdateScrollbar(roundedIndex);
			if (this.PossibleItemsCount >= this.m_items.Count && this.m_containers.Count < this.VisibleItemsCount)
			{
				RectTransform value = UnityEngine.Object.Instantiate<RectTransform>(this.ContainerPrefab, this.m_virtualContent);
				this.m_containers.AddLast(value);
				num++;
			}
			if (roundedIndex <= index && index <= num)
			{
				RectTransform value2 = this.m_containers.Last.Value;
				this.m_containers.RemoveLast();
				if (index == roundedIndex)
				{
					this.m_containers.AddFirst(value2);
					value2.SetSiblingIndex(0);
				}
				else
				{
					RectTransform value3 = this.m_containers.ElementAtOrDefault(index - roundedIndex - 1);
					LinkedListNode<RectTransform> node = this.m_containers.Find(value3);
					this.m_containers.AddAfter(node, value2);
					value2.SetSiblingIndex(index - roundedIndex);
				}
				if (raiseItemDataBindingEvent && this.ItemDataBinding != null)
				{
					this.ItemDataBinding(value2, item);
				}
			}
			else if (index < roundedIndex)
			{
				this.UpdateScrollbar(roundedIndex + 1);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000161A4 File Offset: 0x000145A4
		public void RemoveItems(int[] indices, bool raiseItemDataBindingEvent = true)
		{
			int num = this.RoundedIndex;
			indices = (from i in indices
			orderby i
			select i).ToArray<int>();
			for (int j = indices.Length - 1; j >= 0; j--)
			{
				int num2 = indices[j];
				if (num2 >= 0 && num2 < this.m_items.Count)
				{
					this.m_items.RemoveAt(num2);
					if (num2 < num)
					{
						num--;
					}
				}
			}
			if (num + this.VisibleItemsCount >= this.RoundedItemsCount)
			{
				num = Mathf.Max(0, this.RoundedItemsCount - this.VisibleItemsCount);
			}
			this.UpdateContentSize();
			this.UpdateScrollbar(num);
			this.DataBind(num, false);
			this.OnVirtualContentTransformChaged();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00016274 File Offset: 0x00014674
		public void SetNextSibling(object sibling, object nextSibling)
		{
			if (sibling == nextSibling)
			{
				return;
			}
			int num = this.m_items.IndexOf(sibling);
			int num2 = this.m_items.IndexOf(nextSibling);
			int roundedIndex = this.RoundedIndex;
			int num3 = roundedIndex + this.VisibleItemsCount - 1;
			bool flag = roundedIndex <= num2 && num2 <= num3;
			int num4 = num;
			if (num2 > num)
			{
				num4++;
			}
			int num5 = num2 - roundedIndex;
			int num6 = num4 - roundedIndex;
			bool flag2 = roundedIndex <= num4 && ((num5 < 0) ? (num4 < num3) : (num4 <= num3));
			this.m_items.RemoveAt(num2);
			this.m_items.Insert(num4, nextSibling);
			if (flag2)
			{
				if (flag)
				{
					RectTransform rectTransform = this.m_containers.ElementAt(num5);
					this.m_containers.Remove(rectTransform);
					if (num6 == 0)
					{
						this.m_containers.AddFirst(rectTransform);
						rectTransform.SetSiblingIndex(0);
					}
					else
					{
						RectTransform value = this.m_containers.ElementAt(num6 - 1);
						LinkedListNode<RectTransform> node = this.m_containers.Find(value);
						this.m_containers.AddAfter(node, rectTransform);
					}
					rectTransform.SetSiblingIndex(num6);
					if (this.ItemDataBinding != null)
					{
						this.ItemDataBinding(rectTransform, nextSibling);
					}
				}
				else
				{
					RectTransform value2 = this.m_containers.Last.Value;
					this.m_containers.RemoveLast();
					if (num6 == 0)
					{
						this.m_containers.AddFirst(value2);
					}
					else
					{
						RectTransform value3 = (num5 >= 0) ? this.m_containers.ElementAt(num6 - 1) : this.m_containers.ElementAt(num6);
						LinkedListNode<RectTransform> node2 = this.m_containers.Find(value3);
						this.m_containers.AddAfter(node2, value2);
					}
					if (num5 < 0)
					{
						this.UpdateScrollbar(roundedIndex - 1);
						value2.SetSiblingIndex(num6 + 1);
					}
					else
					{
						value2.SetSiblingIndex(num6);
					}
					if (this.ItemDataBinding != null)
					{
						this.ItemDataBinding(value2, nextSibling);
					}
				}
			}
			else if (flag)
			{
				if (num4 < roundedIndex)
				{
					RectTransform rectTransform2 = this.m_containers.ElementAt(num5);
					this.m_containers.Remove(rectTransform2);
					this.m_containers.AddFirst(rectTransform2);
					rectTransform2.SetSiblingIndex(0);
					if (this.ItemDataBinding != null)
					{
						this.ItemDataBinding(rectTransform2, this.m_items[roundedIndex]);
					}
				}
				else if (num4 > num3)
				{
					RectTransform rectTransform3 = this.m_containers.ElementAt(num5);
					this.m_containers.Remove(rectTransform3);
					this.m_containers.AddLast(rectTransform3);
					rectTransform3.SetSiblingIndex(this.m_containers.Count - 1);
					if (this.ItemDataBinding != null)
					{
						this.ItemDataBinding(rectTransform3, this.m_items[num3]);
					}
				}
			}
			else if (num5 < 0)
			{
				this.UpdateScrollbar(roundedIndex - 1);
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00016580 File Offset: 0x00014980
		public void SetPrevSibling(object sibling, object prevSibling)
		{
			int num = this.m_items.IndexOf(sibling);
			num--;
			if (num >= 0)
			{
				sibling = this.m_items[num];
				this.SetNextSibling(sibling, prevSibling);
			}
			else
			{
				RectTransform rectTransform = this.GetContainer(prevSibling);
				int index = this.m_items.IndexOf(prevSibling);
				this.m_items.RemoveAt(index);
				this.m_items.Insert(0, prevSibling);
				if (rectTransform == null)
				{
					rectTransform = this.m_containers.Last.Value;
					this.m_containers.RemoveLast();
				}
				else
				{
					this.m_containers.Remove(rectTransform);
				}
				this.m_containers.AddFirst(rectTransform);
				rectTransform.SetSiblingIndex(0);
				if (this.ItemDataBinding != null)
				{
					this.ItemDataBinding(rectTransform, prevSibling);
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00016654 File Offset: 0x00014A54
		public RectTransform GetContainer(object obj)
		{
			if (this.m_items == null)
			{
				return null;
			}
			int num = this.m_items.IndexOf(obj);
			if (num < 0)
			{
				return null;
			}
			int roundedIndex = this.RoundedIndex;
			int num2 = roundedIndex + this.VisibleItemsCount - 1;
			if (roundedIndex > num || num > num2)
			{
				return null;
			}
			int num3 = num - roundedIndex;
			if (num3 < 0 || num3 >= this.m_containers.Count)
			{
				return null;
			}
			return this.m_containers.ElementAtOrDefault(num - roundedIndex);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000166D2 File Offset: 0x00014AD2
		public RectTransform FirstContainer()
		{
			if (this.m_containers.Count == 0)
			{
				return null;
			}
			return this.m_containers.First.Value;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000166F8 File Offset: 0x00014AF8
		public void ForEachContainer(Action<RectTransform> action)
		{
			if (action == null)
			{
				return;
			}
			foreach (RectTransform obj in this.m_containers)
			{
				action(obj);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001675C File Offset: 0x00014B5C
		public RectTransform LastContainer()
		{
			if (this.m_containers.Count == 0)
			{
				return null;
			}
			return this.m_containers.Last.Value;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00016780 File Offset: 0x00014B80
		private void UpdateScrollbar(int index)
		{
			this.m_normalizedIndex = this.EvalNormalizedIndex(index);
			if (this.m_mode == VirtualizingMode.Vertical)
			{
				base.verticalNormalizedPosition = 1f - this.m_normalizedIndex;
			}
			else if (this.m_mode == VirtualizingMode.Horizontal)
			{
				base.horizontalNormalizedPosition = this.m_normalizedIndex;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000167D4 File Offset: 0x00014BD4
		public void Refresh()
		{
			int roundedIndex = this.RoundedIndex;
			this.DataBind(roundedIndex, false);
		}

		// Token: 0x0400031B RID: 795
		public RectTransform ContainerPrefab;

		// Token: 0x0400031C RID: 796
		[SerializeField]
		private RectTransform m_virtualContent;

		// Token: 0x0400031D RID: 797
		private HorizontalOrVerticalLayoutGroup m_layoutGroup;

		// Token: 0x0400031E RID: 798
		private RectTransformChangeListener m_virtualContentTransformChangeListener;

		// Token: 0x0400031F RID: 799
		[SerializeField]
		private VirtualizingMode m_mode = VirtualizingMode.Vertical;

		// Token: 0x04000320 RID: 800
		[SerializeField]
		private bool m_useGrid;

		// Token: 0x04000321 RID: 801
		[SerializeField]
		private Vector2 m_gridSpacing = Vector2.zero;

		// Token: 0x04000322 RID: 802
		private GridLayoutGroup m_gridLayoutGroup;

		// Token: 0x04000323 RID: 803
		private LinkedList<RectTransform> m_containers = new LinkedList<RectTransform>();

		// Token: 0x04000324 RID: 804
		private IList m_items;

		// Token: 0x04000325 RID: 805
		private float m_normalizedIndex;
	}
}
