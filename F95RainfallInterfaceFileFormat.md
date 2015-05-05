# Introduction #

This page describes the binary format used by USEPA/PDX SWMM 4.x for rainfall interface files

# Details #

## Fortran-95 Format ##

Standard Fortran binary files use the concept of records and record markers to delimit sections of data.  The record markers are in the form of 4-byte integers that describe the length of the record in bytes.  The record markers are found at the beginning and the end of the record.  These markers are used to facilitate "rewinding," i.e., when the program reading the file performs reverse-seeks.

## Header ##

RECORD
> Integer: Number of raingages

> Integer: Number of records (just use MAXINT)

> multiple, number of raingages of String (80 char): Raingage IDs

## Data ##

Incorporate as many data records as necessary.

RECORD

> Integer: Julian date (see above for format)

> Single: Seconds of day

> Single: Time step in seconds

> multiple, number of interface points of Single: Rain data in in/hr