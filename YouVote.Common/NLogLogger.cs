using System.Linq.Expressions;
using System.Reflection;
using System;
using NHibernate;

namespace YouVote.Common
{
	public class NLogFactory : ILoggerFactory
	{
		private static readonly Type LogManagerType = Type.GetType("NLog.LogManager, NLog");
	    private const string NHibernateLoggerName = "nhibernate";

	    private static readonly Func<string, object> CreateLoggerInstanceFunc;

		static NLogFactory()
		{
			CreateLoggerInstanceFunc = CreateLoggerInstance();
		}

		public IInternalLogger LoggerFor(Type type)
		{
			return new NLogLogger(CreateLoggerInstanceFunc(NHibernateLoggerName));
		}

		public IInternalLogger LoggerFor(string keyName)
		{
			return new NLogLogger(CreateLoggerInstanceFunc(NHibernateLoggerName));
		}

		private static Func<string, object> CreateLoggerInstance()
		{
			var method = LogManagerType.GetMethod("GetLogger", new[] { typeof(string) });
			ParameterExpression nameParam = Expression.Parameter(typeof(string));
			MethodCallExpression methodCall = Expression.Call(null, method, new Expression[] { nameParam });

			return Expression.Lambda<Func<string, object>>(methodCall, new[] { nameParam }).Compile();
		}
	}
	public class NLogLogger : IInternalLogger
	{
		private static readonly Type LoggerType = Type.GetType("NLog.Logger, NLog");

		private static readonly Func<object, bool> DebugPropertyGetter;
		private static readonly Func<object, bool> ErrorPropertyGetter;
		private static readonly Func<object, bool> FatalPropertyGetter;
		private static readonly Func<object, bool> InfoPropertyGetter;
		private static readonly Func<object, bool> WarnPropertyGetter;

		private static readonly Action<object, string> DebugAction;
		private static readonly Action<object, string> ErrorAction;
		private static readonly Action<object, string> WarnAction;
		private static readonly Action<object, string> InfoAction;
		private static readonly Action<object, string> FatalAction;

		private static readonly Action<object, string, Exception> DebugExceptionAction;
		private static readonly Action<object, string, Exception> ErrorExceptionAction;
		private static readonly Action<object, string, Exception> WarnExceptionAction;
		private static readonly Action<object, string, Exception> InfoExceptionAction;
		private static readonly Action<object, string, Exception> FatalExceptionAction;

		private readonly object _log;

		static NLogLogger()
		{
			DebugPropertyGetter = CreatePropertyGetter("IsDebugEnabled");
			ErrorPropertyGetter = CreatePropertyGetter("IsErrorEnabled");
			FatalPropertyGetter = CreatePropertyGetter("IsFatalEnabled");
			InfoPropertyGetter = CreatePropertyGetter("IsInfoEnabled");
			WarnPropertyGetter = CreatePropertyGetter("IsWarnEnabled");

			DebugAction = CreateSimpleAction("Debug");
			ErrorAction = CreateSimpleAction("Error");
			WarnAction = CreateSimpleAction("Warn");
			InfoAction = CreateSimpleAction("Info");
			FatalAction = CreateSimpleAction("Fatal");

			DebugExceptionAction = CreateExceptionAction("Debug");
			ErrorExceptionAction = CreateExceptionAction("Error");
			WarnExceptionAction = CreateExceptionAction("Warn");
			InfoExceptionAction = CreateExceptionAction("Info");
			FatalExceptionAction = CreateExceptionAction("Fatal");
		}

		public NLogLogger(object log)
		{
			_log = log;
		}

		public bool IsDebugEnabled
		{
			get
			{
				return DebugPropertyGetter(_log);
			}
		}

		public bool IsErrorEnabled
		{
			get
			{
				return ErrorPropertyGetter(_log);
			}
		}

		public bool IsFatalEnabled
		{
			get
			{
				return FatalPropertyGetter(_log);
			}
		}

		public bool IsInfoEnabled
		{
			get
			{
				return InfoPropertyGetter(_log);
			}
		}

		public bool IsWarnEnabled
		{
			get
			{
				return WarnPropertyGetter(_log);
			}
		}

		public void Debug(object message, Exception exception)
		{
			if (message == null || exception == null)
				return;

			DebugExceptionAction(_log, message.ToString(), exception);
		}

		public void Debug(object message)
		{
			if (message == null)
				return;

			DebugAction(_log, message.ToString());
		}

		public void DebugFormat(string format, params object[] args)
		{
			Debug(String.Format(format, args));
		}

		public void Error(object message, Exception exception)
		{
			if (message == null || exception == null)
				return;

			ErrorExceptionAction(_log, message.ToString(), exception);
		}

		public void Error(object message)
		{
			if (message == null)
				return;

			ErrorAction(_log, message.ToString());
		}

		public void ErrorFormat(string format, params object[] args)
		{
			Error(String.Format(format, args));
		}

		public void Fatal(object message, Exception exception)
		{
			if (message == null || exception == null)
				return;

			FatalExceptionAction(_log, message.ToString(), exception);
		}

		public void Fatal(object message)
		{
			if (message == null)
				return;

			FatalAction(_log, message.ToString());
		}

		public void Info(object message, Exception exception)
		{
			if (message == null || exception == null)
				return;

			InfoExceptionAction(_log, message.ToString(), exception);
		}

		public void Info(object message)
		{
			if (message == null)
				return;

			InfoAction(_log, message.ToString());
		}

		public void InfoFormat(string format, params object[] args)
		{
			Info(String.Format(format, args));
		}

		public void Warn(object message, Exception exception)
		{
			if (message == null || exception == null)
				return;

			WarnExceptionAction(_log, message.ToString(), exception);
		}

		public void Warn(object message)
		{
			if (message == null)
				return;

			WarnAction(_log, message.ToString());
		}

		public void WarnFormat(string format, params object[] args)
		{
			Warn(String.Format(format, args));
		}

		private static Func<object, bool> CreatePropertyGetter(string propertyName)
		{
			ParameterExpression paramExpr = Expression.Parameter(typeof(object), "pv");
			Expression convertedExpr = Expression.Convert(paramExpr, LoggerType);
			Expression property = Expression.Property(convertedExpr, propertyName);

			return Expression.Lambda<Func<object, bool>>(property, new[] { paramExpr }).Compile();
		}

		private static Action<object, string> CreateSimpleAction(string methodName)
		{
			MethodInfo methodInfo = GetMethodInfo(methodName, new[] { typeof(string) });
			ParameterExpression instanceParam = Expression.Parameter(typeof(object), "i");
			var converterInstanceParam = Expression.Convert(instanceParam, LoggerType);
			ParameterExpression messageParam = Expression.Parameter(typeof(string), "m");

			MethodCallExpression methodCall = Expression.Call(converterInstanceParam, methodInfo, new Expression[] { messageParam });

			return (Action<object, string>)Expression.Lambda(methodCall, new[] { instanceParam, messageParam }).Compile();
		}

		private static Action<object, string, Exception> CreateExceptionAction(string methodName)
		{
			MethodInfo methodInfo = GetMethodInfo(methodName, new[] { typeof(string), typeof(Exception) });

			ParameterExpression messageParam = Expression.Parameter(typeof(string), "m");
			ParameterExpression instanceParam = Expression.Parameter(typeof(object), "i");
			ParameterExpression exceptionParam = Expression.Parameter(typeof(Exception), "e");
			var convertedParam = Expression.Convert(instanceParam, LoggerType);

			MethodCallExpression methodCall = Expression.Call(convertedParam, methodInfo, new Expression[] { messageParam, exceptionParam });

			return (Action<object, string, Exception>)Expression.Lambda(methodCall, new[] { instanceParam, messageParam, exceptionParam }).Compile();
		}

		private static MethodInfo GetMethodInfo(string methodName, Type[] parameters)
		{
			return LoggerType.GetMethod(methodName, parameters);
		}
	}

}