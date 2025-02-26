using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Housing
{
	// Token: 0x020008CC RID: 2252
	public class UndoRedoManager : Singleton<UndoRedoManager>
	{
		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06003AE9 RID: 15081 RVA: 0x0015809C File Offset: 0x0015649C
		// (remove) Token: 0x06003AEA RID: 15082 RVA: 0x001580D4 File Offset: 0x001564D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<CanhangeArgs> CanUndoChange;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06003AEB RID: 15083 RVA: 0x0015810C File Offset: 0x0015650C
		// (remove) Token: 0x06003AEC RID: 15084 RVA: 0x00158144 File Offset: 0x00156544
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<CanhangeArgs> CanRedoChange;

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x0015817A File Offset: 0x0015657A
		// (set) Token: 0x06003AEE RID: 15086 RVA: 0x00158182 File Offset: 0x00156582
		public bool CanUndo
		{
			get
			{
				return this.m_CanUndo;
			}
			private set
			{
				if (this.m_CanUndo != value)
				{
					this.m_CanUndo = value;
					if (this.CanUndoChange != null)
					{
						this.CanUndoChange(this, new CanhangeArgs(value));
					}
				}
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x001581B4 File Offset: 0x001565B4
		// (set) Token: 0x06003AF0 RID: 15088 RVA: 0x001581BC File Offset: 0x001565BC
		public bool CanRedo
		{
			get
			{
				return this.m_CanRedo;
			}
			private set
			{
				if (this.m_CanRedo != value)
				{
					this.m_CanRedo = value;
					if (this.CanRedoChange != null)
					{
						this.CanRedoChange(this, new CanhangeArgs(value));
					}
				}
			}
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x001581EE File Offset: 0x001565EE
		public void Do(ICommand _command)
		{
			if (_command == null)
			{
				return;
			}
			this.undo.Push(_command);
			this.CanUndo = true;
			_command.Do();
			this.redo.Clear();
			this.CanRedo = false;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x00158224 File Offset: 0x00156624
		public void Do(Delegate _doMethod, object[] _doParamater, Delegate _undoMethod, object[] _undoParamater)
		{
			UndoRedoManager.Command command = new UndoRedoManager.Command(_doMethod, _doParamater, _undoMethod, _undoParamater);
			this.Do(command);
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x00158243 File Offset: 0x00156643
		public void Do()
		{
			if (this.undo.Count <= 0)
			{
				return;
			}
			this.Do(this.undo.Peek());
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x00158268 File Offset: 0x00156668
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

		// Token: 0x06003AF5 RID: 15093 RVA: 0x001582C0 File Offset: 0x001566C0
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

		// Token: 0x06003AF6 RID: 15094 RVA: 0x00158318 File Offset: 0x00156718
		public void Push(ICommand _command)
		{
			if (_command == null)
			{
				return;
			}
			this.undo.Push(_command);
			this.CanUndo = true;
			this.redo.Clear();
			this.CanRedo = false;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x00158346 File Offset: 0x00156746
		public void Clear()
		{
			this.undo.Clear();
			this.redo.Clear();
			this.CanUndo = false;
			this.CanRedo = false;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x0015836C File Offset: 0x0015676C
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
		}

		// Token: 0x04003A07 RID: 14855
		private Stack<ICommand> undo = new Stack<ICommand>();

		// Token: 0x04003A08 RID: 14856
		private Stack<ICommand> redo = new Stack<ICommand>();

		// Token: 0x04003A09 RID: 14857
		private bool m_CanUndo;

		// Token: 0x04003A0A RID: 14858
		private bool m_CanRedo;

		// Token: 0x020008CD RID: 2253
		private class Command : ICommand
		{
			// Token: 0x06003AF9 RID: 15097 RVA: 0x0015837A File Offset: 0x0015677A
			public Command(Delegate _doMethod, object[] _doParamater, Delegate _undoMethod, object[] _undoParamater)
			{
				this.doMethod = _doMethod;
				this.doParamater = _doParamater;
				this.undoMethod = _undoMethod;
				this.undoParamater = _undoParamater;
			}

			// Token: 0x06003AFA RID: 15098 RVA: 0x0015839F File Offset: 0x0015679F
			public void Do()
			{
				this.doMethod.DynamicInvoke(this.doParamater);
			}

			// Token: 0x06003AFB RID: 15099 RVA: 0x001583B3 File Offset: 0x001567B3
			public void Undo()
			{
				this.undoMethod.DynamicInvoke(this.undoParamater);
			}

			// Token: 0x06003AFC RID: 15100 RVA: 0x001583C7 File Offset: 0x001567C7
			public void Redo()
			{
				this.doMethod.DynamicInvoke(this.doParamater);
			}

			// Token: 0x04003A0D RID: 14861
			private Delegate doMethod;

			// Token: 0x04003A0E RID: 14862
			private Delegate undoMethod;

			// Token: 0x04003A0F RID: 14863
			private object[] doParamater;

			// Token: 0x04003A10 RID: 14864
			private object[] undoParamater;
		}
	}
}
