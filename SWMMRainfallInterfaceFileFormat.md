# Introduction #

Add your content here.


# Details #

## Specification ##

The Rainfall Interface File in SWMM is a binary format as follows:

  * Integers here are of the 32-bit variety (4 bytes).

  * These interface files have FORTRAN-specific record markers.  Each record has a BOR (Beginning of Record) marker that indicates how long the next record is.  Each record also has an EOR (End of Record) marker that indicates how long the previous record is.  This allows FORTRAN programs to REWIND and READ records.  The Header below is considered a single record.  Therefore, in laying out the binary file, the interface file is:

> `BOR,Header,EOR,BOR,Rainfall Record 1,EOR,BOR,Rainfall Record 2,EOR ...And so on...`

### Header ###
  * **A**: Integer: Number of gages in file
  * **B**: Integer: Maximum possible integer.  This actually controls the number of records to read, but SWMM uses the lesser of the actual number of records in the file or this number.  For simplicity, just set this to the Maximum Possible Integer (i.e. 2 billion +).
  * **C**: Bunch of left-padded 8-character strings denoting the raingage IDs.  Provide as many strings as there are gages (as specified in A above)

### Record ###
  * **D**: For each rainfall record, provide the following:
    * **D.1**: Integer: Julian date of the record (YYYYDDD, where YYYY is the year and DDD is the day of the year; for example, the Julian Date for December 31, 2004, would be 2004366).
    * **D.2**: Single (4-byte floating point): The number of seconds of the day specified in D.1
    * **D.3**: Single: The time step length, in seconds
    * **D.4**: Bunch of Singles: Provide as many rainfall intensities as there are gages.  These intensities must be in inches per hour.

## Sample ##

In text form, a sample interface file would look like this:

```
16,1,2147483647,'    1234',16,16,2000180,0.0,300.0,0.12,16,16,2000180,300.0,300.0,0.24,16
```

The first 5 terms
`16,1,2147483647,'    1234',16`
are the header of the interface.

The last twelve terms
`16,2000180,0.0,300.0,0.12,16,16,2000180,300.0,300.0,0.24,16`
are two rainfall records.