using FlatRedBall.Input;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public class BufferedInput : IBufferedInput
{
    public BufferedInput(IPressableInput internalInput)
    {
        InternalInput = internalInput;
    }

    public IPressableInput InternalInput { get; }

    public bool IsDown { get; }
    public bool WasJustPressed { get; }
    public bool WasJustReleased { get; }

    public bool IsDownShouldBuffer { get; set; }
    public int IsDownBufferFrameSize { get; set; }
    public double IsDownBufferSecondsSize { get; set; }

    public bool WasJustPressedShouldBuffer { get; set; }
    public int WasJustPressedBufferFrameSize { get; set; }
    public double WasJustPressedBufferSecondsSize { get; set; }

    public bool WasJustReleasedShouldBuffer { get; set; }
    public int WasJustReleasedBufferFrameSize { get; set; }
    public double WasJustReleasedBufferSecondsSize { get; set; }

    public void Activity()
    {
        throw new NotImplementedException();
    }
}




