using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AIProject.UI
{
	// Token: 0x02000FA1 RID: 4001
	public abstract class MenuUIBehaviour : SerializedMonoBehaviour
	{
		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x0600855F RID: 34143 RVA: 0x001DC1BC File Offset: 0x001DA5BC
		// (set) Token: 0x06008560 RID: 34144 RVA: 0x001DC26F File Offset: 0x001DA66F
		public bool EnabledInput
		{
			get
			{
				Game game = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance;
				return this._enabledInput.Value && (((game != null) ? game.MapShortcutUI : null) == null || ((game != null) ? game.MapShortcutUI : null) == this) && ((game != null) ? game.Config : null) == null && ((game != null) ? game.Dialog : null) == null && ((game != null) ? game.ExitScene : null) == null;
			}
			set
			{
				this._enabledInput.Value = value;
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06008561 RID: 34145 RVA: 0x001DC280 File Offset: 0x001DA680
		public int FocusLevel
		{
			[CompilerGenerated]
			get
			{
				return this._focusLevel = Mathf.Max(0, this._focusLevel);
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x06008562 RID: 34146 RVA: 0x001DC2A2 File Offset: 0x001DA6A2
		// (set) Token: 0x06008563 RID: 34147 RVA: 0x001DC2AF File Offset: 0x001DA6AF
		public virtual bool IsActiveControl
		{
			get
			{
				return this._isActive.Value;
			}
			set
			{
				this._isActive.Value = value;
			}
		}

		// Token: 0x06008564 RID: 34148 RVA: 0x001DC2BD File Offset: 0x001DA6BD
		protected IObservable<bool> OnActiveChangedAsObservable()
		{
			if (this._activeChanged == null)
			{
				this._activeChanged = this._isActive.TakeUntilDestroy(base.gameObject).Publish<bool>();
				this._activeChanged.Connect();
			}
			return this._activeChanged;
		}

		// Token: 0x06008565 RID: 34149 RVA: 0x001DC2F8 File Offset: 0x001DA6F8
		protected virtual void Awake()
		{
		}

		// Token: 0x06008566 RID: 34150 RVA: 0x001DC2FC File Offset: 0x001DA6FC
		protected virtual void Start()
		{
			this.OnBeforeStart();
			foreach (ActionIDDownCommand item in this._actionCommands)
			{
				this._commands.Add(item);
			}
			foreach (KeyCodeDownCommand item2 in this._keyCommands)
			{
				this._commands.Add(item2);
			}
			this.OnAfterStart();
		}

		// Token: 0x06008567 RID: 34151 RVA: 0x001DC3BC File Offset: 0x001DA7BC
		protected virtual void OnBeforeStart()
		{
		}

		// Token: 0x06008568 RID: 34152 RVA: 0x001DC3BE File Offset: 0x001DA7BE
		protected virtual void OnAfterStart()
		{
		}

		// Token: 0x06008569 RID: 34153 RVA: 0x001DC3C0 File Offset: 0x001DA7C0
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600856A RID: 34154 RVA: 0x001DC3C2 File Offset: 0x001DA7C2
		protected virtual void OnDisable()
		{
		}

		// Token: 0x0600856B RID: 34155 RVA: 0x001DC3C4 File Offset: 0x001DA7C4
		public virtual void OnInputMoveDirection(MoveDirection moveDir)
		{
		}

		// Token: 0x0600856C RID: 34156 RVA: 0x001DC3C6 File Offset: 0x001DA7C6
		public virtual void OnInputSubMoveDirection(MoveDirection moveDir)
		{
		}

		// Token: 0x0600856D RID: 34157 RVA: 0x001DC3C8 File Offset: 0x001DA7C8
		public void OnUpdateInput(Manager.Input instance)
		{
			foreach (ICommandData commandData in this._commands)
			{
				commandData.Invoke(instance);
			}
		}

		// Token: 0x0600856E RID: 34158 RVA: 0x001DC424 File Offset: 0x001DA824
		public void SetFocusLevel(int level)
		{
			this._focusLevel = level;
		}

		// Token: 0x04006BDC RID: 27612
		protected BoolReactiveProperty _enabledInput = new BoolReactiveProperty(false);

		// Token: 0x04006BDD RID: 27613
		[SerializeField]
		[MinValue(0.0)]
		protected int _focusLevel;

		// Token: 0x04006BDE RID: 27614
		[SerializeField]
		protected float _alphaAccelerationTime = 0.1f;

		// Token: 0x04006BDF RID: 27615
		[SerializeField]
		protected float _followAccelerationTime = 0.025f;

		// Token: 0x04006BE0 RID: 27616
		protected List<ICommandData> _commands = new List<ICommandData>();

		// Token: 0x04006BE1 RID: 27617
		[SerializeField]
		protected List<ActionIDDownCommand> _actionCommands = new List<ActionIDDownCommand>();

		// Token: 0x04006BE2 RID: 27618
		[SerializeField]
		protected List<KeyCodeDownCommand> _keyCommands = new List<KeyCodeDownCommand>();

		// Token: 0x04006BE3 RID: 27619
		protected BoolReactiveProperty _isActive = new BoolReactiveProperty(false);

		// Token: 0x04006BE4 RID: 27620
		private IConnectableObservable<bool> _activeChanged;
	}
}
