using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Scene
{
	// Token: 0x0200102F RID: 4143
	public class Title : BaseLoader
	{
		// Token: 0x06008ABF RID: 35519 RVA: 0x003A50AC File Offset: 0x003A34AC
		private void Start()
		{
			IConnectableObservable<Unit> connectableObservable = Observable.FromCoroutine(() => this.InputWait(), false).Publish<Unit>();
			connectableObservable.Connect();
			Observable.WhenAll(new IObservable<Unit>[]
			{
				connectableObservable
			}).Subscribe(delegate(Unit _)
			{
				Scene.Data data = new Scene.Data
				{
					levelName = Singleton<Manager.Resources>.Instance.DefinePack.SceneNames.MapScene,
					isAdd = false,
					isFade = true,
					isAsync = true
				};
				Singleton<Scene>.Instance.LoadReserve(data, false);
			});
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
		}

		// Token: 0x06008AC0 RID: 35520 RVA: 0x003A5120 File Offset: 0x003A3520
		private IEnumerator InputWait()
		{
			while (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				yield return null;
			}
			Manager.Input inputManager = Singleton<Manager.Input>.Instance;
			bool pressed = false;
			while (!pressed)
			{
				Manager.Input.ValidType state = inputManager.State;
				if (state == Manager.Input.ValidType.Action || state == Manager.Input.ValidType.UI)
				{
					pressed |= inputManager.IsPressedAction();
					pressed |= inputManager.IsPressedKey(KeyCode.Mouse0);
				}
				yield return null;
			}
			yield break;
		}
	}
}
