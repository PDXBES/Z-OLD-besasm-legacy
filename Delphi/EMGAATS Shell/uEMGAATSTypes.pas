unit uEMGAATSTypes;

interface

uses SysUtils, Messages;

type
  TTimeFrame = (tfExisting, tfFuture);

  TEMGAATSErrorType = (eetRunCommand, eetToDo, eetStats, eetInfo, eetHint, eetWarning, eetError);
  TEMGAATSError = class
  public
    ErrorType: TEMGAATSErrorType;
    Msg: String;
    LogTime: TDateTime;
    constructor Create(AMessage: String; AErrorType: TEMGAATSErrorType);
  end;

const
  EMG_UPDATE_STATS = WM_APP + 1000;

implementation

{ TEMGAATSError }

constructor TEMGAATSError.Create(AMessage: String;
  AErrorType: TEMGAATSErrorType);
begin
  ErrorType := AErrorType;
  Msg := AMessage;
  LogTime := Now;
end;

end.
