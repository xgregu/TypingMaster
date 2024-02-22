namespace TypingMaster.UI.Events;

public record TestUpdatedEvent : RecordEventBase;

public record BackendConnectionStateChanged(bool IsConnected) : RecordEventBase;