using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

// Token: 0x020004AA RID: 1194
public class RuntimeCoroutineTracker
{
	// Token: 0x06001607 RID: 5639 RVA: 0x0008766C File Offset: 0x00085A6C
	public static Coroutine InvokeStart(MonoBehaviour initiator, IEnumerator routine)
	{
		if (!CoroutineRuntimeTrackingConfig.EnableTracking)
		{
			return initiator.StartCoroutine(routine);
		}
		Coroutine result;
		try
		{
			result = initiator.StartCoroutine(new TrackedCoroutine(routine));
		}
		catch (Exception exception)
		{
			UnityEngine.Debug.LogException(exception);
			result = null;
		}
		return result;
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x000876BC File Offset: 0x00085ABC
	public static Coroutine InvokeStart(MonoBehaviour initiator, string methodName, object arg = null)
	{
		if (!CoroutineRuntimeTrackingConfig.EnableTracking)
		{
			return initiator.StartCoroutine(methodName, arg);
		}
		Coroutine result;
		try
		{
			Type type = initiator.GetType();
			if (type == null)
			{
				throw new ArgumentNullException("initiator", "invalid initiator (null type)");
			}
			MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
			if (method == null)
			{
				throw new ArgumentNullException("methodName", string.Format("Invalid method {0} (method not found)", methodName));
			}
			object[] parameters = null;
			if (arg != null)
			{
				parameters = new object[]
				{
					arg
				};
			}
			IEnumerator enumerator = method.Invoke(initiator, parameters) as IEnumerator;
			if (enumerator == null)
			{
				throw new ArgumentNullException("methodName", string.Format("Invalid method {0} (not an IEnumerator)", methodName));
			}
			result = RuntimeCoroutineTracker.InvokeStart(initiator, enumerator);
		}
		catch (Exception exception)
		{
			UnityEngine.Debug.LogException(exception);
			result = null;
		}
		return result;
	}
}
