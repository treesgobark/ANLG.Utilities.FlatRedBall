namespace ANLG.Utilities.FlatRedBall.Controllers;

/// <summary>
/// Stores the list 
/// </summary>
public class ControllerCollection<T, TController>
    where T : IHasControllers<T, TController>
    where TController : IEntityController<T, TController>
{
    private bool _isInitialized = false;

    protected TController? ExitOverride { get; set; }

    /// <summary>
    /// All the controllers that belong to this collection (state machine)
    /// </summary>
    protected List<TController> Controllers { get; } = new();
    
    /// <summary>
    /// The currently active controller
    /// </summary>
    public TController CurrentController { get; protected set; }
    
    /// <summary>
    /// Adds a controller to the collection.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException for duplicate types.</exception>
    public void Add(TController controller)
    {
        if (Controllers.Any(existing => existing.GetType() == controller.GetType()))
        {
            throw new ArgumentException($"Collection already has a controller of type {controller.GetType().Name}");
        }
        Controllers.Add(controller);
    }

    /// <summary>
    /// Returns the controller in this collection with the type <typeparamref name="TSearch"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException if the collection doesn't have a controller of the given type.</exception>
    public TSearch Get<TSearch>() where TSearch : TController
    {
        foreach (var controller in Controllers)
        {
            if (Type.GetTypeHandle(controller).Value == typeof(TSearch).TypeHandle.Value)
            {
                return (TSearch)controller;
            }
        }
        throw new ArgumentException($"Collection does not contain any controllers of type {typeof(TSearch).Name}");
    }

    /// <summary>
    /// Sets the current controller to the one of type <typeparamref name="TSearch"/> in this collection,
    ///   then calls <see cref="EntityController{T,TSelf}.OnActivate"/> on it.
    ///   Must be called before any controller activity can happen.
    /// </summary>
    public void InitializeStartingController<TSearch>() where TSearch : TController
    {
        Controllers.ForEach(c => c.Initialize());
        
        CurrentController = Get<TSearch>();
        ExitOverride      = CurrentController;
        _isInitialized    = true;
    }

    /// <summary>
    /// Evaluates the exit conditions of the current controller, then if a controller switch happens,
    ///   <see cref="EntityController{T,TSelf}.BeforeDeactivate"/> is called on the old controller,
    ///   then <see cref="EntityController{T,TSelf}.OnActivate"/> and <see cref="EntityController{T,TSelf}.CustomActivity"/>
    ///   are called on the new controller, in that order.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws InvalidOperationException if collection is uninitialized.</exception>
    public void DoCurrentControllerActivity()
    {
        if (!_isInitialized)
        {
            throw new InvalidOperationException($"You must initialize collection with "
                                                + $"{nameof(InitializeStartingController)} before performing activity.");
        }

        var newController = ExitOverride ?? CurrentController.EvaluateExitConditions();
        ExitOverride = default;

        int count = 0;
        while (newController is not null)
        {
            if (count++ > 100)
            {
                throw new StackOverflowException($"The controller collection {GetType().Name} has reached the exit condition limit. " +
                                                 $"The current controller, {CurrentController.GetType().Name}, is trying to go to " +
                                                 $"{newController.GetType().Name}. For more information, consult the innerException.",
                    new Exception("Your controllers have likely encountered an infinite loop of exit conditions. Controller collections" +
                                  " will try to cycle to the next controller via their exit conditions continuously until it reaches " +
                                  "a controller that returns null from EvaluateExitConditions."));
            }
            
            CurrentController.BeforeDeactivate();
            CurrentController = newController;
            CurrentController.OnActivate();

            newController = CurrentController.EvaluateExitConditions();
        }
        
        CurrentController.CustomActivity();
    }

    /// <summary>
    /// Forces the state machine to move to the given state by replacing the next exit condition check.
    /// </summary>
    public void OverrideState<TState>() where TState : TController
    {
        ExitOverride = Get<TState>();
    }
    
    // write log for all previous states entered
    // and you know what fuck it ill also write down the thing about entering state machines within state machines like with a stack like magic lol
}