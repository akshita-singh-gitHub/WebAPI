using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        SqlConnection con = new SqlConnection(@"server=SURFACE\SQLEXPRESS; database=webapi; Integrated Security=true;");
        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            //return new string[] { "value1", "value2" };
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM user_detail", con);
            DataTable dt =new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                return  JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "no data found";
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //  return "value";
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM user_detail WHERE id='"+id+"' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "no data found";
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public string Post([FromBody] string value)

        {
            SqlCommand cmd=new SqlCommand("Insert into user_detail(Name) VALUES('"+value+"')", con);
            con.Open(); 
           int i= cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "record inserted successfully";
            }
            else
            {
                return "Try again .No data inserted";
            }

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand("UPDATE user_detail SET Name ='" + value+"' WHERE ID = '" + id+ "' ", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "record updated with the value as "+ value+" and id as "+id;
            }
            else
            {
                return "Try again .No data inserted";
            }

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE user_detail WHERE ID ='" + id + "'  ", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "record deleted with the id as "  + id;
            }
            else
            {
                return "Try again .No data deeted";
            }

        }
    }
}
