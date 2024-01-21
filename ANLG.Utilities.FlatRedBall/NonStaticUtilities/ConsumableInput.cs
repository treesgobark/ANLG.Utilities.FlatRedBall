using FlatRedBall.Input;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

/// <summary>
/// Allows for a single instance of WasJustPressed to be read at during any frame of the continuous pressing of an input.
/// </summary>
public class ConsumableInput : IPressableInput
{
    public IPressableInput InternalInput { get; }

    /// <summary></summary>
    public ConsumableInput(IPressableInput internalInput)
    {
        InternalInput = internalInput;
    }

    private bool _isConsumed = false;
    private bool _incomingIsConsumed = false;

    /// <inheritdoc cref="IPressableInput.IsDown"/>
    public bool IsDown => InternalInput.IsDown;

    /// <inheritdoc cref="IPressableInput.WasJustPressed"/>
    public bool WasJustPressed
    {
        get
        {
            if (InternalInput.WasJustPressed)
            {
                return true;
            }
            
            if (_isConsumed)
            {
                return false;
            }

            if (!InternalInput.IsDown)
            {
                _incomingIsConsumed = false;
                return false;
            }
        
            _incomingIsConsumed = true;
            return true;
        }
    }

    /// <inheritdoc cref="IPressableInput.WasJustReleased"/>
    public bool WasJustReleased => InternalInput.WasJustReleased;
    
    /// <summary>
    /// Must be called each frame before inputs are read.
    /// </summary>
    public void Activity()
    {
        _isConsumed = _incomingIsConsumed;
    }
}
