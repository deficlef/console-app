using System;

public abstract class Application
{
    public const string DIR_ERROR = "Error calculating dir size: {0}";
    public const string UNKNOWN_ERROR = "Unknown command.";
    public const string LAUNCH_MESSAGE = "Please enter a directory path";
    public const string DELETE_MESSAGE = "Please enter directory to delete";
    public const string SAVE_ERROR = "Error saving.";
    public const string SAVE_SUCCESS = "Save successful.";
    public const string DELETE_ERROR = "Error deleting.";
    public const string DELETE_SUCCESS = "Delete successful.";
    public const string ALL_STRING = "a";
    public const string YES_STRING = "y";
    public const string NO_STRING = "n";
    public const string SAVE_STRING = "s";
    public const string DELETE_STRING = "d";
    public const string RETRY_MESSAGE = "Save to database (" + SAVE_STRING +
                                       // ") Delete data (" + DELETE_STRING +
                                        ") Display all saved data (" + ALL_STRING + 
                                        ") Run a new scan (" + YES_STRING + 
                                        ") or cancel (" + NO_STRING + ")";

}
