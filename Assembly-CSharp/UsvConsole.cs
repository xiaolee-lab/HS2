using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x02000493 RID: 1171
public class UsvConsole
{
	// Token: 0x0600159F RID: 5535 RVA: 0x00085A78 File Offset: 0x00083E78
	public UsvConsole()
	{
		UsvConsoleCmds.Instance = new UsvConsoleCmds();
		foreach (MethodInfo methodInfo in typeof(UsvConsoleCmds).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
		{
			foreach (object obj in methodInfo.GetCustomAttributes(typeof(ConsoleHandler), false))
			{
				ConsoleHandler consoleHandler = obj as ConsoleHandler;
				if (consoleHandler != null)
				{
					try
					{
						Delegate @delegate = Delegate.CreateDelegate(typeof(UsvConsoleCmdHandler), UsvConsoleCmds.Instance, methodInfo);
						if (@delegate != null)
						{
							string key = consoleHandler.Command.ToLower();
							this._handlers[key] = (UsvConsoleCmdHandler)@delegate;
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x00085B68 File Offset: 0x00083F68
	public bool ExecuteCommand(string fullcmd)
	{
		string[] array = fullcmd.Split(Array.Empty<char>());
		if (array.Length == 0)
		{
			Log.Info("empty command received, ignored.", Array.Empty<object>());
			return false;
		}
		UsvConsoleCmdHandler usvConsoleCmdHandler;
		if (!this._handlers.TryGetValue(array[0].ToLower(), out usvConsoleCmdHandler))
		{
			Log.Info("unknown command ('{0}') received, ignored.", new object[]
			{
				fullcmd
			});
			return false;
		}
		if (!usvConsoleCmdHandler(array))
		{
			Log.Info("executing command ('{0}') failed.", new object[]
			{
				fullcmd
			});
			return false;
		}
		return true;
	}

	// Token: 0x040018A5 RID: 6309
	public static UsvConsole Instance;

	// Token: 0x040018A6 RID: 6310
	private Dictionary<string, UsvConsoleCmdHandler> _handlers = new Dictionary<string, UsvConsoleCmdHandler>();
}
