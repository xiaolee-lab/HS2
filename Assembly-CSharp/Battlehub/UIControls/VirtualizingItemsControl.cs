using System;
using System.Diagnostics;

namespace Battlehub.UIControls
{
	// Token: 0x0200009E RID: 158
	public class VirtualizingItemsControl<TDataBindingArgs> : VirtualizingItemsControl where TDataBindingArgs : ItemDataBindingArgs, new()
	{
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060002D4 RID: 724 RVA: 0x00014D58 File Offset: 0x00013158
		// (remove) Token: 0x060002D5 RID: 725 RVA: 0x00014D90 File Offset: 0x00013190
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TDataBindingArgs> ItemDataBinding;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060002D6 RID: 726 RVA: 0x00014DC8 File Offset: 0x000131C8
		// (remove) Token: 0x060002D7 RID: 727 RVA: 0x00014E00 File Offset: 0x00013200
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TDataBindingArgs> ItemBeginEdit;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060002D8 RID: 728 RVA: 0x00014E38 File Offset: 0x00013238
		// (remove) Token: 0x060002D9 RID: 729 RVA: 0x00014E70 File Offset: 0x00013270
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TDataBindingArgs> ItemEndEdit;

		// Token: 0x060002DA RID: 730 RVA: 0x00014EA8 File Offset: 0x000132A8
		protected override void OnItemBeginEdit(object sender, EventArgs e)
		{
			if (!base.CanHandleEvent(sender))
			{
				return;
			}
			VirtualizingItemContainer virtualizingItemContainer = (VirtualizingItemContainer)sender;
			if (this.ItemBeginEdit != null)
			{
				TDataBindingArgs e2 = Activator.CreateInstance<TDataBindingArgs>();
				e2.Item = virtualizingItemContainer.Item;
				e2.ItemPresenter = ((!(virtualizingItemContainer.ItemPresenter == null)) ? virtualizingItemContainer.ItemPresenter : base.gameObject);
				e2.EditorPresenter = ((!(virtualizingItemContainer.EditorPresenter == null)) ? virtualizingItemContainer.EditorPresenter : base.gameObject);
				this.ItemBeginEdit(this, e2);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00014F58 File Offset: 0x00013358
		protected override void OnItemEndEdit(object sender, EventArgs e)
		{
			if (!base.CanHandleEvent(sender))
			{
				return;
			}
			VirtualizingItemContainer virtualizingItemContainer = (VirtualizingItemContainer)sender;
			if (this.ItemBeginEdit != null)
			{
				TDataBindingArgs e2 = Activator.CreateInstance<TDataBindingArgs>();
				e2.Item = virtualizingItemContainer.Item;
				e2.ItemPresenter = ((!(virtualizingItemContainer.ItemPresenter == null)) ? virtualizingItemContainer.ItemPresenter : base.gameObject);
				e2.EditorPresenter = ((!(virtualizingItemContainer.EditorPresenter == null)) ? virtualizingItemContainer.EditorPresenter : base.gameObject);
				this.ItemEndEdit(this, e2);
			}
			base.IsSelected = true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00015010 File Offset: 0x00013410
		public override void DataBindItem(object item, VirtualizingItemContainer itemContainer)
		{
			TDataBindingArgs args = Activator.CreateInstance<TDataBindingArgs>();
			args.Item = item;
			args.ItemPresenter = ((!(itemContainer.ItemPresenter == null)) ? itemContainer.ItemPresenter : base.gameObject);
			args.EditorPresenter = ((!(itemContainer.EditorPresenter == null)) ? itemContainer.EditorPresenter : base.gameObject);
			itemContainer.Clear();
			if (item != null)
			{
				this.RaiseItemDataBinding(args);
				itemContainer.CanEdit = args.CanEdit;
				itemContainer.CanDrag = args.CanDrag;
				itemContainer.CanBeParent = args.CanBeParent;
			}
			else
			{
				itemContainer.CanEdit = false;
				itemContainer.CanDrag = false;
				itemContainer.CanBeParent = false;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000150F5 File Offset: 0x000134F5
		protected void RaiseItemDataBinding(TDataBindingArgs args)
		{
			if (this.ItemDataBinding != null)
			{
				this.ItemDataBinding(this, args);
			}
		}
	}
}
