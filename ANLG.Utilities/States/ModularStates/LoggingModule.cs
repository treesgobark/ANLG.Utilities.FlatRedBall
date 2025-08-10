using ANLG.Utilities.Core;

namespace ANLG.Utilities.States.ModularStates;

public class LoggingModule : IState
{
    private readonly ILogger _logger;
    private readonly ITimeManager _timeManager;
    private readonly string _identifier;
    private readonly LogLevel _logLevel;

    public LoggingModule(ILogger logger, ITimeManager timeManager, string identifier, LogLevel logLevel = LogLevel.Default)
    {
        _logger           = logger;
        _timeManager = timeManager;
        _identifier       = identifier;
        _logLevel         = logLevel;
    }
    
    public void OnActivate()
    {
        _logger.Log($"{_timeManager.TotalGameTime.TotalSeconds * 60}: {_identifier} - OnActivate");
    }

    public void Update()
    {
        if (_logLevel is LogLevel.Verbose)
        {
            _logger.Log($"{_timeManager.TotalGameTime.TotalSeconds * 60}: {_identifier} - CustomActivity");
        }
    }

    public IState? EvaluateExitConditions()
    {
        return null;
    }

    public void BeforeDeactivate()
    {
        _logger.Log($"{_timeManager.TotalGameTime.TotalSeconds * 60}: {_identifier} - BeforeDeactivate");
    }
}

public enum LogLevel
{
    Default,
    Verbose,
}
