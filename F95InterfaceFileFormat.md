# Introduction #

This page describes the binary format used by USEPA/PDX SWMM 4.x


# Details #

## Fortran-95 Format ##

Standard Fortran binary files use the concept of records and record markers to delimit sections of data.  The record markers are in the form of 4-byte integers that describe the length of the record in bytes.  The record markers are found at the beginning and the end of the record.  These markers are used to facilitate "rewinding," i.e., when the program reading the file performs reverse-seeks. Note that since record markers are found at the beginning and end of a record, you will find one record marker at the beginning and end of the file, but you will find _two_ record markers between all records.

## Header ##

RECORD
> String (80 char): Title 1
RECORD
> String (80 char): Title 2
RECORD
> Integer: Beginning of interface file Julian date (YYYYDDD, 4-digit year and days from beginning of year; 2011365 = December 31, 2011)

> Single: Beginning of interface file, Seconds of day
RECORD
> String (80 char): Title 3 (usually the previous run's Title 1)
RECORD
> String (80 char): Title 4 (usually the previous run's Title 2)
RECORD
> String (20 char): Originating source block

> Integer: Number of interface points

> Integer: Number of pollutants (normally 0 in PDX applications)

> Single: Total modeled area generating the interface file data

> Integer: 0 = do not use Alphanumeric IDs for interface points; 1 = use Alphanumeric IDs
RECORD
> multiple, number of interface points of String (10 char)
RECORD
> Single: Flow multiplier; all flows in data below will be multiplied by this value

## Data ##

Incorporate as many data records as necessary.

RECORD
> Integer: Julian date (see above for format)

> Single: Seconds of day

> Single: Time step in seconds

> multiple, number of interface points of Single: Flow data in cfs