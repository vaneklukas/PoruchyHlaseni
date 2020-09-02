using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoruchyUdrzba.Model
{
    class DataGridData
    {
        Database db = new Database();

        public int poruchy_Id  { get; set; }
        public string Stredisko { get; set; }
        public string Machine { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public string OpenTime { get; set; }
        public string Operator { get; set; }
        public string StartTime { get; set; }
        public string MaintOp { get; set; }
        public List<DataGridData> getData()
        {
            List<DataGridData> data = new List<DataGridData>();
            DataTable dt = db.dataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridData lw = new DataGridData();
                lw.poruchy_Id = Convert.ToInt32(dt.Rows[i]["poruchy_ID"]);
                lw.Stredisko = dt.Rows[i]["stredisko"].ToString();
                lw.Machine = dt.Rows[i]["machine"].ToString();
                lw.Type = dt.Rows[i]["type"].ToString();
                lw.Comment= dt.Rows[i]["commentOp"].ToString();
                lw.OpenTime = dt.Rows[i]["startTime"].ToString();
                lw.Operator = dt.Rows[i]["operator"].ToString();
                string a = dt.Rows[i]["openMan"].ToString();

                if (!String.IsNullOrEmpty(a))
                {
                    lw.StartTime = dt.Rows[i]["openMan"].ToString();
                    lw.MaintOp = dt.Rows[i]["MaintOp"].ToString();
                }
                else
                {
                    lw.StartTime = "";
                    lw.MaintOp = "";
                }
                data.Add(lw);
            }


            return data;
        }


    }
}
