using System;
using System.Data;

namespace MWRDTools.Model 
{
  public interface IThreatenedSpeciesModel
  {
    event EventHandler<ModelStatusEventArgs> StatusChanged;
    void OverwriteSightingData(DataTable data);
  }
}
