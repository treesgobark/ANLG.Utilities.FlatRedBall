using ANLG.Utilities.FlatRedBall.Constants;
using FlatRedBall;
using FlatRedBall.Graphics.Animation;

namespace ANLG.Utilities.FlatRedBall.Controllers;

/// <summary>
/// An <see cref="EntityController{TEntity,TController}"/> that automatically starts animation chains when each state is entered.
/// </summary>
/// <typeparam name="TEntity">The type of entity this controller is bound to. Is often your <c>Player</c> class.</typeparam>
/// <typeparam name="TController">The base controller type for <typeparamref name="TEntity"/>. Is often your <c>PlayerController</c> class.</typeparam>
public abstract class AnimationController<TEntity, TController> : EntityController<TEntity, TController>
    where TEntity : PositionedObject, IHasAnimationControllers<TEntity, TController>
    where TController : AnimationController<TEntity, TController>
{
    /// <summary> Seconds </summary>
    private const float DefaultDuration = 1f;

    /// <inheritdoc cref="EntityController{TEntity,TController}(TEntity)"/>
    protected AnimationController(TEntity parent) : base(parent) { }
    
    #region Abstract/Virtual Members

    /// <summary>
    /// The AnimationChainList that this controller's animation belongs to.
    /// </summary>
    public abstract AnimationChainList ChainList { get; }
    
    /// <summary>
    /// The base name of the AnimationChain for this controller without a direction at the end like "Left" 
    /// </summary>
    public abstract string ChainName { get; }
    
    /// <summary>
    /// The state that the state machine will transition to when its animation times out if <see cref="CurrentStateAfterTimeout"/>
    ///   is still null.
    /// </summary>
    public abstract TController? DefaultStateAfterTimeout { get; }
    
    /// <summary>
    /// The direction this entity is facing, for the purpose of choosing animations.
    /// </summary>
    public abstract FourDirections AnimationDirection { get; }

    /// <summary>
    /// The total duration of this controller's animation when your AnimationChain has been normalized.
    ///   Only used if <see cref="ScaleAnimationSpeedByDuration"/> is true.
    /// </summary>
    public virtual float AnimationDuration => DefaultDuration;
    
    /// <summary>
    /// Controls whether the current <see cref="AnimationDirection"/> is appended to the end of <see cref="ChainName"/>.
    /// </summary>
    public virtual bool UseDirectionInChainName => true;
    
    /// <summary>
    /// Controls whether the animation speed will be scaled with <see cref="AnimationDuration"/>. This assumes that
    ///   your AnimationChain has a total duration of one second (is normalized).
    /// </summary>
    public virtual bool ScaleAnimationSpeedByDuration => false;

    #endregion
    
    /// <summary>
    /// The direction this entity was facing last frame.
    /// </summary>
    public FourDirections PreviousAnimationDirection { get; set; }
    
    /// <summary>
    /// If non-null, the state that this controller will transition to once the current animation cycles.
    /// </summary>
    public TController? CurrentStateAfterTimeout { get; set; }
    
    /// <summary>
    /// Will be placed between the <see cref="ChainName"/> and the <see cref="AnimationDirection"/> when getting the <see cref="CurrentChainName"/>.
    ///   Defaults to <see cref="string.Empty"/>.
    /// </summary>
    public string ChainNameSeparator { get; set; } = "";
    
    #region Computed Properties

    /// <summary>
    /// Whether the direction the entity was facing is different from the previous frame.
    /// </summary>
    public bool ChangedDirection => AnimationDirection != PreviousAnimationDirection;
    
    /// <summary>
    /// The part at the end of the chain name with the direction, if applicable.
    /// </summary>
    public string ChainNameSuffix => UseDirectionInChainName ? ChainNameSeparator + AnimationDirection : "";
    
    /// <summary>
    /// The full AnimationChain name to provide to the Sprite.
    /// </summary>
    public string CurrentChainName => $"{ChainName}{ChainNameSuffix}";
    
    /// <summary>
    /// Passthrough property for Parent.SpriteInstance.JustCycled. Returns true if the current animation just finished a full cycle.
    /// </summary>
    public bool AnimationJustCycled => Parent.ControllerSprite.JustCycled;
    
    /// <summary>
    /// The speed multiplier for normalized animations. Only used when <see cref="ScaleAnimationSpeedByDuration"/> is true.
    /// </summary>
    public float AnimationSpeed => ScaleAnimationSpeedByDuration ? 1 / AnimationDuration : 1f;
    
    #endregion

    #region Lifecycle Hooks

    /// <inheritdoc cref="EntityController{TEntity,TController}.Initialize"/>
    public override void Initialize()
    {
    }

    /// <summary>
    /// AnimationController: Calls <see cref="BeginAnimation"/> by default.
    /// <br/><inheritdoc cref="EntityController{TEntity,TController}.OnActivate"/>
    /// </summary>
    public override void OnActivate()
    {
        BeginAnimation();
    }

    /// <inheritdoc cref="EntityController{TEntity,TController}.CustomActivity"/>
    public override void CustomActivity()
    {
        HandleDirectionChange();
    }

    /// <summary>
    /// AnimationController: Checks if this controller's animation has cycled. If it has, returns either the <see cref="CurrentStateAfterTimeout"/>
    ///   or <see cref="DefaultStateAfterTimeout"/>.
    /// <br/><inheritdoc cref="EntityController{TEntity,TController}.EvaluateExitConditions"/>
    /// </summary>
    public override TController? EvaluateExitConditions()
    {
        if (AnimationJustCycled)
        {
            return CurrentStateAfterTimeout ?? DefaultStateAfterTimeout;
        }
        
        return null;
    }

    /// <summary>
    /// AnimationController: Resets <see cref="CurrentStateAfterTimeout"/> to null.
    /// <br/><inheritdoc cref="EntityController{TEntity,TController}.BeforeDeactivate"/>
    /// </summary>
    public override void BeforeDeactivate()
    {
        CurrentStateAfterTimeout = null;
    }

    #endregion

    /// <summary>
    /// Sets the animation chain for this controller.
    /// </summary>
    public virtual void BeginAnimation()
    {
        Parent.ControllerSprite.AnimationChains = ChainList;
        Parent.ControllerSprite.CurrentChainName = null;
        Parent.ControllerSprite.CurrentChainName = CurrentChainName;
        Parent.ControllerSprite.AnimationSpeed = AnimationSpeed;
    }

    /// <summary>
    /// Restarts the animation if the entity direction changed this frame.
    /// </summary>
    public virtual void HandleDirectionChange()
    {
        if (ChangedDirection)
        {
            BeginAnimation();
        }
        
        PreviousAnimationDirection = AnimationDirection;
    }
}
