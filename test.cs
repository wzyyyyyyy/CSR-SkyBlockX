using CSR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;

namespace cland
{
    public class cland 
    {
        public static void c(MCCSAPI api) 
        {
            Dictionary<string, string> uuid = new Dictionary<string, string>();
            Dictionary<string, int> xx = new Dictionary<string, int>();
            Dictionary<string, int> yy = new Dictionary<string, int>();
            Dictionary<string, int> zz = new Dictionary<string, int>();
            _ = api.addBeforeActListener(EventKey.onInputCommand, x =>
              {
                  var a = BaseEvent.getFrom(x) as InputCommandEvent;
                  bool re = true;
                  if (a.cmd.StartsWith("/is "))
                  {
                      string sx = a.cmd.Substring(4);
                      switch (sx)
                      {
                          case "seta":
                              var b = Tools.Player.getPlayerPermissionAndGametype(api, uuid[a.playername]);
                              if (Int32.Parse(b.oplevel) >= 2)
                              {
                                  if (xx.ContainsKey(a.playername) != true)
                                  {
                                      xx.Add(a.playername, (int)a.XYZ.x);
                                      yy.Add(a.playername, (int)a.XYZ.y);
                                      zz.Add(a.playername, (int)a.XYZ.z);
                                  }
                              }
                              break;
                          case "setb":
                              var c = Tools.Player.getPlayerPermissionAndGametype(api, uuid[a.playername]);
                              if (Int32.Parse(c.oplevel) >= 2)
                              {
                                  if (xx.ContainsKey(a.playername))
                                  {
                                       string stru = api.getStructure(a.dimensionid, "{\"x\":" + xx[a.playername] + ",\"y\":" + yy[a.playername] + ",\"z\":" + zz[a.playername] + "}", "{\"x\":" + (int)a.XYZ.x + ",\"y\":" + (int)a.XYZ.y + ",\"z\":" + (int)a.XYZ.z + "}", true, true);
                                       int temp = 0;
                                      while (true)
                                      {
                                          if (File.Exists("./data/skyblock" + temp.ToString() + ".txt"))
                                          {
                                              temp++;
                                          }
                                          else
                                          {
                                              temp++;
                                              File.AppendAllText("./data/skyblock" + temp.ToString() + ".txt", stru);
                                              break;
                                          }
                                      }
                                  }
                              }
                              break;
                      }
                  }
                  return re;
              });
        }
    }
}