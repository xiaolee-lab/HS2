using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200118F RID: 4495
public static class NullCheck
{
	// Token: 0x06009429 RID: 37929 RVA: 0x003D352C File Offset: 0x003D192C
	public static bool IsDefault<T>(this T value) where T : struct
	{
		return value.Equals(default(T));
	}

	// Token: 0x0600942A RID: 37930 RVA: 0x003D3554 File Offset: 0x003D1954
	public static bool IsNull<T, TU>(this KeyValuePair<T, TU> pair)
	{
		return pair.Equals(default(KeyValuePair<T, TU>));
	}

	// Token: 0x0600942B RID: 37931 RVA: 0x003D357C File Offset: 0x003D197C
	public static T GetCache<T>(this object _, ref T ret, Func<T> get)
	{
		return (ret == null) ? (ret = get()) : ret;
	}

	// Token: 0x0600942C RID: 37932 RVA: 0x003D35B4 File Offset: 0x003D19B4
	public static T GetCacheObject<T>(this object _, ref T ret, Func<T> get) where T : UnityEngine.Object
	{
		return (!(ret != null)) ? (ret = get()) : ret;
	}

	// Token: 0x0600942D RID: 37933 RVA: 0x003D35F4 File Offset: 0x003D19F4
	public static T GetComponentCache<T>(this Component component, ref T ret) where T : Component
	{
		return component.GetCacheObject(ref ret, () => component.GetComponent<T>());
	}

	// Token: 0x0600942E RID: 37934 RVA: 0x003D3628 File Offset: 0x003D1A28
	public static T GetComponentCache<T>(this GameObject gameObject, ref T ret) where T : Component
	{
		return gameObject.GetCacheObject(ref ret, () => gameObject.GetComponent<T>());
	}

	// Token: 0x0600942F RID: 37935 RVA: 0x003D365A File Offset: 0x003D1A5A
	public static T GetOrAddComponent<T>(this Component component) where T : Component
	{
		return (!(component == null)) ? component.gameObject.GetOrAddComponent<T>() : ((T)((object)null));
	}

	// Token: 0x06009430 RID: 37936 RVA: 0x003D3680 File Offset: 0x003D1A80
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		if (gameObject == null)
		{
			return (T)((object)null);
		}
		T t = gameObject.GetComponent<T>();
		if (t == null)
		{
			t = gameObject.AddComponent<T>();
		}
		return t;
	}

	// Token: 0x06009431 RID: 37937 RVA: 0x003D36C0 File Offset: 0x003D1AC0
	public static bool IsNullOrWhiteSpace(this string self)
	{
		return self == null || self.Trim() == string.Empty;
	}

	// Token: 0x06009432 RID: 37938 RVA: 0x003D36DB File Offset: 0x003D1ADB
	public static bool IsNullOrEmpty(this string self)
	{
		return string.IsNullOrEmpty(self);
	}

	// Token: 0x06009433 RID: 37939 RVA: 0x003D36E4 File Offset: 0x003D1AE4
	public static bool IsNullOrEmpty(this string[] args, int index)
	{
		bool ret = false;
		args.SafeGet(index).SafeProc(delegate(string s)
		{
			ret = !s.IsNullOrEmpty();
		});
		return !ret;
	}

	// Token: 0x06009434 RID: 37940 RVA: 0x003D3720 File Offset: 0x003D1B20
	public static bool IsNullOrEmpty(this List<string> args, int index)
	{
		bool ret = false;
		args.SafeGet(index).SafeProc(delegate(string s)
		{
			ret = !s.IsNullOrEmpty();
		});
		return !ret;
	}

	// Token: 0x06009435 RID: 37941 RVA: 0x003D375C File Offset: 0x003D1B5C
	public static bool IsNullOrEmpty<T>(this IList<T> self)
	{
		return self == null || self.Count == 0;
	}

	// Token: 0x06009436 RID: 37942 RVA: 0x003D3770 File Offset: 0x003D1B70
	public static bool IsNullOrEmpty<T>(this List<T> self)
	{
		return self == null || self.Count == 0;
	}

	// Token: 0x06009437 RID: 37943 RVA: 0x003D3784 File Offset: 0x003D1B84
	public static bool IsNullOrEmpty(this MulticastDelegate self)
	{
		return self == null || self.GetInvocationList() == null || self.GetInvocationList().Length == 0;
	}

	// Token: 0x06009438 RID: 37944 RVA: 0x003D37A5 File Offset: 0x003D1BA5
	public static bool IsNullOrEmpty(this UnityEvent self)
	{
		return self == null || self.GetPersistentEventCount() == 0;
	}

	// Token: 0x06009439 RID: 37945 RVA: 0x003D37B9 File Offset: 0x003D1BB9
	public static bool IsNullOrEmpty(this UnityEvent self, int target)
	{
		return self.IsNullOrEmpty() || self.GetPersistentTarget(target) == null || self.GetPersistentMethodName(target).IsNullOrEmpty();
	}

	// Token: 0x0600943A RID: 37946 RVA: 0x003D37EC File Offset: 0x003D1BEC
	public static T SafeGet<T>(this T[] array, int index)
	{
		if (array == null)
		{
			return default(T);
		}
		return ((ulong)index >= (ulong)((long)array.Length)) ? default(T) : array[index];
	}

	// Token: 0x0600943B RID: 37947 RVA: 0x003D3829 File Offset: 0x003D1C29
	public static bool SafeProc<T>(this T[] array, int index, Action<T> act)
	{
		return array.SafeGet(index).SafeProc(act);
	}

	// Token: 0x0600943C RID: 37948 RVA: 0x003D3838 File Offset: 0x003D1C38
	public static T SafeGet<T>(this List<T> list, int index)
	{
		if (list == null)
		{
			return default(T);
		}
		return ((ulong)index >= (ulong)((long)list.Count)) ? default(T) : list[index];
	}

	// Token: 0x0600943D RID: 37949 RVA: 0x003D3878 File Offset: 0x003D1C78
	public static bool SafeProc<T>(this List<T> list, int index, Action<T> act)
	{
		return list.SafeGet(index).SafeProc(act);
	}

	// Token: 0x0600943E RID: 37950 RVA: 0x003D3887 File Offset: 0x003D1C87
	public static bool SafeProc(this string[] args, int index, Action<string> act)
	{
		if (args.IsNullOrEmpty(index))
		{
			return false;
		}
		act.Call(args[index]);
		return true;
	}

	// Token: 0x0600943F RID: 37951 RVA: 0x003D38A1 File Offset: 0x003D1CA1
	public static bool SafeProc(this List<string> args, int index, Action<string> act)
	{
		if (args.IsNullOrEmpty(index))
		{
			return false;
		}
		act.Call(args[index]);
		return true;
	}

	// Token: 0x06009440 RID: 37952 RVA: 0x003D38C0 File Offset: 0x003D1CC0
	public static bool SafeProc<T>(this T self, Action<T> act)
	{
		bool flag = self != null;
		if (flag)
		{
			act.Call(self);
		}
		return flag;
	}

	// Token: 0x06009441 RID: 37953 RVA: 0x003D38E8 File Offset: 0x003D1CE8
	public static bool SafeProcObject<T>(this T self, Action<T> act) where T : UnityEngine.Object
	{
		bool flag = self != null;
		if (flag)
		{
			act.Call(self);
		}
		return flag;
	}

	// Token: 0x06009442 RID: 37954 RVA: 0x003D3910 File Offset: 0x003D1D10
	public static void Call(this Action action)
	{
		if (action != null)
		{
			action();
		}
	}

	// Token: 0x06009443 RID: 37955 RVA: 0x003D391E File Offset: 0x003D1D1E
	public static void Call<T>(this Action<T> action, T arg)
	{
		if (action != null)
		{
			action(arg);
		}
	}

	// Token: 0x06009444 RID: 37956 RVA: 0x003D392D File Offset: 0x003D1D2D
	public static void Call<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
	{
		if (action != null)
		{
			action(arg1, arg2);
		}
	}

	// Token: 0x06009445 RID: 37957 RVA: 0x003D393D File Offset: 0x003D1D3D
	public static void Call<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
	{
		if (action != null)
		{
			action(arg1, arg2, arg3);
		}
	}

	// Token: 0x06009446 RID: 37958 RVA: 0x003D394E File Offset: 0x003D1D4E
	public static TResult Call<TResult>(this Func<TResult> func, TResult result = default(TResult))
	{
		return (func == null) ? result : func();
	}

	// Token: 0x06009447 RID: 37959 RVA: 0x003D3962 File Offset: 0x003D1D62
	public static TResult Call<T, TResult>(this Func<T, TResult> func, T arg, TResult result = default(TResult))
	{
		return (func == null) ? result : func(arg);
	}

	// Token: 0x06009448 RID: 37960 RVA: 0x003D3977 File Offset: 0x003D1D77
	public static TResult Call<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1, T2 arg2, TResult result = default(TResult))
	{
		return (func == null) ? result : func(arg1, arg2);
	}

	// Token: 0x06009449 RID: 37961 RVA: 0x003D398D File Offset: 0x003D1D8D
	public static TResult Call<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3, TResult result = default(TResult))
	{
		return (func == null) ? result : func(arg1, arg2, arg3);
	}

	// Token: 0x0600944A RID: 37962 RVA: 0x003D39A8 File Offset: 0x003D1DA8
	public static bool Proc<T>(this T self, Func<T, bool> conditional, Action<T> act)
	{
		bool flag = conditional(self);
		if (flag)
		{
			act.Call(self);
		}
		return flag;
	}

	// Token: 0x0600944B RID: 37963 RVA: 0x003D39CC File Offset: 0x003D1DCC
	public static bool Proc<T>(this T self, Func<T, bool> conditional, Action<T> actTrue, Action<T> actFalse)
	{
		bool flag = conditional(self);
		self.Proc((T _) => true, (!flag) ? actFalse : actTrue);
		return flag;
	}
}
