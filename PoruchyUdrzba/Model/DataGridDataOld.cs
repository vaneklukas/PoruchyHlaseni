using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoruchyUdrzba.Model
{
    class DataGridDataOld
    {
        Database db = new Database();

        public int poruchy_Id { get; set; }
        public string Stredisko { get; set; }
        public string Machine { get; set; }
        public string Type { get; set; }
        public string CommentOp { get; set; }
        public string CommentMt { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string Mainten { get; set; }
        public string Operator { get; set; }
        public string StartTime { get; set; }
        public string MaintOp { get; set; }
        public List<DataGridDataOld> getData()
        {
            List<DataGridDataOld> data = new List<DataGridDataOld>();
            DataTable dt = db.dataTableOld();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridDataOld lw = new DataGridDataOld();
                lw.poruchy_Id = Convert.ToInt32(dt.Rows[i]["poruchy_ID"]);
                lw.Stredisko = dt.Rows[i]["stredisko"].ToString();
                lw.Machine = dt.Rows[i]["machine"].ToString();
                lw.Type = dt.Rows[i]["type"].ToString();
                lw.CommentOp = dt.Rows[i]["commentOp"].ToString();
                lw.OpenTime = dt.Rows[i]["startTime"].ToString();
                lw.Operator = dt.Rows[i]["operator"].ToString();
                lw.StartTime = dt.Rows[i]["openMan"].ToString();
                lw.MaintOp = dt.Rows[i]["MaintOp"].ToString();
                lw.CloseTime= dt.Rows[i]["finishTime"].ToString();
                lw.CommentMt= dt.Rows[i]["commentM"].ToString();
                lw.Mainten = dt.Rows[i]["maintenance"].ToString();
                data.Add(lw);
            }
            return data;
        }
    }
}
