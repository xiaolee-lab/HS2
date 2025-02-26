using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001285 RID: 4741
	public class UndoRedoManager : Singleton<UndoRedoManager>
	{
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06009CF7 RID: 40183 RVA: 0x0040335C File Offset: 0x0040175C
		// (remove) Token: 0x06009CF8 RID: 40184 RVA: 0x00403394 File Offset: 0x00401794
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CanUndoChange;

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06009CF9 RID: 40185 RVA: 0x004033CC File Offset: 0x004017CC
		// (remove) Token: 0x06009CFA RID: 40186 RVA: 0x00403404 File Offset: 0x00401804
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CanRedoChange;

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06009CFB RID: 40187 RVA: 0x0040343A File Offset: 0x0040183A
		// (set) Token: 0x06009CFC RID: 40188 RVA: 0x00403442 File Offset: 0x00401842
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
						this.CanUndoChange(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06009CFD RID: 40189 RVA: 0x00403473 File Offset: 0x00401873
		// (set) Token: 0x06009CFE RID: 40190 RVA: 0x0040347B File Offset: 0x0040187B
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
						this.CanRedoChange(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x06009CFF RID: 40191 RVA: 0x004034AC File Offset: 0x004018AC
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

		// Token: 0x06009D00 RID: 40192 RVA: 0x004034E0 File Offset: 0x004018E0
		public void Do(Delegate _doMethod, object[] _doParamater, Delegate _undoMethod, object[] _undoParamater)
		{
			UndoRedoManager.Command command = new UndoRedoManager.Command(_doMethod, _doParamater, _undoMethod, _undoParamater);
			this.Do(command);
		}

		// Token: 0x06009D01 RID: 40193 RVA: 0x004034FF File Offset: 0x004018FF
		public void Do()
		{
			if (this.undo.Count <= 0)
			{
				return;
			}
			this.Do(this.undo.Peek());
		}

		// Token: 0x06009D02 RID: 40194 RVA: 0x00403524 File Offset: 0x00401924
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

		// Token: 0x06009D03 RID: 40195 RVA: 0x0040357C File Offset: 0x0040197C
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

		// Token: 0x06009D04 RID: 40196 RVA: 0x004035D4 File Offset: 0x004019D4
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

		// Token: 0x06009D05 RID: 40197 RVA: 0x00403602 File Offset: 0x00401A02
		public void Clear()
		{
			this.undo.Clear();
			this.redo.Clear();
			this.CanUndo = false;
			this.CanRedo = false;
		}

		// Token: 0x06009D06 RID: 40198 RVA: 0x00403628 File Offset: 0x00401A28
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x04007CE1 RID: 31969
		private Stack<ICommand> undo = new Stack<ICommand>();

		// Token: 0x04007CE2 RID: 31970
		private Stack<ICommand> redo = new Stack<ICommand>();

		// Token: 0x04007CE3 RID: 31971
		private bool m_CanUndo;

		// Token: 0x04007CE4 RID: 31972
		private bool m_CanRedo;

		// Token: 0x02001286 RID: 4742
		private class Command : ICommand
		{
			// Token: 0x06009D07 RID: 40199 RVA: 0x00403641 File Offset: 0x00401A41
			public Command(Delegate _doMethod, object[] _doParamater, Delegate _undoMethod, object[] _undoParamater)
			{
				this.doMethod = _doMethod;
				this.doParamater = _doParamater;
				this.undoMethod = _undoMethod;
				this.undoParamater = _undoParamater;
			}

			// Token: 0x06009D08 RID: 40200 RVA: 0x00403666 File Offset: 0x00401A66
			public void Do()
			{
				this.doMethod.DynamicInvoke(this.doParamater);
			}

			// Token: 0x06009D09 RID: 40201 RVA: 0x0040367A File Offset: 0x00401A7A
			public void Undo()
			{
				this.undoMethod.DynamicInvoke(this.undoParamater);
			}

			// Token: 0x06009D0A RID: 40202 RVA: 0x0040368E File Offset: 0x00401A8E
			public void Redo()
			{
				this.doMethod.DynamicInvoke(this.doParamater);
			}

			// Token: 0x04007CE7 RID: 31975
			private Delegate doMethod;

			// Token: 0x04007CE8 RID: 31976
			private Delegate undoMethod;

			// Token: 0x04007CE9 RID: 31977
			private object[] doParamater;

			// Token: 0x04007CEA RID: 31978
			private object[] undoParamater;
		}
	}
}
