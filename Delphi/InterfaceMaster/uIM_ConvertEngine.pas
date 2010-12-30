unit uIM_ConvertEngine;

interface

uses SysUtils, Classes, Contnrs, Forms,
	uInterfaceStack,
	uIM_Command, uIM_InterfaceFiles, uIM_IndexLookup;

type

	//////////////////////////////////////////////////////////////////////////////
	// INTERFACES
	//////////////////////////////////////////////////////////////////////////////

	{=============================================================================
		Name: IIM_ConvertFilter
		Purpose: Used for specifying a filter for a time series to be converted
		Requirements: Used by IIM_InterfaceFiles
	=============================================================================}
	IIM_ConvertFilter = interface
		['{002A0AA8-4F5C-4F17-B49D-04D39B0A1877}']
		function GetID: String;
		procedure SetID(Value: String);
		function GetNewID: String;
		procedure SetNewID(Value: String);
		function GetFinalID: String;
		function GetNewSeries: String;
		procedure SetNewSeries(Value: String);
		function GetInclude: Boolean;
		procedure SetInclude(Value: Boolean);
		function GetExpression: String;
		procedure SetExpression(Value: String);
	//----------------------------------------------------------------------------
		property ID: String read GetID write SetID;
		property NewID: String read GetNewID write SetNewID;
		property FinalID: String read GetFinalID;
		property NewSeries: String read GetNewSeries write SetNewSeries;
		property Include: Boolean read GetInclude write SetInclude;
		property Expression: String read GetExpression write SetExpression;
	end;

	{=============================================================================
		Name: IIM_ConvertFilterPackage
		Purpose: Applied to a source IIM_InterfaceFile to determine how to convert
			the source interface to a destination interface (which will hold the
			IIM_ConvertFilterPackage)
		Requirements: -
	=============================================================================}
	IIM_ConvertFilterPackage = interface
		['{623ACCC2-BE3B-4640-AB3B-7B561E2D831A}']
		procedure SetupFilters(SourceInterface: IIM_InterfaceFile);
		function NumIncludes: Integer;
		function IsIncluded(AIndex: Integer): Boolean;
		function GetFilter(AIndex: Integer): IIM_ConvertFilter;
		procedure SetFilter(AIndex: Integer; Value: IIM_ConvertFilter);
		function GetFilterByID(ID: String): IIM_ConvertFilter;
		procedure SetFilterByID(ID: String; Value: IIM_ConvertFilter);
		function IncludeLookup: TIndexLookupList;
		procedure AddFilter(AFilter: IIM_ConvertFilter);
		procedure DeleteFilter(AFilter: IIM_ConvertFilter);
	//----------------------------------------------------------------------------
		property Filter[AIndex: Integer]: IIM_ConvertFilter read GetFilter
			write SetFilter;
		property FilterByID[ID: String]: IIM_ConvertFilter read GetFilterByID
			write SetFilterByID;
	end;

	{=============================================================================
		Name: IIM_Convert
		Purpose: Used for interface files that can transform another interface
		Requirements: Must also implement IIM_InterfaceFile
	=============================================================================}
	IIM_Convert = interface
		['{CE54FA51-C5FE-41E3-BF5A-663E51F4C59E}']
		procedure ConvertFrom(ASourceInterface: IIM_InterfaceFile;
			AFilterPackage: IIM_ConvertFilterPackage = nil);
		function GetFileName: TFileName;
		function GetFormat: String;
		property FileName: TFileName read GetFileName;
		property Format: String read GetFormat;
	end;

	//////////////////////////////////////////////////////////////////////////////
	// CLASSES
	//////////////////////////////////////////////////////////////////////////////

	{=============================================================================
		 Name: TConvertEngine
		 Purpose: Engine for converting an interface file from one format to
			 another; provides checking for conversion compatibility
		 Requirements: -
	=============================================================================}
	TConvertEngine = class
	private
		fSourceFile: IIM_InterfaceFile;
		fDestFile: IIM_Convert;
		fFilterPackage: IIM_ConvertFilterPackage;
	public
	//-Object management----------------------------------------------------------
		constructor Create;
		destructor Destroy; override;
	//----------------------------------------------------------------------------
		property SourceFile: IIM_InterfaceFile read fSourceFile write fSourceFile;
		property DestFile: IIM_Convert read fDestFile write fDestFile;
		property FilterPackage: IIM_ConvertFilterPackage read fFilterPackage
			write fFilterPackage;

		procedure Execute;
		procedure CleanUp;
	end;

implementation

{ TConvertEngine }

procedure TConvertEngine.CleanUp;
begin
	fSourceFile := nil;
	fDestFile := nil;
	fFilterPackage := nil;
end;

constructor TConvertEngine.Create;
begin
	// Do nothing
end;

destructor TConvertEngine.Destroy;
begin
	// Do nothing
	inherited;
end;

procedure TConvertEngine.Execute;
begin
	fDestFile.ConvertFrom(fSourceFile, fFilterPackage);
end;

end.
