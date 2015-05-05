# Introduction #

This page describes the binary format used by XP-SWMM since 8.5

# Details #

## Binary Format ##

XP-SWMM uses a flat binary format with no record markers.

## Header ##

> String (80 char): Title 1

> String (80 char): Title 2

> Integer: Beginning of interface file Julian date (YYYYDDD, 4-digit year and days from beginning of year; 2011365 = December 31, 2011)

> Double: Beginning of interface file, Seconds of day

> String (80 char): Title 3 (usually the previous run's Title 1)

> String (80 char): Title 4 (usually the previous run's Title 2)

> String (20 char): Originating source block

> Integer: Number of interface points

> Integer: Number of pollutants (normally 0 in PDX applications)

> Double: Total modeled area generating the interface file data

> multiple, number of interface points of String (10 char): Interface point IDs

> Double: Flow multiplier; all flows in data below will be multiplied by this value

## Data ##

Incorporate as many data records as necessary.

> Integer: Julian date (see above for format)

> Double: Seconds of day

> Double: Time step in seconds

> multiple, number of interface points of Double: Flow data in cfs