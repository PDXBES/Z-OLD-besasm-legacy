unit EMGAATS_TLB;

// ************************************************************************ //
// WARNING                                                                    
// -------                                                                    
// The types declared in this file were generated from data read from a       
// Type Library. If this type library is explicitly or indirectly (via        
// another type library referring to this type library) re-imported, or the   
// 'Refresh' command of the Type Library Editor activated while editing the   
// Type Library, the contents of this file will be regenerated and all        
// manual modifications will be lost.                                         
// ************************************************************************ //

// PASTLWTR : 1.2
// File generated on 2/24/2011 7:45:59 AM from Type Library described below.

// ************************************************************************  //
// Type Lib: Z:\Development\besasm-legacy\Delphi\EMGAATS Shell\EMGAATS.tlb (1)
// LIBID: {0A9ED862-15C6-11D6-88EF-00B0D0756CA8}
// LCID: 0
// Helpfile: 
// HelpString: EMGAATS Library
// DepndLst: 
//   (1) v2.0 stdole, (C:\WINDOWS\system32\stdole2.tlb)
// ************************************************************************ //
{$TYPEDADDRESS OFF} // Unit must be compiled without type-checked pointers. 
{$WARN SYMBOL_PLATFORM OFF}
{$WRITEABLECONST ON}
{$VARPROPSETTER ON}
interface

uses Windows, ActiveX, Classes, Graphics, StdVCL, Variants;
  

// *********************************************************************//
// GUIDS declared in the TypeLibrary. Following prefixes are used:        
//   Type Libraries     : LIBID_xxxx                                      
//   CoClasses          : CLASS_xxxx                                      
//   DISPInterfaces     : DIID_xxxx                                       
//   Non-DISP interfaces: IID_xxxx                                        
// *********************************************************************//
const
  // TypeLibrary Major and minor versions
  EMGAATSMajorVersion = 1;
  EMGAATSMinorVersion = 0;

  LIBID_EMGAATS: TGUID = '{0A9ED862-15C6-11D6-88EF-00B0D0756CA8}';

  IID_ICobjMICB: TGUID = '{0A9ED863-15C6-11D6-88EF-00B0D0756CA8}';
  CLASS_CobjMICB: TGUID = '{0A9ED865-15C6-11D6-88EF-00B0D0756CA8}';
type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  ICobjMICB = interface;
  ICobjMICBDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  CobjMICB = ICobjMICB;


// *********************************************************************//
// Interface: ICobjMICB
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {0A9ED863-15C6-11D6-88EF-00B0D0756CA8}
// *********************************************************************//
  ICobjMICB = interface(IDispatch)
    ['{0A9ED863-15C6-11D6-88EF-00B0D0756CA8}']
    procedure setstatustext(const txt: WideString); safecall;
  end;

// *********************************************************************//
// DispIntf:  ICobjMICBDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {0A9ED863-15C6-11D6-88EF-00B0D0756CA8}
// *********************************************************************//
  ICobjMICBDisp = dispinterface
    ['{0A9ED863-15C6-11D6-88EF-00B0D0756CA8}']
    procedure setstatustext(const txt: WideString); dispid 1;
  end;

// *********************************************************************//
// The Class CoCobjMICB provides a Create and CreateRemote method to          
// create instances of the default interface ICobjMICB exposed by              
// the CoClass CobjMICB. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCobjMICB = class
    class function Create: ICobjMICB;
    class function CreateRemote(const MachineName: string): ICobjMICB;
  end;

implementation

uses ComObj;

class function CoCobjMICB.Create: ICobjMICB;
begin
  Result := CreateComObject(CLASS_CobjMICB) as ICobjMICB;
end;

class function CoCobjMICB.CreateRemote(const MachineName: string): ICobjMICB;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_CobjMICB) as ICobjMICB;
end;

end.
