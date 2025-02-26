using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000EFB RID: 3835
public class UndoRedoMgr : Singleton<UndoRedoMgr>
{
	// Token: 0x140000CF RID: 207
	// (add) Token: 0x06007D31 RID: 32049 RVA: 0x00358D88 File Offset: 0x00357188
	// (remove) Token: 0x06007D32 RID: 32050 RVA: 0x00358DC0 File Offset: 0x003571C0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EventHandler CanUndoChange;

	// Token: 0x140000D0 RID: 208
	// (add) Token: 0x06007D33 RID: 32051 RVA: 0x00358DF8 File Offset: 0x003571F8
	// (remove) Token: 0x06007D34 RID: 32052 RVA: 0x00358E30 File Offset: 0x00357230
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EventHandler CanRedoChange;

	// Token: 0x1700188E RID: 6286
	// (get) Token: 0x06007D35 RID: 32053 RVA: 0x00358E66 File Offset: 0x00357266
	// (set) Token: 0x06007D36 RID: 32054 RVA: 0x00358E6E File Offset: 0x0035726E
	public bool CanUndo
	{
		get
		{
			return this._CanUndo;
		}
		private set
		{
			if (this._CanUndo != value)
			{
				this._CanUndo = value;
				if (this.CanUndoChange != null)
				{
					this.CanUndoChange(this, EventArgs.Empty);
				}
			}
		}
	}

	// Token: 0x1700188F RID: 6287
	// (get) Token: 0x06007D37 RID: 32055 RVA: 0x00358E9F File Offset: 0x0035729F
	// (set) Token: 0x06007D38 RID: 32056 RVA: 0x00358EA7 File Offset: 0x003572A7
	public bool CanRedo
	{
		get
		{
			return this._CanRedo;
		}
		private set
		{
			if (this._CanRedo != value)
			{
				this._CanRedo = value;
				if (this.CanRedoChange != null)
				{
					this.CanRedoChange(this, EventArgs.Empty);
				}
			}
		}
	}

	// Token: 0x06007D39 RID: 32057 RVA: 0x00358ED8 File Offset: 0x003572D8
	public void Undo()
	{
		if (this.undo.Count <= 0)
		{
			return;
		}
		ICommand command = this.undo.Pop();
		this.CanUndo = (this.undo.Count > 0);
		command.Undo();
		this.redo.Push(command);
		this.CanRedo = true;
	}

	// Token: 0x06007D3A RID: 32058 RVA: 0x00358F30 File Offset: 0x00357330
	public void Redo()
	{
		if (this.redo.Count <= 0)
		{
			return;
		}
		ICommand command = this.redo.Pop();
		this.CanRedo = (this.redo.Count > 0);
		command.Redo();
		this.undo.Push(command);
		this.CanUndo = true;
	}

	// Token: 0x06007D3B RID: 32059 RVA: 0x00358F88 File Offset: 0x00357388
	public void Push(ICommand _command)
	{
		if (_command == null)
		{
			return;
		}
		if (this.bLimit && this.undo.Count >= 100)
		{
			ICommand[] array = this.undo.ToArray();
			this.undo.Clear();
			for (int i = array.Length - 2; i > -1; i--)
			{
				this.undo.Push(array[i]);
			}
		}
		this.undo.Push(_command);
		this.CanUndo = true;
		this.redo.Clear();
		this.CanRedo = false;
	}

	// Token: 0x06007D3C RID: 32060 RVA: 0x00359019 File Offset: 0x00357419
	public void Clear()
	{
		this.undo.Clear();
		this.redo.Clear();
		this.CanUndo = false;
		this.CanRedo = false;
	}

	// Token: 0x04006541 RID: 25921
	private Stack<ICommand> undo = new Stack<ICommand>();

	// Token: 0x04006542 RID: 25922
	private Stack<ICommand> redo = new Stack<ICommand>();

	// Token: 0x04006543 RID: 25923
	private bool _CanUndo;

	// Token: 0x04006544 RID: 25924
	private bool _CanRedo;

	// Token: 0x04006545 RID: 25925
	private const int MaxUndoRedoCnt = 100;

	// Token: 0x04006546 RID: 25926
	[SerializeField]
	private bool bLimit;

	// Token: 0x02000EFC RID: 3836
	private class Command : ICommand
	{
		// Token: 0x06007D3D RID: 32061 RVA: 0x0035903F File Offset: 0x0035743F
		public Command(Delegate _doMethod, object[] _doParamater, Delegate _undoMethod, object[] _undoParamater)
		{
			this.doMethod = _doMethod;
			this.doParamater = _doParamater;
			this.undoMethod = _undoMethod;
			this.undoParamater = _undoParamater;
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x00359064 File Offset: 0x00357464
		public void Undo()
		{
			this.undoMethod.DynamicInvoke(this.undoParamater);
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x00359078 File Offset: 0x00357478
		public void Redo()
		{
			this.doMethod.DynamicInvoke(this.doParamater);
		}

		// Token: 0x04006549 RID: 25929
		private Delegate doMethod;

		// Token: 0x0400654A RID: 25930
		private Delegate undoMethod;

		// Token: 0x0400654B RID: 25931
		private object[] doParamater;

		// Token: 0x0400654C RID: 25932
		private object[] undoParamater;
	}
}
