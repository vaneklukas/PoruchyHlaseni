using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoruchyHlaseni.Model
{
    class Machine
    {
        public int machine_id { get; private set; }
        public string machine_name { get; private set; }
        public int stredisko_id { get; private set; }

        public List<Machine> GetMachines()
        {
            List<Machine> machineList = new List<Machine>();
            Database db = new Database();
            DataTable dt = db.getMachines();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Machine machine = new Machine();
                machine.machine_id = Convert.ToInt32(dt.Rows[i]["machine_id"]);
                machine.machine_name = dt.Rows[i]["machine_name"].ToString();
                machine.stredisko_id = Convert.ToInt32(dt.Rows[i]["stredisko_id"]);
                machineList.Add(machine);
            }
            return machineList;
        }
    }
}
