using System.ComponentModel.Composition;
using FlatRedBall.Glue.FormHelpers.PropertyGrids;
using FlatRedBall.Glue.Plugins;
using FlatRedBall.Glue.SaveClasses;

namespace ANLG.Utilities.FlatRedBall.ControllerPlugin;

[Export(typeof(PluginBase))]
public class EntityControllerPlugin : PluginBase
{
    public override string FriendlyName => "Entity Controller Plugin";
    public override Version Version => new(1, 0);

    public override void StartUp()
    {
        AdjustDisplayedEntity += HandleDisplayedEntity;
        RegisterCodeGenerator(new EntityControllerCodeGenerator());
        // events for:
        // whenever glue is loaded (do this first)
        // property change
        // inheritance change (creating a derived entity, generation should be different)
    }

    public const string ImplementsIHasControllers = nameof(ImplementsIHasControllers);
    private void HandleDisplayedEntity(EntitySave entitySave, EntitySavePropertyGridDisplayer displayer)
    {
        var member = displayer.IncludeCustomPropertyMember(ImplementsIHasControllers, typeof(bool));
        member.SetCategory("EntityControllers");
    }
}