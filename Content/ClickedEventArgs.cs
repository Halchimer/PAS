using PAS.Engine;
using EventArgs = System.EventArgs;

namespace PAS.Content;

public class PlayerActionSendEventArgs : EventArgs
{
    public CharacterActions Action { get; set; }
}