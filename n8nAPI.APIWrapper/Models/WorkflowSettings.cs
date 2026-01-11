namespace n8nAPI.APIWrapper.Models;

public class WorkflowSettings
{
    public bool SaveExecutionProgress { get; set; }
    public bool SaveManualExecutions { get; set; }
    public string SaveDataErrorExecution { get; set; }
    public string SaveDataSuccessExecution { get; set; }
    public int ExecutionTimeout { get; set; }
    public string ErrorWorkflow { get; set; }
    public string Timezone { get; set; }
    public string ExecutionOrder { get; set; }
    public string CallerPolicy { get; set; }
    public string CallerIds { get; set; }
    public int TimeSavedPerExecution { get; set; }
    public bool AvailableInMCP { get; set; }
}