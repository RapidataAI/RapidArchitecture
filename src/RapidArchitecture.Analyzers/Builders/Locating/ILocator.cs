using System;
using Microsoft.CodeAnalysis;

namespace RapidArchitecture.Analyzers.Builders.Locating;

public interface ILocator<in TAnalyze>
{
    Func<TAnalyze, Location> Locate { get; } 
}