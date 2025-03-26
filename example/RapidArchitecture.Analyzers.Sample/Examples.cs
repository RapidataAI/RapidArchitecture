// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using Rapidata;

namespace RapidArchitecture.Analyzers.Sample;

// If you don't see warnings, build the Analyzers Project.

public class Examples
{
    public void ToStars()
    {
        var spaceship = new Spaceship();
        spaceship.SetSpeed(300000000); // Invalid value, it should be highlighted.
        spaceship.SetSpeed(42);

        var peter = new Something();
    }

    public class Google // Try to apply quick fix using the IDE.
    {
    }

    public interface IMessage
    {
        
    }

    public interface IDerivedMessage : IMessage;
    
    public class SomethingMessage : IDerivedMessage
    {
        
    }
}