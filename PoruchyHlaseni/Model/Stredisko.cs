using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoruchyHlaseni.Model
{
    class Stredisko
    {
        public int Id { get;private set; }
        public string Name { get; private set; }

        public List<Stredisko> GetStrediska ()
        {
            List<Stredisko> strediskoList = new List<Stredisko>();

            Database db = new Database();

            DataTable data = db.getStredisko();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                Stredisko stredisko = new Stredisko();
                stredisko.Id = Convert.ToInt32(data.Rows[i]["stredisko_id"]);
                stredisko.Name = data.Rows[i]["stredisko_name"].ToString();
                strediskoList.Add(stredisko);
            }

            return strediskoList;
        }
    }
}
