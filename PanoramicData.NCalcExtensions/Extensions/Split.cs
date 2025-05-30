﻿using System.Collections.Generic;
using System.ComponentModel;

namespace PanoramicData.NCalcExtensions.Extensions;

/// <summary>
/// Used to provide IntelliSense in Monaco editor
/// </summary>
public partial interface IFunctionPrototypes
{
	[DisplayName("split")]
	[Description("Splits a string on a given character into a list of strings.")]
	List<string> Split(
		[Description("The original string to be split.")]
		string longString,
		[Description("The string to split on.")]
		string splitOn
	);
}

internal static class Split
{
	internal static void Evaluate(IFunctionArgs functionArgs)
	{
		string input;
		string splitString;
		try
		{
			input = (string)functionArgs.Parameters[0].Evaluate();
			splitString = (string)functionArgs.Parameters[1].Evaluate();
		}
		catch (Exception e) when (e is not NCalcExtensionsException or FormatException)
		{
			throw new FormatException($"{ExtensionFunction.Split}() requires two string parameters.");
		}

		functionArgs.Result = splitString.Length switch
		{
			0 => throw new FormatException($"{ExtensionFunction.Split}() requires that the second parameter is not empty."),
			1 => [.. input.Split(splitString[0])],
			_ => input.Split(new[] { splitString }, StringSplitOptions.None).ToList()
		};
	}
}