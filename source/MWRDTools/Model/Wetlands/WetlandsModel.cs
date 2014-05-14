using System.Collections.Generic;
using System.Data;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;

namespace MWRDTools.Model {
  public class WetlandsModel : AbstractMWRDModel, IWetlandsModel
  {
    public string[] getGaugeNames() {
      List<string> names = bridge.GetColValuesForQuery<string>(
        Constants.TableName.Gauge,
        null,
        "GaugeName"
      );

      return names.ToArray();
    }

    public string[] getWaggaFlowThresholds() {
      return new string[] { "35,000", "47,000", "> 47,000" };
    }

    public string GetGaugeNameForId(long gaugeId) {
      return bridge.GetFirstColValueForQuery<string>(
          Constants.TableName.Gauge,
          string.Format("GaugeID = {0}", gaugeId),
          "GaugeName"
      );
    }

    public DataTable GetInundatedWetlandsByWaggaFlowThreshold(string flowThreshold) {

      DataTable wetlands;

      using (ComReleaser comReleaser = new ComReleaser()) {

        string whereClause = "";
        switch (flowThreshold) {
          case "> 47,000": {
            whereClause = "Flow = '> 47000' OR Flow = '47000' OR Flow = '35000'";
            break;
          }
          case "47,000": {
            whereClause = "Flow = '47000' OR Flow = '35000'";
            break;
          }
          case "35,000": {
            whereClause = "Flow = '35000'";
            break;
          }
        }

        ICursor wetlandCursor = bridge.GetCursorForQuery(
          Constants.TableName.MCMAWetlands,
          whereClause
        );

        comReleaser.ManageLifetime(wetlandCursor);

        wetlands = Common.CursorToDataTable(wetlandCursor);

      } // using comReleaser

      return wetlands;
    }

    public DataTable GetInundatedWetlandsByFlowAtGauge(long gaugeId, double flow) {
      return GetInundatedWetlandsByFlowAtGauge(
        GetGaugeNameForId(gaugeId), 
        flow
      );
    }

    public DataTable GetInundatedWetlandsByFlowAtGauge(string gaugeName, double flow) {
      DataTable wetlands;

      using (ComReleaser comReleaser = new ComReleaser()) {

        ICursor wetlandCursor = bridge.GetCursorForQuery(
          string.Format(
            "{0}, {1}",
            Constants.TableName.CommenceToFill,
            Constants.TableName.MCMAWetlands
          ),
          string.Format(
            "{0}.WetlandsID = {1}.WetlandsID AND {0}.Flow_mL < {2} AND {0}.GaugeName = '{3}'",
            Constants.TableName.CommenceToFill,
            Constants.TableName.MCMAWetlands,
            flow, gaugeName
          ),
          string.Format("{0}.*", Constants.TableName.MCMAWetlands)
        );

        comReleaser.ManageLifetime(wetlandCursor);

        wetlands = Common.CursorToDataTable(wetlandCursor);

      } // using comReleaser

      return wetlands;
    }

  }
}
