using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200052D RID: 1325
	[AddComponentMenu("")]
	public class ControlMapper : MonoBehaviour
	{
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06001989 RID: 6537 RVA: 0x0009E3DF File Offset: 0x0009C7DF
		// (remove) Token: 0x0600198A RID: 6538 RVA: 0x0009E3F8 File Offset: 0x0009C7F8
		public event Action ScreenClosedEvent
		{
			add
			{
				this._ScreenClosedEvent = (Action)Delegate.Combine(this._ScreenClosedEvent, value);
			}
			remove
			{
				this._ScreenClosedEvent = (Action)Delegate.Remove(this._ScreenClosedEvent, value);
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x0600198B RID: 6539 RVA: 0x0009E411 File Offset: 0x0009C811
		// (remove) Token: 0x0600198C RID: 6540 RVA: 0x0009E42A File Offset: 0x0009C82A
		public event Action ScreenOpenedEvent
		{
			add
			{
				this._ScreenOpenedEvent = (Action)Delegate.Combine(this._ScreenOpenedEvent, value);
			}
			remove
			{
				this._ScreenOpenedEvent = (Action)Delegate.Remove(this._ScreenOpenedEvent, value);
			}
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x0600198D RID: 6541 RVA: 0x0009E443 File Offset: 0x0009C843
		// (remove) Token: 0x0600198E RID: 6542 RVA: 0x0009E45C File Offset: 0x0009C85C
		public event Action PopupWindowClosedEvent
		{
			add
			{
				this._PopupWindowClosedEvent = (Action)Delegate.Combine(this._PopupWindowClosedEvent, value);
			}
			remove
			{
				this._PopupWindowClosedEvent = (Action)Delegate.Remove(this._PopupWindowClosedEvent, value);
			}
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x0600198F RID: 6543 RVA: 0x0009E475 File Offset: 0x0009C875
		// (remove) Token: 0x06001990 RID: 6544 RVA: 0x0009E48E File Offset: 0x0009C88E
		public event Action PopupWindowOpenedEvent
		{
			add
			{
				this._PopupWindowOpenedEvent = (Action)Delegate.Combine(this._PopupWindowOpenedEvent, value);
			}
			remove
			{
				this._PopupWindowOpenedEvent = (Action)Delegate.Remove(this._PopupWindowOpenedEvent, value);
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06001991 RID: 6545 RVA: 0x0009E4A7 File Offset: 0x0009C8A7
		// (remove) Token: 0x06001992 RID: 6546 RVA: 0x0009E4C0 File Offset: 0x0009C8C0
		public event Action InputPollingStartedEvent
		{
			add
			{
				this._InputPollingStartedEvent = (Action)Delegate.Combine(this._InputPollingStartedEvent, value);
			}
			remove
			{
				this._InputPollingStartedEvent = (Action)Delegate.Remove(this._InputPollingStartedEvent, value);
			}
		}

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06001993 RID: 6547 RVA: 0x0009E4D9 File Offset: 0x0009C8D9
		// (remove) Token: 0x06001994 RID: 6548 RVA: 0x0009E4F2 File Offset: 0x0009C8F2
		public event Action InputPollingEndedEvent
		{
			add
			{
				this._InputPollingEndedEvent = (Action)Delegate.Combine(this._InputPollingEndedEvent, value);
			}
			remove
			{
				this._InputPollingEndedEvent = (Action)Delegate.Remove(this._InputPollingEndedEvent, value);
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06001995 RID: 6549 RVA: 0x0009E50B File Offset: 0x0009C90B
		// (remove) Token: 0x06001996 RID: 6550 RVA: 0x0009E519 File Offset: 0x0009C919
		public event UnityAction onScreenClosed
		{
			add
			{
				this._onScreenClosed.AddListener(value);
			}
			remove
			{
				this._onScreenClosed.RemoveListener(value);
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06001997 RID: 6551 RVA: 0x0009E527 File Offset: 0x0009C927
		// (remove) Token: 0x06001998 RID: 6552 RVA: 0x0009E535 File Offset: 0x0009C935
		public event UnityAction onScreenOpened
		{
			add
			{
				this._onScreenOpened.AddListener(value);
			}
			remove
			{
				this._onScreenOpened.RemoveListener(value);
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06001999 RID: 6553 RVA: 0x0009E543 File Offset: 0x0009C943
		// (remove) Token: 0x0600199A RID: 6554 RVA: 0x0009E551 File Offset: 0x0009C951
		public event UnityAction onPopupWindowClosed
		{
			add
			{
				this._onPopupWindowClosed.AddListener(value);
			}
			remove
			{
				this._onPopupWindowClosed.RemoveListener(value);
			}
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x0600199B RID: 6555 RVA: 0x0009E55F File Offset: 0x0009C95F
		// (remove) Token: 0x0600199C RID: 6556 RVA: 0x0009E56D File Offset: 0x0009C96D
		public event UnityAction onPopupWindowOpened
		{
			add
			{
				this._onPopupWindowOpened.AddListener(value);
			}
			remove
			{
				this._onPopupWindowOpened.RemoveListener(value);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x0600199D RID: 6557 RVA: 0x0009E57B File Offset: 0x0009C97B
		// (remove) Token: 0x0600199E RID: 6558 RVA: 0x0009E589 File Offset: 0x0009C989
		public event UnityAction onInputPollingStarted
		{
			add
			{
				this._onInputPollingStarted.AddListener(value);
			}
			remove
			{
				this._onInputPollingStarted.RemoveListener(value);
			}
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x0600199F RID: 6559 RVA: 0x0009E597 File Offset: 0x0009C997
		// (remove) Token: 0x060019A0 RID: 6560 RVA: 0x0009E5A5 File Offset: 0x0009C9A5
		public event UnityAction onInputPollingEnded
		{
			add
			{
				this._onInputPollingEnded.AddListener(value);
			}
			remove
			{
				this._onInputPollingEnded.RemoveListener(value);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0009E5B3 File Offset: 0x0009C9B3
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x0009E5BB File Offset: 0x0009C9BB
		public InputManager rewiredInputManager
		{
			get
			{
				return this._rewiredInputManager;
			}
			set
			{
				this._rewiredInputManager = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x0009E5CB File Offset: 0x0009C9CB
		// (set) Token: 0x060019A4 RID: 6564 RVA: 0x0009E5D3 File Offset: 0x0009C9D3
		public bool dontDestroyOnLoad
		{
			get
			{
				return this._dontDestroyOnLoad;
			}
			set
			{
				if (value != this._dontDestroyOnLoad && value)
				{
					UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
				}
				this._dontDestroyOnLoad = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0009E5FE File Offset: 0x0009C9FE
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x0009E606 File Offset: 0x0009CA06
		public int keyboardMapDefaultLayout
		{
			get
			{
				return this._keyboardMapDefaultLayout;
			}
			set
			{
				this._keyboardMapDefaultLayout = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0009E616 File Offset: 0x0009CA16
		// (set) Token: 0x060019A8 RID: 6568 RVA: 0x0009E61E File Offset: 0x0009CA1E
		public int mouseMapDefaultLayout
		{
			get
			{
				return this._mouseMapDefaultLayout;
			}
			set
			{
				this._mouseMapDefaultLayout = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0009E62E File Offset: 0x0009CA2E
		// (set) Token: 0x060019AA RID: 6570 RVA: 0x0009E636 File Offset: 0x0009CA36
		public int joystickMapDefaultLayout
		{
			get
			{
				return this._joystickMapDefaultLayout;
			}
			set
			{
				this._joystickMapDefaultLayout = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0009E646 File Offset: 0x0009CA46
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x0009E663 File Offset: 0x0009CA63
		public bool showPlayers
		{
			get
			{
				return this._showPlayers && ReInput.players.playerCount > 1;
			}
			set
			{
				this._showPlayers = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0009E673 File Offset: 0x0009CA73
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x0009E67B File Offset: 0x0009CA7B
		public bool showControllers
		{
			get
			{
				return this._showControllers;
			}
			set
			{
				this._showControllers = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0009E68B File Offset: 0x0009CA8B
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x0009E693 File Offset: 0x0009CA93
		public bool showKeyboard
		{
			get
			{
				return this._showKeyboard;
			}
			set
			{
				this._showKeyboard = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0009E6A3 File Offset: 0x0009CAA3
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x0009E6AB File Offset: 0x0009CAAB
		public bool showMouse
		{
			get
			{
				return this._showMouse;
			}
			set
			{
				this._showMouse = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0009E6BB File Offset: 0x0009CABB
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x0009E6C3 File Offset: 0x0009CAC3
		public int maxControllersPerPlayer
		{
			get
			{
				return this._maxControllersPerPlayer;
			}
			set
			{
				this._maxControllersPerPlayer = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0009E6D3 File Offset: 0x0009CAD3
		// (set) Token: 0x060019B6 RID: 6582 RVA: 0x0009E6DB File Offset: 0x0009CADB
		public bool showActionCategoryLabels
		{
			get
			{
				return this._showActionCategoryLabels;
			}
			set
			{
				this._showActionCategoryLabels = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0009E6EB File Offset: 0x0009CAEB
		// (set) Token: 0x060019B8 RID: 6584 RVA: 0x0009E6F3 File Offset: 0x0009CAF3
		public int keyboardInputFieldCount
		{
			get
			{
				return this._keyboardInputFieldCount;
			}
			set
			{
				this._keyboardInputFieldCount = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0009E703 File Offset: 0x0009CB03
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x0009E70B File Offset: 0x0009CB0B
		public int mouseInputFieldCount
		{
			get
			{
				return this._mouseInputFieldCount;
			}
			set
			{
				this._mouseInputFieldCount = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0009E71B File Offset: 0x0009CB1B
		// (set) Token: 0x060019BC RID: 6588 RVA: 0x0009E723 File Offset: 0x0009CB23
		public int controllerInputFieldCount
		{
			get
			{
				return this._controllerInputFieldCount;
			}
			set
			{
				this._controllerInputFieldCount = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0009E733 File Offset: 0x0009CB33
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x0009E73B File Offset: 0x0009CB3B
		public bool showFullAxisInputFields
		{
			get
			{
				return this._showFullAxisInputFields;
			}
			set
			{
				this._showFullAxisInputFields = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0009E74B File Offset: 0x0009CB4B
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0009E753 File Offset: 0x0009CB53
		public bool showSplitAxisInputFields
		{
			get
			{
				return this._showSplitAxisInputFields;
			}
			set
			{
				this._showSplitAxisInputFields = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0009E763 File Offset: 0x0009CB63
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0009E76B File Offset: 0x0009CB6B
		public bool allowElementAssignmentConflicts
		{
			get
			{
				return this._allowElementAssignmentConflicts;
			}
			set
			{
				this._allowElementAssignmentConflicts = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0009E77B File Offset: 0x0009CB7B
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x0009E783 File Offset: 0x0009CB83
		public bool allowElementAssignmentSwap
		{
			get
			{
				return this._allowElementAssignmentSwap;
			}
			set
			{
				this._allowElementAssignmentSwap = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0009E793 File Offset: 0x0009CB93
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x0009E79B File Offset: 0x0009CB9B
		public int actionLabelWidth
		{
			get
			{
				return this._actionLabelWidth;
			}
			set
			{
				this._actionLabelWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0009E7AB File Offset: 0x0009CBAB
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0009E7B3 File Offset: 0x0009CBB3
		public int keyboardColMaxWidth
		{
			get
			{
				return this._keyboardColMaxWidth;
			}
			set
			{
				this._keyboardColMaxWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0009E7C3 File Offset: 0x0009CBC3
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x0009E7CB File Offset: 0x0009CBCB
		public int mouseColMaxWidth
		{
			get
			{
				return this._mouseColMaxWidth;
			}
			set
			{
				this._mouseColMaxWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x0009E7DB File Offset: 0x0009CBDB
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x0009E7E3 File Offset: 0x0009CBE3
		public int controllerColMaxWidth
		{
			get
			{
				return this._controllerColMaxWidth;
			}
			set
			{
				this._controllerColMaxWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x0009E7F3 File Offset: 0x0009CBF3
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0009E7FB File Offset: 0x0009CBFB
		public int inputRowHeight
		{
			get
			{
				return this._inputRowHeight;
			}
			set
			{
				this._inputRowHeight = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0009E80B File Offset: 0x0009CC0B
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x0009E813 File Offset: 0x0009CC13
		public int inputColumnSpacing
		{
			get
			{
				return this._inputColumnSpacing;
			}
			set
			{
				this._inputColumnSpacing = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0009E823 File Offset: 0x0009CC23
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x0009E82B File Offset: 0x0009CC2B
		public int inputRowCategorySpacing
		{
			get
			{
				return this._inputRowCategorySpacing;
			}
			set
			{
				this._inputRowCategorySpacing = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0009E83B File Offset: 0x0009CC3B
		// (set) Token: 0x060019D4 RID: 6612 RVA: 0x0009E843 File Offset: 0x0009CC43
		public int invertToggleWidth
		{
			get
			{
				return this._invertToggleWidth;
			}
			set
			{
				this._invertToggleWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0009E853 File Offset: 0x0009CC53
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x0009E85B File Offset: 0x0009CC5B
		public int defaultWindowWidth
		{
			get
			{
				return this._defaultWindowWidth;
			}
			set
			{
				this._defaultWindowWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0009E86B File Offset: 0x0009CC6B
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x0009E873 File Offset: 0x0009CC73
		public int defaultWindowHeight
		{
			get
			{
				return this._defaultWindowHeight;
			}
			set
			{
				this._defaultWindowHeight = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0009E883 File Offset: 0x0009CC83
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0009E88B File Offset: 0x0009CC8B
		public float controllerAssignmentTimeout
		{
			get
			{
				return this._controllerAssignmentTimeout;
			}
			set
			{
				this._controllerAssignmentTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x0009E89B File Offset: 0x0009CC9B
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x0009E8A3 File Offset: 0x0009CCA3
		public float preInputAssignmentTimeout
		{
			get
			{
				return this._preInputAssignmentTimeout;
			}
			set
			{
				this._preInputAssignmentTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0009E8B3 File Offset: 0x0009CCB3
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x0009E8BB File Offset: 0x0009CCBB
		public float inputAssignmentTimeout
		{
			get
			{
				return this._inputAssignmentTimeout;
			}
			set
			{
				this._inputAssignmentTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0009E8CB File Offset: 0x0009CCCB
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x0009E8D3 File Offset: 0x0009CCD3
		public float axisCalibrationTimeout
		{
			get
			{
				return this._axisCalibrationTimeout;
			}
			set
			{
				this._axisCalibrationTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0009E8E3 File Offset: 0x0009CCE3
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x0009E8EB File Offset: 0x0009CCEB
		public bool ignoreMouseXAxisAssignment
		{
			get
			{
				return this._ignoreMouseXAxisAssignment;
			}
			set
			{
				this._ignoreMouseXAxisAssignment = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0009E8FB File Offset: 0x0009CCFB
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0009E903 File Offset: 0x0009CD03
		public bool ignoreMouseYAxisAssignment
		{
			get
			{
				return this._ignoreMouseYAxisAssignment;
			}
			set
			{
				this._ignoreMouseYAxisAssignment = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0009E913 File Offset: 0x0009CD13
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0009E91B File Offset: 0x0009CD1B
		public bool universalCancelClosesScreen
		{
			get
			{
				return this._universalCancelClosesScreen;
			}
			set
			{
				this._universalCancelClosesScreen = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0009E92B File Offset: 0x0009CD2B
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x0009E933 File Offset: 0x0009CD33
		public bool showInputBehaviorSettings
		{
			get
			{
				return this._showInputBehaviorSettings;
			}
			set
			{
				this._showInputBehaviorSettings = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0009E943 File Offset: 0x0009CD43
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x0009E94B File Offset: 0x0009CD4B
		public bool useThemeSettings
		{
			get
			{
				return this._useThemeSettings;
			}
			set
			{
				this._useThemeSettings = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0009E95B File Offset: 0x0009CD5B
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x0009E963 File Offset: 0x0009CD63
		public LanguageData language
		{
			get
			{
				return this._language;
			}
			set
			{
				this._language = value;
				if (this._language != null)
				{
					this._language.Initialize();
				}
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0009E98F File Offset: 0x0009CD8F
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x0009E997 File Offset: 0x0009CD97
		public bool showPlayersGroupLabel
		{
			get
			{
				return this._showPlayersGroupLabel;
			}
			set
			{
				this._showPlayersGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x0009E9A7 File Offset: 0x0009CDA7
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x0009E9AF File Offset: 0x0009CDAF
		public bool showControllerGroupLabel
		{
			get
			{
				return this._showControllerGroupLabel;
			}
			set
			{
				this._showControllerGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x0009E9BF File Offset: 0x0009CDBF
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x0009E9C7 File Offset: 0x0009CDC7
		public bool showAssignedControllersGroupLabel
		{
			get
			{
				return this._showAssignedControllersGroupLabel;
			}
			set
			{
				this._showAssignedControllersGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x0009E9D7 File Offset: 0x0009CDD7
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x0009E9DF File Offset: 0x0009CDDF
		public bool showSettingsGroupLabel
		{
			get
			{
				return this._showSettingsGroupLabel;
			}
			set
			{
				this._showSettingsGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0009E9EF File Offset: 0x0009CDEF
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0009E9F7 File Offset: 0x0009CDF7
		public bool showMapCategoriesGroupLabel
		{
			get
			{
				return this._showMapCategoriesGroupLabel;
			}
			set
			{
				this._showMapCategoriesGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0009EA07 File Offset: 0x0009CE07
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0009EA0F File Offset: 0x0009CE0F
		public bool showControllerNameLabel
		{
			get
			{
				return this._showControllerNameLabel;
			}
			set
			{
				this._showControllerNameLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0009EA1F File Offset: 0x0009CE1F
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x0009EA27 File Offset: 0x0009CE27
		public bool showAssignedControllers
		{
			get
			{
				return this._showAssignedControllers;
			}
			set
			{
				this._showAssignedControllers = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0009EA37 File Offset: 0x0009CE37
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x0009EA3F File Offset: 0x0009CE3F
		public Action restoreDefaultsDelegate
		{
			get
			{
				return this._restoreDefaultsDelegate;
			}
			set
			{
				this._restoreDefaultsDelegate = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0009EA48 File Offset: 0x0009CE48
		public bool isOpen
		{
			get
			{
				if (!this.initialized)
				{
					return this.references.canvas != null && this.references.canvas.gameObject.activeInHierarchy;
				}
				return this.canvas.activeInHierarchy;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0009EA9D File Offset: 0x0009CE9D
		private bool isFocused
		{
			get
			{
				return this.initialized && !this.windowManager.isWindowOpen;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0009EABA File Offset: 0x0009CEBA
		private bool inputAllowed
		{
			get
			{
				return this.blockInputOnFocusEndTime <= Time.unscaledTime;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0009EAD0 File Offset: 0x0009CED0
		private int inputGridColumnCount
		{
			get
			{
				int num = 1;
				if (this._showKeyboard)
				{
					num++;
				}
				if (this._showMouse)
				{
					num++;
				}
				if (this._showControllers)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0009EB10 File Offset: 0x0009CF10
		private int inputGridWidth
		{
			get
			{
				return this._actionLabelWidth + ((!this._showKeyboard) ? 0 : this._keyboardColMaxWidth) + ((!this._showMouse) ? 0 : this._mouseColMaxWidth) + ((!this._showControllers) ? 0 : this._controllerColMaxWidth) + (this.inputGridColumnCount - 1) * this._inputColumnSpacing;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0009EB7B File Offset: 0x0009CF7B
		private Player currentPlayer
		{
			get
			{
				return ReInput.players.GetPlayer(this.currentPlayerId);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0009EB8D File Offset: 0x0009CF8D
		private InputCategory currentMapCategory
		{
			get
			{
				return ReInput.mapping.GetMapCategory(this.currentMapCategoryId);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x0009EBA0 File Offset: 0x0009CFA0
		private ControlMapper.MappingSet currentMappingSet
		{
			get
			{
				if (this.currentMapCategoryId < 0)
				{
					return null;
				}
				for (int i = 0; i < this._mappingSets.Length; i++)
				{
					if (this._mappingSets[i].mapCategoryId == this.currentMapCategoryId)
					{
						return this._mappingSets[i];
					}
				}
				return null;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0009EBF6 File Offset: 0x0009CFF6
		private Joystick currentJoystick
		{
			get
			{
				return ReInput.controllers.GetJoystick(this.currentJoystickId);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0009EC08 File Offset: 0x0009D008
		private bool isJoystickSelected
		{
			get
			{
				return this.currentJoystickId >= 0;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0009EC16 File Offset: 0x0009D016
		private GameObject currentUISelection
		{
			get
			{
				return (!(EventSystem.current != null)) ? null : EventSystem.current.currentSelectedGameObject;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0009EC38 File Offset: 0x0009D038
		private bool showSettings
		{
			get
			{
				return this._showInputBehaviorSettings && this._inputBehaviorSettings.Length > 0;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0009EC53 File Offset: 0x0009D053
		private bool showMapCategories
		{
			get
			{
				return this._mappingSets != null && this._mappingSets.Length > 1;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0009EC73 File Offset: 0x0009D073
		private void Awake()
		{
			if (this._dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
			}
			this.PreInitialize();
			if (this.isOpen)
			{
				this.Initialize();
				this.Open(true);
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0009ECAE File Offset: 0x0009D0AE
		private void Start()
		{
			if (this._openOnStart)
			{
				this.Open(false);
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0009ECC2 File Offset: 0x0009D0C2
		private void Update()
		{
			if (!this.isOpen)
			{
				return;
			}
			if (!this.initialized)
			{
				return;
			}
			this.CheckUISelection();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0009ECE2 File Offset: 0x0009D0E2
		private void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.OnJoystickConnected;
			ReInput.ControllerDisconnectedEvent -= this.OnJoystickDisconnected;
			ReInput.ControllerPreDisconnectEvent -= this.OnJoystickPreDisconnect;
			this.UnsubscribeMenuControlInputEvents();
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0009ED1D File Offset: 0x0009D11D
		private void PreInitialize()
		{
			if (!ReInput.isReady)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: Rewired has not been initialized! Are you missing a Rewired Input Manager in your scene?");
				return;
			}
			this.SubscribeMenuControlInputEvents();
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0009ED3C File Offset: 0x0009D13C
		private void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			if (this._rewiredInputManager == null)
			{
				this._rewiredInputManager = UnityEngine.Object.FindObjectOfType<InputManager>();
				if (this._rewiredInputManager == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: A Rewired Input Manager was not assigned in the inspector or found in the current scene! Control Mapper will not function.");
					return;
				}
			}
			if (ControlMapper.Instance != null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: Only one ControlMapper can exist at one time!");
				return;
			}
			ControlMapper.Instance = this;
			if (this.prefabs == null || !this.prefabs.Check())
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: All prefabs must be assigned in the inspector!");
				return;
			}
			if (this.references == null || !this.references.Check())
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: All references must be assigned in the inspector!");
				return;
			}
			this.references.inputGridLayoutElement = this.references.inputGridContainer.GetComponent<LayoutElement>();
			if (this.references.inputGridLayoutElement == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: InputGridContainer is missing LayoutElement component!");
				return;
			}
			if (this._showKeyboard && this._keyboardInputFieldCount < 1)
			{
				UnityEngine.Debug.LogWarning("Rewired Control Mapper: Keyboard Input Fields must be at least 1!");
				this._keyboardInputFieldCount = 1;
			}
			if (this._showMouse && this._mouseInputFieldCount < 1)
			{
				UnityEngine.Debug.LogWarning("Rewired Control Mapper: Mouse Input Fields must be at least 1!");
				this._mouseInputFieldCount = 1;
			}
			if (this._showControllers && this._controllerInputFieldCount < 1)
			{
				UnityEngine.Debug.LogWarning("Rewired Control Mapper: Controller Input Fields must be at least 1!");
				this._controllerInputFieldCount = 1;
			}
			if (this._maxControllersPerPlayer < 0)
			{
				UnityEngine.Debug.LogWarning("Rewired Control Mapper: Max Controllers Per Player must be at least 0 (no limit)!");
				this._maxControllersPerPlayer = 0;
			}
			if (this._useThemeSettings && this._themeSettings == null)
			{
				UnityEngine.Debug.LogWarning("Rewired Control Mapper: To use theming, Theme Settings must be set in the inspector! Theming has been disabled.");
				this._useThemeSettings = false;
			}
			if (this._language == null)
			{
				UnityEngine.Debug.LogError("Rawired UI: Language must be set in the inspector!");
				return;
			}
			this._language.Initialize();
			this.inputFieldActivatedDelegate = new Action<InputFieldInfo>(this.OnInputFieldActivated);
			this.inputFieldInvertToggleStateChangedDelegate = new Action<ToggleInfo, bool>(this.OnInputFieldInvertToggleStateChanged);
			ReInput.ControllerConnectedEvent += this.OnJoystickConnected;
			ReInput.ControllerDisconnectedEvent += this.OnJoystickDisconnected;
			ReInput.ControllerPreDisconnectEvent += this.OnJoystickPreDisconnect;
			this.playerCount = ReInput.players.playerCount;
			this.canvas = this.references.canvas.gameObject;
			this.windowManager = new ControlMapper.WindowManager(this.prefabs.window, this.prefabs.fader, this.references.canvas.transform);
			this.playerButtons = new List<ControlMapper.GUIButton>();
			this.mapCategoryButtons = new List<ControlMapper.GUIButton>();
			this.assignedControllerButtons = new List<ControlMapper.GUIButton>();
			this.miscInstantiatedObjects = new List<GameObject>();
			this.currentMapCategoryId = this._mappingSets[0].mapCategoryId;
			this.Draw();
			this.CreateInputGrid();
			this.CreateLayout();
			this.SubscribeFixedUISelectionEvents();
			this.initialized = true;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0009F036 File Offset: 0x0009D436
		private void OnJoystickConnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this._showControllers)
			{
				return;
			}
			this.ClearVarsOnJoystickChange();
			this.ForceRefresh();
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0009F05C File Offset: 0x0009D45C
		private void OnJoystickDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this._showControllers)
			{
				return;
			}
			this.ClearVarsOnJoystickChange();
			this.ForceRefresh();
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0009F082 File Offset: 0x0009D482
		private void OnJoystickPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this._showControllers)
			{
				return;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0009F09C File Offset: 0x0009D49C
		public void OnButtonActivated(ButtonInfo buttonInfo)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.inputAllowed)
			{
				return;
			}
			string identifier = buttonInfo.identifier;
			switch (identifier)
			{
			case "PlayerSelection":
				this.OnPlayerSelected(buttonInfo.intData, true);
				break;
			case "AssignedControllerSelection":
				this.OnControllerSelected(buttonInfo.intData);
				break;
			case "RemoveController":
				this.OnRemoveCurrentController();
				break;
			case "AssignController":
				this.ShowAssignControllerWindow();
				break;
			case "CalibrateController":
				this.ShowCalibrateControllerWindow();
				break;
			case "EditInputBehaviors":
				this.ShowEditInputBehaviorsWindow();
				break;
			case "MapCategorySelection":
				this.OnMapCategorySelected(buttonInfo.intData, true);
				break;
			case "Done":
				this.Close(true);
				break;
			case "RestoreDefaults":
				this.OnRestoreDefaults();
				break;
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0009F20C File Offset: 0x0009D60C
		public void OnInputFieldActivated(InputFieldInfo fieldInfo)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.inputAllowed)
			{
				return;
			}
			if (this.currentPlayer == null)
			{
				return;
			}
			InputAction action = ReInput.mapping.GetAction(fieldInfo.actionId);
			if (action == null)
			{
				return;
			}
			string actionName;
			if (action.type == InputActionType.Button)
			{
				actionName = action.descriptiveName;
			}
			else
			{
				if (action.type != InputActionType.Axis)
				{
					throw new NotImplementedException();
				}
				if (fieldInfo.axisRange == AxisRange.Full)
				{
					actionName = action.descriptiveName;
				}
				else if (fieldInfo.axisRange == AxisRange.Positive)
				{
					if (string.IsNullOrEmpty(action.positiveDescriptiveName))
					{
						actionName = action.descriptiveName + " +";
					}
					else
					{
						actionName = action.positiveDescriptiveName;
					}
				}
				else
				{
					if (fieldInfo.axisRange != AxisRange.Negative)
					{
						throw new NotImplementedException();
					}
					if (string.IsNullOrEmpty(action.negativeDescriptiveName))
					{
						actionName = action.descriptiveName + " -";
					}
					else
					{
						actionName = action.negativeDescriptiveName;
					}
				}
			}
			ControllerMap controllerMap = this.GetControllerMap(fieldInfo.controllerType);
			if (controllerMap == null)
			{
				return;
			}
			ActionElementMap actionElementMap = (fieldInfo.actionElementMapId < 0) ? null : controllerMap.GetElementMap(fieldInfo.actionElementMapId);
			if (actionElementMap != null)
			{
				this.ShowBeginElementAssignmentReplacementWindow(fieldInfo, action, controllerMap, actionElementMap, actionName);
			}
			else
			{
				this.ShowCreateNewElementAssignmentWindow(fieldInfo, action, controllerMap, actionName);
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0009F36F File Offset: 0x0009D76F
		public void OnInputFieldInvertToggleStateChanged(ToggleInfo toggleInfo, bool newState)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.inputAllowed)
			{
				return;
			}
			this.SetActionAxisInverted(newState, toggleInfo.controllerType, toggleInfo.actionElementMapId);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0009F39C File Offset: 0x0009D79C
		private void OnPlayerSelected(int playerId, bool redraw)
		{
			if (!this.initialized)
			{
				return;
			}
			this.currentPlayerId = playerId;
			this.ClearVarsOnPlayerChange();
			if (redraw)
			{
				this.Redraw(true, true);
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0009F3C5 File Offset: 0x0009D7C5
		private void OnControllerSelected(int joystickId)
		{
			if (!this.initialized)
			{
				return;
			}
			this.currentJoystickId = joystickId;
			this.Redraw(true, true);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0009F3E2 File Offset: 0x0009D7E2
		private void OnRemoveCurrentController()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this.currentJoystickId < 0)
			{
				return;
			}
			this.RemoveController(this.currentPlayer, this.currentJoystickId);
			this.ClearVarsOnJoystickChange();
			this.Redraw(false, false);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0009F41D File Offset: 0x0009D81D
		private void OnMapCategorySelected(int id, bool redraw)
		{
			if (!this.initialized)
			{
				return;
			}
			this.currentMapCategoryId = id;
			if (redraw)
			{
				this.Redraw(true, true);
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0009F440 File Offset: 0x0009D840
		private void OnRestoreDefaults()
		{
			if (!this.initialized)
			{
				return;
			}
			this.ShowRestoreDefaultsWindow();
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0009F454 File Offset: 0x0009D854
		private void OnScreenToggleActionPressed(InputActionEventData data)
		{
			if (!this.isOpen)
			{
				this.Open();
				return;
			}
			if (!this.initialized)
			{
				return;
			}
			if (!this.isFocused)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0009F487 File Offset: 0x0009D887
		private void OnScreenOpenActionPressed(InputActionEventData data)
		{
			this.Open();
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0009F48F File Offset: 0x0009D88F
		private void OnScreenCloseActionPressed(InputActionEventData data)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.isOpen)
			{
				return;
			}
			if (!this.isFocused)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0009F4BC File Offset: 0x0009D8BC
		private void OnUniversalCancelActionPressed(InputActionEventData data)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.isOpen)
			{
				return;
			}
			if (this._universalCancelClosesScreen)
			{
				if (this.isFocused)
				{
					this.Close(true);
					return;
				}
			}
			else if (this.isFocused)
			{
				return;
			}
			this.CloseAllWindows();
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0009F516 File Offset: 0x0009D916
		private void OnWindowCancel(int windowId)
		{
			if (!this.initialized)
			{
				return;
			}
			if (windowId < 0)
			{
				return;
			}
			this.CloseWindow(windowId);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0009F533 File Offset: 0x0009D933
		private void OnRemoveElementAssignment(int windowId, ControllerMap map, ActionElementMap aem)
		{
			if (map == null || aem == null)
			{
				return;
			}
			map.DeleteElementMap(aem.id);
			this.CloseWindow(windowId);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0009F558 File Offset: 0x0009D958
		private void OnBeginElementAssignment(InputFieldInfo fieldInfo, ControllerMap map, ActionElementMap aem, string actionName)
		{
			if (fieldInfo == null || map == null)
			{
				return;
			}
			this.pendingInputMapping = new ControlMapper.InputMapping(actionName, fieldInfo, map, aem, fieldInfo.controllerType, fieldInfo.controllerId);
			switch (fieldInfo.controllerType)
			{
			case ControllerType.Keyboard:
				this.ShowElementAssignmentPollingWindow();
				break;
			case ControllerType.Mouse:
				this.ShowElementAssignmentPollingWindow();
				break;
			case ControllerType.Joystick:
				this.ShowElementAssignmentPrePollingWindow();
				break;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0009F5D9 File Offset: 0x0009D9D9
		private void OnControllerAssignmentConfirmed(int windowId, Player player, int controllerId)
		{
			if (windowId < 0 || player == null || controllerId < 0)
			{
				return;
			}
			this.AssignController(player, controllerId);
			this.CloseWindow(windowId);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0009F600 File Offset: 0x0009DA00
		private void OnMouseAssignmentConfirmed(int windowId, Player player)
		{
			if (windowId < 0 || player == null)
			{
				return;
			}
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i] != player)
				{
					players[i].controllers.hasMouse = false;
				}
			}
			player.controllers.hasMouse = true;
			this.CloseWindow(windowId);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0009F674 File Offset: 0x0009DA74
		private void OnElementAssignmentConflictReplaceConfirmed(int windowId, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers, bool allowSwap)
		{
			if (this.currentPlayer == null || mapping == null)
			{
				return;
			}
			ElementAssignmentConflictCheck conflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out conflictCheck))
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: Error creating conflict check!");
				this.CloseWindow(windowId);
				return;
			}
			ElementAssignmentConflictInfo elementAssignmentConflictInfo = default(ElementAssignmentConflictInfo);
			ActionElementMap actionElementMap = null;
			ActionElementMap actionElementMap2 = null;
			bool flag = false;
			if (allowSwap && mapping.aem != null && this.GetFirstElementAssignmentConflict(conflictCheck, out elementAssignmentConflictInfo, skipOtherPlayers))
			{
				flag = true;
				actionElementMap2 = new ActionElementMap(mapping.aem);
				actionElementMap = new ActionElementMap(elementAssignmentConflictInfo.elementMap);
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (!skipOtherPlayers || player == this.currentPlayer || player == ReInput.players.SystemPlayer)
				{
					player.controllers.conflictChecking.RemoveElementAssignmentConflicts(conflictCheck);
				}
			}
			mapping.map.ReplaceOrCreateElementMap(assignment);
			if (allowSwap && flag)
			{
				int actionId = actionElementMap.actionId;
				Pole axisContribution = actionElementMap.axisContribution;
				bool invert = actionElementMap.invert;
				AxisRange axisRange = actionElementMap2.axisRange;
				ControllerElementType elementType = actionElementMap2.elementType;
				int elementIdentifierId = actionElementMap2.elementIdentifierId;
				KeyCode keyCode = actionElementMap2.keyCode;
				ModifierKeyFlags modifierKeyFlags = actionElementMap2.modifierKeyFlags;
				if (elementType == actionElementMap.elementType && elementType == ControllerElementType.Axis)
				{
					if (axisRange != actionElementMap.axisRange)
					{
						if (axisRange == AxisRange.Full)
						{
							axisRange = AxisRange.Positive;
						}
						else if (actionElementMap.axisRange == AxisRange.Full)
						{
						}
					}
				}
				else if (elementType == ControllerElementType.Axis && (actionElementMap.elementType == ControllerElementType.Button || (actionElementMap.elementType == ControllerElementType.Axis && actionElementMap.axisRange != AxisRange.Full)) && axisRange == AxisRange.Full)
				{
					axisRange = AxisRange.Positive;
				}
				if (elementType != ControllerElementType.Axis || axisRange != AxisRange.Full)
				{
					invert = false;
				}
				int num = 0;
				foreach (ActionElementMap actionElementMap3 in elementAssignmentConflictInfo.controllerMap.ElementMapsWithAction(actionId))
				{
					if (this.SwapIsSameInputRange(elementType, axisRange, axisContribution, actionElementMap3.elementType, actionElementMap3.axisRange, actionElementMap3.axisContribution))
					{
						num++;
					}
				}
				if (num < this.GetControllerInputFieldCount(mapping.controllerType))
				{
					elementAssignmentConflictInfo.controllerMap.ReplaceOrCreateElementMap(ElementAssignment.CompleteAssignment(mapping.controllerType, elementType, elementIdentifierId, axisRange, keyCode, modifierKeyFlags, actionId, axisContribution, invert));
				}
			}
			this.CloseWindow(windowId);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0009F914 File Offset: 0x0009DD14
		private void OnElementAssignmentAddConfirmed(int windowId, ControlMapper.InputMapping mapping, ElementAssignment assignment)
		{
			if (this.currentPlayer == null || mapping == null)
			{
				return;
			}
			mapping.map.ReplaceOrCreateElementMap(assignment);
			this.CloseWindow(windowId);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0009F93C File Offset: 0x0009DD3C
		private void OnRestoreDefaultsConfirmed(int windowId)
		{
			if (this._restoreDefaultsDelegate == null)
			{
				IList<Player> players = ReInput.players.Players;
				for (int i = 0; i < players.Count; i++)
				{
					Player player = players[i];
					if (this._showControllers)
					{
						player.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
					}
					if (this._showKeyboard)
					{
						player.controllers.maps.LoadDefaultMaps(ControllerType.Keyboard);
					}
					if (this._showMouse)
					{
						player.controllers.maps.LoadDefaultMaps(ControllerType.Mouse);
					}
				}
			}
			this.CloseWindow(windowId);
			if (this._restoreDefaultsDelegate != null)
			{
				this._restoreDefaultsDelegate();
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0009F9F0 File Offset: 0x0009DDF0
		private void OnAssignControllerWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			ControllerPollingInfo controllerPollingInfo = ReInput.controllers.polling.PollAllControllersOfTypeForFirstElementDown(ControllerType.Joystick);
			if (!controllerPollingInfo.success)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				return;
			}
			this.InputPollingStopped();
			if (ReInput.controllers.IsControllerAssigned(ControllerType.Joystick, controllerPollingInfo.controllerId) && !this.currentPlayer.controllers.ContainsController(ControllerType.Joystick, controllerPollingInfo.controllerId))
			{
				this.ShowControllerAssignmentConflictWindow(controllerPollingInfo.controllerId);
				return;
			}
			this.OnControllerAssignmentConfirmed(windowId, this.currentPlayer, controllerPollingInfo.controllerId);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0009FAE4 File Offset: 0x0009DEE4
		private void OnElementAssignmentPrePollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (!window.timer.finished)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				ControllerPollingInfo controllerPollingInfo;
				switch (this.pendingInputMapping.controllerType)
				{
				case ControllerType.Keyboard:
				case ControllerType.Mouse:
					controllerPollingInfo = ReInput.controllers.polling.PollControllerForFirstButtonDown(this.pendingInputMapping.controllerType, 0);
					break;
				case ControllerType.Joystick:
					if (this.currentPlayer.controllers.joystickCount == 0)
					{
						return;
					}
					controllerPollingInfo = ReInput.controllers.polling.PollControllerForFirstButtonDown(this.pendingInputMapping.controllerType, this.currentJoystick.id);
					break;
				default:
					throw new NotImplementedException();
				}
				if (!controllerPollingInfo.success)
				{
					return;
				}
			}
			this.ShowElementAssignmentPollingWindow();
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0009FBFC File Offset: 0x0009DFFC
		private void OnJoystickElementAssignmentPollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
			if (this.currentPlayer.controllers.joystickCount == 0)
			{
				return;
			}
			ControllerPollingInfo pollingInfo = ReInput.controllers.polling.PollControllerForFirstElementDown(ControllerType.Joystick, this.currentJoystick.id);
			if (!pollingInfo.success)
			{
				return;
			}
			if (!this.IsAllowedAssignment(this.pendingInputMapping, pollingInfo))
			{
				return;
			}
			ElementAssignment elementAssignment = this.pendingInputMapping.ToElementAssignment(pollingInfo);
			if (!this.HasElementAssignmentConflicts(this.currentPlayer, this.pendingInputMapping, elementAssignment, false))
			{
				this.pendingInputMapping.map.ReplaceOrCreateElementMap(elementAssignment);
				this.InputPollingStopped();
				this.CloseWindow(windowId);
			}
			else
			{
				this.InputPollingStopped();
				this.ShowElementAssignmentConflictWindow(elementAssignment, false);
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0009FD2C File Offset: 0x0009E12C
		private void OnKeyboardElementAssignmentPollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			ControllerPollingInfo pollingInfo;
			bool flag;
			ModifierKeyFlags modifierKeyFlags;
			string text;
			this.PollKeyboardForAssignment(out pollingInfo, out flag, out modifierKeyFlags, out text);
			if (flag)
			{
				window.timer.Start(this._inputAssignmentTimeout);
			}
			window.SetContentText((!flag) ? Mathf.CeilToInt(window.timer.remaining).ToString() : string.Empty, 2);
			window.SetContentText(text, 1);
			if (!pollingInfo.success)
			{
				return;
			}
			if (!this.IsAllowedAssignment(this.pendingInputMapping, pollingInfo))
			{
				return;
			}
			ElementAssignment elementAssignment = this.pendingInputMapping.ToElementAssignment(pollingInfo, modifierKeyFlags);
			if (!this.HasElementAssignmentConflicts(this.currentPlayer, this.pendingInputMapping, elementAssignment, false))
			{
				this.pendingInputMapping.map.ReplaceOrCreateElementMap(elementAssignment);
				this.InputPollingStopped();
				this.CloseWindow(windowId);
			}
			else
			{
				this.InputPollingStopped();
				this.ShowElementAssignmentConflictWindow(elementAssignment, false);
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0009FE6C File Offset: 0x0009E26C
		private void OnMouseElementAssignmentPollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
			ControllerPollingInfo pollingInfo;
			if (this._ignoreMouseXAxisAssignment || this._ignoreMouseYAxisAssignment)
			{
				pollingInfo = default(ControllerPollingInfo);
				foreach (ControllerPollingInfo controllerPollingInfo in ReInput.controllers.polling.PollControllerForAllElementsDown(ControllerType.Mouse, 0))
				{
					if (controllerPollingInfo.elementType == ControllerElementType.Axis)
					{
						if (this._ignoreMouseXAxisAssignment && controllerPollingInfo.elementIndex == 0)
						{
							continue;
						}
						if (this._ignoreMouseYAxisAssignment && controllerPollingInfo.elementIndex == 1)
						{
							continue;
						}
					}
					pollingInfo = controllerPollingInfo;
					break;
				}
			}
			else
			{
				pollingInfo = ReInput.controllers.polling.PollControllerForFirstElementDown(ControllerType.Mouse, 0);
			}
			if (!pollingInfo.success)
			{
				return;
			}
			if (!this.IsAllowedAssignment(this.pendingInputMapping, pollingInfo))
			{
				return;
			}
			ElementAssignment elementAssignment = this.pendingInputMapping.ToElementAssignment(pollingInfo);
			if (!this.HasElementAssignmentConflicts(this.currentPlayer, this.pendingInputMapping, elementAssignment, true))
			{
				this.pendingInputMapping.map.ReplaceOrCreateElementMap(elementAssignment);
				this.InputPollingStopped();
				this.CloseWindow(windowId);
			}
			else
			{
				this.InputPollingStopped();
				this.ShowElementAssignmentConflictWindow(elementAssignment, true);
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000A0044 File Offset: 0x0009E444
		private void OnCalibrateAxisStep1WindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingAxisCalibration == null || !this.pendingAxisCalibration.isValid)
			{
				return;
			}
			this.InputPollingStarted();
			if (!window.timer.finished)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				if (this.currentPlayer.controllers.joystickCount == 0)
				{
					return;
				}
				if (!this.pendingAxisCalibration.joystick.PollForFirstButtonDown().success)
				{
					return;
				}
			}
			this.pendingAxisCalibration.RecordZero();
			this.CloseWindow(windowId);
			this.ShowCalibrateAxisStep2Window();
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000A011C File Offset: 0x0009E51C
		private void OnCalibrateAxisStep2WindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingAxisCalibration == null || !this.pendingAxisCalibration.isValid)
			{
				return;
			}
			if (!window.timer.finished)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				this.pendingAxisCalibration.RecordMinMax();
				if (this.currentPlayer.controllers.joystickCount == 0)
				{
					return;
				}
				if (!this.pendingAxisCalibration.joystick.PollForFirstButtonDown().success)
				{
					return;
				}
			}
			this.EndAxisCalibration();
			this.InputPollingStopped();
			this.CloseWindow(windowId);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000A01F4 File Offset: 0x0009E5F4
		private void ShowAssignControllerWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.SetUpdateCallback(new Action<int>(this.OnAssignControllerWindowUpdate));
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.assignControllerWindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.assignControllerWindowMessage);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, string.Empty);
			window.timer.Start(this._controllerAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000A02DC File Offset: 0x0009E6DC
		private void ShowControllerAssignmentConflictWindow(int controllerId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string otherPlayerName = string.Empty;
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i] != this.currentPlayer)
				{
					if (players[i].controllers.ContainsController(ControllerType.Joystick, controllerId))
					{
						otherPlayerName = players[i].descriptiveName;
						break;
					}
				}
			}
			Joystick joystick = ReInput.controllers.GetJoystick(controllerId);
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.controllerAssignmentConflictWindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetControllerAssignmentConflictWindowMessage(joystick.name, otherPlayerName, this.currentPlayer.descriptiveName));
			UnityAction unityAction = delegate()
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.yes, delegate()
			{
				this.OnControllerAssignmentConfirmed(window.id, this.currentPlayer, controllerId);
			}, unityAction, true);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.no, unityAction, unityAction, false);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000A04D0 File Offset: 0x0009E8D0
		private void ShowBeginElementAssignmentReplacementWindow(InputFieldInfo fieldInfo, InputAction action, ControllerMap map, ActionElementMap aem, string actionName)
		{
			ControlMapper.GUIInputField guiinputField = this.inputGrid.GetGUIInputField(this.currentMapCategoryId, action.id, fieldInfo.axisRange, fieldInfo.controllerType, fieldInfo.intData);
			if (guiinputField == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), guiinputField.GetLabel());
			UnityAction unityAction = delegate()
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.replace, delegate()
			{
				this.OnBeginElementAssignment(fieldInfo, map, aem, actionName);
			}, unityAction, true);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.remove, delegate()
			{
				this.OnRemoveElementAssignment(window.id, map, aem);
			}, unityAction, false);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.cancel, unityAction, unityAction, false);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000A0698 File Offset: 0x0009EA98
		private void ShowCreateNewElementAssignmentWindow(InputFieldInfo fieldInfo, InputAction action, ControllerMap map, string actionName)
		{
			if (this.inputGrid.GetGUIInputField(this.currentMapCategoryId, action.id, fieldInfo.axisRange, fieldInfo.controllerType, fieldInfo.intData) == null)
			{
				return;
			}
			this.OnBeginElementAssignment(fieldInfo, map, null, actionName);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000A06E4 File Offset: 0x0009EAE4
		private void ShowElementAssignmentPrePollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.elementAssignmentPrePollingWindowMessage);
			if (this.prefabs.centerStickGraphic != null)
			{
				window.AddContentImage(this.prefabs.centerStickGraphic, UIPivot.BottomCenter, UIAnchor.BottomCenter, new Vector2(0f, 40f));
			}
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, string.Empty);
			window.SetUpdateCallback(new Action<int>(this.OnElementAssignmentPrePollingWindowUpdate));
			window.timer.Start(this._preInputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000A07FC File Offset: 0x0009EBFC
		private void ShowElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			switch (this.pendingInputMapping.controllerType)
			{
			case ControllerType.Keyboard:
				this.ShowKeyboardElementAssignmentPollingWindow();
				break;
			case ControllerType.Mouse:
				if (this.currentPlayer.controllers.hasMouse)
				{
					this.ShowMouseElementAssignmentPollingWindow();
				}
				else
				{
					this.ShowMouseAssignmentConflictWindow();
				}
				break;
			case ControllerType.Joystick:
				this.ShowJoystickElementAssignmentPollingWindow();
				break;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000A0880 File Offset: 0x0009EC80
		private void ShowJoystickElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string text = (this.pendingInputMapping.axisRange != AxisRange.Full || !this._showFullAxisInputFields || this._showSplitAxisInputFields) ? this._language.GetJoystickElementAssignmentPollingWindowMessage(this.pendingInputMapping.actionName) : this._language.GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(this.pendingInputMapping.actionName);
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), text);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, string.Empty);
			window.SetUpdateCallback(new Action<int>(this.OnJoystickElementAssignmentPollingWindowUpdate));
			window.timer.Start(this._inputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000A09A8 File Offset: 0x0009EDA8
		private void ShowKeyboardElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetKeyboardElementAssignmentPollingWindowMessage(this.pendingInputMapping.actionName));
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -(window.GetContentTextHeight(0) + 50f)), string.Empty);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, string.Empty);
			window.SetUpdateCallback(new Action<int>(this.OnKeyboardElementAssignmentPollingWindowUpdate));
			window.timer.Start(this._inputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000A0AC4 File Offset: 0x0009EEC4
		private void ShowMouseElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string text = (this.pendingInputMapping.axisRange != AxisRange.Full || !this._showFullAxisInputFields || this._showSplitAxisInputFields) ? this._language.GetMouseElementAssignmentPollingWindowMessage(this.pendingInputMapping.actionName) : this._language.GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(this.pendingInputMapping.actionName);
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), text);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, string.Empty);
			window.SetUpdateCallback(new Action<int>(this.OnMouseElementAssignmentPollingWindowUpdate));
			window.timer.Start(this._inputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000A0BEC File Offset: 0x0009EFEC
		private void ShowElementAssignmentConflictWindow(ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			bool flag = this.IsBlockingAssignmentConflict(this.pendingInputMapping, assignment, skipOtherPlayers);
			string text = (!flag) ? this._language.GetElementAlreadyInUseCanReplace(this.pendingInputMapping.elementName, this._allowElementAssignmentConflicts) : this._language.GetElementAlreadyInUseBlocked(this.pendingInputMapping.elementName);
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.elementAssignmentConflictWindowMessage);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), text);
			UnityAction unityAction = delegate()
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			if (flag)
			{
				window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.okay, unityAction, unityAction, true);
			}
			else
			{
				window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.replace, delegate()
				{
					this.OnElementAssignmentConflictReplaceConfirmed(window.id, this.pendingInputMapping, assignment, skipOtherPlayers, false);
				}, unityAction, true);
				if (this._allowElementAssignmentConflicts)
				{
					window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.add, delegate()
					{
						this.OnElementAssignmentAddConfirmed(window.id, this.pendingInputMapping, assignment);
					}, unityAction, false);
				}
				else if (this.ShowSwapButton(window.id, this.pendingInputMapping, assignment, skipOtherPlayers))
				{
					window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.swap, delegate()
					{
						this.OnElementAssignmentConflictReplaceConfirmed(window.id, this.pendingInputMapping, assignment, skipOtherPlayers, true);
					}, unityAction, false);
				}
				window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.cancel, unityAction, unityAction, false);
			}
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000A0E7C File Offset: 0x0009F27C
		private void ShowMouseAssignmentConflictWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string otherPlayerName = string.Empty;
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i] != this.currentPlayer)
				{
					if (players[i].controllers.hasMouse)
					{
						otherPlayerName = players[i].descriptiveName;
						break;
					}
				}
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.mouseAssignmentConflictWindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetMouseAssignmentConflictWindowMessage(otherPlayerName, this.currentPlayer.descriptiveName));
			UnityAction unityAction = delegate()
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.yes, delegate()
			{
				this.OnMouseAssignmentConfirmed(window.id, this.currentPlayer);
			}, unityAction, true);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.no, unityAction, unityAction, false);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000A103C File Offset: 0x0009F43C
		private void ShowCalibrateControllerWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this.currentPlayer.controllers.joystickCount == 0)
			{
				return;
			}
			CalibrationWindow calibrationWindow = this.OpenWindow(this.prefabs.calibrationWindow, "CalibrationWindow", true) as CalibrationWindow;
			if (calibrationWindow == null)
			{
				return;
			}
			Joystick currentJoystick = this.currentJoystick;
			calibrationWindow.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.calibrateControllerWindowTitle);
			calibrationWindow.SetJoystick(this.currentPlayer.id, currentJoystick);
			calibrationWindow.SetButtonCallback(CalibrationWindow.ButtonIdentifier.Done, new Action<int>(this.CloseWindow));
			calibrationWindow.SetButtonCallback(CalibrationWindow.ButtonIdentifier.Calibrate, new Action<int>(this.StartAxisCalibration));
			calibrationWindow.SetButtonCallback(CalibrationWindow.ButtonIdentifier.Cancel, new Action<int>(this.CloseWindow));
			this.windowManager.Focus(calibrationWindow);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000A1114 File Offset: 0x0009F514
		private void ShowCalibrateAxisStep1Window()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.OpenWindow(false);
			if (window == null)
			{
				return;
			}
			if (this.pendingAxisCalibration == null)
			{
				return;
			}
			Joystick joystick = this.pendingAxisCalibration.joystick;
			if (joystick.axisCount == 0)
			{
				return;
			}
			int axisIndex = this.pendingAxisCalibration.axisIndex;
			if (axisIndex < 0 || axisIndex >= joystick.axisCount)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.calibrateAxisStep1WindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetCalibrateAxisStep1WindowMessage(joystick.AxisElementIdentifiers[axisIndex].name));
			if (this.prefabs.centerStickGraphic != null)
			{
				window.AddContentImage(this.prefabs.centerStickGraphic, UIPivot.BottomCenter, UIAnchor.BottomCenter, new Vector2(0f, 40f));
			}
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, string.Empty);
			window.SetUpdateCallback(new Action<int>(this.OnCalibrateAxisStep1WindowUpdate));
			window.timer.Start(this._axisCalibrationTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000A1284 File Offset: 0x0009F684
		private void ShowCalibrateAxisStep2Window()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.OpenWindow(false);
			if (window == null)
			{
				return;
			}
			if (this.pendingAxisCalibration == null)
			{
				return;
			}
			Joystick joystick = this.pendingAxisCalibration.joystick;
			if (joystick.axisCount == 0)
			{
				return;
			}
			int axisIndex = this.pendingAxisCalibration.axisIndex;
			if (axisIndex < 0 || axisIndex >= joystick.axisCount)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.calibrateAxisStep2WindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetCalibrateAxisStep2WindowMessage(joystick.AxisElementIdentifiers[axisIndex].name));
			if (this.prefabs.moveStickGraphic != null)
			{
				window.AddContentImage(this.prefabs.moveStickGraphic, UIPivot.BottomCenter, UIAnchor.BottomCenter, new Vector2(0f, 40f));
			}
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, string.Empty);
			window.SetUpdateCallback(new Action<int>(this.OnCalibrateAxisStep2WindowUpdate));
			window.timer.Start(this._axisCalibrationTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000A13F4 File Offset: 0x0009F7F4
		private void ShowEditInputBehaviorsWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this._inputBehaviorSettings == null)
			{
				return;
			}
			InputBehaviorWindow inputBehaviorWindow = this.OpenWindow(this.prefabs.inputBehaviorsWindow, "EditInputBehaviorsWindow", true) as InputBehaviorWindow;
			if (inputBehaviorWindow == null)
			{
				return;
			}
			inputBehaviorWindow.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.inputBehaviorSettingsWindowTitle);
			inputBehaviorWindow.SetData(this.currentPlayer.id, this._inputBehaviorSettings);
			inputBehaviorWindow.SetButtonCallback(InputBehaviorWindow.ButtonIdentifier.Done, new Action<int>(this.CloseWindow));
			inputBehaviorWindow.SetButtonCallback(InputBehaviorWindow.ButtonIdentifier.Cancel, new Action<int>(this.CloseWindow));
			this.windowManager.Focus(inputBehaviorWindow);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000A14B0 File Offset: 0x0009F8B0
		private void ShowRestoreDefaultsWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			this.OpenModal(this._language.restoreDefaultsWindowTitle, this._language.restoreDefaultsWindowMessage, this._language.yes, new Action<int>(this.OnRestoreDefaultsConfirmed), this._language.no, new Action<int>(this.OnWindowCancel), true);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000A1514 File Offset: 0x0009F914
		private void CreateInputGrid()
		{
			this.InitializeInputGrid();
			this.CreateHeaderLabels();
			this.CreateActionLabelColumn();
			this.CreateKeyboardInputFieldColumn();
			this.CreateMouseInputFieldColumn();
			this.CreateControllerInputFieldColumn();
			this.CreateInputActionLabels();
			this.CreateInputFields();
			this.inputGrid.HideAll();
			this.ResetInputGridScrollBar();
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000A1564 File Offset: 0x0009F964
		private void InitializeInputGrid()
		{
			if (this.inputGrid == null)
			{
				this.inputGrid = new ControlMapper.InputGrid();
			}
			else
			{
				this.inputGrid.ClearAll();
			}
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null && mappingSet.isValid)
				{
					InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(mappingSet.mapCategoryId);
					if (mapCategory != null)
					{
						if (mapCategory.userAssignable)
						{
							this.inputGrid.AddMapCategory(mappingSet.mapCategoryId);
							if (mappingSet.actionListMode == ControlMapper.MappingSet.ActionListMode.ActionCategory)
							{
								IList<int> actionCategoryIds = mappingSet.actionCategoryIds;
								for (int j = 0; j < actionCategoryIds.Count; j++)
								{
									int num = actionCategoryIds[j];
									InputCategory actionCategory = ReInput.mapping.GetActionCategory(num);
									if (actionCategory != null)
									{
										if (actionCategory.userAssignable)
										{
											this.inputGrid.AddActionCategory(mappingSet.mapCategoryId, num);
											foreach (InputAction inputAction in ReInput.mapping.UserAssignableActionsInCategory(num))
											{
												if (inputAction.type == InputActionType.Axis)
												{
													if (this._showFullAxisInputFields)
													{
														this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, AxisRange.Full);
													}
													if (this._showSplitAxisInputFields)
													{
														this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, AxisRange.Positive);
														this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, AxisRange.Negative);
													}
												}
												else if (inputAction.type == InputActionType.Button)
												{
													this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, AxisRange.Positive);
												}
											}
										}
									}
								}
							}
							else
							{
								IList<int> actionIds = mappingSet.actionIds;
								for (int k = 0; k < actionIds.Count; k++)
								{
									InputAction action = ReInput.mapping.GetAction(actionIds[k]);
									if (action != null)
									{
										if (action.type == InputActionType.Axis)
										{
											if (this._showFullAxisInputFields)
											{
												this.inputGrid.AddAction(mappingSet.mapCategoryId, action, AxisRange.Full);
											}
											if (this._showSplitAxisInputFields)
											{
												this.inputGrid.AddAction(mappingSet.mapCategoryId, action, AxisRange.Positive);
												this.inputGrid.AddAction(mappingSet.mapCategoryId, action, AxisRange.Negative);
											}
										}
										else if (action.type == InputActionType.Button)
										{
											this.inputGrid.AddAction(mappingSet.mapCategoryId, action, AxisRange.Positive);
										}
									}
								}
							}
						}
					}
				}
			}
			this.references.inputGridInnerGroup.GetComponent<HorizontalLayoutGroup>().spacing = (float)this._inputColumnSpacing;
			this.references.inputGridLayoutElement.flexibleWidth = 0f;
			this.references.inputGridLayoutElement.preferredWidth = (float)this.inputGridWidth;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000A186C File Offset: 0x0009FC6C
		private void RefreshInputGridStructure()
		{
			if (this.currentMappingSet == null)
			{
				return;
			}
			this.inputGrid.HideAll();
			this.inputGrid.Show(this.currentMappingSet.mapCategoryId);
			this.references.inputGridInnerGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.inputGrid.GetColumnHeight(this.currentMappingSet.mapCategoryId));
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000A18D4 File Offset: 0x0009FCD4
		private void CreateHeaderLabels()
		{
			this.references.inputGridHeader1 = this.CreateNewColumnGroup("ActionsHeader", this.references.inputGridHeadersGroup, this._actionLabelWidth).transform;
			this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.actionColumnLabel, this.references.inputGridHeader1, Vector2.zero);
			if (this._showKeyboard)
			{
				this.references.inputGridHeader2 = this.CreateNewColumnGroup("KeybordHeader", this.references.inputGridHeadersGroup, this._keyboardColMaxWidth).transform;
				ControlMapper.GUILabel guilabel = this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.keyboardColumnLabel, this.references.inputGridHeader2, Vector2.zero);
				guilabel.SetTextAlignment(TextAnchor.MiddleCenter);
			}
			if (this._showMouse)
			{
				this.references.inputGridHeader3 = this.CreateNewColumnGroup("MouseHeader", this.references.inputGridHeadersGroup, this._mouseColMaxWidth).transform;
				ControlMapper.GUILabel guilabel = this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.mouseColumnLabel, this.references.inputGridHeader3, Vector2.zero);
				guilabel.SetTextAlignment(TextAnchor.MiddleCenter);
			}
			if (this._showControllers)
			{
				this.references.inputGridHeader4 = this.CreateNewColumnGroup("ControllerHeader", this.references.inputGridHeadersGroup, this._controllerColMaxWidth).transform;
				ControlMapper.GUILabel guilabel = this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.controllerColumnLabel, this.references.inputGridHeader4, Vector2.zero);
				guilabel.SetTextAlignment(TextAnchor.MiddleCenter);
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000A1A7C File Offset: 0x0009FE7C
		private void CreateActionLabelColumn()
		{
			Transform transform = this.CreateNewColumnGroup("ActionLabelColumn", this.references.inputGridInnerGroup, this._actionLabelWidth).transform;
			this.references.inputGridActionColumn = transform;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000A1AB7 File Offset: 0x0009FEB7
		private void CreateKeyboardInputFieldColumn()
		{
			if (!this._showKeyboard)
			{
				return;
			}
			this.CreateInputFieldColumn("KeyboardColumn", ControllerType.Keyboard, this._keyboardColMaxWidth, this._keyboardInputFieldCount, true);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000A1ADE File Offset: 0x0009FEDE
		private void CreateMouseInputFieldColumn()
		{
			if (!this._showMouse)
			{
				return;
			}
			this.CreateInputFieldColumn("MouseColumn", ControllerType.Mouse, this._mouseColMaxWidth, this._mouseInputFieldCount, false);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000A1B05 File Offset: 0x0009FF05
		private void CreateControllerInputFieldColumn()
		{
			if (!this._showControllers)
			{
				return;
			}
			this.CreateInputFieldColumn("ControllerColumn", ControllerType.Joystick, this._controllerColMaxWidth, this._controllerInputFieldCount, false);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000A1B2C File Offset: 0x0009FF2C
		private void CreateInputFieldColumn(string name, ControllerType controllerType, int maxWidth, int cols, bool disableFullAxis)
		{
			Transform transform = this.CreateNewColumnGroup(name, this.references.inputGridInnerGroup, maxWidth).transform;
			switch (controllerType)
			{
			case ControllerType.Keyboard:
				this.references.inputGridKeyboardColumn = transform;
				break;
			case ControllerType.Mouse:
				this.references.inputGridMouseColumn = transform;
				break;
			case ControllerType.Joystick:
				this.references.inputGridControllerColumn = transform;
				break;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000A1BA4 File Offset: 0x0009FFA4
		private void CreateInputActionLabels()
		{
			Transform inputGridActionColumn = this.references.inputGridActionColumn;
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null && mappingSet.isValid)
				{
					int num = 0;
					if (mappingSet.actionListMode == ControlMapper.MappingSet.ActionListMode.ActionCategory)
					{
						int num2 = 0;
						IList<int> actionCategoryIds = mappingSet.actionCategoryIds;
						for (int j = 0; j < actionCategoryIds.Count; j++)
						{
							InputCategory actionCategory = ReInput.mapping.GetActionCategory(actionCategoryIds[j]);
							if (actionCategory != null)
							{
								if (actionCategory.userAssignable)
								{
									if (this.CountIEnumerable<InputAction>(ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id)) != 0)
									{
										if (this._showActionCategoryLabels)
										{
											if (num2 > 0)
											{
												num -= this._inputRowCategorySpacing;
											}
											ControlMapper.GUILabel guilabel = this.CreateLabel(actionCategory.descriptiveName, inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel.SetFontStyle(FontStyle.Bold);
											guilabel.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
											this.inputGrid.AddActionCategoryLabel(mappingSet.mapCategoryId, actionCategory.id, guilabel);
											num -= this._inputRowHeight;
										}
										foreach (InputAction inputAction in ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id, true))
										{
											if (inputAction.type == InputActionType.Axis)
											{
												if (this._showFullAxisInputFields)
												{
													ControlMapper.GUILabel guilabel2 = this.CreateLabel(inputAction.descriptiveName, inputGridActionColumn, new Vector2(0f, (float)num));
													guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
													this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, AxisRange.Full, guilabel2);
													num -= this._inputRowHeight;
												}
												if (this._showSplitAxisInputFields)
												{
													string labelText = string.IsNullOrEmpty(inputAction.positiveDescriptiveName) ? (inputAction.descriptiveName + " +") : inputAction.positiveDescriptiveName;
													ControlMapper.GUILabel guilabel2 = this.CreateLabel(labelText, inputGridActionColumn, new Vector2(0f, (float)num));
													guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
													this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, AxisRange.Positive, guilabel2);
													num -= this._inputRowHeight;
													string labelText2 = string.IsNullOrEmpty(inputAction.negativeDescriptiveName) ? (inputAction.descriptiveName + " -") : inputAction.negativeDescriptiveName;
													guilabel2 = this.CreateLabel(labelText2, inputGridActionColumn, new Vector2(0f, (float)num));
													guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
													this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, AxisRange.Negative, guilabel2);
													num -= this._inputRowHeight;
												}
											}
											else if (inputAction.type == InputActionType.Button)
											{
												ControlMapper.GUILabel guilabel2 = this.CreateLabel(inputAction.descriptiveName, inputGridActionColumn, new Vector2(0f, (float)num));
												guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, AxisRange.Positive, guilabel2);
												num -= this._inputRowHeight;
											}
										}
										num2++;
									}
								}
							}
						}
					}
					else
					{
						IList<int> actionIds = mappingSet.actionIds;
						for (int k = 0; k < actionIds.Count; k++)
						{
							InputAction action = ReInput.mapping.GetAction(actionIds[k]);
							if (action != null)
							{
								if (action.userAssignable)
								{
									InputCategory actionCategory2 = ReInput.mapping.GetActionCategory(action.categoryId);
									if (actionCategory2 != null)
									{
										if (actionCategory2.userAssignable)
										{
											if (action.type == InputActionType.Axis)
											{
												if (this._showFullAxisInputFields)
												{
													ControlMapper.GUILabel guilabel3 = this.CreateLabel(action.descriptiveName, inputGridActionColumn, new Vector2(0f, (float)num));
													guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
													this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, AxisRange.Full, guilabel3);
													num -= this._inputRowHeight;
												}
												if (this._showSplitAxisInputFields)
												{
													ControlMapper.GUILabel guilabel3 = this.CreateLabel(action.positiveDescriptiveName, inputGridActionColumn, new Vector2(0f, (float)num));
													guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
													this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, AxisRange.Positive, guilabel3);
													num -= this._inputRowHeight;
													guilabel3 = this.CreateLabel(action.negativeDescriptiveName, inputGridActionColumn, new Vector2(0f, (float)num));
													guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
													this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, AxisRange.Negative, guilabel3);
													num -= this._inputRowHeight;
												}
											}
											else if (action.type == InputActionType.Button)
											{
												ControlMapper.GUILabel guilabel3 = this.CreateLabel(action.descriptiveName, inputGridActionColumn, new Vector2(0f, (float)num));
												guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, AxisRange.Positive, guilabel3);
												num -= this._inputRowHeight;
											}
										}
									}
								}
							}
						}
					}
					this.inputGrid.SetColumnHeight(mappingSet.mapCategoryId, (float)(-(float)num));
				}
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000A2160 File Offset: 0x000A0560
		private void CreateInputFields()
		{
			if (this._showControllers)
			{
				this.CreateInputFields(this.references.inputGridControllerColumn, ControllerType.Joystick, this._controllerColMaxWidth, this._controllerInputFieldCount, false);
			}
			if (this._showKeyboard)
			{
				this.CreateInputFields(this.references.inputGridKeyboardColumn, ControllerType.Keyboard, this._keyboardColMaxWidth, this._keyboardInputFieldCount, true);
			}
			if (this._showMouse)
			{
				this.CreateInputFields(this.references.inputGridMouseColumn, ControllerType.Mouse, this._mouseColMaxWidth, this._mouseInputFieldCount, false);
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000A21EC File Offset: 0x000A05EC
		private void CreateInputFields(Transform columnXform, ControllerType controllerType, int maxWidth, int cols, bool disableFullAxis)
		{
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null && mappingSet.isValid)
				{
					int fieldWidth = maxWidth / cols;
					int num = 0;
					int num2 = 0;
					if (mappingSet.actionListMode == ControlMapper.MappingSet.ActionListMode.ActionCategory)
					{
						IList<int> actionCategoryIds = mappingSet.actionCategoryIds;
						for (int j = 0; j < actionCategoryIds.Count; j++)
						{
							InputCategory actionCategory = ReInput.mapping.GetActionCategory(actionCategoryIds[j]);
							if (actionCategory != null)
							{
								if (actionCategory.userAssignable)
								{
									if (this.CountIEnumerable<InputAction>(ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id)) != 0)
									{
										if (this._showActionCategoryLabels)
										{
											num -= ((num2 <= 0) ? this._inputRowHeight : (this._inputRowHeight + this._inputRowCategorySpacing));
										}
										foreach (InputAction inputAction in ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id, true))
										{
											if (inputAction.type == InputActionType.Axis)
											{
												if (this._showFullAxisInputFields)
												{
													this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, AxisRange.Full, controllerType, cols, fieldWidth, ref num, disableFullAxis);
												}
												if (this._showSplitAxisInputFields)
												{
													this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, AxisRange.Positive, controllerType, cols, fieldWidth, ref num, false);
													this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, AxisRange.Negative, controllerType, cols, fieldWidth, ref num, false);
												}
											}
											else if (inputAction.type == InputActionType.Button)
											{
												this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, AxisRange.Positive, controllerType, cols, fieldWidth, ref num, false);
											}
											num2++;
										}
									}
								}
							}
						}
					}
					else
					{
						IList<int> actionIds = mappingSet.actionIds;
						for (int k = 0; k < actionIds.Count; k++)
						{
							InputAction action = ReInput.mapping.GetAction(actionIds[k]);
							if (action != null)
							{
								if (action.userAssignable)
								{
									InputCategory actionCategory2 = ReInput.mapping.GetActionCategory(action.categoryId);
									if (actionCategory2 != null)
									{
										if (actionCategory2.userAssignable)
										{
											if (action.type == InputActionType.Axis)
											{
												if (this._showFullAxisInputFields)
												{
													this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, AxisRange.Full, controllerType, cols, fieldWidth, ref num, disableFullAxis);
												}
												if (this._showSplitAxisInputFields)
												{
													this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, AxisRange.Positive, controllerType, cols, fieldWidth, ref num, false);
													this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, AxisRange.Negative, controllerType, cols, fieldWidth, ref num, false);
												}
											}
											else if (action.type == InputActionType.Button)
											{
												this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, AxisRange.Positive, controllerType, cols, fieldWidth, ref num, false);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000A24FC File Offset: 0x000A08FC
		private void CreateInputFieldSet(Transform parent, int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int cols, int fieldWidth, ref int yPos, bool disableFullAxis)
		{
			GameObject gameObject = this.CreateNewGUIObject("FieldLayoutGroup", parent, new Vector2(0f, (float)yPos));
			HorizontalLayoutGroup horizontalLayoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = new Vector2(0f, 1f);
			component.anchorMax = new Vector2(1f, 1f);
			component.pivot = new Vector2(0f, 1f);
			component.sizeDelta = Vector2.zero;
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
			this.inputGrid.AddInputFieldSet(mapCategoryId, action, axisRange, controllerType, gameObject);
			for (int i = 0; i < cols; i++)
			{
				int num = (axisRange != AxisRange.Full) ? 0 : this._invertToggleWidth;
				ControlMapper.GUIInputField guiinputField = this.CreateInputField(horizontalLayoutGroup.transform, Vector2.zero, string.Empty, action.id, axisRange, controllerType, i);
				guiinputField.SetFirstChildObjectWidth(ControlMapper.LayoutElementSizeType.PreferredSize, fieldWidth - num);
				this.inputGrid.AddInputField(mapCategoryId, action, axisRange, controllerType, i, guiinputField);
				if (axisRange == AxisRange.Full)
				{
					if (!disableFullAxis)
					{
						ControlMapper.GUIToggle guitoggle = this.CreateToggle(this.prefabs.inputGridFieldInvertToggle, horizontalLayoutGroup.transform, Vector2.zero, string.Empty, action.id, axisRange, controllerType, i);
						guitoggle.SetFirstChildObjectWidth(ControlMapper.LayoutElementSizeType.MinSize, num);
						guiinputField.AddToggle(guitoggle);
					}
					else
					{
						guiinputField.SetInteractible(false, false, true);
					}
				}
			}
			yPos -= this._inputRowHeight;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000A2674 File Offset: 0x000A0A74
		private void PopulateInputFields()
		{
			this.inputGrid.InitializeFields(this.currentMapCategoryId);
			if (this.currentPlayer == null)
			{
				return;
			}
			this.inputGrid.SetFieldsActive(this.currentMapCategoryId, true);
			foreach (ControlMapper.InputActionSet actionSet in this.inputGrid.GetActionSets(this.currentMapCategoryId))
			{
				if (this._showKeyboard)
				{
					ControllerType controllerType = ControllerType.Keyboard;
					int controllerId = 0;
					int layoutId = this._keyboardMapDefaultLayout;
					int maxFields = this._keyboardInputFieldCount;
					ControllerMap controllerMapOrCreateNew = this.GetControllerMapOrCreateNew(controllerType, controllerId, layoutId);
					this.PopulateInputFieldGroup(actionSet, controllerMapOrCreateNew, controllerType, controllerId, maxFields);
				}
				if (this._showMouse)
				{
					ControllerType controllerType = ControllerType.Mouse;
					int controllerId = 0;
					int layoutId = this._mouseMapDefaultLayout;
					int maxFields = this._mouseInputFieldCount;
					ControllerMap controllerMapOrCreateNew2 = this.GetControllerMapOrCreateNew(controllerType, controllerId, layoutId);
					if (this.currentPlayer.controllers.hasMouse)
					{
						this.PopulateInputFieldGroup(actionSet, controllerMapOrCreateNew2, controllerType, controllerId, maxFields);
					}
				}
				if (this.isJoystickSelected && this.currentPlayer.controllers.joystickCount > 0)
				{
					ControllerType controllerType = ControllerType.Joystick;
					int controllerId = this.currentJoystick.id;
					int layoutId = this._joystickMapDefaultLayout;
					int maxFields = this._controllerInputFieldCount;
					ControllerMap controllerMapOrCreateNew3 = this.GetControllerMapOrCreateNew(controllerType, controllerId, layoutId);
					this.PopulateInputFieldGroup(actionSet, controllerMapOrCreateNew3, controllerType, controllerId, maxFields);
				}
				else
				{
					this.DisableInputFieldGroup(actionSet, ControllerType.Joystick, this._controllerInputFieldCount);
				}
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000A27FC File Offset: 0x000A0BFC
		private void PopulateInputFieldGroup(ControlMapper.InputActionSet actionSet, ControllerMap controllerMap, ControllerType controllerType, int controllerId, int maxFields)
		{
			if (controllerMap == null)
			{
				return;
			}
			int num = 0;
			this.inputGrid.SetFixedFieldData(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId);
			foreach (ActionElementMap actionElementMap in controllerMap.ElementMapsWithAction(actionSet.actionId))
			{
				if (actionElementMap.elementType == ControllerElementType.Button)
				{
					if (actionSet.axisRange == AxisRange.Full)
					{
						continue;
					}
					if (actionSet.axisRange == AxisRange.Positive)
					{
						if (actionElementMap.axisContribution == Pole.Negative)
						{
							continue;
						}
					}
					else if (actionSet.axisRange == AxisRange.Negative && actionElementMap.axisContribution == Pole.Positive)
					{
						continue;
					}
					this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, actionElementMap.elementIdentifierName, false);
				}
				else if (actionElementMap.elementType == ControllerElementType.Axis)
				{
					if (actionSet.axisRange == AxisRange.Full)
					{
						if (actionElementMap.axisRange != AxisRange.Full)
						{
							continue;
						}
						this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, actionElementMap.elementIdentifierName, actionElementMap.invert);
					}
					else if (actionSet.axisRange == AxisRange.Positive)
					{
						if (actionElementMap.axisRange == AxisRange.Full && ReInput.mapping.GetAction(actionSet.actionId).type != InputActionType.Button)
						{
							continue;
						}
						if (actionElementMap.axisContribution == Pole.Negative)
						{
							continue;
						}
						this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, actionElementMap.elementIdentifierName, false);
					}
					else if (actionSet.axisRange == AxisRange.Negative)
					{
						if (actionElementMap.axisRange == AxisRange.Full)
						{
							continue;
						}
						if (actionElementMap.axisContribution == Pole.Positive)
						{
							continue;
						}
						this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, actionElementMap.elementIdentifierName, false);
					}
				}
				num++;
				if (num > maxFields)
				{
					break;
				}
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000A2A5C File Offset: 0x000A0E5C
		private void DisableInputFieldGroup(ControlMapper.InputActionSet actionSet, ControllerType controllerType, int fieldCount)
		{
			for (int i = 0; i < fieldCount; i++)
			{
				ControlMapper.GUIInputField guiinputField = this.inputGrid.GetGUIInputField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, i);
				if (guiinputField != null)
				{
					guiinputField.SetInteractible(false, false);
				}
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000A2AB0 File Offset: 0x000A0EB0
		private void ResetInputGridScrollBar()
		{
			this.references.inputGridInnerGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			this.references.inputGridVScrollbar.value = 1f;
			this.references.inputGridScrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000A2B00 File Offset: 0x000A0F00
		private void CreateLayout()
		{
			this.references.playersGroup.gameObject.SetActive(this.showPlayers);
			this.references.controllerGroup.gameObject.SetActive(this._showControllers);
			this.references.assignedControllersGroup.gameObject.SetActive(this._showControllers && this.ShowAssignedControllers());
			this.references.settingsAndMapCategoriesGroup.gameObject.SetActive(this.showSettings || this.showMapCategories);
			this.references.settingsGroup.gameObject.SetActive(this.showSettings);
			this.references.mapCategoriesGroup.gameObject.SetActive(this.showMapCategories);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000A2BCB File Offset: 0x000A0FCB
		private void Draw()
		{
			this.DrawPlayersGroup();
			this.DrawControllersGroup();
			this.DrawSettingsGroup();
			this.DrawMapCategoriesGroup();
			this.DrawWindowButtonsGroup();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000A2BEC File Offset: 0x000A0FEC
		private void DrawPlayersGroup()
		{
			if (!this.showPlayers)
			{
				return;
			}
			this.references.playersGroup.labelText = this._language.playersGroupLabel;
			this.references.playersGroup.SetLabelActive(this._showPlayersGroupLabel);
			for (int i = 0; i < this.playerCount; i++)
			{
				Player player = ReInput.players.GetPlayer(i);
				if (player != null)
				{
					GameObject gameObject = UITools.InstantiateGUIObject<ButtonInfo>(this.prefabs.button, this.references.playersGroup.content, "Player" + i + "Button");
					ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(gameObject);
					guibutton.SetLabel(player.descriptiveName);
					guibutton.SetButtonInfoData("PlayerSelection", player.id);
					guibutton.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
					guibutton.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
					this.playerButtons.Add(guibutton);
				}
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000A2CF4 File Offset: 0x000A10F4
		private void DrawControllersGroup()
		{
			if (!this._showControllers)
			{
				return;
			}
			this.references.controllerSettingsGroup.labelText = this._language.controllerSettingsGroupLabel;
			this.references.controllerSettingsGroup.SetLabelActive(this._showControllerGroupLabel);
			this.references.controllerNameLabel.gameObject.SetActive(this._showControllerNameLabel);
			this.references.controllerGroupLabelGroup.gameObject.SetActive(this._showControllerGroupLabel || this._showControllerNameLabel);
			if (this.ShowAssignedControllers())
			{
				this.references.assignedControllersGroup.labelText = this._language.assignedControllersGroupLabel;
				this.references.assignedControllersGroup.SetLabelActive(this._showAssignedControllersGroupLabel);
			}
			ButtonInfo component = this.references.removeControllerButton.GetComponent<ButtonInfo>();
			component.text.text = this._language.removeControllerButtonLabel;
			component = this.references.calibrateControllerButton.GetComponent<ButtonInfo>();
			component.text.text = this._language.calibrateControllerButtonLabel;
			component = this.references.assignControllerButton.GetComponent<ButtonInfo>();
			component.text.text = this._language.assignControllerButtonLabel;
			ControlMapper.GUIButton guibutton = this.CreateButton(this._language.none, this.references.assignedControllersGroup.content, Vector2.zero);
			guibutton.SetInteractible(false, false, true);
			this.assignedControllerButtonsPlaceholder = guibutton;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000A2E6C File Offset: 0x000A126C
		private void DrawSettingsGroup()
		{
			if (!this.showSettings)
			{
				return;
			}
			this.references.settingsGroup.labelText = this._language.settingsGroupLabel;
			this.references.settingsGroup.SetLabelActive(this._showSettingsGroupLabel);
			ControlMapper.GUIButton guibutton = this.CreateButton(this._language.inputBehaviorSettingsButtonLabel, this.references.settingsGroup.content, Vector2.zero);
			this.miscInstantiatedObjects.Add(guibutton.gameObject);
			guibutton.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
			guibutton.SetButtonInfoData("EditInputBehaviors", 0);
			guibutton.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000A2F24 File Offset: 0x000A1324
		private void DrawMapCategoriesGroup()
		{
			if (!this.showMapCategories)
			{
				return;
			}
			if (this._mappingSets == null)
			{
				return;
			}
			this.references.mapCategoriesGroup.labelText = this._language.mapCategoriesGroupLabel;
			this.references.mapCategoriesGroup.SetLabelActive(this._showMapCategoriesGroupLabel);
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null)
				{
					InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(mappingSet.mapCategoryId);
					if (mapCategory != null)
					{
						GameObject gameObject = UITools.InstantiateGUIObject<ButtonInfo>(this.prefabs.button, this.references.mapCategoriesGroup.content, mapCategory.name + "Button");
						ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(gameObject);
						guibutton.SetLabel(mapCategory.descriptiveName);
						guibutton.SetButtonInfoData("MapCategorySelection", mapCategory.id);
						guibutton.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
						guibutton.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
						this.mapCategoryButtons.Add(guibutton);
					}
				}
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000A3054 File Offset: 0x000A1454
		private void DrawWindowButtonsGroup()
		{
			this.references.doneButton.GetComponent<ButtonInfo>().text.text = this._language.doneButtonLabel;
			this.references.restoreDefaultsButton.GetComponent<ButtonInfo>().text.text = this._language.restoreDefaultsButtonLabel;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000A30AC File Offset: 0x000A14AC
		private void Redraw(bool listsChanged, bool playTransitions)
		{
			this.RedrawPlayerGroup(playTransitions);
			this.RedrawControllerGroup();
			this.RedrawMapCategoriesGroup(playTransitions);
			this.RedrawInputGrid(listsChanged);
			if (this.currentUISelection == null || !this.currentUISelection.activeInHierarchy)
			{
				this.RestoreLastUISelection();
			}
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000A30FC File Offset: 0x000A14FC
		private void RedrawPlayerGroup(bool playTransitions)
		{
			if (!this.showPlayers)
			{
				return;
			}
			for (int i = 0; i < this.playerButtons.Count; i++)
			{
				bool state = this.currentPlayerId != this.playerButtons[i].buttonInfo.intData;
				this.playerButtons[i].SetInteractible(state, playTransitions);
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000A3168 File Offset: 0x000A1568
		private void RedrawControllerGroup()
		{
			int num = -1;
			this.references.controllerNameLabel.text = this._language.none;
			UITools.SetInteractable(this.references.removeControllerButton, false, false);
			UITools.SetInteractable(this.references.assignControllerButton, false, false);
			UITools.SetInteractable(this.references.calibrateControllerButton, false, false);
			if (this.ShowAssignedControllers())
			{
				foreach (ControlMapper.GUIButton guibutton in this.assignedControllerButtons)
				{
					if (!(guibutton.gameObject == null))
					{
						if (this.currentUISelection == guibutton.gameObject)
						{
							num = guibutton.buttonInfo.intData;
						}
						UnityEngine.Object.Destroy(guibutton.gameObject);
					}
				}
				this.assignedControllerButtons.Clear();
				this.assignedControllerButtonsPlaceholder.SetActive(true);
			}
			Player player = ReInput.players.GetPlayer(this.currentPlayerId);
			if (player == null)
			{
				return;
			}
			if (this.ShowAssignedControllers())
			{
				if (player.controllers.joystickCount > 0)
				{
					this.assignedControllerButtonsPlaceholder.SetActive(false);
				}
				foreach (Joystick joystick in player.controllers.Joysticks)
				{
					ControlMapper.GUIButton guibutton2 = this.CreateButton(joystick.name, this.references.assignedControllersGroup.content, Vector2.zero);
					guibutton2.SetButtonInfoData("AssignedControllerSelection", joystick.id);
					guibutton2.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
					guibutton2.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
					this.assignedControllerButtons.Add(guibutton2);
					if (joystick.id == this.currentJoystickId)
					{
						guibutton2.SetInteractible(false, true);
					}
				}
				if (player.controllers.joystickCount > 0 && !this.isJoystickSelected)
				{
					this.currentJoystickId = player.controllers.Joysticks[0].id;
					this.assignedControllerButtons[0].SetInteractible(false, false);
				}
				if (num >= 0)
				{
					foreach (ControlMapper.GUIButton guibutton3 in this.assignedControllerButtons)
					{
						if (guibutton3.buttonInfo.intData == num)
						{
							this.SetUISelection(guibutton3.gameObject);
							break;
						}
					}
				}
			}
			else if (player.controllers.joystickCount > 0 && !this.isJoystickSelected)
			{
				this.currentJoystickId = player.controllers.Joysticks[0].id;
			}
			if (this.isJoystickSelected && player.controllers.joystickCount > 0)
			{
				this.references.removeControllerButton.interactable = true;
				this.references.controllerNameLabel.text = this.currentJoystick.name;
				if (this.currentJoystick.axisCount > 0)
				{
					this.references.calibrateControllerButton.interactable = true;
				}
			}
			int joystickCount = player.controllers.joystickCount;
			int joystickCount2 = ReInput.controllers.joystickCount;
			int maxControllersPerPlayer = this.GetMaxControllersPerPlayer();
			bool flag = maxControllersPerPlayer == 0;
			if (joystickCount2 > 0 && joystickCount < joystickCount2 && (maxControllersPerPlayer == 1 || flag || joystickCount < maxControllersPerPlayer))
			{
				UITools.SetInteractable(this.references.assignControllerButton, true, false);
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000A3554 File Offset: 0x000A1954
		private void RedrawMapCategoriesGroup(bool playTransitions)
		{
			if (!this.showMapCategories)
			{
				return;
			}
			for (int i = 0; i < this.mapCategoryButtons.Count; i++)
			{
				bool state = this.currentMapCategoryId != this.mapCategoryButtons[i].buttonInfo.intData;
				this.mapCategoryButtons[i].SetInteractible(state, playTransitions);
			}
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000A35BE File Offset: 0x000A19BE
		private void RedrawInputGrid(bool listsChanged)
		{
			if (listsChanged)
			{
				this.RefreshInputGridStructure();
			}
			this.PopulateInputFields();
			if (listsChanged)
			{
				this.ResetInputGridScrollBar();
			}
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000A35DE File Offset: 0x000A19DE
		private void ForceRefresh()
		{
			if (this.windowManager.isWindowOpen)
			{
				this.CloseAllWindows();
			}
			else
			{
				this.Redraw(false, false);
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000A3603 File Offset: 0x000A1A03
		private void CreateInputCategoryRow(ref int rowCount, InputCategory category)
		{
			this.CreateLabel(category.descriptiveName, this.references.inputGridActionColumn, new Vector2(0f, (float)(rowCount * this._inputRowHeight) * -1f));
			rowCount++;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000A363D File Offset: 0x000A1A3D
		private ControlMapper.GUILabel CreateLabel(string labelText, Transform parent, Vector2 offset)
		{
			return this.CreateLabel(this.prefabs.inputGridLabel, labelText, parent, offset);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000A3654 File Offset: 0x000A1A54
		private ControlMapper.GUILabel CreateLabel(GameObject prefab, string labelText, Transform parent, Vector2 offset)
		{
			GameObject gameObject = this.InstantiateGUIObject(prefab, parent, offset);
			Text componentInSelfOrChildren = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
			if (componentInSelfOrChildren == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: Label prefab is missing Text component!");
				return null;
			}
			componentInSelfOrChildren.text = labelText;
			return new ControlMapper.GUILabel(gameObject);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000A3698 File Offset: 0x000A1A98
		private ControlMapper.GUIButton CreateButton(string labelText, Transform parent, Vector2 offset)
		{
			ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(this.InstantiateGUIObject(this.prefabs.button, parent, offset));
			guibutton.SetLabel(labelText);
			return guibutton;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000A36C8 File Offset: 0x000A1AC8
		private ControlMapper.GUIButton CreateFitButton(string labelText, Transform parent, Vector2 offset)
		{
			ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(this.InstantiateGUIObject(this.prefabs.fitButton, parent, offset));
			guibutton.SetLabel(labelText);
			return guibutton;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000A36F8 File Offset: 0x000A1AF8
		private ControlMapper.GUIInputField CreateInputField(Transform parent, Vector2 offset, string label, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
		{
			ControlMapper.GUIInputField guiinputField = this.CreateInputField(parent, offset);
			guiinputField.SetLabel(string.Empty);
			guiinputField.SetFieldInfoData(actionId, axisRange, controllerType, fieldIndex);
			guiinputField.SetOnClickCallback(this.inputFieldActivatedDelegate);
			guiinputField.fieldInfo.OnSelectedEvent += this.OnUIElementSelected;
			return guiinputField;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000A374B File Offset: 0x000A1B4B
		private ControlMapper.GUIInputField CreateInputField(Transform parent, Vector2 offset)
		{
			return new ControlMapper.GUIInputField(this.InstantiateGUIObject(this.prefabs.inputGridFieldButton, parent, offset));
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000A3768 File Offset: 0x000A1B68
		private ControlMapper.GUIToggle CreateToggle(GameObject prefab, Transform parent, Vector2 offset, string label, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
		{
			ControlMapper.GUIToggle guitoggle = this.CreateToggle(prefab, parent, offset);
			guitoggle.SetToggleInfoData(actionId, axisRange, controllerType, fieldIndex);
			guitoggle.SetOnSubmitCallback(this.inputFieldInvertToggleStateChangedDelegate);
			guitoggle.toggleInfo.OnSelectedEvent += this.OnUIElementSelected;
			return guitoggle;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000A37B1 File Offset: 0x000A1BB1
		private ControlMapper.GUIToggle CreateToggle(GameObject prefab, Transform parent, Vector2 offset)
		{
			return new ControlMapper.GUIToggle(this.InstantiateGUIObject(prefab, parent, offset));
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000A37C4 File Offset: 0x000A1BC4
		private GameObject InstantiateGUIObject(GameObject prefab, Transform parent, Vector2 offset)
		{
			if (prefab == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: Prefab is null!");
				return null;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
			return this.InitializeNewGUIGameObject(gameObject, parent, offset);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000A37FC File Offset: 0x000A1BFC
		private GameObject CreateNewGUIObject(string name, Transform parent, Vector2 offset)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = name;
			gameObject.AddComponent<RectTransform>();
			return this.InitializeNewGUIGameObject(gameObject, parent, offset);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000A3828 File Offset: 0x000A1C28
		private GameObject InitializeNewGUIGameObject(GameObject gameObject, Transform parent, Vector2 offset)
		{
			if (gameObject == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: GameObject is null!");
				return null;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			if (component == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: GameObject does not have a RectTransform component!");
				return gameObject;
			}
			if (parent != null)
			{
				component.SetParent(parent, false);
			}
			component.anchoredPosition = offset;
			return gameObject;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000A3888 File Offset: 0x000A1C88
		private GameObject CreateNewColumnGroup(string name, Transform parent, int maxWidth)
		{
			GameObject gameObject = this.CreateNewGUIObject(name, parent, Vector2.zero);
			this.inputGrid.AddGroup(gameObject);
			LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
			if (maxWidth >= 0)
			{
				layoutElement.preferredWidth = (float)maxWidth;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = new Vector2(0f, 0f);
			component.anchorMax = new Vector2(1f, 0f);
			return gameObject;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000A38F7 File Offset: 0x000A1CF7
		private Window OpenWindow(bool closeOthers)
		{
			return this.OpenWindow(string.Empty, closeOthers);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000A3908 File Offset: 0x000A1D08
		private Window OpenWindow(string name, bool closeOthers)
		{
			if (closeOthers)
			{
				this.windowManager.CancelAll();
			}
			Window window = this.windowManager.OpenWindow(name, this._defaultWindowWidth, this._defaultWindowHeight);
			if (window == null)
			{
				return null;
			}
			this.ChildWindowOpened();
			return window;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000A3954 File Offset: 0x000A1D54
		private Window OpenWindow(GameObject windowPrefab, bool closeOthers)
		{
			return this.OpenWindow(windowPrefab, string.Empty, closeOthers);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000A3964 File Offset: 0x000A1D64
		private Window OpenWindow(GameObject windowPrefab, string name, bool closeOthers)
		{
			if (closeOthers)
			{
				this.windowManager.CancelAll();
			}
			Window window = this.windowManager.OpenWindow(windowPrefab, name);
			if (window == null)
			{
				return null;
			}
			this.ChildWindowOpened();
			return window;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000A39A8 File Offset: 0x000A1DA8
		private void OpenModal(string title, string message, string confirmText, Action<int> confirmAction, string cancelText, Action<int> cancelAction, bool closeOthers)
		{
			Window window = this.OpenWindow(closeOthers);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, title);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), message);
			UnityAction unityAction = delegate()
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, confirmText, delegate()
			{
				this.OnRestoreDefaultsConfirmed(window.id);
			}, unityAction, false);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, cancelText, unityAction, unityAction, true);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000A3AB6 File Offset: 0x000A1EB6
		private void CloseWindow(int windowId)
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CloseWindow(windowId);
			this.ChildWindowClosed();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000A3ADB File Offset: 0x000A1EDB
		private void CloseTopWindow()
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CloseTop();
			this.ChildWindowClosed();
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000A3AFF File Offset: 0x000A1EFF
		private void CloseAllWindows()
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CancelAll();
			this.ChildWindowClosed();
			this.InputPollingStopped();
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000A3B2C File Offset: 0x000A1F2C
		private void ChildWindowOpened()
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.SetIsFocused(false);
			if (this._PopupWindowOpenedEvent != null)
			{
				this._PopupWindowOpenedEvent();
			}
			if (this._onPopupWindowOpened != null)
			{
				this._onPopupWindowOpened.Invoke();
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000A3B80 File Offset: 0x000A1F80
		private void ChildWindowClosed()
		{
			if (this.windowManager.isWindowOpen)
			{
				return;
			}
			this.SetIsFocused(true);
			if (this._PopupWindowClosedEvent != null)
			{
				this._PopupWindowClosedEvent();
			}
			if (this._onPopupWindowClosed != null)
			{
				this._onPopupWindowClosed.Invoke();
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000A3BD4 File Offset: 0x000A1FD4
		private bool HasElementAssignmentConflicts(Player player, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (player == null || mapping == null)
			{
				return false;
			}
			ElementAssignmentConflictCheck conflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out conflictCheck))
			{
				return false;
			}
			if (skipOtherPlayers)
			{
				return ReInput.players.SystemPlayer.controllers.conflictChecking.DoesElementAssignmentConflict(conflictCheck) || player.controllers.conflictChecking.DoesElementAssignmentConflict(conflictCheck);
			}
			return ReInput.controllers.conflictChecking.DoesElementAssignmentConflict(conflictCheck);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000A3C54 File Offset: 0x000A2054
		private bool IsBlockingAssignmentConflict(ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			ElementAssignmentConflictCheck conflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out conflictCheck))
			{
				return false;
			}
			if (skipOtherPlayers)
			{
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo in ReInput.players.SystemPlayer.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!elementAssignmentConflictInfo.isUserAssignable)
					{
						return true;
					}
				}
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo2 in this.currentPlayer.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!elementAssignmentConflictInfo2.isUserAssignable)
					{
						return true;
					}
				}
			}
			else
			{
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo3 in ReInput.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!elementAssignmentConflictInfo3.isUserAssignable)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000A3DBC File Offset: 0x000A21BC
		private IEnumerable<ElementAssignmentConflictInfo> ElementAssignmentConflicts(Player player, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (player == null || mapping == null)
			{
				yield break;
			}
			ElementAssignmentConflictCheck conflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out conflictCheck))
			{
				yield break;
			}
			if (skipOtherPlayers)
			{
				foreach (ElementAssignmentConflictInfo conflict in ReInput.players.SystemPlayer.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!conflict.isUserAssignable)
					{
						yield return conflict;
					}
				}
				foreach (ElementAssignmentConflictInfo conflict2 in player.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!conflict2.isUserAssignable)
					{
						yield return conflict2;
					}
				}
			}
			else
			{
				foreach (ElementAssignmentConflictInfo conflict3 in ReInput.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!conflict3.isUserAssignable)
					{
						yield return conflict3;
					}
				}
			}
			yield break;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x000A3DFC File Offset: 0x000A21FC
		private bool CreateConflictCheck(ControlMapper.InputMapping mapping, ElementAssignment assignment, out ElementAssignmentConflictCheck conflictCheck)
		{
			if (mapping == null || this.currentPlayer == null)
			{
				conflictCheck = default(ElementAssignmentConflictCheck);
				return false;
			}
			conflictCheck = assignment.ToElementAssignmentConflictCheck();
			conflictCheck.playerId = this.currentPlayer.id;
			conflictCheck.controllerType = mapping.controllerType;
			conflictCheck.controllerId = mapping.controllerId;
			conflictCheck.controllerMapId = mapping.map.id;
			conflictCheck.controllerMapCategoryId = mapping.map.categoryId;
			if (mapping.aem != null)
			{
				conflictCheck.elementMapId = mapping.aem.id;
			}
			return true;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000A3E98 File Offset: 0x000A2298
		private void PollKeyboardForAssignment(out ControllerPollingInfo pollingInfo, out bool modifierKeyPressed, out ModifierKeyFlags modifierFlags, out string label)
		{
			pollingInfo = default(ControllerPollingInfo);
			label = string.Empty;
			modifierKeyPressed = false;
			modifierFlags = ModifierKeyFlags.None;
			int num = 0;
			ControllerPollingInfo controllerPollingInfo = default(ControllerPollingInfo);
			ControllerPollingInfo controllerPollingInfo2 = default(ControllerPollingInfo);
			ModifierKeyFlags modifierKeyFlags = ModifierKeyFlags.None;
			foreach (ControllerPollingInfo controllerPollingInfo3 in ReInput.controllers.Keyboard.PollForAllKeys())
			{
				KeyCode keyboardKey = controllerPollingInfo3.keyboardKey;
				if (keyboardKey != KeyCode.AltGr)
				{
					if (Keyboard.IsModifierKey(controllerPollingInfo3.keyboardKey))
					{
						if (num == 0)
						{
							controllerPollingInfo2 = controllerPollingInfo3;
						}
						modifierKeyFlags |= Keyboard.KeyCodeToModifierKeyFlags(keyboardKey);
						num++;
					}
					else if (controllerPollingInfo.keyboardKey == KeyCode.None)
					{
						controllerPollingInfo = controllerPollingInfo3;
					}
				}
			}
			if (controllerPollingInfo.keyboardKey == KeyCode.None)
			{
				if (num > 0)
				{
					modifierKeyPressed = true;
					if (num == 1)
					{
						if (ReInput.controllers.Keyboard.GetKeyTimePressed(controllerPollingInfo2.keyboardKey) > 1f)
						{
							pollingInfo = controllerPollingInfo2;
							return;
						}
						label = Keyboard.GetKeyName(controllerPollingInfo2.keyboardKey);
					}
					else
					{
						label = Keyboard.ModifierKeyFlagsToString(modifierKeyFlags);
					}
				}
				return;
			}
			if (!ReInput.controllers.Keyboard.GetKeyDown(controllerPollingInfo.keyboardKey))
			{
				return;
			}
			if (num == 0)
			{
				pollingInfo = controllerPollingInfo;
				return;
			}
			pollingInfo = controllerPollingInfo;
			modifierFlags = modifierKeyFlags;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000A4014 File Offset: 0x000A2414
		private bool GetFirstElementAssignmentConflict(ElementAssignmentConflictCheck conflictCheck, out ElementAssignmentConflictInfo conflict, bool skipOtherPlayers)
		{
			if (this.GetFirstElementAssignmentConflict(this.currentPlayer, conflictCheck, out conflict))
			{
				return true;
			}
			if (this.GetFirstElementAssignmentConflict(ReInput.players.SystemPlayer, conflictCheck, out conflict))
			{
				return true;
			}
			if (!skipOtherPlayers)
			{
				IList<Player> players = ReInput.players.Players;
				for (int i = 0; i < players.Count; i++)
				{
					Player player = players[i];
					if (player != this.currentPlayer)
					{
						if (this.GetFirstElementAssignmentConflict(player, conflictCheck, out conflict))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000A40A4 File Offset: 0x000A24A4
		private bool GetFirstElementAssignmentConflict(Player player, ElementAssignmentConflictCheck conflictCheck, out ElementAssignmentConflictInfo conflict)
		{
			using (IEnumerator<ElementAssignmentConflictInfo> enumerator = player.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ElementAssignmentConflictInfo elementAssignmentConflictInfo = enumerator.Current;
					conflict = elementAssignmentConflictInfo;
					return true;
				}
			}
			conflict = default(ElementAssignmentConflictInfo);
			return false;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000A411C File Offset: 0x000A251C
		private void StartAxisCalibration(int axisIndex)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this.currentPlayer.controllers.joystickCount == 0)
			{
				return;
			}
			Joystick currentJoystick = this.currentJoystick;
			if (axisIndex < 0 || axisIndex >= currentJoystick.axisCount)
			{
				return;
			}
			this.pendingAxisCalibration = new ControlMapper.AxisCalibrator(currentJoystick, axisIndex);
			this.ShowCalibrateAxisStep1Window();
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000A4179 File Offset: 0x000A2579
		private void EndAxisCalibration()
		{
			if (this.pendingAxisCalibration == null)
			{
				return;
			}
			this.pendingAxisCalibration.Commit();
			this.pendingAxisCalibration = null;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000A4199 File Offset: 0x000A2599
		private void SetUISelection(GameObject selection)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(selection);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000A41B7 File Offset: 0x000A25B7
		private void RestoreLastUISelection()
		{
			if (this.lastUISelection == null || !this.lastUISelection.activeInHierarchy)
			{
				this.SetDefaultUISelection();
				return;
			}
			this.SetUISelection(this.lastUISelection);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000A41F0 File Offset: 0x000A25F0
		private void SetDefaultUISelection()
		{
			if (!this.isOpen)
			{
				return;
			}
			if (this.references.defaultSelection == null)
			{
				this.SetUISelection(null);
			}
			else
			{
				this.SetUISelection(this.references.defaultSelection.gameObject);
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000A4244 File Offset: 0x000A2644
		private void SelectDefaultMapCategory(bool redraw)
		{
			this.currentMapCategoryId = this.GetDefaultMapCategoryId();
			this.OnMapCategorySelected(this.currentMapCategoryId, redraw);
			if (!this.showMapCategories)
			{
				return;
			}
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				if (ReInput.mapping.GetMapCategory(this._mappingSets[i].mapCategoryId) != null)
				{
					this.currentMapCategoryId = this._mappingSets[i].mapCategoryId;
					break;
				}
			}
			if (this.currentMapCategoryId < 0)
			{
				return;
			}
			for (int j = 0; j < this._mappingSets.Length; j++)
			{
				bool state = this._mappingSets[j].mapCategoryId != this.currentMapCategoryId;
				this.mapCategoryButtons[j].SetInteractible(state, false);
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000A4323 File Offset: 0x000A2723
		private void CheckUISelection()
		{
			if (!this.isFocused)
			{
				return;
			}
			if (this.currentUISelection == null)
			{
				this.RestoreLastUISelection();
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000A4348 File Offset: 0x000A2748
		private void OnUIElementSelected(GameObject selectedObject)
		{
			this.lastUISelection = selectedObject;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000A4351 File Offset: 0x000A2751
		private void SetIsFocused(bool state)
		{
			this.references.mainCanvasGroup.interactable = state;
			if (state)
			{
				this.Redraw(false, false);
				this.RestoreLastUISelection();
				this.blockInputOnFocusEndTime = Time.unscaledTime + 0.1f;
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000A4389 File Offset: 0x000A2789
		public void Toggle()
		{
			if (this.isOpen)
			{
				this.Close(true);
			}
			else
			{
				this.Open();
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000A43A8 File Offset: 0x000A27A8
		public void Open()
		{
			this.Open(false);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000A43B4 File Offset: 0x000A27B4
		private void Open(bool force)
		{
			if (!this.initialized)
			{
				this.Initialize();
			}
			if (!this.initialized)
			{
				return;
			}
			if (!force && this.isOpen)
			{
				return;
			}
			this.Clear();
			this.canvas.SetActive(true);
			this.OnPlayerSelected(0, false);
			this.SelectDefaultMapCategory(false);
			this.SetDefaultUISelection();
			this.Redraw(true, false);
			if (this._ScreenOpenedEvent != null)
			{
				this._ScreenOpenedEvent();
			}
			if (this._onScreenOpened != null)
			{
				this._onScreenOpened.Invoke();
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000A444C File Offset: 0x000A284C
		public void Close(bool save)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.isOpen)
			{
				return;
			}
			if (save && ReInput.userDataStore != null)
			{
				ReInput.userDataStore.Save();
			}
			this.Clear();
			this.canvas.SetActive(false);
			this.SetUISelection(null);
			if (this._ScreenClosedEvent != null)
			{
				this._ScreenClosedEvent();
			}
			if (this._onScreenClosed != null)
			{
				this._onScreenClosed.Invoke();
			}
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000A44D0 File Offset: 0x000A28D0
		private void Clear()
		{
			this.windowManager.CancelAll();
			this.lastUISelection = null;
			this.pendingInputMapping = null;
			this.pendingAxisCalibration = null;
			this.InputPollingStopped();
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000A44F8 File Offset: 0x000A28F8
		private void ClearCompletely()
		{
			this.ClearSpawnedObjects();
			this.ClearAllVars();
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000A4508 File Offset: 0x000A2908
		private void ClearSpawnedObjects()
		{
			this.windowManager.ClearCompletely();
			this.inputGrid.ClearAll();
			foreach (ControlMapper.GUIButton guibutton in this.playerButtons)
			{
				UnityEngine.Object.Destroy(guibutton.gameObject);
			}
			this.playerButtons.Clear();
			foreach (ControlMapper.GUIButton guibutton2 in this.mapCategoryButtons)
			{
				UnityEngine.Object.Destroy(guibutton2.gameObject);
			}
			this.mapCategoryButtons.Clear();
			foreach (ControlMapper.GUIButton guibutton3 in this.assignedControllerButtons)
			{
				UnityEngine.Object.Destroy(guibutton3.gameObject);
			}
			this.assignedControllerButtons.Clear();
			if (this.assignedControllerButtonsPlaceholder != null)
			{
				UnityEngine.Object.Destroy(this.assignedControllerButtonsPlaceholder.gameObject);
				this.assignedControllerButtonsPlaceholder = null;
			}
			foreach (GameObject obj in this.miscInstantiatedObjects)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.miscInstantiatedObjects.Clear();
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000A46BC File Offset: 0x000A2ABC
		private void ClearVarsOnPlayerChange()
		{
			this.currentJoystickId = -1;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000A46C5 File Offset: 0x000A2AC5
		private void ClearVarsOnJoystickChange()
		{
			this.currentJoystickId = -1;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000A46D0 File Offset: 0x000A2AD0
		private void ClearAllVars()
		{
			this.initialized = false;
			ControlMapper.Instance = null;
			this.playerCount = 0;
			this.inputGrid = null;
			this.windowManager = null;
			this.currentPlayerId = -1;
			this.currentMapCategoryId = -1;
			this.playerButtons = null;
			this.mapCategoryButtons = null;
			this.miscInstantiatedObjects = null;
			this.canvas = null;
			this.lastUISelection = null;
			this.currentJoystickId = -1;
			this.pendingInputMapping = null;
			this.pendingAxisCalibration = null;
			this.inputFieldActivatedDelegate = null;
			this.inputFieldInvertToggleStateChangedDelegate = null;
			this.isPollingForInput = false;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000A475A File Offset: 0x000A2B5A
		public void Reset()
		{
			if (!this.initialized)
			{
				return;
			}
			this.ClearCompletely();
			this.Initialize();
			if (this.isOpen)
			{
				this.Open(true);
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000A4788 File Offset: 0x000A2B88
		private void SetActionAxisInverted(bool state, ControllerType controllerType, int actionElementMapId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			ControllerMapWithAxes controllerMapWithAxes = this.GetControllerMap(controllerType) as ControllerMapWithAxes;
			if (controllerMapWithAxes == null)
			{
				return;
			}
			ActionElementMap elementMap = controllerMapWithAxes.GetElementMap(actionElementMapId);
			if (elementMap == null)
			{
				return;
			}
			elementMap.invert = state;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000A47CC File Offset: 0x000A2BCC
		private ControllerMap GetControllerMap(ControllerType type)
		{
			if (this.currentPlayer == null)
			{
				return null;
			}
			int controllerId = 0;
			switch (type)
			{
			case ControllerType.Keyboard:
				break;
			case ControllerType.Mouse:
				break;
			case ControllerType.Joystick:
				if (this.currentPlayer.controllers.joystickCount <= 0)
				{
					return null;
				}
				controllerId = this.currentJoystick.id;
				break;
			default:
				throw new NotImplementedException();
			}
			return this.currentPlayer.controllers.maps.GetFirstMapInCategory(type, controllerId, this.currentMapCategoryId);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000A485C File Offset: 0x000A2C5C
		private ControllerMap GetControllerMapOrCreateNew(ControllerType controllerType, int controllerId, int layoutId)
		{
			ControllerMap controllerMap = this.GetControllerMap(controllerType);
			if (controllerMap == null)
			{
				this.currentPlayer.controllers.maps.AddEmptyMap(controllerType, controllerId, this.currentMapCategoryId, layoutId);
				controllerMap = this.currentPlayer.controllers.maps.GetMap(controllerType, controllerId, this.currentMapCategoryId, layoutId);
			}
			return controllerMap;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000A48B8 File Offset: 0x000A2CB8
		private int CountIEnumerable<T>(IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return 0;
			}
			IEnumerator<T> enumerator = enumerable.GetEnumerator();
			if (enumerator == null)
			{
				return 0;
			}
			int num = 0;
			while (enumerator.MoveNext())
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000A48F4 File Offset: 0x000A2CF4
		private int GetDefaultMapCategoryId()
		{
			if (this._mappingSets.Length == 0)
			{
				return 0;
			}
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				if (ReInput.mapping.GetMapCategory(this._mappingSets[i].mapCategoryId) != null)
				{
					return this._mappingSets[i].mapCategoryId;
				}
			}
			return 0;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000A495C File Offset: 0x000A2D5C
		private void SubscribeFixedUISelectionEvents()
		{
			if (this.references.fixedSelectableUIElements == null)
			{
				return;
			}
			foreach (GameObject gameObject in this.references.fixedSelectableUIElements)
			{
				UIElementInfo component = UnityTools.GetComponent<UIElementInfo>(gameObject);
				if (!(component == null))
				{
					component.OnSelectedEvent += this.OnUIElementSelected;
				}
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000A49C8 File Offset: 0x000A2DC8
		private void SubscribeMenuControlInputEvents()
		{
			this.SubscribeRewiredInputEventAllPlayers(this._screenToggleAction, new Action<InputActionEventData>(this.OnScreenToggleActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._screenOpenAction, new Action<InputActionEventData>(this.OnScreenOpenActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._screenCloseAction, new Action<InputActionEventData>(this.OnScreenCloseActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._universalCancelAction, new Action<InputActionEventData>(this.OnUniversalCancelActionPressed));
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000A4A38 File Offset: 0x000A2E38
		private void UnsubscribeMenuControlInputEvents()
		{
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenToggleAction, new Action<InputActionEventData>(this.OnScreenToggleActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenOpenAction, new Action<InputActionEventData>(this.OnScreenOpenActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenCloseAction, new Action<InputActionEventData>(this.OnScreenCloseActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._universalCancelAction, new Action<InputActionEventData>(this.OnUniversalCancelActionPressed));
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000A4AA8 File Offset: 0x000A2EA8
		private void SubscribeRewiredInputEventAllPlayers(int actionId, Action<InputActionEventData> callback)
		{
			if (actionId < 0 || callback == null)
			{
				return;
			}
			if (ReInput.mapping.GetAction(actionId) == null)
			{
				UnityEngine.Debug.LogWarning("Rewired Control Mapper: " + actionId + " is not a valid Action id!");
				return;
			}
			foreach (Player player in ReInput.players.AllPlayers)
			{
				player.AddInputEventDelegate(callback, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, actionId);
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000A4B44 File Offset: 0x000A2F44
		private void UnsubscribeRewiredInputEventAllPlayers(int actionId, Action<InputActionEventData> callback)
		{
			if (actionId < 0 || callback == null)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			if (ReInput.mapping.GetAction(actionId) == null)
			{
				UnityEngine.Debug.LogWarning("Rewired Control Mapper: " + actionId + " is not a valid Action id!");
				return;
			}
			foreach (Player player in ReInput.players.AllPlayers)
			{
				player.RemoveInputEventDelegate(callback, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, actionId);
			}
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000A4BE8 File Offset: 0x000A2FE8
		private int GetMaxControllersPerPlayer()
		{
			if (this._rewiredInputManager.userData.ConfigVars.autoAssignJoysticks)
			{
				return this._rewiredInputManager.userData.ConfigVars.maxJoysticksPerPlayer;
			}
			return this._maxControllersPerPlayer;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000A4C20 File Offset: 0x000A3020
		private bool ShowAssignedControllers()
		{
			return this._showControllers && (this._showAssignedControllers || this.GetMaxControllersPerPlayer() != 1);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000A4C4B File Offset: 0x000A304B
		private void InspectorPropertyChanged(bool reset = false)
		{
			if (reset)
			{
				this.Reset();
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000A4C5C File Offset: 0x000A305C
		private void AssignController(Player player, int controllerId)
		{
			if (player == null)
			{
				return;
			}
			if (player.controllers.ContainsController(ControllerType.Joystick, controllerId))
			{
				return;
			}
			if (this.GetMaxControllersPerPlayer() == 1)
			{
				this.RemoveAllControllers(player);
				this.ClearVarsOnJoystickChange();
			}
			foreach (Player player2 in ReInput.players.Players)
			{
				if (player2 != player)
				{
					this.RemoveController(player2, controllerId);
				}
			}
			player.controllers.AddController(ControllerType.Joystick, controllerId, false);
			if (ReInput.userDataStore != null)
			{
				ReInput.userDataStore.LoadControllerData(player.id, ControllerType.Joystick, controllerId);
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000A4D24 File Offset: 0x000A3124
		private void RemoveAllControllers(Player player)
		{
			if (player == null)
			{
				return;
			}
			IList<Joystick> joysticks = player.controllers.Joysticks;
			for (int i = joysticks.Count - 1; i >= 0; i--)
			{
				this.RemoveController(player, joysticks[i].id);
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000A4D70 File Offset: 0x000A3170
		private void RemoveController(Player player, int controllerId)
		{
			if (player == null)
			{
				return;
			}
			if (!player.controllers.ContainsController(ControllerType.Joystick, controllerId))
			{
				return;
			}
			if (ReInput.userDataStore != null)
			{
				ReInput.userDataStore.SaveControllerData(player.id, ControllerType.Joystick, controllerId);
			}
			player.controllers.RemoveController(ControllerType.Joystick, controllerId);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000A4DC0 File Offset: 0x000A31C0
		private bool IsAllowedAssignment(ControlMapper.InputMapping pendingInputMapping, ControllerPollingInfo pollingInfo)
		{
			return pendingInputMapping != null && (pendingInputMapping.axisRange != AxisRange.Full || this._showSplitAxisInputFields || pollingInfo.elementType != ControllerElementType.Button);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000A4DF0 File Offset: 0x000A31F0
		private void InputPollingStarted()
		{
			bool flag = this.isPollingForInput;
			this.isPollingForInput = true;
			if (!flag)
			{
				if (this._InputPollingStartedEvent != null)
				{
					this._InputPollingStartedEvent();
				}
				if (this._onInputPollingStarted != null)
				{
					this._onInputPollingStarted.Invoke();
				}
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000A4E40 File Offset: 0x000A3240
		private void InputPollingStopped()
		{
			bool flag = this.isPollingForInput;
			this.isPollingForInput = false;
			if (flag)
			{
				if (this._InputPollingEndedEvent != null)
				{
					this._InputPollingEndedEvent();
				}
				if (this._onInputPollingEnded != null)
				{
					this._onInputPollingEnded.Invoke();
				}
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000A4E8D File Offset: 0x000A328D
		private int GetControllerInputFieldCount(ControllerType controllerType)
		{
			switch (controllerType)
			{
			case ControllerType.Keyboard:
				return this._keyboardInputFieldCount;
			case ControllerType.Mouse:
				return this._mouseInputFieldCount;
			case ControllerType.Joystick:
				return this._controllerInputFieldCount;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000A4EC0 File Offset: 0x000A32C0
		private bool ShowSwapButton(int windowId, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (this.currentPlayer == null)
			{
				return false;
			}
			if (!this._allowElementAssignmentSwap)
			{
				return false;
			}
			if (mapping == null || mapping.aem == null)
			{
				return false;
			}
			ElementAssignmentConflictCheck conflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out conflictCheck))
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: Error creating conflict check!");
				return false;
			}
			List<ElementAssignmentConflictInfo> list = new List<ElementAssignmentConflictInfo>();
			list.AddRange(this.currentPlayer.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck));
			list.AddRange(ReInput.players.SystemPlayer.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck));
			if (list.Count == 0)
			{
				return false;
			}
			ActionElementMap aem2 = mapping.aem;
			ElementAssignmentConflictInfo elementAssignmentConflictInfo = list[0];
			int actionId = elementAssignmentConflictInfo.elementMap.actionId;
			Pole axisContribution = elementAssignmentConflictInfo.elementMap.axisContribution;
			AxisRange axisRange = aem2.axisRange;
			ControllerElementType elementType = aem2.elementType;
			if (elementType == elementAssignmentConflictInfo.elementMap.elementType && elementType == ControllerElementType.Axis)
			{
				if (axisRange != elementAssignmentConflictInfo.elementMap.axisRange)
				{
					if (axisRange == AxisRange.Full)
					{
						axisRange = AxisRange.Positive;
					}
					else if (elementAssignmentConflictInfo.elementMap.axisRange == AxisRange.Full)
					{
					}
				}
			}
			else if (elementType == ControllerElementType.Axis && (elementAssignmentConflictInfo.elementMap.elementType == ControllerElementType.Button || (elementAssignmentConflictInfo.elementMap.elementType == ControllerElementType.Axis && elementAssignmentConflictInfo.elementMap.axisRange != AxisRange.Full)) && axisRange == AxisRange.Full)
			{
				axisRange = AxisRange.Positive;
			}
			int num = 0;
			if (assignment.actionId == elementAssignmentConflictInfo.actionId && mapping.map == elementAssignmentConflictInfo.controllerMap)
			{
				Controller controller = ReInput.controllers.GetController(mapping.controllerType, mapping.controllerId);
				if (this.SwapIsSameInputRange(elementType, axisRange, axisContribution, controller.GetElementById(assignment.elementIdentifierId).type, assignment.axisRange, assignment.axisContribution))
				{
					num++;
				}
			}
			using (IEnumerator<ActionElementMap> enumerator = elementAssignmentConflictInfo.controllerMap.ElementMapsWithAction(actionId).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ActionElementMap aem = enumerator.Current;
					if (aem.id != aem2.id)
					{
						if (list.FindIndex((ElementAssignmentConflictInfo x) => x.elementMapId == aem.id) < 0)
						{
							if (this.SwapIsSameInputRange(elementType, axisRange, axisContribution, aem.elementType, aem.axisRange, aem.axisContribution))
							{
								num++;
							}
						}
					}
				}
			}
			return num < this.GetControllerInputFieldCount(mapping.controllerType);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000A5194 File Offset: 0x000A3594
		private bool SwapIsSameInputRange(ControllerElementType origElementType, AxisRange origAxisRange, Pole origAxisContribution, ControllerElementType conflictElementType, AxisRange conflictAxisRange, Pole conflictAxisContribution)
		{
			return ((origElementType == ControllerElementType.Button || (origElementType == ControllerElementType.Axis && origAxisRange != AxisRange.Full)) && (conflictElementType == ControllerElementType.Button || (conflictElementType == ControllerElementType.Axis && conflictAxisRange != AxisRange.Full)) && conflictAxisContribution == origAxisContribution) || (origElementType == ControllerElementType.Axis && origAxisRange == AxisRange.Full && conflictElementType == ControllerElementType.Axis && conflictAxisRange == AxisRange.Full);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000A51F4 File Offset: 0x000A35F4
		public static void ApplyTheme(ThemedElement.ElementInfo[] elementInfo)
		{
			if (ControlMapper.Instance == null)
			{
				return;
			}
			if (ControlMapper.Instance._themeSettings == null)
			{
				return;
			}
			if (!ControlMapper.Instance._useThemeSettings)
			{
				return;
			}
			ControlMapper.Instance._themeSettings.Apply(elementInfo);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000A5248 File Offset: 0x000A3648
		public static LanguageData GetLanguage()
		{
			if (ControlMapper.Instance == null)
			{
				return null;
			}
			return ControlMapper.Instance._language;
		}

		// Token: 0x04001C78 RID: 7288
		private const float blockInputOnFocusTimeout = 0.1f;

		// Token: 0x04001C79 RID: 7289
		private const string buttonIdentifier_playerSelection = "PlayerSelection";

		// Token: 0x04001C7A RID: 7290
		private const string buttonIdentifier_removeController = "RemoveController";

		// Token: 0x04001C7B RID: 7291
		private const string buttonIdentifier_assignController = "AssignController";

		// Token: 0x04001C7C RID: 7292
		private const string buttonIdentifier_calibrateController = "CalibrateController";

		// Token: 0x04001C7D RID: 7293
		private const string buttonIdentifier_editInputBehaviors = "EditInputBehaviors";

		// Token: 0x04001C7E RID: 7294
		private const string buttonIdentifier_mapCategorySelection = "MapCategorySelection";

		// Token: 0x04001C7F RID: 7295
		private const string buttonIdentifier_assignedControllerSelection = "AssignedControllerSelection";

		// Token: 0x04001C80 RID: 7296
		private const string buttonIdentifier_done = "Done";

		// Token: 0x04001C81 RID: 7297
		private const string buttonIdentifier_restoreDefaults = "RestoreDefaults";

		// Token: 0x04001C82 RID: 7298
		[SerializeField]
		[Tooltip("Must be assigned a Rewired Input Manager scene object or prefab.")]
		private InputManager _rewiredInputManager;

		// Token: 0x04001C83 RID: 7299
		[SerializeField]
		[Tooltip("Set to True to prevent the Game Object from being destroyed when a new scene is loaded.\n\nNOTE: Changing this value from True to False at runtime will have no effect because Object.DontDestroyOnLoad cannot be undone once set.")]
		private bool _dontDestroyOnLoad;

		// Token: 0x04001C84 RID: 7300
		[SerializeField]
		[Tooltip("Open the control mapping screen immediately on start. Mainly used for testing.")]
		private bool _openOnStart;

		// Token: 0x04001C85 RID: 7301
		[SerializeField]
		[Tooltip("The Layout of the Keyboard Maps to be displayed.")]
		private int _keyboardMapDefaultLayout;

		// Token: 0x04001C86 RID: 7302
		[SerializeField]
		[Tooltip("The Layout of the Mouse Maps to be displayed.")]
		private int _mouseMapDefaultLayout;

		// Token: 0x04001C87 RID: 7303
		[SerializeField]
		[Tooltip("The Layout of the Mouse Maps to be displayed.")]
		private int _joystickMapDefaultLayout;

		// Token: 0x04001C88 RID: 7304
		[SerializeField]
		private ControlMapper.MappingSet[] _mappingSets = new ControlMapper.MappingSet[]
		{
			ControlMapper.MappingSet.Default
		};

		// Token: 0x04001C89 RID: 7305
		[SerializeField]
		[Tooltip("Display a selectable list of Players. If your game only supports 1 player, you can disable this.")]
		private bool _showPlayers = true;

		// Token: 0x04001C8A RID: 7306
		[SerializeField]
		[Tooltip("Display the Controller column for input mapping.")]
		private bool _showControllers = true;

		// Token: 0x04001C8B RID: 7307
		[SerializeField]
		[Tooltip("Display the Keyboard column for input mapping.")]
		private bool _showKeyboard = true;

		// Token: 0x04001C8C RID: 7308
		[SerializeField]
		[Tooltip("Display the Mouse column for input mapping.")]
		private bool _showMouse = true;

		// Token: 0x04001C8D RID: 7309
		[SerializeField]
		[Tooltip("The maximum number of controllers allowed to be assigned to a Player. If set to any value other than 1, a selectable list of currently-assigned controller will be displayed to the user. [0 = infinite]")]
		private int _maxControllersPerPlayer = 1;

		// Token: 0x04001C8E RID: 7310
		[SerializeField]
		[Tooltip("Display section labels for each Action Category in the input field grid. Only applies if Action Categories are used to display the Action list.")]
		private bool _showActionCategoryLabels;

		// Token: 0x04001C8F RID: 7311
		[SerializeField]
		[Tooltip("The number of input fields to display for the keyboard. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _keyboardInputFieldCount = 2;

		// Token: 0x04001C90 RID: 7312
		[SerializeField]
		[Tooltip("The number of input fields to display for the mouse. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _mouseInputFieldCount = 1;

		// Token: 0x04001C91 RID: 7313
		[SerializeField]
		[Tooltip("The number of input fields to display for joysticks. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _controllerInputFieldCount = 1;

		// Token: 0x04001C92 RID: 7314
		[SerializeField]
		[Tooltip("Display a full-axis input assignment field for every axis-type Action in the input field grid. Also displays an invert toggle for the user  to invert the full-axis assignment direction.\n\n*IMPORTANT*: This field is required if you have made any full-axis assignments in the Rewired Input Manager or in saved XML user data. Disabling this field when you have full-axis assignments will result in the inability for the user to view, remove, or modify these full-axis assignments. In addition, these assignments may cause conflicts when trying to remap the same axes to Actions.")]
		private bool _showFullAxisInputFields = true;

		// Token: 0x04001C93 RID: 7315
		[SerializeField]
		[Tooltip("Display a positive and negative input assignment field for every axis-type Action in the input field grid.\n\n*IMPORTANT*: These fields are required to assign buttons, keyboard keys, and hat or D-Pad directions to axis-type Actions. If you have made any split-axis assignments or button/key/D-pad assignments to axis-type Actions in the Rewired Input Manager or in saved XML user data, disabling these fields will result in the inability for the user to view, remove, or modify these assignments. In addition, these assignments may cause conflicts when trying to remap the same elements to Actions.")]
		private bool _showSplitAxisInputFields = true;

		// Token: 0x04001C94 RID: 7316
		[SerializeField]
		[Tooltip("If enabled, when an element assignment conflict is found, an option will be displayed that allows the user to make the conflicting assignment anyway.")]
		private bool _allowElementAssignmentConflicts;

		// Token: 0x04001C95 RID: 7317
		[SerializeField]
		[Tooltip("If enabled, when an element assignment conflict is found, an option will be displayed that allows the user to swap conflicting assignments. This only applies to the first conflicting assignment found. This option will not be displayed if allowElementAssignmentConflicts is true.")]
		private bool _allowElementAssignmentSwap;

		// Token: 0x04001C96 RID: 7318
		[SerializeField]
		[Tooltip("The width in relative pixels of the Action label column.")]
		private int _actionLabelWidth = 200;

		// Token: 0x04001C97 RID: 7319
		[SerializeField]
		[Tooltip("The width in relative pixels of the Keyboard column.")]
		private int _keyboardColMaxWidth = 360;

		// Token: 0x04001C98 RID: 7320
		[SerializeField]
		[Tooltip("The width in relative pixels of the Mouse column.")]
		private int _mouseColMaxWidth = 200;

		// Token: 0x04001C99 RID: 7321
		[SerializeField]
		[Tooltip("The width in relative pixels of the Controller column.")]
		private int _controllerColMaxWidth = 200;

		// Token: 0x04001C9A RID: 7322
		[SerializeField]
		[Tooltip("The height in relative pixels of the input grid button rows.")]
		private int _inputRowHeight = 40;

		// Token: 0x04001C9B RID: 7323
		[SerializeField]
		[Tooltip("The width in relative pixels of spacing between columns.")]
		private int _inputColumnSpacing = 40;

		// Token: 0x04001C9C RID: 7324
		[SerializeField]
		[Tooltip("The height in relative pixels of the space between Action Category sections. Only applicable if Show Action Category Labels is checked.")]
		private int _inputRowCategorySpacing = 20;

		// Token: 0x04001C9D RID: 7325
		[SerializeField]
		[Tooltip("The width in relative pixels of the invert toggle buttons.")]
		private int _invertToggleWidth = 40;

		// Token: 0x04001C9E RID: 7326
		[SerializeField]
		[Tooltip("The width in relative pixels of generated popup windows.")]
		private int _defaultWindowWidth = 500;

		// Token: 0x04001C9F RID: 7327
		[SerializeField]
		[Tooltip("The height in relative pixels of generated popup windows.")]
		private int _defaultWindowHeight = 400;

		// Token: 0x04001CA0 RID: 7328
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller when assigning a controller to a Player. If this time elapses with no user input a controller, the assignment will be canceled.")]
		private float _controllerAssignmentTimeout = 5f;

		// Token: 0x04001CA1 RID: 7329
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller while waiting for axes to be centered before assigning input.")]
		private float _preInputAssignmentTimeout = 5f;

		// Token: 0x04001CA2 RID: 7330
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller when assigning input. If this time elapses with no user input on the target controller, the assignment will be canceled.")]
		private float _inputAssignmentTimeout = 5f;

		// Token: 0x04001CA3 RID: 7331
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller during calibration.")]
		private float _axisCalibrationTimeout = 5f;

		// Token: 0x04001CA4 RID: 7332
		[SerializeField]
		[Tooltip("If checked, mouse X-axis movement will always be ignored during input assignment. Check this if you don't want the horizontal mouse axis to be user-assignable to any Actions.")]
		private bool _ignoreMouseXAxisAssignment = true;

		// Token: 0x04001CA5 RID: 7333
		[SerializeField]
		[Tooltip("If checked, mouse Y-axis movement will always be ignored during input assignment. Check this if you don't want the vertical mouse axis to be user-assignable to any Actions.")]
		private bool _ignoreMouseYAxisAssignment = true;

		// Token: 0x04001CA6 RID: 7334
		[SerializeField]
		[Tooltip("An Action that when activated will alternately close or open the main screen as long as no popup windows are open.")]
		private int _screenToggleAction = -1;

		// Token: 0x04001CA7 RID: 7335
		[SerializeField]
		[Tooltip("An Action that when activated will open the main screen if it is closed.")]
		private int _screenOpenAction = -1;

		// Token: 0x04001CA8 RID: 7336
		[SerializeField]
		[Tooltip("An Action that when activated will close the main screen as long as no popup windows are open.")]
		private int _screenCloseAction = -1;

		// Token: 0x04001CA9 RID: 7337
		[SerializeField]
		[Tooltip("An Action that when activated will cancel and close any open popup window. Use with care because the element assigned to this Action can never be mapped by the user (because it would just cancel his assignment).")]
		private int _universalCancelAction = -1;

		// Token: 0x04001CAA RID: 7338
		[SerializeField]
		[Tooltip("If enabled, Universal Cancel will also close the main screen if pressed when no windows are open.")]
		private bool _universalCancelClosesScreen = true;

		// Token: 0x04001CAB RID: 7339
		[SerializeField]
		[Tooltip("If checked, controls will be displayed which will allow the user to customize certain Input Behavior settings.")]
		private bool _showInputBehaviorSettings;

		// Token: 0x04001CAC RID: 7340
		[SerializeField]
		[Tooltip("Customizable settings for user-modifiable Input Behaviors. This can be used for settings like Mouse Look Sensitivity.")]
		private ControlMapper.InputBehaviorSettings[] _inputBehaviorSettings;

		// Token: 0x04001CAD RID: 7341
		[SerializeField]
		[Tooltip("If enabled, UI elements will be themed based on the settings in Theme Settings.")]
		private bool _useThemeSettings = true;

		// Token: 0x04001CAE RID: 7342
		[SerializeField]
		[Tooltip("Must be assigned a ThemeSettings object. Used to theme UI elements.")]
		private ThemeSettings _themeSettings;

		// Token: 0x04001CAF RID: 7343
		[SerializeField]
		[Tooltip("Must be assigned a LanguageData object. Used to retrieve language entries for UI elements.")]
		private LanguageData _language;

		// Token: 0x04001CB0 RID: 7344
		[SerializeField]
		[Tooltip("A list of prefabs. You should not have to modify this.")]
		private ControlMapper.Prefabs prefabs;

		// Token: 0x04001CB1 RID: 7345
		[SerializeField]
		[Tooltip("A list of references to elements in the hierarchy. You should not have to modify this.")]
		private ControlMapper.References references;

		// Token: 0x04001CB2 RID: 7346
		[SerializeField]
		[Tooltip("Show the label for the Players button group?")]
		private bool _showPlayersGroupLabel = true;

		// Token: 0x04001CB3 RID: 7347
		[SerializeField]
		[Tooltip("Show the label for the Controller button group?")]
		private bool _showControllerGroupLabel = true;

		// Token: 0x04001CB4 RID: 7348
		[SerializeField]
		[Tooltip("Show the label for the Assigned Controllers button group?")]
		private bool _showAssignedControllersGroupLabel = true;

		// Token: 0x04001CB5 RID: 7349
		[SerializeField]
		[Tooltip("Show the label for the Settings button group?")]
		private bool _showSettingsGroupLabel = true;

		// Token: 0x04001CB6 RID: 7350
		[SerializeField]
		[Tooltip("Show the label for the Map Categories button group?")]
		private bool _showMapCategoriesGroupLabel = true;

		// Token: 0x04001CB7 RID: 7351
		[SerializeField]
		[Tooltip("Show the label for the current controller name?")]
		private bool _showControllerNameLabel = true;

		// Token: 0x04001CB8 RID: 7352
		[SerializeField]
		[Tooltip("Show the Assigned Controllers group? If joystick auto-assignment is enabled in the Rewired Input Manager and the max joysticks per player is set to any value other than 1, the Assigned Controllers group will always be displayed.")]
		private bool _showAssignedControllers = true;

		// Token: 0x04001CB9 RID: 7353
		private Action _ScreenClosedEvent;

		// Token: 0x04001CBA RID: 7354
		private Action _ScreenOpenedEvent;

		// Token: 0x04001CBB RID: 7355
		private Action _PopupWindowOpenedEvent;

		// Token: 0x04001CBC RID: 7356
		private Action _PopupWindowClosedEvent;

		// Token: 0x04001CBD RID: 7357
		private Action _InputPollingStartedEvent;

		// Token: 0x04001CBE RID: 7358
		private Action _InputPollingEndedEvent;

		// Token: 0x04001CBF RID: 7359
		[SerializeField]
		[Tooltip("Event sent when the UI is closed.")]
		private UnityEvent _onScreenClosed;

		// Token: 0x04001CC0 RID: 7360
		[SerializeField]
		[Tooltip("Event sent when the UI is opened.")]
		private UnityEvent _onScreenOpened;

		// Token: 0x04001CC1 RID: 7361
		[SerializeField]
		[Tooltip("Event sent when a popup window is closed.")]
		private UnityEvent _onPopupWindowClosed;

		// Token: 0x04001CC2 RID: 7362
		[SerializeField]
		[Tooltip("Event sent when a popup window is opened.")]
		private UnityEvent _onPopupWindowOpened;

		// Token: 0x04001CC3 RID: 7363
		[SerializeField]
		[Tooltip("Event sent when polling for input has started.")]
		private UnityEvent _onInputPollingStarted;

		// Token: 0x04001CC4 RID: 7364
		[SerializeField]
		[Tooltip("Event sent when polling for input has ended.")]
		private UnityEvent _onInputPollingEnded;

		// Token: 0x04001CC5 RID: 7365
		private static ControlMapper Instance;

		// Token: 0x04001CC6 RID: 7366
		private bool initialized;

		// Token: 0x04001CC7 RID: 7367
		private int playerCount;

		// Token: 0x04001CC8 RID: 7368
		private ControlMapper.InputGrid inputGrid;

		// Token: 0x04001CC9 RID: 7369
		private ControlMapper.WindowManager windowManager;

		// Token: 0x04001CCA RID: 7370
		private int currentPlayerId;

		// Token: 0x04001CCB RID: 7371
		private int currentMapCategoryId;

		// Token: 0x04001CCC RID: 7372
		private List<ControlMapper.GUIButton> playerButtons;

		// Token: 0x04001CCD RID: 7373
		private List<ControlMapper.GUIButton> mapCategoryButtons;

		// Token: 0x04001CCE RID: 7374
		private List<ControlMapper.GUIButton> assignedControllerButtons;

		// Token: 0x04001CCF RID: 7375
		private ControlMapper.GUIButton assignedControllerButtonsPlaceholder;

		// Token: 0x04001CD0 RID: 7376
		private List<GameObject> miscInstantiatedObjects;

		// Token: 0x04001CD1 RID: 7377
		private GameObject canvas;

		// Token: 0x04001CD2 RID: 7378
		private GameObject lastUISelection;

		// Token: 0x04001CD3 RID: 7379
		private int currentJoystickId = -1;

		// Token: 0x04001CD4 RID: 7380
		private float blockInputOnFocusEndTime;

		// Token: 0x04001CD5 RID: 7381
		private bool isPollingForInput;

		// Token: 0x04001CD6 RID: 7382
		private ControlMapper.InputMapping pendingInputMapping;

		// Token: 0x04001CD7 RID: 7383
		private ControlMapper.AxisCalibrator pendingAxisCalibration;

		// Token: 0x04001CD8 RID: 7384
		private Action<InputFieldInfo> inputFieldActivatedDelegate;

		// Token: 0x04001CD9 RID: 7385
		private Action<ToggleInfo, bool> inputFieldInvertToggleStateChangedDelegate;

		// Token: 0x04001CDA RID: 7386
		private Action _restoreDefaultsDelegate;

		// Token: 0x0200052E RID: 1326
		private abstract class GUIElement
		{
			// Token: 0x06001AA6 RID: 6822 RVA: 0x000A5268 File Offset: 0x000A3668
			public GUIElement(GameObject gameObject)
			{
				if (gameObject == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: gameObject is null!");
					return;
				}
				this.selectable = gameObject.GetComponent<Selectable>();
				if (this.selectable == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: Selectable is null!");
					return;
				}
				this.gameObject = gameObject;
				this.rectTransform = gameObject.GetComponent<RectTransform>();
				this.text = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
				this.uiElementInfo = gameObject.GetComponent<UIElementInfo>();
				this.children = new List<ControlMapper.GUIElement>();
			}

			// Token: 0x06001AA7 RID: 6823 RVA: 0x000A52F0 File Offset: 0x000A36F0
			public GUIElement(Selectable selectable, Text label)
			{
				if (selectable == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: Selectable is null!");
					return;
				}
				this.selectable = selectable;
				this.gameObject = selectable.gameObject;
				this.rectTransform = this.gameObject.GetComponent<RectTransform>();
				this.text = label;
				this.uiElementInfo = this.gameObject.GetComponent<UIElementInfo>();
				this.children = new List<ControlMapper.GUIElement>();
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000A5361 File Offset: 0x000A3761
			// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x000A5369 File Offset: 0x000A3769
			public RectTransform rectTransform { get; private set; }

			// Token: 0x06001AAA RID: 6826 RVA: 0x000A5372 File Offset: 0x000A3772
			public virtual void SetInteractible(bool state, bool playTransition)
			{
				this.SetInteractible(state, playTransition, false);
			}

			// Token: 0x06001AAB RID: 6827 RVA: 0x000A5380 File Offset: 0x000A3780
			public virtual void SetInteractible(bool state, bool playTransition, bool permanent)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i] != null)
					{
						this.children[i].SetInteractible(state, playTransition, permanent);
					}
				}
				if (this.permanentStateSet)
				{
					return;
				}
				if (this.selectable == null)
				{
					return;
				}
				if (permanent)
				{
					this.permanentStateSet = true;
				}
				if (this.selectable.interactable == state)
				{
					return;
				}
				UITools.SetInteractable(this.selectable, state, playTransition);
			}

			// Token: 0x06001AAC RID: 6828 RVA: 0x000A5420 File Offset: 0x000A3820
			public virtual void SetTextWidth(int value)
			{
				if (this.text == null)
				{
					return;
				}
				LayoutElement layoutElement = this.text.GetComponent<LayoutElement>();
				if (layoutElement == null)
				{
					layoutElement = this.text.gameObject.AddComponent<LayoutElement>();
				}
				layoutElement.preferredWidth = (float)value;
			}

			// Token: 0x06001AAD RID: 6829 RVA: 0x000A5470 File Offset: 0x000A3870
			public virtual void SetFirstChildObjectWidth(ControlMapper.LayoutElementSizeType type, int value)
			{
				if (this.rectTransform.childCount == 0)
				{
					return;
				}
				Transform child = this.rectTransform.GetChild(0);
				LayoutElement layoutElement = child.GetComponent<LayoutElement>();
				if (layoutElement == null)
				{
					layoutElement = child.gameObject.AddComponent<LayoutElement>();
				}
				if (type == ControlMapper.LayoutElementSizeType.MinSize)
				{
					layoutElement.minWidth = (float)value;
				}
				else
				{
					if (type != ControlMapper.LayoutElementSizeType.PreferredSize)
					{
						throw new NotImplementedException();
					}
					layoutElement.preferredWidth = (float)value;
				}
			}

			// Token: 0x06001AAE RID: 6830 RVA: 0x000A54E7 File Offset: 0x000A38E7
			public virtual void SetLabel(string label)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.text = label;
			}

			// Token: 0x06001AAF RID: 6831 RVA: 0x000A5507 File Offset: 0x000A3907
			public virtual string GetLabel()
			{
				if (this.text == null)
				{
					return string.Empty;
				}
				return this.text.text;
			}

			// Token: 0x06001AB0 RID: 6832 RVA: 0x000A552B File Offset: 0x000A392B
			public virtual void AddChild(ControlMapper.GUIElement child)
			{
				this.children.Add(child);
			}

			// Token: 0x06001AB1 RID: 6833 RVA: 0x000A5539 File Offset: 0x000A3939
			public void SetElementInfoData(string identifier, int intData)
			{
				if (this.uiElementInfo == null)
				{
					return;
				}
				this.uiElementInfo.identifier = identifier;
				this.uiElementInfo.intData = intData;
			}

			// Token: 0x06001AB2 RID: 6834 RVA: 0x000A5565 File Offset: 0x000A3965
			public virtual void SetActive(bool state)
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.gameObject.SetActive(state);
			}

			// Token: 0x06001AB3 RID: 6835 RVA: 0x000A5588 File Offset: 0x000A3988
			protected virtual bool Init()
			{
				bool result = true;
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i] != null)
					{
						if (!this.children[i].Init())
						{
							result = false;
						}
					}
				}
				if (this.selectable == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: UI Element is missing Selectable component!");
					result = false;
				}
				if (this.rectTransform == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: UI Element is missing RectTransform component!");
					result = false;
				}
				if (this.uiElementInfo == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: UI Element is missing UIElementInfo component!");
					result = false;
				}
				return result;
			}

			// Token: 0x04001CDC RID: 7388
			public readonly GameObject gameObject;

			// Token: 0x04001CDD RID: 7389
			protected readonly Text text;

			// Token: 0x04001CDE RID: 7390
			public readonly Selectable selectable;

			// Token: 0x04001CDF RID: 7391
			protected readonly UIElementInfo uiElementInfo;

			// Token: 0x04001CE0 RID: 7392
			protected bool permanentStateSet;

			// Token: 0x04001CE1 RID: 7393
			protected readonly List<ControlMapper.GUIElement> children;
		}

		// Token: 0x0200052F RID: 1327
		private class GUIButton : ControlMapper.GUIElement
		{
			// Token: 0x06001AB4 RID: 6836 RVA: 0x000A5639 File Offset: 0x000A3A39
			public GUIButton(GameObject gameObject) : base(gameObject)
			{
				if (!this.Init())
				{
					return;
				}
			}

			// Token: 0x06001AB5 RID: 6837 RVA: 0x000A564E File Offset: 0x000A3A4E
			public GUIButton(Button button, Text label) : base(button, label)
			{
				if (!this.Init())
				{
					return;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x000A5664 File Offset: 0x000A3A64
			protected Button button
			{
				get
				{
					return this.selectable as Button;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000A5671 File Offset: 0x000A3A71
			public ButtonInfo buttonInfo
			{
				get
				{
					return this.uiElementInfo as ButtonInfo;
				}
			}

			// Token: 0x06001AB8 RID: 6840 RVA: 0x000A567E File Offset: 0x000A3A7E
			public void SetButtonInfoData(string identifier, int intData)
			{
				base.SetElementInfoData(identifier, intData);
			}

			// Token: 0x06001AB9 RID: 6841 RVA: 0x000A5688 File Offset: 0x000A3A88
			public void SetOnClickCallback(Action<ButtonInfo> callback)
			{
				if (this.button == null)
				{
					return;
				}
				this.button.onClick.AddListener(delegate()
				{
					callback(this.buttonInfo);
				});
			}
		}

		// Token: 0x02000530 RID: 1328
		private class GUIInputField : ControlMapper.GUIElement
		{
			// Token: 0x06001ABA RID: 6842 RVA: 0x000A56F7 File Offset: 0x000A3AF7
			public GUIInputField(GameObject gameObject) : base(gameObject)
			{
				if (!this.Init())
				{
					return;
				}
			}

			// Token: 0x06001ABB RID: 6843 RVA: 0x000A570C File Offset: 0x000A3B0C
			public GUIInputField(Button button, Text label) : base(button, label)
			{
				if (!this.Init())
				{
					return;
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06001ABC RID: 6844 RVA: 0x000A5722 File Offset: 0x000A3B22
			protected Button button
			{
				get
				{
					return this.selectable as Button;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06001ABD RID: 6845 RVA: 0x000A572F File Offset: 0x000A3B2F
			public InputFieldInfo fieldInfo
			{
				get
				{
					return this.uiElementInfo as InputFieldInfo;
				}
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06001ABE RID: 6846 RVA: 0x000A573C File Offset: 0x000A3B3C
			public bool hasToggle
			{
				get
				{
					return this.toggle != null;
				}
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x06001ABF RID: 6847 RVA: 0x000A574A File Offset: 0x000A3B4A
			// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x000A5752 File Offset: 0x000A3B52
			public ControlMapper.GUIToggle toggle { get; private set; }

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000A575B File Offset: 0x000A3B5B
			// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x000A577B File Offset: 0x000A3B7B
			public int actionElementMapId
			{
				get
				{
					if (this.fieldInfo == null)
					{
						return -1;
					}
					return this.fieldInfo.actionElementMapId;
				}
				set
				{
					if (this.fieldInfo == null)
					{
						return;
					}
					this.fieldInfo.actionElementMapId = value;
				}
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000A579B File Offset: 0x000A3B9B
			// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x000A57BB File Offset: 0x000A3BBB
			public int controllerId
			{
				get
				{
					if (this.fieldInfo == null)
					{
						return -1;
					}
					return this.fieldInfo.controllerId;
				}
				set
				{
					if (this.fieldInfo == null)
					{
						return;
					}
					this.fieldInfo.controllerId = value;
				}
			}

			// Token: 0x06001AC5 RID: 6853 RVA: 0x000A57DC File Offset: 0x000A3BDC
			public void SetFieldInfoData(int actionId, AxisRange axisRange, ControllerType controllerType, int intData)
			{
				base.SetElementInfoData(string.Empty, intData);
				if (this.fieldInfo == null)
				{
					return;
				}
				this.fieldInfo.actionId = actionId;
				this.fieldInfo.axisRange = axisRange;
				this.fieldInfo.controllerType = controllerType;
			}

			// Token: 0x06001AC6 RID: 6854 RVA: 0x000A582C File Offset: 0x000A3C2C
			public void SetOnClickCallback(Action<InputFieldInfo> callback)
			{
				if (this.button == null)
				{
					return;
				}
				this.button.onClick.AddListener(delegate()
				{
					callback(this.fieldInfo);
				});
			}

			// Token: 0x06001AC7 RID: 6855 RVA: 0x000A587B File Offset: 0x000A3C7B
			public virtual void SetInteractable(bool state, bool playTransition, bool permanent)
			{
				if (this.permanentStateSet)
				{
					return;
				}
				if (this.hasToggle && !state)
				{
					this.toggle.SetInteractible(state, playTransition, permanent);
				}
				base.SetInteractible(state, playTransition, permanent);
			}

			// Token: 0x06001AC8 RID: 6856 RVA: 0x000A58B1 File Offset: 0x000A3CB1
			public void AddToggle(ControlMapper.GUIToggle toggle)
			{
				if (toggle == null)
				{
					return;
				}
				this.toggle = toggle;
			}
		}

		// Token: 0x02000531 RID: 1329
		private class GUIToggle : ControlMapper.GUIElement
		{
			// Token: 0x06001AC9 RID: 6857 RVA: 0x000A58E1 File Offset: 0x000A3CE1
			public GUIToggle(GameObject gameObject) : base(gameObject)
			{
				if (!this.Init())
				{
					return;
				}
			}

			// Token: 0x06001ACA RID: 6858 RVA: 0x000A58F6 File Offset: 0x000A3CF6
			public GUIToggle(Toggle toggle, Text label) : base(toggle, label)
			{
				if (!this.Init())
				{
					return;
				}
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000A590C File Offset: 0x000A3D0C
			protected Toggle toggle
			{
				get
				{
					return this.selectable as Toggle;
				}
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06001ACC RID: 6860 RVA: 0x000A5919 File Offset: 0x000A3D19
			public ToggleInfo toggleInfo
			{
				get
				{
					return this.uiElementInfo as ToggleInfo;
				}
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000A5926 File Offset: 0x000A3D26
			// (set) Token: 0x06001ACE RID: 6862 RVA: 0x000A5946 File Offset: 0x000A3D46
			public int actionElementMapId
			{
				get
				{
					if (this.toggleInfo == null)
					{
						return -1;
					}
					return this.toggleInfo.actionElementMapId;
				}
				set
				{
					if (this.toggleInfo == null)
					{
						return;
					}
					this.toggleInfo.actionElementMapId = value;
				}
			}

			// Token: 0x06001ACF RID: 6863 RVA: 0x000A5968 File Offset: 0x000A3D68
			public void SetToggleInfoData(int actionId, AxisRange axisRange, ControllerType controllerType, int intData)
			{
				base.SetElementInfoData(string.Empty, intData);
				if (this.toggleInfo == null)
				{
					return;
				}
				this.toggleInfo.actionId = actionId;
				this.toggleInfo.axisRange = axisRange;
				this.toggleInfo.controllerType = controllerType;
			}

			// Token: 0x06001AD0 RID: 6864 RVA: 0x000A59B8 File Offset: 0x000A3DB8
			public void SetOnSubmitCallback(Action<ToggleInfo, bool> callback)
			{
				if (this.toggle == null)
				{
					return;
				}
				EventTrigger eventTrigger = this.toggle.GetComponent<EventTrigger>();
				if (eventTrigger == null)
				{
					eventTrigger = this.toggle.gameObject.AddComponent<EventTrigger>();
				}
				EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
				triggerEvent.AddListener(delegate(BaseEventData data)
				{
					PointerEventData pointerEventData = data as PointerEventData;
					if (pointerEventData != null && pointerEventData.button != PointerEventData.InputButton.Left)
					{
						return;
					}
					callback(this.toggleInfo, this.toggle.isOn);
				});
				EventTrigger.Entry item = new EventTrigger.Entry
				{
					callback = triggerEvent,
					eventID = EventTriggerType.Submit
				};
				EventTrigger.Entry item2 = new EventTrigger.Entry
				{
					callback = triggerEvent,
					eventID = EventTriggerType.PointerClick
				};
				if (eventTrigger.triggers != null)
				{
					eventTrigger.triggers.Clear();
				}
				else
				{
					eventTrigger.triggers = new List<EventTrigger.Entry>();
				}
				eventTrigger.triggers.Add(item);
				eventTrigger.triggers.Add(item2);
			}

			// Token: 0x06001AD1 RID: 6865 RVA: 0x000A5AA1 File Offset: 0x000A3EA1
			public void SetToggleState(bool state)
			{
				if (this.toggle == null)
				{
					return;
				}
				this.toggle.isOn = state;
			}
		}

		// Token: 0x02000532 RID: 1330
		private class GUILabel
		{
			// Token: 0x06001AD2 RID: 6866 RVA: 0x000A5B18 File Offset: 0x000A3F18
			public GUILabel(GameObject gameObject)
			{
				if (gameObject == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: gameObject is null!");
					return;
				}
				this.text = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
				this.Check();
			}

			// Token: 0x06001AD3 RID: 6867 RVA: 0x000A5B4A File Offset: 0x000A3F4A
			public GUILabel(Text label)
			{
				this.text = label;
				if (!this.Check())
				{
					return;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x000A5B65 File Offset: 0x000A3F65
			// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x000A5B6D File Offset: 0x000A3F6D
			public GameObject gameObject { get; private set; }

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x000A5B76 File Offset: 0x000A3F76
			// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x000A5B7E File Offset: 0x000A3F7E
			private Text text { get; set; }

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x000A5B87 File Offset: 0x000A3F87
			// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x000A5B8F File Offset: 0x000A3F8F
			public RectTransform rectTransform { get; private set; }

			// Token: 0x06001ADA RID: 6874 RVA: 0x000A5B98 File Offset: 0x000A3F98
			public void SetSize(int width, int height)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)width);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)height);
			}

			// Token: 0x06001ADB RID: 6875 RVA: 0x000A5BC8 File Offset: 0x000A3FC8
			public void SetWidth(int width)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)width);
			}

			// Token: 0x06001ADC RID: 6876 RVA: 0x000A5BEA File Offset: 0x000A3FEA
			public void SetHeight(int height)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)height);
			}

			// Token: 0x06001ADD RID: 6877 RVA: 0x000A5C0C File Offset: 0x000A400C
			public void SetLabel(string label)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.text = label;
			}

			// Token: 0x06001ADE RID: 6878 RVA: 0x000A5C2C File Offset: 0x000A402C
			public void SetFontStyle(FontStyle style)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.fontStyle = style;
			}

			// Token: 0x06001ADF RID: 6879 RVA: 0x000A5C4C File Offset: 0x000A404C
			public void SetTextAlignment(TextAnchor alignment)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.alignment = alignment;
			}

			// Token: 0x06001AE0 RID: 6880 RVA: 0x000A5C6C File Offset: 0x000A406C
			public void SetActive(bool state)
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.gameObject.SetActive(state);
			}

			// Token: 0x06001AE1 RID: 6881 RVA: 0x000A5C8C File Offset: 0x000A408C
			private bool Check()
			{
				bool result = true;
				if (this.text == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: Button is missing Text child component!");
					result = false;
				}
				this.gameObject = this.text.gameObject;
				this.rectTransform = this.text.GetComponent<RectTransform>();
				return result;
			}
		}

		// Token: 0x02000533 RID: 1331
		[Serializable]
		public class MappingSet
		{
			// Token: 0x06001AE2 RID: 6882 RVA: 0x000A5CDB File Offset: 0x000A40DB
			public MappingSet()
			{
				this._mapCategoryId = -1;
				this._actionCategoryIds = new int[0];
				this._actionIds = new int[0];
				this._actionListMode = ControlMapper.MappingSet.ActionListMode.ActionCategory;
			}

			// Token: 0x06001AE3 RID: 6883 RVA: 0x000A5D09 File Offset: 0x000A4109
			private MappingSet(int mapCategoryId, ControlMapper.MappingSet.ActionListMode actionListMode, int[] actionCategoryIds, int[] actionIds)
			{
				this._mapCategoryId = mapCategoryId;
				this._actionListMode = actionListMode;
				this._actionCategoryIds = actionCategoryIds;
				this._actionIds = actionIds;
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x000A5D2E File Offset: 0x000A412E
			public int mapCategoryId
			{
				get
				{
					return this._mapCategoryId;
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x000A5D36 File Offset: 0x000A4136
			public ControlMapper.MappingSet.ActionListMode actionListMode
			{
				get
				{
					return this._actionListMode;
				}
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000A5D3E File Offset: 0x000A413E
			public IList<int> actionCategoryIds
			{
				get
				{
					if (this._actionCategoryIds == null)
					{
						return null;
					}
					if (this._actionCategoryIdsReadOnly == null)
					{
						this._actionCategoryIdsReadOnly = new ReadOnlyCollection<int>(this._actionCategoryIds);
					}
					return this._actionCategoryIdsReadOnly;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x000A5D6F File Offset: 0x000A416F
			public IList<int> actionIds
			{
				get
				{
					if (this._actionIds == null)
					{
						return null;
					}
					if (this._actionIdsReadOnly == null)
					{
						this._actionIdsReadOnly = new ReadOnlyCollection<int>(this._actionIds);
					}
					return this._actionIds;
				}
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000A5DA0 File Offset: 0x000A41A0
			public bool isValid
			{
				get
				{
					return this._mapCategoryId >= 0 && ReInput.mapping.GetMapCategory(this._mapCategoryId) != null;
				}
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x000A5DC6 File Offset: 0x000A41C6
			public static ControlMapper.MappingSet Default
			{
				get
				{
					return new ControlMapper.MappingSet(0, ControlMapper.MappingSet.ActionListMode.ActionCategory, new int[1], new int[0]);
				}
			}

			// Token: 0x04001CE7 RID: 7399
			[SerializeField]
			[Tooltip("The Map Category that will be displayed to the user for remapping.")]
			private int _mapCategoryId;

			// Token: 0x04001CE8 RID: 7400
			[SerializeField]
			[Tooltip("Choose whether you want to list Actions to display for this Map Category by individual Action or by all the Actions in an Action Category.")]
			private ControlMapper.MappingSet.ActionListMode _actionListMode;

			// Token: 0x04001CE9 RID: 7401
			[SerializeField]
			private int[] _actionCategoryIds;

			// Token: 0x04001CEA RID: 7402
			[SerializeField]
			private int[] _actionIds;

			// Token: 0x04001CEB RID: 7403
			private IList<int> _actionCategoryIdsReadOnly;

			// Token: 0x04001CEC RID: 7404
			private IList<int> _actionIdsReadOnly;

			// Token: 0x02000534 RID: 1332
			public enum ActionListMode
			{
				// Token: 0x04001CEE RID: 7406
				ActionCategory,
				// Token: 0x04001CEF RID: 7407
				Action
			}
		}

		// Token: 0x02000535 RID: 1333
		[Serializable]
		public class InputBehaviorSettings
		{
			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000A5E3B File Offset: 0x000A423B
			public int inputBehaviorId
			{
				get
				{
					return this._inputBehaviorId;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000A5E43 File Offset: 0x000A4243
			public bool showJoystickAxisSensitivity
			{
				get
				{
					return this._showJoystickAxisSensitivity;
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06001AED RID: 6893 RVA: 0x000A5E4B File Offset: 0x000A424B
			public bool showMouseXYAxisSensitivity
			{
				get
				{
					return this._showMouseXYAxisSensitivity;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000A5E53 File Offset: 0x000A4253
			public string labelLanguageKey
			{
				get
				{
					return this._labelLanguageKey;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000A5E5B File Offset: 0x000A425B
			public string joystickAxisSensitivityLabelLanguageKey
			{
				get
				{
					return this._joystickAxisSensitivityLabelLanguageKey;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000A5E63 File Offset: 0x000A4263
			public string mouseXYAxisSensitivityLabelLanguageKey
			{
				get
				{
					return this._mouseXYAxisSensitivityLabelLanguageKey;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000A5E6B File Offset: 0x000A426B
			public Sprite joystickAxisSensitivityIcon
			{
				get
				{
					return this._joystickAxisSensitivityIcon;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x000A5E73 File Offset: 0x000A4273
			public Sprite mouseXYAxisSensitivityIcon
			{
				get
				{
					return this._mouseXYAxisSensitivityIcon;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000A5E7B File Offset: 0x000A427B
			public float joystickAxisSensitivityMin
			{
				get
				{
					return this._joystickAxisSensitivityMin;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x000A5E83 File Offset: 0x000A4283
			public float joystickAxisSensitivityMax
			{
				get
				{
					return this._joystickAxisSensitivityMax;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000A5E8B File Offset: 0x000A428B
			public float mouseXYAxisSensitivityMin
			{
				get
				{
					return this._mouseXYAxisSensitivityMin;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000A5E93 File Offset: 0x000A4293
			public float mouseXYAxisSensitivityMax
			{
				get
				{
					return this._mouseXYAxisSensitivityMax;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000A5E9B File Offset: 0x000A429B
			public bool isValid
			{
				get
				{
					return this._inputBehaviorId >= 0 && (this._showJoystickAxisSensitivity || this._showMouseXYAxisSensitivity);
				}
			}

			// Token: 0x04001CF0 RID: 7408
			[SerializeField]
			[Tooltip("The Input Behavior that will be displayed to the user for modification.")]
			private int _inputBehaviorId = -1;

			// Token: 0x04001CF1 RID: 7409
			[SerializeField]
			[Tooltip("If checked, a slider will be displayed so the user can change this value.")]
			private bool _showJoystickAxisSensitivity = true;

			// Token: 0x04001CF2 RID: 7410
			[SerializeField]
			[Tooltip("If checked, a slider will be displayed so the user can change this value.")]
			private bool _showMouseXYAxisSensitivity = true;

			// Token: 0x04001CF3 RID: 7411
			[SerializeField]
			[Tooltip("If set to a non-blank value, this key will be used to look up the name in Language to be displayed as the title for the Input Behavior control set. Otherwise, the name field of the InputBehavior will be used.")]
			private string _labelLanguageKey = string.Empty;

			// Token: 0x04001CF4 RID: 7412
			[SerializeField]
			[Tooltip("If set to a non-blank value, this name will be displayed above the individual slider control. Otherwise, no name will be displayed.")]
			private string _joystickAxisSensitivityLabelLanguageKey = string.Empty;

			// Token: 0x04001CF5 RID: 7413
			[SerializeField]
			[Tooltip("If set to a non-blank value, this key will be used to look up the name in Language to be displayed above the individual slider control. Otherwise, no name will be displayed.")]
			private string _mouseXYAxisSensitivityLabelLanguageKey = string.Empty;

			// Token: 0x04001CF6 RID: 7414
			[SerializeField]
			[Tooltip("The icon to display next to the slider. Set to none for no icon.")]
			private Sprite _joystickAxisSensitivityIcon;

			// Token: 0x04001CF7 RID: 7415
			[SerializeField]
			[Tooltip("The icon to display next to the slider. Set to none for no icon.")]
			private Sprite _mouseXYAxisSensitivityIcon;

			// Token: 0x04001CF8 RID: 7416
			[SerializeField]
			[Tooltip("Minimum value the user is allowed to set for this property.")]
			private float _joystickAxisSensitivityMin;

			// Token: 0x04001CF9 RID: 7417
			[SerializeField]
			[Tooltip("Maximum value the user is allowed to set for this property.")]
			private float _joystickAxisSensitivityMax = 2f;

			// Token: 0x04001CFA RID: 7418
			[SerializeField]
			[Tooltip("Minimum value the user is allowed to set for this property.")]
			private float _mouseXYAxisSensitivityMin;

			// Token: 0x04001CFB RID: 7419
			[SerializeField]
			[Tooltip("Maximum value the user is allowed to set for this property.")]
			private float _mouseXYAxisSensitivityMax = 2f;
		}

		// Token: 0x02000536 RID: 1334
		[Serializable]
		private class Prefabs
		{
			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x000A5EC8 File Offset: 0x000A42C8
			public GameObject button
			{
				get
				{
					return this._button;
				}
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06001AFA RID: 6906 RVA: 0x000A5ED0 File Offset: 0x000A42D0
			public GameObject fitButton
			{
				get
				{
					return this._fitButton;
				}
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000A5ED8 File Offset: 0x000A42D8
			public GameObject inputGridLabel
			{
				get
				{
					return this._inputGridLabel;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06001AFC RID: 6908 RVA: 0x000A5EE0 File Offset: 0x000A42E0
			public GameObject inputGridHeaderLabel
			{
				get
				{
					return this._inputGridHeaderLabel;
				}
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06001AFD RID: 6909 RVA: 0x000A5EE8 File Offset: 0x000A42E8
			public GameObject inputGridFieldButton
			{
				get
				{
					return this._inputGridFieldButton;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06001AFE RID: 6910 RVA: 0x000A5EF0 File Offset: 0x000A42F0
			public GameObject inputGridFieldInvertToggle
			{
				get
				{
					return this._inputGridFieldInvertToggle;
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000A5EF8 File Offset: 0x000A42F8
			public GameObject window
			{
				get
				{
					return this._window;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000A5F00 File Offset: 0x000A4300
			public GameObject windowTitleText
			{
				get
				{
					return this._windowTitleText;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000A5F08 File Offset: 0x000A4308
			public GameObject windowContentText
			{
				get
				{
					return this._windowContentText;
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000A5F10 File Offset: 0x000A4310
			public GameObject fader
			{
				get
				{
					return this._fader;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000A5F18 File Offset: 0x000A4318
			public GameObject calibrationWindow
			{
				get
				{
					return this._calibrationWindow;
				}
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x06001B04 RID: 6916 RVA: 0x000A5F20 File Offset: 0x000A4320
			public GameObject inputBehaviorsWindow
			{
				get
				{
					return this._inputBehaviorsWindow;
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000A5F28 File Offset: 0x000A4328
			public GameObject centerStickGraphic
			{
				get
				{
					return this._centerStickGraphic;
				}
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06001B06 RID: 6918 RVA: 0x000A5F30 File Offset: 0x000A4330
			public GameObject moveStickGraphic
			{
				get
				{
					return this._moveStickGraphic;
				}
			}

			// Token: 0x06001B07 RID: 6919 RVA: 0x000A5F38 File Offset: 0x000A4338
			public bool Check()
			{
				return !(this._button == null) && !(this._fitButton == null) && !(this._inputGridLabel == null) && !(this._inputGridHeaderLabel == null) && !(this._inputGridFieldButton == null) && !(this._inputGridFieldInvertToggle == null) && !(this._window == null) && !(this._windowTitleText == null) && !(this._windowContentText == null) && !(this._fader == null) && !(this._calibrationWindow == null) && !(this._inputBehaviorsWindow == null);
			}

			// Token: 0x04001CFC RID: 7420
			[SerializeField]
			private GameObject _button;

			// Token: 0x04001CFD RID: 7421
			[SerializeField]
			private GameObject _fitButton;

			// Token: 0x04001CFE RID: 7422
			[SerializeField]
			private GameObject _inputGridLabel;

			// Token: 0x04001CFF RID: 7423
			[SerializeField]
			private GameObject _inputGridHeaderLabel;

			// Token: 0x04001D00 RID: 7424
			[SerializeField]
			private GameObject _inputGridFieldButton;

			// Token: 0x04001D01 RID: 7425
			[SerializeField]
			private GameObject _inputGridFieldInvertToggle;

			// Token: 0x04001D02 RID: 7426
			[SerializeField]
			private GameObject _window;

			// Token: 0x04001D03 RID: 7427
			[SerializeField]
			private GameObject _windowTitleText;

			// Token: 0x04001D04 RID: 7428
			[SerializeField]
			private GameObject _windowContentText;

			// Token: 0x04001D05 RID: 7429
			[SerializeField]
			private GameObject _fader;

			// Token: 0x04001D06 RID: 7430
			[SerializeField]
			private GameObject _calibrationWindow;

			// Token: 0x04001D07 RID: 7431
			[SerializeField]
			private GameObject _inputBehaviorsWindow;

			// Token: 0x04001D08 RID: 7432
			[SerializeField]
			private GameObject _centerStickGraphic;

			// Token: 0x04001D09 RID: 7433
			[SerializeField]
			private GameObject _moveStickGraphic;
		}

		// Token: 0x02000537 RID: 1335
		[Serializable]
		private class References
		{
			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x06001B09 RID: 6921 RVA: 0x000A601C File Offset: 0x000A441C
			public Canvas canvas
			{
				get
				{
					return this._canvas;
				}
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x06001B0A RID: 6922 RVA: 0x000A6024 File Offset: 0x000A4424
			public CanvasGroup mainCanvasGroup
			{
				get
				{
					return this._mainCanvasGroup;
				}
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000A602C File Offset: 0x000A442C
			public Transform mainContent
			{
				get
				{
					return this._mainContent;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000A6034 File Offset: 0x000A4434
			public Transform mainContentInner
			{
				get
				{
					return this._mainContentInner;
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000A603C File Offset: 0x000A443C
			public UIGroup playersGroup
			{
				get
				{
					return this._playersGroup;
				}
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000A6044 File Offset: 0x000A4444
			public Transform controllerGroup
			{
				get
				{
					return this._controllerGroup;
				}
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000A604C File Offset: 0x000A444C
			public Transform controllerGroupLabelGroup
			{
				get
				{
					return this._controllerGroupLabelGroup;
				}
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000A6054 File Offset: 0x000A4454
			public UIGroup controllerSettingsGroup
			{
				get
				{
					return this._controllerSettingsGroup;
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000A605C File Offset: 0x000A445C
			public UIGroup assignedControllersGroup
			{
				get
				{
					return this._assignedControllersGroup;
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000A6064 File Offset: 0x000A4464
			public Transform settingsAndMapCategoriesGroup
			{
				get
				{
					return this._settingsAndMapCategoriesGroup;
				}
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000A606C File Offset: 0x000A446C
			public UIGroup settingsGroup
			{
				get
				{
					return this._settingsGroup;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06001B14 RID: 6932 RVA: 0x000A6074 File Offset: 0x000A4474
			public UIGroup mapCategoriesGroup
			{
				get
				{
					return this._mapCategoriesGroup;
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06001B15 RID: 6933 RVA: 0x000A607C File Offset: 0x000A447C
			public Transform inputGridGroup
			{
				get
				{
					return this._inputGridGroup;
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000A6084 File Offset: 0x000A4484
			public Transform inputGridContainer
			{
				get
				{
					return this._inputGridContainer;
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x06001B17 RID: 6935 RVA: 0x000A608C File Offset: 0x000A448C
			public Transform inputGridHeadersGroup
			{
				get
				{
					return this._inputGridHeadersGroup;
				}
			}

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000A6094 File Offset: 0x000A4494
			public Scrollbar inputGridVScrollbar
			{
				get
				{
					return this._inputGridVScrollbar;
				}
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06001B19 RID: 6937 RVA: 0x000A609C File Offset: 0x000A449C
			public ScrollRect inputGridScrollRect
			{
				get
				{
					return this._inputGridScrollRect;
				}
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06001B1A RID: 6938 RVA: 0x000A60A4 File Offset: 0x000A44A4
			public Transform inputGridInnerGroup
			{
				get
				{
					return this._inputGridInnerGroup;
				}
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06001B1B RID: 6939 RVA: 0x000A60AC File Offset: 0x000A44AC
			public Text controllerNameLabel
			{
				get
				{
					return this._controllerNameLabel;
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06001B1C RID: 6940 RVA: 0x000A60B4 File Offset: 0x000A44B4
			public Button removeControllerButton
			{
				get
				{
					return this._removeControllerButton;
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06001B1D RID: 6941 RVA: 0x000A60BC File Offset: 0x000A44BC
			public Button assignControllerButton
			{
				get
				{
					return this._assignControllerButton;
				}
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06001B1E RID: 6942 RVA: 0x000A60C4 File Offset: 0x000A44C4
			public Button calibrateControllerButton
			{
				get
				{
					return this._calibrateControllerButton;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06001B1F RID: 6943 RVA: 0x000A60CC File Offset: 0x000A44CC
			public Button doneButton
			{
				get
				{
					return this._doneButton;
				}
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000A60D4 File Offset: 0x000A44D4
			public Button restoreDefaultsButton
			{
				get
				{
					return this._restoreDefaultsButton;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06001B21 RID: 6945 RVA: 0x000A60DC File Offset: 0x000A44DC
			public Selectable defaultSelection
			{
				get
				{
					return this._defaultSelection;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06001B22 RID: 6946 RVA: 0x000A60E4 File Offset: 0x000A44E4
			public GameObject[] fixedSelectableUIElements
			{
				get
				{
					return this._fixedSelectableUIElements;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06001B23 RID: 6947 RVA: 0x000A60EC File Offset: 0x000A44EC
			public Image mainBackgroundImage
			{
				get
				{
					return this._mainBackgroundImage;
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x06001B24 RID: 6948 RVA: 0x000A60F4 File Offset: 0x000A44F4
			// (set) Token: 0x06001B25 RID: 6949 RVA: 0x000A60FC File Offset: 0x000A44FC
			public LayoutElement inputGridLayoutElement { get; set; }

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x06001B26 RID: 6950 RVA: 0x000A6105 File Offset: 0x000A4505
			// (set) Token: 0x06001B27 RID: 6951 RVA: 0x000A610D File Offset: 0x000A450D
			public Transform inputGridActionColumn { get; set; }

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x06001B28 RID: 6952 RVA: 0x000A6116 File Offset: 0x000A4516
			// (set) Token: 0x06001B29 RID: 6953 RVA: 0x000A611E File Offset: 0x000A451E
			public Transform inputGridKeyboardColumn { get; set; }

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06001B2A RID: 6954 RVA: 0x000A6127 File Offset: 0x000A4527
			// (set) Token: 0x06001B2B RID: 6955 RVA: 0x000A612F File Offset: 0x000A452F
			public Transform inputGridMouseColumn { get; set; }

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000A6138 File Offset: 0x000A4538
			// (set) Token: 0x06001B2D RID: 6957 RVA: 0x000A6140 File Offset: 0x000A4540
			public Transform inputGridControllerColumn { get; set; }

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06001B2E RID: 6958 RVA: 0x000A6149 File Offset: 0x000A4549
			// (set) Token: 0x06001B2F RID: 6959 RVA: 0x000A6151 File Offset: 0x000A4551
			public Transform inputGridHeader1 { get; set; }

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000A615A File Offset: 0x000A455A
			// (set) Token: 0x06001B31 RID: 6961 RVA: 0x000A6162 File Offset: 0x000A4562
			public Transform inputGridHeader2 { get; set; }

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000A616B File Offset: 0x000A456B
			// (set) Token: 0x06001B33 RID: 6963 RVA: 0x000A6173 File Offset: 0x000A4573
			public Transform inputGridHeader3 { get; set; }

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000A617C File Offset: 0x000A457C
			// (set) Token: 0x06001B35 RID: 6965 RVA: 0x000A6184 File Offset: 0x000A4584
			public Transform inputGridHeader4 { get; set; }

			// Token: 0x06001B36 RID: 6966 RVA: 0x000A6190 File Offset: 0x000A4590
			public bool Check()
			{
				return !(this._canvas == null) && !(this._mainCanvasGroup == null) && !(this._mainContent == null) && !(this._mainContentInner == null) && !(this._playersGroup == null) && !(this._controllerGroup == null) && !(this._controllerGroupLabelGroup == null) && !(this._controllerSettingsGroup == null) && !(this._assignedControllersGroup == null) && !(this._settingsAndMapCategoriesGroup == null) && !(this._settingsGroup == null) && !(this._mapCategoriesGroup == null) && !(this._inputGridGroup == null) && !(this._inputGridContainer == null) && !(this._inputGridHeadersGroup == null) && !(this._inputGridVScrollbar == null) && !(this._inputGridScrollRect == null) && !(this._inputGridInnerGroup == null) && !(this._controllerNameLabel == null) && !(this._removeControllerButton == null) && !(this._assignControllerButton == null) && !(this._calibrateControllerButton == null) && !(this._doneButton == null) && !(this._restoreDefaultsButton == null) && !(this._defaultSelection == null);
			}

			// Token: 0x04001D0A RID: 7434
			[SerializeField]
			private Canvas _canvas;

			// Token: 0x04001D0B RID: 7435
			[SerializeField]
			private CanvasGroup _mainCanvasGroup;

			// Token: 0x04001D0C RID: 7436
			[SerializeField]
			private Transform _mainContent;

			// Token: 0x04001D0D RID: 7437
			[SerializeField]
			private Transform _mainContentInner;

			// Token: 0x04001D0E RID: 7438
			[SerializeField]
			private UIGroup _playersGroup;

			// Token: 0x04001D0F RID: 7439
			[SerializeField]
			private Transform _controllerGroup;

			// Token: 0x04001D10 RID: 7440
			[SerializeField]
			private Transform _controllerGroupLabelGroup;

			// Token: 0x04001D11 RID: 7441
			[SerializeField]
			private UIGroup _controllerSettingsGroup;

			// Token: 0x04001D12 RID: 7442
			[SerializeField]
			private UIGroup _assignedControllersGroup;

			// Token: 0x04001D13 RID: 7443
			[SerializeField]
			private Transform _settingsAndMapCategoriesGroup;

			// Token: 0x04001D14 RID: 7444
			[SerializeField]
			private UIGroup _settingsGroup;

			// Token: 0x04001D15 RID: 7445
			[SerializeField]
			private UIGroup _mapCategoriesGroup;

			// Token: 0x04001D16 RID: 7446
			[SerializeField]
			private Transform _inputGridGroup;

			// Token: 0x04001D17 RID: 7447
			[SerializeField]
			private Transform _inputGridContainer;

			// Token: 0x04001D18 RID: 7448
			[SerializeField]
			private Transform _inputGridHeadersGroup;

			// Token: 0x04001D19 RID: 7449
			[SerializeField]
			private Scrollbar _inputGridVScrollbar;

			// Token: 0x04001D1A RID: 7450
			[SerializeField]
			private ScrollRect _inputGridScrollRect;

			// Token: 0x04001D1B RID: 7451
			[SerializeField]
			private Transform _inputGridInnerGroup;

			// Token: 0x04001D1C RID: 7452
			[SerializeField]
			private Text _controllerNameLabel;

			// Token: 0x04001D1D RID: 7453
			[SerializeField]
			private Button _removeControllerButton;

			// Token: 0x04001D1E RID: 7454
			[SerializeField]
			private Button _assignControllerButton;

			// Token: 0x04001D1F RID: 7455
			[SerializeField]
			private Button _calibrateControllerButton;

			// Token: 0x04001D20 RID: 7456
			[SerializeField]
			private Button _doneButton;

			// Token: 0x04001D21 RID: 7457
			[SerializeField]
			private Button _restoreDefaultsButton;

			// Token: 0x04001D22 RID: 7458
			[SerializeField]
			private Selectable _defaultSelection;

			// Token: 0x04001D23 RID: 7459
			[SerializeField]
			private GameObject[] _fixedSelectableUIElements;

			// Token: 0x04001D24 RID: 7460
			[SerializeField]
			private Image _mainBackgroundImage;
		}

		// Token: 0x02000538 RID: 1336
		private class InputActionSet
		{
			// Token: 0x06001B37 RID: 6967 RVA: 0x000A6349 File Offset: 0x000A4749
			public InputActionSet(int actionId, AxisRange axisRange)
			{
				this._actionId = actionId;
				this._axisRange = axisRange;
			}

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x06001B38 RID: 6968 RVA: 0x000A635F File Offset: 0x000A475F
			public int actionId
			{
				get
				{
					return this._actionId;
				}
			}

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x06001B39 RID: 6969 RVA: 0x000A6367 File Offset: 0x000A4767
			public AxisRange axisRange
			{
				get
				{
					return this._axisRange;
				}
			}

			// Token: 0x04001D2E RID: 7470
			private int _actionId;

			// Token: 0x04001D2F RID: 7471
			private AxisRange _axisRange;
		}

		// Token: 0x02000539 RID: 1337
		private class InputMapping
		{
			// Token: 0x06001B3A RID: 6970 RVA: 0x000A636F File Offset: 0x000A476F
			public InputMapping(string actionName, InputFieldInfo fieldInfo, ControllerMap map, ActionElementMap aem, ControllerType controllerType, int controllerId)
			{
				this.actionName = actionName;
				this.fieldInfo = fieldInfo;
				this.map = map;
				this.aem = aem;
				this.controllerType = controllerType;
				this.controllerId = controllerId;
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x06001B3B RID: 6971 RVA: 0x000A63A4 File Offset: 0x000A47A4
			// (set) Token: 0x06001B3C RID: 6972 RVA: 0x000A63AC File Offset: 0x000A47AC
			public string actionName { get; private set; }

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x06001B3D RID: 6973 RVA: 0x000A63B5 File Offset: 0x000A47B5
			// (set) Token: 0x06001B3E RID: 6974 RVA: 0x000A63BD File Offset: 0x000A47BD
			public InputFieldInfo fieldInfo { get; private set; }

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06001B3F RID: 6975 RVA: 0x000A63C6 File Offset: 0x000A47C6
			// (set) Token: 0x06001B40 RID: 6976 RVA: 0x000A63CE File Offset: 0x000A47CE
			public ControllerMap map { get; private set; }

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x06001B41 RID: 6977 RVA: 0x000A63D7 File Offset: 0x000A47D7
			// (set) Token: 0x06001B42 RID: 6978 RVA: 0x000A63DF File Offset: 0x000A47DF
			public ActionElementMap aem { get; private set; }

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06001B43 RID: 6979 RVA: 0x000A63E8 File Offset: 0x000A47E8
			// (set) Token: 0x06001B44 RID: 6980 RVA: 0x000A63F0 File Offset: 0x000A47F0
			public ControllerType controllerType { get; private set; }

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x06001B45 RID: 6981 RVA: 0x000A63F9 File Offset: 0x000A47F9
			// (set) Token: 0x06001B46 RID: 6982 RVA: 0x000A6401 File Offset: 0x000A4801
			public int controllerId { get; private set; }

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x06001B47 RID: 6983 RVA: 0x000A640A File Offset: 0x000A480A
			// (set) Token: 0x06001B48 RID: 6984 RVA: 0x000A6412 File Offset: 0x000A4812
			public ControllerPollingInfo pollingInfo { get; set; }

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06001B49 RID: 6985 RVA: 0x000A641B File Offset: 0x000A481B
			// (set) Token: 0x06001B4A RID: 6986 RVA: 0x000A6423 File Offset: 0x000A4823
			public ModifierKeyFlags modifierKeyFlags { get; set; }

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x06001B4B RID: 6987 RVA: 0x000A642C File Offset: 0x000A482C
			public AxisRange axisRange
			{
				get
				{
					AxisRange result = AxisRange.Positive;
					if (this.pollingInfo.elementType == ControllerElementType.Axis)
					{
						if (this.fieldInfo.axisRange == AxisRange.Full)
						{
							result = AxisRange.Full;
						}
						else
						{
							result = ((this.pollingInfo.axisPole != Pole.Positive) ? AxisRange.Negative : AxisRange.Positive);
						}
					}
					return result;
				}
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000A6484 File Offset: 0x000A4884
			public string elementName
			{
				get
				{
					if (this.controllerType == ControllerType.Keyboard && this.modifierKeyFlags != ModifierKeyFlags.None)
					{
						return string.Format("{0} + {1}", Keyboard.ModifierKeyFlagsToString(this.modifierKeyFlags), this.pollingInfo.elementIdentifierName);
					}
					string result = this.pollingInfo.elementIdentifierName;
					if (this.pollingInfo.elementType == ControllerElementType.Axis)
					{
						if (this.axisRange == AxisRange.Positive)
						{
							result = this.pollingInfo.elementIdentifier.positiveName;
						}
						else if (this.axisRange == AxisRange.Negative)
						{
							result = this.pollingInfo.elementIdentifier.negativeName;
						}
					}
					return result;
				}
			}

			// Token: 0x06001B4D RID: 6989 RVA: 0x000A6535 File Offset: 0x000A4935
			public ElementAssignment ToElementAssignment(ControllerPollingInfo pollingInfo)
			{
				this.pollingInfo = pollingInfo;
				return this.ToElementAssignment();
			}

			// Token: 0x06001B4E RID: 6990 RVA: 0x000A6544 File Offset: 0x000A4944
			public ElementAssignment ToElementAssignment(ControllerPollingInfo pollingInfo, ModifierKeyFlags modifierKeyFlags)
			{
				this.pollingInfo = pollingInfo;
				this.modifierKeyFlags = modifierKeyFlags;
				return this.ToElementAssignment();
			}

			// Token: 0x06001B4F RID: 6991 RVA: 0x000A655C File Offset: 0x000A495C
			public ElementAssignment ToElementAssignment()
			{
				return new ElementAssignment(this.controllerType, this.pollingInfo.elementType, this.pollingInfo.elementIdentifierId, this.axisRange, this.pollingInfo.keyboardKey, this.modifierKeyFlags, this.fieldInfo.actionId, (this.fieldInfo.axisRange != AxisRange.Negative) ? Pole.Positive : Pole.Negative, false, (this.aem == null) ? -1 : this.aem.id);
			}
		}

		// Token: 0x0200053A RID: 1338
		private class AxisCalibrator
		{
			// Token: 0x06001B50 RID: 6992 RVA: 0x000A65EC File Offset: 0x000A49EC
			public AxisCalibrator(Joystick joystick, int axisIndex)
			{
				this.data = default(AxisCalibrationData);
				this.joystick = joystick;
				this.axisIndex = axisIndex;
				if (joystick != null && axisIndex >= 0 && joystick.axisCount > axisIndex)
				{
					this.axis = joystick.Axes[axisIndex];
					this.data = joystick.calibrationMap.GetAxis(axisIndex).GetData();
				}
				this.firstRun = true;
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000A6665 File Offset: 0x000A4A65
			public bool isValid
			{
				get
				{
					return this.axis != null;
				}
			}

			// Token: 0x06001B52 RID: 6994 RVA: 0x000A6674 File Offset: 0x000A4A74
			public void RecordMinMax()
			{
				if (this.axis == null)
				{
					return;
				}
				float valueRaw = this.axis.valueRaw;
				if (this.firstRun || valueRaw < this.data.min)
				{
					this.data.min = valueRaw;
				}
				if (this.firstRun || valueRaw > this.data.max)
				{
					this.data.max = valueRaw;
				}
				this.firstRun = false;
			}

			// Token: 0x06001B53 RID: 6995 RVA: 0x000A66F0 File Offset: 0x000A4AF0
			public void RecordZero()
			{
				if (this.axis == null)
				{
					return;
				}
				this.data.zero = this.axis.valueRaw;
			}

			// Token: 0x06001B54 RID: 6996 RVA: 0x000A6714 File Offset: 0x000A4B14
			public void Commit()
			{
				if (this.axis == null)
				{
					return;
				}
				AxisCalibration axisCalibration = this.joystick.calibrationMap.GetAxis(this.axisIndex);
				if (axisCalibration == null)
				{
					return;
				}
				if ((double)Mathf.Abs(this.data.max - this.data.min) < 0.1)
				{
					return;
				}
				axisCalibration.SetData(this.data);
			}

			// Token: 0x04001D38 RID: 7480
			public AxisCalibrationData data;

			// Token: 0x04001D39 RID: 7481
			public readonly Joystick joystick;

			// Token: 0x04001D3A RID: 7482
			public readonly int axisIndex;

			// Token: 0x04001D3B RID: 7483
			private Controller.Axis axis;

			// Token: 0x04001D3C RID: 7484
			private bool firstRun;
		}

		// Token: 0x0200053B RID: 1339
		private class IndexedDictionary<TKey, TValue>
		{
			// Token: 0x06001B55 RID: 6997 RVA: 0x000A6783 File Offset: 0x000A4B83
			public IndexedDictionary()
			{
				this.list = new List<ControlMapper.IndexedDictionary<TKey, TValue>.Entry>();
			}

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000A6796 File Offset: 0x000A4B96
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x17000226 RID: 550
			public TValue this[int index]
			{
				get
				{
					return this.list[index].value;
				}
			}

			// Token: 0x06001B58 RID: 7000 RVA: 0x000A67B8 File Offset: 0x000A4BB8
			public TValue Get(TKey key)
			{
				int num = this.IndexOfKey(key);
				if (num < 0)
				{
					throw new Exception("Key does not exist!");
				}
				return this.list[num].value;
			}

			// Token: 0x06001B59 RID: 7001 RVA: 0x000A67F0 File Offset: 0x000A4BF0
			public bool TryGet(TKey key, out TValue value)
			{
				value = default(TValue);
				int num = this.IndexOfKey(key);
				if (num < 0)
				{
					return false;
				}
				value = this.list[num].value;
				return true;
			}

			// Token: 0x06001B5A RID: 7002 RVA: 0x000A6838 File Offset: 0x000A4C38
			public void Add(TKey key, TValue value)
			{
				if (this.ContainsKey(key))
				{
					throw new Exception("Key " + key.ToString() + " is already in use!");
				}
				this.list.Add(new ControlMapper.IndexedDictionary<TKey, TValue>.Entry(key, value));
			}

			// Token: 0x06001B5B RID: 7003 RVA: 0x000A6888 File Offset: 0x000A4C88
			public int IndexOfKey(TKey key)
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					if (EqualityComparer<TKey>.Default.Equals(this.list[i].key, key))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06001B5C RID: 7004 RVA: 0x000A68D8 File Offset: 0x000A4CD8
			public bool ContainsKey(TKey key)
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					if (EqualityComparer<TKey>.Default.Equals(this.list[i].key, key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001B5D RID: 7005 RVA: 0x000A6927 File Offset: 0x000A4D27
			public void Clear()
			{
				this.list.Clear();
			}

			// Token: 0x04001D3D RID: 7485
			private List<ControlMapper.IndexedDictionary<TKey, TValue>.Entry> list;

			// Token: 0x0200053C RID: 1340
			private class Entry
			{
				// Token: 0x06001B5E RID: 7006 RVA: 0x000A6934 File Offset: 0x000A4D34
				public Entry(TKey key, TValue value)
				{
					this.key = key;
					this.value = value;
				}

				// Token: 0x04001D3E RID: 7486
				public TKey key;

				// Token: 0x04001D3F RID: 7487
				public TValue value;
			}
		}

		// Token: 0x0200053D RID: 1341
		private enum LayoutElementSizeType
		{
			// Token: 0x04001D41 RID: 7489
			MinSize,
			// Token: 0x04001D42 RID: 7490
			PreferredSize
		}

		// Token: 0x0200053E RID: 1342
		private enum WindowType
		{
			// Token: 0x04001D44 RID: 7492
			None,
			// Token: 0x04001D45 RID: 7493
			ChooseJoystick,
			// Token: 0x04001D46 RID: 7494
			JoystickAssignmentConflict,
			// Token: 0x04001D47 RID: 7495
			ElementAssignment,
			// Token: 0x04001D48 RID: 7496
			ElementAssignmentPrePolling,
			// Token: 0x04001D49 RID: 7497
			ElementAssignmentPolling,
			// Token: 0x04001D4A RID: 7498
			ElementAssignmentResult,
			// Token: 0x04001D4B RID: 7499
			ElementAssignmentConflict,
			// Token: 0x04001D4C RID: 7500
			Calibration,
			// Token: 0x04001D4D RID: 7501
			CalibrateStep1,
			// Token: 0x04001D4E RID: 7502
			CalibrateStep2
		}

		// Token: 0x0200053F RID: 1343
		private class InputGrid
		{
			// Token: 0x06001B5F RID: 7007 RVA: 0x000A694A File Offset: 0x000A4D4A
			public InputGrid()
			{
				this.list = new ControlMapper.InputGridEntryList();
				this.groups = new List<GameObject>();
			}

			// Token: 0x06001B60 RID: 7008 RVA: 0x000A6968 File Offset: 0x000A4D68
			public void AddMapCategory(int mapCategoryId)
			{
				this.list.AddMapCategory(mapCategoryId);
			}

			// Token: 0x06001B61 RID: 7009 RVA: 0x000A6976 File Offset: 0x000A4D76
			public void AddAction(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				this.list.AddAction(mapCategoryId, action, axisRange);
			}

			// Token: 0x06001B62 RID: 7010 RVA: 0x000A6986 File Offset: 0x000A4D86
			public void AddActionCategory(int mapCategoryId, int actionCategoryId)
			{
				this.list.AddActionCategory(mapCategoryId, actionCategoryId);
			}

			// Token: 0x06001B63 RID: 7011 RVA: 0x000A6995 File Offset: 0x000A4D95
			public void AddInputFieldSet(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, GameObject fieldSetContainer)
			{
				this.list.AddInputFieldSet(mapCategoryId, action, axisRange, controllerType, fieldSetContainer);
			}

			// Token: 0x06001B64 RID: 7012 RVA: 0x000A69A9 File Offset: 0x000A4DA9
			public void AddInputField(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
			{
				this.list.AddInputField(mapCategoryId, action, axisRange, controllerType, fieldIndex, inputField);
			}

			// Token: 0x06001B65 RID: 7013 RVA: 0x000A69BF File Offset: 0x000A4DBF
			public void AddGroup(GameObject group)
			{
				this.groups.Add(group);
			}

			// Token: 0x06001B66 RID: 7014 RVA: 0x000A69CD File Offset: 0x000A4DCD
			public void AddActionLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControlMapper.GUILabel label)
			{
				this.list.AddActionLabel(mapCategoryId, actionId, axisRange, label);
			}

			// Token: 0x06001B67 RID: 7015 RVA: 0x000A69DF File Offset: 0x000A4DDF
			public void AddActionCategoryLabel(int mapCategoryId, int actionCategoryId, ControlMapper.GUILabel label)
			{
				this.list.AddActionCategoryLabel(mapCategoryId, actionCategoryId, label);
			}

			// Token: 0x06001B68 RID: 7016 RVA: 0x000A69EF File Offset: 0x000A4DEF
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				return this.list.Contains(mapCategoryId, actionId, axisRange, controllerType, fieldIndex);
			}

			// Token: 0x06001B69 RID: 7017 RVA: 0x000A6A03 File Offset: 0x000A4E03
			public ControlMapper.GUIInputField GetGUIInputField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				return this.list.GetGUIInputField(mapCategoryId, actionId, axisRange, controllerType, fieldIndex);
			}

			// Token: 0x06001B6A RID: 7018 RVA: 0x000A6A17 File Offset: 0x000A4E17
			public IEnumerable<ControlMapper.InputActionSet> GetActionSets(int mapCategoryId)
			{
				return this.list.GetActionSets(mapCategoryId);
			}

			// Token: 0x06001B6B RID: 7019 RVA: 0x000A6A25 File Offset: 0x000A4E25
			public void SetColumnHeight(int mapCategoryId, float height)
			{
				this.list.SetColumnHeight(mapCategoryId, height);
			}

			// Token: 0x06001B6C RID: 7020 RVA: 0x000A6A34 File Offset: 0x000A4E34
			public float GetColumnHeight(int mapCategoryId)
			{
				return this.list.GetColumnHeight(mapCategoryId);
			}

			// Token: 0x06001B6D RID: 7021 RVA: 0x000A6A42 File Offset: 0x000A4E42
			public void SetFieldsActive(int mapCategoryId, bool state)
			{
				this.list.SetFieldsActive(mapCategoryId, state);
			}

			// Token: 0x06001B6E RID: 7022 RVA: 0x000A6A51 File Offset: 0x000A4E51
			public void SetFieldLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int index, string label)
			{
				this.list.SetLabel(mapCategoryId, actionId, axisRange, controllerType, index, label);
			}

			// Token: 0x06001B6F RID: 7023 RVA: 0x000A6A68 File Offset: 0x000A4E68
			public void PopulateField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId, int index, int actionElementMapId, string label, bool invert)
			{
				this.list.PopulateField(mapCategoryId, actionId, axisRange, controllerType, controllerId, index, actionElementMapId, label, invert);
			}

			// Token: 0x06001B70 RID: 7024 RVA: 0x000A6A8F File Offset: 0x000A4E8F
			public void SetFixedFieldData(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId)
			{
				this.list.SetFixedFieldData(mapCategoryId, actionId, axisRange, controllerType, controllerId);
			}

			// Token: 0x06001B71 RID: 7025 RVA: 0x000A6AA3 File Offset: 0x000A4EA3
			public void InitializeFields(int mapCategoryId)
			{
				this.list.InitializeFields(mapCategoryId);
			}

			// Token: 0x06001B72 RID: 7026 RVA: 0x000A6AB1 File Offset: 0x000A4EB1
			public void Show(int mapCategoryId)
			{
				this.list.Show(mapCategoryId);
			}

			// Token: 0x06001B73 RID: 7027 RVA: 0x000A6ABF File Offset: 0x000A4EBF
			public void HideAll()
			{
				this.list.HideAll();
			}

			// Token: 0x06001B74 RID: 7028 RVA: 0x000A6ACC File Offset: 0x000A4ECC
			public void ClearLabels(int mapCategoryId)
			{
				this.list.ClearLabels(mapCategoryId);
			}

			// Token: 0x06001B75 RID: 7029 RVA: 0x000A6ADC File Offset: 0x000A4EDC
			private void ClearGroups()
			{
				for (int i = 0; i < this.groups.Count; i++)
				{
					if (!(this.groups[i] == null))
					{
						UnityEngine.Object.Destroy(this.groups[i]);
					}
				}
			}

			// Token: 0x06001B76 RID: 7030 RVA: 0x000A6B32 File Offset: 0x000A4F32
			public void ClearAll()
			{
				this.ClearGroups();
				this.list.Clear();
			}

			// Token: 0x04001D4F RID: 7503
			private ControlMapper.InputGridEntryList list;

			// Token: 0x04001D50 RID: 7504
			private List<GameObject> groups;
		}

		// Token: 0x02000540 RID: 1344
		private class InputGridEntryList
		{
			// Token: 0x06001B77 RID: 7031 RVA: 0x000A6B45 File Offset: 0x000A4F45
			public InputGridEntryList()
			{
				this.entries = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.MapCategoryEntry>();
			}

			// Token: 0x06001B78 RID: 7032 RVA: 0x000A6B58 File Offset: 0x000A4F58
			public void AddMapCategory(int mapCategoryId)
			{
				if (mapCategoryId < 0)
				{
					return;
				}
				if (this.entries.ContainsKey(mapCategoryId))
				{
					return;
				}
				this.entries.Add(mapCategoryId, new ControlMapper.InputGridEntryList.MapCategoryEntry());
			}

			// Token: 0x06001B79 RID: 7033 RVA: 0x000A6B85 File Offset: 0x000A4F85
			public void AddAction(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				this.AddActionEntry(mapCategoryId, action, axisRange);
			}

			// Token: 0x06001B7A RID: 7034 RVA: 0x000A6B94 File Offset: 0x000A4F94
			private ControlMapper.InputGridEntryList.ActionEntry AddActionEntry(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				if (action == null)
				{
					return null;
				}
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return null;
				}
				return mapCategoryEntry.AddAction(action, axisRange);
			}

			// Token: 0x06001B7B RID: 7035 RVA: 0x000A6BC8 File Offset: 0x000A4FC8
			public void AddActionLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControlMapper.GUILabel label)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = mapCategoryEntry.GetActionEntry(actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetLabel(label);
			}

			// Token: 0x06001B7C RID: 7036 RVA: 0x000A6C01 File Offset: 0x000A5001
			public void AddActionCategory(int mapCategoryId, int actionCategoryId)
			{
				this.AddActionCategoryEntry(mapCategoryId, actionCategoryId);
			}

			// Token: 0x06001B7D RID: 7037 RVA: 0x000A6C0C File Offset: 0x000A500C
			private ControlMapper.InputGridEntryList.ActionCategoryEntry AddActionCategoryEntry(int mapCategoryId, int actionCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return null;
				}
				return mapCategoryEntry.AddActionCategory(actionCategoryId);
			}

			// Token: 0x06001B7E RID: 7038 RVA: 0x000A6C38 File Offset: 0x000A5038
			public void AddActionCategoryLabel(int mapCategoryId, int actionCategoryId, ControlMapper.GUILabel label)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				ControlMapper.InputGridEntryList.ActionCategoryEntry actionCategoryEntry = mapCategoryEntry.GetActionCategoryEntry(actionCategoryId);
				if (actionCategoryEntry == null)
				{
					return;
				}
				actionCategoryEntry.SetLabel(label);
			}

			// Token: 0x06001B7F RID: 7039 RVA: 0x000A6C70 File Offset: 0x000A5070
			public void AddInputFieldSet(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, GameObject fieldSetContainer)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, action, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.AddInputFieldSet(controllerType, fieldSetContainer);
			}

			// Token: 0x06001B80 RID: 7040 RVA: 0x000A6C98 File Offset: 0x000A5098
			public void AddInputField(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, action, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.AddInputField(controllerType, fieldIndex, inputField);
			}

			// Token: 0x06001B81 RID: 7041 RVA: 0x000A6CC2 File Offset: 0x000A50C2
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange)
			{
				return this.GetActionEntry(mapCategoryId, actionId, axisRange) != null;
			}

			// Token: 0x06001B82 RID: 7042 RVA: 0x000A6CD4 File Offset: 0x000A50D4
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				return actionEntry != null && actionEntry.Contains(controllerType, fieldIndex);
			}

			// Token: 0x06001B83 RID: 7043 RVA: 0x000A6D00 File Offset: 0x000A5100
			public void SetColumnHeight(int mapCategoryId, float height)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				mapCategoryEntry.columnHeight = height;
			}

			// Token: 0x06001B84 RID: 7044 RVA: 0x000A6D28 File Offset: 0x000A5128
			public float GetColumnHeight(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return 0f;
				}
				return mapCategoryEntry.columnHeight;
			}

			// Token: 0x06001B85 RID: 7045 RVA: 0x000A6D54 File Offset: 0x000A5154
			public ControlMapper.GUIInputField GetGUIInputField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return null;
				}
				return actionEntry.GetGUIInputField(controllerType, fieldIndex);
			}

			// Token: 0x06001B86 RID: 7046 RVA: 0x000A6D80 File Offset: 0x000A5180
			private ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int mapCategoryId, int actionId, AxisRange axisRange)
			{
				if (actionId < 0)
				{
					return null;
				}
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return null;
				}
				return mapCategoryEntry.GetActionEntry(actionId, axisRange);
			}

			// Token: 0x06001B87 RID: 7047 RVA: 0x000A6DB5 File Offset: 0x000A51B5
			private ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				if (action == null)
				{
					return null;
				}
				return this.GetActionEntry(mapCategoryId, action.id, axisRange);
			}

			// Token: 0x06001B88 RID: 7048 RVA: 0x000A6DD0 File Offset: 0x000A51D0
			public IEnumerable<ControlMapper.InputActionSet> GetActionSets(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry entry;
				if (!this.entries.TryGet(mapCategoryId, out entry))
				{
					yield break;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> list = entry.actionList;
				int count = (list == null) ? 0 : list.Count;
				for (int i = 0; i < count; i++)
				{
					yield return list[i].actionSet;
				}
				yield break;
			}

			// Token: 0x06001B89 RID: 7049 RVA: 0x000A6DFC File Offset: 0x000A51FC
			public void SetFieldsActive(int mapCategoryId, bool state)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> actionList = mapCategoryEntry.actionList;
				int num = (actionList == null) ? 0 : actionList.Count;
				for (int i = 0; i < num; i++)
				{
					actionList[i].SetFieldsActive(state);
				}
			}

			// Token: 0x06001B8A RID: 7050 RVA: 0x000A6E58 File Offset: 0x000A5258
			public void SetLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int index, string label)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetFieldLabel(controllerType, index, label);
			}

			// Token: 0x06001B8B RID: 7051 RVA: 0x000A6E84 File Offset: 0x000A5284
			public void PopulateField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId, int index, int actionElementMapId, string label, bool invert)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.PopulateField(controllerType, controllerId, index, actionElementMapId, label, invert);
			}

			// Token: 0x06001B8C RID: 7052 RVA: 0x000A6EB4 File Offset: 0x000A52B4
			public void SetFixedFieldData(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetFixedFieldData(controllerType, controllerId);
			}

			// Token: 0x06001B8D RID: 7053 RVA: 0x000A6EDC File Offset: 0x000A52DC
			public void InitializeFields(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> actionList = mapCategoryEntry.actionList;
				int num = (actionList == null) ? 0 : actionList.Count;
				for (int i = 0; i < num; i++)
				{
					actionList[i].Initialize();
				}
			}

			// Token: 0x06001B8E RID: 7054 RVA: 0x000A6F38 File Offset: 0x000A5338
			public void Show(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				mapCategoryEntry.SetAllActive(true);
			}

			// Token: 0x06001B8F RID: 7055 RVA: 0x000A6F60 File Offset: 0x000A5360
			public void HideAll()
			{
				for (int i = 0; i < this.entries.Count; i++)
				{
					this.entries[i].SetAllActive(false);
				}
			}

			// Token: 0x06001B90 RID: 7056 RVA: 0x000A6F9C File Offset: 0x000A539C
			public void ClearLabels(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> actionList = mapCategoryEntry.actionList;
				int num = (actionList == null) ? 0 : actionList.Count;
				for (int i = 0; i < num; i++)
				{
					actionList[i].ClearLabels();
				}
			}

			// Token: 0x06001B91 RID: 7057 RVA: 0x000A6FF5 File Offset: 0x000A53F5
			public void Clear()
			{
				this.entries.Clear();
			}

			// Token: 0x04001D51 RID: 7505
			private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.MapCategoryEntry> entries;

			// Token: 0x02000541 RID: 1345
			private class MapCategoryEntry
			{
				// Token: 0x06001B92 RID: 7058 RVA: 0x000A7002 File Offset: 0x000A5402
				public MapCategoryEntry()
				{
					this._actionList = new List<ControlMapper.InputGridEntryList.ActionEntry>();
					this._actionCategoryList = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry>();
				}

				// Token: 0x17000227 RID: 551
				// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000A7020 File Offset: 0x000A5420
				public List<ControlMapper.InputGridEntryList.ActionEntry> actionList
				{
					get
					{
						return this._actionList;
					}
				}

				// Token: 0x17000228 RID: 552
				// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000A7028 File Offset: 0x000A5428
				public ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry> actionCategoryList
				{
					get
					{
						return this._actionCategoryList;
					}
				}

				// Token: 0x17000229 RID: 553
				// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000A7030 File Offset: 0x000A5430
				// (set) Token: 0x06001B96 RID: 7062 RVA: 0x000A7038 File Offset: 0x000A5438
				public float columnHeight
				{
					get
					{
						return this._columnHeight;
					}
					set
					{
						this._columnHeight = value;
					}
				}

				// Token: 0x06001B97 RID: 7063 RVA: 0x000A7044 File Offset: 0x000A5444
				public ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int actionId, AxisRange axisRange)
				{
					int num = this.IndexOfActionEntry(actionId, axisRange);
					if (num < 0)
					{
						return null;
					}
					return this._actionList[num];
				}

				// Token: 0x06001B98 RID: 7064 RVA: 0x000A7070 File Offset: 0x000A5470
				public int IndexOfActionEntry(int actionId, AxisRange axisRange)
				{
					int count = this._actionList.Count;
					for (int i = 0; i < count; i++)
					{
						if (this._actionList[i].Matches(actionId, axisRange))
						{
							return i;
						}
					}
					return -1;
				}

				// Token: 0x06001B99 RID: 7065 RVA: 0x000A70B6 File Offset: 0x000A54B6
				public bool ContainsActionEntry(int actionId, AxisRange axisRange)
				{
					return this.IndexOfActionEntry(actionId, axisRange) >= 0;
				}

				// Token: 0x06001B9A RID: 7066 RVA: 0x000A70C8 File Offset: 0x000A54C8
				public ControlMapper.InputGridEntryList.ActionEntry AddAction(InputAction action, AxisRange axisRange)
				{
					if (action == null)
					{
						return null;
					}
					if (this.ContainsActionEntry(action.id, axisRange))
					{
						return null;
					}
					this._actionList.Add(new ControlMapper.InputGridEntryList.ActionEntry(action, axisRange));
					return this._actionList[this._actionList.Count - 1];
				}

				// Token: 0x06001B9B RID: 7067 RVA: 0x000A711B File Offset: 0x000A551B
				public ControlMapper.InputGridEntryList.ActionCategoryEntry GetActionCategoryEntry(int actionCategoryId)
				{
					if (!this._actionCategoryList.ContainsKey(actionCategoryId))
					{
						return null;
					}
					return this._actionCategoryList.Get(actionCategoryId);
				}

				// Token: 0x06001B9C RID: 7068 RVA: 0x000A713C File Offset: 0x000A553C
				public ControlMapper.InputGridEntryList.ActionCategoryEntry AddActionCategory(int actionCategoryId)
				{
					if (actionCategoryId < 0)
					{
						return null;
					}
					if (this._actionCategoryList.ContainsKey(actionCategoryId))
					{
						return null;
					}
					this._actionCategoryList.Add(actionCategoryId, new ControlMapper.InputGridEntryList.ActionCategoryEntry(actionCategoryId));
					return this._actionCategoryList.Get(actionCategoryId);
				}

				// Token: 0x06001B9D RID: 7069 RVA: 0x000A7178 File Offset: 0x000A5578
				public void SetAllActive(bool state)
				{
					for (int i = 0; i < this._actionCategoryList.Count; i++)
					{
						this._actionCategoryList[i].SetActive(state);
					}
					for (int j = 0; j < this._actionList.Count; j++)
					{
						this._actionList[j].SetActive(state);
					}
				}

				// Token: 0x04001D52 RID: 7506
				private List<ControlMapper.InputGridEntryList.ActionEntry> _actionList;

				// Token: 0x04001D53 RID: 7507
				private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry> _actionCategoryList;

				// Token: 0x04001D54 RID: 7508
				private float _columnHeight;
			}

			// Token: 0x02000542 RID: 1346
			private class ActionEntry
			{
				// Token: 0x06001B9E RID: 7070 RVA: 0x000A71E1 File Offset: 0x000A55E1
				public ActionEntry(InputAction action, AxisRange axisRange)
				{
					this.action = action;
					this.axisRange = axisRange;
					this.actionSet = new ControlMapper.InputActionSet(action.id, axisRange);
					this.fieldSets = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.FieldSet>();
				}

				// Token: 0x06001B9F RID: 7071 RVA: 0x000A7214 File Offset: 0x000A5614
				public void SetLabel(ControlMapper.GUILabel label)
				{
					this.label = label;
				}

				// Token: 0x06001BA0 RID: 7072 RVA: 0x000A721D File Offset: 0x000A561D
				public bool Matches(int actionId, AxisRange axisRange)
				{
					return this.action.id == actionId && this.axisRange == axisRange;
				}

				// Token: 0x06001BA1 RID: 7073 RVA: 0x000A7241 File Offset: 0x000A5641
				public void AddInputFieldSet(ControllerType controllerType, GameObject fieldSetContainer)
				{
					if (this.fieldSets.ContainsKey((int)controllerType))
					{
						return;
					}
					this.fieldSets.Add((int)controllerType, new ControlMapper.InputGridEntryList.FieldSet(fieldSetContainer));
				}

				// Token: 0x06001BA2 RID: 7074 RVA: 0x000A7268 File Offset: 0x000A5668
				public void AddInputField(ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
				{
					if (!this.fieldSets.ContainsKey((int)controllerType))
					{
						return;
					}
					ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets.Get((int)controllerType);
					if (fieldSet.fields.ContainsKey(fieldIndex))
					{
						return;
					}
					fieldSet.fields.Add(fieldIndex, inputField);
				}

				// Token: 0x06001BA3 RID: 7075 RVA: 0x000A72B4 File Offset: 0x000A56B4
				public ControlMapper.GUIInputField GetGUIInputField(ControllerType controllerType, int fieldIndex)
				{
					if (!this.fieldSets.ContainsKey((int)controllerType))
					{
						return null;
					}
					if (!this.fieldSets.Get((int)controllerType).fields.ContainsKey(fieldIndex))
					{
						return null;
					}
					return this.fieldSets.Get((int)controllerType).fields.Get(fieldIndex);
				}

				// Token: 0x06001BA4 RID: 7076 RVA: 0x000A7309 File Offset: 0x000A5709
				public bool Contains(ControllerType controllerType, int fieldId)
				{
					return this.fieldSets.ContainsKey((int)controllerType) && this.fieldSets.Get((int)controllerType).fields.ContainsKey(fieldId);
				}

				// Token: 0x06001BA5 RID: 7077 RVA: 0x000A7340 File Offset: 0x000A5740
				public void SetFieldLabel(ControllerType controllerType, int index, string label)
				{
					if (!this.fieldSets.ContainsKey((int)controllerType))
					{
						return;
					}
					if (!this.fieldSets.Get((int)controllerType).fields.ContainsKey(index))
					{
						return;
					}
					this.fieldSets.Get((int)controllerType).fields.Get(index).SetLabel(label);
				}

				// Token: 0x06001BA6 RID: 7078 RVA: 0x000A739C File Offset: 0x000A579C
				public void PopulateField(ControllerType controllerType, int controllerId, int index, int actionElementMapId, string label, bool invert)
				{
					if (!this.fieldSets.ContainsKey((int)controllerType))
					{
						return;
					}
					if (!this.fieldSets.Get((int)controllerType).fields.ContainsKey(index))
					{
						return;
					}
					ControlMapper.GUIInputField guiinputField = this.fieldSets.Get((int)controllerType).fields.Get(index);
					guiinputField.SetLabel(label);
					guiinputField.actionElementMapId = actionElementMapId;
					guiinputField.controllerId = controllerId;
					if (guiinputField.hasToggle)
					{
						guiinputField.toggle.SetInteractible(true, false);
						guiinputField.toggle.SetToggleState(invert);
						guiinputField.toggle.actionElementMapId = actionElementMapId;
					}
				}

				// Token: 0x06001BA7 RID: 7079 RVA: 0x000A743C File Offset: 0x000A583C
				public void SetFixedFieldData(ControllerType controllerType, int controllerId)
				{
					if (!this.fieldSets.ContainsKey((int)controllerType))
					{
						return;
					}
					ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets.Get((int)controllerType);
					int count = fieldSet.fields.Count;
					for (int i = 0; i < count; i++)
					{
						fieldSet.fields[i].controllerId = controllerId;
					}
				}

				// Token: 0x06001BA8 RID: 7080 RVA: 0x000A7498 File Offset: 0x000A5898
				public void Initialize()
				{
					for (int i = 0; i < this.fieldSets.Count; i++)
					{
						ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets[i];
						int count = fieldSet.fields.Count;
						for (int j = 0; j < count; j++)
						{
							ControlMapper.GUIInputField guiinputField = fieldSet.fields[j];
							if (guiinputField.hasToggle)
							{
								guiinputField.toggle.SetInteractible(false, false);
								guiinputField.toggle.SetToggleState(false);
								guiinputField.toggle.actionElementMapId = -1;
							}
							guiinputField.SetLabel(string.Empty);
							guiinputField.actionElementMapId = -1;
							guiinputField.controllerId = -1;
						}
					}
				}

				// Token: 0x06001BA9 RID: 7081 RVA: 0x000A754C File Offset: 0x000A594C
				public void SetActive(bool state)
				{
					if (this.label != null)
					{
						this.label.SetActive(state);
					}
					int count = this.fieldSets.Count;
					for (int i = 0; i < count; i++)
					{
						this.fieldSets[i].groupContainer.SetActive(state);
					}
				}

				// Token: 0x06001BAA RID: 7082 RVA: 0x000A75A8 File Offset: 0x000A59A8
				public void ClearLabels()
				{
					for (int i = 0; i < this.fieldSets.Count; i++)
					{
						ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets[i];
						int count = fieldSet.fields.Count;
						for (int j = 0; j < count; j++)
						{
							ControlMapper.GUIInputField guiinputField = fieldSet.fields[j];
							guiinputField.SetLabel(string.Empty);
						}
					}
				}

				// Token: 0x06001BAB RID: 7083 RVA: 0x000A7618 File Offset: 0x000A5A18
				public void SetFieldsActive(bool state)
				{
					for (int i = 0; i < this.fieldSets.Count; i++)
					{
						ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets[i];
						int count = fieldSet.fields.Count;
						for (int j = 0; j < count; j++)
						{
							ControlMapper.GUIInputField guiinputField = fieldSet.fields[j];
							guiinputField.SetInteractible(state, false);
							if (guiinputField.hasToggle && (!state || guiinputField.toggle.actionElementMapId >= 0))
							{
								guiinputField.toggle.SetInteractible(state, false);
							}
						}
					}
				}

				// Token: 0x04001D55 RID: 7509
				private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.FieldSet> fieldSets;

				// Token: 0x04001D56 RID: 7510
				public ControlMapper.GUILabel label;

				// Token: 0x04001D57 RID: 7511
				public readonly InputAction action;

				// Token: 0x04001D58 RID: 7512
				public readonly AxisRange axisRange;

				// Token: 0x04001D59 RID: 7513
				public readonly ControlMapper.InputActionSet actionSet;
			}

			// Token: 0x02000543 RID: 1347
			private class FieldSet
			{
				// Token: 0x06001BAC RID: 7084 RVA: 0x000A76B5 File Offset: 0x000A5AB5
				public FieldSet(GameObject groupContainer)
				{
					this.groupContainer = groupContainer;
					this.fields = new ControlMapper.IndexedDictionary<int, ControlMapper.GUIInputField>();
				}

				// Token: 0x04001D5A RID: 7514
				public readonly GameObject groupContainer;

				// Token: 0x04001D5B RID: 7515
				public readonly ControlMapper.IndexedDictionary<int, ControlMapper.GUIInputField> fields;
			}

			// Token: 0x02000544 RID: 1348
			private class ActionCategoryEntry
			{
				// Token: 0x06001BAD RID: 7085 RVA: 0x000A76CF File Offset: 0x000A5ACF
				public ActionCategoryEntry(int actionCategoryId)
				{
					this.actionCategoryId = actionCategoryId;
				}

				// Token: 0x06001BAE RID: 7086 RVA: 0x000A76DE File Offset: 0x000A5ADE
				public void SetLabel(ControlMapper.GUILabel label)
				{
					this.label = label;
				}

				// Token: 0x06001BAF RID: 7087 RVA: 0x000A76E7 File Offset: 0x000A5AE7
				public void SetActive(bool state)
				{
					if (this.label != null)
					{
						this.label.SetActive(state);
					}
				}

				// Token: 0x04001D5C RID: 7516
				public readonly int actionCategoryId;

				// Token: 0x04001D5D RID: 7517
				public ControlMapper.GUILabel label;
			}
		}

		// Token: 0x02000545 RID: 1349
		private class WindowManager
		{
			// Token: 0x06001BB0 RID: 7088 RVA: 0x000A7864 File Offset: 0x000A5C64
			public WindowManager(GameObject windowPrefab, GameObject faderPrefab, Transform parent)
			{
				this.windowPrefab = windowPrefab;
				this.parent = parent;
				this.windows = new List<Window>();
				this.fader = UnityEngine.Object.Instantiate<GameObject>(faderPrefab);
				this.fader.transform.SetParent(parent, false);
				this.fader.GetComponent<RectTransform>().localScale = Vector2.one;
				this.SetFaderActive(false);
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x000A78D0 File Offset: 0x000A5CD0
			public bool isWindowOpen
			{
				get
				{
					for (int i = this.windows.Count - 1; i >= 0; i--)
					{
						if (!(this.windows[i] == null))
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x000A791C File Offset: 0x000A5D1C
			public Window topWindow
			{
				get
				{
					for (int i = this.windows.Count - 1; i >= 0; i--)
					{
						if (!(this.windows[i] == null))
						{
							return this.windows[i];
						}
					}
					return null;
				}
			}

			// Token: 0x06001BB3 RID: 7091 RVA: 0x000A7974 File Offset: 0x000A5D74
			public Window OpenWindow(string name, int width, int height)
			{
				Window result = this.InstantiateWindow(name, width, height);
				this.UpdateFader();
				return result;
			}

			// Token: 0x06001BB4 RID: 7092 RVA: 0x000A7994 File Offset: 0x000A5D94
			public Window OpenWindow(GameObject windowPrefab, string name)
			{
				if (windowPrefab == null)
				{
					UnityEngine.Debug.LogError("Rewired Control Mapper: Window Prefab is null!");
					return null;
				}
				Window result = this.InstantiateWindow(name, windowPrefab);
				this.UpdateFader();
				return result;
			}

			// Token: 0x06001BB5 RID: 7093 RVA: 0x000A79CC File Offset: 0x000A5DCC
			public void CloseTop()
			{
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null))
					{
						this.DestroyWindow(this.windows[i]);
						this.windows.RemoveAt(i);
						break;
					}
					this.windows.RemoveAt(i);
				}
				this.UpdateFader();
			}

			// Token: 0x06001BB6 RID: 7094 RVA: 0x000A7A48 File Offset: 0x000A5E48
			public void CloseWindow(int windowId)
			{
				this.CloseWindow(this.GetWindow(windowId));
			}

			// Token: 0x06001BB7 RID: 7095 RVA: 0x000A7A58 File Offset: 0x000A5E58
			public void CloseWindow(Window window)
			{
				if (window == null)
				{
					return;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (this.windows[i] == null)
					{
						this.windows.RemoveAt(i);
					}
					else if (!(this.windows[i] != window))
					{
						this.DestroyWindow(this.windows[i]);
						this.windows.RemoveAt(i);
						break;
					}
				}
				this.UpdateFader();
				this.FocusTopWindow();
			}

			// Token: 0x06001BB8 RID: 7096 RVA: 0x000A7B04 File Offset: 0x000A5F04
			public void CloseAll()
			{
				this.SetFaderActive(false);
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (this.windows[i] == null)
					{
						this.windows.RemoveAt(i);
					}
					else
					{
						this.DestroyWindow(this.windows[i]);
						this.windows.RemoveAt(i);
					}
				}
				this.UpdateFader();
			}

			// Token: 0x06001BB9 RID: 7097 RVA: 0x000A7B84 File Offset: 0x000A5F84
			public void CancelAll()
			{
				if (!this.isWindowOpen)
				{
					return;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null))
					{
						this.windows[i].Cancel();
					}
				}
				this.CloseAll();
			}

			// Token: 0x06001BBA RID: 7098 RVA: 0x000A7BF0 File Offset: 0x000A5FF0
			public Window GetWindow(int windowId)
			{
				if (windowId < 0)
				{
					return null;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null))
					{
						if (this.windows[i].id == windowId)
						{
							return this.windows[i];
						}
					}
				}
				return null;
			}

			// Token: 0x06001BBB RID: 7099 RVA: 0x000A7C6A File Offset: 0x000A606A
			public bool IsFocused(int windowId)
			{
				return windowId >= 0 && !(this.topWindow == null) && this.topWindow.id == windowId;
			}

			// Token: 0x06001BBC RID: 7100 RVA: 0x000A7C96 File Offset: 0x000A6096
			public void Focus(int windowId)
			{
				this.Focus(this.GetWindow(windowId));
			}

			// Token: 0x06001BBD RID: 7101 RVA: 0x000A7CA5 File Offset: 0x000A60A5
			public void Focus(Window window)
			{
				if (window == null)
				{
					return;
				}
				window.TakeInputFocus();
				this.DefocusOtherWindows(window.id);
			}

			// Token: 0x06001BBE RID: 7102 RVA: 0x000A7CC8 File Offset: 0x000A60C8
			private void DefocusOtherWindows(int focusedWindowId)
			{
				if (focusedWindowId < 0)
				{
					return;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null))
					{
						if (this.windows[i].id != focusedWindowId)
						{
							this.windows[i].Disable();
						}
					}
				}
			}

			// Token: 0x06001BBF RID: 7103 RVA: 0x000A7D44 File Offset: 0x000A6144
			private void UpdateFader()
			{
				if (!this.isWindowOpen)
				{
					this.SetFaderActive(false);
					return;
				}
				Transform x = this.topWindow.transform.parent;
				if (x == null)
				{
					return;
				}
				this.SetFaderActive(true);
				this.fader.transform.SetAsLastSibling();
				int siblingIndex = this.topWindow.transform.GetSiblingIndex();
				this.fader.transform.SetSiblingIndex(siblingIndex);
			}

			// Token: 0x06001BC0 RID: 7104 RVA: 0x000A7DBB File Offset: 0x000A61BB
			private void FocusTopWindow()
			{
				if (this.topWindow == null)
				{
					return;
				}
				this.topWindow.TakeInputFocus();
			}

			// Token: 0x06001BC1 RID: 7105 RVA: 0x000A7DDA File Offset: 0x000A61DA
			private void SetFaderActive(bool state)
			{
				this.fader.SetActive(state);
			}

			// Token: 0x06001BC2 RID: 7106 RVA: 0x000A7DE8 File Offset: 0x000A61E8
			private Window InstantiateWindow(string name, int width, int height)
			{
				if (string.IsNullOrEmpty(name))
				{
					name = "Window";
				}
				GameObject gameObject = UITools.InstantiateGUIObject<Window>(this.windowPrefab, this.parent, name);
				if (gameObject == null)
				{
					return null;
				}
				Window component = gameObject.GetComponent<Window>();
				if (component != null)
				{
					component.Initialize(this.GetNewId(), new Func<int, bool>(this.IsFocused));
					this.windows.Add(component);
					component.SetSize(width, height);
				}
				return component;
			}

			// Token: 0x06001BC3 RID: 7107 RVA: 0x000A7E68 File Offset: 0x000A6268
			private Window InstantiateWindow(string name, GameObject windowPrefab)
			{
				if (string.IsNullOrEmpty(name))
				{
					name = "Window";
				}
				if (windowPrefab == null)
				{
					return null;
				}
				GameObject gameObject = UITools.InstantiateGUIObject<Window>(windowPrefab, this.parent, name);
				if (gameObject == null)
				{
					return null;
				}
				Window component = gameObject.GetComponent<Window>();
				if (component != null)
				{
					component.Initialize(this.GetNewId(), new Func<int, bool>(this.IsFocused));
					this.windows.Add(component);
				}
				return component;
			}

			// Token: 0x06001BC4 RID: 7108 RVA: 0x000A7EE9 File Offset: 0x000A62E9
			private void DestroyWindow(Window window)
			{
				if (window == null)
				{
					return;
				}
				UnityEngine.Object.Destroy(window.gameObject);
			}

			// Token: 0x06001BC5 RID: 7109 RVA: 0x000A7F04 File Offset: 0x000A6304
			private int GetNewId()
			{
				int result = this.idCounter;
				this.idCounter++;
				return result;
			}

			// Token: 0x06001BC6 RID: 7110 RVA: 0x000A7F27 File Offset: 0x000A6327
			public void ClearCompletely()
			{
				this.CloseAll();
				if (this.fader != null)
				{
					UnityEngine.Object.Destroy(this.fader);
				}
			}

			// Token: 0x04001D5E RID: 7518
			private List<Window> windows;

			// Token: 0x04001D5F RID: 7519
			private GameObject windowPrefab;

			// Token: 0x04001D60 RID: 7520
			private Transform parent;

			// Token: 0x04001D61 RID: 7521
			private GameObject fader;

			// Token: 0x04001D62 RID: 7522
			private int idCounter;
		}
	}
}
