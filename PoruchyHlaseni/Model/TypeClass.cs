using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoruchyHlaseni.Model
{
    class TypeClass
    {
        public int type_id { get;private set; }
        public string type_name { get; private set; }

        public List<TypeClass> GetTypes()
        {
            List<TypeClass> typeList = new List<TypeClass>();
            Database db = new Database();
            DataTable dt = db.getTypesOfError();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TypeClass typeClass = new TypeClass();
                typeClass.type_id = Convert.ToInt32(dt.Rows[i]["Types_Id"]);
                typeClass.type_name = dt.Rows[i]["Types_name"].ToString();
                typeList.Add(typeClass);
            }
            return typeList;
        }
    }
}
