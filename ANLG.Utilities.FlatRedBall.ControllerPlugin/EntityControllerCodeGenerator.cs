using FlatRedBall.Glue.CodeGeneration;
using FlatRedBall.Glue.CodeGeneration.CodeBuilder;
using FlatRedBall.Glue.Plugins.ExportedImplementations;
using FlatRedBall.Glue.SaveClasses;

namespace ANLG.Utilities.FlatRedBall.ControllerPlugin;

public class EntityControllerCodeGenerator : ElementComponentCodeGenerator
{
    public static bool ImplementsIHasControllers(EntitySave entity) =>
        entity?.Properties.GetValue<bool>(EntityControllerPlugin.ImplementsIHasControllers) == true;

    public override ICodeBlock GenerateFields(ICodeBlock codeBlock, IElement element)
    {
        if (element is not EntitySave entity || !ImplementsIHasControllers(entity))
        {
            return codeBlock;
        }

        // codeBlock.Line($"public {entity.ClassName}ControllerCollection {entity.ClassName}Controllers {{ get; set; }}");
        codeBlock.Line(
            $"public ControllerCollection<{entity.GetQualifiedName(GlueState.Self.ProjectNamespace)}, {entity.ClassName}Controller> Controllers {{ get; set; }}");

        return codeBlock;
    }

    public override ICodeBlock GenerateInitialize(ICodeBlock codeBlock, IElement element)
    {
        if (element is not EntitySave entity || !ImplementsIHasControllers(entity))
        {
            return codeBlock;
        }

        codeBlock.Line($"Controllers = new ControllerCollection<{entity.GetQualifiedName(GlueState.Self.ProjectNamespace)}, {entity.Name}Controller>();");
        
        return codeBlock;
    }

    public override ICodeBlock GenerateActivity(ICodeBlock codeBlock, IElement element)
    {
        if (element is not EntitySave entity || !ImplementsIHasControllers(entity))
        {
            return codeBlock;
        }

        codeBlock.Line($"{entity.Name}Controllers.DoCurrentControllerActivity();");
        
        return codeBlock;
    }
}