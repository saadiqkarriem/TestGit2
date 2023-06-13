using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Personal_Info_API;
using System.Data.SqlClient;


namespace PersonalInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class PersonalinfoController : ControllerBase
    {
        // GET: api/<userController>
        [HttpGet]
        public IActionResult Get()
        {
            string connectionString = @"Data Source=DESKTOP-5NBKNN5;Initial Catalog=Details;Integrated Security=True;";
            string query = "Select * from contact";
            List<Contact> adduser = new List<Contact>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contact user = new Contact();
                                user.Name = reader.GetString(0);
                                user.LastName = reader.GetString(1);
                                user.Email = reader.GetString(2);
                                user.CellNumber = reader.GetString(3);
                                adduser.Add(user);
                            }
                        }
                    }
                }
                return Ok(adduser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET api/<userController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // POST api/<userController>

        [HttpPost("{contact}")]
        public IActionResult Post([FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            string connectionString = @"Data Source=DESKTOP-5NBKNN5;Initial Catalog=Details;Integrated Security=True;";
            string query = "INSERT INTO contact (Name, LastName, Email, CellNumber) VALUES (@Name, @LastName, @Email, @CellNumber)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", contact.Name);
                        command.Parameters.AddWithValue("@LastName", contact.LastName);
                        command.Parameters.AddWithValue("@Email", contact.Email);
                        command.Parameters.AddWithValue("@CellNumber", contact.CellNumber);
                        command.ExecuteNonQuery();
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<userController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }
        // DELETE api/<userController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}