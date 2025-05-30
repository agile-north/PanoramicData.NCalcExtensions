﻿using System.Collections.Generic;

namespace PanoramicData.NCalcExtensions;

public class ExtendedExpression : Expression
{
	private readonly Dictionary<string, object?> _storageDictionary = [];

	public ExtendedExpression(string expression) :
		this(expression.TidyExpression(), ExpressionOptions.None | ExpressionOptions.NoCache, CultureInfo.InvariantCulture)
	{ }

	public ExtendedExpression(string expression, ExpressionOptions expressionOptions) :
		this(expression.TidyExpression(), expressionOptions, CultureInfo.InvariantCulture)
	{ }

	public ExtendedExpression(
		string expression,
		ExpressionOptions expressionOptions,
		CultureInfo cultureInfo) : base(expression.TidyExpression(), expressionOptions, cultureInfo)
	{
		ExpressionHelper.Configure(this.Parameters, _storageDictionary);
		EvaluateFunction += (fn, args) => ExpressionHelper.Extend(fn, args, _storageDictionary, cultureInfo);
	}
}