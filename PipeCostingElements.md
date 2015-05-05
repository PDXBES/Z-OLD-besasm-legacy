# Introduction #

When costing pipes, there are various components that are considered when coming up with the final per-foot cost of constructing a pipe.  These were developed from Beech-Essex TM 4.5.

# Details #

**Trench zones**

![http://wiki.besasm-legacy.googlecode.com/hg/images/CostEstimatorTrenchZones.png](http://wiki.besasm-legacy.googlecode.com/hg/images/CostEstimatorTrenchZones.png)

  * A: Material
  * B: Inside Diameter
  * C: Outsider Diameter = f(B) _table_
  * D: Depth
  * E: Manhole Diameter = f(B) _table_
  * F: Trench Base Width = f(C) = g(f(B)) _table_
  * G: Asphalt Removal Width = F + 1 ft
  * H: Asphalt Removal Surface Area = G\*1 ft -> sqyd
  * I: Asphalt Patch Surface Area = H
  * J: Excavation Volume = D\*F
  * K: Spoils Volume = J\*1.2
  * L: Pipe Zone Depth = f(F) = g(f(C)) = 0.5 + 1 + C(in ft)
  * M: Pipe Volume = pi/4\*C^2
  * N: Shoring = D
  * O: Pipe Zone Volume = f(F,L,M) = (F\*L (in cuyd)) - M -> cuyd
  * P: AC Base Volume = G`*`(2/3 ft)/27
    * _assumes 8 in. thickness_
  * Q: Above Zone Volume = f(J,M,O,P) = J-M-O-P
  * R: Direct Construction (per ft)
    * Trenched
      * Sawcutting AC Pavement = 4 ft (use unit price)
      * Asphalt Removal = H sqyd (use unit price)
      * Trench Excavation = J cuyd (use unit price)
      * Truck Haul Excavation Spoils = K cuyd (use unit price)
      * Trench Shoring = N sqft (use unit price)
      * New Pipe = table (material vs. diameter)
        * Base Cost = table(B), for Diameter < 78 in.
          * for depth < 18 ft, use base cost
          * for 18 ft <= depth < 24 ft, use 1.1\*base cost
          * for 24 ft <= depth, use 1.2\*base cost
        * For Diameter >= 78 in.
          * for depth < 18 ft, use base cost
          * for 18 ft <= depth < 20 ft, use 1.1\*base cost
          * for 20 ft <= depth < 24 ft, use 1.2\*base cost
          * for 24 <= depth, use 1.25\*base cost
    * Trenchless-CIPP
      * Liner Cost = f(B) _table_
      * Lateral = $10 for B <= 24 in, $15 for B > 24 in
      * Flow Diversion = f(B) _table_
      * TV Cleaning = $5 for B <= 24 in, $10 for B > 24 in
    * Trenchless-Pipeburst
      * Liner Cost = f(B) _table_
      * Lateral = $15