using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MassTextProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKaistSafetyGeojsonPreprocessing_Click(object sender, EventArgs e)
        {
            KaistSafetyGeojsonPreprocessing(txtSrc.Text);
        }

        private void KaistSafetyGeojsonPreprocessing(string src)
        {
            lblStatus.Text = "KSGP Start.";
            DirectoryInfo di = new DirectoryInfo(src);
            foreach(DirectoryInfo subdi in di.GetDirectories()) { KaistSafetyGeojsonPreprocessing(subdi.FullName); }
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.FullName.IndexOf(".geojson") != -1)
                    fi.MoveTo(fi.FullName.Replace(".geojson", ".js"));

            }
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.FullName.IndexOf(".js") != -1)
                {
                    RedEyeEngine.CoreFramework REE_CF = new RedEyeEngine.CoreFramework();
                    //string data = String.Join("\r\n", REE_CF.ReadData(fi.FullName));
                    string data = File.ReadAllText(fi.FullName);

                    data = fi.Name.Replace("-", "_").Replace(".js", "") + ".push(" + data + ");";
                    //REE_CF.WriteData(fi.FullName, data, false);
                    File.WriteAllText(fi.FullName, data, Encoding.UTF8);
                }
            }
            lblStatus.Text = "KSGP Done.";
        }

        private void btnKSMG_BAP_Merge_Click(object sender, EventArgs e)
        {
            KKSMGBAPMerge(txtSrc.Text);
        }

        private void KKSMGBAPMerge(string src)
        {
            lblStatus.Text = "KSGP Start.";
            DirectoryInfo di = new DirectoryInfo(src);
            foreach (DirectoryInfo subdi in di.GetDirectories()) { KKSMGBAPMerge(subdi.FullName); }
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.FullName.IndexOf("mapdata-building.js") != -1)
                {
                    string data1 = File.ReadAllText(fi.FullName);
                    string data2 = File.ReadAllText(fi.FullName.Replace("mapdata-building.js", "mapdata-poi.js"));

                    string[] dts2 = System.Text.RegularExpressions.Regex.Split(data2, "type\": \"Feature\", \"properties");
                    Dictionary<string, string> dts2kv = new Dictionary<string, string>();
                    for (int i = 1; i < dts2.Length; i++)
                    {
                        string[] dts22 = System.Text.RegularExpressions.Regex.Split(dts2[i], "DTL_ADD_NM\": ");
                        dts22 = System.Text.RegularExpressions.Regex.Split(dts22[1], ",");
                        string address = dts22[0];

                        dts22 = System.Text.RegularExpressions.Regex.Split(dts2[i], "REF_CD\": ");
                        dts22 = System.Text.RegularExpressions.Regex.Split(dts22[1], ",");
                        string refcode = dts22[0].Replace("\"", "");

                        dts22 = System.Text.RegularExpressions.Regex.Split(dts2[i], "POI_NM\": ");
                        dts22 = System.Text.RegularExpressions.Regex.Split(dts22[1], " }");
                        string poi = dts22[0];

                        dts22 = System.Text.RegularExpressions.Regex.Split(dts2[i], "GEONICK\": ");
                        dts22 = System.Text.RegularExpressions.Regex.Split(dts22[1], " }");
                        string geonick = dts22[0];

                        if (data1.IndexOf("BUL_MAN_NO\": " + refcode + ", ") != -1)
                            data1 = data1.Replace("BUL_MAN_NO\": " + refcode + ", ", "BUL_MAN_NO\": " + refcode + ", \"DTL_ADD_NM\": " + address + ", \"POI_NM\": " + poi + ",  \"GEONICK\": " + geonick + ", ");
                    }

                    File.WriteAllText(fi.FullName, data1, Encoding.UTF8);
                }
            }
            lblStatus.Text = "KSGP Done.";
        }

        private void btnGeojsonLineMerge_Click(object sender, EventArgs e)
        {
            GeojsonLineMerge("");
        }

        public void GeojsonLineMerge(string src)
        {
            string data = File.ReadAllText("C:\\Users\\User\\Documents\\g4.geojson");
            JObject jdata = JObject.Parse(data);
            JArray jarray = (JArray)jdata["features"];

            JObject jlink = new JObject();
            Dictionary<string, List<string>> linkmap = new Dictionary<string, List<string>>();

            JObject jresult = new JObject();
            JArray jresultarr = new JArray();

            int nc = 0;

            //Dictionary
            foreach(JObject jobj in jarray)
            {
                JArray jar2 = (JArray)jobj["geometry"]["coordinates"];
                foreach (JArray jar3 in jar2)
                {
                    JArray bcar = null;
                    foreach (JArray car in jar3)
                    {
                        if(bcar != null)
                        {
                            nc++;
                            double distance = CalculateDistance(Convert.ToDouble(bcar[0]), Convert.ToDouble(bcar[1]), Convert.ToDouble(car[0]), Convert.ToDouble(car[1]));

                            JArray crd = new JArray();
                            crd.Add(bcar);
                            crd.Add(car);

                            //JArray lnk = new JArray();
                            //lnk.Add("");
                            //lnk.Add("");

                            JObject kvp = new JObject();
                            kvp.Add("id", (System.DateTime.Now.Ticks + nc).ToString());
                            kvp.Add("level", "-1");
                            kvp.Add("weight", distance);
                            kvp.Add("coordinates", crd);
                            kvp.Add("link", new JArray());

                            jresult.Add(kvp["id"].ToString(), kvp);
                            jresultarr.Add(kvp);

                            if (!linkmap.ContainsKey(car.ToString()))
                            {
                                linkmap.Add(car.ToString(), new List<string>());
                                linkmap[car.ToString()].Add(kvp["id"].ToString());

                                jlink.Add(car.ToString(), new JArray());
                                ((JArray)jlink[car.ToString()]).Add(kvp["id"].ToString());
                            }
                            else
                            {
                                bool vchk = false;
                                foreach(string value in linkmap[car.ToString()])
                                    if (value.Equals(kvp["id"].ToString())) { vchk = true; break; }
                                if (!vchk)
                                {
                                    linkmap[car.ToString()].Add(kvp["id"].ToString());
                                    ((JArray)jlink[car.ToString()]).Add(kvp["id"].ToString());
                                }
                            }

                            if (!linkmap.ContainsKey(bcar.ToString()))
                            {
                                linkmap.Add(bcar.ToString(), new List<string>());
                                linkmap[bcar.ToString()].Add(kvp["id"].ToString());

                                jlink.Add(bcar.ToString(), new JArray());
                                ((JArray)jlink[bcar.ToString()]).Add(kvp["id"].ToString());
                            }
                            else
                            {
                                bool vchk = false;
                                foreach (string value in linkmap[bcar.ToString()])
                                    if (value.Equals(kvp["id"].ToString())) { vchk = true; break; }
                                if (!vchk)
                                {
                                    linkmap[bcar.ToString()].Add(kvp["id"].ToString());
                                    ((JArray)jlink[bcar.ToString()]).Add(kvp["id"].ToString());
                                }
                            }
                        }

                        bcar = car;
                        txtResult.AppendText(car[0].ToString() + "\r\n");

                    }
                }
            }

            foreach (KeyValuePair<string, JToken> kvp in jresult) 
            {
                string id = (string)((JObject)kvp.Value)["id"];
                JObject jobj = (JObject)kvp.Value;
                JArray jar = (JArray)((JObject)kvp.Value)["coordinates"];
                JArray jlnk = new JArray();
                for (int i = 0; i < jar.Count; i++)
                {
                    foreach(string lnk in linkmap[jar[i].ToString()])
                        if (!lnk.Equals(id))
                            jlnk.Add(lnk.ToString());
                }
                if (jlnk.Count == 1)
                    jlnk.Add("EOL");
                jobj["link"] = jlnk;
                jresult[id] = jobj;
            }

            for(int ik = 0; ik < jresultarr.Count; ik++) 
            {
                JToken kvp = jresultarr[ik];
                string id = (string)((JObject)kvp)["id"];
                JObject jobj = (JObject)kvp;
                JArray jar = (JArray)((JObject)kvp)["coordinates"];
                JArray jlnk = new JArray();
                for (int i = 0; i < jar.Count; i++)
                {
                    foreach (string lnk in linkmap[jar[i].ToString()])
                        if (!lnk.Equals(id))
                            jlnk.Add(lnk.ToString());
                }
                if (jlnk.Count == 1)
                    jlnk.Add("EOL");
                jobj["link"] = jlnk;
                jresultarr[ik] = jobj;
            }

            File.WriteAllText("C:\\Users\\User\\Documents\\g3_result.json", jresult.ToString());
            File.WriteAllText("C:\\Users\\User\\Documents\\g3_result_arr.json", jresultarr.ToString());
            File.WriteAllText("C:\\Users\\User\\Documents\\g3_result_map.json", jlink.ToString());

            MessageBox.Show(txtResult.Lines.Count().ToString());
        }
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // 지구의 반지름 (km)
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // 거리 (km)
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
