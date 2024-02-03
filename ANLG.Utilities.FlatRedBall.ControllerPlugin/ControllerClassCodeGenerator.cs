using FlatRedBall.Glue.Plugins.CodeGenerators;

namespace ANLG.Utilities.FlatRedBall.ControllerPlugin;

public class ControllerClassCodeGenerator : FullFileCodeGenerator
{
    protected override string GenerateFileContents()
    {
        return "";
    }

    public override string RelativeFile { get; }
}