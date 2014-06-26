/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

public static class Constants
{
  public const string OID = "OID";
  
  public static class LayerName
  {
    public const string WetLands = "WetLands"; 
    public const string Gauge = "Gauge";
    public const string ThreatenedSpecies = "ThreatenedSpecies";
  }

  public static class TableName
  {
    public const string Gauge = "Gauge";
    public const string CommenceToFill = "CommenceToFill";
    public const string MCMAWetlands = "MCMAWetlands";

    public const string ThreatenedSpecies = "ThreatenedSpecies";
    public const string ThreatenedSpeciesUnique = "ThreatenedSpeciesUnique";

    public const string CARM_Locations_Gauges = "CARM_Locations_Gauges";

    public const string CARM_time_series_group = "CARM_time_series_group";
    public const string CARM_time_series = "CARM_time_series";
    public const string CARM_time_series_value = "CARM_time_series_value";
  }
}
