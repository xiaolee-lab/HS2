using System;
using System.Diagnostics;

namespace Battlehub.UIControls
{
	// Token: 0x02000089 RID: 137
	public class ItemsControl<TDataBindingArgs> : ItemsControl where TDataBindingArgs : ItemDataBindingArgs, new()
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000188 RID: 392 RVA: 0x0000DB2C File Offset: 0x0000BF2C
		// (remove) Token: 0x06000189 RID: 393 RVA: 0x0000DB64 File Offset: 0x0000BF64
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TDataBindingArgs> ItemDataBinding;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600018A RID: 394 RVA: 0x0000DB9C File Offset: 0x0000BF9C
		// (remove) Token: 0x0600018B RID: 395 RVA: 0x0000DBD4 File Offset: 0x0000BFD4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TDataBindingArgs> ItemBeginEdit;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600018C RID: 396 RVA: 0x0000DC0C File Offset: 0x0000C00C
		// (remove) Token: 0x0600018D RID: 397 RVA: 0x0000DC44 File Offset: 0x0000C044
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TDataBindingArgs> ItemEndEdit;

		// Token: 0x0600018E RID: 398 RVA: 0x0000DC7C File Offset: 0x0000C07C
		protected override void OnItemBeginEdit(object sender, EventArgs e)
		{
			if (!base.CanHandleEvent(sender))
			{
				return;
			}
			ItemContainer itemContainer = (ItemContainer)sender;
			if (this.ItemBeginEdit != null)
			{
				TDataBindingArgs e2 = Activator.CreateInstance<TDataBindingArgs>();
				e2.Item = itemContainer.Item;
				e2.ItemPresenter = ((!(itemContainer.ItemPresenter == null)) ? itemContainer.ItemPresenter : base.gameObject);
				e2.EditorPresenter = ((!(itemContainer.EditorPresenter == null)) ? itemContainer.EditorPresenter : base.gameObject);
				this.ItemBeginEdit(this, e2);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000DD2C File Offset: 0x0000C12C
		protected override void OnItemEndEdit(object sender, EventArgs e)
		{
			if (!base.CanHandleEvent(sender))
			{
				return;
			}
			ItemContainer itemContainer = (ItemContainer)sender;
			if (this.ItemBeginEdit != null)
			{
				TDataBindingArgs e2 = Activator.CreateInstance<TDataBindingArgs>();
				e2.Item = itemContainer.Item;
				e2.ItemPresenter = ((!(itemContainer.ItemPresenter == null)) ? itemContainer.ItemPresenter : base.gameObject);
				e2.EditorPresenter = ((!(itemContainer.EditorPresenter == null)) ? itemContainer.EditorPresenter : base.gameObject);
				this.ItemEndEdit(this, e2);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000DDDC File Offset: 0x0000C1DC
		public override void DataBindItem(object item, ItemContainer itemContainer)
		{
			TDataBindingArgs args = Activator.CreateInstance<TDataBindingArgs>();
			args.Item = item;
			args.ItemPresenter = ((!(itemContainer.ItemPresenter == null)) ? itemContainer.ItemPresenter : base.gameObject);
			args.EditorPresenter = ((!(itemContainer.EditorPresenter == null)) ? itemContainer.EditorPresenter : base.gameObject);
			this.RaiseItemDataBinding(args);
			itemContainer.CanEdit = args.CanEdit;
			itemContainer.CanDrag = args.CanDrag;
			itemContainer.CanDrop = args.CanBeParent;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000DE9B File Offset: 0x0000C29B
		protected void RaiseItemDataBinding(TDataBindingArgs args)
		{
			if (this.ItemDataBinding != null)
			{
				this.ItemDataBinding(this, args);
			}
		}
	}
}
