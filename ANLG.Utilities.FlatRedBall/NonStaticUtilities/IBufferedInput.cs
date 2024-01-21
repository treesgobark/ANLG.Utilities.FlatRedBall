using FlatRedBall.Input;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public interface IBufferedInput : IPressableInput
{
    bool IsDownShouldBuffer { get; set; }
    int IsDownBufferFrameSize { get; set; }
    double IsDownBufferSecondsSize { get; set; }
    
    bool WasJustPressedShouldBuffer { get; set; }
    int WasJustPressedBufferFrameSize { get; set; }
    double WasJustPressedBufferSecondsSize { get; set; }
    
    bool WasJustReleasedShouldBuffer { get; set; }
    int WasJustReleasedBufferFrameSize { get; set; }
    double WasJustReleasedBufferSecondsSize { get; set; }

    void Activity();
}
